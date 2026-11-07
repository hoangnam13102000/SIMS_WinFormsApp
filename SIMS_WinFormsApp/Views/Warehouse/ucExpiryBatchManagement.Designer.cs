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
            components = new System.ComponentModel.Container();
            _root = new TableLayoutPanel();
            _headerPanel = new Panel();
            _headerIconPanel = new Panel();
            _headerIcon = new IconPictureBox();
            _titleLabel = new Label();
            _subtitleLabel = new Label();
            _disposeButton = new Button();
            _summaryLayout = new TableLayoutPanel();
            _expiredCard = new Panel();
            _expiredValue = new Label();
            _expiredCaption = new Label();
            _nearExpiryCard = new Panel();
            _nearExpiryValue = new Label();
            _nearExpiryCaption = new Label();
            _totalCard = new Panel();
            _totalValue = new Label();
            _totalCaption = new Label();
            _filterPanel = new Panel();
            _searchBox = new TextBox();
            _filterCombo = new ComboBox();
            _refreshButton = new Button();
            _clearProductFilterButton = new Button();
            _grid = new DataGridView();
            _colBatchCode = new DataGridViewTextBoxColumn();
            _colProduct = new DataGridViewTextBoxColumn();
            _colLotNumber = new DataGridViewTextBoxColumn();
            _colSupplier = new DataGridViewTextBoxColumn();
            _colExpiryDate = new DataGridViewTextBoxColumn();
            _colRemaining = new DataGridViewTextBoxColumn();
            _colUnitCost = new DataGridViewTextBoxColumn();
            _colLossValue = new DataGridViewTextBoxColumn();
            _colStatus = new DataGridViewTextBoxColumn();
            _footerLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)_grid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_headerIcon).BeginInit();
            _root.SuspendLayout();
            _headerPanel.SuspendLayout();
            _headerIconPanel.SuspendLayout();
            _summaryLayout.SuspendLayout();
            _expiredCard.SuspendLayout();
            _nearExpiryCard.SuspendLayout();
            _totalCard.SuspendLayout();
            _filterPanel.SuspendLayout();
            SuspendLayout();
            //
            // _root
            //
            _root.BackColor = Color.FromArgb(244, 247, 250);
            _root.ColumnCount = 1;
            _root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _root.Controls.Add(_headerPanel, 0, 0);
            _root.Controls.Add(_summaryLayout, 0, 1);
            _root.Controls.Add(_filterPanel, 0, 2);
            _root.Controls.Add(_grid, 0, 3);
            _root.Controls.Add(_footerLabel, 0, 4);
            _root.Dock = DockStyle.Fill;
            _root.Padding = new Padding(24);
            _root.RowCount = 5;
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 104F));
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F));
            _root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            _root.Name = "_root";
            //
            // _headerPanel
            //
            _headerPanel.BackColor = Color.White;
            _headerPanel.BorderStyle = BorderStyle.FixedSingle;
            _headerPanel.Controls.Add(_titleLabel);
            _headerPanel.Controls.Add(_subtitleLabel);
            _headerPanel.Controls.Add(_disposeButton);
            _headerPanel.Controls.Add(_headerIconPanel);
            _headerPanel.Dock = DockStyle.Fill;
            _headerPanel.Name = "_headerPanel";
            //
            // _headerIconPanel
            //
            _headerIconPanel.BackColor = Color.FromArgb(219, 234, 254);
            _headerIconPanel.Controls.Add(_headerIcon);
            _headerIconPanel.Location = new Point(20, 20);
            _headerIconPanel.Name = "_headerIconPanel";
            _headerIconPanel.Size = new Size(44, 44);
            //
            // _headerIcon
            //
            _headerIcon.BackColor = Color.FromArgb(219, 234, 254);
            _headerIcon.ForeColor = Color.FromArgb(37, 99, 235);
            _headerIcon.IconChar = IconChar.HourglassHalf;
            _headerIcon.IconColor = Color.FromArgb(37, 99, 235);
            _headerIcon.IconFont = IconFont.Auto;
            _headerIcon.IconSize = 20;
            _headerIcon.Location = new Point(12, 12);
            _headerIcon.Name = "_headerIcon";
            _headerIcon.Size = new Size(20, 20);
            _headerIcon.SizeMode = PictureBoxSizeMode.CenterImage;
            _headerIcon.TabStop = false;
            //
            // _titleLabel
            //
            _titleLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _titleLabel.AutoEllipsis = true;
            _titleLabel.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            _titleLabel.ForeColor = Color.FromArgb(15, 23, 42);
            _titleLabel.Location = new Point(80, 12);
            _titleLabel.Name = "_titleLabel";
            _titleLabel.Size = new Size(620, 36);
            _titleLabel.Text = "Quản lý sản phẩm hết hạn";
            _titleLabel.TextAlign = ContentAlignment.MiddleLeft;
            _titleLabel.UseMnemonic = false;
            //
            // _subtitleLabel
            //
            _subtitleLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _subtitleLabel.AutoEllipsis = true;
            _subtitleLabel.Font = new Font("Segoe UI", 9F);
            _subtitleLabel.ForeColor = Color.FromArgb(100, 116, 139);
            _subtitleLabel.Location = new Point(80, 52);
            _subtitleLabel.Name = "_subtitleLabel";
            _subtitleLabel.Size = new Size(620, 20);
            _subtitleLabel.Text = "Theo dõi hàng hết hạn, xem chi tiết theo lô và lập phiếu tiêu hủy.";
            _subtitleLabel.UseMnemonic = false;
            //
            // _disposeButton
            //
            _disposeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _disposeButton.BackColor = Color.FromArgb(220, 38, 38);
            _disposeButton.Cursor = Cursors.Hand;
            _disposeButton.FlatAppearance.BorderSize = 0;
            _disposeButton.FlatStyle = FlatStyle.Flat;
            _disposeButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            _disposeButton.ForeColor = Color.White;
            _disposeButton.Location = new Point(910, 22);
            _disposeButton.Name = "_disposeButton";
            _disposeButton.Size = new Size(218, 38);
            _disposeButton.Text = "Lập phiếu tiêu hủy";
            _disposeButton.UseVisualStyleBackColor = false;
            _disposeButton.Enabled = false;
            //
            // _summaryLayout
            //
            _summaryLayout.ColumnCount = 3;
            _summaryLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.333F));
            _summaryLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.333F));
            _summaryLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.334F));
            _summaryLayout.Controls.Add(_expiredCard, 0, 0);
            _summaryLayout.Controls.Add(_nearExpiryCard, 1, 0);
            _summaryLayout.Controls.Add(_totalCard, 2, 0);
            _summaryLayout.Dock = DockStyle.Fill;
            _summaryLayout.Margin = new Padding(0, 2, 0, 8);
            _summaryLayout.Name = "_summaryLayout";
            //
            // _expiredCard
            //
            _expiredCard.BackColor = Color.FromArgb(254, 242, 242);
            _expiredCard.BorderStyle = BorderStyle.FixedSingle;
            _expiredCard.Controls.Add(_expiredCaption);
            _expiredCard.Controls.Add(_expiredValue);
            _expiredCard.Dock = DockStyle.Fill;
            _expiredCard.Margin = new Padding(0, 0, 8, 0);
            _expiredCard.Name = "summaryCard0";
            //
            // _expiredValue
            //
            _expiredValue.AutoSize = true;
            _expiredValue.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            _expiredValue.ForeColor = Color.FromArgb(185, 28, 28);
            _expiredValue.Location = new Point(16, 10);
            _expiredValue.Text = "0";
            //
            // _expiredCaption
            //
            _expiredCaption.AutoSize = true;
            _expiredCaption.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            _expiredCaption.ForeColor = Color.FromArgb(100, 116, 139);
            _expiredCaption.Location = new Point(18, 58);
            _expiredCaption.Text = "ĐƠN VỊ HẾT HẠN CHỜ XỬ LÝ";
            //
            // _nearExpiryCard
            //
            _nearExpiryCard.BackColor = Color.White;
            _nearExpiryCard.BorderStyle = BorderStyle.FixedSingle;
            _nearExpiryCard.Controls.Add(_nearExpiryCaption);
            _nearExpiryCard.Controls.Add(_nearExpiryValue);
            _nearExpiryCard.Dock = DockStyle.Fill;
            _nearExpiryCard.Margin = new Padding(8, 0, 8, 0);
            _nearExpiryCard.Name = "summaryCard1";
            //
            // _nearExpiryValue
            //
            _nearExpiryValue.AutoSize = true;
            _nearExpiryValue.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            _nearExpiryValue.ForeColor = Color.FromArgb(217, 119, 6);
            _nearExpiryValue.Location = new Point(16, 10);
            _nearExpiryValue.Text = "0";
            //
            // _nearExpiryCaption
            //
            _nearExpiryCaption.AutoSize = true;
            _nearExpiryCaption.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            _nearExpiryCaption.ForeColor = Color.FromArgb(100, 116, 139);
            _nearExpiryCaption.Location = new Point(18, 58);
            _nearExpiryCaption.Text = "LÔ SẼ HẾT HẠN TRONG 30 NGÀY";
            //
            // _totalCard
            //
            _totalCard.BackColor = Color.White;
            _totalCard.BorderStyle = BorderStyle.FixedSingle;
            _totalCard.Controls.Add(_totalCaption);
            _totalCard.Controls.Add(_totalValue);
            _totalCard.Dock = DockStyle.Fill;
            _totalCard.Margin = new Padding(8, 0, 8, 0);
            _totalCard.Name = "summaryCard2";
            //
            // _totalValue
            //
            _totalValue.AutoSize = true;
            _totalValue.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            _totalValue.ForeColor = Color.FromArgb(37, 99, 235);
            _totalValue.Location = new Point(16, 10);
            _totalValue.Text = "0";
            //
            // _totalCaption
            //
            _totalCaption.AutoSize = true;
            _totalCaption.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            _totalCaption.ForeColor = Color.FromArgb(100, 116, 139);
            _totalCaption.Location = new Point(18, 58);
            _totalCaption.Text = "TỔNG SỐ LÔ CÓ HẠN SỬ DỤNG";
            //
            // _filterPanel
            //
            _filterPanel.BackColor = Color.White;
            _filterPanel.Controls.Add(_searchBox);
            _filterPanel.Controls.Add(_filterCombo);
            _filterPanel.Controls.Add(_clearProductFilterButton);
            _filterPanel.Controls.Add(_refreshButton);
            _filterPanel.Dock = DockStyle.Fill;
            _filterPanel.Margin = new Padding(0, 4, 0, 8);
            _filterPanel.Name = "_filterPanel";
            //
            // _searchBox
            //
            _searchBox.Font = new Font("Segoe UI", 10F);
            _searchBox.Location = new Point(16, 17);
            _searchBox.Name = "_searchBox";
            _searchBox.Size = new Size(390, 25);
            _searchBox.Text = "";
            //
            // _filterCombo
            //
            _filterCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            _filterCombo.Font = new Font("Segoe UI", 9.5F);
            _filterCombo.Items.AddRange(new object[] { "Tất cả lô còn hàng", "Sắp hết hạn (30 ngày)", "Đã hết hạn" });
            _filterCombo.Location = new Point(420, 15);
            _filterCombo.Name = "_filterCombo";
            _filterCombo.Size = new Size(230, 25);
            _filterCombo.SelectedIndex = 0;
            //
            // _clearProductFilterButton
            //
            _clearProductFilterButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _clearProductFilterButton.FlatStyle = FlatStyle.Flat;
            _clearProductFilterButton.Font = new Font("Segoe UI", 9F);
            _clearProductFilterButton.Location = new Point(820, 12);
            _clearProductFilterButton.Name = "_clearProductFilterButton";
            _clearProductFilterButton.Size = new Size(160, 34);
            _clearProductFilterButton.Text = "Bỏ lọc sản phẩm";
            //
            // _refreshButton
            //
            _refreshButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _refreshButton.FlatStyle = FlatStyle.Flat;
            _refreshButton.Font = new Font("Segoe UI", 9F);
            _refreshButton.Location = new Point(1050, 12);
            _refreshButton.Name = "_refreshButton";
            _refreshButton.Size = new Size(92, 34);
            _refreshButton.Text = "Tải lại";
            //
            // _grid
            //
            _grid.AllowUserToAddRows = false;
            _grid.AllowUserToDeleteRows = false;
            _grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _grid.BackgroundColor = Color.White;
            _grid.BorderStyle = BorderStyle.None;
            _grid.ColumnHeadersHeight = 42;
            _colBatchCode.FillWeight = 85F;
            _colBatchCode.HeaderText = "Mã lô";
            _colBatchCode.MinimumWidth = 45;
            _colBatchCode.Name = "BatchCode";
            _colBatchCode.ReadOnly = true;
            _colBatchCode.SortMode = DataGridViewColumnSortMode.NotSortable;
            _colProduct.FillWeight = 155F;
            _colProduct.HeaderText = "Sản phẩm";
            _colProduct.MinimumWidth = 45;
            _colProduct.Name = "Product";
            _colProduct.ReadOnly = true;
            _colProduct.SortMode = DataGridViewColumnSortMode.NotSortable;
            _colLotNumber.FillWeight = 95F;
            _colLotNumber.HeaderText = "Số lô NCC";
            _colLotNumber.MinimumWidth = 45;
            _colLotNumber.Name = "LotNumber";
            _colLotNumber.ReadOnly = true;
            _colLotNumber.SortMode = DataGridViewColumnSortMode.NotSortable;
            _colSupplier.FillWeight = 125F;
            _colSupplier.HeaderText = "Nhà cung cấp";
            _colSupplier.MinimumWidth = 45;
            _colSupplier.Name = "Supplier";
            _colSupplier.ReadOnly = true;
            _colSupplier.SortMode = DataGridViewColumnSortMode.NotSortable;
            _colExpiryDate.FillWeight = 90F;
            _colExpiryDate.HeaderText = "Hạn sử dụng";
            _colExpiryDate.MinimumWidth = 45;
            _colExpiryDate.Name = "ExpiryDate";
            _colExpiryDate.ReadOnly = true;
            _colExpiryDate.SortMode = DataGridViewColumnSortMode.NotSortable;
            _colRemaining.FillWeight = 65F;
            _colRemaining.HeaderText = "Còn lại";
            _colRemaining.MinimumWidth = 45;
            _colRemaining.Name = "Remaining";
            _colRemaining.ReadOnly = true;
            _colRemaining.SortMode = DataGridViewColumnSortMode.NotSortable;
            _colUnitCost.FillWeight = 85F;
            _colUnitCost.HeaderText = "Giá nhập";
            _colUnitCost.MinimumWidth = 45;
            _colUnitCost.Name = "UnitCost";
            _colUnitCost.ReadOnly = true;
            _colUnitCost.SortMode = DataGridViewColumnSortMode.NotSortable;
            _colLossValue.FillWeight = 95F;
            _colLossValue.HeaderText = "Giá trị tồn";
            _colLossValue.MinimumWidth = 45;
            _colLossValue.Name = "LossValue";
            _colLossValue.ReadOnly = true;
            _colLossValue.SortMode = DataGridViewColumnSortMode.NotSortable;
            _colStatus.FillWeight = 100F;
            _colStatus.HeaderText = "Tình trạng";
            _colStatus.MinimumWidth = 45;
            _colStatus.Name = "Status";
            _colStatus.ReadOnly = true;
            _colStatus.SortMode = DataGridViewColumnSortMode.NotSortable;
            _grid.Columns.AddRange(new DataGridViewColumn[]
            {
                _colBatchCode,
                _colProduct,
                _colLotNumber,
                _colSupplier,
                _colExpiryDate,
                _colRemaining,
                _colUnitCost,
                _colLossValue,
                _colStatus
            });
            _grid.Dock = DockStyle.Fill;
            _grid.ReadOnly = true;
            _grid.RowHeadersVisible = false;
            _grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _grid.MultiSelect = false;
            _grid.RowTemplate.Height = 36;
            _grid.Name = "_grid";
            //
            // _footerLabel
            //
            _footerLabel.Dock = DockStyle.Fill;
            _footerLabel.Font = new Font("Segoe UI", 9F);
            _footerLabel.ForeColor = Color.FromArgb(100, 116, 139);
            _footerLabel.TextAlign = ContentAlignment.MiddleLeft;
            _footerLabel.Text = "Hàng hết hạn được chặn khỏi bán; chỉ người có quyền mới lập phiếu tiêu hủy.";
            _footerLabel.Name = "_footerLabel";
            //
            // ucExpiryBatchManagement
            //
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 247, 250);
            Controls.Add(_root);
            Font = new Font("Segoe UI", 9F);
            Name = "ucExpiryBatchManagement";
            Size = new Size(1200, 760);
            _filterPanel.ResumeLayout(false);
            _filterPanel.PerformLayout();
            _totalCard.ResumeLayout(false);
            _nearExpiryCard.ResumeLayout(false);
            _expiredCard.ResumeLayout(false);
            _summaryLayout.ResumeLayout(false);
            _headerPanel.ResumeLayout(false);
            _headerIconPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_headerIcon).EndInit();
            _root.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_grid).EndInit();
            ResumeLayout(false);
        }
    }
}
