using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.UI.Theme
{
    namespace SIMS_WinFormsApp.UI.Theme
    {
        /// <summary>
        /// Khoảng cách (padding/margin) dùng chung toàn app - tương ứng
        /// com.theme.AppSpacing bên Java.
        /// </summary>
        public static class AppSpacing
        {
            public const int Xs = 4;
            public const int Sm = 8;
            public const int Md = 12;
            public const int Lg = 16;
            public const int Xl = 24;
            public const int Xxl = 32;

            public static Padding XsPadding() => new Padding(Xs);
            public static Padding SmPadding() => new Padding(Sm);
            public static Padding MdPadding() => new Padding(Md);
            public static Padding LgPadding() => new Padding(Lg);
            public static Padding XlPadding() => new Padding(Xl);
            public static Padding XxlPadding() => new Padding(Xxl);

            public static Padding Custom(int left, int top, int right, int bottom) => new Padding(left, top, right, bottom);
        }
    }
}