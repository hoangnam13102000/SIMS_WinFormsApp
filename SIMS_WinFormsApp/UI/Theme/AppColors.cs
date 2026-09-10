using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_WinFormsApp.UI.Theme
{
    public static class AppColors
    {
        // ===================== NỀN TỐI CỐ ĐỊNH (BrandPanel, sidebar login...) =====================
        public static readonly Color DarkTop = Color.FromArgb(3, 45, 95);
        public static readonly Color DarkBottom = Color.FromArgb(24, 120, 180);
        public static readonly Color DarkTextMuted = Color.FromArgb(198, 198, 210);
        public static readonly Color DarkFooter = Color.FromArgb(140, 140, 152);
        public static readonly Color DarkFeatureText = Color.FromArgb(226, 232, 240);

       

        // ===================== NỀN SÁNG (Light) =====================
        public static Color White;
        public static Color BgLight;
        public static Color BgLighter;
        public static Color PageBg;    // nền content area của các Form/Panel chính

        public static Color ContentBackground;

        // ===================== VIỀN (Border) =====================
        public static Color Border;
        public static Color FieldBorder;

        // ===================== CHỮ (Text) =====================
        public static Color TextTitle;
        public static Color TextPrimary;
        public static Color TextSecondary;
        public static Color TextMuted;
        public static Color TextDisabled;
        public static Color TextMutedAlt;   // slate-400 style, dùng cho icon/label phụ
        public static Color IconMuted;      // gray-400 style

        public static Color HeaderText;

        // ===================== MÀU CHỦ ĐẠO (Accent - tím/indigo) =====================
        public static Color Accent;
        public static Color AccentHover;
        public static Color AccentSoft;
        public static Color AccentSelectionBg;
        public static Color AccentBgSoft;

        // ===================== TRẠNG THÁI (Status) =====================
        public static Color Success;
        public static Color SuccessBg;

        public static Color Error;
        public static Color ErrorHover;
        public static Color ErrorBg;
        public static Color RedAlt;

        public static Color Warning;
        public static Color WarningBg;
        public static Color Yellow;
        public static Color Orange;

        public static Color Info;
        public static Color InfoBg;
        public static Color Blue;
        public static Color Green;
        public static Color Teal;
        public static Color OverlayBackdrop; // nền mờ (có alpha) phủ lên UI lúc đang loading

        // ===================== NÚT (Button) =====================
        public static Color DisabledBtn;
        public static Color CancelBg;
        public static Color CancelHover;

        // ===================== BẢNG (Table / DataGridView) =====================
        public static Color TableHeaderBg;
        public static Color TableRowOdd;
        public static Color TableGrid;
        public static Color TableRowText;
        public static Color TableViewAction;
        public static Color TableEditAction;
        public static Color TableDeleteAction;

        public static ThemeMode CurrentMode { get; private set; } = ThemeMode.Light;
        public static AccentColor CurrentAccent { get; private set; } = AccentColor.Blue;

        static AppColors()
        {
            ApplyTheme(ThemeMode.Light);
        }

        public static void ApplyTheme(ThemeMode mode)
        {
            CurrentMode = mode;
            bool dark = mode == ThemeMode.Dark;

            White = dark ? Color.FromArgb(28, 31, 38) : Color.White;
            BgLight = dark ? Color.FromArgb(15, 17, 21) : Color.FromArgb(248, 250, 252);
            BgLighter = dark ? Color.FromArgb(38, 42, 53) : Color.FromArgb(241, 245, 249);
            PageBg = dark ? Color.FromArgb(18, 20, 25) : Color.FromArgb(244, 246, 249);

            Border = dark ? Color.FromArgb(51, 56, 71) : Color.FromArgb(226, 232, 240);
            FieldBorder = dark ? Color.FromArgb(71, 78, 97) : Color.FromArgb(203, 213, 225);

            // Dark mode: tăng độ sáng chữ phụ để tránh bị mờ trên nền tối (đặc biệt POS)
            TextTitle = dark ? Color.FromArgb(248, 250, 252) : Color.FromArgb(15, 23, 42);
            TextPrimary = dark ? Color.FromArgb(236, 242, 250) : Color.FromArgb(30, 41, 59);
            TextSecondary = dark ? Color.FromArgb(200, 210, 225) : Color.FromArgb(71, 85, 105);
            TextMuted = dark ? Color.FromArgb(176, 188, 206) : Color.FromArgb(100, 116, 139);
            TextDisabled = dark ? Color.FromArgb(120, 128, 140) : Color.FromArgb(150, 150, 150);
            TextMutedAlt = dark ? Color.FromArgb(200, 210, 225) : Color.FromArgb(148, 163, 184);
            IconMuted = dark ? Color.FromArgb(190, 198, 212) : Color.FromArgb(156, 163, 175);

            ContentBackground = PageBg;
            HeaderText = TextTitle;

            RecomputeAccentColors(dark);

            Success = dark ? Color.FromArgb(74, 222, 128) : Color.FromArgb(21, 128, 61);
            SuccessBg = dark ? Color.FromArgb(20, 44, 34) : Color.FromArgb(236, 253, 245);

            Error = dark ? Color.FromArgb(248, 113, 113) : Color.FromArgb(220, 38, 38);
            ErrorHover = dark ? Color.FromArgb(220, 38, 38) : Color.FromArgb(185, 28, 28);
            ErrorBg = dark ? Color.FromArgb(56, 24, 24) : Color.FromArgb(254, 242, 242);
            RedAlt = dark ? Color.FromArgb(248, 113, 113) : Color.FromArgb(239, 68, 68);

            Warning = dark ? Color.FromArgb(251, 191, 36) : Color.FromArgb(180, 83, 9);
            WarningBg = dark ? Color.FromArgb(56, 41, 15) : Color.FromArgb(255, 251, 235);
            Yellow = dark ? Color.FromArgb(250, 204, 21) : Color.FromArgb(234, 179, 8);
            Orange = dark ? Color.FromArgb(253, 186, 116) : Color.FromArgb(251, 146, 60);

            Info = dark ? Color.FromArgb(129, 140, 248) : Color.FromArgb(79, 70, 229);
            InfoBg = dark ? Color.FromArgb(30, 27, 60) : Color.FromArgb(238, 242, 255);
            Blue = dark ? Color.FromArgb(96, 165, 250) : Color.FromArgb(59, 130, 246);
            Green = dark ? Color.FromArgb(74, 222, 128) : Color.FromArgb(34, 197, 94);
            Teal = dark ? Color.FromArgb(52, 211, 153) : Color.FromArgb(16, 185, 129);
            OverlayBackdrop = dark ? Color.FromArgb(220, 15, 17, 21) : Color.FromArgb(220, 255, 255, 255);

            DisabledBtn = dark ? Color.FromArgb(71, 76, 92) : Color.FromArgb(165, 165, 180);
            CancelBg = dark ? Color.FromArgb(38, 42, 53) : Color.FromArgb(241, 245, 249);
            CancelHover = dark ? Color.FromArgb(51, 56, 71) : Color.FromArgb(226, 232, 240);

            TableHeaderBg = dark ? Color.FromArgb(15, 20, 32) : Color.FromArgb(30, 41, 59);
            TableRowOdd = dark ? Color.FromArgb(34, 38, 48) : Color.FromArgb(248, 250, 252);
            TableGrid = dark ? Color.FromArgb(44, 48, 60) : Color.FromArgb(241, 245, 249);
            TableRowText = dark ? Color.FromArgb(220, 228, 238) : Color.FromArgb(51, 65, 85);
            TableViewAction = dark ? Color.FromArgb(176, 188, 206) : Color.FromArgb(71, 85, 105);
            TableEditAction = Accent;
            TableDeleteAction = Error;
        }

        public static void ApplyAccent(AccentColor accent)
        {
            CurrentAccent = accent;
            RecomputeAccentColors(CurrentMode == ThemeMode.Dark);
            TableEditAction = Accent;
        }

        private static void RecomputeAccentColors(bool dark)
        {
            Color baseColor = dark ? CurrentAccent.Dark : CurrentAccent.Light;
            Color blendTarget = dark ? Color.FromArgb(15, 17, 21) : Color.White;

            Accent = baseColor;
            AccentHover = Scale(baseColor, dark ? 0.80 : 0.72);
            AccentSoft = Color.FromArgb(dark ? 50 : 40, baseColor.R, baseColor.G, baseColor.B);
            AccentSelectionBg = Blend(baseColor, blendTarget, 0.86);
            AccentBgSoft = Blend(baseColor, blendTarget, dark ? 0.90 : 0.92);
        }

        private static Color Scale(Color c, double factor)
        {
            return Color.FromArgb(
                ClampChannel((int)System.Math.Round(c.R * factor)),
                ClampChannel((int)System.Math.Round(c.G * factor)),
                ClampChannel((int)System.Math.Round(c.B * factor)));
        }

        private static Color Blend(Color c, Color baseColor, double ratio)
        {
            return Color.FromArgb(
                ClampChannel((int)System.Math.Round(c.R * (1 - ratio) + baseColor.R * ratio)),
                ClampChannel((int)System.Math.Round(c.G * (1 - ratio) + baseColor.G * ratio)),
                ClampChannel((int)System.Math.Round(c.B * (1 - ratio) + baseColor.B * ratio)));
        }

        private static int ClampChannel(int v) => System.Math.Max(0, System.Math.Min(255, v));
    }
}
