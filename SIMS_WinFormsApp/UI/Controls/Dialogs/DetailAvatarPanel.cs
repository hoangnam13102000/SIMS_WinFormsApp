using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{
    /// <summary>
    /// Cột bên trái của popup chi tiết: vòng tròn avatar (chữ cái đại diện), tên chính và
    /// dòng phụ (username/mã số...). Không phụ thuộc vào bất kỳ domain model nào (User,
    /// Customer...) nên dùng lại được cho mọi loại popup chi tiết kế thừa từ
    /// <see cref="BaseDetailDialogForm"/>.
    /// </summary>
    [ToolboxItem(false)]
    [DesignerCategory("Code")]
    public class DetailAvatarPanel : Panel
    {
        private const int AvatarSize = 96;

        // Đo chiều cao dòng thật của font (dấu tiếng Việt + chữ có đuôi xuống) thay vì hardcode
        // pixel cố định - cùng kỹ thuật StatCard đang dùng, tránh cắt chữ như trước.
        private const string HeightSample = "Ẵợgqy";
        private static readonly int NameTextHeight = MeasureLineHeight(AppFonts.Subtitle);
        private static readonly int SubtitleTextHeight = MeasureLineHeight(AppFonts.Small);

        private static int MeasureLineHeight(Font font) =>
            TextRenderer.MeasureText(HeightSample, font, new Size(int.MaxValue, int.MaxValue), TextFormatFlags.NoPadding).Height + 4;

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
            set => _lblName.Text = value ?? string.Empty;
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
                Height = NameTextHeight
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
            int centerX = Width / 2;

            _avatarCircle.Location = new Point(centerX - AvatarSize / 2, 8);

            _lblName.Width = Width;
            _lblName.Location = new Point(0, _avatarCircle.Bottom + 12);

            _lblSubtitle.Width = Width;
            _lblSubtitle.Location = new Point(0, _lblName.Bottom + 2);
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