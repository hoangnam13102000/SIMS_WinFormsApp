using System.Drawing;

namespace SIMS_WinFormsApp.UI.Theme
{
    public static class DropdownPalette
    {
        /// <summary>Tương phản tối thiểu cho chữ chính (WCAG AA – normal text).</summary>
        private const double MinBodyContrast = 4.5d;

        /// <summary>Tương phản tối thiểu cho chữ phụ/nhấn/icon lớn (WCAG AA – large text &amp; UI).</summary>
        private const double MinSecondaryContrast = 3.0d;

        /// <summary>Nền của popup.</summary>
        public static Color Surface => LayoutColors.DropdownBg;

        /// <summary>Viền popup và đường kẻ phân cách.</summary>
        public static Color Border => LayoutColors.DropdownBorder;

        /// <summary>Nền khi rê chuột qua một dòng.</summary>
        public static Color RowHover => LayoutColors.RowHover;

        /// <summary>True nếu nền popup là nền tối (khi đó chữ phải sáng).</summary>
        public static bool IsDarkSurface => RelativeLuminance(Surface) < 0.5d;

        /// <summary>Màu chữ đối lập của nền popup – dùng làm phương án cuối.</summary>
        public static Color OnSurface => IsDarkSurface ? LayoutColors.TextWhite : LayoutColors.DropdownText;

        /// <summary>Chữ chính (tên mục, tiêu đề thông báo...).</summary>
        public static Color Text => Readable(LayoutColors.DropdownText, MinBodyContrast);

        /// <summary>Chữ phụ (email, thời gian, empty-state...).</summary>
        public static Color TextMuted => Readable(LayoutColors.TextMuted, MinSecondaryContrast);

        /// <summary>Icon đi kèm mục menu – dùng chung tông với chữ chính.</summary>
        public static Color Icon => Text;

        /// <summary>Màu nhấn (vai trò, chấm chưa đọc...).</summary>
        public static Color Accent => Readable(LayoutColors.Accent, MinSecondaryContrast);

        /// <summary>Màu cho hành động nguy hiểm (Đăng xuất, Xoá...).</summary>
        public static Color Danger => Readable(LayoutColors.Danger, MinSecondaryContrast);

        /// <summary>Nền hover mờ cho hành động nguy hiểm.</summary>
        public static Color DangerHover => Color.FromArgb(40, LayoutColors.Danger);

        /// <summary>
        /// Trả về <paramref name="preferred"/> nếu nó đủ tương phản với nền popup;
        /// nếu không, pha dần về <see cref="OnSurface"/> cho tới khi đạt ngưỡng.
        /// </summary>
        public static Color Readable(Color preferred, double minContrast)
        {
            Color surface = Surface;
            Color fallback = OnSurface;

            if (ContrastRatio(preferred, surface) >= minContrast)
                return preferred;

            for (int step = 1; step <= 9; step++)
            {
                Color candidate = Blend(preferred, fallback, step / 10d);
                if (ContrastRatio(candidate, surface) >= minContrast)
                    return candidate;
            }

            return fallback;
        }

        /// <summary>Quá tải tiện dụng: dùng ngưỡng của chữ chính.</summary>
        public static Color Readable(Color preferred) => Readable(preferred, MinBodyContrast);

        private static double ContrastRatio(Color a, Color b)
        {
            double la = RelativeLuminance(a);
            double lb = RelativeLuminance(b);
            double lighter = la > lb ? la : lb;
            double darker = la > lb ? lb : la;
            return (lighter + 0.05d) / (darker + 0.05d);
        }

        private static double RelativeLuminance(Color c)
        {
            double r = LinearizeChannel(c.R / 255d);
            double g = LinearizeChannel(c.G / 255d);
            double b = LinearizeChannel(c.B / 255d);
            return 0.2126d * r + 0.7152d * g + 0.0722d * b;
        }

        private static double LinearizeChannel(double channel)
        {
            return channel <= 0.03928d
                ? channel / 12.92d
                : System.Math.Pow((channel + 0.055d) / 1.055d, 2.4d);
        }

        private static Color Blend(Color from, Color to, double ratio)
        {
            return Color.FromArgb(
                ClampChannel((int)System.Math.Round(from.R * (1 - ratio) + to.R * ratio)),
                ClampChannel((int)System.Math.Round(from.G * (1 - ratio) + to.G * ratio)),
                ClampChannel((int)System.Math.Round(from.B * (1 - ratio) + to.B * ratio)));
        }

        private static int ClampChannel(int v) => System.Math.Max(0, System.Math.Min(255, v));
    }
}