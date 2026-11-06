namespace SIMS_WinFormsApp.Views.Warehouse
{
    partial class ucExpiredBatchDisposal
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this._rootLayout = new System.Windows.Forms.TableLayoutPanel();
            this._headerPanel = new System.Windows.Forms.Panel();
            this._titleLabel = new System.Windows.Forms.Label();
            this._subtitleLabel = new System.Windows.Forms.Label();
            this._statusLabel = new System.Windows.Forms.Label();
            this._detailsLayout = new System.Windows.Forms.TableLayoutPanel();
            this._productCaption = new System.Windows.Forms.Label();
            this._productValue = new System.Windows.Forms.Label();
            this._batchCaption = new System.Windows.Forms.Label();
            this._batchValue = new System.Windows.Forms.Label();
            this._expiryCaption = new System.Windows.Forms.Label();
            this._expiryValue = new System.Windows.Forms.Label();
            this._quantityCaption = new System.Windows.Forms.Label();
            this._quantityValue = new System.Windows.Forms.Label();
            this._lossCaption = new System.Windows.Forms.Label();
            this._lossValue = new System.Windows.Forms.Label();
            this._buttonsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this._confirmButton = new System.Windows.Forms.Button();
            this._cancelButton = new System.Windows.Forms.Button();
            this._rootLayout.SuspendLayout();
            this._headerPanel.SuspendLayout();
            this._detailsLayout.SuspendLayout();
            this._buttonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _rootLayout
            // 
            this._rootLayout.BackColor = System.Drawing.Color.Transparent;
            this._rootLayout.ColumnCount = 1;
            this._rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._rootLayout.Controls.Add(this._headerPanel, 0, 0);
            this._rootLayout.Controls.Add(this._statusLabel, 0, 1);
            this._rootLayout.Controls.Add(this._detailsLayout, 0, 2);
            this._rootLayout.Controls.Add(this._buttonsPanel, 0, 3);
            this._rootLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._rootLayout.Location = new System.Drawing.Point(28, 28);
            this._rootLayout.Name = "_rootLayout";
            this._rootLayout.RowCount = 4;
            this._rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this._rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this._rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 290F));
            this._rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._rootLayout.Size = new System.Drawing.Size(944, 594);
            this._rootLayout.TabIndex = 0;
            // 
            // _headerPanel
            // 
            this._headerPanel.Controls.Add(this._titleLabel);
            this._headerPanel.Controls.Add(this._subtitleLabel);
            this._headerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._headerPanel.Location = new System.Drawing.Point(3, 3);
            this._headerPanel.Name = "_headerPanel";
            this._headerPanel.Size = new System.Drawing.Size(938, 84);
            this._headerPanel.TabIndex = 0;
            // 
            // _titleLabel
            // 
            this._titleLabel.AutoSize = true;
            this._titleLabel.Font = new System.Drawing.Font("Segoe UI", 21F, System.Drawing.FontStyle.Bold);
            this._titleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._titleLabel.Location = new System.Drawing.Point(0, 0);
            this._titleLabel.Name = "_titleLabel";
            this._titleLabel.Size = new System.Drawing.Size(555, 47);
            this._titleLabel.TabIndex = 0;
            this._titleLabel.Text = "Lập phiếu tiêu hủy hàng hết hạn";
            // 
            // _subtitleLabel
            // 
            this._subtitleLabel.AutoSize = true;
            this._subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this._subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._subtitleLabel.Location = new System.Drawing.Point(2, 50);
            this._subtitleLabel.Name = "_subtitleLabel";
            this._subtitleLabel.Size = new System.Drawing.Size(425, 21);
            this._subtitleLabel.TabIndex = 1;
            this._subtitleLabel.Text = "Kiểm tra đúng lô và số lượng trước khi xác nhận trừ tồn kho.";
            // 
            // _statusLabel
            // 
            this._statusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._statusLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._statusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this._statusLabel.Location = new System.Drawing.Point(3, 90);
            this._statusLabel.Name = "_statusLabel";
            this._statusLabel.Size = new System.Drawing.Size(938, 54);
            this._statusLabel.TabIndex = 1;
            this._statusLabel.Text = "Đang tải thông tin lô...";
            this._statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _detailsLayout
            // 
            this._detailsLayout.BackColor = System.Drawing.Color.White;
            this._detailsLayout.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this._detailsLayout.ColumnCount = 2;
            this._detailsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32F));
            this._detailsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68F));
            this._detailsLayout.Controls.Add(this._productCaption, 0, 0);
            this._detailsLayout.Controls.Add(this._productValue, 1, 0);
            this._detailsLayout.Controls.Add(this._batchCaption, 0, 1);
            this._detailsLayout.Controls.Add(this._batchValue, 1, 1);
            this._detailsLayout.Controls.Add(this._expiryCaption, 0, 2);
            this._detailsLayout.Controls.Add(this._expiryValue, 1, 2);
            this._detailsLayout.Controls.Add(this._quantityCaption, 0, 3);
            this._detailsLayout.Controls.Add(this._quantityValue, 1, 3);
            this._detailsLayout.Controls.Add(this._lossCaption, 0, 4);
            this._detailsLayout.Controls.Add(this._lossValue, 1, 4);
            this._detailsLayout.Dock = System.Windows.Forms.DockStyle.Top;
            this._detailsLayout.Location = new System.Drawing.Point(3, 147);
            this._detailsLayout.Name = "_detailsLayout";
            this._detailsLayout.Padding = new System.Windows.Forms.Padding(18);
            this._detailsLayout.RowCount = 5;
            this._detailsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this._detailsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this._detailsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this._detailsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this._detailsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this._detailsLayout.Size = new System.Drawing.Size(938, 260);
            this._detailsLayout.TabIndex = 2;
            // 
            // _productCaption
            // 
            this._productCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this._productCaption.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this._productCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._productCaption.Location = new System.Drawing.Point(22, 19);
            this._productCaption.Name = "_productCaption";
            this._productCaption.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this._productCaption.Size = new System.Drawing.Size(281, 43);
            this._productCaption.TabIndex = 0;
            this._productCaption.Text = "Sản phẩm";
            this._productCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _productValue
            // 
            this._productValue.AutoEllipsis = true;
            this._productValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this._productValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._productValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._productValue.Location = new System.Drawing.Point(310, 19);
            this._productValue.Name = "_productValue";
            this._productValue.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this._productValue.Size = new System.Drawing.Size(606, 43);
            this._productValue.TabIndex = 1;
            this._productValue.Text = "—";
            this._productValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _batchCaption
            // 
            this._batchCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this._batchCaption.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this._batchCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._batchCaption.Location = new System.Drawing.Point(22, 63);
            this._batchCaption.Name = "_batchCaption";
            this._batchCaption.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this._batchCaption.Size = new System.Drawing.Size(281, 43);
            this._batchCaption.TabIndex = 2;
            this._batchCaption.Text = "Mã lô";
            this._batchCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _batchValue
            // 
            this._batchValue.AutoEllipsis = true;
            this._batchValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this._batchValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._batchValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._batchValue.Location = new System.Drawing.Point(310, 63);
            this._batchValue.Name = "_batchValue";
            this._batchValue.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this._batchValue.Size = new System.Drawing.Size(606, 43);
            this._batchValue.TabIndex = 3;
            this._batchValue.Text = "—";
            this._batchValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _expiryCaption
            // 
            this._expiryCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this._expiryCaption.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this._expiryCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._expiryCaption.Location = new System.Drawing.Point(22, 107);
            this._expiryCaption.Name = "_expiryCaption";
            this._expiryCaption.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this._expiryCaption.Size = new System.Drawing.Size(281, 43);
            this._expiryCaption.TabIndex = 4;
            this._expiryCaption.Text = "Hạn sử dụng";
            this._expiryCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _expiryValue
            // 
            this._expiryValue.AutoEllipsis = true;
            this._expiryValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this._expiryValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._expiryValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._expiryValue.Location = new System.Drawing.Point(310, 107);
            this._expiryValue.Name = "_expiryValue";
            this._expiryValue.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this._expiryValue.Size = new System.Drawing.Size(606, 43);
            this._expiryValue.TabIndex = 5;
            this._expiryValue.Text = "—";
            this._expiryValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _quantityCaption
            // 
            this._quantityCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this._quantityCaption.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this._quantityCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._quantityCaption.Location = new System.Drawing.Point(22, 151);
            this._quantityCaption.Name = "_quantityCaption";
            this._quantityCaption.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this._quantityCaption.Size = new System.Drawing.Size(281, 43);
            this._quantityCaption.TabIndex = 6;
            this._quantityCaption.Text = "Số lượng tiêu hủy";
            this._quantityCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _quantityValue
            // 
            this._quantityValue.AutoEllipsis = true;
            this._quantityValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this._quantityValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._quantityValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._quantityValue.Location = new System.Drawing.Point(310, 151);
            this._quantityValue.Name = "_quantityValue";
            this._quantityValue.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this._quantityValue.Size = new System.Drawing.Size(606, 43);
            this._quantityValue.TabIndex = 7;
            this._quantityValue.Text = "—";
            this._quantityValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lossCaption
            // 
            this._lossCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lossCaption.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this._lossCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._lossCaption.Location = new System.Drawing.Point(22, 195);
            this._lossCaption.Name = "_lossCaption";
            this._lossCaption.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this._lossCaption.Size = new System.Drawing.Size(281, 46);
            this._lossCaption.TabIndex = 8;
            this._lossCaption.Text = "Giá trị thiệt hại";
            this._lossCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lossValue
            // 
            this._lossValue.AutoEllipsis = true;
            this._lossValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lossValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._lossValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._lossValue.Location = new System.Drawing.Point(310, 195);
            this._lossValue.Name = "_lossValue";
            this._lossValue.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this._lossValue.Size = new System.Drawing.Size(606, 46);
            this._lossValue.TabIndex = 9;
            this._lossValue.Text = "—";
            this._lossValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _buttonsPanel
            // 
            this._buttonsPanel.Controls.Add(this._confirmButton);
            this._buttonsPanel.Controls.Add(this._cancelButton);
            this._buttonsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._buttonsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._buttonsPanel.Location = new System.Drawing.Point(3, 437);
            this._buttonsPanel.Name = "_buttonsPanel";
            this._buttonsPanel.Padding = new System.Windows.Forms.Padding(0, 16, 0, 0);
            this._buttonsPanel.Size = new System.Drawing.Size(938, 56);
            this._buttonsPanel.TabIndex = 3;
            this._buttonsPanel.WrapContents = false;
            // 
            // _confirmButton
            // 
            this._confirmButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this._confirmButton.Enabled = false;
            this._confirmButton.FlatAppearance.BorderSize = 0;
            this._confirmButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._confirmButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._confirmButton.ForeColor = System.Drawing.Color.White;
            this._confirmButton.Location = new System.Drawing.Point(758, 16);
            this._confirmButton.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this._confirmButton.Name = "_confirmButton";
            this._confirmButton.Size = new System.Drawing.Size(180, 40);
            this._confirmButton.TabIndex = 0;
            this._confirmButton.Text = "Xác nhận lập phiếu";
            this._confirmButton.UseVisualStyleBackColor = false;
            this._confirmButton.Click += new System.EventHandler(this._confirmButton_Click);
            // 
            // _cancelButton
            // 
            this._cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._cancelButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._cancelButton.Location = new System.Drawing.Point(570, 16);
            this._cancelButton.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(180, 40);
            this._cancelButton.TabIndex = 1;
            this._cancelButton.Text = "Quay lại danh sách lô";
            this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
            // 
            // ucExpiredBatchDisposal
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.Controls.Add(this._rootLayout);
            this.Name = "ucExpiredBatchDisposal";
            this.Padding = new System.Windows.Forms.Padding(28);
            this.Size = new System.Drawing.Size(1000, 650);
            this._rootLayout.ResumeLayout(false);
            this._headerPanel.ResumeLayout(false);
            this._headerPanel.PerformLayout();
            this._detailsLayout.ResumeLayout(false);
            this._buttonsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TableLayoutPanel _rootLayout;
        private System.Windows.Forms.Panel _headerPanel;
        private System.Windows.Forms.Label _titleLabel;
        private System.Windows.Forms.Label _subtitleLabel;
        private System.Windows.Forms.Label _statusLabel;
        private System.Windows.Forms.TableLayoutPanel _detailsLayout;
        private System.Windows.Forms.Label _productCaption;
        private System.Windows.Forms.Label _productValue;
        private System.Windows.Forms.Label _batchCaption;
        private System.Windows.Forms.Label _batchValue;
        private System.Windows.Forms.Label _expiryCaption;
        private System.Windows.Forms.Label _expiryValue;
        private System.Windows.Forms.Label _quantityCaption;
        private System.Windows.Forms.Label _quantityValue;
        private System.Windows.Forms.Label _lossCaption;
        private System.Windows.Forms.Label _lossValue;
        private System.Windows.Forms.FlowLayoutPanel _buttonsPanel;
        private System.Windows.Forms.Button _confirmButton;
        private System.Windows.Forms.Button _cancelButton;
    }
}