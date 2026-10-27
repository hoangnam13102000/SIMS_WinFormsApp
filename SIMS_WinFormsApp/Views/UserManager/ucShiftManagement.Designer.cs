using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Views.UserManager
{
    partial class ucShiftManagement
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel _root;
        private Panel _header;
        private Label _title;
        private Label _subtitle;
        private FlowLayoutPanel _filters;
        private DateTimePicker _date;
        private ComboBox _employee;
        private ComboBox _status;
        private DataGridView _grid;
        private DataGridViewTextBoxColumn _employeeColumn;
        private DataGridViewTextBoxColumn _start;
        private DataGridViewTextBoxColumn _end;
        private DataGridViewTextBoxColumn _opening;
        private DataGridViewTextBoxColumn _closing;
        private DataGridViewTextBoxColumn _shiftStatus;

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
            _date = new DateTimePicker();
            _employee = new ComboBox();
            _status = new ComboBox();
            _grid = new DataGridView();
            _employeeColumn = new DataGridViewTextBoxColumn();
            _start = new DataGridViewTextBoxColumn();
            _end = new DataGridViewTextBoxColumn();
            _opening = new DataGridViewTextBoxColumn();
            _closing = new DataGridViewTextBoxColumn();
            _shiftStatus = new DataGridViewTextBoxColumn();
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
            _title.Text = "Quản lý ca làm";
            _subtitle.AutoSize = true;
            _subtitle.ForeColor = Color.FromArgb(100, 116, 139);
            _subtitle.Location = new Point(2, 44);
            _subtitle.Text = "Lịch sử ca làm và đối soát tiền mặt";
            _filters.Controls.Add(_date);
            _filters.Controls.Add(_employee);
            _filters.Controls.Add(_status);
            _filters.Dock = DockStyle.Fill;
            _date.Format = DateTimePickerFormat.Short;
            _date.Margin = new Padding(0, 4, 8, 0);
            _date.Name = "_date";
            _date.Size = new Size(130, 28);
            _employee.DropDownStyle = ComboBoxStyle.DropDownList;
            _employee.Items.AddRange(new object[] { "Tất cả nhân viên" });
            _employee.Margin = new Padding(4);
            _employee.Name = "_employee";
            _employee.SelectedIndex = 0;
            _employee.Size = new Size(220, 28);
            _status.DropDownStyle = ComboBoxStyle.DropDownList;
            _status.Items.AddRange(new object[] { "Tất cả trạng thái", "Đang mở", "Đã đóng" });
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
            _grid.Columns.AddRange(new DataGridViewColumn[] { _employeeColumn, _start, _end, _opening, _closing, _shiftStatus });
            _grid.Dock = DockStyle.Fill;
            _grid.ReadOnly = true;
            _grid.RowHeadersVisible = false;
            _employeeColumn.HeaderText = "Nhân viên";
            _employeeColumn.Name = "Employee";
            _start.HeaderText = "Bắt đầu";
            _start.Name = "Start";
            _end.HeaderText = "Kết thúc";
            _end.Name = "End";
            _opening.HeaderText = "Tiền đầu ca";
            _opening.Name = "Opening";
            _closing.HeaderText = "Tiền cuối ca";
            _closing.Name = "Closing";
            _shiftStatus.HeaderText = "Trạng thái";
            _shiftStatus.Name = "Status";
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 247, 250);
            Controls.Add(_root);
            Name = "ucShiftManagement";
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
