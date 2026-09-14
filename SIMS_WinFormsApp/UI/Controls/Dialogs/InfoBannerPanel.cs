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
    public class InfoBannerPanel : Control
    {
        private const int IconBoxSize = 40;
        private const int PanelPadding = 16;
        private const int TextLeftOffset = IconBoxSize + 12;

        private readonly Panel _iconBox;
        private readonly IconPictureBox _iconGlyph;
        private readonly Label _lblTitle;
        private readonly Label _lblDescription;

        private Color _backgroundColor = AppColors.AccentBgSoft;

        public InfoBannerPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);

            Dock = DockStyle.Top;
            BackColor = Color.Transparent;
            Height = 96;

            _iconBox = new Panel
            {
                Size = new Size(IconBoxSize, IconBoxSize),
                Location = new Point(PanelPadding, PanelPadding),
                BackColor = Color.Transparent
            };
            _iconBox.Paint += IconBox_Paint;
            Controls.Add(_iconBox);

            _iconGlyph = new IconPictureBox
            {
                IconChar = IconChar.UserGear,
                IconColor = AppColors.Accent,
                IconSize = 18,
                Size = new Size(20, 20),
                Location = new Point((IconBoxSize - 20) / 2, (IconBoxSize - 20) / 2),
                BackColor = Color.Transparent
            };
            _iconBox.Controls.Add(_iconGlyph);

            _lblTitle = new Label
            {
                AutoSize = false,
                Font = AppFonts.BodyBold,
                ForeColor = AppColors.TextTitle,
                BackColor = Color.Transparent,
                Location = new Point(PanelPadding + TextLeftOffset, PanelPadding - 2)
            };
            Controls.Add(_lblTitle);

            _lblDescription = new Label
            {
                AutoSize = false,
                Font = AppFonts.Small,
                ForeColor = AppColors.TextSecondary,
                BackColor = Color.Transparent,
                Location = new Point(PanelPadding + TextLeftOffset, PanelPadding + 18)
            };
            Controls.Add(_lblDescription);

            Resize += (s, e) => LayoutChildren();
        }

        public IconChar Icon
        {
            get => _iconGlyph.IconChar;
            set => _iconGlyph.IconChar = value;
        }

        public Color AccentColor
        {
            get => _iconGlyph.IconColor;
            set { _iconGlyph.IconColor = value; Invalidate(); }
        }

        public Color BannerBackground
        {
            get => _backgroundColor;
            set { _backgroundColor = value; Invalidate(); }
        }

        public string TitleText
        {
            get => _lblTitle.Text;
            set { _lblTitle.Text = value ?? string.Empty; LayoutChildren(); }
        }

        public string DescriptionText
        {
            get => _lblDescription.Text;
            set { _lblDescription.Text = value ?? string.Empty; LayoutChildren(); }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // Nền do OnPaint tự vẽ bo góc - bỏ qua nền mặc định để tránh vệt hình chữ nhật lộ ra
            // ở 4 góc (cùng lý do OnPaintBackground rỗng ở PillTabButton/PrimaryButton).
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            var rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using (var path = AppRadius.GetRoundedPath(rect, AppRadius.Medium))
            using (var brush = new SolidBrush(_backgroundColor))
            {
                g.FillPath(brush, path);
            }

            base.OnPaint(e);
        }

        private void IconBox_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            var rect = new Rectangle(0, 0, _iconBox.Width - 1, _iconBox.Height - 1);
            using (var path = AppRadius.GetRoundedPath(rect, AppRadius.Medium))
            using (var brush = new SolidBrush(AppColors.White))
            {
                g.FillPath(brush, path);
            }
        }

        private void LayoutChildren()
        {
            if (_lblTitle == null || _lblDescription == null) return;

            int textWidth = Math.Max(20, Width - PanelPadding - TextLeftOffset - PanelPadding);
            _lblTitle.Width = textWidth;
            _lblDescription.Width = textWidth;

            int titleHeight = TextRenderer.MeasureText(
                _lblTitle.Text, _lblTitle.Font, new Size(textWidth, int.MaxValue),
                TextFormatFlags.WordBreak | TextFormatFlags.NoPadding).Height;
            _lblTitle.Height = Math.Max(16, titleHeight);
            _lblTitle.Location = new Point(_lblTitle.Left, PanelPadding - 2);

            int descHeight = TextRenderer.MeasureText(
                _lblDescription.Text, _lblDescription.Font, new Size(textWidth, int.MaxValue),
                TextFormatFlags.WordBreak | TextFormatFlags.NoPadding).Height;
            _lblDescription.Height = Math.Max(14, descHeight);
            _lblDescription.Location = new Point(_lblDescription.Left, _lblTitle.Bottom + 2);

            int contentBottom = Math.Max(_iconBox.Bottom, _lblDescription.Bottom);
            Height = contentBottom + PanelPadding;
        }
    }
}