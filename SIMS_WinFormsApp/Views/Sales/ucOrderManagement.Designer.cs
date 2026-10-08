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
            components = new System.ComponentModel.Container();
            _root = new TableLayoutPanel();
            _header = new Panel();
            _title = new Label();
            _subtitle = new Label();
            _filters = new FlowLayoutPanel();
            _search = new TextBox();
            _status = new ComboBox();
            _date = new DateTimePicker();
            _grid = new DataGridView();
            _code = new DataGridViewTextBoxColumn();
            _customer = new DataGridViewTextBoxColumn();
            _created = new DataGridViewTextBoxColumn();
            _total = new DataGridViewTextBoxColumn();
            _state = new DataGridViewTextBoxColumn();
            _root.SuspendLayout();
            _header.SuspendLayout();
            _filters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_grid).BeginInit();
            SuspendLayout();
            _root.ColumnCount = 1;
            _root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _root.Dock = DockStyle.Fill;
            _root.Padding = new Padding(24);
            _root.RowCount = 3;
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 76F));
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            _root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            _root.Controls.Add(_header, 0, 0);
            _root.Controls.Add(_filters, 0, 1);
            _root.Controls.Add(_grid, 0, 2);
            _header.Controls.Add(_subtitle);
            _header.Controls.Add(_title);
            _header.Dock = DockStyle.Fill;
            _title.AutoSize = true;
            _title.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            _title.ForeColor = Color.FromArgb(15, 23, 42);
            _title.Location = new Point(0, 0);
            _title.Text = "Quản lý đơn hàng";
            _subtitle.AutoSize = true;
            _subtitle.ForeColor = Color.FromArgb(100, 116, 139);
            _subtitle.Location = new Point(2, 44);
            _subtitle.Text = "Tra cứu và theo dõi đơn hàng bán";
            _filters.Controls.Add(_search);
            _filters.Controls.Add(_date);
            _filters.Controls.Add(_status);
            _filters.Dock = DockStyle.Fill;
            _search.Margin = new Padding(0, 4, 8, 0);
            _search.Name = "_search";
            _search.Size = new Size(280, 28);
            _date.Format = DateTimePickerFormat.Short;
            _date.Margin = new Padding(4);
            _date.Name = "_date";
            _date.Size = new Size(130, 28);
            _status.DropDownStyle = ComboBoxStyle.DropDownList;
            _status.Items.AddRange(new object[] { "Tất cả trạng thái", "Mới", "Đang xử lý", "Hoàn thành", "Đã hủy" });
            _status.Margin = new Padding(4);
            _status.Name = "_status";
            _status.SelectedIndex = 0;
            _status.Size = new Size(180, 28);
            _grid.AllowUserToAddRows = false;
            _grid.AllowUserToDeleteRows = false;
            _grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _grid.BackgroundColor = Color.White;
            _grid.BorderStyle = BorderStyle.None;
            _grid.ColumnHeadersHeight = 40;
            _grid.Columns.AddRange(new DataGridViewColumn[] { _code, _customer, _created, _total, _state });
            _grid.Dock = DockStyle.Fill;
            _grid.ReadOnly = true;
            _grid.RowHeadersVisible = false;
            _code.HeaderText = "Mã đơn";
            _code.Name = "Code";
            _customer.HeaderText = "Khách hàng";
            _customer.Name = "Customer";
            _created.HeaderText = "Ngày tạo";
            _created.Name = "Created";
            _total.HeaderText = "Tổng tiền";
            _total.Name = "Total";
            _state.HeaderText = "Trạng thái";
            _state.Name = "State";
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 247, 250);
            Controls.Add(_root);
            Name = "ucOrderManagement";
            Size = new Size(1000, 700);
            _root.ResumeLayout(false);
            _header.ResumeLayout(false);
            _header.PerformLayout();
            _filters.ResumeLayout(false);
            _filters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_grid).EndInit();
            ResumeLayout(false);
        }
    }
}
