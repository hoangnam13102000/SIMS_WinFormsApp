using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Controls;

namespace SIMS_WinFormsApp.Forms.Auth
{
    partial class frmForgotPassword
    {
        private System.ComponentModel.IContainer components = null;

        private const int ContentWidth = 400;

        // ===== Step 1: Identify =====
        private Panel pnlStep1;
        private Label lblStep1;
        private Label lblTitle1;
        private Label lblSubtitle1;
        private Label lblUsernameLabel1;
        private RoundedTextBox txtUsername1;
        private Label lblEmailLabel1;
        private RoundedTextBox txtEmail1;
        private Label lblMessage1;
        private PrimaryButton btnSubmit1;
        private ClickableLabel lnkBack1;

        // ===== Step 2: OTP =====
        private Panel pnlStep2;
        private Label lblStep2;
        private Label lblTitle2;
        private Label lblSentTo2;
        private Label lblOtpLabel2;
        private RoundedTextBox txtOtp2;
        private Label lblMessage2;
        private PrimaryButton btnVerify2;
        private ClickableLabel lnkBack2;
        private ClickableLabel lnkResend2;

        // ===== Step 3: New password =====
        private Panel pnlStep3;
        private Label lblStep3;
        private Label lblTitle3;
        private Label lblNewPasswordLabel3;
        private RoundedPasswordTextBox txtNewPassword3;
        private Label lblConfirmPasswordLabel3;
        private RoundedPasswordTextBox txtConfirmPassword3;
        private Label lblRequirements3;
        private Label lblMatch3;
        private Label lblMessage3;
        private PrimaryButton btnReset3;
        private ClickableLabel lnkCancel3;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.pnlStep1 = new Panel();
            this.lblStep1 = new Label();
            this.lblTitle1 = new Label();
            this.lblSubtitle1 = new Label();
            this.lblUsernameLabel1 = new Label();
            this.txtUsername1 = new RoundedTextBox();
            this.lblEmailLabel1 = new Label();
            this.txtEmail1 = new RoundedTextBox();
            this.lblMessage1 = new Label();
            this.btnSubmit1 = new PrimaryButton();
            this.lnkBack1 = new ClickableLabel();

            this.pnlStep2 = new Panel();
            this.lblStep2 = new Label();
            this.lblTitle2 = new Label();
            this.lblSentTo2 = new Label();
            this.lblOtpLabel2 = new Label();
            this.txtOtp2 = new RoundedTextBox();
            this.lblMessage2 = new Label();
            this.btnVerify2 = new PrimaryButton();
            this.lnkBack2 = new ClickableLabel();
            this.lnkResend2 = new ClickableLabel();

            this.pnlStep3 = new Panel();
            this.lblStep3 = new Label();
            this.lblTitle3 = new Label();
            this.lblNewPasswordLabel3 = new Label();
            this.txtNewPassword3 = new RoundedPasswordTextBox();
            this.lblConfirmPasswordLabel3 = new Label();
            this.txtConfirmPassword3 = new RoundedPasswordTextBox();
            this.lblRequirements3 = new Label();
            this.lblMatch3 = new Label();
            this.lblMessage3 = new Label();
            this.btnReset3 = new PrimaryButton();
            this.lnkCancel3 = new ClickableLabel();

            this.SuspendLayout();

            // ===== frmForgotPassword =====
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(480, 640);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Text = "Quên mật khẩu - SIMS";
            this.Font = new Font("Segoe UI", 9F);
            this.BackColor = SIMS_WinFormsApp.UI.Theme.AppColors.White;

            const int left = 40;
            const int top = 40;

            // ================= STEP 1 =================
            this.pnlStep1.Location = new Point(left, top);
            this.pnlStep1.Size = new Size(ContentWidth, 540);
            this.pnlStep1.BackColor = SIMS_WinFormsApp.UI.Theme.AppColors.White;

            int y1 = 0;
            this.lblStep1.AutoSize = false;
            this.lblStep1.Location = new Point(0, y1);
            this.lblStep1.Size = new Size(ContentWidth, 18);
            this.lblStep1.Font = SIMS_WinFormsApp.UI.Theme.AppFonts.SmallBold;
            this.lblStep1.ForeColor = SIMS_WinFormsApp.UI.Theme.AppColors.Accent;
            y1 += 24;

            this.lblTitle1.AutoSize = false;
            this.lblTitle1.Location = new Point(0, y1);
            this.lblTitle1.Size = new Size(ContentWidth, 40);
            this.lblTitle1.Font = SIMS_WinFormsApp.UI.Theme.AppFonts.Title;
            this.lblTitle1.ForeColor = SIMS_WinFormsApp.UI.Theme.AppColors.TextTitle;
            y1 += 44;

            this.lblSubtitle1.AutoSize = false;
            this.lblSubtitle1.Location = new Point(0, y1);
            this.lblSubtitle1.Size = new Size(ContentWidth, 40);
            this.lblSubtitle1.Font = SIMS_WinFormsApp.UI.Theme.AppFonts.Body;
            this.lblSubtitle1.ForeColor = SIMS_WinFormsApp.UI.Theme.AppColors.TextMuted;
            y1 += 52;

            this.lblUsernameLabel1.AutoSize = false;
            this.lblUsernameLabel1.Location = new Point(0, y1);
            this.lblUsernameLabel1.Size = new Size(ContentWidth, 20);
            this.lblUsernameLabel1.Font = SIMS_WinFormsApp.UI.Theme.AppFonts.BodyBold;
            this.lblUsernameLabel1.ForeColor = SIMS_WinFormsApp.UI.Theme.AppColors.TextPrimary;
            y1 += 24;

            this.txtUsername1.Location = new Point(0, y1);
            this.txtUsername1.Size = new Size(ContentWidth, 44);
            this.txtUsername1.MaxLength = 50;
            y1 += 44 + 16;

            this.lblEmailLabel1.AutoSize = false;
            this.lblEmailLabel1.Location = new Point(0, y1);
            this.lblEmailLabel1.Size = new Size(ContentWidth, 20);
            this.lblEmailLabel1.Font = SIMS_WinFormsApp.UI.Theme.AppFonts.BodyBold;
            this.lblEmailLabel1.ForeColor = SIMS_WinFormsApp.UI.Theme.AppColors.TextPrimary;
            y1 += 24;

            this.txtEmail1.Location = new Point(0, y1);
            this.txtEmail1.Size = new Size(ContentWidth, 44);
            this.txtEmail1.MaxLength = 100;
            y1 += 44 + 10;

            this.lblMessage1.AutoSize = false;
            this.lblMessage1.Location = new Point(0, y1);
            this.lblMessage1.Size = new Size(ContentWidth, 40);
            this.lblMessage1.Font = SIMS_WinFormsApp.UI.Theme.AppFonts.Small;
            this.lblMessage1.ForeColor = SIMS_WinFormsApp.UI.Theme.AppColors.TextMuted;
            y1 += 44;

            this.btnSubmit1.Location = new Point(0, y1);
            this.btnSubmit1.Size = new Size(ContentWidth, 46);
            this.btnSubmit1.IsPrimary = true;
            y1 += 46 + 14;

            this.lnkBack1.Location = new Point((ContentWidth - 150) / 2, y1);

            this.pnlStep1.Controls.Add(this.lblStep1);
            this.pnlStep1.Controls.Add(this.lblTitle1);
            this.pnlStep1.Controls.Add(this.lblSubtitle1);
            this.pnlStep1.Controls.Add(this.lblUsernameLabel1);
            this.pnlStep1.Controls.Add(this.txtUsername1);
            this.pnlStep1.Controls.Add(this.lblEmailLabel1);
            this.pnlStep1.Controls.Add(this.txtEmail1);
            this.pnlStep1.Controls.Add(this.lblMessage1);
            this.pnlStep1.Controls.Add(this.btnSubmit1);
            this.pnlStep1.Controls.Add(this.lnkBack1);

            // ================= STEP 2 =================
            this.pnlStep2.Location = new Point(left, top);
            this.pnlStep2.Size = new Size(ContentWidth, 540);
            this.pnlStep2.BackColor = SIMS_WinFormsApp.UI.Theme.AppColors.White;
            this.pnlStep2.Visible = false;

            int y2 = 0;
            this.lblStep2.AutoSize = false;
            this.lblStep2.Location = new Point(0, y2);
            this.lblStep2.Size = new Size(ContentWidth, 18);
            this.lblStep2.Font = SIMS_WinFormsApp.UI.Theme.AppFonts.SmallBold;
            this.lblStep2.ForeColor = SIMS_WinFormsApp.UI.Theme.AppColors.Accent;
            y2 += 24;

            this.lblTitle2.AutoSize = false;
            this.lblTitle2.Location = new Point(0, y2);
            this.lblTitle2.Size = new Size(ContentWidth, 40);
            this.lblTitle2.Font = SIMS_WinFormsApp.UI.Theme.AppFonts.Title;
            this.lblTitle2.ForeColor = SIMS_WinFormsApp.UI.Theme.AppColors.TextTitle;
            y2 += 44;

            this.lblSentTo2.AutoSize = false;
            this.lblSentTo2.Location = new Point(0, y2);
            this.lblSentTo2.Size = new Size(ContentWidth, 40);
            this.lblSentTo2.Font = SIMS_WinFormsApp.UI.Theme.AppFonts.Body;
            this.lblSentTo2.ForeColor = SIMS_WinFormsApp.UI.Theme.AppColors.TextMuted;
            y2 += 52;

            this.lblOtpLabel2.AutoSize = false;
            this.lblOtpLabel2.Location = new Point(0, y2);
            this.lblOtpLabel2.Size = new Size(ContentWidth, 20);
            this.lblOtpLabel2.Font = SIMS_WinFormsApp.UI.Theme.AppFonts.BodyBold;
            this.lblOtpLabel2.ForeColor = SIMS_WinFormsApp.UI.Theme.AppColors.TextPrimary;
            y2 += 24;

            this.txtOtp2.Location = new Point(0, y2);
            this.txtOtp2.Size = new Size(ContentWidth, 50);
            this.txtOtp2.MaxLength = 6;
            y2 += 50 + 10;

            this.lblMessage2.AutoSize = false;
            this.lblMessage2.Location = new Point(0, y2);
            this.lblMessage2.Size = new Size(ContentWidth, 40);
            this.lblMessage2.Font = SIMS_WinFormsApp.UI.Theme.AppFonts.Small;
            this.lblMessage2.ForeColor = SIMS_WinFormsApp.UI.Theme.AppColors.TextMuted;
            y2 += 44;

            this.btnVerify2.Location = new Point(0, y2);
            this.btnVerify2.Size = new Size(ContentWidth, 46);
            this.btnVerify2.IsPrimary = true;
            y2 += 46 + 14;

            this.lnkBack2.Location = new Point(ContentWidth / 2 - 130, y2);
            this.lnkResend2.Location = new Point(ContentWidth / 2 + 10, y2);

            this.pnlStep2.Controls.Add(this.lblStep2);
            this.pnlStep2.Controls.Add(this.lblTitle2);
            this.pnlStep2.Controls.Add(this.lblSentTo2);
            this.pnlStep2.Controls.Add(this.lblOtpLabel2);
            this.pnlStep2.Controls.Add(this.txtOtp2);
            this.pnlStep2.Controls.Add(this.lblMessage2);
            this.pnlStep2.Controls.Add(this.btnVerify2);
            this.pnlStep2.Controls.Add(this.lnkBack2);
            this.pnlStep2.Controls.Add(this.lnkResend2);

            // ================= STEP 3 =================
            this.pnlStep3.Location = new Point(left, top);
            this.pnlStep3.Size = new Size(ContentWidth, 540);
            this.pnlStep3.BackColor = SIMS_WinFormsApp.UI.Theme.AppColors.White;
            this.pnlStep3.Visible = false;

            int y3 = 0;
            this.lblStep3.AutoSize = false;
            this.lblStep3.Location = new Point(0, y3);
            this.lblStep3.Size = new Size(ContentWidth, 18);
            this.lblStep3.Font = SIMS_WinFormsApp.UI.Theme.AppFonts.SmallBold;
            this.lblStep3.ForeColor = SIMS_WinFormsApp.UI.Theme.AppColors.Accent;
            y3 += 24;

            this.lblTitle3.AutoSize = false;
            this.lblTitle3.Location = new Point(0, y3);
            this.lblTitle3.Size = new Size(ContentWidth, 40);
            this.lblTitle3.Font = SIMS_WinFormsApp.UI.Theme.AppFonts.Title;
            this.lblTitle3.ForeColor = SIMS_WinFormsApp.UI.Theme.AppColors.TextTitle;
            y3 += 44;

            this.lblNewPasswordLabel3.AutoSize = false;
            this.lblNewPasswordLabel3.Location = new Point(0, y3);
            this.lblNewPasswordLabel3.Size = new Size(ContentWidth, 20);
            this.lblNewPasswordLabel3.Font = SIMS_WinFormsApp.UI.Theme.AppFonts.BodyBold;
            this.lblNewPasswordLabel3.ForeColor = SIMS_WinFormsApp.UI.Theme.AppColors.TextPrimary;
            y3 += 24;

            this.txtNewPassword3.Location = new Point(0, y3);
            this.txtNewPassword3.Size = new Size(ContentWidth, 44);
            this.txtNewPassword3.MaxLength = 72;
            y3 += 44 + 16;

            this.lblConfirmPasswordLabel3.AutoSize = false;
            this.lblConfirmPasswordLabel3.Location = new Point(0, y3);
            this.lblConfirmPasswordLabel3.Size = new Size(ContentWidth, 20);
            this.lblConfirmPasswordLabel3.Font = SIMS_WinFormsApp.UI.Theme.AppFonts.BodyBold;
            this.lblConfirmPasswordLabel3.ForeColor = SIMS_WinFormsApp.UI.Theme.AppColors.TextPrimary;
            y3 += 24;

            this.txtConfirmPassword3.Location = new Point(0, y3);
            this.txtConfirmPassword3.Size = new Size(ContentWidth, 44);
            this.txtConfirmPassword3.MaxLength = 72;
            y3 += 44 + 8;

            this.lblRequirements3.AutoSize = false;
            this.lblRequirements3.Location = new Point(0, y3);
            this.lblRequirements3.Size = new Size(ContentWidth, 32);
            this.lblRequirements3.Font = SIMS_WinFormsApp.UI.Theme.AppFonts.Small;
            this.lblRequirements3.ForeColor = SIMS_WinFormsApp.UI.Theme.AppColors.TextMuted;
            y3 += 34;

            this.lblMatch3.AutoSize = false;
            this.lblMatch3.Location = new Point(0, y3);
            this.lblMatch3.Size = new Size(ContentWidth, 20);
            this.lblMatch3.Font = SIMS_WinFormsApp.UI.Theme.AppFonts.Small;
            this.lblMatch3.ForeColor = SIMS_WinFormsApp.UI.Theme.AppColors.TextMuted;
            y3 += 22;

            this.lblMessage3.AutoSize = false;
            this.lblMessage3.Location = new Point(0, y3);
            this.lblMessage3.Size = new Size(ContentWidth, 40);
            this.lblMessage3.Font = SIMS_WinFormsApp.UI.Theme.AppFonts.Small;
            this.lblMessage3.ForeColor = SIMS_WinFormsApp.UI.Theme.AppColors.TextMuted;
            y3 += 44;

            this.btnReset3.Location = new Point(0, y3);
            this.btnReset3.Size = new Size(ContentWidth, 46);
            this.btnReset3.IsPrimary = true;
            y3 += 46 + 14;

            this.lnkCancel3.Location = new Point((ContentWidth - 150) / 2, y3);

            this.pnlStep3.Controls.Add(this.lblStep3);
            this.pnlStep3.Controls.Add(this.lblTitle3);
            this.pnlStep3.Controls.Add(this.lblNewPasswordLabel3);
            this.pnlStep3.Controls.Add(this.txtNewPassword3);
            this.pnlStep3.Controls.Add(this.lblConfirmPasswordLabel3);
            this.pnlStep3.Controls.Add(this.txtConfirmPassword3);
            this.pnlStep3.Controls.Add(this.lblRequirements3);
            this.pnlStep3.Controls.Add(this.lblMatch3);
            this.pnlStep3.Controls.Add(this.lblMessage3);
            this.pnlStep3.Controls.Add(this.btnReset3);
            this.pnlStep3.Controls.Add(this.lnkCancel3);

            this.Controls.Add(this.pnlStep1);
            this.Controls.Add(this.pnlStep2);
            this.Controls.Add(this.pnlStep3);

            this.ResumeLayout(false);
        }
    }
}