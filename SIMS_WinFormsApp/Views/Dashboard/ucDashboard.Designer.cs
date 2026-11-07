using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using FontAwesome.Sharp;

namespace SIMS_WinFormsApp.Views.Dashboard
{
    partial class ucDashboard
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel _root;
        private Panel _headerPanel;
        private Panel _headerIconPanel;
        private IconPictureBox _headerIcon;
        private Label _titleLabel;
        private Label _welcomeLabel;
        private LinkLabel _expiryBatchesLink;
        private TableLayoutPanel _contentLayout;
        private TableLayoutPanel _statsLayout;
        private Panel _revenueSummaryPanel;
        private TableLayoutPanel _revenueSummaryLayout;
        private Label _chartTitleLabel;
        private Chart _revenueSummaryChart;
        private Panel _revenueFilterPanel;
        private Label _revenueFilterLabel;
        private ComboBox _revenuePeriodFilter;
        private DateTimePicker _revenueDatePicker;
        private Button _applyRevenueFilterButton;
        private Panel _revenueCard;
        private Panel _ordersCard;
        private Panel _stockCard;
        private Panel _customersCard;
        private Label _revenueValue;
        private Label _ordersValue;
        private Label _stockValue;
        private Label _customersValue;
        private Label _revenueCaption;
        private Label _ordersCaption;
        private Label _stockCaption;
        private Label _customersCaption;
        private Panel _activityCard;
        private Label _activityTitle;
        private Label _activitySubtitle;
        private DataGridView _activityGrid;
        private Panel _invoiceFilterPanel;
        private Label _invoiceFilterLabel;
        private ComboBox _invoicePeriodFilter;
        private DateTimePicker _invoiceDatePicker;
        private Button _applyInvoiceFilterButton;
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this._root = new System.Windows.Forms.TableLayoutPanel();
            this._headerPanel = new System.Windows.Forms.Panel();
            this._welcomeLabel = new System.Windows.Forms.Label();
            this._titleLabel = new System.Windows.Forms.Label();
            this._expiryBatchesLink = new System.Windows.Forms.LinkLabel();
            this._headerIconPanel = new System.Windows.Forms.Panel();
            this._headerIcon = new FontAwesome.Sharp.IconPictureBox();
            this._contentLayout = new System.Windows.Forms.TableLayoutPanel();
            this._statsLayout = new System.Windows.Forms.TableLayoutPanel();
            this._revenueSummaryPanel = new System.Windows.Forms.Panel();
            this._revenueSummaryLayout = new System.Windows.Forms.TableLayoutPanel();
            this._chartTitleLabel = new System.Windows.Forms.Label();
            this._revenueSummaryChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this._revenueFilterPanel = new System.Windows.Forms.Panel();
            this._revenueFilterLabel = new System.Windows.Forms.Label();
            this._revenuePeriodFilter = new System.Windows.Forms.ComboBox();
            this._revenueDatePicker = new System.Windows.Forms.DateTimePicker();
            this._applyRevenueFilterButton = new System.Windows.Forms.Button();
            this._revenueCard = new System.Windows.Forms.Panel();
            this._revenueCaption = new System.Windows.Forms.Label();
            this._revenueValue = new System.Windows.Forms.Label();
            this._ordersCard = new System.Windows.Forms.Panel();
            this._ordersCaption = new System.Windows.Forms.Label();
            this._ordersValue = new System.Windows.Forms.Label();
            this._stockCard = new System.Windows.Forms.Panel();
            this._stockCaption = new System.Windows.Forms.Label();
            this._stockValue = new System.Windows.Forms.Label();
            this._customersCard = new System.Windows.Forms.Panel();
            this._customersCaption = new System.Windows.Forms.Label();
            this._customersValue = new System.Windows.Forms.Label();
            this._activityCard = new System.Windows.Forms.Panel();
            this._invoiceFilterPanel = new System.Windows.Forms.Panel();
            this._invoiceFilterLabel = new System.Windows.Forms.Label();
            this._invoicePeriodFilter = new System.Windows.Forms.ComboBox();
            this._invoiceDatePicker = new System.Windows.Forms.DateTimePicker();
            this._applyInvoiceFilterButton = new System.Windows.Forms.Button();
            this._activityGrid = new System.Windows.Forms.DataGridView();
            this._activity_column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._user_column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._time_column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._status_column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._activitySubtitle = new System.Windows.Forms.Label();
            this._activityTitle = new System.Windows.Forms.Label();
            this._root.SuspendLayout();
            this._headerPanel.SuspendLayout();
            this._headerIconPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._headerIcon)).BeginInit();
            this._contentLayout.SuspendLayout();
            this._statsLayout.SuspendLayout();
            this._revenueSummaryPanel.SuspendLayout();
            this._revenueSummaryLayout.SuspendLayout();
            this._revenueFilterPanel.SuspendLayout();
            this._revenueCard.SuspendLayout();
            this._ordersCard.SuspendLayout();
            this._stockCard.SuspendLayout();
            this._customersCard.SuspendLayout();
            this._activityCard.SuspendLayout();
            this._invoiceFilterPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._revenueSummaryChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._activityGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // _root
            // 
            this._root.ColumnCount = 1;
            this._root.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._root.Controls.Add(this._headerPanel, 0, 0);
            this._root.Controls.Add(this._contentLayout, 0, 1);
            this._root.Dock = System.Windows.Forms.DockStyle.Fill;
            this._root.Location = new System.Drawing.Point(0, 0);
            this._root.Name = "_root";
            this._root.Padding = new System.Windows.Forms.Padding(20, 16, 20, 20);
            this._root.RowCount = 2;
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 94F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._root.Size = new System.Drawing.Size(1251, 857);
            this._root.TabIndex = 0;
            // 
            // _headerPanel
            // 
            this._headerPanel.BackColor = System.Drawing.Color.White;
            this._headerPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._headerPanel.Controls.Add(this._welcomeLabel);
            this._headerPanel.Controls.Add(this._titleLabel);
            this._headerPanel.Controls.Add(this._headerIconPanel);
            this._headerPanel.Controls.Add(this._expiryBatchesLink);
            this._headerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._headerPanel.Location = new System.Drawing.Point(23, 19);
            this._headerPanel.Name = "_headerPanel";
            this._headerPanel.Size = new System.Drawing.Size(1205, 88);
            this._headerPanel.TabIndex = 0;
            //
            // _expiryBatchesLink
            //
            this._expiryBatchesLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._expiryBatchesLink.AutoSize = true;
            this._expiryBatchesLink.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._expiryBatchesLink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._expiryBatchesLink.Location = new System.Drawing.Point(820, 62);
            this._expiryBatchesLink.Name = "_expiryBatchesLink";
            this._expiryBatchesLink.Text = "Xem lô cận hạn / hết hạn →";
            this._expiryBatchesLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ExpiryBatchesLink_LinkClicked);
            // 
            // _welcomeLabel
            // 
            this._welcomeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._welcomeLabel.AutoEllipsis = true;
            this._welcomeLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._welcomeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._welcomeLabel.Location = new System.Drawing.Point(80, 52);
            this._welcomeLabel.Name = "_welcomeLabel";
            this._welcomeLabel.Size = new System.Drawing.Size(1098, 34);
            this._welcomeLabel.TabIndex = 2;
            this._welcomeLabel.Text = "Chào mừng bạn quay trở lại";
            this._welcomeLabel.UseMnemonic = false;
            // 
            // _titleLabel
            // 
            this._titleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._titleLabel.AutoEllipsis = true;
            this._titleLabel.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this._titleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._titleLabel.Location = new System.Drawing.Point(80, 12);
            this._titleLabel.Name = "_titleLabel";
            this._titleLabel.Size = new System.Drawing.Size(1098, 36);
            this._titleLabel.TabIndex = 1;
            this._titleLabel.Text = "Tổng quan";
            this._titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._titleLabel.UseMnemonic = false;
            // 
            // _headerIconPanel
            // 
            this._headerIconPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this._headerIconPanel.Controls.Add(this._headerIcon);
            this._headerIconPanel.Location = new System.Drawing.Point(24, 21);
            this._headerIconPanel.Name = "_headerIconPanel";
            this._headerIconPanel.Size = new System.Drawing.Size(44, 44);
            this._headerIconPanel.TabIndex = 0;
            // 
            // _headerIcon
            // 
            this._headerIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this._headerIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._headerIcon.IconChar = FontAwesome.Sharp.IconChar.ChartLine;
            this._headerIcon.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._headerIcon.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._headerIcon.IconSize = 20;
            this._headerIcon.Location = new System.Drawing.Point(12, 12);
            this._headerIcon.Name = "_headerIcon";
            this._headerIcon.Size = new System.Drawing.Size(20, 20);
            this._headerIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._headerIcon.TabIndex = 0;
            this._headerIcon.TabStop = false;
            // 
            // _contentLayout
            // 
            this._contentLayout.ColumnCount = 1;
            this._contentLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._contentLayout.Controls.Add(this._statsLayout, 0, 0);
            this._contentLayout.Controls.Add(this._revenueSummaryPanel, 0, 1);
            this._contentLayout.Controls.Add(this._activityCard, 0, 2);
            this._contentLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._contentLayout.Location = new System.Drawing.Point(23, 113);
            this._contentLayout.Name = "_contentLayout";
            this._contentLayout.RowCount = 3;
            this._contentLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 128F));
            this._contentLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 260F));
            this._contentLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._contentLayout.Size = new System.Drawing.Size(1205, 721);
            this._contentLayout.TabIndex = 1;
            //
            // _revenueSummaryPanel
            //
            this._revenueSummaryPanel.BackColor = System.Drawing.Color.White;
            this._revenueSummaryPanel.Controls.Add(this._revenueSummaryLayout);
            this._revenueSummaryPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._revenueSummaryPanel.Margin = new System.Windows.Forms.Padding(0, 14, 0, 0);
            this._revenueSummaryPanel.Name = "_revenueSummaryPanel";
            this._revenueSummaryPanel.Padding = new System.Windows.Forms.Padding(18);
            this._revenueSummaryPanel.Size = new System.Drawing.Size(1205, 246);
            this._revenueSummaryPanel.TabIndex = 1;
            this._revenueSummaryPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawCardBorder);
            //
            // _revenueSummaryLayout
            //
            this._revenueSummaryLayout.ColumnCount = 1;
            this._revenueSummaryLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._revenueSummaryLayout.Controls.Add(this._chartTitleLabel, 0, 0);
            this._revenueSummaryLayout.Controls.Add(this._revenueFilterPanel, 0, 1);
            this._revenueSummaryLayout.Controls.Add(this._revenueSummaryChart, 0, 2);
            this._revenueSummaryLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._revenueSummaryLayout.Name = "_revenueSummaryLayout";
            this._revenueSummaryLayout.RowCount = 3;
            this._revenueSummaryLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this._revenueSummaryLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this._revenueSummaryLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._revenueSummaryLayout.TabIndex = 0;
            //
            // _chartTitleLabel
            //
            this._chartTitleLabel.AutoSize = true;
            this._chartTitleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._chartTitleLabel.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this._chartTitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._chartTitleLabel.Name = "_chartTitleLabel";
            this._chartTitleLabel.Text = "Doanh thu 7 ngày gần nhất";
            this._chartTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // _revenueSummaryChart
            //
            this._revenueSummaryChart.BackColor = System.Drawing.Color.White;
            this._revenueSummaryChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this._revenueSummaryChart.Name = "_revenueSummaryChart";
            this._revenueSummaryChart.ChartAreas.Add(new System.Windows.Forms.DataVisualization.Charting.ChartArea("RevenueSummaryArea"));
            this._revenueSummaryChart.ChartAreas["RevenueSummaryArea"].BackColor = System.Drawing.Color.White;
            this._revenueSummaryChart.ChartAreas["RevenueSummaryArea"].AxisX.LabelStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._revenueSummaryChart.ChartAreas["RevenueSummaryArea"].AxisX.MajorGrid.Enabled = false;
            this._revenueSummaryChart.ChartAreas["RevenueSummaryArea"].AxisX.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this._revenueSummaryChart.ChartAreas["RevenueSummaryArea"].AxisY.LabelStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._revenueSummaryChart.ChartAreas["RevenueSummaryArea"].AxisY.LabelStyle.Format = "N0";
            this._revenueSummaryChart.ChartAreas["RevenueSummaryArea"].AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this._revenueSummaryChart.ChartAreas["RevenueSummaryArea"].AxisY.LineColor = System.Drawing.Color.Transparent;
            this._revenueSummaryChart.ChartAreas["RevenueSummaryArea"].AxisY.Minimum = 0D;
            this._revenueSummaryChart.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series("Doanh thu")
            {
                ChartArea = "RevenueSummaryArea",
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column,
                Color = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246))))),
                BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235))))),
                BorderWidth = 1,
                IsValueShownAsLabel = false,
                CustomProperties = "PointWidth=0.55"
            });
            //
            // _revenueFilterPanel
            //
            this._revenueFilterPanel.BackColor = System.Drawing.Color.White;
            this._revenueFilterPanel.Controls.Add(this._revenueFilterLabel);
            this._revenueFilterPanel.Controls.Add(this._revenuePeriodFilter);
            this._revenueFilterPanel.Controls.Add(this._revenueDatePicker);
            this._revenueFilterPanel.Controls.Add(this._applyRevenueFilterButton);
            this._revenueFilterPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._revenueFilterPanel.Name = "_revenueFilterPanel";
            this._revenueFilterPanel.TabIndex = 0;
            //
            // _revenueFilterLabel
            //
            this._revenueFilterLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._revenueFilterLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._revenueFilterLabel.Location = new System.Drawing.Point(0, 3);
            this._revenueFilterLabel.Name = "_revenueFilterLabel";
            this._revenueFilterLabel.Size = new System.Drawing.Size(42, 24);
            this._revenueFilterLabel.Text = "Lọc:";
            this._revenueFilterLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._revenueFilterLabel.UseMnemonic = false;
            //
            // _revenuePeriodFilter
            //
            this._revenuePeriodFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._revenuePeriodFilter.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._revenuePeriodFilter.Items.AddRange(new object[] {
            "Ngày",
            "Tuần",
            "Tháng",
            "Năm"});
            this._revenuePeriodFilter.Location = new System.Drawing.Point(42, 2);
            this._revenuePeriodFilter.Name = "_revenuePeriodFilter";
            this._revenuePeriodFilter.Size = new System.Drawing.Size(78, 26);
            this._revenuePeriodFilter.TabIndex = 0;
            this._revenuePeriodFilter.SelectedIndex = 1;
            //
            // _revenueDatePicker
            //
            this._revenueDatePicker.CustomFormat = "dd/MM/yyyy";
            this._revenueDatePicker.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._revenueDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._revenueDatePicker.Location = new System.Drawing.Point(126, 2);
            this._revenueDatePicker.Name = "_revenueDatePicker";
            this._revenueDatePicker.Size = new System.Drawing.Size(112, 26);
            this._revenueDatePicker.TabIndex = 1;
            //
            // _applyRevenueFilterButton
            //
            this._applyRevenueFilterButton.Location = new System.Drawing.Point(244, 1);
            this._applyRevenueFilterButton.Name = "_applyRevenueFilterButton";
            this._applyRevenueFilterButton.Size = new System.Drawing.Size(54, 28);
            this._applyRevenueFilterButton.TabIndex = 2;
            this._applyRevenueFilterButton.Text = "Lọc";
            this._applyRevenueFilterButton.UseVisualStyleBackColor = true;
            this._applyRevenueFilterButton.Click += new System.EventHandler(this.OnApplyRevenueFilterClick);
            // 
            // _statsLayout
            // 
            this._statsLayout.ColumnCount = 4;
            this._statsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this._statsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this._statsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this._statsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this._statsLayout.Controls.Add(this._revenueCard, 0, 0);
            this._statsLayout.Controls.Add(this._ordersCard, 1, 0);
            this._statsLayout.Controls.Add(this._stockCard, 2, 0);
            this._statsLayout.Controls.Add(this._customersCard, 3, 0);
            this._statsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._statsLayout.Location = new System.Drawing.Point(3, 3);
            this._statsLayout.Name = "_statsLayout";
            this._statsLayout.Size = new System.Drawing.Size(1199, 122);
            this._statsLayout.TabIndex = 0;
            // 
            // _revenueCard
            // 
            this._revenueCard.BackColor = System.Drawing.Color.White;
            this._revenueCard.Controls.Add(this._revenueCaption);
            this._revenueCard.Controls.Add(this._revenueValue);
            this._revenueCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._revenueCard.Location = new System.Drawing.Point(0, 0);
            this._revenueCard.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this._revenueCard.Name = "_revenueCard";
            this._revenueCard.Padding = new System.Windows.Forms.Padding(16);
            this._revenueCard.Size = new System.Drawing.Size(289, 122);
            this._revenueCard.TabIndex = 0;
            this._revenueCard.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawCardBorder);
            // 
            // _revenueCaption
            // 
            this._revenueCaption.AutoSize = true;
            this._revenueCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._revenueCaption.Location = new System.Drawing.Point(18, 66);
            this._revenueCaption.Name = "_revenueCaption";
            this._revenueCaption.Size = new System.Drawing.Size(171, 25);
            this._revenueCaption.TabIndex = 0;
            this._revenueCaption.Text = "Doanh thu hôm nay";
            // 
            // _revenueValue
            // 
            this._revenueValue.AutoSize = true;
            this._revenueValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this._revenueValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._revenueValue.Location = new System.Drawing.Point(16, 20);
            this._revenueValue.Name = "_revenueValue";
            this._revenueValue.Size = new System.Drawing.Size(56, 48);
            this._revenueValue.TabIndex = 1;
            this._revenueValue.Text = "—";
            // 
            // _ordersCard
            // 
            this._ordersCard.BackColor = System.Drawing.Color.White;
            this._ordersCard.Controls.Add(this._ordersCaption);
            this._ordersCard.Controls.Add(this._ordersValue);
            this._ordersCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ordersCard.Location = new System.Drawing.Point(303, 0);
            this._ordersCard.Margin = new System.Windows.Forms.Padding(4, 0, 6, 0);
            this._ordersCard.Name = "_ordersCard";
            this._ordersCard.Padding = new System.Windows.Forms.Padding(16);
            this._ordersCard.Size = new System.Drawing.Size(289, 122);
            this._ordersCard.TabIndex = 1;
            this._ordersCard.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawCardBorder);
            // 
            // _ordersCaption
            // 
            this._ordersCaption.AutoSize = true;
            this._ordersCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._ordersCaption.Location = new System.Drawing.Point(18, 66);
            this._ordersCaption.Name = "_ordersCaption";
            this._ordersCaption.Size = new System.Drawing.Size(166, 25);
            this._ordersCaption.TabIndex = 0;
            this._ordersCaption.Text = "Đơn hàng hôm nay";
            // 
            // _ordersValue
            // 
            this._ordersValue.AutoSize = true;
            this._ordersValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this._ordersValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this._ordersValue.Location = new System.Drawing.Point(16, 20);
            this._ordersValue.Name = "_ordersValue";
            this._ordersValue.Size = new System.Drawing.Size(56, 48);
            this._ordersValue.TabIndex = 1;
            this._ordersValue.Text = "—";
            // 
            // _stockCard
            // 
            this._stockCard.BackColor = System.Drawing.Color.White;
            this._stockCard.Controls.Add(this._stockCaption);
            this._stockCard.Controls.Add(this._stockValue);
            this._stockCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._stockCard.Location = new System.Drawing.Point(604, 0);
            this._stockCard.Margin = new System.Windows.Forms.Padding(6, 0, 4, 0);
            this._stockCard.Name = "_stockCard";
            this._stockCard.Padding = new System.Windows.Forms.Padding(16);
            this._stockCard.Size = new System.Drawing.Size(289, 122);
            this._stockCard.TabIndex = 2;
            this._stockCard.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawCardBorder);
            // 
            // _stockCaption
            // 
            this._stockCaption.AutoSize = true;
            this._stockCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._stockCaption.Location = new System.Drawing.Point(18, 66);
            this._stockCaption.Name = "_stockCaption";
            this._stockCaption.Size = new System.Drawing.Size(120, 25);
            this._stockCaption.TabIndex = 0;
            this._stockCaption.Text = "Tổng tồn kho";
            // 
            // _stockValue
            // 
            this._stockValue.AutoSize = true;
            this._stockValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this._stockValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this._stockValue.Location = new System.Drawing.Point(16, 20);
            this._stockValue.Name = "_stockValue";
            this._stockValue.Size = new System.Drawing.Size(56, 48);
            this._stockValue.TabIndex = 1;
            this._stockValue.Text = "—";
            // 
            // _customersCard
            // 
            this._customersCard.BackColor = System.Drawing.Color.White;
            this._customersCard.Controls.Add(this._customersCaption);
            this._customersCard.Controls.Add(this._customersValue);
            this._customersCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._customersCard.Location = new System.Drawing.Point(907, 0);
            this._customersCard.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this._customersCard.Name = "_customersCard";
            this._customersCard.Padding = new System.Windows.Forms.Padding(16);
            this._customersCard.Size = new System.Drawing.Size(292, 122);
            this._customersCard.TabIndex = 3;
            this._customersCard.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawCardBorder);
            // 
            // _customersCaption
            // 
            this._customersCaption.AutoSize = true;
            this._customersCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._customersCaption.Location = new System.Drawing.Point(18, 66);
            this._customersCaption.Name = "_customersCaption";
            this._customersCaption.Size = new System.Drawing.Size(104, 25);
            this._customersCaption.TabIndex = 0;
            this._customersCaption.Text = "Khách hàng";
            // 
            // _customersValue
            // 
            this._customersValue.AutoSize = true;
            this._customersValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this._customersValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(92)))), ((int)(((byte)(246)))));
            this._customersValue.Location = new System.Drawing.Point(16, 20);
            this._customersValue.Name = "_customersValue";
            this._customersValue.Size = new System.Drawing.Size(56, 48);
            this._customersValue.TabIndex = 1;
            this._customersValue.Text = "—";
            // 
            // _activityCard
            // 
            this._activityCard.BackColor = System.Drawing.Color.White;
            this._activityCard.Controls.Add(this._activityGrid);
            this._activityCard.Controls.Add(this._activitySubtitle);
            this._activityCard.Controls.Add(this._activityTitle);
            this._activityCard.Controls.Add(this._invoiceFilterPanel);
            this._activityCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._activityCard.Location = new System.Drawing.Point(0, 142);
            this._activityCard.Margin = new System.Windows.Forms.Padding(0, 14, 0, 0);
            this._activityCard.Name = "_activityCard";
            this._activityCard.Padding = new System.Windows.Forms.Padding(18);
            this._activityCard.Size = new System.Drawing.Size(1205, 579);
            this._activityCard.TabIndex = 1;
            this._activityCard.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawCardBorder);
            this._activityCard.SizeChanged += new System.EventHandler(this.OnActivityCardSizeChanged);
            // 
            // _activityGrid
            // 
            this._activityGrid.AllowUserToAddRows = false;
            this._activityGrid.AllowUserToDeleteRows = false;
            this._activityGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._activityGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._activityGrid.BackgroundColor = System.Drawing.Color.White;
            this._activityGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._activityGrid.ColumnHeadersHeight = 36;
            this._activityGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._activity_column,
            this._user_column,
            this._time_column,
            this._status_column});
            this._activityGrid.Location = new System.Drawing.Point(18, 80);
            this._activityGrid.Name = "_activityGrid";
            this._activityGrid.ReadOnly = true;
            this._activityGrid.RowHeadersVisible = false;
            this._activityGrid.RowHeadersWidth = 62;
            this._activityGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._activityGrid.Size = new System.Drawing.Size(1169, 481);
            this._activityGrid.TabIndex = 0;
            // 
            // Activity
            // 
            this._activity_column.HeaderText = "Hoạt động";
            this._activity_column.MinimumWidth = 8;
            this._activity_column.Name = "Activity";
            this._activity_column.ReadOnly = true;
            // 
            // User
            // 
            this._user_column.HeaderText = "Người thực hiện";
            this._user_column.MinimumWidth = 8;
            this._user_column.Name = "User";
            this._user_column.ReadOnly = true;
            // 
            // Time
            // 
            this._time_column.HeaderText = "Thời gian";
            this._time_column.MinimumWidth = 8;
            this._time_column.Name = "Time";
            this._time_column.ReadOnly = true;
            // 
            // Status
            // 
            this._status_column.HeaderText = "Trạng thái";
            this._status_column.MinimumWidth = 8;
            this._status_column.Name = "Status";
            this._status_column.ReadOnly = true;
            // 
            // _activitySubtitle
            // 
            this._activitySubtitle.AutoSize = true;
            this._activitySubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._activitySubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._activitySubtitle.Location = new System.Drawing.Point(20, 45);
            this._activitySubtitle.Name = "_activitySubtitle";
            this._activitySubtitle.Size = new System.Drawing.Size(404, 25);
            this._activitySubtitle.TabIndex = 1;
            this._activitySubtitle.Text = "Các giao dịch và sự kiện mới nhất trong hệ thống";
            // 
            // _activityTitle
            // 
            this._activityTitle.AutoSize = true;
            this._activityTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this._activityTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._activityTitle.Location = new System.Drawing.Point(18, 14);
            this._activityTitle.Name = "_activityTitle";
            this._activityTitle.Size = new System.Drawing.Size(250, 36);
            this._activityTitle.TabIndex = 2;
            this._activityTitle.Text = "Hoạt động gần đây";
            //
            // _invoiceFilterPanel
            //
            this._invoiceFilterPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._invoiceFilterPanel.BackColor = System.Drawing.Color.White;
            this._invoiceFilterPanel.Controls.Add(this._invoiceFilterLabel);
            this._invoiceFilterPanel.Controls.Add(this._invoicePeriodFilter);
            this._invoiceFilterPanel.Controls.Add(this._invoiceDatePicker);
            this._invoiceFilterPanel.Controls.Add(this._applyInvoiceFilterButton);
            this._invoiceFilterPanel.Location = new System.Drawing.Point(837, 14);
            this._invoiceFilterPanel.Name = "_invoiceFilterPanel";
            this._invoiceFilterPanel.Size = new System.Drawing.Size(350, 32);
            this._invoiceFilterPanel.TabIndex = 3;
            this._invoiceFilterPanel.Visible = true;
            //
            // _invoiceFilterLabel
            //
            this._invoiceFilterLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._invoiceFilterLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._invoiceFilterLabel.Location = new System.Drawing.Point(0, 3);
            this._invoiceFilterLabel.Name = "_invoiceFilterLabel";
            this._invoiceFilterLabel.Size = new System.Drawing.Size(60, 24);
            this._invoiceFilterLabel.Text = "Lọc theo:";
            this._invoiceFilterLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._invoiceFilterLabel.UseMnemonic = false;
            //
            // _invoicePeriodFilter
            //
            this._invoicePeriodFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._invoicePeriodFilter.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._invoicePeriodFilter.Items.AddRange(new object[] {
            "Tất cả",
            "Ngày",
            "Tuần",
            "Tháng",
            "Năm"});
            this._invoicePeriodFilter.Location = new System.Drawing.Point(64, 2);
            this._invoicePeriodFilter.Name = "_invoicePeriodFilter";
            this._invoicePeriodFilter.Size = new System.Drawing.Size(88, 26);
            this._invoicePeriodFilter.TabIndex = 0;
            this._invoicePeriodFilter.SelectedIndex = 0;
            //
            // _invoiceDatePicker
            //
            this._invoiceDatePicker.CustomFormat = "dd/MM/yyyy";
            this._invoiceDatePicker.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._invoiceDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._invoiceDatePicker.Location = new System.Drawing.Point(158, 2);
            this._invoiceDatePicker.Name = "_invoiceDatePicker";
            this._invoiceDatePicker.Size = new System.Drawing.Size(112, 26);
            this._invoiceDatePicker.TabIndex = 1;
            //
            // _applyInvoiceFilterButton
            //
            this._applyInvoiceFilterButton.Location = new System.Drawing.Point(276, 1);
            this._applyInvoiceFilterButton.Name = "_applyInvoiceFilterButton";
            this._applyInvoiceFilterButton.Size = new System.Drawing.Size(66, 28);
            this._applyInvoiceFilterButton.TabIndex = 2;
            this._applyInvoiceFilterButton.Text = "Lọc";
            this._applyInvoiceFilterButton.UseVisualStyleBackColor = true;
            this._applyInvoiceFilterButton.Click += new System.EventHandler(this.OnApplyInvoiceFilterClick);
            // 
            // ucDashboard
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.Controls.Add(this._root);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ucDashboard";
            this.Size = new System.Drawing.Size(1251, 857);
            this.Load += new System.EventHandler(this.Dashboard_Load);
            this.VisibleChanged += new System.EventHandler(this.Dashboard_VisibleChanged);
            this._root.ResumeLayout(false);
            this._headerPanel.ResumeLayout(false);
            this._headerIconPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._headerIcon)).EndInit();
            this._contentLayout.ResumeLayout(false);
            this._statsLayout.ResumeLayout(false);
            this._revenueCard.ResumeLayout(false);
            this._revenueCard.PerformLayout();
            this._ordersCard.ResumeLayout(false);
            this._ordersCard.PerformLayout();
            this._stockCard.ResumeLayout(false);
            this._stockCard.PerformLayout();
            this._customersCard.ResumeLayout(false);
            this._customersCard.PerformLayout();
            this._revenueFilterPanel.ResumeLayout(false);
            this._revenueSummaryLayout.ResumeLayout(false);
            this._revenueSummaryPanel.ResumeLayout(false);
            this._activityCard.ResumeLayout(false);
            this._activityCard.PerformLayout();
            this._invoiceFilterPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._revenueSummaryChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._activityGrid)).EndInit();
            this.ResumeLayout(false);

        }

        private DataGridViewTextBoxColumn _activity_column;
        private DataGridViewTextBoxColumn _user_column;
        private DataGridViewTextBoxColumn _time_column;
        private DataGridViewTextBoxColumn _status_column;
    }
}
