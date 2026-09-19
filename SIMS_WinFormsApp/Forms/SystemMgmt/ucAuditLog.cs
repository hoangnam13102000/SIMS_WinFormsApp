using FontAwesome.Sharp;
using SIMS_WinFormsApp.Infrastructure.Composition;
using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.Models.Mapping;
using SIMS_WinFormsApp.MVP.Presenters;
using SIMS_WinFormsApp.Repositories.Interfaces;
using SIMS_WinFormsApp.Services.Implementations.Export;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Controls.Filter;
using SIMS_WinFormsApp.UI.Controls.Loading;
using SIMS_WinFormsApp.UI.Controls.Pagination;
using SIMS_WinFormsApp.UI.Controls.Search;
using SIMS_WinFormsApp.UI.Controls.Toast;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Forms.SystemMgmt
{
    /// <summary>
    /// Trang "Nhật ký hệ thống" - nhúng vào MainLayoutControl như 1 mục sidebar (giống
    /// ucDashboard/ucMyProfile), View trong mô hình MVP cho <see cref="AuditLogPresenter"/>.
    ///
    /// Toàn bộ mảnh ghép UI đều TÁI SỬ DỤNG control có sẵn của dự án, không viết lại: StatCard
    /// (dashboard), SearchBarControl+SearchPresenter, FilterComboBox, PaginationControl,
    /// LoadingOverlayHost (đã dựng ở tính năng trước), OverflowMenuButton+ModernDropdownMenu
    /// (đang dùng ở BaseTable), TableExportRunner+CsvTableExporter+ExcelTableExporter (đang dùng
    /// ở ManagementTablePage). Chỉ có cách GHÉP các mảnh này lại theo đúng bố cục màn hình là mới.
    ///
    /// Dữ liệu chỉ được tải LẦN ĐẦU khi trang thật sự hiển thị (VisibleChanged), không tải ngay
    /// lúc dựng layout ứng dụng (constructor) - tránh làm chậm khởi động app với 1 trang quản trị
    /// ít khi được mở tới, và để LoadingOverlayHost có cơ hội hiển thị trước khi câu truy vấn
    /// (có thể mất vài giây) chạy.
    /// </summary>
    public sealed class ucAuditLog : UserControl, IAuditLogView
    {
        private readonly IAuditLogRepository _repository;
        private readonly AuditLogPresenter _presenter;
        private LoadingOverlayHost _overlayHost;

        private SearchBarControl _searchBar;
        private SearchPresenter _searchPresenter;
        private FilterComboBox _filterAction;
        private FilterComboBox _filterTable;
        private DateTimePicker _dtFrom;
        private DateTimePicker _dtTo;
        private Label _lblTotalCount;
        private PrimaryButton _btnTabAudit;
        private PrimaryButton _btnTabIncident;
        private bool _incidentOnly;

        private DataGridView _grid;
        private BaseTable _table;
        private int _actionColIndex, _tableColIndex, _viewColIndex;
        private IReadOnlyList<AuditLogRowDto> _currentRows = Array.Empty<AuditLogRowDto>();

        private bool _dataLoadedOnce;

        public ucAuditLog(IAuditLogRepository repository = null)
        {
            AutoScaleMode = AutoScaleMode.None;
            Font = new Font("Segoe UI", 9f);
            DoubleBuffered = true;
            Dock = DockStyle.Fill;
            BackColor = AppColors.PageBg;
            Padding = new Padding(20, 16, 20, 20);

            _repository = repository ?? AppComposition.CreateAuditLogRepository();

            BuildUI();

            _presenter = new AuditLogPresenter(this, _repository, _overlayHost);

            VisibleChanged += (s, e) =>
            {
                if (Visible && !_dataLoadedOnce)
                {
                    _dataLoadedOnce = true;
                    _presenter.Load();
                }
            };
        }

        #region IAuditLogView
        public AuditLogQuery CurrentQuery => new AuditLogQuery
        {
            PageIndex = _table.PageIndex,
            PageSize = _table.PageSize,
            Search = _searchBar.Text,
            ActionFilter = _filterAction.SelectedOption?.Value,
            TableFilter = _filterTable.SelectedOption?.Value,
            IncidentOnly = _incidentOnly,
            FromDate = _dtFrom.Checked ? _dtFrom.Value.Date : (DateTime?)null,
            ToDate = _dtTo.Checked ? _dtTo.Value.Date : (DateTime?)null
        };

        public event EventHandler QueryChanged;
        public event EventHandler<long> DetailRequested;

        public void DisplayStats(AuditLogStatsDto stats)
        {
            _statTotal.ValueText = stats.TotalCount.ToString();
            _statToday.ValueText = stats.TodayCount.ToString();
            _statFailedLogin.ValueText = stats.FailedLoginCount.ToString();
            _statActiveUsers.ValueText = stats.ActiveUserCount.ToString();
        }

        public void DisplayRows(IReadOnlyList<AuditLogRowDto> rows, int totalCount)
        {
            _currentRows = rows ?? Array.Empty<AuditLogRowDto>();

                var displayRows = _currentRows.Select(r => new object[]
                {
                    r.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss"),
                    r.Username,
                    AuditLogDisplayMapper.GetActionLabel(r.Action),
                    AuditLogDisplayMapper.GetTableLabel(r.TableName),
                    string.IsNullOrWhiteSpace(r.Detail) ? "-" : r.Detail,
                    "Xem"
                }).ToList();
                _table.SetPageResult(displayRows, totalCount);

            _lblTotalCount.Text = "Tổng cộng: " + totalCount + " nhật ký";
        }

        public void DisplayActionOptions(IReadOnlyList<string> actions)
        {
            _filterAction.OptionChanged -= OnFilterOptionChanged;
            var options = new List<FilterOption> { new FilterOption("Tất cả hành động", null) };
            options.AddRange((actions ?? Array.Empty<string>())
                .Select(a => new FilterOption(AuditLogDisplayMapper.GetActionLabel(a), a)));
            _filterAction.SetOptions(options);
            _filterAction.OptionChanged += OnFilterOptionChanged;
        }

        public void DisplayTableOptions(IReadOnlyList<string> tables)
        {
            _filterTable.OptionChanged -= OnFilterOptionChanged;
            var options = new List<FilterOption> { new FilterOption("Tất cả đối tượng", null) };
            options.AddRange((tables ?? Array.Empty<string>())
                .Select(t => new FilterOption(AuditLogDisplayMapper.GetTableLabel(t), t)));
            _filterTable.SetOptions(options);
            _filterTable.OptionChanged += OnFilterOptionChanged;
        }

        public void ShowDetail(AuditLogDetailDto detail) => frmAuditLogDetail.Show(FindForm(), detail);

        public void ShowError(string message) => AppToast.Error(this, message);

        private void OnFilterOptionChanged(object sender, EventArgs e)
        {
            // ResetToFirstPage() đã tự phát PageChanged (đăng ký ở constructor để chuyển tiếp
            // thành QueryChanged) - không cần raise QueryChanged thêm 1 lần ở đây nữa.
            ResetToFirstPage();
        }
        #endregion

        #region Dựng giao diện (chỉ chạy 1 lần lúc khởi tạo)
        private StatCard _statTotal, _statToday, _statFailedLogin, _statActiveUsers;

        private void BuildUI()
        {
            SuspendLayout();

            var filterSurface = new Panel { Dock = DockStyle.Fill, BackColor = AppColors.White };
            filterSurface.Controls.Add(BuildFilterBar());
            filterSurface.Controls.Add(BuildTabsRow());
            filterSurface.Controls.Add(BuildStatsRow());

            var overflowActions = new List<OverflowMenuAction>
            {
                new OverflowMenuAction("Xuất CSV", IconChar.FileCsv, () => ExportAudit("csv")),
                new OverflowMenuAction("Xuất Excel", IconChar.FileExcel, () => ExportAudit("excel"))
            };

            _table = new BaseTable(
                "Nhật ký hệ thống",
                "Lịch sử thao tác người dùng và ghi nhận sự cố hệ thống",
                IconChar.ClockRotateLeft,
                new[] { "Thời gian", "Người dùng", "Hành động", "Đối tượng", "Mô tả", "Thao tác" },
                (pageIndex, pageSize, search, filter) => new TablePageResult(),
                pageSize: 10,
                overflowActions: overflowActions,
                customFilterPanel: filterSurface,
                customFilterHeight: 252,
                externalData: true);
            _grid = _table.Grid;
            ConfigureAuditGrid();
            _table.PageChanged += (s, e) => QueryChanged?.Invoke(this, EventArgs.Empty);
            _overlayHost = new LoadingOverlayHost(_table) { Dock = DockStyle.Fill };
            Controls.Add(_overlayHost);
            ResumeLayout(true);
        }

        private void ResetToFirstPage()
        {
            _table?.ResetToFirstPage();
        }

        private Control BuildStatsRow()
        {
            var row = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 100,
                ColumnCount = 4,
                RowCount = 1,
                Margin = new Padding(0, 16, 0, 0),
                BackColor = Color.Transparent
            };
            for (int i = 0; i < 4; i++) row.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            row.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

            _statTotal = CreateStat("0", "Tổng nhật ký", IconChar.ClockRotateLeft, AppColors.Accent, AppColors.AccentBgSoft);
            _statToday = CreateStat("0", "Hoạt động hôm nay", IconChar.Bolt, AppColors.Success, AppColors.SuccessBg);
            _statFailedLogin = CreateStat("0", "Đăng nhập thất bại", IconChar.TriangleExclamation, AppColors.Error, AppColors.ErrorBg);
            _statActiveUsers = CreateStat("0", "Người dùng hoạt động", IconChar.Users, AppColors.Warning, AppColors.WarningBg);

            row.Controls.Add(Wrap(_statTotal, 0), 0, 0);
            row.Controls.Add(Wrap(_statToday, 12), 1, 0);
            row.Controls.Add(Wrap(_statFailedLogin, 12), 2, 0);
            row.Controls.Add(Wrap(_statActiveUsers, 12), 3, 0);
            return row;

            Control Wrap(StatCard c, int leftGap)
            {
                c.Dock = DockStyle.Fill;
                c.Margin = new Padding(leftGap, 0, 0, 0);
                return c;
            }
        }

        private static StatCard CreateStat(string value, string title, IconChar icon, Color iconColor, Color iconBg) =>
            new StatCard
            {
                ValueText = value,
                TitleText = title,
                TrendText = string.Empty,
                Icon = icon,
                IconColor = iconColor,
                IconBackground = iconBg,
                TopBorderColor = iconColor,
                Dock = DockStyle.Fill
            };

        private void ConfigureAuditGrid()
        {
            _grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            _grid.ColumnHeadersHeight = 44;
            _grid.RowTemplate.Height = 52;
            _grid.CellBorderStyle = DataGridViewCellBorderStyle.None;
            _grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            _grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            _grid.ColumnHeadersDefaultCellStyle.Font = AppFonts.SmallBold;
            _grid.DefaultCellStyle.Font = AppFonts.Body;
            _grid.DefaultCellStyle.SelectionForeColor = AppColors.TableRowText;

            _grid.Columns["Thời gian"].Width = 155;
            _grid.Columns["Người dùng"].Width = 110;
            _grid.Columns["Hành động"].Width = 190;
            _grid.Columns["Đối tượng"].Width = 130;
            _grid.Columns["Mô tả"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            _grid.Columns["Thao tác"].Width = 90;

            _actionColIndex = _grid.Columns["Hành động"].Index;
            _tableColIndex = _grid.Columns["Đối tượng"].Index;
            _viewColIndex = _grid.Columns["Thao tác"].Index;

            _grid.CellPainting += Grid_CellPainting;
            _grid.CellMouseUp += (s, e) =>
            {
                if (e.RowIndex < 0 || e.RowIndex >= _currentRows.Count) return;
                if (e.ColumnIndex == _viewColIndex)
                    DetailRequested?.Invoke(this, _currentRows[e.RowIndex].LogId);
            };
            _grid.CellMouseMove += (s, e) =>
                _grid.Cursor = (e.RowIndex >= 0 && e.ColumnIndex == _viewColIndex) ? Cursors.Hand : Cursors.Default;
        }

        private Control BuildTabsRow()
        {
            var row = new Panel { Dock = DockStyle.Top, Height = 56, Margin = new Padding(0, 16, 0, 0), BackColor = Color.Transparent };

            _btnTabAudit = new PrimaryButton { Text = "Nhật ký audit", IsPrimary = true, Width = 160, Height = 40, Location = new Point(0, 8) };
            _btnTabIncident = new PrimaryButton { Text = "Nhật ký sự cố", IsPrimary = false, Width = 160, Height = 40, Location = new Point(172, 8) };
            _btnTabAudit.Click += (s, e) => SetIncidentTab(false);
            _btnTabIncident.Click += (s, e) => SetIncidentTab(true);

            row.Controls.Add(_btnTabAudit);
            row.Controls.Add(_btnTabIncident);
            return row;
        }

        private void SetIncidentTab(bool incident)
        {
            if (_incidentOnly == incident) return;
            _incidentOnly = incident;
            _btnTabAudit.IsPrimary = !incident;
            _btnTabIncident.IsPrimary = incident;
            _btnTabAudit.Invalidate();
            _btnTabIncident.Invalidate();

            ResetToFirstPage();
        }

        private Control BuildFilterBar()
        {
            var row = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 52,
                ColumnCount = 6,
                RowCount = 1,
                Margin = new Padding(0, 12, 0, 0),
                BackColor = Color.Transparent
            };
            row.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28f));
            row.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18f));
            row.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18f));
            row.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120f));
            row.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120f));
            row.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18f));
            row.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

            _searchBar = new SearchBarControl
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 8, 0),
                PlaceholderText = "Tìm theo người dùng, mô tả, đối tượng..."
            };
            _searchPresenter = new SearchPresenter(_searchBar, SuggestAuditSearchTerms);
            _searchPresenter.SearchCommitted += (s, e) => ResetToFirstPage();

            _filterAction = new FilterComboBox { Dock = DockStyle.Fill, Margin = new Padding(0, 0, 8, 0) };
            _filterAction.OptionChanged += OnFilterOptionChanged;

            _filterTable = new FilterComboBox { Dock = DockStyle.Fill, Margin = new Padding(0, 0, 8, 0) };
            _filterTable.OptionChanged += OnFilterOptionChanged;

            _dtFrom = CreateDatePicker();
            _dtTo = CreateDatePicker();

            _lblTotalCount = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight,
                Font = AppFonts.Small,
                ForeColor = AppColors.TextMuted,
                BackColor = Color.Transparent,
                Margin = new Padding(8, 0, 0, 0)
            };

            row.Controls.Add(_searchBar, 0, 0);
            row.Controls.Add(_filterAction, 1, 0);
            row.Controls.Add(_filterTable, 2, 0);
            row.Controls.Add(_dtFrom, 3, 0);
            row.Controls.Add(_dtTo, 4, 0);
            row.Controls.Add(_lblTotalCount, 5, 0);
            return row;
        }

        private IList<string> SuggestAuditSearchTerms(string keyword)
        {
            var query = CurrentQuery;
            query.PageIndex = 0;
            query.PageSize = 8;
            var result = _repository.GetPage(query);
            var suggestions = new List<string>();

            foreach (var row in result.Rows ?? Array.Empty<AuditLogRowDto>())
            {
                AddSuggestion(suggestions, row.Username);
                AddSuggestion(suggestions, AuditLogDisplayMapper.GetActionLabel(row.Action));
                AddSuggestion(suggestions, AuditLogDisplayMapper.GetTableLabel(row.TableName));
            }

            return suggestions;
        }

        private static void AddSuggestion(IList<string> suggestions, string value)
        {
            if (string.IsNullOrWhiteSpace(value) || suggestions.Contains(value)) return;
            suggestions.Add(value);
        }

        private DateTimePicker CreateDatePicker()
        {
            var picker = new DateTimePicker
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 5, 8, 0),
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy",
                ShowCheckBox = true,
                Checked = false,
                Font = AppFonts.Body
            };
            picker.ValueChanged += (s, e) =>
            {
                if (!picker.Checked) return;
                ResetToFirstPage();
            };
            return picker;
        }

        private void Grid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.CellBounds.Width <= 0 || e.CellBounds.Height <= 0) return;
            if (e.RowIndex >= _currentRows.Count) return;

            try
            {
                if (e.ColumnIndex == _actionColIndex || e.ColumnIndex == _tableColIndex)
                    PaintPillCell(e);
                else if (e.ColumnIndex == _viewColIndex)
                    PaintViewLinkCell(e);
            }
            catch (ArgumentException)
            {
                e.Handled = true;
            }
        }

        private void PaintPillCell(DataGridViewCellPaintingEventArgs e)
        {
            e.PaintBackground(e.CellBounds, true);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            var row = _currentRows[e.RowIndex];
            bool isActionColumn = e.ColumnIndex == _actionColIndex;
            string text = Convert.ToString(e.Value ?? string.Empty);
            var (bg, fg) = isActionColumn
                ? AuditLogDisplayMapper.GetActionColors(row.Action)
                : AuditLogDisplayMapper.GetTableColors(row.TableName);

            if (!string.IsNullOrEmpty(text))
            {
                var textSize = e.Graphics.MeasureString(text, AppFonts.SmallBold);
                const int paddingX = 12;
                const int pillHeight = 28;
                int pillWidth = Math.Min(e.CellBounds.Width - 8, (int)Math.Ceiling(textSize.Width) + paddingX * 2);
                var pillRect = new Rectangle(
                    e.CellBounds.X + (e.CellBounds.Width - pillWidth) / 2,
                    e.CellBounds.Y + (e.CellBounds.Height - pillHeight) / 2,
                    pillWidth, pillHeight);

                using (var path = AppRadius.GetRoundedPath(pillRect, pillHeight / 2))
                using (var brush = new SolidBrush(bg))
                    e.Graphics.FillPath(brush, path);

                TextRenderer.DrawText(e.Graphics, text, AppFonts.SmallBold, pillRect, fg,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
            }

            DrawCellBottomBorder(e);
            e.Handled = true;
        }

        private void PaintViewLinkCell(DataGridViewCellPaintingEventArgs e)
        {
            e.PaintBackground(e.CellBounds, true);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            TextRenderer.DrawText(e.Graphics, "Xem", AppFonts.SmallBold, e.CellBounds, AppColors.Accent,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            DrawCellBottomBorder(e);
            e.Handled = true;
        }

        private static void DrawCellBottomBorder(DataGridViewCellPaintingEventArgs e)
        {
            using (var pen = new Pen(AppColors.TableGrid))
                e.Graphics.DrawLine(pen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right, e.CellBounds.Bottom - 1);
        }

        private void ExportAudit(string format)
        {
            string[] headers = { "Thời gian", "Người dùng", "Hành động", "Đối tượng", "Mô tả" };
            Func<IReadOnlyList<object[]>> fetchAll = () =>
            {
                var query = CurrentQuery;
                query.PageIndex = 0;
                query.PageSize = 100000;
                var result = _repository.GetPage(query);
                return result.Rows.Select(r => new object[]
                {
                    r.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss"),
                    r.Username,
                    AuditLogDisplayMapper.GetActionLabel(r.Action),
                    AuditLogDisplayMapper.GetTableLabel(r.TableName),
                    r.Detail
                }).ToList();
            };

            if (format == "csv")
                TableExportRunner.Run(FindForm(), new CsvTableExporter(), "NhatKyHeThong", () => headers, fetchAll);
            else if (format == "excel")
                TableExportRunner.Run(FindForm(), new ExcelTableExporter(), "NhatKyHeThong", () => headers, fetchAll);
        }
        #endregion
    }
}