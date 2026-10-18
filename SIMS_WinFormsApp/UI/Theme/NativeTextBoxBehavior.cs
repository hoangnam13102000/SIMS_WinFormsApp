using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace SIMS_WinFormsApp.UI.Theme
{
    internal static class NativeTextBoxBehavior
    {
        private const int EmSetCueBanner = 0x1501;

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr handle, int message, IntPtr wParam, string lParam);

        public static void SetCueBanner(TextBox textBox, string text)
        {
            if (textBox == null) throw new ArgumentNullException(nameof(textBox));
            textBox.CreateControl();
            SendMessage(textBox.Handle, EmSetCueBanner, new IntPtr(1), text ?? string.Empty);
        }

        public static void ApplyInputFont(Control root)
        {
            if (root == null) throw new ArgumentNullException(nameof(root));

            foreach (Control control in root.Controls)
            {
                TextBox textBox = control as TextBox;
                if (textBox != null)
                    textBox.Font = AppFonts.Input;

                if (control.HasChildren)
                    ApplyInputFont(control);
            }
        }

    }

    internal sealed class PasswordVisibilityToggle : IDisposable
    {
        private const int EmSetMargins = 0x00D3;
        private const int EcRightMargin = 0x0002;
        private const int ButtonWidth = 34;
        private const int TextMargin = 42;

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr handle, int message, IntPtr wParam, IntPtr lParam);

        private readonly TextBox _textBox;
        private readonly IconButton _button;
        private readonly ToolTip _toolTip;
        private bool _passwordVisible;
        private string _showText = "Show password";
        private string _hideText = "Hide password";
        private bool _disposed;

        public PasswordVisibilityToggle(TextBox textBox)
        {
            _textBox = textBox ?? throw new ArgumentNullException(nameof(textBox));
            _textBox.UseSystemPasswordChar = true;
            _button = new IconButton
            {
                BackColor = _textBox.BackColor,
                FlatStyle = FlatStyle.Flat,
                IconChar = IconChar.Eye,
                IconColor = System.Drawing.SystemColors.ControlDarkDark,
                IconSize = 18,
                Size = new System.Drawing.Size(ButtonWidth, 30),
                TabStop = false,
                UseVisualStyleBackColor = false
            };
            _button.FlatAppearance.BorderSize = 0;
            _button.FlatAppearance.MouseDownBackColor = _button.BackColor;
            _button.FlatAppearance.MouseOverBackColor = _button.BackColor;
            _button.Click += ToggleVisibility;
            _toolTip = new ToolTip();
            _textBox.Parent.Controls.Add(_button);
            _button.BringToFront();
            _textBox.LocationChanged += Reposition;
            _textBox.SizeChanged += Reposition;
            _textBox.BackColorChanged += Reposition;
            _textBox.EnabledChanged += UpdateEnabled;
            _textBox.HandleCreated += ApplyTextMargin;
            _textBox.Disposed += TextBox_Disposed;
            Reposition(this, EventArgs.Empty);
            ApplyTextMargin(this, EventArgs.Empty);
            ApplyToolTip();
        }

        public void SetTooltips(string showText, string hideText)
        {
            _showText = showText ?? string.Empty;
            _hideText = hideText ?? string.Empty;
            ApplyToolTip();
        }

        private void ToggleVisibility(object sender, EventArgs e)
        {
            _passwordVisible = !_passwordVisible;
            _textBox.UseSystemPasswordChar = !_passwordVisible;
            _button.IconChar = _passwordVisible ? IconChar.EyeSlash : IconChar.Eye;
            _button.AccessibleName = _passwordVisible ? _hideText : _showText;
            ApplyToolTip();
            _textBox.Focus();
            _textBox.SelectionStart = _textBox.TextLength;
        }

        private void ApplyToolTip()
        {
            if (_toolTip != null && _button != null)
                _toolTip.SetToolTip(_button, _passwordVisible ? _hideText : _showText);
        }

        private void Reposition(object sender, EventArgs e)
        {
            if (_disposed || _textBox.IsDisposed || _button.IsDisposed) return;
            _button.BackColor = _textBox.BackColor;
            _button.Location = new System.Drawing.Point(
                _textBox.Right - ButtonWidth - 4,
                _textBox.Top + (_textBox.Height - _button.Height) / 2);
            _button.BringToFront();
        }

        private void UpdateEnabled(object sender, EventArgs e)
        {
            if (!_disposed && !_button.IsDisposed)
                _button.Enabled = _textBox.Enabled;
        }

        private void ApplyTextMargin(object sender, EventArgs e)
        {
            if (!_textBox.IsHandleCreated) return;
            IntPtr margins = new IntPtr(TextMargin << 16);
            SendMessage(_textBox.Handle, EmSetMargins, new IntPtr(EcRightMargin), margins);
        }

        private void TextBox_Disposed(object sender, EventArgs e)
        {
            Dispose();
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            _textBox.LocationChanged -= Reposition;
            _textBox.SizeChanged -= Reposition;
            _textBox.BackColorChanged -= Reposition;
            _textBox.EnabledChanged -= UpdateEnabled;
            _textBox.HandleCreated -= ApplyTextMargin;
            _textBox.Disposed -= TextBox_Disposed;
            _button.Click -= ToggleVisibility;
            _toolTip.Dispose();
            if (!_button.IsDisposed)
                _button.Dispose();
        }
    }
}
