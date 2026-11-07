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
    public partial class InfoBannerPanel : Control
    {
        private const int IconBoxSize = 40;
        private const int PanelPadding = 16;
        private const int TextLeftOffset = IconBoxSize + 12;

        private Panel _iconBox;
        private IconPictureBox _iconGlyph;
        private Label _lblTitle;
        private Label _lblDescription;

        private Color _backgroundColor = AppColors.AccentBgSoft;

        public InfoBannerPanel()
        {
            InitializeComponent();
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

        /// <summary>
        /// Vẽ nền phía sau banner (khớp với control cha) TRƯỚC khi OnPaint tô hình bo góc.
        /// Control này bật OptimizedDoubleBuffer + UserPaint nên nếu để trống hàm này, bitmap
        /// double-buffer chưa được tô và các pixel nằm NGOÀI đường bo góc (4 góc + đường viền
        /// đáy/phải do rect = Width-1/Height-1) sẽ hiện màu ĐEN - chính là viền đen đậm quanh
        /// banner. Cùng cách xử lý với BaseButton.PaintBackdrop.
        /// </summary>
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // Bước 1: tô sẵn 1 màu ĐẶC lấy từ control cha (đảm bảo không còn pixel chưa tô).
            pevent.Graphics.Clear(ResolveOpaqueParentColor());

            // Bước 2: để WinForms vẽ lại nền thật của control cha (BackColor trong suốt nhờ
            // ControlStyles.SupportsTransparentBackColor) để 4 góc khớp đúng nền phía sau.
            base.OnPaintBackground(pevent);
        }

        private Color ResolveOpaqueParentColor()
        {
            for (Control c = Parent; c != null; c = c.Parent)
            {
                if (c.BackColor.A == 255)
                    return c.BackColor;
            }

            return SystemColors.Control;
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

        private void InfoBannerPanel_Resize(object sender, EventArgs e)
        {
            LayoutChildren();
        }
    }
}