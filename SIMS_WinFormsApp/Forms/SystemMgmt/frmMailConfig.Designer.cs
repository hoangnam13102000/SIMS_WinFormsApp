using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Forms.SystemMgmt
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
            this.components = new System.ComponentModel.Container();

            this.pnlCard = new Panel();

            this.lblTitle = new Label();
            this.lblSubtitle = new Label();

            this.lblSenderEmail = new Label();
            this.txtSenderEmail = new RoundedTextBox();

            this.lblAppPassword = new Label();
            this.txtAppPassword = new RoundedPasswordTextBox();

            this.lblHint = new Label();
            this.lnkHowTo = new ClickableLabel();

            this.lblMessage = new Label();

            this.pnlButtonsRow = new Panel();
            this.btnTest = new PrimaryButton();
            this.btnSave = new PrimaryButton();

            this.SuspendLayout();

            // ===== frmMailConfig =====
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(480, 468);
            this.MinimumSize = new Size(480, 468);
            this.MaximumSize = new Size(480, 468);
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Text = "Cấu hình Email";
            this.Font = new Font("Segoe UI", 9F);
            this.BackColor = Color.White;

            // ===== pnlCard =====
            const int cardWidth = 400;
            this.pnlCard.Location = new Point(40, 32);
            this.pnlCard.Size = new Size(cardWidth, 400);
            this.pnlCard.BackColor = Color.Transparent;

            int y = 0;

            // Title
            this.lblTitle.AutoSize = false;
            this.lblTitle.Location = new Point(0, y);
            this.lblTitle.Size = new Size(cardWidth, 32);
            this.lblTitle.Font = AppFonts.Subtitle;
            this.lblTitle.ForeColor = AppColors.TextTitle;
            this.lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            y += 34;

            // Subtitle
            this.lblSubtitle.AutoSize = false;
            this.lblSubtitle.Location = new Point(0, y);
            this.lblSubtitle.Size = new Size(cardWidth, 40);
            this.lblSubtitle.Font = AppFonts.Body;
            this.lblSubtitle.ForeColor = AppColors.TextMuted;
            this.lblSubtitle.TextAlign = ContentAlignment.MiddleLeft;
            y += 44;

            // Sender email label + field
            this.lblSenderEmail.AutoSize = false;
            this.lblSenderEmail.Location = new Point(0, y);
            this.lblSenderEmail.Size = new Size(cardWidth, 20);
            this.lblSenderEmail.Font = AppFonts.BodyBold;
            this.lblSenderEmail.ForeColor = AppColors.TextPrimary;
            y += 24;

            this.txtSenderEmail.Location = new Point(0, y);
            this.txtSenderEmail.Size = new Size(cardWidth, 52);
            this.txtSenderEmail.MaxLength = 100;
            y += 52 + 18;

            // App password label + field
            this.lblAppPassword.AutoSize = false;
            this.lblAppPassword.Location = new Point(0, y);
            this.lblAppPassword.Size = new Size(cardWidth, 20);
            this.lblAppPassword.Font = AppFonts.BodyBold;
            this.lblAppPassword.ForeColor = AppColors.TextPrimary;
            y += 24;

            this.txtAppPassword.Location = new Point(0, y);
            this.txtAppPassword.Size = new Size(cardWidth, 52);
            this.txtAppPassword.MaxLength = 32;
            y += 52 + 10;

            // Hint + how-to link
            this.lblHint.AutoSize = false;
            this.lblHint.Location = new Point(0, y);
            this.lblHint.Size = new Size(cardWidth, 34);
            this.lblHint.Font = AppFonts.Small;
            this.lblHint.ForeColor = AppColors.TextMuted;
            y += 34;

            this.lnkHowTo.AutoSize = true;
            this.lnkHowTo.Location = new Point(0, y);
            y += 22 + 10;

            // Message label (kết quả lưu / test)
            this.lblMessage.AutoSize = false;
            this.lblMessage.Location = new Point(0, y);
            this.lblMessage.Size = new Size(cardWidth, 36);
            this.lblMessage.Font = AppFonts.Small;
            this.lblMessage.Text = "";
            y += 40;

            // Buttons row: Test (trái, phụ) + Save (phải, chính)
            const int buttonGap = 12;
            const int testWidth = 150;
            int saveWidth = cardWidth - testWidth - buttonGap;

            this.pnlButtonsRow.Location = new Point(0, y);
            this.pnlButtonsRow.Size = new Size(cardWidth, 46);
            this.pnlButtonsRow.BackColor = Color.Transparent;

            this.btnTest.Location = new Point(0, 0);
            this.btnTest.Size = new Size(testWidth, 46);
            this.btnTest.IsPrimary = false;

            this.btnSave.Location = new Point(testWidth + buttonGap, 0);
            this.btnSave.Size = new Size(saveWidth, 46);
            this.btnSave.IsPrimary = true;

            this.pnlButtonsRow.Controls.Add(this.btnTest);
            this.pnlButtonsRow.Controls.Add(this.btnSave);

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

            this.Controls.Add(this.pnlCard);

            this.ResumeLayout(false);
        }

        #endregion
    }
}