using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Controls.Permission;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{
    public class PermissionInfoBannerPanel : Panel
    {
        private readonly IconPictureBox _icon;
        private readonly Label _label;

        public string Message
        {
            get { return _label.Text; }
            set { _label.Text = value ?? string.Empty; }
        }

        public PermissionInfoBannerPanel() : this(string.Empty)
        {
        }

        public PermissionInfoBannerPanel(string message)
        {
            Height = 52;
            BackColor = AppColors.InfoBg;
            Margin = new Padding(0, 0, 0, 12);
            Padding = new Padding(44, 0, 16, 0);
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);

            _icon = new IconPictureBox
            {
                IconChar = IconChar.CircleInfo,
                IconColor = AppColors.Info,
                IconSize = 16,
                Size = new Size(18, 18),
                BackColor = Color.Transparent,
                Location = new Point(16, (Height - 18) / 2)
            };
            _label = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Font = AppFonts.Small,
                ForeColor = AppColors.TextPrimary,
                TextAlign = ContentAlignment.MiddleLeft,
                Text = message ?? string.Empty
            };
            Controls.Add(_label);
            Controls.Add(_icon);
            _icon.BringToFront();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using (GraphicsPath path = AppRadius.GetRoundedPath(rect, AppRadius.Medium))
            using (var brush = new SolidBrush(AppColors.InfoBg))
            {
                g.FillPath(brush, path);
            }
            using (var pen = new Pen(AppColors.Info, 1f))
            using (GraphicsPath path = AppRadius.GetRoundedPath(rect, AppRadius.Medium))
            {
                g.DrawPath(pen, path);
            }
            base.OnPaint(e);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            pevent.Graphics.Clear(PermissionUiHelpers.GetEffectiveBackColor(this));
        }
    }
}
