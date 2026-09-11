using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{
    public class RoundedTextBox : UserControl
    {
        private readonly TextBox _textBox;
        private readonly Label _placeholderLabel;
        private int _cornerRadius = AppRadius.Medium;
        private bool _isFocused;

        public event System.EventHandler TextChanged2;

        public RoundedTextBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                      ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);

            BackColor = System.Drawing.Color.Transparent;
            Height = 52;
            Padding = new Padding(14, 0, 14, 0);

            _textBox = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Font = AppFonts.Input,
                Location = new Point(14, 12),
                Width = Width - 28
            };
            _textBox.GotFocus += (s, e) => { _isFocused = true; Invalidate(); };
            _textBox.LostFocus += (s, e) => { _isFocused = false; Invalidate(); };
            _textBox.TextChanged += (s, e) =>
            {
                _placeholderLabel.Visible = string.IsNullOrEmpty(_textBox.Text);
                TextChanged2?.Invoke(this, System.EventArgs.Empty);
            };

            _placeholderLabel = new Label
            {
                AutoSize = false,
                BackColor = System.Drawing.Color.Transparent,
                ForeColor = AppColors.TextMutedAlt,
                Font = AppFonts.Input,
                Location = new Point(15, 13),
                TextAlign = ContentAlignment.MiddleLeft,
                Enabled = false
            };

            Controls.Add(_placeholderLabel);
            Controls.Add(_textBox);

            Resize += (s, e) => LayoutInner();
            LayoutInner();
        }

        private void LayoutInner()
        {
            _textBox.Location = new Point(14, (Height - _textBox.Height) / 2);
            _textBox.Width = System.Math.Max(10, Width - 28 - TrailingWidth);
            _placeholderLabel.Location = new Point(15, 0);
            _placeholderLabel.Size = new Size(_textBox.Width - 2, Height);
        }

        protected virtual int TrailingWidth => 0;

        protected TextBox InputControl => _textBox;

        public override string Text
        {
            get => _textBox.Text;
            set => _textBox.Text = value;
        }

        public string PlaceholderText
        {
            get => _placeholderLabel.Text;
            set
            {
                _placeholderLabel.Text = value;
                _placeholderLabel.Visible = string.IsNullOrEmpty(_textBox.Text);
            }
        }

        public int CornerRadius
        {
            get => _cornerRadius;
            set { _cornerRadius = value; Invalidate(); }
        }

        public int MaxLength
        {
            get => _textBox.MaxLength;
            set => _textBox.MaxLength = value;
        }

        public void FocusInput() => _textBox.Focus();

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            var rect = new Rectangle(0, 0, Width - 1, Height - 1);
            Color borderColor = _isFocused ? AppColors.Accent : AppColors.FieldBorder;
            int borderWidth = _isFocused ? 2 : 1;

            using (var path = AppRadius.GetRoundedPath(rect, _cornerRadius))
            using (var fillBrush = new SolidBrush(AppColors.White))
            using (var pen = new Pen(borderColor, borderWidth))
            {
                g.FillPath(fillBrush, path);
                g.DrawPath(pen, path);
            }

            base.OnPaint(e);
        }

        protected override void OnParentBackColorChanged(System.EventArgs e)
        {
            base.OnParentBackColorChanged(e);
            Invalidate();
        }
    }
}