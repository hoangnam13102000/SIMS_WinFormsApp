using System;
using System.Drawing;

namespace SIMS_WinFormsApp.UI.Theme
{
    public static class AppFonts
    {
        private const string FontFamilyName = "Segoe UI";

        private static Font CreateFont(float size, FontStyle style)
        {
            return new Font(FontFamilyName, size, style, GraphicsUnit.Point);
        }

        public static readonly Font Brand = CreateFont(30f, FontStyle.Bold);
        public static readonly Font Title = CreateFont(28f, FontStyle.Bold);
        public static readonly Font HeadingLg = CreateFont(20f, FontStyle.Bold);
        public static readonly Font HeadingMd = CreateFont(16f, FontStyle.Bold);
        public static readonly Font DialogTitle = CreateFont(17f, FontStyle.Bold);
        public static readonly Font Body = CreateFont(13f, FontStyle.Regular);
        public static readonly Font BodyBold = CreateFont(13f, FontStyle.Bold);
        public static readonly Font Input = CreateFont(14f, FontStyle.Regular);
        public static readonly Font Small = CreateFont(12f, FontStyle.Regular);
        public static readonly Font SmallBold = CreateFont(12f, FontStyle.Bold);
        public static readonly Font Button = CreateFont(14f, FontStyle.Bold);
        public static readonly Font Footer = CreateFont(11f, FontStyle.Regular);

        // Backward-compatible aliases used by the current codebase.
        public static readonly Font Subtitle = HeadingMd;
        public static readonly Font Feature = Small;
    }
}