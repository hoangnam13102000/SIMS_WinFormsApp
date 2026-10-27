using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Views.UserManager
{
    partial class frmCustomerManagement
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
            this._surface = new System.Windows.Forms.Panel();
            this._contentLayout = new System.Windows.Forms.TableLayoutPanel();
            this._headerPanel = new System.Windows.Forms.Panel();
            this._toolbar = new System.Windows.Forms.FlowLayoutPanel();
            this._usersGrid = new System.Windows.Forms.DataGridView();
            this._usernameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._fullNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._emailColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._roleColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._statusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._lockColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._actionsColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this._statusFilter = new System.Windows.Forms.ComboBox();
            this._searchBox = new System.Windows.Forms.TextBox();
            this._subtitleLabel = new System.Windows.Forms.Label();
            this._titleLabel = new System.Windows.Forms.Label();
            this._surface.SuspendLayout();
            this._contentLayout.SuspendLayout();
            this._headerPanel.SuspendLayout();
            this._toolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._usersGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // _surface
            // 
            this._surface.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this._surface.Controls.Add(this._contentLayout);
            this._surface.Dock = System.Windows.Forms.DockStyle.Fill;
            this._surface.Location = new System.Drawing.Point(0, 0);
            this._surface.Name = "_surface";
            this._surface.Padding = new System.Windows.Forms.Padding(24);
            this._surface.Size = new System.Drawing.Size(1071, 700);
            this._surface.TabIndex = 0;
            //
            // _contentLayout
            //
            this._contentLayout.ColumnCount = 1;
            this._contentLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._contentLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._contentLayout.Name = "_contentLayout";
            this._contentLayout.RowCount = 3;
            this._contentLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 102F));
            this._contentLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this._contentLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._contentLayout.Controls.Add(this._headerPanel, 0, 0);
            this._contentLayout.Controls.Add(this._toolbar, 0, 1);
            this._contentLayout.Controls.Add(this._usersGrid, 0, 2);
            //
            // _headerPanel
            //
            this._headerPanel.BackColor = System.Drawing.Color.Transparent;
            this._headerPanel.Controls.Add(this._subtitleLabel);
            this._headerPanel.Controls.Add(this._titleLabel);
            this._headerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._headerPanel.Margin = System.Windows.Forms.Padding.Empty;
            this._headerPanel.Name = "_headerPanel";
            //
            // _toolbar
            //
            this._toolbar.AutoSize = true;
            this._toolbar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._toolbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this._toolbar.Margin = System.Windows.Forms.Padding.Empty;
            this._toolbar.Name = "_toolbar";
            this._toolbar.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this._toolbar.WrapContents = true;
            this._toolbar.Controls.Add(this._searchBox);
            this._toolbar.Controls.Add(this._statusFilter);
            // 
            // _usersGrid
            // 
            this._usersGrid.AllowUserToAddRows = false;
            this._usersGrid.AllowUserToDeleteRows = false;
            this._usersGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._usersGrid.Margin = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this._usersGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._usersGrid.BackgroundColor = System.Drawing.Color.White;
            this._usersGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._usersGrid.ColumnHeadersHeight = 40;
            this._usersGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._usernameColumn,
            this._fullNameColumn,
            this._emailColumn,
            this._roleColumn,
            this._statusColumn,
            this._lockColumn,
            this._actionsColumn});
            this._usersGrid.Name = "_usersGrid";
            this._usersGrid.ReadOnly = true;
            this._usersGrid.RowHeadersVisible = false;
            this._usersGrid.RowHeadersWidth = 62;
            this._usersGrid.RowTemplate.Height = 38;
            this._usersGrid.Size = new System.Drawing.Size(1823, 1080);
            this._usersGrid.TabIndex = 0;
            // 
            // Username
            // 
            this._usernameColumn.HeaderText = "Tên đăng nhập";
            this._usernameColumn.MinimumWidth = 8;
            this._usernameColumn.Name = "Username";
            this._usernameColumn.ReadOnly = true;
            this._usernameColumn.FillWeight = 95F;
            // 
            // FullName
            // 
            this._fullNameColumn.HeaderText = "Họ và tên";
            this._fullNameColumn.MinimumWidth = 8;
            this._fullNameColumn.Name = "FullName";
            this._fullNameColumn.ReadOnly = true;
            this._fullNameColumn.FillWeight = 105F;
            // 
            // Email
            // 
            this._emailColumn.HeaderText = "Email";
            this._emailColumn.MinimumWidth = 8;
            this._emailColumn.Name = "Email";
            this._emailColumn.ReadOnly = true;
            this._emailColumn.FillWeight = 135F;
            // 
            // Role
            // 
            this._roleColumn.HeaderText = "Vai trò";
            this._roleColumn.MinimumWidth = 8;
            this._roleColumn.Name = "Role";
            this._roleColumn.ReadOnly = true;
            this._roleColumn.FillWeight = 100F;
            // 
            // Status
            // 
            this._statusColumn.HeaderText = "Trạng thái";
            this._statusColumn.MinimumWidth = 8;
            this._statusColumn.Name = "Status";
            this._statusColumn.ReadOnly = true;
            this._statusColumn.FillWeight = 100F;
            // 
            // Lock
            // 
            this._lockColumn.HeaderText = "Khóa";
            this._lockColumn.MinimumWidth = 8;
            this._lockColumn.Name = "Lock";
            this._lockColumn.ReadOnly = true;
            this._lockColumn.FillWeight = 75F;
            // 
            // Actions
            // 
            this._actionsColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this._actionsColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this._actionsColumn.HeaderText = "Thao tác";
            this._actionsColumn.ImageLayout = DataGridViewImageCellLayout.Normal;
            this._actionsColumn.Name = "Actions";
            this._actionsColumn.ReadOnly = true;
            this._actionsColumn.ToolTipText = "Xem, sửa hoặc khóa / mở khóa";
            this._actionsColumn.Width = 120;
            // 
            // _statusFilter
            // 
            this._statusFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._statusFilter.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._statusFilter.Margin = new System.Windows.Forms.Padding(8, 3, 0, 3);
            this._statusFilter.Items.AddRange(new object[] {
            "Tất cả trạng thái",
            "Đang hoạt động",
            "Vô hiệu hóa"});
            this._statusFilter.SelectedIndex = 0;
            this._statusFilter.Name = "_statusFilter";
            this._statusFilter.Size = new System.Drawing.Size(220, 36);
            this._statusFilter.TabIndex = 1;
            // 
            // _searchBox
            // 
            this._searchBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._searchBox.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this._searchBox.Name = "_searchBox";
            this._searchBox.Size = new System.Drawing.Size(360, 34);
            this._searchBox.TabIndex = 2;
            // 
            // _subtitleLabel
            // 
            this._subtitleLabel.AutoSize = true;
            this._subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._subtitleLabel.Location = new System.Drawing.Point(2, 56);
            this._subtitleLabel.Name = "_subtitleLabel";
            this._subtitleLabel.Size = new System.Drawing.Size(377, 28);
            this._subtitleLabel.TabIndex = 3;
            this._subtitleLabel.Text = "Danh sách khách hàng và lịch sử giao dịch";
            // 
            // _titleLabel
            // 
            this._titleLabel.AutoSize = true;
            this._titleLabel.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this._titleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._titleLabel.Location = new System.Drawing.Point(0, 0);
            this._titleLabel.Name = "_titleLabel";
            this._titleLabel.Size = new System.Drawing.Size(475, 65);
            this._titleLabel.TabIndex = 4;
            this._titleLabel.Text = "Quản lý khách hàng";
            // 
            // frmCustomerManagement
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1071, 700);
            this.Controls.Add(this._surface);
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "frmCustomerManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý khách hàng - SIMS";
            this._surface.ResumeLayout(false);
            this._toolbar.ResumeLayout(false);
            this._headerPanel.ResumeLayout(false);
            this._headerPanel.PerformLayout();
            this._contentLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._usersGrid)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
