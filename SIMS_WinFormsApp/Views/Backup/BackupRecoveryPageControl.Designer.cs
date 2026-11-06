namespace SIMS_WinFormsApp.Views.Backup
{
    partial class BackupRecoveryPageControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this._rootLayout = new System.Windows.Forms.TableLayoutPanel();
            this._headerPanel = new System.Windows.Forms.Panel();
            this._iconCircle = new System.Windows.Forms.Panel();
            this._bannerIcon = new FontAwesome.Sharp.IconPictureBox();
            this._titleLabel = new System.Windows.Forms.Label();
            this._subtitleLabel = new System.Windows.Forms.Label();
            this._actionsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this._restoreFileButton = new SIMS_WinFormsApp.UI.Controls.PrimaryButton();
            this._restoreFileIcon = new FontAwesome.Sharp.IconPictureBox();
            this._restoreFileLabel = new System.Windows.Forms.Label();
            this._backupNowButton = new SIMS_WinFormsApp.UI.Controls.PrimaryButton();
            this._backupNowIcon = new FontAwesome.Sharp.IconPictureBox();
            this._backupNowLabel = new System.Windows.Forms.Label();
            this._cardPanel = new System.Windows.Forms.Panel();
            this._listLayout = new System.Windows.Forms.TableLayoutPanel();
            this._filterHost = new System.Windows.Forms.Panel();
            this._searchFrame = new System.Windows.Forms.Panel();
            this._searchIcon = new System.Windows.Forms.Label();
            this._searchTextBox = new System.Windows.Forms.TextBox();
            this._dateRangePanel = new System.Windows.Forms.Panel();
            this._todayButton = new System.Windows.Forms.Button();
            this._clearRangeButton = new System.Windows.Forms.Button();
            this._fromDateCheckBox = new System.Windows.Forms.CheckBox();
            this._fromDatePicker = new System.Windows.Forms.DateTimePicker();
            this._toDateCheckBox = new System.Windows.Forms.CheckBox();
            this._toDatePicker = new System.Windows.Forms.DateTimePicker();
            this._gridHolder = new System.Windows.Forms.Panel();
            this._grid = new System.Windows.Forms.DataGridView();
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Strategy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreatedAt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SizeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Local = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Cloud = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Actions = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._paginationControl = new SIMS_WinFormsApp.Views.Pagination.PaginationControl();
            this._rootLayout.SuspendLayout();
            this._headerPanel.SuspendLayout();
            this._iconCircle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._bannerIcon)).BeginInit();
            this._actionsPanel.SuspendLayout();
            this._restoreFileButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._restoreFileIcon)).BeginInit();
            this._backupNowButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._backupNowIcon)).BeginInit();
            this._cardPanel.SuspendLayout();
            this._listLayout.SuspendLayout();
            this._filterHost.SuspendLayout();
            this._searchFrame.SuspendLayout();
            this._dateRangePanel.SuspendLayout();
            this._gridHolder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._grid)).BeginInit();
            this.SuspendLayout();
            // 
            // _rootLayout
            // 
            this._rootLayout.BackColor = System.Drawing.Color.Transparent;
            this._rootLayout.ColumnCount = 1;
            this._rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._rootLayout.Controls.Add(this._headerPanel, 0, 0);
            this._rootLayout.Controls.Add(this._cardPanel, 0, 1);
            this._rootLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._rootLayout.Location = new System.Drawing.Point(16, 16);
            this._rootLayout.Name = "_rootLayout";
            this._rootLayout.RowCount = 2;
            this._rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 112F));
            this._rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._rootLayout.Size = new System.Drawing.Size(1490, 844);
            this._rootLayout.TabIndex = 0;
            // 
            // _headerPanel
            // 
            this._headerPanel.BackColor = System.Drawing.Color.White;
            this._headerPanel.Controls.Add(this._iconCircle);
            this._headerPanel.Controls.Add(this._titleLabel);
            this._headerPanel.Controls.Add(this._subtitleLabel);
            this._headerPanel.Controls.Add(this._actionsPanel);
            this._headerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._headerPanel.Location = new System.Drawing.Point(0, 0);
            this._headerPanel.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this._headerPanel.Name = "_headerPanel";
            this._headerPanel.Size = new System.Drawing.Size(1490, 100);
            this._headerPanel.TabIndex = 0;
            this._headerPanel.Resize += new System.EventHandler(this.HeaderPanel_Resize);
            // 
            // _iconCircle
            // 
            this._iconCircle.BackColor = System.Drawing.Color.Transparent;
            this._iconCircle.Controls.Add(this._bannerIcon);
            this._iconCircle.Location = new System.Drawing.Point(20, 16);
            this._iconCircle.Name = "_iconCircle";
            this._iconCircle.Size = new System.Drawing.Size(48, 48);
            this._iconCircle.TabIndex = 0;
            this._iconCircle.Paint += new System.Windows.Forms.PaintEventHandler(this.IconCircle_Paint);
            // 
            // _bannerIcon
            // 
            this._bannerIcon.BackColor = System.Drawing.Color.Transparent;
            this._bannerIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this._bannerIcon.IconChar = FontAwesome.Sharp.IconChar.ShieldAlt;
            this._bannerIcon.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this._bannerIcon.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._bannerIcon.IconSize = 28;
            this._bannerIcon.Location = new System.Drawing.Point(10, 10);
            this._bannerIcon.Name = "_bannerIcon";
            this._bannerIcon.Size = new System.Drawing.Size(28, 28);
            this._bannerIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._bannerIcon.TabIndex = 0;
            this._bannerIcon.TabStop = false;
            // 
            // _titleLabel
            // 
            this._titleLabel.AutoSize = true;
            this._titleLabel.BackColor = System.Drawing.Color.Transparent;
            this._titleLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this._titleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._titleLabel.Location = new System.Drawing.Point(84, 13);
            this._titleLabel.Name = "_titleLabel";
            this._titleLabel.Size = new System.Drawing.Size(278, 37);
            this._titleLabel.TabIndex = 1;
            this._titleLabel.Text = "Sao lưu & Khôi phục";
            this._titleLabel.UseMnemonic = false;
            this._titleLabel.Click += new System.EventHandler(this._titleLabel_Click);
            // 
            // _subtitleLabel
            // 
            this._subtitleLabel.AutoSize = true;
            this._subtitleLabel.BackColor = System.Drawing.Color.Transparent;
            this._subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 12F);
            this._subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._subtitleLabel.Location = new System.Drawing.Point(84, 47);
            this._subtitleLabel.Name = "_subtitleLabel";
            this._subtitleLabel.Size = new System.Drawing.Size(491, 28);
            this._subtitleLabel.TabIndex = 2;
            this._subtitleLabel.Text = "Bảo vệ dữ liệu khi hệ thống gặp sự cố hoặc bị tấn công";
            // 
            // _actionsPanel
            // 
            this._actionsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._actionsPanel.BackColor = System.Drawing.Color.Transparent;
            this._actionsPanel.Controls.Add(this._restoreFileButton);
            this._actionsPanel.Controls.Add(this._backupNowButton);
            this._actionsPanel.Location = new System.Drawing.Point(1290, 0);
            this._actionsPanel.Margin = new System.Windows.Forms.Padding(0);
            this._actionsPanel.Name = "_actionsPanel";
            this._actionsPanel.Padding = new System.Windows.Forms.Padding(0, 19, 0, 0);
            this._actionsPanel.Size = new System.Drawing.Size(360, 80);
            this._actionsPanel.TabIndex = 3;
            this._actionsPanel.WrapContents = false;
            // 
            // _restoreFileButton
            // 
            this._restoreFileButton.BackColor = System.Drawing.Color.Transparent;
            this._restoreFileButton.Controls.Add(this._restoreFileIcon);
            this._restoreFileButton.Controls.Add(this._restoreFileLabel);
            this._restoreFileButton.CornerRadius = 10;
            this._restoreFileButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this._restoreFileButton.CustomAccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(83)))), ((int)(((byte)(9)))));
            this._restoreFileButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._restoreFileButton.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this._restoreFileButton.ForeColor = System.Drawing.Color.White;
            this._restoreFileButton.Icon = null;
            this._restoreFileButton.IconSize = 16;
            this._restoreFileButton.IsPrimary = false;
            this._restoreFileButton.Location = new System.Drawing.Point(0, 19);
            this._restoreFileButton.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this._restoreFileButton.MinimumSize = new System.Drawing.Size(120, 42);
            this._restoreFileButton.Name = "_restoreFileButton";
            this._restoreFileButton.Size = new System.Drawing.Size(192, 42);
            this._restoreFileButton.TabIndex = 0;
            this._restoreFileButton.UseVisualStyleBackColor = false;
            this._restoreFileButton.Click += new System.EventHandler(this.RestoreFileButton_Click);
            // 
            // _restoreFileIcon
            // 
            this._restoreFileIcon.BackColor = System.Drawing.Color.Transparent;
            this._restoreFileIcon.Cursor = System.Windows.Forms.Cursors.Hand;
            this._restoreFileIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this._restoreFileIcon.IconChar = FontAwesome.Sharp.IconChar.Upload;
            this._restoreFileIcon.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this._restoreFileIcon.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._restoreFileIcon.IconSize = 18;
            this._restoreFileIcon.Location = new System.Drawing.Point(16, 12);
            this._restoreFileIcon.Name = "_restoreFileIcon";
            this._restoreFileIcon.Size = new System.Drawing.Size(18, 18);
            this._restoreFileIcon.TabIndex = 0;
            this._restoreFileIcon.TabStop = false;
            this._restoreFileIcon.Click += new System.EventHandler(this.RestoreFileButton_Click);
            // 
            // _restoreFileLabel
            // 
            this._restoreFileLabel.BackColor = System.Drawing.Color.Transparent;
            this._restoreFileLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._restoreFileLabel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this._restoreFileLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this._restoreFileLabel.Location = new System.Drawing.Point(0, 0);
            this._restoreFileLabel.Name = "_restoreFileLabel";
            this._restoreFileLabel.Padding = new System.Windows.Forms.Padding(42, 0, 12, 0);
            this._restoreFileLabel.Size = new System.Drawing.Size(192, 42);
            this._restoreFileLabel.TabIndex = 1;
            this._restoreFileLabel.Text = "Khôi phục từ file...";
            this._restoreFileLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._restoreFileLabel.Click += new System.EventHandler(this.RestoreFileButton_Click);
            // 
            // _backupNowButton
            // 
            this._backupNowButton.BackColor = System.Drawing.Color.Transparent;
            this._backupNowButton.Controls.Add(this._backupNowIcon);
            this._backupNowButton.Controls.Add(this._backupNowLabel);
            this._backupNowButton.CornerRadius = 10;
            this._backupNowButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this._backupNowButton.CustomAccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this._backupNowButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._backupNowButton.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this._backupNowButton.ForeColor = System.Drawing.Color.White;
            this._backupNowButton.Icon = null;
            this._backupNowButton.IconSize = 16;
            this._backupNowButton.IsPrimary = true;
            this._backupNowButton.Location = new System.Drawing.Point(204, 19);
            this._backupNowButton.Margin = new System.Windows.Forms.Padding(0);
            this._backupNowButton.MinimumSize = new System.Drawing.Size(120, 42);
            this._backupNowButton.Name = "_backupNowButton";
            this._backupNowButton.Size = new System.Drawing.Size(150, 42);
            this._backupNowButton.TabIndex = 1;
            this._backupNowButton.UseVisualStyleBackColor = false;
            this._backupNowButton.Click += new System.EventHandler(this.BackupNowButton_Click);
            // 
            // _backupNowIcon
            // 
            this._backupNowIcon.BackColor = System.Drawing.Color.Transparent;
            this._backupNowIcon.Cursor = System.Windows.Forms.Cursors.Hand;
            this._backupNowIcon.IconChar = FontAwesome.Sharp.IconChar.Download;
            this._backupNowIcon.IconColor = System.Drawing.Color.White;
            this._backupNowIcon.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._backupNowIcon.IconSize = 18;
            this._backupNowIcon.Location = new System.Drawing.Point(16, 12);
            this._backupNowIcon.Name = "_backupNowIcon";
            this._backupNowIcon.Size = new System.Drawing.Size(18, 18);
            this._backupNowIcon.TabIndex = 0;
            this._backupNowIcon.TabStop = false;
            this._backupNowIcon.Click += new System.EventHandler(this.BackupNowButton_Click);
            // 
            // _backupNowLabel
            // 
            this._backupNowLabel.BackColor = System.Drawing.Color.Transparent;
            this._backupNowLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._backupNowLabel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this._backupNowLabel.ForeColor = System.Drawing.Color.White;
            this._backupNowLabel.Location = new System.Drawing.Point(0, 0);
            this._backupNowLabel.Name = "_backupNowLabel";
            this._backupNowLabel.Padding = new System.Windows.Forms.Padding(42, 0, 12, 0);
            this._backupNowLabel.Size = new System.Drawing.Size(150, 42);
            this._backupNowLabel.TabIndex = 1;
            this._backupNowLabel.Text = "Sao lưu ngay";
            this._backupNowLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._backupNowLabel.Click += new System.EventHandler(this.BackupNowButton_Click);
            // 
            // _cardPanel
            // 
            this._cardPanel.BackColor = System.Drawing.Color.Transparent;
            this._cardPanel.Controls.Add(this._listLayout);
            this._cardPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._cardPanel.Location = new System.Drawing.Point(0, 128);
            this._cardPanel.Margin = new System.Windows.Forms.Padding(0, 16, 0, 0);
            this._cardPanel.Name = "_cardPanel";
            this._cardPanel.Padding = new System.Windows.Forms.Padding(20);
            this._cardPanel.Size = new System.Drawing.Size(1490, 716);
            this._cardPanel.TabIndex = 1;
            this._cardPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.CardPanel_Paint);
            // 
            // _listLayout
            // 
            this._listLayout.BackColor = System.Drawing.Color.Transparent;
            this._listLayout.ColumnCount = 1;
            this._listLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._listLayout.Controls.Add(this._filterHost, 0, 0);
            this._listLayout.Controls.Add(this._gridHolder, 0, 1);
            this._listLayout.Controls.Add(this._paginationControl, 0, 2);
            this._listLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._listLayout.Location = new System.Drawing.Point(20, 20);
            this._listLayout.Name = "_listLayout";
            this._listLayout.RowCount = 3;
            this._listLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 148F));
            this._listLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._listLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this._listLayout.Size = new System.Drawing.Size(1450, 676);
            this._listLayout.TabIndex = 0;
            // 
            // _filterHost
            // 
            this._filterHost.BackColor = System.Drawing.Color.Transparent;
            this._filterHost.Controls.Add(this._searchFrame);
            this._filterHost.Controls.Add(this._dateRangePanel);
            this._filterHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this._filterHost.Location = new System.Drawing.Point(3, 3);
            this._filterHost.Name = "_filterHost";
            this._filterHost.Padding = new System.Windows.Forms.Padding(4, 2, 4, 0);
            this._filterHost.Size = new System.Drawing.Size(1444, 142);
            this._filterHost.TabIndex = 0;
            this._filterHost.Resize += new System.EventHandler(this.FilterHost_Resize);
            // 
            // _searchFrame
            // 
            this._searchFrame.BackColor = System.Drawing.Color.White;
            this._searchFrame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._searchFrame.Controls.Add(this._searchIcon);
            this._searchFrame.Controls.Add(this._searchTextBox);
            this._searchFrame.Location = new System.Drawing.Point(0, 0);
            this._searchFrame.Name = "_searchFrame";
            this._searchFrame.Size = new System.Drawing.Size(200, 100);
            this._searchFrame.TabIndex = 0;
            this._searchFrame.Resize += new System.EventHandler(this.SearchFrame_Resize);
            // 
            // _searchIcon
            // 
            this._searchIcon.BackColor = System.Drawing.Color.Transparent;
            this._searchIcon.Font = new System.Drawing.Font("Segoe UI Symbol", 16F);
            this._searchIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this._searchIcon.Location = new System.Drawing.Point(0, 0);
            this._searchIcon.Name = "_searchIcon";
            this._searchIcon.Size = new System.Drawing.Size(32, 38);
            this._searchIcon.TabIndex = 0;
            this._searchIcon.Text = "⌕";
            this._searchIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _searchTextBox
            // 
            this._searchTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this._searchTextBox.BackColor = System.Drawing.Color.White;
            this._searchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._searchTextBox.Font = new System.Drawing.Font("Segoe UI", 14F);
            this._searchTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this._searchTextBox.Location = new System.Drawing.Point(38, 6);
            this._searchTextBox.Name = "_searchTextBox";
            this._searchTextBox.Size = new System.Drawing.Size(96, 32);
            this._searchTextBox.TabIndex = 1;
            // 
            // _dateRangePanel
            // 
            this._dateRangePanel.BackColor = System.Drawing.Color.Transparent;
            this._dateRangePanel.Controls.Add(this._todayButton);
            this._dateRangePanel.Controls.Add(this._clearRangeButton);
            this._dateRangePanel.Controls.Add(this._fromDateCheckBox);
            this._dateRangePanel.Controls.Add(this._fromDatePicker);
            this._dateRangePanel.Controls.Add(this._toDateCheckBox);
            this._dateRangePanel.Controls.Add(this._toDatePicker);
            this._dateRangePanel.Location = new System.Drawing.Point(0, 0);
            this._dateRangePanel.Name = "_dateRangePanel";
            this._dateRangePanel.Size = new System.Drawing.Size(200, 100);
            this._dateRangePanel.TabIndex = 1;
            // 
            // _todayButton
            // 
            this._todayButton.BackColor = System.Drawing.Color.White;
            this._todayButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this._todayButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._todayButton.Font = new System.Drawing.Font("Segoe UI", 12F);
            this._todayButton.Location = new System.Drawing.Point(0, 0);
            this._todayButton.Margin = new System.Windows.Forms.Padding(0);
            this._todayButton.Name = "_todayButton";
            this._todayButton.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this._todayButton.Size = new System.Drawing.Size(75, 23);
            this._todayButton.TabIndex = 0;
            this._todayButton.Tag = SIMS_WinFormsApp.Models.Enums.DateRangePreset.Today;
            this._todayButton.Text = "Hôm nay";
            this._todayButton.UseVisualStyleBackColor = false;
            this._todayButton.Click += new System.EventHandler(this.TodayButton_Click);
            // 
            // _clearRangeButton
            // 
            this._clearRangeButton.BackColor = System.Drawing.Color.White;
            this._clearRangeButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this._clearRangeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._clearRangeButton.Font = new System.Drawing.Font("Segoe UI", 12F);
            this._clearRangeButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._clearRangeButton.Location = new System.Drawing.Point(0, 0);
            this._clearRangeButton.Name = "_clearRangeButton";
            this._clearRangeButton.Size = new System.Drawing.Size(75, 30);
            this._clearRangeButton.TabIndex = 1;
            this._clearRangeButton.Text = "Xóa lọc";
            this._clearRangeButton.UseVisualStyleBackColor = false;
            this._clearRangeButton.Click += new System.EventHandler(this.ClearRangeButton_Click);
            // 
            // _fromDateCheckBox
            // 
            this._fromDateCheckBox.AutoSize = true;
            this._fromDateCheckBox.BackColor = System.Drawing.Color.Transparent;
            this._fromDateCheckBox.Font = new System.Drawing.Font("Segoe UI", 12F);
            this._fromDateCheckBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._fromDateCheckBox.Location = new System.Drawing.Point(0, 0);
            this._fromDateCheckBox.Name = "_fromDateCheckBox";
            this._fromDateCheckBox.Size = new System.Drawing.Size(104, 32);
            this._fromDateCheckBox.TabIndex = 2;
            this._fromDateCheckBox.Text = "Từ ngày";
            this._fromDateCheckBox.UseVisualStyleBackColor = false;
            // 
            // _fromDatePicker
            // 
            this._fromDatePicker.CustomFormat = "dd/MM/yyyy";
            this._fromDatePicker.Font = new System.Drawing.Font("Segoe UI", 12F);
            this._fromDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._fromDatePicker.Location = new System.Drawing.Point(0, 0);
            this._fromDatePicker.Name = "_fromDatePicker";
            this._fromDatePicker.Size = new System.Drawing.Size(150, 34);
            this._fromDatePicker.TabIndex = 3;
            // 
            // _toDateCheckBox
            // 
            this._toDateCheckBox.AutoSize = true;
            this._toDateCheckBox.BackColor = System.Drawing.Color.Transparent;
            this._toDateCheckBox.Font = new System.Drawing.Font("Segoe UI", 12F);
            this._toDateCheckBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._toDateCheckBox.Location = new System.Drawing.Point(0, 0);
            this._toDateCheckBox.Name = "_toDateCheckBox";
            this._toDateCheckBox.Size = new System.Drawing.Size(117, 32);
            this._toDateCheckBox.TabIndex = 4;
            this._toDateCheckBox.Text = "Đến ngày";
            this._toDateCheckBox.UseVisualStyleBackColor = false;
            // 
            // _toDatePicker
            // 
            this._toDatePicker.Location = new System.Drawing.Point(0, 0);
            this._toDatePicker.Name = "_toDatePicker";
            this._toDatePicker.Size = new System.Drawing.Size(200, 22);
            this._toDatePicker.TabIndex = 5;
            // 
            // _gridHolder
            // 
            this._gridHolder.Controls.Add(this._grid);
            this._gridHolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridHolder.Location = new System.Drawing.Point(0, 158);
            this._gridHolder.Margin = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this._gridHolder.Name = "_gridHolder";
            this._gridHolder.Size = new System.Drawing.Size(1450, 458);
            this._gridHolder.TabIndex = 1;
            // 
            // _grid
            // 
            this._grid.AllowUserToAddRows = false;
            this._grid.AllowUserToDeleteRows = false;
            this._grid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this._grid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this._grid.BackgroundColor = System.Drawing.Color.White;
            this._grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._grid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this._grid.ColumnHeadersHeight = 50;
            this._grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FileName,
            this.Strategy,
            this.CreatedAt,
            this.SizeColumn,
            this.Local,
            this.Cloud,
            this.Actions});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 13F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            dataGridViewCellStyle9.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(233)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._grid.DefaultCellStyle = dataGridViewCellStyle9;
            this._grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grid.EnableHeadersVisualStyles = false;
            this._grid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this._grid.Location = new System.Drawing.Point(0, 0);
            this._grid.MultiSelect = false;
            this._grid.Name = "_grid";
            this._grid.ReadOnly = true;
            this._grid.RowHeadersVisible = false;
            this._grid.RowHeadersWidth = 51;
            this._grid.RowTemplate.Height = 52;
            this._grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._grid.Size = new System.Drawing.Size(1450, 458);
            this._grid.TabIndex = 0;
            this._grid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this._grid_CellContentClick);
            this._grid.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.OnActionMouseClick);
            this._grid.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnActionMouseLeave);
            this._grid.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.OnActionMouseMove);
            this._grid.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.PaintActionCell);
            this._grid.CellToolTipTextNeeded += new System.Windows.Forms.DataGridViewCellToolTipTextNeededEventHandler(this.OnCellToolTip);
            // 
            // FileName
            // 
            this.FileName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.FileName.DefaultCellStyle = dataGridViewCellStyle3;
            this.FileName.HeaderText = "Tên file";
            this.FileName.MinimumWidth = 140;
            this.FileName.Name = "FileName";
            this.FileName.ReadOnly = true;
            this.FileName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Strategy
            // 
            this.Strategy.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Strategy.DefaultCellStyle = dataGridViewCellStyle4;
            this.Strategy.HeaderText = "Chiến lược";
            this.Strategy.MinimumWidth = 110;
            this.Strategy.Name = "Strategy";
            this.Strategy.ReadOnly = true;
            this.Strategy.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CreatedAt
            // 
            this.CreatedAt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.CreatedAt.DefaultCellStyle = dataGridViewCellStyle5;
            this.CreatedAt.HeaderText = "Thời gian";
            this.CreatedAt.MinimumWidth = 140;
            this.CreatedAt.Name = "CreatedAt";
            this.CreatedAt.ReadOnly = true;
            this.CreatedAt.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Size
            // 
            this.SizeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.SizeColumn.DefaultCellStyle = dataGridViewCellStyle6;
            this.SizeColumn.HeaderText = "Dung lượng";
            this.SizeColumn.MinimumWidth = 100;
            this.SizeColumn.Name = "Size";
            this.SizeColumn.ReadOnly = true;
            this.SizeColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Local
            // 
            this.Local.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this.Local.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Local.DefaultCellStyle = dataGridViewCellStyle7;
            this.Local.HeaderText = "Local";
            this.Local.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this.Local.MinimumWidth = 100;
            this.Local.Name = "Local";
            this.Local.ReadOnly = true;
            this.Local.TrackVisitedState = false;
            this.Local.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            // 
            // Cloud
            // 
            this.Cloud.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this.Cloud.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Cloud.DefaultCellStyle = dataGridViewCellStyle8;
            this.Cloud.HeaderText = "Cloud";
            this.Cloud.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this.Cloud.MinimumWidth = 110;
            this.Cloud.Name = "Cloud";
            this.Cloud.ReadOnly = true;
            this.Cloud.TrackVisitedState = false;
            this.Cloud.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            // 
            // Actions
            // 
            this.Actions.HeaderText = "";
            this.Actions.MinimumWidth = 88;
            this.Actions.Name = "Actions";
            this.Actions.ReadOnly = true;
            this.Actions.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Actions.Width = 88;
            // 
            // _paginationControl
            // 
            this._paginationControl.BackColor = System.Drawing.Color.White;
            this._paginationControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._paginationControl.Location = new System.Drawing.Point(3, 629);
            this._paginationControl.Name = "_paginationControl";
            this._paginationControl.Size = new System.Drawing.Size(1444, 44);
            this._paginationControl.TabIndex = 2;
            // 
            // BackupRecoveryPageControl
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.Controls.Add(this._rootLayout);
            this.Name = "BackupRecoveryPageControl";
            this.Padding = new System.Windows.Forms.Padding(16);
            this.Size = new System.Drawing.Size(1522, 876);
            this._rootLayout.ResumeLayout(false);
            this._headerPanel.ResumeLayout(false);
            this._headerPanel.PerformLayout();
            this._iconCircle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._bannerIcon)).EndInit();
            this._actionsPanel.ResumeLayout(false);
            this._restoreFileButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._restoreFileIcon)).EndInit();
            this._backupNowButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._backupNowIcon)).EndInit();
            this._cardPanel.ResumeLayout(false);
            this._listLayout.ResumeLayout(false);
            this._filterHost.ResumeLayout(false);
            this._searchFrame.ResumeLayout(false);
            this._searchFrame.PerformLayout();
            this._dateRangePanel.ResumeLayout(false);
            this._dateRangePanel.PerformLayout();
            this._gridHolder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._grid)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TableLayoutPanel _rootLayout;
        private System.Windows.Forms.Panel _headerPanel;
        private System.Windows.Forms.Panel _iconCircle;
        private FontAwesome.Sharp.IconPictureBox _bannerIcon;
        private System.Windows.Forms.Label _titleLabel;
        private System.Windows.Forms.Label _subtitleLabel;
        private System.Windows.Forms.FlowLayoutPanel _actionsPanel;
        private SIMS_WinFormsApp.UI.Controls.PrimaryButton _restoreFileButton;
        private FontAwesome.Sharp.IconPictureBox _restoreFileIcon;
        private System.Windows.Forms.Label _restoreFileLabel;
        private SIMS_WinFormsApp.UI.Controls.PrimaryButton _backupNowButton;
        private FontAwesome.Sharp.IconPictureBox _backupNowIcon;
        private System.Windows.Forms.Label _backupNowLabel;
        private System.Windows.Forms.Panel _cardPanel;
        private System.Windows.Forms.TableLayoutPanel _listLayout;
        private System.Windows.Forms.Panel _filterHost;
        private System.Windows.Forms.Panel _searchFrame;
        private System.Windows.Forms.Label _searchIcon;
        private System.Windows.Forms.TextBox _searchTextBox;
        private System.Windows.Forms.Panel _dateRangePanel;
        private System.Windows.Forms.Button _todayButton;
        private System.Windows.Forms.Button _clearRangeButton;
        private System.Windows.Forms.CheckBox _fromDateCheckBox;
        private System.Windows.Forms.DateTimePicker _fromDatePicker;
        private System.Windows.Forms.CheckBox _toDateCheckBox;
        private System.Windows.Forms.DateTimePicker _toDatePicker;
        private System.Windows.Forms.Panel _gridHolder;
        private System.Windows.Forms.DataGridView _grid;
        private SIMS_WinFormsApp.Views.Pagination.PaginationControl _paginationControl;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Strategy;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreatedAt;
        private System.Windows.Forms.DataGridViewTextBoxColumn SizeColumn;
        private System.Windows.Forms.DataGridViewLinkColumn Local;
        private System.Windows.Forms.DataGridViewLinkColumn Cloud;
        private System.Windows.Forms.DataGridViewTextBoxColumn Actions;
    }
}