using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using FontAwesome.Sharp;
using Guna.UI2.WinForms;
using SIMS_WinFormsApp.Infrastructure.Composition;
using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.Models.Mapping;
using SIMS_WinFormsApp.MVP.Presenters;
using SIMS_WinFormsApp.MVP.ViewModels;
using SIMS_WinFormsApp.Repositories.Interfaces;
using SIMS_WinFormsApp.Services.Implementations.Export;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Controls.AuditLog;
using SIMS_WinFormsApp.UI.Controls.Filter;
using SIMS_WinFormsApp.UI.Controls.Loading;
using SIMS_WinFormsApp.UI.Controls.Pagination;
using SIMS_WinFormsApp.UI.Controls.Search;
using SIMS_WinFormsApp.UI.Controls.Toast;
using SIMS_WinFormsApp.UI.I18n;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.Forms.SystemMgmt
{
    /// <summary>
    /// Trang Nhật ký hệ thống — View của <see cref="AuditLogPresenter"/>.
    /// Layout tự co giãn: bộ lọc ngày luôn đủ chỗ hiện dd/MM/yyyy, cột thao tác là icon mắt,
    /// và cả trang cuộn khi cửa sổ thấp để không cắt thẻ thống kê hay ô lọc.
    /// </summary>
    public sealed class ucAuditLog : UserControl, IAuditLogView
    {
        private readonly IAuditLogRepository _repository;
        private readonly AuditLogPresenter _presenter;
        private readonly DateRangeFilterPresenter _datePresenter;
        private readonly PaginationPresenter _paginationPresenter;
        private readonly SearchPresenter _searchPresenter;
        private readonly AuditLogGridPainter _gridPainter;
        private readonly LoadingOverlayHost _overlayHost;

        private readonly Panel _body;
        private readonly AuditLogPageHeader _header;
        private readonly AuditLogFilterSurface _surface;
        private readonly Panel _tableCard;
        private readonly Panel _gridHost;
        private readonly Guna2DataGridView _grid;
        private readonly PaginationControl _pagination;
        private readonly Label _emptyLabel;

        private IReadOnlyList<string> _actionCodes = Array.Empty<string>();
        private IReadOnlyList<string> _tableCodes = Array.Empty<string>();
        private IReadOnlyList<AuditLogRowDto> _currentRows = Array.Empty<AuditLogRowDto>();
        private int _totalCount;
        private bool _dataLoadedOnce;
        private bool _arranging;
        private bool _bindingFilters;

        public ucAuditLog(IAuditLogRepository repository = null)
        {
            AutoScaleMode = AutoScaleMode.None;
            Font = AppFonts.Body;
            DoubleBuffered = true;
            Dock = DockStyle.Fill;
            BackColor = AppColors.PageBg;

            _repository = repository ?? AppComposition.CreateAuditLogRepository();
            _body = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = AppColors.PageBg
            };
            _header = new AuditLogPageHeader { Height = 112 };
            _surface = new AuditLogFilterSurface();
            _pagination = new PaginationControl();
            _paginationPresenter = new PaginationPresenter(_pagination, 10);
            _grid = CreateGrid();
            _gridPainter = new AuditLogGridPainter(_grid);
            _emptyLabel = new Label
            {
                TextAlign = ContentAlignment.MiddleCenter,
                Font = AppFonts.Body,
                ForeColor = AppColors.TextMuted,
                BackColor = Color.Transparent,
                Visible = false
            };
            _gridHost = new Panel { Dock = DockStyle.Fill, BackColor = AppColors.White };
            _gridHost.Controls.Add(_grid);
            _gridHost.Controls.Add(_emptyLabel);
            _emptyLabel.BringToFront();

            var paginationHost = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 64,
                BackColor = AppColors.White,
                Padding = new Padding(8, 4, 8, 4)
            };
            _pagination.Dock = DockStyle.Fill;
            paginationHost.Controls.Add(_pagination);

            _tableCard = new Panel { BackColor = AppColors.Border, Padding = new Padding(1) };
            var tableInner = new Panel { Dock = DockStyle.Fill, BackColor = AppColors.White };
            tableInner.Controls.Add(_gridHost);
            tableInner.Controls.Add(paginationHost);
            _tableCard.Controls.Add(tableInner);

            _body.Controls.Add(_header);
            _body.Controls.Add(_surface);
            _body.Controls.Add(_tableCard);

            _overlayHost = new LoadingOverlayHost(_body) { Dock = DockStyle.Fill };
            Controls.Add(_overlayHost);

            _datePresenter = new DateRangeFilterPresenter(_surface.DateRange);
            _searchPresenter = new SearchPresenter(_surface.SearchBar, SuggestAuditSearchTerms);
            _presenter = new AuditLogPresenter(this, _repository, _overlayHost);

            WireEvents();
            ApplyLocalization();
            _body.Resize += (_, __) => ArrangePage();
            LanguageManager.Instance.LanguageChanged += OnLanguageChanged;
            ThemeManager.Instance.ThemeChanged += OnThemeChanged;
            VisibleChanged += OnVisibleChanged;
        }

        public AuditLogQuery CurrentQuery
        {
            get
            {
                var range = _datePresenter.Current ?? DateRange.Empty;
                return new AuditLogQuery
                {
                    PageIndex = _paginationPresenter.PageIndex,
                    PageSize = _paginationPresenter.PageSize,
                    Search = _surface.SearchBar.Text,
                    ActionFilter = _surface.ActionFilter.SelectedOption?.Value,
                    TableFilter = _surface.TableFilter.SelectedOption?.Value,
                    IncidentOnly = _surface.Tabs.SelectedIndex == 1,
                    FromDate = range.From,
                    ToDate = range.To
                };
            }
        }

        public event EventHandler QueryChanged;
        public event EventHandler<long> DetailRequested;

        public void DisplayStats(AuditLogStatsDto stats)
        {
            stats = stats ?? new AuditLogStatsDto();
            _surface.SetStats(stats.TotalCount, stats.TodayCount, stats.FailedLoginCount, stats.ActiveUserCount);
        }

        public void DisplayRows(IReadOnlyList<AuditLogRowDto> rows, int totalCount)
        {
            int requestedPage = _paginationPresenter.PageIndex;
            _paginationPresenter.ApplyResult(totalCount);
            if (_paginationPresenter.PageIndex != requestedPage)
            {
                QueryChanged?.Invoke(this, EventArgs.Empty);
                return;
            }

            _currentRows = rows ?? Array.Empty<AuditLogRowDto>();
            _totalCount = totalCount;
            RenderRows();
            _surface.SetCount(totalCount);
            _emptyLabel.Visible = totalCount == 0;
            _emptyLabel.Text = Lang.Get("audit.empty");
        }

        public void DisplayActionOptions(IReadOnlyList<string> actions)
        {
            _actionCodes = actions ?? Array.Empty<string>();
            BindOptions(_surface.ActionFilter, _actionCodes, Lang.Get("audit.filter.allActions"), AuditLogDisplayMapper.GetActionLabel);
        }

        public void DisplayTableOptions(IReadOnlyList<string> tables)
        {
            _tableCodes = tables ?? Array.Empty<string>();
            BindOptions(_surface.TableFilter, _tableCodes, Lang.Get("audit.filter.allTables"), AuditLogDisplayMapper.GetTableLabel);
        }

        public void ShowDetail(AuditLogDetailDto detail) => frmAuditLogDetail.Show(FindForm(), detail);

        public void ShowError(string message) => AppToast.Error(this, message);

        private void WireEvents()
        {
            _paginationPresenter.PageChanged += (_, __) => QueryChanged?.Invoke(this, EventArgs.Empty);
            _searchPresenter.SearchCommitted += (_, __) => _paginationPresenter.ResetToFirstPage();
            _datePresenter.RangeChanged += (_, __) => _paginationPresenter.ResetToFirstPage();
            _surface.Tabs.SelectedIndexChanged += (_, __) => _paginationPresenter.ResetToFirstPage();
            _surface.ActionFilter.OptionChanged += OnFilterOptionChanged;
            _surface.TableFilter.OptionChanged += OnFilterOptionChanged;
            _gridPainter.ViewRequested += (_, index) => RequestDetail(index);
            _grid.CellDoubleClick += (_, e) =>
            {
                if (e.RowIndex >= 0) RequestDetail(e.RowIndex);
            };
            _header.OptionsButton.Click += (_, __) => ShowExportMenu();
            _gridHost.Resize += (_, __) => CenterEmptyLabel();
        }

        private void OnFilterOptionChanged(object sender, EventArgs e)
        {
            if (_bindingFilters) return;
            _paginationPresenter.ResetToFirstPage();
        }

        private void OnVisibleChanged(object sender, EventArgs e)
        {
            if (!Visible || _dataLoadedOnce) return;
            _dataLoadedOnce = true;
            if (IsHandleCreated)
                BeginInvoke(new Action(LoadWhenShown));
            else
                LoadWhenShown();
        }

        private void LoadWhenShown()
        {
            ArrangePage();
            _presenter.Load();
        }

        private void RequestDetail(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= _currentRows.Count) return;
            DetailRequested?.Invoke(this, _currentRows[rowIndex].LogId);
        }

        private void RenderRows()
        {
            var viewRows = _currentRows.Select(ToGridRow).ToList();
            _gridPainter.SetRows(viewRows);
            _grid.Rows.Clear();
            foreach (var row in viewRows)
            {
                _grid.Rows.Add(
                    row.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss"),
                    row.Username,
                    row.ActionLabel,
                    row.TableLabel,
                    string.IsNullOrWhiteSpace(row.DetailText) ? "-" : row.DetailText,
                    string.Empty);
            }
            CenterEmptyLabel();
        }

        private static AuditLogGridRow ToGridRow(AuditLogRowDto row)
        {
            var actionColors = AuditLogDisplayMapper.GetActionColors(row.Action);
            var tableColors = AuditLogDisplayMapper.GetTableColors(row.TableName);
            return new AuditLogGridRow
            {
                LogId = row.LogId,
                CreatedAt = row.CreatedAt,
                Username = row.Username,
                ActionLabel = AuditLogDisplayMapper.GetActionLabel(row.Action),
                TableLabel = AuditLogDisplayMapper.GetTableLabel(row.TableName),
                DetailText = string.IsNullOrWhiteSpace(row.Detail) ? "-" : row.Detail,
                ActionBackground = actionColors.Bg,
                ActionForeground = actionColors.Fg,
                TableBackground = tableColors.Bg,
                TableForeground = tableColors.Fg
            };
        }

        private void BindOptions(FilterComboBox box, IReadOnlyList<string> codes, string allLabel, Func<string, string> labelOf)
        {
            string selected = box.SelectedOption?.Value;
            var options = new List<FilterOption> { new FilterOption(allLabel, null) };
            foreach (var code in codes ?? Array.Empty<string>())
                options.Add(new FilterOption(labelOf(code), code));

            _bindingFilters = true;
            box.OptionChanged -= OnFilterOptionChanged;
            box.SetOptions(options);
            if (!string.IsNullOrEmpty(selected))
                SelectOption(box, selected);
            box.OptionChanged += OnFilterOptionChanged;
            _bindingFilters = false;
        }

        private static void SelectOption(FilterComboBox box, string value)
        {
            for (int i = 0; i < box.Items.Count; i++)
            {
                var option = box.Items[i] as FilterOption;
                if (option != null && string.Equals(option.Value, value, StringComparison.Ordinal))
                {
                    box.SelectedIndex = i;
                    return;
                }
            }
        }

        private void ApplyLocalization()
        {
            _header.ApplyLocalization();
            _surface.ApplyLocalization();
            _gridPainter.ApplyLocalization();
            _emptyLabel.Text = Lang.Get("audit.empty");
            if (_actionCodes.Count > 0 || _surface.ActionFilter.Items.Count > 0)
                DisplayActionOptions(_actionCodes);
            if (_tableCodes.Count > 0 || _surface.TableFilter.Items.Count > 0)
                DisplayTableOptions(_tableCodes);
            if (_currentRows.Count > 0)
                RenderRows();
            _surface.SetCount(_totalCount);
        }

        private void OnLanguageChanged(object sender, EventArgs e)
        {
            ApplyLocalization();
            ArrangePage();
        }

        private void OnThemeChanged(object sender, EventArgs e)
        {
            BackColor = AppColors.PageBg;
            _body.BackColor = AppColors.PageBg;
            _tableCard.BackColor = AppColors.Border;
            ApplyGridTheme();
            _emptyLabel.ForeColor = AppColors.TextMuted;
            Invalidate(true);
        }

        private void ArrangePage()
        {
            if (_arranging || _body.ClientSize.Width <= 0 || _body.ClientSize.Height <= 0) return;
            _arranging = true;
            try
            {
                _body.SuspendLayout();
                int viewW = _body.ClientSize.Width;
                int viewH = _body.ClientSize.Height;
                const int padL = 20;
                const int padR = 20;
                const int padT = 16;
                const int padB = 12;
                const int headerH = 112;
                const int paginationH = 64;
                const int gridMin = 240;

                int innerW = Math.Max(560, viewW - padL - padR);
                int contentH = PlaceContent(innerW, viewH, padL, padT, padB, headerH, paginationH, gridMin);

                // ClientSize đã trừ scrollbar nếu nó đang hiện. Chỉ trừ thêm khi scrollbar
                // sắp xuất hiện, tránh mỗi lần resize lại hẹp thêm một lần.
                if (contentH > viewH && !_body.VerticalScroll.Visible)
                {
                    int narrowed = Math.Max(560, viewW - SystemInformation.VerticalScrollBarWidth - padL - padR);
                    if (narrowed != innerW)
                    {
                        innerW = narrowed;
                        contentH = PlaceContent(innerW, viewH, padL, padT, padB, headerH, paginationH, gridMin);
                    }
                }

                int contentW = innerW + padL + padR;
                _body.AutoScrollMinSize = new Size(contentW > viewW ? contentW : 0, contentH);
                CenterEmptyLabel();
            }
            finally
            {
                _body.ResumeLayout(true);
                _arranging = false;
            }
        }

        private int PlaceContent(int innerW, int viewH, int padL, int padT, int padB, int headerH, int paginationH, int gridMin)
        {
            int y = padT;
            _header.SetBounds(padL, y, innerW, headerH);
            y += headerH + 12;

            int filterH = _surface.Arrange(innerW);
            _surface.SetBounds(padL, y, innerW, filterH);
            y += filterH + 12;

            int available = viewH - y - paginationH - padB;
            int gridH = Math.Max(gridMin, available);
            _tableCard.SetBounds(padL, y, innerW, gridH + paginationH);
            y += gridH + paginationH + padB;
            return Math.Max(viewH, y);
        }

        private void CenterEmptyLabel()
        {
            if (_emptyLabel == null || _gridHost.ClientSize.Width <= 0) return;
            _emptyLabel.SetBounds(0, _grid.ColumnHeadersHeight, _gridHost.ClientSize.Width,
                Math.Max(40, _gridHost.ClientSize.Height - _grid.ColumnHeadersHeight));
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

        private void ShowExportMenu()
        {
            var csv = Lang.Get("audit.export.csv");
            var excel = Lang.Get("audit.export.excel");
            var menu = new ModernDropdownMenu { Width = 220 };
            menu.AddItem("csv", csv, IconChar.FileCsv);
            menu.AddItem("excel", excel, IconChar.FileExcel);
            menu.ItemClicked += (_, key) => ExportAudit(key);
            menu.ShowBelow(_header.OptionsButton, 6);
        }

        private void ExportAudit(string format)
        {
            string[] headers =
            {
                Lang.Get("audit.column.time"),
                Lang.Get("audit.column.user"),
                Lang.Get("audit.column.action"),
                Lang.Get("audit.column.table"),
                Lang.Get("audit.column.detail")
            };
            Func<IReadOnlyList<object[]>> fetchAll = () =>
            {
                var query = CurrentQuery;
                query.PageIndex = 0;
                query.PageSize = 100000;
                var result = _repository.GetPage(query);
                return (result.Rows ?? Array.Empty<AuditLogRowDto>()).Select(r => new object[]
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

        private Guna2DataGridView CreateGrid()
        {
            var grid = new Guna2DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                AutoGenerateColumns = false,
                RowHeadersVisible = false,
                BackgroundColor = AppColors.White,
                GridColor = AppColors.TableGrid,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                ColumnHeadersHeight = 48,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
                RowTemplate = { Height = 56 },
                EnableHeadersVisualStyles = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ScrollBars = ScrollBars.Both,
                ShowCellToolTips = true,
                ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
            };

            AddColumn(grid, AuditLogGridPainter.TimeColumn, 128, 16, false);
            AddColumn(grid, AuditLogGridPainter.UserColumn, 132, 16, false);
            AddColumn(grid, AuditLogGridPainter.ActionColumn, 150, 18, false);
            AddColumn(grid, AuditLogGridPainter.TableColumn, 118, 14, false);
            AddColumn(grid, AuditLogGridPainter.DetailColumn, 160, 28, false);
            AddColumn(grid, AuditLogGridPainter.ViewColumn, 76, 8, true);

            ApplyGridTheme(grid);
            var doubleBuffered = typeof(DataGridView).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            doubleBuffered?.SetValue(grid, true, null);
            return grid;
        }

        private static void AddColumn(DataGridView grid, string name, int minWidth, float weight, bool center)
        {
            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = name,
                HeaderText = name,
                MinimumWidth = minWidth,
                FillWeight = weight,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DefaultCellStyle =
                {
                    Alignment = center ? DataGridViewContentAlignment.MiddleCenter : DataGridViewContentAlignment.MiddleLeft,
                    Padding = new Padding(8, 0, 8, 0)
                }
            });
        }

        private void ApplyGridTheme() => ApplyGridTheme(_grid);

        private static void ApplyGridTheme(Guna2DataGridView grid)
        {
            grid.BackgroundColor = AppColors.White;
            grid.GridColor = AppColors.TableGrid;
            grid.DefaultCellStyle.Font = AppFonts.Body;
            grid.DefaultCellStyle.ForeColor = AppColors.TableRowText;
            grid.DefaultCellStyle.BackColor = AppColors.White;
            grid.DefaultCellStyle.SelectionBackColor = AppColors.AccentSelectionBg;
            grid.DefaultCellStyle.SelectionForeColor = AppColors.TextPrimary;
            grid.AlternatingRowsDefaultCellStyle.BackColor = AppColors.TableRowOdd;
            grid.ColumnHeadersDefaultCellStyle.BackColor = AppColors.TableHeaderBg;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = AppFonts.BodyBold;
            grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grid.ColumnHeadersDefaultCellStyle.Padding = new Padding(12, 0, 12, 0);
            grid.ThemeStyle.AlternatingRowsStyle.BackColor = AppColors.TableRowOdd;
            grid.ThemeStyle.HeaderStyle.BackColor = AppColors.TableHeaderBg;
            grid.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            grid.ThemeStyle.HeaderStyle.Font = AppFonts.BodyBold;
            grid.ThemeStyle.RowsStyle.BackColor = AppColors.White;
            grid.ThemeStyle.RowsStyle.ForeColor = AppColors.TableRowText;
            grid.ThemeStyle.RowsStyle.SelectionBackColor = AppColors.AccentBgSoft;
            grid.ThemeStyle.RowsStyle.SelectionForeColor = AppColors.TextPrimary;
            grid.ThemeStyle.RowsStyle.Height = 56;
            grid.ThemeStyle.GridColor = AppColors.TableGrid;
            grid.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
        }

        private void HookWheel(Control root)
        {
            root.MouseWheel += OnMouseWheel;
            foreach (Control child in root.Controls)
            {
                if (child is DataGridView) continue;
                HookWheel(child);
            }
        }

        private void OnMouseWheel(object sender, MouseEventArgs e)
        {
            if (!_body.VerticalScroll.Visible) return;
            int next = _body.VerticalScroll.Value - e.Delta / 2;
            next = Math.Max(_body.VerticalScroll.Minimum, Math.Min(_body.VerticalScroll.Maximum - _body.VerticalScroll.LargeChange + 1, next));
            if (next < _body.VerticalScroll.Minimum) next = _body.VerticalScroll.Minimum;
            try { _body.VerticalScroll.Value = next; }
            catch (ArgumentException) { }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            HookWheel(_body);
            ArrangePage();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                LanguageManager.Instance.LanguageChanged -= OnLanguageChanged;
                ThemeManager.Instance.ThemeChanged -= OnThemeChanged;
                _searchPresenter?.Dispose();
                _gridPainter?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
