using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using FontAwesome.Sharp;

namespace SIMS_WinFormsApp.Views.Reports
{
    partial class frmChartDashboard
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
        private Label _toLabel;
        private DateTimePicker _fromDate;
        private DateTimePicker _toDate;
        private ComboBox _periodFilter;
        private Button _applyFilter;
        private Button _viewRevenueRdlc;
        private TableLayoutPanel _summary;
        private Label _revenueCard;
        private Label _orderCard;
        private Label _averageCard;
        private TableLayoutPanel _charts;
        private Panel _revenueChartPlaceholder;
        private Panel _salesChartPlaceholder;
        private Label _revenueChartTitle;
        private Label _salesChartTitle;
        private Chart _revenueTrendChart;
        private Chart _categoryChart;
        private DataGridView _topProductsGrid;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this._root = new System.Windows.Forms.TableLayoutPanel();
            this._header = new System.Windows.Forms.Panel();
            this._headerIconPanel = new System.Windows.Forms.Panel();
            this._headerIcon = new FontAwesome.Sharp.IconPictureBox();
            this._subtitle = new System.Windows.Forms.Label();
            this._title = new System.Windows.Forms.Label();
            this._viewRevenueRdlc = new System.Windows.Forms.Button();
            this._filters = new System.Windows.Forms.FlowLayoutPanel();
            this._fromLabel = new System.Windows.Forms.Label();
            this._fromDate = new System.Windows.Forms.DateTimePicker();
            this._toLabel = new System.Windows.Forms.Label();
            this._toDate = new System.Windows.Forms.DateTimePicker();
            this._periodFilter = new System.Windows.Forms.ComboBox();
            this._applyFilter = new System.Windows.Forms.Button();
            this._summary = new System.Windows.Forms.TableLayoutPanel();
            this._revenueCard = new System.Windows.Forms.Label();
            this._orderCard = new System.Windows.Forms.Label();
            this._averageCard = new System.Windows.Forms.Label();
            this._charts = new System.Windows.Forms.TableLayoutPanel();
            this._revenueChartPlaceholder = new System.Windows.Forms.Panel();
            this._revenueTrendChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this._revenueChartTitle = new System.Windows.Forms.Label();
            this._salesChartPlaceholder = new System.Windows.Forms.Panel();
            this._categoryChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this._salesChartTitle = new System.Windows.Forms.Label();
            this._topProductsGrid = new System.Windows.Forms.DataGridView();
            this._topProductNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._topQuantityColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._topAmountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._root.SuspendLayout();
            this._header.SuspendLayout();
            this._headerIconPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._headerIcon)).BeginInit();
            this._filters.SuspendLayout();
            this._summary.SuspendLayout();
            this._charts.SuspendLayout();
            this._revenueChartPlaceholder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._revenueTrendChart)).BeginInit();
            this._salesChartPlaceholder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._categoryChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._topProductsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // _root
            // 
            this._root.ColumnCount = 1;
            this._root.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._root.Controls.Add(this._header, 0, 0);
            this._root.Controls.Add(this._filters, 0, 1);
            this._root.Controls.Add(this._summary, 0, 2);
            this._root.Controls.Add(this._charts, 0, 3);
            this._root.Controls.Add(this._topProductsGrid, 0, 4);
            this._root.Dock = System.Windows.Forms.DockStyle.Fill;
            this._root.Location = new System.Drawing.Point(0, 0);
            this._root.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._root.Name = "_root";
            this._root.Padding = new System.Windows.Forms.Padding(21, 19, 21, 19);
            this._root.RowCount = 5;
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 77F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 136F));
            this._root.Size = new System.Drawing.Size(1049, 640);
            this._root.TabIndex = 0;
            // 
            // _header
            // 
            this._header.BackColor = System.Drawing.Color.White;
            this._header.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._header.Controls.Add(this._headerIconPanel);
            this._header.Controls.Add(this._subtitle);
            this._header.Controls.Add(this._title);
            this._header.Controls.Add(this._viewRevenueRdlc);
            this._header.Dock = System.Windows.Forms.DockStyle.Fill;
            this._header.Location = new System.Drawing.Point(24, 21);
            this._header.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._header.Name = "_header";
            this._header.Size = new System.Drawing.Size(1001, 66);
            this._header.TabIndex = 0;
            // 
            // _headerIconPanel
            // 
            this._headerIconPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(240)))), ((int)(((byte)(254)))));
            this._headerIconPanel.Controls.Add(this._headerIcon);
            this._headerIconPanel.Location = new System.Drawing.Point(14, 13);
            this._headerIconPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._headerIconPanel.Name = "_headerIconPanel";
            this._headerIconPanel.Size = new System.Drawing.Size(44, 40);
            this._headerIconPanel.TabIndex = 0;
            // 
            // _headerIcon
            // 
            this._headerIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(240)))), ((int)(((byte)(254)))));
            this._headerIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._headerIcon.IconChar = FontAwesome.Sharp.IconChar.ChartLine;
            this._headerIcon.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._headerIcon.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._headerIcon.IconSize = 19;
            this._headerIcon.Location = new System.Drawing.Point(12, 10);
            this._headerIcon.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._headerIcon.Name = "_headerIcon";
            this._headerIcon.Size = new System.Drawing.Size(21, 19);
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
            this._subtitle.Location = new System.Drawing.Point(71, 42);
            this._subtitle.Name = "_subtitle";
            this._subtitle.Size = new System.Drawing.Size(768, 16);
            this._subtitle.TabIndex = 0;
            this._subtitle.Text = "Tổng quan doanh thu, đơn hàng và sản phẩm bán chạy";
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
            this._title.Location = new System.Drawing.Point(71, 10);
            this._title.Name = "_title";
            this._title.Size = new System.Drawing.Size(768, 29);
            this._title.TabIndex = 1;
            this._title.Text = "Báo cáo doanh thu";
            this._title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._title.UseMnemonic = false;
            // 
            // _viewRevenueRdlc
            // 
            this._viewRevenueRdlc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._viewRevenueRdlc.BackColor = System.Drawing.Color.White;
            this._viewRevenueRdlc.Cursor = System.Windows.Forms.Cursors.Hand;
            this._viewRevenueRdlc.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._viewRevenueRdlc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._viewRevenueRdlc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._viewRevenueRdlc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._viewRevenueRdlc.Location = new System.Drawing.Point(853, 18);
            this._viewRevenueRdlc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._viewRevenueRdlc.Name = "_viewRevenueRdlc";
            this._viewRevenueRdlc.Size = new System.Drawing.Size(133, 30);
            this._viewRevenueRdlc.TabIndex = 3;
            this._viewRevenueRdlc.Text = "Báo cáo doanh thu";
            this._viewRevenueRdlc.UseVisualStyleBackColor = false;
            this._viewRevenueRdlc.Click += new System.EventHandler(this.ViewRevenueRdlc_Click);
            // 
            // _filters
            // 
            this._filters.Controls.Add(this._fromLabel);
            this._filters.Controls.Add(this._fromDate);
            this._filters.Controls.Add(this._toLabel);
            this._filters.Controls.Add(this._toDate);
            this._filters.Controls.Add(this._periodFilter);
            this._filters.Controls.Add(this._applyFilter);
            this._filters.Dock = System.Windows.Forms.DockStyle.Fill;
            this._filters.Location = new System.Drawing.Point(24, 91);
            this._filters.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._filters.Name = "_filters";
            this._filters.Size = new System.Drawing.Size(1001, 34);
            this._filters.TabIndex = 1;
            // 
            // _fromLabel
            // 
            this._fromLabel.AutoSize = true;
            this._fromLabel.Location = new System.Drawing.Point(0, 7);
            this._fromLabel.Margin = new System.Windows.Forms.Padding(0, 7, 5, 0);
            this._fromLabel.Name = "_fromLabel";
            this._fromLabel.Size = new System.Drawing.Size(56, 16);
            this._fromLabel.TabIndex = 0;
            this._fromLabel.Text = "Từ ngày";
            // 
            // _fromDate
            // 
            this._fromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this._fromDate.Location = new System.Drawing.Point(64, 2);
            this._fromDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._fromDate.Name = "_fromDate";
            this._fromDate.Size = new System.Drawing.Size(107, 22);
            this._fromDate.TabIndex = 1;
            // 
            // _toLabel
            // 
            this._toLabel.AutoSize = true;
            this._toLabel.Location = new System.Drawing.Point(188, 7);
            this._toLabel.Margin = new System.Windows.Forms.Padding(14, 7, 5, 0);
            this._toLabel.Name = "_toLabel";
            this._toLabel.Size = new System.Drawing.Size(64, 16);
            this._toLabel.TabIndex = 2;
            this._toLabel.Text = "Đến ngày";
            // 
            // _toDate
            // 
            this._toDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this._toDate.Location = new System.Drawing.Point(260, 2);
            this._toDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._toDate.Name = "_toDate";
            this._toDate.Size = new System.Drawing.Size(107, 22);
            this._toDate.TabIndex = 3;
            // 
            // _periodFilter
            // 
            this._periodFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._periodFilter.Items.AddRange(new object[] {
            "Theo ngày",
            "Theo tháng",
            "Theo quý"});
            this._periodFilter.Location = new System.Drawing.Point(373, 2);
            this._periodFilter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._periodFilter.Name = "_periodFilter";
            this._periodFilter.Size = new System.Drawing.Size(116, 24);
            this._periodFilter.TabIndex = 4;
            // 
            // _applyFilter
            // 
            this._applyFilter.Location = new System.Drawing.Point(495, 2);
            this._applyFilter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._applyFilter.Name = "_applyFilter";
            this._applyFilter.Size = new System.Drawing.Size(84, 24);
            this._applyFilter.TabIndex = 5;
            this._applyFilter.Text = "Áp dụng";
            this._applyFilter.UseVisualStyleBackColor = true;
            this._applyFilter.Click += new System.EventHandler(this.ApplyFilter_Click);
            // 
            // _summary
            // 
            this._summary.ColumnCount = 3;
            this._summary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this._summary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this._summary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this._summary.Controls.Add(this._revenueCard, 0, 0);
            this._summary.Controls.Add(this._orderCard, 1, 0);
            this._summary.Controls.Add(this._averageCard, 2, 0);
            this._summary.Dock = System.Windows.Forms.DockStyle.Fill;
            this._summary.Location = new System.Drawing.Point(24, 129);
            this._summary.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._summary.Name = "_summary";
            this._summary.Size = new System.Drawing.Size(1001, 73);
            this._summary.TabIndex = 2;
            // 
            // _revenueCard
            // 
            this._revenueCard.BackColor = System.Drawing.Color.White;
            this._revenueCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._revenueCard.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._revenueCard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._revenueCard.Location = new System.Drawing.Point(0, 0);
            this._revenueCard.Margin = new System.Windows.Forms.Padding(0, 0, 9, 0);
            this._revenueCard.Name = "_revenueCard";
            this._revenueCard.Padding = new System.Windows.Forms.Padding(14, 13, 14, 13);
            this._revenueCard.Size = new System.Drawing.Size(324, 73);
            this._revenueCard.TabIndex = 0;
            this._revenueCard.Text = "TỔNG DOANH THU\r\n—";
            // 
            // _orderCard
            // 
            this._orderCard.BackColor = System.Drawing.Color.White;
            this._orderCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._orderCard.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._orderCard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._orderCard.Location = new System.Drawing.Point(337, 0);
            this._orderCard.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._orderCard.Name = "_orderCard";
            this._orderCard.Padding = new System.Windows.Forms.Padding(14, 13, 14, 13);
            this._orderCard.Size = new System.Drawing.Size(325, 73);
            this._orderCard.TabIndex = 1;
            this._orderCard.Text = "SỐ ĐƠN HÀNG\r\n—";
            // 
            // _averageCard
            // 
            this._averageCard.BackColor = System.Drawing.Color.White;
            this._averageCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._averageCard.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._averageCard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._averageCard.Location = new System.Drawing.Point(675, 0);
            this._averageCard.Margin = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this._averageCard.Name = "_averageCard";
            this._averageCard.Padding = new System.Windows.Forms.Padding(14, 13, 14, 13);
            this._averageCard.Size = new System.Drawing.Size(326, 73);
            this._averageCard.TabIndex = 2;
            this._averageCard.Text = "GIÁ TRỊ TRUNG BÌNH\r\n—";
            // 
            // _charts
            // 
            this._charts.ColumnCount = 2;
            this._charts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62F));
            this._charts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38F));
            this._charts.Controls.Add(this._revenueChartPlaceholder, 0, 0);
            this._charts.Controls.Add(this._salesChartPlaceholder, 1, 0);
            this._charts.Dock = System.Windows.Forms.DockStyle.Fill;
            this._charts.Location = new System.Drawing.Point(24, 206);
            this._charts.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._charts.Name = "_charts";
            this._charts.RowCount = 1;
            this._charts.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._charts.Size = new System.Drawing.Size(1001, 277);
            this._charts.TabIndex = 3;
            // 
            // _revenueChartPlaceholder
            // 
            this._revenueChartPlaceholder.BackColor = System.Drawing.Color.White;
            this._revenueChartPlaceholder.Controls.Add(this._revenueTrendChart);
            this._revenueChartPlaceholder.Controls.Add(this._revenueChartTitle);
            this._revenueChartPlaceholder.Dock = System.Windows.Forms.DockStyle.Fill;
            this._revenueChartPlaceholder.Location = new System.Drawing.Point(0, 0);
            this._revenueChartPlaceholder.Margin = new System.Windows.Forms.Padding(0, 0, 7, 0);
            this._revenueChartPlaceholder.Name = "_revenueChartPlaceholder";
            this._revenueChartPlaceholder.Padding = new System.Windows.Forms.Padding(11, 10, 11, 10);
            this._revenueChartPlaceholder.Size = new System.Drawing.Size(613, 277);
            this._revenueChartPlaceholder.TabIndex = 0;
            // 
            // _revenueTrendChart
            // 
            chartArea1.Name = "RevenueArea";
            this._revenueTrendChart.ChartAreas.Add(chartArea1);
            this._revenueTrendChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this._revenueTrendChart.Legends.Add(legend1);
            this._revenueTrendChart.Location = new System.Drawing.Point(11, 35);
            this._revenueTrendChart.Margin = new System.Windows.Forms.Padding(0);
            this._revenueTrendChart.Name = "_revenueTrendChart";
            series1.ChartArea = "RevenueArea";
            series1.Legend = "Legend1";
            series1.Name = "Doanh thu";
            series2.ChartArea = "RevenueArea";
            series2.Legend = "Legend1";
            series2.Name = "Số đơn";
            this._revenueTrendChart.Series.Add(series1);
            this._revenueTrendChart.Series.Add(series2);
            this._revenueTrendChart.Size = new System.Drawing.Size(591, 232);
            this._revenueTrendChart.TabIndex = 1;
            this._revenueTrendChart.Text = "Xu hướng doanh thu";
            // 
            // _revenueChartTitle
            // 
            this._revenueChartTitle.AutoSize = true;
            this._revenueChartTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._revenueChartTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._revenueChartTitle.Location = new System.Drawing.Point(11, 10);
            this._revenueChartTitle.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this._revenueChartTitle.Name = "_revenueChartTitle";
            this._revenueChartTitle.Size = new System.Drawing.Size(235, 25);
            this._revenueChartTitle.TabIndex = 0;
            this._revenueChartTitle.Text = "Doanh thu theo thời gian";
            this._revenueChartTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _salesChartPlaceholder
            // 
            this._salesChartPlaceholder.BackColor = System.Drawing.Color.White;
            this._salesChartPlaceholder.Controls.Add(this._categoryChart);
            this._salesChartPlaceholder.Controls.Add(this._salesChartTitle);
            this._salesChartPlaceholder.Dock = System.Windows.Forms.DockStyle.Fill;
            this._salesChartPlaceholder.Location = new System.Drawing.Point(623, 2);
            this._salesChartPlaceholder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._salesChartPlaceholder.Name = "_salesChartPlaceholder";
            this._salesChartPlaceholder.Padding = new System.Windows.Forms.Padding(11, 10, 11, 10);
            this._salesChartPlaceholder.Size = new System.Drawing.Size(375, 273);
            this._salesChartPlaceholder.TabIndex = 0;
            // 
            // _categoryChart
            // 
            chartArea2.Name = "CategoryArea";
            this._categoryChart.ChartAreas.Add(chartArea2);
            this._categoryChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this._categoryChart.Legends.Add(legend2);
            this._categoryChart.Location = new System.Drawing.Point(11, 35);
            this._categoryChart.Margin = new System.Windows.Forms.Padding(0);
            this._categoryChart.Name = "_categoryChart";
            series3.ChartArea = "CategoryArea";
            series3.Legend = "Legend1";
            series3.Name = "Danh mục";
            this._categoryChart.Series.Add(series3);
            this._categoryChart.Size = new System.Drawing.Size(353, 228);
            this._categoryChart.TabIndex = 1;
            this._categoryChart.Text = "Cơ cấu doanh thu";
            // 
            // _salesChartTitle
            // 
            this._salesChartTitle.AutoSize = true;
            this._salesChartTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._salesChartTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._salesChartTitle.Location = new System.Drawing.Point(11, 10);
            this._salesChartTitle.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this._salesChartTitle.Name = "_salesChartTitle";
            this._salesChartTitle.Size = new System.Drawing.Size(244, 25);
            this._salesChartTitle.TabIndex = 0;
            this._salesChartTitle.Text = "Doanh thu theo danh mục";
            this._salesChartTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _topProductsGrid
            // 
            this._topProductsGrid.AllowUserToAddRows = false;
            this._topProductsGrid.AllowUserToDeleteRows = false;
            this._topProductsGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._topProductsGrid.BackgroundColor = System.Drawing.Color.White;
            this._topProductsGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._topProductsGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this._topProductsGrid.ColumnHeadersHeight = 32;
            this._topProductsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._topProductNameColumn,
            this._topQuantityColumn,
            this._topAmountColumn});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(242)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._topProductsGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this._topProductsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._topProductsGrid.EnableHeadersVisualStyles = false;
            this._topProductsGrid.Location = new System.Drawing.Point(24, 487);
            this._topProductsGrid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._topProductsGrid.Name = "_topProductsGrid";
            this._topProductsGrid.ReadOnly = true;
            this._topProductsGrid.RowHeadersVisible = false;
            this._topProductsGrid.RowHeadersWidth = 62;
            this._topProductsGrid.RowTemplate.Height = 25;
            this._topProductsGrid.Size = new System.Drawing.Size(1001, 132);
            this._topProductsGrid.TabIndex = 4;
            // 
            // _topProductNameColumn
            // 
            this._topProductNameColumn.HeaderText = "Sản phẩm bán chạy";
            this._topProductNameColumn.MinimumWidth = 8;
            this._topProductNameColumn.Name = "_topProductNameColumn";
            this._topProductNameColumn.ReadOnly = true;
            // 
            // _topQuantityColumn
            // 
            this._topQuantityColumn.HeaderText = "Số lượng";
            this._topQuantityColumn.MinimumWidth = 8;
            this._topQuantityColumn.Name = "_topQuantityColumn";
            this._topQuantityColumn.ReadOnly = true;
            // 
            // _topAmountColumn
            // 
            this._topAmountColumn.HeaderText = "Doanh thu";
            this._topAmountColumn.MinimumWidth = 8;
            this._topAmountColumn.Name = "_topAmountColumn";
            this._topAmountColumn.ReadOnly = true;
            // 
            // frmChartDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1049, 640);
            this.Controls.Add(this._root);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(802, 529);
            this.Name = "frmChartDashboard";
            this.Text = "Báo cáo doanh thu";
            this.Load += new System.EventHandler(this.frmChartDashboard_Load);
            this._root.ResumeLayout(false);
            this._header.ResumeLayout(false);
            this._headerIconPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._headerIcon)).EndInit();
            this._filters.ResumeLayout(false);
            this._filters.PerformLayout();
            this._summary.ResumeLayout(false);
            this._charts.ResumeLayout(false);
            this._revenueChartPlaceholder.ResumeLayout(false);
            this._revenueChartPlaceholder.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._revenueTrendChart)).EndInit();
            this._salesChartPlaceholder.ResumeLayout(false);
            this._salesChartPlaceholder.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._categoryChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._topProductsGrid)).EndInit();
            this.ResumeLayout(false);

        }

        private DataGridViewTextBoxColumn _topProductNameColumn;
        private DataGridViewTextBoxColumn _topQuantityColumn;
        private DataGridViewTextBoxColumn _topAmountColumn;
    }
}
