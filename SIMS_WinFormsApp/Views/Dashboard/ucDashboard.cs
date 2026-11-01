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
        private Chart _revenueSummaryChart;
        private Label _chartTitleLabel;
        private bool _isLoading;
        private bool _hasLoaded;

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
            ConfigureRevenueSummaryChart();

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

        public void ShowDashboardUser(string displayName)
        {
            _welcomeLabel.Text = "Chào mừng bạn quay trở lại, " +
                (string.IsNullOrWhiteSpace(displayName) ? "bạn" : displayName);
        }

        public void ShowDashboardData(DashboardDataDto data)
        {
            if (data == null) throw new System.ArgumentNullException(nameof(data));
            if (data.Metrics.Count != 4)
                throw new InvalidOperationException("Dashboard phải có đúng 4 chỉ số.");

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

            _titleLabel.Text = data.Kind == DashboardKind.Inventory
                ? "Tổng quan kho hàng"
                : data.Kind == DashboardKind.SalesManagement ? "Tổng quan quản lý bán hàng"
                : data.Kind == DashboardKind.Sales ? "Tổng quan bán hàng" : "Tổng quan";
            _chartTitleLabel.Text = data.ChartTitle;
            _revenueSummaryChart.Series[0].Name = data.SeriesTitle;
            _revenueSummaryChart.Series[0].LegendText = data.SeriesTitle;

            Series chartSeries = _revenueSummaryChart.Series[0];
            chartSeries.Points.Clear();
            foreach (DashboardChartPointDto point in data.ChartPoints)
            {
                string label = point.Date.ToString("dd/MM", vietnamese);
                int pointIndex = chartSeries.Points.AddXY(label, point.Value);
                chartSeries.Points[pointIndex].ToolTip =
                    point.Date.ToString("dd/MM/yyyy", vietnamese) + ": "
                    + point.Value.ToString("N0", vietnamese)
                    + (data.Kind == DashboardKind.Inventory ? " sản phẩm" : " VNĐ");
            }

            bool showActivities = data.Kind == DashboardKind.Overview;
            _activityCard.Visible = showActivities;
            _contentLayout.RowCount = showActivities ? 3 : 2;
            _contentLayout.RowStyles.Clear();
            _contentLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 128F));
            if (showActivities)
            {
                _contentLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 260F));
                _contentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
                _contentLayout.SetRow(_activityCard, 2);
            }
            else
            {
                _contentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            }

            _activityGrid.Rows.Clear();
            foreach (DashboardActivityDto activity in data.RecentActivities)
            {
                string activityText = string.IsNullOrWhiteSpace(activity.Detail)
                    ? AuditLogDisplayMapper.GetActionLabel(activity.Action)
                        + " · " + AuditLogDisplayMapper.GetTableLabel(activity.TableName)
                    : activity.Detail;
                string status = AuditLogDisplayMapper.IsIncident(activity.Action)
                    ? "Cảnh báo"
                    : "Đã ghi nhận";

                _activityGrid.Rows.Add(
                    activityText,
                    activity.Username,
                    activity.CreatedAt.ToString("dd/MM/yyyy HH:mm", vietnamese),
                    status);
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

            var chartPanel = new Panel
            {
                BackColor = Color.White,
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 14, 0, 0),
                Padding = new Padding(18),
                Name = "_revenueSummaryPanel"
            };
            var chartLayout = new TableLayoutPanel
            {
                ColumnCount = 1,
                Dock = DockStyle.Fill,
                RowCount = 2
            };
            chartLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            chartLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            chartLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

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
            chartLayout.Controls.Add(_revenueSummaryChart, 0, 1);
            chartPanel.Controls.Add(chartLayout);
            _contentLayout.Controls.Add(chartPanel, 0, 1);
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
            if (_presenter == null || _isLoading) return;

            _isLoading = true;
            try
            {
                await _presenter.LoadAsync();
                _hasLoaded = true;
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
