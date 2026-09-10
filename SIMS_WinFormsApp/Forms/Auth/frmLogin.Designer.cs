using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Forms.Auth
{
    partial class frmLogin
    {
        private System.ComponentModel.IContainer components = null;

        private AuthBrandPanel brandPanel;
        private Panel pnlRight;
        private Panel pnlFormCard;

        private Label lblTitle;
        private Label lblSubtitle;

        private Label lblUsername;
        private RoundedTextBox txtUsername;

        private Label lblPassword;
        private RoundedPasswordTextBox txtPassword;

        private Panel pnlOptionsRow;
        private ModernCheckBox chkRemember;
        private ClickableLabel lnkForgotPassword;

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
            this.components = new System.ComponentModel.Container();

            this.brandPanel = new AuthBrandPanel();
            this.pnlRight = new Panel();
            this.pnlFormCard = new Panel();

            this.lblTitle = new Label();
            this.lblSubtitle = new Label();

            this.lblUsername = new Label();
            this.txtUsername = new RoundedTextBox();

            this.lblPassword = new Label();
            this.txtPassword = new RoundedPasswordTextBox();

            this.pnlOptionsRow = new Panel();
            this.chkRemember = new ModernCheckBox();
            this.lnkForgotPassword = new ClickableLabel();

            this.lblError = new Label();
            this.btnLogin = new PrimaryButton();

            this.SuspendLayout();

            // ===== frmLogin =====
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1040, 650);
            this.MinimumSize = new Size(920, 580);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.Text = "Đăng nhập - SIMS";
            this.Font = new Font("Segoe UI", 9F);

            // ===== brandPanel =====
            this.brandPanel.Dock = DockStyle.Left;
            this.brandPanel.Width = 440;

            // ===== pnlRight (phải, chứa card đăng nhập được canh giữa) =====
            this.pnlRight.Dock = DockStyle.Fill;
            this.pnlRight.BackColor = Color.White;

            // ===== pnlFormCard =====
            const int cardWidth = 440;
            const int cardHeight = 420;

            this.pnlFormCard.Size = new Size(cardWidth, cardHeight);
            this.pnlFormCard.BackColor = Color.Transparent;
            this.pnlFormCard.Anchor = AnchorStyles.None;

            int y = 0;

            // Title
            this.lblTitle.AutoSize = false;
            this.lblTitle.Location = new Point(-5, y);   
            this.lblTitle.Size = new Size(cardWidth + 5, 44);   
            this.lblTitle.Font = AppFonts.Title;
            this.lblTitle.ForeColor = AppColors.TextTitle;
            this.lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            y += 48;

            // Subtitle
            this.lblSubtitle.AutoSize = false;
            this.lblSubtitle.Location = new Point(0, y);
            this.lblSubtitle.Size = new Size(cardWidth, 24);
            this.lblSubtitle.Font = AppFonts.Body;
            this.lblSubtitle.ForeColor = AppColors.TextMuted;
            this.lblSubtitle.TextAlign = ContentAlignment.MiddleLeft;
            y += 24 + 34;

            // Username label + field
            this.lblUsername.AutoSize = false;
            this.lblUsername.Location = new Point(0, y);
            this.lblUsername.Size = new Size(cardWidth, 20);
            this.lblUsername.Font = AppFonts.BodyBold;          
            this.lblUsername.ForeColor = AppColors.TextPrimary; 
            y += 24;

            this.txtUsername.Location = new Point(0, y);
            this.txtUsername.Size = new Size(cardWidth, 44);
            this.txtUsername.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.txtUsername.MaxLength = 50;
            y += 44 + 20;

            // Password label + field
            this.lblPassword.AutoSize = false;
            this.lblPassword.Location = new Point(0, y);
            this.lblPassword.Size = new Size(cardWidth, 20);
            this.lblPassword.Font = AppFonts.BodyBold;          
            this.lblPassword.ForeColor = AppColors.TextPrimary; 
            y += 24;

            this.txtPassword.Location = new Point(0, y);
            this.txtPassword.Size = new Size(cardWidth, 44);
            this.txtPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.txtPassword.MaxLength = 100;
            y += 44 + 10;

            // Remember me + Forgot password row
            this.pnlOptionsRow.Location = new Point(0, y);
            this.pnlOptionsRow.Size = new Size(cardWidth, 26);
            this.pnlOptionsRow.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            this.chkRemember.Location = new Point(0, 2);
            this.chkRemember.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            this.lnkForgotPassword.AutoSize = true;
            this.lnkForgotPassword.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.lnkForgotPassword.Location = new Point(cardWidth - 130, 4);

            this.pnlOptionsRow.Controls.Add(this.chkRemember);
            this.pnlOptionsRow.Controls.Add(this.lnkForgotPassword);
            this.pnlOptionsRow.Resize += (s, e) => AlignForgotPassword();
            this.lnkForgotPassword.SizeChanged += (s, e) => AlignForgotPassword();
            y += 26 + 6;

            // Error label
            this.lblError.AutoSize = false;
            this.lblError.Location = new Point(0, y);
            this.lblError.Size = new Size(cardWidth, 20);
            this.lblError.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.lblError.ForeColor = AppColors.Error;
            this.lblError.Font = AppFonts.Small;
            this.lblError.Text = "";
            y += 24;

            // Login button
            this.btnLogin.Location = new Point(0, y);
            this.btnLogin.Size = new Size(cardWidth, 46);
            this.btnLogin.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.btnLogin.IsPrimary = true;
            y += 46 + 20;

            this.pnlFormCard.Controls.Add(this.btnLogin);
            this.pnlFormCard.Controls.Add(this.lblError);
            this.pnlFormCard.Controls.Add(this.pnlOptionsRow);
            this.pnlFormCard.Controls.Add(this.txtPassword);
            this.pnlFormCard.Controls.Add(this.lblPassword);
            this.pnlFormCard.Controls.Add(this.txtUsername);
            this.pnlFormCard.Controls.Add(this.lblUsername);
            this.pnlFormCard.Controls.Add(this.lblSubtitle);
            this.pnlFormCard.Controls.Add(this.lblTitle);

            this.pnlRight.Controls.Add(this.pnlFormCard);

            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.brandPanel);


            this.pnlRight.Resize += (s, e) => CenterFormCard();
            CenterFormCard();

            this.ResumeLayout(false);
        }

        private void AlignForgotPassword()
        {
            if (pnlOptionsRow == null || lnkForgotPassword == null) return;

            int x = System.Math.Max(0, pnlOptionsRow.Width - lnkForgotPassword.Width);
            lnkForgotPassword.Location = new Point(x, lnkForgotPassword.Location.Y);
        }

        private void CenterFormCard()
        {
            if (pnlFormCard == null || pnlRight == null) return;

            int x = System.Math.Max(24, (pnlRight.Width - pnlFormCard.Width) / 2);
            int yPos = System.Math.Max(24, (pnlRight.Height - pnlFormCard.Height) / 2);
            pnlFormCard.Location = new Point(x, yPos);
            brandPanel.ContentTop = yPos;
        }

        #endregion
    }
}