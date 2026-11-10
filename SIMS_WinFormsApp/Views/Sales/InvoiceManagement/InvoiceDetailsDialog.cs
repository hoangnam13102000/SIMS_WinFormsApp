using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using SIMS_WinFormsApp.Models.DTOs.Sales;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Reports;

namespace SIMS_WinFormsApp.Views.Sales.InvoiceManagement
{
    public sealed partial class InvoiceDetailsDialog : Form
    {
        private readonly InvoiceDetails _invoice;
        private Func<InvoiceDetails, bool> _returnRequestHandler;

        public InvoiceDetailsDialog()
            : this(CreateDesignerInvoice())
        {
        }

        internal InvoiceDetailsDialog(InvoiceDetails invoice)
        {
            _invoice = invoice ?? throw new ArgumentNullException(nameof(invoice));
            InitializeComponent();
            _closeButton.Click += CloseButton_Click;
            _printButton.Click += ShowPrintPreview;
            _returnButton.Click += ReturnButton_Click;
            _returnButton.Visible = false;
            PopulateInvoice();
            ApplyThemeColors();
        }

        internal Func<InvoiceDetails, bool> ReturnRequestHandler
        {
            get => _returnRequestHandler;
            set
            {
                _returnRequestHandler = value;
                _returnButton.Visible = value != null && !_invoice.IsCancelled;
            }
        }

        private void ReturnButton_Click(object sender, EventArgs e)
        {
            if (_returnRequestHandler != null && _returnRequestHandler(_invoice))
            {
                DialogResult = DialogResult.OK;
                Close();
            }
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
            foreach (Control control in _receiptContent.Controls)
            {
                if (!ReferenceEquals(control, _receiptLogo))
                    control.Dispose();
            }
            _receiptContent.Controls.Clear();

            _receiptContent.Controls.Add(_receiptLogo);
            AddReceiptLabel("SIMS", 15F, true, AppColors.TextTitle, true);
            AddReceiptLabel("HÓA ĐƠN BÁN HÀNG", 9F, true, AppColors.TextSecondary, true);
            AddReceiptLabel("Mã hóa đơn: " + _invoice.InvoiceCode, 9F, true, AppColors.Accent, true);
            AddReceiptLabel(_invoice.CreatedAt.ToString("dd/MM/yyyy HH:mm"), 8F, false, AppColors.TextMuted, true);
            AddReceiptSeparator();
            AddReceiptLabel("Khách hàng: " + _invoice.CustomerName, 8F, false, AppColors.TextPrimary);
            AddReceiptLabel("Nhân viên: " + _invoice.CashierName, 8F, false, AppColors.TextPrimary);
            AddReceiptSeparator();

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

            AddReceiptSeparator();
            AddReceiptLabel("Tạm tính: " + FormatMoney(_invoice.Subtotal), 8F, false, AppColors.TextSecondary);
            AddReceiptLabel(
                "Giảm giá: " + FormatMoney(_invoice.DiscountAmount + _invoice.PointsDiscountAmount),
                8F,
                false,
                AppColors.TextSecondary);
            AddReceiptLabel(
                "VAT (" + _invoice.VatRate.ToString("0.##") + "%): " + FormatMoney(_invoice.VatAmount),
                8F,
                false,
                AppColors.TextSecondary);
            AddReceiptLabel("TỔNG THANH TOÁN", 9F, true, AppColors.TextPrimary, true);
            AddReceiptLabel(FormatMoney(_invoice.TotalAmount), 15F, true, AppColors.Accent, true);
            AddReceiptLabel("Thanh toán: " + GetPaymentName(_invoice.PaymentMethod), 8F, false, AppColors.TextSecondary);
            AddReceiptLabel(
                _invoice.IsCancelled ? "HÓA ĐƠN ĐÃ HỦY" : "Cảm ơn quý khách!",
                8F,
                true,
                _invoice.IsCancelled ? AppColors.Error : AppColors.Success,
                true);
        }

        // Receipt rows are generated and sized from the current invoice data at runtime.
        private void AddReceiptLabel(string text, float size, bool bold, Color color, bool centered = false)
        {
            using (var font = new Font("Segoe UI", size, bold ? FontStyle.Bold : FontStyle.Regular))
            {
                Size textSize = TextRenderer.MeasureText(
                    text ?? string.Empty,
                    font,
                    new Size(300, 1000),
                    TextFormatFlags.WordBreak | TextFormatFlags.NoPadding);
                _receiptContent.Controls.Add(new Label
                {
                    Text = text,
                    Anchor = AnchorStyles.Top,
                    AutoSize = false,
                    Size = new Size(300, Math.Max(22, textSize.Height + 8)),
                    Font = new Font(font, font.Style),
                    ForeColor = color,
                    TextAlign = centered ? ContentAlignment.MiddleCenter : ContentAlignment.MiddleLeft,
                    Margin = new Padding(2, 2, 2, 2)
                });
            }
        }

        // Receipt separators are generated alongside the data-dependent receipt rows.
        private void AddReceiptSeparator()
        {
            _receiptContent.Controls.Add(new Label
            {
                Anchor = AnchorStyles.Top,
                BackColor = AppColors.Border,
                Height = 1,
                Margin = new Padding(2, 6, 2, 6),
                Width = 300
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
            _returnButton.BackColor = Color.FromArgb(239, 246, 255);
            _returnButton.ForeColor = AppColors.Accent;
            _returnButton.FlatAppearance.BorderColor = AppColors.Accent;
        }

        private void ShowPrintPreview(object sender, EventArgs e)
        {
            OpenPrintPreview(this);
        }

        private void _receiptLogo_Click(object sender, EventArgs e)
        {
            ShowPrintPreview(sender, e);
        }

        public void OpenPrintPreview(IWin32Window owner)
        {
            try
            {
                var sources = new[]
                {
                    new ReportDataSource("InvoiceHeaderDataSet", new[] { _invoice }),
                    new ReportDataSource("InvoiceLinesDataSet", _invoice.Lines)
                };
                using (var viewer = new frmReportViewer(
                    "Hóa đơn bán hàng",
                    "SIMS_WinFormsApp.Reports.InvoiceReport.rdlc",
                    sources,
                    null))
                {
                    viewer.ShowDialog(owner);
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

        private void ShowRdlcError(string details)
        {
            MessageBox.Show(this,
                "Không thể mở báo cáo RDLC. Hãy kiểm tra thư viện ReportViewer và mẫu báo cáo đã được cài đặt/nhúng.\r\n\r\n"
                + details,
                "Lỗi báo cáo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
