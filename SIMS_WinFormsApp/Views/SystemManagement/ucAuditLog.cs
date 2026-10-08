using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SIMS_WinFormsApp.Infrastructure.Composition;
using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.Models.Mapping;
using SIMS_WinFormsApp.MVP.Presenters;
using SIMS_WinFormsApp.Repositories.Interfaces;
using SIMS_WinFormsApp.Services.Implementations.Export;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Controls.Loading;
using SIMS_WinFormsApp.UI.Controls.Toast;
using SIMS_WinFormsApp.UI.I18n;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.Views.SystemManagement
{
    public sealed partial class ucAuditLog : UserControl, IAuditLogView, ILoadingIndicator
    {
        private readonly IAuditLogRepository _repository;
        private readonly AuditLogPresenter _presenter;
        private readonly Timer _searchTimer;
        private IReadOnlyList<string> _actionOptions = Array.Empty<string>();
        private IReadOnlyList<string> _tableOptions = Array.Empty<string>();
        private IReadOnlyList<AuditLogRowDto> _currentRows = Array.Empty<AuditLogRowDto>();
        private int _pageIndex;
        private int _pageSize = 10;
        private int _totalCount;
        private bool _bindingFilters;
        private bool _dataLoadedOnce;

        public ucAuditLog() : this(null)
        {
        }

        public ucAuditLog(IAuditLogRepository repository)
        {
            InitializeComponent();
            if (DesignMode || System.ComponentModel.LicenseManager.UsageMode ==
                System.ComponentModel.LicenseUsageMode.Designtime)
                return;

            _repository = repository ?? AppComposition.CreateAuditLogRepository();
            _presenter = new AuditLogPresenter(this, _repository, this);
            _searchTimer = new Timer(components) { Interval = 350 };
            _searchTimer.Tick += (sender, args) =>
            {
                _searchTimer.Stop();
                ResetPageAndReload();
            };

            _searchTextBox.TextChanged += (sender, args) =>
            {
                _searchTimer.Stop();
                _searchTimer.Start();
            };
            _actionComboBox.SelectedIndexChanged += OnFilterChanged;
            _tableComboBox.SelectedIndexChanged += OnFilterChanged;
            _incidentCheckBox.CheckedChanged += OnFilterChanged;
            _fromDatePicker.ValueChanged += OnFromDateChanged;
            _toDatePicker.ValueChanged += OnToDateChanged;
            _previousButton.Click += (sender, args) =>
            {
                if (_pageIndex <= 0) return;
                _pageIndex--;
                QueryChanged?.Invoke(this, EventArgs.Empty);
            };
            _nextButton.Click += (sender, args) =>
            {
                if (_pageIndex + 1 >= PageCount) return;
                _pageIndex++;
                QueryChanged?.Invoke(this, EventArgs.Empty);
            };
            _pageSizeComboBox.SelectedIndexChanged += (sender, args) =>
            {
                _pageSize = _pageSizeComboBox.SelectedIndex == 1 ? 20 :
                    (_pageSizeComboBox.SelectedIndex == 2 ? 50 : 10);
                ResetPageAndReload();
            };
            _exportCsvButton.Click += (sender, args) => ExportAudit(new CsvTableExporter());
            _exportExcelButton.Click += (sender, args) => ExportAudit(new ExcelTableExporter());
            _auditGrid.CellContentClick += OnGridCellContentClick;
            _auditGrid.CellDoubleClick += (sender, args) =>
            {
                if (args.RowIndex >= 0) RequestDetail(args.RowIndex);
            };
            VisibleChanged += OnVisibleChanged;
            Disposed += OnDisposed;
            LanguageManager.Instance.LanguageChanged += OnLanguageChanged;
            ThemeManager.Instance.ThemeChanged += OnThemeChanged;
            ApplyLocalization();
            ApplyTheme();
        }

        private int PageCount => Math.Max(1, (int)Math.Ceiling(_totalCount / (double)_pageSize));

        public AuditLogQuery CurrentQuery
        {
            get
            {
                return new AuditLogQuery
                {
                    PageIndex = _pageIndex,
                    PageSize = _pageSize,
                    Search = _searchTextBox.Text,
                    ActionFilter = SelectedValue(_actionComboBox),
                    TableFilter = SelectedValue(_tableComboBox),
                    IncidentOnly = _incidentCheckBox.Checked,
                    FromDate = _fromDatePicker.Checked ? _fromDatePicker.Value.Date : (DateTime?)null,
                    ToDate = _toDatePicker.Checked ? _toDatePicker.Value.Date : (DateTime?)null
                };
            }
        }

        public event EventHandler QueryChanged;
        public event EventHandler<long> DetailRequested;

        public void DisplayStats(AuditLogStatsDto stats)
        {
            stats = stats ?? new AuditLogStatsDto();
            _totalStatValue.Text = stats.TotalCount.ToString("N0");
            _todayStatValue.Text = stats.TodayCount.ToString("N0");
            _failedStatValue.Text = stats.FailedLoginCount.ToString("N0");
            _usersStatValue.Text = stats.ActiveUserCount.ToString("N0");
        }

        public void DisplayRows(IReadOnlyList<AuditLogRowDto> rows, int totalCount)
        {
            _totalCount = totalCount;
            if (_pageIndex >= PageCount)
            {
                _pageIndex = PageCount - 1;
                QueryChanged?.Invoke(this, EventArgs.Empty);
                return;
            }

            _currentRows = rows ?? Array.Empty<AuditLogRowDto>();
            _auditGrid.Rows.Clear();
            foreach (AuditLogRowDto row in _currentRows)
            {
                int index = _auditGrid.Rows.Add(
                    row.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss"),
                    row.Username,
                    AuditLogDisplayMapper.GetActionLabel(row.Action),
                    AuditLogDisplayMapper.GetTableLabel(row.TableName),
                    string.IsNullOrWhiteSpace(row.Detail) ? "-" : row.Detail,
                    "Chi tiết");
                _auditGrid.Rows[index].Tag = row.LogId;
            }

            _emptyLabel.Visible = _currentRows.Count == 0;
            _pageLabel.Text = "Trang " + (_pageIndex + 1) + " / " + PageCount +
                " (" + _totalCount + " bản ghi)";
            _previousButton.Enabled = _pageIndex > 0;
            _nextButton.Enabled = _pageIndex + 1 < PageCount;
        }

        public void DisplayActionOptions(IReadOnlyList<string> actions)
        {
            _actionOptions = actions ?? Array.Empty<string>();
            BindOptions(_actionComboBox, _actionOptions,
                Lang.Get("audit.filter.allActions"), AuditLogDisplayMapper.GetActionLabel);
        }

        public void DisplayTableOptions(IReadOnlyList<string> tables)
        {
            _tableOptions = tables ?? Array.Empty<string>();
            BindOptions(_tableComboBox, _tableOptions,
                Lang.Get("audit.filter.allTables"), AuditLogDisplayMapper.GetTableLabel);
        }

        public void ShowDetail(AuditLogDetailDto detail)
        {
            frmAuditLogDetail.Show(FindForm(), detail);
        }

        public void ShowError(string message)
        {
            AppToast.Error(this, message);
        }

        public void ShowLoading(string message = null)
        {
            _busyMessageLabel.Text = string.IsNullOrWhiteSpace(message) ? "Đang tải..." : message;
            _busyOverlay.Visible = true;
            _busyOverlay.BringToFront();
            _root.Enabled = false;
        }

        public void HideLoading()
        {
            _busyOverlay.Visible = false;
            _root.Enabled = true;
        }

        private void OnVisibleChanged(object sender, EventArgs e)
        {
            if (!Visible || _dataLoadedOnce) return;
            _dataLoadedOnce = true;
            if (IsHandleCreated)
                BeginInvoke(new Action(_presenter.Load));
            else
                _presenter.Load();
        }

        private void OnLanguageChanged(object sender, EventArgs e) => ApplyLocalization();

        private void OnThemeChanged(object sender, EventArgs e) => ApplyTheme();

        private void OnDisposed(object sender, EventArgs e)
        {
            LanguageManager.Instance.LanguageChanged -= OnLanguageChanged;
            ThemeManager.Instance.ThemeChanged -= OnThemeChanged;
            _searchTimer.Dispose();
        }

        private void ApplyLocalization()
        {
            _titleLabel.Text = Lang.Get("audit.page.title");
            _subtitleLabel.Text = Lang.Get("audit.page.subtitle");
            _totalStatTitle.Text = Lang.Get("audit.stats.total");
            _todayStatTitle.Text = Lang.Get("audit.stats.today");
            _failedStatTitle.Text = Lang.Get("audit.stats.failedLogin");
            _usersStatTitle.Text = Lang.Get("audit.stats.activeUsers");
            _incidentCheckBox.Text = Lang.Get("audit.tab.incident");
            _timeColumn.HeaderText = Lang.Get("audit.column.time");
            _userColumn.HeaderText = Lang.Get("audit.column.user");
            _actionColumn.HeaderText = Lang.Get("audit.column.action");
            _tableColumn.HeaderText = Lang.Get("audit.column.table");
            _descriptionColumn.HeaderText = Lang.Get("audit.column.detail");
            _detailColumn.HeaderText = string.Empty;
            _exportCsvButton.Text = Lang.Get("audit.export.csv");
            _exportExcelButton.Text = Lang.Get("audit.export.excel");
            _emptyLabel.Text = Lang.Get("audit.empty");
            DisplayActionOptions(_actionOptions);
            DisplayTableOptions(_tableOptions);
        }

        private void ApplyTheme()
        {
            ThemeManager.Instance.ApplyTheme(this);
            BackColor = AppColors.PageBg;
            _root.BackColor = AppColors.PageBg;
            _gridPanel.BackColor = AppColors.White;
            _auditGrid.BackgroundColor = AppColors.White;
            _auditGrid.GridColor = AppColors.TableGrid;
            _auditGrid.DefaultCellStyle.BackColor = AppColors.White;
            _auditGrid.DefaultCellStyle.ForeColor = AppColors.TableRowText;
            _auditGrid.DefaultCellStyle.SelectionBackColor = AppColors.AccentSelectionBg;
            _auditGrid.DefaultCellStyle.SelectionForeColor = AppColors.TextPrimary;
            _auditGrid.AlternatingRowsDefaultCellStyle.BackColor = AppColors.TableRowOdd;
            _auditGrid.ColumnHeadersDefaultCellStyle.BackColor = AppColors.TableHeaderBg;
            _auditGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            _emptyLabel.BackColor = AppColors.White;
            _emptyLabel.ForeColor = AppColors.TextMuted;
        }

        private void OnFilterChanged(object sender, EventArgs e)
        {
            if (_bindingFilters) return;
            ResetPageAndReload();
        }

        private void OnFromDateChanged(object sender, EventArgs e)
        {
            if (_fromDatePicker.Checked && _toDatePicker.Checked &&
                _fromDatePicker.Value.Date > _toDatePicker.Value.Date)
                _toDatePicker.Value = _fromDatePicker.Value.Date;
            OnDateFilterChanged(sender, e);
        }

        private void OnToDateChanged(object sender, EventArgs e)
        {
            if (_fromDatePicker.Checked && _toDatePicker.Checked &&
                _toDatePicker.Value.Date < _fromDatePicker.Value.Date)
                _fromDatePicker.Value = _toDatePicker.Value.Date;
            OnDateFilterChanged(sender, e);
        }

        private void OnDateFilterChanged(object sender, EventArgs e)
        {
            ResetPageAndReload();
        }

        private void ResetPageAndReload()
        {
            _pageIndex = 0;
            QueryChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnGridCellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == _detailColumn.Index)
                RequestDetail(e.RowIndex);
        }

        private void RequestDetail(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= _currentRows.Count) return;
            DetailRequested?.Invoke(this, _currentRows[rowIndex].LogId);
        }

        private void BindOptions(
            ComboBox comboBox,
            IReadOnlyList<string> codes,
            string allLabel,
            Func<string, string> labelOf)
        {
            string selected = SelectedValue(comboBox);
            _bindingFilters = true;
            try
            {
                comboBox.Items.Clear();
                comboBox.Items.Add(new AuditFilterOption(allLabel, null));
                foreach (string code in codes ?? Array.Empty<string>())
                    comboBox.Items.Add(new AuditFilterOption(labelOf(code), code));

                int index = 0;
                for (int i = 1; i < comboBox.Items.Count; i++)
                {
                    var option = (AuditFilterOption)comboBox.Items[i];
                    if (string.Equals(option.Value, selected, StringComparison.Ordinal))
                    {
                        index = i;
                        break;
                    }
                }
                comboBox.SelectedIndex = index;
            }
            finally
            {
                _bindingFilters = false;
            }
        }

        private static string SelectedValue(ComboBox comboBox)
        {
            var option = comboBox.SelectedItem as AuditFilterOption;
            return option == null ? null : option.Value;
        }

        private void ExportAudit(ITableDataExporter exporter)
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
                AuditLogQuery query = CurrentQuery;
                query.PageIndex = 0;
                query.PageSize = 100000;
                var result = _repository.GetPage(query);
                return (result.Rows ?? Array.Empty<AuditLogRowDto>())
                    .Select(row => new object[]
                    {
                        row.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss"),
                        row.Username,
                        AuditLogDisplayMapper.GetActionLabel(row.Action),
                        AuditLogDisplayMapper.GetTableLabel(row.TableName),
                        row.Detail
                    })
                    .ToList();
            };

            TableExportRunner.Run(FindForm(), exporter, "NhatKyHeThong", () => headers, fetchAll);
        }

        private sealed class AuditFilterOption
        {
            public string DisplayText { get; }
            public string Value { get; }

            public AuditFilterOption(string displayText, string value)
            {
                DisplayText = displayText;
                Value = value;
            }

            public override string ToString() => DisplayText;
        }
    }
}
