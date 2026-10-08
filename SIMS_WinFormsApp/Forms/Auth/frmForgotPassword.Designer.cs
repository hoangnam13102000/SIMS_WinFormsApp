using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Controls;

namespace SIMS_WinFormsApp.Forms.Auth
{
    partial class frmForgotPassword
    {
        private System.ComponentModel.IContainer components = null;

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
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlStep1 = new System.Windows.Forms.Panel();
            this.lblStep1 = new System.Windows.Forms.Label();
            this.lblTitle1 = new System.Windows.Forms.Label();
            this.lblSubtitle1 = new System.Windows.Forms.Label();
            this.lblUsernameLabel1 = new System.Windows.Forms.Label();
            this.txtUsername1 = new SIMS_WinFormsApp.UI.Controls.RoundedTextBox();
            this.lblEmailLabel1 = new System.Windows.Forms.Label();
            this.txtEmail1 = new SIMS_WinFormsApp.UI.Controls.RoundedTextBox();
            this.lblMessage1 = new System.Windows.Forms.Label();
            this.btnSubmit1 = new SIMS_WinFormsApp.UI.Controls.PrimaryButton();
            this.lnkBack1 = new SIMS_WinFormsApp.UI.Controls.ClickableLabel();
            this.pnlStep2 = new System.Windows.Forms.Panel();
            this.lblStep2 = new System.Windows.Forms.Label();
            this.lblTitle2 = new System.Windows.Forms.Label();
            this.lblSentTo2 = new System.Windows.Forms.Label();
            this.lblOtpLabel2 = new System.Windows.Forms.Label();
            this.txtOtp2 = new SIMS_WinFormsApp.UI.Controls.RoundedTextBox();
            this.lblMessage2 = new System.Windows.Forms.Label();
            this.btnVerify2 = new SIMS_WinFormsApp.UI.Controls.PrimaryButton();
            this.lnkBack2 = new SIMS_WinFormsApp.UI.Controls.ClickableLabel();
            this.lnkResend2 = new SIMS_WinFormsApp.UI.Controls.ClickableLabel();
            this.pnlStep3 = new System.Windows.Forms.Panel();
            this.lblStep3 = new System.Windows.Forms.Label();
            this.lblTitle3 = new System.Windows.Forms.Label();
            this.lblNewPasswordLabel3 = new System.Windows.Forms.Label();
            this.txtNewPassword3 = new SIMS_WinFormsApp.UI.Controls.RoundedPasswordTextBox();
            this.lblConfirmPasswordLabel3 = new System.Windows.Forms.Label();
            this.txtConfirmPassword3 = new SIMS_WinFormsApp.UI.Controls.RoundedPasswordTextBox();
            this.lblRequirements3 = new System.Windows.Forms.Label();
            this.lblMatch3 = new System.Windows.Forms.Label();
            this.lblMessage3 = new System.Windows.Forms.Label();
            this.btnReset3 = new SIMS_WinFormsApp.UI.Controls.PrimaryButton();
            this.lnkCancel3 = new SIMS_WinFormsApp.UI.Controls.ClickableLabel();
            this.pnlStep1.SuspendLayout();
            this.pnlStep2.SuspendLayout();
            this.pnlStep3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlStep1
            // 
            this.pnlStep1.BackColor = System.Drawing.Color.White;
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
            this.pnlStep1.Location = new System.Drawing.Point(40, 40);
            this.pnlStep1.Name = "pnlStep1";
            this.pnlStep1.Size = new System.Drawing.Size(520, 680);
            this.pnlStep1.TabIndex = 0;
            // 
            // lblStep1
            // 
            this.lblStep1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblStep1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblStep1.Location = new System.Drawing.Point(0, 0);
            this.lblStep1.Name = "lblStep1";
            this.lblStep1.Size = new System.Drawing.Size(520, 30);
            this.lblStep1.TabIndex = 0;
            this.lblStep1.Text = "Bước 1/3";
            // 
            // lblTitle1
            // 
            this.lblTitle1.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblTitle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTitle1.Location = new System.Drawing.Point(0, 30);
            this.lblTitle1.Name = "lblTitle1";
            this.lblTitle1.Size = new System.Drawing.Size(520, 72);
            this.lblTitle1.TabIndex = 1;
            this.lblTitle1.Text = "Quên mật khẩu";
            // 
            // lblSubtitle1
            // 
            this.lblSubtitle1.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.lblSubtitle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSubtitle1.Location = new System.Drawing.Point(0, 106);
            this.lblSubtitle1.Name = "lblSubtitle1";
            this.lblSubtitle1.Size = new System.Drawing.Size(520, 52);
            this.lblSubtitle1.TabIndex = 2;
            this.lblSubtitle1.Text = "Nhập tài khoản và email đã đăng ký để nhận mã xác minh.";
            // 
            // lblUsernameLabel1
            // 
            this.lblUsernameLabel1.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblUsernameLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblUsernameLabel1.Location = new System.Drawing.Point(0, 166);
            this.lblUsernameLabel1.Name = "lblUsernameLabel1";
            this.lblUsernameLabel1.Size = new System.Drawing.Size(520, 32);
            this.lblUsernameLabel1.TabIndex = 3;
            this.lblUsernameLabel1.Text = "Tên đăng nhập";
            this.lblUsernameLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtUsername1
            // 
            this.txtUsername1.BackColor = System.Drawing.Color.Transparent;
            this.txtUsername1.CornerRadius = 10;
            this.txtUsername1.DarkMode = false;
            this.txtUsername1.Location = new System.Drawing.Point(0, 202);
            this.txtUsername1.MaxLength = 50;
            this.txtUsername1.Name = "txtUsername1";
            this.txtUsername1.Padding = new System.Windows.Forms.Padding(14, 0, 14, 0);
            this.txtUsername1.PlaceholderText = "";
            this.txtUsername1.Size = new System.Drawing.Size(520, 52);
            this.txtUsername1.TabIndex = 4;
            // 
            // lblEmailLabel1
            // 
            this.lblEmailLabel1.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblEmailLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblEmailLabel1.Location = new System.Drawing.Point(0, 270);
            this.lblEmailLabel1.Name = "lblEmailLabel1";
            this.lblEmailLabel1.Size = new System.Drawing.Size(520, 32);
            this.lblEmailLabel1.TabIndex = 5;
            this.lblEmailLabel1.Text = "Email";
            this.lblEmailLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtEmail1
            // 
            this.txtEmail1.BackColor = System.Drawing.Color.Transparent;
            this.txtEmail1.CornerRadius = 10;
            this.txtEmail1.DarkMode = false;
            this.txtEmail1.Location = new System.Drawing.Point(0, 306);
            this.txtEmail1.MaxLength = 100;
            this.txtEmail1.Name = "txtEmail1";
            this.txtEmail1.Padding = new System.Windows.Forms.Padding(14, 0, 14, 0);
            this.txtEmail1.PlaceholderText = "";
            this.txtEmail1.Size = new System.Drawing.Size(520, 52);
            this.txtEmail1.TabIndex = 6;
            // 
            // lblMessage1
            // 
            this.lblMessage1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblMessage1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblMessage1.Location = new System.Drawing.Point(0, 368);
            this.lblMessage1.Name = "lblMessage1";
            this.lblMessage1.Size = new System.Drawing.Size(520, 40);
            this.lblMessage1.TabIndex = 7;
            // 
            // btnSubmit1
            // 
            this.btnSubmit1.BackColor = System.Drawing.Color.Transparent;
            this.btnSubmit1.CornerRadius = 10;
            this.btnSubmit1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSubmit1.CustomAccentColor = null;
            this.btnSubmit1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmit1.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnSubmit1.ForeColor = System.Drawing.Color.White;
            this.btnSubmit1.Icon = null;
            this.btnSubmit1.IconSize = 16;
            this.btnSubmit1.IsPrimary = true;
            this.btnSubmit1.Location = new System.Drawing.Point(0, 412);
            this.btnSubmit1.MinimumSize = new System.Drawing.Size(120, 42);
            this.btnSubmit1.Name = "btnSubmit1";
            this.btnSubmit1.Size = new System.Drawing.Size(520, 46);
            this.btnSubmit1.TabIndex = 8;
            this.btnSubmit1.Text = "Gửi mã xác minh";
            this.btnSubmit1.UseVisualStyleBackColor = false;
            // 
            // lnkBack1
            // 
            this.lnkBack1.BackColor = System.Drawing.Color.Transparent;
            this.lnkBack1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lnkBack1.Font = new System.Drawing.Font("Segoe UI", 12.5F, System.Drawing.FontStyle.Bold);
            this.lnkBack1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this.lnkBack1.Location = new System.Drawing.Point(134, 472);
            this.lnkBack1.Margin = new System.Windows.Forms.Padding(0);
            this.lnkBack1.Name = "lnkBack1";
            this.lnkBack1.Size = new System.Drawing.Size(291, 32);
            this.lnkBack1.TabIndex = 9;
            this.lnkBack1.Text = "Quay lại đăng nhập";
            this.lnkBack1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlStep2
            // 
            this.pnlStep2.BackColor = System.Drawing.Color.White;
            this.pnlStep2.Controls.Add(this.lblStep2);
            this.pnlStep2.Controls.Add(this.lblTitle2);
            this.pnlStep2.Controls.Add(this.lblSentTo2);
            this.pnlStep2.Controls.Add(this.lblOtpLabel2);
            this.pnlStep2.Controls.Add(this.txtOtp2);
            this.pnlStep2.Controls.Add(this.lblMessage2);
            this.pnlStep2.Controls.Add(this.btnVerify2);
            this.pnlStep2.Controls.Add(this.lnkBack2);
            this.pnlStep2.Controls.Add(this.lnkResend2);
            this.pnlStep2.Location = new System.Drawing.Point(40, 40);
            this.pnlStep2.Name = "pnlStep2";
            this.pnlStep2.Size = new System.Drawing.Size(520, 680);
            this.pnlStep2.TabIndex = 1;
            this.pnlStep2.Visible = false;
            // 
            // lblStep2
            // 
            this.lblStep2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblStep2.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblStep2.Location = new System.Drawing.Point(0, 0);
            this.lblStep2.Name = "lblStep2";
            this.lblStep2.Size = new System.Drawing.Size(520, 24);
            this.lblStep2.TabIndex = 0;
            this.lblStep2.Text = "Bước 2/3";
            // 
            // lblTitle2
            // 
            this.lblTitle2.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblTitle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTitle2.Location = new System.Drawing.Point(0, 30);
            this.lblTitle2.Name = "lblTitle2";
            this.lblTitle2.Size = new System.Drawing.Size(520, 72);
            this.lblTitle2.TabIndex = 1;
            this.lblTitle2.Text = "Xác minh email";
            // 
            // lblSentTo2
            // 
            this.lblSentTo2.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.lblSentTo2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSentTo2.Location = new System.Drawing.Point(0, 106);
            this.lblSentTo2.Name = "lblSentTo2";
            this.lblSentTo2.Size = new System.Drawing.Size(520, 40);
            this.lblSentTo2.TabIndex = 2;
            // 
            // lblOtpLabel2
            // 
            this.lblOtpLabel2.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblOtpLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblOtpLabel2.Location = new System.Drawing.Point(0, 158);
            this.lblOtpLabel2.Name = "lblOtpLabel2";
            this.lblOtpLabel2.Size = new System.Drawing.Size(520, 32);
            this.lblOtpLabel2.TabIndex = 3;
            this.lblOtpLabel2.Text = "Mã xác minh";
            this.lblOtpLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtOtp2
            // 
            this.txtOtp2.BackColor = System.Drawing.Color.Transparent;
            this.txtOtp2.CornerRadius = 10;
            this.txtOtp2.DarkMode = false;
            this.txtOtp2.Location = new System.Drawing.Point(0, 194);
            this.txtOtp2.MaxLength = 6;
            this.txtOtp2.Name = "txtOtp2";
            this.txtOtp2.Padding = new System.Windows.Forms.Padding(14, 0, 14, 0);
            this.txtOtp2.PlaceholderText = "";
            this.txtOtp2.Size = new System.Drawing.Size(520, 52);
            this.txtOtp2.TabIndex = 4;
            // 
            // lblMessage2
            // 
            this.lblMessage2.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblMessage2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblMessage2.Location = new System.Drawing.Point(0, 256);
            this.lblMessage2.Name = "lblMessage2";
            this.lblMessage2.Size = new System.Drawing.Size(520, 40);
            this.lblMessage2.TabIndex = 5;
            // 
            // btnVerify2
            // 
            this.btnVerify2.BackColor = System.Drawing.Color.Transparent;
            this.btnVerify2.CornerRadius = 10;
            this.btnVerify2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVerify2.CustomAccentColor = null;
            this.btnVerify2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerify2.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnVerify2.ForeColor = System.Drawing.Color.White;
            this.btnVerify2.Icon = null;
            this.btnVerify2.IconSize = 16;
            this.btnVerify2.IsPrimary = true;
            this.btnVerify2.Location = new System.Drawing.Point(0, 300);
            this.btnVerify2.MinimumSize = new System.Drawing.Size(120, 42);
            this.btnVerify2.Name = "btnVerify2";
            this.btnVerify2.Size = new System.Drawing.Size(520, 46);
            this.btnVerify2.TabIndex = 6;
            this.btnVerify2.Text = "Xác minh mã";
            this.btnVerify2.UseVisualStyleBackColor = false;
            // 
            // lnkBack2
            // 
            this.lnkBack2.BackColor = System.Drawing.Color.Transparent;
            this.lnkBack2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lnkBack2.Font = new System.Drawing.Font("Segoe UI", 12.5F, System.Drawing.FontStyle.Bold);
            this.lnkBack2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this.lnkBack2.Location = new System.Drawing.Point(110, 360);
            this.lnkBack2.Margin = new System.Windows.Forms.Padding(0);
            this.lnkBack2.Name = "lnkBack2";
            this.lnkBack2.Size = new System.Drawing.Size(150, 32);
            this.lnkBack2.TabIndex = 7;
            this.lnkBack2.Text = "Quay lại";
            this.lnkBack2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lnkResend2
            // 
            this.lnkResend2.BackColor = System.Drawing.Color.Transparent;
            this.lnkResend2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lnkResend2.Font = new System.Drawing.Font("Segoe UI", 12.5F, System.Drawing.FontStyle.Bold);
            this.lnkResend2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this.lnkResend2.Location = new System.Drawing.Point(260, 360);
            this.lnkResend2.Margin = new System.Windows.Forms.Padding(0);
            this.lnkResend2.Name = "lnkResend2";
            this.lnkResend2.Size = new System.Drawing.Size(150, 32);
            this.lnkResend2.TabIndex = 8;
            this.lnkResend2.Text = "Gửi lại mã";
            this.lnkResend2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlStep3
            // 
            this.pnlStep3.BackColor = System.Drawing.Color.White;
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
            this.pnlStep3.Location = new System.Drawing.Point(40, 40);
            this.pnlStep3.Name = "pnlStep3";
            this.pnlStep3.Size = new System.Drawing.Size(520, 680);
            this.pnlStep3.TabIndex = 2;
            this.pnlStep3.Visible = false;
            // 
            // lblStep3
            // 
            this.lblStep3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblStep3.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblStep3.Location = new System.Drawing.Point(0, 0);
            this.lblStep3.Name = "lblStep3";
            this.lblStep3.Size = new System.Drawing.Size(520, 24);
            this.lblStep3.TabIndex = 0;
            this.lblStep3.Text = "Bước 3/3";
            // 
            // lblTitle3
            // 
            this.lblTitle3.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblTitle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTitle3.Location = new System.Drawing.Point(0, 30);
            this.lblTitle3.Name = "lblTitle3";
            this.lblTitle3.Size = new System.Drawing.Size(520, 72);
            this.lblTitle3.TabIndex = 1;
            this.lblTitle3.Text = "Tạo mật khẩu mới";
            // 
            // lblNewPasswordLabel3
            // 
            this.lblNewPasswordLabel3.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblNewPasswordLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblNewPasswordLabel3.Location = new System.Drawing.Point(0, 106);
            this.lblNewPasswordLabel3.Name = "lblNewPasswordLabel3";
            this.lblNewPasswordLabel3.Size = new System.Drawing.Size(520, 32);
            this.lblNewPasswordLabel3.TabIndex = 2;
            this.lblNewPasswordLabel3.Text = "Mật khẩu mới";
            this.lblNewPasswordLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtNewPassword3
            // 
            this.txtNewPassword3.BackColor = System.Drawing.Color.Transparent;
            this.txtNewPassword3.CornerRadius = 10;
            this.txtNewPassword3.DarkMode = false;
            this.txtNewPassword3.HideTooltip = "Ẩn mật khẩu";
            this.txtNewPassword3.Location = new System.Drawing.Point(0, 142);
            this.txtNewPassword3.MaxLength = 72;
            this.txtNewPassword3.Name = "txtNewPassword3";
            this.txtNewPassword3.Padding = new System.Windows.Forms.Padding(14, 0, 14, 0);
            this.txtNewPassword3.PlaceholderText = "";
            this.txtNewPassword3.ShowTooltip = "Hiện mật khẩu";
            this.txtNewPassword3.Size = new System.Drawing.Size(520, 52);
            this.txtNewPassword3.TabIndex = 3;
            // 
            // lblConfirmPasswordLabel3
            // 
            this.lblConfirmPasswordLabel3.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblConfirmPasswordLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblConfirmPasswordLabel3.Location = new System.Drawing.Point(0, 210);
            this.lblConfirmPasswordLabel3.Name = "lblConfirmPasswordLabel3";
            this.lblConfirmPasswordLabel3.Size = new System.Drawing.Size(520, 32);
            this.lblConfirmPasswordLabel3.TabIndex = 4;
            this.lblConfirmPasswordLabel3.Text = "Xác nhận mật khẩu";
            this.lblConfirmPasswordLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtConfirmPassword3
            // 
            this.txtConfirmPassword3.BackColor = System.Drawing.Color.Transparent;
            this.txtConfirmPassword3.CornerRadius = 10;
            this.txtConfirmPassword3.DarkMode = false;
            this.txtConfirmPassword3.HideTooltip = "Ẩn mật khẩu";
            this.txtConfirmPassword3.Location = new System.Drawing.Point(0, 246);
            this.txtConfirmPassword3.MaxLength = 72;
            this.txtConfirmPassword3.Name = "txtConfirmPassword3";
            this.txtConfirmPassword3.Padding = new System.Windows.Forms.Padding(14, 0, 14, 0);
            this.txtConfirmPassword3.PlaceholderText = "";
            this.txtConfirmPassword3.ShowTooltip = "Hiện mật khẩu";
            this.txtConfirmPassword3.Size = new System.Drawing.Size(520, 52);
            this.txtConfirmPassword3.TabIndex = 5;
            // 
            // lblRequirements3
            // 
            this.lblRequirements3.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblRequirements3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblRequirements3.Location = new System.Drawing.Point(0, 306);
            this.lblRequirements3.Name = "lblRequirements3";
            this.lblRequirements3.Size = new System.Drawing.Size(520, 32);
            this.lblRequirements3.TabIndex = 6;
            // 
            // lblMatch3
            // 
            this.lblMatch3.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblMatch3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblMatch3.Location = new System.Drawing.Point(0, 342);
            this.lblMatch3.Name = "lblMatch3";
            this.lblMatch3.Size = new System.Drawing.Size(520, 20);
            this.lblMatch3.TabIndex = 7;
            // 
            // lblMessage3
            // 
            this.lblMessage3.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblMessage3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblMessage3.Location = new System.Drawing.Point(0, 368);
            this.lblMessage3.Name = "lblMessage3";
            this.lblMessage3.Size = new System.Drawing.Size(520, 40);
            this.lblMessage3.TabIndex = 8;
            // 
            // btnReset3
            // 
            this.btnReset3.BackColor = System.Drawing.Color.Transparent;
            this.btnReset3.CornerRadius = 10;
            this.btnReset3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReset3.CustomAccentColor = null;
            this.btnReset3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset3.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnReset3.ForeColor = System.Drawing.Color.White;
            this.btnReset3.Icon = null;
            this.btnReset3.IconSize = 16;
            this.btnReset3.IsPrimary = true;
            this.btnReset3.Location = new System.Drawing.Point(0, 412);
            this.btnReset3.MinimumSize = new System.Drawing.Size(120, 42);
            this.btnReset3.Name = "btnReset3";
            this.btnReset3.Size = new System.Drawing.Size(520, 46);
            this.btnReset3.TabIndex = 9;
            this.btnReset3.Text = "Đổi mật khẩu";
            this.btnReset3.UseVisualStyleBackColor = false;
            // 
            // lnkCancel3
            // 
            this.lnkCancel3.BackColor = System.Drawing.Color.Transparent;
            this.lnkCancel3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lnkCancel3.Font = new System.Drawing.Font("Segoe UI", 12.5F, System.Drawing.FontStyle.Bold);
            this.lnkCancel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this.lnkCancel3.Location = new System.Drawing.Point(185, 472);
            this.lnkCancel3.Margin = new System.Windows.Forms.Padding(0);
            this.lnkCancel3.Name = "lnkCancel3";
            this.lnkCancel3.Size = new System.Drawing.Size(150, 32);
            this.lnkCancel3.TabIndex = 10;
            this.lnkCancel3.Text = "Quay lại đăng nhập";
            this.lnkCancel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmForgotPassword
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(600, 760);
            this.Controls.Add(this.pnlStep1);
            this.Controls.Add(this.pnlStep2);
            this.Controls.Add(this.pnlStep3);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmForgotPassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quên mật khẩu - SIMS";
            this.pnlStep1.ResumeLayout(false);
            this.pnlStep2.ResumeLayout(false);
            this.pnlStep3.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
