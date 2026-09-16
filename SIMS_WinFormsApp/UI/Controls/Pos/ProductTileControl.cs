using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Models.DTOs.Pos;
using SIMS_WinFormsApp.MVP.Presenters.Pos;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls.Pos
{
    /// <summary>
    /// 1 thẻ sản phẩm trên lưới POS - khối màu minh họa (chưa có ảnh sản phẩm thật) + tên +
    /// giá + huy hiệu "Hết hàng" (khi hết hàng) + nút hành động (Thêm vào giỏ / Báo hết hàng).
    /// Chỉ phát sự kiện ra ngoài (không tự gọi service) - PosPresenter xử lý nghiệp vụ.
    /// </summary>
    internal sealed class ProductTileControl : UserControl
    {
        public const int TileWidth = 250;
        public const int TileHeight = 280;
        private const int ImageHeight = 150;

        public int ProductId { get; }

        public event EventHandler AddToCartClicked;
        public event EventHandler NotifyRestockClicked;

        public ProductTileControl(ProductCatalogItemDto product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            ProductId = product.ProductId;

            Size = new Size(TileWidth, TileHeight);
            Margin = new Padding(0, 0, 14, 14);
            BackColor = AppColors.White;
            DoubleBuffered = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);

            var imagePanel = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(TileWidth, ImageHeight),
                BackColor = product.TileColor
            };
            var boxIcon = new IconPictureBox
            {
                IconChar = IconChar.Box,
                IconColor = Darken(product.TileColor, 0.55),
                IconSize = 46,
                Size = new Size(46, 46),
                Location = new Point((TileWidth - 46) / 2, (ImageHeight - 46) / 2),
                BackColor = Color.Transparent
            };
            imagePanel.Controls.Add(boxIcon);
            Controls.Add(imagePanel);

            if (product.IsOutOfStock)
            {
                Controls.Add(CreateBadge("Hết hàng", AppColors.Error));
            }

            var wishlistIcon = new IconPictureBox
            {
                IconChar = IconChar.Heart,
                IconColor = AppColors.TextMuted,
                IconSize = 15,
                Size = new Size(28, 28),
                Location = new Point(TileWidth - 36, 8),
                Cursor = Cursors.Hand,
                BackColor = Color.Transparent
            };
            // Chỉ là icon trang trí (yêu thích) - project chưa có bảng "sản phẩm yêu thích" nên
            // không phát sự kiện ra ngoài, chỉ đổi màu tại chỗ cho có phản hồi khi bấm.
            bool wishlisted = false;
            wishlistIcon.Click += (s, e) =>
            {
                wishlisted = !wishlisted;
                wishlistIcon.IconColor = wishlisted ? AppColors.Error : AppColors.TextMuted;
            };
            Controls.Add(wishlistIcon);

            var nameLabel = new Label
            {
                Location = new Point(12, ImageHeight + 10),
                Size = new Size(TileWidth - 24, 40),
                Text = product.Name,
                Font = AppFonts.BodyBold,
                ForeColor = AppColors.TextTitle,
                AutoEllipsis = true,
                BackColor = Color.Transparent
            };
            Controls.Add(nameLabel);

            var priceLabel = new Label
            {
                Location = new Point(12, nameLabel.Bottom),
                Size = new Size(TileWidth - 24, 34),
                Text = PosFormat.Vnd(product.Price),
                Font = AppFonts.Subtitle,
                ForeColor = AppColors.TextTitle,
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Transparent
            };
            Controls.Add(priceLabel);

            var actionButton = new PrimaryButton
            {
                Location = new Point(12, TileHeight - 46),
                Size = new Size(TileWidth - 24, 34),
                CornerRadius = AppRadius.Medium,
                Font = AppFonts.Button
            };
            if (product.IsOutOfStock)
            {
                actionButton.Text = "🔔  Báo hết hàng";
                actionButton.IsPrimary = false;
                actionButton.CustomAccentColor = AppColors.Warning;
                actionButton.Click += (s, e) => NotifyRestockClicked?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                actionButton.Text = "🛒  Thêm vào giỏ";
                actionButton.IsPrimary = true;
                actionButton.Click += (s, e) => AddToCartClicked?.Invoke(this, EventArgs.Empty);
            }
            Controls.Add(actionButton);
        }

        private static Panel CreateBadge(string text, Color accent)
        {
            var badge = new Panel
            {
                Location = new Point(8, 8),
                Size = new Size(text.Length * 7 + 24, 24),
                BackColor = Color.Transparent
            };
            badge.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = new Rectangle(0, 0, badge.Width - 1, badge.Height - 1);
                using (var path = AppRadius.GetRoundedPath(rect, AppRadius.Large))
                using (var brush = new SolidBrush(accent))
                    e.Graphics.FillPath(brush, path);

                TextRenderer.DrawText(e.Graphics, text, AppFonts.Small, badge.ClientRectangle, Color.White,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            };
            return badge;
        }

        private static Color Darken(Color c, double factor)
        {
            int R(int v) => Math.Max(0, (int)(v * factor));
            return Color.FromArgb(R(c.R), R(c.G), R(c.B));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using (var path = AppRadius.GetRoundedPath(rect, AppRadius.Medium))
            using (var pen = new Pen(AppColors.Border, 1f))
                e.Graphics.DrawPath(pen, path);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (Width <= 0 || Height <= 0) return;
            using (var path = AppRadius.GetRoundedPath(new Rectangle(0, 0, Width, Height), AppRadius.Medium))
                Region = new Region(path);
        }
    }
}