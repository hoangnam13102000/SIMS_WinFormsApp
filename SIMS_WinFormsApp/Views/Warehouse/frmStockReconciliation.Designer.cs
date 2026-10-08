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
        private DataGridViewTextBoxColumn _skuColumn;
        private DataGridViewTextBoxColumn _productColumn;
        private DataGridViewTextBoxColumn _systemQtyColumn;
        private DataGridViewTextBoxColumn _countQtyColumn;
        private DataGridViewTextBoxColumn _differenceColumn;
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
            this.components = new System.ComponentModel.Container();
            this._root = new TableLayoutPanel();
            this._header = new Panel();
            this._title = new Label();
            this._subtitle = new Label();
            this._filters = new FlowLayoutPanel();
            this._searchBox = new TextBox();
            this._categoryFilter = new ComboBox();
            this._loadButton = new Button();
            this._stockGrid = new DataGridView();
            this._skuColumn = new DataGridViewTextBoxColumn();
            this._productColumn = new DataGridViewTextBoxColumn();
            this._systemQtyColumn = new DataGridViewTextBoxColumn();
            this._countQtyColumn = new DataGridViewTextBoxColumn();
            this._differenceColumn = new DataGridViewTextBoxColumn();
            this._actions = new Panel();
            this._countLabel = new Label();
            this._exportButton = new Button();
            this._saveButton = new Button();
            this._root.SuspendLayout();
            this._header.SuspendLayout();
            this._filters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._stockGrid)).BeginInit();
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
            this._root.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            this._root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this._root.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            this._root.Controls.Add(this._header, 0, 0);
            this._root.Controls.Add(this._filters, 0, 1);
            this._root.Controls.Add(this._stockGrid, 0, 2);
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
            this._title.Text = "Kiểm kê tồn kho";
            this._subtitle.AutoSize = true;
            this._subtitle.Font = new Font("Segoe UI", 9F);
            this._subtitle.ForeColor = Color.FromArgb(100, 116, 139);
            this._subtitle.Location = new Point(2, 43);
            this._subtitle.Text = "Đối chiếu số lượng thực tế với tồn kho trên hệ thống";
            //
            // filters
            //
            this._filters.Controls.Add(this._searchBox);
            this._filters.Controls.Add(this._categoryFilter);
            this._filters.Controls.Add(this._loadButton);
            this._filters.Dock = DockStyle.Fill;
            this._searchBox.Margin = new Padding(0, 4, 8, 0);
            this._searchBox.Name = "_searchBox";
            this._searchBox.Text = "Tìm tên hoặc mã sản phẩm...";
            this._searchBox.Size = new Size(330, 28);
            this._categoryFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            this._categoryFilter.Items.AddRange(new object[] { "Tất cả danh mục" });
            this._categoryFilter.Margin = new Padding(4);
            this._categoryFilter.Name = "_categoryFilter";
            this._categoryFilter.Size = new Size(200, 28);
            this._loadButton.Margin = new Padding(4, 2, 0, 0);
            this._loadButton.Name = "_loadButton";
            this._loadButton.Size = new Size(120, 30);
            this._loadButton.Text = "Tải danh sách";
            this._loadButton.UseVisualStyleBackColor = true;
            //
            // stock grid
            //
            this._stockGrid.AllowUserToAddRows = false;
            this._stockGrid.AllowUserToDeleteRows = false;
            this._stockGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this._stockGrid.BackgroundColor = Color.White;
            this._stockGrid.BorderStyle = BorderStyle.None;
            this._stockGrid.ColumnHeadersHeight = 40;
            this._stockGrid.Columns.AddRange(new DataGridViewColumn[] {
                this._skuColumn, this._productColumn, this._systemQtyColumn,
                this._countQtyColumn, this._differenceColumn});
            this._stockGrid.Dock = DockStyle.Fill;
            this._stockGrid.Name = "_stockGrid";
            this._stockGrid.RowHeadersVisible = false;
            this._skuColumn.HeaderText = "Mã sản phẩm";
            this._skuColumn.Name = "Sku";
            this._productColumn.HeaderText = "Tên sản phẩm";
            this._productColumn.Name = "Product";
            this._systemQtyColumn.HeaderText = "Tồn hệ thống";
            this._systemQtyColumn.Name = "SystemQuantity";
            this._systemQtyColumn.ReadOnly = true;
            this._countQtyColumn.HeaderText = "Số lượng thực tế";
            this._countQtyColumn.Name = "CountQuantity";
            this._differenceColumn.HeaderText = "Chênh lệch";
            this._differenceColumn.Name = "Difference";
            this._differenceColumn.ReadOnly = true;
            //
            // actions
            //
            this._actions.Controls.Add(this._saveButton);
            this._actions.Controls.Add(this._exportButton);
            this._actions.Controls.Add(this._countLabel);
            this._actions.Dock = DockStyle.Fill;
            this._countLabel.AutoSize = true;
            this._countLabel.Font = new Font("Segoe UI", 9F);
            this._countLabel.Location = new Point(0, 16);
            this._countLabel.Text = "Số mặt hàng: 0";
            this._exportButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this._exportButton.Location = new Point(915, 8);
            this._exportButton.Name = "_exportButton";
            this._exportButton.Size = new Size(110, 34);
            this._exportButton.Text = "Xuất kiểm kê";
            this._exportButton.UseVisualStyleBackColor = true;
            this._saveButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this._saveButton.Location = new Point(1035, 8);
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new Size(125, 34);
            this._saveButton.Text = "Lưu kết quả";
            this._saveButton.UseVisualStyleBackColor = true;
            //
            // form
            //
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(244, 247, 250);
            this.ClientSize = new Size(1200, 760);
            this.Controls.Add(this._root);
            this.MinimumSize = new Size(900, 600);
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
    }
}
