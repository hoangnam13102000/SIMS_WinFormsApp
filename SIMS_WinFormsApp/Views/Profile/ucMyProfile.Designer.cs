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
            this.profileHost = new System.Windows.Forms.Panel();
            this.passwordHost = new System.Windows.Forms.Panel();
            this.summaryCard = new System.Windows.Forms.TableLayoutPanel();
            this.avatarHost = new System.Windows.Forms.Panel();
            this.pictureAvatar = new System.Windows.Forms.PictureBox();
            this.lblAvatarInitial = new System.Windows.Forms.Label();
            this.btnChooseAvatar = new System.Windows.Forms.Button();
            this.lblAvatarHint = new System.Windows.Forms.Label();
            this.lblFullName = new System.Windows.Forms.Label();
            this.lblRoleBadge = new System.Windows.Forms.Label();
            this.lblSidebarEmail = new System.Windows.Forms.Label();
            this.lblSidebarPhone = new System.Windows.Forms.Label();
            this.lblJoinedAt = new System.Windows.Forms.Label();
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
            this.profileHost.SuspendLayout();
            this.passwordHost.SuspendLayout();
            this.summaryCard.SuspendLayout();
            this.avatarHost.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureAvatar)).BeginInit();
            this.profileCard.SuspendLayout();
            this.passwordCard.SuspendLayout();
            this.SuspendLayout();
            //
            // rootLayout
            //
            this.rootLayout.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.rootLayout.ColumnCount = 1;
            this.rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rootLayout.Controls.Add(this.lblPageTitle, 0, 0);
            this.rootLayout.Controls.Add(this.contentLayout, 0, 1);
            this.rootLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rootLayout.Margin = new System.Windows.Forms.Padding(0);
            this.rootLayout.RowCount = 2;
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rootLayout.Padding = new System.Windows.Forms.Padding(0);
            this.rootLayout.Name = "rootLayout";
            //
            // lblPageTitle
            //
            this.lblPageTitle.AutoSize = true;
            this.lblPageTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPageTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblPageTitle.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.lblPageTitle.Name = "lblPageTitle";
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
            this.contentLayout.Margin = new System.Windows.Forms.Padding(0);
            this.contentLayout.Name = "contentLayout";
            this.contentLayout.RowCount = 1;
            this.contentLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.contentLayout.Padding = new System.Windows.Forms.Padding(0);
            //
            // summaryHost
            //
            this.summaryHost.AutoScroll = true;
            this.summaryHost.BackColor = System.Drawing.Color.Transparent;
            this.summaryHost.Controls.Add(this.summaryCard);
            this.summaryHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.summaryHost.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.summaryHost.Name = "summaryHost";
            this.summaryHost.Padding = new System.Windows.Forms.Padding(0);
            //
            // profileHost
            //
            this.profileHost.AutoScroll = true;
            this.profileHost.BackColor = System.Drawing.Color.Transparent;
            this.profileHost.Controls.Add(this.profileCard);
            this.profileHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.profileHost.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.profileHost.Name = "profileHost";
            this.profileHost.Padding = new System.Windows.Forms.Padding(0);
            //
            // passwordHost
            //
            this.passwordHost.AutoScroll = true;
            this.passwordHost.BackColor = System.Drawing.Color.Transparent;
            this.passwordHost.Controls.Add(this.passwordCard);
            this.passwordHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.passwordHost.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.passwordHost.Name = "passwordHost";
            this.passwordHost.Padding = new System.Windows.Forms.Padding(0);
            //
            // summaryCard
            //
            this.summaryCard.BackColor = System.Drawing.Color.White;
            this.summaryCard.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.None;
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
            this.summaryCard.Height = 440;
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
            //
            // avatarHost
            //
            this.avatarHost.BackColor = System.Drawing.Color.FromArgb(39, 112, 220);
            this.avatarHost.Controls.Add(this.lblAvatarInitial);
            this.avatarHost.Controls.Add(this.pictureAvatar);
            this.avatarHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.avatarHost.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.avatarHost.Name = "avatarHost";
            this.avatarHost.Size = new System.Drawing.Size(200, 104);
            //
            // pictureAvatar
            //
            this.pictureAvatar.BackColor = System.Drawing.Color.White;
            this.pictureAvatar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureAvatar.Location = new System.Drawing.Point(0, 0);
            this.pictureAvatar.Margin = new System.Windows.Forms.Padding(0);
            this.pictureAvatar.Name = "pictureAvatar";
            this.pictureAvatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureAvatar.TabStop = false;
            this.pictureAvatar.Visible = false;
            //
            // lblAvatarInitial
            //
            this.lblAvatarInitial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAvatarInitial.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblAvatarInitial.ForeColor = System.Drawing.Color.White;
            this.lblAvatarInitial.Name = "lblAvatarInitial";
            this.lblAvatarInitial.Text = "?";
            this.lblAvatarInitial.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // btnChooseAvatar
            //
            this.btnChooseAvatar.BackColor = System.Drawing.Color.White;
            this.btnChooseAvatar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnChooseAvatar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChooseAvatar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnChooseAvatar.ForeColor = System.Drawing.Color.FromArgb(39, 112, 220);
            this.btnChooseAvatar.Margin = new System.Windows.Forms.Padding(12, 2, 12, 4);
            this.btnChooseAvatar.Name = "btnChooseAvatar";
            this.btnChooseAvatar.Text = "Chọn ảnh";
            this.btnChooseAvatar.UseVisualStyleBackColor = false;
            //
            // lblAvatarHint
            //
            this.lblAvatarHint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAvatarHint.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblAvatarHint.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblAvatarHint.Name = "lblAvatarHint";
            this.lblAvatarHint.Text = "Tuỳ chọn · JPG, PNG, BMP · tối đa 5MB";
            this.lblAvatarHint.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            //
            // lblFullName
            //
            this.lblFullName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFullName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblFullName.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Text = "Người dùng";
            this.lblFullName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblRoleBadge
            //
            this.lblRoleBadge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRoleBadge.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblRoleBadge.ForeColor = System.Drawing.Color.FromArgb(39, 112, 220);
            this.lblRoleBadge.Name = "lblRoleBadge";
            this.lblRoleBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblSidebarEmail
            //
            this.lblSidebarEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSidebarEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSidebarEmail.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblSidebarEmail.Name = "lblSidebarEmail";
            this.lblSidebarEmail.Text = "Email";
            this.lblSidebarEmail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblSidebarPhone
            //
            this.lblSidebarPhone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSidebarPhone.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSidebarPhone.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblSidebarPhone.Name = "lblSidebarPhone";
            this.lblSidebarPhone.Text = "Số điện thoại: Chưa cập nhật";
            this.lblSidebarPhone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblJoinedAt
            //
            this.lblJoinedAt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblJoinedAt.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblJoinedAt.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblJoinedAt.Name = "lblJoinedAt";
            this.lblJoinedAt.Text = "Tham gia từ";
            this.lblJoinedAt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.profileCard.Height = 372;
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
            //
            // lblProfileHeader
            //
            this.lblProfileHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProfileHeader.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblProfileHeader.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblProfileHeader.Name = "lblProfileHeader";
            this.lblProfileHeader.Text = "Thông tin cá nhân";
            this.lblProfileHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblFullNameField
            //
            this.lblFullNameField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFullNameField.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFullNameField.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblFullNameField.Name = "lblFullNameField";
            this.lblFullNameField.Text = "Họ và tên *";
            this.lblFullNameField.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            //
            // txtFullName
            //
            this.txtFullName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFullName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtFullName.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.txtFullName.MaxLength = 100;
            this.txtFullName.Name = "txtFullName";
            //
            // lblPhoneField
            //
            this.lblPhoneField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPhoneField.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPhoneField.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblPhoneField.Name = "lblPhoneField";
            this.lblPhoneField.Text = "Số điện thoại";
            this.lblPhoneField.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            //
            // txtPhone
            //
            this.txtPhone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPhone.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtPhone.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.txtPhone.MaxLength = 15;
            this.txtPhone.Name = "txtPhone";
            //
            // lblEmailField
            //
            this.lblEmailField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEmailField.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEmailField.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblEmailField.Name = "lblEmailField";
            this.lblEmailField.Text = "Email";
            this.lblEmailField.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            //
            // txtEmail
            //
            this.txtEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.txtEmail.MaxLength = 150;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.ReadOnly = true;
            this.txtEmail.BackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            //
            // lblProfileError
            //
            this.lblProfileError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProfileError.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblProfileError.ForeColor = System.Drawing.Color.FromArgb(185, 28, 28);
            this.lblProfileError.Name = "lblProfileError";
            this.lblProfileError.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblProfileError.Visible = false;
            //
            // btnSaveProfile
            //
            this.btnSaveProfile.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSaveProfile.BackColor = System.Drawing.Color.FromArgb(39, 112, 220);
            this.btnSaveProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveProfile.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSaveProfile.ForeColor = System.Drawing.Color.White;
            this.btnSaveProfile.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.btnSaveProfile.Name = "btnSaveProfile";
            this.btnSaveProfile.Size = new System.Drawing.Size(150, 36);
            this.btnSaveProfile.Text = "Lưu thay đổi";
            this.btnSaveProfile.UseVisualStyleBackColor = false;
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
            this.passwordCard.Height = 372;
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
            //
            // lblPasswordHeader
            //
            this.lblPasswordHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPasswordHeader.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblPasswordHeader.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblPasswordHeader.Name = "lblPasswordHeader";
            this.lblPasswordHeader.Text = "Đổi mật khẩu";
            this.lblPasswordHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblCurrentPassword
            //
            this.lblCurrentPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrentPassword.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCurrentPassword.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblCurrentPassword.Name = "lblCurrentPassword";
            this.lblCurrentPassword.Text = "Mật khẩu hiện tại";
            this.lblCurrentPassword.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            //
            // txtCurrentPassword
            //
            this.txtCurrentPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCurrentPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtCurrentPassword.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.txtCurrentPassword.Name = "txtCurrentPassword";
            this.txtCurrentPassword.UseSystemPasswordChar = true;
            //
            // lblNewPassword
            //
            this.lblNewPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNewPassword.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNewPassword.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblNewPassword.Name = "lblNewPassword";
            this.lblNewPassword.Text = "Mật khẩu mới";
            this.lblNewPassword.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            //
            // txtNewPassword
            //
            this.txtNewPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNewPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNewPassword.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.UseSystemPasswordChar = true;
            //
            // lblConfirmPassword
            //
            this.lblConfirmPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblConfirmPassword.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblConfirmPassword.Name = "lblConfirmPassword";
            this.lblConfirmPassword.Text = "Xác nhận mật khẩu mới";
            this.lblConfirmPassword.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            //
            // txtConfirmPassword
            //
            this.txtConfirmPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtConfirmPassword.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.UseSystemPasswordChar = true;
            //
            // lblPasswordError
            //
            this.lblPasswordError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPasswordError.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblPasswordError.ForeColor = System.Drawing.Color.FromArgb(185, 28, 28);
            this.lblPasswordError.Name = "lblPasswordError";
            this.lblPasswordError.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPasswordError.Visible = false;
            //
            // btnChangePassword
            //
            this.btnChangePassword.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnChangePassword.BackColor = System.Drawing.Color.FromArgb(39, 112, 220);
            this.btnChangePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangePassword.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnChangePassword.ForeColor = System.Drawing.Color.White;
            this.btnChangePassword.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(150, 36);
            this.btnChangePassword.Text = "Đổi mật khẩu";
            this.btnChangePassword.UseVisualStyleBackColor = false;
            //
            // ucMyProfile
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.Controls.Add(this.rootLayout);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ucMyProfile";
            this.Padding = new System.Windows.Forms.Padding(20, 16, 20, 20);
            this.Size = new System.Drawing.Size(1100, 680);
            this.rootLayout.ResumeLayout(false);
            this.rootLayout.PerformLayout();
            this.contentLayout.ResumeLayout(false);
            this.summaryHost.ResumeLayout(false);
            this.profileHost.ResumeLayout(false);
            this.passwordHost.ResumeLayout(false);
            this.summaryCard.ResumeLayout(false);
            this.avatarHost.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureAvatar)).EndInit();
            this.profileCard.ResumeLayout(false);
            this.profileCard.PerformLayout();
            this.passwordCard.ResumeLayout(false);
            this.passwordCard.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
