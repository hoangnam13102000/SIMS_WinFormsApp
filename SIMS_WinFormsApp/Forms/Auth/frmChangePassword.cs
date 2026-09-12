using SIMS_WinFormsApp.MVP.Presenters;
using SIMS_WinFormsApp.Views.Interfaces;
using SIMS_WinFormsApp.Infrastructure.Composition;
using SIMS_WinFormsApp.Services;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.Services.Session;
using SIMS_WinFormsApp.UI.I18n;
using SIMS_WinFormsApp.UI.Theme;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Forms.Auth
{
    public partial class frmChangePassword : Form, IChangePasswordView
    {
        private readonly IAuthService _authService;
        private readonly ChangePasswordPresenter _presenter;

        public frmChangePassword(IAuthService authService = null)
        {
            InitializeComponent();
            _authService = authService ?? AppComposition.CreateAuthService();
            _presenter = new ChangePasswordPresenter(this, _authService);

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
            await _presenter.ChangeAsync();
        }

        public string CurrentPassword => txtCurrent.Text;
        public string NewPassword => txtNew.Text;
        public string Confirmation => txtConfirm.Text;
        public int CurrentUserId => UserSession.Instance.CurrentUser?.UserId ?? 0;
        public void ClearError() => lblError.Text = "";
        public void ShowError(string message) => lblError.Text = message;
        public void SetLoading(bool busy)
        {
            if (IsDisposed) return;
            txtCurrent.Enabled = !busy;
            txtNew.Enabled = !busy;
            txtConfirm.Enabled = !busy;
            btnSubmit.Enabled = !busy;
            btnCancel.Enabled = !busy;
            btnSubmit.Text = busy ? Lang.Get("common.loading") : Lang.Get("changepassword.submit");
            Cursor = busy ? Cursors.WaitCursor : Cursors.Default;
        }
        public void ShowSuccess(string message) => MessageBox.Show(this, message,
            Lang.Get("common.success"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        public void CloseOnSuccess()
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}