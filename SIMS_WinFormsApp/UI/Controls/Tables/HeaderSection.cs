using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{
    /// <summary>
    /// Header section dùng cho Dashboard / các trang nội dung.
    /// Hiển thị icon tròn + tiêu đề + mô tả phụ.
    /// </summary>
    [ToolboxItem(true)]
    [DesignerCategory("Code")]
    public class HeaderSection : Control
    {
        #region Fields

        private IconPictureBox _iconBox;
        private Panel _iconCircle;
        private Label _lblTitle;
        private Label _lblSubtitle;
        private Panel _actionHost;
        private int _actionWidth;

        private string _title = "Tổng quan";
        private string _subtitle = string.Empty;
        private IconChar _icon = IconChar.GaugeHigh;
        private Color _iconColor = Color.FromArgb(37, 99, 235);
        private Color _iconBackground = Color.FromArgb(219, 234, 254);

        #endregion

        #region Properties

        [Category("HeaderSection")]
        [Description("Tiêu đề chính")]
        public string Title
        {
            get => _title;
            set
            {
                _title = value ?? string.Empty;
                if (_lblTitle != null) _lblTitle.Text = _title;
                Invalidate();
            }
        }

        [Category("HeaderSection")]
        [Description("Mô tả phụ dưới tiêu đề")]
        public string Subtitle
        {
            get => _subtitle;
            set
            {
                _subtitle = value ?? string.Empty;
                if (_lblSubtitle != null) _lblSubtitle.Text = _subtitle;
                Invalidate();
            }
        }

        [Category("HeaderSection")]
        [Description("Icon FontAwesome")]
        public IconChar Icon
        {
            get => _icon;
            set
            {
                _icon = value;
                if (_iconBox != null) _iconBox.IconChar = _icon;
                Invalidate();
            }
        }

        [Category("HeaderSection")]
        [Description("Màu icon")]
        public Color IconColor
        {
            get => _iconColor;
            set
            {
                _iconColor = value;
                if (_iconBox != null) _iconBox.IconColor = _iconColor;
                Invalidate();
            }
        }

        [Category("HeaderSection")]
        [Description("Màu nền vòng tròn icon")]
        public Color IconBackground
        {
            get => _iconBackground;
            set
            {
                _iconBackground = value;
                _iconCircle?.Invalidate();
            }
        }

        #endregion

        #region Constructor

        public HeaderSection()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor, true);

            // Dùng cùng một chuẩn height cho toàn project, lấy theo dashboard để mọi page
            // đều có header đồng nhất. Không để từng page tự định nghĩa row khác nhau.
            Height = 120;
            MinimumSize = new Size(300, 120);
            BackColor = AppColors.White;
            Padding = new Padding(0);
            Margin = new Padding(0, 0, 0, 12);

            BuildUI();
        }

        #endregion

        #region Build UI

        private void BuildUI()
        {
            SuspendLayout();

            // Icon circle – canh giữa theo chiều cao mới (108px)
            _iconCircle = new Panel
            {
                Size = new Size(42, 42),
                Location = new Point(20, (Height - 42) / 2),
                BackColor = Color.Transparent
            };
            _iconCircle.Paint += IconCircle_Paint;
            Controls.Add(_iconCircle);

            // Icon
            _iconBox = new IconPictureBox
            {
                IconChar = _icon,
                IconColor = _iconColor,
                IconSize = 24,
                Size = new Size(28, 28),
                Location = new Point(10, 10),
                BackColor = Color.Transparent
            };
            _iconCircle.Controls.Add(_iconBox);

            var titleFont = new Font("Segoe UI Semibold", 16f, FontStyle.Bold);
            var subtitleFont = new Font("Segoe UI", 9.5f);

            // Đo chiều cao THẬT của font tại runtime (bao gồm đuôi chữ g,q,y và dấu tiếng Việt)
            // thay vì đoán số cố định — tránh bị cắt chữ nếu máy chạy không có đúng font
            // "Segoe UI Semibold" và Windows phải dùng font thay thế có kích thước khác.
            int titleH = TextRenderer.MeasureText("Ẵợgqy", titleFont,
                new Size(int.MaxValue, int.MaxValue), TextFormatFlags.NoPadding).Height + 6;
            int subtitleH = TextRenderer.MeasureText("Ẵợgqy", subtitleFont,
                new Size(int.MaxValue, int.MaxValue), TextFormatFlags.NoPadding).Height + 6;

            // Title
            _lblTitle = new Label
            {
                AutoSize = false,
                Text = _title,
                Font = titleFont,
                ForeColor = AppColors.TextTitle,
                Location = new Point(78, 16),
                Size = new Size(Math.Max(80, Width - 104), titleH),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Transparent,
                AutoEllipsis = true
            };
            Controls.Add(_lblTitle);

            // Subtitle – cách Title một khoảng rõ ràng, tính theo chiều cao Title đo được
            _lblSubtitle = new Label
            {
                AutoSize = false,
                Text = _subtitle,
                Font = subtitleFont,
                ForeColor = AppColors.TextSecondary,
                Location = new Point(78, _lblTitle.Bottom + 3),
                Size = new Size(Math.Max(80, Width - 104), subtitleH),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Transparent,
                AutoEllipsis = true
            };
            Controls.Add(_lblSubtitle);

            _actionHost = new Panel
            {
                Dock = DockStyle.Right,
                Width = 320,
                BackColor = Color.Transparent,
                Padding = new Padding(0, 20, 14, 0),
                Visible = false
            };
            Controls.Add(_actionHost);

            ThemeManager.Instance.ThemeChanged += OnThemeChanged;
            ResumeLayout(false);
        }

        public void SetActions(Control actions)
        {
            if (actions == null) throw new ArgumentNullException(nameof(actions));

            _actionHost.Controls.Clear();
            actions.Dock = DockStyle.Fill;
            _actionHost.Controls.Add(actions);
            _actionWidth = _actionHost.Width;
            _actionHost.Visible = true;
            PerformLayout();
            UpdateTextLayout();
        }

        private void OnThemeChanged(object sender, EventArgs e)
        {
            BackColor = AppColors.White;
            _lblTitle.ForeColor = AppColors.TextTitle;
            _lblSubtitle.ForeColor = AppColors.TextSecondary;
            _iconBox.IconColor = AppColors.Info;
            _iconBackground = AppColors.InfoBg;
            Invalidate(true);
            Refresh();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                ThemeManager.Instance.ThemeChanged -= OnThemeChanged;
            base.Dispose(disposing);
        }

        #endregion

        #region Paint

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            var rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using (var path = AppRadius.GetRoundedPath(rect, AppRadius.Large))
            using (var brush = new SolidBrush(AppColors.White))
            {
                g.FillPath(brush, path);
            }

            using (var pen = new Pen(AppColors.Border, 1f))
            {
                var borderRect = new Rectangle(0, 0, Width - 1, Height - 1);
                g.DrawPath(pen, AppRadius.GetRoundedPath(borderRect, AppRadius.Large));
            }

            base.OnPaint(e);
        }

        private void IconCircle_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (var path = new GraphicsPath())
            {
                path.AddEllipse(0, 0, _iconCircle.Width - 1, _iconCircle.Height - 1);
                using (var brush = new SolidBrush(_iconBackground))
                    g.FillPath(brush, path);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (_iconCircle != null)
            {
                int centerY = Math.Max(0, (ClientSize.Height - _iconCircle.Height) / 2);
                _iconCircle.Location = new Point(20, centerY);
            }

            UpdateTextLayout();

            Invalidate();
        }

        private void UpdateTextLayout()
        {
            int textW = Math.Max(80, ClientSize.Width - 104 - _actionWidth);
            if (_lblTitle == null || _lblSubtitle == null) return;

            _lblTitle.Width = textW;
            _lblSubtitle.Width = textW;
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            pevent.Graphics.Clear(BackColor);
        }

        #endregion

        #region Public helpers

        public void SetDashboard(string userDisplayName)
        {
            Title = "Tổng quan";
            Subtitle = $"Chào {userDisplayName}, đây là toàn cảnh hoạt động kinh doanh của ConnectMart hôm nay";
            Icon = IconChar.GaugeHigh;
            IconColor = Color.FromArgb(37, 99, 235);
            IconBackground = Color.FromArgb(219, 234, 254);
        }

        public void Set(string title, string subtitle, IconChar icon,
            Color? iconColor = null, Color? iconBg = null)
        {
            Title = title;
            Subtitle = subtitle;
            Icon = icon;
            if (iconColor.HasValue) IconColor = iconColor.Value;
            if (iconBg.HasValue) IconBackground = iconBg.Value;
        }

        #endregion
    }
}