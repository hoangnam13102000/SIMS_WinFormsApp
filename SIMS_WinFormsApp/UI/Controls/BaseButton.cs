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
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
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
            // Không để WinForms vẽ nền mặc định dưới custom rounded surface.
            // Điều này tránh hiện tượng nền đen / khối viền nhỏ xung quanh nút.
            pevent.Graphics.Clear(Color.Transparent);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            var g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            var rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using (var path = AppRadius.GetRoundedPath(rect, CornerRadius))
            {
                using (var brush = new SolidBrush(GetBackgroundColor()))
                    g.FillPath(brush, path);

                using (var pen = new Pen(GetBorderColor(), 1f))
                    g.DrawPath(pen, path);
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

            var iconBox = EnsureIconBox();
            var totalTextWidth = TextRenderer.MeasureText(Text, Font).Width;
            var iconSpacing = iconBox.Visible ? IconSize + 10 : 0;
            var startX = Math.Max(0, (Width - (iconSpacing + totalTextWidth)) / 2);
            var textRect = new Rectangle(startX + iconSpacing, 0, Width - startX - iconSpacing, Height);

            if (iconBox.Visible)
            {
                iconBox.IconColor = GetTextColor();
                iconBox.Location = new Point(startX, (Height - iconBox.Height) / 2);
            }

            TextRenderer.DrawText(g, Text, Font, textRect, GetTextColor(),
                TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis);
        }

        protected virtual void UpdateLayout()
        {
            var iconBox = EnsureIconBox();
            if (Width <= 0 || Height <= 0 || iconBox == null) return;

            var totalTextWidth = string.IsNullOrEmpty(Text) ? 0 : TextRenderer.MeasureText(Text, Font).Width;
            var iconSpacing = iconBox.Visible ? iconBox.Width + 10 : 0;
            var startX = Math.Max(0, (Width - (iconSpacing + totalTextWidth)) / 2);
            iconBox.Location = new Point(startX, (Height - iconBox.Height) / 2);
            Invalidate();
        }

        private static Color Scale(Color c, double factor)
        {
            int Clamp(int v) => Math.Max(0, Math.Min(255, (int)(v * factor)));
            return Color.FromArgb(Clamp(c.R), Clamp(c.G), Clamp(c.B));
        }

    }
}
