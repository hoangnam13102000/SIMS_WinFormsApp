using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{
    public class RoundedPasswordTextBox : RoundedTextBox
    {
        private readonly PasswordEyeToggle _eyeToggle;
        private bool _passwordVisible;

        public RoundedPasswordTextBox()
        {
            InputControl.UseSystemPasswordChar = true;

            _eyeToggle = new PasswordEyeToggle
            {
                Size = new Size(32, 32),
                Cursor = Cursors.Hand
            };
            _eyeToggle.Click += EyeToggle_Click;
            Controls.Add(_eyeToggle);
            _eyeToggle.BringToFront();

            Resize += RoundedPasswordTextBox_Resize;
            PositionToggle();
        }

        protected override int TrailingWidth => 38;

        private void PositionToggle()
        {
            _eyeToggle.Location = new Point(Width - _eyeToggle.Width - 12, (Height - _eyeToggle.Height) / 2);
        }

        public string ShowTooltip { get; set; } = "Hiện mật khẩu";
        public string HideTooltip { get; set; } = "Ẩn mật khẩu";

        private readonly ToolTip _tip = new ToolTip();

        private void TogglePasswordVisibility()
        {
            _passwordVisible = !_passwordVisible;
            InputControl.UseSystemPasswordChar = !_passwordVisible;
            _eyeToggle.IsOpen = _passwordVisible;
            _tip.SetToolTip(_eyeToggle, _passwordVisible ? HideTooltip : ShowTooltip);
            InputControl.Focus();
            InputControl.SelectionStart = InputControl.Text.Length;
        }

        private void EyeToggle_Click(object sender, EventArgs e)
        {
            TogglePasswordVisibility();
        }

        private void RoundedPasswordTextBox_Resize(object sender, EventArgs e)
        {
            PositionToggle();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            PositionToggle();
        }

    }
}