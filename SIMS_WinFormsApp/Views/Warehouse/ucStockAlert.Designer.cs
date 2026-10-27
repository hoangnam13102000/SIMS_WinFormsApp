using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Views.Warehouse
{
    partial class ucStockAlert
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel _root;
        private Panel _header;
        private Label _title;
        private Label _subtitle;
        private FlowLayoutPanel _filters;
        private TextBox _search;
        private ComboBox _category;
        private ComboBox _level;
        private DataGridView _grid;
        private DataGridViewTextBoxColumn _product;
        private DataGridViewTextBoxColumn _categoryColumn;
        private DataGridViewTextBoxColumn _quantity;
        private DataGridViewTextBoxColumn _minimum;
        private DataGridViewTextBoxColumn _levelColumn;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            _root = new TableLayoutPanel();
            _header = new Panel();
            _title = new Label();
            _subtitle = new Label();
            _filters = new FlowLayoutPanel();
            _search = new TextBox();
            _category = new ComboBox();
            _level = new ComboBox();
            _grid = new DataGridView();
            _product = new DataGridViewTextBoxColumn();
            _categoryColumn = new DataGridViewTextBoxColumn();
            _quantity = new DataGridViewTextBoxColumn();
            _minimum = new DataGridViewTextBoxColumn();
            _levelColumn = new DataGridViewTextBoxColumn();
            _root.SuspendLayout();
            _header.SuspendLayout();
            _filters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_grid).BeginInit();
            SuspendLayout();
            _root.ColumnCount = 1;
            _root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _root.Dock = DockStyle.Fill;
            _root.Padding = new Padding(24);
            _root.RowCount = 3;
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 76F));
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            _root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            _root.Controls.Add(_header, 0, 0);
            _root.Controls.Add(_filters, 0, 1);
            _root.Controls.Add(_grid, 0, 2);
            _header.Controls.Add(_subtitle);
            _header.Controls.Add(_title);
            _header.Dock = DockStyle.Fill;
            _title.AutoSize = true;
            _title.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            _title.ForeColor = Color.FromArgb(15, 23, 42);
            _title.Location = new Point(0, 0);
            _title.Text = "Cảnh báo tồn kho";
            _subtitle.AutoSize = true;
            _subtitle.ForeColor = Color.FromArgb(100, 116, 139);
            _subtitle.Location = new Point(2, 44);
            _subtitle.Text = "Danh sách sản phẩm sắp hết hoặc đã hết hàng";
            _filters.Controls.Add(_search);
            _filters.Controls.Add(_category);
            _filters.Controls.Add(_level);
            _filters.Dock = DockStyle.Fill;
            _search.Margin = new Padding(0, 4, 8, 0);
            _search.Name = "_search";
            _search.Size = new Size(280, 28);
            _category.DropDownStyle = ComboBoxStyle.DropDownList;
            _category.Items.AddRange(new object[] { "Tất cả danh mục" });
            _category.Margin = new Padding(4);
            _category.Name = "_category";
            _category.SelectedIndex = 0;
            _category.Size = new Size(190, 28);
            _level.DropDownStyle = ComboBoxStyle.DropDownList;
            _level.Items.AddRange(new object[] { "Tất cả mức cảnh báo", "Sắp hết hàng", "Hết hàng" });
            _level.Margin = new Padding(4);
            _level.Name = "_level";
            _level.SelectedIndex = 0;
            _level.Size = new Size(190, 28);
            _grid.AllowUserToAddRows = false;
            _grid.AllowUserToDeleteRows = false;
            _grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _grid.BackgroundColor = Color.White;
            _grid.BorderStyle = BorderStyle.None;
            _grid.ColumnHeadersHeight = 40;
            _grid.Columns.AddRange(new DataGridViewColumn[] { _product, _categoryColumn, _quantity, _minimum, _levelColumn });
            _grid.Dock = DockStyle.Fill;
            _grid.ReadOnly = true;
            _grid.RowHeadersVisible = false;
            _product.HeaderText = "Sản phẩm";
            _product.Name = "Product";
            _categoryColumn.HeaderText = "Danh mục";
            _categoryColumn.Name = "Category";
            _quantity.HeaderText = "Tồn kho";
            _quantity.Name = "Quantity";
            _minimum.HeaderText = "Mức tối thiểu";
            _minimum.Name = "Minimum";
            _levelColumn.HeaderText = "Mức cảnh báo";
            _levelColumn.Name = "AlertLevel";
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 247, 250);
            Controls.Add(_root);
            Name = "ucStockAlert";
            Size = new Size(1000, 700);
            _root.ResumeLayout(false);
            _header.ResumeLayout(false);
            _header.PerformLayout();
            _filters.ResumeLayout(false);
            _filters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_grid).EndInit();
            ResumeLayout(false);
        }
    }
}
