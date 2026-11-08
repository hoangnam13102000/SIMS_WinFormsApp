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
        private TableLayoutPanel _dialogLayout;
        private Panel _headerPanel;
        private FlowLayoutPanel _buttonFlow;
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
            this._dialogLayout = new System.Windows.Forms.TableLayoutPanel();
            this._headerPanel = new System.Windows.Forms.Panel();
            this._buttonFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSecondary = new System.Windows.Forms.Button();
            this.btnPrimary = new System.Windows.Forms.Button();
            this._tertiaryButton = new System.Windows.Forms.Button();
            this.pnlIcon.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this._dialogLayout.SuspendLayout();
            this._headerPanel.SuspendLayout();
            this._buttonFlow.SuspendLayout();
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
            // _headerPanel
            // 
            this._headerPanel.Controls.Add(this.pnlIcon);
            this._headerPanel.Controls.Add(this.lblTitle);
            this._headerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._headerPanel.Name = "_headerPanel";
            this._headerPanel.Size = new System.Drawing.Size(520, 72);
            this._headerPanel.TabIndex = 0;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblMessage.Margin = new System.Windows.Forms.Padding(24, 16, 24, 18);
            this.lblMessage.MaximumSize = new System.Drawing.Size(472, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.TabIndex = 3;
            this.lblMessage.Text = "Nội dung hộp thoại";
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlFooter.Controls.Add(this._buttonFlow);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(520, 64);
            this.pnlFooter.TabIndex = 4;
            // 
            // _buttonFlow
            // 
            this._buttonFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this._buttonFlow.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._buttonFlow.Margin = new System.Windows.Forms.Padding(0);
            this._buttonFlow.Name = "_buttonFlow";
            this._buttonFlow.Padding = new System.Windows.Forms.Padding(0, 10, 24, 0);
            this._buttonFlow.WrapContents = false;
            this._buttonFlow.Controls.Add(this.btnPrimary);
            this._buttonFlow.Controls.Add(this.btnSecondary);
            this._buttonFlow.Controls.Add(this._tertiaryButton);
            // 
            // btnSecondary
            // 
            this.btnSecondary.BackColor = System.Drawing.Color.White;
            this.btnSecondary.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSecondary.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnSecondary.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSecondary.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSecondary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnSecondary.Margin = new System.Windows.Forms.Padding(5);
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
            this.btnPrimary.Margin = new System.Windows.Forms.Padding(5);
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
            this._tertiaryButton.Margin = new System.Windows.Forms.Padding(5);
            this._tertiaryButton.Name = "_tertiaryButton";
            this._tertiaryButton.Size = this.btnSecondary.Size;
            this._tertiaryButton.TabIndex = 2;
            this._tertiaryButton.Visible = false;
            this._tertiaryButton.UseVisualStyleBackColor = false;
            this._tertiaryButton.Click += new System.EventHandler(this._tertiaryButton_Click);
            // 
            // frmMessageDialog
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(520, 228);
            this.Controls.Add(this._dialogLayout);
            this._dialogLayout.ColumnCount = 1;
            this._dialogLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._dialogLayout.Controls.Add(this._headerPanel, 0, 0);
            this._dialogLayout.Controls.Add(this.lblMessage, 0, 1);
            this._dialogLayout.Controls.Add(this.pnlFooter, 0, 2);
            this._dialogLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dialogLayout.Margin = new System.Windows.Forms.Padding(0);
            this._dialogLayout.Name = "_dialogLayout";
            this._dialogLayout.RowCount = 3;
            this._dialogLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this._dialogLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this._dialogLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this._dialogLayout.Size = new System.Drawing.Size(520, 228);
            this._dialogLayout.TabIndex = 0;
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmMessageDialog";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.KeyPreview = true;
            this.Text = "Hộp thoại";
            this.pnlIcon.ResumeLayout(false);
            this.pnlFooter.ResumeLayout(false);
            this._buttonFlow.ResumeLayout(false);
            this._headerPanel.ResumeLayout(false);
            this._dialogLayout.ResumeLayout(false);
            this._dialogLayout.PerformLayout();
            this.ResumeLayout(false);

        }

    }
}
