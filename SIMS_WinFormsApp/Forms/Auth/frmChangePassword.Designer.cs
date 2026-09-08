using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Controls;

namespace SIMS_WinFormsApp.Forms.Auth
{
    partial class frmChangePassword
    {
        private System.ComponentModel.IContainer components = null;

        private AuthBrandPanel brandPanel;
        private Panel pnlRight;

        private Label lblTitle;
        private Label lblSubtitle;

        private Label lblCurrent;
        private RoundedPasswordTextBox txtCurrent;

        private Label lblNew;
        private RoundedPasswordTextBox txtNew;

        private Label lblConfirm;
        private RoundedPasswordTextBox txtConfirm;

        private Label lblError;

        private Panel pnlButtonRow;
        private PrimaryButton btnCancel;
        private PrimaryButton btnSubmit;

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

            this.lblTitle = new Label();
            this.lblSubtitle = new Label();

            this.lblCurrent = new Label();
            this.txtCurrent = new RoundedPasswordTextBox();

            this.lblNew = new Label();
            this.txtNew = new RoundedPasswordTextBox();

            this.lblConfirm = new Label();
            this.txtConfirm = new RoundedPasswordTextBox();

            this.lblError = new Label();

            this.pnlButtonRow = new Panel();
            this.btnCancel = new PrimaryButton();
            this.btnSubmit = new PrimaryButton();

            this.SuspendLayout();

            // ===== frmChangePassword =====
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1040, 650);
            this.MinimumSize = new Size(920, 580);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.Text = "Đổi mật khẩu - SIMS";
            this.Font = new Font("Segoe UI", 9F);

            // ===== brandPanel (trai, dung chung voi man hinh dang nhap) =====
            this.brandPanel.Dock = DockStyle.Left;
            this.brandPanel.Width = 440;

            // ===== pnlRight (phai) =====
            this.pnlRight.Dock = DockStyle.Fill;
            this.pnlRight.BackColor = Color.White;

            const int formLeft = 70;
            int y = 128;

            this.lblTitle.AutoSize = false;
            this.lblTitle.Location = new Point(formLeft, y);
            this.lblTitle.Size = new Size(440, 40);
            this.lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            y += 42;

            this.lblSubtitle.AutoSize = false;
            this.lblSubtitle.Location = new Point(formLeft, y);
            this.lblSubtitle.Size = new Size(440, 24);
            this.lblSubtitle.TextAlign = ContentAlignment.MiddleLeft;
            y += 24 + 30;

            // Mat khau hien tai
            this.lblCurrent.AutoSize = false;
            this.lblCurrent.Location = new Point(formLeft, y);
            this.lblCurrent.Size = new Size(440, 20);
            y += 24;

            this.txtCurrent.Location = new Point(formLeft, y);
            this.txtCurrent.Size = new Size(440, 44);
            this.txtCurrent.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtCurrent.MaxLength = 100;
            y += 44 + 16;

            // Mat khau moi
            this.lblNew.AutoSize = false;
            this.lblNew.Location = new Point(formLeft, y);
            this.lblNew.Size = new Size(440, 20);
            y += 24;

            this.txtNew.Location = new Point(formLeft, y);
            this.txtNew.Size = new Size(440, 44);
            this.txtNew.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtNew.MaxLength = 100;
            y += 44 + 16;

            // Xac nhan mat khau moi
            this.lblConfirm.AutoSize = false;
            this.lblConfirm.Location = new Point(formLeft, y);
            this.lblConfirm.Size = new Size(440, 20);
            y += 24;

            this.txtConfirm.Location = new Point(formLeft, y);
            this.txtConfirm.Size = new Size(440, 44);
            this.txtConfirm.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtConfirm.MaxLength = 100;
            y += 44 + 10;

            // Error label
            this.lblError.AutoSize = false;
            this.lblError.Location = new Point(formLeft, y);
            this.lblError.Size = new Size(440, 20);
            this.lblError.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.lblError.ForeColor = Color.FromArgb(220, 38, 38);
            this.lblError.Font = new Font("Segoe UI", 9F);
            this.lblError.Text = "";
            y += 24;

            // Hang nut Huy / Cap nhat
            this.pnlButtonRow.Location = new Point(formLeft, y);
            this.pnlButtonRow.Size = new Size(440, 46);
            this.pnlButtonRow.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            this.btnCancel.Location = new Point(0, 0);
            this.btnCancel.Size = new Size(140, 46);
            this.btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.btnCancel.IsPrimary = false;

            this.btnSubmit.Location = new Point(156, 0);
            this.btnSubmit.Size = new Size(284, 46);
            this.btnSubmit.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.btnSubmit.IsPrimary = true;

            this.pnlButtonRow.Controls.Add(this.btnCancel);
            this.pnlButtonRow.Controls.Add(this.btnSubmit);

            this.pnlRight.Controls.Add(this.pnlButtonRow);
            this.pnlRight.Controls.Add(this.lblError);
            this.pnlRight.Controls.Add(this.txtConfirm);
            this.pnlRight.Controls.Add(this.lblConfirm);
            this.pnlRight.Controls.Add(this.txtNew);
            this.pnlRight.Controls.Add(this.lblNew);
            this.pnlRight.Controls.Add(this.txtCurrent);
            this.pnlRight.Controls.Add(this.lblCurrent);
            this.pnlRight.Controls.Add(this.lblSubtitle);
            this.pnlRight.Controls.Add(this.lblTitle);

            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.brandPanel);

            this.ResumeLayout(false);
        }

        #endregion
    }
}