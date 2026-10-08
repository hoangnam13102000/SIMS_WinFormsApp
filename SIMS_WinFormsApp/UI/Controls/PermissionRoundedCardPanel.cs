using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{
    public class PermissionRoundedCardPanel : Panel
    {
        public PermissionRoundedCardPanel()
        {
            BorderStyle = BorderStyle.None;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            BackColor = AppColors.White;
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            pevent.Graphics.Clear(AppColors.White);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using (GraphicsPath path = AppRadius.GetRoundedPath(rect, AppRadius.Large))
            using (var brush = new SolidBrush(AppColors.White))
            using (var pen = new Pen(AppColors.Border, 1f))
            {
                g.FillPath(brush, path);
                g.DrawPath(pen, path);
            }
            base.OnPaint(e);
        }
    }
}
