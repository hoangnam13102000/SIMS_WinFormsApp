using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Views.Warehouse
{
    partial class frmStockReconciliation
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel _root;
        private Panel _header;
        private Label _title;
        private Label _subtitle;
        private FlowLayoutPanel _filters;
        private TextBox _searchBox;
        private ComboBox _categoryFilter;
        private Button _loadButton;
        private DataGridView _stockGrid;
        private DataGridViewTextBoxColumn _productIdColumn;
        private DataGridViewTextBoxColumn _skuColumn;
        private DataGridViewTextBoxColumn _productColumn;
        private DataGridViewTextBoxColumn _systemQtyColumn;
        private DataGridViewTextBoxColumn _statusColumn;
        private DataGridViewTextBoxColumn _countQtyColumn;
        private DataGridViewTextBoxColumn _differenceColumn;
        private DataGridViewTextBoxColumn _noteColumn;
        private Panel _actions;
        private Label _countLabel;
        private Button _exportButton;
        private Button _saveButton;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this._root = new System.Windows.Forms.TableLayoutPanel();
            this._header = new System.Windows.Forms.Panel();
            this._subtitle = new System.Windows.Forms.Label();
            this._title = new System.Windows.Forms.Label();
            this._filters = new System.Windows.Forms.FlowLayoutPanel();
            this._searchBox = new System.Windows.Forms.TextBox();
            this._categoryFilter = new System.Windows.Forms.ComboBox();
            this._loadButton = new System.Windows.Forms.Button();
            this._stockGrid = new System.Windows.Forms.DataGridView();
            this._productIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sku = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Product = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SystemQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._statusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Difference = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._noteColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._actions = new System.Windows.Forms.Panel();
            this._saveButton = new System.Windows.Forms.Button();
            this._exportButton = new System.Windows.Forms.Button();
            this._countLabel = new System.Windows.Forms.Label();
            this._root.SuspendLayout();
            this._header.SuspendLayout();
            this._filters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._stockGrid)).BeginInit();
            this._actions.SuspendLayout();
            this.SuspendLayout();
            // 
            // _root
            // 
            this._root.ColumnCount = 1;
            this._root.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._root.Controls.Add(this._header, 0, 0);
            this._root.Controls.Add(this._filters, 0, 1);
            this._root.Controls.Add(this._stockGrid, 0, 2);
            this._root.Controls.Add(this._actions, 0, 3);
            this._root.Dock = System.Windows.Forms.DockStyle.Fill;
            this._root.Location = new System.Drawing.Point(0, 0);
            this._root.Name = "_root";
            this._root.Padding = new System.Windows.Forms.Padding(24);
            this._root.RowCount = 4;
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 76F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this._root.Size = new System.Drawing.Size(1200, 760);
            this._root.TabIndex = 0;
            // 
            // _header
            // 
            this._header.Controls.Add(this._subtitle);
            this._header.Controls.Add(this._title);
            this._header.Dock = System.Windows.Forms.DockStyle.Fill;
            this._header.Location = new System.Drawing.Point(27, 27);
            this._header.Name = "_header";
            this._header.Size = new System.Drawing.Size(1146, 70);
            this._header.TabIndex = 0;
            // 
            // _subtitle
            // 
            this._subtitle.AutoSize = true;
            this._subtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._subtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._subtitle.Location = new System.Drawing.Point(2, 43);
            this._subtitle.Name = "_subtitle";
            this._subtitle.Size = new System.Drawing.Size(355, 20);
            this._subtitle.TabIndex = 0;
            this._subtitle.Text = "Đối chiếu số lượng thực tế với tồn kho trên hệ thống";
            // 
            // _title
            // 
            this._title.AutoSize = true;
            this._title.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this._title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._title.Location = new System.Drawing.Point(0, 0);
            this._title.Name = "_title";
            this._title.Size = new System.Drawing.Size(306, 50);
            this._title.TabIndex = 1;
            this._title.Text = "Kiểm kê tồn kho";
            // 
            // _filters
            // 
            this._filters.Controls.Add(this._searchBox);
            this._filters.Controls.Add(this._categoryFilter);
            this._filters.Controls.Add(this._loadButton);
            this._filters.Dock = System.Windows.Forms.DockStyle.Fill;
            this._filters.Location = new System.Drawing.Point(27, 103);
            this._filters.Name = "_filters";
            this._filters.Size = new System.Drawing.Size(1146, 44);
            this._filters.TabIndex = 1;
            // 
            // _searchBox
            // 
            this._searchBox.Location = new System.Drawing.Point(0, 4);
            this._searchBox.Margin = new System.Windows.Forms.Padding(0, 4, 8, 0);
            this._searchBox.Name = "_searchBox";
            this._searchBox.Size = new System.Drawing.Size(330, 22);
            this._searchBox.TabIndex = 0;
            // 
            // _categoryFilter
            // 
            this._categoryFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._categoryFilter.Items.AddRange(new object[] {
            "Tất cả danh mục"});
            this._categoryFilter.Location = new System.Drawing.Point(342, 4);
            this._categoryFilter.Margin = new System.Windows.Forms.Padding(4);
            this._categoryFilter.Name = "_categoryFilter";
            this._categoryFilter.Size = new System.Drawing.Size(200, 24);
            this._categoryFilter.TabIndex = 1;
            // 
            // _loadButton
            // 
            this._loadButton.Location = new System.Drawing.Point(550, 2);
            this._loadButton.Margin = new System.Windows.Forms.Padding(4, 2, 0, 0);
            this._loadButton.Name = "_loadButton";
            this._loadButton.Size = new System.Drawing.Size(120, 30);
            this._loadButton.TabIndex = 2;
            this._loadButton.Text = "Tải danh sách";
            this._loadButton.UseVisualStyleBackColor = true;
            // 
            // _stockGrid
            // 
            this._stockGrid.AllowUserToAddRows = false;
            this._stockGrid.AllowUserToDeleteRows = false;
            this._stockGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._stockGrid.BackgroundColor = System.Drawing.Color.White;
            this._stockGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._stockGrid.ColumnHeadersHeight = 40;
            this._stockGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._productIdColumn,
            this.Sku,
            this.Product,
            this.SystemQuantity,
            this._statusColumn,
            this.CountQuantity,
            this.Difference,
            this._noteColumn});
            this._stockGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._stockGrid.Location = new System.Drawing.Point(27, 153);
            this._stockGrid.Name = "_stockGrid";
            this._stockGrid.RowHeadersVisible = false;
            this._stockGrid.RowHeadersWidth = 51;
            this._stockGrid.Size = new System.Drawing.Size(1146, 526);
            this._stockGrid.TabIndex = 2;
            // 
            // _productIdColumn
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this._productIdColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this._productIdColumn.MinimumWidth = 6;
            this._productIdColumn.Name = "_productIdColumn";
            this._productIdColumn.ReadOnly = true;
            this._productIdColumn.Visible = false;
            // 
            // Sku
            // 
            this.Sku.HeaderText = "Mã sản phẩm";
            this.Sku.MinimumWidth = 6;
            this.Sku.Name = "Sku";
            this.Sku.ReadOnly = true;
            // 
            // Product
            // 
            this.Product.HeaderText = "Tên sản phẩm";
            this.Product.MinimumWidth = 6;
            this.Product.Name = "Product";
            this.Product.ReadOnly = true;
            // 
            // SystemQuantity
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.SystemQuantity.DefaultCellStyle = dataGridViewCellStyle2;
            this.SystemQuantity.HeaderText = "Tồn hệ thống";
            this.SystemQuantity.MinimumWidth = 6;
            this.SystemQuantity.Name = "SystemQuantity";
            this.SystemQuantity.ReadOnly = true;
            // 
            // _statusColumn
            // 
            this._statusColumn.HeaderText = "Trạng thái";
            this._statusColumn.MinimumWidth = 6;
            this._statusColumn.Name = "_statusColumn";
            this._statusColumn.ReadOnly = true;
            // 
            // CountQuantity
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.CountQuantity.DefaultCellStyle = dataGridViewCellStyle3;
            this.CountQuantity.HeaderText = "Số lượng thực tế";
            this.CountQuantity.MinimumWidth = 6;
            this.CountQuantity.Name = "CountQuantity";
            // 
            // Difference
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Difference.DefaultCellStyle = dataGridViewCellStyle4;
            this.Difference.HeaderText = "Chênh lệch";
            this.Difference.MinimumWidth = 6;
            this.Difference.Name = "Difference";
            this.Difference.ReadOnly = true;
            // 
            // _noteColumn
            // 
            this._noteColumn.HeaderText = "Ghi chú";
            this._noteColumn.MinimumWidth = 6;
            this._noteColumn.Name = "_noteColumn";
            // 
            // _actions
            // 
            this._actions.Controls.Add(this._saveButton);
            this._actions.Controls.Add(this._exportButton);
            this._actions.Controls.Add(this._countLabel);
            this._actions.Dock = System.Windows.Forms.DockStyle.Fill;
            this._actions.Location = new System.Drawing.Point(27, 685);
            this._actions.Name = "_actions";
            this._actions.Size = new System.Drawing.Size(1146, 48);
            this._actions.TabIndex = 3;
            // 
            // _saveButton
            // 
            this._saveButton.Dock = System.Windows.Forms.DockStyle.Right;
            this._saveButton.Location = new System.Drawing.Point(911, 0);
            this._saveButton.Margin = new System.Windows.Forms.Padding(8, 10, 0, 10);
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(125, 48);
            this._saveButton.TabIndex = 0;
            this._saveButton.Text = "Lưu kết quả";
            this._saveButton.UseVisualStyleBackColor = true;
            // 
            // _exportButton
            // 
            this._exportButton.Dock = System.Windows.Forms.DockStyle.Right;
            this._exportButton.Location = new System.Drawing.Point(1036, 0);
            this._exportButton.Margin = new System.Windows.Forms.Padding(8, 10, 0, 10);
            this._exportButton.Name = "_exportButton";
            this._exportButton.Size = new System.Drawing.Size(110, 48);
            this._exportButton.TabIndex = 1;
            this._exportButton.Text = "Xuất kiểm kê";
            this._exportButton.UseVisualStyleBackColor = true;
            // 
            // _countLabel
            // 
            this._countLabel.AutoSize = true;
            this._countLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this._countLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._countLabel.Location = new System.Drawing.Point(0, 0);
            this._countLabel.Margin = new System.Windows.Forms.Padding(0, 16, 4, 0);
            this._countLabel.Name = "_countLabel";
            this._countLabel.Size = new System.Drawing.Size(108, 20);
            this._countLabel.TabIndex = 2;
            this._countLabel.Text = "Số mặt hàng: 0";
            // 
            // frmStockReconciliation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1200, 760);
            this.Controls.Add(this._root);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "frmStockReconciliation";
            this.Text = "Kiểm kê tồn kho";
            this._root.ResumeLayout(false);
            this._header.ResumeLayout(false);
            this._header.PerformLayout();
            this._filters.ResumeLayout(false);
            this._filters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._stockGrid)).EndInit();
            this._actions.ResumeLayout(false);
            this._actions.PerformLayout();
            this.ResumeLayout(false);

        }

        private DataGridViewTextBoxColumn Sku;
        private DataGridViewTextBoxColumn Product;
        private DataGridViewTextBoxColumn SystemQuantity;
        private DataGridViewTextBoxColumn CountQuantity;
        private DataGridViewTextBoxColumn Difference;
    }
}
