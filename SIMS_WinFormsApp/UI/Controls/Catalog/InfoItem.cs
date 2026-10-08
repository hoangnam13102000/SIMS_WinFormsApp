using System;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{
    public sealed class InfoItem : Panel
    {
        private const int TextLeft = 52;
        private const int TextRightPadding = 6;
        private readonly Label _lblValue;
        private readonly Label _lblLabel;
        private bool _isLayingOut;

        public IconChar Icon
        {
            get { return ((IconBadge)Controls[0]).Icon; }
            set { ((IconBadge)Controls[0]).Icon = value; }
        }

        public string LabelText
        {
            get { return _lblLabel.Text; }
            set { _lblLabel.Text = value ?? string.Empty; LayoutText(); }
        }

        public string Value
        {
            get { return _lblValue.Text; }
            set
            {
                _lblValue.Text = string.IsNullOrWhiteSpace(value) ? "-" : value;
                LayoutText();
            }
        }

        public InfoItem() : this(IconChar.Tags, string.Empty)
        {
        }

        public InfoItem(IconChar icon, string label)
        {
            Dock = DockStyle.Top;
            Height = 64;
            BackColor = Color.Transparent;
            Controls.Add(new IconBadge(icon));

            _lblLabel = new Label
            {
                AutoSize = false,
                Text = label ?? string.Empty,
                Font = AppFonts.Small,
                ForeColor = AppColors.TextMuted,
                BackColor = Color.Transparent,
                UseMnemonic = false,
                Padding = new Padding(0, 4, 0, 4)
            };
            _lblValue = new Label
            {
                AutoSize = false,
                Text = "-",
                Font = AppFonts.BodyBold,
                ForeColor = AppColors.TextTitle,
                BackColor = Color.Transparent,
                UseMnemonic = false,
                Padding = new Padding(0, 4, 0, 4)
            };
            Controls.Add(_lblValue);
            Controls.Add(_lblLabel);
            Resize += InfoItem_Resize;
            LayoutText();
        }

        private void InfoItem_Resize(object sender, EventArgs e)
        {
            LayoutText();
        }

        private void LayoutText()
        {
            if (_isLayingOut || _lblLabel == null || _lblValue == null) return;

            _isLayingOut = true;
            try
            {
                int textWidth = Math.Max(40, ClientSize.Width - TextLeft - TextRightPadding);
                _lblLabel.SetBounds(TextLeft, 0, textWidth, MeasureTextHeight(_lblLabel.Text, _lblLabel.Font, textWidth));
                _lblValue.SetBounds(TextLeft, _lblLabel.Bottom, textWidth, MeasureTextHeight(_lblValue.Text, _lblValue.Font, textWidth));

                int desiredHeight = Math.Max(48, _lblValue.Bottom + 2);
                if (Height != desiredHeight) Height = desiredHeight;
            }
            finally
            {
                _isLayingOut = false;
            }
        }

        private static int MeasureTextHeight(string text, Font font, int width)
        {
            Size measured = TextRenderer.MeasureText(
                text ?? string.Empty,
                font,
                new Size(width, int.MaxValue),
                TextFormatFlags.WordBreak | TextFormatFlags.NoPrefix | TextFormatFlags.NoPadding);
            return measured.Height + 12;
        }
    }
}
