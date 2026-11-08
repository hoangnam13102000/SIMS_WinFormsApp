using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace SIMS_WinFormsApp.Views.UserManager
{
    partial class frmCustomerManagement
    {
        private System.ComponentModel.IContainer components = null;
        private ContextMenuStrip _rowMenu;
        private ToolStripMenuItem _viewDetailsMenuItem;
        private ToolStripMenuItem _editUserMenuItem;
        private ToolStripMenuItem _toggleLockMenuItem;
        private Panel _surface;
        private TableLayoutPanel _contentLayout;
        private Panel _headerPanel;
        private Panel _headerIconPanel;
        private IconPictureBox _headerIcon;
        private Panel _toolbar;
        private Label _titleLabel;
        private Label _subtitleLabel;
        private Label _searchLabel;
        private TextBox _searchBox;
        private Label _statusLabel;
        private ComboBox _statusFilter;
        private Button _lockButton;
        private Button _refreshButton;
        private Panel _gridCard;
        private DataGridView _usersGrid;
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this._rowMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._viewDetailsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._editUserMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._toggleLockMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._surface = new System.Windows.Forms.Panel();
            this._contentLayout = new System.Windows.Forms.TableLayoutPanel();
            this._headerPanel = new System.Windows.Forms.Panel();
            this._headerIconPanel = new System.Windows.Forms.Panel();
            this._headerIcon = new FontAwesome.Sharp.IconPictureBox();
            this._subtitleLabel = new System.Windows.Forms.Label();
            this._titleLabel = new System.Windows.Forms.Label();
            this._lockButton = new System.Windows.Forms.Button();
            this._refreshButton = new System.Windows.Forms.Button();
            this._toolbar = new System.Windows.Forms.Panel();
            this._searchLabel = new System.Windows.Forms.Label();
            this._searchBox = new System.Windows.Forms.TextBox();
            this._statusLabel = new System.Windows.Forms.Label();
            this._statusFilter = new System.Windows.Forms.ComboBox();
            this._gridCard = new System.Windows.Forms.Panel();
            this._usersGrid = new System.Windows.Forms.DataGridView();
            this.Username = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Role = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Lock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Actions = new System.Windows.Forms.DataGridViewImageColumn();
            this._rowMenu.SuspendLayout();
            this._surface.SuspendLayout();
            this._contentLayout.SuspendLayout();
            this._headerPanel.SuspendLayout();
            this._headerIconPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._headerIcon)).BeginInit();
            this._toolbar.SuspendLayout();
            this._gridCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._usersGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // _rowMenu
            // 
            this._rowMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this._rowMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._viewDetailsMenuItem,
            this._editUserMenuItem,
            this._toggleLockMenuItem});
            this._rowMenu.Name = "_rowMenu";
            this._rowMenu.Size = new System.Drawing.Size(213, 100);
            // 
            // _viewDetailsMenuItem
            // 
            this._viewDetailsMenuItem.Name = "_viewDetailsMenuItem";
            this._viewDetailsMenuItem.Size = new System.Drawing.Size(212, 32);
            this._viewDetailsMenuItem.Text = "Xem chi tiết";
            this._viewDetailsMenuItem.Click += new System.EventHandler(this.ViewDetailsMenuItem_Click);
            // 
            // _editUserMenuItem
            // 
            this._editUserMenuItem.Name = "_editUserMenuItem";
            this._editUserMenuItem.Size = new System.Drawing.Size(212, 32);
            this._editUserMenuItem.Text = "Sửa tài khoản";
            this._editUserMenuItem.Click += new System.EventHandler(this.EditUserMenuItem_Click);
            // 
            // _toggleLockMenuItem
            // 
            this._toggleLockMenuItem.Name = "_toggleLockMenuItem";
            this._toggleLockMenuItem.Size = new System.Drawing.Size(212, 32);
            this._toggleLockMenuItem.Text = "Khóa / mở khóa";
            this._toggleLockMenuItem.Click += new System.EventHandler(this.ToggleLockMenuItem_Click);
            // 
            // _surface
            // 
            this._surface.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
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
            this._contentLayout.Controls.Add(this._headerPanel, 0, 0);
            this._contentLayout.Controls.Add(this._toolbar, 0, 1);
            this._contentLayout.Controls.Add(this._gridCard, 0, 2);
            this._contentLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._contentLayout.Location = new System.Drawing.Point(24, 24);
            this._contentLayout.Name = "_contentLayout";
            this._contentLayout.RowCount = 4;
            this._contentLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this._contentLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this._contentLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._contentLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this._contentLayout.Size = new System.Drawing.Size(1023, 652);
            this._contentLayout.TabIndex = 0;
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
            this._headerPanel.Location = new System.Drawing.Point(0, 0);
            this._headerPanel.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this._headerPanel.Name = "_headerPanel";
            this._headerPanel.Size = new System.Drawing.Size(1023, 96);
            this._headerPanel.TabIndex = 0;
            // 
            // _headerIconPanel
            // 
            this._headerIconPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(240)))), ((int)(((byte)(254)))));
            this._headerIconPanel.Controls.Add(this._headerIcon);
            this._headerIconPanel.Location = new System.Drawing.Point(20, 20);
            this._headerIconPanel.Name = "_headerIconPanel";
            this._headerIconPanel.Size = new System.Drawing.Size(56, 56);
            this._headerIconPanel.TabIndex = 0;
            // 
            // _headerIcon
            // 
            this._headerIcon.BackColor = System.Drawing.Color.Transparent;
            this._headerIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._headerIcon.IconChar = FontAwesome.Sharp.IconChar.User;
            this._headerIcon.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._headerIcon.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._headerIcon.IconSize = 28;
            this._headerIcon.Location = new System.Drawing.Point(14, 14);
            this._headerIcon.Name = "_headerIcon";
            this._headerIcon.Size = new System.Drawing.Size(28, 28);
            this._headerIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._headerIcon.TabIndex = 0;
            this._headerIcon.TabStop = false;
            // 
            // _subtitleLabel
            // 
            this._subtitleLabel.AutoSize = true;
            this._subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this._subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._subtitleLabel.Location = new System.Drawing.Point(96, 56);
            this._subtitleLabel.Name = "_subtitleLabel";
            this._subtitleLabel.Size = new System.Drawing.Size(369, 25);
            this._subtitleLabel.TabIndex = 3;
            this._subtitleLabel.Text = "Danh sách khách hàng và lịch sử giao dịch";
            // 
            // _titleLabel
            // 
            this._titleLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this._titleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this._titleLabel.Location = new System.Drawing.Point(96, 0);
            this._titleLabel.Name = "_titleLabel";
            this._titleLabel.Size = new System.Drawing.Size(600, 52);
            this._titleLabel.TabIndex = 4;
            this._titleLabel.Text = "Quản lý khách hàng";
            this._titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lockButton
            // 
            this._lockButton.BackColor = System.Drawing.Color.White;
            this._lockButton.Enabled = false;
            this._lockButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this._lockButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this._lockButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._lockButton.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this._lockButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(83)))), ((int)(((byte)(9)))));
            this._lockButton.Location = new System.Drawing.Point(536, 28);
            this._lockButton.Name = "_lockButton";
            this._lockButton.Size = new System.Drawing.Size(110, 38);
            this._lockButton.TabIndex = 7;
            this._lockButton.Text = "Khóa";
            this._lockButton.UseVisualStyleBackColor = false;
            this._lockButton.Click += new System.EventHandler(this._lockButton_Click);
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
            this._refreshButton.Location = new System.Drawing.Point(1627, 28);
            this._refreshButton.Name = "_refreshButton";
            this._refreshButton.Size = new System.Drawing.Size(126, 38);
            this._refreshButton.TabIndex = 2;
            this._refreshButton.Text = "Làm mới";
            this._refreshButton.UseVisualStyleBackColor = false;
            this._refreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // _toolbar
            // 
            this._toolbar.BackColor = System.Drawing.Color.White;
            this._toolbar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._toolbar.Controls.Add(this._searchLabel);
            this._toolbar.Controls.Add(this._searchBox);
            this._toolbar.Controls.Add(this._statusLabel);
            this._toolbar.Controls.Add(this._statusFilter);
            this._toolbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this._toolbar.Location = new System.Drawing.Point(0, 108);
            this._toolbar.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this._toolbar.Name = "_toolbar";
            this._toolbar.Size = new System.Drawing.Size(1023, 70);
            this._toolbar.TabIndex = 1;
            // 
            // _searchLabel
            // 
            this._searchLabel.AutoSize = true;
            this._searchLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._searchLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._searchLabel.Location = new System.Drawing.Point(16, 8);
            this._searchLabel.Name = "_searchLabel";
            this._searchLabel.Size = new System.Drawing.Size(138, 23);
            this._searchLabel.TabIndex = 0;
            this._searchLabel.Text = "Tìm khách hàng";
            // 
            // _searchBox
            // 
            this._searchBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._searchBox.Location = new System.Drawing.Point(16, 31);
            this._searchBox.Name = "_searchBox";
            this._searchBox.Size = new System.Drawing.Size(560, 34);
            this._searchBox.TabIndex = 2;
            // 
            // _statusLabel
            // 
            this._statusLabel.AutoSize = true;
            this._statusLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._statusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._statusLabel.Location = new System.Drawing.Point(600, 8);
            this._statusLabel.Name = "_statusLabel";
            this._statusLabel.Size = new System.Drawing.Size(92, 23);
            this._statusLabel.TabIndex = 3;
            this._statusLabel.Text = "Trạng thái";
            // 
            // _statusFilter
            // 
            this._statusFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._statusFilter.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._statusFilter.Items.AddRange(new object[] {
            "Tất cả trạng thái",
            "Đang hoạt động",
            "Vô hiệu hóa"});
            this._statusFilter.Location = new System.Drawing.Point(600, 31);
            this._statusFilter.Margin = new System.Windows.Forms.Padding(8, 3, 0, 3);
            this._statusFilter.Name = "_statusFilter";
            this._statusFilter.Size = new System.Drawing.Size(300, 36);
            this._statusFilter.TabIndex = 1;
            // 
            // _gridCard
            // 
            this._gridCard.BackColor = System.Drawing.Color.White;
            this._gridCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._gridCard.Controls.Add(this._usersGrid);
            this._gridCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridCard.Location = new System.Drawing.Point(0, 190);
            this._gridCard.Margin = new System.Windows.Forms.Padding(0);
            this._gridCard.Name = "_gridCard";
            this._gridCard.Size = new System.Drawing.Size(1023, 418);
            this._gridCard.TabIndex = 2;
            // 
            // _usersGrid
            // 
            this._usersGrid.AllowUserToAddRows = false;
            this._usersGrid.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this._usersGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this._usersGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._usersGrid.BackgroundColor = System.Drawing.Color.White;
            this._usersGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._usersGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this._usersGrid.ColumnHeadersHeight = 40;
            this._usersGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Username,
            this.FullName,
            this.Email,
            this.Role,
            this.Status,
            this.Lock,
            this.Actions});
            this._usersGrid.ContextMenuStrip = this._rowMenu;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._usersGrid.DefaultCellStyle = dataGridViewCellStyle3;
            this._usersGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._usersGrid.EnableHeadersVisualStyles = false;
            this._usersGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this._usersGrid.Location = new System.Drawing.Point(0, 0);
            this._usersGrid.Margin = new System.Windows.Forms.Padding(1);
            this._usersGrid.MultiSelect = false;
            this._usersGrid.Name = "_usersGrid";
            this._usersGrid.ReadOnly = true;
            this._usersGrid.RowHeadersVisible = false;
            this._usersGrid.RowHeadersWidth = 62;
            this._usersGrid.RowTemplate.Height = 38;
            this._usersGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._usersGrid.Size = new System.Drawing.Size(1021, 416);
            this._usersGrid.TabIndex = 0;
            this._usersGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.UsersGrid_CellDoubleClick);
            this._usersGrid.SelectionChanged += new System.EventHandler(this.UsersGrid_SelectionChanged);
            // 
            // Username
            // 
            this.Username.FillWeight = 95F;
            this.Username.HeaderText = "Tên đăng nhập";
            this.Username.MinimumWidth = 8;
            this.Username.Name = "Username";
            this.Username.ReadOnly = true;
            // 
            // FullName
            // 
            this.FullName.FillWeight = 105F;
            this.FullName.HeaderText = "Họ và tên";
            this.FullName.MinimumWidth = 8;
            this.FullName.Name = "FullName";
            this.FullName.ReadOnly = true;
            // 
            // Email
            // 
            this.Email.FillWeight = 135F;
            this.Email.HeaderText = "Email";
            this.Email.MinimumWidth = 8;
            this.Email.Name = "Email";
            this.Email.ReadOnly = true;
            // 
            // Role
            // 
            this.Role.HeaderText = "Vai trò";
            this.Role.MinimumWidth = 8;
            this.Role.Name = "Role";
            this.Role.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.HeaderText = "Trạng thái";
            this.Status.MinimumWidth = 8;
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // Lock
            // 
            this.Lock.FillWeight = 75F;
            this.Lock.HeaderText = "Khóa";
            this.Lock.MinimumWidth = 8;
            this.Lock.Name = "Lock";
            this.Lock.ReadOnly = true;
            // 
            // Actions
            // 
            this.Actions.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Actions.HeaderText = "Thao tác";
            this.Actions.MinimumWidth = 8;
            this.Actions.Name = "Actions";
            this.Actions.ReadOnly = true;
            this.Actions.ToolTipText = "Xem, sửa hoặc khóa / mở khóa";
            this.Actions.Width = 120;
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
            this._rowMenu.ResumeLayout(false);
            this._surface.ResumeLayout(false);
            this._contentLayout.ResumeLayout(false);
            this._headerPanel.ResumeLayout(false);
            this._headerPanel.PerformLayout();
            this._headerIconPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._headerIcon)).EndInit();
            this._toolbar.ResumeLayout(false);
            this._toolbar.PerformLayout();
            this._gridCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._usersGrid)).EndInit();
            this.ResumeLayout(false);

        }

        private DataGridViewTextBoxColumn Username;
        private DataGridViewTextBoxColumn FullName;
        private DataGridViewTextBoxColumn Email;
        private DataGridViewTextBoxColumn Role;
        private DataGridViewTextBoxColumn Status;
        private DataGridViewTextBoxColumn Lock;
        private DataGridViewImageColumn Actions;
    }
}
