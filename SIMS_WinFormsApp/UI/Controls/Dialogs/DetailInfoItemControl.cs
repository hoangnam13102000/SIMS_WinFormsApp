using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{
    [ToolboxItem(false)]
    [DesignerCategory("Code")]
    public class DetailInfoItemControl : Control
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
                AutoEllipsis = true // MỚI: nhãn quá dài (hiếm) sẽ hiện "..." thay vì tràn/bị cắt cứng
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
                // MỚI: bật AutoEllipsis làm lưới an toàn cuối cùng - ví dụ email dài không có
                // khoảng trắng để xuống dòng sẽ hiện "..." thay vì bị cắt cứng không dấu hiệu
                // như trước đây (AutoEllipsis=false).
                AutoEllipsis = true
            };
            Controls.Add(_lblValue);

            LayoutChildren();
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