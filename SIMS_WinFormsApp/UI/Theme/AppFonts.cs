using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.UI.Theme
{
    public static class AppFonts
    {
        private const string FamilyName = "Segoe UI";
        public static readonly Font Title = new Font(FamilyName, 18f, FontStyle.Bold);
        public static readonly Font Subtitle = new Font(FamilyName, 13f, FontStyle.Bold);

        public static readonly Font Brand = new Font(FamilyName, 26f, FontStyle.Bold);
        public static readonly Font Body = new Font(FamilyName, 10f, FontStyle.Regular);
        public static readonly Font BodyBold = new Font(FamilyName, 10f, FontStyle.Bold);
        public static readonly Font Feature = new Font(FamilyName, 10.5f, FontStyle.Regular);
        public static readonly Font Small = new Font(FamilyName, 9f, FontStyle.Regular);
        public static readonly Font SmallBold = new Font(FamilyName, 9f, FontStyle.Bold);
        public static readonly Font Button = new Font(FamilyName, 10.5f, FontStyle.Bold);
    }
}
