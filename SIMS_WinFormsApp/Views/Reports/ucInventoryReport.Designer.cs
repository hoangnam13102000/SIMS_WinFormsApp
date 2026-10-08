using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Views.Reports
{
    partial class ucInventoryReport
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel _root;
        private Panel _header;
        private Label _title;
        private Label _subtitle;
        private FlowLayoutPanel _filters;
        private DateTimePicker _from;
        private DateTimePicker _to;
        private ComboBox _category;
        private DataGridView _grid;
        private DataGridViewTextBoxColumn _code;
        private DataGridViewTextBoxColumn _name;
        private DataGridViewTextBoxColumn _categoryColumn;
        private DataGridViewTextBoxColumn _quantity;
        private DataGridViewTextBoxColumn _unitPrice;
        private DataGridViewTextBoxColumn _stockValue;

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
            _from = new DateTimePicker();
            _to = new DateTimePicker();
            _category = new ComboBox();
            _grid = new DataGridView();
            _code = new DataGridViewTextBoxColumn();
            _name = new DataGridViewTextBoxColumn();
            _categoryColumn = new DataGridViewTextBoxColumn();
            _quantity = new DataGridViewTextBoxColumn();
            _unitPrice = new DataGridViewTextBoxColumn();
            _stockValue = new DataGridViewTextBoxColumn();
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
            _title.Text = "Báo cáo tồn kho";
            _subtitle.AutoSize = true;
            _subtitle.ForeColor = Color.FromArgb(100, 116, 139);
            _subtitle.Location = new Point(2, 44);
            _subtitle.Text = "Tổng hợp số lượng và giá trị hàng hóa tồn";
            _filters.Controls.Add(_from);
            _filters.Controls.Add(_to);
            _filters.Controls.Add(_category);
            _filters.Dock = DockStyle.Fill;
            _from.Format = DateTimePickerFormat.Short;
            _from.Margin = new Padding(0, 4, 8, 0);
            _from.Name = "_from";
            _from.Size = new Size(130, 28);
            _to.Format = DateTimePickerFormat.Short;
            _to.Margin = new Padding(4);
            _to.Name = "_to";
            _to.Size = new Size(130, 28);
            _category.DropDownStyle = ComboBoxStyle.DropDownList;
            _category.Items.AddRange(new object[] { "Tất cả danh mục" });
            _category.Margin = new Padding(4);
            _category.Name = "_category";
            _category.SelectedIndex = 0;
            _category.Size = new Size(200, 28);
            _grid.AllowUserToAddRows = false;
            _grid.AllowUserToDeleteRows = false;
            _grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _grid.BackgroundColor = Color.White;
            _grid.BorderStyle = BorderStyle.None;
            _grid.ColumnHeadersHeight = 40;
            _grid.Columns.AddRange(new DataGridViewColumn[] { _code, _name, _categoryColumn, _quantity, _unitPrice, _stockValue });
            _grid.Dock = DockStyle.Fill;
            _grid.ReadOnly = true;
            _grid.RowHeadersVisible = false;
            _code.HeaderText = "Mã hàng";
            _code.Name = "Code";
            _name.HeaderText = "Tên sản phẩm";
            _name.Name = "Name";
            _categoryColumn.HeaderText = "Danh mục";
            _categoryColumn.Name = "Category";
            _quantity.HeaderText = "Số lượng tồn";
            _quantity.Name = "Quantity";
            _unitPrice.HeaderText = "Đơn giá";
            _unitPrice.Name = "UnitPrice";
            _stockValue.HeaderText = "Giá trị tồn";
            _stockValue.Name = "StockValue";
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 247, 250);
            Controls.Add(_root);
            Name = "ucInventoryReport";
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
