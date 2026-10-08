using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Controls;

namespace SIMS_WinFormsApp.Forms.SystemMgmt
{
    partial class ucSystemSettings
    {
        private TableLayoutPanel _designerSettings;
        private Panel _designerSettingsHeader;
        private PrimaryButton _designerSaveButton;

        private void InitializeComponent()
        {
            _designerSettings = new TableLayoutPanel();
            _designerSettingsHeader = new Panel();
            _designerSaveButton = new PrimaryButton();
            SuspendLayout();
            _designerSettings.SuspendLayout();
            _designerSettingsHeader.SuspendLayout();

            _designerSettings.Dock = DockStyle.Fill;
            _designerSettings.ColumnCount = 2;
            _designerSettings.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            _designerSettings.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            _designerSettings.RowCount = 4;
            _designerSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 112F));
            _designerSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            _designerSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            _designerSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            _designerSettings.Name = "settingsLayout";

            _designerSettingsHeader.Dock = DockStyle.Fill;
            _designerSettingsHeader.Name = "settingsHeader";
            _designerSettingsHeader.Controls.Add(new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                Location = new Point(8, 24),
                Text = "Cài đặt hệ thống"
            });
            _designerSettings.Controls.Add(_designerSettingsHeader, 0, 0);
            _designerSettings.SetColumnSpan(_designerSettingsHeader, 2);

            var storeName = new LabeledIconField { Dock = DockStyle.Fill, LabelText = "Tên cửa hàng" };
            _designerSettings.Controls.Add(storeName, 0, 1);
            var defaultUnit = new LabeledIconField { Dock = DockStyle.Fill, LabelText = "Đơn vị mặc định" };
            _designerSettings.Controls.Add(defaultUnit, 1, 1);
            var vatRate = new LabeledIconField { Dock = DockStyle.Fill, LabelText = "Thuế VAT (%)" };
            _designerSettings.Controls.Add(vatRate, 0, 2);
            var defaultMargin = new LabeledIconField { Dock = DockStyle.Fill, LabelText = "Biên lợi nhuận mặc định (%)" };
            _designerSettings.Controls.Add(defaultMargin, 1, 2);
            var returnDays = new LabeledIconField { Dock = DockStyle.Fill, LabelText = "Số ngày đổi trả" };
            _designerSettings.Controls.Add(returnDays, 0, 3);
            var approvalThreshold = new LabeledIconField { Dock = DockStyle.Fill, LabelText = "Ngưỡng duyệt" };
            _designerSettings.Controls.Add(approvalThreshold, 1, 3);

            _designerSaveButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _designerSaveButton.Location = new Point(900, 640);
            _designerSaveButton.Name = "saveSettingsButton";
            _designerSaveButton.Size = new Size(180, 46);
            _designerSaveButton.Text = "Lưu thay đổi";
            Controls.Add(_designerSettings);
            Controls.Add(_designerSaveButton);
            Name = "ucSystemSettings";
            Size = new Size(1120, 720);
            _designerSettingsHeader.ResumeLayout(false);
            _designerSettingsHeader.PerformLayout();
            _designerSettings.ResumeLayout(false);
            _designerSettings.PerformLayout();
            ResumeLayout(false);
        }
    }
}
