using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.Reporting.WinForms;
using SIMS_WinFormsApp.Infrastructure.Composition;
using SIMS_WinFormsApp.Services.Implementations.Reports;

namespace SIMS_WinFormsApp.Views.Reports
{
    public partial class frmChartDashboard : Form
    {
        private RevenueReportService _reportService;

        public frmChartDashboard()
        {
            InitializeComponent();
        }

        private void frmChartDashboard_Load(object sender, EventArgs e)
        {
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return;
            }

            _reportService = AppComposition.CreateRevenueReportService();
            if (_periodFilter.SelectedIndex < 0)
                _periodFilter.SelectedIndex = 0;   // mặc định xem theo Tuần
            _fromDate.Value = DateTime.Today.AddDays(-6);
            _toDate.Value = DateTime.Today;
            if (!SetInitialDateRange())
                return;
            UpdateReport();
        }

        private void ViewRevenueRdlc_Click(object sender, EventArgs e)
            {
                if (!TryGetSelectedDateRange(out DateTime from, out DateTime to))
                    return;

                try
                {
                    RevenueReportPeriod period = (RevenueReportPeriod)_periodFilter.SelectedIndex;
                    RevenueReportData revenue = _reportService.GetReport(from, to, period);
                    FinancialReportSignatories signatories = _reportService.GetFinancialReportSignatories();
                    var sources = new[]
                    {
                        new ReportDataSource("RevenueSummaryDataSet", new[] { revenue }),
                        new ReportDataSource("RevenueTrendDataSet", revenue.Trend),
                        new ReportDataSource("RevenueCategoriesDataSet", revenue.Categories),
                        new ReportDataSource("RevenueTopProductsDataSet", revenue.TopProducts)
                    };
                    IDictionary<string, string> parameters = ReportParameterBuilder.CreateFinancialParameters(
                        signatories, from, to, GetCurrentUserName(), GetPeriodLabel(period));

                    using (var viewer = new frmReportViewer(
                        "Báo cáo doanh thu",
                        "SIMS_WinFormsApp.Reports.RevenueReport.rdlc",
                        sources,
                        parameters))
                    {
                        viewer.ShowDialog(this);
                    }
                }
                catch (SqlException exception)
                {
                    ShowRdlcError(exception.Message);
                }
                catch (InvalidOperationException exception)
                {
                    ShowRdlcError(exception.Message);
                }
                catch (FileNotFoundException exception)
                {
                    ShowRdlcError(exception.Message);
                }
                catch (FileLoadException exception)
                {
                    ShowRdlcError(exception.Message);
                }
                catch (TypeLoadException exception)
                {
                    ShowRdlcError(exception.Message);
                }
                catch (BadImageFormatException exception)
                {
                    ShowRdlcError(exception.Message);
                }
                catch (LocalProcessingException exception)
                {
                    ShowRdlcError(exception.Message);
                }
            }

        private static string GetPeriodLabel(RevenueReportPeriod period)
            {
                switch (period)
                {
                    case RevenueReportPeriod.Month:
                        return "Theo tháng";
                    case RevenueReportPeriod.Quarter:
                        return "Theo quý";
                    default:
                        return "Theo ngày";
                }
            }

        private void ShowRdlcError(string details)
            {
                MessageBox.Show(this, "Không thể mở báo cáo RDLC. Hãy kiểm tra thư viện ReportViewer và mẫu báo cáo đã được cài đặt/nhúng.\r\n\r\n"
                    + details, "Lỗi báo cáo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        private bool TryGetSelectedDateRange(out DateTime from, out DateTime to)
        {
            from = _fromDate.Value.Date;
            to = _toDate.Value.Date;
            if (from <= to)
                return true;

            MessageBox.Show(
                "Ngày bắt đầu không được sau ngày kết thúc.",
                "Khoảng thời gian không hợp lệ",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return false;
        }

        private static string GetCurrentUserName()
        {
            var user = SIMS_WinFormsApp.Services.Session.UserSession.Instance.CurrentUser;
            return user == null ? "Hệ thống" : user.FullName;
        }

        private void ApplyFilter_Click(object sender, EventArgs e)
        {
            if (_fromDate.Value.Date > _toDate.Value.Date)
            {
                MessageBox.Show(
                    "Ngày bắt đầu không được sau ngày kết thúc.",
                    "Khoảng thời gian không hợp lệ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            UpdateReport();
        }

        private bool SetInitialDateRange()
        {
            try
            {
                DateTime? latestInvoiceDate = _reportService.GetLatestInvoiceDate();
                if (latestInvoiceDate.HasValue &&
                    latestInvoiceDate.Value <= _toDate.MaxDate.Date)
                {
                    _toDate.Value = latestInvoiceDate.Value;
                    DateTime fromDate = latestInvoiceDate.Value.AddDays(-6);
                    _fromDate.Value = fromDate < _fromDate.MinDate ? _fromDate.MinDate : fromDate;
                }

                return true;
            }
            catch (SqlException exception)
            {
                ShowDatabaseError(exception.Message);
                return false;
            }
            catch (InvalidOperationException exception)
            {
                ShowDatabaseError(exception.Message);
                return false;
            }
        }

        private void UpdateReport()
        {
            try
            {
                var report = _reportService.GetReport(
                    _fromDate.Value.Date,
                    _toDate.Value.Date,
                    (RevenueReportPeriod)_periodFilter.SelectedIndex);
                RenderReport(report);
            }
            catch (SqlException exception)
            {
                ShowDatabaseError(exception.Message);
            }
            catch (InvalidOperationException exception)
            {
                ShowDatabaseError(exception.Message);
            }
        }

        private void RenderReport(RevenueReportData report)
        {
            _revenueCard.Text = "TỔNG DOANH THU\r\n" + report.Revenue.ToString("N0") + " ₫";
            _orderCard.Text = "SỐ ĐƠN HÀNG\r\n" + report.OrderCount.ToString("N0") + " đơn";
            decimal average = report.OrderCount == 0 ? 0 : report.Revenue / report.OrderCount;
            _averageCard.Text = "GIÁ TRỊ TRUNG BÌNH\r\n" + average.ToString("N0") + " ₫ / đơn";

            _revenueChartTitle.Text = GetTrendTitle((RevenueReportPeriod)_periodFilter.SelectedIndex);
            UpdateTrendChart(report);
            UpdateCategoryChart(report);
            UpdateTopProducts(report);
        }

        private void UpdateTrendChart(RevenueReportData report)
        {
            var revenueSeries = _revenueTrendChart.Series["Doanh thu"];
            var orderSeries = _revenueTrendChart.Series["Số đơn"];
            revenueSeries.Points.Clear();
            orderSeries.Points.Clear();

            foreach (RevenueTrendPoint revenue in report.Trend)
            {
                DateTime periodStart = revenue.PeriodStart;
                string label = FormatPeriod(periodStart, (RevenueReportPeriod)_periodFilter.SelectedIndex);
                int revenueIndex = revenueSeries.Points.AddXY(label, revenue.Revenue);
                revenueSeries.Points[revenueIndex].ToolTip = label + ": " + revenue.Revenue.ToString("N0") + " ₫";
                int orderIndex = orderSeries.Points.AddXY(label, revenue.OrderCount);
                orderSeries.Points[orderIndex].ToolTip = label + ": " + revenue.OrderCount.ToString("N0") + " đơn";
            }

            _revenueTrendChart.Titles.Clear();
            if (report.Trend.Count == 0)
            {
                _revenueTrendChart.Titles.Add(new Title("Không có doanh thu trong khoảng thời gian này")
                {
                    Font = new Font("Segoe UI", 9F),
                    ForeColor = Color.FromArgb(100, 116, 139)
                });
            }
        }

        private void UpdateCategoryChart(RevenueReportData report)
        {
            var series = _categoryChart.Series["Danh mục"];
            series.Points.Clear();
            Color[] colors =
            {
                Color.FromArgb(37, 99, 235),
                Color.FromArgb(14, 165, 233),
                Color.FromArgb(34, 197, 94),
                Color.FromArgb(251, 146, 60),
                Color.FromArgb(168, 85, 247),
                Color.FromArgb(236, 72, 153)
            };

            for (int i = 0; i < report.Categories.Count; i++)
            {
                RevenueCategoryTotal category = report.Categories[i];
                int pointIndex = series.Points.AddXY(category.Name, category.Revenue);
                series.Points[pointIndex].Color = colors[i % colors.Length];
                series.Points[pointIndex].LegendText = category.Name;
            }

            _categoryChart.Titles.Clear();
            if (report.Categories.Count == 0)
            {
                _categoryChart.Titles.Add(new Title("Chưa có dữ liệu doanh thu")
                {
                    Font = new Font("Segoe UI", 9F),
                    ForeColor = Color.FromArgb(100, 116, 139)
                });
            }
        }

        private void UpdateTopProducts(RevenueReportData report)
        {
            _topProductsGrid.Rows.Clear();
            foreach (RevenueProductTotal product in report.TopProducts)
            {
                _topProductsGrid.Rows.Add(
                    product.Name,
                    product.Quantity.ToString("N0"),
                    product.Revenue.ToString("N0") + " ₫");
            }
        }

        private static string GetTrendTitle(RevenueReportPeriod period)
        {
            switch (period)
            {
                case RevenueReportPeriod.Month:
                    return "Doanh thu và số đơn theo tháng";
                case RevenueReportPeriod.Quarter:
                    return "Doanh thu và số đơn theo quý";
                default:
                    return "Doanh thu và số đơn theo ngày";
            }
        }

        private static string FormatPeriod(DateTime periodStart, RevenueReportPeriod period)
        {
            switch (period)
            {
                case RevenueReportPeriod.Month:
                    return periodStart.ToString("MM/yyyy");
                case RevenueReportPeriod.Quarter:
                    return "Q" + (((periodStart.Month - 1) / 3) + 1) + " " + periodStart.Year;
                default:
                    return periodStart.ToString("dd/MM");
            }
        }

        private void ShowDatabaseError(string details)
        {
            MessageBox.Show(
                "Không thể tải báo cáo doanh thu từ cơ sở dữ liệu.\r\n\r\n" + details,
                "Lỗi tải báo cáo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

    }
}
