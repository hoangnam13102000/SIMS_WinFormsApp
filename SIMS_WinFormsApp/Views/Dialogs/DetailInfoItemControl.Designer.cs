using System;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Views.Dialogs
{
    public partial class DetailInfoItemControl
    {
        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor, true);

            this.Size = new Size(260, 56);
            this.MinimumSize = new Size(160, 48);
            this.BackColor = Color.Transparent;

            _iconBox = new Panel
            {
                Size = new Size(IconBoxSize, IconBoxSize),
                Location = new Point(0, 0),
                BackColor = Color.Transparent
            };
            this._iconBox.Paint += new System.Windows.Forms.PaintEventHandler(this.IconBox_Paint);
            this.Controls.Add(_iconBox);

            _iconGlyph = new IconPictureBox
            {
                IconChar = IconChar.CircleInfo,
                IconColor = AppColors.Accent,
                IconSize = 18,
                Size = new Size(20, 20),
                Location = new Point((IconBoxSize - 20) / 2, (IconBoxSize - 20) / 2),
                BackColor = Color.Transparent
            };
            _iconBox.Controls.Add(_iconGlyph);

            _lblLabel = new Label
            {
                AutoSize = false,
                Text = string.Empty,
                Font = AppFonts.Small,
                ForeColor = AppColors.TextMuted,
                BackColor = Color.Transparent,
                Location = new Point(TextLeftOffset, 2),
                Size = new Size(Width - TextLeftOffset, LabelTextHeight),
                TextAlign = ContentAlignment.MiddleLeft,
                AutoEllipsis = true
            };
            this.Controls.Add(_lblLabel);

            _lblValue = new Label
            {
                AutoSize = false,
                Text = string.Empty,
                Font = AppFonts.BodyBold,
                ForeColor = AppColors.TextTitle,
                BackColor = Color.Transparent,
                Location = new Point(TextLeftOffset, _lblLabel.Bottom + 2),
                Size = new Size(Width - TextLeftOffset, ValueTextHeight),
                TextAlign = ContentAlignment.MiddleLeft,
                AutoEllipsis = true
            };
            this.Controls.Add(_lblValue);

            LayoutChildren();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
