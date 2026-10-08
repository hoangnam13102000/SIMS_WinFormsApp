using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Controls;

namespace SIMS_WinFormsApp.Forms.SystemMgmt
{
    partial class ucRolePermission
    {
        private Panel _designerRolePanel;
        private Panel _designerPermissionPanel;

        private void InitializeComponent()
        {
            _designerRolePanel = new Panel();
            _designerPermissionPanel = new Panel();
            SuspendLayout();
            _designerRolePanel.SuspendLayout();
            _designerPermissionPanel.SuspendLayout();

            _designerRolePanel.Dock = DockStyle.Left;
            _designerRolePanel.Width = 408;
            _designerRolePanel.Padding = new Padding(12);
            _designerRolePanel.Name = "roleListPanel";
            _designerRolePanel.Controls.Add(new ListBox { Dock = DockStyle.Fill, Name = "roleList" });
            _designerRolePanel.Controls.Add(new TextBox { Dock = DockStyle.Top, Name = "roleSearch", Text = "Tìm vai trò" });
            _designerRolePanel.Controls.Add(new Label { Dock = DockStyle.Top, Height = 32, Text = "VAI TRÒ" });

            _designerPermissionPanel.Dock = DockStyle.Fill;
            _designerPermissionPanel.Padding = new Padding(16);
            _designerPermissionPanel.Name = "permissionDetailsPanel";
            _designerPermissionPanel.Controls.Add(new CheckedListBox
            {
                Dock = DockStyle.Fill,
                CheckOnClick = true,
                Items = { "Xem dữ liệu", "Thêm dữ liệu", "Sửa dữ liệu", "Xóa dữ liệu" },
                Name = "permissionList"
            });
            _designerPermissionPanel.Controls.Add(new Label { Dock = DockStyle.Top, Height = 36, Text = "QUYỀN CỦA VAI TRÒ" });
            var buttons = new Panel { Dock = DockStyle.Bottom, Height = 52 };
            buttons.Controls.Add(new PrimaryButton { Dock = DockStyle.Right, Text = "Lưu thay đổi" });
            buttons.Controls.Add(new PrimaryButton { Dock = DockStyle.Right, IsPrimary = false, Text = "Đặt lại" });
            _designerPermissionPanel.Controls.Add(buttons);

            Controls.Add(_designerPermissionPanel);
            Controls.Add(_designerRolePanel);
            Name = "ucRolePermission";
            Size = new Size(1200, 760);
            _designerPermissionPanel.ResumeLayout(false);
            _designerRolePanel.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
