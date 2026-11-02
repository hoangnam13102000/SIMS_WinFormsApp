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
        private Button _exportOptions;
        private ContextMenuStrip _exportMenu;
        private ToolStripMenuItem _exportCategoryCsv;
        private ToolStripMenuItem _exportCategoryExcel;
        private TableLayoutPanel _summary;
        private Label _productCount;
        private Label _totalStock;
        private Label _stockValue;
        private Label _lowStock;
        private Label _sellValue;
        private Label _outOfStock;
        private Label _disposalSummary;
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
        private Panel _disposalPanel;
        private TableLayoutPanel _disposalLayout;
        private Panel _disposalChartPanel;
        private Label _disposalChartTitle;
        private Chart _disposalChart;
        private Panel _disposalProductPanel;
        private Label _disposalProductTitle;
        private DataGridView _disposalGrid;
        private DataGridViewTextBoxColumn _disposalProductColumn;
        private DataGridViewTextBoxColumn _disposalQuantityColumn;
        private DataGridViewTextBoxColumn _disposalLossColumn;
        private Panel _productStockPanel;
        private Label _productStockTitle;
        private FlowLayoutPanel _productStockLegend;
        private Panel _legendNormalColor;
        private Label _legendNormal;
        private Panel _legendWarningColor;
        private Label _legendWarning;
        private Panel _legendExpiredColor;
        private Label _legendExpired;
        private Panel _legendOutColor;
        private Label _legendOut;
        private Chart _productStockChart;
        private TableLayoutPanel _breakdownLayout;
        private Panel _categoryBreakdownPanel;
        private Label _categoryBreakdownTitle;
        private DataGridView _categoryBreakdownGrid;
        private DataGridViewTextBoxColumn _cbName;
        private DataGridViewTextBoxColumn _cbProducts;
        private DataGridViewTextBoxColumn _cbQuantity;
        private DataGridViewTextBoxColumn _cbValue;
        private DataGridViewTextBoxColumn _cbRatio;
        private Panel _priceBreakdownPanel;
        private Label _priceBreakdownTitle;
        private DataGridView _priceBreakdownGrid;
        private DataGridViewTextBoxColumn _pbRange;
        private DataGridViewTextBoxColumn _pbProducts;
        private DataGridViewTextBoxColumn _pbQuantity;
        private DataGridViewTextBoxColumn _pbValue;
        private DataGridViewTextBoxColumn _pbRatio;
        private Panel _trendPanel;
        private Label _trendTitle;
        private FlowLayoutPanel _trendFilters;
        private ComboBox _trendPreset;
        private Label _trendFromLabel;
        private DateTimePicker _trendFrom;
        private Label _trendToLabel;
        private DateTimePicker _trendTo;
        private Button _trendApply;
        private Chart _trendChart;
        private Panel _historyPanel;
        private Label _historyTitle;
        private Label _historySubtitle;
        private FlowLayoutPanel _historyFilters;
        private Label _historySearchLabel;
        private TextBox _historySearch;
        private Label _historyFromLabel;
        private DateTimePicker _historyFrom;
        private Label _historyToLabel;
        private DateTimePicker _historyTo;
        private Button _historySearchButton;
        private Button _historyClear;
        private Button _historyExport;
        private ContextMenuStrip _historyExportMenu;
        private ToolStripMenuItem _historyExportCsv;
        private ToolStripMenuItem _historyExportExcel;
        private FlowLayoutPanel _historyPager;
        private Button _historyNext;
        private Label _historyPageInfo;
        private Button _historyPrev;
        private DataGridView _historyGrid;
        private DataGridViewTextBoxColumn _hBatch;
        private DataGridViewTextBoxColumn _hTime;
        private DataGridViewTextBoxColumn _hType;
        private DataGridViewTextBoxColumn _hDocument;
        private DataGridViewTextBoxColumn _hProductCode;
        private DataGridViewTextBoxColumn _hProductName;
        private DataGridViewTextBoxColumn _hStock;
        private DataGridViewTextBoxColumn _hChange;
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
            _exportOptions = new Button();
            _exportMenu = new ContextMenuStrip(components);
            _exportCategoryCsv = new ToolStripMenuItem();
            _exportCategoryExcel = new ToolStripMenuItem();
            _summary = new TableLayoutPanel();
            _productCount = new Label();
            _totalStock = new Label();
            _stockValue = new Label();
            _lowStock = new Label();
            _sellValue = new Label();
            _outOfStock = new Label();
            _disposalSummary = new Label();
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
            _disposalPanel = new Panel();
            _disposalLayout = new TableLayoutPanel();
            _disposalChartPanel = new Panel();
            _disposalChartTitle = new Label();
            _disposalChart = new Chart();
            _disposalProductPanel = new Panel();
            _disposalProductTitle = new Label();
            _disposalGrid = new DataGridView();
            _disposalProductColumn = new DataGridViewTextBoxColumn();
            _disposalQuantityColumn = new DataGridViewTextBoxColumn();
            _disposalLossColumn = new DataGridViewTextBoxColumn();
            _productStockPanel = new Panel();
            _productStockTitle = new Label();
            _productStockLegend = new FlowLayoutPanel();
            _legendNormalColor = new Panel();
            _legendNormal = new Label();
            _legendWarningColor = new Panel();
            _legendWarning = new Label();
            _legendExpiredColor = new Panel();
            _legendExpired = new Label();
            _legendOutColor = new Panel();
            _legendOut = new Label();
            _productStockChart = new Chart();
            _breakdownLayout = new TableLayoutPanel();
            _categoryBreakdownPanel = new Panel();
            _categoryBreakdownTitle = new Label();
            _categoryBreakdownGrid = new DataGridView();
            _cbName = new DataGridViewTextBoxColumn();
            _cbProducts = new DataGridViewTextBoxColumn();
            _cbQuantity = new DataGridViewTextBoxColumn();
            _cbValue = new DataGridViewTextBoxColumn();
            _cbRatio = new DataGridViewTextBoxColumn();
            _priceBreakdownPanel = new Panel();
            _priceBreakdownTitle = new Label();
            _priceBreakdownGrid = new DataGridView();
            _pbRange = new DataGridViewTextBoxColumn();
            _pbProducts = new DataGridViewTextBoxColumn();
            _pbQuantity = new DataGridViewTextBoxColumn();
            _pbValue = new DataGridViewTextBoxColumn();
            _pbRatio = new DataGridViewTextBoxColumn();
            _trendPanel = new Panel();
            _trendTitle = new Label();
            _trendFilters = new FlowLayoutPanel();
            _trendPreset = new ComboBox();
            _trendFromLabel = new Label();
            _trendFrom = new DateTimePicker();
            _trendToLabel = new Label();
            _trendTo = new DateTimePicker();
            _trendApply = new Button();
            _trendChart = new Chart();
            _historyPanel = new Panel();
            _historyTitle = new Label();
            _historySubtitle = new Label();
            _historyFilters = new FlowLayoutPanel();
            _historySearchLabel = new Label();
            _historySearch = new TextBox();
            _historyFromLabel = new Label();
            _historyFrom = new DateTimePicker();
            _historyToLabel = new Label();
            _historyTo = new DateTimePicker();
            _historySearchButton = new Button();
            _historyClear = new Button();
            _historyExport = new Button();
            _historyExportMenu = new ContextMenuStrip(components);
            _historyExportCsv = new ToolStripMenuItem();
            _historyExportExcel = new ToolStripMenuItem();
            _historyPager = new FlowLayoutPanel();
            _historyNext = new Button();
            _historyPageInfo = new Label();
            _historyPrev = new Button();
            _historyGrid = new DataGridView();
            _hBatch = new DataGridViewTextBoxColumn();
            _hTime = new DataGridViewTextBoxColumn();
            _hType = new DataGridViewTextBoxColumn();
            _hDocument = new DataGridViewTextBoxColumn();
            _hProductCode = new DataGridViewTextBoxColumn();
            _hProductName = new DataGridViewTextBoxColumn();
            _hStock = new DataGridViewTextBoxColumn();
            _hChange = new DataGridViewTextBoxColumn();
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
            _disposalPanel.SuspendLayout();
            _disposalLayout.SuspendLayout();
            _disposalChartPanel.SuspendLayout();
            _disposalProductPanel.SuspendLayout();
            _productStockPanel.SuspendLayout();
            _productStockLegend.SuspendLayout();
            _breakdownLayout.SuspendLayout();
            _categoryBreakdownPanel.SuspendLayout();
            _priceBreakdownPanel.SuspendLayout();
            _trendPanel.SuspendLayout();
            _trendFilters.SuspendLayout();
            _historyPanel.SuspendLayout();
            _historyFilters.SuspendLayout();
            _historyPager.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_importChart).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_categoryChart).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_grid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_disposalChart).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_disposalGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_productStockChart).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_categoryBreakdownGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_priceBreakdownGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_trendChart).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_historyGrid).BeginInit();
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
            _root.Controls.Add(_productStockPanel, 0, 4);
            _root.Controls.Add(_breakdownLayout, 0, 5);
            _root.Controls.Add(_trendPanel, 0, 6);
            _root.Controls.Add(_historyPanel, 0, 7);
            _root.AutoSize = true;
            _root.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            _root.Dock = DockStyle.Top;
            _root.Padding = new Padding(24);
            _root.RowCount = 8;
            _root.RowStyles.Clear();
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 76F));
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 96F));
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 640F));
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 400F));
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 320F));
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 440F));
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 600F));
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
            _title.Text = "Báo cáo hàng tồn kho";
            _subtitle.AutoSize = true;
            _subtitle.ForeColor = Color.FromArgb(100, 116, 139);
            _subtitle.Location = new Point(2, 44);
            _subtitle.Name = "_subtitle";
            _subtitle.Text = "Thống kê số lượng và giá trị tồn kho theo danh mục, theo khoảng giá bán, xu hướng theo tháng và lịch sử từng lô";
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
            _filters.Controls.Add(_exportOptions);
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
            _exportOptions.Margin = new Padding(8, 3, 0, 0);
            _exportOptions.Size = new Size(110, 30);
            _exportOptions.Text = "Tùy chọn ▾";
            _exportOptions.UseVisualStyleBackColor = true;
            _exportOptions.Name = "_exportOptions";
            _exportMenu.Items.AddRange(new ToolStripItem[]
            {
                _exportCategoryCsv,
                _exportCategoryExcel
            });
            _exportCategoryCsv.Text = "Xuất CSV tồn theo danh mục";
            _exportCategoryExcel.Text = "Xuất Excel tồn theo danh mục";
            //
            // _summary
            //
            _summary.ColumnCount = 6;
            _summary.ColumnStyles.Clear();
            _summary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66F));
            _summary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66F));
            _summary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66F));
            _summary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66F));
            _summary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66F));
            _summary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66F));
            _summary.Controls.Add(_productCount, 0, 0);
            _summary.Controls.Add(_totalStock, 1, 0);
            _summary.Controls.Add(_stockValue, 2, 0);
            _summary.Controls.Add(_lowStock, 3, 0);
            _summary.Controls.Add(_sellValue, 4, 0);
            _summary.Controls.Add(_outOfStock, 5, 0);
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
            _sellValue.AutoSize = false;
            _sellValue.BackColor = Color.White;
            _sellValue.Dock = DockStyle.Fill;
            _sellValue.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _sellValue.ForeColor = Color.FromArgb(30, 64, 110);
            _sellValue.Margin = new Padding(0, 0, 10, 0);
            _sellValue.Padding = new Padding(14);
            _sellValue.TextAlign = ContentAlignment.MiddleLeft;
            _sellValue.Name = "_sellValue";
            _outOfStock.AutoSize = false;
            _outOfStock.BackColor = Color.White;
            _outOfStock.Dock = DockStyle.Fill;
            _outOfStock.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _outOfStock.ForeColor = Color.FromArgb(30, 64, 110);
            _outOfStock.Margin = new Padding(0);
            _outOfStock.Padding = new Padding(14);
            _outOfStock.TextAlign = ContentAlignment.MiddleLeft;
            _outOfStock.Name = "_outOfStock";
            _disposalSummary.BackColor = Color.White;
            _disposalSummary.Dock = DockStyle.Fill;
            _disposalSummary.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _disposalSummary.ForeColor = Color.FromArgb(30, 64, 110);
            _disposalSummary.Margin = new Padding(0, 0, 10, 0);
            _disposalSummary.Padding = new Padding(14);
            _disposalSummary.Text = "THIỆT HẠI TIÊU HỦY\r\n0 đ";
            _disposalSummary.TextAlign = ContentAlignment.MiddleLeft;
            _disposalSummary.Name = "_disposalSummary";
            //
            // _content
            //
            _content.ColumnCount = 1;
            _content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _content.Controls.Add(_chartPanel, 0, 0);
            _content.Controls.Add(_grid, 0, 1);
            _content.Controls.Add(_disposalPanel, 0, 2);
            _content.Dock = DockStyle.Fill;
            _content.RowCount = 3;
            _content.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            _content.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            _content.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
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
            _chartsLayout.ColumnCount = 3;
            _chartsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            _chartsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            _chartsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            _chartsLayout.Controls.Add(_importChartPanel, 0, 0);
            _chartsLayout.Controls.Add(_categoryChartPanel, 1, 0);
            _chartsLayout.Controls.Add(_disposalChartPanel, 2, 0);
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
            _categoryChartPanel.Padding = new Padding(6, 0, 6, 0);
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
            // _productStockPanel
            //
            _productStockPanel.BackColor = Color.White;
            _productStockPanel.Controls.Add(_productStockChart);
            _productStockPanel.Controls.Add(_productStockLegend);
            _productStockPanel.Controls.Add(_productStockTitle);
            _productStockPanel.Dock = DockStyle.Fill;
            _productStockPanel.Margin = new Padding(0, 8, 0, 8);
            _productStockPanel.Padding = new Padding(12);
            _productStockPanel.Name = "_productStockPanel";
            _productStockTitle.Dock = DockStyle.Top;
            _productStockTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            _productStockTitle.Height = 28;
            _productStockTitle.Text = "Tồn kho theo sản phẩm (top 20)";
            _productStockTitle.Name = "_productStockTitle";
            _productStockLegend.Controls.Add(_legendNormalColor);
            _productStockLegend.Controls.Add(_legendNormal);
            _productStockLegend.Controls.Add(_legendWarningColor);
            _productStockLegend.Controls.Add(_legendWarning);
            _productStockLegend.Controls.Add(_legendExpiredColor);
            _productStockLegend.Controls.Add(_legendExpired);
            _productStockLegend.Controls.Add(_legendOutColor);
            _productStockLegend.Controls.Add(_legendOut);
            _productStockLegend.Dock = DockStyle.Bottom;
            _productStockLegend.Height = 28;
            _productStockLegend.WrapContents = false;
            _productStockLegend.Name = "_productStockLegend";
            _legendNormalColor.BackColor = Color.FromArgb(34, 197, 94);
            _legendNormalColor.Margin = new Padding(0, 8, 4, 0);
            _legendNormalColor.Size = new Size(12, 12);
            _legendNormalColor.Name = "_legendNormalColor";
            _legendNormal.AutoSize = true;
            _legendNormal.Margin = new Padding(0, 6, 16, 0);
            _legendNormal.Text = "Còn hàng, bình thường";
            _legendNormal.Name = "_legendNormal";
            _legendWarningColor.BackColor = Color.FromArgb(245, 158, 11);
            _legendWarningColor.Margin = new Padding(0, 8, 4, 0);
            _legendWarningColor.Size = new Size(12, 12);
            _legendWarningColor.Name = "_legendWarningColor";
            _legendWarning.AutoSize = true;
            _legendWarning.Margin = new Padding(0, 6, 16, 0);
            _legendWarning.Text = "Sắp hết hạn / dưới tồn tối thiểu";
            _legendWarning.Name = "_legendWarning";
            _legendExpiredColor.BackColor = Color.FromArgb(239, 68, 68);
            _legendExpiredColor.Margin = new Padding(0, 8, 4, 0);
            _legendExpiredColor.Size = new Size(12, 12);
            _legendExpiredColor.Name = "_legendExpiredColor";
            _legendExpired.AutoSize = true;
            _legendExpired.Margin = new Padding(0, 6, 16, 0);
            _legendExpired.Text = "Có lô đã hết hạn còn tồn";
            _legendExpired.Name = "_legendExpired";
            _legendOutColor.BackColor = Color.FromArgb(148, 163, 184);
            _legendOutColor.Margin = new Padding(0, 8, 4, 0);
            _legendOutColor.Size = new Size(12, 12);
            _legendOutColor.Name = "_legendOutColor";
            _legendOut.AutoSize = true;
            _legendOut.Margin = new Padding(0, 6, 0, 0);
            _legendOut.Text = "Đã hết hàng";
            _legendOut.Name = "_legendOut";
            _productStockChart.Dock = DockStyle.Fill;
            _productStockChart.ChartAreas.Add(new ChartArea("ProductStockArea"));
            _productStockChart.Series.Add(new Series("Tồn kho")
            {
                ChartArea = "ProductStockArea",
                ChartType = SeriesChartType.Column,
                IsXValueIndexed = true,
                IsVisibleInLegend = false
            });
            _productStockChart.Name = "_productStockChart";
            //
            // _breakdownLayout
            //
            _breakdownLayout.ColumnCount = 2;
            _breakdownLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            _breakdownLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            _breakdownLayout.Controls.Add(_categoryBreakdownPanel, 0, 0);
            _breakdownLayout.Controls.Add(_priceBreakdownPanel, 1, 0);
            _breakdownLayout.Dock = DockStyle.Fill;
            _breakdownLayout.Margin = new Padding(0, 8, 0, 8);
            _breakdownLayout.RowCount = 1;
            _breakdownLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            _breakdownLayout.Name = "_breakdownLayout";
            //
            // _categoryBreakdownPanel
            //
            _categoryBreakdownPanel.BackColor = Color.White;
            _categoryBreakdownPanel.Controls.Add(_categoryBreakdownGrid);
            _categoryBreakdownPanel.Controls.Add(_categoryBreakdownTitle);
            _categoryBreakdownPanel.Dock = DockStyle.Fill;
            _categoryBreakdownPanel.Margin = new Padding(0, 0, 6, 0);
            _categoryBreakdownPanel.Padding = new Padding(12);
            _categoryBreakdownPanel.Name = "_categoryBreakdownPanel";
            _categoryBreakdownTitle.Dock = DockStyle.Top;
            _categoryBreakdownTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            _categoryBreakdownTitle.Height = 28;
            _categoryBreakdownTitle.Text = "Tồn kho theo danh mục";
            _categoryBreakdownTitle.Name = "_categoryBreakdownTitle";
            //
            // _categoryBreakdownGrid
            //
            _categoryBreakdownGrid.AllowUserToAddRows = false;
            _categoryBreakdownGrid.AllowUserToDeleteRows = false;
            _categoryBreakdownGrid.AllowUserToResizeRows = false;
            _categoryBreakdownGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _categoryBreakdownGrid.BackgroundColor = _grid.BackgroundColor;
            _categoryBreakdownGrid.BorderStyle = _grid.BorderStyle;
            _categoryBreakdownGrid.CellBorderStyle = _grid.CellBorderStyle;
            _categoryBreakdownGrid.ColumnHeadersDefaultCellStyle = _grid.ColumnHeadersDefaultCellStyle.Clone();
            _categoryBreakdownGrid.ColumnHeadersHeight = _grid.ColumnHeadersHeight;
            _categoryBreakdownGrid.Columns.AddRange(new DataGridViewColumn[]
            {
                _cbName, _cbProducts, _cbQuantity, _cbValue, _cbRatio
            });
            _categoryBreakdownGrid.DefaultCellStyle = _grid.DefaultCellStyle.Clone();
            _categoryBreakdownGrid.Dock = DockStyle.Fill;
            _categoryBreakdownGrid.EnableHeadersVisualStyles = _grid.EnableHeadersVisualStyles;
            _categoryBreakdownGrid.GridColor = _grid.GridColor;
            _categoryBreakdownGrid.Margin = new Padding(0);
            _categoryBreakdownGrid.ReadOnly = true;
            _categoryBreakdownGrid.RowHeadersVisible = false;
            _categoryBreakdownGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _categoryBreakdownGrid.ShowCellToolTips = true;
            _categoryBreakdownGrid.Name = "_categoryBreakdownGrid";
            _cbName.HeaderText = "Danh mục";
            _cbName.Name = "_cbName";
            _cbName.FillWeight = 30F;
            _cbProducts.HeaderText = "Số SP";
            _cbProducts.Name = "_cbProducts";
            _cbProducts.FillWeight = 12F;
            _cbProducts.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            _cbQuantity.HeaderText = "Số lượng";
            _cbQuantity.Name = "_cbQuantity";
            _cbQuantity.FillWeight = 15F;
            _cbQuantity.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            _cbValue.HeaderText = "Giá trị (giá bán)";
            _cbValue.Name = "_cbValue";
            _cbValue.FillWeight = 23F;
            _cbValue.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            _cbRatio.HeaderText = "Tỷ lệ";
            _cbRatio.Name = "_cbRatio";
            _cbRatio.FillWeight = 20F;
            //
            // _priceBreakdownPanel
            //
            _priceBreakdownPanel.BackColor = Color.White;
            _priceBreakdownPanel.Controls.Add(_priceBreakdownGrid);
            _priceBreakdownPanel.Controls.Add(_priceBreakdownTitle);
            _priceBreakdownPanel.Dock = DockStyle.Fill;
            _priceBreakdownPanel.Margin = new Padding(6, 0, 0, 0);
            _priceBreakdownPanel.Padding = new Padding(12);
            _priceBreakdownPanel.Name = "_priceBreakdownPanel";
            _priceBreakdownTitle.Dock = DockStyle.Top;
            _priceBreakdownTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            _priceBreakdownTitle.Height = 28;
            _priceBreakdownTitle.Text = "Tồn kho theo khoảng giá bán";
            _priceBreakdownTitle.Name = "_priceBreakdownTitle";
            //
            // _priceBreakdownGrid
            //
            _priceBreakdownGrid.AllowUserToAddRows = false;
            _priceBreakdownGrid.AllowUserToDeleteRows = false;
            _priceBreakdownGrid.AllowUserToResizeRows = false;
            _priceBreakdownGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _priceBreakdownGrid.BackgroundColor = _grid.BackgroundColor;
            _priceBreakdownGrid.BorderStyle = _grid.BorderStyle;
            _priceBreakdownGrid.CellBorderStyle = _grid.CellBorderStyle;
            _priceBreakdownGrid.ColumnHeadersDefaultCellStyle = _grid.ColumnHeadersDefaultCellStyle.Clone();
            _priceBreakdownGrid.ColumnHeadersHeight = _grid.ColumnHeadersHeight;
            _priceBreakdownGrid.Columns.AddRange(new DataGridViewColumn[]
            {
                _pbRange, _pbProducts, _pbQuantity, _pbValue, _pbRatio
            });
            _priceBreakdownGrid.DefaultCellStyle = _grid.DefaultCellStyle.Clone();
            _priceBreakdownGrid.Dock = DockStyle.Fill;
            _priceBreakdownGrid.EnableHeadersVisualStyles = _grid.EnableHeadersVisualStyles;
            _priceBreakdownGrid.GridColor = _grid.GridColor;
            _priceBreakdownGrid.Margin = new Padding(0);
            _priceBreakdownGrid.ReadOnly = true;
            _priceBreakdownGrid.RowHeadersVisible = false;
            _priceBreakdownGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _priceBreakdownGrid.ShowCellToolTips = true;
            _priceBreakdownGrid.Name = "_priceBreakdownGrid";
            _pbRange.HeaderText = "Khoảng giá bán";
            _pbRange.Name = "_pbRange";
            _pbRange.FillWeight = 30F;
            _pbProducts.HeaderText = "Số SP";
            _pbProducts.Name = "_pbProducts";
            _pbProducts.FillWeight = 12F;
            _pbProducts.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            _pbQuantity.HeaderText = "Số lượng";
            _pbQuantity.Name = "_pbQuantity";
            _pbQuantity.FillWeight = 15F;
            _pbQuantity.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            _pbValue.HeaderText = "Giá trị (giá bán)";
            _pbValue.Name = "_pbValue";
            _pbValue.FillWeight = 23F;
            _pbValue.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            _pbRatio.HeaderText = "Tỷ lệ";
            _pbRatio.Name = "_pbRatio";
            _pbRatio.FillWeight = 20F;
            //
            // _trendPanel
            //
            _trendPanel.BackColor = Color.White;
            _trendPanel.Controls.Add(_trendChart);
            _trendPanel.Controls.Add(_trendFilters);
            _trendPanel.Controls.Add(_trendTitle);
            _trendPanel.Dock = DockStyle.Fill;
            _trendPanel.Margin = new Padding(0, 8, 0, 8);
            _trendPanel.Padding = new Padding(12);
            _trendPanel.Name = "_trendPanel";
            _trendTitle.Dock = DockStyle.Top;
            _trendTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            _trendTitle.Height = 28;
            _trendTitle.Text = "Xu hướng tồn kho theo tháng & danh mục";
            _trendTitle.Name = "_trendTitle";
            _trendFilters.Controls.Add(_trendPreset);
            _trendFilters.Controls.Add(_trendFromLabel);
            _trendFilters.Controls.Add(_trendFrom);
            _trendFilters.Controls.Add(_trendToLabel);
            _trendFilters.Controls.Add(_trendTo);
            _trendFilters.Controls.Add(_trendApply);
            _trendFilters.Dock = DockStyle.Top;
            _trendFilters.Height = 40;
            _trendFilters.WrapContents = false;
            _trendFilters.Name = "_trendFilters";
            _trendPreset.DropDownStyle = ComboBoxStyle.DropDownList;
            _trendPreset.Items.AddRange(new object[]
            {
                "3 tháng gần đây", "6 tháng gần đây", "12 tháng gần đây", "Năm nay", "Tùy chọn"
            });
            _trendPreset.Width = 160;
            _trendPreset.Name = "_trendPreset";
            _trendFromLabel.AutoSize = true;
            _trendFromLabel.Margin = new Padding(12, 9, 6, 0);
            _trendFromLabel.Text = "Từ tháng";
            _trendFromLabel.Name = "_trendFromLabel";
            _trendFrom.Format = DateTimePickerFormat.Custom;
            _trendFrom.CustomFormat = "MM/yyyy";
            _trendFrom.ShowUpDown = true;
            _trendFrom.Width = 100;
            _trendFrom.Name = "_trendFrom";
            _trendToLabel.AutoSize = true;
            _trendToLabel.Margin = new Padding(8, 9, 6, 0);
            _trendToLabel.Text = "Đến tháng";
            _trendToLabel.Name = "_trendToLabel";
            _trendTo.Format = DateTimePickerFormat.Custom;
            _trendTo.CustomFormat = "MM/yyyy";
            _trendTo.ShowUpDown = true;
            _trendTo.Width = 100;
            _trendTo.Name = "_trendTo";
            _trendApply.Margin = new Padding(8, 3, 0, 0);
            _trendApply.Size = new Size(80, 30);
            _trendApply.Text = "Xem";
            _trendApply.UseVisualStyleBackColor = true;
            _trendApply.Name = "_trendApply";
            _trendChart.Dock = DockStyle.Fill;
            _trendChart.ChartAreas.Add(new ChartArea("TrendArea"));
            _trendChart.Legends.Add(new Legend("TrendLegend")
            {
                Docking = Docking.Bottom,
                Alignment = StringAlignment.Center
            });
            _trendChart.Name = "_trendChart";
            //
            // _historyPanel
            //
            _historyPanel.BackColor = Color.White;
            _historyPanel.Controls.Add(_historyGrid);
            _historyPanel.Controls.Add(_historyPager);
            _historyPanel.Controls.Add(_historyFilters);
            _historyPanel.Controls.Add(_historySubtitle);
            _historyPanel.Controls.Add(_historyTitle);
            _historyPanel.Dock = DockStyle.Fill;
            _historyPanel.Margin = new Padding(0, 8, 0, 0);
            _historyPanel.Padding = new Padding(12);
            _historyPanel.Name = "_historyPanel";
            _historyTitle.Dock = DockStyle.Top;
            _historyTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            _historyTitle.Height = 28;
            _historyTitle.Text = "Lịch sử phiếu / hóa đơn làm thay đổi lô";
            _historyTitle.Name = "_historyTitle";
            _historySubtitle.Dock = DockStyle.Top;
            _historySubtitle.Font = new Font("Segoe UI", 9F);
            _historySubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            _historySubtitle.Height = 22;
            _historySubtitle.Text = "Theo dõi từng chứng từ đã nhập, xuất, trả, hủy hoặc điều chỉnh số lượng của từng lô";
            _historySubtitle.Name = "_historySubtitle";
            _historyFilters.Controls.Add(_historySearchLabel);
            _historyFilters.Controls.Add(_historySearch);
            _historyFilters.Controls.Add(_historyFromLabel);
            _historyFilters.Controls.Add(_historyFrom);
            _historyFilters.Controls.Add(_historyToLabel);
            _historyFilters.Controls.Add(_historyTo);
            _historyFilters.Controls.Add(_historySearchButton);
            _historyFilters.Controls.Add(_historyClear);
            _historyFilters.Controls.Add(_historyExport);
            _historyFilters.Dock = DockStyle.Top;
            _historyFilters.Height = 40;
            _historyFilters.WrapContents = false;
            _historyFilters.Name = "_historyFilters";
            _historySearchLabel.AutoSize = true;
            _historySearchLabel.Margin = new Padding(0, 9, 6, 0);
            _historySearchLabel.Text = "Tìm";
            _historySearchLabel.Name = "_historySearchLabel";
            _historySearch.Margin = new Padding(0, 5, 8, 0);
            _historySearch.Width = 260;
            _historySearch.Name = "_historySearch";
            _historyFromLabel.AutoSize = true;
            _historyFromLabel.Margin = new Padding(0, 9, 6, 0);
            _historyFromLabel.Text = "Từ ngày";
            _historyFromLabel.Name = "_historyFromLabel";
            _historyFrom.Format = DateTimePickerFormat.Custom;
            _historyFrom.CustomFormat = "dd/MM/yyyy";
            _historyFrom.ShowCheckBox = true;
            _historyFrom.Checked = false;
            _historyFrom.Width = 130;
            _historyFrom.Name = "_historyFrom";
            _historyToLabel.AutoSize = true;
            _historyToLabel.Margin = new Padding(8, 9, 6, 0);
            _historyToLabel.Text = "Đến ngày";
            _historyToLabel.Name = "_historyToLabel";
            _historyTo.Format = DateTimePickerFormat.Custom;
            _historyTo.CustomFormat = "dd/MM/yyyy";
            _historyTo.ShowCheckBox = true;
            _historyTo.Checked = false;
            _historyTo.Width = 130;
            _historyTo.Name = "_historyTo";
            _historySearchButton.Margin = new Padding(8, 3, 4, 0);
            _historySearchButton.Size = new Size(80, 30);
            _historySearchButton.Text = "Lọc";
            _historySearchButton.UseVisualStyleBackColor = true;
            _historySearchButton.Name = "_historySearchButton";
            _historyClear.Margin = new Padding(4, 3, 4, 0);
            _historyClear.Size = new Size(80, 30);
            _historyClear.Text = "Xóa lọc";
            _historyClear.UseVisualStyleBackColor = true;
            _historyClear.Name = "_historyClear";
            _historyExport.Margin = new Padding(4, 3, 0, 0);
            _historyExport.Size = new Size(110, 30);
            _historyExport.Text = "Tùy chọn ▾";
            _historyExport.UseVisualStyleBackColor = true;
            _historyExport.Name = "_historyExport";
            _historyExportMenu.Items.AddRange(new ToolStripItem[]
            {
                _historyExportCsv,
                _historyExportExcel
            });
            _historyExportCsv.Text = "Xuất CSV";
            _historyExportExcel.Text = "Xuất Excel";
            _historyPager.Controls.Add(_historyNext);
            _historyPager.Controls.Add(_historyPageInfo);
            _historyPager.Controls.Add(_historyPrev);
            _historyPager.Dock = DockStyle.Bottom;
            _historyPager.FlowDirection = FlowDirection.RightToLeft;
            _historyPager.Height = 40;
            _historyPager.WrapContents = false;
            _historyPager.Name = "_historyPager";
            _historyNext.Size = new Size(80, 30);
            _historyNext.Text = "Sau ›";
            _historyNext.UseVisualStyleBackColor = true;
            _historyNext.Name = "_historyNext";
            _historyPageInfo.AutoSize = true;
            _historyPageInfo.Margin = new Padding(8, 9, 8, 0);
            _historyPageInfo.Text = "Trang 1/1";
            _historyPageInfo.Name = "_historyPageInfo";
            _historyPrev.Size = new Size(80, 30);
            _historyPrev.Text = "‹ Trước";
            _historyPrev.UseVisualStyleBackColor = true;
            _historyPrev.Name = "_historyPrev";
            _historyGrid.AllowUserToAddRows = false;
            _historyGrid.AllowUserToDeleteRows = false;
            _historyGrid.AllowUserToResizeRows = false;
            _historyGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _historyGrid.BackgroundColor = _grid.BackgroundColor;
            _historyGrid.BorderStyle = _grid.BorderStyle;
            _historyGrid.CellBorderStyle = _grid.CellBorderStyle;
            _historyGrid.ColumnHeadersDefaultCellStyle = _grid.ColumnHeadersDefaultCellStyle.Clone();
            _historyGrid.ColumnHeadersHeight = _grid.ColumnHeadersHeight;
            _historyGrid.Columns.AddRange(new DataGridViewColumn[]
            {
                _hBatch, _hTime, _hType, _hDocument, _hProductCode, _hProductName, _hStock, _hChange
            });
            _historyGrid.DefaultCellStyle = _grid.DefaultCellStyle.Clone();
            _historyGrid.Dock = DockStyle.Fill;
            _historyGrid.EnableHeadersVisualStyles = _grid.EnableHeadersVisualStyles;
            _historyGrid.GridColor = _grid.GridColor;
            _historyGrid.Margin = new Padding(0);
            _historyGrid.ReadOnly = true;
            _historyGrid.RowHeadersVisible = false;
            _historyGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _historyGrid.ShowCellToolTips = true;
            _historyGrid.Name = "_historyGrid";
            _hBatch.HeaderText = "Mã lô";
            _hBatch.Name = "_hBatch";
            _hBatch.FillWeight = 11F;
            _hTime.HeaderText = "Thời gian";
            _hTime.Name = "_hTime";
            _hTime.FillWeight = 13F;
            _hType.HeaderText = "Loại chứng từ";
            _hType.Name = "_hType";
            _hType.FillWeight = 13F;
            _hDocument.HeaderText = "Mã chứng từ";
            _hDocument.Name = "_hDocument";
            _hDocument.FillWeight = 12F;
            _hProductCode.HeaderText = "Mã SP";
            _hProductCode.Name = "_hProductCode";
            _hProductCode.FillWeight = 9F;
            _hProductName.HeaderText = "Tên sản phẩm";
            _hProductName.Name = "_hProductName";
            _hProductName.FillWeight = 24F;
            _hStock.HeaderText = "Tồn kho";
            _hStock.Name = "_hStock";
            _hStock.FillWeight = 9F;
            _hStock.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            _hChange.HeaderText = "Thay đổi";
            _hChange.Name = "_hChange";
            _hChange.FillWeight = 9F;
            _hChange.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //
            // _disposalPanel
            //
            _disposalPanel.BackColor = Color.White;
            _disposalPanel.Controls.Add(_disposalLayout);
            _disposalPanel.Dock = DockStyle.Fill;
            _disposalPanel.Margin = new Padding(0, 8, 0, 0);
            _disposalPanel.Padding = new Padding(12);
            _disposalPanel.Name = "_disposalPanel";
            //
            // _disposalLayout
            //
            _disposalLayout.ColumnCount = 1;
            _disposalLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _disposalLayout.Controls.Add(_disposalSummary, 0, 0);
            _disposalLayout.Controls.Add(_disposalProductPanel, 0, 1);
            _disposalLayout.Dock = DockStyle.Fill;
            _disposalLayout.Margin = new Padding(0);
            _disposalLayout.Padding = new Padding(0);
            _disposalLayout.RowCount = 2;
            _disposalLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            _disposalLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            _disposalLayout.Name = "_disposalLayout";
            //
            // _disposalChartPanel
            //
            _disposalChartPanel.Controls.Add(_disposalChart);
            _disposalChartPanel.Controls.Add(_disposalChartTitle);
            _disposalChartPanel.Dock = DockStyle.Fill;
            _disposalChartPanel.Padding = new Padding(0, 0, 6, 0);
            _disposalChartPanel.Name = "_disposalChartPanel";
            _disposalChartTitle.Dock = DockStyle.Top;
            _disposalChartTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            _disposalChartTitle.ForeColor = Color.FromArgb(15, 23, 42);
            _disposalChartTitle.Height = 30;
            _disposalChartTitle.Text = "Thiệt hại tiêu hủy theo lý do";
            _disposalChartTitle.Name = "_disposalChartTitle";
            _disposalChart.Dock = DockStyle.Fill;
            _disposalChart.Name = "_disposalChart";
            //
            // _disposalProductPanel
            //
            _disposalProductPanel.Controls.Add(_disposalGrid);
            _disposalProductPanel.Controls.Add(_disposalProductTitle);
            _disposalProductPanel.Dock = DockStyle.Fill;
            _disposalProductPanel.Padding = new Padding(6, 0, 0, 0);
            _disposalProductPanel.Name = "_disposalProductPanel";
            _disposalProductTitle.Dock = DockStyle.Top;
            _disposalProductTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            _disposalProductTitle.ForeColor = Color.FromArgb(15, 23, 42);
            _disposalProductTitle.Height = 30;
            _disposalProductTitle.Text = "Sản phẩm bị tiêu hủy nhiều nhất";
            _disposalProductTitle.Name = "_disposalProductTitle";
            //
            // _disposalGrid
            //
            _disposalGrid.AllowUserToAddRows = false;
            _disposalGrid.AllowUserToDeleteRows = false;
            _disposalGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _disposalGrid.BackgroundColor = Color.White;
            _disposalGrid.BorderStyle = BorderStyle.None;
            _disposalGrid.ColumnHeadersHeight = 40;
            _disposalGrid.Columns.AddRange(new DataGridViewColumn[]
            {
                _disposalProductColumn, _disposalQuantityColumn, _disposalLossColumn
            });
            _disposalGrid.Dock = DockStyle.Fill;
            _disposalGrid.Margin = new Padding(0);
            _disposalGrid.ReadOnly = true;
            _disposalGrid.RowHeadersVisible = false;
            _disposalGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _disposalGrid.Name = "_disposalGrid";
            _disposalProductColumn.HeaderText = "Sản phẩm";
            _disposalProductColumn.Name = "DisposalProduct";
            _disposalProductColumn.FillWeight = 55F;
            _disposalQuantityColumn.HeaderText = "SL hủy";
            _disposalQuantityColumn.Name = "DisposalQuantity";
            _disposalQuantityColumn.FillWeight = 15F;
            _disposalQuantityColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            _disposalLossColumn.HeaderText = "Thiệt hại";
            _disposalLossColumn.Name = "DisposalLoss";
            _disposalLossColumn.FillWeight = 30F;
            _disposalLossColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //
            // ucInventoryReport
            //
            AutoScroll = true;
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
            _disposalPanel.ResumeLayout(false);
            _disposalLayout.ResumeLayout(false);
            _disposalChartPanel.ResumeLayout(false);
            _disposalProductPanel.ResumeLayout(false);
            _productStockPanel.ResumeLayout(false);
            _productStockLegend.ResumeLayout(false);
            _productStockLegend.PerformLayout();
            _breakdownLayout.ResumeLayout(false);
            _categoryBreakdownPanel.ResumeLayout(false);
            _priceBreakdownPanel.ResumeLayout(false);
            _trendPanel.ResumeLayout(false);
            _trendFilters.ResumeLayout(false);
            _trendFilters.PerformLayout();
            _historyPanel.ResumeLayout(false);
            _historyFilters.ResumeLayout(false);
            _historyFilters.PerformLayout();
            _historyPager.ResumeLayout(false);
            _historyPager.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_importChart).EndInit();
            ((System.ComponentModel.ISupportInitialize)_categoryChart).EndInit();
            ((System.ComponentModel.ISupportInitialize)_grid).EndInit();
            ((System.ComponentModel.ISupportInitialize)_disposalChart).EndInit();
            ((System.ComponentModel.ISupportInitialize)_disposalGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)_productStockChart).EndInit();
            ((System.ComponentModel.ISupportInitialize)_categoryBreakdownGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)_priceBreakdownGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)_trendChart).EndInit();
            ((System.ComponentModel.ISupportInitialize)_historyGrid).EndInit();
            ResumeLayout(false);
        }

    }
}
