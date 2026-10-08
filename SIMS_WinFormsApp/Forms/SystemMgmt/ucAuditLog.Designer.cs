using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Forms.SystemMgmt
{
    partial class ucAuditLog
    {
        private Panel _designerPageHeader;
        private Panel _designerFilterSurface;
        private Panel _designerTable;

        private void InitializeComponent()
        {
            _designerPageHeader = new Panel();
            _designerFilterSurface = new Panel();
            _designerTable = new Panel();
            SuspendLayout();

            _designerPageHeader.Dock = DockStyle.Top;
            _designerPageHeader.Height = 112;
            _designerPageHeader.Name = "auditLogHeader";
            _designerPageHeader.Controls.Add(new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                Location = new Point(20, 20),
                Text = "Nhật ký hệ thống"
            });

            _designerFilterSurface.Dock = DockStyle.Top;
            _designerFilterSurface.Height = 112;
            _designerFilterSurface.Name = "auditLogFilters";
            _designerFilterSurface.Padding = new Padding(16);
            _designerFilterSurface.Controls.Add(new Label
            {
                Dock = DockStyle.Fill,
                Text = "Khoảng ngày     Tìm kiếm     Hành động     Bảng dữ liệu",
                TextAlign = ContentAlignment.MiddleLeft
            });

            _designerTable.Dock = DockStyle.Fill;
            _designerTable.Name = "auditLogTable";
            _designerTable.Padding = new Padding(12);
            _designerTable.Controls.Add(new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ColumnHeadersHeight = 38,
                Name = "auditGrid"
            });

            Controls.Add(_designerTable);
            Controls.Add(_designerFilterSurface);
            Controls.Add(_designerPageHeader);
            Name = "ucAuditLog";
            Size = new Size(1200, 760);
            ResumeLayout(false);
        }
    }
}
