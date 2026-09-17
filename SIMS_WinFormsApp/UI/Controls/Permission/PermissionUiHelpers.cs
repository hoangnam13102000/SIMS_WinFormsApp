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
    }
}