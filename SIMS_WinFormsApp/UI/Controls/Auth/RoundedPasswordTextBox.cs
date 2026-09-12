using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{
    public class RoundedPasswordTextBox : RoundedTextBox
    {
        private readonly EyeToggle _eyeToggle;
        private bool _passwordVisible;

        public RoundedPasswordTextBox()
        {
            InputControl.UseSystemPasswordChar = true;

            _eyeToggle = new EyeToggle
            {
                Size = new Size(32, 32),
                Cursor = Cursors.Hand
            };
            _eyeToggle.Click += (s, e) => TogglePasswordVisibility();
            Controls.Add(_eyeToggle);
            _eyeToggle.BringToFront();

            Resize += (s, e) => PositionToggle();
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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            PositionToggle();
        }

        private sealed class EyeToggle : Control
        {
            public bool IsOpen { get; set; }

            public EyeToggle()
            {
                SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                          ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
                BackColor = Color.Transparent;
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                var color = AppColors.TextMutedAlt;
                var rect = new RectangleF(4, 8, Width - 8, Height - 16);

                using (var pen = new Pen(color, 1.6f))
                {
                    
                    g.DrawEllipse(pen, rect);

                    float pupilSize = rect.Height * 0.55f;
                    var pupilRect = new RectangleF(
                        rect.X + rect.Width / 2 - pupilSize / 2,
                        rect.Y + rect.Height / 2 - pupilSize / 2,
                        pupilSize, pupilSize);
                    g.DrawEllipse(pen, pupilRect);

                    if (!IsOpen)
                    {
                       
                        g.DrawLine(pen, rect.X - 1, rect.Y + rect.Height + 1, rect.Right + 1, rect.Y - 1);
                    }
                }
            }
        }
    }
}