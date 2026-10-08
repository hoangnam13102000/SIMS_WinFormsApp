using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Views.UserManager
{
    partial class ucRolePermission
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel _root;
        private Panel _headerPanel;
        private Label _titleLabel;
        private Label _subtitleLabel;
        private FlowLayoutPanel _headerActions;
        private Button _resetButton;
        private Button _saveButton;
        private TableLayoutPanel _content;
        private Panel _rolesPanel;
        private Label _rolesHeaderLabel;
        private Label _roleCountLabel;
        private TextBox _roleSearchTextBox;
        private DataGridView _rolesGrid;
        private DataGridViewTextBoxColumn _roleNameColumn;
        private DataGridViewTextBoxColumn _roleCountColumn;
        private DataGridViewTextBoxColumn _roleCodeColumn;
        private Label _rolesEmptyLabel;
        private Panel _permissionsPanel;
        private Label _permissionsTitleLabel;
        private Label _adminInfoLabel;
        private DataGridView _permissionsGrid;
        private DataGridViewTextBoxColumn _groupColumn;
        private DataGridViewTextBoxColumn _permissionColumn;
        private DataGridViewTextBoxColumn _descriptionColumn;
        private DataGridViewCheckBoxColumn _enabledColumn;
        private Label _permissionEmptyLabel;
        private Panel _busyOverlay;
        private Label _busyMessageLabel;
        private ProgressBar _busyProgressBar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            _root = new TableLayoutPanel();
            _headerPanel = new Panel();
            _titleLabel = new Label();
            _subtitleLabel = new Label();
            _headerActions = new FlowLayoutPanel();
            _resetButton = new Button();
            _saveButton = new Button();
            _content = new TableLayoutPanel();
            _rolesPanel = new Panel();
            _rolesHeaderLabel = new Label();
            _roleCountLabel = new Label();
            _roleSearchTextBox = new TextBox();
            _rolesGrid = new DataGridView();
            _roleNameColumn = new DataGridViewTextBoxColumn();
            _roleCountColumn = new DataGridViewTextBoxColumn();
            _roleCodeColumn = new DataGridViewTextBoxColumn();
            _rolesEmptyLabel = new Label();
            _permissionsPanel = new Panel();
            _permissionsTitleLabel = new Label();
            _adminInfoLabel = new Label();
            _permissionsGrid = new DataGridView();
            _groupColumn = new DataGridViewTextBoxColumn();
            _permissionColumn = new DataGridViewTextBoxColumn();
            _descriptionColumn = new DataGridViewTextBoxColumn();
            _enabledColumn = new DataGridViewCheckBoxColumn();
            _permissionEmptyLabel = new Label();
            _busyOverlay = new Panel();
            _busyMessageLabel = new Label();
            _busyProgressBar = new ProgressBar();
            _root.SuspendLayout();
            _headerPanel.SuspendLayout();
            _headerActions.SuspendLayout();
            _content.SuspendLayout();
            _rolesPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_rolesGrid).BeginInit();
            _permissionsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_permissionsGrid).BeginInit();
            _busyOverlay.SuspendLayout();
            SuspendLayout();

            BackColor = Color.FromArgb(248, 250, 252);
            Padding = new Padding(20);
            _root.ColumnCount = 1;
            _root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _root.Dock = DockStyle.Fill;
            _root.RowCount = 2;
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 96F));
            _root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            _root.Controls.Add(_headerPanel, 0, 0);
            _root.Controls.Add(_content, 0, 1);

            _headerPanel.Dock = DockStyle.Fill;
            _headerPanel.Controls.Add(_subtitleLabel);
            _headerPanel.Controls.Add(_titleLabel);
            _headerPanel.Controls.Add(_headerActions);
            _titleLabel.AutoSize = true;
            _titleLabel.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
            _titleLabel.ForeColor = Color.FromArgb(15, 23, 42);
            _titleLabel.Location = new Point(0, 0);
            _titleLabel.Text = "Phân quyền vai trò";
            _subtitleLabel.AutoSize = true;
            _subtitleLabel.ForeColor = Color.FromArgb(100, 116, 139);
            _subtitleLabel.Location = new Point(2, 45);
            _subtitleLabel.Text = "Chọn vai trò, điều chỉnh quyền và lưu thay đổi";
            _headerActions.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _headerActions.AutoSize = true;
            _headerActions.FlowDirection = FlowDirection.LeftToRight;
            _headerActions.Location = new Point(600, 18);
            _headerActions.WrapContents = false;
            _headerActions.Controls.AddRange(new Control[] { _resetButton, _saveButton });
            ConfigureButton(_resetButton, "Khôi phục mặc định", false, 176);
            ConfigureButton(_saveButton, "Lưu thay đổi", true, 132);

            _content.ColumnCount = 2;
            _content.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 370F));
            _content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _content.Dock = DockStyle.Fill;
            _content.Padding = new Padding(0, 8, 0, 0);
            _content.RowCount = 1;
            _content.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            _content.Controls.Add(_rolesPanel, 0, 0);
            _content.Controls.Add(_permissionsPanel, 1, 0);

            ConfigureCard(_rolesPanel);
            _rolesPanel.Controls.Add(_rolesGrid);
            _rolesPanel.Controls.Add(_rolesEmptyLabel);
            _rolesPanel.Controls.Add(_roleSearchTextBox);
            _rolesPanel.Controls.Add(_roleCountLabel);
            _rolesPanel.Controls.Add(_rolesHeaderLabel);
            _rolesHeaderLabel.AutoSize = true;
            _rolesHeaderLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _rolesHeaderLabel.ForeColor = Color.FromArgb(71, 85, 105);
            _rolesHeaderLabel.Location = new Point(16, 14);
            _rolesHeaderLabel.Text = "VAI TRÒ";
            _roleCountLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _roleCountLabel.AutoSize = true;
            _roleCountLabel.ForeColor = Color.FromArgb(100, 116, 139);
            _roleCountLabel.Location = new Point(270, 17);
            _roleCountLabel.Text = "0 vai trò";
            _roleSearchTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _roleSearchTextBox.Location = new Point(16, 44);
            _roleSearchTextBox.Name = "_roleSearchTextBox";
            _roleSearchTextBox.Size = new Size(336, 27);
            _roleSearchTextBox.AccessibleName = "Tìm vai trò";
            _rolesGrid.AllowUserToAddRows = false;
            _rolesGrid.AllowUserToDeleteRows = false;
            _rolesGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _rolesGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ConfigureGrid(_rolesGrid);
            _rolesGrid.Location = new Point(16, 82);
            _rolesGrid.MultiSelect = false;
            _rolesGrid.ReadOnly = true;
            _rolesGrid.RowTemplate.Height = 34;
            _rolesGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _rolesGrid.Columns.AddRange(new DataGridViewColumn[] {
                _roleNameColumn, _roleCountColumn, _roleCodeColumn
            });
            ConfigureTextColumn(_roleNameColumn, "Vai trò", "RoleName", 48F);
            ConfigureTextColumn(_roleCountColumn, "Quyền", "PermissionCount", 25F);
            ConfigureTextColumn(_roleCodeColumn, "Mã", "RoleCode", 27F);
            _roleCodeColumn.Visible = false;
            _rolesEmptyLabel.Anchor = AnchorStyles.None;
            _rolesEmptyLabel.AutoSize = true;
            _rolesEmptyLabel.ForeColor = Color.FromArgb(100, 116, 139);
            _rolesEmptyLabel.Location = new Point(92, 280);
            _rolesEmptyLabel.Text = "Không tìm thấy vai trò phù hợp.";
            _rolesEmptyLabel.Visible = false;

            ConfigureCard(_permissionsPanel);
            _permissionsPanel.Controls.Add(_permissionsGrid);
            _permissionsPanel.Controls.Add(_permissionEmptyLabel);
            _permissionsPanel.Controls.Add(_adminInfoLabel);
            _permissionsPanel.Controls.Add(_permissionsTitleLabel);
            _permissionsTitleLabel.AutoEllipsis = true;
            _permissionsTitleLabel.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            _permissionsTitleLabel.ForeColor = Color.FromArgb(15, 23, 42);
            _permissionsTitleLabel.Location = new Point(16, 14);
            _permissionsTitleLabel.Size = new Size(650, 34);
            _permissionsTitleLabel.Text = "Quyền của vai trò";
            _permissionsTitleLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _adminInfoLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _adminInfoLabel.BackColor = Color.FromArgb(239, 246, 255);
            _adminInfoLabel.ForeColor = Color.FromArgb(30, 64, 175);
            _adminInfoLabel.Location = new Point(16, 54);
            _adminInfoLabel.Padding = new Padding(10, 8, 10, 8);
            _adminInfoLabel.Size = new Size(650, 48);
            _adminInfoLabel.Text = "Quản trị viên luôn có toàn quyền hệ thống. Các quyền được bật và không thể chỉnh sửa.";
            _adminInfoLabel.Visible = false;
            _permissionsGrid.AllowUserToAddRows = false;
            _permissionsGrid.AllowUserToDeleteRows = false;
            _permissionsGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _permissionsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ConfigureGrid(_permissionsGrid);
            _permissionsGrid.Location = new Point(16, 58);
            _permissionsGrid.RowTemplate.Height = 42;
            _permissionsGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            _permissionsGrid.Columns.AddRange(new DataGridViewColumn[] {
                _groupColumn, _permissionColumn, _descriptionColumn, _enabledColumn
            });
            ConfigureTextColumn(_groupColumn, "Nhóm", "Group", 18F);
            ConfigureTextColumn(_permissionColumn, "Quyền", "Permission", 25F);
            ConfigureTextColumn(_descriptionColumn, "Mô tả", "Description", 47F);
            _descriptionColumn.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            _enabledColumn.FillWeight = 10F;
            _enabledColumn.HeaderText = "Bật";
            _enabledColumn.Name = "Enabled";
            _enabledColumn.Width = 60;
            _permissionEmptyLabel.Anchor = AnchorStyles.None;
            _permissionEmptyLabel.AutoSize = true;
            _permissionEmptyLabel.ForeColor = Color.FromArgb(100, 116, 139);
            _permissionEmptyLabel.Location = new Point(210, 280);
            _permissionEmptyLabel.Text = "Chọn một vai trò để xem quyền.";
            _permissionEmptyLabel.Visible = true;

            _busyOverlay.BackColor = Color.FromArgb(235, 255, 255, 255);
            _busyOverlay.Dock = DockStyle.Fill;
            _busyOverlay.Controls.Add(_busyProgressBar);
            _busyOverlay.Controls.Add(_busyMessageLabel);
            _busyOverlay.Visible = false;
            _busyMessageLabel.Anchor = AnchorStyles.None;
            _busyMessageLabel.AutoSize = true;
            _busyMessageLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            _busyMessageLabel.Location = new Point(370, 325);
            _busyMessageLabel.Text = "Đang tải phân quyền...";
            _busyProgressBar.Anchor = AnchorStyles.None;
            _busyProgressBar.Location = new Point(370, 365);
            _busyProgressBar.Size = new Size(300, 8);
            _busyProgressBar.Style = ProgressBarStyle.Marquee;
            _busyProgressBar.MarqueeAnimationSpeed = 25;

            Controls.Add(_root);
            Controls.Add(_busyOverlay);
            Name = "ucRolePermission";
            Size = new Size(1200, 760);
            _busyOverlay.BringToFront();
            _busyOverlay.ResumeLayout(false);
            _permissionsPanel.ResumeLayout(false);
            _permissionsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_permissionsGrid).EndInit();
            _rolesPanel.ResumeLayout(false);
            _rolesPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_rolesGrid).EndInit();
            _content.ResumeLayout(false);
            _headerActions.ResumeLayout(false);
            _headerPanel.ResumeLayout(false);
            _headerPanel.PerformLayout();
            _root.ResumeLayout(false);
            ResumeLayout(false);
        }

        private static void ConfigureCard(Panel panel)
        {
            panel.BackColor = Color.White;
            panel.Dock = DockStyle.Fill;
            panel.Margin = new Padding(0, 0, 16, 0);
            panel.Padding = new Padding(16);
            panel.BorderStyle = BorderStyle.FixedSingle;
        }

        private static void ConfigureGrid(DataGridView grid)
        {
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.None;
            grid.ColumnHeadersHeight = 38;
            grid.EnableHeadersVisualStyles = false;
            grid.GridColor = Color.FromArgb(226, 232, 240);
            grid.RowHeadersVisible = false;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(71, 85, 105);
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            grid.DefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);
        }

        private static void ConfigureTextColumn(
            DataGridViewTextBoxColumn column, string title, string name, float fill)
        {
            column.FillWeight = fill;
            column.HeaderText = title;
            column.Name = name;
        }

        private static void ConfigureButton(Button button, string text, bool primary, int width)
        {
            button.BackColor = primary ? Color.FromArgb(37, 99, 235) : Color.White;
            button.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            button.FlatStyle = FlatStyle.Flat;
            button.ForeColor = primary ? Color.White : Color.FromArgb(51, 65, 85);
            button.Margin = new Padding(0, 0, 10, 0);
            button.Size = new Size(width, 40);
            button.Text = text;
        }
    }
}
