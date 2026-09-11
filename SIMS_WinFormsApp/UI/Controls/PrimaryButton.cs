using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{
    public class PrimaryButton : Control, IButtonControl
    {
        private bool _isHover;
        private bool _isPressed;

        public int CornerRadius { get; set; } = AppRadius.Medium;
        public bool IsPrimary { get; set; } = true;
        public Color? CustomAccentColor { get; set; }
        public DialogResult DialogResult { get; set; }

        public PrimaryButton()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserMouse |
                ControlStyles.SupportsTransparentBackColor |
                ControlStyles.Selectable, true);

            BackColor = Color.Transparent;
            ForeColor = Color.White;
            Font = AppFonts.Button;
            Height = 46;
            Cursor = Cursors.Hand;
            TabStop = true;
        }

        public void NotifyDefault(bool value) { }

        public void PerformClick()
        {
            if (!Enabled || !Visible) return;
            OnClick(EventArgs.Empty);
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

        // ═══════════════════════════════════════════════════════════════
        // ✅ ĐÃ SỬA: Bỏ Focus() - không cần focus trước khi click
        // ═══════════════════════════════════════════════════════════════
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
                bool wasPressed = _isPressed;
                _isPressed = false;
                Capture = false;

                if (wasPressed && ClientRectangle.Contains(e.Location) && Enabled)
                    PerformClick();

                Invalidate();
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ApplyRoundedRegion();
        }

        private void ApplyRoundedRegion()
        {
            if (Width <= 0 || Height <= 0) return;
            using (var path = AppRadius.GetRoundedPath(new Rectangle(0, 0, Width, Height), CornerRadius))
                Region = new Region(path);
        }

        protected override bool ShowFocusCues => false;

        protected override void OnPaintBackground(PaintEventArgs pevent) { }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            var g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            Color accent = CustomAccentColor ?? AppColors.Accent;
            Color fill, textColor;

            if (!Enabled)
            {
                fill = AppColors.DisabledBtn;
                textColor = Color.White;
            }
            else if (IsPrimary)
            {
                fill = _isPressed ? Scale(accent, 0.85)
                                  : (_isHover ? Scale(accent, 0.72) : accent);
                textColor = Color.White;
            }
            else
            {
                fill = _isHover ? AppColors.CancelHover : AppColors.CancelBg;
                textColor = AppColors.TextPrimary;
            }

            var rect = new Rectangle(0, 0, Width, Height);
            using (var path = AppRadius.GetRoundedPath(rect, CornerRadius))
            using (var brush = new SolidBrush(fill))
            {
                g.FillPath(brush, path);
                if (!IsPrimary)
                {
                    using (var pen = new Pen(AppColors.Border, 1f))
                        g.DrawPath(pen, path);
                }
            }

            TextRenderer.DrawText(g, Text, Font, ClientRectangle, textColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
        }

        private static Color Scale(Color c, double factor)
        {
            int Clamp(int v) => Math.Max(0, Math.Min(255, (int)(v * factor)));
            return Color.FromArgb(Clamp(c.R), Clamp(c.G), Clamp(c.B));
        }
    }
}