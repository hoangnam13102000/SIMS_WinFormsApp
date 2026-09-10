using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIMS_WinFormsApp.Infrastructure;
using SIMS_WinFormsApp.Services.Mail;
using SIMS_WinFormsApp.UI.I18n;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Forms.SystemMgmt
{
   
    public partial class frmMailConfig : Form
    {
        private static readonly Regex EmailPattern =
            new Regex(@"^[\w.+-]+@[\w-]+\.[a-zA-Z]{2,}$", RegexOptions.Compiled);

        private const string DefaultSenderEmail = "hoangnam131020@gmail.com";

        private bool _busy;

        public frmMailConfig()
        {
            InitializeComponent();

            RefreshTexts();
            LoadCurrentConfig();
            WireEvents();

            LanguageManager.Instance.LanguageChanged += OnLanguageChanged;
            FormClosed += (s, e) => LanguageManager.Instance.LanguageChanged -= OnLanguageChanged;
        }

        private void OnLanguageChanged(object sender, EventArgs e) => RefreshTexts();

        private void RefreshTexts()
        {
            Text = Lang.Get("mailconfig.frame.title") + " - SIMS";

            lblTitle.Text = Lang.Get("mailconfig.title");
            lblSubtitle.Text = Lang.Get("mailconfig.subtitle");

            lblSenderEmail.Text = Lang.Get("mailconfig.senderEmail");
            txtSenderEmail.PlaceholderText = Lang.Get("mailconfig.senderEmail.placeholder");

            lblAppPassword.Text = Lang.Get("mailconfig.appPassword");
            txtAppPassword.PlaceholderText = Lang.Get("mailconfig.appPassword.placeholder");
            txtAppPassword.ShowTooltip = Lang.Get("mailconfig.appPassword.show");
            txtAppPassword.HideTooltip = Lang.Get("mailconfig.appPassword.hide");

            lblHint.Text = Lang.Get("mailconfig.hint");
            lnkHowTo.Text = Lang.Get("mailconfig.howTo");

            btnTest.Text = Lang.Get("mailconfig.test");
            btnSave.Text = Lang.Get("mailconfig.save");
        }

        private void LoadCurrentConfig()
        {
            
            string savedEmail = SecureConfigStore.Get(MailSender.KeySenderAddress, "");
            txtSenderEmail.Text = string.IsNullOrWhiteSpace(savedEmail) ? DefaultSenderEmail : savedEmail;
        }

        private void WireEvents()
        {
            lnkHowTo.Click += (s, e) =>
            {
                try
                {
                    Process.Start(new ProcessStartInfo("https://myaccount.google.com/apppasswords")
                    {
                        UseShellExecute = true
                    });
                }
                catch (Exception)
                {
                    // Không mở được trình duyệt mặc định -> bỏ qua, không phải lỗi nghiêm trọng.
                }
            };

            btnSave.Click += async (s, e) => await SaveAsync();
            btnTest.Click += async (s, e) => await SendTestAsync();
        }

        private bool ValidateInput(out string email, out string appPassword)
        {
            email = txtSenderEmail.Text.Trim();
            appPassword = NormalizeAppPassword(txtAppPassword.Text);

            if (string.IsNullOrEmpty(email))
            {
                ShowMessage(Lang.Get("mailconfig.validation.email.required"), AppColors.Error);
                return false;
            }
            if (!EmailPattern.IsMatch(email))
            {
                ShowMessage(Lang.Get("mailconfig.validation.email.invalid"), AppColors.Error);
                return false;
            }
            if (string.IsNullOrEmpty(appPassword))
            {
                ShowMessage(Lang.Get("mailconfig.validation.password.required"), AppColors.Error);
                return false;
            }
            return true;
        }

        private static string NormalizeAppPassword(string input) =>
            (input ?? "").Replace(" ", "").Trim();

        private async Task SaveAsync()
        {
            if (_busy) return;
            if (!ValidateInput(out string email, out string appPassword)) return;

            SetBusy(true);
            try
            {
                SecureConfigStore.Set(MailSender.KeySenderAddress, email);
                SecureConfigStore.Set(MailSender.KeySenderAppPassword, appPassword);
                ShowMessage(Lang.Get("mailconfig.save.success"), AppColors.Success);
            }
            catch (Exception)
            {
                ShowMessage(Lang.Get("mailconfig.save.failed"), AppColors.Error);
            }
            finally
            {
                if (!IsDisposed) SetBusy(false);
            }
        }

        private async Task SendTestAsync()
        {
            if (_busy) return;
            if (!ValidateInput(out string email, out string appPassword)) return;

            SetBusy(true, testing: true);
            try
            {
                // Lưu trước khi test để MailSender đọc đúng giá trị vừa nhập
                // (tránh trường hợp người dùng bấm "Gửi thử" mà chưa "Lưu").
                SecureConfigStore.Set(MailSender.KeySenderAddress, email);
                SecureConfigStore.Set(MailSender.KeySenderAppPassword, appPassword);

                var sender = new MailSender();
                await Task.Run(() => sender.Send(
                    email,
                    Lang.Get("mailconfig.test.mail.subject"),
                    Lang.Get("mailconfig.test.mail.body")));

                ShowMessage(Lang.Get("mailconfig.test.success"), AppColors.Success);
            }
            catch (MailFailedException ex)
            {
                ShowMessage(Lang.Get("mailconfig.test.failed") + " (" + ex.Message + ")", AppColors.Error);
            }
            catch (Exception)
            {
                ShowMessage(Lang.Get("mailconfig.test.failed"), AppColors.Error);
            }
            finally
            {
                if (!IsDisposed) SetBusy(false, testing: true);
            }
        }

        private void SetBusy(bool busy, bool testing = false)
        {
            _busy = busy;
            txtSenderEmail.Enabled = !busy;
            txtAppPassword.Enabled = !busy;
            btnSave.Enabled = !busy;
            btnTest.Enabled = !busy;

            if (testing)
                btnTest.Text = busy ? Lang.Get("mailconfig.testing") : Lang.Get("mailconfig.test");
            else
                btnSave.Text = busy ? Lang.Get("mailconfig.saving") : Lang.Get("mailconfig.save");

            Cursor = busy ? Cursors.WaitCursor : Cursors.Default;
        }

        private void ShowMessage(string message, System.Drawing.Color color)
        {
            lblMessage.Text = message;
            lblMessage.ForeColor = color;
        }
    }
}