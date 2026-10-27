using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Views.Auth
{
    partial class frmLogin
    {
        private System.ComponentModel.IContainer components = null;

        private Panel brandPanel;
        private Guna2Panel pnlRight;
        private TableLayoutPanel pnlAuthSplit;
        private Guna2Panel pnlFormCard;
        private Guna2Panel pnlUsernameInput;
        private Guna2Panel pnlPasswordInput;

        private Label lblTitle;
        private Label lblSubtitle;

        private Label lblUsername;
        private TextBox txtUsername;

        private Label lblPassword;
        private TextBox txtPassword;

        private Panel pnlOptionsRow;
        private CheckBox chkRemember;
        private LinkLabel lnkForgotPassword;

        private Label lblError;
        private PrimaryButton btnLogin;

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
            this.pnlRight = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlFormCard = new Guna.UI2.WinForms.Guna2Panel();
            this.btnLogin = new SIMS_WinFormsApp.UI.Controls.PrimaryButton();
            this.lblError = new System.Windows.Forms.Label();
            this.pnlOptionsRow = new System.Windows.Forms.Panel();
            this.chkRemember = new System.Windows.Forms.CheckBox();
            this.lnkForgotPassword = new System.Windows.Forms.LinkLabel();
            this.pnlPasswordInput = new Guna.UI2.WinForms.Guna2Panel();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.pnlUsernameInput = new Guna.UI2.WinForms.Guna2Panel();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlAuthSplit = new System.Windows.Forms.TableLayoutPanel();
            this.brandPanel = new System.Windows.Forms.Panel();
            this.pnlRight.SuspendLayout();
            this.pnlFormCard.SuspendLayout();
            this.pnlOptionsRow.SuspendLayout();
            this.pnlPasswordInput.SuspendLayout();
            this.pnlUsernameInput.SuspendLayout();
            this.pnlAuthSplit.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.White;
            this.pnlRight.Controls.Add(this.pnlFormCard);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.FillColor = System.Drawing.Color.White;
            this.pnlRight.Location = new System.Drawing.Point(480, 0);
            this.pnlRight.Margin = new System.Windows.Forms.Padding(0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(720, 760);
            this.pnlRight.TabIndex = 1;
            // 
            // pnlFormCard
            // 
            this.pnlFormCard.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlFormCard.BackColor = System.Drawing.Color.Transparent;
            this.pnlFormCard.Controls.Add(this.btnLogin);
            this.pnlFormCard.Controls.Add(this.lblError);
            this.pnlFormCard.Controls.Add(this.pnlOptionsRow);
            this.pnlFormCard.Controls.Add(this.pnlPasswordInput);
            this.pnlFormCard.Controls.Add(this.lblPassword);
            this.pnlFormCard.Controls.Add(this.pnlUsernameInput);
            this.pnlFormCard.Controls.Add(this.lblUsername);
            this.pnlFormCard.Controls.Add(this.lblSubtitle);
            this.pnlFormCard.Controls.Add(this.lblTitle);
            this.pnlFormCard.FillColor = System.Drawing.Color.Transparent;
            this.pnlFormCard.Location = new System.Drawing.Point(127, 100);
            this.pnlFormCard.Name = "pnlFormCard";
            this.pnlFormCard.Size = new System.Drawing.Size(475, 560);
            this.pnlFormCard.TabIndex = 0;
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.Transparent;
            this.btnLogin.CornerRadius = 10;
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.CustomAccentColor = null;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Icon = null;
            this.btnLogin.IconSize = 16;
            this.btnLogin.IsPrimary = true;
            this.btnLogin.Location = new System.Drawing.Point(0, 432);
            this.btnLogin.MinimumSize = new System.Drawing.Size(120, 42);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(440, 58);
            this.btnLogin.TabIndex = 0;
            this.btnLogin.Text = "Đăng nhập";
            this.btnLogin.UseVisualStyleBackColor = false;
            // 
            // lblError
            // 
            this.lblError.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblError.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lblError.Location = new System.Drawing.Point(0, 402);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(440, 26);
            this.lblError.TabIndex = 1;
            // 
            // pnlOptionsRow
            // 
            this.pnlOptionsRow.Controls.Add(this.chkRemember);
            this.pnlOptionsRow.Controls.Add(this.lnkForgotPassword);
            this.pnlOptionsRow.Location = new System.Drawing.Point(0, 366);
            this.pnlOptionsRow.Name = "pnlOptionsRow";
            this.pnlOptionsRow.Size = new System.Drawing.Size(440, 40);
            this.pnlOptionsRow.TabIndex = 2;
            // 
            // chkRemember
            // 
            this.chkRemember.BackColor = System.Drawing.Color.White;
            this.chkRemember.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkRemember.Font = new System.Drawing.Font("Segoe UI", 12.5F);
            this.chkRemember.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.chkRemember.Location = new System.Drawing.Point(0, 5);
            this.chkRemember.Name = "chkRemember";
            this.chkRemember.Size = new System.Drawing.Size(255, 39);
            this.chkRemember.TabIndex = 0;
            this.chkRemember.TabStop = false;
            this.chkRemember.Text = "Ghi nhớ đăng nhập";
            this.chkRemember.UseVisualStyleBackColor = false;
            // 
            // lnkForgotPassword
            // 
            this.lnkForgotPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkForgotPassword.BackColor = System.Drawing.Color.Transparent;
            this.lnkForgotPassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lnkForgotPassword.Font = new System.Drawing.Font("Segoe UI", 12.5F, System.Drawing.FontStyle.Bold);
            this.lnkForgotPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this.lnkForgotPassword.Location = new System.Drawing.Point(249, 0);
            this.lnkForgotPassword.Margin = new System.Windows.Forms.Padding(0);
            this.lnkForgotPassword.Name = "lnkForgotPassword";
            this.lnkForgotPassword.Size = new System.Drawing.Size(198, 44);
            this.lnkForgotPassword.TabIndex = 1;
            this.lnkForgotPassword.TabStop = true;
            this.lnkForgotPassword.Text = "Quên mật khẩu?";
            this.lnkForgotPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lnkForgotPassword.Click += new System.EventHandler(this.lnkForgotPassword_Click_1);
            // 
            // pnlPasswordInput
            // 
            this.pnlPasswordInput.BackColor = System.Drawing.Color.Transparent;
            this.pnlPasswordInput.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.pnlPasswordInput.BorderRadius = 10;
            this.pnlPasswordInput.BorderThickness = 1;
            this.pnlPasswordInput.Controls.Add(this.txtPassword);
            this.pnlPasswordInput.FillColor = System.Drawing.Color.White;
            this.pnlPasswordInput.Location = new System.Drawing.Point(0, 304);
            this.pnlPasswordInput.Margin = new System.Windows.Forms.Padding(0);
            this.pnlPasswordInput.Name = "pnlPasswordInput";
            this.pnlPasswordInput.Padding = new System.Windows.Forms.Padding(8);
            this.pnlPasswordInput.Size = new System.Drawing.Size(440, 52);
            this.pnlPasswordInput.TabIndex = 3;
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.White;
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPassword.Location = new System.Drawing.Point(8, 8);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(0);
            this.txtPassword.MaxLength = 100;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(424, 24);
            this.txtPassword.TabIndex = 3;
            // 
            // lblPassword
            // 
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblPassword.Location = new System.Drawing.Point(0, 268);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(440, 32);
            this.lblPassword.TabIndex = 4;
            this.lblPassword.Text = "Mật khẩu";
            this.lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlUsernameInput
            // 
            this.pnlUsernameInput.BackColor = System.Drawing.Color.Transparent;
            this.pnlUsernameInput.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.pnlUsernameInput.BorderRadius = 10;
            this.pnlUsernameInput.BorderThickness = 1;
            this.pnlUsernameInput.Controls.Add(this.txtUsername);
            this.pnlUsernameInput.FillColor = System.Drawing.Color.White;
            this.pnlUsernameInput.Location = new System.Drawing.Point(0, 196);
            this.pnlUsernameInput.Margin = new System.Windows.Forms.Padding(0);
            this.pnlUsernameInput.Name = "pnlUsernameInput";
            this.pnlUsernameInput.Padding = new System.Windows.Forms.Padding(8);
            this.pnlUsernameInput.Size = new System.Drawing.Size(440, 52);
            this.pnlUsernameInput.TabIndex = 5;
            // 
            // txtUsername
            // 
            this.txtUsername.BackColor = System.Drawing.Color.White;
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUsername.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUsername.Location = new System.Drawing.Point(8, 8);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(0);
            this.txtUsername.MaxLength = 50;
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(424, 24);
            this.txtUsername.TabIndex = 5;
            // 
            // lblUsername
            // 
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblUsername.Location = new System.Drawing.Point(0, 160);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(440, 32);
            this.lblUsername.TabIndex = 6;
            this.lblUsername.Text = "Tên đăng nhập";
            this.lblUsername.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSubtitle.Location = new System.Drawing.Point(0, 90);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(440, 34);
            this.lblSubtitle.TabIndex = 7;
            this.lblSubtitle.Text = "Vui lòng đăng nhập để tiếp tục";
            this.lblSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(440, 78);
            this.lblTitle.TabIndex = 8;
            this.lblTitle.Text = "Đăng nhập";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlAuthSplit
            // 
            this.pnlAuthSplit.ColumnCount = 2;
            this.pnlAuthSplit.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.pnlAuthSplit.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.pnlAuthSplit.Controls.Add(this.brandPanel, 0, 0);
            this.pnlAuthSplit.Controls.Add(this.pnlRight, 1, 0);
            this.pnlAuthSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAuthSplit.Location = new System.Drawing.Point(0, 0);
            this.pnlAuthSplit.Margin = new System.Windows.Forms.Padding(0);
            this.pnlAuthSplit.Name = "pnlAuthSplit";
            this.pnlAuthSplit.RowCount = 1;
            this.pnlAuthSplit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlAuthSplit.Size = new System.Drawing.Size(1200, 760);
            this.pnlAuthSplit.TabIndex = 0;
            // 
            // brandPanel
            // 
            this.brandPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(120)))), ((int)(((byte)(180)))));
            this.brandPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.brandPanel.Location = new System.Drawing.Point(0, 0);
            this.brandPanel.Margin = new System.Windows.Forms.Padding(0);
            this.brandPanel.Name = "brandPanel";
            this.brandPanel.Size = new System.Drawing.Size(480, 760);
            this.brandPanel.TabIndex = 0;
            this.brandPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.brandPanel_Paint);
            // 
            // frmLogin
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1200, 760);
            this.Controls.Add(this.pnlAuthSplit);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(1100, 760);
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập - SIMS";
            this.pnlRight.ResumeLayout(false);
            this.pnlFormCard.ResumeLayout(false);
            this.pnlOptionsRow.ResumeLayout(false);
            this.pnlPasswordInput.ResumeLayout(false);
            this.pnlPasswordInput.PerformLayout();
            this.pnlUsernameInput.ResumeLayout(false);
            this.pnlUsernameInput.PerformLayout();
            this.pnlAuthSplit.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    }
}