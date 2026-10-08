using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Controls;

namespace SIMS_WinFormsApp.Views.UserManager
{
    partial class frmAddEmployee
    {
        private Panel _designerAvatar;
        private TableLayoutPanel _designerFields;
        private Label _designerPersonalHeader;
        private Label _designerWorkHeader;

        private void InitializeComponent()
        {
            _designerAvatar = new Panel();
            _designerFields = new TableLayoutPanel();
            _designerPersonalHeader = new Label();
            _designerWorkHeader = new Label();
            SuspendLayout();
            ContentHost.SuspendLayout();
            _designerFields.SuspendLayout();

            _designerAvatar.BackColor = SystemColors.ControlLight;
            _designerAvatar.Location = new Point(12, 24);
            _designerAvatar.Name = "avatarPreview";
            _designerAvatar.Size = new Size(144, 176);

            _designerFields.AutoSize = true;
            _designerFields.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            _designerFields.ColumnCount = 3;
            _designerFields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.333F));
            _designerFields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.334F));
            _designerFields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.333F));
            _designerFields.Dock = DockStyle.Top;
            _designerFields.Location = new Point(176, 0);
            _designerFields.Name = "employeeFields";
            _designerFields.RowCount = 5;
            _designerFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            _designerFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 78F));
            _designerFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 78F));
            _designerFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            _designerFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 78F));
            _designerPersonalHeader.Text = "THÔNG TIN CÁ NHÂN";
            _designerPersonalHeader.Dock = DockStyle.Fill;
            _designerFields.Controls.Add(_designerPersonalHeader, 0, 0);
            _designerFields.SetColumnSpan(_designerPersonalHeader, 3);

            var fullName = new LabeledIconField { LabelText = "Họ và tên", Dock = DockStyle.Fill };
            _designerFields.Controls.Add(fullName, 0, 1);
            _designerFields.SetColumnSpan(fullName, 2);
            var email = new LabeledIconField { LabelText = "Email", Dock = DockStyle.Fill };
            _designerFields.Controls.Add(email, 2, 1);
            var phone = new LabeledIconField { LabelText = "Số điện thoại", Dock = DockStyle.Fill };
            _designerFields.Controls.Add(phone, 0, 2);
            var birthDate = new LabeledDateField { LabelText = "Ngày sinh", Dock = DockStyle.Fill };
            _designerFields.Controls.Add(birthDate, 1, 2);
            var gender = new LabeledComboField { LabelText = "Giới tính", Dock = DockStyle.Fill };
            _designerFields.Controls.Add(gender, 2, 2);
            _designerWorkHeader.Text = "THÔNG TIN CÔNG VIỆC";
            _designerWorkHeader.Dock = DockStyle.Fill;
            _designerFields.Controls.Add(_designerWorkHeader, 0, 3);
            _designerFields.SetColumnSpan(_designerWorkHeader, 3);
            var role = new LabeledComboField { LabelText = "Vai trò", Dock = DockStyle.Fill };
            _designerFields.Controls.Add(role, 0, 4);
            var hireDate = new LabeledDateField { LabelText = "Ngày vào làm", Dock = DockStyle.Fill };
            _designerFields.Controls.Add(hireDate, 1, 4);
            var salary = new LabeledIconField { LabelText = "Lương", Dock = DockStyle.Fill };
            _designerFields.Controls.Add(salary, 2, 4);

            ContentHost.Controls.Add(_designerFields);
            ContentHost.Controls.Add(_designerAvatar);
            _designerFields.ResumeLayout(false);
            _designerFields.PerformLayout();
            ContentHost.ResumeLayout(false);
            ClientSize = new Size(1240, 720);
            Name = "frmAddEmployee";
            Text = "Thêm nhân viên";
            ResumeLayout(false);
        }
    }
}
