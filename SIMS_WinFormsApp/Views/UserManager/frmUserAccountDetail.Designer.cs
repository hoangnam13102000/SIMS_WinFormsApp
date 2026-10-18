using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.Views.Dialogs;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Views.UserManager
{
    partial class frmUserAccountDetail
    {
        private Panel _designerHeader;
        private Label _lblDialogTitle;
        private Button _btnClose;
        private Panel _designerBody;
        private Panel _designerSidebar;
        private Panel _designerAvatarCircle;
        private Label _lblAvatarInitial;
        private Label _lblAvatarName;
        private Label _lblAvatarSubtitle;
        private Panel _designerContent;
        private Button _tabAccount;
        private Button _tabSecurity;
        private Panel _designerFooter;
        private Button _btnFooterClose;
        private TableLayoutPanel _designerAccountGrid;
        private TableLayoutPanel _designerSecurityGrid;
        private DetailInfoItemControl _itemAccountCode;
        private DetailInfoItemControl _itemRole;
        private DetailInfoItemControl _itemEmail;
        private DetailInfoItemControl _itemPhone;
        private DetailInfoItemControl _itemCreatedAt;
        private DetailInfoItemControl _itemAccountStatus;
        private DetailInfoItemControl _itemLockStatus;
        private DetailInfoItemControl _itemFailedLogin;
        private DetailInfoItemControl _itemGender;
        private DetailInfoItemControl _itemSalary;

        private void InitializeComponent()
        {
            this._designerHeader = new System.Windows.Forms.Panel();
            this._lblDialogTitle = new System.Windows.Forms.Label();
            this._btnClose = new System.Windows.Forms.Button();
            this._designerBody = new System.Windows.Forms.Panel();
            this._designerContent = new System.Windows.Forms.Panel();
            this._designerAccountGrid = new System.Windows.Forms.TableLayoutPanel();
            this._itemAccountCode = new DetailInfoItemControl();
            this._itemEmail = new DetailInfoItemControl();
            this._itemRole = new DetailInfoItemControl();
            this._itemPhone = new DetailInfoItemControl();
            this._itemCreatedAt = new DetailInfoItemControl();
            this._itemAccountStatus = new DetailInfoItemControl();
            this._itemGender = new DetailInfoItemControl();
            this._itemSalary = new DetailInfoItemControl();
            this._designerSecurityGrid = new System.Windows.Forms.TableLayoutPanel();
            this._itemLockStatus = new DetailInfoItemControl();
            this._itemFailedLogin = new DetailInfoItemControl();
            this._tabAccount = new System.Windows.Forms.Button();
            this._tabSecurity = new System.Windows.Forms.Button();
            this._designerSidebar = new System.Windows.Forms.Panel();
            this._designerAvatarCircle = new System.Windows.Forms.Panel();
            this._lblAvatarInitial = new System.Windows.Forms.Label();
            this._lblAvatarName = new System.Windows.Forms.Label();
            this._lblAvatarSubtitle = new System.Windows.Forms.Label();
            this._designerFooter = new System.Windows.Forms.Panel();
            this._btnFooterClose = new System.Windows.Forms.Button();
            this._designerHeader.SuspendLayout();
            this._designerBody.SuspendLayout();
            this._designerContent.SuspendLayout();
            this._designerAccountGrid.SuspendLayout();
            this._designerSecurityGrid.SuspendLayout();
            this._designerSidebar.SuspendLayout();
            this._designerAvatarCircle.SuspendLayout();
            this._designerFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // _designerHeader
            // 
            this._designerHeader.BackColor = System.Drawing.Color.White;
            this._designerHeader.Controls.Add(this._lblDialogTitle);
            this._designerHeader.Controls.Add(this._btnClose);
            this._designerHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this._designerHeader.Location = new System.Drawing.Point(0, 0);
            this._designerHeader.Name = "_designerHeader";
            this._designerHeader.Size = new System.Drawing.Size(1150, 64);
            this._designerHeader.TabIndex = 0;
            // 
            // _lblDialogTitle
            // 
            this._lblDialogTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this._lblDialogTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._lblDialogTitle.Location = new System.Drawing.Point(28, 16);
            this._lblDialogTitle.Name = "_lblDialogTitle";
            this._lblDialogTitle.Size = new System.Drawing.Size(850, 45);
            this._lblDialogTitle.TabIndex = 0;
            this._lblDialogTitle.Text = "Chi tiết tài khoản nhân viên";
            this._lblDialogTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _btnClose
            // 
            this._btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this._btnClose.FlatAppearance.BorderSize = 0;
            this._btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnClose.Font = new System.Drawing.Font("Segoe UI", 16F);
            this._btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._btnClose.Location = new System.Drawing.Point(1092, 12);
            this._btnClose.Name = "_btnClose";
            this._btnClose.Size = new System.Drawing.Size(40, 40);
            this._btnClose.TabIndex = 1;
            this._btnClose.Text = "×";
            this._btnClose.UseVisualStyleBackColor = false;
            // 
            // _designerBody
            // 
            this._designerBody.BackColor = System.Drawing.Color.White;
            this._designerBody.Controls.Add(this._designerContent);
            this._designerBody.Controls.Add(this._designerSidebar);
            this._designerBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this._designerBody.Location = new System.Drawing.Point(0, 64);
            this._designerBody.Name = "_designerBody";
            this._designerBody.Size = new System.Drawing.Size(1150, 524);
            this._designerBody.TabIndex = 1;
            // 
            // _designerContent
            // 
            this._designerContent.BackColor = System.Drawing.Color.White;
            this._designerContent.Controls.Add(this._designerAccountGrid);
            this._designerContent.Controls.Add(this._designerSecurityGrid);
            this._designerContent.Controls.Add(this._tabAccount);
            this._designerContent.Controls.Add(this._tabSecurity);
            this._designerContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this._designerContent.Location = new System.Drawing.Point(208, 0);
            this._designerContent.Name = "_designerContent";
            this._designerContent.Padding = new System.Windows.Forms.Padding(24, 16, 24, 16);
            this._designerContent.Size = new System.Drawing.Size(942, 524);
            this._designerContent.TabIndex = 1;
            // 
            // _designerAccountGrid
            // 
            this._designerAccountGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._designerAccountGrid.BackColor = System.Drawing.Color.Transparent;
            this._designerAccountGrid.ColumnCount = 2;
            this._designerAccountGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._designerAccountGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._designerAccountGrid.Controls.Add(this._itemAccountCode, 0, 0);
            this._designerAccountGrid.Controls.Add(this._itemEmail, 1, 0);
            this._designerAccountGrid.Controls.Add(this._itemRole, 0, 1);
            this._designerAccountGrid.Controls.Add(this._itemPhone, 1, 1);
            this._designerAccountGrid.Controls.Add(this._itemCreatedAt, 0, 2);
            this._designerAccountGrid.Controls.Add(this._itemAccountStatus, 1, 2);
            this._designerAccountGrid.Controls.Add(this._itemGender, 0, 3);
            this._designerAccountGrid.Controls.Add(this._itemSalary, 1, 3);
            this._designerAccountGrid.Location = new System.Drawing.Point(24, 56);
            this._designerAccountGrid.Name = "_designerAccountGrid";
            this._designerAccountGrid.RowCount = 4;
            this._designerAccountGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this._designerAccountGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this._designerAccountGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this._designerAccountGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this._designerAccountGrid.Size = new System.Drawing.Size(894, 384);
            this._designerAccountGrid.TabIndex = 2;
            // 
            // _itemAccountCode
            // 
            this._itemAccountCode.BackColor = System.Drawing.Color.Transparent;
            this._itemAccountCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this._itemAccountCode.Icon = FontAwesome.Sharp.IconChar.IdCard;
            this._itemAccountCode.IconBackground = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(243)))), ((int)(((byte)(251)))));
            this._itemAccountCode.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this._itemAccountCode.LabelText = "Mã tài khoản";
            this._itemAccountCode.Location = new System.Drawing.Point(0, 0);
            this._itemAccountCode.Margin = new System.Windows.Forms.Padding(0, 0, 16, 8);
            this._itemAccountCode.MinimumSize = new System.Drawing.Size(160, 48);
            this._itemAccountCode.Name = "_itemAccountCode";
            this._itemAccountCode.Size = new System.Drawing.Size(431, 88);
            this._itemAccountCode.TabIndex = 0;
            this._itemAccountCode.ValueText = "nguyenvana";
            // 
            // _itemEmail
            // 
            this._itemEmail.BackColor = System.Drawing.Color.Transparent;
            this._itemEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this._itemEmail.Icon = FontAwesome.Sharp.IconChar.Envelope;
            this._itemEmail.IconBackground = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(243)))), ((int)(((byte)(251)))));
            this._itemEmail.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this._itemEmail.LabelText = "Email";
            this._itemEmail.Location = new System.Drawing.Point(447, 0);
            this._itemEmail.Margin = new System.Windows.Forms.Padding(0, 0, 16, 8);
            this._itemEmail.MinimumSize = new System.Drawing.Size(160, 48);
            this._itemEmail.Name = "_itemEmail";
            this._itemEmail.Size = new System.Drawing.Size(431, 88);
            this._itemEmail.TabIndex = 1;
            this._itemEmail.ValueText = "nguyenvana@example.com";
            // 
            // _itemRole
            // 
            this._itemRole.BackColor = System.Drawing.Color.Transparent;
            this._itemRole.Dock = System.Windows.Forms.DockStyle.Fill;
            this._itemRole.Icon = FontAwesome.Sharp.IconChar.Briefcase;
            this._itemRole.IconBackground = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(243)))), ((int)(((byte)(251)))));
            this._itemRole.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this._itemRole.LabelText = "Vai trò";
            this._itemRole.Location = new System.Drawing.Point(0, 96);
            this._itemRole.Margin = new System.Windows.Forms.Padding(0, 0, 16, 8);
            this._itemRole.MinimumSize = new System.Drawing.Size(160, 48);
            this._itemRole.Name = "_itemRole";
            this._itemRole.Size = new System.Drawing.Size(431, 88);
            this._itemRole.TabIndex = 2;
            this._itemRole.ValueText = "Nhân viên";
            // 
            // _itemPhone
            // 
            this._itemPhone.BackColor = System.Drawing.Color.Transparent;
            this._itemPhone.Dock = System.Windows.Forms.DockStyle.Fill;
            this._itemPhone.Icon = FontAwesome.Sharp.IconChar.Phone;
            this._itemPhone.IconBackground = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(243)))), ((int)(((byte)(251)))));
            this._itemPhone.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this._itemPhone.LabelText = "Số điện thoại";
            this._itemPhone.Location = new System.Drawing.Point(447, 96);
            this._itemPhone.Margin = new System.Windows.Forms.Padding(0, 0, 16, 8);
            this._itemPhone.MinimumSize = new System.Drawing.Size(160, 48);
            this._itemPhone.Name = "_itemPhone";
            this._itemPhone.Size = new System.Drawing.Size(431, 88);
            this._itemPhone.TabIndex = 3;
            this._itemPhone.ValueText = "0901 234 567";
            // 
            // _itemCreatedAt
            // 
            this._itemCreatedAt.BackColor = System.Drawing.Color.Transparent;
            this._itemCreatedAt.Dock = System.Windows.Forms.DockStyle.Fill;
            this._itemCreatedAt.Icon = FontAwesome.Sharp.IconChar.Calendar;
            this._itemCreatedAt.IconBackground = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(243)))), ((int)(((byte)(251)))));
            this._itemCreatedAt.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this._itemCreatedAt.LabelText = "Ngày tạo tài khoản";
            this._itemCreatedAt.Location = new System.Drawing.Point(0, 192);
            this._itemCreatedAt.Margin = new System.Windows.Forms.Padding(0, 0, 16, 8);
            this._itemCreatedAt.MinimumSize = new System.Drawing.Size(160, 48);
            this._itemCreatedAt.Name = "_itemCreatedAt";
            this._itemCreatedAt.Size = new System.Drawing.Size(431, 88);
            this._itemCreatedAt.TabIndex = 4;
            this._itemCreatedAt.ValueText = "15/01/2025";
            // 
            // _itemAccountStatus
            // 
            this._itemAccountStatus.BackColor = System.Drawing.Color.Transparent;
            this._itemAccountStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this._itemAccountStatus.Icon = FontAwesome.Sharp.IconChar.CircleCheck;
            this._itemAccountStatus.IconBackground = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(253)))), ((int)(((byte)(245)))));
            this._itemAccountStatus.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this._itemAccountStatus.LabelText = "Trạng thái tài khoản";
            this._itemAccountStatus.Location = new System.Drawing.Point(447, 192);
            this._itemAccountStatus.Margin = new System.Windows.Forms.Padding(0, 0, 16, 8);
            this._itemAccountStatus.MinimumSize = new System.Drawing.Size(160, 48);
            this._itemAccountStatus.Name = "_itemAccountStatus";
            this._itemAccountStatus.Size = new System.Drawing.Size(431, 88);
            this._itemAccountStatus.TabIndex = 5;
            this._itemAccountStatus.ValueText = "Đang hoạt động";
            // 
            // _itemGender
            // 
            this._itemGender.BackColor = System.Drawing.Color.Transparent;
            this._itemGender.Dock = System.Windows.Forms.DockStyle.Fill;
            this._itemGender.Icon = FontAwesome.Sharp.IconChar.VenusMars;
            this._itemGender.IconBackground = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(243)))), ((int)(((byte)(251)))));
            this._itemGender.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this._itemGender.LabelText = "Giới tính";
            this._itemGender.Location = new System.Drawing.Point(0, 288);
            this._itemGender.Margin = new System.Windows.Forms.Padding(0, 0, 16, 8);
            this._itemGender.MinimumSize = new System.Drawing.Size(160, 48);
            this._itemGender.Name = "_itemGender";
            this._itemGender.Size = new System.Drawing.Size(431, 88);
            this._itemGender.TabIndex = 6;
            this._itemGender.ValueText = "Nam";
            // 
            // _itemSalary
            // 
            this._itemSalary.BackColor = System.Drawing.Color.Transparent;
            this._itemSalary.Dock = System.Windows.Forms.DockStyle.Fill;
            this._itemSalary.Icon = FontAwesome.Sharp.IconChar.MoneyBillWave;
            this._itemSalary.IconBackground = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(243)))), ((int)(((byte)(251)))));
            this._itemSalary.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this._itemSalary.LabelText = "Lương";
            this._itemSalary.Location = new System.Drawing.Point(447, 288);
            this._itemSalary.Margin = new System.Windows.Forms.Padding(0, 0, 16, 8);
            this._itemSalary.MinimumSize = new System.Drawing.Size(160, 48);
            this._itemSalary.Name = "_itemSalary";
            this._itemSalary.Size = new System.Drawing.Size(431, 88);
            this._itemSalary.TabIndex = 7;
            this._itemSalary.ValueText = "12,000,000";
            // 
            // _designerSecurityGrid
            // 
            this._designerSecurityGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._designerSecurityGrid.BackColor = System.Drawing.Color.Transparent;
            this._designerSecurityGrid.ColumnCount = 2;
            this._designerSecurityGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._designerSecurityGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._designerSecurityGrid.Controls.Add(this._itemLockStatus, 0, 0);
            this._designerSecurityGrid.Controls.Add(this._itemFailedLogin, 1, 0);
            this._designerSecurityGrid.Location = new System.Drawing.Point(24, 56);
            this._designerSecurityGrid.Name = "_designerSecurityGrid";
            this._designerSecurityGrid.RowCount = 1;
            this._designerSecurityGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this._designerSecurityGrid.Size = new System.Drawing.Size(894, 96);
            this._designerSecurityGrid.TabIndex = 3;
            this._designerSecurityGrid.Visible = false;
            // 
            // _itemLockStatus
            // 
            this._itemLockStatus.BackColor = System.Drawing.Color.Transparent;
            this._itemLockStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this._itemLockStatus.Icon = FontAwesome.Sharp.IconChar.LockOpen;
            this._itemLockStatus.IconBackground = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(253)))), ((int)(((byte)(245)))));
            this._itemLockStatus.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this._itemLockStatus.LabelText = "Trạng thái khóa";
            this._itemLockStatus.Location = new System.Drawing.Point(0, 0);
            this._itemLockStatus.Margin = new System.Windows.Forms.Padding(0, 0, 16, 8);
            this._itemLockStatus.MinimumSize = new System.Drawing.Size(160, 48);
            this._itemLockStatus.Name = "_itemLockStatus";
            this._itemLockStatus.Size = new System.Drawing.Size(431, 88);
            this._itemLockStatus.TabIndex = 0;
            this._itemLockStatus.ValueText = "Bình thường";
            // 
            // _itemFailedLogin
            // 
            this._itemFailedLogin.BackColor = System.Drawing.Color.Transparent;
            this._itemFailedLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this._itemFailedLogin.Icon = FontAwesome.Sharp.IconChar.ExclamationTriangle;
            this._itemFailedLogin.IconBackground = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(251)))), ((int)(((byte)(235)))));
            this._itemFailedLogin.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(83)))), ((int)(((byte)(9)))));
            this._itemFailedLogin.LabelText = "Số lần đăng nhập sai";
            this._itemFailedLogin.Location = new System.Drawing.Point(447, 0);
            this._itemFailedLogin.Margin = new System.Windows.Forms.Padding(0, 0, 16, 8);
            this._itemFailedLogin.MinimumSize = new System.Drawing.Size(160, 48);
            this._itemFailedLogin.Name = "_itemFailedLogin";
            this._itemFailedLogin.Size = new System.Drawing.Size(431, 88);
            this._itemFailedLogin.TabIndex = 1;
            this._itemFailedLogin.ValueText = "0";
            // 
            // _tabAccount
            // 
            this._tabAccount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(243)))), ((int)(((byte)(251)))));
            this._tabAccount.FlatAppearance.BorderSize = 0;
            this._tabAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._tabAccount.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this._tabAccount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this._tabAccount.Location = new System.Drawing.Point(24, 12);
            this._tabAccount.Name = "_tabAccount";
            this._tabAccount.Size = new System.Drawing.Size(163, 38);
            this._tabAccount.TabIndex = 0;
            this._tabAccount.Text = "Tài khoản";
            this._tabAccount.UseVisualStyleBackColor = false;
            this._tabAccount.Click += new System.EventHandler(this._tabAccount_Click);
            // 
            // _tabSecurity
            // 
            this._tabSecurity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this._tabSecurity.FlatAppearance.BorderSize = 0;
            this._tabSecurity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._tabSecurity.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this._tabSecurity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._tabSecurity.Location = new System.Drawing.Point(193, 12);
            this._tabSecurity.Name = "_tabSecurity";
            this._tabSecurity.Size = new System.Drawing.Size(129, 38);
            this._tabSecurity.TabIndex = 1;
            this._tabSecurity.Text = "Bảo mật";
            this._tabSecurity.UseVisualStyleBackColor = false;
            this._tabSecurity.Click += new System.EventHandler(this._tabSecurity_Click);
            // 
            // _designerSidebar
            // 
            this._designerSidebar.BackColor = System.Drawing.Color.White;
            this._designerSidebar.Controls.Add(this._designerAvatarCircle);
            this._designerSidebar.Controls.Add(this._lblAvatarName);
            this._designerSidebar.Controls.Add(this._lblAvatarSubtitle);
            this._designerSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this._designerSidebar.Location = new System.Drawing.Point(0, 0);
            this._designerSidebar.Name = "_designerSidebar";
            this._designerSidebar.Padding = new System.Windows.Forms.Padding(8, 24, 8, 8);
            this._designerSidebar.Size = new System.Drawing.Size(208, 524);
            this._designerSidebar.TabIndex = 0;
            // 
            // _designerAvatarCircle
            // 
            this._designerAvatarCircle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this._designerAvatarCircle.Controls.Add(this._lblAvatarInitial);
            this._designerAvatarCircle.Location = new System.Drawing.Point(56, 32);
            this._designerAvatarCircle.Name = "_designerAvatarCircle";
            this._designerAvatarCircle.Size = new System.Drawing.Size(96, 96);
            this._designerAvatarCircle.TabIndex = 0;
            // 
            // _lblAvatarInitial
            // 
            this._lblAvatarInitial.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lblAvatarInitial.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this._lblAvatarInitial.ForeColor = System.Drawing.Color.White;
            this._lblAvatarInitial.Location = new System.Drawing.Point(0, 0);
            this._lblAvatarInitial.Name = "_lblAvatarInitial";
            this._lblAvatarInitial.Size = new System.Drawing.Size(96, 96);
            this._lblAvatarInitial.TabIndex = 0;
            this._lblAvatarInitial.Text = "A";
            this._lblAvatarInitial.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _lblAvatarName
            // 
            this._lblAvatarName.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this._lblAvatarName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._lblAvatarName.Location = new System.Drawing.Point(8, 144);
            this._lblAvatarName.Name = "_lblAvatarName";
            this._lblAvatarName.Size = new System.Drawing.Size(200, 47);
            this._lblAvatarName.TabIndex = 1;
            this._lblAvatarName.Text = "Nguyễn Văn A";
            this._lblAvatarName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _lblAvatarSubtitle
            // 
            this._lblAvatarSubtitle.Font = new System.Drawing.Font("Segoe UI", 12F);
            this._lblAvatarSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._lblAvatarSubtitle.Location = new System.Drawing.Point(8, 201);
            this._lblAvatarSubtitle.Name = "_lblAvatarSubtitle";
            this._lblAvatarSubtitle.Size = new System.Drawing.Size(192, 39);
            this._lblAvatarSubtitle.TabIndex = 2;
            this._lblAvatarSubtitle.Text = "@nguyenvana";
            this._lblAvatarSubtitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // _designerFooter
            // 
            this._designerFooter.BackColor = System.Drawing.Color.White;
            this._designerFooter.Controls.Add(this._btnFooterClose);
            this._designerFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._designerFooter.Location = new System.Drawing.Point(0, 588);
            this._designerFooter.Name = "_designerFooter";
            this._designerFooter.Size = new System.Drawing.Size(1150, 72);
            this._designerFooter.TabIndex = 2;
            // 
            // _btnFooterClose
            // 
            this._btnFooterClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnFooterClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this._btnFooterClose.FlatAppearance.BorderSize = 0;
            this._btnFooterClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnFooterClose.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this._btnFooterClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._btnFooterClose.Location = new System.Drawing.Point(1020, 16);
            this._btnFooterClose.Name = "_btnFooterClose";
            this._btnFooterClose.Size = new System.Drawing.Size(112, 40);
            this._btnFooterClose.TabIndex = 0;
            this._btnFooterClose.Text = "Đóng";
            this._btnFooterClose.UseVisualStyleBackColor = false;
            // 
            // frmUserAccountDetail
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1150, 660);
            this.Controls.Add(this._designerBody);
            this.Controls.Add(this._designerFooter);
            this.Controls.Add(this._designerHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(1150, 660);
            this.Name = "frmUserAccountDetail";
            this.ShowInTaskbar = false;
            this.Text = "Chi tiết tài khoản";
            this._designerHeader.ResumeLayout(false);
            this._designerBody.ResumeLayout(false);
            this._designerContent.ResumeLayout(false);
            this._designerAccountGrid.ResumeLayout(false);
            this._designerSecurityGrid.ResumeLayout(false);
            this._designerSidebar.ResumeLayout(false);
            this._designerAvatarCircle.ResumeLayout(false);
            this._designerFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
