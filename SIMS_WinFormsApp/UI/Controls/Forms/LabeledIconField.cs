using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{

    [ToolboxItem(false)]
    [DesignerCategory("Code")]
    public class LabeledIconField : UserControl
    {
        private const int FieldHeight = 46;
        private const int SpacingAfterLabel = 6;
        private const int SpacingAfterField = 6;
        private const int SpacingAfterBlock = 18;

        private readonly Label _lblTitle;
        private readonly Label _lblRequiredMark;
        private readonly IconRoundedTextBox _textBox;
        private readonly Label _lblHint;

        public LabeledIconField()
        {
            BackColor = Color.Transparent;
            Dock = DockStyle.Top;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;

            _lblTitle = new Label
            {
                AutoSize = true,
                Font = AppFonts.SmallBold,
                ForeColor = AppColors.TextTitle,
                BackColor = Color.Transparent,
                Location = new Point(0, 0)
            };
            _lblRequiredMark = new Label
            {
                AutoSize = true,
                Text = " *",
                Font = AppFonts.SmallBold,
                ForeColor = AppColors.Error,
                BackColor = Color.Transparent,
                Visible = false
            };
            _textBox = new IconRoundedTextBox { Height = FieldHeight };
            _lblHint = new Label
            {
                AutoSize = false,
                Font = AppFonts.Small,
                ForeColor = AppColors.TextMuted,
                BackColor = Color.Transparent,
                Visible = false
            };

            Controls.Add(_lblHint);
            Controls.Add(_textBox);
            Controls.Add(_lblRequiredMark);
            Controls.Add(_lblTitle);

            Resize += (s, e) => LayoutChildren();
            LayoutChildren();
        }

        public string LabelText
        {
            get => _lblTitle.Text;
            set { _lblTitle.Text = value ?? string.Empty; LayoutChildren(); }
        }

        public bool IsRequired
        {
            get => _lblRequiredMark.Visible;
            set { _lblRequiredMark.Visible = value; LayoutChildren(); }
        }

        public IconChar Icon
        {
            get => _textBox.Icon;
            set => _textBox.Icon = value;
        }

        public string PlaceholderText
        {
            get => _textBox.PlaceholderText;
            set => _textBox.PlaceholderText = value;
        }

        public int MaxLength
        {
            get => _textBox.MaxLength;
            set => _textBox.MaxLength = value;
        }

        public string HintText
        {
            get => _lblHint.Text;
            set
            {
                _lblHint.Text = value ?? string.Empty;
                _lblHint.Visible = !string.IsNullOrEmpty(_lblHint.Text);
                LayoutChildren();
            }
        }

        /// <summary>Giá trị hiện tại của ô nhập, đã Trim().</summary>
        public string Value
        {
            get => (_textBox.Text ?? string.Empty).Trim();
            set => _textBox.Text = value ?? string.Empty;
        }

        public void FocusInput() => _textBox.FocusInput();

        private void LayoutChildren()
        {
            if (_textBox == null) return;

            _lblTitle.Location = new Point(0, 0);
            _lblRequiredMark.Location = new Point(_lblTitle.Right + 1, 0);

            int titleBottom = Math.Max(_lblTitle.Bottom, _lblRequiredMark.Visible ? _lblRequiredMark.Bottom : 0);
            _textBox.Location = new Point(0, titleBottom + SpacingAfterLabel);
            int fieldWidth = Math.Max(10, ClientSize.Width);
            _textBox.Width = fieldWidth;

            int y = _textBox.Bottom + SpacingAfterField;
            if (_lblHint.Visible)
            {
                _lblHint.Location = new Point(0, y);
                _lblHint.Width = fieldWidth;
                int hintHeight = TextRenderer.MeasureText(
                    _lblHint.Text, _lblHint.Font, new Size(fieldWidth, int.MaxValue),
                    TextFormatFlags.WordBreak | TextFormatFlags.NoPadding).Height;
                _lblHint.Height = Math.Max(14, hintHeight);
                y = _lblHint.Bottom;
            }

            int preferredHeight = y + SpacingAfterBlock;
            if (Height != preferredHeight)
                Height = preferredHeight;
        }
    }
}