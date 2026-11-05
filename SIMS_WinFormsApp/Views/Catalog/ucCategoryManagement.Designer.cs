using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace SIMS_WinFormsApp.Views.Catalog
{
    partial class ucCategoryManagement
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel _root;
        private Panel _header;
        private Panel _headerIconPanel;
        private IconPictureBox _headerIcon;
        private Label _titleLabel;
        private Label _subtitleLabel;
        private Panel _toolbar;
        private Label _searchLabel;
        private Label _statusLabel;
        private TextBox _searchTextBox;
        private ComboBox _statusComboBox;
        private Button _addButton;
        private Button _optionsButton;
        private Button _refreshButton;
        private ContextMenuStrip _optionsMenu;
        private ToolStripMenuItem _exportCsvItem;
        private ToolStripMenuItem _exportExcelItem;
        private ToolStripMenuItem _importItem;
        private Panel _gridCard;
        private DataGridView _categoriesGrid;
        private TableLayoutPanel _paging;
        private FlowLayoutPanel _pagingInfo;
        private FlowLayoutPanel _pagingNavigation;
        private Label _resultsLabel;
        private Button _previousButton;
        private Label _pageLabel;
        private Button _nextButton;
        private ComboBox _pageSizeComboBox;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this._root = new System.Windows.Forms.TableLayoutPanel();
            this._header = new System.Windows.Forms.Panel();
            this._headerIconPanel = new System.Windows.Forms.Panel();
            this._headerIcon = new FontAwesome.Sharp.IconPictureBox();
            this._subtitleLabel = new System.Windows.Forms.Label();
            this._titleLabel = new System.Windows.Forms.Label();
            this._addButton = new System.Windows.Forms.Button();
            this._optionsButton = new System.Windows.Forms.Button();
            this._refreshButton = new System.Windows.Forms.Button();
            this._toolbar = new System.Windows.Forms.Panel();
            this._searchLabel = new System.Windows.Forms.Label();
            this._searchTextBox = new System.Windows.Forms.TextBox();
            this._statusLabel = new System.Windows.Forms.Label();
            this._statusComboBox = new System.Windows.Forms.ComboBox();
            this._gridCard = new System.Windows.Forms.Panel();
            this._categoriesGrid = new System.Windows.Forms.DataGridView();
            this._nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Actions = new System.Windows.Forms.DataGridViewImageColumn();
            this._paging = new System.Windows.Forms.TableLayoutPanel();
            this._pagingInfo = new System.Windows.Forms.FlowLayoutPanel();
            this._resultsLabel = new System.Windows.Forms.Label();
            this._pageSizeComboBox = new System.Windows.Forms.ComboBox();
            this._pagingNavigation = new System.Windows.Forms.FlowLayoutPanel();
            this._nextButton = new System.Windows.Forms.Button();
            this._pageLabel = new System.Windows.Forms.Label();
            this._previousButton = new System.Windows.Forms.Button();
            this._optionsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._exportCsvItem = new System.Windows.Forms.ToolStripMenuItem();
            this._exportExcelItem = new System.Windows.Forms.ToolStripMenuItem();
            this._importItem = new System.Windows.Forms.ToolStripMenuItem();
            this._root.SuspendLayout();
            this._header.SuspendLayout();
            this._headerIconPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._headerIcon)).BeginInit();
            this._toolbar.SuspendLayout();
            this._gridCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._categoriesGrid)).BeginInit();
            this._paging.SuspendLayout();
            this._pagingInfo.SuspendLayout();
            this._pagingNavigation.SuspendLayout();
            this._optionsMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // _root
            // 
            this._root.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this._root.ColumnCount = 1;
            this._root.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._root.Controls.Add(this._header, 0, 0);
            this._root.Controls.Add(this._toolbar, 0, 1);
            this._root.Controls.Add(this._gridCard, 0, 2);
            this._root.Controls.Add(this._paging, 0, 3);
            this._root.Dock = System.Windows.Forms.DockStyle.Fill;
            this._root.Location = new System.Drawing.Point(0, 0);
            this._root.Name = "_root";
            this._root.Padding = new System.Windows.Forms.Padding(24);
            this._root.RowCount = 4;
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this._root.Size = new System.Drawing.Size(1100, 720);
            this._root.TabIndex = 0;
            // 
            // _header
            // 
            this._header.BackColor = System.Drawing.Color.White;
            this._header.Controls.Add(this._headerIconPanel);
            this._header.Controls.Add(this._subtitleLabel);
            this._header.Controls.Add(this._titleLabel);
            this._header.Controls.Add(this._addButton);
            this._header.Controls.Add(this._optionsButton);
            this._header.Controls.Add(this._refreshButton);
            this._header.Dock = System.Windows.Forms.DockStyle.Fill;
            this._header.Location = new System.Drawing.Point(24, 24);
            this._header.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this._header.Name = "_header";
            this._header.Size = new System.Drawing.Size(1052, 96);
            this._header.TabIndex = 0;
            // 
            // _headerIconPanel
            // 
            this._headerIconPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(240)))), ((int)(((byte)(254)))));
            this._headerIconPanel.Controls.Add(this._headerIcon);
            this._headerIconPanel.Location = new System.Drawing.Point(20, 20);
            this._headerIconPanel.Name = "_headerIconPanel";
            this._headerIconPanel.Size = new System.Drawing.Size(56, 56);
            this._headerIconPanel.TabIndex = 0;
            // 
            // _headerIcon
            // 
            this._headerIcon.BackColor = System.Drawing.Color.Transparent;
            this._headerIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._headerIcon.IconChar = FontAwesome.Sharp.IconChar.List;
            this._headerIcon.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._headerIcon.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._headerIcon.IconSize = 28;
            this._headerIcon.Location = new System.Drawing.Point(14, 14);
            this._headerIcon.Name = "_headerIcon";
            this._headerIcon.Size = new System.Drawing.Size(28, 28);
            this._headerIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._headerIcon.TabIndex = 0;
            this._headerIcon.TabStop = false;
            // 
            // _subtitleLabel
            // 
            this._subtitleLabel.AutoSize = true;
            this._subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this._subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._subtitleLabel.Location = new System.Drawing.Point(96, 56);
            this._subtitleLabel.Name = "_subtitleLabel";
            this._subtitleLabel.Size = new System.Drawing.Size(423, 25);
            this._subtitleLabel.TabIndex = 1;
            this._subtitleLabel.Text = "Nhóm sản phẩm dùng khi bán hàng và nhập kho";
            // 
            // _titleLabel
            // 
            this._titleLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this._titleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this._titleLabel.Location = new System.Drawing.Point(93, 4);
            this._titleLabel.Name = "_titleLabel";
            this._titleLabel.Size = new System.Drawing.Size(460, 52);
            this._titleLabel.TabIndex = 2;
            this._titleLabel.Text = "Quản lý danh mục";
            this._titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _addButton
            // 
            this._addButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._addButton.FlatAppearance.BorderSize = 0;
            this._addButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this._addButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._addButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._addButton.ForeColor = System.Drawing.Color.White;
            this._addButton.Location = new System.Drawing.Point(559, 34);
            this._addButton.Name = "_addButton";
            this._addButton.Size = new System.Drawing.Size(145, 38);
            this._addButton.TabIndex = 3;
            this._addButton.Text = "Thêm danh mục";
            this._addButton.UseVisualStyleBackColor = false;
            this._addButton.Click += new System.EventHandler(this._addButton_Click);
            // 
            // _optionsButton
            // 
            this._optionsButton.BackColor = System.Drawing.Color.White;
            this._optionsButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this._optionsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._optionsButton.Location = new System.Drawing.Point(710, 34);
            this._optionsButton.Name = "_optionsButton";
            this._optionsButton.Size = new System.Drawing.Size(110, 38);
            this._optionsButton.TabIndex = 4;
            this._optionsButton.Text = "Tùy chọn";
            this._optionsButton.UseVisualStyleBackColor = false;
            this._optionsButton.Click += new System.EventHandler(this._optionsButton_Click);
            // 
            // _refreshButton
            // 
            this._refreshButton.BackColor = System.Drawing.Color.White;
            this._refreshButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this._refreshButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._refreshButton.Location = new System.Drawing.Point(828, 34);
            this._refreshButton.Name = "_refreshButton";
            this._refreshButton.Size = new System.Drawing.Size(92, 38);
            this._refreshButton.TabIndex = 5;
            this._refreshButton.Text = "Làm mới";
            this._refreshButton.UseVisualStyleBackColor = false;
            this._refreshButton.Click += new System.EventHandler(this._refreshButton_Click);
            // 
            // _toolbar
            // 
            this._toolbar.BackColor = System.Drawing.Color.White;
            this._toolbar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._toolbar.Controls.Add(this._searchLabel);
            this._toolbar.Controls.Add(this._searchTextBox);
            this._toolbar.Controls.Add(this._statusLabel);
            this._toolbar.Controls.Add(this._statusComboBox);
            this._toolbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this._toolbar.Location = new System.Drawing.Point(24, 132);
            this._toolbar.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this._toolbar.Name = "_toolbar";
            this._toolbar.Size = new System.Drawing.Size(1052, 70);
            this._toolbar.TabIndex = 1;
            // 
            // _searchLabel
            // 
            this._searchLabel.AutoSize = true;
            this._searchLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._searchLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._searchLabel.Location = new System.Drawing.Point(16, 8);
            this._searchLabel.Name = "_searchLabel";
            this._searchLabel.Size = new System.Drawing.Size(125, 23);
            this._searchLabel.TabIndex = 0;
            this._searchLabel.Text = "Tìm danh mục";
            // 
            // _searchTextBox
            // 
            this._searchTextBox.AccessibleName = "Tìm theo tên danh mục";
            this._searchTextBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._searchTextBox.Location = new System.Drawing.Point(16, 31);
            this._searchTextBox.Name = "_searchTextBox";
            this._searchTextBox.Size = new System.Drawing.Size(500, 34);
            this._searchTextBox.TabIndex = 1;
            // 
            // _statusLabel
            // 
            this._statusLabel.AutoSize = true;
            this._statusLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._statusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._statusLabel.Location = new System.Drawing.Point(540, 8);
            this._statusLabel.Name = "_statusLabel";
            this._statusLabel.Size = new System.Drawing.Size(92, 23);
            this._statusLabel.TabIndex = 2;
            this._statusLabel.Text = "Trạng thái";
            // 
            // _statusComboBox
            // 
            this._statusComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._statusComboBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._statusComboBox.Items.AddRange(new object[] {
            "Tất cả trạng thái",
            "Đang hoạt động",
            "Vô hiệu hóa"});
            this._statusComboBox.Location = new System.Drawing.Point(540, 31);
            this._statusComboBox.Name = "_statusComboBox";
            this._statusComboBox.Size = new System.Drawing.Size(300, 36);
            this._statusComboBox.TabIndex = 3;
            // 
            // _gridCard
            // 
            this._gridCard.BackColor = System.Drawing.Color.White;
            this._gridCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._gridCard.Controls.Add(this._categoriesGrid);
            this._gridCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridCard.Location = new System.Drawing.Point(24, 214);
            this._gridCard.Margin = new System.Windows.Forms.Padding(0);
            this._gridCard.Name = "_gridCard";
            this._gridCard.Size = new System.Drawing.Size(1052, 438);
            this._gridCard.TabIndex = 2;
            // 
            // _categoriesGrid
            // 
            this._categoriesGrid.AllowUserToAddRows = false;
            this._categoriesGrid.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this._categoriesGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this._categoriesGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._categoriesGrid.BackgroundColor = System.Drawing.Color.White;
            this._categoriesGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._categoriesGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this._categoriesGrid.ColumnHeadersHeight = 40;
            this._categoriesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._nameColumn,
            this.ProductCount,
            this.Status,
            this.Actions});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._categoriesGrid.DefaultCellStyle = dataGridViewCellStyle3;
            this._categoriesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._categoriesGrid.EnableHeadersVisualStyles = false;
            this._categoriesGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this._categoriesGrid.Location = new System.Drawing.Point(0, 0);
            this._categoriesGrid.Margin = new System.Windows.Forms.Padding(1);
            this._categoriesGrid.MultiSelect = false;
            this._categoriesGrid.Name = "_categoriesGrid";
            this._categoriesGrid.ReadOnly = true;
            this._categoriesGrid.RowHeadersVisible = false;
            this._categoriesGrid.RowHeadersWidth = 62;
            this._categoriesGrid.RowTemplate.Height = 38;
            this._categoriesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._categoriesGrid.Size = new System.Drawing.Size(1050, 436);
            this._categoriesGrid.TabIndex = 0;
            // 
            // Name
            // 
            this._nameColumn.FillWeight = 44F;
            this._nameColumn.HeaderText = "Tên danh mục";
            this._nameColumn.MinimumWidth = 8;
            this._nameColumn.Name = "Name";
            this._nameColumn.ReadOnly = true;
            // 
            // ProductCount
            // 
            this.ProductCount.FillWeight = 18F;
            this.ProductCount.HeaderText = "Số sản phẩm";
            this.ProductCount.MinimumWidth = 8;
            this.ProductCount.Name = "ProductCount";
            this.ProductCount.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.FillWeight = 20F;
            this.Status.HeaderText = "Trạng thái";
            this.Status.MinimumWidth = 8;
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // Actions
            // 
            this.Actions.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Actions.HeaderText = "Thao tác";
            this.Actions.MinimumWidth = 8;
            this.Actions.Name = "Actions";
            this.Actions.ReadOnly = true;
            this.Actions.ToolTipText = "Xem, sửa hoặc đổi trạng thái danh mục";
            this.Actions.Width = 120;
            // 
            // _paging
            // 
            this._paging.BackColor = System.Drawing.Color.White;
            this._paging.ColumnCount = 2;
            this._paging.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58F));
            this._paging.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42F));
            this._paging.Controls.Add(this._pagingInfo, 0, 0);
            this._paging.Controls.Add(this._pagingNavigation, 1, 0);
            this._paging.Dock = System.Windows.Forms.DockStyle.Fill;
            this._paging.Location = new System.Drawing.Point(24, 652);
            this._paging.Margin = new System.Windows.Forms.Padding(0);
            this._paging.Name = "_paging";
            this._paging.RowCount = 1;
            this._paging.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._paging.Size = new System.Drawing.Size(1052, 44);
            this._paging.TabIndex = 3;
            // 
            // _pagingInfo
            // 
            this._pagingInfo.Controls.Add(this._resultsLabel);
            this._pagingInfo.Controls.Add(this._pageSizeComboBox);
            this._pagingInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pagingInfo.Location = new System.Drawing.Point(3, 3);
            this._pagingInfo.Name = "_pagingInfo";
            this._pagingInfo.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this._pagingInfo.Size = new System.Drawing.Size(604, 38);
            this._pagingInfo.TabIndex = 0;
            this._pagingInfo.WrapContents = false;
            // 
            // _resultsLabel
            // 
            this._resultsLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._resultsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._resultsLabel.Location = new System.Drawing.Point(12, 0);
            this._resultsLabel.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this._resultsLabel.Name = "_resultsLabel";
            this._resultsLabel.Size = new System.Drawing.Size(190, 44);
            this._resultsLabel.TabIndex = 0;
            this._resultsLabel.Text = "Hiển thị 0 danh mục";
            this._resultsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _pageSizeComboBox
            // 
            this._pageSizeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._pageSizeComboBox.Items.AddRange(new object[] {
            "10 dòng",
            "20 dòng",
            "50 dòng"});
            this._pageSizeComboBox.Location = new System.Drawing.Point(212, 8);
            this._pageSizeComboBox.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this._pageSizeComboBox.Name = "_pageSizeComboBox";
            this._pageSizeComboBox.Size = new System.Drawing.Size(94, 28);
            this._pageSizeComboBox.TabIndex = 1;
            // 
            // _pagingNavigation
            // 
            this._pagingNavigation.Controls.Add(this._nextButton);
            this._pagingNavigation.Controls.Add(this._pageLabel);
            this._pagingNavigation.Controls.Add(this._previousButton);
            this._pagingNavigation.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pagingNavigation.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._pagingNavigation.Location = new System.Drawing.Point(613, 3);
            this._pagingNavigation.Name = "_pagingNavigation";
            this._pagingNavigation.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this._pagingNavigation.Size = new System.Drawing.Size(436, 38);
            this._pagingNavigation.TabIndex = 1;
            this._pagingNavigation.WrapContents = false;
            // 
            // _nextButton
            // 
            this._nextButton.AccessibleName = "Trang sau";
            this._nextButton.BackColor = System.Drawing.Color.White;
            this._nextButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this._nextButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this._nextButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._nextButton.Font = new System.Drawing.Font("Segoe UI Symbol", 16F);
            this._nextButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this._nextButton.Location = new System.Drawing.Point(398, 5);
            this._nextButton.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._nextButton.Name = "_nextButton";
            this._nextButton.Size = new System.Drawing.Size(36, 34);
            this._nextButton.TabIndex = 0;
            this._nextButton.UseVisualStyleBackColor = false;
            this._nextButton.Click += new System.EventHandler(this._nextButton_Click);
            // 
            // _pageLabel
            // 
            this._pageLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this._pageLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._pageLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this._pageLabel.Location = new System.Drawing.Point(276, 5);
            this._pageLabel.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this._pageLabel.Name = "_pageLabel";
            this._pageLabel.Size = new System.Drawing.Size(112, 34);
            this._pageLabel.TabIndex = 1;
            this._pageLabel.Text = "Trang 1 / 1";
            this._pageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _previousButton
            // 
            this._previousButton.AccessibleName = "Trang trước";
            this._previousButton.BackColor = System.Drawing.Color.White;
            this._previousButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this._previousButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this._previousButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._previousButton.Font = new System.Drawing.Font("Segoe UI Symbol", 16F);
            this._previousButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this._previousButton.Location = new System.Drawing.Point(230, 5);
            this._previousButton.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._previousButton.Name = "_previousButton";
            this._previousButton.Size = new System.Drawing.Size(36, 34);
            this._previousButton.TabIndex = 2;
            this._previousButton.UseVisualStyleBackColor = false;
            this._previousButton.Click += new System.EventHandler(this._previousButton_Click);
            // 
            // _optionsMenu
            // 
            this._optionsMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this._optionsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._exportCsvItem,
            this._exportExcelItem,
            this._importItem});
            this._optionsMenu.Name = "_optionsMenu";
            this._optionsMenu.Size = new System.Drawing.Size(187, 100);
            // 
            // _exportCsvItem
            // 
            this._exportCsvItem.Name = "_exportCsvItem";
            this._exportCsvItem.Size = new System.Drawing.Size(186, 32);
            this._exportCsvItem.Text = "Xuất CSV";
            this._exportCsvItem.Click += new System.EventHandler(this._exportCsvItem_Click);
            // 
            // _exportExcelItem
            // 
            this._exportExcelItem.Name = "_exportExcelItem";
            this._exportExcelItem.Size = new System.Drawing.Size(186, 32);
            this._exportExcelItem.Text = "Xuất Excel";
            this._exportExcelItem.Click += new System.EventHandler(this._exportExcelItem_Click);
            // 
            // _importItem
            // 
            this._importItem.Name = "_importItem";
            this._importItem.Size = new System.Drawing.Size(186, 32);
            this._importItem.Text = "Nhập dữ liệu";
            this._importItem.Click += new System.EventHandler(this._importItem_Click);
            // 
            // ucCategoryManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.Controls.Add(this._root);
            this.Name = "ucCategoryManagement";
            this.Size = new System.Drawing.Size(1100, 720);
            this._root.ResumeLayout(false);
            this._header.ResumeLayout(false);
            this._header.PerformLayout();
            this._headerIconPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._headerIcon)).EndInit();
            this._toolbar.ResumeLayout(false);
            this._toolbar.PerformLayout();
            this._gridCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._categoriesGrid)).EndInit();
            this._paging.ResumeLayout(false);
            this._pagingInfo.ResumeLayout(false);
            this._pagingNavigation.ResumeLayout(false);
            this._optionsMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private DataGridViewTextBoxColumn _nameColumn;
        private DataGridViewTextBoxColumn ProductCount;
        private DataGridViewTextBoxColumn Status;
        private DataGridViewImageColumn Actions;
    }
}
