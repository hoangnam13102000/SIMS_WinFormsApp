using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{
    public sealed class NotificationBadgeControl : Control
    {
        private const int BadgeHeight = 20;
        private readonly Font _badgeFont = new Font("Segoe UI", 8f, FontStyle.Bold);

        public NotificationBadgeControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.Selectable, false);
            BackColor = Color.Transparent;
            Size = new Size(BadgeHeight, BadgeHeight);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Width = (Text ?? string.Empty).Length > 1 ? BadgeHeight + 8 : BadgeHeight;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using (var brush = new SolidBrush(LayoutColors.RedDot))
            using (GraphicsPath path = AppRadius.GetRoundedPath(rect, rect.Height / 2))
            {
                g.FillPath(brush, path);
            }

            TextRenderer.DrawText(
                g,
                Text,
                _badgeFont,
                rect,
                Color.White,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter |
                TextFormatFlags.SingleLine | TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _badgeFont.Dispose();
            base.Dispose(disposing);
        }
    }
}
