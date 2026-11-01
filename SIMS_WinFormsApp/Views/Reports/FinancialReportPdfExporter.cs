using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using SIMS_WinFormsApp.Services.Implementations.Reports;

namespace SIMS_WinFormsApp.Views.Reports
{
    public static class FinancialReportPdfExporter
    {
        private const int PageWidth = 1240;
        private const int PageHeight = 1754;
        private static readonly Color Navy = Color.FromArgb(19, 38, 67);
        private static readonly Color Blue = Color.FromArgb(37, 99, 235);
        private static readonly Color Muted = Color.FromArgb(100, 116, 139);
        private static readonly Color Border = Color.FromArgb(226, 232, 240);
        private static readonly Color Body = Color.FromArgb(51, 65, 85);

        public static string SaveSections(
            IWin32Window owner,
            string defaultFileName,
            string title,
            string dateRange,
            string preparedBy,
            IReadOnlyList<PdfReportSection> sections)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Tiêu đề báo cáo là bắt buộc.", nameof(title));
            if (sections == null)
                throw new ArgumentNullException(nameof(sections));

            using (var dialog = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = "pdf",
                FileName = defaultFileName,
                Filter = "Tệp PDF (*.pdf)|*.pdf",
                OverwritePrompt = true
            })
            {
                if (dialog.ShowDialog(owner) != DialogResult.OK)
                    return null;

                try
                {
                    List<Bitmap> pages = RenderTextPages(title, dateRange, preparedBy, sections);
                    try
                    {
                        WritePdf(dialog.FileName, pages);
                    }
                    finally
                    {
                        foreach (Bitmap page in pages)
                            page.Dispose();
                    }
                    return dialog.FileName;
                }
                catch (ExternalException exception)
                {
                    throw new IOException("Không thể tạo nội dung ảnh cho báo cáo PDF.", exception);
                }
            }
        }

        public static string Save(
            IWin32Window owner,
            RevenueReportData revenue,
            StockDisposalReportData disposal,
            FinancialReportSignatories signatories,
            string preparedBy,
            DateTime from,
            DateTime to,
            RevenueReportPeriod period)
        {
            if (revenue == null) throw new ArgumentNullException(nameof(revenue));
            if (disposal == null) throw new ArgumentNullException(nameof(disposal));
            if (signatories == null) throw new ArgumentNullException(nameof(signatories));

            using (var dialog = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = "pdf",
                FileName = "Bao_cao_tong_hop_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".pdf",
                Filter = "Tệp PDF (*.pdf)|*.pdf",
                OverwritePrompt = true
            })
            {
                if (dialog.ShowDialog(owner) != DialogResult.OK)
                    return null;

                try
                {
                    DateTime generatedAt = DateTime.Now;
                    using (var summaryPage = RenderSummaryPage(
                        revenue, disposal, signatories, preparedBy, from, to, period, generatedAt))
                    using (var detailPage = RenderDetailsPage(
                        revenue, disposal, signatories, preparedBy, from, to, generatedAt))
                    {
                        WritePdf(dialog.FileName, new[] { summaryPage, detailPage });
                    }
                    return dialog.FileName;
                }
                catch (ExternalException exception)
                {
                    throw new IOException("Không thể tạo nội dung ảnh cho báo cáo PDF.", exception);
                }
            }
        }

        public static string SaveInventory(
            IWin32Window owner,
            InventoryReportData report,
            string categoryName,
            string preparedBy,
            DateTime from,
            DateTime to,
            string defaultFileName,
            string title)
        {
            if (report == null) throw new ArgumentNullException(nameof(report));
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Tiêu đề báo cáo là bắt buộc.", nameof(title));

            using (var dialog = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = "pdf",
                FileName = defaultFileName,
                Filter = "Tệp PDF (*.pdf)|*.pdf",
                OverwritePrompt = true
            })
            {
                if (dialog.ShowDialog(owner) != DialogResult.OK)
                    return null;

                try
                {
                    DateTime generatedAt = DateTime.Now;
                    List<Bitmap> pages = new List<Bitmap>
                    {
                        RenderInventoryOverviewPage(report, categoryName, preparedBy, from, to, title, generatedAt)
                    };
                    try
                    {
                        List<Bitmap> detailPages = RenderInventoryDetailPages(
                            report, categoryName, preparedBy, from, to, generatedAt);
                        pages.AddRange(detailPages);
                        pages.Add(RenderInventorySignatoriesPage(
                            report, categoryName, preparedBy, from, to, generatedAt, pages.Count + 1));
                        WritePdf(dialog.FileName, pages);
                    }
                    finally
                    {
                        foreach (Bitmap page in pages)
                            page.Dispose();
                    }
                    return dialog.FileName;
                }
                catch (ExternalException exception)
                {
                    throw new IOException("Không thể tạo nội dung ảnh cho báo cáo PDF.", exception);
                }
            }
        }

        private static List<Bitmap> RenderTextPages(
            string title,
            string dateRange,
            string preparedBy,
            IReadOnlyList<PdfReportSection> sections)
        {
            const int maximumLines = 32;
            var pages = new List<List<TextLine>> { new List<TextLine>() };
            using (var measureBitmap = new Bitmap(1, 1))
            using (Graphics measureGraphics = Graphics.FromImage(measureBitmap))
            using (var headingFont = new Font("Segoe UI", 13F, FontStyle.Bold))
            using (var bodyFont = new Font("Segoe UI", 10F))
            {
                foreach (PdfReportSection section in sections)
                {
                    AddTextLines(pages, maximumLines, WrapText(
                        measureGraphics, section.Title, headingFont, PageWidth - 160), true);
                    foreach (string paragraph in section.Paragraphs)
                        AddTextLines(pages, maximumLines, WrapText(
                            measureGraphics, paragraph ?? string.Empty, bodyFont, PageWidth - 180), false);
                }
            }

            var rendered = new List<Bitmap>();
            using (var titleFont = new Font("Segoe UI", 25F, FontStyle.Bold))
            using (var headingFont = new Font("Segoe UI", 13F, FontStyle.Bold))
            using (var bodyFont = new Font("Segoe UI", 10F))
            using (var titleBrush = new SolidBrush(Navy))
            using (var bodyBrush = new SolidBrush(Body))
            using (var mutedBrush = new SolidBrush(Muted))
            using (var separatorPen = new Pen(Border, 2F))
            {
                for (int pageIndex = 0; pageIndex < pages.Count; pageIndex++)
                {
                    Bitmap bitmap = CreatePage();
                    rendered.Add(bitmap);
                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    {
                        graphics.DrawString(title, titleFont, titleBrush,
                            new RectangleF(72, 48, PageWidth - 144, 48));
                        graphics.DrawString("Họ và tên: " + preparedBy, bodyFont, mutedBrush, 72, 116);
                        graphics.DrawString("Kỳ báo cáo: " + dateRange, bodyFont, mutedBrush, 72, 144);
                        graphics.DrawString("Ngày lập: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                            bodyFont, mutedBrush, 72, 172);
                        graphics.DrawLine(separatorPen, 72, 202, PageWidth - 72, 202);

                        int y = 230;
                        foreach (TextLine line in pages[pageIndex])
                        {
                            Font font = line.IsHeading ? headingFont : bodyFont;
                            Brush brush = line.IsHeading ? titleBrush : bodyBrush;
                            graphics.DrawString(line.Text, font, brush,
                                new RectangleF(78, y, PageWidth - 156, line.IsHeading ? 32 : 28));
                            y += line.IsHeading ? 42 : 30;
                        }

                        DrawFooter(graphics, pageIndex + 1, bodyFont, mutedBrush, separatorPen,
                            pages.Count);
                    }
                }
            }

            return rendered;
        }

        private static void AddTextLines(
            List<List<TextLine>> pages, int maximumLines, IReadOnlyList<string> lines, bool isHeading)
        {
            foreach (string line in lines)
            {
                if (pages[pages.Count - 1].Count >= maximumLines)
                    pages.Add(new List<TextLine>());
                pages[pages.Count - 1].Add(new TextLine(line, isHeading));
            }
        }

        private static List<string> WrapText(Graphics graphics, string text, Font font, float width)
        {
            var result = new List<string>();
            string current = string.Empty;
            foreach (string word in text.Split((char[])null, StringSplitOptions.RemoveEmptyEntries))
            {
                string candidate = current.Length == 0 ? word : current + " " + word;
                if (graphics.MeasureString(candidate, font).Width <= width)
                {
                    current = candidate;
                    continue;
                }

                if (current.Length > 0)
                    result.Add(current);
                current = word;
            }

            if (current.Length > 0 || result.Count == 0)
                result.Add(current);
            return result;
        }

        private static Bitmap RenderSummaryPage(
            RevenueReportData revenue,
            StockDisposalReportData disposal,
            FinancialReportSignatories signatories,
            string preparedBy,
            DateTime from,
            DateTime to,
            RevenueReportPeriod period,
            DateTime generatedAt)
        {
            var page = CreatePage();
            using (Graphics graphics = Graphics.FromImage(page))
            using (var titleFont = new Font("Segoe UI", 32F, FontStyle.Bold))
            using (var sectionFont = new Font("Segoe UI", 20F, FontStyle.Bold))
            using (var bodyFont = new Font("Segoe UI", 14F))
            using (var smallFont = new Font("Segoe UI", 12F))
            using (var metricValueFont = new Font("Segoe UI", 18F, FontStyle.Bold))
            using (var metricLabelFont = new Font("Segoe UI", 12F, FontStyle.Bold))
            using (var whiteBrush = new SolidBrush(Color.White))
            using (var darkBrush = new SolidBrush(Navy))
            using (var mutedBrush = new SolidBrush(Muted))
            using (var bodyBrush = new SolidBrush(Body))
            using (var borderPen = new Pen(Border, 2F))
            {
                DrawHeader(graphics, signatories.BusinessName, "BÁO CÁO TỔNG HỢP DOANH THU VÀ TIÊU HỦY",
                    FormatDateRange(from, to), preparedBy, generatedAt, titleFont, darkBrush);

                DrawSectionTitle(graphics, "TỔNG QUAN KẾT QUẢ", 278, sectionFont, darkBrush);
                decimal average = revenue.OrderCount == 0 ? 0m : revenue.Revenue / revenue.OrderCount;
                string[] metrics =
                {
                    "DOANH THU", FormatCurrency(revenue.Revenue),
                    "HÓA ĐƠN", revenue.OrderCount.ToString("N0", CultureInfo.GetCultureInfo("vi-VN")),
                    "TRUNG BÌNH / HÓA ĐƠN",
                    FormatCurrency(revenue.OrderCount == 0 ? 0m : revenue.Revenue / revenue.OrderCount),
                    "THIỆT HẠI TIÊU HỦY", FormatCurrency(disposal.TotalLoss),
                    "HÀNG ĐÃ TIÊU HỦY", disposal.ProductQuantity.ToString("N0", CultureInfo.GetCultureInfo("vi-VN")) + " sản phẩm"
                };
                int cardWidth = 208;
                int cardGap = 12;
                for (int i = 0; i < 5; i++)
                    DrawMetricCard(graphics, 72 + i * (cardWidth + cardGap), 326, cardWidth, 142,
                        metrics[i * 2], metrics[i * 2 + 1], i == 2 ? Color.FromArgb(185, 28, 28) : Blue,
                        metricValueFont, metricLabelFont, bodyBrush, borderPen);

                DrawSectionTitle(graphics, "DOANH THU THEO THỜI GIAN", 516, sectionFont, darkBrush);
                DrawLineChart(graphics, revenue.Trend, period, new Rectangle(72, 560, 1096, 430),
                    bodyFont, smallFont, borderPen, darkBrush, mutedBrush, blue: Blue);

                DrawSectionTitle(graphics, "THIỆT HẠI THEO LÝ DO TIÊU HỦY", 1050, sectionFont, darkBrush);
                DrawHorizontalBarChart(graphics, disposal.Reasons
                    .Select(item => new ChartBar(GetDisposalReasonName(item.Reason), item.LossAmount))
                    .ToList(), new Rectangle(72, 1095, 1096, 380), bodyFont, smallFont, borderPen, Blue);

                graphics.DrawLine(borderPen, 72, 1518, 1168, 1518);
                graphics.DrawString("Giá trị tiêu hủy được tính theo giá vốn ghi nhận trên các phiếu đã hoàn tất.",
                    bodyFont, mutedBrush, new RectangleF(72, 1540, 1096, 36));
                DrawFooter(graphics, 1, smallFont, mutedBrush, borderPen);
            }

            return page;
        }

        private static Bitmap RenderDetailsPage(
            RevenueReportData revenue,
            StockDisposalReportData disposal,
            FinancialReportSignatories signatories,
            string preparedBy,
            DateTime from,
            DateTime to,
            DateTime generatedAt)
        {
            var page = CreatePage();
            using (Graphics graphics = Graphics.FromImage(page))
            using (var titleFont = new Font("Segoe UI", 30F, FontStyle.Bold))
            using (var sectionFont = new Font("Segoe UI", 18F, FontStyle.Bold))
            using (var bodyFont = new Font("Segoe UI", 13F))
            using (var smallFont = new Font("Segoe UI", 11F))
            using (var darkBrush = new SolidBrush(Navy))
            using (var bodyBrush = new SolidBrush(Body))
            using (var mutedBrush = new SolidBrush(Muted))
            using (var borderPen = new Pen(Border, 2F))
            {
                DrawHeader(graphics, signatories.BusinessName, "CHI TIẾT VÀ XÁC NHẬN BÁO CÁO",
                    FormatDateRange(from, to), preparedBy, generatedAt, titleFont, darkBrush);

                int left = 72;
                int right = 646;
                int columnWidth = 522;
                DrawSectionTitle(graphics, "DOANH THU THEO DANH MỤC", 278, sectionFont, darkBrush);
                DrawTextRows(graphics, revenue.Categories
                    .Select(item => item.Name + "    " + FormatCurrency(item.Revenue))
                    .ToList(), left, 320, columnWidth, 270, bodyFont, bodyBrush, mutedBrush, borderPen);

                DrawSectionTitle(graphics, "SẢN PHẨM BÁN CHẠY", 620, sectionFont, darkBrush);
                DrawTextRows(graphics, revenue.TopProducts
                    .Select(item => item.Name + "    " + item.Quantity.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"))
                        + " sản phẩm    " + FormatCurrency(item.Revenue))
                    .ToList(), left, 660, columnWidth, 285, bodyFont, bodyBrush, mutedBrush, borderPen);

                DrawSectionTitle(graphics, "THIỆT HẠI THEO LÝ DO", 278, sectionFont, darkBrush, right);
                DrawTextRows(graphics, disposal.Reasons
                    .Select(item => GetDisposalReasonName(item.Reason) + "    "
                        + item.ProductQuantity.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"))
                        + " sản phẩm    " + FormatCurrency(item.LossAmount))
                    .ToList(), right, 320, columnWidth, 270, bodyFont, bodyBrush, mutedBrush, borderPen);

                DrawSectionTitle(graphics, "SẢN PHẨM GÂY THIỆT HẠI CAO", 620, sectionFont, darkBrush, right);
                DrawTextRows(graphics, disposal.Products
                    .Select(item => item.Name + "    "
                        + item.Quantity.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"))
                        + " sản phẩm    " + FormatCurrency(item.LossAmount))
                    .ToList(), right, 660, columnWidth, 285, bodyFont, bodyBrush, mutedBrush, borderPen);

                DrawSectionTitle(graphics, "PHẠM VI VÀ CÁCH TÍNH", 1008, sectionFont, darkBrush);
                DrawTextRows(graphics, new[]
                {
                    "Doanh thu gồm hóa đơn đang hoạt động được tạo trong khoảng thời gian đã chọn.",
                    "Phiếu tiêu hủy chỉ được tính khi đã hoàn tất; phiếu đã hủy được loại trừ.",
                    "Thiệt hại tiêu hủy tính theo giá vốn tại thời điểm ghi nhận, không gồm chi phí xử lý khác."
                }, 72, 1050, 1096, 172, bodyFont, bodyBrush, mutedBrush, borderPen);

                DrawSectionTitle(graphics, "XÁC NHẬN", 1252, sectionFont, darkBrush);
                DrawSignature(graphics, "GIÁM ĐỐC / ADMIN", signatories.DirectorName,
                    new Rectangle(72, 1295, 340, 220), bodyFont, smallFont, darkBrush, mutedBrush, borderPen);
                DrawSignature(graphics, "QUẢN LÝ KHO", signatories.InventoryManagerName,
                    new Rectangle(450, 1295, 340, 220), bodyFont, smallFont, darkBrush, mutedBrush, borderPen);
                DrawSignature(graphics, "NGƯỜI LẬP BÁO CÁO", preparedBy,
                    new Rectangle(828, 1295, 340, 220), bodyFont, smallFont, darkBrush, mutedBrush, borderPen);
                DrawFooter(graphics, 2, smallFont, mutedBrush, borderPen);
            }

            return page;
        }

        private static Bitmap RenderInventoryOverviewPage(
            InventoryReportData report,
            string categoryName,
            string preparedBy,
            DateTime from,
            DateTime to,
            string title,
            DateTime generatedAt)
        {
            var page = CreatePage();
            using (Graphics graphics = Graphics.FromImage(page))
            using (var titleFont = new Font("Segoe UI", 30F, FontStyle.Bold))
            using (var sectionFont = new Font("Segoe UI", 20F, FontStyle.Bold))
            using (var bodyFont = new Font("Segoe UI", 14F))
            using (var smallFont = new Font("Segoe UI", 12F))
            using (var metricValueFont = new Font("Segoe UI", 18F, FontStyle.Bold))
            using (var metricLabelFont = new Font("Segoe UI", 12F, FontStyle.Bold))
            using (var darkBrush = new SolidBrush(Navy))
            using (var mutedBrush = new SolidBrush(Muted))
            using (var bodyBrush = new SolidBrush(Body))
            using (var borderPen = new Pen(Border, 2F))
            {
                DrawInventoryHeader(graphics, report.BusinessName, title, preparedBy,
                    generatedAt, from, to, categoryName, titleFont, darkBrush);

                DrawSectionTitle(graphics, "TỔNG QUAN TỒN KHO", 278, sectionFont, darkBrush);
                CultureInfo vietnamese = CultureInfo.GetCultureInfo("vi-VN");
                string[] metrics =
                {
                    "SẢN PHẨM", report.ProductCount.ToString("N0", vietnamese),
                    "TỔNG SỐ LƯỢNG TỒN", report.TotalQuantity.ToString("N0", vietnamese),
                    "GIÁ TRỊ TỒN", FormatCurrency(report.TotalStockValue),
                    "CẦN BỔ SUNG", report.LowStockProductCount.ToString("N0", vietnamese) + " sản phẩm",
                    "HÀNG HẾT HẠN", report.ExpiredQuantity.ToString("N0", vietnamese) + " đơn vị"
                };
                const int cardWidth = 208;
                const int cardGap = 12;
                for (int i = 0; i < 5; i++)
                {
                    Color accent = i == 3 || i == 4
                        ? Color.FromArgb(185, 28, 28)
                        : Blue;
                    DrawMetricCard(graphics, 72 + i * (cardWidth + cardGap), 326,
                        cardWidth, 142, metrics[i * 2], metrics[i * 2 + 1],
                        accent, metricValueFont, metricLabelFont, bodyBrush, borderPen);
                }

                DrawSectionTitle(graphics, "TÌNH HÌNH NHẬP HÀNG", 510, sectionFont, darkBrush);
                DrawInventoryImportChart(graphics, report.ImportTrend,
                    new Rectangle(72, 554, 536, 520), bodyFont, smallFont, borderPen);
                DrawInventoryCategoryChart(graphics, report.Products,
                    new Rectangle(632, 554, 536, 520), bodyFont, smallFont, borderPen);

                DrawSectionTitle(graphics, "SẢN PHẨM CẦN QUAN TÂM", 1110, sectionFont, darkBrush);
                DrawInventoryAlerts(graphics, report, new Rectangle(72, 1154, 1096, 430),
                    bodyFont, smallFont, borderPen);
                DrawFooter(graphics, 1, smallFont, mutedBrush, borderPen,
                    2 + GetInventoryDetailPageCount(report));
            }

            return page;
        }

        private static List<Bitmap> RenderInventoryDetailPages(
            InventoryReportData report,
            string categoryName,
            string preparedBy,
            DateTime from,
            DateTime to,
            DateTime generatedAt)
        {
            const int rowsPerPage = 21;
            int pageCount = GetInventoryDetailPageCount(report);
            var pages = new List<Bitmap>(pageCount);
            using (var titleFont = new Font("Segoe UI", 30F, FontStyle.Bold))
            using (var sectionFont = new Font("Segoe UI", 20F, FontStyle.Bold))
            using (var bodyFont = new Font("Segoe UI", 12F))
            using (var smallFont = new Font("Segoe UI", 11F))
            using (var headerFont = new Font("Segoe UI", 11F, FontStyle.Bold))
            using (var darkBrush = new SolidBrush(Navy))
            using (var mutedBrush = new SolidBrush(Muted))
            using (var bodyBrush = new SolidBrush(Body))
            using (var borderPen = new Pen(Border, 1F))
            using (var headerFill = new SolidBrush(Color.FromArgb(239, 244, 250)))
            using (var alternateFill = new SolidBrush(Color.FromArgb(248, 250, 252)))
            using (var alertBrush = new SolidBrush(Color.FromArgb(185, 28, 28)))
            using (var textFormat = new StringFormat
            {
                Trimming = StringTrimming.EllipsisCharacter,
                LineAlignment = StringAlignment.Center
            })
            {
                for (int pageIndex = 0; pageIndex < pageCount; pageIndex++)
                {
                    Bitmap page = CreatePage();
                    pages.Add(page);
                    using (Graphics graphics = Graphics.FromImage(page))
                    {
                        DrawInventoryHeader(graphics, report.BusinessName, "CHI TIẾT TỒN KHO",
                            preparedBy, generatedAt, from, to, categoryName,
                            titleFont, darkBrush);
                        DrawSectionTitle(graphics, "DANH SÁCH SẢN PHẨM", 278, sectionFont, darkBrush);

                        int[] widths = { 110, 220, 150, 100, 110, 180, 226 };
                        string[] headings =
                        {
                            "MÃ SP", "TÊN SẢN PHẨM", "DANH MỤC", "TỒN KHO",
                            "TỒN TỐI THIỂU", "GIÁ TRỊ TỒN", "TRẠNG THÁI"
                        };
                        int tableX = 72;
                        int tableY = 326;
                        int rowHeight = 55;
                        int tableWidth = widths.Sum();
                        int headerHeight = 58;
                        graphics.FillRectangle(headerFill, tableX, tableY, tableWidth, headerHeight);
                        graphics.DrawRectangle(borderPen, tableX, tableY, tableWidth, headerHeight);
                        int columnX = tableX;
                        for (int column = 0; column < widths.Length; column++)
                        {
                            graphics.DrawString(headings[column], headerFont, darkBrush,
                                new RectangleF(columnX + 8, tableY + 4, widths[column] - 16, headerHeight - 8),
                                textFormat);
                            if (column > 0)
                                graphics.DrawLine(borderPen, columnX, tableY, columnX, tableY + headerHeight);
                            columnX += widths[column];
                        }

                        int firstProduct = pageIndex * rowsPerPage;
                        int rowCount = Math.Min(rowsPerPage, report.Products.Count - firstProduct);
                        if (rowCount <= 0)
                        {
                            graphics.DrawRectangle(borderPen, tableX, tableY + headerHeight,
                                tableWidth, rowHeight);
                            graphics.DrawString("Không có sản phẩm trong danh mục đã chọn.",
                                bodyFont, mutedBrush,
                                new RectangleF(tableX + 16, tableY + headerHeight + 8,
                                    tableWidth - 32, rowHeight - 16));
                        }
                        else
                        {
                            for (int row = 0; row < rowCount; row++)
                            {
                                InventoryReportProduct product = report.Products[firstProduct + row];
                                int rowY = tableY + headerHeight + row * rowHeight;
                                if (row % 2 == 1)
                                    graphics.FillRectangle(alternateFill, tableX, rowY, tableWidth, rowHeight);
                                graphics.DrawRectangle(borderPen, tableX, rowY, tableWidth, rowHeight);

                                string[] values =
                                {
                                    product.ProductCode ?? string.Empty,
                                    product.ProductName ?? string.Empty,
                                    product.CategoryName ?? string.Empty,
                                    product.Stock.ToString("N0", CultureInfo.GetCultureInfo("vi-VN")),
                                    product.MinStock.ToString("N0", CultureInfo.GetCultureInfo("vi-VN")),
                                    FormatCurrency(product.StockValue),
                                    product.Stock <= 0 ? "Hết hàng"
                                        : product.Stock <= product.MinStock ? "Cần bổ sung" : "Đủ hàng"
                                };
                                columnX = tableX;
                                for (int column = 0; column < widths.Length; column++)
                                {
                                    Brush valueBrush = column == 6 && product.Stock <= product.MinStock
                                        ? alertBrush
                                        : bodyBrush;
                                    graphics.DrawString(values[column], bodyFont, valueBrush,
                                        new RectangleF(columnX + 8, rowY + 4, widths[column] - 16, rowHeight - 8),
                                        textFormat);
                                    if (column > 0)
                                        graphics.DrawLine(borderPen, columnX, rowY, columnX, rowY + rowHeight);
                                    columnX += widths[column];
                                }
                            }
                        }

                        string pageCaption = report.Products.Count == 0
                            ? "Không có sản phẩm trong danh mục đã chọn."
                            : "Hiển thị " + (firstProduct + 1).ToString("N0",
                                CultureInfo.GetCultureInfo("vi-VN"))
                                + " - " + Math.Min(firstProduct + rowsPerPage, report.Products.Count).ToString("N0",
                                    CultureInfo.GetCultureInfo("vi-VN"))
                                + " / " + report.Products.Count.ToString("N0",
                                    CultureInfo.GetCultureInfo("vi-VN")) + " sản phẩm";
                        graphics.DrawString(pageCaption, smallFont, mutedBrush,
                            new RectangleF(72, 1550, 1096, 28));
                        graphics.DrawString(
                            "Giá trị tồn tính theo giá nhập hiện tại; kỳ báo cáo áp dụng cho biểu đồ nhập hàng.",
                            smallFont, mutedBrush, new RectangleF(72, 1580, 1096, 26));
                        DrawFooter(graphics, pageIndex + 2, smallFont, mutedBrush, borderPen, pageCount + 2);
                    }
                }
            }

            return pages;
        }

        private static Bitmap RenderInventorySignatoriesPage(
            InventoryReportData report,
            string categoryName,
            string preparedBy,
            DateTime from,
            DateTime to,
            DateTime generatedAt,
            int pageNumber)
        {
            var page = CreatePage();
            using (Graphics graphics = Graphics.FromImage(page))
            using (var titleFont = new Font("Segoe UI", 30F, FontStyle.Bold))
            using (var sectionFont = new Font("Segoe UI", 20F, FontStyle.Bold))
            using (var bodyFont = new Font("Segoe UI", 14F))
            using (var smallFont = new Font("Segoe UI", 12F))
            using (var darkBrush = new SolidBrush(Navy))
            using (var bodyBrush = new SolidBrush(Body))
            using (var mutedBrush = new SolidBrush(Muted))
            using (var borderPen = new Pen(Border, 2F))
            {
                DrawInventoryHeader(graphics, report.BusinessName, "XÁC NHẬN BÁO CÁO TỒN KHO",
                    preparedBy, generatedAt, from, to, categoryName, titleFont, darkBrush);
                DrawSectionTitle(graphics, "THÔNG TIN VÀ PHẠM VI BÁO CÁO", 330, sectionFont, darkBrush);
                graphics.DrawString(
                    "Báo cáo phản ánh số liệu tồn kho tại thời điểm "
                    + report.SnapshotDate.ToString("dd/MM/yyyy HH:mm") + ".",
                    bodyFont, bodyBrush, new RectangleF(90, 385, 1060, 40));
                graphics.DrawString(
                    "Biểu đồ nhập hàng áp dụng cho kỳ " + FormatDateRange(from, to)
                    + "; danh mục được chọn: " + categoryName + ".",
                    bodyFont, bodyBrush, new RectangleF(90, 435, 1060, 40));
                graphics.DrawString(
                    "Giá trị tồn được tính theo giá nhập hiện tại. Hàng hết hạn được tính từ các lô còn số lượng.",
                    bodyFont, bodyBrush, new RectangleF(90, 485, 1060, 40));

                DrawSectionTitle(graphics, "XÁC NHẬN VÀ KÝ TÊN", 650, sectionFont, darkBrush);
                DrawSignature(graphics, "GIÁM ĐỐC / ADMIN",
                    GetConfiguredSignatory(report.DirectorName),
                    new Rectangle(72, 710, 340, 250), bodyFont, smallFont,
                    darkBrush, mutedBrush, borderPen);
                DrawSignature(graphics, "QUẢN LÝ KHO",
                    GetConfiguredSignatory(report.InventoryManagerName),
                    new Rectangle(450, 710, 340, 250), bodyFont, smallFont,
                    darkBrush, mutedBrush, borderPen);
                DrawSignature(graphics, "NGƯỜI LẬP BÁO CÁO",
                    string.IsNullOrWhiteSpace(preparedBy) ? "Hệ thống" : preparedBy,
                    new Rectangle(828, 710, 340, 250), bodyFont, smallFont,
                    darkBrush, mutedBrush, borderPen);
                DrawFooter(graphics, pageNumber, smallFont, mutedBrush, borderPen,
                    pageNumber);
            }

            return page;
        }

        private static int GetInventoryDetailPageCount(InventoryReportData report)
        {
            const int rowsPerPage = 21;
            return Math.Max(1, (report.Products.Count + rowsPerPage - 1) / rowsPerPage);
        }

        private static string GetConfiguredSignatory(string name)
        {
            return string.IsNullOrWhiteSpace(name) ? "Chưa phân công" : name.Trim();
        }

        private static void DrawInventoryHeader(
            Graphics graphics,
            string businessName,
            string title,
            string preparedBy,
            DateTime generatedAt,
            DateTime from,
            DateTime to,
            string categoryName,
            Font titleFont,
            Brush titleBrush)
        {
            using (var headerBrush = new SolidBrush(Navy))
            using (var infoBackground = new SolidBrush(Color.FromArgb(241, 245, 249)))
            using (var infoBrush = new SolidBrush(Body))
            using (var infoAccentBrush = new SolidBrush(Blue))
            using (var infoBorder = new Pen(Border, 1F))
            using (var accentPen = new Pen(Color.FromArgb(96, 165, 250), 5F))
            using (var businessFont = new Font("Segoe UI", 22F, FontStyle.Bold))
            using (var metaFont = new Font("Segoe UI", 13F))
            using (var labelFont = new Font("Segoe UI", 13F, FontStyle.Bold))
            using (var whiteBrush = new SolidBrush(Color.White))
            using (var valueFormat = new StringFormat { Trimming = StringTrimming.EllipsisCharacter })
            using (var centered = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
                Trimming = StringTrimming.EllipsisCharacter
            })
            {
                graphics.FillRectangle(headerBrush, 0, 0, PageWidth, 100);
                graphics.FillRectangle(Brushes.White, 68, 16, 72, 68);
                graphics.DrawImage(Properties.Resources.logo_icon, new Rectangle(72, 20, 64, 60));
                graphics.DrawString(string.IsNullOrWhiteSpace(businessName)
                        ? "Chưa cấu hình tên cửa hàng"
                        : businessName.Trim(),
                    businessFont, whiteBrush, new RectangleF(160, 27, 1008, 48), centered);
                graphics.DrawLine(accentPen, 72, 98, 1168, 98);

                graphics.DrawString(title, titleFont, titleBrush,
                    new RectangleF(72, 112, 1096, 48), centered);
                graphics.FillRectangle(infoBackground, 72, 170, 1096, 82);
                graphics.DrawRectangle(infoBorder, 72, 170, 1096, 82);
                graphics.FillRectangle(infoAccentBrush, 72, 170, 6, 82);
                graphics.DrawString("Họ và tên:", labelFont, titleBrush, 94, 180);
                graphics.DrawString(preparedBy ?? "Hệ thống", metaFont, infoBrush, 205, 180);
                graphics.DrawString("Ngày lập:", labelFont, titleBrush, 780, 180);
                graphics.DrawString(generatedAt.ToString("dd/MM/yyyy HH:mm"), metaFont, infoBrush, 883, 180);
                graphics.DrawString("Kỳ nhập hàng:", labelFont, titleBrush, 94, 214);
                graphics.DrawString(FormatDateRange(from, to) + "   |   Danh mục: " + categoryName,
                    metaFont, infoBrush, new RectangleF(255, 210, 900, 30), valueFormat);
            }
        }

        private static void DrawInventoryImportChart(
            Graphics graphics,
            IReadOnlyList<InventoryImportTrendPoint> points,
            Rectangle bounds,
            Font labelFont,
            Font smallFont,
            Pen borderPen)
        {
            using (var whiteBrush = new SolidBrush(Color.White))
            using (var gridPen = new Pen(Border, 1F))
            using (var barBrush = new SolidBrush(Blue))
            using (var mutedBrush = new SolidBrush(Muted))
            using (var bodyBrush = new SolidBrush(Body))
            {
                graphics.FillRectangle(whiteBrush, bounds);
                graphics.DrawRectangle(borderPen, bounds);
                if (points.Count == 0)
                {
                    graphics.DrawString("Không ghi nhận nhập hàng trong kỳ đã chọn.",
                        labelFont, mutedBrush,
                        new RectangleF(bounds.Left + 32, bounds.Top + 48, bounds.Width - 64, 36));
                    return;
                }

                int left = bounds.Left + 94;
                int top = bounds.Top + 34;
                int width = bounds.Width - 126;
                int height = bounds.Height - 112;
                long max = Math.Max(1L, points.Max(point => point.Quantity));
                for (int line = 0; line <= 4; line++)
                {
                    int y = top + height * line / 4;
                    graphics.DrawLine(gridPen, left, y, left + width, y);
                    long value = max * (4 - line) / 4;
                    graphics.DrawString(value.ToString("N0", CultureInfo.GetCultureInfo("vi-VN")),
                        smallFont, mutedBrush, new RectangleF(bounds.Left + 8, y - 11, 76, 24));
                }

                float slotWidth = width / (float)points.Count;
                float barWidth = Math.Max(5F, Math.Min(38F, slotWidth * 0.62F));
                for (int i = 0; i < points.Count; i++)
                {
                    float barHeight = (float)(points[i].Quantity / (double)max) * height;
                    float x = left + i * slotWidth + (slotWidth - barWidth) / 2F;
                    float y = top + height - barHeight;
                    graphics.FillRectangle(barBrush, x, y, barWidth, Math.Max(2F, barHeight));
                    if (i == 0 || i == points.Count - 1 || i % Math.Max(1, points.Count / 7) == 0)
                        graphics.DrawString(points[i].Date.ToString("dd/MM"), smallFont, bodyBrush,
                            new RectangleF(x - 24, top + height + 12, Math.Max(48F, slotWidth + 8), 24));
                }

                graphics.DrawString("Số lượng nhập theo ngày", smallFont, mutedBrush,
                    new RectangleF(left, bounds.Bottom - 34, width, 22));
            }
        }

        private static void DrawInventoryCategoryChart(
            Graphics graphics,
            IReadOnlyList<InventoryReportProduct> products,
            Rectangle bounds,
            Font labelFont,
            Font smallFont,
            Pen borderPen)
        {
            List<ChartBar> categories = products
                .GroupBy(product => string.IsNullOrWhiteSpace(product.CategoryName)
                    ? "Chưa phân loại"
                    : product.CategoryName)
                .Select(group => new ChartBar(group.Key, group.Sum(product => product.StockValue)))
                .OrderByDescending(item => item.Value)
                .Take(5)
                .ToList();

            using (var whiteBrush = new SolidBrush(Color.White))
            using (var barBrush = new SolidBrush(Color.FromArgb(14, 165, 233)))
            using (var labelBrush = new SolidBrush(Body))
            using (var valueBrush = new SolidBrush(Muted))
            using (var gridPen = new Pen(Border, 1F))
            using (var textFormat = new StringFormat
            {
                Trimming = StringTrimming.EllipsisCharacter,
                LineAlignment = StringAlignment.Center
            })
            {
                graphics.FillRectangle(whiteBrush, bounds);
                graphics.DrawRectangle(borderPen, bounds);
                graphics.DrawString("Giá trị tồn theo danh mục", labelFont, labelBrush,
                    new RectangleF(bounds.Left + 18, bounds.Top + 16, bounds.Width - 36, 32));
                if (categories.Count == 0)
                {
                    graphics.DrawString("Không có dữ liệu tồn kho theo danh mục.",
                        labelFont, valueBrush,
                        new RectangleF(bounds.Left + 20, bounds.Top + 66, bounds.Width - 40, 36));
                    return;
                }

                decimal max = Math.Max(1m, categories.Max(item => item.Value));
                int rowHeight = Math.Min(58, (bounds.Height - 82) / categories.Count);
                int labelWidth = 160;
                int valueWidth = 112;
                int barX = bounds.Left + labelWidth + 18;
                int availableBarWidth = Math.Max(20, bounds.Width - labelWidth - valueWidth - 54);
                for (int i = 0; i < categories.Count; i++)
                {
                    int rowY = bounds.Top + 62 + i * rowHeight;
                    graphics.DrawString(categories[i].Name, smallFont, labelBrush,
                        new RectangleF(bounds.Left + 14, rowY, labelWidth, rowHeight - 4), textFormat);
                    graphics.DrawLine(gridPen, barX, rowY + rowHeight - 5,
                        barX + availableBarWidth, rowY + rowHeight - 5);
                    int barWidth = (int)(availableBarWidth * categories[i].Value / max);
                    graphics.FillRectangle(barBrush, barX, rowY + 10, Math.Max(2, barWidth),
                        Math.Max(8, rowHeight - 20));
                    graphics.DrawString(FormatCompactCurrency(categories[i].Value), smallFont, valueBrush,
                        new RectangleF(bounds.Right - valueWidth - 8, rowY, valueWidth, rowHeight - 4),
                        textFormat);
                }
            }
        }

        private static void DrawInventoryAlerts(
            Graphics graphics,
            InventoryReportData report,
            Rectangle bounds,
            Font bodyFont,
            Font smallFont,
            Pen borderPen)
        {
            using (var panelBrush = new SolidBrush(Color.FromArgb(255, 251, 235)))
            using (var alertBrush = new SolidBrush(Color.FromArgb(185, 28, 28)))
            using (var textBrush = new SolidBrush(Body))
            using (var mutedBrush = new SolidBrush(Muted))
            using (var accentBrush = new SolidBrush(Color.FromArgb(217, 119, 6)))
            {
                graphics.FillRectangle(panelBrush, bounds);
                graphics.DrawRectangle(borderPen, bounds);
                graphics.FillRectangle(accentBrush, bounds.X, bounds.Y, 7, bounds.Height);
                graphics.DrawString("Hàng cần bổ sung: " + report.LowStockProductCount.ToString("N0",
                    CultureInfo.GetCultureInfo("vi-VN")) + " sản phẩm",
                    bodyFont, alertBrush, new RectangleF(bounds.X + 24, bounds.Y + 18, bounds.Width - 48, 30));
                graphics.DrawString("Hàng hết hạn còn trong kho: " + report.ExpiredQuantity.ToString("N0",
                    CultureInfo.GetCultureInfo("vi-VN")) + " đơn vị",
                    bodyFont, alertBrush, new RectangleF(bounds.X + 24, bounds.Y + 56, bounds.Width - 48, 30));

                List<InventoryReportProduct> lowStock = report.Products
                    .Where(product => product.Stock <= product.MinStock)
                    .Take(6)
                    .ToList();
                if (lowStock.Count == 0)
                {
                    graphics.DrawString("Không có sản phẩm nào cần bổ sung theo mức tồn tối thiểu.",
                        smallFont, mutedBrush,
                        new RectangleF(bounds.X + 24, bounds.Y + 112, bounds.Width - 48, 28));
                    return;
                }

                int rowY = bounds.Y + 108;
                int rowHeight = 48;
                using (var separatorPen = new Pen(Color.FromArgb(245, 220, 180), 1F))
                {
                    foreach (InventoryReportProduct product in lowStock)
                    {
                        string detail = product.ProductCode + "  " + product.ProductName
                            + "     Tồn: " + product.Stock.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"))
                            + "     Tối thiểu: " + product.MinStock.ToString("N0",
                                CultureInfo.GetCultureInfo("vi-VN"));
                        graphics.DrawString(detail, smallFont, textBrush,
                            new RectangleF(bounds.X + 24, rowY, bounds.Width - 48, rowHeight - 4));
                        rowY += rowHeight;
                        if (rowY < bounds.Bottom - 12)
                            graphics.DrawLine(separatorPen, bounds.X + 24, rowY - 5, bounds.Right - 18, rowY - 5);
                    }
                }
            }
        }

        private static Bitmap CreatePage()
        {
            var page = new Bitmap(PageWidth, PageHeight, PixelFormat.Format24bppRgb);
            using (Graphics graphics = Graphics.FromImage(page))
            {
                graphics.Clear(Color.White);
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            }
            return page;
        }

        private static void DrawHeader(
            Graphics graphics,
            string businessName,
            string reportTitle,
            string dateRange,
            string preparedBy,
            DateTime generatedAt,
            Font titleFont,
            Brush titleBrush)
        {
            using (var backgroundBrush = new SolidBrush(Navy))
            using (var businessFont = new Font("Segoe UI", 22F, FontStyle.Bold))
            using (var metaFont = new Font("Segoe UI", 13F))
            using (var metaLabelFont = new Font("Segoe UI", 13F, FontStyle.Bold))
            using (var accentPen = new Pen(Color.FromArgb(96, 165, 250), 5F))
            using (var metaBrush = new SolidBrush(Color.FromArgb(219, 234, 254)))
            using (var infoBackground = new SolidBrush(Color.FromArgb(241, 245, 249)))
            using (var infoBrush = new SolidBrush(Body))
            using (var infoAccentBrush = new SolidBrush(Blue))
            using (var infoBorder = new Pen(Border, 1F))
            using (var headerWhiteBrush = new SolidBrush(Color.White))
            using (var titleFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
                Trimming = StringTrimming.EllipsisCharacter
            })
            {
                graphics.FillRectangle(backgroundBrush, 0, 0, PageWidth, 100);
                graphics.FillRectangle(Brushes.White, 68, 16, 72, 68);
                graphics.DrawImage(Properties.Resources.logo_icon, new Rectangle(72, 20, 64, 60));
                graphics.DrawString(businessName, businessFont, headerWhiteBrush,
                    new RectangleF(160, 27, 1008, 48), titleFormat);
                graphics.DrawLine(accentPen, 72, 98, 1168, 98);

                graphics.DrawString(reportTitle, titleFont, titleBrush,
                    new RectangleF(72, 112, 1096, 48), titleFormat);
                graphics.FillRectangle(infoBackground, 72, 170, 1096, 82);
                graphics.DrawRectangle(infoBorder, 72, 170, 1096, 82);
                graphics.FillRectangle(infoAccentBrush, 72, 170, 6, 82);
                graphics.DrawString("Họ và tên:", metaLabelFont, titleBrush, 94, 180);
                graphics.DrawString(preparedBy, metaFont, infoBrush, 205, 180);
                graphics.DrawString("Ngày lập:", metaLabelFont, titleBrush, 780, 180);
                graphics.DrawString(generatedAt.ToString("dd/MM/yyyy HH:mm"),
                    metaFont, infoBrush, 883, 180);
                graphics.DrawString("Kỳ báo cáo:", metaLabelFont, titleBrush, 94, 214);
                graphics.DrawString(dateRange, metaFont, infoBrush, 222, 214);
            }
        }

        private static void DrawMetricCard(
            Graphics graphics, int x, int y, int width, int height,
            string title, string value, Color accent, Font valueFont, Font labelFont,
            Brush bodyBrush, Pen borderPen)
        {
            using (var fill = new SolidBrush(Color.White))
            using (var accentBrush = new SolidBrush(accent))
            using (var titleBrush = new SolidBrush(Muted))
            using (var dividerPen = new Pen(Border, 1F))
            {
                graphics.FillRectangle(fill, x, y, width, height);
                graphics.DrawRectangle(borderPen, x, y, width, height);
                graphics.FillRectangle(accentBrush, x, y, 7, height);
                graphics.DrawString(title, labelFont, titleBrush, new RectangleF(x + 20, y + 18, width - 32, 42));
                graphics.DrawLine(dividerPen, x + 20, y + 67, x + width - 16, y + 67);
                graphics.DrawString(value, valueFont, bodyBrush, new RectangleF(x + 20, y + 80, width - 32, 50));
            }
        }

        private static void DrawSectionTitle(
            Graphics graphics, string title, int y, Font font, Brush brush, int x = 72)
        {
            using (var accentBrush = new SolidBrush(Blue))
            {
                graphics.FillRectangle(accentBrush, x, y + 4, 7, 28);
                graphics.DrawString(title, font, brush, new PointF(x + 17, y));
            }
        }

        private static void DrawLineChart(
            Graphics graphics,
            IReadOnlyList<RevenueTrendPoint> points,
            RevenueReportPeriod period,
            Rectangle bounds,
            Font labelFont,
            Font smallFont,
            Pen borderPen,
            Brush titleBrush,
            Brush mutedBrush,
            Color blue)
        {
            using (var whiteBrush = new SolidBrush(Color.White))
            using (var gridPen = new Pen(Border, 1F))
            using (var linePen = new Pen(blue, 5F))
            using (var pointBrush = new SolidBrush(blue))
            {
                graphics.FillRectangle(whiteBrush, bounds);
                graphics.DrawRectangle(borderPen, bounds);
                int plotLeft = bounds.Left + 72;
                int plotTop = bounds.Top + 35;
                int plotWidth = bounds.Width - 105;
                int plotHeight = bounds.Height - 105;
                decimal max = points.Count == 0 ? 1m : Math.Max(1m, points.Max(point => point.Revenue));

                for (int line = 0; line <= 4; line++)
                {
                    int y = plotTop + plotHeight * line / 4;
                    graphics.DrawLine(gridPen, plotLeft, y, plotLeft + plotWidth, y);
                    decimal value = max * (4 - line) / 4m;
                    graphics.DrawString(FormatCompactCurrency(value), smallFont, mutedBrush,
                        new RectangleF(bounds.Left + 6, y - 10, 60, 20));
                }

                if (points.Count == 0)
                {
                    graphics.DrawString("Không có dữ liệu doanh thu trong kỳ.",
                        labelFont, mutedBrush, new RectangleF(plotLeft, plotTop + plotHeight / 2, plotWidth, 32));
                    return;
                }

                PointF? previous = null;
                for (int i = 0; i < points.Count; i++)
                {
                    float x = points.Count == 1
                        ? plotLeft + plotWidth / 2F
                        : plotLeft + plotWidth * i / (float)(points.Count - 1);
                    float y = plotTop + plotHeight
                        - (float)(points[i].Revenue / max) * plotHeight;
                    var current = new PointF(x, y);
                    if (previous.HasValue)
                        graphics.DrawLine(linePen, previous.Value, current);
                    graphics.FillEllipse(pointBrush, x - 7, y - 7, 14, 14);
                    previous = current;

                    if (i == 0 || i == points.Count - 1 || i % Math.Max(1, points.Count / 6) == 0)
                    {
                        string label = FormatPeriod(points[i].PeriodStart, period);
                        graphics.DrawString(label, smallFont, mutedBrush,
                            new RectangleF(x - 48, plotTop + plotHeight + 12, 96, 22));
                    }
                }

                if (points.Count > 0)
                {
                    RevenueTrendPoint last = points[points.Count - 1];
                    graphics.DrawString("Doanh thu: " + FormatCurrency(last.Revenue),
                        smallFont, titleBrush, new RectangleF(plotLeft, bounds.Bottom - 34, plotWidth, 22));
                }
            }
        }

        private static void DrawHorizontalBarChart(
            Graphics graphics,
            IReadOnlyList<ChartBar> bars,
            Rectangle bounds,
            Font labelFont,
            Font smallFont,
            Pen borderPen,
            Color barColor)
        {
            using (var whiteBrush = new SolidBrush(Color.White))
            using (var barBrush = new SolidBrush(barColor))
            using (var labelBrush = new SolidBrush(Body))
            using (var valueBrush = new SolidBrush(Muted))
            {
                graphics.FillRectangle(whiteBrush, bounds);
                graphics.DrawRectangle(borderPen, bounds);
                if (bars.Count == 0)
                {
                    graphics.DrawString("Không có phiếu tiêu hủy hoàn tất trong kỳ.",
                        labelFont, valueBrush, new RectangleF(bounds.Left + 24, bounds.Top + 40, bounds.Width - 48, 30));
                    return;
                }

                decimal max = Math.Max(1m, bars.Max(bar => bar.Value));
                int rowHeight = Math.Min(62, (bounds.Height - 40) / bars.Count);
                for (int i = 0; i < bars.Count; i++)
                {
                    int y = bounds.Top + 18 + i * rowHeight;
                    graphics.DrawString(bars[i].Name, smallFont, labelBrush,
                        new RectangleF(bounds.Left + 18, y, 245, rowHeight - 4));
                    int barX = bounds.Left + 270;
                    int barWidth = (int)((bounds.Width - 580) * bars[i].Value / max);
                    graphics.FillRectangle(barBrush, barX, y + 5, Math.Max(2, barWidth), 22);
                    graphics.DrawString(FormatCurrency(bars[i].Value), smallFont, valueBrush,
                        new RectangleF(bounds.Right - 285, y, 265, rowHeight - 4));
                }
            }
        }

        private static void DrawTextRows(
            Graphics graphics,
            IReadOnlyList<string> rows,
            int x,
            int y,
            int width,
            int height,
            Font font,
            Brush textBrush,
            Brush mutedBrush,
            Pen borderPen)
        {
            graphics.DrawRectangle(borderPen, x, y, width, height);
            if (rows.Count == 0)
            {
                graphics.DrawString("Không có dữ liệu trong kỳ.", font, mutedBrush,
                    new RectangleF(x + 16, y + 16, width - 32, 34));
                return;
            }

            int rowHeight = Math.Max(28, (height - 12) / Math.Min(rows.Count, 8));
            for (int i = 0; i < rows.Count && i < 8; i++)
            {
                int rowY = y + 8 + i * rowHeight;
                graphics.DrawString(rows[i], font, textBrush,
                    new RectangleF(x + 14, rowY, width - 28, rowHeight - 2));
                if (i < Math.Min(rows.Count, 8) - 1)
                    graphics.DrawLine(borderPen, x + 12, rowY + rowHeight - 2, x + width - 12, rowY + rowHeight - 2);
            }
        }

        private static void DrawSignature(
            Graphics graphics,
            string role,
            string name,
            Rectangle bounds,
            Font bodyFont,
            Font smallFont,
            Brush darkBrush,
            Brush mutedBrush,
            Pen borderPen)
        {
            using (var whiteBrush = new SolidBrush(Color.White))
            using (var lightPen = new Pen(Border, 1F))
            {
                graphics.FillRectangle(whiteBrush, bounds);
                graphics.DrawRectangle(borderPen, bounds);
                graphics.DrawString(role, bodyFont, darkBrush,
                    new RectangleF(bounds.X + 12, bounds.Y + 12, bounds.Width - 24, 28));
                graphics.DrawString("Ký và ghi rõ họ tên", smallFont, mutedBrush,
                    new RectangleF(bounds.X + 12, bounds.Y + 48, bounds.Width - 24, 24));
                graphics.DrawLine(lightPen, bounds.X + 16, bounds.Bottom - 55, bounds.Right - 16, bounds.Bottom - 55);
                graphics.DrawString(name, smallFont, darkBrush,
                    new RectangleF(bounds.X + 12, bounds.Bottom - 44, bounds.Width - 24, 28));
            }
        }

        private static void DrawFooter(Graphics graphics, int page, Font font, Brush brush, Pen pen, int pageCount = 2)
        {
            graphics.DrawLine(pen, 72, 1640, 1168, 1640);
            graphics.DrawString("Tài liệu tổng hợp từ dữ liệu đã ghi nhận trong hệ thống.",
                font, brush, new PointF(72, 1655));
            graphics.DrawString("Trang " + page + " / " + pageCount, font, brush, new PointF(1060, 1655));
        }

        private static void WritePdf(string path, IReadOnlyList<Bitmap> pages)
        {
            var images = pages.Select(EncodeJpeg).ToList();
            var objects = new List<byte[]>();
            objects.Add(Ascii("<< /Type /Catalog /Pages 2 0 R >>"));

            int pageCount = images.Count;
            var kids = string.Join(" ", Enumerable.Range(0, pageCount)
                .Select(index => (3 + index * 3) + " 0 R"));
            objects.Add(Ascii("<< /Type /Pages /Kids [ " + kids + " ] /Count " + pageCount + " >>"));

            for (int i = 0; i < pageCount; i++)
            {
                int pageObject = 3 + i * 3;
                int contentObject = pageObject + 1;
                int imageObject = pageObject + 2;
                objects.Add(Ascii("<< /Type /Page /Parent 2 0 R /MediaBox [0 0 595 842] "
                    + "/Resources << /XObject << /Im" + i + " " + imageObject + " 0 R >> >> "
                    + "/Contents " + contentObject + " 0 R >>"));

                byte[] content = Ascii("q\n595 0 0 842 0 0 cm\n/Im" + i + " Do\nQ\n");
                objects.Add(BuildStreamObject("<< /Length " + content.Length + " >>", content));
                objects.Add(BuildStreamObject(
                    "<< /Type /XObject /Subtype /Image /Width " + PageWidth
                    + " /Height " + PageHeight
                    + " /ColorSpace /DeviceRGB /BitsPerComponent 8 /Filter /DCTDecode /Length "
                    + images[i].Length + " >>",
                    images[i]));
            }

            using (var output = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                WriteAscii(output, "%PDF-1.4\n");
                var offsets = new List<long> { 0 };
                for (int i = 0; i < objects.Count; i++)
                {
                    offsets.Add(output.Position);
                    WriteAscii(output, (i + 1) + " 0 obj\n");
                    output.Write(objects[i], 0, objects[i].Length);
                    WriteAscii(output, "\nendobj\n");
                }

                long xrefOffset = output.Position;
                WriteAscii(output, "xref\n0 " + (objects.Count + 1) + "\n");
                WriteAscii(output, "0000000000 65535 f \n");
                for (int i = 1; i < offsets.Count; i++)
                    WriteAscii(output, offsets[i].ToString("D10", CultureInfo.InvariantCulture) + " 00000 n \n");
                WriteAscii(output, "trailer\n<< /Size " + (objects.Count + 1) + " /Root 1 0 R >>\n");
                WriteAscii(output, "startxref\n" + xrefOffset + "\n%%EOF");
            }
        }

        private static byte[] EncodeJpeg(Bitmap page)
        {
            ImageCodecInfo codec = ImageCodecInfo.GetImageEncoders()
                .FirstOrDefault(item => item.MimeType == "image/jpeg");
            if (codec == null)
                throw new InvalidOperationException("Không tìm thấy bộ mã hóa JPEG để tạo báo cáo PDF.");

            using (var stream = new MemoryStream())
            using (var parameters = new EncoderParameters(1))
            {
                parameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 88L);
                page.Save(stream, codec, parameters);
                return stream.ToArray();
            }
        }

        private static byte[] BuildStreamObject(string dictionary, byte[] content)
        {
            byte[] prefix = Ascii(dictionary + "\nstream\n");
            byte[] suffix = Ascii("\nendstream");
            var result = new byte[prefix.Length + content.Length + suffix.Length];
            Buffer.BlockCopy(prefix, 0, result, 0, prefix.Length);
            Buffer.BlockCopy(content, 0, result, prefix.Length, content.Length);
            Buffer.BlockCopy(suffix, 0, result, prefix.Length + content.Length, suffix.Length);
            return result;
        }

        private static byte[] Ascii(string text) => Encoding.ASCII.GetBytes(text);

        private static void WriteAscii(Stream stream, string text)
        {
            byte[] bytes = Ascii(text);
            stream.Write(bytes, 0, bytes.Length);
        }

        private static string FormatCurrency(decimal value)
        {
            return value.ToString("N0", CultureInfo.GetCultureInfo("vi-VN")) + " đ";
        }

        private static string FormatCompactCurrency(decimal value)
        {
            if (value >= 1000000000m)
                return (value / 1000000000m).ToString("0.#", CultureInfo.GetCultureInfo("vi-VN")) + " tỷ";
            if (value >= 1000000m)
                return (value / 1000000m).ToString("0.#", CultureInfo.GetCultureInfo("vi-VN")) + " tr";
            if (value >= 1000m)
                return (value / 1000m).ToString("0.#", CultureInfo.GetCultureInfo("vi-VN")) + " k";
            return value.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
        }

        private static string FormatDateRange(DateTime from, DateTime to)
        {
            return from.Date == to.Date
                ? from.ToString("dd/MM/yyyy")
                : from.ToString("dd/MM/yyyy") + " đến " + to.ToString("dd/MM/yyyy");
        }

        private static string FormatPeriod(DateTime date, RevenueReportPeriod period)
        {
            switch (period)
            {
                case RevenueReportPeriod.Month:
                    return date.ToString("MM/yyyy");
                case RevenueReportPeriod.Quarter:
                    return "Q" + (((date.Month - 1) / 3) + 1) + " " + date.Year;
                default:
                    return date.ToString("dd/MM");
            }
        }

        private static string GetDisposalReasonName(string reason)
        {
            switch (reason)
            {
                case "EXPIRED": return "Hàng hết hạn";
                case "DAMAGED": return "Hàng hư hỏng";
                case "QUALITY": return "Không đạt chất lượng";
                case "OTHER": return "Lý do khác";
                default: return "Chưa phân loại";
            }
        }

        private sealed class ChartBar
        {
            public ChartBar(string name, decimal value)
            {
                Name = name;
                Value = value;
            }

            public string Name { get; }
            public decimal Value { get; }
        }

        private sealed class TextLine
        {
            public TextLine(string text, bool isHeading)
            {
                Text = text;
                IsHeading = isHeading;
            }

            public string Text { get; }
            public bool IsHeading { get; }
        }
    }
}
