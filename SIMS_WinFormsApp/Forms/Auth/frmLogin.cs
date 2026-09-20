using SIMS_WinFormsApp.Infrastructure;
using SIMS_WinFormsApp.MVP.Presenters;
using SIMS_WinFormsApp.Views.Interfaces;
using SIMS_WinFormsApp.Services;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.Services.Session;
using SIMS_WinFormsApp.UI.I18n;
using SIMS_WinFormsApp.UI.Theme;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Forms.Auth
{
    public partial class frmLogin : Form, ILoginView
    {
        private const string PrefKeyRememberMe = "sims.login.rememberMe";
        private const string PrefKeyRememberedUsername = "sims.login.rememberedUsername";

        private readonly IAuthService _authService;
        private readonly LoginPresenter _presenter;

        public frmLogin(IAuthService authService)
        {
            InitializeComponent();
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _presenter = new LoginPresenter(this, _authService);

            AcceptButton = btnLogin;

            bool dark = ThemeManager.Instance.IsDark;
            btnLogin.CustomAccentColor = dark ? Color.FromArgb(33, 128, 185) : AppColors.Accent;
            txtUsername.DarkMode = dark;
            txtPassword.DarkMode = dark;
            chkRemember.DarkMode = dark;
            lnkForgotPassword.ForeColor = dark ? Color.FromArgb(120, 200, 255) : AppColors.Accent;

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

            // Đồng bộ màu pnlRight theo theme đang active NGAY từ đầu (trước đây chỉ
            // được cập nhật khi ThemeChanged bắn ra sau này). Nếu app khởi động khi
            // theme đã ở chế độ Dark (được lưu từ phiên trước), pnlRight vẫn giữ màu
            // trắng cứng khai báo trong Designer trong khi các label/nút bên trong đã
            // dùng màu chữ của theme Dark -> tương phản kém, giao diện login bị "vỡ".
            OnThemeChanged(this, EventArgs.Empty);
        }

        private void WireEvents()
        {
            btnLogin.Click += async (s, e) => await DoLoginAsync();
            lnkForgotPassword.Click += (s, e) =>
            {
                using (var dlg = new frmForgotPassword(txtUsername.Text.Trim()))
                {
                    dlg.ShowDialog(this);
                }
            };
        }

        private void OnLanguageChanged(object sender, EventArgs e) => RefreshTexts();

        private void OnThemeChanged(object sender, EventArgs e)
        {
            bool dark = ThemeManager.Instance.IsDark;

            pnlRight.BackColor = dark ? Color.FromArgb(18, 20, 26) : AppColors.White;
            pnlRight.ForeColor = dark ? Color.White : AppColors.TextPrimary;
            lblTitle.ForeColor = dark ? Color.White : AppColors.TextTitle;
            lblSubtitle.ForeColor = dark ? Color.FromArgb(200, 210, 225) : AppColors.TextMuted;
            lblUsername.ForeColor = dark ? Color.White : AppColors.TextPrimary;
            lblPassword.ForeColor = dark ? Color.White : AppColors.TextPrimary;
            lblError.ForeColor = AppColors.Error;
            btnLogin.CustomAccentColor = dark ? Color.FromArgb(33, 128, 185) : AppColors.Accent;
            txtUsername.DarkMode = dark;
            txtPassword.DarkMode = dark;
            chkRemember.DarkMode = dark;
            lnkForgotPassword.ForeColor = dark ? Color.FromArgb(120, 200, 255) : AppColors.Accent;

            Refresh();
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

        private async Task DoLoginAsync()
        {
            await _presenter.LoginAsync();
        }

        public string Username => txtUsername.Text;

        public string Password => txtPassword.Text;

        public bool RememberMe => chkRemember.Checked;

        public void ShowError(string message) => lblError.Text = message;

        public void ClearError() => lblError.Text = "";

        public void SetLoading(bool busy)
        {
            if (IsDisposed) return;

            txtUsername.Enabled = !busy;
            txtPassword.Enabled = !busy;
            chkRemember.Enabled = !busy;
            btnLogin.Enabled = !busy;
            btnLogin.Text = busy ? Lang.Get("common.loading") : Lang.Get("login.submit");
            Cursor = busy ? Cursors.WaitCursor : Cursors.Default;
        }

        public void SaveRememberedUsername(string username, bool rememberMe)
        {
            if (rememberMe)
            {
                AppSettingsStore.Set(PrefKeyRememberMe, "1");
                AppSettingsStore.Set(PrefKeyRememberedUsername, username);
            }
            else
            {
                AppSettingsStore.Set(PrefKeyRememberMe, "0");
                AppSettingsStore.Set(PrefKeyRememberedUsername, string.Empty);
            }
        }

        public void CloseOnSuccess()
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}