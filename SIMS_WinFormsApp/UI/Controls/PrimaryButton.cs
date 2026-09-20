using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{
    public class PrimaryButton : BaseButton
    {
        /// <summary>Độ sâu bóng đổ (px) chừa ở đáy nút.</summary>
        private const int ShadowSize = 3;

        public bool IsPrimary { get; set; } = true;
        public Color? CustomAccentColor { get; set; }

        public PrimaryButton()
        {
            ForeColor = Color.White;
            Font = AppFonts.Button;
            Height = 46;
            IconSize = 16;
            Cursor = Cursors.Hand;
            CornerRadius = AppRadius.Medium;
        }

        protected override int ShadowDepth => Enabled ? ShadowSize : 0;

        protected override Color GetBackgroundColor()
        {
            Color accent = CustomAccentColor ?? AppColors.Accent;

            if (!Enabled)
                return AppColors.DisabledBtn;

            if (IsPrimary)
            {
                if (_isPressed)
                    return Scale(accent, 0.85);
                if (_isHover)
                    return Scale(accent, 0.92);
                return accent;
            }

            return _isHover ? AppColors.CancelHover : AppColors.CancelBg;
        }

        protected override Color GetBorderColor()
        {
            if (!IsPrimary)
                return AppColors.Border;

            return Enabled
                ? Scale(CustomAccentColor ?? AppColors.Accent, 0.80)
                : AppColors.Border;
        }

        protected override Color GetTextColor()
        {
            return IsPrimary ? Color.White : AppColors.TextPrimary;
        }

        /// <summary>
        /// Mặt nút: bóng đổ mềm (nhuốm màu accent) → nền gradient dọc → vệt sáng 1px
        /// ở mép trên → viền đậm hơn nền một chút. Đây là phần tạo chiều sâu, thay cho
        /// khối màu phẳng của bản cũ.
        /// </summary>
        protected override void PaintSurface(Graphics g, Rectangle surface, GraphicsPath path)
        {
            if (Enabled && !_isPressed)
                PaintShadow(g, surface);

            Color baseColor = GetBackgroundColor();

            if (Enabled)
            {
                var gradientBounds = new Rectangle(
                    surface.X, surface.Y,
                    Math.Max(1, surface.Width), Math.Max(1, surface.Height + 1));

                using (var brush = new LinearGradientBrush(
                    gradientBounds,
                    Lighten(baseColor, IsPrimary ? 0.16 : 0.05),
                    Scale(baseColor, IsPrimary ? 0.94 : 0.99),
                    LinearGradientMode.Vertical))
                {
                    g.FillPath(brush, path);
                }
            }
            else
            {
                using (var brush = new SolidBrush(baseColor))
                    g.FillPath(brush, path);
            }

            PaintTopHighlight(g, surface, path);

            using (var pen = new Pen(GetBorderColor(), 1f))
                g.DrawPath(pen, path);
        }

        private void PaintShadow(Graphics g, Rectangle surface)
        {
            Color tint = IsPrimary ? (CustomAccentColor ?? AppColors.Accent) : Color.Black;
            int strength = _isHover ? 30 : 20;

            for (int layer = ShadowSize; layer >= 1; layer--)
            {
                int alpha = strength - (ShadowSize - layer) * 6;
                if (alpha <= 0) continue;

                var shadowRect = new Rectangle(
                    surface.X + 1,
                    surface.Y + layer,
                    Math.Max(1, surface.Width - 2),
                    Math.Max(1, surface.Height));

                using (var shadowPath = AppRadius.GetRoundedPath(shadowRect, CornerRadius))
                using (var brush = new SolidBrush(Color.FromArgb(alpha, tint.R, tint.G, tint.B)))
                {
                    g.FillPath(brush, shadowPath);
                }
            }
        }

        private void PaintTopHighlight(Graphics g, Rectangle surface, GraphicsPath path)
        {
            if (!IsPrimary || !Enabled || _isPressed) return;

            var previousClip = g.Clip;
            try
            {
                g.SetClip(path, CombineMode.Intersect);
                using (var pen = new Pen(Color.FromArgb(56, 255, 255, 255), 1f))
                {
                    int y = surface.Top + 1;
                    g.DrawLine(pen,
                        surface.Left + CornerRadius, y,
                        surface.Right - CornerRadius, y);
                }
            }
            finally
            {
                g.Clip = previousClip;
            }
        }

        protected override void DrawText(Graphics g)
        {
            if (string.IsNullOrEmpty(Text)) return;

            var surface = GetSurfaceBounds();
            var iconWidth = Icon.HasValue ? IconSize + 10 : 0;
            var textWidth = TextRenderer.MeasureText(Text, Font).Width;
            var totalWidth = iconWidth + textWidth;
            var startX = Math.Max(0, (Width - totalWidth) / 2);

            if (Icon.HasValue)
            {
                var iconBox = Controls[0] as IconPictureBox;
                if (iconBox != null)
                {
                    iconBox.IconColor = GetTextColor();
                    iconBox.Location = new Point(startX, surface.Top + (surface.Height - IconSize) / 2);
                }
            }

            var textBounds = new Rectangle(
                startX + iconWidth,
                surface.Top,
                Math.Max(0, Width - startX - iconWidth),
                surface.Height);

            TextRenderer.DrawText(
                g,
                Text,
                Font,
                textBounds,
                GetTextColor(),
                TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis);
        }

        private static Color Scale(Color c, double factor)
        {
            int Clamp(int v) => Math.Max(0, Math.Min(255, (int)(v * factor)));
            return Color.FromArgb(Clamp(c.R), Clamp(c.G), Clamp(c.B));
        }

        private static Color Lighten(Color c, double amount)
        {
            int Mix(int channel) => Math.Max(0, Math.Min(255,
                (int)Math.Round(channel + (255 - channel) * amount)));
            return Color.FromArgb(Mix(c.R), Mix(c.G), Mix(c.B));
        }
    }
}