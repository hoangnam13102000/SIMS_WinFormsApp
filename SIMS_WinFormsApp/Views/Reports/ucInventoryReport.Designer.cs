using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using FontAwesome.Sharp;

namespace SIMS_WinFormsApp.Views.Reports
{
    partial class ucInventoryReport
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel _root;
        private Panel _header;
        private Panel _headerIconPanel;
        private IconPictureBox _headerIcon;
        private Label _title;
        private Label _subtitle;
        private FlowLayoutPanel _filters;
        private Label _fromLabel;
        private DateTimePicker _from;
        private Label _toLabel;
        private DateTimePicker _to;
        private ComboBox _category;
        private Button _apply;
        private Button _viewRdlc;
        private Button _exportOptions;
        private ContextMenuStrip _exportMenu;
        private ToolStripMenuItem _exportCategoryCsv;
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this._root = new System.Windows.Forms.TableLayoutPanel();
            this._header = new System.Windows.Forms.Panel();
            this._headerIconPanel = new System.Windows.Forms.Panel();
            this._headerIcon = new FontAwesome.Sharp.IconPictureBox();
            this._subtitle = new System.Windows.Forms.Label();
            this._title = new System.Windows.Forms.Label();
            this._viewRdlc = new System.Windows.Forms.Button();
            this._exportOptions = new System.Windows.Forms.Button();
            this._filters = new System.Windows.Forms.FlowLayoutPanel();
            this._fromLabel = new System.Windows.Forms.Label();
            this._from = new System.Windows.Forms.DateTimePicker();
            this._toLabel = new System.Windows.Forms.Label();
            this._to = new System.Windows.Forms.DateTimePicker();
            this._category = new System.Windows.Forms.ComboBox();
            this._apply = new System.Windows.Forms.Button();
            this._summary = new System.Windows.Forms.TableLayoutPanel();
            this._productCount = new System.Windows.Forms.Label();
            this._totalStock = new System.Windows.Forms.Label();
            this._stockValue = new System.Windows.Forms.Label();
            this._lowStock = new System.Windows.Forms.Label();
            this._sellValue = new System.Windows.Forms.Label();
            this._outOfStock = new System.Windows.Forms.Label();
            this._content = new System.Windows.Forms.TableLayoutPanel();
            this._chartPanel = new System.Windows.Forms.Panel();
            this._chartsLayout = new System.Windows.Forms.TableLayoutPanel();
            this._importChartPanel = new System.Windows.Forms.Panel();
            this._importChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this._chartTitle = new System.Windows.Forms.Label();
            this._categoryChartPanel = new System.Windows.Forms.Panel();
            this._categoryChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this._categoryChartTitle = new System.Windows.Forms.Label();
            this._disposalChartPanel = new System.Windows.Forms.Panel();
            this._disposalChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this._disposalChartTitle = new System.Windows.Forms.Label();
            this._grid = new System.Windows.Forms.DataGridView();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._statusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._disposalPanel = new System.Windows.Forms.Panel();
            this._disposalLayout = new System.Windows.Forms.TableLayoutPanel();
            this._disposalSummary = new System.Windows.Forms.Label();
            this._disposalProductPanel = new System.Windows.Forms.Panel();
            this._disposalGrid = new System.Windows.Forms.DataGridView();
            this.DisposalProduct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DisposalQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DisposalLoss = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._disposalProductTitle = new System.Windows.Forms.Label();
            this._productStockPanel = new System.Windows.Forms.Panel();
            this._productStockChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this._productStockLegend = new System.Windows.Forms.FlowLayoutPanel();
            this._legendNormalColor = new System.Windows.Forms.Panel();
            this._legendNormal = new System.Windows.Forms.Label();
            this._legendWarningColor = new System.Windows.Forms.Panel();
            this._legendWarning = new System.Windows.Forms.Label();
            this._legendExpiredColor = new System.Windows.Forms.Panel();
            this._legendExpired = new System.Windows.Forms.Label();
            this._legendOutColor = new System.Windows.Forms.Panel();
            this._legendOut = new System.Windows.Forms.Label();
            this._productStockTitle = new System.Windows.Forms.Label();
            this._breakdownLayout = new System.Windows.Forms.TableLayoutPanel();
            this._categoryBreakdownPanel = new System.Windows.Forms.Panel();
            this._categoryBreakdownGrid = new System.Windows.Forms.DataGridView();
            this._cbName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._cbProducts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._cbQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._cbValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._cbRatio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._categoryBreakdownTitle = new System.Windows.Forms.Label();
            this._priceBreakdownPanel = new System.Windows.Forms.Panel();
            this._priceBreakdownGrid = new System.Windows.Forms.DataGridView();
            this._pbRange = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._pbProducts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._pbQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._pbValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._pbRatio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._priceBreakdownTitle = new System.Windows.Forms.Label();
            this._trendPanel = new System.Windows.Forms.Panel();
            this._trendChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this._trendFilters = new System.Windows.Forms.FlowLayoutPanel();
            this._trendPreset = new System.Windows.Forms.ComboBox();
            this._trendFromLabel = new System.Windows.Forms.Label();
            this._trendFrom = new System.Windows.Forms.DateTimePicker();
            this._trendToLabel = new System.Windows.Forms.Label();
            this._trendTo = new System.Windows.Forms.DateTimePicker();
            this._trendApply = new System.Windows.Forms.Button();
            this._trendTitle = new System.Windows.Forms.Label();
            this._historyPanel = new System.Windows.Forms.Panel();
            this._historyGrid = new System.Windows.Forms.DataGridView();
            this._hBatch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._hTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._hType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._hDocument = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._hProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._hProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._hStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._hChange = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._historyPager = new System.Windows.Forms.FlowLayoutPanel();
            this._historyNext = new System.Windows.Forms.Button();
            this._historyPageInfo = new System.Windows.Forms.Label();
            this._historyPrev = new System.Windows.Forms.Button();
            this._historyFilters = new System.Windows.Forms.FlowLayoutPanel();
            this._historySearchLabel = new System.Windows.Forms.Label();
            this._historySearch = new System.Windows.Forms.TextBox();
            this._historyFromLabel = new System.Windows.Forms.Label();
            this._historyFrom = new System.Windows.Forms.DateTimePicker();
            this._historyToLabel = new System.Windows.Forms.Label();
            this._historyTo = new System.Windows.Forms.DateTimePicker();
            this._historySearchButton = new System.Windows.Forms.Button();
            this._historyClear = new System.Windows.Forms.Button();
            this._historyExport = new System.Windows.Forms.Button();
            this._historySubtitle = new System.Windows.Forms.Label();
            this._historyTitle = new System.Windows.Forms.Label();
            this._exportMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._exportCategoryCsv = new System.Windows.Forms.ToolStripMenuItem();
            this._historyExportMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._historyExportCsv = new System.Windows.Forms.ToolStripMenuItem();
            this._root.SuspendLayout();
            this._header.SuspendLayout();
            this._headerIconPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._headerIcon)).BeginInit();
            this._filters.SuspendLayout();
            this._summary.SuspendLayout();
            this._content.SuspendLayout();
            this._chartPanel.SuspendLayout();
            this._chartsLayout.SuspendLayout();
            this._importChartPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._importChart)).BeginInit();
            this._categoryChartPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._categoryChart)).BeginInit();
            this._disposalChartPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._disposalChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._grid)).BeginInit();
            this._disposalPanel.SuspendLayout();
            this._disposalLayout.SuspendLayout();
            this._disposalProductPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._disposalGrid)).BeginInit();
            this._productStockPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._productStockChart)).BeginInit();
            this._productStockLegend.SuspendLayout();
            this._breakdownLayout.SuspendLayout();
            this._categoryBreakdownPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._categoryBreakdownGrid)).BeginInit();
            this._priceBreakdownPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._priceBreakdownGrid)).BeginInit();
            this._trendPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._trendChart)).BeginInit();
            this._trendFilters.SuspendLayout();
            this._historyPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._historyGrid)).BeginInit();
            this._historyPager.SuspendLayout();
            this._historyFilters.SuspendLayout();
            this._exportMenu.SuspendLayout();
            this._historyExportMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // _root
            // 
            this._root.AutoSize = true;
            this._root.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._root.ColumnCount = 1;
            this._root.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._root.Controls.Add(this._header, 0, 0);
            this._root.Controls.Add(this._filters, 0, 1);
            this._root.Controls.Add(this._summary, 0, 2);
            this._root.Controls.Add(this._content, 0, 3);
            this._root.Controls.Add(this._productStockPanel, 0, 4);
            this._root.Controls.Add(this._breakdownLayout, 0, 5);
            this._root.Controls.Add(this._trendPanel, 0, 6);
            this._root.Controls.Add(this._historyPanel, 0, 7);
            this._root.Dock = System.Windows.Forms.DockStyle.Top;
            this._root.Location = new System.Drawing.Point(0, 0);
            this._root.Name = "_root";
            this._root.Padding = new System.Windows.Forms.Padding(24);
            this._root.RowCount = 8;
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 640F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 320F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 440F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 600F));
            this._root.Size = new System.Drawing.Size(998, 2680);
            this._root.TabIndex = 0;
            // 
            // _header
            // 
            this._header.BackColor = System.Drawing.Color.White;
            this._header.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._header.Controls.Add(this._headerIconPanel);
            this._header.Controls.Add(this._subtitle);
            this._header.Controls.Add(this._title);
            this._header.Controls.Add(this._viewRdlc);
            this._header.Controls.Add(this._exportOptions);
            this._header.Dock = System.Windows.Forms.DockStyle.Fill;
            this._header.Location = new System.Drawing.Point(27, 27);
            this._header.Name = "_header";
            this._header.Size = new System.Drawing.Size(944, 82);
            this._header.TabIndex = 0;
            // 
            // _headerIconPanel
            // 
            this._headerIconPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(240)))), ((int)(((byte)(254)))));
            this._headerIconPanel.Controls.Add(this._headerIcon);
            this._headerIconPanel.Location = new System.Drawing.Point(16, 18);
            this._headerIconPanel.Name = "_headerIconPanel";
            this._headerIconPanel.Size = new System.Drawing.Size(50, 50);
            this._headerIconPanel.TabIndex = 0;
            // 
            // _headerIcon
            // 
            this._headerIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(240)))), ((int)(((byte)(254)))));
            this._headerIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._headerIcon.IconChar = FontAwesome.Sharp.IconChar.ChartLine;
            this._headerIcon.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._headerIcon.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._headerIcon.IconSize = 24;
            this._headerIcon.Location = new System.Drawing.Point(13, 13);
            this._headerIcon.Name = "_headerIcon";
            this._headerIcon.Size = new System.Drawing.Size(24, 24);
            this._headerIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._headerIcon.TabIndex = 0;
            this._headerIcon.TabStop = false;
            // 
            // _subtitle
            // 
            this._subtitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._subtitle.AutoEllipsis = true;
            this._subtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._subtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._subtitle.Location = new System.Drawing.Point(80, 52);
            this._subtitle.Name = "_subtitle";
            this._subtitle.Size = new System.Drawing.Size(1477, 20);
            this._subtitle.TabIndex = 1;
            this._subtitle.Text = "Thống kê số lượng và giá trị tồn kho theo danh mục, theo khoảng giá bán, xu hướng" +
    " theo tháng và lịch sử từng lô";
            this._subtitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._subtitle.UseMnemonic = false;
            // 
            // _title
            // 
            this._title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._title.AutoEllipsis = true;
            this._title.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this._title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this._title.Location = new System.Drawing.Point(80, 12);
            this._title.Name = "_title";
            this._title.Size = new System.Drawing.Size(1477, 36);
            this._title.TabIndex = 2;
            this._title.Text = "Báo cáo hàng tồn kho";
            this._title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._title.UseMnemonic = false;
            // _viewRdlc
            this._viewRdlc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._viewRdlc.BackColor = System.Drawing.Color.White;
            this._viewRdlc.Cursor = System.Windows.Forms.Cursors.Hand;
            this._viewRdlc.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._viewRdlc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._viewRdlc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._viewRdlc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._viewRdlc.Location = new System.Drawing.Point(1430, 25);
            this._viewRdlc.Name = "_viewRdlc";
            this._viewRdlc.Size = new System.Drawing.Size(154, 38);
            this._viewRdlc.TabIndex = 4;
            this._viewRdlc.Text = "Xem báo cáo (RDLC)";
            this._viewRdlc.UseVisualStyleBackColor = false;
            // 
            // _exportOptions
            // 
            this._exportOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._exportOptions.BackColor = System.Drawing.Color.White;
            this._exportOptions.Cursor = System.Windows.Forms.Cursors.Hand;
            this._exportOptions.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._exportOptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._exportOptions.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._exportOptions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._exportOptions.Location = new System.Drawing.Point(1744, 25);
            this._exportOptions.Name = "_exportOptions";
            this._exportOptions.Size = new System.Drawing.Size(110, 38);
            this._exportOptions.TabIndex = 4;
            this._exportOptions.Text = "Tùy chọn ▾";
            this._exportOptions.UseVisualStyleBackColor = false;
            // 
            // _filters
            // 
            this._filters.Controls.Add(this._fromLabel);
            this._filters.Controls.Add(this._from);
            this._filters.Controls.Add(this._toLabel);
            this._filters.Controls.Add(this._to);
            this._filters.Controls.Add(this._category);
            this._filters.Controls.Add(this._apply);
            this._filters.Dock = System.Windows.Forms.DockStyle.Fill;
            this._filters.Location = new System.Drawing.Point(27, 115);
            this._filters.Name = "_filters";
            this._filters.Size = new System.Drawing.Size(944, 42);
            this._filters.TabIndex = 1;
            this._filters.WrapContents = false;
            // 
            // _fromLabel
            // 
            this._fromLabel.AutoSize = true;
            this._fromLabel.Location = new System.Drawing.Point(0, 9);
            this._fromLabel.Margin = new System.Windows.Forms.Padding(0, 9, 6, 0);
            this._fromLabel.Name = "_fromLabel";
            this._fromLabel.Size = new System.Drawing.Size(65, 20);
            this._fromLabel.TabIndex = 0;
            this._fromLabel.Text = "Từ ngày";
            // 
            // _from
            // 
            this._from.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this._from.Location = new System.Drawing.Point(71, 4);
            this._from.Margin = new System.Windows.Forms.Padding(0, 4, 8, 0);
            this._from.Name = "_from";
            this._from.Size = new System.Drawing.Size(120, 26);
            this._from.TabIndex = 1;
            // 
            // _toLabel
            // 
            this._toLabel.AutoSize = true;
            this._toLabel.Location = new System.Drawing.Point(207, 9);
            this._toLabel.Margin = new System.Windows.Forms.Padding(8, 9, 6, 0);
            this._toLabel.Name = "_toLabel";
            this._toLabel.Size = new System.Drawing.Size(77, 20);
            this._toLabel.TabIndex = 2;
            this._toLabel.Text = "Đến ngày";
            // 
            // _to
            // 
            this._to.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this._to.Location = new System.Drawing.Point(290, 4);
            this._to.Margin = new System.Windows.Forms.Padding(0, 4, 8, 0);
            this._to.Name = "_to";
            this._to.Size = new System.Drawing.Size(120, 26);
            this._to.TabIndex = 3;
            // 
            // _category
            // 
            this._category.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._category.Location = new System.Drawing.Point(422, 4);
            this._category.Margin = new System.Windows.Forms.Padding(4, 4, 8, 0);
            this._category.Name = "_category";
            this._category.Size = new System.Drawing.Size(190, 28);
            this._category.TabIndex = 4;
            // 
            // _apply
            // 
            this._apply.Location = new System.Drawing.Point(624, 3);
            this._apply.Margin = new System.Windows.Forms.Padding(4, 3, 4, 0);
            this._apply.Name = "_apply";
            this._apply.Size = new System.Drawing.Size(90, 30);
            this._apply.TabIndex = 5;
            this._apply.Text = "Áp dụng";
            this._apply.UseVisualStyleBackColor = true;
            // 
            // _summary
            // 
            this._summary.ColumnCount = 6;
            this._summary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66F));
            this._summary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66F));
            this._summary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66F));
            this._summary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66F));
            this._summary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66F));
            this._summary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66F));
            this._summary.Controls.Add(this._productCount, 0, 0);
            this._summary.Controls.Add(this._totalStock, 1, 0);
            this._summary.Controls.Add(this._stockValue, 2, 0);
            this._summary.Controls.Add(this._lowStock, 3, 0);
            this._summary.Controls.Add(this._sellValue, 4, 0);
            this._summary.Controls.Add(this._outOfStock, 5, 0);
            this._summary.Dock = System.Windows.Forms.DockStyle.Fill;
            this._summary.Location = new System.Drawing.Point(27, 163);
            this._summary.Name = "_summary";
            this._summary.Size = new System.Drawing.Size(944, 90);
            this._summary.TabIndex = 2;
            // 
            // _productCount
            // 
            this._productCount.BackColor = System.Drawing.Color.White;
            this._productCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this._productCount.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._productCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(110)))));
            this._productCount.Location = new System.Drawing.Point(0, 0);
            this._productCount.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this._productCount.Name = "_productCount";
            this._productCount.Padding = new System.Windows.Forms.Padding(14);
            this._productCount.Size = new System.Drawing.Size(147, 90);
            this._productCount.TabIndex = 0;
            this._productCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _totalStock
            // 
            this._totalStock.BackColor = System.Drawing.Color.White;
            this._totalStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this._totalStock.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._totalStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(110)))));
            this._totalStock.Location = new System.Drawing.Point(157, 0);
            this._totalStock.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this._totalStock.Name = "_totalStock";
            this._totalStock.Padding = new System.Windows.Forms.Padding(14);
            this._totalStock.Size = new System.Drawing.Size(147, 90);
            this._totalStock.TabIndex = 1;
            this._totalStock.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _stockValue
            // 
            this._stockValue.BackColor = System.Drawing.Color.White;
            this._stockValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this._stockValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._stockValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(110)))));
            this._stockValue.Location = new System.Drawing.Point(314, 0);
            this._stockValue.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this._stockValue.Name = "_stockValue";
            this._stockValue.Padding = new System.Windows.Forms.Padding(14);
            this._stockValue.Size = new System.Drawing.Size(147, 90);
            this._stockValue.TabIndex = 2;
            this._stockValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lowStock
            // 
            this._lowStock.BackColor = System.Drawing.Color.White;
            this._lowStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lowStock.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._lowStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(110)))));
            this._lowStock.Location = new System.Drawing.Point(471, 0);
            this._lowStock.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this._lowStock.Name = "_lowStock";
            this._lowStock.Padding = new System.Windows.Forms.Padding(14);
            this._lowStock.Size = new System.Drawing.Size(147, 90);
            this._lowStock.TabIndex = 3;
            this._lowStock.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _sellValue
            // 
            this._sellValue.BackColor = System.Drawing.Color.White;
            this._sellValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this._sellValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._sellValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(110)))));
            this._sellValue.Location = new System.Drawing.Point(628, 0);
            this._sellValue.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this._sellValue.Name = "_sellValue";
            this._sellValue.Padding = new System.Windows.Forms.Padding(14);
            this._sellValue.Size = new System.Drawing.Size(147, 90);
            this._sellValue.TabIndex = 4;
            this._sellValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _outOfStock
            // 
            this._outOfStock.BackColor = System.Drawing.Color.White;
            this._outOfStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this._outOfStock.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._outOfStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(110)))));
            this._outOfStock.Location = new System.Drawing.Point(785, 0);
            this._outOfStock.Margin = new System.Windows.Forms.Padding(0);
            this._outOfStock.Name = "_outOfStock";
            this._outOfStock.Padding = new System.Windows.Forms.Padding(14);
            this._outOfStock.Size = new System.Drawing.Size(159, 90);
            this._outOfStock.TabIndex = 5;
            this._outOfStock.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _content
            // 
            this._content.ColumnCount = 1;
            this._content.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._content.Controls.Add(this._chartPanel, 0, 0);
            this._content.Controls.Add(this._grid, 0, 1);
            this._content.Controls.Add(this._disposalPanel, 0, 2);
            this._content.Dock = System.Windows.Forms.DockStyle.Fill;
            this._content.Location = new System.Drawing.Point(27, 259);
            this._content.Name = "_content";
            this._content.RowCount = 3;
            this._content.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this._content.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this._content.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this._content.Size = new System.Drawing.Size(944, 634);
            this._content.TabIndex = 3;
            // 
            // _chartPanel
            // 
            this._chartPanel.BackColor = System.Drawing.Color.White;
            this._chartPanel.Controls.Add(this._chartsLayout);
            this._chartPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._chartPanel.Location = new System.Drawing.Point(0, 8);
            this._chartPanel.Margin = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this._chartPanel.Name = "_chartPanel";
            this._chartPanel.Padding = new System.Windows.Forms.Padding(12);
            this._chartPanel.Size = new System.Drawing.Size(944, 237);
            this._chartPanel.TabIndex = 0;
            // 
            // _chartsLayout
            // 
            this._chartsLayout.ColumnCount = 3;
            this._chartsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this._chartsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this._chartsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this._chartsLayout.Controls.Add(this._importChartPanel, 0, 0);
            this._chartsLayout.Controls.Add(this._categoryChartPanel, 1, 0);
            this._chartsLayout.Controls.Add(this._disposalChartPanel, 2, 0);
            this._chartsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._chartsLayout.Location = new System.Drawing.Point(12, 12);
            this._chartsLayout.Margin = new System.Windows.Forms.Padding(0);
            this._chartsLayout.Name = "_chartsLayout";
            this._chartsLayout.RowCount = 1;
            this._chartsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._chartsLayout.Size = new System.Drawing.Size(920, 213);
            this._chartsLayout.TabIndex = 0;
            // 
            // _importChartPanel
            // 
            this._importChartPanel.Controls.Add(this._importChart);
            this._importChartPanel.Controls.Add(this._chartTitle);
            this._importChartPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._importChartPanel.Location = new System.Drawing.Point(3, 3);
            this._importChartPanel.Name = "_importChartPanel";
            this._importChartPanel.Padding = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this._importChartPanel.Size = new System.Drawing.Size(306, 207);
            this._importChartPanel.TabIndex = 0;
            // 
            // _importChart
            // 
            this._importChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this._importChart.Location = new System.Drawing.Point(0, 30);
            this._importChart.MinimumSize = new System.Drawing.Size(1, 1);
            this._importChart.Name = "_importChart";
            this._importChart.Size = new System.Drawing.Size(300, 177);
            this._importChart.TabIndex = 0;
            // 
            // _chartTitle
            // 
            this._chartTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._chartTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._chartTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._chartTitle.Location = new System.Drawing.Point(0, 0);
            this._chartTitle.Name = "_chartTitle";
            this._chartTitle.Size = new System.Drawing.Size(300, 30);
            this._chartTitle.TabIndex = 1;
            this._chartTitle.Text = "Số lượng nhập theo ngày";
            // 
            // _categoryChartPanel
            // 
            this._categoryChartPanel.Controls.Add(this._categoryChart);
            this._categoryChartPanel.Controls.Add(this._categoryChartTitle);
            this._categoryChartPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._categoryChartPanel.Location = new System.Drawing.Point(315, 3);
            this._categoryChartPanel.Name = "_categoryChartPanel";
            this._categoryChartPanel.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._categoryChartPanel.Size = new System.Drawing.Size(297, 207);
            this._categoryChartPanel.TabIndex = 1;
            // 
            // _categoryChart
            // 
            this._categoryChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this._categoryChart.Location = new System.Drawing.Point(6, 30);
            this._categoryChart.MinimumSize = new System.Drawing.Size(1, 1);
            this._categoryChart.Name = "_categoryChart";
            this._categoryChart.Size = new System.Drawing.Size(285, 177);
            this._categoryChart.TabIndex = 0;
            // 
            // _categoryChartTitle
            // 
            this._categoryChartTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._categoryChartTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._categoryChartTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._categoryChartTitle.Location = new System.Drawing.Point(6, 0);
            this._categoryChartTitle.Name = "_categoryChartTitle";
            this._categoryChartTitle.Size = new System.Drawing.Size(285, 30);
            this._categoryChartTitle.TabIndex = 1;
            this._categoryChartTitle.Text = "Giá trị tồn theo danh mục";
            // 
            // _disposalChartPanel
            // 
            this._disposalChartPanel.Controls.Add(this._disposalChart);
            this._disposalChartPanel.Controls.Add(this._disposalChartTitle);
            this._disposalChartPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._disposalChartPanel.Location = new System.Drawing.Point(618, 3);
            this._disposalChartPanel.Name = "_disposalChartPanel";
            this._disposalChartPanel.Padding = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this._disposalChartPanel.Size = new System.Drawing.Size(299, 207);
            this._disposalChartPanel.TabIndex = 2;
            // 
            // _disposalChart
            // 
            this._disposalChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this._disposalChart.Location = new System.Drawing.Point(0, 30);
            this._disposalChart.MinimumSize = new System.Drawing.Size(1, 1);
            this._disposalChart.Name = "_disposalChart";
            this._disposalChart.Size = new System.Drawing.Size(293, 177);
            this._disposalChart.TabIndex = 0;
            // 
            // _disposalChartTitle
            // 
            this._disposalChartTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._disposalChartTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._disposalChartTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._disposalChartTitle.Location = new System.Drawing.Point(0, 0);
            this._disposalChartTitle.Name = "_disposalChartTitle";
            this._disposalChartTitle.Size = new System.Drawing.Size(293, 30);
            this._disposalChartTitle.TabIndex = 1;
            this._disposalChartTitle.Text = "Thiệt hại tiêu hủy theo lý do";
            // 
            // _grid
            // 
            this._grid.AllowUserToAddRows = false;
            this._grid.AllowUserToDeleteRows = false;
            this._grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._grid.BackgroundColor = System.Drawing.Color.White;
            this._grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._grid.ColumnHeadersHeight = 40;
            this._grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Code,
            this.ProductNameColumn,
            this.Category,
            this.Quantity,
            this.UnitPrice,
            this.StockValue,
            this._statusColumn});
            this._grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grid.Location = new System.Drawing.Point(0, 261);
            this._grid.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this._grid.Name = "_grid";
            this._grid.ReadOnly = true;
            this._grid.RowHeadersVisible = false;
            this._grid.RowHeadersWidth = 62;
            this._grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._grid.Size = new System.Drawing.Size(944, 182);
            this._grid.TabIndex = 1;
            // 
            // Code
            // 
            this.Code.FillWeight = 22F;
            this.Code.HeaderText = "Mã hàng";
            this.Code.MinimumWidth = 8;
            this.Code.Name = "Code";
            this.Code.ReadOnly = true;
            // 
            // ProductNameColumn
            // 
            this.ProductNameColumn.FillWeight = 38F;
            this.ProductNameColumn.HeaderText = "Sản phẩm";
            this.ProductNameColumn.MinimumWidth = 8;
            this.ProductNameColumn.Name = "ProductNameColumn";
            this.ProductNameColumn.ReadOnly = true;
            // 
            // Category
            // 
            this.Category.FillWeight = 28F;
            this.Category.HeaderText = "Danh mục";
            this.Category.MinimumWidth = 8;
            this.Category.Name = "Category";
            this.Category.ReadOnly = true;
            // 
            // Quantity
            // 
            this.Quantity.FillWeight = 16F;
            this.Quantity.HeaderText = "Tồn";
            this.Quantity.MinimumWidth = 8;
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            // 
            // UnitPrice
            // 
            this.UnitPrice.FillWeight = 25F;
            this.UnitPrice.HeaderText = "Giá nhập";
            this.UnitPrice.MinimumWidth = 8;
            this.UnitPrice.Name = "UnitPrice";
            this.UnitPrice.ReadOnly = true;
            // 
            // StockValue
            // 
            this.StockValue.FillWeight = 28F;
            this.StockValue.HeaderText = "Giá trị tồn";
            this.StockValue.MinimumWidth = 8;
            this.StockValue.Name = "StockValue";
            this.StockValue.ReadOnly = true;
            // 
            // _statusColumn
            // 
            this._statusColumn.FillWeight = 27F;
            this._statusColumn.HeaderText = "Tình trạng";
            this._statusColumn.MinimumWidth = 8;
            this._statusColumn.Name = "_statusColumn";
            this._statusColumn.ReadOnly = true;
            // 
            // _disposalPanel
            // 
            this._disposalPanel.BackColor = System.Drawing.Color.White;
            this._disposalPanel.Controls.Add(this._disposalLayout);
            this._disposalPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._disposalPanel.Location = new System.Drawing.Point(0, 451);
            this._disposalPanel.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this._disposalPanel.Name = "_disposalPanel";
            this._disposalPanel.Padding = new System.Windows.Forms.Padding(12);
            this._disposalPanel.Size = new System.Drawing.Size(944, 183);
            this._disposalPanel.TabIndex = 2;
            // 
            // _disposalLayout
            // 
            this._disposalLayout.ColumnCount = 1;
            this._disposalLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._disposalLayout.Controls.Add(this._disposalSummary, 0, 0);
            this._disposalLayout.Controls.Add(this._disposalProductPanel, 0, 1);
            this._disposalLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._disposalLayout.Location = new System.Drawing.Point(12, 12);
            this._disposalLayout.Margin = new System.Windows.Forms.Padding(0);
            this._disposalLayout.Name = "_disposalLayout";
            this._disposalLayout.RowCount = 2;
            this._disposalLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this._disposalLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._disposalLayout.Size = new System.Drawing.Size(920, 159);
            this._disposalLayout.TabIndex = 0;
            // 
            // _disposalSummary
            // 
            this._disposalSummary.BackColor = System.Drawing.Color.White;
            this._disposalSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this._disposalSummary.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._disposalSummary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(110)))));
            this._disposalSummary.Location = new System.Drawing.Point(0, 0);
            this._disposalSummary.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this._disposalSummary.Name = "_disposalSummary";
            this._disposalSummary.Padding = new System.Windows.Forms.Padding(14);
            this._disposalSummary.Size = new System.Drawing.Size(910, 48);
            this._disposalSummary.TabIndex = 0;
            this._disposalSummary.Text = "THIỆT HẠI TIÊU HỦY\r\n0 đ";
            this._disposalSummary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _disposalProductPanel
            // 
            this._disposalProductPanel.Controls.Add(this._disposalGrid);
            this._disposalProductPanel.Controls.Add(this._disposalProductTitle);
            this._disposalProductPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._disposalProductPanel.Location = new System.Drawing.Point(3, 51);
            this._disposalProductPanel.Name = "_disposalProductPanel";
            this._disposalProductPanel.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this._disposalProductPanel.Size = new System.Drawing.Size(914, 105);
            this._disposalProductPanel.TabIndex = 1;
            // 
            // _disposalGrid
            // 
            this._disposalGrid.AllowUserToAddRows = false;
            this._disposalGrid.AllowUserToDeleteRows = false;
            this._disposalGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._disposalGrid.BackgroundColor = System.Drawing.Color.White;
            this._disposalGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._disposalGrid.ColumnHeadersHeight = 40;
            this._disposalGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DisposalProduct,
            this.DisposalQuantity,
            this.DisposalLoss});
            this._disposalGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._disposalGrid.Location = new System.Drawing.Point(6, 30);
            this._disposalGrid.Margin = new System.Windows.Forms.Padding(0);
            this._disposalGrid.Name = "_disposalGrid";
            this._disposalGrid.ReadOnly = true;
            this._disposalGrid.RowHeadersVisible = false;
            this._disposalGrid.RowHeadersWidth = 62;
            this._disposalGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._disposalGrid.Size = new System.Drawing.Size(908, 75);
            this._disposalGrid.TabIndex = 0;
            // 
            // DisposalProduct
            // 
            this.DisposalProduct.FillWeight = 55F;
            this.DisposalProduct.HeaderText = "Sản phẩm";
            this.DisposalProduct.MinimumWidth = 8;
            this.DisposalProduct.Name = "DisposalProduct";
            this.DisposalProduct.ReadOnly = true;
            // 
            // DisposalQuantity
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DisposalQuantity.DefaultCellStyle = dataGridViewCellStyle1;
            this.DisposalQuantity.FillWeight = 15F;
            this.DisposalQuantity.HeaderText = "SL hủy";
            this.DisposalQuantity.MinimumWidth = 8;
            this.DisposalQuantity.Name = "DisposalQuantity";
            this.DisposalQuantity.ReadOnly = true;
            // 
            // DisposalLoss
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DisposalLoss.DefaultCellStyle = dataGridViewCellStyle2;
            this.DisposalLoss.FillWeight = 30F;
            this.DisposalLoss.HeaderText = "Thiệt hại";
            this.DisposalLoss.MinimumWidth = 8;
            this.DisposalLoss.Name = "DisposalLoss";
            this.DisposalLoss.ReadOnly = true;
            // 
            // _disposalProductTitle
            // 
            this._disposalProductTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._disposalProductTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._disposalProductTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._disposalProductTitle.Location = new System.Drawing.Point(6, 0);
            this._disposalProductTitle.Name = "_disposalProductTitle";
            this._disposalProductTitle.Size = new System.Drawing.Size(908, 30);
            this._disposalProductTitle.TabIndex = 1;
            this._disposalProductTitle.Text = "Sản phẩm bị tiêu hủy nhiều nhất";
            // 
            // _productStockPanel
            // 
            this._productStockPanel.BackColor = System.Drawing.Color.White;
            this._productStockPanel.Controls.Add(this._productStockChart);
            this._productStockPanel.Controls.Add(this._productStockLegend);
            this._productStockPanel.Controls.Add(this._productStockTitle);
            this._productStockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._productStockPanel.Location = new System.Drawing.Point(24, 904);
            this._productStockPanel.Margin = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this._productStockPanel.Name = "_productStockPanel";
            this._productStockPanel.Padding = new System.Windows.Forms.Padding(12);
            this._productStockPanel.Size = new System.Drawing.Size(950, 384);
            this._productStockPanel.TabIndex = 4;
            // 
            // _productStockChart
            // 
            chartArea1.Name = "ProductStockArea";
            this._productStockChart.ChartAreas.Add(chartArea1);
            this._productStockChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this._productStockChart.Location = new System.Drawing.Point(12, 40);
            this._productStockChart.MinimumSize = new System.Drawing.Size(1, 1);
            this._productStockChart.Name = "_productStockChart";
            series1.ChartArea = "ProductStockArea";
            series1.Name = "Tồn kho";
            this._productStockChart.Series.Add(series1);
            this._productStockChart.Size = new System.Drawing.Size(926, 304);
            this._productStockChart.TabIndex = 0;
            // 
            // _productStockLegend
            // 
            this._productStockLegend.Controls.Add(this._legendNormalColor);
            this._productStockLegend.Controls.Add(this._legendNormal);
            this._productStockLegend.Controls.Add(this._legendWarningColor);
            this._productStockLegend.Controls.Add(this._legendWarning);
            this._productStockLegend.Controls.Add(this._legendExpiredColor);
            this._productStockLegend.Controls.Add(this._legendExpired);
            this._productStockLegend.Controls.Add(this._legendOutColor);
            this._productStockLegend.Controls.Add(this._legendOut);
            this._productStockLegend.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._productStockLegend.Location = new System.Drawing.Point(12, 344);
            this._productStockLegend.Name = "_productStockLegend";
            this._productStockLegend.Size = new System.Drawing.Size(926, 28);
            this._productStockLegend.TabIndex = 1;
            this._productStockLegend.WrapContents = false;
            // 
            // _legendNormalColor
            // 
            this._legendNormalColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this._legendNormalColor.Location = new System.Drawing.Point(0, 8);
            this._legendNormalColor.Margin = new System.Windows.Forms.Padding(0, 8, 4, 0);
            this._legendNormalColor.Name = "_legendNormalColor";
            this._legendNormalColor.Size = new System.Drawing.Size(12, 12);
            this._legendNormalColor.TabIndex = 0;
            // 
            // _legendNormal
            // 
            this._legendNormal.AutoSize = true;
            this._legendNormal.Location = new System.Drawing.Point(16, 6);
            this._legendNormal.Margin = new System.Windows.Forms.Padding(0, 6, 16, 0);
            this._legendNormal.Name = "_legendNormal";
            this._legendNormal.Size = new System.Drawing.Size(170, 20);
            this._legendNormal.TabIndex = 1;
            this._legendNormal.Text = "Còn hàng, bình thường";
            // 
            // _legendWarningColor
            // 
            this._legendWarningColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this._legendWarningColor.Location = new System.Drawing.Point(202, 8);
            this._legendWarningColor.Margin = new System.Windows.Forms.Padding(0, 8, 4, 0);
            this._legendWarningColor.Name = "_legendWarningColor";
            this._legendWarningColor.Size = new System.Drawing.Size(12, 12);
            this._legendWarningColor.TabIndex = 2;
            // 
            // _legendWarning
            // 
            this._legendWarning.AutoSize = true;
            this._legendWarning.Location = new System.Drawing.Point(218, 6);
            this._legendWarning.Margin = new System.Windows.Forms.Padding(0, 6, 16, 0);
            this._legendWarning.Name = "_legendWarning";
            this._legendWarning.Size = new System.Drawing.Size(225, 20);
            this._legendWarning.TabIndex = 3;
            this._legendWarning.Text = "Sắp hết hạn / dưới tồn tối thiểu";
            // 
            // _legendExpiredColor
            // 
            this._legendExpiredColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this._legendExpiredColor.Location = new System.Drawing.Point(459, 8);
            this._legendExpiredColor.Margin = new System.Windows.Forms.Padding(0, 8, 4, 0);
            this._legendExpiredColor.Name = "_legendExpiredColor";
            this._legendExpiredColor.Size = new System.Drawing.Size(12, 12);
            this._legendExpiredColor.TabIndex = 4;
            // 
            // _legendExpired
            // 
            this._legendExpired.AutoSize = true;
            this._legendExpired.Location = new System.Drawing.Point(475, 6);
            this._legendExpired.Margin = new System.Windows.Forms.Padding(0, 6, 16, 0);
            this._legendExpired.Name = "_legendExpired";
            this._legendExpired.Size = new System.Drawing.Size(182, 20);
            this._legendExpired.TabIndex = 5;
            this._legendExpired.Text = "Có lô đã hết hạn còn tồn";
            // 
            // _legendOutColor
            // 
            this._legendOutColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this._legendOutColor.Location = new System.Drawing.Point(673, 8);
            this._legendOutColor.Margin = new System.Windows.Forms.Padding(0, 8, 4, 0);
            this._legendOutColor.Name = "_legendOutColor";
            this._legendOutColor.Size = new System.Drawing.Size(12, 12);
            this._legendOutColor.TabIndex = 6;
            // 
            // _legendOut
            // 
            this._legendOut.AutoSize = true;
            this._legendOut.Location = new System.Drawing.Point(689, 6);
            this._legendOut.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this._legendOut.Name = "_legendOut";
            this._legendOut.Size = new System.Drawing.Size(97, 20);
            this._legendOut.TabIndex = 7;
            this._legendOut.Text = "Đã hết hàng";
            // 
            // _productStockTitle
            // 
            this._productStockTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._productStockTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._productStockTitle.Location = new System.Drawing.Point(12, 12);
            this._productStockTitle.Name = "_productStockTitle";
            this._productStockTitle.Size = new System.Drawing.Size(926, 28);
            this._productStockTitle.TabIndex = 2;
            this._productStockTitle.Text = "Tồn kho theo sản phẩm (top 20)";
            // 
            // _breakdownLayout
            // 
            this._breakdownLayout.ColumnCount = 2;
            this._breakdownLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._breakdownLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._breakdownLayout.Controls.Add(this._categoryBreakdownPanel, 0, 0);
            this._breakdownLayout.Controls.Add(this._priceBreakdownPanel, 1, 0);
            this._breakdownLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._breakdownLayout.Location = new System.Drawing.Point(24, 1304);
            this._breakdownLayout.Margin = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this._breakdownLayout.Name = "_breakdownLayout";
            this._breakdownLayout.RowCount = 1;
            this._breakdownLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._breakdownLayout.Size = new System.Drawing.Size(950, 304);
            this._breakdownLayout.TabIndex = 5;
            // 
            // _categoryBreakdownPanel
            // 
            this._categoryBreakdownPanel.BackColor = System.Drawing.Color.White;
            this._categoryBreakdownPanel.Controls.Add(this._categoryBreakdownGrid);
            this._categoryBreakdownPanel.Controls.Add(this._categoryBreakdownTitle);
            this._categoryBreakdownPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._categoryBreakdownPanel.Location = new System.Drawing.Point(0, 0);
            this._categoryBreakdownPanel.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this._categoryBreakdownPanel.Name = "_categoryBreakdownPanel";
            this._categoryBreakdownPanel.Padding = new System.Windows.Forms.Padding(12);
            this._categoryBreakdownPanel.Size = new System.Drawing.Size(469, 304);
            this._categoryBreakdownPanel.TabIndex = 0;
            // 
            // _categoryBreakdownGrid
            // 
            this._categoryBreakdownGrid.AllowUserToAddRows = false;
            this._categoryBreakdownGrid.AllowUserToDeleteRows = false;
            this._categoryBreakdownGrid.AllowUserToResizeRows = false;
            this._categoryBreakdownGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._categoryBreakdownGrid.BackgroundColor = this._grid.BackgroundColor;
            this._categoryBreakdownGrid.BorderStyle = this._grid.BorderStyle;
            this._categoryBreakdownGrid.CellBorderStyle = this._grid.CellBorderStyle;
            this._categoryBreakdownGrid.ColumnHeadersHeight = this._grid.ColumnHeadersHeight;
            this._categoryBreakdownGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._cbName,
            this._cbProducts,
            this._cbQuantity,
            this._cbValue,
            this._cbRatio});
            this._categoryBreakdownGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._categoryBreakdownGrid.EnableHeadersVisualStyles = this._grid.EnableHeadersVisualStyles;
            this._categoryBreakdownGrid.GridColor = this._grid.GridColor;
            this._categoryBreakdownGrid.Location = new System.Drawing.Point(12, 40);
            this._categoryBreakdownGrid.Margin = new System.Windows.Forms.Padding(0);
            this._categoryBreakdownGrid.Name = "_categoryBreakdownGrid";
            this._categoryBreakdownGrid.ReadOnly = true;
            this._categoryBreakdownGrid.RowHeadersVisible = false;
            this._categoryBreakdownGrid.RowHeadersWidth = 62;
            this._categoryBreakdownGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._categoryBreakdownGrid.Size = new System.Drawing.Size(445, 252);
            this._categoryBreakdownGrid.TabIndex = 0;
            // 
            // _cbName
            // 
            this._cbName.FillWeight = 30F;
            this._cbName.HeaderText = "Danh mục";
            this._cbName.MinimumWidth = 8;
            this._cbName.Name = "_cbName";
            this._cbName.ReadOnly = true;
            // 
            // _cbProducts
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this._cbProducts.DefaultCellStyle = dataGridViewCellStyle3;
            this._cbProducts.FillWeight = 12F;
            this._cbProducts.HeaderText = "Số SP";
            this._cbProducts.MinimumWidth = 8;
            this._cbProducts.Name = "_cbProducts";
            this._cbProducts.ReadOnly = true;
            // 
            // _cbQuantity
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this._cbQuantity.DefaultCellStyle = dataGridViewCellStyle4;
            this._cbQuantity.FillWeight = 15F;
            this._cbQuantity.HeaderText = "Số lượng";
            this._cbQuantity.MinimumWidth = 8;
            this._cbQuantity.Name = "_cbQuantity";
            this._cbQuantity.ReadOnly = true;
            // 
            // _cbValue
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this._cbValue.DefaultCellStyle = dataGridViewCellStyle5;
            this._cbValue.FillWeight = 23F;
            this._cbValue.HeaderText = "Giá trị (giá bán)";
            this._cbValue.MinimumWidth = 8;
            this._cbValue.Name = "_cbValue";
            this._cbValue.ReadOnly = true;
            // 
            // _cbRatio
            // 
            this._cbRatio.FillWeight = 20F;
            this._cbRatio.HeaderText = "Tỷ lệ";
            this._cbRatio.MinimumWidth = 8;
            this._cbRatio.Name = "_cbRatio";
            this._cbRatio.ReadOnly = true;
            // 
            // _categoryBreakdownTitle
            // 
            this._categoryBreakdownTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._categoryBreakdownTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._categoryBreakdownTitle.Location = new System.Drawing.Point(12, 12);
            this._categoryBreakdownTitle.Name = "_categoryBreakdownTitle";
            this._categoryBreakdownTitle.Size = new System.Drawing.Size(445, 28);
            this._categoryBreakdownTitle.TabIndex = 1;
            this._categoryBreakdownTitle.Text = "Tồn kho theo danh mục";
            // 
            // _priceBreakdownPanel
            // 
            this._priceBreakdownPanel.BackColor = System.Drawing.Color.White;
            this._priceBreakdownPanel.Controls.Add(this._priceBreakdownGrid);
            this._priceBreakdownPanel.Controls.Add(this._priceBreakdownTitle);
            this._priceBreakdownPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._priceBreakdownPanel.Location = new System.Drawing.Point(481, 0);
            this._priceBreakdownPanel.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this._priceBreakdownPanel.Name = "_priceBreakdownPanel";
            this._priceBreakdownPanel.Padding = new System.Windows.Forms.Padding(12);
            this._priceBreakdownPanel.Size = new System.Drawing.Size(469, 304);
            this._priceBreakdownPanel.TabIndex = 1;
            // 
            // _priceBreakdownGrid
            // 
            this._priceBreakdownGrid.AllowUserToAddRows = false;
            this._priceBreakdownGrid.AllowUserToDeleteRows = false;
            this._priceBreakdownGrid.AllowUserToResizeRows = false;
            this._priceBreakdownGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._priceBreakdownGrid.BackgroundColor = this._grid.BackgroundColor;
            this._priceBreakdownGrid.BorderStyle = this._grid.BorderStyle;
            this._priceBreakdownGrid.CellBorderStyle = this._grid.CellBorderStyle;
            this._priceBreakdownGrid.ColumnHeadersHeight = this._grid.ColumnHeadersHeight;
            this._priceBreakdownGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._pbRange,
            this._pbProducts,
            this._pbQuantity,
            this._pbValue,
            this._pbRatio});
            this._priceBreakdownGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._priceBreakdownGrid.EnableHeadersVisualStyles = this._grid.EnableHeadersVisualStyles;
            this._priceBreakdownGrid.GridColor = this._grid.GridColor;
            this._priceBreakdownGrid.Location = new System.Drawing.Point(12, 40);
            this._priceBreakdownGrid.Margin = new System.Windows.Forms.Padding(0);
            this._priceBreakdownGrid.Name = "_priceBreakdownGrid";
            this._priceBreakdownGrid.ReadOnly = true;
            this._priceBreakdownGrid.RowHeadersVisible = false;
            this._priceBreakdownGrid.RowHeadersWidth = 62;
            this._priceBreakdownGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._priceBreakdownGrid.Size = new System.Drawing.Size(445, 252);
            this._priceBreakdownGrid.TabIndex = 0;
            // 
            // _pbRange
            // 
            this._pbRange.FillWeight = 30F;
            this._pbRange.HeaderText = "Khoảng giá bán";
            this._pbRange.MinimumWidth = 8;
            this._pbRange.Name = "_pbRange";
            this._pbRange.ReadOnly = true;
            // 
            // _pbProducts
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this._pbProducts.DefaultCellStyle = dataGridViewCellStyle6;
            this._pbProducts.FillWeight = 12F;
            this._pbProducts.HeaderText = "Số SP";
            this._pbProducts.MinimumWidth = 8;
            this._pbProducts.Name = "_pbProducts";
            this._pbProducts.ReadOnly = true;
            // 
            // _pbQuantity
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this._pbQuantity.DefaultCellStyle = dataGridViewCellStyle7;
            this._pbQuantity.FillWeight = 15F;
            this._pbQuantity.HeaderText = "Số lượng";
            this._pbQuantity.MinimumWidth = 8;
            this._pbQuantity.Name = "_pbQuantity";
            this._pbQuantity.ReadOnly = true;
            // 
            // _pbValue
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this._pbValue.DefaultCellStyle = dataGridViewCellStyle8;
            this._pbValue.FillWeight = 23F;
            this._pbValue.HeaderText = "Giá trị (giá bán)";
            this._pbValue.MinimumWidth = 8;
            this._pbValue.Name = "_pbValue";
            this._pbValue.ReadOnly = true;
            // 
            // _pbRatio
            // 
            this._pbRatio.FillWeight = 20F;
            this._pbRatio.HeaderText = "Tỷ lệ";
            this._pbRatio.MinimumWidth = 8;
            this._pbRatio.Name = "_pbRatio";
            this._pbRatio.ReadOnly = true;
            // 
            // _priceBreakdownTitle
            // 
            this._priceBreakdownTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._priceBreakdownTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._priceBreakdownTitle.Location = new System.Drawing.Point(12, 12);
            this._priceBreakdownTitle.Name = "_priceBreakdownTitle";
            this._priceBreakdownTitle.Size = new System.Drawing.Size(445, 28);
            this._priceBreakdownTitle.TabIndex = 1;
            this._priceBreakdownTitle.Text = "Tồn kho theo khoảng giá bán";
            // 
            // _trendPanel
            // 
            this._trendPanel.BackColor = System.Drawing.Color.White;
            this._trendPanel.Controls.Add(this._trendChart);
            this._trendPanel.Controls.Add(this._trendFilters);
            this._trendPanel.Controls.Add(this._trendTitle);
            this._trendPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._trendPanel.Location = new System.Drawing.Point(24, 1624);
            this._trendPanel.Margin = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this._trendPanel.Name = "_trendPanel";
            this._trendPanel.Padding = new System.Windows.Forms.Padding(12);
            this._trendPanel.Size = new System.Drawing.Size(950, 424);
            this._trendPanel.TabIndex = 6;
            // 
            // _trendChart
            // 
            chartArea2.Name = "TrendArea";
            this._trendChart.ChartAreas.Add(chartArea2);
            this._trendChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "TrendLegend";
            this._trendChart.Legends.Add(legend1);
            this._trendChart.Location = new System.Drawing.Point(12, 80);
            this._trendChart.MinimumSize = new System.Drawing.Size(1, 1);
            this._trendChart.Name = "_trendChart";
            this._trendChart.Size = new System.Drawing.Size(926, 332);
            this._trendChart.TabIndex = 0;
            // 
            // _trendFilters
            // 
            this._trendFilters.Controls.Add(this._trendPreset);
            this._trendFilters.Controls.Add(this._trendFromLabel);
            this._trendFilters.Controls.Add(this._trendFrom);
            this._trendFilters.Controls.Add(this._trendToLabel);
            this._trendFilters.Controls.Add(this._trendTo);
            this._trendFilters.Controls.Add(this._trendApply);
            this._trendFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this._trendFilters.Location = new System.Drawing.Point(12, 40);
            this._trendFilters.Name = "_trendFilters";
            this._trendFilters.Size = new System.Drawing.Size(926, 40);
            this._trendFilters.TabIndex = 1;
            this._trendFilters.WrapContents = false;
            // 
            // _trendPreset
            // 
            this._trendPreset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._trendPreset.Items.AddRange(new object[] {
            "3 tháng gần đây",
            "6 tháng gần đây",
            "12 tháng gần đây",
            "Năm nay",
            "Tùy chọn"});
            this._trendPreset.Location = new System.Drawing.Point(3, 3);
            this._trendPreset.Name = "_trendPreset";
            this._trendPreset.Size = new System.Drawing.Size(160, 28);
            this._trendPreset.TabIndex = 0;
            // 
            // _trendFromLabel
            // 
            this._trendFromLabel.AutoSize = true;
            this._trendFromLabel.Location = new System.Drawing.Point(178, 9);
            this._trendFromLabel.Margin = new System.Windows.Forms.Padding(12, 9, 6, 0);
            this._trendFromLabel.Name = "_trendFromLabel";
            this._trendFromLabel.Size = new System.Drawing.Size(72, 20);
            this._trendFromLabel.TabIndex = 1;
            this._trendFromLabel.Text = "Từ tháng";
            // 
            // _trendFrom
            // 
            this._trendFrom.CustomFormat = "MM/yyyy";
            this._trendFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._trendFrom.Location = new System.Drawing.Point(259, 3);
            this._trendFrom.Name = "_trendFrom";
            this._trendFrom.ShowUpDown = true;
            this._trendFrom.Size = new System.Drawing.Size(100, 26);
            this._trendFrom.TabIndex = 2;
            // 
            // _trendToLabel
            // 
            this._trendToLabel.AutoSize = true;
            this._trendToLabel.Location = new System.Drawing.Point(370, 9);
            this._trendToLabel.Margin = new System.Windows.Forms.Padding(8, 9, 6, 0);
            this._trendToLabel.Name = "_trendToLabel";
            this._trendToLabel.Size = new System.Drawing.Size(84, 20);
            this._trendToLabel.TabIndex = 3;
            this._trendToLabel.Text = "Đến tháng";
            // 
            // _trendTo
            // 
            this._trendTo.CustomFormat = "MM/yyyy";
            this._trendTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._trendTo.Location = new System.Drawing.Point(463, 3);
            this._trendTo.Name = "_trendTo";
            this._trendTo.ShowUpDown = true;
            this._trendTo.Size = new System.Drawing.Size(100, 26);
            this._trendTo.TabIndex = 4;
            // 
            // _trendApply
            // 
            this._trendApply.Location = new System.Drawing.Point(574, 3);
            this._trendApply.Margin = new System.Windows.Forms.Padding(8, 3, 0, 0);
            this._trendApply.Name = "_trendApply";
            this._trendApply.Size = new System.Drawing.Size(80, 30);
            this._trendApply.TabIndex = 5;
            this._trendApply.Text = "Xem";
            this._trendApply.UseVisualStyleBackColor = true;
            // 
            // _trendTitle
            // 
            this._trendTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._trendTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._trendTitle.Location = new System.Drawing.Point(12, 12);
            this._trendTitle.Name = "_trendTitle";
            this._trendTitle.Size = new System.Drawing.Size(926, 28);
            this._trendTitle.TabIndex = 2;
            this._trendTitle.Text = "Xu hướng tồn kho theo tháng & danh mục";
            // 
            // _historyPanel
            // 
            this._historyPanel.BackColor = System.Drawing.Color.White;
            this._historyPanel.Controls.Add(this._historyGrid);
            this._historyPanel.Controls.Add(this._historyPager);
            this._historyPanel.Controls.Add(this._historyFilters);
            this._historyPanel.Controls.Add(this._historySubtitle);
            this._historyPanel.Controls.Add(this._historyTitle);
            this._historyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._historyPanel.Location = new System.Drawing.Point(24, 2064);
            this._historyPanel.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this._historyPanel.Name = "_historyPanel";
            this._historyPanel.Padding = new System.Windows.Forms.Padding(12);
            this._historyPanel.Size = new System.Drawing.Size(950, 592);
            this._historyPanel.TabIndex = 7;
            // 
            // _historyGrid
            // 
            this._historyGrid.AllowUserToAddRows = false;
            this._historyGrid.AllowUserToDeleteRows = false;
            this._historyGrid.AllowUserToResizeRows = false;
            this._historyGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._historyGrid.BackgroundColor = this._grid.BackgroundColor;
            this._historyGrid.BorderStyle = this._grid.BorderStyle;
            this._historyGrid.CellBorderStyle = this._grid.CellBorderStyle;
            this._historyGrid.ColumnHeadersHeight = this._grid.ColumnHeadersHeight;
            this._historyGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._hBatch,
            this._hTime,
            this._hType,
            this._hDocument,
            this._hProductCode,
            this._hProductName,
            this._hStock,
            this._hChange});
            this._historyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._historyGrid.EnableHeadersVisualStyles = this._grid.EnableHeadersVisualStyles;
            this._historyGrid.GridColor = this._grid.GridColor;
            this._historyGrid.Location = new System.Drawing.Point(12, 102);
            this._historyGrid.Margin = new System.Windows.Forms.Padding(0);
            this._historyGrid.Name = "_historyGrid";
            this._historyGrid.ReadOnly = true;
            this._historyGrid.RowHeadersVisible = false;
            this._historyGrid.RowHeadersWidth = 62;
            this._historyGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._historyGrid.Size = new System.Drawing.Size(926, 438);
            this._historyGrid.TabIndex = 0;
            // 
            // _hBatch
            // 
            this._hBatch.FillWeight = 11F;
            this._hBatch.HeaderText = "Mã lô";
            this._hBatch.MinimumWidth = 8;
            this._hBatch.Name = "_hBatch";
            this._hBatch.ReadOnly = true;
            // 
            // _hTime
            // 
            this._hTime.FillWeight = 13F;
            this._hTime.HeaderText = "Thời gian";
            this._hTime.MinimumWidth = 8;
            this._hTime.Name = "_hTime";
            this._hTime.ReadOnly = true;
            // 
            // _hType
            // 
            this._hType.FillWeight = 13F;
            this._hType.HeaderText = "Loại chứng từ";
            this._hType.MinimumWidth = 8;
            this._hType.Name = "_hType";
            this._hType.ReadOnly = true;
            // 
            // _hDocument
            // 
            this._hDocument.FillWeight = 12F;
            this._hDocument.HeaderText = "Mã chứng từ";
            this._hDocument.MinimumWidth = 8;
            this._hDocument.Name = "_hDocument";
            this._hDocument.ReadOnly = true;
            // 
            // _hProductCode
            // 
            this._hProductCode.FillWeight = 9F;
            this._hProductCode.HeaderText = "Mã SP";
            this._hProductCode.MinimumWidth = 8;
            this._hProductCode.Name = "_hProductCode";
            this._hProductCode.ReadOnly = true;
            // 
            // _hProductName
            // 
            this._hProductName.FillWeight = 24F;
            this._hProductName.HeaderText = "Tên sản phẩm";
            this._hProductName.MinimumWidth = 8;
            this._hProductName.Name = "_hProductName";
            this._hProductName.ReadOnly = true;
            // 
            // _hStock
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this._hStock.DefaultCellStyle = dataGridViewCellStyle9;
            this._hStock.FillWeight = 9F;
            this._hStock.HeaderText = "Tồn kho";
            this._hStock.MinimumWidth = 8;
            this._hStock.Name = "_hStock";
            this._hStock.ReadOnly = true;
            // 
            // _hChange
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this._hChange.DefaultCellStyle = dataGridViewCellStyle10;
            this._hChange.FillWeight = 9F;
            this._hChange.HeaderText = "Thay đổi";
            this._hChange.MinimumWidth = 8;
            this._hChange.Name = "_hChange";
            this._hChange.ReadOnly = true;
            // 
            // _historyPager
            // 
            this._historyPager.Controls.Add(this._historyNext);
            this._historyPager.Controls.Add(this._historyPageInfo);
            this._historyPager.Controls.Add(this._historyPrev);
            this._historyPager.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._historyPager.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._historyPager.Location = new System.Drawing.Point(12, 540);
            this._historyPager.Name = "_historyPager";
            this._historyPager.Size = new System.Drawing.Size(926, 40);
            this._historyPager.TabIndex = 1;
            this._historyPager.WrapContents = false;
            // 
            // _historyNext
            // 
            this._historyNext.Location = new System.Drawing.Point(843, 3);
            this._historyNext.Name = "_historyNext";
            this._historyNext.Size = new System.Drawing.Size(80, 30);
            this._historyNext.TabIndex = 0;
            this._historyNext.Text = "Sau ›";
            this._historyNext.UseVisualStyleBackColor = true;
            // 
            // _historyPageInfo
            // 
            this._historyPageInfo.AutoSize = true;
            this._historyPageInfo.Location = new System.Drawing.Point(756, 9);
            this._historyPageInfo.Margin = new System.Windows.Forms.Padding(8, 9, 8, 0);
            this._historyPageInfo.Name = "_historyPageInfo";
            this._historyPageInfo.Size = new System.Drawing.Size(76, 20);
            this._historyPageInfo.TabIndex = 1;
            this._historyPageInfo.Text = "Trang 1/1";
            // 
            // _historyPrev
            // 
            this._historyPrev.Location = new System.Drawing.Point(665, 3);
            this._historyPrev.Name = "_historyPrev";
            this._historyPrev.Size = new System.Drawing.Size(80, 30);
            this._historyPrev.TabIndex = 2;
            this._historyPrev.Text = "‹ Trước";
            this._historyPrev.UseVisualStyleBackColor = true;
            // 
            // _historyFilters
            // 
            this._historyFilters.Controls.Add(this._historySearchLabel);
            this._historyFilters.Controls.Add(this._historySearch);
            this._historyFilters.Controls.Add(this._historyFromLabel);
            this._historyFilters.Controls.Add(this._historyFrom);
            this._historyFilters.Controls.Add(this._historyToLabel);
            this._historyFilters.Controls.Add(this._historyTo);
            this._historyFilters.Controls.Add(this._historySearchButton);
            this._historyFilters.Controls.Add(this._historyClear);
            this._historyFilters.Controls.Add(this._historyExport);
            this._historyFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this._historyFilters.Location = new System.Drawing.Point(12, 62);
            this._historyFilters.Name = "_historyFilters";
            this._historyFilters.Size = new System.Drawing.Size(926, 40);
            this._historyFilters.TabIndex = 2;
            this._historyFilters.WrapContents = false;
            // 
            // _historySearchLabel
            // 
            this._historySearchLabel.AutoSize = true;
            this._historySearchLabel.Location = new System.Drawing.Point(0, 9);
            this._historySearchLabel.Margin = new System.Windows.Forms.Padding(0, 9, 6, 0);
            this._historySearchLabel.Name = "_historySearchLabel";
            this._historySearchLabel.Size = new System.Drawing.Size(34, 20);
            this._historySearchLabel.TabIndex = 0;
            this._historySearchLabel.Text = "Tìm";
            // 
            // _historySearch
            // 
            this._historySearch.Location = new System.Drawing.Point(40, 5);
            this._historySearch.Margin = new System.Windows.Forms.Padding(0, 5, 8, 0);
            this._historySearch.Name = "_historySearch";
            this._historySearch.Size = new System.Drawing.Size(260, 26);
            this._historySearch.TabIndex = 1;
            // 
            // _historyFromLabel
            // 
            this._historyFromLabel.AutoSize = true;
            this._historyFromLabel.Location = new System.Drawing.Point(308, 9);
            this._historyFromLabel.Margin = new System.Windows.Forms.Padding(0, 9, 6, 0);
            this._historyFromLabel.Name = "_historyFromLabel";
            this._historyFromLabel.Size = new System.Drawing.Size(65, 20);
            this._historyFromLabel.TabIndex = 2;
            this._historyFromLabel.Text = "Từ ngày";
            // 
            // _historyFrom
            // 
            this._historyFrom.Checked = false;
            this._historyFrom.CustomFormat = "dd/MM/yyyy";
            this._historyFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._historyFrom.Location = new System.Drawing.Point(382, 3);
            this._historyFrom.Name = "_historyFrom";
            this._historyFrom.ShowCheckBox = true;
            this._historyFrom.Size = new System.Drawing.Size(130, 26);
            this._historyFrom.TabIndex = 3;
            // 
            // _historyToLabel
            // 
            this._historyToLabel.AutoSize = true;
            this._historyToLabel.Location = new System.Drawing.Point(523, 9);
            this._historyToLabel.Margin = new System.Windows.Forms.Padding(8, 9, 6, 0);
            this._historyToLabel.Name = "_historyToLabel";
            this._historyToLabel.Size = new System.Drawing.Size(77, 20);
            this._historyToLabel.TabIndex = 4;
            this._historyToLabel.Text = "Đến ngày";
            // 
            // _historyTo
            // 
            this._historyTo.Checked = false;
            this._historyTo.CustomFormat = "dd/MM/yyyy";
            this._historyTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._historyTo.Location = new System.Drawing.Point(609, 3);
            this._historyTo.Name = "_historyTo";
            this._historyTo.ShowCheckBox = true;
            this._historyTo.Size = new System.Drawing.Size(130, 26);
            this._historyTo.TabIndex = 5;
            // 
            // _historySearchButton
            // 
            this._historySearchButton.Location = new System.Drawing.Point(750, 3);
            this._historySearchButton.Margin = new System.Windows.Forms.Padding(8, 3, 4, 0);
            this._historySearchButton.Name = "_historySearchButton";
            this._historySearchButton.Size = new System.Drawing.Size(80, 30);
            this._historySearchButton.TabIndex = 6;
            this._historySearchButton.Text = "Lọc";
            this._historySearchButton.UseVisualStyleBackColor = true;
            // 
            // _historyClear
            // 
            this._historyClear.Location = new System.Drawing.Point(838, 3);
            this._historyClear.Margin = new System.Windows.Forms.Padding(4, 3, 4, 0);
            this._historyClear.Name = "_historyClear";
            this._historyClear.Size = new System.Drawing.Size(80, 30);
            this._historyClear.TabIndex = 7;
            this._historyClear.Text = "Xóa lọc";
            this._historyClear.UseVisualStyleBackColor = true;
            // 
            // _historyExport
            // 
            this._historyExport.Location = new System.Drawing.Point(926, 3);
            this._historyExport.Margin = new System.Windows.Forms.Padding(4, 3, 0, 0);
            this._historyExport.Name = "_historyExport";
            this._historyExport.Size = new System.Drawing.Size(110, 30);
            this._historyExport.TabIndex = 8;
            this._historyExport.Text = "Tùy chọn ▾";
            this._historyExport.UseVisualStyleBackColor = true;
            // 
            // _historySubtitle
            // 
            this._historySubtitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._historySubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._historySubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._historySubtitle.Location = new System.Drawing.Point(12, 40);
            this._historySubtitle.Name = "_historySubtitle";
            this._historySubtitle.Size = new System.Drawing.Size(926, 22);
            this._historySubtitle.TabIndex = 3;
            this._historySubtitle.Text = "Theo dõi từng chứng từ đã nhập, xuất, trả, hủy hoặc điều chỉnh số lượng của từng " +
    "lô";
            // 
            // _historyTitle
            // 
            this._historyTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._historyTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._historyTitle.Location = new System.Drawing.Point(12, 12);
            this._historyTitle.Name = "_historyTitle";
            this._historyTitle.Size = new System.Drawing.Size(926, 28);
            this._historyTitle.TabIndex = 4;
            this._historyTitle.Text = "Lịch sử phiếu / hóa đơn làm thay đổi lô";
            // 
            // _exportMenu
            // 
            this._exportMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this._exportMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._exportCategoryCsv});
            this._exportMenu.Name = "_exportMenu";
            this._exportMenu.Size = new System.Drawing.Size(321, 40);
            // 
            // _exportCategoryCsv
            // 
            this._exportCategoryCsv.Name = "_exportCategoryCsv";
            this._exportCategoryCsv.Size = new System.Drawing.Size(320, 32);
            this._exportCategoryCsv.Text = "Xuất CSV tồn theo danh mục";
            // 
            // _historyExportMenu
            // 
            this._historyExportMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this._historyExportMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._historyExportCsv});
            this._historyExportMenu.Name = "_historyExportMenu";
            this._historyExportMenu.Size = new System.Drawing.Size(164, 40);
            // 
            // _historyExportCsv
            // 
            this._historyExportCsv.Name = "_historyExportCsv";
            this._historyExportCsv.Size = new System.Drawing.Size(163, 32);
            this._historyExportCsv.Text = "Xuất CSV";
            // 
            // ucInventoryReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.Controls.Add(this._root);
            this.Name = "ucInventoryReport";
            this.Size = new System.Drawing.Size(998, 800);
            this._root.ResumeLayout(false);
            this._header.ResumeLayout(false);
            this._headerIconPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._headerIcon)).EndInit();
            this._filters.ResumeLayout(false);
            this._filters.PerformLayout();
            this._summary.ResumeLayout(false);
            this._content.ResumeLayout(false);
            this._chartPanel.ResumeLayout(false);
            this._chartsLayout.ResumeLayout(false);
            this._importChartPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._importChart)).EndInit();
            this._categoryChartPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._categoryChart)).EndInit();
            this._disposalChartPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._disposalChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._grid)).EndInit();
            this._disposalPanel.ResumeLayout(false);
            this._disposalLayout.ResumeLayout(false);
            this._disposalProductPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._disposalGrid)).EndInit();
            this._productStockPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._productStockChart)).EndInit();
            this._productStockLegend.ResumeLayout(false);
            this._productStockLegend.PerformLayout();
            this._breakdownLayout.ResumeLayout(false);
            this._categoryBreakdownPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._categoryBreakdownGrid)).EndInit();
            this._priceBreakdownPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._priceBreakdownGrid)).EndInit();
            this._trendPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._trendChart)).EndInit();
            this._trendFilters.ResumeLayout(false);
            this._trendFilters.PerformLayout();
            this._historyPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._historyGrid)).EndInit();
            this._historyPager.ResumeLayout(false);
            this._historyPager.PerformLayout();
            this._historyFilters.ResumeLayout(false);
            this._historyFilters.PerformLayout();
            this._exportMenu.ResumeLayout(false);
            this._historyExportMenu.ResumeLayout(false);
            this._apply.Click += new System.EventHandler(this.Apply_Click);
            this._viewRdlc.Click += new System.EventHandler(this.ViewRdlc_Click);
            this._exportOptions.Click += new System.EventHandler(this.ExportOptions_Click);
            this._exportCategoryCsv.Click += new System.EventHandler(this.ExportCategoryCsv_Click);
            this._trendApply.Click += new System.EventHandler(this.TrendApply_Click);
            this._historySearchButton.Click += new System.EventHandler(this.HistorySearchButton_Click);
            this._historyClear.Click += new System.EventHandler(this.HistoryClear_Click);
            this._historyPrev.Click += new System.EventHandler(this.HistoryPrev_Click);
            this._historyNext.Click += new System.EventHandler(this.HistoryNext_Click);
            this._historyExport.Click += new System.EventHandler(this.HistoryExport_Click);
            this._historyExportCsv.Click += new System.EventHandler(this.HistoryExportCsv_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private DataGridViewTextBoxColumn Code;
        private DataGridViewTextBoxColumn ProductNameColumn;
        private DataGridViewTextBoxColumn Category;
        private DataGridViewTextBoxColumn Quantity;
        private DataGridViewTextBoxColumn UnitPrice;
        private DataGridViewTextBoxColumn StockValue;
        private DataGridViewTextBoxColumn _statusColumn;
        private DataGridViewTextBoxColumn DisposalProduct;
        private DataGridViewTextBoxColumn DisposalQuantity;
        private DataGridViewTextBoxColumn DisposalLoss;
    }
}
