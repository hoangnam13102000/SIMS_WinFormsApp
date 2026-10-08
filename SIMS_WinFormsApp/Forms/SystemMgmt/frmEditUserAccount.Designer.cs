using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Controls;

namespace SIMS_WinFormsApp.Forms.SystemMgmt
{
    partial class frmEditUserAccount
    {
        private TableLayoutPanel _designerFields;

        private void InitializeComponent()
        {
            _designerFields = new TableLayoutPanel();
            SuspendLayout();
            ContentHost.SuspendLayout();
            _designerFields.SuspendLayout();

            _designerFields.AutoSize = true;
            _designerFields.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            _designerFields.ColumnCount = 3;
            _designerFields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.333F));
            _designerFields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.334F));
            _designerFields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.333F));
            _designerFields.Dock = DockStyle.Top;
            _designerFields.Name = "accountFields";
            _designerFields.RowCount = 6;
            _designerFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 82F));
            _designerFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 82F));
            _designerFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 82F));
            _designerFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 82F));
            _designerFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 82F));
            _designerFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 82F));

            var fullName = new LabeledIconField { LabelText = "Họ và tên", Dock = DockStyle.Fill };
            _designerFields.Controls.Add(fullName, 0, 0);
            _designerFields.SetColumnSpan(fullName, 2);
            var email = new LabeledIconField { LabelText = "Email", Dock = DockStyle.Fill };
            _designerFields.Controls.Add(email, 2, 0);
            var phone = new LabeledIconField { LabelText = "Số điện thoại", Dock = DockStyle.Fill };
            _designerFields.Controls.Add(phone, 0, 1);
            var birth = new LabeledDateField { LabelText = "Ngày sinh", Dock = DockStyle.Fill };
            _designerFields.Controls.Add(birth, 1, 1);
            var gender = new LabeledComboField { LabelText = "Giới tính", Dock = DockStyle.Fill };
            _designerFields.Controls.Add(gender, 2, 1);
            var hireDate = new LabeledDateField { LabelText = "Ngày vào làm", Dock = DockStyle.Fill };
            _designerFields.Controls.Add(hireDate, 0, 2);
            var salary = new LabeledIconField { LabelText = "Lương", Dock = DockStyle.Fill };
            _designerFields.Controls.Add(salary, 1, 2);
            var accountInfo = new Label { Text = "Thông tin tài khoản", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
            _designerFields.Controls.Add(accountInfo, 0, 3);
            _designerFields.SetColumnSpan(accountInfo, 3);

            ContentHost.Controls.Add(_designerFields);
            _designerFields.ResumeLayout(false);
            _designerFields.PerformLayout();
            ContentHost.ResumeLayout(false);
            ClientSize = new Size(1240, 720);
            Name = "frmEditUserAccount";
            Text = "Cập nhật tài khoản";
            ResumeLayout(false);
        }
    }
}
