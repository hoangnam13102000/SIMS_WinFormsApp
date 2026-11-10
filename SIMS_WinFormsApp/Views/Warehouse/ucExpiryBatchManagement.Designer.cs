using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace SIMS_WinFormsApp.Views.Warehouse
{
    partial class ucExpiryBatchManagement
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel _root;
        private Panel _headerPanel;
        private Panel _headerIconPanel;
        private IconPictureBox _headerIcon;
        private Label _titleLabel;
        private Label _subtitleLabel;
        private Button _disposeButton;
        private TableLayoutPanel _summaryLayout;
        private Panel _expiredCard;
        private Label _expiredValue;
        private Label _expiredCaption;
        private Panel _nearExpiryCard;
        private Label _nearExpiryValue;
        private Label _nearExpiryCaption;
        private Panel _totalCard;
        private Label _totalValue;
        private Label _totalCaption;
        private Panel _filterPanel;
        private TextBox _searchBox;
        private ComboBox _filterCombo;
        private Button _refreshButton;
        private Button _clearProductFilterButton;
        private DataGridView _grid;
        private DataGridViewTextBoxColumn _colBatchCode;
        private DataGridViewTextBoxColumn _colProduct;
        private DataGridViewTextBoxColumn _colLotNumber;
        private DataGridViewTextBoxColumn _colSupplier;
        private DataGridViewTextBoxColumn _colExpiryDate;
        private DataGridViewTextBoxColumn _colRemaining;
        private DataGridViewTextBoxColumn _colUnitCost;
        private DataGridViewTextBoxColumn _colLossValue;
        private DataGridViewTextBoxColumn _colStatus;
        private Label _footerLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._root = new System.Windows.Forms.TableLayoutPanel();
            this._headerPanel = new System.Windows.Forms.Panel();
            this._titleLabel = new System.Windows.Forms.Label();
            this._subtitleLabel = new System.Windows.Forms.Label();
            this._disposeButton = new System.Windows.Forms.Button();
            this._headerIconPanel = new System.Windows.Forms.Panel();
            this._headerIcon = new FontAwesome.Sharp.IconPictureBox();
            this._summaryLayout = new System.Windows.Forms.TableLayoutPanel();
            this._expiredCard = new System.Windows.Forms.Panel();
            this._expiredCaption = new System.Windows.Forms.Label();
            this._expiredValue = new System.Windows.Forms.Label();
            this._nearExpiryCard = new System.Windows.Forms.Panel();
            this._nearExpiryCaption = new System.Windows.Forms.Label();
            this._nearExpiryValue = new System.Windows.Forms.Label();
            this._totalCard = new System.Windows.Forms.Panel();
            this._totalCaption = new System.Windows.Forms.Label();
            this._totalValue = new System.Windows.Forms.Label();
            this._filterPanel = new System.Windows.Forms.Panel();
            this._searchBox = new System.Windows.Forms.TextBox();
            this._filterCombo = new System.Windows.Forms.ComboBox();
            this._clearProductFilterButton = new System.Windows.Forms.Button();
            this._refreshButton = new System.Windows.Forms.Button();
            this._grid = new System.Windows.Forms.DataGridView();
            this.BatchCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Product = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LotNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Supplier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExpiryDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remaining = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LossValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._footerLabel = new System.Windows.Forms.Label();
            this._searchTimer = new System.Windows.Forms.Timer(this.components);
            this._root.SuspendLayout();
            this._headerPanel.SuspendLayout();
            this._headerIconPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._headerIcon)).BeginInit();
            this._summaryLayout.SuspendLayout();
            this._expiredCard.SuspendLayout();
            this._nearExpiryCard.SuspendLayout();
            this._totalCard.SuspendLayout();
            this._filterPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._grid)).BeginInit();
            this.SuspendLayout();
            // 
            // _root
            // 
            this._root.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this._root.ColumnCount = 1;
            this._root.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._root.Controls.Add(this._headerPanel, 0, 0);
            this._root.Controls.Add(this._summaryLayout, 0, 1);
            this._root.Controls.Add(this._filterPanel, 0, 2);
            this._root.Controls.Add(this._grid, 0, 3);
            this._root.Controls.Add(this._footerLabel, 0, 4);
            this._root.Dock = System.Windows.Forms.DockStyle.Fill;
            this._root.Location = new System.Drawing.Point(0, 0);
            this._root.Name = "_root";
            this._root.Padding = new System.Windows.Forms.Padding(24);
            this._root.RowCount = 5;
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 104F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this._root.Size = new System.Drawing.Size(1200, 760);
            this._root.TabIndex = 0;
            // 
            // _headerPanel
            // 
            this._headerPanel.BackColor = System.Drawing.Color.White;
            this._headerPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._headerPanel.Controls.Add(this._titleLabel);
            this._headerPanel.Controls.Add(this._subtitleLabel);
            this._headerPanel.Controls.Add(this._disposeButton);
            this._headerPanel.Controls.Add(this._headerIconPanel);
            this._headerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._headerPanel.Location = new System.Drawing.Point(27, 27);
            this._headerPanel.Name = "_headerPanel";
            this._headerPanel.Size = new System.Drawing.Size(1146, 84);
            this._headerPanel.TabIndex = 0;
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
            this._titleLabel.Size = new System.Drawing.Size(1566, 36);
            this._titleLabel.TabIndex = 0;
            this._titleLabel.Text = "Quản lý sản phẩm hết hạn";
            this._titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._titleLabel.UseMnemonic = false;
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
            this._subtitleLabel.Size = new System.Drawing.Size(1566, 20);
            this._subtitleLabel.TabIndex = 1;
            this._subtitleLabel.Text = "Theo dõi hàng hết hạn, xem chi tiết theo lô và lập phiếu tiêu hủy.";
            this._subtitleLabel.UseMnemonic = false;
            // 
            // _disposeButton
            // 
            this._disposeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._disposeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this._disposeButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this._disposeButton.Enabled = false;
            this._disposeButton.FlatAppearance.BorderSize = 0;
            this._disposeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._disposeButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._disposeButton.ForeColor = System.Drawing.Color.White;
            this._disposeButton.Location = new System.Drawing.Point(1856, 22);
            this._disposeButton.Name = "_disposeButton";
            this._disposeButton.Size = new System.Drawing.Size(218, 38);
            this._disposeButton.TabIndex = 2;
            this._disposeButton.Text = "Lập phiếu tiêu hủy";
            this._disposeButton.UseVisualStyleBackColor = false;
            this._disposeButton.Click += new System.EventHandler(this._disposeButton_Click);
            // 
            // _headerIconPanel
            // 
            this._headerIconPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this._headerIconPanel.Controls.Add(this._headerIcon);
            this._headerIconPanel.Location = new System.Drawing.Point(20, 20);
            this._headerIconPanel.Name = "_headerIconPanel";
            this._headerIconPanel.Size = new System.Drawing.Size(44, 44);
            this._headerIconPanel.TabIndex = 3;
            // 
            // _headerIcon
            // 
            this._headerIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this._headerIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._headerIcon.IconChar = FontAwesome.Sharp.IconChar.HourglassHalf;
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
            // _summaryLayout
            // 
            this._summaryLayout.ColumnCount = 3;
            this._summaryLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333F));
            this._summaryLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333F));
            this._summaryLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.334F));
            this._summaryLayout.Controls.Add(this._expiredCard, 0, 0);
            this._summaryLayout.Controls.Add(this._nearExpiryCard, 1, 0);
            this._summaryLayout.Controls.Add(this._totalCard, 2, 0);
            this._summaryLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._summaryLayout.Location = new System.Drawing.Point(24, 116);
            this._summaryLayout.Margin = new System.Windows.Forms.Padding(0, 2, 0, 8);
            this._summaryLayout.Name = "_summaryLayout";
            this._summaryLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 94F));
            this._summaryLayout.Size = new System.Drawing.Size(1152, 94);
            this._summaryLayout.TabIndex = 1;
            // 
            // _expiredCard
            // 
            this._expiredCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this._expiredCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._expiredCard.Controls.Add(this._expiredCaption);
            this._expiredCard.Controls.Add(this._expiredValue);
            this._expiredCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._expiredCard.Location = new System.Drawing.Point(0, 0);
            this._expiredCard.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this._expiredCard.Name = "_expiredCard";
            this._expiredCard.Size = new System.Drawing.Size(375, 94);
            this._expiredCard.TabIndex = 0;
            // 
            // _expiredCaption
            // 
            this._expiredCaption.AutoSize = true;
            this._expiredCaption.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._expiredCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._expiredCaption.Location = new System.Drawing.Point(18, 58);
            this._expiredCaption.Name = "_expiredCaption";
            this._expiredCaption.Size = new System.Drawing.Size(215, 20);
            this._expiredCaption.TabIndex = 0;
            this._expiredCaption.Text = "ĐƠN VỊ HẾT HẠN CHỜ XỬ LÝ";
            // 
            // _expiredValue
            // 
            this._expiredValue.AutoSize = true;
            this._expiredValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this._expiredValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this._expiredValue.Location = new System.Drawing.Point(16, 10);
            this._expiredValue.Name = "_expiredValue";
            this._expiredValue.Size = new System.Drawing.Size(40, 46);
            this._expiredValue.TabIndex = 1;
            this._expiredValue.Text = "0";
            // 
            // _nearExpiryCard
            // 
            this._nearExpiryCard.BackColor = System.Drawing.Color.White;
            this._nearExpiryCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._nearExpiryCard.Controls.Add(this._nearExpiryCaption);
            this._nearExpiryCard.Controls.Add(this._nearExpiryValue);
            this._nearExpiryCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._nearExpiryCard.Location = new System.Drawing.Point(391, 0);
            this._nearExpiryCard.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this._nearExpiryCard.Name = "_nearExpiryCard";
            this._nearExpiryCard.Size = new System.Drawing.Size(367, 94);
            this._nearExpiryCard.TabIndex = 1;
            // 
            // _nearExpiryCaption
            // 
            this._nearExpiryCaption.AutoSize = true;
            this._nearExpiryCaption.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._nearExpiryCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._nearExpiryCaption.Location = new System.Drawing.Point(18, 58);
            this._nearExpiryCaption.Name = "_nearExpiryCaption";
            this._nearExpiryCaption.Size = new System.Drawing.Size(243, 20);
            this._nearExpiryCaption.TabIndex = 0;
            this._nearExpiryCaption.Text = "LÔ SẼ HẾT HẠN TRONG 30 NGÀY";
            // 
            // _nearExpiryValue
            // 
            this._nearExpiryValue.AutoSize = true;
            this._nearExpiryValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this._nearExpiryValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(119)))), ((int)(((byte)(6)))));
            this._nearExpiryValue.Location = new System.Drawing.Point(16, 10);
            this._nearExpiryValue.Name = "_nearExpiryValue";
            this._nearExpiryValue.Size = new System.Drawing.Size(40, 46);
            this._nearExpiryValue.TabIndex = 1;
            this._nearExpiryValue.Text = "0";
            // 
            // _totalCard
            // 
            this._totalCard.BackColor = System.Drawing.Color.White;
            this._totalCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._totalCard.Controls.Add(this._totalCaption);
            this._totalCard.Controls.Add(this._totalValue);
            this._totalCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._totalCard.Location = new System.Drawing.Point(774, 0);
            this._totalCard.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this._totalCard.Name = "_totalCard";
            this._totalCard.Size = new System.Drawing.Size(370, 94);
            this._totalCard.TabIndex = 2;
            // 
            // _totalCaption
            // 
            this._totalCaption.AutoSize = true;
            this._totalCaption.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._totalCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._totalCaption.Location = new System.Drawing.Point(18, 58);
            this._totalCaption.Name = "_totalCaption";
            this._totalCaption.Size = new System.Drawing.Size(233, 20);
            this._totalCaption.TabIndex = 0;
            this._totalCaption.Text = "TỔNG SỐ LÔ CÓ HẠN SỬ DỤNG";
            // 
            // _totalValue
            // 
            this._totalValue.AutoSize = true;
            this._totalValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this._totalValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._totalValue.Location = new System.Drawing.Point(16, 10);
            this._totalValue.Name = "_totalValue";
            this._totalValue.Size = new System.Drawing.Size(40, 46);
            this._totalValue.TabIndex = 1;
            this._totalValue.Text = "0";
            // 
            // _filterPanel
            // 
            this._filterPanel.BackColor = System.Drawing.Color.White;
            this._filterPanel.Controls.Add(this._searchBox);
            this._filterPanel.Controls.Add(this._filterCombo);
            this._filterPanel.Controls.Add(this._clearProductFilterButton);
            this._filterPanel.Controls.Add(this._refreshButton);
            this._filterPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._filterPanel.Location = new System.Drawing.Point(24, 222);
            this._filterPanel.Margin = new System.Windows.Forms.Padding(0, 4, 0, 8);
            this._filterPanel.Name = "_filterPanel";
            this._filterPanel.Size = new System.Drawing.Size(1152, 52);
            this._filterPanel.TabIndex = 2;
            // 
            // _searchBox
            // 
            this._searchBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._searchBox.Location = new System.Drawing.Point(16, 17);
            this._searchBox.Name = "_searchBox";
            this._searchBox.Size = new System.Drawing.Size(390, 30);
            this._searchBox.TabIndex = 0;
            this._searchBox.TextChanged += new System.EventHandler(this._searchBox_TextChanged);
            // 
            // _filterCombo
            // 
            this._filterCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._filterCombo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this._filterCombo.Items.AddRange(new object[] {
            "Tất cả lô còn hàng",
            "Sắp hết hạn (30 ngày)",
            "Đã hết hạn"});
            this._filterCombo.Location = new System.Drawing.Point(420, 15);
            this._filterCombo.Name = "_filterCombo";
            this._filterCombo.Size = new System.Drawing.Size(230, 29);
            this._filterCombo.TabIndex = 1;
            // 
            // _clearProductFilterButton
            // 
            this._clearProductFilterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._clearProductFilterButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._clearProductFilterButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._clearProductFilterButton.Location = new System.Drawing.Point(1772, 12);
            this._clearProductFilterButton.Name = "_clearProductFilterButton";
            this._clearProductFilterButton.Size = new System.Drawing.Size(160, 34);
            this._clearProductFilterButton.TabIndex = 2;
            this._clearProductFilterButton.Text = "Bỏ lọc sản phẩm";
            this._clearProductFilterButton.Click += new System.EventHandler(this._clearProductFilterButton_Click);
            // 
            // _refreshButton
            // 
            this._refreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._refreshButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._refreshButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._refreshButton.Location = new System.Drawing.Point(2002, 12);
            this._refreshButton.Name = "_refreshButton";
            this._refreshButton.Size = new System.Drawing.Size(92, 34);
            this._refreshButton.TabIndex = 3;
            this._refreshButton.Text = "Tải lại";
            this._refreshButton.Click += new System.EventHandler(this._refreshButton_Click);
            // 
            // _grid
            // 
            this._grid.AllowUserToAddRows = false;
            this._grid.AllowUserToDeleteRows = false;
            this._grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._grid.BackgroundColor = System.Drawing.Color.White;
            this._grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._grid.ColumnHeadersHeight = 42;
            this._grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BatchCode,
            this.Product,
            this.LotNumber,
            this.Supplier,
            this.ExpiryDate,
            this.Remaining,
            this.UnitCost,
            this.LossValue,
            this.Status});
            this._grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grid.Location = new System.Drawing.Point(27, 285);
            this._grid.MultiSelect = false;
            this._grid.Name = "_grid";
            this._grid.ReadOnly = true;
            this._grid.RowHeadersVisible = false;
            this._grid.RowHeadersWidth = 51;
            this._grid.RowTemplate.Height = 36;
            this._grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._grid.Size = new System.Drawing.Size(1146, 412);
            this._grid.TabIndex = 3;
            this._grid.SelectionChanged += new System.EventHandler(this._grid_SelectionChanged);
            // 
            // BatchCode
            // 
            this.BatchCode.FillWeight = 85F;
            this.BatchCode.HeaderText = "Mã lô";
            this.BatchCode.MinimumWidth = 45;
            this.BatchCode.Name = "BatchCode";
            this.BatchCode.ReadOnly = true;
            this.BatchCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Product
            // 
            this.Product.FillWeight = 155F;
            this.Product.HeaderText = "Sản phẩm";
            this.Product.MinimumWidth = 45;
            this.Product.Name = "Product";
            this.Product.ReadOnly = true;
            this.Product.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // LotNumber
            // 
            this.LotNumber.FillWeight = 95F;
            this.LotNumber.HeaderText = "Số lô NCC";
            this.LotNumber.MinimumWidth = 45;
            this.LotNumber.Name = "LotNumber";
            this.LotNumber.ReadOnly = true;
            this.LotNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Supplier
            // 
            this.Supplier.FillWeight = 125F;
            this.Supplier.HeaderText = "Nhà cung cấp";
            this.Supplier.MinimumWidth = 45;
            this.Supplier.Name = "Supplier";
            this.Supplier.ReadOnly = true;
            this.Supplier.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ExpiryDate
            // 
            this.ExpiryDate.FillWeight = 90F;
            this.ExpiryDate.HeaderText = "Hạn sử dụng";
            this.ExpiryDate.MinimumWidth = 45;
            this.ExpiryDate.Name = "ExpiryDate";
            this.ExpiryDate.ReadOnly = true;
            this.ExpiryDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Remaining
            // 
            this.Remaining.FillWeight = 65F;
            this.Remaining.HeaderText = "Còn lại";
            this.Remaining.MinimumWidth = 45;
            this.Remaining.Name = "Remaining";
            this.Remaining.ReadOnly = true;
            this.Remaining.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // UnitCost
            // 
            this.UnitCost.FillWeight = 85F;
            this.UnitCost.HeaderText = "Giá nhập";
            this.UnitCost.MinimumWidth = 45;
            this.UnitCost.Name = "UnitCost";
            this.UnitCost.ReadOnly = true;
            this.UnitCost.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // LossValue
            // 
            this.LossValue.FillWeight = 95F;
            this.LossValue.HeaderText = "Giá trị tồn";
            this.LossValue.MinimumWidth = 45;
            this.LossValue.Name = "LossValue";
            this.LossValue.ReadOnly = true;
            this.LossValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Status
            // 
            this.Status.HeaderText = "Tình trạng";
            this.Status.MinimumWidth = 45;
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // _footerLabel
            // 
            this._footerLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._footerLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._footerLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._footerLabel.Location = new System.Drawing.Point(27, 700);
            this._footerLabel.Name = "_footerLabel";
            this._footerLabel.Size = new System.Drawing.Size(1146, 36);
            this._footerLabel.TabIndex = 4;
            this._footerLabel.Text = "Hàng hết hạn được chặn khỏi bán; chỉ người có quyền mới lập phiếu tiêu hủy.";
            this._footerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _searchTimer
            // 
            this._searchTimer.Interval = 300;
            this._searchTimer.Tick += new System.EventHandler(this._searchTimer_Tick);
            // 
            // ucExpiryBatchManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.Controls.Add(this._root);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "ucExpiryBatchManagement";
            this.Size = new System.Drawing.Size(1200, 760);
            this._root.ResumeLayout(false);
            this._headerPanel.ResumeLayout(false);
            this._headerIconPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._headerIcon)).EndInit();
            this._summaryLayout.ResumeLayout(false);
            this._expiredCard.ResumeLayout(false);
            this._expiredCard.PerformLayout();
            this._nearExpiryCard.ResumeLayout(false);
            this._nearExpiryCard.PerformLayout();
            this._totalCard.ResumeLayout(false);
            this._totalCard.PerformLayout();
            this._filterPanel.ResumeLayout(false);
            this._filterPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._grid)).EndInit();
            this.ResumeLayout(false);

        }

        private DataGridViewTextBoxColumn BatchCode;
        private DataGridViewTextBoxColumn Product;
        private DataGridViewTextBoxColumn LotNumber;
        private DataGridViewTextBoxColumn Supplier;
        private DataGridViewTextBoxColumn ExpiryDate;
        private DataGridViewTextBoxColumn Remaining;
        private DataGridViewTextBoxColumn UnitCost;
        private DataGridViewTextBoxColumn LossValue;
        private DataGridViewTextBoxColumn Status;
        private Timer _searchTimer;
    }
}
