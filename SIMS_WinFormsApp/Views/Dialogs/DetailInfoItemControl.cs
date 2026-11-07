using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Views.Dialogs
{
    [DesignerCategory("Code")]
    public partial class DetailInfoItemControl : Control
    {
        private const int IconBoxSize = 40;
        private const int TextLeftOffset = IconBoxSize + 12;

        private const string HeightSample = "Ẵợgqy";
        private static readonly int LabelTextHeight = MeasureLineHeight(AppFonts.Small);
        private static readonly int ValueTextHeight = MeasureLineHeight(AppFonts.BodyBold);

        private static int MeasureLineHeight(Font font) =>
            TextRenderer.MeasureText(HeightSample, font, new Size(int.MaxValue, int.MaxValue), TextFormatFlags.NoPadding).Height + 8;

        // MỚI: cờ đo khi cho phép xuống dòng theo từ, dùng để tính chiều cao THẬT của giá trị
        // khi nó dài hơn 1 dòng (vd "Nhân viên bán hàng", "Đang hoạt động") - trước đây control
        // luôn ép _lblValue chỉ cao đúng 1 dòng nên dòng thứ 2 bị cắt mất phần dưới.
        private const TextFormatFlags WrapMeasureFlags =
            TextFormatFlags.WordBreak | TextFormatFlags.NoPadding | TextFormatFlags.Left | TextFormatFlags.Top;
        private const TextFormatFlags SingleLineMeasureFlags =
            TextFormatFlags.NoPadding | TextFormatFlags.SingleLine;

        private Panel _iconBox;
        private IconPictureBox _iconGlyph;
        private Label _lblLabel;
        private Label _lblValue;

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
            set
            {
                _lblValue.Text = value ?? string.Empty;
                // MỚI: nội dung đổi -> chiều cao 1-hay-2-dòng cần tính lại ngay, không chỉ chờ
                // OnResize (giá trị có thể được set SAU khi control đã có Width ổn định, như
                // Render() trong frmUserAccountDetail đang làm).
                LayoutChildren();
            }
        }

        public DetailInfoItemControl()
        {
            InitializeComponent();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (_iconBox == null) return;
            LayoutChildren();
        }

        private void LayoutChildren()
        {
            if (_iconBox == null) return; // phòng hờ gọi từ ValueText setter trước khi ctor xong

            _iconBox.Location = new Point(0, Math.Max(0, (Height - IconBoxSize) / 2));

            int textWidth = Math.Max(20, Width - TextLeftOffset);
            _lblLabel.Width = textWidth;
            _lblValue.Width = textWidth;

            // MỚI: co giãn _lblValue theo nội dung thật (tối đa 2 dòng) thay vì luôn ép 1 dòng -
            // đây là nguyên nhân chính khiến "Nhân viên bán hàng", "Đang hoạt động", "Chưa cập
            // nhật"... bị cắt mất phần dưới của dòng thứ 2.
            _lblValue.Height = MeasureValueHeight(textWidth);

            int blockHeight = _lblLabel.Height + 2 + _lblValue.Height;
            int top = Math.Max(0, (Height - blockHeight) / 2);
            _lblLabel.Location = new Point(TextLeftOffset, top);
            _lblValue.Location = new Point(TextLeftOffset, _lblLabel.Bottom + 2);
        }

        /// <summary>Đo chiều cao thật _lblValue cần để hiển thị ĐẦY ĐỦ nội dung, cho phép tối đa
        /// 2 dòng (đủ cho hầu hết giá trị trong popup chi tiết). Giữ nguyên chiều cao 1 dòng khi
        /// nội dung đã vừa (không phóng to control 1 cách không cần thiết cho các giá trị ngắn
        /// như "staff02", "—"...). Nếu nội dung dài hơn cả 2 dòng (hiếm), phần dư sẽ được
        /// AutoEllipsis của Label xử lý thay vì làm control cao vô hạn.</summary>
        private int MeasureValueHeight(int maxWidth)
        {
            string text = _lblValue.Text;
            if (string.IsNullOrEmpty(text) || maxWidth <= 0) return ValueTextHeight;

            int singleLineWidth = TextRenderer.MeasureText(text, _lblValue.Font, Size.Empty, SingleLineMeasureFlags).Width;
            if (singleLineWidth <= maxWidth) return ValueTextHeight;

            var wrapped = TextRenderer.MeasureText(text, _lblValue.Font,
                new Size(maxWidth, int.MaxValue), WrapMeasureFlags);

            int twoLineCap = ValueTextHeight * 2 - 6; // 2 dòng khít hơn tổng 2 lần dòng đơn (bớt phần giãn dòng dư)
            return Math.Max(ValueTextHeight, Math.Min(wrapped.Height + 6, twoLineCap));
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