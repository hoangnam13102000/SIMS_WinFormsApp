using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Views.Warehouse
{
    partial class frmPurchaseReceipt
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel _root;
        private Panel _header;
        private Label _title;
        private Label _subtitle;
        private TableLayoutPanel _details;
        private Label _receiptCodeLabel;
        private Label _supplierLabel;
        private Label _receiptDateLabel;
        private Label _statusLabel;
        private TextBox _receiptCode;
        private ComboBox _supplier;
        private DateTimePicker _receiptDate;
        private ComboBox _status;
        private DataGridView _itemsGrid;
        private Panel _actions;
        private Button _addItemButton;
        private Button _saveButton;
        private Button _cancelButton;
        private Label _totalLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this._root = new System.Windows.Forms.TableLayoutPanel();
            this._header = new System.Windows.Forms.Panel();
            this._subtitle = new System.Windows.Forms.Label();
            this._title = new System.Windows.Forms.Label();
            this._details = new System.Windows.Forms.TableLayoutPanel();
            this._receiptCodeLabel = new System.Windows.Forms.Label();
            this._supplierLabel = new System.Windows.Forms.Label();
            this._receiptDateLabel = new System.Windows.Forms.Label();
            this._statusLabel = new System.Windows.Forms.Label();
            this._receiptCode = new System.Windows.Forms.TextBox();
            this._supplier = new System.Windows.Forms.ComboBox();
            this._receiptDate = new System.Windows.Forms.DateTimePicker();
            this._status = new System.Windows.Forms.ComboBox();
            this._itemsGrid = new System.Windows.Forms.DataGridView();
            this.Product = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LotNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExpiryDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._actions = new System.Windows.Forms.Panel();
            this._cancelButton = new System.Windows.Forms.Button();
            this._saveButton = new System.Windows.Forms.Button();
            this._totalLabel = new System.Windows.Forms.Label();
            this._addItemButton = new System.Windows.Forms.Button();
            this._root.SuspendLayout();
            this._header.SuspendLayout();
            this._details.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._itemsGrid)).BeginInit();
            this._actions.SuspendLayout();
            this.SuspendLayout();
            // 
            // _root
            // 
            this._root.ColumnCount = 1;
            this._root.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._root.Controls.Add(this._header, 0, 0);
            this._root.Controls.Add(this._details, 0, 1);
            this._root.Controls.Add(this._itemsGrid, 0, 2);
            this._root.Controls.Add(this._actions, 0, 3);
            this._root.Dock = System.Windows.Forms.DockStyle.Fill;
            this._root.Location = new System.Drawing.Point(0, 0);
            this._root.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._root.Name = "_root";
            this._root.Padding = new System.Windows.Forms.Padding(21, 19, 21, 19);
            this._root.RowCount = 4;
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 61F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 83F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this._root.Size = new System.Drawing.Size(1067, 640);
            this._root.TabIndex = 0;
            // 
            // _header
            // 
            this._header.Controls.Add(this._subtitle);
            this._header.Controls.Add(this._title);
            this._header.Dock = System.Windows.Forms.DockStyle.Fill;
            this._header.Location = new System.Drawing.Point(24, 21);
            this._header.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._header.Name = "_header";
            this._header.Size = new System.Drawing.Size(1019, 57);
            this._header.TabIndex = 0;
            // 
            // _subtitle
            // 
            this._subtitle.AutoSize = true;
            this._subtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._subtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._subtitle.Location = new System.Drawing.Point(2, 34);
            this._subtitle.Name = "_subtitle";
            this._subtitle.Size = new System.Drawing.Size(327, 20);
            this._subtitle.TabIndex = 0;
            this._subtitle.Text = "Thông tin nhà cung cấp và danh sách hàng nhập";
            // 
            // _title
            // 
            this._title.AutoSize = true;
            this._title.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this._title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._title.Location = new System.Drawing.Point(0, 0);
            this._title.Name = "_title";
            this._title.Size = new System.Drawing.Size(314, 50);
            this._title.TabIndex = 1;
            this._title.Text = "Phiếu nhập hàng";
            // 
            // _details
            // 
            this._details.ColumnCount = 4;
            this._details.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this._details.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this._details.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this._details.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this._details.Controls.Add(this._receiptCodeLabel, 0, 0);
            this._details.Controls.Add(this._supplierLabel, 1, 0);
            this._details.Controls.Add(this._receiptDateLabel, 2, 0);
            this._details.Controls.Add(this._statusLabel, 3, 0);
            this._details.Controls.Add(this._receiptCode, 0, 1);
            this._details.Controls.Add(this._supplier, 1, 1);
            this._details.Controls.Add(this._receiptDate, 2, 1);
            this._details.Controls.Add(this._status, 3, 1);
            this._details.Dock = System.Windows.Forms.DockStyle.Fill;
            this._details.Location = new System.Drawing.Point(24, 82);
            this._details.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._details.Name = "_details";
            this._details.RowCount = 2;
            this._details.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this._details.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this._details.Size = new System.Drawing.Size(1019, 79);
            this._details.TabIndex = 1;
            // 
            // _receiptCodeLabel
            // 
            this._receiptCodeLabel.AutoSize = true;
            this._receiptCodeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._receiptCodeLabel.Location = new System.Drawing.Point(3, 0);
            this._receiptCodeLabel.Name = "_receiptCodeLabel";
            this._receiptCodeLabel.Size = new System.Drawing.Size(248, 21);
            this._receiptCodeLabel.TabIndex = 0;
            this._receiptCodeLabel.Text = "Mã phiếu";
            // 
            // _supplierLabel
            // 
            this._supplierLabel.AutoSize = true;
            this._supplierLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._supplierLabel.Location = new System.Drawing.Point(257, 0);
            this._supplierLabel.Name = "_supplierLabel";
            this._supplierLabel.Size = new System.Drawing.Size(248, 21);
            this._supplierLabel.TabIndex = 1;
            this._supplierLabel.Text = "Nhà cung cấp";
            // 
            // _receiptDateLabel
            // 
            this._receiptDateLabel.AutoSize = true;
            this._receiptDateLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._receiptDateLabel.Location = new System.Drawing.Point(511, 0);
            this._receiptDateLabel.Name = "_receiptDateLabel";
            this._receiptDateLabel.Size = new System.Drawing.Size(248, 21);
            this._receiptDateLabel.TabIndex = 2;
            this._receiptDateLabel.Text = "Ngày nhập";
            // 
            // _statusLabel
            // 
            this._statusLabel.AutoSize = true;
            this._statusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._statusLabel.Location = new System.Drawing.Point(765, 0);
            this._statusLabel.Name = "_statusLabel";
            this._statusLabel.Size = new System.Drawing.Size(251, 21);
            this._statusLabel.TabIndex = 3;
            this._statusLabel.Text = "Trạng thái";
            // 
            // _receiptCode
            // 
            this._receiptCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this._receiptCode.Location = new System.Drawing.Point(0, 21);
            this._receiptCode.Margin = new System.Windows.Forms.Padding(0, 0, 11, 0);
            this._receiptCode.Name = "_receiptCode";
            this._receiptCode.ReadOnly = true;
            this._receiptCode.Size = new System.Drawing.Size(243, 22);
            this._receiptCode.TabIndex = 4;
            this._receiptCode.Text = "Tự động";
            // 
            // _supplier
            // 
            this._supplier.Dock = System.Windows.Forms.DockStyle.Fill;
            this._supplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._supplier.Location = new System.Drawing.Point(254, 21);
            this._supplier.Margin = new System.Windows.Forms.Padding(0, 0, 11, 0);
            this._supplier.Name = "_supplier";
            this._supplier.Size = new System.Drawing.Size(243, 24);
            this._supplier.TabIndex = 5;
            // 
            // _receiptDate
            // 
            this._receiptDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this._receiptDate.Enabled = false;
            this._receiptDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this._receiptDate.Location = new System.Drawing.Point(508, 21);
            this._receiptDate.Margin = new System.Windows.Forms.Padding(0, 0, 11, 0);
            this._receiptDate.Name = "_receiptDate";
            this._receiptDate.Size = new System.Drawing.Size(243, 22);
            this._receiptDate.TabIndex = 6;
            // 
            // _status
            // 
            this._status.Dock = System.Windows.Forms.DockStyle.Fill;
            this._status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._status.Items.AddRange(new object[] {
            "Hoàn thành"});
            this._status.Location = new System.Drawing.Point(765, 23);
            this._status.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._status.Name = "_status";
            this._status.Size = new System.Drawing.Size(251, 24);
            this._status.TabIndex = 7;
            // 
            // _itemsGrid
            // 
            this._itemsGrid.AllowUserToAddRows = false;
            this._itemsGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._itemsGrid.BackgroundColor = System.Drawing.Color.White;
            this._itemsGrid.ColumnHeadersHeight = 40;
            this._itemsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Product,
            this.Quantity,
            this.UnitPrice,
            this.LotNumber,
            this.ExpiryDate,
            this.Amount});
            this._itemsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._itemsGrid.Location = new System.Drawing.Point(24, 165);
            this._itemsGrid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._itemsGrid.Name = "_itemsGrid";
            this._itemsGrid.RowHeadersVisible = false;
            this._itemsGrid.RowHeadersWidth = 62;
            this._itemsGrid.Size = new System.Drawing.Size(1019, 409);
            this._itemsGrid.TabIndex = 2;
            // 
            // Product
            // 
            this.Product.HeaderText = "Sản phẩm";
            this.Product.MinimumWidth = 8;
            this.Product.Name = "Product";
            // 
            // Quantity
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Quantity.DefaultCellStyle = dataGridViewCellStyle1;
            this.Quantity.HeaderText = "Số lượng";
            this.Quantity.MinimumWidth = 8;
            this.Quantity.Name = "Quantity";
            // 
            // UnitPrice
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.UnitPrice.DefaultCellStyle = dataGridViewCellStyle2;
            this.UnitPrice.HeaderText = "Đơn giá nhập";
            this.UnitPrice.MinimumWidth = 8;
            this.UnitPrice.Name = "UnitPrice";
            // 
            // LotNumber
            // 
            this.LotNumber.HeaderText = "Số lô";
            this.LotNumber.MinimumWidth = 8;
            this.LotNumber.Name = "LotNumber";
            // 
            // ExpiryDate
            // 
            this.ExpiryDate.HeaderText = "Hạn sử dụng (dd/MM/yyyy)";
            this.ExpiryDate.MinimumWidth = 8;
            this.ExpiryDate.Name = "ExpiryDate";
            // 
            // Amount
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Amount.DefaultCellStyle = dataGridViewCellStyle3;
            this.Amount.HeaderText = "Thành tiền";
            this.Amount.MinimumWidth = 8;
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            // 
            // _actions
            // 
            this._actions.Controls.Add(this._cancelButton);
            this._actions.Controls.Add(this._saveButton);
            this._actions.Controls.Add(this._totalLabel);
            this._actions.Controls.Add(this._addItemButton);
            this._actions.Dock = System.Windows.Forms.DockStyle.Fill;
            this._actions.Location = new System.Drawing.Point(24, 578);
            this._actions.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._actions.Name = "_actions";
            this._actions.Size = new System.Drawing.Size(1019, 41);
            this._actions.TabIndex = 3;
            // 
            // _cancelButton
            // 
            this._cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._cancelButton.Location = new System.Drawing.Point(928, 8);
            this._cancelButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(80, 27);
            this._cancelButton.TabIndex = 0;
            this._cancelButton.Text = "Đóng";
            this._cancelButton.UseVisualStyleBackColor = true;
            // 
            // _saveButton
            // 
            this._saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._saveButton.Location = new System.Drawing.Point(830, 8);
            this._saveButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(89, 27);
            this._saveButton.TabIndex = 1;
            this._saveButton.Text = "Lưu phiếu";
            this._saveButton.UseVisualStyleBackColor = true;
            // 
            // _totalLabel
            // 
            this._totalLabel.AutoSize = true;
            this._totalLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this._totalLabel.Location = new System.Drawing.Point(400, 12);
            this._totalLabel.Name = "_totalLabel";
            this._totalLabel.Size = new System.Drawing.Size(126, 28);
            this._totalLabel.TabIndex = 2;
            this._totalLabel.Text = "Tổng tiền: 0";
            // 
            // _addItemButton
            // 
            this._addItemButton.Location = new System.Drawing.Point(0, 8);
            this._addItemButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._addItemButton.Name = "_addItemButton";
            this._addItemButton.Size = new System.Drawing.Size(116, 27);
            this._addItemButton.TabIndex = 3;
            this._addItemButton.Text = "Thêm sản phẩm";
            this._addItemButton.UseVisualStyleBackColor = true;
            // 
            // frmPurchaseReceipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1067, 640);
            this.Controls.Add(this._root);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(802, 505);
            this.Name = "frmPurchaseReceipt";
            this.Text = "Phiếu nhập hàng";
            this._root.ResumeLayout(false);
            this._header.ResumeLayout(false);
            this._header.PerformLayout();
            this._details.ResumeLayout(false);
            this._details.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._itemsGrid)).EndInit();
            this._actions.ResumeLayout(false);
            this._actions.PerformLayout();
            this.ResumeLayout(false);

        }

        private DataGridViewComboBoxColumn Product;
        private DataGridViewTextBoxColumn Quantity;
        private DataGridViewTextBoxColumn UnitPrice;
        private DataGridViewTextBoxColumn LotNumber;
        private DataGridViewTextBoxColumn ExpiryDate;
        private DataGridViewTextBoxColumn Amount;
    }
}
