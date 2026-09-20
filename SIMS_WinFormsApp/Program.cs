using FontAwesome.Sharp;
using SIMS_WinFormsApp.Forms.Auth;
using SIMS_WinFormsApp.Forms.SystemMgmt;
using SIMS_WinFormsApp.Infrastructure.Composition;
using SIMS_WinFormsApp.Services.Session;
using SIMS_WinFormsApp.UI.I18n;
using SIMS_WinFormsApp.UI.Theme;
using System;
using System.Drawing;
using System.IO;
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

            WarmUpFontAwesome();

            ThemeManager.Instance.ApplyStartupTheme();
            _ = LanguageManager.Instance;

            var appIcon = TryLoadApplicationIcon();

            while (true)
            {
                using (var loginForm = AppComposition.CreateLoginForm())
                {
                    if (appIcon != null)
                    {
                        loginForm.Icon = appIcon;
                    }

                    loginForm.StartPosition = FormStartPosition.CenterScreen;
                    loginForm.TopMost = true;
                    loginForm.Load += (s, e) => loginForm.BeginInvoke(new Action(() => { loginForm.TopMost = false; }));

                    var loginResult = loginForm.ShowDialog();
                    if (loginResult != DialogResult.OK || !UserSession.Instance.IsLoggedIn)
                        break;
                }

                using (var mainForm = new frmMain())
                {
                    if (appIcon != null)
                    {
                        mainForm.Icon = appIcon;
                    }

                    mainForm.StartPosition = FormStartPosition.CenterScreen;
                    mainForm.Shown += (s, e) => { mainForm.Activate(); mainForm.BringToFront(); };
                    Application.Run(mainForm);
                }

                if (!UserSession.Instance.IsLoggedIn)
                    continue;

                break;
            }
        }

        private static System.Drawing.Icon TryLoadApplicationIcon()
        {
            try
            {
                var relativePath = Path.Combine("Resources", "Logo", "app_logo.ico");
                var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);

                if (File.Exists(fullPath))
                    return new System.Drawing.Icon(fullPath);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("[AppIcon] Failed to load icon: " + ex.Message);
            }

            return null;
        }

        private static void WarmUpFontAwesome()
        {
            try
            {
                using (var pic = new IconPictureBox
                {
                    IconChar = IconChar.Bell,
                    IconFont = IconFont.Solid,
                    IconSize = 16,
                    Size = new Size(16, 16)
                })
                {

                    var _ = pic.IconChar;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("[FontAwesome] Warm-up failed: " + ex.Message);
              
            }
        }
    }
}