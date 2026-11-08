using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Views.Sales.InvoiceManagement
{
    partial class InvoiceReturnDialog
    {
        private IContainer components;
        private TableLayoutPanel _rootLayout;
        private Panel _headerPanel;
        private Label _headerTitle;
        private Label _policyLabel;
        private CheckBox _selectAllCheckBox;
        private DataGridView _linesGrid;
        private DataGridViewCheckBoxColumn _selectedColumn;
        private DataGridViewTextBoxColumn _productColumn;
        private DataGridViewTextBoxColumn _soldColumn;
        private DataGridViewTextBoxColumn _returnedColumn;
        private DataGridViewTextBoxColumn _quantityColumn;
        private DataGridViewTextBoxColumn _priceColumn;
        private TableLayoutPanel _footerPanel;
        private FlowLayoutPanel _reasonPanel;
        private Label _reasonLabel;
        private ComboBox _reasonCombo;
        private TextBox _reasonDetails;
        private Label _summaryLabel;
        private FlowLayoutPanel _actionButtonsPanel;
        private Label _noteLabel;
        private Button _cancelButton;
        private Button _submitButton;

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this._rootLayout = new System.Windows.Forms.TableLayoutPanel();
            this._headerPanel = new System.Windows.Forms.Panel();
            this._headerTitle = new System.Windows.Forms.Label();
            this._policyLabel = new System.Windows.Forms.Label();
            this._selectAllCheckBox = new System.Windows.Forms.CheckBox();
            this._linesGrid = new System.Windows.Forms.DataGridView();
            this.Selected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Product = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sold = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Returned = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._footerPanel = new System.Windows.Forms.TableLayoutPanel();
            this._reasonPanel = new System.Windows.Forms.FlowLayoutPanel();
            this._reasonLabel = new System.Windows.Forms.Label();
            this._reasonCombo = new System.Windows.Forms.ComboBox();
            this._reasonDetails = new System.Windows.Forms.TextBox();
            this._summaryLabel = new System.Windows.Forms.Label();
            this._actionButtonsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this._cancelButton = new System.Windows.Forms.Button();
            this._submitButton = new System.Windows.Forms.Button();
            this._noteLabel = new System.Windows.Forms.Label();
            this._rootLayout.SuspendLayout();
            this._headerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._linesGrid)).BeginInit();
            this._footerPanel.SuspendLayout();
            this._reasonPanel.SuspendLayout();
            this._actionButtonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _rootLayout
            // 
            this._rootLayout.ColumnCount = 1;
            this._rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._rootLayout.Controls.Add(this._headerPanel, 0, 0);
            this._rootLayout.Controls.Add(this._linesGrid, 0, 1);
            this._rootLayout.Controls.Add(this._footerPanel, 0, 2);
            this._rootLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._rootLayout.Location = new System.Drawing.Point(0, 0);
            this._rootLayout.Name = "_rootLayout";
            this._rootLayout.Padding = new System.Windows.Forms.Padding(20);
            this._rootLayout.RowCount = 3;
            this._rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this._rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this._rootLayout.Size = new System.Drawing.Size(820, 600);
            this._rootLayout.TabIndex = 0;
            // 
            // _headerPanel
            // 
            this._headerPanel.Controls.Add(this._headerTitle);
            this._headerPanel.Controls.Add(this._policyLabel);
            this._headerPanel.Controls.Add(this._selectAllCheckBox);
            this._headerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._headerPanel.Location = new System.Drawing.Point(23, 23);
            this._headerPanel.Name = "_headerPanel";
            this._headerPanel.Size = new System.Drawing.Size(774, 66);
            this._headerPanel.TabIndex = 0;
            // 
            // _headerTitle
            // 
            this._headerTitle.AutoSize = true;
            this._headerTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this._headerTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._headerTitle.Location = new System.Drawing.Point(0, 0);
            this._headerTitle.Name = "_headerTitle";
            this._headerTitle.Size = new System.Drawing.Size(595, 45);
            this._headerTitle.TabIndex = 0;
            this._headerTitle.Text = "Trả hàng từ hóa đơn HD-MẪU-000001";
            // 
            // _policyLabel
            // 
            this._policyLabel.AutoSize = true;
            this._policyLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._policyLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._policyLabel.Location = new System.Drawing.Point(2, 38);
            this._policyLabel.Name = "_policyLabel";
            this._policyLabel.Size = new System.Drawing.Size(395, 25);
            this._policyLabel.TabIndex = 1;
            this._policyLabel.Text = "Hóa đơn ngày 01/01/2026 • Thời hạn trả 7 ngày";
            // 
            // _selectAllCheckBox
            // 
            this._selectAllCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._selectAllCheckBox.AutoSize = true;
            this._selectAllCheckBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._selectAllCheckBox.Location = new System.Drawing.Point(1040, 43);
            this._selectAllCheckBox.Name = "_selectAllCheckBox";
            this._selectAllCheckBox.Size = new System.Drawing.Size(228, 29);
            this._selectAllCheckBox.TabIndex = 2;
            this._selectAllCheckBox.Text = "Chọn toàn bộ hóa đơn";
            this._selectAllCheckBox.CheckedChanged += new System.EventHandler(this.SelectAllCheckBox_CheckedChanged);
            // 
            // _linesGrid
            // 
            this._linesGrid.AllowUserToAddRows = false;
            this._linesGrid.AllowUserToDeleteRows = false;
            this._linesGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._linesGrid.BackgroundColor = System.Drawing.Color.White;
            this._linesGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._linesGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this._linesGrid.ColumnHeadersHeight = 38;
            this._linesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Selected,
            this.Product,
            this.Sold,
            this.Returned,
            this.Quantity,
            this.Price});
            this._linesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._linesGrid.EnableHeadersVisualStyles = false;
            this._linesGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this._linesGrid.Location = new System.Drawing.Point(23, 95);
            this._linesGrid.MultiSelect = false;
            this._linesGrid.Name = "_linesGrid";
            this._linesGrid.RowHeadersVisible = false;
            this._linesGrid.RowHeadersWidth = 62;
            this._linesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this._linesGrid.Size = new System.Drawing.Size(774, 352);
            this._linesGrid.TabIndex = 1;
            this._linesGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.LinesGrid_CellEndEdit);
            this._linesGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.LinesGrid_CellValueChanged);
            this._linesGrid.CurrentCellDirtyStateChanged += new System.EventHandler(this.LinesGrid_CurrentCellDirtyStateChanged);
            this._linesGrid.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.LinesGrid_DataError);
            this._linesGrid.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.OnLinesGridEditingControlShowing);
            // 
            // Selected
            // 
            this.Selected.FillWeight = 8F;
            this.Selected.HeaderText = "Trả";
            this.Selected.MinimumWidth = 48;
            this.Selected.Name = "Selected";
            // 
            // Product
            // 
            this.Product.FillWeight = 39F;
            this.Product.HeaderText = "Sản phẩm";
            this.Product.MinimumWidth = 8;
            this.Product.Name = "Product";
            this.Product.ReadOnly = true;
            // 
            // Sold
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Sold.DefaultCellStyle = dataGridViewCellStyle8;
            this.Sold.FillWeight = 10F;
            this.Sold.HeaderText = "Đã mua";
            this.Sold.MinimumWidth = 8;
            this.Sold.Name = "Sold";
            this.Sold.ReadOnly = true;
            // 
            // Returned
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Returned.DefaultCellStyle = dataGridViewCellStyle9;
            this.Returned.FillWeight = 17F;
            this.Returned.HeaderText = "Đã yêu cầu/đã trả";
            this.Returned.MinimumWidth = 8;
            this.Returned.Name = "Returned";
            this.Returned.ReadOnly = true;
            // 
            // Quantity
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Quantity.DefaultCellStyle = dataGridViewCellStyle10;
            this.Quantity.FillWeight = 9F;
            this.Quantity.HeaderText = "SL trả";
            this.Quantity.MinimumWidth = 8;
            this.Quantity.Name = "Quantity";
            // 
            // Price
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Price.DefaultCellStyle = dataGridViewCellStyle11;
            this.Price.FillWeight = 17F;
            this.Price.HeaderText = "Đơn giá";
            this.Price.MinimumWidth = 8;
            this.Price.Name = "Price";
            this.Price.ReadOnly = true;
            // 
            // _footerPanel
            // 
            this._footerPanel.ColumnCount = 2;
            this._footerPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._footerPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 230F));
            this._footerPanel.Controls.Add(this._reasonPanel, 0, 0);
            this._footerPanel.Controls.Add(this._summaryLabel, 0, 1);
            this._footerPanel.Controls.Add(this._actionButtonsPanel, 1, 1);
            this._footerPanel.Controls.Add(this._noteLabel, 0, 2);
            this._footerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._footerPanel.Location = new System.Drawing.Point(23, 453);
            this._footerPanel.Name = "_footerPanel";
            this._footerPanel.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this._footerPanel.RowCount = 3;
            this._footerPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this._footerPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this._footerPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._footerPanel.Size = new System.Drawing.Size(774, 124);
            this._footerPanel.TabIndex = 2;
            // 
            // _reasonPanel
            // 
            this._footerPanel.SetColumnSpan(this._reasonPanel, 2);
            this._reasonPanel.Controls.Add(this._reasonLabel);
            this._reasonPanel.Controls.Add(this._reasonCombo);
            this._reasonPanel.Controls.Add(this._reasonDetails);
            this._reasonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._reasonPanel.Location = new System.Drawing.Point(0, 10);
            this._reasonPanel.Margin = new System.Windows.Forms.Padding(0);
            this._reasonPanel.Name = "_reasonPanel";
            this._reasonPanel.Size = new System.Drawing.Size(774, 30);
            this._reasonPanel.TabIndex = 0;
            this._reasonPanel.WrapContents = false;
            // 
            // _reasonLabel
            // 
            this._reasonLabel.AutoSize = true;
            this._reasonLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._reasonLabel.Location = new System.Drawing.Point(0, 6);
            this._reasonLabel.Margin = new System.Windows.Forms.Padding(0, 6, 8, 0);
            this._reasonLabel.Name = "_reasonLabel";
            this._reasonLabel.Size = new System.Drawing.Size(62, 25);
            this._reasonLabel.TabIndex = 0;
            this._reasonLabel.Text = "Lý do:";
            // 
            // _reasonCombo
            // 
            this._reasonCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._reasonCombo.Items.AddRange(new object[] {
            "Lỗi sản phẩm",
            "Giao sai sản phẩm",
            "Không còn nhu cầu",
            "Khác"});
            this._reasonCombo.Location = new System.Drawing.Point(70, 2);
            this._reasonCombo.Margin = new System.Windows.Forms.Padding(0, 2, 8, 0);
            this._reasonCombo.Name = "_reasonCombo";
            this._reasonCombo.Size = new System.Drawing.Size(160, 33);
            this._reasonCombo.TabIndex = 1;
            // 
            // _reasonDetails
            // 
            this._reasonDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this._reasonDetails.Location = new System.Drawing.Point(241, 3);
            this._reasonDetails.MaxLength = 180;
            this._reasonDetails.Name = "_reasonDetails";
            this._reasonDetails.Size = new System.Drawing.Size(100, 31);
            this._reasonDetails.TabIndex = 2;
            // 
            // _summaryLabel
            // 
            this._summaryLabel.AutoSize = true;
            this._summaryLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._summaryLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._summaryLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this._summaryLabel.Location = new System.Drawing.Point(3, 40);
            this._summaryLabel.Name = "_summaryLabel";
            this._summaryLabel.Size = new System.Drawing.Size(538, 40);
            this._summaryLabel.TabIndex = 1;
            this._summaryLabel.Text = "Giá trị hàng chọn: 0 đ";
            this._summaryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _actionButtonsPanel
            // 
            this._actionButtonsPanel.Controls.Add(this._cancelButton);
            this._actionButtonsPanel.Controls.Add(this._submitButton);
            this._actionButtonsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._actionButtonsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._actionButtonsPanel.Location = new System.Drawing.Point(547, 43);
            this._actionButtonsPanel.Name = "_actionButtonsPanel";
            this._actionButtonsPanel.Size = new System.Drawing.Size(224, 34);
            this._actionButtonsPanel.TabIndex = 2;
            this._actionButtonsPanel.WrapContents = false;
            // 
            // _cancelButton
            // 
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._cancelButton.Location = new System.Drawing.Point(146, 2);
            this._cancelButton.Margin = new System.Windows.Forms.Padding(8, 2, 0, 0);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(78, 34);
            this._cancelButton.TabIndex = 0;
            this._cancelButton.Text = "Hủy";
            this._cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // _submitButton
            // 
            this._submitButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this._submitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._submitButton.ForeColor = System.Drawing.Color.White;
            this._submitButton.Location = new System.Drawing.Point(14, 2);
            this._submitButton.Margin = new System.Windows.Forms.Padding(8, 2, 0, 0);
            this._submitButton.Name = "_submitButton";
            this._submitButton.Size = new System.Drawing.Size(124, 34);
            this._submitButton.TabIndex = 1;
            this._submitButton.Text = "Gửi yêu cầu";
            this._submitButton.UseVisualStyleBackColor = false;
            this._submitButton.Click += new System.EventHandler(this.SubmitButton_Click);
            // 
            // _noteLabel
            // 
            this._noteLabel.AutoSize = true;
            this._footerPanel.SetColumnSpan(this._noteLabel, 2);
            this._noteLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._noteLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._noteLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._noteLabel.Location = new System.Drawing.Point(3, 80);
            this._noteLabel.Name = "_noteLabel";
            this._noteLabel.Size = new System.Drawing.Size(768, 44);
            this._noteLabel.TabIndex = 3;
            this._noteLabel.Text = "Tiền hoàn được ghi nhận theo giá trị dự kiến; thanh toán thực tế xử lý bên ngoài." +
    "";
            // 
            // InvoiceReturnDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this._cancelButton;
            this.ClientSize = new System.Drawing.Size(820, 600);
            this.Controls.Add(this._rootLayout);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(820, 600);
            this.Name = "InvoiceReturnDialog";
            this.Text = "Tạo yêu cầu trả hàng";
            this._rootLayout.ResumeLayout(false);
            this._headerPanel.ResumeLayout(false);
            this._headerPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._linesGrid)).EndInit();
            this._footerPanel.ResumeLayout(false);
            this._footerPanel.PerformLayout();
            this._reasonPanel.ResumeLayout(false);
            this._reasonPanel.PerformLayout();
            this._actionButtonsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                components?.Dispose();
            base.Dispose(disposing);
        }

        private DataGridViewCheckBoxColumn Selected;
        private DataGridViewTextBoxColumn Product;
        private DataGridViewTextBoxColumn Sold;
        private DataGridViewTextBoxColumn Returned;
        private DataGridViewTextBoxColumn Quantity;
        private DataGridViewTextBoxColumn Price;
    }
}
