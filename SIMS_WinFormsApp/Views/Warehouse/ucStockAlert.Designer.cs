using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace SIMS_WinFormsApp.Views.Warehouse
{
    partial class ucStockAlert
    {
        private IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            this._headerCard = new System.Windows.Forms.Panel();
            this._headerIconPanel = new System.Windows.Forms.Panel();
            this._headerIcon = new FontAwesome.Sharp.IconPictureBox();
            this._titleLabel = new System.Windows.Forms.Label();
            this._subtitleLabel = new System.Windows.Forms.Label();
            this._optionsButton = new System.Windows.Forms.Button();
            this._optionsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._exportCsvItem = new System.Windows.Forms.ToolStripMenuItem();
            this._exportExcelItem = new System.Windows.Forms.ToolStripMenuItem();
            this._filterCard = new System.Windows.Forms.Panel();
            this._searchBox = new System.Windows.Forms.TextBox();
            this._categoryCombo = new System.Windows.Forms.ComboBox();
            this._fromLabel = new System.Windows.Forms.Label();
            this._fromDatePicker = new System.Windows.Forms.DateTimePicker();
            this._toLabel = new System.Windows.Forms.Label();
            this._toDatePicker = new System.Windows.Forms.DateTimePicker();
            this._clearFilterLink = new System.Windows.Forms.LinkLabel();
            this._totalLabel = new System.Windows.Forms.Label();
            this._gridCard = new System.Windows.Forms.Panel();
            this._grid = new System.Windows.Forms.DataGridView();
            this.colAlertId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAlertType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReporter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCreatedAt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPlan = new System.Windows.Forms.DataGridViewImageColumn();
            this.colResolve = new System.Windows.Forms.DataGridViewImageColumn();
            this._emptyLabel = new System.Windows.Forms.Label();
            this._gapHeader = new System.Windows.Forms.Panel();
            this._gapFilter = new System.Windows.Forms.Panel();
            this._gapPager = new System.Windows.Forms.Panel();
            this._pagerPanel = new System.Windows.Forms.Panel();
            this._pagerInner = new System.Windows.Forms.Panel();
            this._prevButton = new System.Windows.Forms.Button();
            this._pageLabel = new System.Windows.Forms.Label();
            this._nextButton = new System.Windows.Forms.Button();
            this._pageInfoLabel = new System.Windows.Forms.Label();
            this._displayLabel = new System.Windows.Forms.Label();
            this._pageSizeCombo = new System.Windows.Forms.ComboBox();
            this._rowsLabel = new System.Windows.Forms.Label();
            this._headerCard.SuspendLayout();
            this._headerIconPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._headerIcon)).BeginInit();
            this._optionsMenu.SuspendLayout();
            this._filterCard.SuspendLayout();
            this._gridCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._grid)).BeginInit();
            this._pagerPanel.SuspendLayout();
            this._pagerInner.SuspendLayout();
            this.SuspendLayout();
            // 
            // _headerCard
            // 
            this._headerCard.BackColor = System.Drawing.Color.White;
            this._headerCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._headerCard.Controls.Add(this._headerIconPanel);
            this._headerCard.Controls.Add(this._titleLabel);
            this._headerCard.Controls.Add(this._subtitleLabel);
            this._headerCard.Controls.Add(this._optionsButton);
            this._headerCard.Dock = System.Windows.Forms.DockStyle.Top;
            this._headerCard.Location = new System.Drawing.Point(16, 16);
            this._headerCard.Name = "_headerCard";
            this._headerCard.Size = new System.Drawing.Size(1252, 88);
            this._headerCard.TabIndex = 0;
            // 
            // _headerIconPanel
            // 
            this._headerIconPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(243)))), ((int)(((byte)(199)))));
            this._headerIconPanel.Controls.Add(this._headerIcon);
            this._headerIconPanel.Location = new System.Drawing.Point(16, 19);
            this._headerIconPanel.Name = "_headerIconPanel";
            this._headerIconPanel.Size = new System.Drawing.Size(50, 50);
            this._headerIconPanel.TabIndex = 0;
            // 
            // _headerIcon
            // 
            this._headerIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(243)))), ((int)(((byte)(199)))));
            this._headerIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(119)))), ((int)(((byte)(6)))));
            this._headerIcon.IconChar = FontAwesome.Sharp.IconChar.ExclamationTriangle;
            this._headerIcon.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(119)))), ((int)(((byte)(6)))));
            this._headerIcon.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._headerIcon.IconSize = 24;
            this._headerIcon.Location = new System.Drawing.Point(13, 13);
            this._headerIcon.Name = "_headerIcon";
            this._headerIcon.Size = new System.Drawing.Size(24, 24);
            this._headerIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._headerIcon.TabIndex = 0;
            this._headerIcon.TabStop = false;
            // 
            // _titleLabel
            // 
            this._titleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._titleLabel.AutoEllipsis = true;
            this._titleLabel.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this._titleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this._titleLabel.Location = new System.Drawing.Point(80, 12);
            this._titleLabel.Name = "_titleLabel";
            this._titleLabel.Size = new System.Drawing.Size(1006, 36);
            this._titleLabel.TabIndex = 1;
            this._titleLabel.Text = "Cảnh báo tồn kho";
            this._titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._titleLabel.UseMnemonic = false;
            // 
            // _subtitleLabel
            // 
            this._subtitleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._subtitleLabel.AutoEllipsis = true;
            this._subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._subtitleLabel.Location = new System.Drawing.Point(80, 52);
            this._subtitleLabel.Name = "_subtitleLabel";
            this._subtitleLabel.Size = new System.Drawing.Size(1006, 20);
            this._subtitleLabel.TabIndex = 2;
            this._subtitleLabel.Text = "Sản phẩm hết hoặc sắp hết hàng cần nhập thêm";
            this._subtitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._subtitleLabel.UseMnemonic = false;
            // 
            // _optionsButton
            // 
            this._optionsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._optionsButton.BackColor = System.Drawing.Color.White;
            this._optionsButton.ContextMenuStrip = this._optionsMenu;
            this._optionsButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this._optionsButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._optionsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._optionsButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._optionsButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._optionsButton.Location = new System.Drawing.Point(1102, 25);
            this._optionsButton.Name = "_optionsButton";
            this._optionsButton.Size = new System.Drawing.Size(130, 38);
            this._optionsButton.TabIndex = 3;
            this._optionsButton.Text = "Tùy chọn ▾";
            this._optionsButton.UseVisualStyleBackColor = false;
            // 
            // _optionsMenu
            // 
            this._optionsMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this._optionsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._exportCsvItem,
            this._exportExcelItem});
            this._optionsMenu.Name = "_optionsMenu";
            this._optionsMenu.Size = new System.Drawing.Size(164, 68);
            // 
            // _exportCsvItem
            // 
            this._exportCsvItem.Name = "_exportCsvItem";
            this._exportCsvItem.Size = new System.Drawing.Size(163, 32);
            this._exportCsvItem.Text = "Xuất CSV";
            // 
            // _exportExcelItem
            // 
            this._exportExcelItem.Name = "_exportExcelItem";
            this._exportExcelItem.Size = new System.Drawing.Size(163, 32);
            this._exportExcelItem.Text = "Xuất Excel";
            // 
            // _filterCard
            // 
            this._filterCard.BackColor = System.Drawing.Color.White;
            this._filterCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._filterCard.Controls.Add(this._searchBox);
            this._filterCard.Controls.Add(this._categoryCombo);
            this._filterCard.Controls.Add(this._fromLabel);
            this._filterCard.Controls.Add(this._fromDatePicker);
            this._filterCard.Controls.Add(this._toLabel);
            this._filterCard.Controls.Add(this._toDatePicker);
            this._filterCard.Controls.Add(this._clearFilterLink);
            this._filterCard.Controls.Add(this._totalLabel);
            this._filterCard.Dock = System.Windows.Forms.DockStyle.Top;
            this._filterCard.Location = new System.Drawing.Point(16, 116);
            this._filterCard.Name = "_filterCard";
            this._filterCard.Size = new System.Drawing.Size(1252, 60);
            this._filterCard.TabIndex = 1;
            // 
            // _searchBox
            // 
            this._searchBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._searchBox.Location = new System.Drawing.Point(16, 18);
            this._searchBox.Name = "_searchBox";
            this._searchBox.Size = new System.Drawing.Size(260, 31);
            this._searchBox.TabIndex = 0;
            // 
            // _categoryCombo
            // 
            this._categoryCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._categoryCombo.FormattingEnabled = true;
            this._categoryCombo.Location = new System.Drawing.Point(290, 18);
            this._categoryCombo.Name = "_categoryCombo";
            this._categoryCombo.Size = new System.Drawing.Size(170, 33);
            this._categoryCombo.TabIndex = 1;
            // 
            // _fromLabel
            // 
            this._fromLabel.AutoSize = true;
            this._fromLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._fromLabel.Location = new System.Drawing.Point(476, 22);
            this._fromLabel.Name = "_fromLabel";
            this._fromLabel.Size = new System.Drawing.Size(32, 25);
            this._fromLabel.TabIndex = 2;
            this._fromLabel.Text = "Từ";
            // 
            // _fromDatePicker
            // 
            this._fromDatePicker.Checked = false;
            this._fromDatePicker.CustomFormat = " ";
            this._fromDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._fromDatePicker.Location = new System.Drawing.Point(510, 18);
            this._fromDatePicker.Name = "_fromDatePicker";
            this._fromDatePicker.Size = new System.Drawing.Size(140, 31);
            this._fromDatePicker.TabIndex = 3;
            // 
            // _toLabel
            // 
            this._toLabel.AutoSize = true;
            this._toLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._toLabel.Location = new System.Drawing.Point(666, 22);
            this._toLabel.Name = "_toLabel";
            this._toLabel.Size = new System.Drawing.Size(44, 25);
            this._toLabel.TabIndex = 4;
            this._toLabel.Text = "Đến";
            // 
            // _toDatePicker
            // 
            this._toDatePicker.Checked = false;
            this._toDatePicker.CustomFormat = " ";
            this._toDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._toDatePicker.Location = new System.Drawing.Point(710, 18);
            this._toDatePicker.Name = "_toDatePicker";
            this._toDatePicker.Size = new System.Drawing.Size(140, 31);
            this._toDatePicker.TabIndex = 5;
            // 
            // _clearFilterLink
            // 
            this._clearFilterLink.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this._clearFilterLink.AutoSize = true;
            this._clearFilterLink.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._clearFilterLink.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this._clearFilterLink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this._clearFilterLink.Location = new System.Drawing.Point(856, 21);
            this._clearFilterLink.Name = "_clearFilterLink";
            this._clearFilterLink.Size = new System.Drawing.Size(96, 25);
            this._clearFilterLink.TabIndex = 6;
            this._clearFilterLink.TabStop = true;
            this._clearFilterLink.Text = "✕ Xóa lọc";
            this._clearFilterLink.Visible = false;
            this._clearFilterLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._clearFilterLink_LinkClicked);
            // 
            // _totalLabel
            // 
            this._totalLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._totalLabel.Location = new System.Drawing.Point(1064, 22);
            this._totalLabel.Name = "_totalLabel";
            this._totalLabel.Size = new System.Drawing.Size(170, 22);
            this._totalLabel.TabIndex = 7;
            this._totalLabel.Text = "Tổng cộng: 0 cảnh báo";
            this._totalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _gridCard
            // 
            this._gridCard.BackColor = System.Drawing.Color.White;
            this._gridCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._gridCard.Controls.Add(this._grid);
            this._gridCard.Controls.Add(this._emptyLabel);
            this._gridCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridCard.Location = new System.Drawing.Point(16, 188);
            this._gridCard.Name = "_gridCard";
            this._gridCard.Size = new System.Drawing.Size(1252, 412);
            this._gridCard.TabIndex = 2;
            // 
            // _grid
            // 
            this._grid.AllowUserToAddRows = false;
            this._grid.AllowUserToDeleteRows = false;
            this._grid.AllowUserToResizeRows = false;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this._grid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle8;
            this._grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._grid.BackgroundColor = System.Drawing.Color.White;
            this._grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._grid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this._grid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this._grid.ColumnHeadersHeight = 38;
            this._grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this._grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAlertId,
            this.colProductCode,
            this.colProductName,
            this.colAlertType,
            this.colReporter,
            this.colCreatedAt,
            this.colStatus,
            this.colPlan,
            this.colResolve});
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._grid.DefaultCellStyle = dataGridViewCellStyle14;
            this._grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grid.EnableHeadersVisualStyles = false;
            this._grid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this._grid.Location = new System.Drawing.Point(0, 0);
            this._grid.MultiSelect = false;
            this._grid.Name = "_grid";
            this._grid.ReadOnly = true;
            this._grid.RowHeadersVisible = false;
            this._grid.RowHeadersWidth = 51;
            this._grid.RowTemplate.Height = 36;
            this._grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._grid.Size = new System.Drawing.Size(1250, 410);
            this._grid.TabIndex = 0;
            // 
            // colAlertId
            // 
            this.colAlertId.HeaderText = "AlertID";
            this.colAlertId.MinimumWidth = 6;
            this.colAlertId.Name = "colAlertId";
            this.colAlertId.ReadOnly = true;
            this.colAlertId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colAlertId.Visible = false;
            // 
            // colProductCode
            // 
            this.colProductCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colProductCode.FillWeight = 10F;
            this.colProductCode.HeaderText = "Mã SP";
            this.colProductCode.MinimumWidth = 6;
            this.colProductCode.Name = "colProductCode";
            this.colProductCode.ReadOnly = true;
            this.colProductCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colProductName
            // 
            this.colProductName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colProductName.FillWeight = 22F;
            this.colProductName.HeaderText = "Tên sản phẩm";
            this.colProductName.MinimumWidth = 6;
            this.colProductName.Name = "colProductName";
            this.colProductName.ReadOnly = true;
            this.colProductName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colAlertType
            // 
            this.colAlertType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colAlertType.DefaultCellStyle = dataGridViewCellStyle10;
            this.colAlertType.FillWeight = 13F;
            this.colAlertType.HeaderText = "Loại cảnh báo";
            this.colAlertType.MinimumWidth = 6;
            this.colAlertType.Name = "colAlertType";
            this.colAlertType.ReadOnly = true;
            this.colAlertType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colReporter
            // 
            this.colReporter.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colReporter.FillWeight = 15F;
            this.colReporter.HeaderText = "Người báo cáo";
            this.colReporter.MinimumWidth = 6;
            this.colReporter.Name = "colReporter";
            this.colReporter.ReadOnly = true;
            this.colReporter.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colCreatedAt
            // 
            this.colCreatedAt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colCreatedAt.FillWeight = 13F;
            this.colCreatedAt.HeaderText = "Thời gian";
            this.colCreatedAt.MinimumWidth = 6;
            this.colCreatedAt.Name = "colCreatedAt";
            this.colCreatedAt.ReadOnly = true;
            this.colCreatedAt.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colStatus
            // 
            this.colStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colStatus.DefaultCellStyle = dataGridViewCellStyle11;
            this.colStatus.FillWeight = 14F;
            this.colStatus.HeaderText = "Trạng thái";
            this.colStatus.MinimumWidth = 6;
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            this.colStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colPlan
            // 
            this.colPlan.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.NullValue = null;
            this.colPlan.DefaultCellStyle = dataGridViewCellStyle12;
            this.colPlan.HeaderText = "Thao tác";
            this.colPlan.MinimumWidth = 6;
            this.colPlan.Name = "colPlan";
            this.colPlan.ReadOnly = true;
            this.colPlan.Width = 56;
            // 
            // colResolve
            // 
            this.colResolve.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.NullValue = null;
            this.colResolve.DefaultCellStyle = dataGridViewCellStyle13;
            this.colResolve.HeaderText = "";
            this.colResolve.MinimumWidth = 6;
            this.colResolve.Name = "colResolve";
            this.colResolve.ReadOnly = true;
            this.colResolve.Width = 56;
            // 
            // _emptyLabel
            // 
            this._emptyLabel.BackColor = System.Drawing.Color.White;
            this._emptyLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._emptyLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._emptyLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._emptyLabel.Location = new System.Drawing.Point(0, 0);
            this._emptyLabel.Name = "_emptyLabel";
            this._emptyLabel.Size = new System.Drawing.Size(1250, 410);
            this._emptyLabel.TabIndex = 1;
            this._emptyLabel.Text = "Chưa có cảnh báo nào";
            this._emptyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._emptyLabel.Visible = false;
            // 
            // _gapHeader
            // 
            this._gapHeader.BackColor = System.Drawing.Color.Transparent;
            this._gapHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this._gapHeader.Location = new System.Drawing.Point(16, 104);
            this._gapHeader.Name = "_gapHeader";
            this._gapHeader.Size = new System.Drawing.Size(1252, 12);
            this._gapHeader.TabIndex = 3;
            // 
            // _gapFilter
            // 
            this._gapFilter.BackColor = System.Drawing.Color.Transparent;
            this._gapFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this._gapFilter.Location = new System.Drawing.Point(16, 176);
            this._gapFilter.Name = "_gapFilter";
            this._gapFilter.Size = new System.Drawing.Size(1252, 12);
            this._gapFilter.TabIndex = 4;
            // 
            // _gapPager
            // 
            this._gapPager.BackColor = System.Drawing.Color.Transparent;
            this._gapPager.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._gapPager.Location = new System.Drawing.Point(16, 600);
            this._gapPager.Name = "_gapPager";
            this._gapPager.Size = new System.Drawing.Size(1252, 12);
            this._gapPager.TabIndex = 5;
            // 
            // _pagerPanel
            // 
            this._pagerPanel.BackColor = System.Drawing.Color.Transparent;
            this._pagerPanel.Controls.Add(this._pagerInner);
            this._pagerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._pagerPanel.Location = new System.Drawing.Point(16, 612);
            this._pagerPanel.Name = "_pagerPanel";
            this._pagerPanel.Size = new System.Drawing.Size(1252, 52);
            this._pagerPanel.TabIndex = 6;
            // 
            // _pagerInner
            // 
            this._pagerInner.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._pagerInner.BackColor = System.Drawing.Color.Transparent;
            this._pagerInner.Controls.Add(this._prevButton);
            this._pagerInner.Controls.Add(this._pageLabel);
            this._pagerInner.Controls.Add(this._nextButton);
            this._pagerInner.Controls.Add(this._pageInfoLabel);
            this._pagerInner.Controls.Add(this._displayLabel);
            this._pagerInner.Controls.Add(this._pageSizeCombo);
            this._pagerInner.Controls.Add(this._rowsLabel);
            this._pagerInner.Location = new System.Drawing.Point(426, 8);
            this._pagerInner.Name = "_pagerInner";
            this._pagerInner.Size = new System.Drawing.Size(400, 36);
            this._pagerInner.TabIndex = 0;
            // 
            // _prevButton
            // 
            this._prevButton.BackColor = System.Drawing.Color.White;
            this._prevButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this._prevButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._prevButton.Font = new System.Drawing.Font("Segoe UI", 12F);
            this._prevButton.Location = new System.Drawing.Point(0, 2);
            this._prevButton.Name = "_prevButton";
            this._prevButton.Size = new System.Drawing.Size(32, 32);
            this._prevButton.TabIndex = 0;
            this._prevButton.Text = "‹";
            this._prevButton.UseVisualStyleBackColor = false;
            // 
            // _pageLabel
            // 
            this._pageLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._pageLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._pageLabel.ForeColor = System.Drawing.Color.White;
            this._pageLabel.Location = new System.Drawing.Point(38, 2);
            this._pageLabel.Name = "_pageLabel";
            this._pageLabel.Size = new System.Drawing.Size(32, 32);
            this._pageLabel.TabIndex = 1;
            this._pageLabel.Text = "1";
            this._pageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _nextButton
            // 
            this._nextButton.BackColor = System.Drawing.Color.White;
            this._nextButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this._nextButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._nextButton.Font = new System.Drawing.Font("Segoe UI", 12F);
            this._nextButton.Location = new System.Drawing.Point(76, 2);
            this._nextButton.Name = "_nextButton";
            this._nextButton.Size = new System.Drawing.Size(32, 32);
            this._nextButton.TabIndex = 2;
            this._nextButton.Text = "›";
            this._nextButton.UseVisualStyleBackColor = false;
            // 
            // _pageInfoLabel
            // 
            this._pageInfoLabel.Location = new System.Drawing.Point(118, 9);
            this._pageInfoLabel.Name = "_pageInfoLabel";
            this._pageInfoLabel.Size = new System.Drawing.Size(92, 20);
            this._pageInfoLabel.TabIndex = 3;
            this._pageInfoLabel.Text = "Trang 1 / 1";
            this._pageInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _displayLabel
            // 
            this._displayLabel.Location = new System.Drawing.Point(214, 9);
            this._displayLabel.Name = "_displayLabel";
            this._displayLabel.Size = new System.Drawing.Size(60, 20);
            this._displayLabel.TabIndex = 4;
            this._displayLabel.Text = "Hiển thị:";
            this._displayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _pageSizeCombo
            // 
            this._pageSizeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._pageSizeCombo.FormattingEnabled = true;
            this._pageSizeCombo.Items.AddRange(new object[] {
            10,
            20,
            50});
            this._pageSizeCombo.Location = new System.Drawing.Point(278, 5);
            this._pageSizeCombo.Name = "_pageSizeCombo";
            this._pageSizeCombo.Size = new System.Drawing.Size(60, 33);
            this._pageSizeCombo.TabIndex = 5;
            // 
            // _rowsLabel
            // 
            this._rowsLabel.Location = new System.Drawing.Point(344, 9);
            this._rowsLabel.Name = "_rowsLabel";
            this._rowsLabel.Size = new System.Drawing.Size(40, 20);
            this._rowsLabel.TabIndex = 6;
            this._rowsLabel.Text = "dòng";
            this._rowsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucStockAlert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.Controls.Add(this._gridCard);
            this.Controls.Add(this._gapPager);
            this.Controls.Add(this._pagerPanel);
            this.Controls.Add(this._gapFilter);
            this.Controls.Add(this._filterCard);
            this.Controls.Add(this._gapHeader);
            this.Controls.Add(this._headerCard);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "ucStockAlert";
            this.Padding = new System.Windows.Forms.Padding(16);
            this.Size = new System.Drawing.Size(1284, 680);
            this._headerCard.ResumeLayout(false);
            this._headerIconPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._headerIcon)).EndInit();
            this._optionsMenu.ResumeLayout(false);
            this._filterCard.ResumeLayout(false);
            this._filterCard.PerformLayout();
            this._gridCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._grid)).EndInit();
            this._pagerPanel.ResumeLayout(false);
            this._pagerInner.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private Panel _headerCard;
        private Panel _headerIconPanel;
        private IconPictureBox _headerIcon;
        private Label _titleLabel;
        private Label _subtitleLabel;
        private Button _optionsButton;
        private ContextMenuStrip _optionsMenu;
        private ToolStripMenuItem _exportCsvItem;
        private ToolStripMenuItem _exportExcelItem;
        private Panel _filterCard;
        private TextBox _searchBox;
        private ComboBox _categoryCombo;
        private Label _fromLabel;
        private DateTimePicker _fromDatePicker;
        private Label _toLabel;
        private DateTimePicker _toDatePicker;
        private LinkLabel _clearFilterLink;
        private Label _totalLabel;
        private Panel _gridCard;
        private DataGridView _grid;
        private DataGridViewTextBoxColumn colAlertId;
        private DataGridViewTextBoxColumn colProductCode;
        private DataGridViewTextBoxColumn colProductName;
        private DataGridViewTextBoxColumn colAlertType;
        private DataGridViewTextBoxColumn colReporter;
        private DataGridViewTextBoxColumn colCreatedAt;
        private DataGridViewTextBoxColumn colStatus;
        private DataGridViewImageColumn colPlan;
        private DataGridViewImageColumn colResolve;
        private Label _emptyLabel;
        private Panel _gapHeader;
        private Panel _gapFilter;
        private Panel _gapPager;
        private Panel _pagerPanel;
        private Panel _pagerInner;
        private Button _prevButton;
        private Label _pageLabel;
        private Button _nextButton;
        private Label _pageInfoLabel;
        private Label _displayLabel;
        private ComboBox _pageSizeCombo;
        private Label _rowsLabel;
    }
}
