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
            this._root = new TableLayoutPanel();
            this._headerCard = new Panel();
            this._headerLayout = new TableLayoutPanel();
            this._headerLeftLayout = new TableLayoutPanel();
            this._headerIconCircle = new Panel();
            this._titleIcon = new IconPictureBox();
            this._headerCopyLayout = new TableLayoutPanel();
            this._titleLabel = new Label();
            this._subtitleLabel = new Label();
            this._headerActions = new FlowLayoutPanel();
            this._optionsButton = new Button();
            this._createButton = new Button();
            this._filterCard = new Panel();
            this._filterLayout = new TableLayoutPanel();
            this._searchContainer = new Panel();
            this._searchLayout = new TableLayoutPanel();
            this._searchIcon = new IconPictureBox();
            this._searchBox = new TextBox();
            this._clearSearchButton = new Button();
            this._fromDateLabel = new Label();
            this._fromDate = new DateFilterBox();
            this._toDateLabel = new Label();
            this._toDate = new DateFilterBox();
            this._clearDateLink = new Button();
            this._totalLabel = new Label();
            this._gridCard = new Panel();
            this._grid = new DataGridView();
            this._receiptIdColumn = new DataGridViewTextBoxColumn();
            this._codeColumn = new DataGridViewTextBoxColumn();
            this._supplierColumn = new DataGridViewTextBoxColumn();
            this._creatorColumn = new DataGridViewTextBoxColumn();
            this._dateColumn = new DataGridViewTextBoxColumn();
            this._itemCountColumn = new DataGridViewTextBoxColumn();
            this._totalColumn = new DataGridViewTextBoxColumn();
            this._statusColumn = new DataGridViewTextBoxColumn();
            this._actionColumn = new DataGridViewTextBoxColumn();
            this._pagingPanel = new FlowLayoutPanel();
            this._prevButton = new Button();
            this._pageNumberBadge = new Panel();
            this._pageNumberLabel = new Label();
            this._nextButton = new Button();
            this._pageInfoLabel = new Label();
            this._displayLabel = new Label();
            this._pageSizeCombo = new ComboBox();
            this._rowsLabel = new Label();
            this._toolTip = new ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this._titleIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._searchIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._grid)).BeginInit();
            this._root.SuspendLayout();
            this._headerCard.SuspendLayout();
            this._headerLayout.SuspendLayout();
            this._headerLeftLayout.SuspendLayout();
            this._headerIconCircle.SuspendLayout();
            this._headerCopyLayout.SuspendLayout();
            this._headerActions.SuspendLayout();
            this._filterCard.SuspendLayout();
            this._filterLayout.SuspendLayout();
            this._searchContainer.SuspendLayout();
            this._searchLayout.SuspendLayout();
            this._gridCard.SuspendLayout();
            this._pagingPanel.SuspendLayout();
            this._pageNumberBadge.SuspendLayout();
            this.SuspendLayout();
            //
            // _root
            //
            this._root.ColumnCount = 1;
            this._root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this._root.Controls.Add(this._headerCard, 0, 0);
            this._root.Controls.Add(this._filterCard, 0, 1);
            this._root.Controls.Add(this._gridCard, 0, 2);
            this._root.Controls.Add(this._pagingPanel, 0, 3);
            this._root.Dock = DockStyle.Fill;
            this._root.Margin = Padding.Empty;
            this._root.Name = "_root";
            this._root.RowCount = 4;
            this._root.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            this._root.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
            this._root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this._root.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F));
            //
            // _headerCard
            //
            this._headerCard.BackColor = Color.White;
            this._headerCard.BorderStyle = BorderStyle.FixedSingle;
            this._headerCard.Controls.Add(this._headerLayout);
            this._headerCard.Dock = DockStyle.Fill;
            this._headerCard.Margin = Padding.Empty;
            this._headerCard.Padding = new Padding(12, 6, 12, 6);
            this._headerCard.Name = "_headerCard";
            //
            // _headerLayout
            //
            this._headerLayout.ColumnCount = 2;
            this._headerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this._headerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 340F));
            this._headerLayout.Controls.Add(this._headerLeftLayout, 0, 0);
            this._headerLayout.Controls.Add(this._headerActions, 1, 0);
            this._headerLayout.Dock = DockStyle.Fill;
            this._headerLayout.Margin = Padding.Empty;
            this._headerLayout.RowCount = 1;
            this._headerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this._headerLayout.Name = "_headerLayout";
            //
            // _headerLeftLayout
            //
            this._headerLeftLayout.ColumnCount = 2;
            this._headerLeftLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 68F));
            this._headerLeftLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this._headerLeftLayout.Controls.Add(this._headerIconCircle, 0, 0);
            this._headerLeftLayout.Controls.Add(this._headerCopyLayout, 1, 0);
            this._headerLeftLayout.Dock = DockStyle.Fill;
            this._headerLeftLayout.Margin = Padding.Empty;
            this._headerLeftLayout.RowCount = 1;
            this._headerLeftLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this._headerLeftLayout.Name = "_headerLeftLayout";
            //
            // _headerIconCircle
            //
            this._headerIconCircle.Anchor = AnchorStyles.None;
            this._headerIconCircle.BackColor = Color.Transparent;
            this._headerIconCircle.Controls.Add(this._titleIcon);
            this._headerIconCircle.Location = new Point(6, 16);
            this._headerIconCircle.Name = "_headerIconCircle";
            this._headerIconCircle.Size = new Size(56, 56);
            //
            // _titleIcon
            //
            this._titleIcon.BackColor = Color.Transparent;
            this._titleIcon.ForeColor = Color.FromArgb(37, 99, 235);
            this._titleIcon.IconChar = IconChar.FileInvoice;
            this._titleIcon.IconColor = Color.FromArgb(37, 99, 235);
            this._titleIcon.IconSize = 26;
            this._titleIcon.Location = new Point(15, 15);
            this._titleIcon.Name = "_titleIcon";
            this._titleIcon.Size = new Size(26, 26);
            this._titleIcon.SizeMode = PictureBoxSizeMode.CenterImage;
            //
            // _headerCopyLayout
            //
            this._headerCopyLayout.ColumnCount = 1;
            this._headerCopyLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this._headerCopyLayout.Controls.Add(this._titleLabel, 0, 0);
            this._headerCopyLayout.Controls.Add(this._subtitleLabel, 0, 1);
            this._headerCopyLayout.Dock = DockStyle.Fill;
            this._headerCopyLayout.Margin = new Padding(0, 12, 0, 12);
            this._headerCopyLayout.RowCount = 2;
            this._headerCopyLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 54F));
            this._headerCopyLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 46F));
            this._headerCopyLayout.Name = "_headerCopyLayout";
            //
            // _titleLabel
            //
            this._titleLabel.Dock = DockStyle.Fill;
            this._titleLabel.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this._titleLabel.ForeColor = Color.FromArgb(15, 23, 42);
            this._titleLabel.Name = "_titleLabel";
            this._titleLabel.Text = "Quản lý nhập kho";
            this._titleLabel.TextAlign = ContentAlignment.BottomLeft;
            //
            // _subtitleLabel
            //
            this._subtitleLabel.Dock = DockStyle.Fill;
            this._subtitleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this._subtitleLabel.ForeColor = Color.FromArgb(100, 116, 139);
            this._subtitleLabel.Name = "_subtitleLabel";
            this._subtitleLabel.Text = "Lập phiếu nhập nhiều sản phẩm và tra cứu lịch sử theo nhà cung cấp";
            this._subtitleLabel.TextAlign = ContentAlignment.TopLeft;
            //
            // _headerActions
            //
            this._headerActions.Controls.Add(this._createButton);
            this._headerActions.Controls.Add(this._optionsButton);
            this._headerActions.Dock = DockStyle.Right;
            this._headerActions.FlowDirection = FlowDirection.RightToLeft;
            this._headerActions.Margin = Padding.Empty;
            this._headerActions.Name = "_headerActions";
            this._headerActions.Padding = new Padding(0, 26, 8, 0);
            this._headerActions.Size = new Size(340, 86);
            this._headerActions.WrapContents = false;
            //
            // _optionsButton
            //
            this._optionsButton.BackColor = Color.White;
            this._optionsButton.FlatAppearance.BorderColor = Color.FromArgb(37, 99, 235);
            this._optionsButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(239, 246, 255);
            this._optionsButton.FlatStyle = FlatStyle.Flat;
            this._optionsButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this._optionsButton.ForeColor = Color.FromArgb(37, 99, 235);
            this._optionsButton.Margin = new Padding(10, 0, 0, 0);
            this._optionsButton.Name = "_optionsButton";
            this._optionsButton.Size = new Size(130, 44);
            this._optionsButton.Text = "Tùy chọn  ⌄";
            this._optionsButton.UseVisualStyleBackColor = false;
            //
            // _createButton
            //
            this._createButton.BackColor = Color.FromArgb(37, 99, 235);
            this._createButton.FlatAppearance.BorderColor = Color.FromArgb(37, 99, 235);
            this._createButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(29, 78, 216);
            this._createButton.FlatStyle = FlatStyle.Flat;
            this._createButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this._createButton.ForeColor = Color.White;
            this._createButton.Margin = Padding.Empty;
            this._createButton.Name = "_createButton";
            this._createButton.Size = new Size(180, 44);
            this._createButton.Text = "+ Lập phiếu nhập";
            this._createButton.UseVisualStyleBackColor = false;
            //
            // _filterCard
            //
            this._filterCard.BackColor = Color.White;
            this._filterCard.BorderStyle = BorderStyle.FixedSingle;
            this._filterCard.Controls.Add(this._filterLayout);
            this._filterCard.Dock = DockStyle.Fill;
            this._filterCard.Margin = Padding.Empty;
            this._filterCard.Name = "_filterCard";
            this._filterCard.Padding = new Padding(8);
            //
            // _filterLayout
            //
            this._filterLayout.ColumnCount = 7;
            this._filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58F));
            this._filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            this._filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 180F));
            this._filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 28F));
            this._filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 180F));
            this._filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 32F));
            this._filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42F));
            this._filterLayout.Controls.Add(this._searchContainer, 0, 0);
            this._filterLayout.Controls.Add(this._fromDateLabel, 1, 0);
            this._filterLayout.Controls.Add(this._fromDate, 2, 0);
            this._filterLayout.Controls.Add(this._toDateLabel, 3, 0);
            this._filterLayout.Controls.Add(this._toDate, 4, 0);
            this._filterLayout.Controls.Add(this._clearDateLink, 5, 0);
            this._filterLayout.Controls.Add(this._totalLabel, 6, 0);
            this._filterLayout.Dock = DockStyle.Fill;
            this._filterLayout.Margin = Padding.Empty;
            this._filterLayout.Name = "_filterLayout";
            this._filterLayout.RowCount = 1;
            this._filterLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            //
            // _searchContainer
            //
            this._searchContainer.BackColor = Color.White;
            this._searchContainer.BorderStyle = BorderStyle.FixedSingle;
            this._searchContainer.Controls.Add(this._searchLayout);
            this._searchContainer.Dock = DockStyle.Fill;
            this._searchContainer.Margin = new Padding(2, 7, 8, 7);
            this._searchContainer.Name = "_searchContainer";
            //
            // _searchLayout
            //
            this._searchLayout.ColumnCount = 3;
            this._searchLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 38F));
            this._searchLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this._searchLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
            this._searchLayout.Controls.Add(this._searchIcon, 0, 0);
            this._searchLayout.Controls.Add(this._searchBox, 1, 0);
            this._searchLayout.Controls.Add(this._clearSearchButton, 2, 0);
            this._searchLayout.Dock = DockStyle.Fill;
            this._searchLayout.Margin = Padding.Empty;
            this._searchLayout.Name = "_searchLayout";
            this._searchLayout.RowCount = 1;
            this._searchLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            //
            // _searchIcon
            //
            this._searchIcon.Anchor = AnchorStyles.None;
            this._searchIcon.BackColor = Color.Transparent;
            this._searchIcon.ForeColor = Color.FromArgb(100, 116, 139);
            this._searchIcon.IconChar = IconChar.MagnifyingGlass;
            this._searchIcon.IconColor = Color.FromArgb(100, 116, 139);
            this._searchIcon.IconSize = 18;
            this._searchIcon.Name = "_searchIcon";
            this._searchIcon.Size = new Size(18, 18);
            this._searchIcon.SizeMode = PictureBoxSizeMode.CenterImage;
            //
            // _searchBox
            //
            this._searchBox.BorderStyle = BorderStyle.None;
            this._searchBox.Dock = DockStyle.Fill;
            this._searchBox.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this._searchBox.Margin = new Padding(0, 2, 0, 2);
            this._searchBox.Name = "_searchBox";
            //
            // _clearSearchButton
            //
            this._clearSearchButton.BackColor = Color.White;
            this._clearSearchButton.FlatAppearance.BorderSize = 0;
            this._clearSearchButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(241, 245, 249);
            this._clearSearchButton.FlatStyle = FlatStyle.Flat;
            this._clearSearchButton.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this._clearSearchButton.ForeColor = Color.FromArgb(100, 116, 139);
            this._clearSearchButton.Margin = Padding.Empty;
            this._clearSearchButton.Name = "_clearSearchButton";
            this._clearSearchButton.Size = new Size(30, 30);
            this._clearSearchButton.Text = "✕";
            this._clearSearchButton.UseVisualStyleBackColor = false;
            this._clearSearchButton.Visible = false;
            //
            // _fromDateLabel
            //
            this._fromDateLabel.Dock = DockStyle.Fill;
            this._fromDateLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this._fromDateLabel.ForeColor = Color.FromArgb(71, 85, 105);
            this._fromDateLabel.Name = "_fromDateLabel";
            this._fromDateLabel.Text = "Từ ngày";
            this._fromDateLabel.TextAlign = ContentAlignment.MiddleCenter;
            //
            // _fromDate
            //
            this._fromDate.Anchor = AnchorStyles.None;
            this._fromDate.Margin = Padding.Empty;
            this._fromDate.Name = "_fromDate";
            this._fromDate.Size = new Size(180, 36);
            this._fromDate.TabIndex = 0;
            //
            // _toDateLabel
            //
            this._toDateLabel.Dock = DockStyle.Fill;
            this._toDateLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this._toDateLabel.ForeColor = Color.FromArgb(71, 85, 105);
            this._toDateLabel.Name = "_toDateLabel";
            this._toDateLabel.Text = "đến";
            this._toDateLabel.TextAlign = ContentAlignment.MiddleCenter;
            //
            // _toDate
            //
            this._toDate.Anchor = AnchorStyles.None;
            this._toDate.Margin = Padding.Empty;
            this._toDate.Name = "_toDate";
            this._toDate.Size = new Size(180, 36);
            this._toDate.TabIndex = 1;
            //
            // _clearDateLink
            //
            this._clearDateLink.Anchor = AnchorStyles.None;
            this._clearDateLink.BackColor = Color.White;
            this._clearDateLink.FlatAppearance.BorderSize = 0;
            this._clearDateLink.FlatAppearance.MouseOverBackColor = Color.FromArgb(241, 245, 249);
            this._clearDateLink.FlatStyle = FlatStyle.Flat;
            this._clearDateLink.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this._clearDateLink.ForeColor = Color.FromArgb(100, 116, 139);
            this._clearDateLink.Name = "_clearDateLink";
            this._clearDateLink.Size = new Size(28, 28);
            this._clearDateLink.Text = "✕";
            this._clearDateLink.UseVisualStyleBackColor = false;
            this._clearDateLink.Visible = false;
            this._toolTip.SetToolTip(this._clearDateLink, "Xóa lọc ngày");
            //
            // _totalLabel
            //
            this._totalLabel.Dock = DockStyle.Right;
            this._totalLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this._totalLabel.ForeColor = Color.FromArgb(100, 116, 139);
            this._totalLabel.Name = "_totalLabel";
            this._totalLabel.Text = "Tổng cộng: 0 phiếu nhập kho";
            this._totalLabel.TextAlign = ContentAlignment.MiddleRight;
            this._totalLabel.AutoSize = true;
            //
            // _gridCard
            //
            this._gridCard.BackColor = Color.White;
            this._gridCard.BorderStyle = BorderStyle.FixedSingle;
            this._gridCard.Controls.Add(this._grid);
            this._gridCard.Dock = DockStyle.Fill;
            this._gridCard.Margin = Padding.Empty;
            this._gridCard.Name = "_gridCard";
            //
            // _grid
            //
            this._grid.AllowUserToAddRows = false;
            this._grid.AllowUserToDeleteRows = false;
            this._grid.AllowUserToResizeRows = false;
            this._grid.BackgroundColor = Color.White;
            this._grid.BorderStyle = BorderStyle.None;
            this._grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this._grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this._grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this._grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 41, 59);
            this._grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this._grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this._grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(30, 41, 59);
            this._grid.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            this._grid.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this._grid.ColumnHeadersHeight = 64;
            this._grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this._grid.Columns.AddRange(new DataGridViewColumn[] {
                this._receiptIdColumn,
                this._codeColumn,
                this._supplierColumn,
                this._creatorColumn,
                this._dateColumn,
                this._itemCountColumn,
                this._totalColumn,
                this._statusColumn,
                this._actionColumn});
            this._grid.DefaultCellStyle.BackColor = Color.White;
            this._grid.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this._grid.DefaultCellStyle.ForeColor = Color.FromArgb(51, 65, 85);
            this._grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(239, 246, 255);
            this._grid.DefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);
            this._grid.DefaultCellStyle.Padding = new Padding(8, 0, 8, 0);
            this._grid.Dock = DockStyle.Fill;
            this._grid.EnableHeadersVisualStyles = false;
            this._grid.GridColor = Color.FromArgb(226, 232, 240);
            this._grid.MultiSelect = false;
            this._grid.Name = "_grid";
            this._grid.ReadOnly = true;
            this._grid.RowHeadersVisible = false;
            this._grid.RowTemplate.Height = 60;
            this._grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this._grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            this._grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            //
            // columns
            //
            this._receiptIdColumn.HeaderText = "ReceiptID";
            this._receiptIdColumn.Name = "_receiptIdColumn";
            this._receiptIdColumn.ReadOnly = true;
            this._receiptIdColumn.Visible = false;
            this._codeColumn.HeaderText = "Mã phiếu";
            this._codeColumn.Name = "_codeColumn";
            this._codeColumn.ReadOnly = true;
            this._codeColumn.SortMode = DataGridViewColumnSortMode.Automatic;
            this._codeColumn.Width = 140;
            this._supplierColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this._supplierColumn.HeaderText = "Nhà cung cấp";
            this._supplierColumn.MinimumWidth = 180;
            this._supplierColumn.Name = "_supplierColumn";
            this._supplierColumn.ReadOnly = true;
            this._supplierColumn.SortMode = DataGridViewColumnSortMode.Automatic;
            this._creatorColumn.HeaderText = "Người tạo";
            this._creatorColumn.Name = "_creatorColumn";
            this._creatorColumn.ReadOnly = true;
            this._creatorColumn.SortMode = DataGridViewColumnSortMode.Automatic;
            this._creatorColumn.Width = 190;
            this._dateColumn.HeaderText = "Ngày tạo";
            this._dateColumn.Name = "_dateColumn";
            this._dateColumn.ReadOnly = true;
            this._dateColumn.SortMode = DataGridViewColumnSortMode.Automatic;
            this._dateColumn.DefaultCellStyle.Format = "dd/MM/yyyy";
            this._dateColumn.DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("vi-VN");
            this._dateColumn.Width = 150;
            this._itemCountColumn.HeaderText = "Số SP";
            this._itemCountColumn.Name = "_itemCountColumn";
            this._itemCountColumn.ReadOnly = true;
            this._itemCountColumn.SortMode = DataGridViewColumnSortMode.Automatic;
            this._itemCountColumn.Width = 100;
            this._totalColumn.HeaderText = "Tổng tiền";
            this._totalColumn.Name = "_totalColumn";
            this._totalColumn.ReadOnly = true;
            this._totalColumn.SortMode = DataGridViewColumnSortMode.Automatic;
            this._totalColumn.DefaultCellStyle.Format = "N0";
            this._totalColumn.DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("vi-VN");
            this._totalColumn.Width = 140;
            this._statusColumn.HeaderText = "Trạng thái";
            this._statusColumn.Name = "_statusColumn";
            this._statusColumn.ReadOnly = true;
            this._statusColumn.SortMode = DataGridViewColumnSortMode.Automatic;
            this._statusColumn.Width = 120;
            this._actionColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this._actionColumn.HeaderText = "Thao tác";
            this._actionColumn.Name = "_actionColumn";
            this._actionColumn.ReadOnly = true;
            this._actionColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this._actionColumn.ToolTipText = "Xem chi tiết phiếu nhập";
            this._actionColumn.Width = 90;
            //
            // _pagingPanel
            //
            this._pagingPanel.Anchor = AnchorStyles.None;
            this._pagingPanel.AutoSize = true;
            this._pagingPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this._pagingPanel.Controls.Add(this._prevButton);
            this._pagingPanel.Controls.Add(this._pageNumberBadge);
            this._pagingPanel.Controls.Add(this._nextButton);
            this._pagingPanel.Controls.Add(this._pageInfoLabel);
            this._pagingPanel.Controls.Add(this._displayLabel);
            this._pagingPanel.Controls.Add(this._pageSizeCombo);
            this._pagingPanel.Controls.Add(this._rowsLabel);
            this._pagingPanel.FlowDirection = FlowDirection.LeftToRight;
            this._pagingPanel.Margin = Padding.Empty;
            this._pagingPanel.Name = "_pagingPanel";
            this._pagingPanel.WrapContents = false;
            //
            // _prevButton
            //
            this._prevButton.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            this._prevButton.FlatStyle = FlatStyle.Flat;
            this._prevButton.Font = new Font("Segoe UI", 16F, FontStyle.Regular);
            this._prevButton.Margin = new Padding(0, 0, 6, 0);
            this._prevButton.Name = "_prevButton";
            this._prevButton.Size = new Size(34, 34);
            this._prevButton.Text = "‹";
            this._prevButton.UseVisualStyleBackColor = true;
            //
            // _pageNumberBadge
            //
            this._pageNumberBadge.BackColor = Color.Transparent;
            this._pageNumberBadge.Controls.Add(this._pageNumberLabel);
            this._pageNumberBadge.Margin = new Padding(0, 0, 6, 0);
            this._pageNumberBadge.Name = "_pageNumberBadge";
            this._pageNumberBadge.Size = new Size(34, 34);
            //
            // _pageNumberLabel
            //
            this._pageNumberLabel.BackColor = Color.Transparent;
            this._pageNumberLabel.Dock = DockStyle.Fill;
            this._pageNumberLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this._pageNumberLabel.ForeColor = Color.White;
            this._pageNumberLabel.Name = "_pageNumberLabel";
            this._pageNumberLabel.Text = "1";
            this._pageNumberLabel.TextAlign = ContentAlignment.MiddleCenter;
            //
            // _nextButton
            //
            this._nextButton.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            this._nextButton.FlatStyle = FlatStyle.Flat;
            this._nextButton.Font = new Font("Segoe UI", 16F, FontStyle.Regular);
            this._nextButton.Margin = new Padding(0, 0, 12, 0);
            this._nextButton.Name = "_nextButton";
            this._nextButton.Size = new Size(34, 34);
            this._nextButton.Text = "›";
            this._nextButton.UseVisualStyleBackColor = true;
            //
            // _pageInfoLabel
            //
            this._pageInfoLabel.AutoSize = true;
            this._pageInfoLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this._pageInfoLabel.ForeColor = Color.FromArgb(71, 85, 105);
            this._pageInfoLabel.Margin = new Padding(0, 8, 18, 0);
            this._pageInfoLabel.Name = "_pageInfoLabel";
            this._pageInfoLabel.Text = "Trang 1 / 1";
            this._pageInfoLabel.TextAlign = ContentAlignment.MiddleCenter;
            //
            // _displayLabel
            //
            this._displayLabel.AutoSize = true;
            this._displayLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this._displayLabel.ForeColor = Color.FromArgb(100, 116, 139);
            this._displayLabel.Margin = new Padding(0, 8, 6, 0);
            this._displayLabel.Name = "_displayLabel";
            this._displayLabel.Text = "Hiển thị:";
            this._displayLabel.TextAlign = ContentAlignment.MiddleCenter;
            //
            // _pageSizeCombo
            //
            this._pageSizeCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            this._pageSizeCombo.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this._pageSizeCombo.Items.AddRange(new object[] { 10, 20, 50 });
            this._pageSizeCombo.Margin = new Padding(0, 2, 6, 0);
            this._pageSizeCombo.Name = "_pageSizeCombo";
            this._pageSizeCombo.Size = new Size(64, 28);
            this._pageSizeCombo.SelectedIndex = 0;
            //
            // _rowsLabel
            //
            this._rowsLabel.AutoSize = true;
            this._rowsLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this._rowsLabel.ForeColor = Color.FromArgb(100, 116, 139);
            this._rowsLabel.Margin = new Padding(0, 8, 0, 0);
            this._rowsLabel.Name = "_rowsLabel";
            this._rowsLabel.Text = "dòng";
            this._rowsLabel.TextAlign = ContentAlignment.MiddleCenter;
            //
            // ToolTip
            //
            this._toolTip.AutoPopDelay = 5000;
            this._toolTip.InitialDelay = 500;
            this._toolTip.ReshowDelay = 100;
            //
            // ucPurchaseReceiptManagement
            //
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(244, 247, 250);
            this.Controls.Add(this._root);
            this.Name = "ucPurchaseReceiptManagement";
            this.Padding = new Padding(20);
            this.Size = new Size(1200, 760);
            this._root.ResumeLayout(false);
            this._headerCard.ResumeLayout(false);
            this._headerLayout.ResumeLayout(false);
            this._headerLeftLayout.ResumeLayout(false);
            this._headerIconCircle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._titleIcon)).EndInit();
            this._headerCopyLayout.ResumeLayout(false);
            this._headerActions.ResumeLayout(false);
            this._filterCard.ResumeLayout(false);
            this._filterLayout.ResumeLayout(false);
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