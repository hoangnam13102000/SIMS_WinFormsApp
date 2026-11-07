namespace SIMS_WinFormsApp.Views.Warehouse
{
    partial class ucStockReconciliation
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this._optionsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._exportCsvItem = new System.Windows.Forms.ToolStripMenuItem();
            this._exportExcelItem = new System.Windows.Forms.ToolStripMenuItem();
            this._menuSeparator = new System.Windows.Forms.ToolStripSeparator();
            this._importItem = new System.Windows.Forms.ToolStripMenuItem();
            this._toolTip = new System.Windows.Forms.ToolTip(this.components);
            this._clearDateLink = new System.Windows.Forms.LinkLabel();
            this._searchTimer = new System.Windows.Forms.Timer(this.components);
            this._rolloverTimer = new System.Windows.Forms.Timer(this.components);
            this._headerCard = new System.Windows.Forms.Panel();
            this._syncButton = new FontAwesome.Sharp.IconButton();
            this._optionsButton = new FontAwesome.Sharp.IconButton();
            this._subtitleLabel = new System.Windows.Forms.Label();
            this._titleLabel = new System.Windows.Forms.Label();
            this._headerIconPanel = new System.Windows.Forms.Panel();
            this._headerIcon = new FontAwesome.Sharp.IconPictureBox();
            this._gapHeader = new System.Windows.Forms.Panel();
            this._filterCard = new System.Windows.Forms.Panel();
            this._searchPanel = new System.Windows.Forms.Panel();
            this._searchBox = new System.Windows.Forms.TextBox();
            this._searchIcon = new FontAwesome.Sharp.IconPictureBox();
            this._fromDateLabel = new System.Windows.Forms.Label();
            this._fromDatePicker = new System.Windows.Forms.DateTimePicker();
            this._fromDateIcon = new FontAwesome.Sharp.IconPictureBox();
            this._toDateLabel = new System.Windows.Forms.Label();
            this._toDatePicker = new System.Windows.Forms.DateTimePicker();
            this._toDateIcon = new FontAwesome.Sharp.IconPictureBox();
            this._totalLabel = new System.Windows.Forms.Label();
            this._gridCard = new System.Windows.Forms.Panel();
            this._grid = new System.Windows.Forms.DataGridView();
            this.colReconciliationId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCopy = new System.Windows.Forms.DataGridViewImageColumn();
            this.colProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSystem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEdit = new System.Windows.Forms.DataGridViewImageColumn();
            this.colDiff = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReason = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetail = new System.Windows.Forms.DataGridViewImageColumn();
            this._emptyLabel = new System.Windows.Forms.Label();
            this._gapGrid = new System.Windows.Forms.Panel();
            this._pagerCard = new System.Windows.Forms.Panel();
            this._pagerInner = new System.Windows.Forms.Panel();
            this._prevButton = new FontAwesome.Sharp.IconButton();
            this._pageLabel = new System.Windows.Forms.Label();
            this._nextButton = new FontAwesome.Sharp.IconButton();
            this._pageInfoLabel = new System.Windows.Forms.Label();
            this._pageSizeCombo = new System.Windows.Forms.ComboBox();
            this._optionsMenu.SuspendLayout();
            this._headerCard.SuspendLayout();
            this._headerIconPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._headerIcon)).BeginInit();
            this._filterCard.SuspendLayout();
            this._searchPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._searchIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._fromDateIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._toDateIcon)).BeginInit();
            this._gridCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._grid)).BeginInit();
            this._pagerCard.SuspendLayout();
            this._pagerInner.SuspendLayout();
            this.SuspendLayout();
            // 
            // _optionsMenu
            // 
            this._optionsMenu.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this._optionsMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this._optionsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._exportCsvItem,
            this._exportExcelItem,
            this._menuSeparator,
            this._importItem});
            this._optionsMenu.Name = "_optionsMenu";
            this._optionsMenu.Size = new System.Drawing.Size(180, 94);
            // 
            // _exportCsvItem
            // 
            this._exportCsvItem.Name = "_exportCsvItem";
            this._exportCsvItem.Size = new System.Drawing.Size(179, 28);
            this._exportCsvItem.Text = "Xuất CSV";
            // 
            // _exportExcelItem
            // 
            this._exportExcelItem.Name = "_exportExcelItem";
            this._exportExcelItem.Size = new System.Drawing.Size(179, 28);
            this._exportExcelItem.Text = "Xuất Excel";
            // 
            // _menuSeparator
            // 
            this._menuSeparator.Name = "_menuSeparator";
            this._menuSeparator.Size = new System.Drawing.Size(176, 6);
            // 
            // _importItem
            // 
            this._importItem.Name = "_importItem";
            this._importItem.Size = new System.Drawing.Size(179, 28);
            this._importItem.Text = "Nhập dữ liệu";
            // 
            // _clearDateLink
            // 
            this._clearDateLink.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this._clearDateLink.AutoSize = true;
            this._clearDateLink.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this._clearDateLink.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this._clearDateLink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this._clearDateLink.Location = new System.Drawing.Point(725, 24);
            this._clearDateLink.Name = "_clearDateLink";
            this._clearDateLink.Size = new System.Drawing.Size(68, 23);
            this._clearDateLink.TabIndex = 6;
            this._clearDateLink.TabStop = true;
            this._clearDateLink.Text = "Xoá lọc";
            this._toolTip.SetToolTip(this._clearDateLink, "Xóa lọc ngày");
            this._clearDateLink.Visible = false;
            // 
            // _searchTimer
            // 
            this._searchTimer.Interval = 300;
            // 
            // _rolloverTimer
            // 
            this._rolloverTimer.Interval = 60000;
            // 
            // _headerCard
            // 
            this._headerCard.BackColor = System.Drawing.Color.White;
            this._headerCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._headerCard.Controls.Add(this._syncButton);
            this._headerCard.Controls.Add(this._optionsButton);
            this._headerCard.Controls.Add(this._subtitleLabel);
            this._headerCard.Controls.Add(this._titleLabel);
            this._headerCard.Controls.Add(this._headerIconPanel);
            this._headerCard.Dock = System.Windows.Forms.DockStyle.Top;
            this._headerCard.Location = new System.Drawing.Point(24, 24);
            this._headerCard.Name = "_headerCard";
            this._headerCard.Size = new System.Drawing.Size(952, 88);
            this._headerCard.TabIndex = 1;
            // 
            // _syncButton
            // 
            this._syncButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._syncButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._syncButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this._syncButton.FlatAppearance.BorderSize = 0;
            this._syncButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._syncButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this._syncButton.ForeColor = System.Drawing.Color.White;
            this._syncButton.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this._syncButton.IconColor = System.Drawing.Color.White;
            this._syncButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._syncButton.IconSize = 14;
            this._syncButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._syncButton.Location = new System.Drawing.Point(726, 22);
            this._syncButton.Name = "_syncButton";
            this._syncButton.Size = new System.Drawing.Size(206, 38);
            this._syncButton.TabIndex = 4;
            this._syncButton.Text = "Đồng bộ SP hôm nay";
            this._syncButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this._syncButton.UseVisualStyleBackColor = false;
            // 
            // _optionsButton
            // 
            this._optionsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._optionsButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this._optionsButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._optionsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._optionsButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this._optionsButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._optionsButton.IconChar = FontAwesome.Sharp.IconChar.ChevronDown;
            this._optionsButton.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._optionsButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._optionsButton.IconSize = 12;
            this._optionsButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._optionsButton.Location = new System.Drawing.Point(604, 22);
            this._optionsButton.Name = "_optionsButton";
            this._optionsButton.Size = new System.Drawing.Size(110, 38);
            this._optionsButton.TabIndex = 3;
            this._optionsButton.Text = "Tùy chọn";
            this._optionsButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this._optionsButton.UseVisualStyleBackColor = false;
            // 
            // _subtitleLabel
            // 
            this._subtitleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._subtitleLabel.AutoEllipsis = true;
            this._subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._subtitleLabel.Location = new System.Drawing.Point(80, 52);
            this._subtitleLabel.Name = "_subtitleLabel";
            this._subtitleLabel.Size = new System.Drawing.Size(508, 20);
            this._subtitleLabel.TabIndex = 2;
            this._subtitleLabel.Text = "Đếm tồn thực tế mỗi ngày, 00:00 tự khóa phiên";
            this._subtitleLabel.UseMnemonic = false;
            // 
            // _titleLabel
            // 
            this._titleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._titleLabel.AutoEllipsis = true;
            this._titleLabel.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this._titleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._titleLabel.Location = new System.Drawing.Point(80, 12);
            this._titleLabel.Name = "_titleLabel";
            this._titleLabel.Size = new System.Drawing.Size(508, 36);
            this._titleLabel.TabIndex = 1;
            this._titleLabel.Text = "Đối chiếu kho cuối ngày";
            this._titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._titleLabel.UseMnemonic = false;
            // 
            // _headerIconPanel
            // 
            this._headerIconPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this._headerIconPanel.Controls.Add(this._headerIcon);
            this._headerIconPanel.Location = new System.Drawing.Point(24, 18);
            this._headerIconPanel.Name = "_headerIconPanel";
            this._headerIconPanel.Size = new System.Drawing.Size(44, 44);
            this._headerIconPanel.TabIndex = 0;
            // 
            // _headerIcon
            // 
            this._headerIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this._headerIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._headerIcon.IconChar = FontAwesome.Sharp.IconChar.ScaleBalanced;
            this._headerIcon.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._headerIcon.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._headerIcon.IconSize = 20;
            this._headerIcon.Location = new System.Drawing.Point(12, 12);
            this._headerIcon.Name = "_headerIcon";
            this._headerIcon.Size = new System.Drawing.Size(20, 20);
            this._headerIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._headerIcon.TabIndex = 0;
            this._headerIcon.TabStop = false;
            // 
            // _gapHeader
            // 
            this._gapHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this._gapHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this._gapHeader.Location = new System.Drawing.Point(24, 112);
            this._gapHeader.Name = "_gapHeader";
            this._gapHeader.Size = new System.Drawing.Size(952, 16);
            this._gapHeader.TabIndex = 2;
            // 
            // _filterCard
            // 
            this._filterCard.BackColor = System.Drawing.Color.White;
            this._filterCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._filterCard.Controls.Add(this._searchPanel);
            this._filterCard.Controls.Add(this._fromDateLabel);
            this._filterCard.Controls.Add(this._fromDatePicker);
            this._filterCard.Controls.Add(this._fromDateIcon);
            this._filterCard.Controls.Add(this._toDateLabel);
            this._filterCard.Controls.Add(this._toDatePicker);
            this._filterCard.Controls.Add(this._toDateIcon);
            this._filterCard.Controls.Add(this._clearDateLink);
            this._filterCard.Controls.Add(this._totalLabel);
            this._filterCard.Dock = System.Windows.Forms.DockStyle.Top;
            this._filterCard.Location = new System.Drawing.Point(24, 128);
            this._filterCard.Name = "_filterCard";
            this._filterCard.Size = new System.Drawing.Size(952, 70);
            this._filterCard.TabIndex = 3;
            // 
            // _searchPanel
            // 
            this._searchPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this._searchPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._searchPanel.Controls.Add(this._searchBox);
            this._searchPanel.Controls.Add(this._searchIcon);
            this._searchPanel.Location = new System.Drawing.Point(24, 16);
            this._searchPanel.Name = "_searchPanel";
            this._searchPanel.Size = new System.Drawing.Size(300, 36);
            this._searchPanel.TabIndex = 0;
            // 
            // _searchBox
            // 
            this._searchBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._searchBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this._searchBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._searchBox.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this._searchBox.Location = new System.Drawing.Point(34, 8);
            this._searchBox.Name = "_searchBox";
            this._searchBox.Size = new System.Drawing.Size(256, 22);
            this._searchBox.TabIndex = 1;
            // 
            // _searchIcon
            // 
            this._searchIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this._searchIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._searchIcon.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this._searchIcon.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._searchIcon.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._searchIcon.IconSize = 16;
            this._searchIcon.Location = new System.Drawing.Point(10, 9);
            this._searchIcon.Name = "_searchIcon";
            this._searchIcon.Size = new System.Drawing.Size(16, 16);
            this._searchIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._searchIcon.TabIndex = 0;
            this._searchIcon.TabStop = false;
            // 
            // _fromDateLabel
            // 
            this._fromDateLabel.AutoSize = true;
            this._fromDateLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._fromDateLabel.Location = new System.Drawing.Point(338, 25);
            this._fromDateLabel.Name = "_fromDateLabel";
            this._fromDateLabel.Size = new System.Drawing.Size(29, 23);
            this._fromDateLabel.TabIndex = 1;
            this._fromDateLabel.Text = "Từ";
            // 
            // _fromDatePicker
            // 
            this._fromDatePicker.CustomFormat = " ";
            this._fromDatePicker.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this._fromDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._fromDatePicker.Location = new System.Drawing.Point(394, 20);
            this._fromDatePicker.Name = "_fromDatePicker";
            this._fromDatePicker.Size = new System.Drawing.Size(124, 29);
            this._fromDatePicker.TabIndex = 2;
            this._fromDatePicker.Value = new System.DateTime(2026, 11, 1, 2, 5, 1, 526);
            // 
            // _fromDateIcon
            // 
            this._fromDateIcon.BackColor = System.Drawing.Color.White;
            this._fromDateIcon.Cursor = System.Windows.Forms.Cursors.Hand;
            this._fromDateIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._fromDateIcon.IconChar = FontAwesome.Sharp.IconChar.CalendarDay;
            this._fromDateIcon.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._fromDateIcon.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._fromDateIcon.IconSize = 18;
            this._fromDateIcon.Location = new System.Drawing.Point(496, 23);
            this._fromDateIcon.Name = "_fromDateIcon";
            this._fromDateIcon.Size = new System.Drawing.Size(18, 18);
            this._fromDateIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._fromDateIcon.TabIndex = 3;
            this._fromDateIcon.TabStop = false;
            // 
            // _toDateLabel
            // 
            this._toDateLabel.AutoSize = true;
            this._toDateLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._toDateLabel.Location = new System.Drawing.Point(530, 25);
            this._toDateLabel.Name = "_toDateLabel";
            this._toDateLabel.Size = new System.Drawing.Size(41, 23);
            this._toDateLabel.TabIndex = 4;
            this._toDateLabel.Text = "Đến";
            // 
            // _toDatePicker
            // 
            this._toDatePicker.CustomFormat = " ";
            this._toDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._toDatePicker.Location = new System.Drawing.Point(592, 20);
            this._toDatePicker.Name = "_toDatePicker";
            this._toDatePicker.Size = new System.Drawing.Size(124, 29);
            this._toDatePicker.TabIndex = 5;
            // 
            // _toDateIcon
            // 
            this._toDateIcon.BackColor = System.Drawing.Color.White;
            this._toDateIcon.Cursor = System.Windows.Forms.Cursors.Hand;
            this._toDateIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._toDateIcon.IconChar = FontAwesome.Sharp.IconChar.CalendarDay;
            this._toDateIcon.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._toDateIcon.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._toDateIcon.IconSize = 18;
            this._toDateIcon.Location = new System.Drawing.Point(694, 23);
            this._toDateIcon.Name = "_toDateIcon";
            this._toDateIcon.Size = new System.Drawing.Size(18, 18);
            this._toDateIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._toDateIcon.TabIndex = 6;
            this._toDateIcon.TabStop = false;
            // 
            // _totalLabel
            // 
            this._totalLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._totalLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this._totalLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._totalLabel.Location = new System.Drawing.Point(702, 24);
            this._totalLabel.Name = "_totalLabel";
            this._totalLabel.Size = new System.Drawing.Size(230, 22);
            this._totalLabel.TabIndex = 7;
            this._totalLabel.Text = "Tổng cộng: 0 dòng đối chiếu";
            this._totalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _gridCard
            // 
            this._gridCard.BackColor = System.Drawing.Color.White;
            this._gridCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._gridCard.Controls.Add(this._grid);
            this._gridCard.Controls.Add(this._emptyLabel);
            this._gridCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridCard.Location = new System.Drawing.Point(24, 198);
            this._gridCard.Name = "_gridCard";
            this._gridCard.Padding = new System.Windows.Forms.Padding(10);
            this._gridCard.Size = new System.Drawing.Size(952, 418);
            this._gridCard.TabIndex = 4;
            // 
            // _grid
            // 
            this._grid.AllowUserToAddRows = false;
            this._grid.AllowUserToDeleteRows = false;
            this._grid.AllowUserToResizeRows = false;
            this._grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._grid.BackgroundColor = System.Drawing.Color.White;
            this._grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._grid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this._grid.ColumnHeadersHeight = 48;
            this._grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colReconciliationId,
            this.colProductCode,
            this.colCopy,
            this.colProductName,
            this.colSystem,
            this.colActual,
            this.colEdit,
            this.colDiff,
            this.colReason,
            this.colTime,
            this.colDetail});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle5.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._grid.DefaultCellStyle = dataGridViewCellStyle5;
            this._grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this._grid.EnableHeadersVisualStyles = false;
            this._grid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this._grid.Location = new System.Drawing.Point(10, 10);
            this._grid.MultiSelect = false;
            this._grid.Name = "_grid";
            this._grid.RowHeadersVisible = false;
            this._grid.RowHeadersWidth = 51;
            this._grid.RowTemplate.Height = 46;
            this._grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._grid.Size = new System.Drawing.Size(930, 396);
            this._grid.TabIndex = 0;
            // 
            // colReconciliationId
            // 
            this.colReconciliationId.HeaderText = "ReconciliationId";
            this.colReconciliationId.MinimumWidth = 6;
            this.colReconciliationId.Name = "colReconciliationId";
            this.colReconciliationId.ReadOnly = true;
            this.colReconciliationId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colReconciliationId.Visible = false;
            // 
            // colProductCode
            // 
            this.colProductCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colProductCode.FillWeight = 10F;
            this.colProductCode.HeaderText = "Mã SP";
            this.colProductCode.MinimumWidth = 6;
            this.colProductCode.Name = "colProductCode";
            this.colProductCode.ReadOnly = true;
            this.colProductCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colProductCode.Width = 90;
            // 
            // colCopy
            // 
            this.colCopy.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colCopy.HeaderText = "";
            this.colCopy.MinimumWidth = 6;
            this.colCopy.Name = "colCopy";
            this.colCopy.ReadOnly = true;
            this.colCopy.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colCopy.Width = 36;
            // 
            // colProductName
            // 
            this.colProductName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colProductName.FillWeight = 55F;
            this.colProductName.HeaderText = "Sản phẩm";
            this.colProductName.MinimumWidth = 160;
            this.colProductName.Name = "colProductName";
            this.colProductName.ReadOnly = true;
            this.colProductName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colSystem
            // 
            this.colSystem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colSystem.DefaultCellStyle = dataGridViewCellStyle2;
            this.colSystem.HeaderText = "Tồn hệ thống";
            this.colSystem.MinimumWidth = 6;
            this.colSystem.Name = "colSystem";
            this.colSystem.ReadOnly = true;
            this.colSystem.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colSystem.Width = 90;
            // 
            // colActual
            // 
            this.colActual.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colActual.DefaultCellStyle = dataGridViewCellStyle3;
            this.colActual.HeaderText = "Tồn thực tế";
            this.colActual.MaxInputLength = 9;
            this.colActual.MinimumWidth = 6;
            this.colActual.Name = "colActual";
            this.colActual.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colActual.Width = 90;
            // 
            // colEdit
            // 
            this.colEdit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colEdit.HeaderText = "";
            this.colEdit.MinimumWidth = 6;
            this.colEdit.Name = "colEdit";
            this.colEdit.ReadOnly = true;
            this.colEdit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colEdit.Width = 36;
            // 
            // colDiff
            // 
            this.colDiff.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colDiff.DefaultCellStyle = dataGridViewCellStyle4;
            this.colDiff.HeaderText = "Chênh lệch";
            this.colDiff.MinimumWidth = 6;
            this.colDiff.Name = "colDiff";
            this.colDiff.ReadOnly = true;
            this.colDiff.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colDiff.Width = 90;
            // 
            // colReason
            // 
            this.colReason.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colReason.FillWeight = 45F;
            this.colReason.HeaderText = "Lý do";
            this.colReason.MaxInputLength = 255;
            this.colReason.MinimumWidth = 150;
            this.colReason.Name = "colReason";
            this.colReason.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colTime
            // 
            this.colTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colTime.HeaderText = "Cập nhật";
            this.colTime.MinimumWidth = 6;
            this.colTime.Name = "colTime";
            this.colTime.ReadOnly = true;
            this.colTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colTime.Width = 140;
            // 
            // colDetail
            // 
            this.colDetail.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colDetail.HeaderText = "";
            this.colDetail.MinimumWidth = 6;
            this.colDetail.Name = "colDetail";
            this.colDetail.ReadOnly = true;
            this.colDetail.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colDetail.Width = 56;
            // 
            // _emptyLabel
            // 
            this._emptyLabel.BackColor = System.Drawing.Color.White;
            this._emptyLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._emptyLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._emptyLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._emptyLabel.Location = new System.Drawing.Point(10, 10);
            this._emptyLabel.Name = "_emptyLabel";
            this._emptyLabel.Size = new System.Drawing.Size(930, 396);
            this._emptyLabel.TabIndex = 1;
            this._emptyLabel.Text = "Chưa có dữ liệu đối chiếu";
            this._emptyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._emptyLabel.Visible = false;
            // 
            // _gapGrid
            // 
            this._gapGrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this._gapGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this._gapGrid.Location = new System.Drawing.Point(24, 198);
            this._gapGrid.Name = "_gapGrid";
            this._gapGrid.Size = new System.Drawing.Size(952, 16);
            this._gapGrid.TabIndex = 4;
            // 
            // _pagerCard
            // 
            this._pagerCard.BackColor = System.Drawing.Color.White;
            this._pagerCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._pagerCard.Controls.Add(this._pagerInner);
            this._pagerCard.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._pagerCard.Location = new System.Drawing.Point(24, 554);
            this._pagerCard.Name = "_pagerCard";
            this._pagerCard.Size = new System.Drawing.Size(952, 62);
            this._pagerCard.TabIndex = 5;
            // 
            // _pagerInner
            // 
            this._pagerInner.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._pagerInner.BackColor = System.Drawing.Color.White;
            this._pagerInner.Controls.Add(this._prevButton);
            this._pagerInner.Controls.Add(this._pageLabel);
            this._pagerInner.Controls.Add(this._nextButton);
            this._pagerInner.Controls.Add(this._pageInfoLabel);
            this._pagerInner.Controls.Add(this._pageSizeCombo);
            this._pagerInner.Location = new System.Drawing.Point(260, 13);
            this._pagerInner.Name = "_pagerInner";
            this._pagerInner.Size = new System.Drawing.Size(430, 36);
            this._pagerInner.TabIndex = 0;
            // 
            // _prevButton
            // 
            this._prevButton.BackColor = System.Drawing.Color.White;
            this._prevButton.FlatAppearance.BorderSize = 0;
            this._prevButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._prevButton.IconChar = FontAwesome.Sharp.IconChar.ChevronLeft;
            this._prevButton.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._prevButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._prevButton.IconSize = 14;
            this._prevButton.Location = new System.Drawing.Point(0, 2);
            this._prevButton.Name = "_prevButton";
            this._prevButton.Size = new System.Drawing.Size(32, 32);
            this._prevButton.TabIndex = 0;
            this._prevButton.UseVisualStyleBackColor = false;
            // 
            // _pageLabel
            // 
            this._pageLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._pageLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._pageLabel.ForeColor = System.Drawing.Color.White;
            this._pageLabel.Location = new System.Drawing.Point(38, 0);
            this._pageLabel.Name = "_pageLabel";
            this._pageLabel.Size = new System.Drawing.Size(36, 36);
            this._pageLabel.TabIndex = 1;
            this._pageLabel.Text = "1";
            this._pageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _nextButton
            // 
            this._nextButton.BackColor = System.Drawing.Color.White;
            this._nextButton.FlatAppearance.BorderSize = 0;
            this._nextButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._nextButton.IconChar = FontAwesome.Sharp.IconChar.ChevronRight;
            this._nextButton.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._nextButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._nextButton.IconSize = 14;
            this._nextButton.Location = new System.Drawing.Point(80, 2);
            this._nextButton.Name = "_nextButton";
            this._nextButton.Size = new System.Drawing.Size(32, 32);
            this._nextButton.TabIndex = 2;
            this._nextButton.UseVisualStyleBackColor = false;
            // 
            // _pageInfoLabel
            // 
            this._pageInfoLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this._pageInfoLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._pageInfoLabel.Location = new System.Drawing.Point(128, 6);
            this._pageInfoLabel.Name = "_pageInfoLabel";
            this._pageInfoLabel.Size = new System.Drawing.Size(120, 24);
            this._pageInfoLabel.TabIndex = 3;
            this._pageInfoLabel.Text = "Trang 1 / 1";
            this._pageInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _pageSizeCombo
            // 
            this._pageSizeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._pageSizeCombo.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this._pageSizeCombo.FormattingEnabled = true;
            this._pageSizeCombo.Items.AddRange(new object[] {
            "10",
            "20",
            "50",
            "100"});
            this._pageSizeCombo.Location = new System.Drawing.Point(286, 4);
            this._pageSizeCombo.Name = "_pageSizeCombo";
            this._pageSizeCombo.Size = new System.Drawing.Size(104, 29);
            this._pageSizeCombo.TabIndex = 4;
            // 
            // ucStockReconciliation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.Controls.Add(this._pagerCard);
            this.Controls.Add(this._gapGrid);
            this.Controls.Add(this._gridCard);
            this.Controls.Add(this._filterCard);
            this.Controls.Add(this._gapHeader);
            this.Controls.Add(this._headerCard);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ucStockReconciliation";
            this.Padding = new System.Windows.Forms.Padding(24);
            this.Size = new System.Drawing.Size(1000, 640);
            this._optionsMenu.ResumeLayout(false);
            this._headerCard.ResumeLayout(false);
            this._headerIconPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._headerIcon)).EndInit();
            this._filterCard.ResumeLayout(false);
            this._filterCard.PerformLayout();
            this._searchPanel.ResumeLayout(false);
            this._searchPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._searchIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._fromDateIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._toDateIcon)).EndInit();
            this._gridCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._grid)).EndInit();
            this._pagerCard.ResumeLayout(false);
            this._pagerInner.ResumeLayout(false);
            this._fromDateIcon.Click += new System.EventHandler(this.FromDateIcon_Click);
            this._toDateIcon.Click += new System.EventHandler(this.ToDateIcon_Click);
            this._clearDateLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ClearDateLink_Click);
            this._optionsButton.Click += new System.EventHandler(this.OptionsButton_Click);
            this._exportCsvItem.Click += new System.EventHandler(this.ExportCsvItem_Click);
            this._exportExcelItem.Click += new System.EventHandler(this.ExportExcelItem_Click);
            this._importItem.Click += new System.EventHandler(this.ImportItem_Click);
            this._syncButton.Click += new System.EventHandler(this.SyncButton_Click);
            this._prevButton.Click += new System.EventHandler(this.PreviousButton_Click);
            this._nextButton.Click += new System.EventHandler(this.NextButton_Click);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip _optionsMenu;
        private System.Windows.Forms.ToolStripMenuItem _exportCsvItem;
        private System.Windows.Forms.ToolStripMenuItem _exportExcelItem;
        private System.Windows.Forms.ToolStripSeparator _menuSeparator;
        private System.Windows.Forms.ToolStripMenuItem _importItem;
        private System.Windows.Forms.ToolTip _toolTip;
        private System.Windows.Forms.Timer _searchTimer;
        private System.Windows.Forms.Timer _rolloverTimer;
        private System.Windows.Forms.Panel _headerCard;
        private System.Windows.Forms.Panel _headerIconPanel;
        private FontAwesome.Sharp.IconPictureBox _headerIcon;
        private System.Windows.Forms.Label _subtitleLabel;
        private System.Windows.Forms.Label _titleLabel;
        private FontAwesome.Sharp.IconButton _optionsButton;
        private FontAwesome.Sharp.IconButton _syncButton;
        private System.Windows.Forms.Panel _gapHeader;
        private System.Windows.Forms.Panel _filterCard;
        private System.Windows.Forms.Panel _searchPanel;
        private FontAwesome.Sharp.IconPictureBox _searchIcon;
        private System.Windows.Forms.Label _fromDateLabel;
        private System.Windows.Forms.TextBox _searchBox;
        private FontAwesome.Sharp.IconPictureBox _fromDateIcon;
        private System.Windows.Forms.DateTimePicker _fromDatePicker;
        private System.Windows.Forms.Label _toDateLabel;
        private System.Windows.Forms.DateTimePicker _toDatePicker;
        private FontAwesome.Sharp.IconPictureBox _toDateIcon;
        private System.Windows.Forms.LinkLabel _clearDateLink;
        private System.Windows.Forms.Label _totalLabel;
        private System.Windows.Forms.Panel _gridCard;
        private System.Windows.Forms.DataGridView _grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReconciliationId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductCode;
        private System.Windows.Forms.DataGridViewImageColumn colCopy;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSystem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colActual;
        private System.Windows.Forms.DataGridViewImageColumn colEdit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDiff;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReason;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
        private System.Windows.Forms.DataGridViewImageColumn colDetail;
        private System.Windows.Forms.Label _emptyLabel;
        private System.Windows.Forms.Panel _gapGrid;
        private System.Windows.Forms.Panel _pagerCard;
        private System.Windows.Forms.Panel _pagerInner;
        private FontAwesome.Sharp.IconButton _prevButton;
        private System.Windows.Forms.Label _pageLabel;
        private FontAwesome.Sharp.IconButton _nextButton;
        private System.Windows.Forms.Label _pageInfoLabel;
        private System.Windows.Forms.ComboBox _pageSizeCombo;
    }
}
