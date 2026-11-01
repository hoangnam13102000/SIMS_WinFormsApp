using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace SIMS_WinFormsApp.Views.Warehouse
{
    partial class ucStockDisposalManagement
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            this._toolTip = new System.Windows.Forms.ToolTip(this.components);
            this._clearDateLink = new System.Windows.Forms.LinkLabel();
            this._headerCard = new System.Windows.Forms.Panel();
            this._headerIconPanel = new System.Windows.Forms.Panel();
            this._headerIcon = new FontAwesome.Sharp.IconPictureBox();
            this._titleLabel = new System.Windows.Forms.Label();
            this._subtitleLabel = new System.Windows.Forms.Label();
            this._optionsButton = new FontAwesome.Sharp.IconButton();
            this._createButton = new FontAwesome.Sharp.IconButton();
            this._headerLine = new System.Windows.Forms.Panel();
            this._filterCard = new System.Windows.Forms.Panel();
            this._searchPanel = new System.Windows.Forms.Panel();
            this._searchIcon = new FontAwesome.Sharp.IconPictureBox();
            this._searchBox = new System.Windows.Forms.TextBox();
            this._fromDateLabel = new System.Windows.Forms.Label();
            this._fromDatePicker = new System.Windows.Forms.DateTimePicker();
            this._toDateLabel = new System.Windows.Forms.Label();
            this._toDatePicker = new System.Windows.Forms.DateTimePicker();
            this._totalLabel = new System.Windows.Forms.Label();
            this._gridCard = new System.Windows.Forms.Panel();
            this._grid = new System.Windows.Forms.DataGridView();
            this._emptyLabel = new System.Windows.Forms.Label();
            this._gapHeader = new System.Windows.Forms.Panel();
            this._gapFilter = new System.Windows.Forms.Panel();
            this._gapPager = new System.Windows.Forms.Panel();
            this._pagerCard = new System.Windows.Forms.Panel();
            this._pagerInner = new System.Windows.Forms.Panel();
            this._prevButton = new FontAwesome.Sharp.IconButton();
            this._pageLabel = new System.Windows.Forms.Label();
            this._nextButton = new FontAwesome.Sharp.IconButton();
            this._pagerLine1 = new System.Windows.Forms.Panel();
            this._pageInfoLabel = new System.Windows.Forms.Label();
            this._pagerLine2 = new System.Windows.Forms.Panel();
            this._displayLabel = new System.Windows.Forms.Label();
            this._pageSizeCombo = new System.Windows.Forms.ComboBox();
            this._rowsLabel = new System.Windows.Forms.Label();
            this._searchTimer = new System.Windows.Forms.Timer(this.components);
            this._optionsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._exportCsvItem = new System.Windows.Forms.ToolStripMenuItem();
            this._exportExcelItem = new System.Windows.Forms.ToolStripMenuItem();
            this._menuSeparator = new System.Windows.Forms.ToolStripSeparator();
            this._importItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colDisposalId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCopy = new System.Windows.Forms.DataGridViewImageColumn();
            this.colReason = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItemCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLoss = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCreator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetail = new System.Windows.Forms.DataGridViewImageColumn();
            this._headerCard.SuspendLayout();
            this._headerIconPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._headerIcon)).BeginInit();
            this._filterCard.SuspendLayout();
            this._searchPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._searchIcon)).BeginInit();
            this._gridCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._grid)).BeginInit();
            this._pagerCard.SuspendLayout();
            this._pagerInner.SuspendLayout();
            this._optionsMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // _clearDateLink
            // 
            this._clearDateLink.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this._clearDateLink.AutoSize = true;
            this._clearDateLink.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this._clearDateLink.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this._clearDateLink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this._clearDateLink.Location = new System.Drawing.Point(756, 0);
            this._clearDateLink.Name = "_clearDateLink";
            this._clearDateLink.Size = new System.Drawing.Size(68, 23);
            this._clearDateLink.TabIndex = 5;
            this._clearDateLink.TabStop = true;
            this._clearDateLink.Text = "Xoá lọc";
            this._toolTip.SetToolTip(this._clearDateLink, "Xóa lọc ngày");
            this._clearDateLink.Visible = false;
            // 
            // _headerCard
            // 
            this._headerCard.BackColor = System.Drawing.Color.White;
            this._headerCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._headerCard.Controls.Add(this._headerIconPanel);
            this._headerCard.Controls.Add(this._titleLabel);
            this._headerCard.Controls.Add(this._subtitleLabel);
            this._headerCard.Controls.Add(this._optionsButton);
            this._headerCard.Controls.Add(this._createButton);
            this._headerCard.Controls.Add(this._headerLine);
            this._headerCard.Dock = System.Windows.Forms.DockStyle.Top;
            this._headerCard.Location = new System.Drawing.Point(24, 24);
            this._headerCard.Name = "_headerCard";
            this._headerCard.Size = new System.Drawing.Size(952, 88);
            this._headerCard.TabIndex = 0;
            // 
            // _headerIconPanel
            // 
            this._headerIconPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this._headerIconPanel.Controls.Add(this._headerIcon);
            this._headerIconPanel.Location = new System.Drawing.Point(24, 22);
            this._headerIconPanel.Name = "_headerIconPanel";
            this._headerIconPanel.Size = new System.Drawing.Size(44, 44);
            this._headerIconPanel.TabIndex = 0;
            // 
            // _headerIcon
            // 
            this._headerIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this._headerIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._headerIcon.IconChar = FontAwesome.Sharp.IconChar.TrashAlt;
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
            // _titleLabel
            // 
            this._titleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this._titleLabel.AutoEllipsis = true;
            this._titleLabel.AutoSize = false;
            this._titleLabel.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this._titleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._titleLabel.Location = new System.Drawing.Point(80, 12);
            this._titleLabel.Name = "_titleLabel";
            this._titleLabel.Size = new System.Drawing.Size(544, 36);
            this._titleLabel.TabIndex = 1;
            this._titleLabel.Text = "Tiêu hủy hàng";
            this._titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._titleLabel.UseMnemonic = false;
            // 
            // _subtitleLabel
            // 
            this._subtitleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this._subtitleLabel.AutoEllipsis = true;
            this._subtitleLabel.AutoSize = false;
            this._subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._subtitleLabel.Location = new System.Drawing.Point(80, 52);
            this._subtitleLabel.Name = "_subtitleLabel";
            this._subtitleLabel.Size = new System.Drawing.Size(544, 20);
            this._subtitleLabel.TabIndex = 2;
            this._subtitleLabel.Text = "Hủy hàng theo lô và ghi nhận tổn thất";
            this._subtitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._subtitleLabel.UseMnemonic = false;
            // 
            // _optionsButton
            // 
            this._optionsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._optionsButton.BackColor = System.Drawing.Color.White;
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
            this._optionsButton.Location = new System.Drawing.Point(640, 22);
            this._optionsButton.Name = "_optionsButton";
            this._optionsButton.Size = new System.Drawing.Size(108, 38);
            this._optionsButton.TabIndex = 3;
            this._optionsButton.Text = "Tùy chọn";
            this._optionsButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this._optionsButton.UseVisualStyleBackColor = false;
            // 
            // _createButton
            // 
            this._createButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._createButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._createButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this._createButton.FlatAppearance.BorderSize = 0;
            this._createButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._createButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this._createButton.ForeColor = System.Drawing.Color.White;
            this._createButton.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this._createButton.IconColor = System.Drawing.Color.White;
            this._createButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._createButton.IconSize = 14;
            this._createButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._createButton.Location = new System.Drawing.Point(760, 22);
            this._createButton.Name = "_createButton";
            this._createButton.Size = new System.Drawing.Size(172, 38);
            this._createButton.TabIndex = 4;
            this._createButton.Text = "Lập phiếu tiêu hủy";
            this._createButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._createButton.UseVisualStyleBackColor = false;
            // 
            // _headerLine
            // 
            this._headerLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this._headerLine.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._headerLine.Location = new System.Drawing.Point(0, 79);
            this._headerLine.Name = "_headerLine";
            this._headerLine.Size = new System.Drawing.Size(950, 1);
            this._headerLine.TabIndex = 5;
            // 
            // _filterCard
            // 
            this._filterCard.BackColor = System.Drawing.Color.White;
            this._filterCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._filterCard.Controls.Add(this._searchPanel);
            this._filterCard.Controls.Add(this._fromDateLabel);
            this._filterCard.Controls.Add(this._fromDatePicker);
            this._filterCard.Controls.Add(this._toDateLabel);
            this._filterCard.Controls.Add(this._toDatePicker);
            this._filterCard.Controls.Add(this._clearDateLink);
            this._filterCard.Controls.Add(this._totalLabel);
            this._filterCard.Dock = System.Windows.Forms.DockStyle.Top;
            this._filterCard.Location = new System.Drawing.Point(24, 122);
            this._filterCard.Name = "_filterCard";
            this._filterCard.Size = new System.Drawing.Size(952, 70);
            this._filterCard.TabIndex = 1;
            // 
            // _searchPanel
            // 
            this._searchPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this._searchPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._searchPanel.Controls.Add(this._searchIcon);
            this._searchPanel.Controls.Add(this._searchBox);
            this._searchPanel.Location = new System.Drawing.Point(24, 16);
            this._searchPanel.Name = "_searchPanel";
            this._searchPanel.Size = new System.Drawing.Size(298, 36);
            this._searchPanel.TabIndex = 0;
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
            // _searchBox
            // 
            this._searchBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._searchBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this._searchBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._searchBox.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this._searchBox.Location = new System.Drawing.Point(34, 8);
            this._searchBox.Name = "_searchBox";
            this._searchBox.Size = new System.Drawing.Size(254, 22);
            this._searchBox.TabIndex = 1;
            // 
            // _fromDateLabel
            // 
            this._fromDateLabel.AutoSize = true;
            this._fromDateLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this._fromDateLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._fromDateLabel.Location = new System.Drawing.Point(347, 26);
            this._fromDateLabel.Name = "_fromDateLabel";
            this._fromDateLabel.Size = new System.Drawing.Size(34, 23);
            this._fromDateLabel.TabIndex = 1;
            this._fromDateLabel.Text = "Từ";
            this._fromDateLabel.Click += new System.EventHandler(this._fromDateLabel_Click);
            // 
            // _fromDatePicker
            // 
            this._fromDatePicker.Checked = false;
            this._fromDatePicker.CustomFormat = " dd/MM/yyyy";
            this._fromDatePicker.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this._fromDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._fromDatePicker.Location = new System.Drawing.Point(389, 23);
            this._fromDatePicker.Name = "_fromDatePicker";
            this._fromDatePicker.ShowCheckBox = false;
            this._fromDatePicker.Size = new System.Drawing.Size(146, 29);
            this._fromDatePicker.TabIndex = 2;
            // 
            // _toDateLabel
            // 
            this._toDateLabel.AutoSize = true;
            this._toDateLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this._toDateLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._toDateLabel.Location = new System.Drawing.Point(551, 26);
            this._toDateLabel.Name = "_toDateLabel";
            this._toDateLabel.Size = new System.Drawing.Size(41, 23);
            this._toDateLabel.TabIndex = 3;
            this._toDateLabel.Text = "Đến";
            // 
            // _toDatePicker
            // 
            this._toDatePicker.Checked = false;
            this._toDatePicker.CustomFormat = " dd/MM/yyyy";
            this._toDatePicker.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this._toDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._toDatePicker.Location = new System.Drawing.Point(600, 23);
            this._toDatePicker.Name = "_toDatePicker";
            this._toDatePicker.ShowCheckBox = false;
            this._toDatePicker.Size = new System.Drawing.Size(146, 29);
            this._toDatePicker.TabIndex = 4;
            this._toDatePicker.ValueChanged += new System.EventHandler(this._toDatePicker_ValueChanged);
            // 
            // _totalLabel
            // 
            this._totalLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._totalLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this._totalLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._totalLabel.Location = new System.Drawing.Point(768, 27);
            this._totalLabel.Name = "_totalLabel";
            this._totalLabel.Size = new System.Drawing.Size(180, 22);
            this._totalLabel.TabIndex = 6;
            this._totalLabel.Text = "Tổng cộng: 0 phiếu tiêu hủy";
            this._totalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _gridCard
            // 
            this._gridCard.BackColor = System.Drawing.Color.White;
            this._gridCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._gridCard.Controls.Add(this._grid);
            this._gridCard.Controls.Add(this._emptyLabel);
            this._gridCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridCard.Location = new System.Drawing.Point(24, 208);
            this._gridCard.Name = "_gridCard";
            this._gridCard.Padding = new System.Windows.Forms.Padding(10);
            this._gridCard.Size = new System.Drawing.Size(952, 330);
            this._gridCard.TabIndex = 2;
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
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            dataGridViewCellStyle15.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this._grid.ColumnHeadersHeight = 48;
            this._grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this._grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDisposalId,
            this.colCode,
            this.colCopy,
            this.colReason,
            this.colItemCount,
            this.colLoss,
            this.colCreator,
            this.colDate,
            this.colStatus,
            this.colDetail});
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle21.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle21.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            dataGridViewCellStyle21.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle21.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            dataGridViewCellStyle21.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle21.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._grid.DefaultCellStyle = dataGridViewCellStyle21;
            this._grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grid.EnableHeadersVisualStyles = false;
            this._grid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this._grid.Location = new System.Drawing.Point(10, 10);
            this._grid.MultiSelect = false;
            this._grid.Name = "_grid";
            this._grid.ReadOnly = true;
            this._grid.RowHeadersVisible = false;
            this._grid.RowHeadersWidth = 51;
            this._grid.RowTemplate.Height = 46;
            this._grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._grid.Size = new System.Drawing.Size(930, 308);
            this._grid.TabIndex = 0;
            // 
            // _emptyLabel
            // 
            this._emptyLabel.BackColor = System.Drawing.Color.White;
            this._emptyLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._emptyLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._emptyLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._emptyLabel.Location = new System.Drawing.Point(10, 10);
            this._emptyLabel.Name = "_emptyLabel";
            this._emptyLabel.Size = new System.Drawing.Size(930, 308);
            this._emptyLabel.TabIndex = 1;
            this._emptyLabel.Text = "Chưa có phiếu tiêu hủy nào";
            this._emptyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._emptyLabel.Visible = false;
            // 
            // _gapHeader
            // 
            this._gapHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this._gapHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this._gapHeader.Location = new System.Drawing.Point(24, 106);
            this._gapHeader.Name = "_gapHeader";
            this._gapHeader.Size = new System.Drawing.Size(952, 16);
            this._gapHeader.TabIndex = 3;
            // 
            // _gapFilter
            // 
            this._gapFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this._gapFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this._gapFilter.Location = new System.Drawing.Point(24, 192);
            this._gapFilter.Name = "_gapFilter";
            this._gapFilter.Size = new System.Drawing.Size(952, 16);
            this._gapFilter.TabIndex = 4;
            // 
            // _gapPager
            // 
            this._gapPager.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this._gapPager.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._gapPager.Location = new System.Drawing.Point(24, 538);
            this._gapPager.Name = "_gapPager";
            this._gapPager.Size = new System.Drawing.Size(952, 16);
            this._gapPager.TabIndex = 5;
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
            this._pagerCard.TabIndex = 6;
            // 
            // _pagerInner
            // 
            this._pagerInner.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._pagerInner.BackColor = System.Drawing.Color.White;
            this._pagerInner.Controls.Add(this._prevButton);
            this._pagerInner.Controls.Add(this._pageLabel);
            this._pagerInner.Controls.Add(this._nextButton);
            this._pagerInner.Controls.Add(this._pagerLine1);
            this._pagerInner.Controls.Add(this._pageInfoLabel);
            this._pagerInner.Controls.Add(this._pagerLine2);
            this._pagerInner.Controls.Add(this._displayLabel);
            this._pagerInner.Controls.Add(this._pageSizeCombo);
            this._pagerInner.Controls.Add(this._rowsLabel);
            this._pagerInner.Location = new System.Drawing.Point(276, 12);
            this._pagerInner.Name = "_pagerInner";
            this._pagerInner.Size = new System.Drawing.Size(421, 36);
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
            // _pagerLine1
            // 
            this._pagerLine1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this._pagerLine1.Location = new System.Drawing.Point(122, 6);
            this._pagerLine1.Name = "_pagerLine1";
            this._pagerLine1.Size = new System.Drawing.Size(1, 24);
            this._pagerLine1.TabIndex = 3;
            // 
            // _pageInfoLabel
            // 
            this._pageInfoLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this._pageInfoLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._pageInfoLabel.Location = new System.Drawing.Point(132, 8);
            this._pageInfoLabel.Name = "_pageInfoLabel";
            this._pageInfoLabel.Size = new System.Drawing.Size(84, 20);
            this._pageInfoLabel.TabIndex = 4;
            this._pageInfoLabel.Text = "Trang 1 / 1";
            this._pageInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _pagerLine2
            // 
            this._pagerLine2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this._pagerLine2.Location = new System.Drawing.Point(222, 6);
            this._pagerLine2.Name = "_pagerLine2";
            this._pagerLine2.Size = new System.Drawing.Size(1, 24);
            this._pagerLine2.TabIndex = 5;
            // 
            // _displayLabel
            // 
            this._displayLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this._displayLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._displayLabel.Location = new System.Drawing.Point(232, 8);
            this._displayLabel.Name = "_displayLabel";
            this._displayLabel.Size = new System.Drawing.Size(58, 20);
            this._displayLabel.TabIndex = 6;
            this._displayLabel.Text = "Hiển thị:";
            this._displayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _pageSizeCombo
            // 
            this._pageSizeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._pageSizeCombo.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this._pageSizeCombo.Items.AddRange(new object[] {
            10,
            20,
            50});
            this._pageSizeCombo.Location = new System.Drawing.Point(292, 5);
            this._pageSizeCombo.Name = "_pageSizeCombo";
            this._pageSizeCombo.Size = new System.Drawing.Size(60, 29);
            this._pageSizeCombo.TabIndex = 7;
            // 
            // _rowsLabel
            // 
            this._rowsLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this._rowsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._rowsLabel.Location = new System.Drawing.Point(358, 8);
            this._rowsLabel.Name = "_rowsLabel";
            this._rowsLabel.Size = new System.Drawing.Size(67, 22);
            this._rowsLabel.TabIndex = 8;
            this._rowsLabel.Text = "dòng";
            this._rowsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _searchTimer
            // 
            this._searchTimer.Interval = 300;
            this._searchTimer.Tick += new System.EventHandler(this.SearchTimer_Tick);
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
            this._exportCsvItem.Size = new System.Drawing.Size(158, 28);
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
            // colDisposalId
            // 
            this.colDisposalId.HeaderText = "DisposalID";
            this.colDisposalId.MinimumWidth = 6;
            this.colDisposalId.Name = "colDisposalId";
            this.colDisposalId.ReadOnly = true;
            this.colDisposalId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colDisposalId.Visible = false;
            // 
            // colCode
            // 
            this.colCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colCode.FillWeight = 12F;
            this.colCode.HeaderText = "Mã phiếu";
            this.colCode.MinimumWidth = 6;
            this.colCode.Name = "colCode";
            this.colCode.ReadOnly = true;
            this.colCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colCopy
            // 
            this.colCopy.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle16.NullValue = null;
            this.colCopy.DefaultCellStyle = dataGridViewCellStyle16;
            this.colCopy.HeaderText = "";
            this.colCopy.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.colCopy.MinimumWidth = 6;
            this.colCopy.Name = "colCopy";
            this.colCopy.ReadOnly = true;
            this.colCopy.Width = 36;
            // 
            // colReason
            // 
            this.colReason.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colReason.FillWeight = 13F;
            this.colReason.HeaderText = "Lý do";
            this.colReason.MinimumWidth = 6;
            this.colReason.Name = "colReason";
            this.colReason.ReadOnly = true;
            this.colReason.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colItemCount
            // 
            this.colItemCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colItemCount.DefaultCellStyle = dataGridViewCellStyle17;
            this.colItemCount.FillWeight = 8F;
            this.colItemCount.HeaderText = "Số dòng";
            this.colItemCount.MinimumWidth = 6;
            this.colItemCount.Name = "colItemCount";
            this.colItemCount.ReadOnly = true;
            this.colItemCount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colLoss
            // 
            this.colLoss.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.colLoss.DefaultCellStyle = dataGridViewCellStyle18;
            this.colLoss.FillWeight = 12F;
            this.colLoss.HeaderText = "Tổn thất";
            this.colLoss.MinimumWidth = 6;
            this.colLoss.Name = "colLoss";
            this.colLoss.ReadOnly = true;
            this.colLoss.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colCreator
            // 
            this.colCreator.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colCreator.FillWeight = 15F;
            this.colCreator.HeaderText = "Người lập";
            this.colCreator.MinimumWidth = 6;
            this.colCreator.Name = "colCreator";
            this.colCreator.ReadOnly = true;
            this.colCreator.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colDate
            // 
            this.colDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDate.FillWeight = 12F;
            this.colDate.HeaderText = "Ngày lập";
            this.colDate.MinimumWidth = 6;
            this.colDate.Name = "colDate";
            this.colDate.ReadOnly = true;
            this.colDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colStatus
            // 
            this.colStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colStatus.DefaultCellStyle = dataGridViewCellStyle19;
            this.colStatus.FillWeight = 12F;
            this.colStatus.HeaderText = "Trạng thái";
            this.colStatus.MinimumWidth = 6;
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            this.colStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colDetail
            // 
            this.colDetail.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle20.NullValue = null;
            this.colDetail.DefaultCellStyle = dataGridViewCellStyle20;
            this.colDetail.HeaderText = "Thao tác";
            this.colDetail.MinimumWidth = 6;
            this.colDetail.Name = "colDetail";
            this.colDetail.ReadOnly = true;
            this.colDetail.Width = 90;
            // 
            // ucStockDisposalManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.Controls.Add(this._gridCard);
            this.Controls.Add(this._gapPager);
            this.Controls.Add(this._pagerCard);
            this.Controls.Add(this._gapFilter);
            this.Controls.Add(this._filterCard);
            this.Controls.Add(this._gapHeader);
            this.Controls.Add(this._headerCard);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.Name = "ucStockDisposalManagement";
            this.Padding = new System.Windows.Forms.Padding(24);
            this.Size = new System.Drawing.Size(1000, 640);
            this._headerCard.ResumeLayout(false);
            this._headerIconPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._headerIcon)).EndInit();
            this._filterCard.ResumeLayout(false);
            this._filterCard.PerformLayout();
            this._searchPanel.ResumeLayout(false);
            this._searchPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._searchIcon)).EndInit();
            this._gridCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._grid)).EndInit();
            this._pagerCard.ResumeLayout(false);
            this._pagerInner.ResumeLayout(false);
            this._optionsMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private Panel _headerCard;
        private Panel _headerIconPanel;
        private IconPictureBox _headerIcon;
        private Label _titleLabel;
        private Label _subtitleLabel;
        private IconButton _optionsButton;
        private IconButton _createButton;
        private Panel _headerLine;
        private ContextMenuStrip _optionsMenu;
        private ToolStripMenuItem _exportCsvItem;
        private ToolStripMenuItem _exportExcelItem;
        private ToolStripSeparator _menuSeparator;
        private ToolStripMenuItem _importItem;
        private Panel _filterCard;
        private Panel _searchPanel;
        private IconPictureBox _searchIcon;
        private TextBox _searchBox;
        private Label _fromDateLabel;
        private DateTimePicker _fromDatePicker;
        private Label _toDateLabel;
        private DateTimePicker _toDatePicker;
        private LinkLabel _clearDateLink;
        private Label _totalLabel;
        private Panel _gridCard;
        private DataGridView _grid;
        private Label _emptyLabel;
        private Panel _gapHeader;
        private Panel _gapFilter;
        private Panel _gapPager;
        private Panel _pagerCard;
        private Panel _pagerInner;
        private IconButton _prevButton;
        private Label _pageLabel;
        private IconButton _nextButton;
        private Panel _pagerLine1;
        private Label _pageInfoLabel;
        private Panel _pagerLine2;
        private Label _displayLabel;
        private ComboBox _pageSizeCombo;
        private Label _rowsLabel;
        private Timer _searchTimer;
        private ToolTip _toolTip;
        private DataGridViewTextBoxColumn colDisposalId;
        private DataGridViewTextBoxColumn colCode;
        private DataGridViewImageColumn colCopy;
        private DataGridViewTextBoxColumn colReason;
        private DataGridViewTextBoxColumn colItemCount;
        private DataGridViewTextBoxColumn colLoss;
        private DataGridViewTextBoxColumn colCreator;
        private DataGridViewTextBoxColumn colDate;
        private DataGridViewTextBoxColumn colStatus;
        private DataGridViewImageColumn colDetail;
    }
}
