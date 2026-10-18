using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Views.Sales
{
    partial class ucOrderManagement
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel _root;
        private Panel _header;
        private Label _title;
        private Label _subtitle;
        private FlowLayoutPanel _filters;
        private TextBox _search;
        private ComboBox _status;
        private DateTimePicker _date;
        private DataGridView _grid;
        private DataGridViewTextBoxColumn _code;
        private DataGridViewTextBoxColumn _customer;
        private DataGridViewTextBoxColumn _created;
        private DataGridViewTextBoxColumn _total;
        private DataGridViewTextBoxColumn _state;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this._root = new System.Windows.Forms.TableLayoutPanel();
            this._header = new System.Windows.Forms.Panel();
            this._subtitle = new System.Windows.Forms.Label();
            this._title = new System.Windows.Forms.Label();
            this._filters = new System.Windows.Forms.FlowLayoutPanel();
            this._search = new System.Windows.Forms.TextBox();
            this._date = new System.Windows.Forms.DateTimePicker();
            this._status = new System.Windows.Forms.ComboBox();
            this._grid = new System.Windows.Forms.DataGridView();
            this._code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._customer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._created = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._state = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._root.SuspendLayout();
            this._header.SuspendLayout();
            this._filters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._grid)).BeginInit();
            this.SuspendLayout();
            // 
            // _root
            // 
            this._root.ColumnCount = 1;
            this._root.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._root.Controls.Add(this._header, 0, 0);
            this._root.Controls.Add(this._filters, 0, 1);
            this._root.Controls.Add(this._grid, 0, 2);
            this._root.Dock = System.Windows.Forms.DockStyle.Fill;
            this._root.Location = new System.Drawing.Point(0, 0);
            this._root.Name = "_root";
            this._root.Padding = new System.Windows.Forms.Padding(24);
            this._root.RowCount = 3;
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 76F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._root.Size = new System.Drawing.Size(1000, 700);
            this._root.TabIndex = 0;
            // 
            // _header
            // 
            this._header.Controls.Add(this._subtitle);
            this._header.Controls.Add(this._title);
            this._header.Dock = System.Windows.Forms.DockStyle.Fill;
            this._header.Location = new System.Drawing.Point(27, 27);
            this._header.Name = "_header";
            this._header.Size = new System.Drawing.Size(946, 70);
            this._header.TabIndex = 0;
            // 
            // _subtitle
            // 
            this._subtitle.AutoSize = true;
            this._subtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._subtitle.Location = new System.Drawing.Point(2, 44);
            this._subtitle.Name = "_subtitle";
            this._subtitle.Size = new System.Drawing.Size(245, 20);
            this._subtitle.TabIndex = 0;
            this._subtitle.Text = "Tra cứu và theo dõi đơn hàng bán";
            // 
            // _title
            // 
            this._title.AutoSize = true;
            this._title.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this._title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._title.Location = new System.Drawing.Point(-4, -16);
            this._title.Name = "_title";
            this._title.Size = new System.Drawing.Size(396, 60);
            this._title.TabIndex = 1;
            this._title.Text = "Quản lý đơn hàng";
            // 
            // _filters
            // 
            this._filters.Controls.Add(this._search);
            this._filters.Controls.Add(this._date);
            this._filters.Controls.Add(this._status);
            this._filters.Dock = System.Windows.Forms.DockStyle.Fill;
            this._filters.Location = new System.Drawing.Point(27, 103);
            this._filters.Name = "_filters";
            this._filters.Size = new System.Drawing.Size(946, 42);
            this._filters.TabIndex = 1;
            // 
            // _search
            // 
            this._search.Location = new System.Drawing.Point(0, 4);
            this._search.Margin = new System.Windows.Forms.Padding(0, 4, 8, 0);
            this._search.Name = "_search";
            this._search.Size = new System.Drawing.Size(280, 26);
            this._search.TabIndex = 0;
            // 
            // _date
            // 
            this._date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this._date.Location = new System.Drawing.Point(292, 4);
            this._date.Margin = new System.Windows.Forms.Padding(4);
            this._date.Name = "_date";
            this._date.Size = new System.Drawing.Size(130, 26);
            this._date.TabIndex = 1;
            // 
            // _status
            // 
            this._status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._status.Items.AddRange(new object[] {
            "Tất cả trạng thái",
            "Mới",
            "Đang xử lý",
            "Hoàn thành",
            "Đã hủy"});
            this._status.Location = new System.Drawing.Point(430, 4);
            this._status.Margin = new System.Windows.Forms.Padding(4);
            this._status.Name = "_status";
            this._status.Size = new System.Drawing.Size(180, 28);
            this._status.TabIndex = 2;
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
            this._code,
            this._customer,
            this._created,
            this._total,
            this._state});
            this._grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grid.Location = new System.Drawing.Point(27, 151);
            this._grid.Name = "_grid";
            this._grid.ReadOnly = true;
            this._grid.RowHeadersVisible = false;
            this._grid.RowHeadersWidth = 62;
            this._grid.Size = new System.Drawing.Size(946, 522);
            this._grid.TabIndex = 2;
            this._grid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this._grid_CellContentClick);
            // 
            // Code
            // 
            this._code.HeaderText = "Mã đơn";
            this._code.MinimumWidth = 8;
            this._code.Name = "Code";
            this._code.ReadOnly = true;
            // 
            // Customer
            // 
            this._customer.HeaderText = "Khách hàng";
            this._customer.MinimumWidth = 8;
            this._customer.Name = "Customer";
            this._customer.ReadOnly = true;
            // 
            // Created
            // 
            this._created.HeaderText = "Ngày tạo";
            this._created.MinimumWidth = 8;
            this._created.Name = "Created";
            this._created.ReadOnly = true;
            // 
            // Total
            // 
            this._total.HeaderText = "Tổng tiền";
            this._total.MinimumWidth = 8;
            this._total.Name = "Total";
            this._total.ReadOnly = true;
            // 
            // State
            // 
            this._state.HeaderText = "Trạng thái";
            this._state.MinimumWidth = 8;
            this._state.Name = "State";
            this._state.ReadOnly = true;
            // 
            // ucOrderManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.Controls.Add(this._root);
            this.Name = "ucOrderManagement";
            this.Size = new System.Drawing.Size(1000, 700);
            this._root.ResumeLayout(false);
            this._header.ResumeLayout(false);
            this._header.PerformLayout();
            this._filters.ResumeLayout(false);
            this._filters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._grid)).EndInit();
            this.ResumeLayout(false);

        }

    }
}
