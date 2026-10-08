using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Views.Catalog
{
    partial class ucProductManagement
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel _root;
        private Panel _header;
        private Label _titleLabel;
        private Label _subtitleLabel;
        private FlowLayoutPanel _toolbar;
        private TextBox _searchTextBox;
        private ComboBox _statusComboBox;
        private Button _addButton;
        private Button _importButton;
        private Button _exportCsvButton;
        private Button _exportExcelButton;
        private DataGridView _productsGrid;
        private DataGridViewImageColumn _imageColumn;
        private DataGridViewTextBoxColumn _codeColumn;
        private DataGridViewTextBoxColumn _nameColumn;
        private DataGridViewTextBoxColumn _categoryColumn;
        private DataGridViewTextBoxColumn _priceColumn;
        private DataGridViewTextBoxColumn _stockColumn;
        private DataGridViewTextBoxColumn _statusColumn;
        private DataGridViewImageColumn _actionsColumn;
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
            components = new System.ComponentModel.Container();
            _root = new TableLayoutPanel();
            _header = new Panel();
            _titleLabel = new Label();
            _subtitleLabel = new Label();
            _toolbar = new FlowLayoutPanel();
            _searchTextBox = new TextBox();
            _statusComboBox = new ComboBox();
            _addButton = new Button();
            _importButton = new Button();
            _exportCsvButton = new Button();
            _exportExcelButton = new Button();
            _productsGrid = new DataGridView();
            _imageColumn = new DataGridViewImageColumn();
            _codeColumn = new DataGridViewTextBoxColumn();
            _nameColumn = new DataGridViewTextBoxColumn();
            _categoryColumn = new DataGridViewTextBoxColumn();
            _priceColumn = new DataGridViewTextBoxColumn();
            _stockColumn = new DataGridViewTextBoxColumn();
            _statusColumn = new DataGridViewTextBoxColumn();
            _actionsColumn = new DataGridViewImageColumn();
            _paging = new TableLayoutPanel();
            _pagingInfo = new FlowLayoutPanel();
            _pagingNavigation = new FlowLayoutPanel();
            _resultsLabel = new Label();
            _previousButton = new Button();
            _pageLabel = new Label();
            _nextButton = new Button();
            _pageSizeComboBox = new ComboBox();
            _root.SuspendLayout();
            _header.SuspendLayout();
            _toolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_productsGrid).BeginInit();
            _paging.SuspendLayout();
            _pagingInfo.SuspendLayout();
            _pagingNavigation.SuspendLayout();
            SuspendLayout();

            _root.ColumnCount = 1;
            _root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _root.Dock = DockStyle.Fill;
            _root.Padding = new Padding(24);
            _root.RowCount = 4;
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 82F));
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            _root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            _root.Controls.Add(_header, 0, 0);
            _root.Controls.Add(_toolbar, 0, 1);
            _root.Controls.Add(_productsGrid, 0, 2);
            _root.Controls.Add(_paging, 0, 3);

            _header.Controls.Add(_subtitleLabel);
            _header.Controls.Add(_titleLabel);
            _header.Dock = DockStyle.Fill;
            _titleLabel.AutoSize = true;
            _titleLabel.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
            _titleLabel.ForeColor = Color.FromArgb(15, 23, 42);
            _titleLabel.Location = new Point(0, 0);
            _titleLabel.Text = "Quản lý sản phẩm";
            _subtitleLabel.AutoSize = true;
            _subtitleLabel.ForeColor = Color.FromArgb(100, 116, 139);
            _subtitleLabel.Location = new Point(2, 44);
            _subtitleLabel.Text = "Danh sách sản phẩm, giá bán, tồn kho và trạng thái";
            _toolbar.Controls.AddRange(new Control[] {
                _searchTextBox, _statusComboBox, _addButton, _importButton, _exportCsvButton, _exportExcelButton
            });
            _toolbar.Dock = DockStyle.Fill;
            _toolbar.WrapContents = false;
            _searchTextBox.Margin = new Padding(0, 7, 8, 0);
            _searchTextBox.Name = "_searchTextBox";
            _searchTextBox.Size = new Size(270, 28);
            _searchTextBox.AccessibleName = "Tìm theo tên, mã hoặc danh mục";
            _statusComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            _statusComboBox.Items.AddRange(new object[] { "Tất cả trạng thái", "Đang hoạt động", "Vô hiệu hóa" });
            _statusComboBox.Margin = new Padding(4, 7, 8, 0);
            _statusComboBox.Name = "_statusComboBox";
            _statusComboBox.SelectedIndex = 0;
            _statusComboBox.Size = new Size(165, 28);
            _addButton.BackColor = Color.FromArgb(37, 99, 235);
            _addButton.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            _addButton.FlatAppearance.BorderSize = 0;
            _addButton.FlatStyle = FlatStyle.Flat;
            _addButton.ForeColor = Color.White;
            _addButton.Margin = new Padding(4, 4, 4, 0);
            _addButton.Size = new Size(132, 34);
            _addButton.Text = "Thêm sản phẩm";
            _importButton.BackColor = Color.White;
            _importButton.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            _importButton.FlatStyle = FlatStyle.Flat;
            _importButton.ForeColor = Color.FromArgb(51, 65, 85);
            _importButton.Margin = new Padding(4, 4, 4, 0);
            _importButton.Size = new Size(112, 34);
            _importButton.Text = "Nhập dữ liệu";
            _exportCsvButton.BackColor = Color.White;
            _exportCsvButton.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            _exportCsvButton.FlatStyle = FlatStyle.Flat;
            _exportCsvButton.ForeColor = Color.FromArgb(51, 65, 85);
            _exportCsvButton.Margin = new Padding(4, 4, 4, 0);
            _exportCsvButton.Size = new Size(94, 34);
            _exportCsvButton.Text = "Xuất CSV";
            _exportExcelButton.BackColor = Color.White;
            _exportExcelButton.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            _exportExcelButton.FlatStyle = FlatStyle.Flat;
            _exportExcelButton.ForeColor = Color.FromArgb(51, 65, 85);
            _exportExcelButton.Margin = new Padding(4, 4, 4, 0);
            _exportExcelButton.Size = new Size(104, 34);
            _exportExcelButton.Text = "Xuất Excel";

            _productsGrid.AllowUserToAddRows = false;
            _productsGrid.AllowUserToDeleteRows = false;
            _productsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _productsGrid.BackgroundColor = Color.White;
            _productsGrid.BorderStyle = BorderStyle.FixedSingle;
            _productsGrid.ColumnHeadersHeight = 42;
            _productsGrid.Columns.AddRange(new DataGridViewColumn[] {
                _imageColumn, _codeColumn, _nameColumn, _categoryColumn, _priceColumn, _stockColumn,
                _statusColumn, _actionsColumn
            });
            _productsGrid.Dock = DockStyle.Fill;
            _productsGrid.MultiSelect = false;
            _productsGrid.ReadOnly = true;
            _productsGrid.RowHeadersVisible = false;
            _productsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _imageColumn.FillWeight = 8F;
            _imageColumn.HeaderText = "Ảnh";
            _imageColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            _imageColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            _imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            _imageColumn.Name = "Image";
            _imageColumn.Width = 72;
            _codeColumn.FillWeight = 12F;
            _codeColumn.HeaderText = "Mã";
            _codeColumn.Name = "Code";
            _nameColumn.FillWeight = 23F;
            _nameColumn.HeaderText = "Tên sản phẩm";
            _nameColumn.Name = "Name";
            _categoryColumn.FillWeight = 15F;
            _categoryColumn.HeaderText = "Danh mục";
            _categoryColumn.Name = "Category";
            _priceColumn.FillWeight = 12F;
            _priceColumn.HeaderText = "Giá bán";
            _priceColumn.Name = "SellPrice";
            _stockColumn.FillWeight = 8F;
            _stockColumn.HeaderText = "Tồn kho";
            _stockColumn.Name = "Stock";
            _statusColumn.FillWeight = 12F;
            _statusColumn.HeaderText = "Trạng thái";
            _statusColumn.Name = "Status";
            _actionsColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            _actionsColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            _actionsColumn.HeaderText = "Thao tác";
            _actionsColumn.ImageLayout = DataGridViewImageCellLayout.Normal;
            _actionsColumn.Name = "Actions";
            _actionsColumn.ToolTipText = "Xem, sửa hoặc đổi trạng thái sản phẩm";
            _actionsColumn.Width = 120;
            _productsGrid.RowTemplate.Height = 52;

            _paging.ColumnCount = 2;
            _paging.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58F));
            _paging.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42F));
            _paging.Dock = DockStyle.Fill;
            _paging.Margin = new Padding(0);
            _paging.RowCount = 1;
            _paging.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            _paging.BackColor = Color.White;
            _paging.Controls.Add(_pagingInfo, 0, 0);
            _paging.Controls.Add(_pagingNavigation, 1, 0);

            _pagingInfo.Dock = DockStyle.Fill;
            _pagingInfo.FlowDirection = FlowDirection.LeftToRight;
            _pagingInfo.WrapContents = false;
            _pagingInfo.Padding = new Padding(12, 0, 0, 0);
            _pagingInfo.Controls.Add(_resultsLabel);
            _pagingInfo.Controls.Add(_pageSizeComboBox);
            _resultsLabel.AutoSize = false;
            _resultsLabel.Size = new Size(190, 44);
            _resultsLabel.Margin = new Padding(0, 0, 10, 0);
            _resultsLabel.Text = "Hiển thị 0 sản phẩm";
            _resultsLabel.TextAlign = ContentAlignment.MiddleLeft;
            _resultsLabel.ForeColor = Color.FromArgb(100, 116, 139);
            _resultsLabel.Font = new Font("Segoe UI", 9F);
            _resultsLabel.AccessibleName = "Số sản phẩm đang hiển thị";

            _pagingNavigation.Dock = DockStyle.Fill;
            _pagingNavigation.FlowDirection = FlowDirection.RightToLeft;
            _pagingNavigation.WrapContents = false;
            _pagingNavigation.Padding = new Padding(0, 5, 0, 5);
            _pagingNavigation.Controls.AddRange(new Control[] { _nextButton, _pageLabel, _previousButton });
            _previousButton.FlatStyle = FlatStyle.Flat;
            _previousButton.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            _previousButton.FlatAppearance.BorderSize = 1;
            _previousButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(239, 246, 255);
            _previousButton.BackColor = Color.White;
            _previousButton.ForeColor = Color.FromArgb(51, 65, 85);
            _previousButton.Font = new Font("Segoe UI Symbol", 16F, FontStyle.Regular);
            _previousButton.Margin = new Padding(2, 0, 2, 0);
            _previousButton.Size = new Size(36, 34);
            _previousButton.Text = "‹";
            _previousButton.AccessibleName = "Trang trước";
            _nextButton.FlatStyle = FlatStyle.Flat;
            _nextButton.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            _nextButton.FlatAppearance.BorderSize = 1;
            _nextButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(239, 246, 255);
            _nextButton.BackColor = Color.White;
            _nextButton.ForeColor = Color.FromArgb(51, 65, 85);
            _nextButton.Font = new Font("Segoe UI Symbol", 16F, FontStyle.Regular);
            _nextButton.Margin = new Padding(2, 0, 2, 0);
            _nextButton.Size = new Size(36, 34);
            _nextButton.Text = "›";
            _nextButton.AccessibleName = "Trang sau";
            _pageLabel.AutoSize = false;
            _pageLabel.Margin = new Padding(8, 0, 8, 0);
            _pageLabel.Size = new Size(112, 34);
            _pageLabel.Text = "Trang 1 / 1";
            _pageLabel.TextAlign = ContentAlignment.MiddleCenter;
            _pageLabel.ForeColor = Color.FromArgb(51, 65, 85);
            _pageLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            _pageLabel.BackColor = Color.FromArgb(248, 250, 252);
            _pageLabel.AccessibleName = "Trang hiện tại";
            _pageSizeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            _pageSizeComboBox.Items.AddRange(new object[] { "10 dòng", "20 dòng", "50 dòng" });
            _pageSizeComboBox.Margin = new Padding(0, 8, 0, 0);
            _pageSizeComboBox.SelectedIndex = 0;
            _pageSizeComboBox.Size = new Size(94, 28);
            _pageSizeComboBox.AccessibleName = "Số dòng mỗi trang";

            Controls.Add(_root);
            Name = "ucProductManagement";
            Size = new Size(1100, 680);
            _pagingNavigation.ResumeLayout(false);
            _pagingInfo.ResumeLayout(false);
            _paging.ResumeLayout(false);
            _toolbar.ResumeLayout(false);
            _toolbar.PerformLayout();
            _header.ResumeLayout(false);
            _header.PerformLayout();
            _root.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_productsGrid).EndInit();
            ResumeLayout(false);
        }

    }
}
