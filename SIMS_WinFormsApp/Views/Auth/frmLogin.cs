using SIMS_WinFormsApp.Infrastructure;
using SIMS_WinFormsApp.MVP.Presenters;
using SIMS_WinFormsApp.Views.Interfaces;
using SIMS_WinFormsApp.Services;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.Services.Session;
using SIMS_WinFormsApp.UI.I18n;
using SIMS_WinFormsApp.UI.Theme;
using Guna.UI2.WinForms;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Views.Auth
{
    public partial class frmLogin : Form, ILoginView
    {
        private const string PrefKeyRememberMe = "sims.login.rememberMe";
        private const string PrefKeyRememberedUsername = "sims.login.rememberedUsername";

        private readonly IAuthService _authService;
        private readonly LoginPresenter _presenter;
        private AuthBrandPanelRenderer _brandPanelRenderer;
        private PasswordVisibilityToggle _passwordVisibilityToggle;

        public frmLogin()
        {
            InitializeComponent();
            InitializeNativeControls();
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return;
            }

            // Designer/preview and runtime must not call the parameterless constructor.
            // The app always creates this form via the overload that receives IAuthService.
            // Keeping this constructor silent avoids breaking the WinForms Designer while
            // preserving the intended runtime guard at the call site.
        }

        public frmLogin(IAuthService authService)
        {
            InitializeComponent();
            InitializeNativeControls();
            LayoutLoginPanels();
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _presenter = new LoginPresenter(this, _authService);

            AcceptButton = btnLogin;

            bool dark = ThemeManager.Instance.IsDark;
            btnLogin.CustomAccentColor = dark ? Color.FromArgb(33, 128, 185) : AppColors.Accent;
            lnkForgotPassword.ForeColor = dark ? Color.FromArgb(120, 200, 255) : AppColors.Accent;

            WireEvents();
            RefreshTexts();
            LoadRememberedUsername();

            LanguageManager.Instance.LanguageChanged += OnLanguageChanged;
            ThemeManager.Instance.ThemeChanged += OnThemeChanged;
            FormClosed += FrmLogin_FormClosed;

            // Đồng bộ màu pnlRight theo theme đang active NGAY từ đầu (trước đây chỉ
            // được cập nhật khi ThemeChanged bắn ra sau này). Nếu app khởi động khi
            // theme đã ở chế độ Dark (được lưu từ phiên trước), pnlRight vẫn giữ màu
            // trắng cứng khai báo trong Designer trong khi các label/nút bên trong đã
            // dùng màu chữ của theme Dark -> tương phản kém, giao diện login bị "vỡ".
            OnThemeChanged(this, EventArgs.Empty);
        }

        private void InitializeNativeControls()
        {
            NativeTextBoxBehavior.ApplyInputFont(this);
            NativeTextBoxBehavior.SetTextMargins(txtUsername, 6, 6);
            NativeTextBoxBehavior.SetTextMargins(txtPassword, 6, 42);
            ConfigureInputPanel(pnlUsernameInput, txtUsername);
            ConfigureInputPanel(pnlPasswordInput, txtPassword);
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
                },
                FooterText = "© " + DateTime.Now.Year + " SIMS"
            };
            brandPanel.BackgroundImage = Properties.Resources.auth_brand_preview;
            brandPanel.BackgroundImageLayout = ImageLayout.Stretch;
            _passwordVisibilityToggle = new PasswordVisibilityToggle(txtPassword);
        }

        private static void ConfigureInputPanel(Guna2Panel panel, TextBox textBox)
        {
            panel.BorderColor = AppColors.FieldBorder;
            panel.FillColor = AppColors.White;
            textBox.Enter += (sender, e) => panel.BorderColor = AppColors.Accent;
            textBox.Leave += (sender, e) => panel.BorderColor = AppColors.FieldBorder;
        }

        private void WireEvents()
        {
            btnLogin.Click += BtnLogin_Click;
            lnkForgotPassword.Click += LnkForgotPassword_Click;
        }

        private async void BtnLogin_Click(object sender, EventArgs e)
        {
            await DoLoginAsync();
        }

        private void LnkForgotPassword_Click(object sender, EventArgs e)
        {
            using (var dlg = new frmForgotPassword(txtUsername.Text.Trim()))
            {
                dlg.ShowDialog(this);
            }
        }

        private void FrmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            LanguageManager.Instance.LanguageChanged -= OnLanguageChanged;
            ThemeManager.Instance.ThemeChanged -= OnThemeChanged;
        }

        private void PnlOptionsRow_Resize(object sender, EventArgs e)
        {
            AlignOptionsRow();
        }

        private void ChkRemember_SizeChanged(object sender, EventArgs e)
        {
            AlignOptionsRow();
        }

        private void LnkForgotPassword_SizeChanged(object sender, EventArgs e)
        {
            AlignOptionsRow();
        }

        private void LnkForgotPassword_TextChanged(object sender, EventArgs e)
        {
            FitForgotPasswordLink();
        }

        private void PnlRight_Resize(object sender, EventArgs e)
        {
            LayoutLoginPanels();
        }

        private void PnlFormCard_Resize(object sender, EventArgs e)
        {
            ResizeFormCardControls();
        }

        private void AlignOptionsRow()
        {
            if (pnlOptionsRow == null || lnkForgotPassword == null || chkRemember == null) return;

            int linkX = Math.Max(0, pnlOptionsRow.Width - lnkForgotPassword.Width);
            int linkY = Math.Max(0, (pnlOptionsRow.Height - lnkForgotPassword.Height) / 2);
            lnkForgotPassword.Location = new Point(linkX, linkY);

            int checkY = Math.Max(0, (pnlOptionsRow.Height - chkRemember.Height) / 2);
            chkRemember.Location = new Point(0, checkY);
        }

        private void FitForgotPasswordLink()
        {
            if (lnkForgotPassword == null) return;

            int neededWidth = lnkForgotPassword.GetPreferredSize(Size.Empty).Width + 4;
            if (neededWidth > lnkForgotPassword.Width)
                lnkForgotPassword.Width = neededWidth;
        }

        private void CenterFormCard()
        {
            if (pnlFormCard == null || pnlRight == null) return;

            int x = Math.Max(24, (pnlRight.Width - pnlFormCard.Width) / 2);
            int yPos = Math.Max(24, (pnlRight.Height - pnlFormCard.Height) / 2);
            pnlFormCard.Location = new Point(x, yPos);
            _brandPanelRenderer.ContentTop = yPos;
        }

        private void LayoutLoginPanels()
        {
            if (pnlRight == null || pnlFormCard == null || brandPanel == null) return;

            pnlAuthSplit.PerformLayout();
            int availableCardWidth = Math.Max(1, pnlRight.ClientSize.Width - 80);
            int cardWidth = Math.Max(1, Math.Min(560, (int)(availableCardWidth * 0.78f)));
            pnlFormCard.Width = cardWidth;
            ResizeFormCardControls();
            CenterFormCard();
        }

        private void ResizeFormCardControls()
        {
            if (pnlFormCard == null) return;

            int width = Math.Max(1, pnlFormCard.ClientSize.Width);
            lblTitle.Width = width + 5;
            lblSubtitle.Width = width;
            lblUsername.Width = width;
            pnlUsernameInput.Width = width;
            lblPassword.Width = width;
            pnlPasswordInput.Width = width;
            pnlOptionsRow.Width = width;
            lblError.Width = width;
            btnLogin.Width = width;
            AlignOptionsRow();
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
            txtUsername.BackColor = dark ? Color.FromArgb(18, 20, 26) : Color.White;
            txtUsername.ForeColor = dark ? Color.White : AppColors.TextPrimary;
            pnlUsernameInput.FillColor = txtUsername.BackColor;
            pnlUsernameInput.BorderColor = txtUsername.Focused ? AppColors.Accent : AppColors.FieldBorder;
            txtPassword.BackColor = dark ? Color.FromArgb(18, 20, 26) : Color.White;
            txtPassword.ForeColor = dark ? Color.White : AppColors.TextPrimary;
            pnlPasswordInput.FillColor = txtPassword.BackColor;
            pnlPasswordInput.BorderColor = txtPassword.Focused ? AppColors.Accent : AppColors.FieldBorder;
            chkRemember.BackColor = dark ? Color.FromArgb(18, 20, 26) : AppColors.White;
            chkRemember.ForeColor = dark ? Color.White : AppColors.TextMuted;
            lnkForgotPassword.ForeColor = dark ? Color.FromArgb(120, 200, 255) : AppColors.Accent;

            Refresh();
        }
        private void RefreshTexts()
        {
            Text = Lang.Get("login.title") + " - SIMS";

            lblTitle.Text = Lang.Get("login.title");
            lblSubtitle.Text = Lang.Get("login.subtitle");

            lblUsername.Text = Lang.Get("login.username");
            NativeTextBoxBehavior.SetCueBanner(txtUsername, Lang.Get("login.username.placeholder"));

            lblPassword.Text = Lang.Get("login.password");
            NativeTextBoxBehavior.SetCueBanner(txtPassword, Lang.Get("login.password.placeholder"));
            _passwordVisibilityToggle.SetTooltips(
                Lang.Get("login.password.show"),
                Lang.Get("login.password.hide"));

            chkRemember.Text = Lang.Get("login.rememberMe");
            lnkForgotPassword.Text = Lang.Get("login.forgotPassword");

            btnLogin.Text = Lang.Get("login.submit");

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

        private void LoadRememberedUsername()
        {
            bool remembered = AppSettingsStore.Get(PrefKeyRememberMe, "0") == "1";
            chkRemember.Checked = remembered;

            if (remembered)
            {
                txtUsername.Text = AppSettingsStore.Get(PrefKeyRememberedUsername, "");
                txtPassword.Focus();
            }
            else
            {
                txtUsername.Focus();
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

        private void lnkForgotPassword_Click_1(object sender, EventArgs e)
        {

        }

        private void brandPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}