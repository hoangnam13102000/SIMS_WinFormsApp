using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{
    public sealed class Pill : Control
    {
        private const int HorizontalPadding = 28;
        private const int MinWidth = 60;
        private Color _backgroundColor = AppColors.AccentBgSoft;
        private Color _foregroundColor = AppColors.Accent;

        public Color BackgroundColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; Invalidate(); }
        }

        public Color ForegroundColor
        {
            get { return _foregroundColor; }
            set { _foregroundColor = value; Invalidate(); }
        }

        public Pill()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);
            Font = AppFonts.SmallBold;
            BackColor = Color.Transparent;
            Height = 34;
        }

        public void SetColors(Color background, Color foreground)
        {
            BackgroundColor = background;
            ForegroundColor = foreground;
        }

        public void SetText(string text)
        {
            Text = text ?? string.Empty;
            int textWidth = TextRenderer.MeasureText(
                Text, Font, new Size(int.MaxValue, int.MaxValue), TextFormatFlags.NoPadding).Width;
            Width = System.Math.Max(MinWidth, textWidth + HorizontalPadding);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using (GraphicsPath path = AppRadius.GetRoundedPath(rect, Height / 2))
            using (SolidBrush brush = new SolidBrush(BackgroundColor))
                g.FillPath(brush, path);

            TextRenderer.DrawText(g, Text, Font, ClientRectangle, ForegroundColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
    }
}
