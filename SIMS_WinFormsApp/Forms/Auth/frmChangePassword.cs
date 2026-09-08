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
    public partial class frmChangePassword : Form
    {
        private readonly AuthService _authService = new AuthService();

        public frmChangePassword()
        {
            InitializeComponent();

            AcceptButton = btnSubmit;
            CancelButton = btnCancel;

            WireEvents();
            RefreshTexts();

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
            btnCancel.Click += (s, e) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };

            btnSubmit.Click += async (s, e) => await DoChangePasswordAsync();

            Load += (s, e) =>
            {
                if (!UserSession.Instance.IsLoggedIn)
                {
                    Close();
                    return;
                }
                txtCurrent.FocusInput();
            };
        }

        private void OnLanguageChanged(object sender, EventArgs e) => RefreshTexts();

        private void OnThemeChanged(object sender, EventArgs e)
        {
            pnlRight.BackColor = AppColors.White;
            Invalidate(true);
        }

        private void RefreshTexts()
        {
            Text = Lang.Get("changepassword.title") + " - SIMS";

            lblTitle.Text = Lang.Get("changepassword.title");
            lblSubtitle.Text = Lang.Get("changepassword.subtitle");

            lblCurrent.Text = Lang.Get("changepassword.current");
            txtCurrent.PlaceholderText = Lang.Get("changepassword.current.placeholder");
            txtCurrent.ShowTooltip = Lang.Get("login.password.show");
            txtCurrent.HideTooltip = Lang.Get("login.password.hide");

            lblNew.Text = Lang.Get("changepassword.new");
            txtNew.PlaceholderText = Lang.Get("changepassword.new.placeholder");
            txtNew.ShowTooltip = Lang.Get("login.password.show");
            txtNew.HideTooltip = Lang.Get("login.password.hide");

            lblConfirm.Text = Lang.Get("changepassword.confirm");
            txtConfirm.PlaceholderText = Lang.Get("changepassword.confirm.placeholder");
            txtConfirm.ShowTooltip = Lang.Get("login.password.show");
            txtConfirm.HideTooltip = Lang.Get("login.password.hide");

            btnCancel.Text = Lang.Get("changepassword.cancel");
            btnSubmit.Text = Lang.Get("changepassword.submit");

            brandPanel.BrandName = Lang.Get("login.leftpanel.brand");
            brandPanel.Tagline = Lang.Get("login.leftpanel.tagline");
            brandPanel.Features = new[]
            {
                Lang.Get("login.leftpanel.feature1"),
                Lang.Get("login.leftpanel.feature2"),
                Lang.Get("login.leftpanel.feature3")
            };
            brandPanel.FooterText = Lang.Get("app.copyright", DateTime.Now.Year);
        }

        private async Task DoChangePasswordAsync()
        {
            ClearError();

            string current = txtCurrent.Text;
            string newPassword = txtNew.Text;
            string confirm = txtConfirm.Text;

            if (string.IsNullOrEmpty(current) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirm))
            {
                ShowError(Lang.Get("changepassword.error.emptyFields"));
                return;
            }

            if (newPassword != confirm)
            {
                ShowError(Lang.Get("changepassword.error.mismatch"));
                return;
            }

            int userId = UserSession.Instance.CurrentUser.UserId;

            SetLoading(true);
            try
            {
                ChangePasswordStatus status = await Task.Run(() =>
                    _authService.ChangePassword(userId, current, newPassword));

                switch (status)
                {
                    case ChangePasswordStatus.Success:
                        MessageBox.Show(this, Lang.Get("changepassword.success"), Lang.Get("common.success"),
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DialogResult = DialogResult.OK;
                        Close();
                        break;

                    case ChangePasswordStatus.CurrentPasswordWrong:
                        ShowError(Lang.Get("changepassword.error.currentWrong"));
                        break;

                    case ChangePasswordStatus.NewPasswordTooShort:
                        ShowError(Lang.Get("changepassword.error.tooShort"));
                        break;

                    case ChangePasswordStatus.NewPasswordSameAsOld:
                        ShowError(Lang.Get("changepassword.error.sameAsOld"));
                        break;
                }
            }
            catch (Exception)
            {
                ShowError(Lang.Get("login.error.unexpected"));
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
            txtCurrent.Enabled = !busy;
            txtNew.Enabled = !busy;
            txtConfirm.Enabled = !busy;
            btnSubmit.Enabled = !busy;
            btnCancel.Enabled = !busy;
            btnSubmit.Text = busy ? Lang.Get("common.loading") : Lang.Get("changepassword.submit");
            Cursor = busy ? Cursors.WaitCursor : Cursors.Default;
        }
    }
}