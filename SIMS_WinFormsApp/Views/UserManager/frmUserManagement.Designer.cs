using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Views.UserManager
{
    partial class frmUserManagement
    {
        private System.ComponentModel.IContainer components = null;
        private Panel _designPreview;
        private TableLayoutPanel _contentLayout;
        private Panel _headerPanel;
        private Label _titleLabel;
        private Label _subtitleLabel;
        private FlowLayoutPanel _toolbar;
        private TextBox _searchBox;
        private ComboBox _statusFilter;
        private DataGridView _usersGrid;
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
            this._designPreview = new System.Windows.Forms.Panel();
            this._contentLayout = new System.Windows.Forms.TableLayoutPanel();
            this._toolbar = new System.Windows.Forms.FlowLayoutPanel();
            this._usersGrid = new System.Windows.Forms.DataGridView();
            this.Username = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Role = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._actionsColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this._statusFilter = new System.Windows.Forms.ComboBox();
            this._searchBox = new System.Windows.Forms.TextBox();
            this._headerPanel = new System.Windows.Forms.Panel();
            this._subtitleLabel = new System.Windows.Forms.Label();
            this._titleLabel = new System.Windows.Forms.Label();
            this._designPreview.SuspendLayout();
            this._contentLayout.SuspendLayout();
            this._toolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._usersGrid)).BeginInit();
            this._headerPanel.SuspendLayout();
            this.SuspendLayout();
            //
            // _designPreview
            //
            this._designPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
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
            this._contentLayout.RowCount = 3;
            this._contentLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 102F));
            this._contentLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this._contentLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._contentLayout.Controls.Add(this._headerPanel, 0, 0);
            this._contentLayout.Controls.Add(this._toolbar, 0, 1);
            this._contentLayout.Controls.Add(this._usersGrid, 0, 2);
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
            this.Username,
            this.FullName,
            this.Email,
            this.Role,
            this.Status,
            this._actionsColumn});
            this._usersGrid.Name = "_usersGrid";
            this._usersGrid.ReadOnly = true;
            this._usersGrid.RowHeadersVisible = false;
            this._usersGrid.RowHeadersWidth = 62;
            this._usersGrid.RowTemplate.Height = 38;
            this._usersGrid.Size = new System.Drawing.Size(952, 480);
            this._usersGrid.TabIndex = 3;
            //
            // Username
            //
            this.Username.HeaderText = "Tên đăng nhập";
            this.Username.MinimumWidth = 8;
            this.Username.Name = "Username";
            this.Username.ReadOnly = true;
            this.Username.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Username.FillWeight = 100F;
            //
            // FullName
            //
            this.FullName.HeaderText = "Họ và tên";
            this.FullName.MinimumWidth = 8;
            this.FullName.Name = "FullName";
            this.FullName.ReadOnly = true;
            this.FullName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FullName.FillWeight = 110F;
            //
            // Email
            //
            this.Email.HeaderText = "Email";
            this.Email.MinimumWidth = 8;
            this.Email.Name = "Email";
            this.Email.ReadOnly = true;
            this.Email.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Email.FillWeight = 140F;
            //
            // Role
            //
            this.Role.HeaderText = "Vai trò";
            this.Role.MinimumWidth = 8;
            this.Role.Name = "Role";
            this.Role.ReadOnly = true;
            this.Role.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Role.FillWeight = 110F;
            //
            // Status
            //
            this.Status.HeaderText = "Trạng thái";
            this.Status.MinimumWidth = 8;
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Status.FillWeight = 95F;
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
            this._statusFilter.TabIndex = 2;
            //
            // _searchBox
            //
            this._searchBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._searchBox.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this._searchBox.Name = "_searchBox";
            this._searchBox.Size = new System.Drawing.Size(360, 34);
            this._searchBox.TabIndex = 1;
            //
            // _headerPanel
            //
            this._headerPanel.BackColor = System.Drawing.Color.Transparent;
            this._headerPanel.Controls.Add(this._subtitleLabel);
            this._headerPanel.Controls.Add(this._titleLabel);
            this._headerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._headerPanel.Margin = System.Windows.Forms.Padding.Empty;
            this._headerPanel.Name = "_headerPanel";
            this._headerPanel.Size = new System.Drawing.Size(952, 102);
            this._headerPanel.TabIndex = 0;
            //
            // _subtitleLabel
            //
            this._subtitleLabel.AutoSize = true;
            this._subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._subtitleLabel.Location = new System.Drawing.Point(2, 52);
            this._subtitleLabel.Name = "_subtitleLabel";
            this._subtitleLabel.Size = new System.Drawing.Size(544, 28);
            this._subtitleLabel.TabIndex = 1;
            this._subtitleLabel.Text = "Quản lý tài khoản người dùng và phân quyền trong hệ thống";
            //
            // _titleLabel
            //
            this._titleLabel.AutoSize = true;
            this._titleLabel.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this._titleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._titleLabel.Location = new System.Drawing.Point(0, 0);
            this._titleLabel.Name = "_titleLabel";
            this._titleLabel.Size = new System.Drawing.Size(426, 65);
            this._titleLabel.TabIndex = 0;
            this._titleLabel.Text = "Quản lý tài khoản";
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
            ((System.ComponentModel.ISupportInitialize)(this._usersGrid)).EndInit();
            this._headerPanel.ResumeLayout(false);
            this._headerPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        private DataGridViewTextBoxColumn Username;
        private DataGridViewTextBoxColumn FullName;
        private DataGridViewTextBoxColumn Email;
        private DataGridViewTextBoxColumn Role;
        private DataGridViewTextBoxColumn Status;
    }
}
