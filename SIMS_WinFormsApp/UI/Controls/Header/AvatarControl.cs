using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{
    /// <summary>
    /// Avatar hình tròn tự vẽ (chữ cái đầu hoặc ảnh, có viền tuỳ chọn).
    /// Không dùng Label vì Label luôn tô nền chữ nhật trước sự kiện Paint nên không bo tròn được.
    /// Ảnh được cắt kiểu "cover" và vẽ bằng TextureBrush nên mép tròn được khử răng cưa.
    /// </summary>
    public class AvatarControl : Control
    {
        private string _initial = "?";
        private Color _fillColor = LayoutColors.Accent;
        private Color _ringColor = Color.Transparent;
        private int _ringWidth;

        private Image _image;
        private Bitmap _texture;
        private Font _initialFont;

        public AvatarControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.Selectable, false);

            BackColor = Color.Transparent;
            ForeColor = Color.White;
            Size = new Size(52, 52);
            RebuildInitialFont();
        }

        public string Initial
        {
            get => _initial;
            set
            {
                string v = string.IsNullOrEmpty(value) ? "?" : value;
                if (_initial == v) return;
                _initial = v;
                Invalidate();
            }
        }

        public Color FillColor
        {
            get => _fillColor;
            set { _fillColor = value; Invalidate(); }
        }

        public Color RingColor
        {
            get => _ringColor;
            set { _ringColor = value; Invalidate(); }
        }

        public int RingWidth
        {
            get => _ringWidth;
            set
            {
                int v = Math.Max(0, value);
                if (_ringWidth == v) return;
                _ringWidth = v;
                DisposeTexture();
                Invalidate();
            }
        }

        public bool HasImage => _image != null;

        /// <summary>Gán ảnh (control sở hữu và tự Dispose ảnh này). Truyền null để quay về chữ cái.</summary>
        public void SetImage(Image image)
        {
            DisposeTexture();
            _image?.Dispose();
            _image = image;
            Invalidate();
        }

        /// <summary>Nạp ảnh từ đường dẫn (không khoá file). Thất bại -> hiển thị chữ cái.</summary>
        public bool TryLoadImage(string path)
        {
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                SetImage(null);
                return false;
            }

            try
            {
                Bitmap copy;
                using (var source = Image.FromFile(path))
                    copy = new Bitmap(source);
                SetImage(copy);
                return true;
            }
            catch (Exception)
            {
                SetImage(null);
                return false;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            RebuildInitialFont();
            DisposeTexture();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            int diameter = Math.Min(Width, Height) - _ringWidth * 2;
            if (diameter <= 0) return;

            var inner = new RectangleF(
                (Width - diameter) / 2f,
                (Height - diameter) / 2f,
                diameter,
                diameter);

            if (_ringWidth > 0 && _ringColor.A > 0)
            {
                float half = _ringWidth / 2f;
                using (var pen = new Pen(_ringColor, _ringWidth))
                    g.DrawEllipse(pen,
                        inner.X - half, inner.Y - half,
                        inner.Width + _ringWidth, inner.Height + _ringWidth);
            }

            if (_image != null)
            {
                EnsureTexture(diameter);
                using (var brush = new TextureBrush(_texture, WrapMode.Clamp))
                {
                    brush.TranslateTransform(inner.X, inner.Y);
                    g.FillEllipse(brush, inner);
                }
                return;
            }

            using (var fill = new SolidBrush(_fillColor))
                g.FillEllipse(fill, inner);

            TextRenderer.DrawText(g, _initial, _initialFont, Rectangle.Round(inner), ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter |
                TextFormatFlags.SingleLine | TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix);
        }

        private void EnsureTexture(int diameter)
        {
            if (_texture != null && _texture.Width == diameter) return;

            DisposeTexture();

            var bmp = new Bitmap(diameter, diameter, PixelFormat.Format32bppArgb);
            using (var g = Graphics.FromImage(bmp))
            using (var attrs = new ImageAttributes())
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;
                attrs.SetWrapMode(WrapMode.TileFlipXY);

                // Cắt vuông ở giữa ("cover") rồi co về đúng đường kính.
                float scale = Math.Max((float)diameter / _image.Width, (float)diameter / _image.Height);
                float srcW = diameter / scale;
                float srcH = diameter / scale;
                float srcX = (_image.Width - srcW) / 2f;
                float srcY = (_image.Height - srcH) / 2f;

                g.DrawImage(_image, new Rectangle(0, 0, diameter, diameter),
                    srcX, srcY, srcW, srcH, GraphicsUnit.Pixel, attrs);
            }
            _texture = bmp;
        }

        private void RebuildInitialFont()
        {
            _initialFont?.Dispose();
            float px = Math.Max(8f, Math.Min(Width, Height) * 0.4f);
            _initialFont = new Font("Segoe UI Semibold", px, FontStyle.Bold, GraphicsUnit.Pixel);
        }

        private void DisposeTexture()
        {
            _texture?.Dispose();
            _texture = null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _image?.Dispose();
                _image = null;
                DisposeTexture();
                _initialFont?.Dispose();
                _initialFont = null;
            }
            base.Dispose(disposing);
        }
    }
}