using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIMS_WinFormsApp.Services.Security;
using SIMS_WinFormsApp.UI.I18n;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Forms.Auth
{
    public partial class frmForgotPassword : Form
    {
        private enum Step { Identify, VerifyOtp, ResetPassword }

        private readonly PasswordResetService _resetService = PasswordResetService.Instance;

        private Step _currentStep = Step.Identify;
        private string _challengeId;
        private bool _busy;
        private bool _completed;
        private string _pendingUsername = "";

        private Timer _countdownTimer;
        private int _countdownRemaining;

        private bool _suppressOtpTextChanged;

        public frmForgotPassword() : this("") { }

        public frmForgotPassword(string initialUsername)
        {
            InitializeComponent();

            txtUsername1.Text = (initialUsername ?? "").Trim();
            RefreshTexts();
            WireEvents();

            FormClosing += FrmForgotPassword_FormClosing;
        }

        private void WireEvents()
        {
            btnSubmit1.Click += async (s, e) => await RequestOtpAsync();
            lnkBack1.Click += (s, e) => RequestClose();

            btnVerify2.Click += async (s, e) => await VerifyOtpAsync();
            lnkBack2.Click += (s, e) => ReturnToIdentify();
            lnkResend2.Click += async (s, e) => await ResendOtpAsync();

            txtOtp2.TextChanged2 += (s, e) => FilterOtpInput();

            txtNewPassword3.TextChanged2 += (s, e) => UpdatePasswordMatch();
            txtConfirmPassword3.TextChanged2 += (s, e) => UpdatePasswordMatch();

            btnReset3.Click += async (s, e) => await ResetPasswordAsync();
            lnkCancel3.Click += (s, e) => RequestClose();
        }

        private void RefreshTexts()
        {
            Text = Lang.Get("forgot.frame.title") + " - SIMS";

            lblStep1.Text = Lang.Get("forgot.step.counter", 1, 3);
            lblTitle1.Text = Lang.Get("forgot.identify.title");
            lblSubtitle1.Text = Lang.Get("forgot.identify.subtitle");
            lblUsernameLabel1.Text = Lang.Get("forgot.identify.username");
            txtUsername1.PlaceholderText = Lang.Get("forgot.identify.username.placeholder");
            lblEmailLabel1.Text = Lang.Get("forgot.identify.email");
            txtEmail1.PlaceholderText = Lang.Get("forgot.identify.email.placeholder");
            btnSubmit1.Text = Lang.Get("forgot.identify.submit");
            lnkBack1.Text = Lang.Get("forgot.backToLogin");
            CenterLink(lnkBack1);   // <-- thêm dòng này

            lblStep2.Text = Lang.Get("forgot.step.counter", 2, 3);
            lblTitle2.Text = Lang.Get("forgot.otp.title");
            lblOtpLabel2.Text = Lang.Get("forgot.otp.code");
            txtOtp2.PlaceholderText = Lang.Get("forgot.otp.code.placeholder");
            btnVerify2.Text = Lang.Get("forgot.otp.verify");
            lnkBack2.Text = Lang.Get("forgot.otp.back");
            lnkResend2.Text = Lang.Get("forgot.otp.resend");

            lblStep3.Text = Lang.Get("forgot.step.counter", 3, 3);
            lblTitle3.Text = Lang.Get("forgot.password.title");
            lblNewPasswordLabel3.Text = Lang.Get("forgot.password.new");
            txtNewPassword3.PlaceholderText = Lang.Get("forgot.password.new.placeholder");
            txtNewPassword3.ShowTooltip = Lang.Get("forgot.password.show");
            txtNewPassword3.HideTooltip = Lang.Get("forgot.password.hide");
            lblConfirmPasswordLabel3.Text = Lang.Get("forgot.password.confirm");
            txtConfirmPassword3.PlaceholderText = Lang.Get("forgot.password.confirm.placeholder");
            txtConfirmPassword3.ShowTooltip = Lang.Get("forgot.password.show");
            txtConfirmPassword3.HideTooltip = Lang.Get("forgot.password.hide");
            lblRequirements3.Text = Lang.Get("forgot.password.requirements");
            btnReset3.Text = Lang.Get("forgot.password.submit");
            lnkCancel3.Text = Lang.Get("forgot.backToLogin");
            CenterLink(lnkCancel3);  
        }

        private void CenterLink(Label link)
        {
            link.Left = (ContentWidth - link.Width) / 2;
        }

        // ===================== STEP 1: Identify =====================

        private async Task RequestOtpAsync()
        {
            if (_busy) return;

            string username = txtUsername1.Text.Trim();
            string email = txtEmail1.Text.Trim();

            if (string.IsNullOrEmpty(username))
            {
                ShowMessage1(Lang.Get("forgot.validation.username.required"), AppColors.Error);
                return;
            }
            if (username.Length < 3 || username.Length > 50)
            {
                ShowMessage1(Lang.Get("forgot.validation.username.length"), AppColors.Error);
                return;
            }
            if (string.IsNullOrEmpty(email))
            {
                ShowMessage1(Lang.Get("forgot.validation.email.required"), AppColors.Error);
                return;
            }
            if (!Regex.IsMatch(email, @"^[\w.+-]+@[\w-]+\.[a-zA-Z]{2,}$"))
            {
                ShowMessage1(Lang.Get("forgot.validation.email.invalid"), AppColors.Error);
                return;
            }

            SetBusy1(true);
            ShowMessage1(Lang.Get("forgot.identify.genericNotice"), AppColors.TextMuted);

            var result = await Task.Run(() => _resetService.RequestOtp(username, email));

            SetBusy1(false);

            switch (result.Status)
            {
                case PasswordResetService.RequestStatus.Accepted:
                    _challengeId = result.ChallengeId;
                    _pendingUsername = username;
                    lblSentTo2.Text = Lang.Get("forgot.otp.sentTo", result.MaskedEmail);
                    txtOtp2.Text = "";
                    ShowMessage2(Lang.Get("forgot.request.accepted"), AppColors.Info);
                    ShowStep(Step.VerifyOtp);
                    StartCountdown(result.RetryAfterSeconds);
                    break;

                case PasswordResetService.RequestStatus.RateLimited:
                    ShowMessage1(Lang.Get("forgot.request.rateLimited", result.RetryAfterSeconds), AppColors.Error);
                    break;

                case PasswordResetService.RequestStatus.MailFailed:
                    ShowMessage1(Lang.Get("forgot.request.mailFailed"), AppColors.Error);
                    break;

                case PasswordResetService.RequestStatus.InvalidInput:
                    ShowMessage1(Lang.Get("forgot.validation.email.invalid"), AppColors.Error);
                    break;

                default:
                    ShowMessage1(Lang.Get("forgot.request.systemError"), AppColors.Error);
                    break;
            }
        }

        private void ShowMessage1(string text, System.Drawing.Color color)
        {
            lblMessage1.ForeColor = color;
            lblMessage1.Text = text;
        }

        private void SetBusy1(bool busy)
        {
            _busy = busy;
            btnSubmit1.Enabled = !busy;
            btnSubmit1.Text = busy ? Lang.Get("forgot.identify.submitting") : Lang.Get("forgot.identify.submit");
            Cursor = busy ? Cursors.WaitCursor : Cursors.Default;
        }

        // ===================== STEP 2: OTP =====================

        private void FilterOtpInput()
        {
            if (_suppressOtpTextChanged) return;

            string digitsOnly = Regex.Replace(txtOtp2.Text, @"\D", "");
            if (digitsOnly.Length > 6) digitsOnly = digitsOnly.Substring(0, 6);

            if (digitsOnly != txtOtp2.Text)
            {
                _suppressOtpTextChanged = true;
                txtOtp2.Text = digitsOnly;
                txtOtp2.FocusInput();
                _suppressOtpTextChanged = false;
            }
        }

        private async Task VerifyOtpAsync()
        {
            if (_busy || _challengeId == null) return;

            string code = txtOtp2.Text.Trim();
            if (code.Length != 6)
            {
                ShowMessage2(Lang.Get("forgot.validation.otp.length"), AppColors.Error);
                return;
            }

            SetBusy2(true);

            var result = await Task.Run(() => _resetService.VerifyOtp(_challengeId, code));

            SetBusy2(false);

            switch (result.Status)
            {
                case PasswordResetService.VerifyStatus.Success:
                    StopCountdown();
                    txtOtp2.Text = "";
                    txtNewPassword3.Text = "";
                    txtConfirmPassword3.Text = "";
                    ShowMessage3(" ", AppColors.TextMuted);
                    ShowMatch3(" ", AppColors.TextMuted);
                    ShowStep(Step.ResetPassword);
                    break;

                case PasswordResetService.VerifyStatus.InvalidCode:
                    ShowMessage2(Lang.Get("forgot.verify.invalid", result.RemainingAttempts), AppColors.Error);
                    txtOtp2.FocusInput();
                    break;

                case PasswordResetService.VerifyStatus.TooManyAttempts:
                    ShowMessage2(Lang.Get("forgot.verify.tooManyAttempts"), AppColors.Error);
                    break;

                case PasswordResetService.VerifyStatus.Expired:
                    ShowMessage2(Lang.Get("forgot.verify.expired"), AppColors.Error);
                    break;

                case PasswordResetService.VerifyStatus.AlreadyVerified:
                    ShowStep(Step.ResetPassword);
                    break;

                default:
                    ShowMessage2(Lang.Get("forgot.verify.notFound"), AppColors.Error);
                    break;
            }
        }

        private async Task ResendOtpAsync()
        {
            if (_busy || _challengeId == null) return;

            lnkResend2.Enabled = false;
            var result = await Task.Run(() => _resetService.ResendOtp(_challengeId));

            switch (result.Status)
            {
                case PasswordResetService.ResendStatus.Success:
                    txtOtp2.Text = "";
                    ShowMessage2(Lang.Get("forgot.otp.resent"), AppColors.Success);
                    StartCountdown(result.RetryAfterSeconds);
                    txtOtp2.FocusInput();
                    break;

                case PasswordResetService.ResendStatus.Cooldown:
                    StartCountdown(result.RetryAfterSeconds);
                    break;

                case PasswordResetService.ResendStatus.RateLimited:
                    lnkResend2.Enabled = false;
                    ShowMessage2(Lang.Get("forgot.request.rateLimited", result.RetryAfterSeconds), AppColors.Error);
                    break;

                case PasswordResetService.ResendStatus.MailFailed:
                    lnkResend2.Enabled = true;
                    ShowMessage2(Lang.Get("forgot.request.mailFailed"), AppColors.Error);
                    break;

                case PasswordResetService.ResendStatus.AlreadyVerified:
                    ShowStep(Step.ResetPassword);
                    break;

                default:
                    lnkResend2.Enabled = true;
                    ShowMessage2(Lang.Get("forgot.verify.notFound"), AppColors.Error);
                    break;
            }
        }

        private void ReturnToIdentify()
        {
            if (_busy) return;
            _resetService.CancelChallenge(_challengeId);
            _challengeId = null;
            StopCountdown();
            txtOtp2.Text = "";
            ShowMessage1(" ", AppColors.TextMuted);
            ShowStep(Step.Identify);
        }

        private void ShowMessage2(string text, System.Drawing.Color color)
        {
            lblMessage2.ForeColor = color;
            lblMessage2.Text = text;
        }

        private void SetBusy2(bool busy)
        {
            _busy = busy;
            btnVerify2.Enabled = !busy;
            btnVerify2.Text = busy ? Lang.Get("forgot.otp.verifying") : Lang.Get("forgot.otp.verify");
            Cursor = busy ? Cursors.WaitCursor : Cursors.Default;
        }

        private void StartCountdown(int seconds)
        {
            StopCountdown();
            _countdownRemaining = Math.Max(1, seconds);
            lnkResend2.Enabled = false;
            UpdateCountdownText();

            _countdownTimer = new Timer { Interval = 1000 };
            _countdownTimer.Tick += (s, e) =>
            {
                _countdownRemaining--;
                if (_countdownRemaining <= 0)
                {
                    StopCountdown();
                    lnkResend2.Enabled = true;
                    lnkResend2.Text = Lang.Get("forgot.otp.resend");
                }
                else
                {
                    UpdateCountdownText();
                }
            };
            _countdownTimer.Start();
        }

        private void UpdateCountdownText()
        {
            lnkResend2.Text = Lang.Get("forgot.otp.resendCountdown", _countdownRemaining);
        }

        private void StopCountdown()
        {
            if (_countdownTimer != null)
            {
                _countdownTimer.Stop();
                _countdownTimer.Dispose();
                _countdownTimer = null;
            }
        }

        // ===================== STEP 3: New password =====================

        private void UpdatePasswordMatch()
        {
            string password = txtNewPassword3.Text;
            string confirm = txtConfirmPassword3.Text;

            if (confirm.Length == 0)
                ShowMatch3(" ", AppColors.TextMuted);
            else if (password == confirm)
                ShowMatch3(Lang.Get("forgot.password.match"), AppColors.Success);
            else
                ShowMatch3(Lang.Get("forgot.password.mismatch"), AppColors.Error);
        }

        private async Task ResetPasswordAsync()
        {
            if (_busy || _challengeId == null) return;

            string password = txtNewPassword3.Text;
            string confirm = txtConfirmPassword3.Text;

            var validation = PasswordResetService.ValidatePassword(password);
            if (validation != PasswordResetService.PasswordValidationStatus.Valid)
            {
                ShowMessage3(ValidationMessage(validation), AppColors.Error);
                return;
            }
            if (confirm.Length == 0)
            {
                ShowMessage3(Lang.Get("forgot.validation.confirm.required"), AppColors.Error);
                return;
            }
            if (password != confirm)
            {
                ShowMessage3(Lang.Get("forgot.validation.confirm.mismatch"), AppColors.Error);
                return;
            }

            SetBusy3(true);

            var result = await Task.Run(() => _resetService.ResetPassword(_challengeId, password));

            SetBusy3(false);

            switch (result.Status)
            {
                case PasswordResetService.ResetStatus.Success:
                    _completed = true;
                    string username = _pendingUsername;
                    ClearSensitiveFields();
                    MessageBox.Show(this,
                        Lang.Get("forgot.reset.success.message"),
                        Lang.Get("forgot.reset.success.title"),
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                    break;

                case PasswordResetService.ResetStatus.SameAsOldPassword:
                    ShowMessage3(Lang.Get("forgot.validation.samePassword"), AppColors.Error);
                    break;

                case PasswordResetService.ResetStatus.InvalidPassword:
                    ShowMessage3(ValidationMessage(result.ValidationStatus), AppColors.Error);
                    break;

                case PasswordResetService.ResetStatus.NotVerified:
                    ShowMessage3(Lang.Get("forgot.reset.notVerified"), AppColors.Error);
                    break;

                case PasswordResetService.ResetStatus.SessionExpired:
                case PasswordResetService.ResetStatus.NotFound:
                    ResetToIdentify(Lang.Get("forgot.reset.sessionExpired"));
                    break;

                case PasswordResetService.ResetStatus.AccountUnavailable:
                    ResetToIdentify(Lang.Get("forgot.reset.accountUnavailable"));
                    break;

                default:
                    ShowMessage3(Lang.Get("forgot.reset.failed"), AppColors.Error);
                    break;
            }
        }

        private void ResetToIdentify(string message)
        {
            _resetService.CancelChallenge(_challengeId);
            _challengeId = null;
            StopCountdown();
            txtOtp2.Text = "";
            ClearSensitiveFields();
            ShowStep(Step.Identify);
            ShowMessage1(message, AppColors.Error);
        }

        private void ClearSensitiveFields()
        {
            txtNewPassword3.Text = "";
            txtConfirmPassword3.Text = "";
        }

        private string ValidationMessage(PasswordResetService.PasswordValidationStatus status)
        {
            switch (status)
            {
                case PasswordResetService.PasswordValidationStatus.Required:
                    return Lang.Get("forgot.validation.password.required");
                case PasswordResetService.PasswordValidationStatus.Length:
                    return Lang.Get("forgot.validation.password.length");
                case PasswordResetService.PasswordValidationStatus.Letter:
                    return Lang.Get("forgot.validation.password.letter");
                case PasswordResetService.PasswordValidationStatus.Digit:
                    return Lang.Get("forgot.validation.password.digit");
                case PasswordResetService.PasswordValidationStatus.Whitespace:
                    return Lang.Get("forgot.validation.password.whitespace");
                case PasswordResetService.PasswordValidationStatus.ByteLength:
                    return Lang.Get("forgot.validation.password.byteLength");
                default:
                    return " ";
            }
        }

        private void ShowMessage3(string text, System.Drawing.Color color)
        {
            lblMessage3.ForeColor = color;
            lblMessage3.Text = text;
        }

        private void ShowMatch3(string text, System.Drawing.Color color)
        {
            lblMatch3.ForeColor = color;
            lblMatch3.Text = text;
        }

        private void SetBusy3(bool busy)
        {
            _busy = busy;
            btnReset3.Enabled = !busy;
            btnReset3.Text = busy ? Lang.Get("forgot.password.submitting") : Lang.Get("forgot.password.submit");
            Cursor = busy ? Cursors.WaitCursor : Cursors.Default;
        }

        // ===================== Điều hướng chung =====================

        private void ShowStep(Step step)
        {
            _currentStep = step;
            pnlStep1.Visible = step == Step.Identify;
            pnlStep2.Visible = step == Step.VerifyOtp;
            pnlStep3.Visible = step == Step.ResetPassword;

            switch (step)
            {
                case Step.Identify:
                    AcceptButton = btnSubmit1;
                    txtUsername1.FocusInput();
                    break;
                case Step.VerifyOtp:
                    AcceptButton = btnVerify2;
                    txtOtp2.FocusInput();
                    break;
                case Step.ResetPassword:
                    AcceptButton = btnReset3;
                    txtNewPassword3.FocusInput();
                    break;
            }
        }

        private void RequestClose()
        {
            if (_busy) return;

            if (_currentStep != Step.Identify && _challengeId != null)
            {
                var confirm = MessageBox.Show(this,
                    Lang.Get("forgot.close.confirm.message"),
                    Lang.Get("forgot.close.confirm.title"),
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm != DialogResult.Yes) return;
            }

            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FrmForgotPassword_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopCountdown();
            ClearSensitiveFields();
            if (!_completed && _challengeId != null)
            {
                _resetService.CancelChallenge(_challengeId);
                _challengeId = null;
            }
        }
    }
}