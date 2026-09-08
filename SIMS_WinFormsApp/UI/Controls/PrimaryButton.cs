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

        public DialogResult DialogResult { get; set; }

        public PrimaryButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                      ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw |
                      ControlStyles.UserMouse, true);
            BackColor = AppColors.White;
            ForeColor = System.Drawing.Color.White;
            Font = AppFonts.Button;
            Height = 46;
            Cursor = Cursors.Hand;
            TabStop = true;

            MouseEnter += (s, e) => { _isHover = true; Invalidate(); };
            MouseLeave += (s, e) => { _isHover = false; _isPressed = false; Invalidate(); };
            MouseDown += (s, e) => { _isPressed = true; Invalidate(); };
            MouseUp += (s, e) => { _isPressed = false; Invalidate(); };
        }

        public void NotifyDefault(bool value)
        {
        }

        public void PerformClick()
        {
            if (!Enabled) return;
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

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _isPressed = false;
            if (e.Button == MouseButtons.Left)
                PerformClick();
            Invalidate();
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            pevent.Graphics.Clear(BackColor);
        }
        protected override bool ShowFocusCues => false;

        protected override void OnPaint(PaintEventArgs pevent)
        {
            var g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Vẽ full size, không chừa 1px
            var rect = new Rectangle(0, 0, Width, Height);

            Color fill;
            Color border;
            Color textColor;

            if (!Enabled)
            {
                fill = AppColors.DisabledBtn;
                border = AppColors.DisabledBtn;
                textColor = Color.White;
            }
            else if (IsPrimary)
            {
                fill = _isPressed ? Scale(AppColors.Accent, 0.85)
                                  : (_isHover ? AppColors.AccentHover : AppColors.Accent);
                border = fill;
                textColor = Color.White;
            }
            else
            {
                fill = _isHover ? AppColors.CancelHover : AppColors.CancelBg;
                border = AppColors.Border;
                textColor = AppColors.TextPrimary;
            }

            var drawRect = new Rectangle(0, 0, Width, Height);

            using (var path = AppRadius.GetRoundedPath(drawRect, CornerRadius))
            using (var brush = new SolidBrush(fill))
            using (var pen = new Pen(border))
            {
                g.FillPath(brush, path);
                if (!IsPrimary)
                    g.DrawPath(pen, path);
            }

            TextRenderer.DrawText(g, Text, Font, ClientRectangle, textColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
        }

        private static Color Scale(Color c, double factor)
        {
            int Clamp(int v) => System.Math.Max(0, System.Math.Min(255, v));
            return Color.FromArgb(Clamp((int)(c.R * factor)), Clamp((int)(c.G * factor)), Clamp((int)(c.B * factor)));
        }
    }
}