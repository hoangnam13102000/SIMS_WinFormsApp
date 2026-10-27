using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SIMS_WinFormsApp.Views.Reports
{
    partial class frmChartDashboard
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel _root;
        private Panel _header;
        private Label _title;
        private Label _subtitle;
        private FlowLayoutPanel _filters;
        private Label _fromLabel;
        private Label _toLabel;
        private DateTimePicker _fromDate;
        private DateTimePicker _toDate;
        private ComboBox _periodFilter;
        private Button _applyFilter;
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this._root = new System.Windows.Forms.TableLayoutPanel();
            this._header = new System.Windows.Forms.Panel();
            this._subtitle = new System.Windows.Forms.Label();
            this._title = new System.Windows.Forms.Label();
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
            this._productNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._quantityColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._amountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._root.SuspendLayout();
            this._header.SuspendLayout();
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
            this._root.Name = "_root";
            this._root.Padding = new System.Windows.Forms.Padding(24);
            this._root.RowCount = 5;
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 76F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            this._root.Size = new System.Drawing.Size(1180, 800);
            this._root.TabIndex = 0;
            // 
            // _header
            // 
            this._header.Controls.Add(this._subtitle);
            this._header.Controls.Add(this._title);
            this._header.Dock = System.Windows.Forms.DockStyle.Fill;
            this._header.Location = new System.Drawing.Point(27, 27);
            this._header.Name = "_header";
            this._header.Size = new System.Drawing.Size(1126, 70);
            this._header.TabIndex = 0;
            // 
            // _subtitle
            // 
            this._subtitle.AutoSize = true;
            this._subtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._subtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._subtitle.Location = new System.Drawing.Point(2, 43);
            this._subtitle.Name = "_subtitle";
            this._subtitle.Size = new System.Drawing.Size(453, 25);
            this._subtitle.TabIndex = 0;
            this._subtitle.Text = "Tổng quan doanh thu, đơn hàng và sản phẩm bán chạy";
            // 
            // _title
            // 
            this._title.AutoSize = true;
            this._title.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this._title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._title.Location = new System.Drawing.Point(0, 0);
            this._title.Name = "_title";
            this._title.Size = new System.Drawing.Size(413, 60);
            this._title.TabIndex = 1;
            this._title.Text = "Báo cáo doanh thu";
            this._title.Click += new System.EventHandler(this._title_Click);
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
            this._filters.Location = new System.Drawing.Point(27, 103);
            this._filters.Name = "_filters";
            this._filters.Size = new System.Drawing.Size(1126, 42);
            this._filters.TabIndex = 1;
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
            // _fromDate
            // 
            this._fromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this._fromDate.Location = new System.Drawing.Point(74, 3);
            this._fromDate.Name = "_fromDate";
            this._fromDate.Size = new System.Drawing.Size(120, 26);
            this._fromDate.TabIndex = 1;
            // 
            // _toLabel
            // 
            this._toLabel.AutoSize = true;
            this._toLabel.Location = new System.Drawing.Point(213, 9);
            this._toLabel.Margin = new System.Windows.Forms.Padding(16, 9, 6, 0);
            this._toLabel.Name = "_toLabel";
            this._toLabel.Size = new System.Drawing.Size(77, 20);
            this._toLabel.TabIndex = 2;
            this._toLabel.Text = "Đến ngày";
            // 
            // _toDate
            // 
            this._toDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this._toDate.Location = new System.Drawing.Point(299, 3);
            this._toDate.Name = "_toDate";
            this._toDate.Size = new System.Drawing.Size(120, 26);
            this._toDate.TabIndex = 3;
            // 
            // _periodFilter
            // 
            this._periodFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._periodFilter.Items.AddRange(new object[] {
            "Theo ngày",
            "Theo tháng",
            "Theo năm"});
            this._periodFilter.Location = new System.Drawing.Point(425, 3);
            this._periodFilter.Name = "_periodFilter";
            this._periodFilter.Size = new System.Drawing.Size(130, 28);
            this._periodFilter.TabIndex = 4;
            // 
            // _applyFilter
            // 
            this._applyFilter.Location = new System.Drawing.Point(561, 3);
            this._applyFilter.Name = "_applyFilter";
            this._applyFilter.Size = new System.Drawing.Size(94, 30);
            this._applyFilter.TabIndex = 5;
            this._applyFilter.Text = "Áp dụng";
            this._applyFilter.UseVisualStyleBackColor = true;
            // 
            // _summary
            // 
            this._summary.ColumnCount = 3;
            this._summary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333F));
            this._summary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333F));
            this._summary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.334F));
            this._summary.Controls.Add(this._revenueCard, 0, 0);
            this._summary.Controls.Add(this._orderCard, 1, 0);
            this._summary.Controls.Add(this._averageCard, 2, 0);
            this._summary.Dock = System.Windows.Forms.DockStyle.Fill;
            this._summary.Location = new System.Drawing.Point(27, 151);
            this._summary.Name = "_summary";
            this._summary.Size = new System.Drawing.Size(1126, 90);
            this._summary.TabIndex = 2;
            // 
            // _revenueCard
            // 
            this._revenueCard.BackColor = System.Drawing.Color.White;
            this._revenueCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._revenueCard.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._revenueCard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._revenueCard.Location = new System.Drawing.Point(0, 0);
            this._revenueCard.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this._revenueCard.Name = "_revenueCard";
            this._revenueCard.Padding = new System.Windows.Forms.Padding(16);
            this._revenueCard.Size = new System.Drawing.Size(365, 90);
            this._revenueCard.TabIndex = 0;
            this._revenueCard.Text = "TỔNG DOANH THU\r\n—";
            // 
            // _orderCard
            // 
            this._orderCard.BackColor = System.Drawing.Color.White;
            this._orderCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._orderCard.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._orderCard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._orderCard.Location = new System.Drawing.Point(380, 0);
            this._orderCard.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this._orderCard.Name = "_orderCard";
            this._orderCard.Padding = new System.Windows.Forms.Padding(16);
            this._orderCard.Size = new System.Drawing.Size(365, 90);
            this._orderCard.TabIndex = 1;
            this._orderCard.Text = "SỐ ĐƠN HÀNG\r\n—";
            // 
            // _averageCard
            // 
            this._averageCard.BackColor = System.Drawing.Color.White;
            this._averageCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._averageCard.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._averageCard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._averageCard.Location = new System.Drawing.Point(760, 0);
            this._averageCard.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this._averageCard.Name = "_averageCard";
            this._averageCard.Padding = new System.Windows.Forms.Padding(16);
            this._averageCard.Size = new System.Drawing.Size(366, 90);
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
            this._charts.Location = new System.Drawing.Point(27, 247);
            this._charts.Name = "_charts";
            this._charts.RowCount = 1;
            this._charts.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._charts.Size = new System.Drawing.Size(1126, 356);
            this._charts.TabIndex = 3;
            // 
            // _revenueChartPlaceholder
            // 
            this._revenueChartPlaceholder.BackColor = System.Drawing.Color.White;
            this._revenueChartPlaceholder.Controls.Add(this._revenueTrendChart);
            this._revenueChartPlaceholder.Controls.Add(this._revenueChartTitle);
            this._revenueChartPlaceholder.Dock = System.Windows.Forms.DockStyle.Fill;
            this._revenueChartPlaceholder.Location = new System.Drawing.Point(0, 0);
            this._revenueChartPlaceholder.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this._revenueChartPlaceholder.Name = "_revenueChartPlaceholder";
            this._revenueChartPlaceholder.Padding = new System.Windows.Forms.Padding(12);
            this._revenueChartPlaceholder.Size = new System.Drawing.Size(690, 356);
            this._revenueChartPlaceholder.TabIndex = 0;
            // 
            // _revenueTrendChart
            // 
            this._revenueTrendChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this._revenueTrendChart.Location = new System.Drawing.Point(12, 42);
            this._revenueTrendChart.Margin = new System.Windows.Forms.Padding(0);
            this._revenueTrendChart.Name = "_revenueTrendChart";
            this._revenueTrendChart.Size = new System.Drawing.Size(666, 302);
            this._revenueTrendChart.TabIndex = 1;
            this._revenueTrendChart.Text = "Xu hướng doanh thu";
            // 
            // _revenueChartTitle
            // 
            this._revenueChartTitle.AutoSize = true;
            this._revenueChartTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._revenueChartTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._revenueChartTitle.Location = new System.Drawing.Point(12, 12);
            this._revenueChartTitle.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this._revenueChartTitle.Name = "_revenueChartTitle";
            this._revenueChartTitle.Size = new System.Drawing.Size(273, 30);
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
            this._salesChartPlaceholder.Location = new System.Drawing.Point(701, 3);
            this._salesChartPlaceholder.Name = "_salesChartPlaceholder";
            this._salesChartPlaceholder.Padding = new System.Windows.Forms.Padding(12);
            this._salesChartPlaceholder.Size = new System.Drawing.Size(422, 350);
            this._salesChartPlaceholder.TabIndex = 0;
            // 
            // _categoryChart
            // 
            this._categoryChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this._categoryChart.Location = new System.Drawing.Point(12, 42);
            this._categoryChart.Margin = new System.Windows.Forms.Padding(0);
            this._categoryChart.Name = "_categoryChart";
            this._categoryChart.Size = new System.Drawing.Size(398, 296);
            this._categoryChart.TabIndex = 1;
            this._categoryChart.Text = "Cơ cấu doanh thu";
            // 
            // _salesChartTitle
            // 
            this._salesChartTitle.AutoSize = true;
            this._salesChartTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._salesChartTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._salesChartTitle.Location = new System.Drawing.Point(12, 12);
            this._salesChartTitle.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this._salesChartTitle.Name = "_salesChartTitle";
            this._salesChartTitle.Size = new System.Drawing.Size(282, 30);
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
            this._productNameColumn,
            this._quantityColumn,
            this._amountColumn});
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
            this._topProductsGrid.Location = new System.Drawing.Point(27, 609);
            this._topProductsGrid.Name = "_topProductsGrid";
            this._topProductsGrid.ReadOnly = true;
            this._topProductsGrid.RowHeadersVisible = false;
            this._topProductsGrid.RowHeadersWidth = 62;
            this._topProductsGrid.RowTemplate.Height = 25;
            this._topProductsGrid.Size = new System.Drawing.Size(1126, 164);
            this._topProductsGrid.TabIndex = 4;
            // 
            // ProductName
            // 
            this._productNameColumn.HeaderText = "Sản phẩm bán chạy";
            this._productNameColumn.MinimumWidth = 8;
            this._productNameColumn.Name = "ProductName";
            this._productNameColumn.ReadOnly = true;
            // 
            // Quantity
            // 
            this._quantityColumn.HeaderText = "Số lượng";
            this._quantityColumn.MinimumWidth = 8;
            this._quantityColumn.Name = "Quantity";
            this._quantityColumn.ReadOnly = true;
            // 
            // Amount
            // 
            this._amountColumn.HeaderText = "Doanh thu";
            this._amountColumn.MinimumWidth = 8;
            this._amountColumn.Name = "Amount";
            this._amountColumn.ReadOnly = true;
            // 
            // frmChartDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1180, 800);
            this.Controls.Add(this._root);
            this.MinimumSize = new System.Drawing.Size(900, 650);
            this.Name = "frmChartDashboard";
            this.Text = "Báo cáo doanh thu";
            this._root.ResumeLayout(false);
            this._header.ResumeLayout(false);
            this._header.PerformLayout();
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

        private DataGridViewTextBoxColumn _productNameColumn;
        private DataGridViewTextBoxColumn _quantityColumn;
        private DataGridViewTextBoxColumn _amountColumn;
    }
}
