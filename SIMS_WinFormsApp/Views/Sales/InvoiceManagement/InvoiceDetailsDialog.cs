using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Views.Sales.InvoiceManagement
{
    public sealed partial class InvoiceDetailsDialog : Form
    {
        private readonly InvoiceDetails _invoice;
        private readonly PrintDocument _printDocument;
        private int _nextLineToPrint;

        public InvoiceDetailsDialog()
            : this(CreateDesignerInvoice())
        {
        }

        internal InvoiceDetailsDialog(InvoiceDetails invoice)
        {
            _invoice = invoice ?? throw new ArgumentNullException(nameof(invoice));
            _printDocument = new PrintDocument();
            _printDocument.BeginPrint += (sender, args) => _nextLineToPrint = 0;
            _printDocument.PrintPage += PrintDocument_PrintPage;

            InitializeComponent();
            _closeButton.Click += CloseButton_Click;
            _printButton.Click += ShowPrintPreview;
            PopulateInvoice();
            ApplyThemeColors();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
                _printDocument.Dispose();
            }
            base.Dispose(disposing);
        }

        private static InvoiceDetails CreateDesignerInvoice()
        {
            var invoice = new InvoiceDetails
            {
                InvoiceId = 1,
                InvoiceCode = "HD-20261028-0001",
                CreatedAt = new DateTime(2026, 10, 28, 14, 30, 0),
                CashierName = "Nguyễn Văn An",
                CustomerName = "Khách hàng mẫu",
                PaymentMethod = "CASH",
                Subtotal = 250000m,
                DiscountAmount = 10000m,
                PromotionCode = "WELCOME10",
                VatRate = 8m,
                VatAmount = 19200m,
                TotalAmount = 259200m
            };
            invoice.Lines.Add(new InvoiceLine
            {
                ProductName = "Sản phẩm mẫu",
                Quantity = 2,
                UnitPrice = 125000m,
                LineTotal = 250000m
            });
            return invoice;
        }

        private void PopulateInvoice()
        {
            _invoiceCodeValue.Text = _invoice.InvoiceCode;
            _dateValue.Text = _invoice.CreatedAt.ToString("dd/MM/yyyy HH:mm");
            _customerValue.Text = _invoice.CustomerName;
            _cashierValue.Text = _invoice.CashierName;
            _paymentValue.Text = GetPaymentName(_invoice.PaymentMethod);
            _subtotalValue.Text = FormatMoney(_invoice.Subtotal);
            _discountValue.Text = FormatMoney(_invoice.DiscountAmount + _invoice.PointsDiscountAmount);
            _vatLabel.Text = "VAT (" + _invoice.VatRate.ToString("0.##") + "%)";
            _vatValue.Text = FormatMoney(_invoice.VatAmount);
            _promotionValue.Text = string.IsNullOrWhiteSpace(_invoice.PromotionCode)
                ? "Không áp dụng"
                : _invoice.PromotionCode;
            _pointsValue.Text = _invoice.PointsUsed.ToString("N0");
            _statusValue.Text = _invoice.IsCancelled
                ? "ĐÃ HỦY" +
                  (_invoice.CancelledAt.HasValue
                      ? " • " + _invoice.CancelledAt.Value.ToString("dd/MM/yyyy HH:mm")
                      : string.Empty) +
                  (string.IsNullOrWhiteSpace(_invoice.CancelReason) ? string.Empty : " • " + _invoice.CancelReason)
                : "HOÀN THÀNH";
            _statusValue.ForeColor = _invoice.IsCancelled ? AppColors.Error : AppColors.Success;
            _totalValue.Text = FormatMoney(_invoice.TotalAmount);

            _linesGrid.Rows.Clear();
            _receiptContent.Controls.Clear();

            AddReceiptLabel("CONNECT MART", 15F, true, AppColors.TextTitle);
            AddReceiptLabel("HÓA ĐƠN BÁN HÀNG", 10F, true, AppColors.TextPrimary);
            AddReceiptLabel(_invoice.InvoiceCode, 9F, true, AppColors.Accent);
            AddReceiptLabel(_invoice.CreatedAt.ToString("dd/MM/yyyy HH:mm"), 8F, false, AppColors.TextMuted);
            AddReceiptLabel("Khách: " + _invoice.CustomerName, 8F, false, AppColors.TextPrimary);

            foreach (InvoiceLine line in _invoice.Lines)
            {
                _linesGrid.Rows.Add(
                    line.ProductName,
                    line.Quantity,
                    FormatMoney(line.UnitPrice),
                    FormatMoney(line.LineTotal));
                AddReceiptLabel(line.ProductName, 8F, true, AppColors.TextPrimary);
                AddReceiptLabel(
                    line.Quantity + " x " + FormatMoney(line.UnitPrice) + "     " + FormatMoney(line.LineTotal),
                    8F,
                    false,
                    AppColors.TextSecondary);
            }

            AddReceiptLabel("Tạm tính: " + FormatMoney(_invoice.Subtotal), 8F, false, AppColors.TextSecondary);
            AddReceiptLabel(
                "Giảm giá: " + FormatMoney(_invoice.DiscountAmount + _invoice.PointsDiscountAmount),
                8F,
                false,
                AppColors.TextSecondary);
            AddReceiptLabel("VAT: " + FormatMoney(_invoice.VatAmount), 8F, false, AppColors.TextSecondary);
            AddReceiptLabel("TỔNG: " + FormatMoney(_invoice.TotalAmount), 11F, true, AppColors.Accent);
            AddReceiptLabel("Thanh toán: " + GetPaymentName(_invoice.PaymentMethod), 8F, false, AppColors.TextSecondary);
            AddReceiptLabel(
                _invoice.IsCancelled ? "HÓA ĐƠN ĐÃ HỦY" : "Cảm ơn quý khách!",
                8F,
                true,
                _invoice.IsCancelled ? AppColors.Error : AppColors.Success);
        }

        private void AddReceiptLabel(string text, float size, bool bold, Color color)
        {
            _receiptContent.Controls.Add(new Label
            {
                Text = text,
                AutoSize = true,
                MaximumSize = new Size(320, 0),
                Font = new Font("Segoe UI", size, bold ? FontStyle.Bold : FontStyle.Regular),
                ForeColor = color,
                Margin = new Padding(2, 4, 2, 4)
            });
        }

        private void ApplyThemeColors()
        {
            BackColor = AppColors.PageBg;
            _root.BackColor = AppColors.PageBg;
            _detailsCard.BackColor = AppColors.White;
            _previewCard.BackColor = AppColors.White;
            _infoLayout.BackColor = AppColors.White;
            _invoiceHeading.ForeColor = AppColors.TextTitle;
            _previewHeading.ForeColor = AppColors.TextTitle;
            _receiptContent.BackColor = Color.White;
            _receiptPanel.BackColor = Color.White;

            foreach (DataGridView grid in new[] { _linesGrid })
            {
                grid.BackgroundColor = AppColors.White;
                grid.GridColor = AppColors.TableGrid;
                grid.ColumnHeadersDefaultCellStyle.BackColor = AppColors.TableHeaderBg;
                grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                grid.DefaultCellStyle.BackColor = AppColors.White;
                grid.DefaultCellStyle.ForeColor = AppColors.TableRowText;
                grid.DefaultCellStyle.SelectionBackColor = AppColors.AccentSelectionBg;
                grid.DefaultCellStyle.SelectionForeColor = AppColors.TextPrimary;
                grid.AlternatingRowsDefaultCellStyle.BackColor = AppColors.TableRowOdd;
            }

            _closeButton.BackColor = AppColors.CancelBg;
            _closeButton.ForeColor = AppColors.TextPrimary;
            _closeButton.FlatAppearance.BorderColor = AppColors.Border;
            _printButton.BackColor = AppColors.Accent;
            _printButton.ForeColor = Color.White;
            _printButton.FlatAppearance.BorderColor = AppColors.Accent;
        }

        private void ShowPrintPreview(object sender, EventArgs e)
        {
            OpenPrintPreview(this);
        }

        public void OpenPrintPreview(IWin32Window owner)
        {
            using (var preview = new PrintPreviewDialog
            {
                Document = _printDocument,
                Width = 900,
                Height = 700,
                StartPosition = FormStartPosition.CenterParent
            })
            {
                preview.ShowDialog(owner);
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            float left = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;
            using (var titleFont = new Font("Segoe UI", 15F, FontStyle.Bold))
            using (var bodyFont = new Font("Segoe UI", 9F))
            using (var boldFont = new Font("Segoe UI", 9F, FontStyle.Bold))
            using (var smallFont = new Font("Segoe UI", 8F))
            using (var linePen = new Pen(Color.LightGray))
            {
                float width = e.MarginBounds.Width;
                DrawPrintLine(e.Graphics, "CONNECT MART", titleFont, Brushes.Black, left, ref y, width);
                DrawPrintLine(e.Graphics, "HÓA ĐƠN BÁN HÀNG", boldFont, Brushes.Black, left, ref y, width);
                DrawPrintLine(e.Graphics, "Mã: " + _invoice.InvoiceCode, bodyFont, Brushes.Black, left, ref y, width);
                DrawPrintLine(e.Graphics, "Ngày: " + _invoice.CreatedAt.ToString("dd/MM/yyyy HH:mm"), bodyFont, Brushes.Black, left, ref y, width);
                DrawPrintLine(e.Graphics, "Khách hàng: " + _invoice.CustomerName, bodyFont, Brushes.Black, left, ref y, width);
                DrawPrintLine(e.Graphics, "Nhân viên: " + _invoice.CashierName, bodyFont, Brushes.Black, left, ref y, width);
                y += 6;
                e.Graphics.DrawLine(linePen, left, y, e.MarginBounds.Right, y);
                y += 8;
                float itemAreaBottom = e.MarginBounds.Bottom - 145;
                while (_nextLineToPrint < _invoice.Lines.Count)
                {
                    InvoiceLine item = _invoice.Lines[_nextLineToPrint];
                    string detail = item.Quantity + " x " + FormatMoney(item.UnitPrice) + "       " + FormatMoney(item.LineTotal);
                    float itemHeight = e.Graphics.MeasureString(item.ProductName, boldFont, (int)width).Height +
                                       e.Graphics.MeasureString(detail, smallFont, (int)width).Height + 8;
                    if (y + itemHeight > itemAreaBottom && _nextLineToPrint > 0)
                    {
                        e.HasMorePages = true;
                        return;
                    }

                    DrawPrintLine(e.Graphics, item.ProductName, boldFont, Brushes.Black, left, ref y, width);
                    DrawPrintLine(e.Graphics, detail, smallFont, Brushes.Black, left, ref y, width);
                    _nextLineToPrint++;
                }
                y += 6;
                e.Graphics.DrawLine(linePen, left, y, e.MarginBounds.Right, y);
                y += 8;
                DrawPrintLine(e.Graphics, "Tạm tính: " + FormatMoney(_invoice.Subtotal), bodyFont, Brushes.Black, left, ref y, width);
                DrawPrintLine(e.Graphics, "Giảm giá: " + FormatMoney(_invoice.DiscountAmount + _invoice.PointsDiscountAmount),
                    bodyFont, Brushes.Black, left, ref y, width);
                DrawPrintLine(e.Graphics, "VAT (" + _invoice.VatRate.ToString("0.##") + "%): " + FormatMoney(_invoice.VatAmount),
                    bodyFont, Brushes.Black, left, ref y, width);
                DrawPrintLine(e.Graphics, "TỔNG: " + FormatMoney(_invoice.TotalAmount), titleFont, Brushes.Black, left, ref y, width);
                DrawPrintLine(e.Graphics, "Thanh toán: " + GetPaymentName(_invoice.PaymentMethod), bodyFont, Brushes.Black, left, ref y, width);
                if (_invoice.IsCancelled)
                {
                    DrawPrintLine(e.Graphics, "HÓA ĐƠN ĐÃ HỦY", boldFont, Brushes.Red, left, ref y, width);
                    DrawPrintLine(e.Graphics, "Lý do: " + _invoice.CancelReason, bodyFont, Brushes.Black, left, ref y, width);
                }
                else
                {
                    DrawPrintLine(e.Graphics, "Cảm ơn quý khách!", bodyFont, Brushes.Black, left, ref y, width);
                }
            }

            e.HasMorePages = false;
        }

        private static void DrawPrintLine(Graphics graphics, string text, Font font, Brush brush, float x, ref float y, float width)
        {
            string value = text ?? string.Empty;
            float height = graphics.MeasureString(value, font, (int)width).Height;
            graphics.DrawString(value, font, brush, new RectangleF(x, y, width, height + 2));
            y += height + 3;
        }

        private static string GetPaymentName(string method)
        {
            switch (method)
            {
                case "CASH": return "Tiền mặt";
                case "BANK_TRANSFER": return "Chuyển khoản";
                case "PAYPAL": return "PayPal";
                case "CARD": return "Thẻ";
                default: return method ?? string.Empty;
            }
        }

        private static string FormatMoney(decimal amount)
        {
            return amount.ToString("N0", CultureInfo.GetCultureInfo("vi-VN")) + " đ";
        }
    }
}
