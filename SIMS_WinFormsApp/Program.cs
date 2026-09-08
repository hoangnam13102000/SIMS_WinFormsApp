using SIMS_WinFormsApp.Forms.Auth;
using SIMS_WinFormsApp.Forms.SystemMgmt;
using SIMS_WinFormsApp.Services.Session;
using SIMS_WinFormsApp.UI.I18n;
using SIMS_WinFormsApp.UI.Theme;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIMS_WinFormsApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ThemeManager.Instance.ApplyStartupTheme();
            var languageManager = LanguageManager.Instance;

            using (var loginForm = new frmLogin())
            {
                if (loginForm.ShowDialog() == DialogResult.OK && UserSession.Instance.IsLoggedIn)
                {
                    Application.Run(new frmMain());
                }
            }
        }
    }
}