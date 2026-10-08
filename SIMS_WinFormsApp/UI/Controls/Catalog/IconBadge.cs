using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{
    public sealed class IconBadge : Panel
    {
        private readonly IconPictureBox _iconBox;

        public IconChar Icon
        {
            get { return _iconBox.IconChar; }
            set { _iconBox.IconChar = value; }
        }

        public IconBadge() : this(IconChar.Warehouse)
        {
        }

        public IconBadge(IconChar icon)
        {
            Size = new Size(40, 40);
            Location = new Point(0, 4);
            BackColor = Color.Transparent;
            _iconBox = new IconPictureBox
            {
                IconChar = icon,
                IconColor = AppColors.Accent,
                IconSize = 18,
                Size = new Size(18, 18),
                Location = new Point(11, 11),
                BackColor = Color.Transparent
            };
            Controls.Add(_iconBox);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using (GraphicsPath path = AppRadius.GetRoundedPath(rect, AppRadius.Medium))
            using (SolidBrush brush = new SolidBrush(AppColors.AccentBgSoft))
                e.Graphics.FillPath(brush, path);
        }
    }
}
