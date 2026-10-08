using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Models.DTOs.Pos;
using SIMS_WinFormsApp.MVP.Presenters.Pos;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls.Pos
{
    /// <summary>
    /// 1 thẻ sản phẩm trên lưới POS - ảnh + tên +
    /// giá + huy hiệu "Hết hàng" (khi hết hàng) + nút hành động (Thêm vào giỏ / Báo hết hàng).
    /// Chỉ phát sự kiện ra ngoài (không tự gọi service) - PosPresenter xử lý nghiệp vụ.
    /// </summary>
    internal sealed class ProductTileControl : UserControl
    {
        public const int TileWidth = 250;
        public const int TileHeight = 330;
        private const int ImageHeight = 150;
        private const int ContentPadding = 12;

        private Image _productImage;

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
            _productImage = TryLoadProductImage(product.ImagePath);
            if (_productImage != null)
            {
                imagePanel.Controls.Add(new PictureBox
                {
                    Dock = DockStyle.Fill,
                    Image = _productImage,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BackColor = Color.Transparent
                });
            }
            else
            {
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
            }
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
            bool wishlisted = false;
            wishlistIcon.Click += (s, e) =>
            {
                wishlisted = !wishlisted;
                wishlistIcon.IconColor = wishlisted ? AppColors.Error : AppColors.TextMuted;
            };
            Controls.Add(wishlistIcon);

            var nameLabel = new Label
            {
                Location = new Point(ContentPadding, ImageHeight + 10),
                Size = new Size(TileWidth - (ContentPadding * 2), 48),
                Text = product.Name,
                Font = new Font("Segoe UI", 13f, FontStyle.Bold, GraphicsUnit.Point),
                ForeColor = AppColors.TextTitle,
                AutoEllipsis = false,
                AutoSize = false,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(0),
                UseMnemonic = false
            };
            Controls.Add(nameLabel);

            var detailsLabel = new Label
            {
                Location = new Point(ContentPadding, nameLabel.Bottom + 1),
                Size = new Size(TileWidth - (ContentPadding * 2), 20),
                Text = (string.IsNullOrWhiteSpace(product.CategoryName) ? "Chưa phân loại" : product.CategoryName) +
                    "  ·  Tồn: " + product.StockQuantity,
                Font = new Font("Segoe UI", 9f, FontStyle.Regular, GraphicsUnit.Point),
                ForeColor = product.IsOutOfStock ? AppColors.Error : AppColors.TextMuted,
                AutoSize = false,
                AutoEllipsis = true,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleLeft,
                UseMnemonic = false
            };
            Controls.Add(detailsLabel);

            var priceLabel = new Label
            {
                Location = new Point(ContentPadding, detailsLabel.Bottom + 2),
                Size = new Size(TileWidth - (ContentPadding * 2), 30),
                Text = PosFormat.Vnd(product.Price),
                Font = new Font("Segoe UI", 18f, FontStyle.Bold, GraphicsUnit.Point),
                ForeColor = AppColors.TextTitle,
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Transparent,
                Padding = new Padding(0),
                AutoSize = false,
                AutoEllipsis = false
            };
            Controls.Add(priceLabel);

            var actionButton = new PrimaryButton
            {
                Location = new Point(ContentPadding, TileHeight - 48),
                Size = new Size(TileWidth - (ContentPadding * 2), 38),
                CornerRadius = AppRadius.Medium,
                Font = new Font("Segoe UI", 15f, FontStyle.Bold, GraphicsUnit.Point),
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(0),
                FlatStyle = FlatStyle.Flat
            };
            if (product.IsOutOfStock)
            {
                actionButton.Text = "Báo hết hàng";
                actionButton.IsPrimary = false;
                actionButton.CustomAccentColor = AppColors.Warning;
                actionButton.Click += (s, e) => NotifyRestockClicked?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                actionButton.Text = "Thêm vào giỏ";
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

        private static Image TryLoadProductImage(string path)
        {
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path)) return null;

            try
            {
                using (var source = Image.FromFile(path))
                    return new Bitmap(source);
            }
            catch (ArgumentException)
            {
                return null;
            }
            catch (ExternalException)
            {
                return null;
            }
            catch (OutOfMemoryException)
            {
                return null;
            }
            catch (IOException)
            {
                return null;
            }
            catch (UnauthorizedAccessException)
            {
                return null;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _productImage?.Dispose();
                _productImage = null;
            }
            base.Dispose(disposing);
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