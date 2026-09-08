using SIMS_WinFormsApp.Infrastructure;
using SIMS_WinFormsApp.Services;
using SIMS_WinFormsApp.Services.Session;
using SIMS_WinFormsApp.UI.I18n;
using SIMS_WinFormsApp.UI.Theme;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Forms.Auth
{
    public partial class frmLogin : Form
    {
        private const string PrefKeyRememberMe = "sims.login.rememberMe";
        private const string PrefKeyRememberedUsername = "sims.login.rememberedUsername";

        private readonly AuthService _authService = new AuthService();

        public frmLogin()
        {
            InitializeComponent();

            AcceptButton = btnLogin;

            WireEvents();
            RefreshTexts();
            LoadRememberedUsername();

            LanguageManager.Instance.LanguageChanged += OnLanguageChanged;
            ThemeManager.Instance.ThemeChanged += OnThemeChanged;
            FormClosed += (s, e) =>
            {
                LanguageManager.Instance.LanguageChanged -= OnLanguageChanged;
                ThemeManager.Instance.ThemeChanged -= OnThemeChanged;
            };
        }

        private void WireEvents()
        {
            btnLogin.Click += async (s, e) => await DoLoginAsync();
            lnkForgotPassword.Click += (s, e) =>
                MessageBox.Show(this, Lang.Get("login.forgotPassword.notice"), Lang.Get("login.forgotPassword"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void OnLanguageChanged(object sender, EventArgs e) => RefreshTexts();

        private void OnThemeChanged(object sender, EventArgs e)
        {
            pnlRight.BackColor = AppColors.White;
            Invalidate(true);
        }
        private void RefreshTexts()
        {
            Text = Lang.Get("login.title") + " - SIMS";

            lblTitle.Text = Lang.Get("login.title");
            lblSubtitle.Text = Lang.Get("login.subtitle");

            lblUsername.Text = Lang.Get("login.username");
            txtUsername.PlaceholderText = Lang.Get("login.username.placeholder");

            lblPassword.Text = Lang.Get("login.password");
            txtPassword.PlaceholderText = Lang.Get("login.password.placeholder");
            txtPassword.ShowTooltip = Lang.Get("login.password.show");
            txtPassword.HideTooltip = Lang.Get("login.password.hide");

            chkRemember.Text = Lang.Get("login.rememberMe");
            lnkForgotPassword.Text = Lang.Get("login.forgotPassword");
            AlignForgotPassword();

            btnLogin.Text = Lang.Get("login.submit");

            brandPanel.BrandName = Lang.Get("login.leftpanel.brand");
            brandPanel.Logo = Properties.Resources.logo_icon;
            brandPanel.Tagline = Lang.Get("login.leftpanel.tagline");
            brandPanel.Features = new[]
            {
                Lang.Get("login.leftpanel.feature1"),
                Lang.Get("login.leftpanel.feature2"),
                Lang.Get("login.leftpanel.feature3")
            };
            brandPanel.FooterText = Lang.Get("app.copyright", DateTime.Now.Year);
        }

        private void AlignForgotPassword()
        {
            if (pnlOptionsRow == null || lnkForgotPassword == null) return;
            lnkForgotPassword.Location = new System.Drawing.Point(
                Math.Max(0, pnlOptionsRow.ClientSize.Width - lnkForgotPassword.Width), 4);
        }

        private void LoadRememberedUsername()
        {
            bool remembered = AppSettingsStore.Get(PrefKeyRememberMe, "0") == "1";
            chkRemember.Checked = remembered;

            if (remembered)
            {
                txtUsername.Text = AppSettingsStore.Get(PrefKeyRememberedUsername, "");
                txtPassword.FocusInput();
            }
            else
            {
                txtUsername.FocusInput();
            }
        }

        private void SaveRememberedUsername(string username)
        {
            if (chkRemember.Checked)
            {
                AppSettingsStore.Set(PrefKeyRememberMe, "1");
                AppSettingsStore.Set(PrefKeyRememberedUsername, username);
            }
            else
            {
                AppSettingsStore.Set(PrefKeyRememberMe, "0");
                AppSettingsStore.Set(PrefKeyRememberedUsername, "");
            }
        }

        private async Task DoLoginAsync()
        {
            ClearError();

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ShowError(Lang.Get("login.error.emptyFields"));
                return;
            }

            SetLoading(true);
            try
            {
                LoginResult result = await Task.Run(() => _authService.TryLogin(username, password));

                switch (result.Status)
                {
                    case LoginStatus.Success:
                        SaveRememberedUsername(username);
                        DialogResult = DialogResult.OK;
                        Close();
                        break;

                    case LoginStatus.AccountLocked:
                        ShowError(Lang.Get("login.error.locked"));
                        break;

                    case LoginStatus.AccountDisabled:
                        ShowError(Lang.Get("login.error.disabled"));
                        break;

                    default:
                        ShowError(Lang.Get("login.error.invalid"));
                        break;
                }
            }
            catch (Exception)
            {

                ShowError(Lang.Get("login.error.configMissing"));
            }
            finally
            {
                if (!IsDisposed) SetLoading(false);
            }
        }

        private void ShowError(string message) => lblError.Text = message;

        private void ClearError() => lblError.Text = "";

        private void SetLoading(bool busy)
        {
            txtUsername.Enabled = !busy;
            txtPassword.Enabled = !busy;
            chkRemember.Enabled = !busy;
            btnLogin.Enabled = !busy;
            btnLogin.Text = busy ? Lang.Get("common.loading") : Lang.Get("login.submit");
            Cursor = busy ? Cursors.WaitCursor : Cursors.Default;
        }
    }
}