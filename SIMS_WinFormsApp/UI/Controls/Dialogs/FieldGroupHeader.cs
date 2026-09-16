using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{

    [ToolboxItem(false)]
    [DesignerCategory("Code")]
    public class FieldGroupHeader : Panel
    {
        private const int IconSize = 18;
        private const int TextGap = 8;
        private const int SpacingAfter = 14;

        private readonly IconPictureBox _iconBox;
        private readonly Label _lblText;

        public FieldGroupHeader()
        {
            BackColor = Color.Transparent;
            Dock = DockStyle.Top;
            Height = IconSize + SpacingAfter;

            _iconBox = new IconPictureBox
            {
                Size = new Size(IconSize, IconSize),
                Location = new Point(0, 1),
                BackColor = Color.Transparent,
                IconColor = AppColors.Accent,
                IconSize = IconSize,
                SizeMode = PictureBoxSizeMode.CenterImage
            };
            Controls.Add(_iconBox);

            _lblText = new Label
            {
                AutoSize = true,
                Location = new Point(IconSize + TextGap, 0),
                Font = AppFonts.SmallBold,
                ForeColor = AppColors.TextSecondary,
                BackColor = Color.Transparent,
                UseMnemonic = false
            };
            Controls.Add(_lblText);
        }

        public IconChar Icon
        {
            get => _iconBox.IconChar;
            set => _iconBox.IconChar = value;
        }

        public Color AccentColor
        {
            get => _iconBox.IconColor;
            set => _iconBox.IconColor = value;
        }

        /// <summary>Nội dung tiêu đề nhóm - tự động in hoa để đồng nhất phong cách nhãn nhóm.</summary>
        public string HeaderText
        {
            get => _lblText.Text;
            set => _lblText.Text = (value ?? string.Empty).ToUpperInvariant();
        }
    }
}