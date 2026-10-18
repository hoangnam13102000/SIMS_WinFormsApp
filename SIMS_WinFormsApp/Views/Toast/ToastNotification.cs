using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Views.Toast
{

    internal sealed partial class ToastNotification : Form
    {
        internal const int CardWidth = 380;

        private const int CardPadding = 16;
        private const int IconSize = 28;
        private const int CloseSize = 14;
        private const int TextGap = 12;
        private const int CornerRadius = 12;
        private const int FadeStepMs = 25;
        private const double FadeInStep = 0.15d;
        private const double FadeOutStep = 0.18d;

        private Color _accentColor;
        private readonly Timer _fadeTimer = new Timer();
        private Timer _lifeTimer;
        private bool _isClosing;
        private IconPictureBox _iconBox;
        private IconPictureBox _closeBox;
        private Label _titleLabel;
        private Label _messageLabel;

        /// <summary>Phát sinh khi toast đã fade-out xong và tự đóng, để ToastService dồn lại các toast còn lại.</summary>
        public event EventHandler Dismissed;

        public ToastNotification()
            : this(null, DialogType.Info, "Thông tin", "Đây là nội dung xem trước của thông báo.")
        {
        }

        public ToastNotification(Form owner, DialogType type, string title, string message)
        {
            InitializeComponent();

            bool isDesignTime = LicenseManager.UsageMode == LicenseUsageMode.Designtime;
            if (owner != null) Owner = owner;

            AutoScaleMode = AutoScaleMode.None;
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            TopMost = !isDesignTime;
            DoubleBuffered = true;
            Opacity = isDesignTime ? 1d : 0d;

            _accentColor = DialogTypeMetadata.GetAccentColor(type);
            BackColor = DialogTypeMetadata.GetSoftBgColor(type);

            int textLeft = CardPadding + IconSize + TextGap;
            int textWidth = Math.Max(60, CardWidth - textLeft - CardPadding - CloseSize - TextGap);
            _titleLabel.MaximumSize = new Size(textWidth, 0);
            _titleLabel.Text = title ?? string.Empty;
            _messageLabel.MaximumSize = new Size(textWidth, 0);
            _messageLabel.Text = message ?? string.Empty;
            _messageLabel.Visible = !string.IsNullOrWhiteSpace(message);
            _iconBox.IconChar = DialogTypeMetadata.GetIcon(type);
            _iconBox.IconColor = _accentColor;
            _closeBox.Click += (s, e) => Dismiss();
            _messageLabel.PerformLayout();
            _titleLabel.PerformLayout();

            const int LineGap = 3;
            int titleHeight = _titleLabel.PreferredSize.Height;
            int messageHeight = _messageLabel.Visible ? _messageLabel.PreferredSize.Height : 0;
            int textBlockHeight = _messageLabel.Visible ? titleHeight + LineGap + messageHeight : titleHeight;

            int blockHeight = Math.Max(IconSize, textBlockHeight);
            int iconTop = CardPadding + (blockHeight - IconSize) / 2;
            int textTop = CardPadding + (blockHeight - textBlockHeight) / 2;

            _iconBox.Location = new Point(CardPadding, iconTop);
            _titleLabel.Location = new Point(textLeft, textTop);
            _messageLabel.Location = new Point(textLeft, _titleLabel.Bottom + LineGap);

            int contentBottom = Math.Max(_iconBox.Bottom,
                _messageLabel.Visible ? _messageLabel.Bottom : _titleLabel.Bottom);
            ClientSize = new Size(CardWidth, contentBottom + CardPadding);

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