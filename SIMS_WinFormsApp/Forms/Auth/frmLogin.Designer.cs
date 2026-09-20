using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Forms.Auth
{
    partial class frmLogin
    {
        private System.ComponentModel.IContainer components = null;

        private AuthBrandPanel brandPanel;
        private Guna2Panel pnlRight;
        private TableLayoutPanel pnlAuthSplit;
        private Guna2Panel pnlFormCard;

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
            this.pnlRight = new Guna2Panel();
            this.pnlAuthSplit = new TableLayoutPanel();
            this.pnlFormCard = new Guna2Panel();

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

            this.AutoScaleMode = AutoScaleMode.None;
            this.ClientSize = new Size(1200, 760);
            this.MinimumSize = new Size(1100, 760);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.Text = "Đăng nhập - SIMS";
            this.Font = new Font("Segoe UI", 9F);

            // ===== brandPanel =====
            this.brandPanel.Dock = DockStyle.Fill;
            this.brandPanel.Margin = Padding.Empty;
            this.brandPanel.Padding = Padding.Empty;

            // ===== pnlRight (phải, chứa card đăng nhập được canh giữa) =====
            this.pnlRight.Dock = DockStyle.Fill;
            this.pnlRight.BackColor = Color.White;
            this.pnlRight.FillColor = AppColors.White;
            this.pnlRight.Margin = Padding.Empty;
            this.pnlRight.Padding = Padding.Empty;

            // A percentage table layout keeps the split at exactly 40/60.
            // Unlike manually changing a Dock.Left width, it remains stable
            // during maximize/restore while both children are relaid out.
            this.pnlAuthSplit.Dock = DockStyle.Fill;
            this.pnlAuthSplit.Margin = Padding.Empty;
            this.pnlAuthSplit.Padding = Padding.Empty;
            this.pnlAuthSplit.ColumnCount = 2;
            this.pnlAuthSplit.RowCount = 1;
            this.pnlAuthSplit.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40f));
            this.pnlAuthSplit.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60f));
            this.pnlAuthSplit.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            this.pnlAuthSplit.Controls.Add(this.brandPanel, 0, 0);
            this.pnlAuthSplit.Controls.Add(this.pnlRight, 1, 0);

            // ===== pnlFormCard =====
            const int cardWidth = 440;

            // Tăng chiều cao card để các nhãn/title/button không bị cắt ở chân khi render
            // với font thực tế trên Windows (Segoe UI / ClearType có chiều cao lớn hơn tính toán sơ bộ).
            const int cardHeight = 560;

            this.pnlFormCard.Size = new Size(cardWidth, cardHeight);
            this.pnlFormCard.BackColor = Color.Transparent;
            this.pnlFormCard.FillColor = Color.Transparent;
            this.pnlFormCard.Anchor = AnchorStyles.None;

            int y = 0;

            // Title
            this.lblTitle.AutoSize = false;
            this.lblTitle.Location = new Point(-5, y);
            this.lblTitle.Size = new Size(cardWidth + 5, 78);
            this.lblTitle.Font = AppFonts.Title;
            this.lblTitle.ForeColor = AppColors.TextTitle;
            this.lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            y += 78 + 12;

            // Subtitle
            this.lblSubtitle.AutoSize = false;
            this.lblSubtitle.Location = new Point(0, y);
            this.lblSubtitle.Size = new Size(cardWidth, 34);
            this.lblSubtitle.Font = AppFonts.Body;
            this.lblSubtitle.ForeColor = AppColors.TextMuted;
            this.lblSubtitle.TextAlign = ContentAlignment.MiddleLeft;
            y += 34 + 36;

            // Username label + field
            this.lblUsername.AutoSize = false;
            this.lblUsername.Location = new Point(0, y);
            this.lblUsername.Size = new Size(cardWidth, 32);
            this.lblUsername.Font = AppFonts.BodyBold;
            this.lblUsername.ForeColor = AppColors.TextPrimary;
            this.lblUsername.TextAlign = ContentAlignment.MiddleLeft;
            y += 36;

            this.txtUsername.Location = new Point(0, y);
            this.txtUsername.Size = new Size(cardWidth, 52);
            this.txtUsername.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.txtUsername.MaxLength = 50;
            y += 52 + 20;

            // Password label + field
            this.lblPassword.AutoSize = false;
            this.lblPassword.Location = new Point(0, y);
            this.lblPassword.Size = new Size(cardWidth, 32);
            this.lblPassword.Font = AppFonts.BodyBold;
            this.lblPassword.ForeColor = AppColors.TextPrimary;
            this.lblPassword.TextAlign = ContentAlignment.MiddleLeft;
            y += 36;

            this.txtPassword.Location = new Point(0, y);
            this.txtPassword.Size = new Size(cardWidth, 52);
            this.txtPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.txtPassword.MaxLength = 100;
            y += 52 + 10;

            // Remember me + Forgot password row
            this.pnlOptionsRow.Location = new Point(0, y);
            this.pnlOptionsRow.Size = new Size(cardWidth, 40);
            this.pnlOptionsRow.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            this.chkRemember.AutoSize = false;
            this.chkRemember.Size = new Size(220, 30);
            this.chkRemember.Location = new Point(0, 5);
            this.chkRemember.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            this.lnkForgotPassword.AutoSize = false;
            this.lnkForgotPassword.Size = new Size(200, 30);
            this.lnkForgotPassword.Font = new Font("Segoe UI", 12.5f, FontStyle.Bold, GraphicsUnit.Point);
            this.lnkForgotPassword.TextAlign = ContentAlignment.MiddleRight;
            this.lnkForgotPassword.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.lnkForgotPassword.Location = new Point(cardWidth - 200, 4);

            this.pnlOptionsRow.Controls.Add(this.chkRemember);
            this.pnlOptionsRow.Controls.Add(this.lnkForgotPassword);
            this.pnlOptionsRow.Resize += (s, e) => AlignForgotPassword();
            this.lnkForgotPassword.SizeChanged += (s, e) => AlignForgotPassword();
            y += 30 + 6;

            // Error label
            this.lblError.AutoSize = false;
            this.lblError.Location = new Point(0, y);
            this.lblError.Size = new Size(cardWidth, 26);
            this.lblError.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.lblError.ForeColor = AppColors.Error;
            this.lblError.Font = AppFonts.Small;
            this.lblError.Text = "";
            y += 30;

            this.btnLogin.Location = new Point(0, y);
            this.btnLogin.Size = new Size(cardWidth, 58);
            this.btnLogin.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.btnLogin.IsPrimary = true;
            y += 58 + 20;

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

            this.Controls.Add(this.pnlAuthSplit);


            this.pnlRight.Resize += (s, e) => LayoutLoginPanels();
            this.pnlFormCard.Resize += (s, e) => ResizeFormCardControls();
            this.ResizeFormCardControls();
            LayoutLoginPanels();
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

        private void LayoutLoginPanels()
        {
            if (pnlRight == null || pnlFormCard == null || brandPanel == null) return;

            pnlAuthSplit.PerformLayout();
            int availableCardWidth = System.Math.Max(1, pnlRight.ClientSize.Width - 80);
            int cardWidth = System.Math.Max(1, System.Math.Min(560, (int)(availableCardWidth * 0.78f)));
            pnlFormCard.Width = cardWidth;
            ResizeFormCardControls();
            CenterFormCard();
        }

        private void ResizeFormCardControls()
        {
            if (pnlFormCard == null) return;

            int width = System.Math.Max(1, pnlFormCard.ClientSize.Width);
            lblTitle.Width = width + 5;
            lblSubtitle.Width = width;
            lblUsername.Width = width;
            txtUsername.Width = width;
            lblPassword.Width = width;
            txtPassword.Width = width;
            pnlOptionsRow.Width = width;
            lblError.Width = width;
            btnLogin.Width = width;
            AlignForgotPassword();
        }

        #endregion
    }
}