using System;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Views.Dialogs
{
    public partial class InfoBannerPanel
    {
        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor, true);

            this.Dock = DockStyle.Top;
            this.BackColor = Color.Transparent;
            this.Height = 96;

            _iconBox = new Panel
            {
                Size = new Size(IconBoxSize, IconBoxSize),
                Location = new Point(PanelPadding, PanelPadding),
                BackColor = Color.Transparent
            };
            _iconBox.Paint += IconBox_Paint;
            this.Controls.Add(_iconBox);

            _iconGlyph = new IconPictureBox
            {
                IconChar = IconChar.UserGear,
                IconColor = AppColors.Accent,
                IconSize = 18,
                Size = new Size(20, 20),
                Location = new Point((IconBoxSize - 20) / 2, (IconBoxSize - 20) / 2),
                BackColor = Color.Transparent
            };
            _iconBox.Controls.Add(_iconGlyph);

            _lblTitle = new Label
            {
                AutoSize = false,
                Font = AppFonts.BodyBold,
                ForeColor = AppColors.TextTitle,
                BackColor = Color.Transparent,
                Location = new Point(PanelPadding + TextLeftOffset, PanelPadding - 2)
            };
            this.Controls.Add(_lblTitle);

            _lblDescription = new Label
            {
                AutoSize = false,
                Font = AppFonts.Small,
                ForeColor = AppColors.TextSecondary,
                BackColor = Color.Transparent,
                Location = new Point(PanelPadding + TextLeftOffset, PanelPadding + 18)
            };
            this.Controls.Add(_lblDescription);

            this.Resize += (s, e) => LayoutChildren();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
