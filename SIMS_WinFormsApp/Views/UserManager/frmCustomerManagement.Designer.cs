using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace SIMS_WinFormsApp.Views.UserManager
{
    partial class frmCustomerManagement
    {
        private System.ComponentModel.IContainer components = null;
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
        private Button _refreshButton;
        private Panel _gridCard;
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
            this._headerIconPanel = new System.Windows.Forms.Panel();
            this._headerIcon = new FontAwesome.Sharp.IconPictureBox();
            this._toolbar = new System.Windows.Forms.Panel();
            this._searchLabel = new System.Windows.Forms.Label();
            this._statusLabel = new System.Windows.Forms.Label();
            this._refreshButton = new System.Windows.Forms.Button();
            this._gridCard = new System.Windows.Forms.Panel();
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
            this._headerIconPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._headerIcon)).BeginInit();
            this._toolbar.SuspendLayout();
            this._gridCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._usersGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // _surface
            // 
            this._surface.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
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
            this._contentLayout.RowCount = 4;
            this._contentLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this._contentLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this._contentLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._contentLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this._contentLayout.Controls.Add(this._headerPanel, 0, 0);
            this._contentLayout.Controls.Add(this._toolbar, 0, 1);
            this._contentLayout.Controls.Add(this._gridCard, 0, 2);
            //
            // _headerPanel
            //
            this._headerPanel.BackColor = System.Drawing.Color.White;
            this._headerPanel.Controls.Add(this._headerIconPanel);
            this._headerPanel.Controls.Add(this._subtitleLabel);
            this._headerPanel.Controls.Add(this._titleLabel);
            this._headerPanel.Controls.Add(this._refreshButton);
            this._headerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._headerPanel.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this._headerPanel.Name = "_headerPanel";
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
            this._headerIcon.IconChar = FontAwesome.Sharp.IconChar.User;
            this._headerIcon.IconColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this._headerIcon.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._headerIcon.IconSize = 28;
            this._headerIcon.Location = new System.Drawing.Point(14, 14);
            this._headerIcon.Size = new System.Drawing.Size(28, 28);
            this._headerIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._headerIcon.TabStop = false;
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
            this._searchLabel.Text = "Tìm khách hàng";
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
            this._statusFilter.Location = new System.Drawing.Point(600, 31);
            this._statusFilter.Name = "_statusFilter";
            this._statusFilter.Size = new System.Drawing.Size(300, 31);
            this._statusFilter.TabIndex = 1;
            // 
            // _searchBox
            // 
            this._searchBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._searchBox.Name = "_searchBox";
            this._searchBox.Location = new System.Drawing.Point(16, 31);
            this._searchBox.Size = new System.Drawing.Size(560, 30);
            this._searchBox.TabIndex = 2;
            // 
            // _subtitleLabel
            // 
            this._subtitleLabel.AutoSize = true;
            this._subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this._subtitleLabel.Location = new System.Drawing.Point(96, 56);
            this._subtitleLabel.Name = "_subtitleLabel";
            this._subtitleLabel.Size = new System.Drawing.Size(600, 22);
            this._subtitleLabel.Size = new System.Drawing.Size(377, 28);
            this._subtitleLabel.TabIndex = 3;
            this._subtitleLabel.Text = "Danh sách khách hàng và lịch sử giao dịch";
            // 
            // _titleLabel
            // 
            this._titleLabel.AutoSize = false;
            this._titleLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this._titleLabel.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this._titleLabel.Location = new System.Drawing.Point(96, 18);
            this._titleLabel.Name = "_titleLabel";
            this._titleLabel.Size = new System.Drawing.Size(600, 34);
            this._titleLabel.TabIndex = 4;
            this._titleLabel.Text = "Quản lý khách hàng";
            this._titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this._refreshButton.UseVisualStyleBackColor = false;
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
            this._headerPanel.ResumeLayout(false);
            this._headerPanel.PerformLayout();
            this._headerIconPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._headerIcon)).EndInit();
            this._toolbar.ResumeLayout(false);
            this._toolbar.PerformLayout();
            this._gridCard.ResumeLayout(false);
            this._contentLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._usersGrid)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
