using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Views.UserManager
{
    partial class frmAddEmployee
    {
        private Panel _designerAvatar;
        private Panel _designerAvatarCircle;
        private PictureBox _designerAvatarImage;
        private Label _designerAvatarLabel;
        private Button _designerChooseAvatarButton;
        private Label _designerAvatarHint;
        private Panel _designerBanner;
        private Label _designerBannerTitle;
        private Label _designerBannerDescription;
        private Panel _designerPreview;
        private Panel _designerFields;
        private Label _designerPersonalHeader;
        private Label _designerWorkHeader;
        private Label _designerFullNameLabel;
        private Label _designerEmailLabel;
        private Label _designerPhoneLabel;
        private Label _designerBirthDateLabel;
        private Label _designerGenderLabel;
        private Label _designerRoleLabel;
        private Label _designerHireDateLabel;
        private Label _designerSalaryLabel;
        private TextBox _designerFullName;
        private TextBox _designerEmail;
        private TextBox _designerPhone;
        private DateTimePicker _designerBirthDate;
        private ComboBox _designerGender;
        private ComboBox _designerRole;
        private DateTimePicker _designerHireDate;
        private TextBox _designerSalary;
        private Button _designerCancelButton;
        private Button _designerSaveButton;

        private void InitializeComponent()
        {
            this._designerAvatar = new System.Windows.Forms.Panel();
            this._designerAvatarCircle = new System.Windows.Forms.Panel();
            this._designerAvatarImage = new System.Windows.Forms.PictureBox();
            this._designerAvatarLabel = new System.Windows.Forms.Label();
            this._designerChooseAvatarButton = new System.Windows.Forms.Button();
            this._designerAvatarHint = new System.Windows.Forms.Label();
            this._designerBanner = new System.Windows.Forms.Panel();
            this._designerBannerTitle = new System.Windows.Forms.Label();
            this._designerBannerDescription = new System.Windows.Forms.Label();
            this._designerPreview = new System.Windows.Forms.Panel();
            this._designerFields = new System.Windows.Forms.Panel();
            this._designerPersonalHeader = new System.Windows.Forms.Label();
            this._designerFullNameLabel = new System.Windows.Forms.Label();
            this._designerEmailLabel = new System.Windows.Forms.Label();
            this._designerFullName = new System.Windows.Forms.TextBox();
            this._designerEmail = new System.Windows.Forms.TextBox();
            this._designerPhoneLabel = new System.Windows.Forms.Label();
            this._designerBirthDateLabel = new System.Windows.Forms.Label();
            this._designerGenderLabel = new System.Windows.Forms.Label();
            this._designerPhone = new System.Windows.Forms.TextBox();
            this._designerBirthDate = new System.Windows.Forms.DateTimePicker();
            this._designerGender = new System.Windows.Forms.ComboBox();
            this._designerWorkHeader = new System.Windows.Forms.Label();
            this._designerRoleLabel = new System.Windows.Forms.Label();
            this._designerHireDateLabel = new System.Windows.Forms.Label();
            this._designerSalaryLabel = new System.Windows.Forms.Label();
            this._designerRole = new System.Windows.Forms.ComboBox();
            this._designerHireDate = new System.Windows.Forms.DateTimePicker();
            this._designerSalary = new System.Windows.Forms.TextBox();
            this._designerCancelButton = new System.Windows.Forms.Button();
            this._designerSaveButton = new System.Windows.Forms.Button();
            this._bodyScrollPanel.SuspendLayout();
            this._footerPanel.SuspendLayout();
            this._designerAvatar.SuspendLayout();
            this._designerAvatarCircle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._designerAvatarImage)).BeginInit();
            this._designerBanner.SuspendLayout();
            this._designerPreview.SuspendLayout();
            this._designerFields.SuspendLayout();
            this.SuspendLayout();
            // 
            // _bodyScrollPanel
            // 
            this._bodyScrollPanel.Controls.Add(this._designerPreview);
            this._bodyScrollPanel.Size = new System.Drawing.Size(1236, 576);
            // 
            // _footerPanel
            // 
            this._footerPanel.Controls.Add(this._designerCancelButton);
            this._footerPanel.Controls.Add(this._designerSaveButton);
            this._footerPanel.Size = new System.Drawing.Size(1236, 76);
            // 
            // _designerAvatar
            // 
            this._designerAvatar.BackColor = System.Drawing.Color.Transparent;
            this._designerAvatar.Controls.Add(this._designerAvatarCircle);
            this._designerAvatar.Controls.Add(this._designerChooseAvatarButton);
            this._designerAvatar.Controls.Add(this._designerAvatarHint);
            this._designerAvatar.Location = new System.Drawing.Point(0, 96);
            this._designerAvatar.Name = "_designerAvatar";
            this._designerAvatar.Size = new System.Drawing.Size(160, 190);
            this._designerAvatar.TabIndex = 1;
            // 
            // _designerAvatarCircle
            // 
            this._designerAvatarCircle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._designerAvatarCircle.Controls.Add(this._designerAvatarImage);
            this._designerAvatarCircle.Controls.Add(this._designerAvatarLabel);
            this._designerAvatarCircle.Location = new System.Drawing.Point(32, 0);
            this._designerAvatarCircle.Name = "_designerAvatarCircle";
            this._designerAvatarCircle.Size = new System.Drawing.Size(96, 96);
            this._designerAvatarCircle.TabIndex = 0;
            // 
            // _designerAvatarImage
            // 
            this._designerAvatarImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this._designerAvatarImage.Location = new System.Drawing.Point(0, 0);
            this._designerAvatarImage.Name = "_designerAvatarImage";
            this._designerAvatarImage.Size = new System.Drawing.Size(96, 96);
            this._designerAvatarImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this._designerAvatarImage.TabIndex = 0;
            this._designerAvatarImage.TabStop = false;
            this._designerAvatarImage.Visible = false;
            // 
            // _designerAvatarLabel
            // 
            this._designerAvatarLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._designerAvatarLabel.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this._designerAvatarLabel.ForeColor = System.Drawing.Color.White;
            this._designerAvatarLabel.Location = new System.Drawing.Point(0, 0);
            this._designerAvatarLabel.Name = "_designerAvatarLabel";
            this._designerAvatarLabel.Size = new System.Drawing.Size(96, 96);
            this._designerAvatarLabel.TabIndex = 1;
            this._designerAvatarLabel.Text = "N";
            this._designerAvatarLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _designerChooseAvatarButton
            // 
            this._designerChooseAvatarButton.Location = new System.Drawing.Point(12, 108);
            this._designerChooseAvatarButton.Name = "_designerChooseAvatarButton";
            this._designerChooseAvatarButton.Size = new System.Drawing.Size(136, 36);
            this._designerChooseAvatarButton.TabIndex = 1;
            this._designerChooseAvatarButton.Text = "Chọn ảnh";
            this._designerChooseAvatarButton.UseVisualStyleBackColor = true;
            // 
            // _designerAvatarHint
            // 
            this._designerAvatarHint.ForeColor = System.Drawing.SystemColors.GrayText;
            this._designerAvatarHint.Location = new System.Drawing.Point(0, 148);
            this._designerAvatarHint.Name = "_designerAvatarHint";
            this._designerAvatarHint.Size = new System.Drawing.Size(160, 36);
            this._designerAvatarHint.TabIndex = 2;
            this._designerAvatarHint.Text = "Tuỳ chọn · tối đa 5MB";
            this._designerAvatarHint.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // _designerBanner
            // 
            this._designerBanner.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._designerBanner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this._designerBanner.Controls.Add(this._designerBannerTitle);
            this._designerBanner.Controls.Add(this._designerBannerDescription);
            this._designerBanner.Location = new System.Drawing.Point(0, 0);
            this._designerBanner.Name = "_designerBanner";
            this._designerBanner.Size = new System.Drawing.Size(1180, 90);
            this._designerBanner.TabIndex = 0;
            // 
            // _designerBannerTitle
            // 
            this._designerBannerTitle.AutoSize = true;
            this._designerBannerTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this._designerBannerTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this._designerBannerTitle.Location = new System.Drawing.Point(16, 10);
            this._designerBannerTitle.Name = "_designerBannerTitle";
            this._designerBannerTitle.Size = new System.Drawing.Size(247, 32);
            this._designerBannerTitle.TabIndex = 0;
            this._designerBannerTitle.Text = "Thêm nhân viên mới";
            // 
            // _designerBannerDescription
            // 
            this._designerBannerDescription.AutoSize = true;
            this._designerBannerDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._designerBannerDescription.Location = new System.Drawing.Point(16, 39);
            this._designerBannerDescription.Name = "_designerBannerDescription";
            this._designerBannerDescription.Size = new System.Drawing.Size(821, 36);
            this._designerBannerDescription.TabIndex = 1;
            this._designerBannerDescription.Text = "Tài khoản đăng nhập sẽ được tạo tự động và gửi qua email sau khi lưu.";
            // 
            // _designerPreview
            // 
            this._designerPreview.Controls.Add(this._designerBanner);
            this._designerPreview.Controls.Add(this._designerAvatar);
            this._designerPreview.Controls.Add(this._designerFields);
            this._designerPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this._designerPreview.Location = new System.Drawing.Point(28, 20);
            this._designerPreview.Name = "_designerPreview";
            this._designerPreview.Size = new System.Drawing.Size(1180, 536);
            this._designerPreview.TabIndex = 0;
            // 
            // _designerFields
            // 
            this._designerFields.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._designerFields.Controls.Add(this._designerPersonalHeader);
            this._designerFields.Controls.Add(this._designerFullNameLabel);
            this._designerFields.Controls.Add(this._designerEmailLabel);
            this._designerFields.Controls.Add(this._designerFullName);
            this._designerFields.Controls.Add(this._designerEmail);
            this._designerFields.Controls.Add(this._designerPhoneLabel);
            this._designerFields.Controls.Add(this._designerBirthDateLabel);
            this._designerFields.Controls.Add(this._designerGenderLabel);
            this._designerFields.Controls.Add(this._designerPhone);
            this._designerFields.Controls.Add(this._designerBirthDate);
            this._designerFields.Controls.Add(this._designerGender);
            this._designerFields.Controls.Add(this._designerWorkHeader);
            this._designerFields.Controls.Add(this._designerRoleLabel);
            this._designerFields.Controls.Add(this._designerHireDateLabel);
            this._designerFields.Controls.Add(this._designerSalaryLabel);
            this._designerFields.Controls.Add(this._designerRole);
            this._designerFields.Controls.Add(this._designerHireDate);
            this._designerFields.Controls.Add(this._designerSalary);
            this._designerFields.Location = new System.Drawing.Point(196, 88);
            this._designerFields.Name = "_designerFields";
            this._designerFields.Size = new System.Drawing.Size(956, 358);
            this._designerFields.TabIndex = 2;
            // 
            // _designerPersonalHeader
            // 
            this._designerPersonalHeader.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._designerPersonalHeader.Location = new System.Drawing.Point(0, 0);
            this._designerPersonalHeader.Name = "_designerPersonalHeader";
            this._designerPersonalHeader.Size = new System.Drawing.Size(956, 28);
            this._designerPersonalHeader.TabIndex = 0;
            this._designerPersonalHeader.Text = "THÔNG TIN CÁ NHÂN";
            // 
            // _designerFullNameLabel
            // 
            this._designerFullNameLabel.Location = new System.Drawing.Point(0, 25);
            this._designerFullNameLabel.Name = "_designerFullNameLabel";
            this._designerFullNameLabel.Size = new System.Drawing.Size(308, 34);
            this._designerFullNameLabel.TabIndex = 1;
            this._designerFullNameLabel.Text = "Họ và tên";
            // 
            // _designerEmailLabel
            // 
            this._designerEmailLabel.Location = new System.Drawing.Point(324, 28);
            this._designerEmailLabel.Name = "_designerEmailLabel";
            this._designerEmailLabel.Size = new System.Drawing.Size(632, 39);
            this._designerEmailLabel.TabIndex = 2;
            this._designerEmailLabel.Text = "Email";
            // 
            // _designerFullName
            // 
            this._designerFullName.Location = new System.Drawing.Point(0, 62);
            this._designerFullName.Name = "_designerFullName";
            this._designerFullName.Size = new System.Drawing.Size(308, 42);
            this._designerFullName.TabIndex = 3;
            // 
            // _designerEmail
            // 
            this._designerEmail.Location = new System.Drawing.Point(321, 62);
            this._designerEmail.Name = "_designerEmail";
            this._designerEmail.Size = new System.Drawing.Size(632, 42);
            this._designerEmail.TabIndex = 4;
            // 
            // _designerPhoneLabel
            // 
            this._designerPhoneLabel.Location = new System.Drawing.Point(-1, 103);
            this._designerPhoneLabel.Name = "_designerPhoneLabel";
            this._designerPhoneLabel.Size = new System.Drawing.Size(308, 39);
            this._designerPhoneLabel.TabIndex = 5;
            this._designerPhoneLabel.Text = "Số điện thoại";
            // 
            // _designerBirthDateLabel
            // 
            this._designerBirthDateLabel.Location = new System.Drawing.Point(324, 101);
            this._designerBirthDateLabel.Name = "_designerBirthDateLabel";
            this._designerBirthDateLabel.Size = new System.Drawing.Size(308, 37);
            this._designerBirthDateLabel.TabIndex = 6;
            this._designerBirthDateLabel.Text = "Ngày sinh";
            // 
            // _designerGenderLabel
            // 
            this._designerGenderLabel.Location = new System.Drawing.Point(639, 101);
            this._designerGenderLabel.Name = "_designerGenderLabel";
            this._designerGenderLabel.Size = new System.Drawing.Size(308, 37);
            this._designerGenderLabel.TabIndex = 7;
            this._designerGenderLabel.Text = "Giới tính";
            // 
            // _designerPhone
            // 
            this._designerPhone.Location = new System.Drawing.Point(0, 145);
            this._designerPhone.Name = "_designerPhone";
            this._designerPhone.Size = new System.Drawing.Size(308, 42);
            this._designerPhone.TabIndex = 8;
            this._designerPhone.TextChanged += new System.EventHandler(this._designerPhone_TextChanged);
            // 
            // _designerBirthDate
            // 
            this._designerBirthDate.Location = new System.Drawing.Point(324, 143);
            this._designerBirthDate.Name = "_designerBirthDate";
            this._designerBirthDate.Size = new System.Drawing.Size(308, 42);
            this._designerBirthDate.TabIndex = 9;
            // 
            // _designerGender
            // 
            this._designerGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._designerGender.Location = new System.Drawing.Point(645, 141);
            this._designerGender.Name = "_designerGender";
            this._designerGender.Size = new System.Drawing.Size(308, 44);
            this._designerGender.TabIndex = 10;
            // 
            // _designerWorkHeader
            // 
            this._designerWorkHeader.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._designerWorkHeader.Location = new System.Drawing.Point(-5, 190);
            this._designerWorkHeader.Name = "_designerWorkHeader";
            this._designerWorkHeader.Size = new System.Drawing.Size(956, 28);
            this._designerWorkHeader.TabIndex = 11;
            this._designerWorkHeader.Text = "THÔNG TIN CÔNG VIỆC";
            // 
            // _designerRoleLabel
            // 
            this._designerRoleLabel.Location = new System.Drawing.Point(-1, 216);
            this._designerRoleLabel.Name = "_designerRoleLabel";
            this._designerRoleLabel.Size = new System.Drawing.Size(308, 38);
            this._designerRoleLabel.TabIndex = 12;
            this._designerRoleLabel.Text = "Vai trò";
            // 
            // _designerHireDateLabel
            // 
            this._designerHireDateLabel.Location = new System.Drawing.Point(324, 216);
            this._designerHireDateLabel.Name = "_designerHireDateLabel";
            this._designerHireDateLabel.Size = new System.Drawing.Size(308, 38);
            this._designerHireDateLabel.TabIndex = 13;
            this._designerHireDateLabel.Text = "Ngày vào làm";
            // 
            // _designerSalaryLabel
            // 
            this._designerSalaryLabel.Location = new System.Drawing.Point(639, 218);
            this._designerSalaryLabel.Name = "_designerSalaryLabel";
            this._designerSalaryLabel.Size = new System.Drawing.Size(308, 38);
            this._designerSalaryLabel.TabIndex = 14;
            this._designerSalaryLabel.Text = "Lương (VNĐ)";
            // 
            // _designerRole
            // 
            this._designerRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._designerRole.Location = new System.Drawing.Point(0, 257);
            this._designerRole.Name = "_designerRole";
            this._designerRole.Size = new System.Drawing.Size(308, 44);
            this._designerRole.TabIndex = 15;
            // 
            // _designerHireDate
            // 
            this._designerHireDate.Location = new System.Drawing.Point(324, 259);
            this._designerHireDate.Name = "_designerHireDate";
            this._designerHireDate.Size = new System.Drawing.Size(308, 42);
            this._designerHireDate.TabIndex = 16;
            // 
            // _designerSalary
            // 
            this._designerSalary.Location = new System.Drawing.Point(645, 259);
            this._designerSalary.Name = "_designerSalary";
            this._designerSalary.Size = new System.Drawing.Size(308, 42);
            this._designerSalary.TabIndex = 17;
            // 
            // _designerCancelButton
            // 
            this._designerCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._designerCancelButton.Location = new System.Drawing.Point(918, 16);
            this._designerCancelButton.Name = "_designerCancelButton";
            this._designerCancelButton.Size = new System.Drawing.Size(112, 40);
            this._designerCancelButton.TabIndex = 0;
            this._designerCancelButton.Text = "Hủy";
            this._designerCancelButton.UseVisualStyleBackColor = true;
            // 
            // _designerSaveButton
            // 
            this._designerSaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._designerSaveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._designerSaveButton.ForeColor = System.Drawing.Color.White;
            this._designerSaveButton.Location = new System.Drawing.Point(1042, 16);
            this._designerSaveButton.Name = "_designerSaveButton";
            this._designerSaveButton.Size = new System.Drawing.Size(112, 40);
            this._designerSaveButton.TabIndex = 1;
            this._designerSaveButton.Text = "Thêm mới";
            this._designerSaveButton.UseVisualStyleBackColor = false;
            // 
            // frmAddEmployee
            // 
            this.ClientSize = new System.Drawing.Size(1240, 720);
            this.Name = "frmAddEmployee";
            this.Text = "Thêm nhân viên";
            this._bodyScrollPanel.ResumeLayout(false);
            this._footerPanel.ResumeLayout(false);
            this._designerAvatar.ResumeLayout(false);
            this._designerAvatarCircle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._designerAvatarImage)).EndInit();
            this._designerBanner.ResumeLayout(false);
            this._designerBanner.PerformLayout();
            this._designerPreview.ResumeLayout(false);
            this._designerFields.ResumeLayout(false);
            this._designerFields.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
