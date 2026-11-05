using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Views.Warehouse
{
    partial class ucExpiryBatchManagement
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel _root;
        private Panel _headerPanel;
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
            _footerLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)_grid).BeginInit();
            _root.SuspendLayout();
            _headerPanel.SuspendLayout();
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
            _headerPanel.BackColor = Color.Transparent;
            _headerPanel.Controls.Add(_titleLabel);
            _headerPanel.Controls.Add(_subtitleLabel);
            _headerPanel.Controls.Add(_disposeButton);
            _headerPanel.Dock = DockStyle.Fill;
            _headerPanel.Name = "_headerPanel";
            //
            // _titleLabel
            //
            _titleLabel.AutoSize = true;
            _titleLabel.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
            _titleLabel.ForeColor = Color.FromArgb(15, 23, 42);
            _titleLabel.Location = new Point(0, 0);
            _titleLabel.Name = "_titleLabel";
            _titleLabel.Text = "Quản lý sản phẩm hết hạn";
            //
            // _subtitleLabel
            //
            _subtitleLabel.AutoSize = true;
            _subtitleLabel.Font = new Font("Segoe UI", 9.5F);
            _subtitleLabel.ForeColor = Color.FromArgb(100, 116, 139);
            _subtitleLabel.Location = new Point(3, 50);
            _subtitleLabel.Name = "_subtitleLabel";
            _subtitleLabel.Text = "Theo dõi hàng hết hạn, xem chi tiết theo lô và lập phiếu tiêu hủy.";
            //
            // _disposeButton
            //
            _disposeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _disposeButton.BackColor = Color.FromArgb(220, 38, 38);
            _disposeButton.FlatAppearance.BorderSize = 0;
            _disposeButton.FlatStyle = FlatStyle.Flat;
            _disposeButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            _disposeButton.ForeColor = Color.White;
            _disposeButton.Location = new Point(940, 14);
            _disposeButton.Name = "_disposeButton";
            _disposeButton.Size = new Size(218, 42);
            _disposeButton.Text = "Lập phiếu tiêu hủy";
            _disposeButton.UseVisualStyleBackColor = false;
            _disposeButton.Enabled = false;
            this._disposeButton.Click += new System.EventHandler(this._disposeButton_Click);
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
            // summary cards
            //
            ConfigureSummaryCard(_expiredCard, _expiredValue, _expiredCaption,
                "0", "ĐƠN VỊ HẾT HẠN CHỜ XỬ LÝ", Color.FromArgb(254, 242, 242),
                Color.FromArgb(185, 28, 28), 0);
            ConfigureSummaryCard(_nearExpiryCard, _nearExpiryValue, _nearExpiryCaption,
                "0", "LÔ SẼ HẾT HẠN TRONG 30 NGÀY", Color.White,
                Color.FromArgb(217, 119, 6), 1);
            ConfigureSummaryCard(_totalCard, _totalValue, _totalCaption,
                "0", "TỔNG SỐ LÔ CÓ HẠN SỬ DỤNG", Color.White,
                Color.FromArgb(37, 99, 235), 2);
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
            this._searchBox.TextChanged += new System.EventHandler(this._searchBox_TextChanged);
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
            this._filterCombo.SelectedIndexChanged += new System.EventHandler(this._filterCombo_SelectedIndexChanged);
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
            this._clearProductFilterButton.Click += new System.EventHandler(this._clearProductFilterButton_Click);
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
            this._refreshButton.Click += new System.EventHandler(this._refreshButton_Click);
            //
            // _grid
            //
            _grid.AllowUserToAddRows = false;
            _grid.AllowUserToDeleteRows = false;
            _grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _grid.BackgroundColor = Color.White;
            _grid.BorderStyle = BorderStyle.None;
            _grid.ColumnHeadersHeight = 42;
            _grid.Columns.AddRange(new DataGridViewColumn[]
            {
                MakeColumn("BatchId", "ID", 45),
                MakeColumn("BatchCode", "Mã lô", 85),
                MakeColumn("Product", "Sản phẩm", 155),
                MakeColumn("LotNumber", "Số lô NCC", 95),
                MakeColumn("Supplier", "Nhà cung cấp", 125),
                MakeColumn("ExpiryDate", "Hạn sử dụng", 90),
                MakeColumn("Remaining", "Còn lại", 65),
                MakeColumn("UnitCost", "Giá nhập", 85),
                MakeColumn("LossValue", "Giá trị tồn", 95),
                MakeColumn("Status", "Tình trạng", 100)
            });
            _grid.Dock = DockStyle.Fill;
            _grid.ReadOnly = true;
            _grid.RowHeadersVisible = false;
            _grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _grid.MultiSelect = false;
            _grid.RowTemplate.Height = 36;
            _grid.Name = "_grid";
            _grid.Rows.Add("12", "LOT_000012", "Sữa tươi nguyên kem", "MILK-042", "NCC Thực phẩm An Tâm",
                "05/11/2026", "18", "32.000 ₫", "576.000 ₫", "Sắp hết hạn");
            _grid.Rows.Add("8", "LOT_000008", "Cà phê bột 500g", "LOT-CF-001", "NCC Cà phê Việt",
                "28/10/2026", "10", "65.000 ₫", "650.000 ₫", "Đã hết hạn");
            _grid.Rows[1].DefaultCellStyle.BackColor = Color.FromArgb(254, 242, 242);
            _grid.Rows[1].DefaultCellStyle.ForeColor = Color.FromArgb(153, 27, 27);
            this._grid.SelectionChanged += new System.EventHandler(this._grid_SelectionChanged);
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
            _headerPanel.PerformLayout();
            _root.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_grid).EndInit();
            this.Load += new System.EventHandler(this.ucExpiryBatchManagement_Load);
            this.VisibleChanged += new System.EventHandler(this.ucExpiryBatchManagement_VisibleChanged);
            ResumeLayout(false);
        }

        private static void ConfigureSummaryCard(
            Panel card, Label value, Label caption, string initialValue,
            string captionText, Color background, Color foreground, int column)
        {
            card.BackColor = background;
            card.BorderStyle = BorderStyle.FixedSingle;
            card.Controls.Add(caption);
            card.Controls.Add(value);
            card.Dock = DockStyle.Fill;
            card.Margin = column == 0 ? new Padding(0, 0, 8, 0) : new Padding(8, 0, 8, 0);
            card.Name = "summaryCard" + column;
            value.AutoSize = true;
            value.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            value.ForeColor = foreground;
            value.Location = new Point(16, 10);
            value.Text = initialValue;
            caption.AutoSize = true;
            caption.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            caption.ForeColor = Color.FromArgb(100, 116, 139);
            caption.Location = new Point(18, 58);
            caption.Text = captionText;
        }

        private static DataGridViewTextBoxColumn MakeColumn(string name, string header, int fill)
        {
            return new DataGridViewTextBoxColumn
            {
                Name = name,
                HeaderText = header,
                ReadOnly = true,
                FillWeight = fill,
                MinimumWidth = 45,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
        }
    }
}
