using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Controls;

namespace SIMS_WinFormsApp.Views.SystemManagement
{
    partial class frmMailConfig
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlCard;

        private Label lblTitle;
        private Label lblSubtitle;

        private Label lblSenderEmail;
        private RoundedTextBox txtSenderEmail;

        private Label lblAppPassword;
        private RoundedPasswordTextBox txtAppPassword;

        private Label lblHint;
        private ClickableLabel lnkHowTo;

        private Label lblMessage;

        private Panel pnlButtonsRow;
        private PrimaryButton btnTest;
        private PrimaryButton btnSave;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlCard = new System.Windows.Forms.Panel();
            this.pnlButtonsRow = new System.Windows.Forms.Panel();
            this.btnTest = new SIMS_WinFormsApp.UI.Controls.PrimaryButton();
            this.btnSave = new SIMS_WinFormsApp.UI.Controls.PrimaryButton();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lnkHowTo = new SIMS_WinFormsApp.UI.Controls.ClickableLabel();
            this.lblHint = new System.Windows.Forms.Label();
            this.txtAppPassword = new SIMS_WinFormsApp.UI.Controls.RoundedPasswordTextBox();
            this.lblAppPassword = new System.Windows.Forms.Label();
            this.txtSenderEmail = new SIMS_WinFormsApp.UI.Controls.RoundedTextBox();
            this.lblSenderEmail = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlCard.SuspendLayout();
            this.pnlButtonsRow.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCard
            // 
            this.pnlCard.BackColor = System.Drawing.Color.Transparent;
            this.pnlCard.Controls.Add(this.pnlButtonsRow);
            this.pnlCard.Controls.Add(this.lblMessage);
            this.pnlCard.Controls.Add(this.lnkHowTo);
            this.pnlCard.Controls.Add(this.lblHint);
            this.pnlCard.Controls.Add(this.txtAppPassword);
            this.pnlCard.Controls.Add(this.lblAppPassword);
            this.pnlCard.Controls.Add(this.txtSenderEmail);
            this.pnlCard.Controls.Add(this.lblSenderEmail);
            this.pnlCard.Controls.Add(this.lblSubtitle);
            this.pnlCard.Controls.Add(this.lblTitle);
            this.pnlCard.Location = new System.Drawing.Point(40, 30);
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.Size = new System.Drawing.Size(520, 540);
            this.pnlCard.TabIndex = 0;
            // 
            // pnlButtonsRow
            // 
            this.pnlButtonsRow.BackColor = System.Drawing.Color.Transparent;
            this.pnlButtonsRow.Controls.Add(this.btnTest);
            this.pnlButtonsRow.Controls.Add(this.btnSave);
            this.pnlButtonsRow.Location = new System.Drawing.Point(0, 466);
            this.pnlButtonsRow.Name = "pnlButtonsRow";
            this.pnlButtonsRow.Size = new System.Drawing.Size(520, 48);
            this.pnlButtonsRow.TabIndex = 0;
            // 
            // btnTest
            // 
            this.btnTest.BackColor = System.Drawing.Color.Transparent;
            this.btnTest.CornerRadius = 10;
            this.btnTest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTest.CustomAccentColor = null;
            this.btnTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTest.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnTest.ForeColor = System.Drawing.Color.White;
            this.btnTest.Icon = null;
            this.btnTest.IconSize = 16;
            this.btnTest.IsPrimary = false;
            this.btnTest.Location = new System.Drawing.Point(0, 0);
            this.btnTest.MinimumSize = new System.Drawing.Size(120, 42);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(160, 48);
            this.btnTest.TabIndex = 0;
            this.btnTest.Text = "Gửi thử";
            this.btnTest.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.CornerRadius = 10;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.CustomAccentColor = null;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Icon = null;
            this.btnSave.IconSize = 16;
            this.btnSave.IsPrimary = true;
            this.btnSave.Location = new System.Drawing.Point(172, 0);
            this.btnSave.MinimumSize = new System.Drawing.Size(120, 42);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(348, 48);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Lưu cấu hình";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // lblMessage
            // 
            this.lblMessage.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblMessage.Location = new System.Drawing.Point(0, 414);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(520, 36);
            this.lblMessage.TabIndex = 1;
            // 
            // lnkHowTo
            // 
            this.lnkHowTo.BackColor = System.Drawing.Color.Transparent;
            this.lnkHowTo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lnkHowTo.Font = new System.Drawing.Font("Segoe UI", 12.5F, System.Drawing.FontStyle.Bold);
            this.lnkHowTo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this.lnkHowTo.Location = new System.Drawing.Point(0, 370);
            this.lnkHowTo.Margin = new System.Windows.Forms.Padding(0);
            this.lnkHowTo.Name = "lnkHowTo";
            this.lnkHowTo.Size = new System.Drawing.Size(520, 32);
            this.lnkHowTo.TabIndex = 2;
            this.lnkHowTo.Text = "Cách tạo App Password ↗";
            this.lnkHowTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblHint
            // 
            this.lblHint.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblHint.Location = new System.Drawing.Point(0, 300);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(520, 64);
            this.lblHint.TabIndex = 3;
            this.lblHint.Text = "Đây không phải mật khẩu Gmail thường. Cần bật xác minh 2 bước rồi tạo App Passwor" +
    "d riêng cho ứng dụng.";
            // 
            // txtAppPassword
            // 
            this.txtAppPassword.BackColor = System.Drawing.Color.Transparent;
            this.txtAppPassword.CornerRadius = 10;
            this.txtAppPassword.DarkMode = false;
            this.txtAppPassword.HideTooltip = "Ẩn App Password";
            this.txtAppPassword.Location = new System.Drawing.Point(3, 245);
            this.txtAppPassword.MaxLength = 32;
            this.txtAppPassword.Name = "txtAppPassword";
            this.txtAppPassword.Padding = new System.Windows.Forms.Padding(14, 0, 14, 0);
            this.txtAppPassword.PlaceholderText = "Nhập App Password 16 ký tự";
            this.txtAppPassword.ShowTooltip = "Hiện App Password";
            this.txtAppPassword.Size = new System.Drawing.Size(514, 52);
            this.txtAppPassword.TabIndex = 4;
            // 
            // lblAppPassword
            // 
            this.lblAppPassword.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblAppPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblAppPassword.Location = new System.Drawing.Point(0, 195);
            this.lblAppPassword.Name = "lblAppPassword";
            this.lblAppPassword.Size = new System.Drawing.Size(520, 37);
            this.lblAppPassword.TabIndex = 5;
            this.lblAppPassword.Text = "App Password";
            this.lblAppPassword.Click += new System.EventHandler(this.lblAppPassword_Click);
            // 
            // txtSenderEmail
            // 
            this.txtSenderEmail.BackColor = System.Drawing.Color.Transparent;
            this.txtSenderEmail.CornerRadius = 10;
            this.txtSenderEmail.DarkMode = false;
            this.txtSenderEmail.Location = new System.Drawing.Point(0, 140);
            this.txtSenderEmail.MaxLength = 100;
            this.txtSenderEmail.Name = "txtSenderEmail";
            this.txtSenderEmail.Padding = new System.Windows.Forms.Padding(14, 0, 14, 0);
            this.txtSenderEmail.PlaceholderText = "vidu@gmail.com";
            this.txtSenderEmail.Size = new System.Drawing.Size(520, 52);
            this.txtSenderEmail.TabIndex = 6;
            // 
            // lblSenderEmail
            // 
            this.lblSenderEmail.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblSenderEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblSenderEmail.Location = new System.Drawing.Point(0, 100);
            this.lblSenderEmail.Name = "lblSenderEmail";
            this.lblSenderEmail.Size = new System.Drawing.Size(520, 36);
            this.lblSenderEmail.TabIndex = 7;
            this.lblSenderEmail.Text = "Địa chỉ Gmail";
            this.lblSenderEmail.Click += new System.EventHandler(this.lblSenderEmail_Click);
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSubtitle.Location = new System.Drawing.Point(0, 46);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(520, 54);
            this.lblSubtitle.TabIndex = 8;
            this.lblSubtitle.Text = "Dùng một tài khoản Gmail để hệ thống gửi mã OTP quên mật khẩu cho người dùng";
            this.lblSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTitle.Location = new System.Drawing.Point(0, -11);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(520, 53);
            this.lblTitle.TabIndex = 9;
            this.lblTitle.Text = "Cấu hình Email gửi OTP";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmMailConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(578, 564);
            this.Controls.Add(this.pnlCard);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(600, 620);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 620);
            this.Name = "frmMailConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cấu hình Email";
            this.pnlCard.ResumeLayout(false);
            this.pnlButtonsRow.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    }
}