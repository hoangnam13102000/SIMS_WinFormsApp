using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{
    [ToolboxItem(false)]
    [DesignerCategory("Code")]
    public class DetailAvatarPanel : Panel
    {
        private const int AvatarSize = 96;

        private const string HeightSample = "Ẵợgqy";
        private static readonly int NameLineHeight = MeasureLineHeight(AppFonts.Subtitle);
        private static readonly int SubtitleTextHeight = MeasureLineHeight(AppFonts.Small);

        private static int MeasureLineHeight(Font font) =>
            TextRenderer.MeasureText(HeightSample, font, new Size(int.MaxValue, int.MaxValue), TextFormatFlags.NoPadding).Height + 4;

        private const TextFormatFlags NameWrapMeasureFlags =
            TextFormatFlags.WordBreak | TextFormatFlags.NoPadding | TextFormatFlags.HorizontalCenter | TextFormatFlags.Top;
        private const TextFormatFlags NameSingleLineMeasureFlags =
            TextFormatFlags.NoPadding | TextFormatFlags.SingleLine;

        private readonly Panel _avatarCircle;
        private readonly Label _lblInitial;
        private readonly Label _lblName;
        private readonly Label _lblSubtitle;

        private Color _avatarColor = AppColors.Accent;

        public Color AvatarColor
        {
            get => _avatarColor;
            set { _avatarColor = value; _avatarCircle.Invalidate(); }
        }

        public string Initial
        {
            get => _lblInitial.Text;
            set => _lblInitial.Text = value ?? string.Empty;
        }

        public string NameText
        {
            get => _lblName.Text;
            set
            {
                _lblName.Text = value ?? string.Empty;
                LayoutChildren();
            }
        }

        public string SubtitleText
        {
            get => _lblSubtitle.Text;
            set => _lblSubtitle.Text = value ?? string.Empty;
        }

        public DetailAvatarPanel()
        {
            BackColor = Color.Transparent;
            Size = new Size(200, 260);

            _avatarCircle = new Panel
            {
                Size = new Size(AvatarSize, AvatarSize),
                BackColor = Color.Transparent
            };
            _avatarCircle.Paint += AvatarCircle_Paint;
            Controls.Add(_avatarCircle);

            _lblInitial = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                Text = string.Empty,
                Font = new Font(AppFonts.Title.FontFamily, 28f, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleCenter
            };
            _avatarCircle.Controls.Add(_lblInitial);

            _lblName = new Label
            {
                AutoSize = false,
                Text = string.Empty,
                Font = AppFonts.Subtitle,
                ForeColor = AppColors.TextTitle,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.TopCenter,
                Height = NameLineHeight
            };
            Controls.Add(_lblName);

            _lblSubtitle = new Label
            {
                AutoSize = false,
                Text = string.Empty,
                Font = AppFonts.Small,
                ForeColor = AppColors.TextMuted,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.TopCenter,
                Height = SubtitleTextHeight
            };
            Controls.Add(_lblSubtitle);

            LayoutChildren();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            // Gán Size trong constructor sẽ bắn OnResize ngay lập tức, TRƯỚC khi các control
            // con (_avatarCircle, _lblName...) kịp khởi tạo -> phải chặn ở đây để tránh
            // NullReferenceException; constructor sẽ tự gọi LayoutChildren() lại sau khi mọi
            // control con đã sẵn sàng.
            if (_avatarCircle == null) return;
            LayoutChildren();
        }

        private void LayoutChildren()
        {
            if (_avatarCircle == null) return; // NameText có thể set trước khi ctor xong (phòng hờ)

            int centerX = Width / 2;

            _avatarCircle.Location = new Point(centerX - AvatarSize / 2, 8);

            _lblName.Width = Width;
            _lblName.Height = MeasureNameHeight(Width);
            _lblName.Location = new Point(0, _avatarCircle.Bottom + 12);

            _lblSubtitle.Width = Width;
            _lblSubtitle.Location = new Point(0, _lblName.Bottom + 2);
        }

        private int MeasureNameHeight(int maxWidth)
        {
            string text = _lblName.Text;
            if (string.IsNullOrEmpty(text) || maxWidth <= 0) return NameLineHeight;

            int singleLineWidth = TextRenderer.MeasureText(text, _lblName.Font, Size.Empty, NameSingleLineMeasureFlags).Width;
            if (singleLineWidth <= maxWidth) return NameLineHeight;

            var wrapped = TextRenderer.MeasureText(text, _lblName.Font, new Size(maxWidth, int.MaxValue), NameWrapMeasureFlags);

            int twoLineCap = NameLineHeight * 2 - 4; // 2 dòng khít hơn tổng 2 lần dòng đơn (bớt phần giãn dòng dư)
            return Math.Max(NameLineHeight, Math.Min(wrapped.Height + 4, twoLineCap));
        }

        private void AvatarCircle_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (var brush = new SolidBrush(_avatarColor))
            {
                g.FillEllipse(brush, 0, 0, AvatarSize - 1, AvatarSize - 1);
            }
        }
    }
}