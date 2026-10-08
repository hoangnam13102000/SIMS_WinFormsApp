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
        private DataGridViewTextBoxColumn _productColumn;
        private DataGridViewTextBoxColumn _quantityColumn;
        private DataGridViewTextBoxColumn _unitPriceColumn;
        private DataGridViewTextBoxColumn _amountColumn;
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
            this.components = new System.ComponentModel.Container();
            this._root = new TableLayoutPanel();
            this._header = new Panel();
            this._title = new Label();
            this._subtitle = new Label();
            this._details = new TableLayoutPanel();
            this._receiptCodeLabel = new Label();
            this._supplierLabel = new Label();
            this._receiptDateLabel = new Label();
            this._statusLabel = new Label();
            this._receiptCode = new TextBox();
            this._supplier = new ComboBox();
            this._receiptDate = new DateTimePicker();
            this._status = new ComboBox();
            this._itemsGrid = new DataGridView();
            this._productColumn = new DataGridViewTextBoxColumn();
            this._quantityColumn = new DataGridViewTextBoxColumn();
            this._unitPriceColumn = new DataGridViewTextBoxColumn();
            this._amountColumn = new DataGridViewTextBoxColumn();
            this._actions = new Panel();
            this._addItemButton = new Button();
            this._saveButton = new Button();
            this._cancelButton = new Button();
            this._totalLabel = new Label();
            this._root.SuspendLayout();
            this._header.SuspendLayout();
            this._details.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._itemsGrid)).BeginInit();
            this._actions.SuspendLayout();
            this.SuspendLayout();
            //
            // root
            //
            this._root.ColumnCount = 1;
            this._root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this._root.Dock = DockStyle.Fill;
            this._root.Padding = new Padding(24);
            this._root.RowCount = 4;
            this._root.RowStyles.Add(new RowStyle(SizeType.Absolute, 76F));
            this._root.RowStyles.Add(new RowStyle(SizeType.Absolute, 104F));
            this._root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this._root.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            this._root.Controls.Add(this._header, 0, 0);
            this._root.Controls.Add(this._details, 0, 1);
            this._root.Controls.Add(this._itemsGrid, 0, 2);
            this._root.Controls.Add(this._actions, 0, 3);
            //
            // header
            //
            this._header.Controls.Add(this._subtitle);
            this._header.Controls.Add(this._title);
            this._header.Dock = DockStyle.Fill;
            this._title.AutoSize = true;
            this._title.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            this._title.ForeColor = Color.FromArgb(15, 23, 42);
            this._title.Location = new Point(0, 0);
            this._title.Text = "Phiếu nhập hàng";
            this._subtitle.AutoSize = true;
            this._subtitle.Font = new Font("Segoe UI", 9F);
            this._subtitle.ForeColor = Color.FromArgb(100, 116, 139);
            this._subtitle.Location = new Point(2, 43);
            this._subtitle.Text = "Thông tin nhà cung cấp và danh sách hàng nhập";
            //
            // details
            //
            this._details.ColumnCount = 4;
            this._details.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this._details.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this._details.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this._details.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this._details.Dock = DockStyle.Fill;
            this._details.RowCount = 2;
            this._details.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
            this._details.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            this._details.Controls.Add(this._receiptCodeLabel, 0, 0);
            this._details.Controls.Add(this._supplierLabel, 1, 0);
            this._details.Controls.Add(this._receiptDateLabel, 2, 0);
            this._details.Controls.Add(this._statusLabel, 3, 0);
            this._details.Controls.Add(this._receiptCode, 0, 1);
            this._details.Controls.Add(this._supplier, 1, 1);
            this._details.Controls.Add(this._receiptDate, 2, 1);
            this._details.Controls.Add(this._status, 3, 1);
            this._receiptCodeLabel.AutoSize = true;
            this._receiptCodeLabel.Dock = DockStyle.Fill;
            this._receiptCodeLabel.Name = "_receiptCodeLabel";
            this._receiptCodeLabel.Text = "Mã phiếu";
            this._supplierLabel.AutoSize = true;
            this._supplierLabel.Dock = DockStyle.Fill;
            this._supplierLabel.Name = "_supplierLabel";
            this._supplierLabel.Text = "Nhà cung cấp";
            this._receiptDateLabel.AutoSize = true;
            this._receiptDateLabel.Dock = DockStyle.Fill;
            this._receiptDateLabel.Name = "_receiptDateLabel";
            this._receiptDateLabel.Text = "Ngày nhập";
            this._statusLabel.AutoSize = true;
            this._statusLabel.Dock = DockStyle.Fill;
            this._statusLabel.Name = "_statusLabel";
            this._statusLabel.Text = "Trạng thái";
            this._receiptCode.Dock = DockStyle.Fill;
            this._receiptCode.Name = "_receiptCode";
            this._receiptCode.Text = "Tự động";
            this._receiptCode.Margin = new Padding(0, 0, 12, 0);
            this._supplier.Dock = DockStyle.Fill;
            this._supplier.DropDownStyle = ComboBoxStyle.DropDownList;
            this._supplier.Name = "_supplier";
            this._supplier.Margin = new Padding(0, 0, 12, 0);
            this._receiptDate.Dock = DockStyle.Fill;
            this._receiptDate.Format = DateTimePickerFormat.Short;
            this._receiptDate.Name = "_receiptDate";
            this._receiptDate.Margin = new Padding(0, 0, 12, 0);
            this._status.Dock = DockStyle.Fill;
            this._status.DropDownStyle = ComboBoxStyle.DropDownList;
            this._status.Items.AddRange(new object[] { "Nháp", "Hoàn thành" });
            this._status.Name = "_status";
            //
            // items grid
            //
            this._itemsGrid.AllowUserToAddRows = false;
            this._itemsGrid.AllowUserToDeleteRows = false;
            this._itemsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this._itemsGrid.BackgroundColor = Color.White;
            this._itemsGrid.ColumnHeadersHeight = 40;
            this._itemsGrid.Columns.AddRange(new DataGridViewColumn[] {
                this._productColumn, this._quantityColumn, this._unitPriceColumn, this._amountColumn});
            this._itemsGrid.Dock = DockStyle.Fill;
            this._itemsGrid.Name = "_itemsGrid";
            this._itemsGrid.RowHeadersVisible = false;
            this._productColumn.HeaderText = "Sản phẩm";
            this._productColumn.Name = "Product";
            this._quantityColumn.HeaderText = "Số lượng";
            this._quantityColumn.Name = "Quantity";
            this._unitPriceColumn.HeaderText = "Đơn giá nhập";
            this._unitPriceColumn.Name = "UnitPrice";
            this._amountColumn.HeaderText = "Thành tiền";
            this._amountColumn.Name = "Amount";
            //
            // actions
            //
            this._actions.Controls.Add(this._cancelButton);
            this._actions.Controls.Add(this._saveButton);
            this._actions.Controls.Add(this._totalLabel);
            this._actions.Controls.Add(this._addItemButton);
            this._actions.Dock = DockStyle.Fill;
            this._addItemButton.Location = new Point(0, 10);
            this._addItemButton.Name = "_addItemButton";
            this._addItemButton.Size = new Size(130, 34);
            this._addItemButton.Text = "Thêm sản phẩm";
            this._addItemButton.UseVisualStyleBackColor = true;
            this._totalLabel.AutoSize = true;
            this._totalLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this._totalLabel.Location = new Point(450, 15);
            this._totalLabel.Text = "Tổng tiền: 0";
            this._saveButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this._saveButton.Location = new Point(960, 10);
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new Size(100, 34);
            this._saveButton.Text = "Lưu phiếu";
            this._saveButton.UseVisualStyleBackColor = true;
            this._cancelButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this._cancelButton.Location = new Point(1070, 10);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new Size(90, 34);
            this._cancelButton.Text = "Đóng";
            this._cancelButton.UseVisualStyleBackColor = true;
            //
            // form
            //
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(244, 247, 250);
            this.ClientSize = new Size(1200, 800);
            this.Controls.Add(this._root);
            this.MinimumSize = new Size(900, 620);
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
    }
}
