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
            this._filters = new System.Windows.Forms.FlowLayoutPanel();
            this._searchBox = new System.Windows.Forms.TextBox();
            this._fromDate = new System.Windows.Forms.DateTimePicker();
            this._toDate = new System.Windows.Forms.DateTimePicker();
            this._statusFilter = new System.Windows.Forms.ComboBox();
            this._exportButton = new System.Windows.Forms.Button();
            this._refreshButton = new System.Windows.Forms.Button();
            this._gridCard = new System.Windows.Forms.Panel();
            this._invoicesGrid = new System.Windows.Forms.DataGridView();
            this._invoice_id_column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._invoice_column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._date_column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._customer_column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._cashier_column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._total_column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._status_column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._actions_column = new System.Windows.Forms.DataGridViewImageColumn();
            this._print_column = new System.Windows.Forms.DataGridViewImageColumn();
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
            this._root.Name = "_root";
            this._root.Padding = new System.Windows.Forms.Padding(24);
            this._root.RowCount = 3;
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._root.Size = new System.Drawing.Size(1180, 760);
            this._root.TabIndex = 0;
            // 
            // _header
            // 
            this._header.BackColor = System.Drawing.Color.White;
            this._header.Controls.Add(this._headerIconPanel);
            this._header.Controls.Add(this._subtitle);
            this._header.Controls.Add(this._title);
            this._header.Controls.Add(this._refreshButton);
            this._header.Dock = System.Windows.Forms.DockStyle.Fill;
            this._header.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this._header.Location = new System.Drawing.Point(24, 24);
            this._header.Name = "_header";
            this._header.Size = new System.Drawing.Size(1132, 96);
            this._header.TabIndex = 0;
            //
            // _headerIconPanel
            //
            this._headerIconPanel.BackColor = System.Drawing.Color.FromArgb(232, 240, 254);
            this._headerIconPanel.Controls.Add(this._headerIcon);
            this._headerIconPanel.Location = new System.Drawing.Point(20, 20);
            this._headerIconPanel.Name = "_headerIconPanel";
            this._headerIconPanel.Size = new System.Drawing.Size(56, 56);
            //
            // _headerIcon
            //
            this._headerIcon.BackColor = System.Drawing.Color.Transparent;
            this._headerIcon.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this._headerIcon.IconChar = FontAwesome.Sharp.IconChar.Receipt;
            this._headerIcon.IconColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this._headerIcon.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._headerIcon.IconSize = 28;
            this._headerIcon.Location = new System.Drawing.Point(14, 14);
            this._headerIcon.Name = "_headerIcon";
            this._headerIcon.Size = new System.Drawing.Size(28, 28);
            this._headerIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._headerIcon.TabStop = false;
            // 
            // _subtitle
            // 
            this._subtitle.AutoSize = true;
            this._subtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._subtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._subtitle.Location = new System.Drawing.Point(96, 56);
            this._subtitle.Name = "_subtitle";
            this._subtitle.Size = new System.Drawing.Size(427, 25);
            this._subtitle.TabIndex = 0;
            this._subtitle.Text = "Tra cứu, xem chi tiết và xem trước hóa đơn bán hàng";
            this._subtitle.Click += new System.EventHandler(this._subtitle_Click);
            // 
            // _title
            // 
            this._title.AutoSize = false;
            this._title.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this._title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this._title.Location = new System.Drawing.Point(96, 18);
            this._title.Name = "_title";
            this._title.Size = new System.Drawing.Size(600, 34);
            this._title.TabIndex = 1;
            this._title.Text = "Quản lý hóa đơn";
            this._title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _filters
            // 
            this._filters.Controls.Add(this._searchBox);
            this._filters.Controls.Add(this._fromDate);
            this._filters.Controls.Add(this._toDate);
            this._filters.Controls.Add(this._statusFilter);
            this._filters.Controls.Add(this._exportButton);
            this._filters.Dock = System.Windows.Forms.DockStyle.Fill;
            this._filters.BackColor = System.Drawing.Color.White;
            this._filters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._filters.Location = new System.Drawing.Point(24, 132);
            this._filters.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this._filters.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this._filters.Name = "_filters";
            this._filters.Size = new System.Drawing.Size(1132, 70);
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
            // _refreshButton
            //
            this._refreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._refreshButton.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this._refreshButton.FlatAppearance.BorderSize = 0;
            this._refreshButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(29, 78, 216);
            this._refreshButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._refreshButton.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this._refreshButton.ForeColor = System.Drawing.Color.White;
            this._refreshButton.Location = new System.Drawing.Point(990, 29);
            this._refreshButton.Name = "_refreshButton";
            this._refreshButton.Size = new System.Drawing.Size(126, 38);
            this._refreshButton.Text = "Làm mới";
            this._refreshButton.UseVisualStyleBackColor = false;
            //
            // _gridCard
            //
            this._gridCard.BackColor = System.Drawing.Color.White;
            this._gridCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._gridCard.Controls.Add(this._invoicesGrid);
            this._gridCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridCard.Margin = System.Windows.Forms.Padding.Empty;
            this._gridCard.Name = "_gridCard";
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
            this._invoice_id_column,
            this._invoice_column,
            this._date_column,
            this._customer_column,
            this._cashier_column,
            this._total_column,
            this._status_column,
            this._actions_column,
            this._print_column});
            this._invoicesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._invoicesGrid.Location = new System.Drawing.Point(0, 0);
            this._invoicesGrid.Margin = new System.Windows.Forms.Padding(1);
            this._invoicesGrid.Name = "_invoicesGrid";
            this._invoicesGrid.ReadOnly = true;
            this._invoicesGrid.RowHeadersVisible = false;
            this._invoicesGrid.RowHeadersWidth = 62;
            this._invoicesGrid.Size = new System.Drawing.Size(1126, 566);
            this._invoicesGrid.TabIndex = 2;
            // 
            // InvoiceId
            // 
            this._invoice_id_column.MinimumWidth = 8;
            this._invoice_id_column.Name = "InvoiceId";
            this._invoice_id_column.ReadOnly = true;
            this._invoice_id_column.Visible = false;
            // 
            // Invoice
            // 
            this._invoice_column.HeaderText = "Mã hóa đơn";
            this._invoice_column.MinimumWidth = 8;
            this._invoice_column.Name = "Invoice";
            this._invoice_column.ReadOnly = true;
            // 
            // Date
            // 
            this._date_column.HeaderText = "Ngày tạo";
            this._date_column.MinimumWidth = 8;
            this._date_column.Name = "Date";
            this._date_column.ReadOnly = true;
            // 
            // Customer
            // 
            this._customer_column.HeaderText = "Khách hàng";
            this._customer_column.MinimumWidth = 8;
            this._customer_column.Name = "Customer";
            this._customer_column.ReadOnly = true;
            // 
            // Cashier
            // 
            this._cashier_column.HeaderText = "Nhân viên";
            this._cashier_column.MinimumWidth = 8;
            this._cashier_column.Name = "Cashier";
            this._cashier_column.ReadOnly = true;
            // 
            // Total
            // 
            this._total_column.HeaderText = "Tổng tiền";
            this._total_column.MinimumWidth = 8;
            this._total_column.Name = "Total";
            this._total_column.ReadOnly = true;
            // 
            // Status
            // 
            this._status_column.HeaderText = "Trạng thái";
            this._status_column.MinimumWidth = 8;
            this._status_column.Name = "Status";
            this._status_column.ReadOnly = true;
            // 
            // Actions
            // 
            this._actions_column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle3.NullValue")));
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this._actions_column.DefaultCellStyle = dataGridViewCellStyle3;
            this._actions_column.HeaderText = "Thao tác";
            this._actions_column.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this._actions_column.MinimumWidth = 78;
            this._actions_column.Name = "Actions";
            this._actions_column.ReadOnly = true;
            this._actions_column.Width = 78;
            // 
            // Print
            // 
            this._print_column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle4.NullValue")));
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this._print_column.DefaultCellStyle = dataGridViewCellStyle4;
            this._print_column.HeaderText = "In";
            this._print_column.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this._print_column.MinimumWidth = 52;
            this._print_column.Name = "Print";
            this._print_column.ReadOnly = true;
            this._print_column.Width = 52;
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
            this._gridCard.ResumeLayout(false);
            this._headerIconPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._headerIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._invoicesGrid)).EndInit();
            this.ResumeLayout(false);

        }

        private DataGridViewTextBoxColumn _invoice_id_column;
        private DataGridViewTextBoxColumn _invoice_column;
        private DataGridViewTextBoxColumn _date_column;
        private DataGridViewTextBoxColumn _customer_column;
        private DataGridViewTextBoxColumn _cashier_column;
        private DataGridViewTextBoxColumn _total_column;
        private DataGridViewTextBoxColumn _status_column;
        private DataGridViewImageColumn _actions_column;
        private DataGridViewImageColumn _print_column;
    }
}
