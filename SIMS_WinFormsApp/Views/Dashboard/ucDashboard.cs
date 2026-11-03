using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using SIMS_WinFormsApp.Infrastructure.Composition;
using SIMS_WinFormsApp.Models.DTOs.Dashboard;
using SIMS_WinFormsApp.Models.Enums;
using SIMS_WinFormsApp.Models.Mapping;
using SIMS_WinFormsApp.MVP.Presenters;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Interfaces;
using SIMS_WinFormsApp.Views.Toast;

namespace SIMS_WinFormsApp.Views.Dashboard
{
    public partial class ucDashboard : UserControl, IDashboardView
    {
        private readonly DashboardPresenter _presenter;
        private Panel _revenueSummaryPanel;
        private TableLayoutPanel _revenueSummaryLayout;
        private Chart _revenueSummaryChart;
        private Label _chartTitleLabel;
        private Panel _revenueFilterPanel;
        private ComboBox _revenuePeriodFilter;
        private DateTimePicker _revenueDatePicker;
        private Panel _invoiceFilterPanel;
        private ComboBox _invoicePeriodFilter;
        private DateTimePicker _invoiceDatePicker;
        private Button _applyInvoiceFilterButton;
        private int _loadedInvoiceCount;
        private DashboardKind _currentDashboardKind;
        private bool _isLoading;
        private bool _hasLoaded;
        private bool _reloadRequested;
        private LinkLabel _expiryBatchesLink;

        public ucDashboard() : this(null, null)
        {
        }

        public ucDashboard(string displayName) : this(displayName, null)
        {
        }

        public ucDashboard(string displayName, IDashboardDataService dataService)
            : this(displayName, string.Empty, 0, dataService)
        {
        }

        public ucDashboard(
            string displayName, string roleCode, int currentUserId, IDashboardDataService dataService = null)
        {
            InitializeComponent();
            BackColor = AppColors.PageBg;
            _revenueCard.Paint += DrawCardBorder;
            _ordersCard.Paint += DrawCardBorder;
            _stockCard.Paint += DrawCardBorder;
            _customersCard.Paint += DrawCardBorder;
            _activityCard.Paint += DrawCardBorder;
            ConfigureExpiryBatchesLink();
            ConfigureRevenueSummaryChart();
            ConfigureRevenuePeriodFilter();
            ConfigureRecentInvoiceFilter();

            bool isDesignTime = DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime;
            if (isDesignTime)
            {
                PopulateRevenueChartDesignPreview();
                _presenter = null;
                return;
            }

            _presenter = new DashboardPresenter(
                this,
                displayName,
                roleCode,
                currentUserId,
                dataService ?? AppComposition.CreateDashboardDataService());
            Load += Dashboard_Load;
            VisibleChanged += Dashboard_VisibleChanged;
        }

        public DataGridViewTextBoxColumn Activity => _activity_column;
        public DataGridViewTextBoxColumn User => _user_column;
        public DataGridViewTextBoxColumn Time => _time_column;
        public DataGridViewTextBoxColumn Status => _status_column;

        public void ShowDashboardUser(string displayName)
        {
            _welcomeLabel.Text = "Chào mừng bạn quay trở lại, " +
                (string.IsNullOrWhiteSpace(displayName) ? "bạn" : displayName);
        }

        public event EventHandler ExpiryBatchesRequested;

        public void ShowDashboardData(DashboardDataDto data)
        {
            if (data == null) throw new System.ArgumentNullException(nameof(data));
            if (data.Metrics.Count != 4)
                throw new InvalidOperationException("Dashboard phải có đúng 4 chỉ số.");

            _currentDashboardKind = data.Kind;
            CultureInfo vietnamese = CultureInfo.GetCultureInfo("vi-VN");
            Label[] captions = { _revenueCaption, _ordersCaption, _stockCaption, _customersCaption };
            Label[] values = { _revenueValue, _ordersValue, _stockValue, _customersValue };
            for (int i = 0; i < data.Metrics.Count; i++)
            {
                DashboardMetricDto metric = data.Metrics[i];
                captions[i].Text = metric.Caption;
                values[i].Text = metric.Value.ToString("N0", vietnamese)
                    + (metric.IsCurrency ? " VNĐ" : string.Empty);
            }

            if (data.IsSalesStaffDashboard)
            {
                _activityTitle.Text = "Hóa đơn gần đây của bạn";
                _loadedInvoiceCount = data.RecentActivities.Count;
                _invoiceFilterPanel.Visible = true;
                Activity.HeaderText = "Mã hóa đơn";
                User.HeaderText = "Tổng tiền";
            }
            else
            {
                _invoiceFilterPanel.Visible = false;
            }
            bool showActivities = data.Kind == DashboardKind.Overview || data.IsSalesStaffDashboard;
            _activityCard.Visible = showActivities;
            ConfigureDashboardLayout(data.IsSalesStaffDashboard, showActivities);

            _titleLabel.Text = data.Kind == DashboardKind.Inventory
                ? "Tổng quan kho hàng"
                : data.Kind == DashboardKind.SalesManagement ? "Tổng quan quản lý bán hàng"
                : data.Kind == DashboardKind.Sales ? "Tổng quan bán hàng" : "Tổng quan";
            _expiryBatchesLink.Visible = data.Kind == DashboardKind.Inventory;
            _chartTitleLabel.Text = data.ChartTitle;
            _revenueSummaryChart.Series[0].Name = data.SeriesTitle;
            _revenueSummaryChart.Series[0].LegendText = data.SeriesTitle;

            Series chartSeries = _revenueSummaryChart.Series[0];
            chartSeries.Points.Clear();
            foreach (DashboardChartPointDto point in data.ChartPoints)
            {
                string label = _revenuePeriodFilter.SelectedIndex == 3
                    ? point.Date.ToString("MM/yy", vietnamese)
                    : point.Date.ToString("dd/MM", vietnamese);
                int pointIndex = chartSeries.Points.AddXY(label, point.Value);
                chartSeries.Points[pointIndex].ToolTip =
                    point.Date.ToString("dd/MM/yyyy", vietnamese) + ": "
                    + point.Value.ToString("N0", vietnamese)
                    + (data.Kind == DashboardKind.Inventory ? " sản phẩm" : " VNĐ");
            }

            UpdateRevenuePeriodDisplay();
            if (data.IsSalesStaffDashboard)
                UpdateInvoicePeriodSubtitle();
            _activityGrid.Rows.Clear();
            foreach (DashboardActivityDto activity in data.RecentActivities)
            {
                string activityText = data.IsSalesStaffDashboard
                    ? activity.Detail
                    : string.IsNullOrWhiteSpace(activity.Detail)
                    ? AuditLogDisplayMapper.GetActionLabel(activity.Action)
                        + " · " + AuditLogDisplayMapper.GetTableLabel(activity.TableName)
                    : activity.Detail;
                string amount = data.IsSalesStaffDashboard
                    ? (activity.TotalAmount ?? 0m).ToString("N0", vietnamese) + " VNĐ"
                    : activity.Username;
                string status = data.IsSalesStaffDashboard
                    ? (string.Equals(activity.RecordStatus, "ACTIVE", StringComparison.OrdinalIgnoreCase)
                        ? "Hoàn tất"
                        : "Đã hủy")
                    : AuditLogDisplayMapper.IsIncident(activity.Action)
                    ? "Cảnh báo"
                    : "Đã ghi nhận";

                _activityGrid.Rows.Add(
                    activityText,
                    amount,
                    activity.CreatedAt.ToString("dd/MM/yyyy HH:mm", vietnamese),
                    status);
            }
        }

        private void ConfigureExpiryBatchesLink()
        {
            _expiryBatchesLink = new LinkLabel
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                LinkColor = Color.FromArgb(37, 99, 235),
                Location = new Point(820, 62),
                Name = "_expiryBatchesLink",
                Text = "Xem lô cận hạn / hết hạn →",
                Visible = false
            };
            _expiryBatchesLink.LinkClicked += (sender, args) =>
                ExpiryBatchesRequested?.Invoke(this, EventArgs.Empty);
            _headerPanel.Controls.Add(_expiryBatchesLink);
            _expiryBatchesLink.BringToFront();
        }

        private void ConfigureDashboardLayout(bool isSalesStaffDashboard, bool showActivities)
        {
            _contentLayout.SuspendLayout();
            try
            {
                _contentLayout.ColumnCount = isSalesStaffDashboard ? 2 : 1;
                _contentLayout.ColumnStyles.Clear();
                if (isSalesStaffDashboard)
                {
                    _contentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42F));
                    _contentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58F));
                }
                else
                {
                    _contentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
                }

                _contentLayout.RowCount = isSalesStaffDashboard || !showActivities ? 2 : 3;
                _contentLayout.RowStyles.Clear();
                _contentLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 128F));
                if (isSalesStaffDashboard || !showActivities)
                {
                    _contentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
                }
                else
                {
                    _contentLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 260F));
                    _contentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
                }

                _contentLayout.SetColumn(_statsLayout, 0);
                _contentLayout.SetRow(_statsLayout, 0);
                _contentLayout.SetColumnSpan(_statsLayout, isSalesStaffDashboard ? 2 : 1);
                _contentLayout.SetColumn(_revenueSummaryPanel, 0);
                _contentLayout.SetRow(_revenueSummaryPanel, 1);
                _contentLayout.SetColumn(_activityCard, isSalesStaffDashboard ? 1 : 0);
                _contentLayout.SetRow(_activityCard, isSalesStaffDashboard ? 1 : showActivities ? 2 : 1);

                _revenueSummaryPanel.Visible = true;
                _revenueSummaryPanel.Margin = new Padding(0, 14, isSalesStaffDashboard ? 7 : 0, 0);
                _activityCard.Margin = new Padding(isSalesStaffDashboard ? 7 : 0, 14, 0, 0);
            }
            finally
            {
                _contentLayout.ResumeLayout(true);
            }
        }

        private void ConfigureRevenueSummaryChart()
        {
            _contentLayout.RowCount = 3;
            _contentLayout.RowStyles.Clear();
            _contentLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 128F));
            _contentLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 260F));
            _contentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            _contentLayout.SetRow(_activityCard, 2);

            _revenueSummaryPanel = new Panel
            {
                BackColor = Color.White,
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 14, 0, 0),
                Padding = new Padding(18),
                Name = "_revenueSummaryPanel"
            };
            _revenueSummaryPanel.Paint += DrawCardBorder;
            var chartLayout = new TableLayoutPanel
            {
                ColumnCount = 1,
                Dock = DockStyle.Fill,
                RowCount = 3
            };
            chartLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            chartLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            chartLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            chartLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            _revenueSummaryLayout = chartLayout;

            _chartTitleLabel = new Label
            {
                AutoSize = true,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                Text = "Doanh thu 7 ngày gần nhất",
                TextAlign = ContentAlignment.MiddleLeft
            };

            var chartArea = new ChartArea("RevenueSummaryArea");
            chartArea.BackColor = Color.White;
            chartArea.AxisX.LabelStyle.ForeColor = Color.FromArgb(100, 116, 139);
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisX.LineColor = Color.FromArgb(226, 232, 240);
            chartArea.AxisY.LabelStyle.ForeColor = Color.FromArgb(100, 116, 139);
            chartArea.AxisY.LabelStyle.Format = "N0";
            chartArea.AxisY.MajorGrid.LineColor = Color.FromArgb(226, 232, 240);
            chartArea.AxisY.LineColor = Color.Transparent;
            chartArea.AxisY.Minimum = 0;

            _revenueSummaryChart = new Chart
            {
                BackColor = Color.White,
                Dock = DockStyle.Fill,
                Name = "_revenueSummaryChart"
            };
            _revenueSummaryChart.ChartAreas.Add(chartArea);
            _revenueSummaryChart.Series.Add(new Series("Doanh thu")
            {
                ChartArea = chartArea.Name,
                ChartType = SeriesChartType.Column,
                Color = Color.FromArgb(59, 130, 246),
                BorderColor = Color.FromArgb(37, 99, 235),
                BorderWidth = 1,
                IsValueShownAsLabel = false
            });
            _revenueSummaryChart.Series["Doanh thu"]["PointWidth"] = "0.55";

            chartLayout.Controls.Add(_chartTitleLabel, 0, 0);
            chartLayout.Controls.Add(_revenueSummaryChart, 0, 2);
            _revenueSummaryPanel.Controls.Add(chartLayout);
            _contentLayout.Controls.Add(_revenueSummaryPanel, 0, 1);
        }

        private void ConfigureRevenuePeriodFilter()
        {
            _revenuePeriodFilter = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9F),
                Location = new Point(0, 2),
                Size = new Size(78, 26),
                TabIndex = 0
            };
            _revenuePeriodFilter.Items.AddRange(new object[] { "Ngày", "Tuần", "Tháng", "Năm" });
            _revenuePeriodFilter.SelectedIndex = 1;

            _revenueDatePicker = new DateTimePicker
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy",
                Font = new Font("Segoe UI", 9F),
                Location = new Point(84, 2),
                Size = new Size(112, 26),
                TabIndex = 1,
                Value = DateTime.Today
            };

            var applyRevenueFilterButton = new Button
            {
                Location = new Point(202, 1),
                Name = "_applyRevenueFilterButton",
                Size = new Size(54, 28),
                TabIndex = 2,
                Text = "Lọc",
                UseVisualStyleBackColor = true
            };

            _revenueFilterPanel = new Panel
            {
                BackColor = Color.White,
                Dock = DockStyle.Fill,
                Name = "_revenueFilterPanel"
            };
            var filterLabel = new Label
            {
                AutoSize = false,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(0, 3),
                Name = "_revenueFilterLabel",
                Size = new Size(42, 24),
                Text = "Lọc:",
                TextAlign = ContentAlignment.MiddleLeft,
                UseMnemonic = false
            };
            _revenuePeriodFilter.Location = new Point(42, 2);
            _revenueDatePicker.Location = new Point(126, 2);
            applyRevenueFilterButton.Location = new Point(244, 1);
            _revenueFilterPanel.Controls.Add(filterLabel);
            _revenueFilterPanel.Controls.Add(_revenuePeriodFilter);
            _revenueFilterPanel.Controls.Add(_revenueDatePicker);
            _revenueFilterPanel.Controls.Add(applyRevenueFilterButton);
            _revenueSummaryLayout.Controls.Add(_revenueFilterPanel, 0, 1);

            applyRevenueFilterButton.Click += OnApplyRevenueFilterClick;
        }

        private void ConfigureRecentInvoiceFilter()
        {
            const int filterPanelWidth = 350;
            _invoicePeriodFilter = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9F),
                Location = new Point(64, 2),
                Size = new Size(88, 26),
                TabIndex = 0
            };
            _invoicePeriodFilter.Items.AddRange(new object[] { "Tất cả", "Ngày", "Tuần", "Tháng", "Năm" });
            _invoicePeriodFilter.SelectedIndex = 0;

            _invoiceDatePicker = new DateTimePicker
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy",
                Font = new Font("Segoe UI", 9F),
                Location = new Point(158, 2),
                Size = new Size(112, 26),
                TabIndex = 1,
                Value = DateTime.Today
            };

            _applyInvoiceFilterButton = new Button
            {
                Location = new Point(276, 1),
                Name = "_applyInvoiceFilterButton",
                Size = new Size(66, 28),
                TabIndex = 2,
                Text = "Lọc",
                UseVisualStyleBackColor = true
            };
            _invoiceFilterPanel = new Panel
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                BackColor = Color.White,
                Location = new Point(Math.Max(18, _activityCard.ClientSize.Width - 18 - filterPanelWidth), 14),
                Name = "_invoiceFilterPanel",
                Size = new Size(filterPanelWidth, 32),
                Visible = false
            };
            var filterLabel = new Label
            {
                AutoSize = false,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(0, 3),
                Name = "_invoiceFilterLabel",
                Size = new Size(60, 24),
                Text = "Lọc theo:",
                TextAlign = ContentAlignment.MiddleLeft,
                UseMnemonic = false
            };
            _invoiceFilterPanel.Controls.Add(filterLabel);
            _invoiceFilterPanel.Controls.Add(_invoicePeriodFilter);
            _invoiceFilterPanel.Controls.Add(_invoiceDatePicker);
            _invoiceFilterPanel.Controls.Add(_applyInvoiceFilterButton);
            _activityCard.Controls.Add(_invoiceFilterPanel);
            _invoiceFilterPanel.BringToFront();

            _applyInvoiceFilterButton.Click += OnApplyInvoiceFilterClick;
            _activityCard.SizeChanged += OnActivityCardSizeChanged;
        }

        private void OnActivityCardSizeChanged(object sender, EventArgs e)
        {
            _invoiceFilterPanel.Location = new Point(
                Math.Max(18, _activityCard.ClientSize.Width - 18 - _invoiceFilterPanel.Width),
                14);
        }

        private async void OnApplyInvoiceFilterClick(object sender, EventArgs e)
        {
            await LoadDashboardDataAsync();
        }

        private async void OnApplyRevenueFilterClick(object sender, EventArgs e)
        {
            await LoadDashboardDataAsync();
        }

        private void GetSelectedRevenuePeriod(out DateTime periodStart, out DateTime periodEnd)
        {
            DateTime selectedDate = _revenueDatePicker.Value.Date;
            switch (_revenuePeriodFilter.SelectedIndex)
            {
                case 0:
                    periodStart = selectedDate;
                    periodEnd = selectedDate.AddDays(1);
                    break;
                case 2:
                    periodStart = new DateTime(selectedDate.Year, selectedDate.Month, 1);
                    periodEnd = periodStart.AddMonths(1);
                    break;
                case 3:
                    periodStart = new DateTime(selectedDate.Year, 1, 1);
                    periodEnd = periodStart.AddYears(1);
                    break;
                default:
                    int daysSinceMonday = ((int)selectedDate.DayOfWeek + 6) % 7;
                    periodStart = selectedDate.AddDays(-daysSinceMonday);
                    periodEnd = periodStart.AddDays(7);
                    break;
            }
        }

        private void UpdateRevenuePeriodDisplay()
        {
            if (_revenuePeriodFilter.SelectedItem == null)
                return;

            DateTime periodStart;
            DateTime periodEnd;
            GetSelectedRevenuePeriod(out periodStart, out periodEnd);
            string periodText;
            switch (_revenuePeriodFilter.SelectedIndex)
            {
                case 0:
                    periodText = "ngày " + periodStart.ToString("dd/MM/yyyy");
                    break;
                case 2:
                    periodText = "tháng " + periodStart.ToString("MM/yyyy");
                    break;
                case 3:
                    periodText = "năm " + periodStart.ToString("yyyy");
                    break;
                default:
                    periodText = "tuần " + periodStart.ToString("dd/MM")
                        + " - " + periodEnd.AddDays(-1).ToString("dd/MM/yyyy");
                    break;
            }

            if (_currentDashboardKind != DashboardKind.Inventory)
                _chartTitleLabel.Text = "Doanh thu cửa hàng · " + periodText;
        }

        private void GetSelectedInvoicePeriod(out DateTime? periodStart, out DateTime? periodEnd)
        {
            DateTime selectedDate = _invoiceDatePicker.Value.Date;
            switch (_invoicePeriodFilter.SelectedIndex)
            {
                case 1:
                    periodStart = selectedDate;
                    periodEnd = selectedDate.AddDays(1);
                    break;
                case 2:
                    int daysSinceMonday = ((int)selectedDate.DayOfWeek + 6) % 7;
                    periodStart = selectedDate.AddDays(-daysSinceMonday);
                    periodEnd = periodStart.Value.AddDays(7);
                    break;
                case 4:
                    periodStart = new DateTime(selectedDate.Year, 1, 1);
                    periodEnd = periodStart.Value.AddYears(1);
                    break;
                case 3:
                    periodStart = new DateTime(selectedDate.Year, selectedDate.Month, 1);
                    periodEnd = periodStart.Value.AddMonths(1);
                    break;
                default:
                    periodStart = null;
                    periodEnd = null;
                    break;
            }
        }

        private void UpdateInvoicePeriodSubtitle()
        {
            if (_invoicePeriodFilter.SelectedItem == null)
                return;

            DateTime? periodStart;
            DateTime? periodEnd;
            GetSelectedInvoicePeriod(out periodStart, out periodEnd);
            string periodText;
            switch (_invoicePeriodFilter.SelectedIndex)
            {
                case 0:
                    _activitySubtitle.Text = string.Format(
                        CultureInfo.GetCultureInfo("vi-VN"),
                        "Tìm thấy {0:N0} hóa đơn do bạn tạo",
                        _loadedInvoiceCount);
                    return;
                case 1:
                    periodText = "ngày " + periodStart.Value.ToString("dd/MM/yyyy");
                    break;
                case 2:
                    periodText = "tuần " + periodStart.Value.ToString("dd/MM")
                        + " - " + periodEnd.Value.AddDays(-1).ToString("dd/MM/yyyy");
                    break;
                case 4:
                    periodText = "năm " + periodStart.Value.ToString("yyyy");
                    break;
                default:
                    periodText = "tháng " + periodStart.Value.ToString("MM/yyyy");
                    break;
            }

            _activitySubtitle.Text = string.Format(
                CultureInfo.GetCultureInfo("vi-VN"),
                "Tìm thấy {0:N0} hóa đơn của bạn trong {1}",
                _loadedInvoiceCount,
                periodText);
        }

        private void PopulateRevenueChartDesignPreview()
        {
            decimal[] sampleRevenue = { 4200000m, 6100000m, 3800000m, 7500000m, 5600000m, 9200000m, 6800000m };
            Series revenueSeries = _revenueSummaryChart.Series[0];

            for (int day = 0; day < sampleRevenue.Length; day++)
            {
                DateTime date = DateTime.Today.AddDays(day - sampleRevenue.Length + 1);
                revenueSeries.Points.AddXY(date.ToString("dd/MM"), sampleRevenue[day]);
            }
        }

        private static void DrawCardBorder(object sender, PaintEventArgs e)
        {
            var panel = sender as Panel;
            if (panel == null || panel.Width < 2 || panel.Height < 2)
                return;

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            using (var pen = new Pen(AppColors.FieldBorder))
                e.Graphics.DrawRectangle(pen, 0, 0, panel.Width - 1, panel.Height - 1);
        }

        private async void Dashboard_Load(object sender, System.EventArgs e)
        {
            await LoadDashboardDataAsync();
        }

        private async void Dashboard_VisibleChanged(object sender, System.EventArgs e)
        {
            if (Visible && IsHandleCreated && _hasLoaded)
                await LoadDashboardDataAsync();
        }

        private async Task LoadDashboardDataAsync()
        {
            if (_presenter == null) return;
            if (_isLoading)
            {
                _reloadRequested = true;
                return;
            }

            _isLoading = true;
            try
            {
                DateTime? periodStart;
                DateTime? periodEnd;
                DateTime revenuePeriodStart;
                DateTime revenuePeriodEnd;
                GetSelectedInvoicePeriod(out periodStart, out periodEnd);
                GetSelectedRevenuePeriod(out revenuePeriodStart, out revenuePeriodEnd);
                await _presenter.LoadAsync(
                    periodStart,
                    periodEnd,
                    revenuePeriodStart,
                    revenuePeriodEnd);
                _hasLoaded = true;
                UpdateInvoicePeriodSubtitle();
                UpdateRevenuePeriodDisplay();
            }
            catch (SqlException ex)
            {
                AppToast.Error(this, "Lỗi tải dữ liệu tổng quan", ex.Message);
            }
            catch (System.InvalidOperationException ex)
            {
                AppToast.Error(this, "Lỗi tải dữ liệu tổng quan", ex.Message);
            }
            finally
            {
                _isLoading = false;
                if (_reloadRequested)
                {
                    _reloadRequested = false;
                    await LoadDashboardDataAsync();
                }
            }
        }

        private void _titleLabel_Click(object sender, System.EventArgs e)
        {
        }

        private void _activityTitle_Click(object sender, System.EventArgs e)
        {

        }
    }
}
