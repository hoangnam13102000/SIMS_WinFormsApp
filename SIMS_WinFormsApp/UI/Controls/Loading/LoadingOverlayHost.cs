using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls.Loading
{
    public sealed class LoadingOverlayHost : Panel, ILoadingIndicator
    {
        private const int SpinIntervalMs = 40;
        private const float SpinStepDegrees = 18f;

        private readonly Control _content;
        private readonly Panel _overlay;
        private readonly IconPictureBox _spinnerIcon;
        private readonly Label _lblMessage;
        private readonly Timer _spinTimer;
        private float _spinAngle;

        public LoadingOverlayHost(Control content)
        {
            _content = content ?? throw new ArgumentNullException(nameof(content));

            Dock = DockStyle.Fill;
            BackColor = Color.Transparent;

            _content.Dock = DockStyle.Fill;
            Controls.Add(_content);

            _spinnerIcon = new IconPictureBox
            {
                IconChar = IconChar.Spinner,
                IconColor = AppColors.Accent,
                IconSize = 36,
                Size = new Size(36, 36),
                BackColor = Color.Transparent
            };

            _lblMessage = new Label
            {
                AutoSize = true,
                Font = AppFonts.SmallBold,
                ForeColor = AppColors.TextTitle,
                BackColor = Color.Transparent,
                Text = "Đang tải dữ liệu..."
            };

            _overlay = new Panel
            {
                Dock = DockStyle.Fill,
                Visible = false,
                BackColor = Color.Transparent
            };
            _overlay.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var brush = new SolidBrush(AppColors.OverlayBackdrop))
                {
                    e.Graphics.FillRectangle(brush, _overlay.ClientRectangle);
                }
            };
            _overlay.Controls.Add(_spinnerIcon);
            _overlay.Controls.Add(_lblMessage);
            _overlay.Resize += (s, e) => CenterOverlayContent();

            Controls.Add(_overlay);
            _overlay.BringToFront();

            _spinTimer = new Timer { Interval = SpinIntervalMs };
            _spinTimer.Tick += (s, e) =>
            {
                _spinAngle = (_spinAngle + SpinStepDegrees) % 360f;
                _spinnerIcon.Rotation = _spinAngle;
            };
        }

        public void ShowLoading(string message = null)
        {
            _lblMessage.Text = string.IsNullOrWhiteSpace(message) ? "Đang tải dữ liệu..." : message;
            CenterOverlayContent();

            _overlay.Visible = true;
            _overlay.BringToFront();
            _content.Enabled = false;
            _spinTimer.Start();
        }

        public void HideLoading()
        {
            _spinTimer.Stop();
            _overlay.Visible = false;
            _content.Enabled = true;
        }

        private void CenterOverlayContent()
        {
            const int gap = 10;
            int totalHeight = _spinnerIcon.Height + gap + _lblMessage.Height;
            int startY = Math.Max(0, (_overlay.Height - totalHeight) / 2);

            _spinnerIcon.Location = new Point((_overlay.Width - _spinnerIcon.Width) / 2, startY);
            _lblMessage.Location = new Point((_overlay.Width - _lblMessage.Width) / 2, _spinnerIcon.Bottom + gap);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _spinTimer.Stop();
                _spinTimer.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}