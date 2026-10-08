using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Controls;

namespace SIMS_WinFormsApp.Views.Auth
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
            this.brandPanel = new SIMS_WinFormsApp.UI.Controls.AuthBrandPanel();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.pnlButtonRow = new System.Windows.Forms.Panel();
            this.btnCancel = new SIMS_WinFormsApp.UI.Controls.PrimaryButton();
            this.btnSubmit = new SIMS_WinFormsApp.UI.Controls.PrimaryButton();
            this.lblError = new System.Windows.Forms.Label();
            this.txtConfirm = new SIMS_WinFormsApp.UI.Controls.RoundedPasswordTextBox();
            this.lblConfirm = new System.Windows.Forms.Label();
            this.txtNew = new SIMS_WinFormsApp.UI.Controls.RoundedPasswordTextBox();
            this.lblNew = new System.Windows.Forms.Label();
            this.txtCurrent = new SIMS_WinFormsApp.UI.Controls.RoundedPasswordTextBox();
            this.lblCurrent = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlRight.SuspendLayout();
            this.pnlButtonRow.SuspendLayout();
            this.SuspendLayout();
            // 
            // brandPanel
            // 
            this.brandPanel.BrandName = "SIMS";
            this.brandPanel.ContentTop = -1;
            this.brandPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.brandPanel.Features = new string[0];
            this.brandPanel.FooterText = "";
            this.brandPanel.Location = new System.Drawing.Point(0, 0);
            this.brandPanel.Logo = null;
            this.brandPanel.Name = "brandPanel";
            this.brandPanel.Size = new System.Drawing.Size(440, 650);
            this.brandPanel.TabIndex = 1;
            this.brandPanel.Tagline = "Quản lý cửa hàng thông minh";
            this.brandPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.brandPanel_Paint);
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.White;
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
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(440, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(600, 650);
            this.pnlRight.TabIndex = 0;
            // 
            // pnlButtonRow
            // 
            this.pnlButtonRow.Controls.Add(this.btnCancel);
            this.pnlButtonRow.Controls.Add(this.btnSubmit);
            this.pnlButtonRow.Location = new System.Drawing.Point(70, 584);
            this.pnlButtonRow.Name = "pnlButtonRow";
            this.pnlButtonRow.Size = new System.Drawing.Size(440, 46);
            this.pnlButtonRow.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.CornerRadius = 10;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.CustomAccentColor = null;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Icon = null;
            this.btnCancel.IconSize = 16;
            this.btnCancel.IsPrimary = false;
            this.btnCancel.Location = new System.Drawing.Point(0, 0);
            this.btnCancel.MinimumSize = new System.Drawing.Size(120, 42);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(140, 46);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.BackColor = System.Drawing.Color.Transparent;
            this.btnSubmit.CornerRadius = 10;
            this.btnSubmit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSubmit.CustomAccentColor = null;
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmit.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnSubmit.ForeColor = System.Drawing.Color.White;
            this.btnSubmit.Icon = null;
            this.btnSubmit.IconSize = 16;
            this.btnSubmit.IsPrimary = true;
            this.btnSubmit.Location = new System.Drawing.Point(156, 0);
            this.btnSubmit.MinimumSize = new System.Drawing.Size(120, 42);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(284, 46);
            this.btnSubmit.TabIndex = 1;
            this.btnSubmit.Text = "Cập nhật mật khẩu";
            this.btnSubmit.UseVisualStyleBackColor = false;
            // 
            // lblError
            // 
            this.lblError.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblError.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lblError.Location = new System.Drawing.Point(70, 554);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(440, 24);
            this.lblError.TabIndex = 1;
            // 
            // txtConfirm
            // 
            this.txtConfirm.BackColor = System.Drawing.Color.Transparent;
            this.txtConfirm.CornerRadius = 10;
            this.txtConfirm.DarkMode = false;
            this.txtConfirm.HideTooltip = "Ẩn mật khẩu";
            this.txtConfirm.Location = new System.Drawing.Point(70, 494);
            this.txtConfirm.MaxLength = 100;
            this.txtConfirm.Name = "txtConfirm";
            this.txtConfirm.Padding = new System.Windows.Forms.Padding(14, 0, 14, 0);
            this.txtConfirm.PlaceholderText = "Nhập lại mật khẩu mới";
            this.txtConfirm.ShowTooltip = "Hiện mật khẩu";
            this.txtConfirm.Size = new System.Drawing.Size(440, 52);
            this.txtConfirm.TabIndex = 2;
            // 
            // lblConfirm
            // 
            this.lblConfirm.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblConfirm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblConfirm.Location = new System.Drawing.Point(70, 458);
            this.lblConfirm.Name = "lblConfirm";
            this.lblConfirm.Size = new System.Drawing.Size(440, 32);
            this.lblConfirm.TabIndex = 3;
            this.lblConfirm.Text = "Xác nhận mật khẩu mới";
            this.lblConfirm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtNew
            // 
            this.txtNew.BackColor = System.Drawing.Color.Transparent;
            this.txtNew.CornerRadius = 10;
            this.txtNew.DarkMode = false;
            this.txtNew.HideTooltip = "Ẩn mật khẩu";
            this.txtNew.Location = new System.Drawing.Point(70, 390);
            this.txtNew.MaxLength = 100;
            this.txtNew.Name = "txtNew";
            this.txtNew.Padding = new System.Windows.Forms.Padding(14, 0, 14, 0);
            this.txtNew.PlaceholderText = "Nhập mật khẩu mới";
            this.txtNew.ShowTooltip = "Hiện mật khẩu";
            this.txtNew.Size = new System.Drawing.Size(440, 52);
            this.txtNew.TabIndex = 4;
            // 
            // lblNew
            // 
            this.lblNew.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblNew.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblNew.Location = new System.Drawing.Point(70, 354);
            this.lblNew.Name = "lblNew";
            this.lblNew.Size = new System.Drawing.Size(440, 32);
            this.lblNew.TabIndex = 5;
            this.lblNew.Text = "Mật khẩu mới";
            this.lblNew.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCurrent
            // 
            this.txtCurrent.BackColor = System.Drawing.Color.Transparent;
            this.txtCurrent.CornerRadius = 10;
            this.txtCurrent.DarkMode = false;
            this.txtCurrent.HideTooltip = "Ẩn mật khẩu";
            this.txtCurrent.Location = new System.Drawing.Point(70, 286);
            this.txtCurrent.MaxLength = 100;
            this.txtCurrent.Name = "txtCurrent";
            this.txtCurrent.Padding = new System.Windows.Forms.Padding(14, 0, 14, 0);
            this.txtCurrent.PlaceholderText = "Nhập mật khẩu hiện tại";
            this.txtCurrent.ShowTooltip = "Hiện mật khẩu";
            this.txtCurrent.Size = new System.Drawing.Size(440, 52);
            this.txtCurrent.TabIndex = 6;
            // 
            // lblCurrent
            // 
            this.lblCurrent.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblCurrent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblCurrent.Location = new System.Drawing.Point(70, 250);
            this.lblCurrent.Name = "lblCurrent";
            this.lblCurrent.Size = new System.Drawing.Size(440, 32);
            this.lblCurrent.TabIndex = 7;
            this.lblCurrent.Text = "Mật khẩu hiện tại";
            this.lblCurrent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSubtitle.Location = new System.Drawing.Point(70, 190);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(440, 48);
            this.lblSubtitle.TabIndex = 8;
            this.lblSubtitle.Text = "Vui lòng nhập mật khẩu hiện tại và mật khẩu mới.";
            this.lblSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTitle.Location = new System.Drawing.Point(70, 112);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(440, 72);
            this.lblTitle.TabIndex = 9;
            this.lblTitle.Text = "Đổi mật khẩu";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmChangePassword
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1040, 650);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.brandPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(920, 580);
            this.Name = "frmChangePassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đổi mật khẩu - SIMS";
            this.pnlRight.ResumeLayout(false);
            this.pnlButtonRow.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    }
}