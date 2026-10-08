using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Controls;

namespace SIMS_WinFormsApp.Forms.SystemMgmt
{
    partial class frmAuditLogDetail
    {
        private TableLayoutPanel _previewInfoGrid;
        private Label _previewDiffTitle;
        private Panel _previewDiffPanel;

        private void InitializeComponent()
        {
            _previewInfoGrid = new TableLayoutPanel();
            _previewDiffTitle = new Label();
            _previewDiffPanel = new Panel();
            SuspendLayout();
            ContentHost.SuspendLayout();
            _previewInfoGrid.SuspendLayout();

            _previewInfoGrid.AutoSize = true;
            _previewInfoGrid.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            _previewInfoGrid.ColumnCount = 2;
            _previewInfoGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            _previewInfoGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            _previewInfoGrid.Dock = DockStyle.Top;
            _previewInfoGrid.Name = "auditInfoGrid";
            _previewInfoGrid.RowCount = 3;
            _previewInfoGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
            _previewInfoGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
            _previewInfoGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));

            var timePanel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(8) };
            timePanel.Controls.Add(new Label { Dock = DockStyle.Top, Text = "01/01/2026 12:00:00", Font = new Font(Font, FontStyle.Bold) });
            timePanel.Controls.Add(new Label { Dock = DockStyle.Top, Text = "Thời gian" });
            _previewInfoGrid.Controls.Add(timePanel, 0, 0);

            var userPanel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(8) };
            userPanel.Controls.Add(new Label { Dock = DockStyle.Top, Text = "Tên người dùng", Font = new Font(Font, FontStyle.Bold) });
            userPanel.Controls.Add(new Label { Dock = DockStyle.Top, Text = "Người dùng" });
            _previewInfoGrid.Controls.Add(userPanel, 1, 0);

            var actionPanel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(8) };
            actionPanel.Controls.Add(new Label { Dock = DockStyle.Top, Text = "Cập nhật", Font = new Font(Font, FontStyle.Bold) });
            actionPanel.Controls.Add(new Label { Dock = DockStyle.Top, Text = "Hành động" });
            _previewInfoGrid.Controls.Add(actionPanel, 0, 1);

            var targetPanel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(8) };
            targetPanel.Controls.Add(new Label { Dock = DockStyle.Top, Text = "Bản ghi", Font = new Font(Font, FontStyle.Bold) });
            targetPanel.Controls.Add(new Label { Dock = DockStyle.Top, Text = "Đối tượng" });
            _previewInfoGrid.Controls.Add(targetPanel, 1, 1);

            var ipPanel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(8) };
            ipPanel.Controls.Add(new Label { Dock = DockStyle.Top, Text = "-", Font = new Font(Font, FontStyle.Bold) });
            ipPanel.Controls.Add(new Label { Dock = DockStyle.Top, Text = "Địa chỉ IP" });
            _previewInfoGrid.Controls.Add(ipPanel, 0, 2);

            var descriptionPanel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(8) };
            descriptionPanel.Controls.Add(new Label { Dock = DockStyle.Top, Text = "Nội dung thay đổi", Font = new Font(Font, FontStyle.Bold) });
            descriptionPanel.Controls.Add(new Label { Dock = DockStyle.Top, Text = "Mô tả" });
            _previewInfoGrid.Controls.Add(descriptionPanel, 1, 2);

            _previewDiffTitle.Dock = DockStyle.Top;
            _previewDiffTitle.Height = 32;
            _previewDiffTitle.Text = "Thay đổi";
            _previewDiffTitle.Name = "diffTitle";

            _previewDiffPanel.Dock = DockStyle.Fill;
            _previewDiffPanel.Name = "diffPreview";
            _previewDiffPanel.Controls.Add(new Label { Dock = DockStyle.Fill, Text = "Giá trị trước  →  Giá trị sau", TextAlign = ContentAlignment.MiddleCenter });

            ContentHost.Controls.Add(_previewDiffPanel);
            ContentHost.Controls.Add(_previewDiffTitle);
            ContentHost.Controls.Add(_previewInfoGrid);
            _previewInfoGrid.ResumeLayout(false);
            ContentHost.ResumeLayout(false);
            ClientSize = new Size(760, 620);
            Name = "frmAuditLogDetail";
            Text = "Chi tiết nhật ký";
            ResumeLayout(false);
        }

    }
}
