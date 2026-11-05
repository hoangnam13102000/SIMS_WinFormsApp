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
        private readonly RevenueReportService _reportService;

        public frmChartDashboard()
        {
            InitializeComponent();
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return;
            }

            _reportService = AppComposition.CreateRevenueReportService();
            _subtitle.Text = "Doanh thu, đơn hàng và thiệt hại tiêu hủy trong cùng kỳ báo cáo";
            _periodFilter.Items.Clear();
            _periodFilter.Items.AddRange(new object[] { "Theo ngày", "Theo tháng", "Theo quý" });
            _periodFilter.SelectedIndex = (int)RevenueReportPeriod.Week;
            _fromDate.Value = DateTime.Today.AddDays(-6);
            _toDate.Value = DateTime.Today;
            _applyFilter.Click += ApplyFilter_Click;
            _viewRevenueRdlc.Click += ViewRevenueRdlc_Click;
            _viewDisposalRdlc.Click += ViewDisposalRdlc_Click;
            _header.SizeChanged += (s, e) => LayoutReportHeader();
            LayoutReportHeader();

            ConfigureRevenueChart();
            ConfigureCategoryChart();
            if (!SetInitialDateRange())
                return;
            UpdateReport();
        }

        private void LayoutReportHeader()
        {
            int right = _header.ClientSize.Width - 16;
            _viewDisposalRdlc.Location = new Point(right - _viewDisposalRdlc.Width, 22);
            _viewRevenueRdlc.Location = new Point(_viewDisposalRdlc.Left - 8 - _viewRevenueRdlc.Width, 22);
            int textWidth = Math.Max(160, _viewRevenueRdlc.Left - _title.Left - 16);
            _title.Width = textWidth;
            _subtitle.Width = textWidth;
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

        private void ViewDisposalRdlc_Click(object sender, EventArgs e)
            {
                if (!TryGetSelectedDateRange(out DateTime from, out DateTime to))
                    return;

                try
                {
                    RevenueReportPeriod period = (RevenueReportPeriod)_periodFilter.SelectedIndex;
                    StockDisposalReportData disposal = _reportService.GetStockDisposalReport(from, to, period);
                    FinancialReportSignatories signatories = _reportService.GetFinancialReportSignatories();
                    var sources = new[]
                    {
                        new ReportDataSource("DisposalSummaryDataSet", new[] { disposal }),
                        new ReportDataSource("DisposalReasonsDataSet", disposal.Reasons),
                        new ReportDataSource("DisposalProductsDataSet", disposal.Products)
                    };
                    IDictionary<string, string> parameters = ReportParameterBuilder.CreateFinancialParameters(
                        signatories, from, to, GetCurrentUserName(), GetPeriodLabel(period));

                    using (var viewer = new frmReportViewer(
                        "Báo cáo tiêu hủy",
                        "SIMS_WinFormsApp.Reports.DisposalReport.rdlc",
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

        private void ConfigureRevenueChart()
        {
            var area = new ChartArea("RevenueArea");
            area.BackColor = Color.White;
            area.AxisX.LabelStyle.ForeColor = Color.FromArgb(100, 116, 139);
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisX.LineColor = Color.FromArgb(226, 232, 240);
            area.AxisY.LabelStyle.ForeColor = Color.FromArgb(100, 116, 139);
            area.AxisY.LabelStyle.Format = "N0";
            area.AxisY.Title = "Doanh thu / thiệt hại (₫)";
            area.AxisY.TitleForeColor = Color.FromArgb(100, 116, 139);
            area.AxisY.MajorGrid.LineColor = Color.FromArgb(226, 232, 240);
            area.AxisY.LineColor = Color.Transparent;
            area.AxisY.Minimum = 0;
            area.AxisY2.Enabled = AxisEnabled.True;
            area.AxisY2.Title = "Số đơn";
            area.AxisY2.TitleForeColor = Color.FromArgb(234, 88, 12);
            area.AxisY2.LabelStyle.ForeColor = Color.FromArgb(234, 88, 12);
            area.AxisY2.LabelStyle.Format = "N0";
            area.AxisY2.MajorGrid.Enabled = false;
            area.AxisY2.Minimum = 0;
            _revenueTrendChart.ChartAreas.Add(area);
            _revenueTrendChart.Legends.Add(new Legend
            {
                Docking = Docking.Bottom,
                Alignment = StringAlignment.Center,
                Font = new Font("Segoe UI", 8F),
                ForeColor = Color.FromArgb(71, 85, 105)
            });

            _revenueTrendChart.Series.Add(new Series("Doanh thu")
            {
                ChartArea = area.Name,
                ChartType = SeriesChartType.Column,
                Color = Color.FromArgb(59, 130, 246),
                BorderColor = Color.FromArgb(37, 99, 235),
                BorderWidth = 1,
                Legend = _revenueTrendChart.Legends[0].Name,
                IsValueShownAsLabel = false,
                ToolTip = "#VALX: #VAL{N0} ₫"
            });
            _revenueTrendChart.Series["Doanh thu"]["PointWidth"] = "0.4";

            _revenueTrendChart.Series.Add(new Series("Thiệt hại")
            {
                ChartArea = area.Name,
                ChartType = SeriesChartType.Column,
                Color = Color.FromArgb(239, 68, 68),
                BorderColor = Color.FromArgb(220, 38, 38),
                BorderWidth = 1,
                Legend = _revenueTrendChart.Legends[0].Name,
                IsValueShownAsLabel = false,
                ToolTip = "#VALX: #VAL{N0} ₫"
            });
            _revenueTrendChart.Series["Thiệt hại"]["PointWidth"] = "0.4";

            _revenueTrendChart.Series.Add(new Series("Số đơn")
            {
                ChartArea = area.Name,
                ChartType = SeriesChartType.Spline,
                YAxisType = AxisType.Secondary,
                BorderWidth = 3,
                Color = Color.FromArgb(234, 88, 12),
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 7,
                MarkerColor = Color.FromArgb(234, 88, 12),
                Legend = _revenueTrendChart.Legends[0].Name,
                ToolTip = "#VALX: #VAL đơn"
            });
        }

        private void ConfigureCategoryChart()
        {
            var area = new ChartArea("CategoryArea");
            area.BackColor = Color.White;
            area.AxisX.Enabled = AxisEnabled.False;
            area.AxisY.Enabled = AxisEnabled.False;
            _categoryChart.ChartAreas.Add(area);
            _categoryChart.Legends.Add(new Legend
            {
                Docking = Docking.Right,
                Alignment = StringAlignment.Center,
                Font = new Font("Segoe UI", 8F),
                ForeColor = Color.FromArgb(71, 85, 105),
                IsDockedInsideChartArea = false
            });
            _categoryChart.Series.Add(new Series("Danh mục")
            {
                ChartArea = area.Name,
                ChartType = SeriesChartType.Doughnut,
                BorderWidth = 2,
                BorderColor = Color.White,
                Legend = _categoryChart.Legends[0].Name,
                IsValueShownAsLabel = false,
                ToolTip = "#VALX: #VAL{N0} ₫ (#PERCENT{P0})"
            });
            _categoryChart.Series["Danh mục"]["DoughnutRadius"] = "58";
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
                StockDisposalReportData disposal = _reportService.GetStockDisposalReport(
                    _fromDate.Value.Date,
                    _toDate.Value.Date,
                    (RevenueReportPeriod)_periodFilter.SelectedIndex);
                RenderReport(report, disposal);
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

        private void RenderReport(RevenueReportData report, StockDisposalReportData disposal)
        {
            _revenueCard.Text = "TỔNG DOANH THU\r\n" + report.Revenue.ToString("N0") + " ₫";
            _orderCard.Text = "SỐ ĐƠN HÀNG\r\n" + report.OrderCount.ToString("N0") + " đơn";
            decimal average = report.OrderCount == 0 ? 0 : report.Revenue / report.OrderCount;
            _averageCard.Text = "GIÁ TRỊ TRUNG BÌNH\r\n" + average.ToString("N0") + " ₫ / đơn";
            _disposalCard.Text = "THIỆT HẠI TIÊU HỦY\r\n"
                + disposal.TotalLoss.ToString("N0") + " ₫ · "
                + disposal.ProductQuantity.ToString("N0") + " sản phẩm";

            _revenueChartTitle.Text = GetTrendTitle((RevenueReportPeriod)_periodFilter.SelectedIndex);
            UpdateTrendChart(report, disposal);
            UpdateCategoryChart(report);
            UpdateTopProducts(report);
        }

        private void UpdateTrendChart(RevenueReportData report, StockDisposalReportData disposal)
        {
            var revenueSeries = _revenueTrendChart.Series["Doanh thu"];
            var lossSeries = _revenueTrendChart.Series["Thiệt hại"];
            var orderSeries = _revenueTrendChart.Series["Số đơn"];
            revenueSeries.Points.Clear();
            lossSeries.Points.Clear();
            orderSeries.Points.Clear();

            var revenueByPeriod = report.Trend.ToDictionary(point => point.PeriodStart);
            var lossByPeriod = disposal.Trend.ToDictionary(point => point.PeriodStart);
            DateTime[] periods = report.Trend.Select(point => point.PeriodStart)
                .Concat(disposal.Trend.Select(point => point.PeriodStart))
                .Distinct()
                .OrderBy(period => period)
                .ToArray();

            foreach (DateTime periodStart in periods)
            {
                revenueByPeriod.TryGetValue(periodStart, out RevenueTrendPoint revenue);
                lossByPeriod.TryGetValue(periodStart, out StockDisposalTrendPoint loss);
                string label = FormatPeriod(periodStart, (RevenueReportPeriod)_periodFilter.SelectedIndex);
                decimal revenueAmount = revenue == null ? 0m : revenue.Revenue;
                decimal lossAmount = loss == null ? 0m : loss.LossAmount;
                int revenueIndex = revenueSeries.Points.AddXY(label, revenueAmount);
                revenueSeries.Points[revenueIndex].ToolTip = label + ": " + revenueAmount.ToString("N0") + " ₫";
                int lossIndex = lossSeries.Points.AddXY(label, lossAmount);
                lossSeries.Points[lossIndex].ToolTip = label + ": " + lossAmount.ToString("N0") + " ₫ thiệt hại";
                int orderIndex = orderSeries.Points.AddXY(label, revenue == null ? 0 : revenue.OrderCount);
                orderSeries.Points[orderIndex].ToolTip = label + ": "
                    + (revenue == null ? 0 : revenue.OrderCount).ToString("N0") + " đơn";
            }

            _revenueTrendChart.Titles.Clear();
            if (periods.Length == 0)
            {
                _revenueTrendChart.Titles.Add(new Title("Không có doanh thu hoặc thiệt hại trong khoảng thời gian này")
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
                    return "Doanh thu, thiệt hại và số đơn theo tháng";
                case RevenueReportPeriod.Quarter:
                    return "Doanh thu, thiệt hại và số đơn theo quý";
                default:
                    return "Doanh thu, thiệt hại và số đơn theo ngày";
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
