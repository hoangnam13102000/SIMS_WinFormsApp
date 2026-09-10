using SIMS_WinFormsApp.Forms.Auth;
using SIMS_WinFormsApp.Forms.SystemMgmt;
using SIMS_WinFormsApp.Services.Session;
using SIMS_WinFormsApp.UI.I18n;
using SIMS_WinFormsApp.UI.Theme;
using System;
using System.Windows.Forms;

namespace SIMS_WinFormsApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // DPI-awareness cho .NET Framework khai báo qua App.config
            // (xem thẻ System.Windows.Forms.ApplicationConfigurationSection),
            // không dùng Application.SetHighDpiMode (chỉ có ở .NET Core/.NET 5+).
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ThemeManager.Instance.ApplyStartupTheme();
            var languageManager = LanguageManager.Instance;

            while (true)
            {
                using (var loginForm = new frmLogin())
                {
                    var loginResult = loginForm.ShowDialog();
                    if (loginResult != DialogResult.OK || !UserSession.Instance.IsLoggedIn)
                        break; // thoát app
                }

                using (var mainForm = new frmMain())
                {
                    Application.Run(mainForm);
                }

                if (!UserSession.Instance.IsLoggedIn)
                    continue;

                break;
            }
        }
    }
}