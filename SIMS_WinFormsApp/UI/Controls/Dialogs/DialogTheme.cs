using System.Drawing;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{

    internal static class DialogTheme
    {
        public const float BorderWidth = 2f;

        public static Color SurfaceColor =>
            ThemeManager.Instance.IsDark ? AppColors.BgLighter : AppColors.White;
        public static Color BorderColor =>
            ThemeManager.Instance.IsDark
                ? Color.FromArgb(120, 130, 158)
                : Color.FromArgb(180, 190, 206);
    }
}