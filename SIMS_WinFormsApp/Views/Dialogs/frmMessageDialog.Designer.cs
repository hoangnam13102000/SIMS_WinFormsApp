using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Views.Dialogs
{
    partial class frmMessageDialog
    {
        private Panel pnlIcon;
        private Label lblIcon;
        private Label lblTitle;
        private Label lblMessage;
        private Panel pnlFooter;
        private Button btnPrimary;
        private Button btnSecondary;
        private Button _tertiaryButton;

        private void InitializeComponent()
        {
            this.pnlIcon = new System.Windows.Forms.Panel();
            this.lblIcon = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnSecondary = new System.Windows.Forms.Button();
            this.btnPrimary = new System.Windows.Forms.Button();
            this._tertiaryButton = new System.Windows.Forms.Button();
            this.pnlIcon.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlIcon
            // 
            this.pnlIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(240)))), ((int)(((byte)(254)))));
            this.pnlIcon.Controls.Add(this.lblIcon);
            this.pnlIcon.Location = new System.Drawing.Point(24, 24);
            this.pnlIcon.Name = "pnlIcon";
            this.pnlIcon.Size = new System.Drawing.Size(44, 44);
            this.pnlIcon.TabIndex = 0;
            // 
            // lblIcon
            // 
            this.lblIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIcon.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lblIcon.Location = new System.Drawing.Point(0, 0);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(44, 44);
            this.lblIcon.TabIndex = 0;
            this.lblIcon.Text = "i";
            this.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoEllipsis = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTitle.Location = new System.Drawing.Point(82, 25);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(414, 42);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Hộp thoại";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMessage
            // 
            this.lblMessage.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblMessage.Location = new System.Drawing.Point(24, 88);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(472, 58);
            this.lblMessage.TabIndex = 3;
            this.lblMessage.Text = "Nội dung hộp thoại";
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlFooter.Controls.Add(this.btnSecondary);
            this.pnlFooter.Controls.Add(this.btnPrimary);
            this.pnlFooter.Controls.Add(this._tertiaryButton);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 164);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Padding = new System.Windows.Forms.Padding(16, 10, 16, 10);
            this.pnlFooter.Size = new System.Drawing.Size(520, 64);
            this.pnlFooter.TabIndex = 4;
            // 
            // btnSecondary
            // 
            this.btnSecondary.BackColor = System.Drawing.Color.White;
            this.btnSecondary.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSecondary.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnSecondary.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSecondary.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSecondary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnSecondary.Location = new System.Drawing.Point(238, 12);
            this.btnSecondary.Name = "btnSecondary";
            this.btnSecondary.Size = new System.Drawing.Size(124, 40);
            this.btnSecondary.TabIndex = 1;
            this.btnSecondary.Text = "Hủy";
            this.btnSecondary.UseVisualStyleBackColor = false;
            this.btnSecondary.Click += new System.EventHandler(this.btnSecondary_Click);
            // 
            // btnPrimary
            // 
            this.btnPrimary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnPrimary.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnPrimary.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrimary.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnPrimary.ForeColor = System.Drawing.Color.White;
            this.btnPrimary.Location = new System.Drawing.Point(372, 12);
            this.btnPrimary.Name = "btnPrimary";
            this.btnPrimary.Size = new System.Drawing.Size(124, 40);
            this.btnPrimary.TabIndex = 0;
            this.btnPrimary.Text = "Đóng";
            this.btnPrimary.UseVisualStyleBackColor = false;
            this.btnPrimary.Click += new System.EventHandler(this.btnPrimary_Click);
            // 
            // _tertiaryButton
            // 
            this._tertiaryButton.BackColor = System.Drawing.Color.White;
            this._tertiaryButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this._tertiaryButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._tertiaryButton.Font = this.btnSecondary.Font;
            this._tertiaryButton.ForeColor = this.btnSecondary.ForeColor;
            this._tertiaryButton.Location = new System.Drawing.Point(104, 12);
            this._tertiaryButton.Name = "_tertiaryButton";
            this._tertiaryButton.Size = this.btnSecondary.Size;
            this._tertiaryButton.TabIndex = 2;
            this._tertiaryButton.UseVisualStyleBackColor = false;
            this._tertiaryButton.Click += new System.EventHandler(this._tertiaryButton_Click);
            // 
            // frmMessageDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(520, 228);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pnlIcon);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmMessageDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Hộp thoại";
            this.pnlIcon.ResumeLayout(false);
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

    }
}
