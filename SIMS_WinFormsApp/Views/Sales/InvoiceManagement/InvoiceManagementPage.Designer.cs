using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Views.Sales.InvoiceManagement
{
    partial class InvoiceManagementPage
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
        protected override void Dispose(bool disposing)
        {
            if (disposing && _themeSubscribed)
            {
                ThemeManager.Instance.ThemeChanged -= OnThemeChanged;
                _themeSubscribed = false;
            }
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InvoiceManagementPage));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this._root = new System.Windows.Forms.TableLayoutPanel();
            this._header = new System.Windows.Forms.Panel();
            this._subtitle = new System.Windows.Forms.Label();
            this._title = new System.Windows.Forms.Label();
            this._filters = new System.Windows.Forms.FlowLayoutPanel();
            this._searchBox = new System.Windows.Forms.TextBox();
            this._fromDate = new System.Windows.Forms.DateTimePicker();
            this._toDate = new System.Windows.Forms.DateTimePicker();
            this._statusFilter = new System.Windows.Forms.ComboBox();
            this._exportButton = new System.Windows.Forms.Button();
            this._invoicesGrid = new System.Windows.Forms.DataGridView();
            this.InvoiceId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Invoice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Customer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cashier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Actions = new System.Windows.Forms.DataGridViewImageColumn();
            this.Print = new System.Windows.Forms.DataGridViewImageColumn();
            this._root.SuspendLayout();
            this._header.SuspendLayout();
            this._filters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._invoicesGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // _root
            // 
            this._root.ColumnCount = 1;
            this._root.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._root.Controls.Add(this._header, 0, 0);
            this._root.Controls.Add(this._filters, 0, 1);
            this._root.Controls.Add(this._invoicesGrid, 0, 2);
            this._root.Dock = System.Windows.Forms.DockStyle.Fill;
            this._root.Location = new System.Drawing.Point(0, 0);
            this._root.Name = "_root";
            this._root.Padding = new System.Windows.Forms.Padding(24);
            this._root.RowCount = 3;
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._root.Size = new System.Drawing.Size(1180, 760);
            this._root.TabIndex = 0;
            // 
            // _header
            // 
            this._header.Controls.Add(this._subtitle);
            this._header.Controls.Add(this._title);
            this._header.Dock = System.Windows.Forms.DockStyle.Fill;
            this._header.Location = new System.Drawing.Point(27, 27);
            this._header.Name = "_header";
            this._header.Size = new System.Drawing.Size(1126, 82);
            this._header.TabIndex = 0;
            // 
            // _subtitle
            // 
            this._subtitle.AutoSize = true;
            this._subtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._subtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._subtitle.Location = new System.Drawing.Point(-5, 57);
            this._subtitle.Name = "_subtitle";
            this._subtitle.Size = new System.Drawing.Size(427, 25);
            this._subtitle.TabIndex = 0;
            this._subtitle.Text = "Tra cứu, xem chi tiết và xem trước hóa đơn bán hàng";
            this._subtitle.Click += new System.EventHandler(this._subtitle_Click);
            // 
            // _title
            // 
            this._title.AutoSize = true;
            this._title.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this._title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._title.Location = new System.Drawing.Point(0, 0);
            this._title.Name = "_title";
            this._title.Size = new System.Drawing.Size(369, 60);
            this._title.TabIndex = 1;
            this._title.Text = "Quản lý hóa đơn";
            // 
            // _filters
            // 
            this._filters.Controls.Add(this._searchBox);
            this._filters.Controls.Add(this._fromDate);
            this._filters.Controls.Add(this._toDate);
            this._filters.Controls.Add(this._statusFilter);
            this._filters.Controls.Add(this._exportButton);
            this._filters.Dock = System.Windows.Forms.DockStyle.Fill;
            this._filters.Location = new System.Drawing.Point(27, 115);
            this._filters.Name = "_filters";
            this._filters.Size = new System.Drawing.Size(1126, 46);
            this._filters.TabIndex = 1;
            this._filters.WrapContents = false;
            // 
            // _searchBox
            // 
            this._searchBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._searchBox.Location = new System.Drawing.Point(0, 4);
            this._searchBox.Margin = new System.Windows.Forms.Padding(0, 4, 8, 0);
            this._searchBox.Name = "_searchBox";
            this._searchBox.Size = new System.Drawing.Size(190, 31);
            this._searchBox.TabIndex = 0;
            // 
            // _fromDate
            // 
            this._fromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this._fromDate.Location = new System.Drawing.Point(202, 4);
            this._fromDate.Margin = new System.Windows.Forms.Padding(4);
            this._fromDate.Name = "_fromDate";
            this._fromDate.Size = new System.Drawing.Size(105, 26);
            this._fromDate.TabIndex = 1;
            // 
            // _toDate
            // 
            this._toDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this._toDate.Location = new System.Drawing.Point(315, 4);
            this._toDate.Margin = new System.Windows.Forms.Padding(4);
            this._toDate.Name = "_toDate";
            this._toDate.Size = new System.Drawing.Size(105, 26);
            this._toDate.TabIndex = 2;
            // 
            // _statusFilter
            // 
            this._statusFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._statusFilter.Items.AddRange(new object[] {
            "Tất cả trạng thái",
            "Hoàn thành",
            "Đã hủy"});
            this._statusFilter.Location = new System.Drawing.Point(428, 4);
            this._statusFilter.Margin = new System.Windows.Forms.Padding(4);
            this._statusFilter.Name = "_statusFilter";
            this._statusFilter.Size = new System.Drawing.Size(140, 28);
            this._statusFilter.TabIndex = 3;
            // 
            // _exportButton
            // 
            this._exportButton.Location = new System.Drawing.Point(576, 2);
            this._exportButton.Margin = new System.Windows.Forms.Padding(4, 2, 0, 0);
            this._exportButton.Name = "_exportButton";
            this._exportButton.Size = new System.Drawing.Size(92, 30);
            this._exportButton.TabIndex = 4;
            this._exportButton.Text = "Lọc hóa đơn";
            this._exportButton.UseVisualStyleBackColor = true;
            // 
            // _invoicesGrid
            // 
            this._invoicesGrid.AllowUserToAddRows = false;
            this._invoicesGrid.AllowUserToDeleteRows = false;
            this._invoicesGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._invoicesGrid.BackgroundColor = System.Drawing.Color.White;
            this._invoicesGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._invoicesGrid.ColumnHeadersHeight = 40;
            this._invoicesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.InvoiceId,
            this.Invoice,
            this.Date,
            this.Customer,
            this.Cashier,
            this.Total,
            this.Status,
            this.Actions,
            this.Print});
            this._invoicesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._invoicesGrid.Location = new System.Drawing.Point(27, 167);
            this._invoicesGrid.Name = "_invoicesGrid";
            this._invoicesGrid.ReadOnly = true;
            this._invoicesGrid.RowHeadersVisible = false;
            this._invoicesGrid.RowHeadersWidth = 62;
            this._invoicesGrid.Size = new System.Drawing.Size(1126, 566);
            this._invoicesGrid.TabIndex = 2;
            // 
            // InvoiceId
            // 
            this.InvoiceId.MinimumWidth = 8;
            this.InvoiceId.Name = "InvoiceId";
            this.InvoiceId.ReadOnly = true;
            this.InvoiceId.Visible = false;
            // 
            // Invoice
            // 
            this.Invoice.HeaderText = "Mã hóa đơn";
            this.Invoice.MinimumWidth = 8;
            this.Invoice.Name = "Invoice";
            this.Invoice.ReadOnly = true;
            // 
            // Date
            // 
            this.Date.HeaderText = "Ngày tạo";
            this.Date.MinimumWidth = 8;
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            // 
            // Customer
            // 
            this.Customer.HeaderText = "Khách hàng";
            this.Customer.MinimumWidth = 8;
            this.Customer.Name = "Customer";
            this.Customer.ReadOnly = true;
            // 
            // Cashier
            // 
            this.Cashier.HeaderText = "Nhân viên";
            this.Cashier.MinimumWidth = 8;
            this.Cashier.Name = "Cashier";
            this.Cashier.ReadOnly = true;
            // 
            // Total
            // 
            this.Total.HeaderText = "Tổng tiền";
            this.Total.MinimumWidth = 8;
            this.Total.Name = "Total";
            this.Total.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.HeaderText = "Trạng thái";
            this.Status.MinimumWidth = 8;
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // Actions
            // 
            this.Actions.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle3.NullValue")));
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.Actions.DefaultCellStyle = dataGridViewCellStyle3;
            this.Actions.HeaderText = "Thao tác";
            this.Actions.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Actions.MinimumWidth = 78;
            this.Actions.Name = "Actions";
            this.Actions.ReadOnly = true;
            this.Actions.Width = 78;
            // 
            // Print
            // 
            this.Print.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle4.NullValue")));
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.Print.DefaultCellStyle = dataGridViewCellStyle4;
            this.Print.HeaderText = "In";
            this.Print.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Print.MinimumWidth = 52;
            this.Print.Name = "Print";
            this.Print.ReadOnly = true;
            this.Print.Width = 52;
            // 
            // InvoiceManagementPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1180, 760);
            this.Controls.Add(this._root);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "InvoiceManagementPage";
            this.Text = "Quản lý hóa đơn";
            this._root.ResumeLayout(false);
            this._header.ResumeLayout(false);
            this._header.PerformLayout();
            this._filters.ResumeLayout(false);
            this._filters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._invoicesGrid)).EndInit();
            this.ResumeLayout(false);

        }

        private DataGridViewTextBoxColumn InvoiceId;
        private DataGridViewTextBoxColumn Invoice;
        private DataGridViewTextBoxColumn Date;
        private DataGridViewTextBoxColumn Customer;
        private DataGridViewTextBoxColumn Cashier;
        private DataGridViewTextBoxColumn Total;
        private DataGridViewTextBoxColumn Status;
        private DataGridViewImageColumn Actions;
        private DataGridViewImageColumn Print;
    }
}
