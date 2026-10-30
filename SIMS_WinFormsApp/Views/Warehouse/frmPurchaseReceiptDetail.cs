using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;
using SIMS_WinFormsApp.Models.DTOs.Inventory;

namespace SIMS_WinFormsApp.Views.Warehouse
{
    public sealed partial class frmPurchaseReceiptDetail : Form
    {
        private static readonly CultureInfo VietnameseCulture = CultureInfo.GetCultureInfo("vi-VN");
        private static readonly Font GridEmphasisFont = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        private readonly PurchaseReceiptListItemDto _receipt;
        private readonly IReadOnlyList<PurchaseReceiptDetailLineDto> _lines;
        public frmPurchaseReceiptDetail() : this(
            new PurchaseReceiptListItemDto(),
            new List<PurchaseReceiptDetailLineDto>())
        {
        }

        public frmPurchaseReceiptDetail(
            PurchaseReceiptListItemDto receipt,
            IReadOnlyList<PurchaseReceiptDetailLineDto> lines)
        {
            InitializeComponent();
            _receipt = receipt ?? new PurchaseReceiptListItemDto();
            _lines = lines ?? new List<PurchaseReceiptDetailLineDto>();
            _grid.Columns["ImportPrice"].DefaultCellStyle.FormatProvider = VietnameseCulture;
            _grid.Columns["Amount"].DefaultCellStyle.FormatProvider = VietnameseCulture;
            UpdateReceiptData();
            _headerIconCircle.Paint += new PaintEventHandler(HeaderIconCircle_Paint);
            _closeButton.Click += new EventHandler(_closeButton_Click);
            Load += new EventHandler(DetailForm_Load);
        }

        private void UpdateReceiptData()
        {
            _productCountLabel.Text = "Danh sách sản phẩm (" + _lines.Count.ToString(CultureInfo.InvariantCulture) + ")";
            _receiptCodeLabel.Text = DisplayValue(_receipt.ReceiptCode);
            _receiptSubtitleLabel.Text = DisplayValue(_receipt.SupplierName) + " · " + GetStatusLabel(_receipt.Status);
            _statusLabel.Text = GetStatusLabel(_receipt.Status);
            bool cancelled = IsCancelled(_receipt.Status);
            _statusLabel.FillColor = cancelled ? Color.FromArgb(254, 226, 226) : Color.FromArgb(220, 252, 231);
            _statusLabel.TextColor = cancelled ? Color.FromArgb(185, 28, 28) : Color.FromArgb(22, 101, 52);
            _supplierValueLabel.Text = DisplayValue(_receipt.SupplierName);
            _createdByValueLabel.Text = DisplayValue(_receipt.CreatedByName);
            _createdAtValueLabel.Text = _receipt.CreatedAt.ToString("dd/MM/yyyy HH:mm", VietnameseCulture);
            _itemCountValueLabel.Text = _receipt.ItemCount.ToString("N0", VietnameseCulture);
            _receiptStatusValueLabel.Text = GetStatusLabel(_receipt.Status);
            _receiptTotalValueLabel.Text = _receipt.TotalAmount.ToString("N0", VietnameseCulture) + " đ";
            _totalValueLabel.Text = _receipt.TotalAmount.ToString("N0", VietnameseCulture) + " đ";
            PopulateLines();
            ConfigureExpiryAlert();
        }

        private void DetailForm_Load(object sender, EventArgs e)
        {
            Height = Math.Min(620, Screen.FromControl(this).WorkingArea.Height - 80);
            AutoScroll = false;
        }

        private void HeaderIconCircle_Paint(object sender, PaintEventArgs args)
        {
            args.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (var brush = new SolidBrush(Color.FromArgb(220, 252, 231)))
                args.Graphics.FillEllipse(brush, 1, 1, _headerIconCircle.Width - 2, _headerIconCircle.Height - 2);
        }

        private void ConfigureExpiryAlert()
        {
            bool hasExpiringLine = false;
            DateTime alertThreshold = DateTime.Today.AddDays(30);
            foreach (PurchaseReceiptDetailLineDto line in _lines)
            {
                if (line.ExpiryDate.HasValue && line.ExpiryDate.Value.Date <= alertThreshold)
                {
                    hasExpiringLine = true;
                    break;
                }
            }

            _expiryAlertPanel.Visible = hasExpiringLine;
            _contentLayout.RowStyles[3].Height = hasExpiringLine ? 40F : 0F;
        }

        private void PopulateLines()
        {
            _grid.Rows.Clear();
            DateTime today = DateTime.Today;
            foreach (PurchaseReceiptDetailLineDto line in _lines)
            {
                string manufactureDate = line.ManufactureDate.HasValue
                    ? line.ManufactureDate.Value.ToString("dd/MM/yyyy", VietnameseCulture)
                    : "-";
                string expiryDate = line.ExpiryDate.HasValue
                    ? line.ExpiryDate.Value.ToString("dd/MM/yyyy", VietnameseCulture)
                    : "-";
                int rowIndex = _grid.Rows.Add(
                    line.ProductCode,
                    line.ProductName,
                    line.BatchCode,
                    line.LotNumber,
                    manufactureDate,
                    expiryDate,
                    line.Quantity,
                    line.ImportPrice,
                    line.Amount);

                if (!line.ExpiryDate.HasValue)
                    continue;

                DateTime expiry = line.ExpiryDate.Value.Date;
                DataGridViewCell expiryCell = _grid.Rows[rowIndex].Cells["ExpiryDate"];
                if (expiry < today)
                {
                    expiryCell.Style.ForeColor = Color.FromArgb(185, 28, 28);
                    expiryCell.Style.Font = GridEmphasisFont;
                }
                else if ((expiry - today).TotalDays <= 30)
                {
                    expiryCell.Style.ForeColor = Color.FromArgb(194, 65, 12);
                    expiryCell.Style.Font = GridEmphasisFont;
                }
            }
            _grid.ClearSelection();
            _grid.CurrentCell = null;
        }

        private static string DisplayValue(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? "-" : value;
        }

        private static bool IsCancelled(string status)
        {
            return string.Equals(status, "CANCELLED", StringComparison.OrdinalIgnoreCase);
        }

        private static string GetStatusLabel(string status)
        {
            return IsCancelled(status) ? "Đã hủy" : "Hoàn tất";
        }

        private sealed class RoundedInformationPanel : Panel
        {
            public Color FillColor { get; set; }

            public RoundedInformationPanel()
            {
                SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                         ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw |
                         ControlStyles.SupportsTransparentBackColor, true);
            }

            protected override void OnPaintBackground(PaintEventArgs e)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle bounds = new Rectangle(0, 0, Width - 1, Height - 1);
                using (GraphicsPath path = CreateRoundedPath(bounds, 10))
                using (var brush = new SolidBrush(FillColor))
                    e.Graphics.FillPath(brush, path);
            }
        }

        private sealed class RoundedBadgeLabel : Label
        {
            public Color FillColor { get; set; }
            public Color TextColor { get; set; }

            protected override void OnPaint(PaintEventArgs e)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle bounds = new Rectangle(0, 0, Width - 1, Height - 1);
                using (GraphicsPath path = CreateRoundedPath(bounds, Height / 2))
                using (var brush = new SolidBrush(FillColor))
                    e.Graphics.FillPath(brush, path);
                TextRenderer.DrawText(e.Graphics, Text, Font, ClientRectangle, TextColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter |
                    TextFormatFlags.NoPadding | TextFormatFlags.SingleLine);
            }
        }

        private static GraphicsPath CreateRoundedPath(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            var path = new GraphicsPath();
            path.AddArc(bounds.Left, bounds.Top, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Top, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.Left, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}