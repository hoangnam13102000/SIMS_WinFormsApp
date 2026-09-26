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
    public sealed class CardPanel : Panel
    {
        public Color FillColor { get; set; } = AppColors.BgLighter;

        public CardPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using (var path = AppRadius.GetRoundedPath(rect, AppRadius.Large))
            using (var brush = new SolidBrush(FillColor))
                g.FillPath(brush, path);
        }
    }

    [ToolboxItem(false)]
    [DesignerCategory("Code")]
    public sealed class Pill : Control
    {
        
        private const int HorizontalPadding = 28;
        private const int MinWidth = 60;

        private Color _bg = AppColors.AccentBgSoft;
        private Color _fg = AppColors.Accent;

        public Pill()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);
            Font = AppFonts.SmallBold;
            BackColor = Color.Transparent;
            Height = 34;
        }

        public void SetColors(Color background, Color foreground)
        {
            _bg = background;
            _fg = foreground;
            Invalidate();
        }

        public void SetText(string text)
        {
            Text = text ?? string.Empty;
            int textWidth = TextRenderer.MeasureText(
                Text, Font, new Size(int.MaxValue, int.MaxValue), TextFormatFlags.NoPadding).Width;
            Width = Math.Max(MinWidth, textWidth + HorizontalPadding);
        }

        // Cùng lý do đã giải thích ở CardPanel.OnPaintBackground(): gọi base để cơ chế trong suốt
        // vẽ đúng nền phía sau vào phần bo tròn 2 đầu Pill, tránh 2 góc ngoài cung bo tròn bị bỏ
        // trắng/đen do back-buffer chưa từng được tô.
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using (var path = AppRadius.GetRoundedPath(rect, Height / 2))
            using (var brush = new SolidBrush(_bg))
                g.FillPath(brush, path);

            TextRenderer.DrawText(g, Text, Font, ClientRectangle, _fg,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
    }

    /// <summary>Ô vuông bo góc chứa 1 icon - dùng làm điểm nhấn cho dòng "Tồn kho" trong popup
    /// chi tiết.</summary>
    [ToolboxItem(false)]
    [DesignerCategory("Code")]
    public sealed class IconBadge : Panel
    {
        public IconBadge(IconChar icon)
        {
            Size = new Size(40, 40);
            Location = new Point(0, 4);
            BackColor = Color.Transparent;
            Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = new Rectangle(0, 0, Width - 1, Height - 1);
                using (var path = AppRadius.GetRoundedPath(rect, AppRadius.Medium))
                using (var brush = new SolidBrush(AppColors.AccentBgSoft))
                    e.Graphics.FillPath(brush, path);
            };
            Controls.Add(new IconPictureBox
            {
                IconChar = icon,
                IconColor = AppColors.Accent,
                IconSize = 18,
                Size = new Size(18, 18),
                Location = new Point(11, 11),
                BackColor = Color.Transparent
            });
        }
    }

    /// <summary>1 mục thông tin trong lưới xem chi tiết: ô icon + nhãn nhỏ + giá trị in đậm,
    /// tương ứng với từng ô "Danh mục / Mã sản phẩm / Thương hiệu..." trong popup chi tiết sản
    /// phẩm.</summary>
    [ToolboxItem(false)]
    [DesignerCategory("Code")]
    public sealed class InfoItem : Panel
    {
        private const int TextLeft = 52;
        private const int TextRightPadding = 6;
        private readonly Label _lblValue;
        private readonly Label _lblLabel;
        private readonly string _labelText;
        private bool _isLayingOut;

        public InfoItem(IconChar icon, string label)
        {
            Dock = DockStyle.Top;
            Height = 64;
            BackColor = Color.Transparent;
            _labelText = label ?? string.Empty;

            Controls.Add(new IconBadge(icon));

            _lblLabel = new Label
            {
                AutoSize = false,
                Text = _labelText,
                Font = AppFonts.Small,
                ForeColor = AppColors.TextMuted,
                BackColor = Color.Transparent,
                UseMnemonic = false,
                Padding = new Padding(0, 4, 0, 4)
            };
            _lblValue = new Label
            {
                AutoSize = false,
                Text = "-",
                Font = AppFonts.BodyBold,
                ForeColor = AppColors.TextTitle,
                BackColor = Color.Transparent,
                UseMnemonic = false,
                Padding = new Padding(0, 4, 0, 4)
            };

            Controls.Add(_lblValue);
            Controls.Add(_lblLabel);
            Resize += (s, e) => LayoutText();
            LayoutText();
        }

        public string Value
        {
            get => _lblValue.Text;
            set
            {
                _lblValue.Text = string.IsNullOrWhiteSpace(value) ? "-" : value;
                LayoutText();
            }
        }

        private void LayoutText()
        {
            if (_isLayingOut || _lblLabel == null || _lblValue == null) return;

            _isLayingOut = true;
            try
            {
                int textWidth = Math.Max(40, ClientSize.Width - TextLeft - TextRightPadding);
                _lblLabel.SetBounds(TextLeft, 0, textWidth, MeasureTextHeight(_labelText, _lblLabel.Font, textWidth));
                _lblValue.SetBounds(TextLeft, _lblLabel.Bottom, textWidth, MeasureTextHeight(_lblValue.Text, _lblValue.Font, textWidth));

                int desiredHeight = Math.Max(48, _lblValue.Bottom + 2);
                if (Height != desiredHeight) Height = desiredHeight;
            }
            finally
            {
                _isLayingOut = false;
            }
        }

        private static int MeasureTextHeight(string text, Font font, int width)
        {
            Size measured = TextRenderer.MeasureText(
                text ?? string.Empty,
                font,
                new Size(width, int.MaxValue),
                TextFormatFlags.WordBreak | TextFormatFlags.NoPrefix | TextFormatFlags.NoPadding);
            return measured.Height + 12;
        }
    }
}