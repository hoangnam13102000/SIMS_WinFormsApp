using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Views.Warehouse
{
    partial class ucPurchaseReceiptManagement
    {
        private System.ComponentModel.IContainer components = null;
        private ContextMenuStrip _optionsMenu;
        private ToolStripMenuItem _exportCsvMenuItem;
        private ToolStripMenuItem _exportExcelMenuItem;
        private ToolStripSeparator _optionsSeparator;
        private ToolStripMenuItem _importMenuItem;
        private TableLayoutPanel _root;
        private Panel _headerCard;
        private TableLayoutPanel _headerLayout;
        private TableLayoutPanel _headerLeftLayout;
        private Panel _headerIconCircle;
        private IconPictureBox _titleIcon;
        private TableLayoutPanel _headerCopyLayout;
        private Label _titleLabel;
        private Label _subtitleLabel;
        private FlowLayoutPanel _headerActions;
        private Button _optionsButton;
        private Button _createButton;
        private Panel _filterCard;
        private TableLayoutPanel _filterLayout;
        private Panel _searchContainer;
        private TableLayoutPanel _searchLayout;
        private IconPictureBox _searchIcon;
        private TextBox _searchBox;
        private Button _clearSearchButton;
        private Label _fromDateLabel;
        private DateFilterBox _fromDate;
        private Label _toDateLabel;
        private DateFilterBox _toDate;
        private Button _clearDateLink;
        private Label _totalLabel;
        private Panel _gridCard;
        private DataGridView _grid;
        private DataGridViewTextBoxColumn _receiptIdColumn;
        private DataGridViewTextBoxColumn _codeColumn;
        private DataGridViewTextBoxColumn _supplierColumn;
        private DataGridViewTextBoxColumn _creatorColumn;
        private DataGridViewTextBoxColumn _dateColumn;
        private DataGridViewTextBoxColumn _itemCountColumn;
        private DataGridViewTextBoxColumn _totalColumn;
        private DataGridViewTextBoxColumn _statusColumn;
        private DataGridViewTextBoxColumn _actionColumn;
        private FlowLayoutPanel _pagingPanel;
        private Button _prevButton;
        private Panel _pageNumberBadge;
        private Label _pageNumberLabel;
        private Button _nextButton;
        private Label _pageInfoLabel;
        private Label _displayLabel;
        private ComboBox _pageSizeCombo;
        private Label _rowsLabel;
        private ToolTip _toolTip;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucPurchaseReceiptManagement));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this._optionsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._exportCsvMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._exportExcelMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._optionsSeparator = new System.Windows.Forms.ToolStripSeparator();
            this._importMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._root = new System.Windows.Forms.TableLayoutPanel();
            this._headerCard = new System.Windows.Forms.Panel();
            this._headerLayout = new System.Windows.Forms.TableLayoutPanel();
            this._headerLeftLayout = new System.Windows.Forms.TableLayoutPanel();
            this._headerIconCircle = new System.Windows.Forms.Panel();
            this._titleIcon = new FontAwesome.Sharp.IconPictureBox();
            this._headerCopyLayout = new System.Windows.Forms.TableLayoutPanel();
            this._titleLabel = new System.Windows.Forms.Label();
            this._subtitleLabel = new System.Windows.Forms.Label();
            this._headerActions = new System.Windows.Forms.FlowLayoutPanel();
            this._createButton = new System.Windows.Forms.Button();
            this._optionsButton = new System.Windows.Forms.Button();
            this._filterCard = new System.Windows.Forms.Panel();
            this._filterLayout = new System.Windows.Forms.TableLayoutPanel();
            this._toDateLabel = new System.Windows.Forms.Label();
            this._searchContainer = new System.Windows.Forms.Panel();
            this._searchLayout = new System.Windows.Forms.TableLayoutPanel();
            this._searchIcon = new FontAwesome.Sharp.IconPictureBox();
            this._searchBox = new System.Windows.Forms.TextBox();
            this._clearSearchButton = new System.Windows.Forms.Button();
            this._fromDateLabel = new System.Windows.Forms.Label();
            this._fromDate = new SIMS_WinFormsApp.UI.Controls.DateFilterBox();
            this._toDate = new SIMS_WinFormsApp.UI.Controls.DateFilterBox();
            this._totalLabel = new System.Windows.Forms.Label();
            this._clearDateLink = new System.Windows.Forms.Button();
            this._gridCard = new System.Windows.Forms.Panel();
            this._grid = new System.Windows.Forms.DataGridView();
            this._receiptIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._codeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._supplierColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._creatorColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._dateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._itemCountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._totalColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._statusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._actionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._pagingPanel = new System.Windows.Forms.FlowLayoutPanel();
            this._prevButton = new System.Windows.Forms.Button();
            this._pageNumberBadge = new System.Windows.Forms.Panel();
            this._pageNumberLabel = new System.Windows.Forms.Label();
            this._nextButton = new System.Windows.Forms.Button();
            this._pageInfoLabel = new System.Windows.Forms.Label();
            this._displayLabel = new System.Windows.Forms.Label();
            this._pageSizeCombo = new System.Windows.Forms.ComboBox();
            this._rowsLabel = new System.Windows.Forms.Label();
            this._toolTip = new System.Windows.Forms.ToolTip(this.components);
            this._optionsMenu.SuspendLayout();
            this._root.SuspendLayout();
            this._headerCard.SuspendLayout();
            this._headerLayout.SuspendLayout();
            this._headerLeftLayout.SuspendLayout();
            this._headerIconCircle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._titleIcon)).BeginInit();
            this._headerCopyLayout.SuspendLayout();
            this._headerActions.SuspendLayout();
            this._filterCard.SuspendLayout();
            this._filterLayout.SuspendLayout();
            this._searchContainer.SuspendLayout();
            this._searchLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._searchIcon)).BeginInit();
            this._gridCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._grid)).BeginInit();
            this._pagingPanel.SuspendLayout();
            this._pageNumberBadge.SuspendLayout();
            this.SuspendLayout();
            // 
            // _optionsMenu
            // 
            this._optionsMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this._optionsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._exportCsvMenuItem,
            this._exportExcelMenuItem,
            this._optionsSeparator,
            this._importMenuItem});
            this._optionsMenu.Name = "_optionsMenu";
            this._optionsMenu.Size = new System.Drawing.Size(195, 106);
            // 
            // _exportCsvMenuItem
            // 
            this._exportCsvMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("_exportCsvMenuItem.Image")));
            this._exportCsvMenuItem.Name = "_exportCsvMenuItem";
            this._exportCsvMenuItem.Size = new System.Drawing.Size(194, 32);
            this._exportCsvMenuItem.Text = "Xuất CSV";
            this._exportCsvMenuItem.Click += new System.EventHandler(this.ExportCsvMenuItem_Click);
            // 
            // _exportExcelMenuItem
            // 
            this._exportExcelMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("_exportExcelMenuItem.Image")));
            this._exportExcelMenuItem.Name = "_exportExcelMenuItem";
            this._exportExcelMenuItem.Size = new System.Drawing.Size(194, 32);
            this._exportExcelMenuItem.Text = "Xuất Excel";
            this._exportExcelMenuItem.Click += new System.EventHandler(this.ExportExcelMenuItem_Click);
            // 
            // _optionsSeparator
            // 
            this._optionsSeparator.Name = "_optionsSeparator";
            this._optionsSeparator.Size = new System.Drawing.Size(191, 6);
            // 
            // _importMenuItem
            // 
            this._importMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("_importMenuItem.Image")));
            this._importMenuItem.Name = "_importMenuItem";
            this._importMenuItem.Size = new System.Drawing.Size(194, 32);
            this._importMenuItem.Text = "Nhập dữ liệu";
            this._importMenuItem.Click += new System.EventHandler(this.ImportMenuItem_Click);
            // 
            // _root
            // 
            this._root.ColumnCount = 1;
            this._root.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._root.Controls.Add(this._headerCard, 0, 0);
            this._root.Controls.Add(this._filterCard, 0, 1);
            this._root.Controls.Add(this._gridCard, 0, 2);
            this._root.Controls.Add(this._pagingPanel, 0, 3);
            this._root.Dock = System.Windows.Forms.DockStyle.Fill;
            this._root.Location = new System.Drawing.Point(22, 25);
            this._root.Margin = new System.Windows.Forms.Padding(0);
            this._root.Name = "_root";
            this._root.RowCount = 4;
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this._root.Size = new System.Drawing.Size(1306, 900);
            this._root.TabIndex = 0;
            // 
            // _headerCard
            // 
            this._headerCard.BackColor = System.Drawing.Color.White;
            this._headerCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._headerCard.Controls.Add(this._headerLayout);
            this._headerCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._headerCard.Location = new System.Drawing.Point(0, 0);
            this._headerCard.Margin = new System.Windows.Forms.Padding(0);
            this._headerCard.Name = "_headerCard";
            this._headerCard.Padding = new System.Windows.Forms.Padding(14, 8, 14, 8);
            this._headerCard.Size = new System.Drawing.Size(1306, 125);
            this._headerCard.TabIndex = 0;
            // 
            // _headerLayout
            // 
            this._headerLayout.ColumnCount = 2;
            this._headerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._headerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 484F));
            this._headerLayout.Controls.Add(this._headerLeftLayout, 0, 0);
            this._headerLayout.Controls.Add(this._headerActions, 1, 0);
            this._headerLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._headerLayout.Location = new System.Drawing.Point(14, 8);
            this._headerLayout.Margin = new System.Windows.Forms.Padding(0);
            this._headerLayout.Name = "_headerLayout";
            this._headerLayout.RowCount = 1;
            this._headerLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._headerLayout.Size = new System.Drawing.Size(1276, 107);
            this._headerLayout.TabIndex = 0;
            // 
            // _headerLeftLayout
            // 
            this._headerLeftLayout.ColumnCount = 2;
            this._headerLeftLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 76F));
            this._headerLeftLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._headerLeftLayout.Controls.Add(this._headerIconCircle, 0, 0);
            this._headerLeftLayout.Controls.Add(this._headerCopyLayout, 1, 0);
            this._headerLeftLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._headerLeftLayout.Location = new System.Drawing.Point(0, 0);
            this._headerLeftLayout.Margin = new System.Windows.Forms.Padding(0);
            this._headerLeftLayout.Name = "_headerLeftLayout";
            this._headerLeftLayout.RowCount = 1;
            this._headerLeftLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._headerLeftLayout.Size = new System.Drawing.Size(792, 107);
            this._headerLeftLayout.TabIndex = 0;
            // 
            // _headerIconCircle
            // 
            this._headerIconCircle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._headerIconCircle.BackColor = System.Drawing.Color.Transparent;
            this._headerIconCircle.Controls.Add(this._titleIcon);
            this._headerIconCircle.Location = new System.Drawing.Point(6, 18);
            this._headerIconCircle.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._headerIconCircle.Name = "_headerIconCircle";
            this._headerIconCircle.Size = new System.Drawing.Size(63, 70);
            this._headerIconCircle.TabIndex = 0;
            // 
            // _titleIcon
            // 
            this._titleIcon.BackColor = System.Drawing.Color.Transparent;
            this._titleIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._titleIcon.IconChar = FontAwesome.Sharp.IconChar.FileInvoice;
            this._titleIcon.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._titleIcon.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._titleIcon.IconSize = 29;
            this._titleIcon.Location = new System.Drawing.Point(17, 19);
            this._titleIcon.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._titleIcon.Name = "_titleIcon";
            this._titleIcon.Size = new System.Drawing.Size(29, 32);
            this._titleIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._titleIcon.TabIndex = 0;
            this._titleIcon.TabStop = false;
            // 
            // _headerCopyLayout
            // 
            this._headerCopyLayout.ColumnCount = 1;
            this._headerCopyLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._headerCopyLayout.Controls.Add(this._titleLabel, 0, 0);
            this._headerCopyLayout.Controls.Add(this._subtitleLabel, 0, 1);
            this._headerCopyLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._headerCopyLayout.Location = new System.Drawing.Point(76, 14);
            this._headerCopyLayout.Margin = new System.Windows.Forms.Padding(0, 14, 0, 14);
            this._headerCopyLayout.Name = "_headerCopyLayout";
            this._headerCopyLayout.RowCount = 2;
            this._headerCopyLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this._headerCopyLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this._headerCopyLayout.Size = new System.Drawing.Size(716, 79);
            this._headerCopyLayout.TabIndex = 1;
            // 
            // _titleLabel
            // 
            this._titleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._titleLabel.AutoEllipsis = true;
            this._titleLabel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this._titleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._titleLabel.Location = new System.Drawing.Point(3, 0);
            this._titleLabel.Margin = new System.Windows.Forms.Padding(3, 0, 4, 0);
            this._titleLabel.Name = "_titleLabel";
            this._titleLabel.Size = new System.Drawing.Size(709, 50);
            this._titleLabel.TabIndex = 0;
            this._titleLabel.Text = "Quản lý nhập kho";
            this._titleLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // _subtitleLabel
            // 
            this._subtitleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._subtitleLabel.AutoEllipsis = true;
            this._subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._subtitleLabel.Location = new System.Drawing.Point(3, 55);
            this._subtitleLabel.Margin = new System.Windows.Forms.Padding(3, 5, 4, 0);
            this._subtitleLabel.Name = "_subtitleLabel";
            this._subtitleLabel.Size = new System.Drawing.Size(709, 25);
            this._subtitleLabel.TabIndex = 1;
            this._subtitleLabel.Text = "Lập phiếu nhập và tra cứu lịch sử nhập hàng";
            // 
            // _headerActions
            // 
            this._headerActions.Controls.Add(this._createButton);
            this._headerActions.Controls.Add(this._optionsButton);
            this._headerActions.Dock = System.Windows.Forms.DockStyle.Right;
            this._headerActions.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._headerActions.Location = new System.Drawing.Point(792, 0);
            this._headerActions.Margin = new System.Windows.Forms.Padding(0);
            this._headerActions.Name = "_headerActions";
            this._headerActions.Padding = new System.Windows.Forms.Padding(0, 26, 7, 0);
            this._headerActions.Size = new System.Drawing.Size(484, 107);
            this._headerActions.TabIndex = 1;
            this._headerActions.WrapContents = false;
            // 
            // _createButton
            // 
            this._createButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._createButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._createButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._createButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this._createButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._createButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._createButton.ForeColor = System.Drawing.Color.White;
            this._createButton.Location = new System.Drawing.Point(275, 26);
            this._createButton.Margin = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this._createButton.Name = "_createButton";
            this._createButton.Size = new System.Drawing.Size(202, 55);
            this._createButton.TabIndex = 0;
            this._createButton.Text = "+ Lập phiếu nhập";
            this._createButton.UseVisualStyleBackColor = false;
            this._createButton.Click += new System.EventHandler(this.CreateButton_Click);
            // 
            // _optionsButton
            // 
            this._optionsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._optionsButton.BackColor = System.Drawing.Color.White;
            this._optionsButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._optionsButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this._optionsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._optionsButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._optionsButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._optionsButton.Location = new System.Drawing.Point(115, 26);
            this._optionsButton.Margin = new System.Windows.Forms.Padding(0);
            this._optionsButton.Name = "_optionsButton";
            this._optionsButton.Size = new System.Drawing.Size(146, 55);
            this._optionsButton.TabIndex = 1;
            this._optionsButton.Text = "Tùy chọn  ⌄";
            this._optionsButton.UseVisualStyleBackColor = false;
            this._optionsButton.Click += new System.EventHandler(this.OptionsButton_Click);
            // 
            // _filterCard
            // 
            this._filterCard.BackColor = System.Drawing.Color.White;
            this._filterCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._filterCard.Controls.Add(this._filterLayout);
            this._filterCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._filterCard.Location = new System.Drawing.Point(0, 125);
            this._filterCard.Margin = new System.Windows.Forms.Padding(0);
            this._filterCard.Name = "_filterCard";
            this._filterCard.Padding = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this._filterCard.Size = new System.Drawing.Size(1306, 90);
            this._filterCard.TabIndex = 1;
            // 
            // _filterLayout
            // 
            this._filterLayout.ColumnCount = 7;
            this._filterLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58F));
            this._filterLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this._filterLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 202F));
            this._filterLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this._filterLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 202F));
            this._filterLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this._filterLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42F));
            this._filterLayout.Controls.Add(this._toDateLabel, 3, 0);
            this._filterLayout.Controls.Add(this._searchContainer, 0, 0);
            this._filterLayout.Controls.Add(this._fromDateLabel, 1, 0);
            this._filterLayout.Controls.Add(this._fromDate, 2, 0);
            this._filterLayout.Controls.Add(this._toDate, 4, 0);
            this._filterLayout.Controls.Add(this._totalLabel, 6, 0);
            this._filterLayout.Controls.Add(this._clearDateLink, 5, 0);
            this._filterLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._filterLayout.Location = new System.Drawing.Point(9, 10);
            this._filterLayout.Margin = new System.Windows.Forms.Padding(0);
            this._filterLayout.Name = "_filterLayout";
            this._filterLayout.RowCount = 1;
            this._filterLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._filterLayout.Size = new System.Drawing.Size(1286, 68);
            this._filterLayout.TabIndex = 0;
            // 
            // _toDateLabel
            // 
            this._toDateLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this._toDateLabel.AutoSize = true;
            this._toDateLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._toDateLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._toDateLabel.Location = new System.Drawing.Point(698, 9);
            this._toDateLabel.Name = "_toDateLabel";
            this._toDateLabel.Size = new System.Drawing.Size(34, 50);
            this._toDateLabel.TabIndex = 2;
            this._toDateLabel.Text = "Đến";
            this._toDateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _searchContainer
            // 
            this._searchContainer.BackColor = System.Drawing.Color.White;
            this._searchContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._searchContainer.Controls.Add(this._searchLayout);
            this._searchContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._searchContainer.Location = new System.Drawing.Point(2, 9);
            this._searchContainer.Margin = new System.Windows.Forms.Padding(2, 9, 9, 9);
            this._searchContainer.Name = "_searchContainer";
            this._searchContainer.Size = new System.Drawing.Size(419, 50);
            this._searchContainer.TabIndex = 0;
            // 
            // _searchLayout
            // 
            this._searchLayout.ColumnCount = 3;
            this._searchLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this._searchLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._searchLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this._searchLayout.Controls.Add(this._searchIcon, 0, 0);
            this._searchLayout.Controls.Add(this._searchBox, 1, 0);
            this._searchLayout.Controls.Add(this._clearSearchButton, 2, 0);
            this._searchLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._searchLayout.Location = new System.Drawing.Point(0, 0);
            this._searchLayout.Margin = new System.Windows.Forms.Padding(0);
            this._searchLayout.Name = "_searchLayout";
            this._searchLayout.RowCount = 1;
            this._searchLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._searchLayout.Size = new System.Drawing.Size(417, 48);
            this._searchLayout.TabIndex = 0;
            // 
            // _searchIcon
            // 
            this._searchIcon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._searchIcon.BackColor = System.Drawing.Color.Transparent;
            this._searchIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._searchIcon.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this._searchIcon.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._searchIcon.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._searchIcon.IconSize = 20;
            this._searchIcon.Location = new System.Drawing.Point(11, 13);
            this._searchIcon.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._searchIcon.Name = "_searchIcon";
            this._searchIcon.Size = new System.Drawing.Size(20, 22);
            this._searchIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._searchIcon.TabIndex = 0;
            this._searchIcon.TabStop = false;
            // 
            // _searchBox
            // 
            this._searchBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._searchBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._searchBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._searchBox.Location = new System.Drawing.Point(43, 2);
            this._searchBox.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this._searchBox.Name = "_searchBox";
            this._searchBox.Size = new System.Drawing.Size(340, 24);
            this._searchBox.TabIndex = 1;
            // 
            // _clearSearchButton
            // 
            this._clearSearchButton.BackColor = System.Drawing.Color.White;
            this._clearSearchButton.FlatAppearance.BorderSize = 0;
            this._clearSearchButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this._clearSearchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._clearSearchButton.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._clearSearchButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._clearSearchButton.Location = new System.Drawing.Point(383, 0);
            this._clearSearchButton.Margin = new System.Windows.Forms.Padding(0);
            this._clearSearchButton.Name = "_clearSearchButton";
            this._clearSearchButton.Size = new System.Drawing.Size(34, 38);
            this._clearSearchButton.TabIndex = 2;
            this._clearSearchButton.Text = "✕";
            this._clearSearchButton.UseVisualStyleBackColor = false;
            this._clearSearchButton.Visible = false;
            this._clearSearchButton.Click += new System.EventHandler(this.ClearSearchButton_Click);
            // 
            // _fromDateLabel
            // 
            this._fromDateLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this._fromDateLabel.AutoSize = true;
            this._fromDateLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._fromDateLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._fromDateLabel.Location = new System.Drawing.Point(451, 21);
            this._fromDateLabel.Name = "_fromDateLabel";
            this._fromDateLabel.Size = new System.Drawing.Size(32, 25);
            this._fromDateLabel.TabIndex = 1;
            this._fromDateLabel.Text = "Từ";
            this._fromDateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _fromDate
            // 
            this._fromDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._fromDate.Location = new System.Drawing.Point(489, 11);
            this._fromDate.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this._fromDate.MinimumSize = new System.Drawing.Size(112, 45);
            this._fromDate.Name = "_fromDate";
            this._fromDate.Size = new System.Drawing.Size(199, 45);
            this._fromDate.TabIndex = 0;
            this._fromDate.Value = null;
            // 
            // _toDate
            // 
            this._toDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._toDate.Location = new System.Drawing.Point(738, 11);
            this._toDate.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this._toDate.MinimumSize = new System.Drawing.Size(112, 45);
            this._toDate.Name = "_toDate";
            this._toDate.Size = new System.Drawing.Size(199, 45);
            this._toDate.TabIndex = 1;
            this._toDate.Value = null;
            // 
            // _totalLabel
            // 
            this._totalLabel.AutoSize = true;
            this._totalLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this._totalLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._totalLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._totalLabel.Location = new System.Drawing.Point(1037, 0);
            this._totalLabel.Name = "_totalLabel";
            this._totalLabel.Size = new System.Drawing.Size(246, 68);
            this._totalLabel.TabIndex = 4;
            this._totalLabel.Text = "Tổng cộng: 0 phiếu nhập kho";
            this._totalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _clearDateLink
            // 
            this._clearDateLink.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._clearDateLink.BackColor = System.Drawing.Color.White;
            this._clearDateLink.FlatAppearance.BorderSize = 0;
            this._clearDateLink.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this._clearDateLink.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._clearDateLink.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._clearDateLink.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._clearDateLink.Location = new System.Drawing.Point(940, 16);
            this._clearDateLink.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._clearDateLink.Name = "_clearDateLink";
            this._clearDateLink.Size = new System.Drawing.Size(29, 35);
            this._clearDateLink.TabIndex = 3;
            this._clearDateLink.Text = "✕";
            this._toolTip.SetToolTip(this._clearDateLink, "Xóa lọc ngày");
            this._clearDateLink.UseVisualStyleBackColor = false;
            this._clearDateLink.Visible = false;
            this._clearDateLink.Click += new System.EventHandler(this.ClearDateLink_Click);
            // 
            // _gridCard
            // 
            this._gridCard.BackColor = System.Drawing.Color.White;
            this._gridCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._gridCard.Controls.Add(this._grid);
            this._gridCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridCard.Location = new System.Drawing.Point(0, 215);
            this._gridCard.Margin = new System.Windows.Forms.Padding(0);
            this._gridCard.Name = "_gridCard";
            this._gridCard.Size = new System.Drawing.Size(1306, 605);
            this._gridCard.TabIndex = 2;
            // 
            // _grid
            // 
            this._grid.AllowUserToAddRows = false;
            this._grid.AllowUserToDeleteRows = false;
            this._grid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this._grid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this._grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._grid.BackgroundColor = System.Drawing.Color.White;
            this._grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._grid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this._grid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this._grid.ColumnHeadersHeight = 44;
            this._grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this._grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._receiptIdColumn,
            this._codeColumn,
            this._supplierColumn,
            this._creatorColumn,
            this._dateColumn,
            this._itemCountColumn,
            this._totalColumn,
            this._statusColumn,
            this._actionColumn});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._grid.DefaultCellStyle = dataGridViewCellStyle6;
            this._grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grid.EnableHeadersVisualStyles = false;
            this._grid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this._grid.Location = new System.Drawing.Point(0, 0);
            this._grid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._grid.MultiSelect = false;
            this._grid.Name = "_grid";
            this._grid.ReadOnly = true;
            this._grid.RowHeadersVisible = false;
            this._grid.RowHeadersWidth = 51;
            this._grid.RowTemplate.Height = 44;
            this._grid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._grid.Size = new System.Drawing.Size(1304, 603);
            this._grid.TabIndex = 0;
            this._grid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellDoubleClick);
            // 
            // _receiptIdColumn
            // 
            this._receiptIdColumn.HeaderText = "ReceiptID";
            this._receiptIdColumn.MinimumWidth = 6;
            this._receiptIdColumn.Name = "_receiptIdColumn";
            this._receiptIdColumn.ReadOnly = true;
            this._receiptIdColumn.Visible = false;
            // 
            // _codeColumn
            // 
            this._codeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this._codeColumn.FillWeight = 15F;
            this._codeColumn.HeaderText = "Mã phiếu";
            this._codeColumn.MinimumWidth = 125;
            this._codeColumn.Name = "_codeColumn";
            this._codeColumn.ReadOnly = true;
            // 
            // _supplierColumn
            // 
            this._supplierColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this._supplierColumn.FillWeight = 20F;
            this._supplierColumn.HeaderText = "Nhà cung cấp";
            this._supplierColumn.MinimumWidth = 145;
            this._supplierColumn.Name = "_supplierColumn";
            this._supplierColumn.ReadOnly = true;
            // 
            // _creatorColumn
            // 
            this._creatorColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this._creatorColumn.FillWeight = 15F;
            this._creatorColumn.HeaderText = "Người tạo";
            this._creatorColumn.MinimumWidth = 115;
            this._creatorColumn.Name = "_creatorColumn";
            this._creatorColumn.ReadOnly = true;
            // 
            // _dateColumn
            // 
            this._dateColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Format = "dd/MM/yyyy";
            dataGridViewCellStyle3.FormatProvider = new System.Globalization.CultureInfo("vi-VN");
            this._dateColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this._dateColumn.FillWeight = 12F;
            this._dateColumn.HeaderText = "Ngày tạo";
            this._dateColumn.MinimumWidth = 95;
            this._dateColumn.Name = "_dateColumn";
            this._dateColumn.ReadOnly = true;
            // 
            // _itemCountColumn
            // 
            this._itemCountColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this._itemCountColumn.FillWeight = 8F;
            this._itemCountColumn.HeaderText = "Số SP";
            this._itemCountColumn.MinimumWidth = 65;
            this._itemCountColumn.Name = "_itemCountColumn";
            this._itemCountColumn.ReadOnly = true;
            // 
            // _totalColumn
            // 
            this._totalColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Format = "N0";
            dataGridViewCellStyle4.FormatProvider = new System.Globalization.CultureInfo("vi-VN");
            this._totalColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this._totalColumn.FillWeight = 14F;
            this._totalColumn.HeaderText = "Tổng tiền";
            this._totalColumn.MinimumWidth = 105;
            this._totalColumn.Name = "_totalColumn";
            this._totalColumn.ReadOnly = true;
            // 
            // _statusColumn
            // 
            this._statusColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this._statusColumn.FillWeight = 10F;
            this._statusColumn.HeaderText = "Trạng thái";
            this._statusColumn.MinimumWidth = 95;
            this._statusColumn.Name = "_statusColumn";
            this._statusColumn.ReadOnly = true;
            // 
            // _actionColumn
            // 
            this._actionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this._actionColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this._actionColumn.FillWeight = 6F;
            this._actionColumn.HeaderText = "Thao tác";
            this._actionColumn.MinimumWidth = 65;
            this._actionColumn.Name = "_actionColumn";
            this._actionColumn.ReadOnly = true;
            this._actionColumn.ToolTipText = "Xem chi tiết phiếu nhập";
            // 
            // _pagingPanel
            // 
            this._pagingPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._pagingPanel.AutoSize = true;
            this._pagingPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._pagingPanel.Controls.Add(this._prevButton);
            this._pagingPanel.Controls.Add(this._pageNumberBadge);
            this._pagingPanel.Controls.Add(this._nextButton);
            this._pagingPanel.Controls.Add(this._pageInfoLabel);
            this._pagingPanel.Controls.Add(this._displayLabel);
            this._pagingPanel.Controls.Add(this._pageSizeCombo);
            this._pagingPanel.Controls.Add(this._rowsLabel);
            this._pagingPanel.Location = new System.Drawing.Point(414, 839);
            this._pagingPanel.Margin = new System.Windows.Forms.Padding(0);
            this._pagingPanel.Name = "_pagingPanel";
            this._pagingPanel.Size = new System.Drawing.Size(477, 42);
            this._pagingPanel.TabIndex = 3;
            this._pagingPanel.WrapContents = false;
            // 
            // _prevButton
            // 
            this._prevButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this._prevButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._prevButton.Font = new System.Drawing.Font("Segoe UI", 16F);
            this._prevButton.Location = new System.Drawing.Point(0, 0);
            this._prevButton.Margin = new System.Windows.Forms.Padding(0, 0, 7, 0);
            this._prevButton.Name = "_prevButton";
            this._prevButton.Size = new System.Drawing.Size(38, 42);
            this._prevButton.TabIndex = 0;
            this._prevButton.Text = "‹";
            this._prevButton.UseVisualStyleBackColor = true;
            this._prevButton.Click += new System.EventHandler(this.PreviousButton_Click);
            // 
            // _pageNumberBadge
            // 
            this._pageNumberBadge.BackColor = System.Drawing.Color.Transparent;
            this._pageNumberBadge.Controls.Add(this._pageNumberLabel);
            this._pageNumberBadge.Location = new System.Drawing.Point(45, 0);
            this._pageNumberBadge.Margin = new System.Windows.Forms.Padding(0, 0, 7, 0);
            this._pageNumberBadge.Name = "_pageNumberBadge";
            this._pageNumberBadge.Size = new System.Drawing.Size(38, 42);
            this._pageNumberBadge.TabIndex = 1;
            // 
            // _pageNumberLabel
            // 
            this._pageNumberLabel.BackColor = System.Drawing.Color.Transparent;
            this._pageNumberLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pageNumberLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._pageNumberLabel.ForeColor = System.Drawing.Color.White;
            this._pageNumberLabel.Location = new System.Drawing.Point(0, 0);
            this._pageNumberLabel.Name = "_pageNumberLabel";
            this._pageNumberLabel.Size = new System.Drawing.Size(38, 42);
            this._pageNumberLabel.TabIndex = 0;
            this._pageNumberLabel.Text = "1";
            this._pageNumberLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _nextButton
            // 
            this._nextButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this._nextButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._nextButton.Font = new System.Drawing.Font("Segoe UI", 16F);
            this._nextButton.Location = new System.Drawing.Point(90, 0);
            this._nextButton.Margin = new System.Windows.Forms.Padding(0, 0, 14, 0);
            this._nextButton.Name = "_nextButton";
            this._nextButton.Size = new System.Drawing.Size(38, 42);
            this._nextButton.TabIndex = 2;
            this._nextButton.Text = "›";
            this._nextButton.UseVisualStyleBackColor = true;
            this._nextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // _pageInfoLabel
            // 
            this._pageInfoLabel.AutoSize = true;
            this._pageInfoLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._pageInfoLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._pageInfoLabel.Location = new System.Drawing.Point(142, 10);
            this._pageInfoLabel.Margin = new System.Windows.Forms.Padding(0, 10, 20, 0);
            this._pageInfoLabel.Name = "_pageInfoLabel";
            this._pageInfoLabel.Size = new System.Drawing.Size(97, 25);
            this._pageInfoLabel.TabIndex = 3;
            this._pageInfoLabel.Text = "Trang 1 / 1";
            this._pageInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _displayLabel
            // 
            this._displayLabel.AutoSize = true;
            this._displayLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._displayLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._displayLabel.Location = new System.Drawing.Point(259, 10);
            this._displayLabel.Margin = new System.Windows.Forms.Padding(0, 10, 7, 0);
            this._displayLabel.Name = "_displayLabel";
            this._displayLabel.Size = new System.Drawing.Size(77, 25);
            this._displayLabel.TabIndex = 4;
            this._displayLabel.Text = "Hiển thị:";
            this._displayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _pageSizeCombo
            // 
            this._pageSizeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._pageSizeCombo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._pageSizeCombo.Items.AddRange(new object[] {
            10,
            20,
            50});
            this._pageSizeCombo.Location = new System.Drawing.Point(343, 2);
            this._pageSizeCombo.Margin = new System.Windows.Forms.Padding(0, 2, 7, 0);
            this._pageSizeCombo.Name = "_pageSizeCombo";
            this._pageSizeCombo.Size = new System.Drawing.Size(72, 33);
            this._pageSizeCombo.TabIndex = 5;
            // 
            // _rowsLabel
            // 
            this._rowsLabel.AutoSize = true;
            this._rowsLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._rowsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._rowsLabel.Location = new System.Drawing.Point(422, 10);
            this._rowsLabel.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this._rowsLabel.Name = "_rowsLabel";
            this._rowsLabel.Size = new System.Drawing.Size(55, 25);
            this._rowsLabel.TabIndex = 6;
            this._rowsLabel.Text = "dòng";
            this._rowsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _toolTip
            // 
            this._toolTip.AutoPopDelay = 5000;
            this._toolTip.InitialDelay = 500;
            this._toolTip.ReshowDelay = 100;
            // 
            // ucPurchaseReceiptManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.Controls.Add(this._root);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ucPurchaseReceiptManagement";
            this.Padding = new System.Windows.Forms.Padding(22, 25, 22, 25);
            this.Size = new System.Drawing.Size(1350, 950);
            this._optionsMenu.ResumeLayout(false);
            this._root.ResumeLayout(false);
            this._root.PerformLayout();
            this._headerCard.ResumeLayout(false);
            this._headerLayout.ResumeLayout(false);
            this._headerLeftLayout.ResumeLayout(false);
            this._headerIconCircle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._titleIcon)).EndInit();
            this._headerCopyLayout.ResumeLayout(false);
            this._headerActions.ResumeLayout(false);
            this._filterCard.ResumeLayout(false);
            this._filterLayout.ResumeLayout(false);
            this._filterLayout.PerformLayout();
            this._searchContainer.ResumeLayout(false);
            this._searchLayout.ResumeLayout(false);
            this._searchLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._searchIcon)).EndInit();
            this._gridCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._grid)).EndInit();
            this._pagingPanel.ResumeLayout(false);
            this._pagingPanel.PerformLayout();
            this._pageNumberBadge.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}