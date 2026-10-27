using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Views.Sales
{
    partial class ucReturnManagement
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel _root;
        private Panel _header;
        private Label _title;
        private Label _subtitle;
        private FlowLayoutPanel _filters;
        private TextBox _search;
        private DateTimePicker _from;
        private DateTimePicker _to;
        private ComboBox _reason;
        private DataGridView _grid;
        private DataGridViewTextBoxColumn _returnCode;
        private DataGridViewTextBoxColumn _orderCode;
        private DataGridViewTextBoxColumn _customer;
        private DataGridViewTextBoxColumn _date;
        private DataGridViewTextBoxColumn _reasonColumn;
        private DataGridViewTextBoxColumn _status;

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
            _from = new DateTimePicker();
            _to = new DateTimePicker();
            _reason = new ComboBox();
            _grid = new DataGridView();
            _returnCode = new DataGridViewTextBoxColumn();
            _orderCode = new DataGridViewTextBoxColumn();
            _customer = new DataGridViewTextBoxColumn();
            _date = new DataGridViewTextBoxColumn();
            _reasonColumn = new DataGridViewTextBoxColumn();
            _status = new DataGridViewTextBoxColumn();
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
            _title.Text = "Đổi trả hàng";
            _subtitle.AutoSize = true;
            _subtitle.ForeColor = Color.FromArgb(100, 116, 139);
            _subtitle.Location = new Point(2, 44);
            _subtitle.Text = "Theo dõi yêu cầu đổi trả và hoàn tiền";
            _filters.Controls.Add(_search);
            _filters.Controls.Add(_from);
            _filters.Controls.Add(_to);
            _filters.Controls.Add(_reason);
            _filters.Dock = DockStyle.Fill;
            _search.Margin = new Padding(0, 4, 8, 0);
            _search.Name = "_search";
            _search.Size = new Size(230, 28);
            _from.Format = DateTimePickerFormat.Short;
            _from.Margin = new Padding(4);
            _from.Name = "_from";
            _from.Size = new Size(125, 28);
            _to.Format = DateTimePickerFormat.Short;
            _to.Margin = new Padding(4);
            _to.Name = "_to";
            _to.Size = new Size(125, 28);
            _reason.DropDownStyle = ComboBoxStyle.DropDownList;
            _reason.Items.AddRange(new object[] { "Tất cả lý do", "Lỗi sản phẩm", "Đổi nhu cầu", "Khác" });
            _reason.Margin = new Padding(4);
            _reason.Name = "_reason";
            _reason.SelectedIndex = 0;
            _reason.Size = new Size(160, 28);
            _grid.AllowUserToAddRows = false;
            _grid.AllowUserToDeleteRows = false;
            _grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _grid.BackgroundColor = Color.White;
            _grid.BorderStyle = BorderStyle.None;
            _grid.ColumnHeadersHeight = 40;
            _grid.Columns.AddRange(new DataGridViewColumn[] { _returnCode, _orderCode, _customer, _date, _reasonColumn, _status });
            _grid.Dock = DockStyle.Fill;
            _grid.ReadOnly = true;
            _grid.RowHeadersVisible = false;
            _returnCode.HeaderText = "Mã đổi trả";
            _returnCode.Name = "ReturnCode";
            _orderCode.HeaderText = "Mã đơn hàng";
            _orderCode.Name = "OrderCode";
            _customer.HeaderText = "Khách hàng";
            _customer.Name = "Customer";
            _date.HeaderText = "Ngày yêu cầu";
            _date.Name = "Date";
            _reasonColumn.HeaderText = "Lý do";
            _reasonColumn.Name = "Reason";
            _status.HeaderText = "Trạng thái";
            _status.Name = "Status";
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 247, 250);
            Controls.Add(_root);
            Name = "ucReturnManagement";
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
