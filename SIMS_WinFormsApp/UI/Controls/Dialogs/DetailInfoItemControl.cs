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
    /// Một ô thông tin dạng "icon vuông bo góc + nhãn + giá trị", giống các ô trong ảnh mẫu
    /// (Mã khách hàng, Email, Số điện thoại...). Là control độc lập, không biết gì về
    /// User/Customer/Product - vì vậy dùng lại được cho BẤT KỲ popup chi tiết nào
    /// (khách hàng, sản phẩm, nhân viên...) miễn là dữ liệu quy về "icon + nhãn + giá trị".
    /// </summary>
    [ToolboxItem(false)]
    [DesignerCategory("Code")]
    public class DetailInfoItemControl : Control
    {
        private const int IconBoxSize = 40;
        private const int TextLeftOffset = IconBoxSize + 12;

        // Chuỗi mẫu để đo chiều cao dòng thật của font (có dấu tiếng Việt cao nhất + chữ có
        // đuôi xuống g/q/y) - cùng kỹ thuật StatCard đang dùng, tránh cắt chữ như hardcode số
        // pixel cố định.
        private const string HeightSample = "Ẵợgqy";
        private static readonly int LabelTextHeight = MeasureLineHeight(AppFonts.Small);
        private static readonly int ValueTextHeight = MeasureLineHeight(AppFonts.BodyBold);

        private static int MeasureLineHeight(Font font) =>
            TextRenderer.MeasureText(HeightSample, font, new Size(int.MaxValue, int.MaxValue), TextFormatFlags.NoPadding).Height + 4;

        private readonly Panel _iconBox;
        private readonly IconPictureBox _iconGlyph;
        private readonly Label _lblLabel;
        private readonly Label _lblValue;

        private Color _iconBackground = AppColors.AccentBgSoft;

        public IconChar Icon
        {
            get => _iconGlyph.IconChar;
            set => _iconGlyph.IconChar = value;
        }

        public Color IconColor
        {
            get => _iconGlyph.IconColor;
            set => _iconGlyph.IconColor = value;
        }

        public Color IconBackground
        {
            get => _iconBackground;
            set { _iconBackground = value; _iconBox.Invalidate(); }
        }

        public string LabelText
        {
            get => _lblLabel.Text;
            set => _lblLabel.Text = value ?? string.Empty;
        }

        public string ValueText
        {
            get => _lblValue.Text;
            set => _lblValue.Text = value ?? string.Empty;
        }

        public DetailInfoItemControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);

            Size = new Size(260, 56);
            MinimumSize = new Size(160, 48);
            BackColor = Color.Transparent;

            _iconBox = new Panel
            {
                Size = new Size(IconBoxSize, IconBoxSize),
                Location = new Point(0, 0),
                BackColor = Color.Transparent
            };
            _iconBox.Paint += IconBox_Paint;
            Controls.Add(_iconBox);

            _iconGlyph = new IconPictureBox
            {
                IconChar = IconChar.CircleInfo,
                IconColor = AppColors.Accent,
                IconSize = 18,
                Size = new Size(20, 20),
                Location = new Point((IconBoxSize - 20) / 2, (IconBoxSize - 20) / 2),
                BackColor = Color.Transparent
            };
            _iconBox.Controls.Add(_iconGlyph);

            _lblLabel = new Label
            {
                AutoSize = false,
                Text = string.Empty,
                Font = AppFonts.Small,
                ForeColor = AppColors.TextMuted,
                BackColor = Color.Transparent,
                Location = new Point(TextLeftOffset, 2),
                Size = new Size(Width - TextLeftOffset, LabelTextHeight),
                TextAlign = ContentAlignment.MiddleLeft,
                AutoEllipsis = true
            };
            Controls.Add(_lblLabel);

            _lblValue = new Label
            {
                AutoSize = false,
                Text = string.Empty,
                Font = AppFonts.BodyBold,
                ForeColor = AppColors.TextTitle,
                BackColor = Color.Transparent,
                Location = new Point(TextLeftOffset, _lblLabel.Bottom + 2),
                Size = new Size(Width - TextLeftOffset, ValueTextHeight),
                TextAlign = ContentAlignment.MiddleLeft,
                AutoEllipsis = true
            };
            Controls.Add(_lblValue);

            LayoutChildren();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            // Cùng lý do như DetailAvatarPanel: Size gán trong constructor bắn OnResize ngay,
            // trước khi _iconBox/_lblLabel/_lblValue kịp khởi tạo -> phải chặn ở đây.
            if (_iconBox == null) return;
            LayoutChildren();
        }

        private void LayoutChildren()
        {
            _iconBox.Location = new Point(0, Math.Max(0, (Height - IconBoxSize) / 2));

            int textWidth = Math.Max(20, Width - TextLeftOffset);
            _lblLabel.Width = textWidth;
            _lblValue.Width = textWidth;

            int blockHeight = _lblLabel.Height + 2 + _lblValue.Height;
            int top = Math.Max(0, (Height - blockHeight) / 2);
            _lblLabel.Location = new Point(TextLeftOffset, top);
            _lblValue.Location = new Point(TextLeftOffset, _lblLabel.Bottom + 2);
        }

        private void IconBox_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            var rect = new Rectangle(0, 0, _iconBox.Width - 1, _iconBox.Height - 1);
            using (var path = AppRadius.GetRoundedPath(rect, AppRadius.Medium))
            using (var brush = new SolidBrush(_iconBackground))
            {
                g.FillPath(brush, path);
            }
        }
    }
}