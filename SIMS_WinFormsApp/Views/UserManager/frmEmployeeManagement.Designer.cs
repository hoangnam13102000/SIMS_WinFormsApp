using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Views.UserManager
{
    partial class frmEmployeeManagement
    {
        private System.ComponentModel.IContainer components = null;
        private Panel _surface;
        private TableLayoutPanel _contentLayout;
        private Panel _headerPanel;
        private FlowLayoutPanel _toolbar;
        private Label _titleLabel;
        private Label _subtitleLabel;
        private TextBox _searchBox;
        private ComboBox _statusFilter;
        private Button _addButton;
        private Button _optionsButton;
        private DataGridView _usersGrid;
        private DataGridViewTextBoxColumn _usernameColumn;
        private DataGridViewTextBoxColumn _fullNameColumn;
        private DataGridViewTextBoxColumn _emailColumn;
        private DataGridViewTextBoxColumn _roleColumn;
        private DataGridViewTextBoxColumn _statusColumn;
        private DataGridViewTextBoxColumn _lockColumn;
        private DataGridViewImageColumn _actionsColumn;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._surface = new Panel();
            this._contentLayout = new TableLayoutPanel();
            this._headerPanel = new Panel();
            this._toolbar = new FlowLayoutPanel();
            this._titleLabel = new Label();
            this._subtitleLabel = new Label();
            this._searchBox = new TextBox();
            this._statusFilter = new ComboBox();
            this._addButton = new Button();
            this._optionsButton = new Button();
            this._usersGrid = new DataGridView();
            this._usernameColumn = new DataGridViewTextBoxColumn();
            this._fullNameColumn = new DataGridViewTextBoxColumn();
            this._emailColumn = new DataGridViewTextBoxColumn();
            this._roleColumn = new DataGridViewTextBoxColumn();
            this._statusColumn = new DataGridViewTextBoxColumn();
            this._lockColumn = new DataGridViewTextBoxColumn();
            this._actionsColumn = new DataGridViewImageColumn();
            this._surface.SuspendLayout();
            this._contentLayout.SuspendLayout();
            this._headerPanel.SuspendLayout();
            this._toolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._usersGrid)).BeginInit();
            this.SuspendLayout();
            //
            // _surface
            //
            this._surface.BackColor = Color.FromArgb(244, 247, 250);
            this._surface.Controls.Add(this._contentLayout);
            this._surface.Dock = DockStyle.Fill;
            this._surface.Padding = new Padding(24);
            this._surface.Name = "_surface";
            //
            // _contentLayout
            //
            this._contentLayout.ColumnCount = 1;
            this._contentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this._contentLayout.Dock = DockStyle.Fill;
            this._contentLayout.Name = "_contentLayout";
            this._contentLayout.RowCount = 3;
            this._contentLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 102F));
            this._contentLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this._contentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this._contentLayout.Controls.Add(this._headerPanel, 0, 0);
            this._contentLayout.Controls.Add(this._toolbar, 0, 1);
            this._contentLayout.Controls.Add(this._usersGrid, 0, 2);
            //
            // _headerPanel
            //
            this._headerPanel.BackColor = Color.Transparent;
            this._headerPanel.Controls.Add(this._subtitleLabel);
            this._headerPanel.Controls.Add(this._titleLabel);
            this._headerPanel.Dock = DockStyle.Fill;
            this._headerPanel.Margin = Padding.Empty;
            this._headerPanel.Name = "_headerPanel";
            //
            // _toolbar
            //
            this._toolbar.AutoSize = true;
            this._toolbar.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this._toolbar.Dock = DockStyle.Fill;
            this._toolbar.Margin = Padding.Empty;
            this._toolbar.Name = "_toolbar";
            this._toolbar.Padding = new Padding(0, 5, 0, 5);
            this._toolbar.WrapContents = true;
            this._toolbar.Controls.Add(this._searchBox);
            this._toolbar.Controls.Add(this._statusFilter);
            this._toolbar.Controls.Add(this._addButton);
            this._toolbar.Controls.Add(this._optionsButton);
            //
            // _titleLabel
            //
            this._titleLabel.AutoSize = true;
            this._titleLabel.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            this._titleLabel.ForeColor = Color.FromArgb(15, 23, 42);
            this._titleLabel.Location = new Point(0, 0);
            this._titleLabel.Name = "_titleLabel";
            this._titleLabel.Text = "Quản lý nhân viên";
            //
            // _subtitleLabel
            //
            this._subtitleLabel.AutoSize = true;
            this._subtitleLabel.Font = new Font("Segoe UI", 10F);
            this._subtitleLabel.ForeColor = Color.FromArgb(100, 116, 139);
            this._subtitleLabel.Location = new Point(2, 56);
            this._subtitleLabel.Name = "_subtitleLabel";
            this._subtitleLabel.Text = "Danh sách nhân viên và thông tin làm việc";
            //
            // _searchBox
            //
            this._searchBox.Font = new Font("Segoe UI", 10F);
            this._searchBox.Margin = new Padding(0, 3, 0, 3);
            this._searchBox.Name = "_searchBox";
            this._searchBox.Size = new Size(340, 30);
            //
            // _statusFilter
            //
            this._statusFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            this._statusFilter.Font = new Font("Segoe UI", 10F);
            this._statusFilter.Items.AddRange(new object[] { "Tất cả trạng thái", "Đang hoạt động", "Vô hiệu hóa" });
            this._statusFilter.SelectedIndex = 0;
            this._statusFilter.Margin = new Padding(8, 3, 0, 3);
            this._statusFilter.Name = "_statusFilter";
            this._statusFilter.Size = new Size(210, 31);
            //
            // _addButton
            //
            this._addButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this._addButton.Margin = new Padding(12, 0, 0, 0);
            this._addButton.Name = "_addButton";
            this._addButton.Size = new Size(150, 36);
            this._addButton.Text = "+ Thêm nhân viên";
            this._addButton.UseVisualStyleBackColor = true;
            //
            // _optionsButton
            //
            this._optionsButton.Font = new Font("Segoe UI", 9F);
            this._optionsButton.Margin = new Padding(8, 0, 0, 0);
            this._optionsButton.Name = "_optionsButton";
            this._optionsButton.Size = new Size(110, 36);
            this._optionsButton.Text = "Tùy chọn";
            this._optionsButton.UseVisualStyleBackColor = true;
            //
            // _usersGrid
            //
            this._usersGrid.AllowUserToAddRows = false;
            this._usersGrid.AllowUserToDeleteRows = false;
            this._usersGrid.Dock = DockStyle.Fill;
            this._usersGrid.Margin = new Padding(0, 4, 0, 0);
            this._usersGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this._usersGrid.BackgroundColor = Color.White;
            this._usersGrid.BorderStyle = BorderStyle.None;
            this._usersGrid.ColumnHeadersHeight = 40;
            this._usersGrid.Columns.AddRange(new DataGridViewColumn[] {
                this._usernameColumn, this._fullNameColumn, this._emailColumn,
                this._roleColumn, this._statusColumn, this._lockColumn,
                this._actionsColumn});
            this._usersGrid.Name = "_usersGrid";
            this._usersGrid.ReadOnly = true;
            this._usersGrid.RowHeadersVisible = false;
            this._usersGrid.RowTemplate.Height = 38;
            this._usersGrid.Size = new Size(952, 480);
            //
            // columns
            //
            this._usernameColumn.HeaderText = "Tên đăng nhập";
            this._usernameColumn.Name = "Username";
            this._usernameColumn.FillWeight = 95F;
            this._fullNameColumn.HeaderText = "Họ và tên";
            this._fullNameColumn.Name = "FullName";
            this._fullNameColumn.FillWeight = 105F;
            this._emailColumn.HeaderText = "Email";
            this._emailColumn.Name = "Email";
            this._emailColumn.FillWeight = 135F;
            this._roleColumn.HeaderText = "Vai trò";
            this._roleColumn.Name = "Role";
            this._roleColumn.FillWeight = 100F;
            this._statusColumn.HeaderText = "Trạng thái";
            this._statusColumn.Name = "Status";
            this._statusColumn.FillWeight = 100F;
            this._lockColumn.HeaderText = "Khóa";
            this._lockColumn.Name = "Lock";
            this._lockColumn.FillWeight = 75F;
            this._actionsColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this._actionsColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this._actionsColumn.HeaderText = "Thao tác";
            this._actionsColumn.ImageLayout = DataGridViewImageCellLayout.Normal;
            this._actionsColumn.Name = "Actions";
            this._actionsColumn.ReadOnly = true;
            this._actionsColumn.ToolTipText = "Xem, sửa hoặc khóa / mở khóa";
            this._actionsColumn.Width = 120;
            //
            // frmEmployeeManagement
            //
            this.AutoScaleMode = AutoScaleMode.None;
            this.ClientSize = new Size(1000, 700);
            this.Controls.Add(this._surface);
            this.MinimumSize = new Size(640, 480);
            this.Name = "frmEmployeeManagement";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Quản lý nhân viên - SIMS";
            this._surface.ResumeLayout(false);
            this._contentLayout.ResumeLayout(false);
            this._headerPanel.ResumeLayout(false);
            this._headerPanel.PerformLayout();
            this._toolbar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._usersGrid)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
