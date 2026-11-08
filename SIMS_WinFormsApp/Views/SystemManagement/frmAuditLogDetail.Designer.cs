using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Views.SystemManagement
{
    partial class frmAuditLogDetail
    {
        private PrimaryButton _closeButton;
        private TableLayoutPanel _detailInfoGrid;
        private Panel _previewTimeField;
        private Panel _previewUserField;
        private Panel _previewActionField;
        private Panel _previewTargetField;
        private Panel _previewIpField;
        private Panel _previewDescriptionField;
        private Label _previewTimeLabel;
        private Label _previewUserLabel;
        private Label _previewActionLabel;
        private Label _previewTargetLabel;
        private Label _previewIpLabel;
        private Label _previewDescriptionLabel;
        private Label _previewTimeValue;
        private Label _previewUserValue;
        private Label _previewActionValue;
        private Label _previewTargetValue;
        private Label _previewIpValue;
        private Label _previewDescriptionValue;
        private Label _detailDiffHeader;
        private Panel _detailDiffHost;
        private Label _previewNoDiffLabel;

        private void InitializeComponent()
        {
            this._closeButton = new SIMS_WinFormsApp.UI.Controls.PrimaryButton();
            this._detailInfoGrid = new System.Windows.Forms.TableLayoutPanel();
            this._previewTimeField = new System.Windows.Forms.Panel();
            this._previewTimeValue = new System.Windows.Forms.Label();
            this._previewTimeLabel = new System.Windows.Forms.Label();
            this._previewUserField = new System.Windows.Forms.Panel();
            this._previewUserValue = new System.Windows.Forms.Label();
            this._previewUserLabel = new System.Windows.Forms.Label();
            this._previewActionField = new System.Windows.Forms.Panel();
            this._previewActionValue = new System.Windows.Forms.Label();
            this._previewActionLabel = new System.Windows.Forms.Label();
            this._previewTargetField = new System.Windows.Forms.Panel();
            this._previewTargetValue = new System.Windows.Forms.Label();
            this._previewTargetLabel = new System.Windows.Forms.Label();
            this._previewIpField = new System.Windows.Forms.Panel();
            this._previewIpValue = new System.Windows.Forms.Label();
            this._previewIpLabel = new System.Windows.Forms.Label();
            this._previewDescriptionField = new System.Windows.Forms.Panel();
            this._previewDescriptionValue = new System.Windows.Forms.Label();
            this._previewDescriptionLabel = new System.Windows.Forms.Label();
            this._detailDiffHeader = new System.Windows.Forms.Label();
            this._detailDiffHost = new System.Windows.Forms.Panel();
            this._previewNoDiffLabel = new System.Windows.Forms.Label();
            this._bodyScrollPanel.SuspendLayout();
            this._footerPanel.SuspendLayout();
            this._detailInfoGrid.SuspendLayout();
            this._previewTimeField.SuspendLayout();
            this._previewUserField.SuspendLayout();
            this._previewActionField.SuspendLayout();
            this._previewTargetField.SuspendLayout();
            this._previewIpField.SuspendLayout();
            this._previewDescriptionField.SuspendLayout();
            this._detailDiffHost.SuspendLayout();
            this.SuspendLayout();
            // 
            // _bodyScrollPanel
            // 
            this._bodyScrollPanel.Controls.Add(this._detailDiffHost);
            this._bodyScrollPanel.Controls.Add(this._detailDiffHeader);
            this._bodyScrollPanel.Controls.Add(this._detailInfoGrid);
            this._bodyScrollPanel.Size = new System.Drawing.Size(896, 576);
            // 
            // _footerPanel
            // 
            this._footerPanel.Controls.Add(this._closeButton);
            this._footerPanel.Size = new System.Drawing.Size(896, 76);
            // 
            // _closeButton
            // 
            this._closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._closeButton.BackColor = System.Drawing.Color.Transparent;
            this._closeButton.CornerRadius = 10;
            this._closeButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this._closeButton.CustomAccentColor = null;
            this._closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._closeButton.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this._closeButton.ForeColor = System.Drawing.Color.White;
            this._closeButton.Icon = null;
            this._closeButton.IconSize = 16;
            this._closeButton.IsPrimary = true;
            this._closeButton.Location = new System.Drawing.Point(764, 22);
            this._closeButton.MinimumSize = new System.Drawing.Size(120, 42);
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(120, 44);
            this._closeButton.TabIndex = 0;
            this._closeButton.Text = "Đóng";
            this._closeButton.UseVisualStyleBackColor = false;
            this._closeButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // _detailInfoGrid
            // 
            this._detailInfoGrid.AutoSize = true;
            this._detailInfoGrid.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._detailInfoGrid.ColumnCount = 2;
            this._detailInfoGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._detailInfoGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._detailInfoGrid.Controls.Add(this._previewTimeField, 0, 0);
            this._detailInfoGrid.Controls.Add(this._previewUserField, 1, 0);
            this._detailInfoGrid.Controls.Add(this._previewActionField, 0, 1);
            this._detailInfoGrid.Controls.Add(this._previewTargetField, 1, 1);
            this._detailInfoGrid.Controls.Add(this._previewIpField, 0, 2);
            this._detailInfoGrid.Controls.Add(this._previewDescriptionField, 1, 2);
            this._detailInfoGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this._detailInfoGrid.Location = new System.Drawing.Point(28, 20);
            this._detailInfoGrid.Margin = new System.Windows.Forms.Padding(0, 0, 0, 16);
            this._detailInfoGrid.Name = "_detailInfoGrid";
            this._detailInfoGrid.RowCount = 3;
            this._detailInfoGrid.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._detailInfoGrid.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._detailInfoGrid.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._detailInfoGrid.Size = new System.Drawing.Size(840, 240);
            this._detailInfoGrid.TabIndex = 2;
            // 
            // _previewTimeField
            // 
            this._previewTimeField.AutoSize = true;
            this._previewTimeField.Controls.Add(this._previewTimeValue);
            this._previewTimeField.Controls.Add(this._previewTimeLabel);
            this._previewTimeField.Dock = System.Windows.Forms.DockStyle.Fill;
            this._previewTimeField.Location = new System.Drawing.Point(0, 0);
            this._previewTimeField.Margin = new System.Windows.Forms.Padding(0, 0, 12, 12);
            this._previewTimeField.Name = "_previewTimeField";
            this._previewTimeField.Size = new System.Drawing.Size(408, 68);
            this._previewTimeField.TabIndex = 0;
            // 
            // _previewTimeValue
            // 
            this._previewTimeValue.AutoSize = true;
            this._previewTimeValue.BackColor = System.Drawing.Color.Transparent;
            this._previewTimeValue.Dock = System.Windows.Forms.DockStyle.Top;
            this._previewTimeValue.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this._previewTimeValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._previewTimeValue.Location = new System.Drawing.Point(0, 32);
            this._previewTimeValue.MaximumSize = new System.Drawing.Size(340, 0);
            this._previewTimeValue.Name = "_previewTimeValue";
            this._previewTimeValue.Size = new System.Drawing.Size(270, 36);
            this._previewTimeValue.TabIndex = 0;
            this._previewTimeValue.Text = "01/01/2026 12:00:00";
            // 
            // _previewTimeLabel
            // 
            this._previewTimeLabel.BackColor = System.Drawing.Color.Transparent;
            this._previewTimeLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._previewTimeLabel.Font = new System.Drawing.Font("Segoe UI", 12F);
            this._previewTimeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._previewTimeLabel.Location = new System.Drawing.Point(0, 0);
            this._previewTimeLabel.Name = "_previewTimeLabel";
            this._previewTimeLabel.Size = new System.Drawing.Size(408, 32);
            this._previewTimeLabel.TabIndex = 1;
            this._previewTimeLabel.Text = "Thời gian";
            // 
            // _previewUserField
            // 
            this._previewUserField.AutoSize = true;
            this._previewUserField.Controls.Add(this._previewUserValue);
            this._previewUserField.Controls.Add(this._previewUserLabel);
            this._previewUserField.Dock = System.Windows.Forms.DockStyle.Fill;
            this._previewUserField.Location = new System.Drawing.Point(420, 0);
            this._previewUserField.Margin = new System.Windows.Forms.Padding(0, 0, 12, 12);
            this._previewUserField.Name = "_previewUserField";
            this._previewUserField.Size = new System.Drawing.Size(408, 68);
            this._previewUserField.TabIndex = 1;
            // 
            // _previewUserValue
            // 
            this._previewUserValue.AutoSize = true;
            this._previewUserValue.BackColor = System.Drawing.Color.Transparent;
            this._previewUserValue.Dock = System.Windows.Forms.DockStyle.Top;
            this._previewUserValue.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this._previewUserValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._previewUserValue.Location = new System.Drawing.Point(0, 32);
            this._previewUserValue.MaximumSize = new System.Drawing.Size(340, 0);
            this._previewUserValue.Name = "_previewUserValue";
            this._previewUserValue.Size = new System.Drawing.Size(208, 36);
            this._previewUserValue.TabIndex = 0;
            this._previewUserValue.Text = "Tên người dùng";
            // 
            // _previewUserLabel
            // 
            this._previewUserLabel.BackColor = System.Drawing.Color.Transparent;
            this._previewUserLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._previewUserLabel.Font = new System.Drawing.Font("Segoe UI", 12F);
            this._previewUserLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._previewUserLabel.Location = new System.Drawing.Point(0, 0);
            this._previewUserLabel.Name = "_previewUserLabel";
            this._previewUserLabel.Size = new System.Drawing.Size(408, 32);
            this._previewUserLabel.TabIndex = 1;
            this._previewUserLabel.Text = "Người dùng";
            // 
            // _previewActionField
            // 
            this._previewActionField.AutoSize = true;
            this._previewActionField.Controls.Add(this._previewActionValue);
            this._previewActionField.Controls.Add(this._previewActionLabel);
            this._previewActionField.Dock = System.Windows.Forms.DockStyle.Fill;
            this._previewActionField.Location = new System.Drawing.Point(0, 80);
            this._previewActionField.Margin = new System.Windows.Forms.Padding(0, 0, 12, 12);
            this._previewActionField.Name = "_previewActionField";
            this._previewActionField.Size = new System.Drawing.Size(408, 68);
            this._previewActionField.TabIndex = 2;
            // 
            // _previewActionValue
            // 
            this._previewActionValue.AutoSize = true;
            this._previewActionValue.BackColor = System.Drawing.Color.Transparent;
            this._previewActionValue.Dock = System.Windows.Forms.DockStyle.Top;
            this._previewActionValue.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this._previewActionValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._previewActionValue.Location = new System.Drawing.Point(0, 32);
            this._previewActionValue.MaximumSize = new System.Drawing.Size(340, 0);
            this._previewActionValue.Name = "_previewActionValue";
            this._previewActionValue.Size = new System.Drawing.Size(124, 36);
            this._previewActionValue.TabIndex = 0;
            this._previewActionValue.Text = "Cập nhật";
            // 
            // _previewActionLabel
            // 
            this._previewActionLabel.BackColor = System.Drawing.Color.Transparent;
            this._previewActionLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._previewActionLabel.Font = new System.Drawing.Font("Segoe UI", 12F);
            this._previewActionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._previewActionLabel.Location = new System.Drawing.Point(0, 0);
            this._previewActionLabel.Name = "_previewActionLabel";
            this._previewActionLabel.Size = new System.Drawing.Size(408, 32);
            this._previewActionLabel.TabIndex = 1;
            this._previewActionLabel.Text = "Hành động";
            // 
            // _previewTargetField
            // 
            this._previewTargetField.AutoSize = true;
            this._previewTargetField.Controls.Add(this._previewTargetValue);
            this._previewTargetField.Controls.Add(this._previewTargetLabel);
            this._previewTargetField.Dock = System.Windows.Forms.DockStyle.Fill;
            this._previewTargetField.Location = new System.Drawing.Point(420, 80);
            this._previewTargetField.Margin = new System.Windows.Forms.Padding(0, 0, 12, 12);
            this._previewTargetField.Name = "_previewTargetField";
            this._previewTargetField.Size = new System.Drawing.Size(408, 68);
            this._previewTargetField.TabIndex = 3;
            // 
            // _previewTargetValue
            // 
            this._previewTargetValue.AutoSize = true;
            this._previewTargetValue.BackColor = System.Drawing.Color.Transparent;
            this._previewTargetValue.Dock = System.Windows.Forms.DockStyle.Top;
            this._previewTargetValue.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this._previewTargetValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._previewTargetValue.Location = new System.Drawing.Point(0, 32);
            this._previewTargetValue.MaximumSize = new System.Drawing.Size(340, 0);
            this._previewTargetValue.Name = "_previewTargetValue";
            this._previewTargetValue.Size = new System.Drawing.Size(108, 36);
            this._previewTargetValue.TabIndex = 0;
            this._previewTargetValue.Text = "Bản ghi";
            // 
            // _previewTargetLabel
            // 
            this._previewTargetLabel.BackColor = System.Drawing.Color.Transparent;
            this._previewTargetLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._previewTargetLabel.Font = new System.Drawing.Font("Segoe UI", 12F);
            this._previewTargetLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._previewTargetLabel.Location = new System.Drawing.Point(0, 0);
            this._previewTargetLabel.Name = "_previewTargetLabel";
            this._previewTargetLabel.Size = new System.Drawing.Size(408, 32);
            this._previewTargetLabel.TabIndex = 1;
            this._previewTargetLabel.Text = "Đối tượng";
            // 
            // _previewIpField
            // 
            this._previewIpField.AutoSize = true;
            this._previewIpField.Controls.Add(this._previewIpValue);
            this._previewIpField.Controls.Add(this._previewIpLabel);
            this._previewIpField.Dock = System.Windows.Forms.DockStyle.Fill;
            this._previewIpField.Location = new System.Drawing.Point(0, 160);
            this._previewIpField.Margin = new System.Windows.Forms.Padding(0, 0, 12, 12);
            this._previewIpField.Name = "_previewIpField";
            this._previewIpField.Size = new System.Drawing.Size(408, 68);
            this._previewIpField.TabIndex = 4;
            // 
            // _previewIpValue
            // 
            this._previewIpValue.AutoSize = true;
            this._previewIpValue.BackColor = System.Drawing.Color.Transparent;
            this._previewIpValue.Dock = System.Windows.Forms.DockStyle.Top;
            this._previewIpValue.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this._previewIpValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._previewIpValue.Location = new System.Drawing.Point(0, 32);
            this._previewIpValue.MaximumSize = new System.Drawing.Size(340, 0);
            this._previewIpValue.Name = "_previewIpValue";
            this._previewIpValue.Size = new System.Drawing.Size(26, 36);
            this._previewIpValue.TabIndex = 0;
            this._previewIpValue.Text = "-";
            // 
            // _previewIpLabel
            // 
            this._previewIpLabel.BackColor = System.Drawing.Color.Transparent;
            this._previewIpLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._previewIpLabel.Font = new System.Drawing.Font("Segoe UI", 12F);
            this._previewIpLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._previewIpLabel.Location = new System.Drawing.Point(0, 0);
            this._previewIpLabel.Name = "_previewIpLabel";
            this._previewIpLabel.Size = new System.Drawing.Size(408, 32);
            this._previewIpLabel.TabIndex = 1;
            this._previewIpLabel.Text = "Địa chỉ IP";
            // 
            // _previewDescriptionField
            // 
            this._previewDescriptionField.AutoSize = true;
            this._previewDescriptionField.Controls.Add(this._previewDescriptionValue);
            this._previewDescriptionField.Controls.Add(this._previewDescriptionLabel);
            this._previewDescriptionField.Dock = System.Windows.Forms.DockStyle.Fill;
            this._previewDescriptionField.Location = new System.Drawing.Point(420, 160);
            this._previewDescriptionField.Margin = new System.Windows.Forms.Padding(0, 0, 12, 12);
            this._previewDescriptionField.Name = "_previewDescriptionField";
            this._previewDescriptionField.Size = new System.Drawing.Size(408, 68);
            this._previewDescriptionField.TabIndex = 5;
            // 
            // _previewDescriptionValue
            // 
            this._previewDescriptionValue.AutoSize = true;
            this._previewDescriptionValue.BackColor = System.Drawing.Color.Transparent;
            this._previewDescriptionValue.Dock = System.Windows.Forms.DockStyle.Top;
            this._previewDescriptionValue.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this._previewDescriptionValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._previewDescriptionValue.Location = new System.Drawing.Point(0, 32);
            this._previewDescriptionValue.MaximumSize = new System.Drawing.Size(340, 0);
            this._previewDescriptionValue.Name = "_previewDescriptionValue";
            this._previewDescriptionValue.Size = new System.Drawing.Size(237, 36);
            this._previewDescriptionValue.TabIndex = 0;
            this._previewDescriptionValue.Text = "Nội dung thay đổi";
            // 
            // _previewDescriptionLabel
            // 
            this._previewDescriptionLabel.BackColor = System.Drawing.Color.Transparent;
            this._previewDescriptionLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._previewDescriptionLabel.Font = new System.Drawing.Font("Segoe UI", 12F);
            this._previewDescriptionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._previewDescriptionLabel.Location = new System.Drawing.Point(0, 0);
            this._previewDescriptionLabel.Name = "_previewDescriptionLabel";
            this._previewDescriptionLabel.Size = new System.Drawing.Size(408, 32);
            this._previewDescriptionLabel.TabIndex = 1;
            this._previewDescriptionLabel.Text = "Mô tả";
            // 
            // _detailDiffHeader
            // 
            this._detailDiffHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this._detailDiffHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this._detailDiffHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._detailDiffHeader.Location = new System.Drawing.Point(28, 260);
            this._detailDiffHeader.Name = "_detailDiffHeader";
            this._detailDiffHeader.Size = new System.Drawing.Size(840, 32);
            this._detailDiffHeader.TabIndex = 1;
            this._detailDiffHeader.Text = "Thay đổi";
            this._detailDiffHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _detailDiffHost
            // 
            this._detailDiffHost.BackColor = System.Drawing.Color.Transparent;
            this._detailDiffHost.Controls.Add(this._previewNoDiffLabel);
            this._detailDiffHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this._detailDiffHost.Location = new System.Drawing.Point(28, 292);
            this._detailDiffHost.Name = "_detailDiffHost";
            this._detailDiffHost.Size = new System.Drawing.Size(840, 264);
            this._detailDiffHost.TabIndex = 0;
            // 
            // _previewNoDiffLabel
            // 
            this._previewNoDiffLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._previewNoDiffLabel.Font = new System.Drawing.Font("Segoe UI", 13F);
            this._previewNoDiffLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._previewNoDiffLabel.Location = new System.Drawing.Point(0, 0);
            this._previewNoDiffLabel.Name = "_previewNoDiffLabel";
            this._previewNoDiffLabel.Size = new System.Drawing.Size(840, 264);
            this._previewNoDiffLabel.TabIndex = 0;
            this._previewNoDiffLabel.Text = "Bản ghi này không có dữ liệu trước/sau để so sánh.";
            this._previewNoDiffLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmAuditLogDetail
            // 
            this.ClientSize = new System.Drawing.Size(900, 720);
            this.HeaderTitle = "Chi tiết nhật ký";
            this.Name = "frmAuditLogDetail";
            this.Text = "Chi tiết nhật ký";
            this._bodyScrollPanel.ResumeLayout(false);
            this._bodyScrollPanel.PerformLayout();
            this._footerPanel.ResumeLayout(false);
            this._detailInfoGrid.ResumeLayout(false);
            this._detailInfoGrid.PerformLayout();
            this._previewTimeField.ResumeLayout(false);
            this._previewTimeField.PerformLayout();
            this._previewUserField.ResumeLayout(false);
            this._previewUserField.PerformLayout();
            this._previewActionField.ResumeLayout(false);
            this._previewActionField.PerformLayout();
            this._previewTargetField.ResumeLayout(false);
            this._previewTargetField.PerformLayout();
            this._previewIpField.ResumeLayout(false);
            this._previewIpField.PerformLayout();
            this._previewDescriptionField.ResumeLayout(false);
            this._previewDescriptionField.PerformLayout();
            this._detailDiffHost.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
