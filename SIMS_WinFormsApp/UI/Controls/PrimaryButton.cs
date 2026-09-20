using System;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{
    public class PrimaryButton : BaseButton
    {
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
                    return Scale(accent, 0.72);
                return accent;
            }

            return _isHover ? AppColors.CancelHover : AppColors.CancelBg;
        }

        protected override Color GetBorderColor()
        {
            if (!IsPrimary)
                return AppColors.Border;

            return Enabled ? (CustomAccentColor ?? AppColors.Accent) : AppColors.Border;
        }

        protected override Color GetTextColor()
        {
            return IsPrimary ? Color.White : AppColors.TextPrimary;
        }

        protected override void DrawText(Graphics g)
        {
            if (string.IsNullOrEmpty(Text)) return;

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
                    iconBox.Location = new Point(startX, (Height - IconSize) / 2);
                }
            }

            var textBounds = new Rectangle(
                startX + iconWidth,
                0,
                Math.Max(0, Width - startX - iconWidth),
                Height);

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
    }
}
