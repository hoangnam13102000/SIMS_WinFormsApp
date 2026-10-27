using SIMS_WinFormsApp.MVP.Presenters;
using SIMS_WinFormsApp.Views.Interfaces;
using SIMS_WinFormsApp.Infrastructure.Composition;
using SIMS_WinFormsApp.Services;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.Services.Session;
using SIMS_WinFormsApp.UI.I18n;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Dialogs;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Views.Auth
{
    public partial class frmChangePassword : Form, IChangePasswordView
    {
        private readonly IAuthService _authService;
        private readonly ChangePasswordPresenter _presenter;
        private AuthBrandPanelRenderer _brandPanelRenderer;
        private PasswordVisibilityToggle _currentPasswordVisibilityToggle;
        private PasswordVisibilityToggle _newPasswordVisibilityToggle;
        private PasswordVisibilityToggle _confirmPasswordVisibilityToggle;

        public frmChangePassword() : this(null)
        {
        }

        public frmChangePassword(IAuthService authService = null)
        {
            InitializeComponent();
            InitializeNativeControls();

            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                _authService = authService;
                _presenter = null;
                return;
            }

            _authService = authService ?? AppComposition.CreateAuthService();
            _presenter = new ChangePasswordPresenter(this, _authService);

            AcceptButton = btnSubmit;
            CancelButton = btnCancel;

            WireEvents();
            RefreshTexts();

            LanguageManager.Instance.LanguageChanged += OnLanguageChanged;
            ThemeManager.Instance.ThemeChanged += OnThemeChanged;
            FormClosed += FrmChangePassword_FormClosed;
        }

        private void InitializeNativeControls()
        {
            NativeTextBoxBehavior.ApplyInputFont(this);
            _brandPanelRenderer = new AuthBrandPanelRenderer(brandPanel)
            {
                Logo = Properties.Resources.logo_icon,
                BrandName = "SIMS",
                Tagline = "Quản lý cửa hàng thông minh",
                Features = new[]
                {
                    "Quản lý bán hàng",
                    "Theo dõi tồn kho",
                    "Báo cáo kinh doanh"
                }
            };
            _currentPasswordVisibilityToggle = new PasswordVisibilityToggle(txtCurrent);
            _newPasswordVisibilityToggle = new PasswordVisibilityToggle(txtNew);
            _confirmPasswordVisibilityToggle = new PasswordVisibilityToggle(txtConfirm);
        }

        private void WireEvents()
        {
            btnCancel.Click += BtnCancel_Click;
            btnSubmit.Click += BtnSubmit_Click;
            Load += FrmChangePassword_Load;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private async void BtnSubmit_Click(object sender, EventArgs e)
        {
            await DoChangePasswordAsync();
        }

        private void FrmChangePassword_Load(object sender, EventArgs e)
        {
            if (!UserSession.Instance.IsLoggedIn)
            {
                Close();
                return;
            }
            txtCurrent.Focus();
        }

        private void FrmChangePassword_FormClosed(object sender, FormClosedEventArgs e)
        {
            LanguageManager.Instance.LanguageChanged -= OnLanguageChanged;
            ThemeManager.Instance.ThemeChanged -= OnThemeChanged;
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
            NativeTextBoxBehavior.SetCueBanner(txtCurrent, Lang.Get("changepassword.current.placeholder"));
            _currentPasswordVisibilityToggle.SetTooltips(
                Lang.Get("login.password.show"),
                Lang.Get("login.password.hide"));

            lblNew.Text = Lang.Get("changepassword.new");
            NativeTextBoxBehavior.SetCueBanner(txtNew, Lang.Get("changepassword.new.placeholder"));
            _newPasswordVisibilityToggle.SetTooltips(
                Lang.Get("login.password.show"),
                Lang.Get("login.password.hide"));

            lblConfirm.Text = Lang.Get("changepassword.confirm");
            NativeTextBoxBehavior.SetCueBanner(txtConfirm, Lang.Get("changepassword.confirm.placeholder"));
            _confirmPasswordVisibilityToggle.SetTooltips(
                Lang.Get("login.password.show"),
                Lang.Get("login.password.hide"));

            btnCancel.Text = Lang.Get("changepassword.cancel");
            btnSubmit.Text = Lang.Get("changepassword.submit");

            _brandPanelRenderer.BrandName = Lang.Get("login.leftpanel.brand");
            _brandPanelRenderer.Logo = Properties.Resources.logo_icon;
            _brandPanelRenderer.Tagline = Lang.Get("login.leftpanel.tagline");
            _brandPanelRenderer.Features = new[]
            {
                Lang.Get("login.leftpanel.feature1"),
                Lang.Get("login.leftpanel.feature2"),
                Lang.Get("login.leftpanel.feature3")
            };
            _brandPanelRenderer.FooterText = Lang.Get("app.copyright", DateTime.Now.Year);
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
        public void ShowSuccess(string message) => DialogHelper.ShowSuccess(this,
            Lang.Get("common.success"), message);
        public void CloseOnSuccess()
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void brandPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}