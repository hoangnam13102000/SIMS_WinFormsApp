using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{

    [ToolboxItem(false)]
    [DesignerCategory("Code")]
    public class IconRoundedTextBox : RoundedTextBox
    {
        private const int IconAreaWidth = 32;

        private readonly IconPictureBox _icon;
        private bool _useThousandsSeparator;

        public IconRoundedTextBox()
        {
            _icon = new IconPictureBox
            {
                IconChar = IconChar.User,
                IconColor = AppColors.TextMutedAlt,
                IconSize = 16,
                Size = new Size(16, 16),
                BackColor = Color.Transparent
            };
            Controls.Add(_icon);

            Resize += (s, e) => PositionIcon();
            PositionIcon();
        }

        /// <summary>Icon hiển thị bên trái ô nhập.</summary>
        public IconChar Icon
        {
            get => _icon.IconChar;
            set => _icon.IconChar = value;
        }

        public Color IconColor
        {
            get => _icon.IconColor;
            set => _icon.IconColor = value;
        }

        protected override int LeadingWidth => IconAreaWidth;

        public bool UseThousandsSeparator
        {
            get => _useThousandsSeparator;
            set
            {
                if (_useThousandsSeparator == value) return;
                _useThousandsSeparator = value;
                if (value)
                    NumberInputFormatter.Attach(InputControl);
            }
        }

        public new int MaxLength
        {
            get => _useThousandsSeparator
                ? NumberInputFormatter.GetMaximumDigits(InputControl)
                : base.MaxLength;
            set
            {
                if (_useThousandsSeparator)
                    NumberInputFormatter.SetMaximumDigits(InputControl, value);
                else
                    base.MaxLength = value;
            }
        }

        private void PositionIcon()
        {
            if (_icon == null) return;
            _icon.Location = new Point(15, (Height - _icon.Height) / 2);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            _icon.IconColor = AppColors.TextMutedAlt;
            PositionIcon();
        }
    }
}