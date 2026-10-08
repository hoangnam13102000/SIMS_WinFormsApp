using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Views.SystemManagement
{
    partial class ucAuditLog
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel _root;
        private Panel _headerPanel;
        private Label _titleLabel;
        private Label _subtitleLabel;
        private FlowLayoutPanel _statsPanel;
        private Panel _totalStatCard;
        private Panel _todayStatCard;
        private Panel _failedStatCard;
        private Panel _usersStatCard;
        private Label _totalStatTitle;
        private Label _totalStatValue;
        private Label _todayStatTitle;
        private Label _todayStatValue;
        private Label _failedStatTitle;
        private Label _failedStatValue;
        private Label _usersStatTitle;
        private Label _usersStatValue;
        private TableLayoutPanel _filterPanel;
        private DateTimePicker _fromDatePicker;
        private DateTimePicker _toDatePicker;
        private TextBox _searchTextBox;
        private ComboBox _actionComboBox;
        private ComboBox _tableComboBox;
        private CheckBox _incidentCheckBox;
        private Button _exportCsvButton;
        private Button _exportExcelButton;
        private Panel _gridPanel;
        private DataGridView _auditGrid;
        private DataGridViewTextBoxColumn _timeColumn;
        private DataGridViewTextBoxColumn _userColumn;
        private DataGridViewTextBoxColumn _actionColumn;
        private DataGridViewTextBoxColumn _tableColumn;
        private DataGridViewTextBoxColumn _descriptionColumn;
        private DataGridViewButtonColumn _detailColumn;
        private Label _emptyLabel;
        private FlowLayoutPanel _pagingPanel;
        private ComboBox _pageSizeComboBox;
        private Label _pageLabel;
        private Button _previousButton;
        private Button _nextButton;
        private Panel _busyOverlay;
        private Label _busyMessageLabel;
        private ProgressBar _busyProgressBar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            _root = new TableLayoutPanel();
            _headerPanel = new Panel();
            _titleLabel = new Label();
            _subtitleLabel = new Label();
            _statsPanel = new FlowLayoutPanel();
            _totalStatCard = new Panel();
            _todayStatCard = new Panel();
            _failedStatCard = new Panel();
            _usersStatCard = new Panel();
            _totalStatTitle = new Label();
            _totalStatValue = new Label();
            _todayStatTitle = new Label();
            _todayStatValue = new Label();
            _failedStatTitle = new Label();
            _failedStatValue = new Label();
            _usersStatTitle = new Label();
            _usersStatValue = new Label();
            _filterPanel = new TableLayoutPanel();
            _fromDatePicker = new DateTimePicker();
            _toDatePicker = new DateTimePicker();
            _searchTextBox = new TextBox();
            _actionComboBox = new ComboBox();
            _tableComboBox = new ComboBox();
            _incidentCheckBox = new CheckBox();
            _exportCsvButton = new Button();
            _exportExcelButton = new Button();
            _gridPanel = new Panel();
            _auditGrid = new DataGridView();
            _timeColumn = new DataGridViewTextBoxColumn();
            _userColumn = new DataGridViewTextBoxColumn();
            _actionColumn = new DataGridViewTextBoxColumn();
            _tableColumn = new DataGridViewTextBoxColumn();
            _descriptionColumn = new DataGridViewTextBoxColumn();
            _detailColumn = new DataGridViewButtonColumn();
            _emptyLabel = new Label();
            _pagingPanel = new FlowLayoutPanel();
            _pageSizeComboBox = new ComboBox();
            _pageLabel = new Label();
            _previousButton = new Button();
            _nextButton = new Button();
            _busyOverlay = new Panel();
            _busyMessageLabel = new Label();
            _busyProgressBar = new ProgressBar();
            _root.SuspendLayout();
            _headerPanel.SuspendLayout();
            _statsPanel.SuspendLayout();
            _filterPanel.SuspendLayout();
            _gridPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_auditGrid).BeginInit();
            _pagingPanel.SuspendLayout();
            _busyOverlay.SuspendLayout();
            SuspendLayout();

            BackColor = Color.FromArgb(248, 250, 252);
            Padding = new Padding(20);
            _root.ColumnCount = 1;
            _root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _root.Dock = DockStyle.Fill;
            _root.RowCount = 5;
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 82F));
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            _root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            _root.Controls.Add(_headerPanel, 0, 0);
            _root.Controls.Add(_statsPanel, 0, 1);
            _root.Controls.Add(_filterPanel, 0, 2);
            _root.Controls.Add(_gridPanel, 0, 3);
            _root.Controls.Add(_pagingPanel, 0, 4);

            _headerPanel.Dock = DockStyle.Fill;
            _headerPanel.Controls.Add(_subtitleLabel);
            _headerPanel.Controls.Add(_titleLabel);
            _titleLabel.AutoSize = true;
            _titleLabel.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            _titleLabel.ForeColor = Color.FromArgb(15, 23, 42);
            _titleLabel.Location = new Point(0, 0);
            _titleLabel.Text = "Nhật ký hệ thống";
            _subtitleLabel.AutoSize = true;
            _subtitleLabel.ForeColor = Color.FromArgb(100, 116, 139);
            _subtitleLabel.Location = new Point(2, 43);
            _subtitleLabel.Text = "Theo dõi hoạt động và sự cố phát sinh trong hệ thống";

            _statsPanel.Dock = DockStyle.Fill;
            _statsPanel.WrapContents = false;
            _statsPanel.Controls.AddRange(new Control[] {
                _totalStatCard, _todayStatCard, _failedStatCard, _usersStatCard
            });
            ConfigureStatCard(_totalStatCard, _totalStatTitle, _totalStatValue, "Tổng nhật ký");
            ConfigureStatCard(_todayStatCard, _todayStatTitle, _todayStatValue, "Hoạt động hôm nay");
            ConfigureStatCard(_failedStatCard, _failedStatTitle, _failedStatValue, "Đăng nhập thất bại");
            ConfigureStatCard(_usersStatCard, _usersStatTitle, _usersStatValue, "Người dùng hoạt động");

            _filterPanel.ColumnCount = 8;
            _filterPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 142F));
            _filterPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 142F));
            _filterPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _filterPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            _filterPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            _filterPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 115F));
            _filterPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 96F));
            _filterPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 104F));
            _filterPanel.Dock = DockStyle.Fill;
            _filterPanel.Padding = new Padding(0, 5, 0, 0);
            _filterPanel.RowCount = 1;
            _filterPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            _filterPanel.Controls.Add(_fromDatePicker, 0, 0);
            _filterPanel.Controls.Add(_toDatePicker, 1, 0);
            _filterPanel.Controls.Add(_searchTextBox, 2, 0);
            _filterPanel.Controls.Add(_actionComboBox, 3, 0);
            _filterPanel.Controls.Add(_tableComboBox, 4, 0);
            _filterPanel.Controls.Add(_incidentCheckBox, 5, 0);
            _filterPanel.Controls.Add(_exportCsvButton, 6, 0);
            _filterPanel.Controls.Add(_exportExcelButton, 7, 0);
            ConfigureDatePicker(_fromDatePicker, "Từ");
            ConfigureDatePicker(_toDatePicker, "Đến");
            _searchTextBox.Dock = DockStyle.Fill;
            _searchTextBox.Margin = new Padding(4, 2, 4, 4);
            _searchTextBox.Name = "_searchTextBox";
            _searchTextBox.AccessibleName = "Tìm người dùng, hành động hoặc nội dung";
            _actionComboBox.Dock = DockStyle.Fill;
            _actionComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            _actionComboBox.Margin = new Padding(4, 2, 4, 4);
            _actionComboBox.Name = "_actionComboBox";
            _tableComboBox.Dock = DockStyle.Fill;
            _tableComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            _tableComboBox.Margin = new Padding(4, 2, 4, 4);
            _tableComboBox.Name = "_tableComboBox";
            _incidentCheckBox.Dock = DockStyle.Fill;
            _incidentCheckBox.Margin = new Padding(6, 3, 4, 4);
            _incidentCheckBox.Text = "Nhật ký sự cố";
            _incidentCheckBox.TextAlign = ContentAlignment.MiddleLeft;
            ConfigureToolbarButton(_exportCsvButton, "CSV", 88);
            ConfigureToolbarButton(_exportExcelButton, "Excel", 96);

            _gridPanel.BackColor = Color.White;
            _gridPanel.BorderStyle = BorderStyle.FixedSingle;
            _gridPanel.Dock = DockStyle.Fill;
            _gridPanel.Controls.Add(_auditGrid);
            _gridPanel.Controls.Add(_emptyLabel);
            _auditGrid.AllowUserToAddRows = false;
            _auditGrid.AllowUserToDeleteRows = false;
            _auditGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _auditGrid.BackgroundColor = Color.White;
            _auditGrid.BorderStyle = BorderStyle.None;
            _auditGrid.ColumnHeadersHeight = 40;
            _auditGrid.Columns.AddRange(new DataGridViewColumn[] {
                _timeColumn, _userColumn, _actionColumn, _tableColumn, _descriptionColumn, _detailColumn
            });
            _auditGrid.Dock = DockStyle.Fill;
            _auditGrid.EnableHeadersVisualStyles = false;
            _auditGrid.GridColor = Color.FromArgb(226, 232, 240);
            _auditGrid.MultiSelect = false;
            _auditGrid.ReadOnly = true;
            _auditGrid.RowHeadersVisible = false;
            _auditGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _auditGrid.RowTemplate.Height = 42;
            ConfigureTextColumn(_timeColumn, "Thời gian", "Time", 17F);
            ConfigureTextColumn(_userColumn, "Người dùng", "User", 15F);
            ConfigureTextColumn(_actionColumn, "Hành động", "Action", 17F);
            ConfigureTextColumn(_tableColumn, "Bảng dữ liệu", "Table", 14F);
            ConfigureTextColumn(_descriptionColumn, "Chi tiết", "Description", 29F);
            _descriptionColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            _descriptionColumn.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            _detailColumn.FillWeight = 8F;
            _detailColumn.HeaderText = " ";
            _detailColumn.Name = "ViewDetail";
            _detailColumn.Text = "Xem";
            _detailColumn.UseColumnTextForButtonValue = true;
            _emptyLabel.Anchor = AnchorStyles.None;
            _emptyLabel.AutoSize = true;
            _emptyLabel.BackColor = Color.White;
            _emptyLabel.ForeColor = Color.FromArgb(100, 116, 139);
            _emptyLabel.Location = new Point(400, 180);
            _emptyLabel.Text = "Không có dữ liệu nhật ký phù hợp.";
            _emptyLabel.Visible = false;

            _pagingPanel.Dock = DockStyle.Fill;
            _pagingPanel.FlowDirection = FlowDirection.RightToLeft;
            _pagingPanel.WrapContents = false;
            _pagingPanel.Controls.AddRange(new Control[] {
                _nextButton, _pageLabel, _previousButton, _pageSizeComboBox
            });
            _nextButton.Margin = new Padding(4, 4, 0, 0);
            _nextButton.Size = new Size(78, 30);
            _nextButton.Text = "Sau";
            _previousButton.Margin = new Padding(4, 4, 0, 0);
            _previousButton.Size = new Size(78, 30);
            _previousButton.Text = "Trước";
            _pageLabel.AutoSize = false;
            _pageLabel.Margin = new Padding(8, 9, 8, 0);
            _pageLabel.Size = new Size(150, 24);
            _pageLabel.Text = "Trang 1 / 1";
            _pageLabel.TextAlign = ContentAlignment.MiddleCenter;
            _pageSizeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            _pageSizeComboBox.Items.AddRange(new object[] { "10 dòng", "20 dòng", "50 dòng" });
            _pageSizeComboBox.Margin = new Padding(8, 5, 8, 0);
            _pageSizeComboBox.SelectedIndex = 0;
            _pageSizeComboBox.Size = new Size(100, 28);

            _busyOverlay.BackColor = Color.FromArgb(235, 255, 255, 255);
            _busyOverlay.Dock = DockStyle.Fill;
            _busyOverlay.Controls.Add(_busyProgressBar);
            _busyOverlay.Controls.Add(_busyMessageLabel);
            _busyOverlay.Visible = false;
            _busyMessageLabel.Anchor = AnchorStyles.None;
            _busyMessageLabel.AutoSize = true;
            _busyMessageLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            _busyMessageLabel.Location = new Point(400, 310);
            _busyMessageLabel.Text = "Đang tải dữ liệu...";
            _busyProgressBar.Anchor = AnchorStyles.None;
            _busyProgressBar.Location = new Point(400, 350);
            _busyProgressBar.Size = new Size(300, 8);
            _busyProgressBar.Style = ProgressBarStyle.Marquee;
            _busyProgressBar.MarqueeAnimationSpeed = 25;

            Controls.Add(_root);
            Controls.Add(_busyOverlay);
            Name = "ucAuditLog";
            Size = new Size(1200, 760);
            _busyOverlay.BringToFront();
            _busyOverlay.ResumeLayout(false);
            _pagingPanel.ResumeLayout(false);
            _gridPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_auditGrid).EndInit();
            _filterPanel.ResumeLayout(false);
            _filterPanel.PerformLayout();
            _statsPanel.ResumeLayout(false);
            _headerPanel.ResumeLayout(false);
            _headerPanel.PerformLayout();
            _root.ResumeLayout(false);
            ResumeLayout(false);
        }

        private static void ConfigureStatCard(Panel card, Label title, Label value, string caption)
        {
            card.BackColor = Color.White;
            card.BorderStyle = BorderStyle.FixedSingle;
            card.Margin = new Padding(0, 0, 12, 0);
            card.Size = new Size(230, 72);
            card.Controls.Add(value);
            card.Controls.Add(title);
            title.AutoSize = true;
            title.Font = new Font("Segoe UI", 9F);
            title.ForeColor = Color.FromArgb(100, 116, 139);
            title.Location = new Point(12, 8);
            title.Text = caption;
            value.AutoSize = true;
            value.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
            value.ForeColor = Color.FromArgb(15, 23, 42);
            value.Location = new Point(12, 30);
            value.Text = "0";
        }

        private static void ConfigureDatePicker(DateTimePicker picker, string prefix)
        {
            picker.Dock = DockStyle.Fill;
            picker.Format = DateTimePickerFormat.Custom;
            picker.CustomFormat = "'" + prefix + " ' dd/MM/yy";
            picker.ShowCheckBox = true;
            picker.Checked = false;
            picker.Margin = new Padding(4, 2, 4, 4);
            picker.Name = prefix;
        }

        private static void ConfigureTextColumn(
            DataGridViewTextBoxColumn column, string header, string name, float weight)
        {
            column.FillWeight = weight;
            column.HeaderText = header;
            column.Name = name;
            column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private static void ConfigureToolbarButton(Button button, string text, int width)
        {
            button.Dock = DockStyle.Fill;
            button.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            button.FlatStyle = FlatStyle.Flat;
            button.Margin = new Padding(4, 2, 4, 4);
            button.Size = new Size(width, 32);
            button.Text = text;
        }
    }
}
