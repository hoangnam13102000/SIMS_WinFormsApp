using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using SIMS_WinFormsApp.Infrastructure.Composition;
using SIMS_WinFormsApp.Services.Implementations.Reports;

namespace SIMS_WinFormsApp.Views.Reports
{
    public partial class ucInventoryReport : UserControl
    {
        private const int AllCategoriesValue = 0;
        private readonly InventoryReportService _reportService;

        public ucInventoryReport()
        {
            InitializeComponent();

            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            _reportService = AppComposition.CreateInventoryReportService();
            _from.Value = DateTime.Today.AddDays(-29);
            _to.Value = DateTime.Today;
            _category.DataSource = CreateCategoryOptions();
            _category.DisplayMember = nameof(InventoryReportCategory.Name);
            _category.ValueMember = nameof(InventoryReportCategory.CategoryId);
            _apply.Click += Apply_Click;
            _exportPdf.Click += ExportPdf_Click;
            ConfigureImportChart();
            ConfigureCategoryChart();
            LoadReport();
        }

        private static List<InventoryReportCategory> CreateCategoryOptions()
        {
            return new List<InventoryReportCategory>
            {
                new InventoryReportCategory { CategoryId = AllCategoriesValue, Name = "Tất cả danh mục" }
            };
        }

        private void ConfigureImportChart()
        {
            var area = new ChartArea("InventoryImportArea");
            area.BackColor = Color.White;
            area.AxisX.LabelStyle.ForeColor = Color.FromArgb(100, 116, 139);
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisX.LineColor = Color.FromArgb(226, 232, 240);
            area.AxisY.LabelStyle.ForeColor = Color.FromArgb(100, 116, 139);
            area.AxisY.LabelStyle.Format = "N0";
            area.AxisY.Title = "Số lượng";
            area.AxisY.TitleForeColor = Color.FromArgb(100, 116, 139);
            area.AxisY.MajorGrid.LineColor = Color.FromArgb(226, 232, 240);
            area.AxisY.Minimum = 0;
            _importChart.ChartAreas.Add(area);
            _importChart.Series.Add(new Series("Số lượng nhập")
            {
                ChartArea = area.Name,
                ChartType = SeriesChartType.Column,
                Color = Color.FromArgb(59, 130, 246),
                BorderColor = Color.FromArgb(37, 99, 235),
                BorderWidth = 1,
                IsValueShownAsLabel = false,
                ToolTip = "#VALX: #VAL{N0} sản phẩm"
            });
            _importChart.Series["Số lượng nhập"]["PointWidth"] = "0.65";
        }

        private void ConfigureCategoryChart()
        {
            var area = new ChartArea("InventoryCategoryArea");
            area.BackColor = Color.White;
            area.AxisX.LabelStyle.ForeColor = Color.FromArgb(100, 116, 139);
            area.AxisX.LabelStyle.Format = "#,##0";
            area.AxisX.MajorGrid.LineColor = Color.FromArgb(226, 232, 240);
            area.AxisX.Minimum = 0;
            area.AxisX.IsReversed = false;
            area.AxisY.LabelStyle.ForeColor = Color.FromArgb(71, 85, 105);
            area.AxisY.LabelStyle.Font = new Font("Segoe UI", 8F);
            area.AxisY.LabelStyle.Interval = 1;
            area.AxisY.Interval = 1;
            area.AxisY.MajorGrid.Enabled = false;
            area.AxisY.LineColor = Color.Transparent;
            area.AxisY.IsReversed = false;
            _categoryChart.ChartAreas.Add(area);
            var series = new Series("Giá trị tồn")
            {
                ChartArea = area.Name,
                ChartType = SeriesChartType.Bar,
                Color = Color.FromArgb(14, 165, 233),
                BorderColor = Color.FromArgb(2, 132, 199),
                BorderWidth = 1,
                IsXValueIndexed = true,
                IsValueShownAsLabel = false,
                ToolTip = "#VALX: #VAL{N0} đ"
            };
            series["PointWidth"] = "0.65";
            _categoryChart.Series.Add(series);
        }

        private void Apply_Click(object sender, EventArgs e)
        {
            if (!TryGetSelectedDateRange(out _, out _))
                return;

            LoadReport();
        }

        private bool TryGetSelectedDateRange(out DateTime from, out DateTime to)
        {
            from = _from.Value.Date;
            to = _to.Value.Date;
            if (from <= to)
                return true;

            MessageBox.Show(this, "Ngày bắt đầu không được sau ngày kết thúc.",
                "Khoảng thời gian không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        private int? GetSelectedCategoryId()
        {
            InventoryReportCategory category = _category.SelectedItem as InventoryReportCategory;
            if (category == null || category.CategoryId == AllCategoriesValue)
                return null;

            return category.CategoryId;
        }

        private void LoadReport()
        {
            if (!TryGetSelectedDateRange(out DateTime from, out DateTime to))
                return;

            try
            {
                int? selectedCategoryId = GetSelectedCategoryId();
                List<InventoryReportCategory> categories = _reportService.GetCategories();
                var options = CreateCategoryOptions();
                options.AddRange(categories);
                _category.DataSource = options;
                _category.DisplayMember = nameof(InventoryReportCategory.Name);
                _category.ValueMember = nameof(InventoryReportCategory.CategoryId);
                if (selectedCategoryId.HasValue)
                    _category.SelectedValue = selectedCategoryId.Value;

                InventoryReportData report = _reportService.GetReport(from, to, selectedCategoryId);
                RenderReport(report);
            }
            catch (SqlException exception)
            {
                MessageBox.Show(this, "Không thể tải báo cáo tồn kho từ cơ sở dữ liệu.\r\n\r\n"
                    + exception.Message, "Lỗi tải báo cáo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RenderReport(InventoryReportData report)
        {
            CultureInfo vietnamese = CultureInfo.GetCultureInfo("vi-VN");
            _productCount.Text = "SẢN PHẨM ĐANG KINH DOANH\r\n" + report.ProductCount.ToString("N0", vietnamese);
            _totalStock.Text = "TỔNG SỐ LƯỢNG TỒN\r\n" + report.TotalQuantity.ToString("N0", vietnamese);
            _stockValue.Text = "GIÁ TRỊ TỒN THEO GIÁ NHẬP\r\n" + FormatCurrency(report.TotalStockValue, vietnamese);
            _lowStock.Text = "CẦN BỔ SUNG / HẾT HẠN\r\n"
                + report.LowStockProductCount.ToString("N0", vietnamese) + " SP / "
                + report.ExpiredQuantity.ToString("N0", vietnamese) + " đơn vị";
            _chartTitle.Text = "Số lượng nhập theo ngày (" + FormatDateRange(_from.Value.Date, _to.Value.Date) + ")";

            _grid.Rows.Clear();
            foreach (InventoryReportProduct product in report.Products)
            {
                int rowIndex = _grid.Rows.Add(
                    product.ProductCode,
                    product.ProductName,
                    product.CategoryName,
                    product.Stock.ToString("N0", vietnamese),
                    FormatCurrency(product.ImportPrice, vietnamese),
                    FormatCurrency(product.StockValue, vietnamese),
                    GetStockStatus(product));
                if (product.Stock <= product.MinStock)
                    _grid.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.FromArgb(185, 28, 28);
            }

            Series series = _importChart.Series["Số lượng nhập"];
            series.Points.Clear();
            foreach (InventoryImportTrendPoint point in report.ImportTrend)
            {
                int pointIndex = series.Points.AddXY(point.Date.ToString("dd/MM"), point.Quantity);
                series.Points[pointIndex].ToolTip = point.Date.ToString("dd/MM/yyyy")
                    + ": " + point.Quantity.ToString("N0", vietnamese) + " sản phẩm";
            }
            _importChart.Invalidate();

            Series categorySeries = _categoryChart.Series["Giá trị tồn"];
            categorySeries.Points.Clear();
            foreach (var category in report.Products
                .GroupBy(product => string.IsNullOrWhiteSpace(product.CategoryName)
                    ? "Chưa phân loại"
                    : product.CategoryName)
                .Select(group => new
                {
                    Name = group.Key,
                    StockValue = group.Sum(product => product.StockValue)
                })
                .OrderByDescending(item => item.StockValue)
                .Take(5)
                .OrderBy(item => item.StockValue))
            {
                var point = new DataPoint
                {
                    AxisLabel = category.Name,
                    YValues = new[] { (double)category.StockValue },
                    ToolTip = category.Name + ": " + FormatCurrency(category.StockValue, vietnamese)
                };
                categorySeries.Points.Add(point);
            }
            _categoryChart.ChartAreas["InventoryCategoryArea"].RecalculateAxesScale();
            _categoryChart.Invalidate();
        }

        private static string GetStockStatus(InventoryReportProduct product)
        {
            if (product.Stock <= 0)
                return "Hết hàng";
            return product.Stock <= product.MinStock ? "Cần bổ sung" : "Đủ hàng";
        }

        private void ExportPdf_Click(object sender, EventArgs e)
        {
            if (!TryGetSelectedDateRange(out DateTime from, out DateTime to))
                return;

            try
            {
                InventoryReportData report = _reportService.GetReport(from, to, GetSelectedCategoryId());
                string categoryName = _category.SelectedItem is InventoryReportCategory category
                    ? category.Name
                    : "Tất cả danh mục";
                string path = FinancialReportPdfExporter.SaveInventory(
                    this,
                    report,
                    categoryName,
                    GetCurrentUserName(),
                    from,
                    to,
                    "Bao_cao_ton_kho_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".pdf",
                    "BÁO CÁO TỒN KHO");
                if (path != null)
                {
                    MessageBox.Show(this, "Đã tạo báo cáo PDF tại:\r\n" + path,
                        "Xuất báo cáo thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (SqlException exception)
            {
                ShowExportError(exception.Message);
            }
            catch (InvalidOperationException exception)
            {
                ShowExportError(exception.Message);
            }
            catch (IOException exception)
            {
                ShowExportError(exception.Message);
            }
            catch (UnauthorizedAccessException exception)
            {
                ShowExportError(exception.Message);
            }
            catch (System.ComponentModel.Win32Exception exception)
            {
                ShowExportError(exception.Message);
            }
            catch (System.Runtime.InteropServices.ExternalException exception)
            {
                ShowExportError(exception.Message);
            }
        }

        private static string FormatCurrency(decimal value, CultureInfo culture)
        {
            return value.ToString("N0", culture) + " đ";
        }

        private static string FormatDateRange(DateTime from, DateTime to)
        {
            return from == to
                ? from.ToString("dd/MM/yyyy")
                : from.ToString("dd/MM/yyyy") + " đến " + to.ToString("dd/MM/yyyy");
        }

        private static string GetCurrentUserName()
        {
            var user = SIMS_WinFormsApp.Services.Session.UserSession.Instance.CurrentUser;
            return user == null ? "Hệ thống" : user.FullName;
        }

        private void ShowExportError(string details)
        {
            MessageBox.Show(this, "Không thể tạo báo cáo PDF.\r\n\r\n" + details,
                "Lỗi tạo báo cáo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
