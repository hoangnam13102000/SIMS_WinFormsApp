using System.Drawing;
using System.Windows.Forms;

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
        private SplitContainer _charts;
        private Panel _revenueChartPlaceholder;
        private Panel _salesChartPlaceholder;
        private Label _revenueChartTitle;
        private Label _salesChartTitle;
        private DataGridView _topProductsGrid;
        private DataGridViewTextBoxColumn _productNameColumn;
        private DataGridViewTextBoxColumn _quantityColumn;
        private DataGridViewTextBoxColumn _amountColumn;

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
            this._fromLabel = new Label();
            this._toLabel = new Label();
            this._fromDate = new DateTimePicker();
            this._toDate = new DateTimePicker();
            this._periodFilter = new ComboBox();
            this._applyFilter = new Button();
            this._summary = new TableLayoutPanel();
            this._revenueCard = new Label();
            this._orderCard = new Label();
            this._averageCard = new Label();
            this._charts = new SplitContainer();
            this._revenueChartPlaceholder = new Panel();
            this._salesChartPlaceholder = new Panel();
            this._revenueChartTitle = new Label();
            this._salesChartTitle = new Label();
            this._topProductsGrid = new DataGridView();
            this._productNameColumn = new DataGridViewTextBoxColumn();
            this._quantityColumn = new DataGridViewTextBoxColumn();
            this._amountColumn = new DataGridViewTextBoxColumn();
            this._root.SuspendLayout();
            this._header.SuspendLayout();
            this._filters.SuspendLayout();
            this._summary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._charts)).BeginInit();
            this._charts.Panel1.SuspendLayout();
            this._charts.Panel2.SuspendLayout();
            this._charts.SuspendLayout();
            this._revenueChartPlaceholder.SuspendLayout();
            this._salesChartPlaceholder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._topProductsGrid)).BeginInit();
            this.SuspendLayout();
            //
            // root
            //
            this._root.ColumnCount = 1;
            this._root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this._root.Dock = DockStyle.Fill;
            this._root.Padding = new Padding(24);
            this._root.RowCount = 5;
            this._root.RowStyles.Add(new RowStyle(SizeType.Absolute, 76F));
            this._root.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            this._root.RowStyles.Add(new RowStyle(SizeType.Absolute, 96F));
            this._root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this._root.RowStyles.Add(new RowStyle(SizeType.Absolute, 170F));
            this._root.Controls.Add(this._header, 0, 0);
            this._root.Controls.Add(this._filters, 0, 1);
            this._root.Controls.Add(this._summary, 0, 2);
            this._root.Controls.Add(this._charts, 0, 3);
            this._root.Controls.Add(this._topProductsGrid, 0, 4);
            //
            // header
            //
            this._header.Controls.Add(this._subtitle);
            this._header.Controls.Add(this._title);
            this._header.Dock = DockStyle.Fill;
            this._header.Name = "_header";
            this._title.AutoSize = true;
            this._title.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            this._title.ForeColor = Color.FromArgb(15, 23, 42);
            this._title.Location = new Point(0, 0);
            this._title.Text = "Báo cáo doanh thu";
            this._subtitle.AutoSize = true;
            this._subtitle.Font = new Font("Segoe UI", 9F);
            this._subtitle.ForeColor = Color.FromArgb(100, 116, 139);
            this._subtitle.Location = new Point(2, 43);
            this._subtitle.Text = "Tổng quan doanh thu, đơn hàng và sản phẩm bán chạy";
            //
            // filters
            //
            this._filters.Controls.Add(this._fromLabel);
            this._filters.Controls.Add(this._fromDate);
            this._filters.Controls.Add(this._toLabel);
            this._filters.Controls.Add(this._toDate);
            this._filters.Controls.Add(this._periodFilter);
            this._filters.Controls.Add(this._applyFilter);
            this._filters.Dock = DockStyle.Fill;
            this._fromDate.Format = DateTimePickerFormat.Short;
            this._fromDate.Name = "_fromDate";
            this._fromDate.Size = new Size(120, 27);
            this._fromLabel.AutoSize = true;
            this._fromLabel.Margin = new Padding(0, 9, 6, 0);
            this._fromLabel.Name = "_fromLabel";
            this._fromLabel.Text = "Từ ngày";
            this._toDate.Format = DateTimePickerFormat.Short;
            this._toDate.Name = "_toDate";
            this._toDate.Size = new Size(120, 27);
            this._toLabel.AutoSize = true;
            this._toLabel.Margin = new Padding(16, 9, 6, 0);
            this._toLabel.Name = "_toLabel";
            this._toLabel.Text = "Đến ngày";
            this._periodFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            this._periodFilter.Items.AddRange(new object[] { "Theo ngày", "Theo tháng", "Theo năm" });
            this._periodFilter.Name = "_periodFilter";
            this._periodFilter.Size = new Size(130, 28);
            this._applyFilter.Name = "_applyFilter";
            this._applyFilter.Size = new Size(94, 30);
            this._applyFilter.Text = "Áp dụng";
            this._applyFilter.UseVisualStyleBackColor = true;
            //
            // summary
            //
            this._summary.ColumnCount = 3;
            this._summary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.333F));
            this._summary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.333F));
            this._summary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.334F));
            this._summary.Dock = DockStyle.Fill;
            this._summary.Controls.Add(this._revenueCard, 0, 0);
            this._summary.Controls.Add(this._orderCard, 1, 0);
            this._summary.Controls.Add(this._averageCard, 2, 0);
            this._revenueCard.BackColor = Color.White;
            this._revenueCard.Dock = DockStyle.Fill;
            this._revenueCard.Margin = new Padding(0, 0, 10, 0);
            this._revenueCard.Padding = new Padding(16);
            this._revenueCard.Text = "TỔNG DOANH THU\r\n—";
            this._orderCard.BackColor = Color.White;
            this._orderCard.Dock = DockStyle.Fill;
            this._orderCard.Margin = new Padding(5, 0, 5, 0);
            this._orderCard.Padding = new Padding(16);
            this._orderCard.Text = "SỐ ĐƠN HÀNG\r\n—";
            this._averageCard.BackColor = Color.White;
            this._averageCard.Dock = DockStyle.Fill;
            this._averageCard.Margin = new Padding(10, 0, 0, 0);
            this._averageCard.Padding = new Padding(16);
            this._averageCard.Text = "GIÁ TRỊ TRUNG BÌNH\r\n—";
            //
            // charts
            //
            this._charts.Dock = DockStyle.Fill;
            this._charts.Name = "_charts";
            this._charts.Orientation = Orientation.Vertical;
            this._charts.Panel1.Controls.Add(this._revenueChartPlaceholder);
            this._charts.Panel2.Controls.Add(this._salesChartPlaceholder);
            this._revenueChartPlaceholder.BackColor = Color.White;
            this._revenueChartPlaceholder.Controls.Add(this._revenueChartTitle);
            this._revenueChartPlaceholder.Dock = DockStyle.Fill;
            this._revenueChartPlaceholder.Margin = new Padding(0, 0, 8, 0);
            this._revenueChartTitle.AutoSize = true;
            this._revenueChartTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this._revenueChartTitle.Location = new Point(16, 14);
            this._revenueChartTitle.Text = "Doanh thu theo thời gian";
            this._salesChartPlaceholder.BackColor = Color.White;
            this._salesChartPlaceholder.Controls.Add(this._salesChartTitle);
            this._salesChartPlaceholder.Dock = DockStyle.Fill;
            this._salesChartTitle.AutoSize = true;
            this._salesChartTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this._salesChartTitle.Location = new Point(16, 14);
            this._salesChartTitle.Text = "Cơ cấu bán hàng";
            //
            // top products
            //
            this._topProductsGrid.AllowUserToAddRows = false;
            this._topProductsGrid.AllowUserToDeleteRows = false;
            this._topProductsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this._topProductsGrid.BackgroundColor = Color.White;
            this._topProductsGrid.ColumnHeadersHeight = 32;
            this._topProductsGrid.Columns.AddRange(new DataGridViewColumn[] {
                this._productNameColumn, this._quantityColumn, this._amountColumn});
            this._topProductsGrid.Dock = DockStyle.Fill;
            this._topProductsGrid.ReadOnly = true;
            this._topProductsGrid.RowHeadersVisible = false;
            this._productNameColumn.HeaderText = "Sản phẩm bán chạy";
            this._productNameColumn.Name = "ProductName";
            this._quantityColumn.HeaderText = "Số lượng";
            this._quantityColumn.Name = "Quantity";
            this._amountColumn.HeaderText = "Doanh thu";
            this._amountColumn.Name = "Amount";
            //
            // form
            //
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(244, 247, 250);
            this.ClientSize = new Size(1180, 800);
            this.Controls.Add(this._root);
            this.MinimumSize = new Size(900, 650);
            this.Name = "frmChartDashboard";
            this.Text = "Báo cáo doanh thu";
            this._root.ResumeLayout(false);
            this._header.ResumeLayout(false);
            this._header.PerformLayout();
            this._filters.ResumeLayout(false);
            this._filters.PerformLayout();
            this._summary.ResumeLayout(false);
            this._charts.Panel1.ResumeLayout(false);
            this._charts.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._charts)).EndInit();
            this._charts.ResumeLayout(false);
            this._revenueChartPlaceholder.ResumeLayout(false);
            this._revenueChartPlaceholder.PerformLayout();
            this._salesChartPlaceholder.ResumeLayout(false);
            this._salesChartPlaceholder.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._topProductsGrid)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
