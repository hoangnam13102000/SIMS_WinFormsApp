using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

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
        private Label _fromLabel;
        private DateTimePicker _from;
        private Label _toLabel;
        private DateTimePicker _to;
        private ComboBox _category;
        private Button _apply;
        private Button _exportPdf;
        private TableLayoutPanel _summary;
        private Label _productCount;
        private Label _totalStock;
        private Label _stockValue;
        private Label _lowStock;
        private TableLayoutPanel _content;
        private Panel _chartPanel;
        private TableLayoutPanel _chartsLayout;
        private Panel _importChartPanel;
        private Label _chartTitle;
        private Chart _importChart;
        private Panel _categoryChartPanel;
        private Label _categoryChartTitle;
        private Chart _categoryChart;
        private DataGridView _grid;
        private DataGridViewTextBoxColumn _code;
        private DataGridViewTextBoxColumn _name;
        private DataGridViewTextBoxColumn _categoryColumn;
        private DataGridViewTextBoxColumn _quantity;
        private DataGridViewTextBoxColumn _unitPrice;
        private DataGridViewTextBoxColumn _stockValueColumn;
        private DataGridViewTextBoxColumn _status;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
            }

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
            _fromLabel = new Label();
            _from = new DateTimePicker();
            _toLabel = new Label();
            _to = new DateTimePicker();
            _category = new ComboBox();
            _apply = new Button();
            _exportPdf = new Button();
            _summary = new TableLayoutPanel();
            _productCount = new Label();
            _totalStock = new Label();
            _stockValue = new Label();
            _lowStock = new Label();
            _content = new TableLayoutPanel();
            _chartPanel = new Panel();
            _chartsLayout = new TableLayoutPanel();
            _importChartPanel = new Panel();
            _chartTitle = new Label();
            _importChart = new Chart();
            _categoryChartPanel = new Panel();
            _categoryChartTitle = new Label();
            _categoryChart = new Chart();
            _grid = new DataGridView();
            _code = new DataGridViewTextBoxColumn();
            _name = new DataGridViewTextBoxColumn();
            _categoryColumn = new DataGridViewTextBoxColumn();
            _quantity = new DataGridViewTextBoxColumn();
            _unitPrice = new DataGridViewTextBoxColumn();
            _stockValueColumn = new DataGridViewTextBoxColumn();
            _status = new DataGridViewTextBoxColumn();
            _root.SuspendLayout();
            _header.SuspendLayout();
            _filters.SuspendLayout();
            _summary.SuspendLayout();
            _content.SuspendLayout();
            _chartPanel.SuspendLayout();
            _chartsLayout.SuspendLayout();
            _importChartPanel.SuspendLayout();
            _categoryChartPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_importChart).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_categoryChart).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_grid).BeginInit();
            SuspendLayout();
            //
            // _root
            //
            _root.ColumnCount = 1;
            _root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _root.Controls.Add(_header, 0, 0);
            _root.Controls.Add(_filters, 0, 1);
            _root.Controls.Add(_summary, 0, 2);
            _root.Controls.Add(_content, 0, 3);
            _root.Dock = DockStyle.Fill;
            _root.Padding = new Padding(24);
            _root.RowCount = 4;
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 76F));
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 96F));
            _root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            _root.Name = "_root";
            //
            // _header
            //
            _header.Controls.Add(_subtitle);
            _header.Controls.Add(_title);
            _header.Dock = DockStyle.Fill;
            _title.AutoSize = true;
            _title.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            _title.ForeColor = Color.FromArgb(15, 23, 42);
            _title.Location = new Point(0, 0);
            _title.Name = "_title";
            _title.Text = "Báo cáo tồn kho";
            _subtitle.AutoSize = true;
            _subtitle.ForeColor = Color.FromArgb(100, 116, 139);
            _subtitle.Location = new Point(2, 44);
            _subtitle.Name = "_subtitle";
            _subtitle.Text = "Tồn kho hiện tại, hàng cần bổ sung và tình hình nhập hàng";
            //
            // _filters
            //
            _filters.Controls.Add(_fromLabel);
            _filters.Controls.Add(_from);
            _filters.Controls.Add(_toLabel);
            _filters.Controls.Add(_to);
            _filters.Controls.Add(_category);
            _filters.Controls.Add(_apply);
            _filters.Controls.Add(_exportPdf);
            _filters.Dock = DockStyle.Fill;
            _filters.WrapContents = false;
            _filters.Name = "_filters";
            _fromLabel.AutoSize = true;
            _fromLabel.Margin = new Padding(0, 9, 6, 0);
            _fromLabel.Text = "Từ ngày";
            _from.Format = DateTimePickerFormat.Short;
            _from.Margin = new Padding(0, 4, 8, 0);
            _from.Size = new Size(120, 28);
            _from.Name = "_from";
            _toLabel.AutoSize = true;
            _toLabel.Margin = new Padding(8, 9, 6, 0);
            _toLabel.Text = "Đến ngày";
            _to.Format = DateTimePickerFormat.Short;
            _to.Margin = new Padding(0, 4, 8, 0);
            _to.Size = new Size(120, 28);
            _to.Name = "_to";
            _category.DropDownStyle = ComboBoxStyle.DropDownList;
            _category.Margin = new Padding(4, 4, 8, 0);
            _category.Size = new Size(190, 28);
            _category.Name = "_category";
            _apply.Margin = new Padding(4, 3, 4, 0);
            _apply.Size = new Size(90, 30);
            _apply.Text = "Áp dụng";
            _apply.UseVisualStyleBackColor = true;
            _apply.Name = "_apply";
            _exportPdf.Margin = new Padding(4, 3, 0, 0);
            _exportPdf.Size = new Size(150, 30);
            _exportPdf.Text = "Xuất báo cáo PDF";
            _exportPdf.UseVisualStyleBackColor = true;
            _exportPdf.Name = "_exportPdf";
            //
            // _summary
            //
            _summary.ColumnCount = 4;
            _summary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            _summary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            _summary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            _summary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            _summary.Controls.Add(_productCount, 0, 0);
            _summary.Controls.Add(_totalStock, 1, 0);
            _summary.Controls.Add(_stockValue, 2, 0);
            _summary.Controls.Add(_lowStock, 3, 0);
            _summary.Dock = DockStyle.Fill;
            _summary.Name = "_summary";
            _productCount.BackColor = Color.White;
            _productCount.Dock = DockStyle.Fill;
            _productCount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _productCount.ForeColor = Color.FromArgb(30, 64, 110);
            _productCount.Margin = new Padding(0, 0, 10, 0);
            _productCount.Padding = new Padding(14);
            _productCount.TextAlign = ContentAlignment.MiddleLeft;
            _productCount.Name = "_productCount";
            _totalStock.BackColor = Color.White;
            _totalStock.Dock = DockStyle.Fill;
            _totalStock.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _totalStock.ForeColor = Color.FromArgb(30, 64, 110);
            _totalStock.Margin = new Padding(0, 0, 10, 0);
            _totalStock.Padding = new Padding(14);
            _totalStock.TextAlign = ContentAlignment.MiddleLeft;
            _totalStock.Name = "_totalStock";
            _stockValue.BackColor = Color.White;
            _stockValue.Dock = DockStyle.Fill;
            _stockValue.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _stockValue.ForeColor = Color.FromArgb(30, 64, 110);
            _stockValue.Margin = new Padding(0, 0, 10, 0);
            _stockValue.Padding = new Padding(14);
            _stockValue.TextAlign = ContentAlignment.MiddleLeft;
            _stockValue.Name = "_stockValue";
            _lowStock.BackColor = Color.White;
            _lowStock.Dock = DockStyle.Fill;
            _lowStock.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _lowStock.ForeColor = Color.FromArgb(30, 64, 110);
            _lowStock.Margin = new Padding(0, 0, 10, 0);
            _lowStock.Padding = new Padding(14);
            _lowStock.TextAlign = ContentAlignment.MiddleLeft;
            _lowStock.Name = "_lowStock";
            //
            // _content
            //
            _content.ColumnCount = 1;
            _content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _content.Controls.Add(_chartPanel, 0, 0);
            _content.Controls.Add(_grid, 0, 1);
            _content.Dock = DockStyle.Fill;
            _content.RowCount = 2;
            _content.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            _content.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            _content.Name = "_content";
            //
            // _chartPanel
            //
            _chartPanel.BackColor = Color.White;
            _chartPanel.Controls.Add(_chartsLayout);
            _chartPanel.Dock = DockStyle.Fill;
            _chartPanel.Margin = new Padding(0, 8, 0, 8);
            _chartPanel.Padding = new Padding(12);
            //
            // _chartsLayout
            //
            _chartsLayout.ColumnCount = 2;
            _chartsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            _chartsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            _chartsLayout.Controls.Add(_importChartPanel, 0, 0);
            _chartsLayout.Controls.Add(_categoryChartPanel, 1, 0);
            _chartsLayout.Dock = DockStyle.Fill;
            _chartsLayout.Margin = new Padding(0);
            _chartsLayout.Padding = new Padding(0);
            _chartsLayout.RowCount = 1;
            _chartsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            _chartsLayout.Name = "_chartsLayout";
            //
            // _importChartPanel
            //
            _importChartPanel.Controls.Add(_importChart);
            _importChartPanel.Controls.Add(_chartTitle);
            _importChartPanel.Dock = DockStyle.Fill;
            _importChartPanel.Padding = new Padding(0, 0, 6, 0);
            _importChartPanel.Name = "_importChartPanel";
            _chartTitle.Dock = DockStyle.Top;
            _chartTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            _chartTitle.ForeColor = Color.FromArgb(15, 23, 42);
            _chartTitle.Height = 30;
            _chartTitle.Text = "Số lượng nhập theo ngày";
            _chartTitle.Name = "_chartTitle";
            _importChart.Dock = DockStyle.Fill;
            _importChart.Name = "_importChart";
            //
            // _categoryChartPanel
            //
            _categoryChartPanel.Controls.Add(_categoryChart);
            _categoryChartPanel.Controls.Add(_categoryChartTitle);
            _categoryChartPanel.Dock = DockStyle.Fill;
            _categoryChartPanel.Padding = new Padding(6, 0, 0, 0);
            _categoryChartPanel.Name = "_categoryChartPanel";
            _categoryChartTitle.Dock = DockStyle.Top;
            _categoryChartTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            _categoryChartTitle.ForeColor = Color.FromArgb(15, 23, 42);
            _categoryChartTitle.Height = 30;
            _categoryChartTitle.Text = "Giá trị tồn theo danh mục";
            _categoryChartTitle.Name = "_categoryChartTitle";
            _categoryChart.Dock = DockStyle.Fill;
            _categoryChart.Name = "_categoryChart";
            //
            // _grid
            //
            _grid.AllowUserToAddRows = false;
            _grid.AllowUserToDeleteRows = false;
            _grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _grid.BackgroundColor = Color.White;
            _grid.BorderStyle = BorderStyle.None;
            _grid.ColumnHeadersHeight = 40;
            _grid.Columns.AddRange(new DataGridViewColumn[]
            {
                _code, _name, _categoryColumn, _quantity, _unitPrice, _stockValueColumn, _status
            });
            _grid.Dock = DockStyle.Fill;
            _grid.Margin = new Padding(0, 8, 0, 0);
            _grid.ReadOnly = true;
            _grid.RowHeadersVisible = false;
            _grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _grid.Name = "_grid";
            _code.HeaderText = "Mã hàng";
            _code.Name = "Code";
            _code.FillWeight = 22F;
            _name.HeaderText = "Sản phẩm";
            _name.Name = "Name";
            _name.FillWeight = 38F;
            _categoryColumn.HeaderText = "Danh mục";
            _categoryColumn.Name = "Category";
            _categoryColumn.FillWeight = 28F;
            _quantity.HeaderText = "Tồn";
            _quantity.Name = "Quantity";
            _quantity.FillWeight = 16F;
            _unitPrice.HeaderText = "Giá nhập";
            _unitPrice.Name = "UnitPrice";
            _unitPrice.FillWeight = 25F;
            _stockValueColumn.HeaderText = "Giá trị tồn";
            _stockValueColumn.Name = "StockValue";
            _stockValueColumn.FillWeight = 28F;
            _status.HeaderText = "Tình trạng";
            _status.Name = "Status";
            _status.FillWeight = 27F;
            //
            // ucInventoryReport
            //
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 247, 250);
            Controls.Add(_root);
            Name = "ucInventoryReport";
            Size = new Size(1180, 800);
            _root.ResumeLayout(false);
            _header.ResumeLayout(false);
            _header.PerformLayout();
            _filters.ResumeLayout(false);
            _filters.PerformLayout();
            _summary.ResumeLayout(false);
            _content.ResumeLayout(false);
            _chartPanel.ResumeLayout(false);
            _chartsLayout.ResumeLayout(false);
            _importChartPanel.ResumeLayout(false);
            _categoryChartPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_importChart).EndInit();
            ((System.ComponentModel.ISupportInitialize)_categoryChart).EndInit();
            ((System.ComponentModel.ISupportInitialize)_grid).EndInit();
            ResumeLayout(false);
        }

    }
}
