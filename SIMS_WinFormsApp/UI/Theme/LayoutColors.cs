using System.Drawing;

namespace SIMS_WinFormsApp.UI.Theme
{
    public static class LayoutColors
    {
        // ===== Kích thước đã điều chỉnh =====
        public const int HeaderHeight = 104;        
        public const int FooterHeight = 36;
        public const int SidebarWidth = 260;
        public const int SidebarWidthCollapsed = 72;
        public const int SidebarItemHeight = 54;

        // ... Giữ nguyên toàn bộ màu sắc bên dưới ...
        public static readonly Color SidebarBg = Color.FromArgb(24, 26, 32);
        public static readonly Color SidebarBorder = Color.FromArgb(45, 48, 55);
        public static readonly Color SidebarItemHover = Color.FromArgb(42, 46, 55);
        public static readonly Color SidebarTextInactive = Color.FromArgb(170, 175, 185);
        public static readonly Color SidebarTextActive = Color.White;
        public static readonly Color SidebarTextHover = Color.FromArgb(220, 225, 235);
        public static readonly Color SidebarTextMuted = Color.FromArgb(120, 125, 135);
        public static readonly Color SidebarSectionHover = Color.FromArgb(38, 42, 50);

        public static readonly Color HeaderBg = Color.FromArgb(24, 26, 32);
        public static readonly Color HeaderBorder = Color.FromArgb(45, 48, 55);
        public static readonly Color HeaderText = Color.White;
        public static readonly Color HeaderSubtitle = Color.FromArgb(160, 165, 175);
        public static readonly Color HeaderIconBgHover = Color.FromArgb(45, 48, 55);
        public static readonly Color HeaderAccountHover = Color.FromArgb(45, 48, 55);

        public static readonly Color FooterBg = Color.FromArgb(24, 26, 32);
        public static readonly Color FooterBorder = Color.FromArgb(45, 48, 55);
        public static readonly Color FooterText = Color.FromArgb(160, 165, 175);
        public static readonly Color FooterDotOnline = Color.FromArgb(34, 197, 94);

        public static readonly Color DropdownBg = Color.FromArgb(35, 38, 46);
        public static readonly Color DropdownBorder = Color.FromArgb(55, 58, 66);
        public static readonly Color RowHover = Color.FromArgb(50, 54, 64);

        public static readonly Color Accent = Color.FromArgb(59, 130, 246);
        public static readonly Color RedDot = Color.FromArgb(239, 68, 68);
        public static readonly Color Danger = Color.FromArgb(239, 68, 68);

        public static readonly Color TextWhite = Color.White;
        public static readonly Color TextMuted = Color.FromArgb(160, 165, 175);
    }
}