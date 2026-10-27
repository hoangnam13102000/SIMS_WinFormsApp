using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Views.UserManager
{
    partial class frmEditUserAccount
    {
        private Panel _designerPreview;
        private Panel _designerAvatarCircle;
        private PictureBox _designerAvatarImage;
        private Label _designerAvatarInitial;
        private Button _designerChooseAvatarButton;
        private Label _designerAvatarHint;
        private Label _designerPersonalHeader;
        private Label _designerWorkHeader;
        private Label _designerAccountHeader;
        private Label _designerFullNameLabel;
        private Label _designerEmailLabel;
        private Label _designerPhoneLabel;
        private Label _designerBirthDateLabel;
        private Label _designerGenderLabel;
        private Label _designerHireDateLabel;
        private Label _designerSalaryLabel;
        private Label _designerFullNameHint;
        private Label _designerEmailHint;
        private Label _designerPhoneHint;
        private TextBox _designerFullName;
        private TextBox _designerEmail;
        private TextBox _designerPhone;
        private DateTimePicker _designerBirthDate;
        private ComboBox _designerGender;
        private DateTimePicker _designerHireDate;
        private TextBox _designerSalary;
        private Panel _designerPersonalSeparator;
        private Panel _designerWorkSeparator;
        private Panel _designerAccountInfo;
        private Label _designerAccountIdCaption;
        private Label _designerAccountIdValue;
        private Label _designerUsernameCaption;
        private Label _designerUsernameValue;
        private Label _designerRoleCaption;
        private Label _designerRoleValue;
        private Button _designerCancelButton;
        private Button _designerSaveButton;

        private void InitializeComponent()
        {
            this._designerPreview = new System.Windows.Forms.Panel();
            this._designerAvatarCircle = new System.Windows.Forms.Panel();
            this._designerAvatarImage = new System.Windows.Forms.PictureBox();
            this._designerAvatarInitial = new System.Windows.Forms.Label();
            this._designerChooseAvatarButton = new System.Windows.Forms.Button();
            this._designerAvatarHint = new System.Windows.Forms.Label();
            this._designerPersonalHeader = new System.Windows.Forms.Label();
            this._designerFullNameLabel = new System.Windows.Forms.Label();
            this._designerFullName = new System.Windows.Forms.TextBox();
            this._designerFullNameHint = new System.Windows.Forms.Label();
            this._designerEmailLabel = new System.Windows.Forms.Label();
            this._designerEmail = new System.Windows.Forms.TextBox();
            this._designerEmailHint = new System.Windows.Forms.Label();
            this._designerPhoneLabel = new System.Windows.Forms.Label();
            this._designerPhone = new System.Windows.Forms.TextBox();
            this._designerPhoneHint = new System.Windows.Forms.Label();
            this._designerBirthDateLabel = new System.Windows.Forms.Label();
            this._designerBirthDate = new System.Windows.Forms.DateTimePicker();
            this._designerGenderLabel = new System.Windows.Forms.Label();
            this._designerGender = new System.Windows.Forms.ComboBox();
            this._designerPersonalSeparator = new System.Windows.Forms.Panel();
            this._designerWorkHeader = new System.Windows.Forms.Label();
            this._designerHireDateLabel = new System.Windows.Forms.Label();
            this._designerHireDate = new System.Windows.Forms.DateTimePicker();
            this._designerSalaryLabel = new System.Windows.Forms.Label();
            this._designerSalary = new System.Windows.Forms.TextBox();
            this._designerWorkSeparator = new System.Windows.Forms.Panel();
            this._designerAccountHeader = new System.Windows.Forms.Label();
            this._designerAccountInfo = new System.Windows.Forms.Panel();
            this._designerAccountIdCaption = new System.Windows.Forms.Label();
            this._designerAccountIdValue = new System.Windows.Forms.Label();
            this._designerUsernameCaption = new System.Windows.Forms.Label();
            this._designerUsernameValue = new System.Windows.Forms.Label();
            this._designerRoleCaption = new System.Windows.Forms.Label();
            this._designerRoleValue = new System.Windows.Forms.Label();
            this._designerCancelButton = new System.Windows.Forms.Button();
            this._designerSaveButton = new System.Windows.Forms.Button();
            this._bodyScrollPanel.SuspendLayout();
            this._footerPanel.SuspendLayout();
            this._designerPreview.SuspendLayout();
            this._designerAvatarCircle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._designerAvatarImage)).BeginInit();
            this._designerAccountInfo.SuspendLayout();
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
            // _designerPreview
            // 
            this._designerPreview.Controls.Add(this._designerAvatarCircle);
            this._designerPreview.Controls.Add(this._designerChooseAvatarButton);
            this._designerPreview.Controls.Add(this._designerAvatarHint);
            this._designerPreview.Controls.Add(this._designerPersonalHeader);
            this._designerPreview.Controls.Add(this._designerFullNameLabel);
            this._designerPreview.Controls.Add(this._designerFullName);
            this._designerPreview.Controls.Add(this._designerFullNameHint);
            this._designerPreview.Controls.Add(this._designerEmailLabel);
            this._designerPreview.Controls.Add(this._designerEmail);
            this._designerPreview.Controls.Add(this._designerEmailHint);
            this._designerPreview.Controls.Add(this._designerPhoneLabel);
            this._designerPreview.Controls.Add(this._designerPhone);
            this._designerPreview.Controls.Add(this._designerPhoneHint);
            this._designerPreview.Controls.Add(this._designerBirthDateLabel);
            this._designerPreview.Controls.Add(this._designerBirthDate);
            this._designerPreview.Controls.Add(this._designerGenderLabel);
            this._designerPreview.Controls.Add(this._designerGender);
            this._designerPreview.Controls.Add(this._designerPersonalSeparator);
            this._designerPreview.Controls.Add(this._designerWorkHeader);
            this._designerPreview.Controls.Add(this._designerHireDateLabel);
            this._designerPreview.Controls.Add(this._designerHireDate);
            this._designerPreview.Controls.Add(this._designerSalaryLabel);
            this._designerPreview.Controls.Add(this._designerSalary);
            this._designerPreview.Controls.Add(this._designerWorkSeparator);
            this._designerPreview.Controls.Add(this._designerAccountHeader);
            this._designerPreview.Controls.Add(this._designerAccountInfo);
            this._designerPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this._designerPreview.Location = new System.Drawing.Point(28, 20);
            this._designerPreview.Name = "_designerPreview";
            this._designerPreview.Size = new System.Drawing.Size(1180, 536);
            this._designerPreview.TabIndex = 0;
            // 
            // _designerAvatarCircle
            // 
            this._designerAvatarCircle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._designerAvatarCircle.Controls.Add(this._designerAvatarImage);
            this._designerAvatarCircle.Controls.Add(this._designerAvatarInitial);
            this._designerAvatarCircle.Location = new System.Drawing.Point(20, 0);
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
            // 
            // _designerAvatarInitial
            // 
            this._designerAvatarInitial.Dock = System.Windows.Forms.DockStyle.Fill;
            this._designerAvatarInitial.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            this._designerAvatarInitial.ForeColor = System.Drawing.Color.White;
            this._designerAvatarInitial.Location = new System.Drawing.Point(0, 0);
            this._designerAvatarInitial.Name = "_designerAvatarInitial";
            this._designerAvatarInitial.Size = new System.Drawing.Size(96, 96);
            this._designerAvatarInitial.TabIndex = 1;
            this._designerAvatarInitial.Text = "N";
            this._designerAvatarInitial.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _designerChooseAvatarButton
            // 
            this._designerChooseAvatarButton.Location = new System.Drawing.Point(0, 108);
            this._designerChooseAvatarButton.Name = "_designerChooseAvatarButton";
            this._designerChooseAvatarButton.Size = new System.Drawing.Size(136, 41);
            this._designerChooseAvatarButton.TabIndex = 1;
            this._designerChooseAvatarButton.Text = "Chọn ảnh";
            // 
            // _designerAvatarHint
            // 
            this._designerAvatarHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._designerAvatarHint.Location = new System.Drawing.Point(0, 152);
            this._designerAvatarHint.Name = "_designerAvatarHint";
            this._designerAvatarHint.Size = new System.Drawing.Size(160, 41);
            this._designerAvatarHint.TabIndex = 2;
            this._designerAvatarHint.Text = "Tuỳ chọn · tối đa 5MB";
            this._designerAvatarHint.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // _designerPersonalHeader
            // 
            this._designerPersonalHeader.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._designerPersonalHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this._designerPersonalHeader.Location = new System.Drawing.Point(200, 0);
            this._designerPersonalHeader.Name = "_designerPersonalHeader";
            this._designerPersonalHeader.Size = new System.Drawing.Size(900, 28);
            this._designerPersonalHeader.TabIndex = 3;
            this._designerPersonalHeader.Text = "THÔNG TIN CÁ NHÂN";
            // 
            // _designerFullNameLabel
            // 
            this._designerFullNameLabel.Location = new System.Drawing.Point(200, 28);
            this._designerFullNameLabel.Name = "_designerFullNameLabel";
            this._designerFullNameLabel.Size = new System.Drawing.Size(290, 32);
            this._designerFullNameLabel.TabIndex = 4;
            this._designerFullNameLabel.Text = "Họ và tên  *";
            // 
            // _designerFullName
            // 
            this._designerFullName.Location = new System.Drawing.Point(200, 64);
            this._designerFullName.MaxLength = 100;
            this._designerFullName.Name = "_designerFullName";
            this._designerFullName.Size = new System.Drawing.Size(290, 42);
            this._designerFullName.TabIndex = 5;
            this._designerFullName.Text = "Nguyễn Văn An";
            // 
            // _designerFullNameHint
            // 
            this._designerFullNameHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._designerFullNameHint.Location = new System.Drawing.Point(199, 106);
            this._designerFullNameHint.Name = "_designerFullNameHint";
            this._designerFullNameHint.Size = new System.Drawing.Size(290, 36);
            this._designerFullNameHint.TabIndex = 6;
            this._designerFullNameHint.Text = "Họ tên hiển thị trên hệ thống.";
            // 
            // _designerEmailLabel
            // 
            this._designerEmailLabel.Location = new System.Drawing.Point(516, 28);
            this._designerEmailLabel.Name = "_designerEmailLabel";
            this._designerEmailLabel.Size = new System.Drawing.Size(584, 32);
            this._designerEmailLabel.TabIndex = 7;
            this._designerEmailLabel.Text = "Email  *";
            // 
            // _designerEmail
            // 
            this._designerEmail.Location = new System.Drawing.Point(516, 64);
            this._designerEmail.MaxLength = 150;
            this._designerEmail.Name = "_designerEmail";
            this._designerEmail.Size = new System.Drawing.Size(584, 42);
            this._designerEmail.TabIndex = 8;
            this._designerEmail.Text = "an@example.com";
            // 
            // _designerEmailHint
            // 
            this._designerEmailHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._designerEmailHint.Location = new System.Drawing.Point(516, 108);
            this._designerEmailHint.Name = "_designerEmailHint";
            this._designerEmailHint.Size = new System.Drawing.Size(584, 48);
            this._designerEmailHint.TabIndex = 9;
            this._designerEmailHint.Text = "Dùng để nhận thông báo / khôi phục tài khoản.";
            // 
            // _designerPhoneLabel
            // 
            this._designerPhoneLabel.Location = new System.Drawing.Point(199, 146);
            this._designerPhoneLabel.Name = "_designerPhoneLabel";
            this._designerPhoneLabel.Size = new System.Drawing.Size(290, 41);
            this._designerPhoneLabel.TabIndex = 10;
            this._designerPhoneLabel.Text = "Số điện thoại";
            // 
            // _designerPhone
            // 
            this._designerPhone.Location = new System.Drawing.Point(199, 195);
            this._designerPhone.MaxLength = 15;
            this._designerPhone.Name = "_designerPhone";
            this._designerPhone.Size = new System.Drawing.Size(290, 42);
            this._designerPhone.TabIndex = 11;
            this._designerPhone.Text = "0901234567";
            // 
            // _designerPhoneHint
            // 
            this._designerPhoneHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._designerPhoneHint.Location = new System.Drawing.Point(193, 240);
            this._designerPhoneHint.Name = "_designerPhoneHint";
            this._designerPhoneHint.Size = new System.Drawing.Size(298, 31);
            this._designerPhoneHint.TabIndex = 12;
            this._designerPhoneHint.Text = "VD: 09xxxxxxxx (tùy chọn).";
            // 
            // _designerBirthDateLabel
            // 
            this._designerBirthDateLabel.Location = new System.Drawing.Point(510, 152);
            this._designerBirthDateLabel.Name = "_designerBirthDateLabel";
            this._designerBirthDateLabel.Size = new System.Drawing.Size(270, 41);
            this._designerBirthDateLabel.TabIndex = 13;
            this._designerBirthDateLabel.Text = "Ngày sinh";
            this._designerBirthDateLabel.Click += new System.EventHandler(this._designerBirthDateLabel_Click);
            // 
            // _designerBirthDate
            // 
            this._designerBirthDate.CustomFormat = "dd/MM/yyyy";
            this._designerBirthDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._designerBirthDate.Location = new System.Drawing.Point(507, 195);
            this._designerBirthDate.Name = "_designerBirthDate";
            this._designerBirthDate.ShowCheckBox = true;
            this._designerBirthDate.Size = new System.Drawing.Size(270, 42);
            this._designerBirthDate.TabIndex = 14;
            // 
            // _designerGenderLabel
            // 
            this._designerGenderLabel.Location = new System.Drawing.Point(812, 156);
            this._designerGenderLabel.Name = "_designerGenderLabel";
            this._designerGenderLabel.Size = new System.Drawing.Size(288, 31);
            this._designerGenderLabel.TabIndex = 15;
            this._designerGenderLabel.Text = "Giới tính";
            this._designerGenderLabel.Click += new System.EventHandler(this._designerGenderLabel_Click);
            // 
            // _designerGender
            // 
            this._designerGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._designerGender.Location = new System.Drawing.Point(812, 197);
            this._designerGender.Name = "_designerGender";
            this._designerGender.Size = new System.Drawing.Size(288, 44);
            this._designerGender.TabIndex = 16;
            // 
            // _designerPersonalSeparator
            // 
            this._designerPersonalSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this._designerPersonalSeparator.Location = new System.Drawing.Point(200, 226);
            this._designerPersonalSeparator.Name = "_designerPersonalSeparator";
            this._designerPersonalSeparator.Size = new System.Drawing.Size(900, 1);
            this._designerPersonalSeparator.TabIndex = 17;
            // 
            // _designerWorkHeader
            // 
            this._designerWorkHeader.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._designerWorkHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this._designerWorkHeader.Location = new System.Drawing.Point(200, 271);
            this._designerWorkHeader.Name = "_designerWorkHeader";
            this._designerWorkHeader.Size = new System.Drawing.Size(900, 28);
            this._designerWorkHeader.TabIndex = 18;
            this._designerWorkHeader.Text = "THÔNG TIN CÔNG VIỆC";
            // 
            // _designerHireDateLabel
            // 
            this._designerHireDateLabel.Location = new System.Drawing.Point(200, 299);
            this._designerHireDateLabel.Name = "_designerHireDateLabel";
            this._designerHireDateLabel.Size = new System.Drawing.Size(290, 45);
            this._designerHireDateLabel.TabIndex = 19;
            this._designerHireDateLabel.Text = "Ngày vào làm  *";
            // 
            // _designerHireDate
            // 
            this._designerHireDate.CustomFormat = "dd/MM/yyyy";
            this._designerHireDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._designerHireDate.Location = new System.Drawing.Point(199, 349);
            this._designerHireDate.Name = "_designerHireDate";
            this._designerHireDate.Size = new System.Drawing.Size(290, 42);
            this._designerHireDate.TabIndex = 20;
            this._designerHireDate.ValueChanged += new System.EventHandler(this._designerHireDate_ValueChanged);
            // 
            // _designerSalaryLabel
            // 
            this._designerSalaryLabel.Location = new System.Drawing.Point(516, 299);
            this._designerSalaryLabel.Name = "_designerSalaryLabel";
            this._designerSalaryLabel.Size = new System.Drawing.Size(290, 47);
            this._designerSalaryLabel.TabIndex = 21;
            this._designerSalaryLabel.Text = "Lương (VNĐ)";
            // 
            // _designerSalary
            // 
            this._designerSalary.Location = new System.Drawing.Point(516, 349);
            this._designerSalary.MaxLength = 19;
            this._designerSalary.Name = "_designerSalary";
            this._designerSalary.Size = new System.Drawing.Size(290, 42);
            this._designerSalary.TabIndex = 22;
            this._designerSalary.Text = "12,000,000";
            // 
            // _designerWorkSeparator
            // 
            this._designerWorkSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this._designerWorkSeparator.Location = new System.Drawing.Point(200, 360);
            this._designerWorkSeparator.Name = "_designerWorkSeparator";
            this._designerWorkSeparator.Size = new System.Drawing.Size(900, 1);
            this._designerWorkSeparator.TabIndex = 23;
            // 
            // _designerAccountHeader
            // 
            this._designerAccountHeader.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._designerAccountHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this._designerAccountHeader.Location = new System.Drawing.Point(200, 394);
            this._designerAccountHeader.Name = "_designerAccountHeader";
            this._designerAccountHeader.Size = new System.Drawing.Size(900, 28);
            this._designerAccountHeader.TabIndex = 24;
            this._designerAccountHeader.Text = "THÔNG TIN TÀI KHOẢN";
            // 
            // _designerAccountInfo
            // 
            this._designerAccountInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this._designerAccountInfo.Controls.Add(this._designerAccountIdCaption);
            this._designerAccountInfo.Controls.Add(this._designerAccountIdValue);
            this._designerAccountInfo.Controls.Add(this._designerUsernameCaption);
            this._designerAccountInfo.Controls.Add(this._designerUsernameValue);
            this._designerAccountInfo.Controls.Add(this._designerRoleCaption);
            this._designerAccountInfo.Controls.Add(this._designerRoleValue);
            this._designerAccountInfo.Location = new System.Drawing.Point(200, 437);
            this._designerAccountInfo.Name = "_designerAccountInfo";
            this._designerAccountInfo.Size = new System.Drawing.Size(900, 96);
            this._designerAccountInfo.TabIndex = 25;
            // 
            // _designerAccountIdCaption
            // 
            this._designerAccountIdCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._designerAccountIdCaption.Location = new System.Drawing.Point(16, 10);
            this._designerAccountIdCaption.Name = "_designerAccountIdCaption";
            this._designerAccountIdCaption.Size = new System.Drawing.Size(260, 41);
            this._designerAccountIdCaption.TabIndex = 0;
            this._designerAccountIdCaption.Text = "Mã tài khoản";
            // 
            // _designerAccountIdValue
            // 
            this._designerAccountIdValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._designerAccountIdValue.Location = new System.Drawing.Point(16, 51);
            this._designerAccountIdValue.Name = "_designerAccountIdValue";
            this._designerAccountIdValue.Size = new System.Drawing.Size(260, 24);
            this._designerAccountIdValue.TabIndex = 1;
            this._designerAccountIdValue.Text = "1";
            this._designerAccountIdValue.Click += new System.EventHandler(this._designerAccountIdValue_Click);
            // 
            // _designerUsernameCaption
            // 
            this._designerUsernameCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._designerUsernameCaption.Location = new System.Drawing.Point(310, 10);
            this._designerUsernameCaption.Name = "_designerUsernameCaption";
            this._designerUsernameCaption.Size = new System.Drawing.Size(260, 41);
            this._designerUsernameCaption.TabIndex = 2;
            this._designerUsernameCaption.Text = "Tên đăng nhập";
            // 
            // _designerUsernameValue
            // 
            this._designerUsernameValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._designerUsernameValue.Location = new System.Drawing.Point(317, 51);
            this._designerUsernameValue.Name = "_designerUsernameValue";
            this._designerUsernameValue.Size = new System.Drawing.Size(260, 24);
            this._designerUsernameValue.TabIndex = 3;
            this._designerUsernameValue.Text = "nhanvien";
            // 
            // _designerRoleCaption
            // 
            this._designerRoleCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._designerRoleCaption.Location = new System.Drawing.Point(616, 10);
            this._designerRoleCaption.Name = "_designerRoleCaption";
            this._designerRoleCaption.Size = new System.Drawing.Size(260, 41);
            this._designerRoleCaption.TabIndex = 4;
            this._designerRoleCaption.Text = "Vai trò";
            // 
            // _designerRoleValue
            // 
            this._designerRoleValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._designerRoleValue.Location = new System.Drawing.Point(607, 51);
            this._designerRoleValue.Name = "_designerRoleValue";
            this._designerRoleValue.Size = new System.Drawing.Size(260, 24);
            this._designerRoleValue.TabIndex = 5;
            this._designerRoleValue.Text = "Nhân viên bán hàng";
            // 
            // _designerCancelButton
            // 
            this._designerCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._designerCancelButton.Location = new System.Drawing.Point(898, 16);
            this._designerCancelButton.Name = "_designerCancelButton";
            this._designerCancelButton.Size = new System.Drawing.Size(112, 40);
            this._designerCancelButton.TabIndex = 0;
            this._designerCancelButton.Text = "Hủy";
            // 
            // _designerSaveButton
            // 
            this._designerSaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._designerSaveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._designerSaveButton.ForeColor = System.Drawing.Color.White;
            this._designerSaveButton.Location = new System.Drawing.Point(1022, 16);
            this._designerSaveButton.Name = "_designerSaveButton";
            this._designerSaveButton.Size = new System.Drawing.Size(170, 40);
            this._designerSaveButton.TabIndex = 1;
            this._designerSaveButton.Text = "Lưu thay đổi";
            this._designerSaveButton.UseVisualStyleBackColor = false;
            // 
            // frmEditUserAccount
            // 
            this.ClientSize = new System.Drawing.Size(1240, 720);
            this.HeaderTitle = "Cập nhật tài khoản nhân viên";
            this.Name = "frmEditUserAccount";
            this.Text = "Cập nhật tài khoản";
            this._bodyScrollPanel.ResumeLayout(false);
            this._footerPanel.ResumeLayout(false);
            this._designerPreview.ResumeLayout(false);
            this._designerPreview.PerformLayout();
            this._designerAvatarCircle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._designerAvatarImage)).EndInit();
            this._designerAccountInfo.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
