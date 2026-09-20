using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{
    public abstract class BaseButton : Button, IButtonControl
    {
        private IconPictureBox _iconBox;
        protected bool _isHover;
        protected bool _isPressed;
        private int _cornerRadius = AppRadius.Medium;

        public int CornerRadius
        {
            get => _cornerRadius;
            set => _cornerRadius = Math.Max(0, value);
        }

        public new DialogResult DialogResult { get; set; }

        protected BaseButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            FlatStyle = FlatStyle.Flat;
            UseVisualStyleBackColor = false;
            BackColor = Color.Transparent;
            ForeColor = Color.White;
            Font = AppFonts.Button;
            Height = 46;
            MinimumSize = new Size(120, 42);
            Cursor = Cursors.Hand;
            TabStop = true;
            FlatAppearance.BorderSize = 0;
            FlatAppearance.MouseDownBackColor = Color.Transparent;
            FlatAppearance.MouseOverBackColor = Color.Transparent;

            EnsureIconBox();
        }

        private IconPictureBox EnsureIconBox()
        {
            if (_iconBox == null)
            {
                _iconBox = new IconPictureBox
                {
                    BackColor = Color.Transparent,
                    IconFont = IconFont.Solid,
                    IconColor = Color.White,
                    Size = new Size(16, 16),
                    Visible = false,
                    Cursor = Cursors.Hand
                };
                Controls.Add(_iconBox);
            }

            return _iconBox;
        }

        public IconChar? Icon
        {
            get
            {
                var iconBox = EnsureIconBox();
                if (iconBox.IconChar == IconChar.None)
                    return null;

                return iconBox.IconChar;
            }
            set
            {
                var iconBox = EnsureIconBox();
                if (value.HasValue)
                {
                    iconBox.IconChar = value.Value;
                    iconBox.Visible = true;
                }
                else
                {
                    iconBox.IconChar = IconChar.None;
                    iconBox.Visible = false;
                }

                UpdateLayout();
                Invalidate();
            }
        }

        public int IconSize
        {
            get
            {
                var iconBox = EnsureIconBox();
                return iconBox.IconSize;
            }
            set
            {
                var iconBox = EnsureIconBox();
                iconBox.IconSize = Math.Max(10, value);
                iconBox.Size = new Size(iconBox.IconSize, iconBox.IconSize);
                UpdateLayout();
                Invalidate();
            }
        }

        public virtual new void NotifyDefault(bool value) { }

        public new void PerformClick()
        {
            if (!Enabled || !Visible) return;
            OnClick(EventArgs.Empty);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateLayout();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _isHover = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _isHover = false;
            _isPressed = false;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left && Enabled)
            {
                _isPressed = true;
                Capture = true;
                UpdateLayout();
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Left)
            {
                _isPressed = false;
                Capture = false;
                UpdateLayout();
                Invalidate();
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                PerformClick();
                e.Handled = true;
            }
        }

        protected override bool ShowFocusCues => false;

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // Cố ý để trống. ButtonBase bật ControlStyles.Opaque nên WinForms KHÔNG gọi hàm này;
            // nền được vẽ trực tiếp trong OnPaint thông qua PaintBackdrop().
        }

        /// <summary>
        /// Vẽ nền phía sau nút (khớp với control cha) TRƯỚC khi vẽ hình bo tròn.
        /// Nếu bỏ qua bước này, bitmap double-buffer chưa được tô nên các pixel nằm ngoài
        /// đường bo tròn (4 góc + viền đáy/phải do rect = Width-1/Height-1) sẽ hiện màu đen.
        /// </summary>
        private void PaintBackdrop(PaintEventArgs pevent)
        {
            // Bước 1: tô sẵn 1 màu ĐẶC lấy từ control cha (đảm bảo không còn pixel chưa tô).
            pevent.Graphics.Clear(ResolveOpaqueParentColor());

            // Bước 2: để WinForms vẽ lại nền thật của control cha (hỗ trợ BackColor trong suốt
            // nhờ ControlStyles.SupportsTransparentBackColor) để 4 góc khớp đúng nền phía sau nút.
            base.OnPaintBackground(pevent);
        }

        private Color ResolveOpaqueParentColor()
        {
            for (Control c = Parent; c != null; c = c.Parent)
            {
                if (c.BackColor.A == 255)
                    return c.BackColor;
            }

            return SystemColors.Control;
        }

        /// <summary>
        /// Số pixel chừa ở đáy control để lớp con vẽ bóng đổ. Mặc định 0 —
        /// các nút không dùng bóng giữ nguyên hình dạng như trước.
        /// </summary>
        protected virtual int ShadowDepth => 0;

        /// <summary>
        /// Vùng vẽ THẬT của mặt nút (đã trừ chỗ cho bóng và đã dịch xuống khi đang nhấn).
        /// Mọi thứ thuộc về nút (nền, viền, chữ, icon) đều canh theo vùng này để khi
        /// bấm thì cả khối "lún xuống" thay vì chỉ đổi màu.
        /// </summary>
        protected Rectangle GetSurfaceBounds()
        {
            int shadow = Math.Max(0, ShadowDepth);
            int top = (_isPressed && Enabled && shadow > 0) ? Math.Min(shadow, 2) : 0;
            int height = Math.Max(1, Height - 1 - shadow);
            return new Rectangle(0, top, Math.Max(1, Width - 1), height);
        }

        /// <summary>
        /// Điểm mở rộng (Template Method): vẽ nền + viền của mặt nút.
        /// Mặc định là tô đặc + viền 1px; lớp con có thể thay bằng gradient, bóng đổ...
        /// </summary>
        protected virtual void PaintSurface(Graphics g, Rectangle surface, GraphicsPath path)
        {
            using (var brush = new SolidBrush(GetBackgroundColor()))
                g.FillPath(brush, path);

            using (var pen = new Pen(GetBorderColor(), 1f))
                g.DrawPath(pen, path);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            PaintBackdrop(pevent);

            var g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            var surface = GetSurfaceBounds();
            using (var path = AppRadius.GetRoundedPath(surface, CornerRadius))
            {
                PaintSurface(g, surface, path);
            }

            DrawText(g);
        }

        protected virtual Color GetBackgroundColor()
        {
            if (!Enabled)
                return AppColors.DisabledBtn;

            if (_isPressed)
                return Scale(AppColors.Accent, 0.86);

            if (_isHover)
                return Scale(AppColors.Accent, 0.78);

            return AppColors.Accent;
        }

        protected virtual Color GetBorderColor()
        {
            return Enabled ? AppColors.Accent : AppColors.Border;
        }

        protected virtual Color GetTextColor()
        {
            return Color.White;
        }

        protected virtual void DrawText(Graphics g)
        {
            if (string.IsNullOrEmpty(Text)) return;

            var surface = GetSurfaceBounds();
            var iconBox = EnsureIconBox();
            var totalTextWidth = TextRenderer.MeasureText(Text, Font).Width;
            var iconSpacing = iconBox.Visible ? IconSize + 10 : 0;
            var startX = Math.Max(0, (Width - (iconSpacing + totalTextWidth)) / 2);
            var textRect = new Rectangle(startX + iconSpacing, surface.Top,
                Math.Max(0, Width - startX - iconSpacing), surface.Height);

            if (iconBox.Visible)
            {
                iconBox.IconColor = GetTextColor();
                iconBox.Location = new Point(startX, surface.Top + (surface.Height - iconBox.Height) / 2);
            }

            TextRenderer.DrawText(g, Text, Font, textRect, GetTextColor(),
                TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis);
        }

        protected virtual void UpdateLayout()
        {
            var iconBox = EnsureIconBox();
            if (Width <= 0 || Height <= 0 || iconBox == null) return;

            var surface = GetSurfaceBounds();
            var totalTextWidth = string.IsNullOrEmpty(Text) ? 0 : TextRenderer.MeasureText(Text, Font).Width;
            var iconSpacing = iconBox.Visible ? iconBox.Width + 10 : 0;
            var startX = Math.Max(0, (Width - (iconSpacing + totalTextWidth)) / 2);
            iconBox.Location = new Point(startX, surface.Top + (surface.Height - iconBox.Height) / 2);
            Invalidate();
        }

        private static Color Scale(Color c, double factor)
        {
            int Clamp(int v) => Math.Max(0, Math.Min(255, (int)(v * factor)));
            return Color.FromArgb(Clamp(c.R), Clamp(c.G), Clamp(c.B));
        }

    }
}