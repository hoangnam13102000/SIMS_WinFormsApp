using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{
    public sealed class PasswordEyeToggle : Control
    {
        private bool _isOpen;

        public bool IsOpen
        {
            get { return _isOpen; }
            set
            {
                if (_isOpen == value) return;
                _isOpen = value;
                Invalidate();
            }
        }

        public PasswordEyeToggle()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Color color = AppColors.TextMutedAlt;
            RectangleF rect = new RectangleF(4, 8, Width - 8, Height - 16);
            using (var pen = new Pen(color, 1.6f))
            {
                g.DrawEllipse(pen, rect);
                float pupilSize = rect.Height * 0.55f;
                var pupilRect = new RectangleF(
                    rect.X + rect.Width / 2 - pupilSize / 2,
                    rect.Y + rect.Height / 2 - pupilSize / 2,
                    pupilSize,
                    pupilSize);
                g.DrawEllipse(pen, pupilRect);

                if (!IsOpen)
                {
                    g.DrawLine(pen, rect.X - 1, rect.Y + rect.Height + 1, rect.Right + 1, rect.Y - 1);
                }
            }
        }
    }
}
