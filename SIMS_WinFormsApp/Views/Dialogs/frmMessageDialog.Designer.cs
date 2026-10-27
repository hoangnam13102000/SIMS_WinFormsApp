using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Views.Dialogs
{
    partial class frmMessageDialog
    {
        private Panel pnlIcon;
        private Label lblIcon;
        private Label lblTitle;
        private Button btnClose;
        private Label lblMessage;
        private Panel pnlFooter;
        private Button btnPrimary;
        private Button btnSecondary;

        private void InitializeComponent()
        {
            pnlIcon = new Panel();
            lblIcon = new Label();
            lblTitle = new Label();
            btnClose = new Button();
            lblMessage = new Label();
            pnlFooter = new Panel();
            btnPrimary = new Button();
            btnSecondary = new Button();
            pnlIcon.SuspendLayout();
            pnlFooter.SuspendLayout();
            SuspendLayout();

            pnlIcon.BackColor = Color.FromArgb(232, 240, 254);
            pnlIcon.Controls.Add(lblIcon);
            pnlIcon.Location = new Point(24, 24);
            pnlIcon.Name = "pnlIcon";
            pnlIcon.Size = new Size(44, 44);
            pnlIcon.TabIndex = 0;

            lblIcon.Dock = DockStyle.Fill;
            lblIcon.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblIcon.ForeColor = Color.FromArgb(37, 99, 235);
            lblIcon.Location = new Point(0, 0);
            lblIcon.Name = "lblIcon";
            lblIcon.Size = new Size(44, 44);
            lblIcon.TabIndex = 0;
            lblIcon.Text = "i";
            lblIcon.TextAlign = ContentAlignment.MiddleCenter;

            lblTitle.AutoEllipsis = true;
            lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblTitle.Location = new Point(82, 25);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(360, 42);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Hộp thoại";
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;

            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 13F);
            btnClose.ForeColor = Color.FromArgb(100, 116, 139);
            btnClose.Location = new Point(464, 24);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(32, 32);
            btnClose.TabIndex = 2;
            btnClose.Text = "×";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;

            lblMessage.AutoEllipsis = false;
            lblMessage.Font = new Font("Segoe UI", 9.5F);
            lblMessage.ForeColor = Color.FromArgb(71, 85, 105);
            lblMessage.Location = new Point(24, 88);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(472, 58);
            lblMessage.TabIndex = 3;
            lblMessage.Text = "Nội dung hộp thoại";

            pnlFooter.BackColor = Color.FromArgb(248, 250, 252);
            pnlFooter.Controls.Add(btnSecondary);
            pnlFooter.Controls.Add(btnPrimary);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(0, 164);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Padding = new Padding(16, 10, 16, 10);
            pnlFooter.Size = new Size(520, 64);
            pnlFooter.TabIndex = 4;

            btnPrimary.BackColor = Color.FromArgb(37, 99, 235);
            btnPrimary.DialogResult = DialogResult.OK;
            btnPrimary.FlatAppearance.BorderSize = 1;
            btnPrimary.FlatStyle = FlatStyle.Flat;
            btnPrimary.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnPrimary.ForeColor = Color.White;
            btnPrimary.Location = new Point(372, 12);
            btnPrimary.Name = "btnPrimary";
            btnPrimary.Size = new Size(124, 40);
            btnPrimary.TabIndex = 0;
            btnPrimary.Text = "Đóng";
            btnPrimary.UseVisualStyleBackColor = false;

            btnSecondary.BackColor = Color.White;
            btnSecondary.DialogResult = DialogResult.Cancel;
            btnSecondary.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnSecondary.FlatStyle = FlatStyle.Flat;
            btnSecondary.Font = new Font("Segoe UI", 9F);
            btnSecondary.ForeColor = Color.FromArgb(51, 65, 85);
            btnSecondary.Location = new Point(238, 12);
            btnSecondary.Name = "btnSecondary";
            btnSecondary.Size = new Size(124, 40);
            btnSecondary.TabIndex = 1;
            btnSecondary.Text = "Hủy";
            btnSecondary.UseVisualStyleBackColor = false;
            btnSecondary.Visible = true;

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(520, 228);
            Controls.Add(pnlFooter);
            Controls.Add(lblMessage);
            Controls.Add(btnClose);
            Controls.Add(lblTitle);
            Controls.Add(pnlIcon);
            Font = new Font("Segoe UI", 9F);
            Name = "frmMessageDialog";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Hộp thoại";
            pnlFooter.ResumeLayout(false);
            pnlIcon.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
