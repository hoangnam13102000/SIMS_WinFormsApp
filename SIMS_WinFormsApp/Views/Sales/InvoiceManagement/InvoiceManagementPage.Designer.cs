using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Views.Sales.InvoiceManagement
{
    partial class InvoiceManagementPage
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel _root;
        private Panel _header;
        private Panel _headerIconPanel;
        private IconPictureBox _headerIcon;
        private Label _title;
        private Label _subtitle;
        private FlowLayoutPanel _filters;
        private DateTimePicker _fromDate;
        private DateTimePicker _toDate;
        private TextBox _searchBox;
        private ComboBox _statusFilter;
        private Button _exportButton;
        private Button _refreshButton;
        private Panel _gridCard;
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
            this._headerIconPanel = new System.Windows.Forms.Panel();
            this._headerIcon = new FontAwesome.Sharp.IconPictureBox();
            this._subtitle = new System.Windows.Forms.Label();
            this._title = new System.Windows.Forms.Label();
            this._refreshButton = new System.Windows.Forms.Button();
            this._filters = new System.Windows.Forms.FlowLayoutPanel();
            this._searchBox = new System.Windows.Forms.TextBox();
            this._fromDate = new System.Windows.Forms.DateTimePicker();
            this._toDate = new System.Windows.Forms.DateTimePicker();
            this._statusFilter = new System.Windows.Forms.ComboBox();
            this._exportButton = new System.Windows.Forms.Button();
            this._gridCard = new System.Windows.Forms.Panel();
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
            this._viewButton = new System.Windows.Forms.Button();
            this._printButton = new System.Windows.Forms.Button();
            this._root.SuspendLayout();
            this._header.SuspendLayout();
            this._headerIconPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._headerIcon)).BeginInit();
            this._filters.SuspendLayout();
            this._gridCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._invoicesGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // _root
            // 
            this._root.ColumnCount = 1;
            this._root.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._root.Controls.Add(this._header, 0, 0);
            this._root.Controls.Add(this._filters, 0, 1);
            this._root.Controls.Add(this._gridCard, 0, 2);
            this._root.Dock = System.Windows.Forms.DockStyle.Fill;
            this._root.Location = new System.Drawing.Point(0, 0);
            this._root.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._root.Name = "_root";
            this._root.Padding = new System.Windows.Forms.Padding(21, 19, 21, 19);
            this._root.RowCount = 3;
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 86F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 66F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._root.Size = new System.Drawing.Size(1049, 608);
            this._root.TabIndex = 0;
            // 
            // _header
            // 
            this._header.BackColor = System.Drawing.Color.White;
            this._header.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._header.Controls.Add(this._headerIconPanel);
            this._header.Controls.Add(this._subtitle);
            this._header.Controls.Add(this._title);
            this._header.Controls.Add(this._refreshButton);
            this._header.Dock = System.Windows.Forms.DockStyle.Fill;
            this._header.Location = new System.Drawing.Point(21, 19);
            this._header.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this._header.Name = "_header";
            this._header.Size = new System.Drawing.Size(1007, 76);
            this._header.TabIndex = 0;
            // 
            // _headerIconPanel
            // 
            this._headerIconPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(240)))), ((int)(((byte)(254)))));
            this._headerIconPanel.Controls.Add(this._headerIcon);
            this._headerIconPanel.Location = new System.Drawing.Point(18, 16);
            this._headerIconPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._headerIconPanel.Name = "_headerIconPanel";
            this._headerIconPanel.Size = new System.Drawing.Size(50, 45);
            this._headerIconPanel.TabIndex = 0;
            // 
            // _headerIcon
            // 
            this._headerIcon.BackColor = System.Drawing.Color.Transparent;
            this._headerIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._headerIcon.IconChar = FontAwesome.Sharp.IconChar.Receipt;
            this._headerIcon.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._headerIcon.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._headerIcon.IconSize = 22;
            this._headerIcon.Location = new System.Drawing.Point(12, 11);
            this._headerIcon.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._headerIcon.Name = "_headerIcon";
            this._headerIcon.Size = new System.Drawing.Size(25, 22);
            this._headerIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._headerIcon.TabIndex = 0;
            this._headerIcon.TabStop = false;
            // 
            // _subtitle
            // 
            this._subtitle.AutoSize = true;
            this._subtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._subtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._subtitle.Location = new System.Drawing.Point(85, 45);
            this._subtitle.Name = "_subtitle";
            this._subtitle.Size = new System.Drawing.Size(356, 20);
            this._subtitle.TabIndex = 0;
            this._subtitle.Text = "Tra cứu, xem chi tiết và xem trước hóa đơn bán hàng";
            // 
            // _title
            // 
            this._title.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this._title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this._title.Location = new System.Drawing.Point(85, 0);
            this._title.Name = "_title";
            this._title.Size = new System.Drawing.Size(533, 42);
            this._title.TabIndex = 1;
            this._title.Text = "Quản lý hóa đơn";
            this._title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _refreshButton
            // 
            this._refreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._refreshButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._refreshButton.FlatAppearance.BorderSize = 0;
            this._refreshButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this._refreshButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._refreshButton.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this._refreshButton.ForeColor = System.Drawing.Color.White;
            this._refreshButton.Location = new System.Drawing.Point(881, 23);
            this._refreshButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._refreshButton.Name = "_refreshButton";
            this._refreshButton.Size = new System.Drawing.Size(112, 30);
            this._refreshButton.TabIndex = 2;
            this._refreshButton.Text = "Làm mới";
            this._refreshButton.UseVisualStyleBackColor = false;
            // 
            // _filters
            // 
            this._filters.BackColor = System.Drawing.Color.White;
            this._filters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._filters.Controls.Add(this._searchBox);
            this._filters.Controls.Add(this._fromDate);
            this._filters.Controls.Add(this._toDate);
            this._filters.Controls.Add(this._statusFilter);
            this._filters.Controls.Add(this._exportButton);
            this._filters.Controls.Add(this._viewButton);
            this._filters.Controls.Add(this._printButton);
            this._filters.Dock = System.Windows.Forms.DockStyle.Fill;
            this._filters.Location = new System.Drawing.Point(21, 105);
            this._filters.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this._filters.Name = "_filters";
            this._filters.Padding = new System.Windows.Forms.Padding(11, 6, 11, 6);
            this._filters.Size = new System.Drawing.Size(1007, 56);
            this._filters.TabIndex = 1;
            this._filters.WrapContents = false;
            // 
            // _searchBox
            // 
            this._searchBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._searchBox.Location = new System.Drawing.Point(11, 9);
            this._searchBox.Margin = new System.Windows.Forms.Padding(0, 3, 7, 0);
            this._searchBox.Name = "_searchBox";
            this._searchBox.Size = new System.Drawing.Size(169, 27);
            this._searchBox.TabIndex = 0;
            // 
            // _fromDate
            // 
            this._fromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this._fromDate.Location = new System.Drawing.Point(191, 9);
            this._fromDate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._fromDate.Name = "_fromDate";
            this._fromDate.Size = new System.Drawing.Size(94, 22);
            this._fromDate.TabIndex = 1;
            // 
            // _toDate
            // 
            this._toDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this._toDate.Location = new System.Drawing.Point(293, 9);
            this._toDate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._toDate.Name = "_toDate";
            this._toDate.Size = new System.Drawing.Size(94, 22);
            this._toDate.TabIndex = 2;
            // 
            // _statusFilter
            // 
            this._statusFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._statusFilter.Items.AddRange(new object[] {
            "Tất cả trạng thái",
            "Hoàn thành",
            "Đã hủy"});
            this._statusFilter.Location = new System.Drawing.Point(395, 9);
            this._statusFilter.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._statusFilter.Name = "_statusFilter";
            this._statusFilter.Size = new System.Drawing.Size(125, 24);
            this._statusFilter.TabIndex = 3;
            // 
            // _exportButton
            // 
            this._exportButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this._exportButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this._exportButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(72)))), ((int)(((byte)(144)))));
            this._exportButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._exportButton.ForeColor = System.Drawing.Color.White;
            this._exportButton.Location = new System.Drawing.Point(528, 8);
            this._exportButton.Margin = new System.Windows.Forms.Padding(4, 2, 0, 0);
            this._exportButton.Name = "_exportButton";
            this._exportButton.Size = new System.Drawing.Size(96, 24);
            this._exportButton.TabIndex = 4;
            this._exportButton.Text = "Lọc hóa đơn";
            this._exportButton.UseVisualStyleBackColor = false;
            // 
            // _gridCard
            // 
            this._gridCard.BackColor = System.Drawing.Color.White;
            this._gridCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._gridCard.Controls.Add(this._invoicesGrid);
            this._gridCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridCard.Location = new System.Drawing.Point(21, 171);
            this._gridCard.Margin = new System.Windows.Forms.Padding(0);
            this._gridCard.Name = "_gridCard";
            this._gridCard.Size = new System.Drawing.Size(1007, 418);
            this._gridCard.TabIndex = 2;
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
             this.Status});
            this._invoicesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._invoicesGrid.Location = new System.Drawing.Point(0, 0);
            this._invoicesGrid.Margin = new System.Windows.Forms.Padding(1);
            this._invoicesGrid.Name = "_invoicesGrid";
            this._invoicesGrid.ReadOnly = true;
            this._invoicesGrid.RowHeadersVisible = false;
            this._invoicesGrid.RowHeadersWidth = 62;
            this._invoicesGrid.Size = new System.Drawing.Size(1005, 416);
            this._invoicesGrid.TabIndex = 2;
            this._invoicesGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this._invoicesGrid_CellDoubleClick);
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
            // _viewButton
            // 
            this._viewButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._viewButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._viewButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._viewButton.Location = new System.Drawing.Point(627, 9);
            this._viewButton.Name = "_viewButton";
            this._viewButton.Size = new System.Drawing.Size(114, 24);
            this._viewButton.TabIndex = 6;
            this._viewButton.Text = "Xem chi tiết";
            this._viewButton.UseVisualStyleBackColor = true;
            this._viewButton.Click += new System.EventHandler(this._viewButton_Click);
            // 
            // _printButton
            // 
            this._printButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._printButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._printButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._printButton.Location = new System.Drawing.Point(747, 9);
            this._printButton.Name = "_printButton";
            this._printButton.Size = new System.Drawing.Size(75, 23);
            this._printButton.TabIndex = 7;
            this._printButton.Text = "In hóa đơn";
            this._printButton.UseVisualStyleBackColor = true;
            this._printButton.Click += new System.EventHandler(this._printButton_Click);
            // 
            // InvoiceManagementPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1049, 608);
            this.Controls.Add(this._root);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(802, 489);
            this.Name = "InvoiceManagementPage";
            this.Text = "Quản lý hóa đơn";
            this._root.ResumeLayout(false);
            this._header.ResumeLayout(false);
            this._header.PerformLayout();
            this._headerIconPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._headerIcon)).EndInit();
            this._filters.ResumeLayout(false);
            this._filters.PerformLayout();
            this._gridCard.ResumeLayout(false);
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
        private Button _viewButton;
        private Button _printButton;
    }
}
