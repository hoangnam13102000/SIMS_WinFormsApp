using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls.Toast
{

    internal sealed class ToastNotification : Form
    {
        internal const int CardWidth = 380;

        private const int Padding = 16;
        private const int IconSize = 22;
        private const int CloseSize = 14;
        private const int TextGap = 10;
        private const int CornerRadius = 12;
        private const int FadeStepMs = 25;
        private const double FadeInStep = 0.15d;
        private const double FadeOutStep = 0.18d;

        private readonly Color _accentColor;
        private readonly Timer _fadeTimer = new Timer();
        private Timer _lifeTimer;
        private bool _isClosing;

        /// <summary>Phát sinh khi toast đã fade-out xong và tự đóng, để ToastService dồn lại các toast còn lại.</summary>
        public event EventHandler Dismissed;

        public ToastNotification(Form owner, DialogType type, string title, string message)
        {
            if (owner != null) Owner = owner;

            AutoScaleMode = AutoScaleMode.None;
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            TopMost = true;
            DoubleBuffered = true;
            Opacity = 0d;

            _accentColor = DialogTypeMetadata.GetAccentColor(type);
            BackColor = DialogTypeMetadata.GetSoftBgColor(type);

            var iconBox = new IconPictureBox
            {
                Size = new Size(IconSize, IconSize),
                Location = new Point(Padding, Padding),
                BackColor = Color.Transparent,
                IconChar = DialogTypeMetadata.GetIcon(type),
                IconColor = _accentColor,
                IconSize = (int)(IconSize * 0.92),
                SizeMode = PictureBoxSizeMode.CenterImage
            };
            Controls.Add(iconBox);

            var closeBox = new IconPictureBox
            {
                Size = new Size(CloseSize, CloseSize),
                Location = new Point(CardWidth - Padding - CloseSize, Padding + 3),
                BackColor = Color.Transparent,
                IconChar = IconChar.Xmark,
                IconColor = AppColors.TextMuted,
                IconSize = CloseSize,
                Cursor = Cursors.Hand
            };
            closeBox.Click += (s, e) => Dismiss();
            Controls.Add(closeBox);

            int textLeft = Padding + IconSize + TextGap;
            int textWidth = Math.Max(60, CardWidth - textLeft - Padding - CloseSize - TextGap);

            var titleLabel = new Label
            {
                AutoSize = true,
                MaximumSize = new Size(textWidth, 0),
                Location = new Point(textLeft, Padding - 1),
                Font = AppFonts.BodyBold,
                ForeColor = AppColors.TextTitle,
                BackColor = Color.Transparent,
                Text = title ?? string.Empty,
                UseMnemonic = false
            };
            Controls.Add(titleLabel);

            Label messageLabel = null;
            if (!string.IsNullOrWhiteSpace(message))
            {
                messageLabel = new Label
                {
                    AutoSize = true,
                    MaximumSize = new Size(textWidth, 0),
                    Location = new Point(textLeft, titleLabel.Bottom + 3),
                    Font = AppFonts.Small,
                    ForeColor = AppColors.TextSecondary,
                    BackColor = Color.Transparent,
                    Text = message,
                    UseMnemonic = false
                };
                Controls.Add(messageLabel);
            }

            int contentBottom = Math.Max(iconBox.Bottom, messageLabel?.Bottom ?? titleLabel.Bottom);
            ClientSize = new Size(CardWidth, contentBottom + Padding);
            ApplyRoundedRegion();

            // Bấm vào bất kỳ đâu trên toast (kể cả tiêu đề/nội dung) đều đóng sớm được,
            // không cần đợi hết thời gian hiển thị.
            AttachDismissOnClick(this);

            _fadeTimer.Interval = FadeStepMs;
            _fadeTimer.Tick += FadeTimer_Tick;
        }

        /// <summary>Toast không cướp focus của cửa sổ đang thao tác khi xuất hiện (giống toast hệ thống Windows).</summary>
        protected override bool ShowWithoutActivation => true;

        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_EX_NOACTIVATE = 0x08000000;
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= WS_EX_NOACTIVATE;
                return cp;
            }
        }

        private void AttachDismissOnClick(Control root)
        {
            root.Click += (s, e) => Dismiss();
            foreach (Control child in root.Controls)
            {
                AttachDismissOnClick(child);
            }
        }

        /// <summary>Hiển thị toast tại vị trí đã tính sẵn, fade-in rồi tự đếm ngược trước khi tự fade-out.</summary>
        public void Present(Point location, int durationMs)
        {
            Location = location;
            Show();

            _fadeTimer.Start();

            _lifeTimer = new Timer { Interval = Math.Max(500, durationMs) };
            _lifeTimer.Tick += (s, e) =>
            {
                _lifeTimer.Stop();
                Dismiss();
            };
            _lifeTimer.Start();
        }

        /// <summary>Cập nhật lại vị trí (dùng khi ToastService dồn các toast còn lại lên sau khi 1 toast biến mất).</summary>
        public void Reposition(Point location)
        {
            if (IsDisposed) return;
            Location = location;
        }

        /// <summary>Đóng sớm (bấm vào toast/nút X) hoặc hết giờ - fade-out rồi mới thực sự đóng. Gọi nhiều lần vô hại.</summary>
        public void Dismiss()
        {
            if (_isClosing) return;
            _isClosing = true;
            _lifeTimer?.Stop();
            _fadeTimer.Start();
        }

        private void FadeTimer_Tick(object sender, EventArgs e)
        {
            double next = Opacity + (_isClosing ? -FadeOutStep : FadeInStep);

            if (_isClosing && next <= 0d)
            {
                _fadeTimer.Stop();
                Dismissed?.Invoke(this, EventArgs.Empty);
                Close();
                return;
            }

            if (!_isClosing && next >= 1d)
            {
                next = 1d;
                _fadeTimer.Stop();
            }

            try { Opacity = next; }
            catch
            {
                // Một số hệ thống không hỗ trợ per-pixel opacity trên layered window -
                // bỏ qua có chủ đích: toast vẫn hiển thị bình thường, chỉ mất hiệu ứng fade.
            }
        }

        private void ApplyRoundedRegion()
        {
            if (Width <= 0 || Height <= 0) return;
            var rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using (GraphicsPath path = AppRadius.GetRoundedPath(rect, CornerRadius))
            {
                Region = new Region(path);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            var contentRect = new Rectangle(0, 0, Math.Max(1, ClientSize.Width - 1), Math.Max(1, ClientSize.Height - 1));
            using (GraphicsPath borderPath = AppRadius.GetRoundedPath(contentRect, CornerRadius))
            using (var pen = new Pen(_accentColor, 1.4f))
            {
                g.DrawPath(pen, borderPath);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _fadeTimer.Tick -= FadeTimer_Tick;
                _fadeTimer.Dispose();
                _lifeTimer?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}