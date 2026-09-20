using System.Drawing;

namespace SIMS_WinFormsApp.UI.Theme
{
    public static class LayoutColors
    {
        public const int HeaderHeight = 96;
        public const int FooterHeight = 36;
        public const int SidebarWidth = 260;
        public const int SidebarWidthCollapsed = 60;
        public const int SidebarItemHeight = 52;

        // ===== Màu theo chuẩn Java =====
        public static readonly Color SidebarBg = Color.FromArgb(24, 33, 48);
        public static readonly Color SidebarBorder = Color.FromArgb(24, 33, 48);
        public static readonly Color SidebarItemHover = Color.FromArgb(45, 55, 72);
        public static readonly Color SidebarTextInactive = Color.FromArgb(177, 184, 194);
        public static readonly Color SidebarTextActive = Color.White;
        public static readonly Color SidebarTextHover = Color.White;
        public static readonly Color SidebarTextMuted = Color.FromArgb(100, 116, 139);
        public static readonly Color SidebarSectionHover = Color.FromArgb(36, 45, 58);

        public static readonly Color HeaderBg = Color.FromArgb(15, 23, 42);
        public static readonly Color HeaderBorder = Color.FromArgb(30, 41, 59);
        public static readonly Color HeaderDivider = Color.FromArgb(51, 65, 85);
        public static readonly Color HeaderText = Color.White;
        public static readonly Color HeaderSubtitle = Color.FromArgb(148, 163, 184);
        public static readonly Color HeaderIconBgHover = Color.FromArgb(30, 41, 59);
        public static readonly Color HeaderAccountHover = Color.FromArgb(30, 41, 59);

        public static readonly Color FooterBg = Color.FromArgb(255, 255, 255);
        public static readonly Color FooterBorder = Color.FromArgb(226, 232, 240);
        public static readonly Color FooterText = Color.FromArgb(100, 116, 139);
        public static readonly Color FooterDotOnline = Color.FromArgb(34, 197, 94);

        public static readonly Color DropdownBg = Color.FromArgb(255, 255, 255);
        public static readonly Color DropdownBorder = Color.FromArgb(226, 232, 240);
        public static readonly Color DropdownText = Color.FromArgb(15, 23, 42);
        public static readonly Color RowHover = Color.FromArgb(244, 246, 249);

        public static readonly Color Accent = Color.FromArgb(124, 58, 237);
        public static readonly Color AccentSoft = Color.FromArgb(238, 233, 254);
        public static readonly Color RedDot = Color.FromArgb(239, 68, 68);
        public static readonly Color Danger = Color.FromArgb(239, 68, 68);

        public static readonly Color TextWhite = Color.White;
        public static readonly Color TextMuted = Color.FromArgb(100, 116, 139);
    }
}