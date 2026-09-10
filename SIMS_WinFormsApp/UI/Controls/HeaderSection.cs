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

            Height = 108;
            MinimumSize = new Size(300, 108);
            // Không set Dock ở đây – parent quyết định
            BackColor = Color.Transparent;
            Padding = new Padding(0);

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
                Size = new Size(48, 48),
                Location = new Point(20, 30),
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
                new Size(int.MaxValue, int.MaxValue), TextFormatFlags.NoPadding).Height + 8;
            int subtitleH = TextRenderer.MeasureText("Ẵợgqy", subtitleFont,
                new Size(int.MaxValue, int.MaxValue), TextFormatFlags.NoPadding).Height + 8;

            // Title
            _lblTitle = new Label
            {
                AutoSize = false,
                Text = _title,
                Font = titleFont,
                ForeColor = AppColors.TextTitle,
                Location = new Point(84, 16),
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
                Location = new Point(84, _lblTitle.Bottom + 4),
                Size = new Size(Math.Max(80, Width - 104), subtitleH),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Transparent,
                AutoEllipsis = true
            };
            Controls.Add(_lblSubtitle);

            ResumeLayout(false);
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
            using (var pen = new Pen(AppColors.Border, 1f))
            {
                g.FillPath(brush, path);
                g.DrawPath(pen, path);
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

            // Chỉ đổi WIDTH theo kích thước control; HEIGHT giữ nguyên giá trị đã đo
            // từ font thật (đo 1 lần trong BuildUI) để không tái tạo lỗi cắt chữ.
            int textW = Math.Max(80, Width - 104);
            if (_lblTitle != null)
                _lblTitle.Width = textW;
            if (_lblSubtitle != null)
                _lblSubtitle.Width = textW;

            Invalidate();
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            if (Parent != null)
                pevent.Graphics.Clear(Parent.BackColor);
            else
                pevent.Graphics.Clear(AppColors.PageBg);
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