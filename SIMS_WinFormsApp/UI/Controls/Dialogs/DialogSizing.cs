using System;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.UI.Controls
{
    /// <summary>Tính độ rộng popup theo màn hình đang hiển thị: dùng độ rộng mong muốn, chỉ thu
    /// lại khi màn hình không đủ chỗ.</summary>
    public static class DialogSizing
    {
        private const int DefaultScreenEdgeMargin = 48;

        public static int FitWidthToScreen(IWin32Window owner, int preferredWidth, int screenEdgeMargin = DefaultScreenEdgeMargin)
        {
            Screen screen = owner is Control control
                ? Screen.FromControl(control)
                : Screen.PrimaryScreen;
            return Math.Min(preferredWidth, screen.WorkingArea.Width - screenEdgeMargin);
        }
    }
}