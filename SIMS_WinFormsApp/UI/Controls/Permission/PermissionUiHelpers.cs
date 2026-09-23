using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls.Permission
{
    internal static class PermissionUiHelpers
    {
        public static Color GetEffectiveBackColor(Control control)
        {
            var current = control?.Parent;
            while (current != null)
            {
                if (current.BackColor.A == 255) return current.BackColor;
                current = current.Parent;
            }
            return AppColors.White;
        }

        public static int MeasureLineHeight(Font font)
        {
            const string tallAndDeepSample = "Ặẩộ";
            var size = TextRenderer.MeasureText(
                tallAndDeepSample,
                font,
                new Size(int.MaxValue, int.MaxValue),
                TextFormatFlags.NoPadding | TextFormatFlags.SingleLine);
            return size.Height;
        }

        public static int MeasureTextWidth(string text, Font font)
        {
            const int SafetyBuffer = 4;
            var size = TextRenderer.MeasureText(
                text ?? string.Empty,
                font,
                new Size(int.MaxValue, int.MaxValue),
                TextFormatFlags.SingleLine);
            return size.Width + SafetyBuffer;
        }
    }
}