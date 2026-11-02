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
using SIMS_WinFormsApp.Models.DTOs.Inventory;
using SIMS_WinFormsApp.Services.Implementations.Export;
using SIMS_WinFormsApp.Services.Implementations.Reports;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.Views.Toast;

namespace SIMS_WinFormsApp.Views.Reports
{
    public partial class ucInventoryReport : UserControl
    {
        private const int AllCategoriesValue = 0;
        private const int HistoryPageSize = 10;
        private static readonly CultureInfo Vi = CultureInfo.GetCultureInfo("vi-VN");
        private readonly InventoryReportService _reportService;
        private readonly RevenueReportService _disposalReportService;
        private List<InventoryBatchHistoryRow> _historyRows = new List<InventoryBatchHistoryRow>();
        private List<InventoryStockBreakdownRow> _categoryBreakdown = new List<InventoryStockBreakdownRow>();
        private int _historyPage = 1;

        public ucInventoryReport()
        {
            InitializeComponent();

            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            _reportService = AppComposition.CreateInventoryReportService();
            _disposalReportService = AppComposition.CreateRevenueReportService();
            _from.Value = DateTime.Today.AddDays(-29);
            _to.Value = DateTime.Today;
            _category.DataSource = CreateCategoryOptions();
            _category.DisplayMember = nameof(InventoryReportCategory.Name);
            _category.ValueMember = nameof(InventoryReportCategory.CategoryId);
            _apply.Click += Apply_Click;
            _exportPdf.Click += ExportPdf_Click;
            ConfigureImportChart();
            ConfigureCategoryChart();
            ConfigureDisposalChart();
            _exportOptions.Click += (s, e) => _exportMenu.Show(_exportOptions, 0, _exportOptions.Height);
            _exportCategoryCsv.Click += (s, e) => ExportCategoryBreakdown(new CsvTableExporter());
            _exportCategoryExcel.Click += (s, e) => ExportCategoryBreakdown(new ExcelTableExporter());
            _categoryBreakdownGrid.CellPainting += RatioCell_Painting;
            _priceBreakdownGrid.CellPainting += RatioCell_Painting;
            _trendPreset.SelectedIndex = 1;
            ApplyTrendPreset();
            _trendPreset.SelectedIndexChanged += TrendPreset_Changed;
            _trendApply.Click += (s, e) => LoadTrend();
            _historySearchButton.Click += (s, e) => LoadHistory();
            _historySearch.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; LoadHistory(); } };
            _historyClear.Click += (s, e) => { _historySearch.Clear(); _historyFrom.Checked = false; _historyTo.Checked = false; LoadHistory(); };
            _historyPrev.Click += (s, e) => { if (_historyPage > 1) { _historyPage--; RenderHistoryPage(); } };
            _historyNext.Click += (s, e) => { _historyPage++; RenderHistoryPage(); };
            _historyExport.Click += (s, e) => _historyExportMenu.Show(_historyExport, 0, _historyExport.Height);
            _historyExportCsv.Click += (s, e) => ExportHistory(new CsvTableExporter());
            _historyExportExcel.Click += (s, e) => ExportHistory(new ExcelTableExporter());
            LoadReport();
            LoadTrend();
            LoadHistory();
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

        private void ConfigureDisposalChart()
        {
            var area = new ChartArea("InventoryDisposalArea");
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
            _disposalChart.ChartAreas.Add(area);
            var series = new Series("Thiệt hại")
            {
                ChartArea = area.Name,
                ChartType = SeriesChartType.Bar,
                Color = Color.FromArgb(239, 68, 68),
                BorderColor = Color.FromArgb(220, 38, 38),
                BorderWidth = 1,
                IsXValueIndexed = true,
                IsValueShownAsLabel = false,
                ToolTip = "#VALX: #VAL{N0} đ"
            };
            series["PointWidth"] = "0.65";
            _disposalChart.Series.Add(series);
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
                RenderDisposalChart(from, to);
                RenderStockSections(selectedCategoryId);
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

        private void RenderDisposal(StockDisposalReportData disposal)
        {
            CultureInfo vietnamese = CultureInfo.GetCultureInfo("vi-VN");
            _disposalSummary.Text = "THIỆT HẠI TIÊU HỦY\r\n"
                + FormatCurrency(disposal.TotalLoss, vietnamese) + " · "
                + disposal.DisposalCount.ToString("N0", vietnamese) + " phiếu · "
                + disposal.ProductQuantity.ToString("N0", vietnamese) + " SP";

            _disposalGrid.Rows.Clear();
            foreach (StockDisposalProductTotal product in disposal.Products)
            {
                int rowIndex = _disposalGrid.Rows.Add(
                    product.Name,
                    product.Quantity.ToString("N0", vietnamese),
                    FormatCurrency(product.LossAmount, vietnamese));
                _disposalGrid.Rows[rowIndex].Cells[0].ToolTipText = product.Name;
            }

            if (disposal.DisposalCount == 0)
                _disposalGrid.Rows.Add("Không có phiếu tiêu hủy trong kỳ", string.Empty, string.Empty);
        }

        private void RenderDisposalChart(DateTime from, DateTime to)
        {
            CultureInfo vietnamese = CultureInfo.GetCultureInfo("vi-VN");
            StockDisposalReportData disposal = _disposalReportService.GetStockDisposalReport(from, to);
            _disposalChartTitle.Text = "Thiệt hại tiêu hủy theo lý do ("
                + disposal.TotalLoss.ToString("N0", vietnamese) + " đ)";

            Series series = _disposalChart.Series["Thiệt hại"];
            series.Points.Clear();
            foreach (StockDisposalReasonTotal item in disposal.Reasons)
            {
                string label = DisposalReasons.ToLabel(item.Reason);
                var point = new DataPoint
                {
                    AxisLabel = label,
                    YValues = new[] { (double)item.LossAmount },
                    ToolTip = label + ": " + FormatCurrency(item.LossAmount, vietnamese)
                        + " · " + item.ProductQuantity.ToString("N0", vietnamese)
                        + " SP · " + item.DisposalCount + " phiếu"
                };
                series.Points.Add(point);
            }
            _disposalChart.ChartAreas["InventoryDisposalArea"].RecalculateAxesScale();
            _disposalChart.Invalidate();
            RenderDisposal(disposal);
        }

        private void RenderStockSections(int? categoryId)
        {
            InventoryStockSummary summary = _reportService.GetStockSummary(categoryId);
            _totalStock.Text = "Tổng số lượng tồn" + Environment.NewLine + summary.TotalQuantity.ToString("N0", Vi);
            _stockValue.Text = "Giá trị tồn (giá nhập)" + Environment.NewLine + FormatCurrency(summary.ImportValue, Vi);
            _sellValue.Text = "Giá trị tồn kho (giá bán)" + Environment.NewLine + FormatCurrency(summary.SellValue, Vi);
            _lowStock.Text = "Sản phẩm sắp hết hàng" + Environment.NewLine + summary.LowStockCount.ToString("N0", Vi);
            _outOfStock.Text = "Sản phẩm hết hàng" + Environment.NewLine + summary.OutOfStockCount.ToString("N0", Vi);
            _lowStock.ForeColor = summary.LowStockCount > 0
                ? Color.FromArgb(217, 119, 6)
                : _productCount.ForeColor;
            _outOfStock.ForeColor = summary.OutOfStockCount > 0
                ? Color.FromArgb(220, 38, 38)
                : _productCount.ForeColor;

            Series series = _productStockChart.Series["Tồn kho"];
            series.Points.Clear();
            foreach (InventoryProductStockPoint item in _reportService.GetTopProductStock(categoryId, 20))
            {
                string label = item.ProductName.Length > 18
                    ? item.ProductName.Substring(0, 18) + "…"
                    : item.ProductName;
                int pointIndex = series.Points.AddXY(label, item.Stock);
                DataPoint point = series.Points[pointIndex];
                if (item.Stock == 0)
                    point.Color = Color.FromArgb(148, 163, 184);
                else if (item.HasExpired)
                    point.Color = Color.FromArgb(239, 68, 68);
                else if (item.Stock <= item.MinStock
                    || (item.NearestExpiry.HasValue
                        && item.NearestExpiry.Value.Date <= DateTime.Today.AddDays(30)))
                    point.Color = Color.FromArgb(245, 158, 11);
                else
                    point.Color = Color.FromArgb(34, 197, 94);

                point.ToolTip = item.ProductCode + " · " + item.ProductName
                    + "\nTồn: " + item.Stock.ToString("N0", Vi)
                    + " (tối thiểu " + item.MinStock + ")"
                    + (item.NearestExpiry.HasValue
                        ? "\nHSD gần nhất: " + item.NearestExpiry.Value.ToString("dd/MM/yyyy")
                        : string.Empty);
            }

            ChartArea area = _productStockChart.ChartAreas["ProductStockArea"];
            area.AxisX.Interval = 1;
            area.AxisX.LabelStyle.Angle = -45;
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.LineColor = Color.FromArgb(226, 232, 240);
            area.RecalculateAxesScale();
            _productStockTitle.Text = "Tồn kho theo sản phẩm (top 20)"
                + (series.Points.Count == 0 ? " – chưa có dữ liệu" : string.Empty);

            _categoryBreakdown = _reportService.GetStockByCategory(categoryId);
            FillBreakdownGrid(_categoryBreakdownGrid, _categoryBreakdown);
            FillBreakdownGrid(_priceBreakdownGrid, _reportService.GetStockByPriceRange(categoryId));
        }

        private static void FillBreakdownGrid(
            DataGridView grid,
            List<InventoryStockBreakdownRow> rows)
        {
            grid.Rows.Clear();
            foreach (InventoryStockBreakdownRow row in rows)
            {
                grid.Rows.Add(
                    row.Label,
                    row.ProductCount.ToString("N0", Vi),
                    row.Quantity.ToString("N0", Vi),
                    FormatCurrency(row.SellValue, Vi),
                    row.Ratio);
            }
        }

        private void RatioCell_Painting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            var grid = sender as DataGridView;
            if (grid == null || e.RowIndex < 0
                || (grid.Columns[e.ColumnIndex].Name != "_cbRatio" && grid.Columns[e.ColumnIndex].Name != "_pbRatio")
                || !(e.Value is double ratio))
                return;

            e.PaintBackground(e.CellBounds, true);
            var track = new Rectangle(
                e.CellBounds.X + 6,
                e.CellBounds.Y + e.CellBounds.Height / 2 - 4,
                Math.Max(10, e.CellBounds.Width - 70),
                8);
            using (var trackBrush = new SolidBrush(Color.FromArgb(226, 232, 240)))
                e.Graphics.FillRectangle(trackBrush, track);
            int fillWidth = Math.Max(0, Math.Min(track.Width, (int)(track.Width * ratio / 100d)));
            if (fillWidth > 0)
            {
                using (var fillBrush = new SolidBrush(Color.FromArgb(59, 130, 246)))
                    e.Graphics.FillRectangle(fillBrush, track.X, track.Y, fillWidth, track.Height);
            }
            TextRenderer.DrawText(
                e.Graphics,
                ratio.ToString("0.0", Vi) + " %",
                e.CellStyle.Font,
                new Rectangle(track.Right + 4, e.CellBounds.Y, 60, e.CellBounds.Height),
                e.CellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
            e.Handled = true;
        }

        private void ApplyTrendPreset()
        {
            DateTime first = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime from;
            DateTime to = DateTime.Today;
            switch (_trendPreset.SelectedIndex)
            {
                case 0:
                    from = first.AddMonths(-2);
                    break;
                case 1:
                    from = first.AddMonths(-5);
                    break;
                case 2:
                    from = first.AddMonths(-11);
                    break;
                case 3:
                    from = new DateTime(DateTime.Today.Year, 1, 1);
                    break;
                case 4:
                    _trendFrom.Enabled = true;
                    _trendTo.Enabled = true;
                    return;
                default:
                    _trendFrom.Enabled = false;
                    _trendTo.Enabled = false;
                    return;
            }

            _trendFrom.Value = from;
            _trendTo.Value = to;
            _trendFrom.Enabled = false;
            _trendTo.Enabled = false;
        }

        private void TrendPreset_Changed(object sender, EventArgs e)
        {
            ApplyTrendPreset();
            if (_trendPreset.SelectedIndex != 4)
                LoadTrend();
        }

        private void LoadTrend()
        {
            try
            {
                InventoryMonthlyTrend trend = _reportService.GetMonthlyCategoryTrend(_trendFrom.Value, _trendTo.Value);
                _trendChart.Series.Clear();
                Color[] palette =
                {
                    Color.FromArgb(59, 130, 246),
                    Color.FromArgb(16, 185, 129),
                    Color.FromArgb(245, 158, 11),
                    Color.FromArgb(239, 68, 68),
                    Color.FromArgb(139, 92, 246),
                    Color.FromArgb(236, 72, 153),
                    Color.FromArgb(20, 184, 166),
                    Color.FromArgb(100, 116, 139)
                };

                for (int seriesIndex = 0; seriesIndex < trend.Series.Count; seriesIndex++)
                {
                    InventoryCategoryTrendSeries category = trend.Series[seriesIndex];
                    var series = new Series(category.CategoryName)
                    {
                        ChartType = SeriesChartType.Line,
                        ChartArea = "TrendArea",
                        Legend = "TrendLegend",
                        BorderWidth = 3,
                        MarkerStyle = MarkerStyle.Circle,
                        MarkerSize = 7,
                        IsXValueIndexed = true,
                        Color = palette[seriesIndex % palette.Length],
                        ToolTip = "#SERIESNAME – #VALX: #VAL{N0}"
                    };
                    for (int monthIndex = 0; monthIndex < trend.Months.Count; monthIndex++)
                    {
                        series.Points.AddXY(
                            trend.Months[monthIndex].ToString("MM/yyyy", Vi),
                            category.Quantities[monthIndex]);
                    }
                    _trendChart.Series.Add(series);
                }

                _trendChart.ChartAreas["TrendArea"].RecalculateAxesScale();
            }
            catch (ArgumentException exception)
            {
                AppToast.Warning(this, "Xu hướng tồn kho", exception.Message);
            }
            catch (SqlException exception)
            {
                AppToast.Error(this, "Lỗi", "Không tải được xu hướng tồn kho: " + exception.Message);
            }
        }

        private void LoadHistory()
        {
            DateTime? from = _historyFrom.Checked ? _historyFrom.Value.Date : (DateTime?)null;
            DateTime? to = _historyTo.Checked ? _historyTo.Value.Date : (DateTime?)null;
            if (from.HasValue && to.HasValue && from.Value > to.Value)
            {
                AppToast.Warning(this, "Lịch sử lô", "Từ ngày không được sau đến ngày.");
                return;
            }

            try
            {
                _historyRows = _reportService.GetBatchHistory(_historySearch.Text.Trim(), from, to);
                _historyPage = 1;
                RenderHistoryPage();
            }
            catch (SqlException exception)
            {
                AppToast.Error(this, "Lỗi", "Không tải được lịch sử lô: " + exception.Message);
            }
        }

        private void RenderHistoryPage()
        {
            int total = _historyRows.Count;
            int pages = Math.Max(1, (total + HistoryPageSize - 1) / HistoryPageSize);
            _historyPage = Math.Min(Math.Max(1, _historyPage), pages);
            _historyGrid.Rows.Clear();
            foreach (InventoryBatchHistoryRow row in _historyRows
                .Skip((_historyPage - 1) * HistoryPageSize)
                .Take(HistoryPageSize))
            {
                bool isIn = row.Direction == "IN";
                int rowIndex = _historyGrid.Rows.Add(
                    row.BatchCode,
                    row.ChangedAt.ToString("dd/MM/yyyy HH:mm", Vi),
                    row.DocumentType,
                    row.DocumentCode,
                    row.ProductCode,
                    row.ProductName,
                    row.StockAfter.ToString("N0", Vi),
                    (isIn ? "+" : "−") + row.Quantity.ToString("N0", Vi));
                DataGridViewCell change = _historyGrid.Rows[rowIndex].Cells["_hChange"];
                change.Style.ForeColor = isIn ? Color.FromArgb(22, 163, 74) : Color.FromArgb(220, 38, 38);
                change.Style.Font = new Font(_historyGrid.Font, FontStyle.Bold);

                string note = row.DocumentType == "Phiếu hủy" ? DisposalReasons.ToLabel(row.Note) : row.Note;
                string tip = "Lô " + row.BatchCode
                    + (string.IsNullOrEmpty(row.LotNumber) ? string.Empty : " · Số lô " + row.LotNumber)
                    + (row.ExpiryDate.HasValue
                        ? " · HSD " + row.ExpiryDate.Value.ToString("dd/MM/yyyy", Vi)
                        : string.Empty)
                    + "\nTồn trước " + row.StockBefore.ToString("N0", Vi)
                    + " → sau " + row.StockAfter.ToString("N0", Vi)
                    + (string.IsNullOrEmpty(note) ? string.Empty : "\nGhi chú: " + note);
                foreach (DataGridViewCell cell in _historyGrid.Rows[rowIndex].Cells)
                    cell.ToolTipText = tip;
            }

            _historyPageInfo.Text = total == 0
                ? "Không có chứng từ nào phù hợp"
                : "Trang " + _historyPage + "/" + pages + " · " + total.ToString("N0", Vi) + " dòng";
            _historyPrev.Enabled = _historyPage > 1;
            _historyNext.Enabled = _historyPage < pages;
        }

        private void ExportCategoryBreakdown(ITableDataExporter exporter)
        {
            string[] headers =
            {
                "Danh mục", "Số sản phẩm", "Số lượng tồn", "Giá trị tồn (giá bán)", "Tỷ lệ (%)"
            };
            List<object[]> rows = _categoryBreakdown
                .Select(row => new object[]
                {
                    row.Label,
                    row.ProductCount,
                    row.Quantity,
                    row.SellValue,
                    Math.Round(row.Ratio, 1)
                })
                .ToList();
            TableExportRunner.Run(this, exporter, "tồn kho theo danh mục", () => headers, () => rows);
        }

        private void ExportHistory(ITableDataExporter exporter)
        {
            string[] headers =
            {
                "Mã lô", "Số lô", "HSD", "Thời gian", "Loại chứng từ", "Mã chứng từ",
                "Mã SP", "Tên sản phẩm", "Tồn trước", "Thay đổi", "Tồn sau", "Ghi chú"
            };
            List<object[]> rows = _historyRows
                .Select(row => new object[]
                {
                    row.BatchCode,
                    row.LotNumber,
                    row.ExpiryDate.HasValue ? row.ExpiryDate.Value.ToString("dd/MM/yyyy", Vi) : string.Empty,
                    row.ChangedAt.ToString("dd/MM/yyyy HH:mm", Vi),
                    row.DocumentType,
                    row.DocumentCode,
                    row.ProductCode,
                    row.ProductName,
                    row.StockBefore,
                    row.Direction == "IN" ? row.Quantity : -row.Quantity,
                    row.StockAfter,
                    row.DocumentType == "Phiếu hủy" ? DisposalReasons.ToLabel(row.Note) : row.Note
                })
                .ToList();
            TableExportRunner.Run(this, exporter, "lịch sử lô hàng", () => headers, () => rows);
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
