namespace SIMS_WinFormsApp.Views.Profile
{
    partial class ucMyProfile
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel rootLayout;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.TableLayoutPanel contentLayout;
        private System.Windows.Forms.Panel summaryHost;
        private System.Windows.Forms.Panel profileHost;
        private System.Windows.Forms.Panel passwordHost;
        private System.Windows.Forms.TableLayoutPanel summaryCard;
        private System.Windows.Forms.TableLayoutPanel profileCard;
        private System.Windows.Forms.TableLayoutPanel passwordCard;
        private System.Windows.Forms.Panel avatarHost;
        private System.Windows.Forms.PictureBox pictureAvatar;
        private System.Windows.Forms.Label lblAvatarInitial;
        private System.Windows.Forms.Button btnChooseAvatar;
        private System.Windows.Forms.Label lblAvatarHint;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.Label lblRoleBadge;
        private System.Windows.Forms.Label lblSidebarEmail;
        private System.Windows.Forms.Label lblSidebarPhone;
        private System.Windows.Forms.Label lblJoinedAt;
        private System.Windows.Forms.Label lblProfileHeader;
        private System.Windows.Forms.Label lblFullNameField;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label lblPhoneField;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblEmailField;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblProfileError;
        private System.Windows.Forms.Button btnSaveProfile;
        private System.Windows.Forms.Label lblPasswordHeader;
        private System.Windows.Forms.Label lblCurrentPassword;
        private System.Windows.Forms.TextBox txtCurrentPassword;
        private System.Windows.Forms.Label lblNewPassword;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.Label lblConfirmPassword;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.Label lblPasswordError;
        private System.Windows.Forms.Button btnChangePassword;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
                if (pictureAvatar != null && pictureAvatar.Image != null)
                {
                    pictureAvatar.Image.Dispose();
                    pictureAvatar.Image = null;
                }
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.rootLayout = new System.Windows.Forms.TableLayoutPanel();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.contentLayout = new System.Windows.Forms.TableLayoutPanel();
            this.summaryHost = new System.Windows.Forms.Panel();
            this.summaryCard = new System.Windows.Forms.TableLayoutPanel();
            this.avatarHost = new System.Windows.Forms.Panel();
            this.lblAvatarInitial = new System.Windows.Forms.Label();
            this.pictureAvatar = new System.Windows.Forms.PictureBox();
            this.btnChooseAvatar = new System.Windows.Forms.Button();
            this.lblAvatarHint = new System.Windows.Forms.Label();
            this.lblFullName = new System.Windows.Forms.Label();
            this.lblRoleBadge = new System.Windows.Forms.Label();
            this.lblSidebarEmail = new System.Windows.Forms.Label();
            this.lblSidebarPhone = new System.Windows.Forms.Label();
            this.lblJoinedAt = new System.Windows.Forms.Label();
            this.profileHost = new System.Windows.Forms.Panel();
            this.profileCard = new System.Windows.Forms.TableLayoutPanel();
            this.lblProfileHeader = new System.Windows.Forms.Label();
            this.lblFullNameField = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.lblPhoneField = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblEmailField = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblProfileError = new System.Windows.Forms.Label();
            this.btnSaveProfile = new System.Windows.Forms.Button();
            this.passwordHost = new System.Windows.Forms.Panel();
            this.passwordCard = new System.Windows.Forms.TableLayoutPanel();
            this.lblPasswordHeader = new System.Windows.Forms.Label();
            this.lblCurrentPassword = new System.Windows.Forms.Label();
            this.txtCurrentPassword = new System.Windows.Forms.TextBox();
            this.lblNewPassword = new System.Windows.Forms.Label();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.lblConfirmPassword = new System.Windows.Forms.Label();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.lblPasswordError = new System.Windows.Forms.Label();
            this.btnChangePassword = new System.Windows.Forms.Button();
            this.rootLayout.SuspendLayout();
            this.contentLayout.SuspendLayout();
            this.summaryHost.SuspendLayout();
            this.summaryCard.SuspendLayout();
            this.avatarHost.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureAvatar)).BeginInit();
            this.profileHost.SuspendLayout();
            this.profileCard.SuspendLayout();
            this.passwordHost.SuspendLayout();
            this.passwordCard.SuspendLayout();
            this.SuspendLayout();
            // 
            // rootLayout
            // 
            this.rootLayout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.rootLayout.ColumnCount = 1;
            this.rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rootLayout.Controls.Add(this.lblPageTitle, 0, 0);
            this.rootLayout.Controls.Add(this.contentLayout, 0, 1);
            this.rootLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rootLayout.Location = new System.Drawing.Point(20, 16);
            this.rootLayout.Margin = new System.Windows.Forms.Padding(0);
            this.rootLayout.Name = "rootLayout";
            this.rootLayout.RowCount = 2;
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rootLayout.Size = new System.Drawing.Size(1344, 821);
            this.rootLayout.TabIndex = 0;
            // 
            // lblPageTitle
            // 
            this.lblPageTitle.AutoSize = true;
            this.lblPageTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPageTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblPageTitle.Location = new System.Drawing.Point(0, 0);
            this.lblPageTitle.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Size = new System.Drawing.Size(1344, 36);
            this.lblPageTitle.TabIndex = 0;
            this.lblPageTitle.Text = "Trang cá nhân";
            this.lblPageTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // contentLayout
            // 
            this.contentLayout.BackColor = System.Drawing.Color.Transparent;
            this.contentLayout.ColumnCount = 3;
            this.contentLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27F));
            this.contentLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.5F));
            this.contentLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.5F));
            this.contentLayout.Controls.Add(this.summaryHost, 0, 0);
            this.contentLayout.Controls.Add(this.profileHost, 1, 0);
            this.contentLayout.Controls.Add(this.passwordHost, 2, 0);
            this.contentLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentLayout.Location = new System.Drawing.Point(0, 48);
            this.contentLayout.Margin = new System.Windows.Forms.Padding(0);
            this.contentLayout.Name = "contentLayout";
            this.contentLayout.RowCount = 1;
            this.contentLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.contentLayout.Size = new System.Drawing.Size(1344, 773);
            this.contentLayout.TabIndex = 1;
            // 
            // summaryHost
            // 
            this.summaryHost.AutoScroll = true;
            this.summaryHost.BackColor = System.Drawing.Color.Transparent;
            this.summaryHost.Controls.Add(this.summaryCard);
            this.summaryHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.summaryHost.Location = new System.Drawing.Point(0, 0);
            this.summaryHost.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.summaryHost.Name = "summaryHost";
            this.summaryHost.Size = new System.Drawing.Size(350, 773);
            this.summaryHost.TabIndex = 0;
            // 
            // summaryCard
            // 
            this.summaryCard.BackColor = System.Drawing.Color.White;
            this.summaryCard.ColumnCount = 1;
            this.summaryCard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.summaryCard.Controls.Add(this.avatarHost, 0, 0);
            this.summaryCard.Controls.Add(this.btnChooseAvatar, 0, 1);
            this.summaryCard.Controls.Add(this.lblAvatarHint, 0, 2);
            this.summaryCard.Controls.Add(this.lblFullName, 0, 3);
            this.summaryCard.Controls.Add(this.lblRoleBadge, 0, 4);
            this.summaryCard.Controls.Add(this.lblSidebarEmail, 0, 5);
            this.summaryCard.Controls.Add(this.lblSidebarPhone, 0, 6);
            this.summaryCard.Controls.Add(this.lblJoinedAt, 0, 7);
            this.summaryCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.summaryCard.Location = new System.Drawing.Point(0, 0);
            this.summaryCard.Margin = new System.Windows.Forms.Padding(0);
            this.summaryCard.Name = "summaryCard";
            this.summaryCard.Padding = new System.Windows.Forms.Padding(16);
            this.summaryCard.RowCount = 8;
            this.summaryCard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 112F));
            this.summaryCard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.summaryCard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.summaryCard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.summaryCard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.summaryCard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.summaryCard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.summaryCard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.summaryCard.Size = new System.Drawing.Size(350, 440);
            this.summaryCard.TabIndex = 0;
            // 
            // avatarHost
            // 
            this.avatarHost.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(112)))), ((int)(((byte)(220)))));
            this.avatarHost.Controls.Add(this.lblAvatarInitial);
            this.avatarHost.Controls.Add(this.pictureAvatar);
            this.avatarHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.avatarHost.Location = new System.Drawing.Point(16, 16);
            this.avatarHost.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.avatarHost.Name = "avatarHost";
            this.avatarHost.Size = new System.Drawing.Size(318, 104);
            this.avatarHost.TabIndex = 0;
            // 
            // lblAvatarInitial
            // 
            this.lblAvatarInitial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAvatarInitial.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblAvatarInitial.ForeColor = System.Drawing.Color.White;
            this.lblAvatarInitial.Location = new System.Drawing.Point(0, 0);
            this.lblAvatarInitial.Name = "lblAvatarInitial";
            this.lblAvatarInitial.Size = new System.Drawing.Size(318, 104);
            this.lblAvatarInitial.TabIndex = 0;
            this.lblAvatarInitial.Text = "?";
            this.lblAvatarInitial.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureAvatar
            // 
            this.pictureAvatar.BackColor = System.Drawing.Color.White;
            this.pictureAvatar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureAvatar.Location = new System.Drawing.Point(0, 0);
            this.pictureAvatar.Margin = new System.Windows.Forms.Padding(0);
            this.pictureAvatar.Name = "pictureAvatar";
            this.pictureAvatar.Size = new System.Drawing.Size(318, 104);
            this.pictureAvatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureAvatar.TabIndex = 1;
            this.pictureAvatar.TabStop = false;
            this.pictureAvatar.Visible = false;
            // 
            // btnChooseAvatar
            // 
            this.btnChooseAvatar.BackColor = System.Drawing.Color.White;
            this.btnChooseAvatar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnChooseAvatar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChooseAvatar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnChooseAvatar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(112)))), ((int)(((byte)(220)))));
            this.btnChooseAvatar.Location = new System.Drawing.Point(28, 130);
            this.btnChooseAvatar.Margin = new System.Windows.Forms.Padding(12, 2, 12, 4);
            this.btnChooseAvatar.Name = "btnChooseAvatar";
            this.btnChooseAvatar.Size = new System.Drawing.Size(294, 32);
            this.btnChooseAvatar.TabIndex = 1;
            this.btnChooseAvatar.Text = "Chọn ảnh";
            this.btnChooseAvatar.UseVisualStyleBackColor = false;
            // 
            // lblAvatarHint
            // 
            this.lblAvatarHint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAvatarHint.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblAvatarHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblAvatarHint.Location = new System.Drawing.Point(19, 166);
            this.lblAvatarHint.Name = "lblAvatarHint";
            this.lblAvatarHint.Size = new System.Drawing.Size(312, 30);
            this.lblAvatarHint.TabIndex = 2;
            this.lblAvatarHint.Text = "Tuỳ chọn · JPG, PNG, BMP · tối đa 5MB";
            this.lblAvatarHint.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblFullName
            // 
            this.lblFullName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFullName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblFullName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblFullName.Location = new System.Drawing.Point(19, 196);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(312, 48);
            this.lblFullName.TabIndex = 3;
            this.lblFullName.Text = "Người dùng";
            this.lblFullName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRoleBadge
            // 
            this.lblRoleBadge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRoleBadge.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblRoleBadge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(112)))), ((int)(((byte)(220)))));
            this.lblRoleBadge.Location = new System.Drawing.Point(19, 244);
            this.lblRoleBadge.Name = "lblRoleBadge";
            this.lblRoleBadge.Size = new System.Drawing.Size(312, 32);
            this.lblRoleBadge.TabIndex = 4;
            this.lblRoleBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSidebarEmail
            // 
            this.lblSidebarEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSidebarEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSidebarEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblSidebarEmail.Location = new System.Drawing.Point(19, 276);
            this.lblSidebarEmail.Name = "lblSidebarEmail";
            this.lblSidebarEmail.Size = new System.Drawing.Size(312, 56);
            this.lblSidebarEmail.TabIndex = 5;
            this.lblSidebarEmail.Text = "Email";
            this.lblSidebarEmail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSidebarPhone
            // 
            this.lblSidebarPhone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSidebarPhone.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSidebarPhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblSidebarPhone.Location = new System.Drawing.Point(19, 332);
            this.lblSidebarPhone.Name = "lblSidebarPhone";
            this.lblSidebarPhone.Size = new System.Drawing.Size(312, 56);
            this.lblSidebarPhone.TabIndex = 6;
            this.lblSidebarPhone.Text = "Số điện thoại: Chưa cập nhật";
            this.lblSidebarPhone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblJoinedAt
            // 
            this.lblJoinedAt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblJoinedAt.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblJoinedAt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblJoinedAt.Location = new System.Drawing.Point(19, 388);
            this.lblJoinedAt.Name = "lblJoinedAt";
            this.lblJoinedAt.Size = new System.Drawing.Size(312, 36);
            this.lblJoinedAt.TabIndex = 7;
            this.lblJoinedAt.Text = "Tham gia từ";
            this.lblJoinedAt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // profileHost
            // 
            this.profileHost.AutoScroll = true;
            this.profileHost.BackColor = System.Drawing.Color.Transparent;
            this.profileHost.Controls.Add(this.profileCard);
            this.profileHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.profileHost.Location = new System.Drawing.Point(362, 0);
            this.profileHost.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.profileHost.Name = "profileHost";
            this.profileHost.Size = new System.Drawing.Size(482, 773);
            this.profileHost.TabIndex = 1;
            // 
            // profileCard
            // 
            this.profileCard.BackColor = System.Drawing.Color.White;
            this.profileCard.ColumnCount = 1;
            this.profileCard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.profileCard.Controls.Add(this.lblProfileHeader, 0, 0);
            this.profileCard.Controls.Add(this.lblFullNameField, 0, 1);
            this.profileCard.Controls.Add(this.txtFullName, 0, 2);
            this.profileCard.Controls.Add(this.lblPhoneField, 0, 3);
            this.profileCard.Controls.Add(this.txtPhone, 0, 4);
            this.profileCard.Controls.Add(this.lblEmailField, 0, 5);
            this.profileCard.Controls.Add(this.txtEmail, 0, 6);
            this.profileCard.Controls.Add(this.lblProfileError, 0, 7);
            this.profileCard.Controls.Add(this.btnSaveProfile, 0, 8);
            this.profileCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.profileCard.Location = new System.Drawing.Point(0, 0);
            this.profileCard.Margin = new System.Windows.Forms.Padding(0);
            this.profileCard.Name = "profileCard";
            this.profileCard.Padding = new System.Windows.Forms.Padding(18);
            this.profileCard.RowCount = 9;
            this.profileCard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.profileCard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.profileCard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.profileCard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.profileCard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.profileCard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.profileCard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.profileCard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.profileCard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.profileCard.Size = new System.Drawing.Size(482, 372);
            this.profileCard.TabIndex = 0;
            // 
            // lblProfileHeader
            // 
            this.lblProfileHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProfileHeader.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblProfileHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblProfileHeader.Location = new System.Drawing.Point(21, 18);
            this.lblProfileHeader.Name = "lblProfileHeader";
            this.lblProfileHeader.Size = new System.Drawing.Size(440, 42);
            this.lblProfileHeader.TabIndex = 0;
            this.lblProfileHeader.Text = "Thông tin cá nhân";
            this.lblProfileHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFullNameField
            // 
            this.lblFullNameField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFullNameField.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFullNameField.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblFullNameField.Location = new System.Drawing.Point(21, 60);
            this.lblFullNameField.Name = "lblFullNameField";
            this.lblFullNameField.Size = new System.Drawing.Size(440, 26);
            this.lblFullNameField.TabIndex = 1;
            this.lblFullNameField.Text = "Họ và tên *";
            this.lblFullNameField.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // txtFullName
            // 
            this.txtFullName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFullName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtFullName.Location = new System.Drawing.Point(18, 90);
            this.txtFullName.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.txtFullName.MaxLength = 100;
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(446, 31);
            this.txtFullName.TabIndex = 2;
            // 
            // lblPhoneField
            // 
            this.lblPhoneField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPhoneField.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPhoneField.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblPhoneField.Location = new System.Drawing.Point(21, 126);
            this.lblPhoneField.Name = "lblPhoneField";
            this.lblPhoneField.Size = new System.Drawing.Size(440, 26);
            this.lblPhoneField.TabIndex = 3;
            this.lblPhoneField.Text = "Số điện thoại";
            this.lblPhoneField.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // txtPhone
            // 
            this.txtPhone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPhone.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtPhone.Location = new System.Drawing.Point(18, 156);
            this.txtPhone.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.txtPhone.MaxLength = 15;
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(446, 31);
            this.txtPhone.TabIndex = 4;
            // 
            // lblEmailField
            // 
            this.lblEmailField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEmailField.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEmailField.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblEmailField.Location = new System.Drawing.Point(21, 192);
            this.lblEmailField.Name = "lblEmailField";
            this.lblEmailField.Size = new System.Drawing.Size(440, 26);
            this.lblEmailField.TabIndex = 5;
            this.lblEmailField.Text = "Email";
            this.lblEmailField.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.txtEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtEmail.Location = new System.Drawing.Point(18, 222);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.txtEmail.MaxLength = 150;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.ReadOnly = true;
            this.txtEmail.Size = new System.Drawing.Size(446, 31);
            this.txtEmail.TabIndex = 6;
            // 
            // lblProfileError
            // 
            this.lblProfileError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProfileError.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblProfileError.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.lblProfileError.Location = new System.Drawing.Point(21, 258);
            this.lblProfileError.Name = "lblProfileError";
            this.lblProfileError.Size = new System.Drawing.Size(440, 48);
            this.lblProfileError.TabIndex = 7;
            this.lblProfileError.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblProfileError.Visible = false;
            // 
            // btnSaveProfile
            // 
            this.btnSaveProfile.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSaveProfile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(112)))), ((int)(((byte)(220)))));
            this.btnSaveProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveProfile.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSaveProfile.ForeColor = System.Drawing.Color.White;
            this.btnSaveProfile.Location = new System.Drawing.Point(314, 312);
            this.btnSaveProfile.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.btnSaveProfile.Name = "btnSaveProfile";
            this.btnSaveProfile.Size = new System.Drawing.Size(150, 36);
            this.btnSaveProfile.TabIndex = 8;
            this.btnSaveProfile.Text = "Lưu thay đổi";
            this.btnSaveProfile.UseVisualStyleBackColor = false;
            // 
            // passwordHost
            // 
            this.passwordHost.AutoScroll = true;
            this.passwordHost.BackColor = System.Drawing.Color.Transparent;
            this.passwordHost.Controls.Add(this.passwordCard);
            this.passwordHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.passwordHost.Location = new System.Drawing.Point(852, 0);
            this.passwordHost.Margin = new System.Windows.Forms.Padding(0);
            this.passwordHost.Name = "passwordHost";
            this.passwordHost.Size = new System.Drawing.Size(492, 773);
            this.passwordHost.TabIndex = 2;
            // 
            // passwordCard
            // 
            this.passwordCard.BackColor = System.Drawing.Color.White;
            this.passwordCard.ColumnCount = 1;
            this.passwordCard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.passwordCard.Controls.Add(this.lblPasswordHeader, 0, 0);
            this.passwordCard.Controls.Add(this.lblCurrentPassword, 0, 1);
            this.passwordCard.Controls.Add(this.txtCurrentPassword, 0, 2);
            this.passwordCard.Controls.Add(this.lblNewPassword, 0, 3);
            this.passwordCard.Controls.Add(this.txtNewPassword, 0, 4);
            this.passwordCard.Controls.Add(this.lblConfirmPassword, 0, 5);
            this.passwordCard.Controls.Add(this.txtConfirmPassword, 0, 6);
            this.passwordCard.Controls.Add(this.lblPasswordError, 0, 7);
            this.passwordCard.Controls.Add(this.btnChangePassword, 0, 8);
            this.passwordCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.passwordCard.Location = new System.Drawing.Point(0, 0);
            this.passwordCard.Margin = new System.Windows.Forms.Padding(0);
            this.passwordCard.Name = "passwordCard";
            this.passwordCard.Padding = new System.Windows.Forms.Padding(18);
            this.passwordCard.RowCount = 9;
            this.passwordCard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.passwordCard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.passwordCard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.passwordCard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.passwordCard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.passwordCard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.passwordCard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.passwordCard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.passwordCard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.passwordCard.Size = new System.Drawing.Size(492, 372);
            this.passwordCard.TabIndex = 0;
            // 
            // lblPasswordHeader
            // 
            this.lblPasswordHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPasswordHeader.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblPasswordHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblPasswordHeader.Location = new System.Drawing.Point(21, 18);
            this.lblPasswordHeader.Name = "lblPasswordHeader";
            this.lblPasswordHeader.Size = new System.Drawing.Size(450, 42);
            this.lblPasswordHeader.TabIndex = 0;
            this.lblPasswordHeader.Text = "Đổi mật khẩu";
            this.lblPasswordHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCurrentPassword
            // 
            this.lblCurrentPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrentPassword.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCurrentPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblCurrentPassword.Location = new System.Drawing.Point(21, 60);
            this.lblCurrentPassword.Name = "lblCurrentPassword";
            this.lblCurrentPassword.Size = new System.Drawing.Size(450, 26);
            this.lblCurrentPassword.TabIndex = 1;
            this.lblCurrentPassword.Text = "Mật khẩu hiện tại";
            this.lblCurrentPassword.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // txtCurrentPassword
            // 
            this.txtCurrentPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCurrentPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtCurrentPassword.Location = new System.Drawing.Point(18, 90);
            this.txtCurrentPassword.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.txtCurrentPassword.Name = "txtCurrentPassword";
            this.txtCurrentPassword.Size = new System.Drawing.Size(456, 31);
            this.txtCurrentPassword.TabIndex = 2;
            this.txtCurrentPassword.UseSystemPasswordChar = true;
            // 
            // lblNewPassword
            // 
            this.lblNewPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNewPassword.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNewPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblNewPassword.Location = new System.Drawing.Point(21, 126);
            this.lblNewPassword.Name = "lblNewPassword";
            this.lblNewPassword.Size = new System.Drawing.Size(450, 26);
            this.lblNewPassword.TabIndex = 3;
            this.lblNewPassword.Text = "Mật khẩu mới";
            this.lblNewPassword.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNewPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNewPassword.Location = new System.Drawing.Point(18, 156);
            this.txtNewPassword.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.Size = new System.Drawing.Size(456, 31);
            this.txtNewPassword.TabIndex = 4;
            this.txtNewPassword.UseSystemPasswordChar = true;
            // 
            // lblConfirmPassword
            // 
            this.lblConfirmPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblConfirmPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblConfirmPassword.Location = new System.Drawing.Point(21, 192);
            this.lblConfirmPassword.Name = "lblConfirmPassword";
            this.lblConfirmPassword.Size = new System.Drawing.Size(450, 26);
            this.lblConfirmPassword.TabIndex = 5;
            this.lblConfirmPassword.Text = "Xác nhận mật khẩu mới";
            this.lblConfirmPassword.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtConfirmPassword.Location = new System.Drawing.Point(18, 222);
            this.txtConfirmPassword.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.Size = new System.Drawing.Size(456, 31);
            this.txtConfirmPassword.TabIndex = 6;
            this.txtConfirmPassword.UseSystemPasswordChar = true;
            // 
            // lblPasswordError
            // 
            this.lblPasswordError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPasswordError.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblPasswordError.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.lblPasswordError.Location = new System.Drawing.Point(21, 258);
            this.lblPasswordError.Name = "lblPasswordError";
            this.lblPasswordError.Size = new System.Drawing.Size(450, 48);
            this.lblPasswordError.TabIndex = 7;
            this.lblPasswordError.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPasswordError.Visible = false;
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnChangePassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(112)))), ((int)(((byte)(220)))));
            this.btnChangePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangePassword.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnChangePassword.ForeColor = System.Drawing.Color.White;
            this.btnChangePassword.Location = new System.Drawing.Point(324, 312);
            this.btnChangePassword.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(150, 36);
            this.btnChangePassword.TabIndex = 8;
            this.btnChangePassword.Text = "Đổi mật khẩu";
            this.btnChangePassword.UseVisualStyleBackColor = false;
            // 
            // ucMyProfile
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.rootLayout);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ucMyProfile";
            this.Padding = new System.Windows.Forms.Padding(20, 16, 20, 20);
            this.Size = new System.Drawing.Size(1384, 857);
            this.rootLayout.ResumeLayout(false);
            this.rootLayout.PerformLayout();
            this.contentLayout.ResumeLayout(false);
            this.summaryHost.ResumeLayout(false);
            this.summaryCard.ResumeLayout(false);
            this.avatarHost.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureAvatar)).EndInit();
            this.profileHost.ResumeLayout(false);
            this.profileCard.ResumeLayout(false);
            this.profileCard.PerformLayout();
            this.passwordHost.ResumeLayout(false);
            this.passwordCard.ResumeLayout(false);
            this.passwordCard.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
