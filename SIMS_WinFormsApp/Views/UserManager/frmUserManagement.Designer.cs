using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace SIMS_WinFormsApp.Views.UserManager
{
    partial class frmUserManagement
    {
        private System.ComponentModel.IContainer components = null;
        private ContextMenuStrip _rowMenu;
        private ToolStripMenuItem _viewDetailsMenuItem;
        private ToolStripMenuItem _editUserMenuItem;
        private ToolStripMenuItem _toggleLockMenuItem;
        private Panel _designPreview;
        private TableLayoutPanel _contentLayout;
        private Panel _headerPanel;
        private Panel _headerIconPanel;
        private IconPictureBox _headerIcon;
        private Label _titleLabel;
        private Label _subtitleLabel;
        private Button _lockButton;
        private Button _refreshButton;
        private Panel _toolbar;
        private Label _searchLabel;
        private TextBox _searchBox;
        private Label _statusLabel;
        private ComboBox _statusFilter;
        private Panel _gridCard;
        private DataGridView _usersGrid;
        private DataGridViewImageColumn _actionsColumn;
        private DataGridViewTextBoxColumn _username_column;
        private DataGridViewTextBoxColumn _full_name_column;
        private DataGridViewTextBoxColumn _email_column;
        private DataGridViewTextBoxColumn _role_column;
        private DataGridViewTextBoxColumn _status_column;
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._rowMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._viewDetailsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._editUserMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._toggleLockMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._designPreview = new System.Windows.Forms.Panel();
            this._contentLayout = new System.Windows.Forms.TableLayoutPanel();
            this._toolbar = new System.Windows.Forms.Panel();
            this._searchLabel = new System.Windows.Forms.Label();
            this._statusLabel = new System.Windows.Forms.Label();
            this._refreshButton = new System.Windows.Forms.Button();
            this._gridCard = new System.Windows.Forms.Panel();
            this._usersGrid = new System.Windows.Forms.DataGridView();
            this._username_column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._full_name_column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._email_column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._role_column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._status_column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._actionsColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this._statusFilter = new System.Windows.Forms.ComboBox();
            this._searchBox = new System.Windows.Forms.TextBox();
            this._headerPanel = new System.Windows.Forms.Panel();
            this._headerIconPanel = new System.Windows.Forms.Panel();
            this._headerIcon = new FontAwesome.Sharp.IconPictureBox();
            this._subtitleLabel = new System.Windows.Forms.Label();
            this._titleLabel = new System.Windows.Forms.Label();
            this._lockButton = new System.Windows.Forms.Button();
            this._designPreview.SuspendLayout();
            this._contentLayout.SuspendLayout();
            this._toolbar.SuspendLayout();
            this._gridCard.SuspendLayout();
            this._rowMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._usersGrid)).BeginInit();
            this._headerPanel.SuspendLayout();
            this._headerIconPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._headerIcon)).BeginInit();
            this.SuspendLayout();
            //
            // _designPreview
            //
            this._designPreview.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this._designPreview.Controls.Add(this._contentLayout);
            this._designPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this._designPreview.Location = new System.Drawing.Point(0, 0);
            this._designPreview.Name = "_designPreview";
            this._designPreview.Padding = new System.Windows.Forms.Padding(24);
            this._designPreview.Size = new System.Drawing.Size(1000, 700);
            this._designPreview.TabIndex = 0;
            //
            // _contentLayout
            //
            this._contentLayout.ColumnCount = 1;
            this._contentLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._contentLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._contentLayout.Name = "_contentLayout";
            this._contentLayout.RowCount = 4;
            this._contentLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this._contentLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this._contentLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._contentLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this._contentLayout.Controls.Add(this._headerPanel, 0, 0);
            this._contentLayout.Controls.Add(this._toolbar, 0, 1);
            this._contentLayout.Controls.Add(this._gridCard, 0, 2);
            //
            // _toolbar
            //
            this._toolbar.BackColor = System.Drawing.Color.White;
            this._toolbar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._toolbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this._toolbar.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this._toolbar.Name = "_toolbar";
            this._toolbar.Controls.Add(this._searchLabel);
            this._toolbar.Controls.Add(this._searchBox);
            this._toolbar.Controls.Add(this._statusLabel);
            this._toolbar.Controls.Add(this._statusFilter);
            //
            // _searchLabel
            //
            this._searchLabel.AutoSize = true;
            this._searchLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._searchLabel.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this._searchLabel.Location = new System.Drawing.Point(16, 8);
            this._searchLabel.Name = "_searchLabel";
            this._searchLabel.Text = "Tìm tài khoản";
            //
            // _searchBox
            //
            this._searchBox.Anchor = System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this._searchBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._searchBox.Location = new System.Drawing.Point(16, 31);
            this._searchBox.Name = "_searchBox";
            this._searchBox.Size = new System.Drawing.Size(560, 30);
            this._searchBox.TabIndex = 1;
            //
            // _statusLabel
            //
            this._statusLabel.AutoSize = true;
            this._statusLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._statusLabel.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this._statusLabel.Location = new System.Drawing.Point(600, 8);
            this._statusLabel.Name = "_statusLabel";
            this._statusLabel.Text = "Trạng thái";
            //
            // _statusFilter
            //
            this._statusFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._statusFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._statusFilter.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._statusFilter.Items.AddRange(new object[] {
            "Tất cả trạng thái",
            "Đang hoạt động",
            "Vô hiệu hóa"});
            this._statusFilter.SelectedIndex = 0;
            this._statusFilter.Location = new System.Drawing.Point(600, 31);
            this._statusFilter.Name = "_statusFilter";
            this._statusFilter.Size = new System.Drawing.Size(300, 31);
            this._statusFilter.TabIndex = 2;
            //
            // _gridCard
            //
            this._gridCard.BackColor = System.Drawing.Color.White;
            this._gridCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._gridCard.Controls.Add(this._usersGrid);
            this._gridCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridCard.Margin = System.Windows.Forms.Padding.Empty;
            this._gridCard.Name = "_gridCard";
            //
            // _usersGrid
            //
            this._usersGrid.AllowUserToAddRows = false;
            this._usersGrid.AllowUserToDeleteRows = false;
            this._usersGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._usersGrid.Margin = new System.Windows.Forms.Padding(1);
            this._usersGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._usersGrid.BackgroundColor = System.Drawing.Color.White;
            this._usersGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._usersGrid.ColumnHeadersHeight = 40;
            this._usersGrid.EnableHeadersVisualStyles = false;
            this._usersGrid.GridColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this._usersGrid.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this._usersGrid.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this._usersGrid.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._usersGrid.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._usersGrid.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(219, 234, 254);
            this._usersGrid.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this._usersGrid.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this._usersGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._usersGrid.MultiSelect = false;
            this._usersGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._username_column,
            this._full_name_column,
            this._email_column,
            this._role_column,
            this._status_column,
            this._actionsColumn});
            this._usersGrid.Name = "_usersGrid";
            this._usersGrid.ReadOnly = true;
            this._usersGrid.ContextMenuStrip = this._rowMenu;
            this._usersGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.UsersGrid_CellDoubleClick);
            this._usersGrid.SelectionChanged += new System.EventHandler(this.UsersGrid_SelectionChanged);
            this._usersGrid.RowHeadersVisible = false;
            this._usersGrid.RowHeadersWidth = 62;
            this._usersGrid.RowTemplate.Height = 38;
            this._usersGrid.Size = new System.Drawing.Size(952, 480);
            this._usersGrid.TabIndex = 3;
            //
            // Username
            //
            this._username_column.HeaderText = "Tên đăng nhập";
            this._username_column.MinimumWidth = 8;
            this._username_column.Name = "Username";
            this._username_column.ReadOnly = true;
            this._username_column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this._username_column.FillWeight = 100F;
            //
            // FullName
            //
            this._full_name_column.HeaderText = "Họ và tên";
            this._full_name_column.MinimumWidth = 8;
            this._full_name_column.Name = "FullName";
            this._full_name_column.ReadOnly = true;
            this._full_name_column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this._full_name_column.FillWeight = 110F;
            //
            // Email
            //
            this._email_column.HeaderText = "Email";
            this._email_column.MinimumWidth = 8;
            this._email_column.Name = "Email";
            this._email_column.ReadOnly = true;
            this._email_column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this._email_column.FillWeight = 140F;
            //
            // Role
            //
            this._role_column.HeaderText = "Vai trò";
            this._role_column.MinimumWidth = 8;
            this._role_column.Name = "Role";
            this._role_column.ReadOnly = true;
            this._role_column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this._role_column.FillWeight = 110F;
            //
            // Status
            //
            this._status_column.HeaderText = "Trạng thái";
            this._status_column.MinimumWidth = 8;
            this._status_column.Name = "Status";
            this._status_column.ReadOnly = true;
            this._status_column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this._status_column.FillWeight = 95F;
            //
            // Actions
            //
            this._actionsColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this._actionsColumn.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this._actionsColumn.HeaderText = "Thao tác";
            this._actionsColumn.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Normal;
            this._actionsColumn.Name = "Actions";
            this._actionsColumn.ReadOnly = true;
            this._actionsColumn.ToolTipText = "Xem, sửa hoặc khóa / mở khóa";
            this._actionsColumn.Width = 120;
            //
            // _viewDetailsMenuItem
            //
            this._viewDetailsMenuItem.AutoSize = true;
            this._viewDetailsMenuItem.Name = "_viewDetailsMenuItem";
            this._viewDetailsMenuItem.Text = "Xem chi tiết";
            this._viewDetailsMenuItem.Click += new System.EventHandler(this.ViewDetailsMenuItem_Click);
            //
            // _editUserMenuItem
            //
            this._editUserMenuItem.AutoSize = true;
            this._editUserMenuItem.Name = "_editUserMenuItem";
            this._editUserMenuItem.Text = "Sửa tài khoản";
            this._editUserMenuItem.Click += new System.EventHandler(this.EditUserMenuItem_Click);
            //
            // _toggleLockMenuItem
            //
            this._toggleLockMenuItem.AutoSize = true;
            this._toggleLockMenuItem.Name = "_toggleLockMenuItem";
            this._toggleLockMenuItem.Text = "Khóa / mở khóa";
            this._toggleLockMenuItem.Click += new System.EventHandler(this.ToggleLockMenuItem_Click);
            //
            // _rowMenu
            //
            this._rowMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._viewDetailsMenuItem,
            this._editUserMenuItem,
            this._toggleLockMenuItem});
            this._rowMenu.Name = "_rowMenu";
            this._rowMenu.ResumeLayout(false);
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
            this._statusFilter.Size = new System.Drawing.Size(300, 31);
            this._statusFilter.TabIndex = 2;
            //
            // _searchBox
            //
            this._searchBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._searchBox.Name = "_searchBox";
            this._searchBox.Size = new System.Drawing.Size(560, 30);
            this._searchBox.TabIndex = 1;
            //
            // _headerPanel
            //
            this._headerPanel.BackColor = System.Drawing.Color.White;
            this._headerPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._headerPanel.Controls.Add(this._headerIconPanel);
            this._headerPanel.Controls.Add(this._subtitleLabel);
            this._headerPanel.Controls.Add(this._titleLabel);
            this._headerPanel.Controls.Add(this._lockButton);
            this._headerPanel.Controls.Add(this._refreshButton);
            this._headerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._headerPanel.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this._headerPanel.Name = "_headerPanel";
            this._headerPanel.Size = new System.Drawing.Size(952, 96);
            this._headerPanel.TabIndex = 0;
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
            this._headerIcon.IconChar = FontAwesome.Sharp.IconChar.Users;
            this._headerIcon.IconColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this._headerIcon.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._headerIcon.IconSize = 28;
            this._headerIcon.Location = new System.Drawing.Point(14, 14);
            this._headerIcon.Size = new System.Drawing.Size(28, 28);
            this._headerIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._headerIcon.TabStop = false;
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
            this._refreshButton.Location = new System.Drawing.Point(806, 28);
            this._refreshButton.Name = "_refreshButton";
            this._refreshButton.Size = new System.Drawing.Size(126, 38);
            this._refreshButton.TabIndex = 2;
            this._refreshButton.Text = "Làm mới";
            this._refreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            this._refreshButton.UseVisualStyleBackColor = false;
            //
            // _subtitleLabel
            //
            this._subtitleLabel.AutoSize = true;
            this._subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._subtitleLabel.Location = new System.Drawing.Point(96, 56);
            this._subtitleLabel.Name = "_subtitleLabel";
            this._subtitleLabel.Size = new System.Drawing.Size(544, 28);
            this._subtitleLabel.TabIndex = 1;
            this._subtitleLabel.Text = "Quản lý tài khoản người dùng và phân quyền trong hệ thống";
            //
            // _titleLabel
            //
            this._titleLabel.AutoSize = false;
            this._titleLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this._titleLabel.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this._titleLabel.Location = new System.Drawing.Point(96, 18);
            this._titleLabel.Name = "_titleLabel";
            this._titleLabel.Size = new System.Drawing.Size(600, 34);
            this._titleLabel.TabIndex = 0;
            this._titleLabel.Text = "Quản lý tài khoản";
            this._titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._titleLabel.AutoEllipsis = true;
            // _lockButton
            //
            this._lockButton.BackColor = System.Drawing.Color.White;
            this._lockButton.Enabled = false;
            this._lockButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            this._lockButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this._lockButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._lockButton.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this._lockButton.ForeColor = System.Drawing.Color.FromArgb(180, 83, 9);
            this._lockButton.Location = new System.Drawing.Point(688, 28);
            this._lockButton.Name = "_lockButton";
            this._lockButton.Size = new System.Drawing.Size(110, 38);
            this._lockButton.TabIndex = 5;
            this._lockButton.Text = "Khóa";
            this._lockButton.Click += new System.EventHandler(this.LockButton_Click);
            this._lockButton.UseVisualStyleBackColor = false;
            //
            // frmUserManagement
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this._designPreview);
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "frmUserManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý tài khoản - SIMS";
            this._designPreview.ResumeLayout(false);
            this._toolbar.ResumeLayout(false);
            this._contentLayout.ResumeLayout(false);
            this._contentLayout.PerformLayout();
            this._toolbar.ResumeLayout(false);
            this._toolbar.PerformLayout();
            this._gridCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._usersGrid)).EndInit();
            this._headerPanel.ResumeLayout(false);
            this._headerPanel.PerformLayout();
            this._headerIconPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._headerIcon)).EndInit();
            this.ResumeLayout(false);

        }

    }
}
