using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Views.Reports
{
    partial class frmInvoiceReport
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel _root;
        private Panel _header;
        private Label _title;
        private Label _subtitle;
        private FlowLayoutPanel _filters;
        private DateTimePicker _fromDate;
        private DateTimePicker _toDate;
        private TextBox _searchBox;
        private ComboBox _statusFilter;
        private Button _exportButton;
        private DataGridView _invoicesGrid;
        private DataGridViewTextBoxColumn _invoiceColumn;
        private DataGridViewTextBoxColumn _dateColumn;
        private DataGridViewTextBoxColumn _customerColumn;
        private DataGridViewTextBoxColumn _cashierColumn;
        private DataGridViewTextBoxColumn _totalColumn;
        private DataGridViewTextBoxColumn _statusColumn;

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
            this._fromDate = new DateTimePicker();
            this._toDate = new DateTimePicker();
            this._searchBox = new TextBox();
            this._statusFilter = new ComboBox();
            this._exportButton = new Button();
            this._invoicesGrid = new DataGridView();
            this._invoiceColumn = new DataGridViewTextBoxColumn();
            this._dateColumn = new DataGridViewTextBoxColumn();
            this._customerColumn = new DataGridViewTextBoxColumn();
            this._cashierColumn = new DataGridViewTextBoxColumn();
            this._totalColumn = new DataGridViewTextBoxColumn();
            this._statusColumn = new DataGridViewTextBoxColumn();
            this._root.SuspendLayout();
            this._header.SuspendLayout();
            this._filters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._invoicesGrid)).BeginInit();
            this.SuspendLayout();
            //
            // root
            //
            this._root.ColumnCount = 1;
            this._root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this._root.Dock = DockStyle.Fill;
            this._root.Padding = new Padding(24);
            this._root.RowCount = 3;
            this._root.RowStyles.Add(new RowStyle(SizeType.Absolute, 76F));
            this._root.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            this._root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this._root.Controls.Add(this._header, 0, 0);
            this._root.Controls.Add(this._filters, 0, 1);
            this._root.Controls.Add(this._invoicesGrid, 0, 2);
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
            this._title.Text = "Báo cáo hóa đơn";
            this._subtitle.AutoSize = true;
            this._subtitle.Font = new Font("Segoe UI", 9F);
            this._subtitle.ForeColor = Color.FromArgb(100, 116, 139);
            this._subtitle.Location = new Point(2, 43);
            this._subtitle.Text = "Tra cứu hóa đơn bán hàng theo thời gian và trạng thái";
            //
            // filters
            //
            this._filters.Controls.Add(this._searchBox);
            this._filters.Controls.Add(this._fromDate);
            this._filters.Controls.Add(this._toDate);
            this._filters.Controls.Add(this._statusFilter);
            this._filters.Controls.Add(this._exportButton);
            this._filters.Dock = DockStyle.Fill;
            this._searchBox.Font = new Font("Segoe UI", 9F);
            this._searchBox.Margin = new Padding(0, 4, 8, 0);
            this._searchBox.Name = "_searchBox";
            this._searchBox.Text = "Tìm mã hóa đơn, khách hàng...";
            this._searchBox.Size = new Size(240, 28);
            this._fromDate.Format = DateTimePickerFormat.Short;
            this._fromDate.Margin = new Padding(4);
            this._fromDate.Name = "_fromDate";
            this._fromDate.Size = new Size(125, 27);
            this._toDate.Format = DateTimePickerFormat.Short;
            this._toDate.Margin = new Padding(4);
            this._toDate.Name = "_toDate";
            this._toDate.Size = new Size(125, 27);
            this._statusFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            this._statusFilter.Items.AddRange(new object[] { "Tất cả trạng thái", "Hoàn thành", "Đã hủy" });
            this._statusFilter.Margin = new Padding(4);
            this._statusFilter.Name = "_statusFilter";
            this._statusFilter.Size = new Size(150, 28);
            this._exportButton.Margin = new Padding(4, 2, 0, 0);
            this._exportButton.Name = "_exportButton";
            this._exportButton.Size = new Size(100, 30);
            this._exportButton.Text = "Xuất báo cáo";
            this._exportButton.UseVisualStyleBackColor = true;
            //
            // grid
            //
            this._invoicesGrid.AllowUserToAddRows = false;
            this._invoicesGrid.AllowUserToDeleteRows = false;
            this._invoicesGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this._invoicesGrid.BackgroundColor = Color.White;
            this._invoicesGrid.BorderStyle = BorderStyle.None;
            this._invoicesGrid.ColumnHeadersHeight = 40;
            this._invoicesGrid.Columns.AddRange(new DataGridViewColumn[] {
                this._invoiceColumn, this._dateColumn, this._customerColumn,
                this._cashierColumn, this._totalColumn, this._statusColumn});
            this._invoicesGrid.Dock = DockStyle.Fill;
            this._invoicesGrid.ReadOnly = true;
            this._invoicesGrid.RowHeadersVisible = false;
            this._invoiceColumn.HeaderText = "Mã hóa đơn";
            this._invoiceColumn.Name = "Invoice";
            this._dateColumn.HeaderText = "Ngày tạo";
            this._dateColumn.Name = "Date";
            this._customerColumn.HeaderText = "Khách hàng";
            this._customerColumn.Name = "Customer";
            this._cashierColumn.HeaderText = "Nhân viên";
            this._cashierColumn.Name = "Cashier";
            this._totalColumn.HeaderText = "Tổng tiền";
            this._totalColumn.Name = "Total";
            this._statusColumn.HeaderText = "Trạng thái";
            this._statusColumn.Name = "Status";
            //
            // form
            //
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(244, 247, 250);
            this.ClientSize = new Size(1180, 760);
            this.Controls.Add(this._root);
            this.MinimumSize = new Size(900, 600);
            this.Name = "frmInvoiceReport";
            this.Text = "Báo cáo hóa đơn";
            this._root.ResumeLayout(false);
            this._header.ResumeLayout(false);
            this._header.PerformLayout();
            this._filters.ResumeLayout(false);
            this._filters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._invoicesGrid)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
