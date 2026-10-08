using System;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{
    public sealed class ReadOnlyInfoItem : Panel
    {
        private const int IconSize = 14;
        private const int IconColumnWidth = 22;

        private readonly IconPictureBox _icon;
        private readonly Label _caption;
        private readonly Label _value;
        private readonly int _captionHeight;
        private readonly int _valueHeight;

        public IconChar Icon
        {
            get { return _icon.IconChar; }
            set { _icon.IconChar = value; }
        }

        public string Caption
        {
            get { return _caption.Text; }
            set { _caption.Text = value ?? string.Empty; }
        }

        public string Value
        {
            get { return _value.Text; }
            set { _value.Text = value ?? string.Empty; }
        }

        public int PreferredHeight
        {
            get { return _captionHeight + _valueHeight; }
        }

        public ReadOnlyInfoItem() : this(IconChar.CircleInfo, string.Empty, string.Empty)
        {
        }

        public ReadOnlyInfoItem(IconChar icon, string caption, string value)
        {
            BackColor = Color.Transparent;
            _icon = new IconPictureBox
            {
                IconChar = icon,
                IconColor = AppColors.TextMuted,
                IconSize = IconSize,
                Size = new Size(IconSize, IconSize),
                BackColor = Color.Transparent
            };
            _caption = new Label
            {
                AutoSize = false,
                Text = caption ?? string.Empty,
                Font = AppFonts.Small,
                ForeColor = AppColors.TextMuted,
                BackColor = Color.Transparent,
                AutoEllipsis = true,
                UseMnemonic = false
            };
            _value = new Label
            {
                AutoSize = false,
                Text = value ?? string.Empty,
                Font = AppFonts.BodyBold,
                ForeColor = AppColors.TextTitle,
                BackColor = Color.Transparent,
                AutoEllipsis = true,
                UseMnemonic = false
            };

            _captionHeight = Math.Max(16, MeasureLineHeight("Ag", _caption.Font));
            _valueHeight = Math.Max(20, MeasureLineHeight("Ag", _value.Font));
            Height = PreferredHeight;
            Controls.Add(_icon);
            Controls.Add(_caption);
            Controls.Add(_value);
            Resize += ReadOnlyInfoItem_Resize;
            LayoutParts();
        }

        private static int MeasureLineHeight(string text, Font font)
        {
            return TextRenderer.MeasureText(
                text,
                font,
                new Size(int.MaxValue, int.MaxValue),
                TextFormatFlags.NoPadding).Height;
        }

        private void ReadOnlyInfoItem_Resize(object sender, EventArgs e)
        {
            LayoutParts();
        }

        private void LayoutParts()
        {
            int textWidth = Math.Max(10, Width - IconColumnWidth);
            _icon.Location = new Point(0, 4);
            _caption.SetBounds(IconColumnWidth, 0, textWidth, _captionHeight);
            _value.SetBounds(IconColumnWidth, _captionHeight, textWidth, _valueHeight);
        }
    }
}
