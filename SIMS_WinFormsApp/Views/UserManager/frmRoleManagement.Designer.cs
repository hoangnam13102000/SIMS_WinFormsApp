using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Views.UserManager
{
    partial class frmRoleManagement
    {
        private TableLayoutPanel _roleLayout;
        private Panel _rolesColumn;
        private Label _rolesCaption;
        private ListBox _lstRoles;
        private Panel _permissionsColumn;
        private Label _lblSelectedRoleName;
        private Label _lblSelectedRoleDescription;
        private Panel _separator;
        private Panel _permissionListPanel;

        private void InitializeComponent()
        {
            _roleLayout = new TableLayoutPanel();
            _rolesColumn = new Panel();
            _rolesCaption = new Label();
            _lstRoles = new ListBox();
            _permissionsColumn = new Panel();
            _lblSelectedRoleName = new Label();
            _lblSelectedRoleDescription = new Label();
            _separator = new Panel();
            _permissionListPanel = new Panel();

            SuspendLayout();
            ContentHost.SuspendLayout();
            _roleLayout.SuspendLayout();
            _rolesColumn.SuspendLayout();
            _permissionsColumn.SuspendLayout();

            ContentHost.Padding = Padding.Empty;

            _roleLayout.BackColor = Color.Transparent;
            _roleLayout.ColumnCount = 2;
            _roleLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 220F));
            _roleLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _roleLayout.Dock = DockStyle.Fill;
            _roleLayout.Name = "roleLayout";
            _roleLayout.RowCount = 1;
            _roleLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            _roleLayout.Controls.Add(_rolesColumn, 0, 0);
            _roleLayout.Controls.Add(_permissionsColumn, 1, 0);

            _rolesColumn.BackColor = Color.Transparent;
            _rolesColumn.Dock = DockStyle.Fill;
            _rolesColumn.Name = "rolesColumn";
            _rolesColumn.Padding = new Padding(20, 20, 12, 20);

            _rolesCaption.BackColor = Color.Transparent;
            _rolesCaption.Dock = DockStyle.Top;
            _rolesCaption.Font = AppFonts.SmallBold;
            _rolesCaption.ForeColor = AppColors.TextMuted;
            _rolesCaption.Height = 26;
            _rolesCaption.Name = "rolesCaption";
            _rolesCaption.Text = "Vai trò";

            _lstRoles.BorderStyle = BorderStyle.FixedSingle;
            _lstRoles.Dock = DockStyle.Fill;
            _lstRoles.Font = AppFonts.Body;
            _lstRoles.IntegralHeight = false;
            _lstRoles.Name = "rolesList";
            _lstRoles.SelectedIndexChanged += LstRoles_SelectedIndexChanged;
            _rolesColumn.Controls.Add(_lstRoles);
            _rolesColumn.Controls.Add(_rolesCaption);

            _permissionsColumn.BackColor = Color.Transparent;
            _permissionsColumn.Dock = DockStyle.Fill;
            _permissionsColumn.Name = "permissionsColumn";
            _permissionsColumn.Padding = new Padding(12, 20, 20, 20);

            _lblSelectedRoleName.BackColor = Color.Transparent;
            _lblSelectedRoleName.Dock = DockStyle.Top;
            _lblSelectedRoleName.Font = AppFonts.Subtitle;
            _lblSelectedRoleName.ForeColor = AppColors.TextTitle;
            _lblSelectedRoleName.Height = 28;
            _lblSelectedRoleName.Name = "selectedRoleName";

            _lblSelectedRoleDescription.BackColor = Color.Transparent;
            _lblSelectedRoleDescription.Dock = DockStyle.Top;
            _lblSelectedRoleDescription.Font = AppFonts.Small;
            _lblSelectedRoleDescription.ForeColor = AppColors.TextMuted;
            _lblSelectedRoleDescription.Height = 22;
            _lblSelectedRoleDescription.Name = "selectedRoleDescription";

            _separator.BackColor = Color.Transparent;
            _separator.Dock = DockStyle.Top;
            _separator.Height = 12;
            _separator.Name = "roleSeparator";
            _separator.Paint += Separator_Paint;

            _permissionListPanel.AutoScroll = true;
            _permissionListPanel.BackColor = Color.Transparent;
            _permissionListPanel.Dock = DockStyle.Fill;
            _permissionListPanel.Name = "permissionListPanel";

            _permissionsColumn.Controls.Add(_permissionListPanel);
            _permissionsColumn.Controls.Add(_separator);
            _permissionsColumn.Controls.Add(_lblSelectedRoleDescription);
            _permissionsColumn.Controls.Add(_lblSelectedRoleName);

            ContentHost.Controls.Add(_roleLayout);
            ContentHost.ResumeLayout(false);
            _permissionsColumn.ResumeLayout(false);
            _rolesColumn.ResumeLayout(false);
            _roleLayout.ResumeLayout(false);
            ClientSize = new Size(980, 640);
            Name = "frmRoleManagement";
            Text = "Quản lý phân quyền vai trò";
            ResumeLayout(false);
        }
    }
}