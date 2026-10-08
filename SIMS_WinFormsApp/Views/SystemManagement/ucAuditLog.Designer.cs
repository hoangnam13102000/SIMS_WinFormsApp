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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this._root = new System.Windows.Forms.TableLayoutPanel();
            this._headerPanel = new System.Windows.Forms.Panel();
            this._subtitleLabel = new System.Windows.Forms.Label();
            this._titleLabel = new System.Windows.Forms.Label();
            this._statsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this._totalStatCard = new System.Windows.Forms.Panel();
            this._totalStatValue = new System.Windows.Forms.Label();
            this._totalStatTitle = new System.Windows.Forms.Label();
            this._todayStatCard = new System.Windows.Forms.Panel();
            this._todayStatValue = new System.Windows.Forms.Label();
            this._todayStatTitle = new System.Windows.Forms.Label();
            this._failedStatCard = new System.Windows.Forms.Panel();
            this._failedStatValue = new System.Windows.Forms.Label();
            this._failedStatTitle = new System.Windows.Forms.Label();
            this._usersStatCard = new System.Windows.Forms.Panel();
            this._usersStatValue = new System.Windows.Forms.Label();
            this._usersStatTitle = new System.Windows.Forms.Label();
            this._filterPanel = new System.Windows.Forms.TableLayoutPanel();
            this._fromDatePicker = new System.Windows.Forms.DateTimePicker();
            this._toDatePicker = new System.Windows.Forms.DateTimePicker();
            this._searchTextBox = new System.Windows.Forms.TextBox();
            this._actionComboBox = new System.Windows.Forms.ComboBox();
            this._tableComboBox = new System.Windows.Forms.ComboBox();
            this._incidentCheckBox = new System.Windows.Forms.CheckBox();
            this._exportCsvButton = new System.Windows.Forms.Button();
            this._exportExcelButton = new System.Windows.Forms.Button();
            this._gridPanel = new System.Windows.Forms.Panel();
            this._auditGrid = new System.Windows.Forms.DataGridView();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.User = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Action = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Table = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ViewDetail = new System.Windows.Forms.DataGridViewButtonColumn();
            this._emptyLabel = new System.Windows.Forms.Label();
            this._pagingPanel = new System.Windows.Forms.FlowLayoutPanel();
            this._nextButton = new System.Windows.Forms.Button();
            this._pageLabel = new System.Windows.Forms.Label();
            this._previousButton = new System.Windows.Forms.Button();
            this._pageSizeComboBox = new System.Windows.Forms.ComboBox();
            this._busyOverlay = new System.Windows.Forms.Panel();
            this._busyProgressBar = new System.Windows.Forms.ProgressBar();
            this._busyMessageLabel = new System.Windows.Forms.Label();
            this._root.SuspendLayout();
            this._headerPanel.SuspendLayout();
            this._statsPanel.SuspendLayout();
            this._totalStatCard.SuspendLayout();
            this._todayStatCard.SuspendLayout();
            this._failedStatCard.SuspendLayout();
            this._usersStatCard.SuspendLayout();
            this._filterPanel.SuspendLayout();
            this._gridPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._auditGrid)).BeginInit();
            this._pagingPanel.SuspendLayout();
            this._busyOverlay.SuspendLayout();
            this.SuspendLayout();
            // 
            // _root
            // 
            this._root.ColumnCount = 1;
            this._root.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._root.Controls.Add(this._headerPanel, 0, 0);
            this._root.Controls.Add(this._statsPanel, 0, 1);
            this._root.Controls.Add(this._filterPanel, 0, 2);
            this._root.Controls.Add(this._gridPanel, 0, 3);
            this._root.Controls.Add(this._pagingPanel, 0, 4);
            this._root.Dock = System.Windows.Forms.DockStyle.Fill;
            this._root.Location = new System.Drawing.Point(20, 20);
            this._root.Name = "_root";
            this._root.RowCount = 5;
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this._root.Size = new System.Drawing.Size(1160, 720);
            this._root.TabIndex = 0;
            // 
            // _headerPanel
            // 
            this._headerPanel.Controls.Add(this._subtitleLabel);
            this._headerPanel.Controls.Add(this._titleLabel);
            this._headerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._headerPanel.Location = new System.Drawing.Point(3, 3);
            this._headerPanel.Name = "_headerPanel";
            this._headerPanel.Size = new System.Drawing.Size(1154, 64);
            this._headerPanel.TabIndex = 0;
            // 
            // _subtitleLabel
            // 
            this._subtitleLabel.AutoSize = true;
            this._subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._subtitleLabel.Location = new System.Drawing.Point(2, 43);
            this._subtitleLabel.Name = "_subtitleLabel";
            this._subtitleLabel.Size = new System.Drawing.Size(385, 20);
            this._subtitleLabel.TabIndex = 0;
            this._subtitleLabel.Text = "Theo dõi hoạt động và sự cố phát sinh trong hệ thống";
            // 
            // _titleLabel
            // 
            this._titleLabel.AutoSize = true;
            this._titleLabel.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this._titleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._titleLabel.Location = new System.Drawing.Point(0, 0);
            this._titleLabel.Name = "_titleLabel";
            this._titleLabel.Size = new System.Drawing.Size(353, 54);
            this._titleLabel.TabIndex = 1;
            this._titleLabel.Text = "Nhật ký hệ thống";
            // 
            // _statsPanel
            // 
            this._statsPanel.Controls.Add(this._totalStatCard);
            this._statsPanel.Controls.Add(this._todayStatCard);
            this._statsPanel.Controls.Add(this._failedStatCard);
            this._statsPanel.Controls.Add(this._usersStatCard);
            this._statsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._statsPanel.Location = new System.Drawing.Point(3, 73);
            this._statsPanel.Name = "_statsPanel";
            this._statsPanel.Size = new System.Drawing.Size(1154, 76);
            this._statsPanel.TabIndex = 1;
            this._statsPanel.WrapContents = false;
            // 
            // _totalStatCard
            // 
            this._totalStatCard.BackColor = System.Drawing.Color.White;
            this._totalStatCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._totalStatCard.Controls.Add(this._totalStatValue);
            this._totalStatCard.Controls.Add(this._totalStatTitle);
            this._totalStatCard.Location = new System.Drawing.Point(0, 0);
            this._totalStatCard.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this._totalStatCard.Name = "_totalStatCard";
            this._totalStatCard.Size = new System.Drawing.Size(230, 72);
            this._totalStatCard.TabIndex = 0;
            // 
            // _totalStatValue
            // 
            this._totalStatValue.AutoSize = true;
            this._totalStatValue.Font = new System.Drawing.Font("Segoe UI", 17F, System.Drawing.FontStyle.Bold);
            this._totalStatValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._totalStatValue.Location = new System.Drawing.Point(12, 30);
            this._totalStatValue.Name = "_totalStatValue";
            this._totalStatValue.Size = new System.Drawing.Size(40, 46);
            this._totalStatValue.TabIndex = 0;
            this._totalStatValue.Text = "0";
            // 
            // _totalStatTitle
            // 
            this._totalStatTitle.AutoSize = true;
            this._totalStatTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._totalStatTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._totalStatTitle.Location = new System.Drawing.Point(12, 8);
            this._totalStatTitle.Name = "_totalStatTitle";
            this._totalStatTitle.Size = new System.Drawing.Size(116, 25);
            this._totalStatTitle.TabIndex = 1;
            this._totalStatTitle.Text = "Tổng nhật ký";
            // 
            // _todayStatCard
            // 
            this._todayStatCard.BackColor = System.Drawing.Color.White;
            this._todayStatCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._todayStatCard.Controls.Add(this._todayStatValue);
            this._todayStatCard.Controls.Add(this._todayStatTitle);
            this._todayStatCard.Location = new System.Drawing.Point(242, 0);
            this._todayStatCard.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this._todayStatCard.Name = "_todayStatCard";
            this._todayStatCard.Size = new System.Drawing.Size(230, 72);
            this._todayStatCard.TabIndex = 1;
            // 
            // _todayStatValue
            // 
            this._todayStatValue.AutoSize = true;
            this._todayStatValue.Font = new System.Drawing.Font("Segoe UI", 17F, System.Drawing.FontStyle.Bold);
            this._todayStatValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._todayStatValue.Location = new System.Drawing.Point(12, 30);
            this._todayStatValue.Name = "_todayStatValue";
            this._todayStatValue.Size = new System.Drawing.Size(40, 46);
            this._todayStatValue.TabIndex = 0;
            this._todayStatValue.Text = "0";
            // 
            // _todayStatTitle
            // 
            this._todayStatTitle.AutoSize = true;
            this._todayStatTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._todayStatTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._todayStatTitle.Location = new System.Drawing.Point(12, 8);
            this._todayStatTitle.Name = "_todayStatTitle";
            this._todayStatTitle.Size = new System.Drawing.Size(174, 25);
            this._todayStatTitle.TabIndex = 1;
            this._todayStatTitle.Text = "Hoạt động hôm nay";
            // 
            // _failedStatCard
            // 
            this._failedStatCard.BackColor = System.Drawing.Color.White;
            this._failedStatCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._failedStatCard.Controls.Add(this._failedStatValue);
            this._failedStatCard.Controls.Add(this._failedStatTitle);
            this._failedStatCard.Location = new System.Drawing.Point(484, 0);
            this._failedStatCard.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this._failedStatCard.Name = "_failedStatCard";
            this._failedStatCard.Size = new System.Drawing.Size(230, 72);
            this._failedStatCard.TabIndex = 2;
            // 
            // _failedStatValue
            // 
            this._failedStatValue.AutoSize = true;
            this._failedStatValue.Font = new System.Drawing.Font("Segoe UI", 17F, System.Drawing.FontStyle.Bold);
            this._failedStatValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._failedStatValue.Location = new System.Drawing.Point(12, 30);
            this._failedStatValue.Name = "_failedStatValue";
            this._failedStatValue.Size = new System.Drawing.Size(40, 46);
            this._failedStatValue.TabIndex = 0;
            this._failedStatValue.Text = "0";
            // 
            // _failedStatTitle
            // 
            this._failedStatTitle.AutoSize = true;
            this._failedStatTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._failedStatTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._failedStatTitle.Location = new System.Drawing.Point(12, 8);
            this._failedStatTitle.Name = "_failedStatTitle";
            this._failedStatTitle.Size = new System.Drawing.Size(165, 25);
            this._failedStatTitle.TabIndex = 1;
            this._failedStatTitle.Text = "Đăng nhập thất bại";
            // 
            // _usersStatCard
            // 
            this._usersStatCard.BackColor = System.Drawing.Color.White;
            this._usersStatCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._usersStatCard.Controls.Add(this._usersStatValue);
            this._usersStatCard.Controls.Add(this._usersStatTitle);
            this._usersStatCard.Location = new System.Drawing.Point(726, 0);
            this._usersStatCard.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this._usersStatCard.Name = "_usersStatCard";
            this._usersStatCard.Size = new System.Drawing.Size(230, 72);
            this._usersStatCard.TabIndex = 3;
            // 
            // _usersStatValue
            // 
            this._usersStatValue.AutoSize = true;
            this._usersStatValue.Font = new System.Drawing.Font("Segoe UI", 17F, System.Drawing.FontStyle.Bold);
            this._usersStatValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._usersStatValue.Location = new System.Drawing.Point(12, 30);
            this._usersStatValue.Name = "_usersStatValue";
            this._usersStatValue.Size = new System.Drawing.Size(40, 46);
            this._usersStatValue.TabIndex = 0;
            this._usersStatValue.Text = "0";
            // 
            // _usersStatTitle
            // 
            this._usersStatTitle.AutoSize = true;
            this._usersStatTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._usersStatTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._usersStatTitle.Location = new System.Drawing.Point(12, 8);
            this._usersStatTitle.Name = "_usersStatTitle";
            this._usersStatTitle.Size = new System.Drawing.Size(198, 25);
            this._usersStatTitle.TabIndex = 1;
            this._usersStatTitle.Text = "Người dùng hoạt động";
            // 
            // _filterPanel
            // 
            this._filterPanel.ColumnCount = 8;
            this._filterPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 142F));
            this._filterPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 142F));
            this._filterPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._filterPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this._filterPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this._filterPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            this._filterPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this._filterPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 104F));
            this._filterPanel.Controls.Add(this._fromDatePicker, 0, 0);
            this._filterPanel.Controls.Add(this._toDatePicker, 1, 0);
            this._filterPanel.Controls.Add(this._searchTextBox, 2, 0);
            this._filterPanel.Controls.Add(this._actionComboBox, 3, 0);
            this._filterPanel.Controls.Add(this._tableComboBox, 4, 0);
            this._filterPanel.Controls.Add(this._incidentCheckBox, 5, 0);
            this._filterPanel.Controls.Add(this._exportCsvButton, 6, 0);
            this._filterPanel.Controls.Add(this._exportExcelButton, 7, 0);
            this._filterPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._filterPanel.Location = new System.Drawing.Point(3, 155);
            this._filterPanel.Name = "_filterPanel";
            this._filterPanel.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this._filterPanel.RowCount = 1;
            this._filterPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._filterPanel.Size = new System.Drawing.Size(1154, 42);
            this._filterPanel.TabIndex = 2;
            // 
            // _fromDatePicker
            // 
            this._fromDatePicker.Checked = false;
            this._fromDatePicker.CustomFormat = "\'Từ \' dd/MM/yy";
            this._fromDatePicker.Dock = System.Windows.Forms.DockStyle.Fill;
            this._fromDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._fromDatePicker.Location = new System.Drawing.Point(4, 7);
            this._fromDatePicker.Margin = new System.Windows.Forms.Padding(4, 2, 4, 4);
            this._fromDatePicker.Name = "_fromDatePicker";
            this._fromDatePicker.ShowCheckBox = true;
            this._fromDatePicker.Size = new System.Drawing.Size(134, 26);
            this._fromDatePicker.TabIndex = 0;
            // 
            // _toDatePicker
            // 
            this._toDatePicker.Checked = false;
            this._toDatePicker.CustomFormat = "\'Đến \' dd/MM/yy";
            this._toDatePicker.Dock = System.Windows.Forms.DockStyle.Fill;
            this._toDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._toDatePicker.Location = new System.Drawing.Point(146, 7);
            this._toDatePicker.Margin = new System.Windows.Forms.Padding(4, 2, 4, 4);
            this._toDatePicker.Name = "_toDatePicker";
            this._toDatePicker.ShowCheckBox = true;
            this._toDatePicker.Size = new System.Drawing.Size(134, 26);
            this._toDatePicker.TabIndex = 1;
            // 
            // _searchTextBox
            // 
            this._searchTextBox.AccessibleName = "Tìm người dùng, hành động hoặc nội dung";
            this._searchTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._searchTextBox.Location = new System.Drawing.Point(288, 7);
            this._searchTextBox.Margin = new System.Windows.Forms.Padding(4, 2, 4, 4);
            this._searchTextBox.Name = "_searchTextBox";
            this._searchTextBox.Size = new System.Drawing.Size(257, 26);
            this._searchTextBox.TabIndex = 2;
            // 
            // _actionComboBox
            // 
            this._actionComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._actionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._actionComboBox.Location = new System.Drawing.Point(553, 7);
            this._actionComboBox.Margin = new System.Windows.Forms.Padding(4, 2, 4, 4);
            this._actionComboBox.Name = "_actionComboBox";
            this._actionComboBox.Size = new System.Drawing.Size(142, 28);
            this._actionComboBox.TabIndex = 3;
            // 
            // _tableComboBox
            // 
            this._tableComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tableComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._tableComboBox.Location = new System.Drawing.Point(703, 7);
            this._tableComboBox.Margin = new System.Windows.Forms.Padding(4, 2, 4, 4);
            this._tableComboBox.Name = "_tableComboBox";
            this._tableComboBox.Size = new System.Drawing.Size(132, 28);
            this._tableComboBox.TabIndex = 4;
            // 
            // _incidentCheckBox
            // 
            this._incidentCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._incidentCheckBox.Location = new System.Drawing.Point(845, 8);
            this._incidentCheckBox.Margin = new System.Windows.Forms.Padding(6, 3, 4, 4);
            this._incidentCheckBox.Name = "_incidentCheckBox";
            this._incidentCheckBox.Size = new System.Drawing.Size(105, 30);
            this._incidentCheckBox.TabIndex = 5;
            this._incidentCheckBox.Text = "Nhật ký sự cố";
            // 
            // _exportCsvButton
            // 
            this._exportCsvButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this._exportCsvButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this._exportCsvButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._exportCsvButton.Location = new System.Drawing.Point(958, 7);
            this._exportCsvButton.Margin = new System.Windows.Forms.Padding(4, 2, 4, 4);
            this._exportCsvButton.Name = "_exportCsvButton";
            this._exportCsvButton.Size = new System.Drawing.Size(88, 31);
            this._exportCsvButton.TabIndex = 6;
            this._exportCsvButton.Text = "CSV";
            // 
            // _exportExcelButton
            // 
            this._exportExcelButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this._exportExcelButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this._exportExcelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._exportExcelButton.Location = new System.Drawing.Point(1054, 7);
            this._exportExcelButton.Margin = new System.Windows.Forms.Padding(4, 2, 4, 4);
            this._exportExcelButton.Name = "_exportExcelButton";
            this._exportExcelButton.Size = new System.Drawing.Size(96, 31);
            this._exportExcelButton.TabIndex = 7;
            this._exportExcelButton.Text = "Excel";
            // 
            // _gridPanel
            // 
            this._gridPanel.BackColor = System.Drawing.Color.White;
            this._gridPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._gridPanel.Controls.Add(this._auditGrid);
            this._gridPanel.Controls.Add(this._emptyLabel);
            this._gridPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridPanel.Location = new System.Drawing.Point(3, 203);
            this._gridPanel.Name = "_gridPanel";
            this._gridPanel.Size = new System.Drawing.Size(1154, 470);
            this._gridPanel.TabIndex = 3;
            // 
            // _auditGrid
            // 
            this._auditGrid.AllowUserToAddRows = false;
            this._auditGrid.AllowUserToDeleteRows = false;
            this._auditGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._auditGrid.BackgroundColor = System.Drawing.Color.White;
            this._auditGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._auditGrid.ColumnHeadersHeight = 40;
            this._auditGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Time,
            this.User,
            this.Action,
            this.Table,
            this.Description,
            this.ViewDetail});
            this._auditGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._auditGrid.EnableHeadersVisualStyles = false;
            this._auditGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this._auditGrid.Location = new System.Drawing.Point(0, 0);
            this._auditGrid.MultiSelect = false;
            this._auditGrid.Name = "_auditGrid";
            this._auditGrid.ReadOnly = true;
            this._auditGrid.RowHeadersVisible = false;
            this._auditGrid.RowHeadersWidth = 62;
            this._auditGrid.RowTemplate.Height = 42;
            this._auditGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._auditGrid.Size = new System.Drawing.Size(1152, 468);
            this._auditGrid.TabIndex = 0;
            // 
            // Time
            // 
            this.Time.FillWeight = 17F;
            this.Time.HeaderText = "Thời gian";
            this.Time.MinimumWidth = 8;
            this.Time.Name = "Time";
            this.Time.ReadOnly = true;
            this.Time.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // User
            // 
            this.User.FillWeight = 15F;
            this.User.HeaderText = "Người dùng";
            this.User.MinimumWidth = 8;
            this.User.Name = "User";
            this.User.ReadOnly = true;
            this.User.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Action
            // 
            this.Action.FillWeight = 17F;
            this.Action.HeaderText = "Hành động";
            this.Action.MinimumWidth = 8;
            this.Action.Name = "Action";
            this.Action.ReadOnly = true;
            this.Action.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Table
            // 
            this.Table.FillWeight = 14F;
            this.Table.HeaderText = "Bảng dữ liệu";
            this.Table.MinimumWidth = 8;
            this.Table.Name = "Table";
            this.Table.ReadOnly = true;
            this.Table.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Description
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Description.DefaultCellStyle = dataGridViewCellStyle1;
            this.Description.FillWeight = 29F;
            this.Description.HeaderText = "Chi tiết";
            this.Description.MinimumWidth = 8;
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ViewDetail
            // 
            this.ViewDetail.FillWeight = 8F;
            this.ViewDetail.HeaderText = " ";
            this.ViewDetail.MinimumWidth = 8;
            this.ViewDetail.Name = "ViewDetail";
            this.ViewDetail.ReadOnly = true;
            this.ViewDetail.Text = "Xem";
            this.ViewDetail.UseColumnTextForButtonValue = true;
            // 
            // _emptyLabel
            // 
            this._emptyLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._emptyLabel.AutoSize = true;
            this._emptyLabel.BackColor = System.Drawing.Color.White;
            this._emptyLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._emptyLabel.Location = new System.Drawing.Point(876, 364);
            this._emptyLabel.Name = "_emptyLabel";
            this._emptyLabel.Size = new System.Drawing.Size(247, 20);
            this._emptyLabel.TabIndex = 1;
            this._emptyLabel.Text = "Không có dữ liệu nhật ký phù hợp.";
            this._emptyLabel.Visible = false;
            // 
            // _pagingPanel
            // 
            this._pagingPanel.Controls.Add(this._nextButton);
            this._pagingPanel.Controls.Add(this._pageLabel);
            this._pagingPanel.Controls.Add(this._previousButton);
            this._pagingPanel.Controls.Add(this._pageSizeComboBox);
            this._pagingPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pagingPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._pagingPanel.Location = new System.Drawing.Point(3, 679);
            this._pagingPanel.Name = "_pagingPanel";
            this._pagingPanel.Size = new System.Drawing.Size(1154, 38);
            this._pagingPanel.TabIndex = 4;
            this._pagingPanel.WrapContents = false;
            // 
            // _nextButton
            // 
            this._nextButton.Location = new System.Drawing.Point(1076, 4);
            this._nextButton.Margin = new System.Windows.Forms.Padding(4, 4, 0, 0);
            this._nextButton.Name = "_nextButton";
            this._nextButton.Size = new System.Drawing.Size(78, 30);
            this._nextButton.TabIndex = 0;
            this._nextButton.Text = "Sau";
            // 
            // _pageLabel
            // 
            this._pageLabel.Location = new System.Drawing.Point(914, 9);
            this._pageLabel.Margin = new System.Windows.Forms.Padding(8, 9, 8, 0);
            this._pageLabel.Name = "_pageLabel";
            this._pageLabel.Size = new System.Drawing.Size(150, 24);
            this._pageLabel.TabIndex = 1;
            this._pageLabel.Text = "Trang 1 / 1";
            this._pageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _previousButton
            // 
            this._previousButton.Location = new System.Drawing.Point(828, 4);
            this._previousButton.Margin = new System.Windows.Forms.Padding(4, 4, 0, 0);
            this._previousButton.Name = "_previousButton";
            this._previousButton.Size = new System.Drawing.Size(78, 30);
            this._previousButton.TabIndex = 2;
            this._previousButton.Text = "Trước";
            // 
            // _pageSizeComboBox
            // 
            this._pageSizeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._pageSizeComboBox.Items.AddRange(new object[] {
            "10 dòng",
            "20 dòng",
            "50 dòng"});
            this._pageSizeComboBox.Location = new System.Drawing.Point(716, 5);
            this._pageSizeComboBox.Margin = new System.Windows.Forms.Padding(8, 5, 8, 0);
            this._pageSizeComboBox.Name = "_pageSizeComboBox";
            this._pageSizeComboBox.Size = new System.Drawing.Size(100, 28);
            this._pageSizeComboBox.TabIndex = 3;
            // 
            // _busyOverlay
            // 
            this._busyOverlay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._busyOverlay.Controls.Add(this._busyProgressBar);
            this._busyOverlay.Controls.Add(this._busyMessageLabel);
            this._busyOverlay.Dock = System.Windows.Forms.DockStyle.Fill;
            this._busyOverlay.Location = new System.Drawing.Point(20, 20);
            this._busyOverlay.Name = "_busyOverlay";
            this._busyOverlay.Size = new System.Drawing.Size(1160, 720);
            this._busyOverlay.TabIndex = 1;
            this._busyOverlay.Visible = false;
            // 
            // _busyProgressBar
            // 
            this._busyProgressBar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._busyProgressBar.Location = new System.Drawing.Point(880, 660);
            this._busyProgressBar.MarqueeAnimationSpeed = 25;
            this._busyProgressBar.Name = "_busyProgressBar";
            this._busyProgressBar.Size = new System.Drawing.Size(300, 8);
            this._busyProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this._busyProgressBar.TabIndex = 0;
            // 
            // _busyMessageLabel
            // 
            this._busyMessageLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._busyMessageLabel.AutoSize = true;
            this._busyMessageLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this._busyMessageLabel.Location = new System.Drawing.Point(880, 620);
            this._busyMessageLabel.Name = "_busyMessageLabel";
            this._busyMessageLabel.Size = new System.Drawing.Size(219, 32);
            this._busyMessageLabel.TabIndex = 1;
            this._busyMessageLabel.Text = "Đang tải dữ liệu...";
            // 
            // ucAuditLog
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.Controls.Add(this._root);
            this.Controls.Add(this._busyOverlay);
            this.Name = "ucAuditLog";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.Size = new System.Drawing.Size(1200, 760);
            this._root.ResumeLayout(false);
            this._headerPanel.ResumeLayout(false);
            this._headerPanel.PerformLayout();
            this._statsPanel.ResumeLayout(false);
            this._totalStatCard.ResumeLayout(false);
            this._totalStatCard.PerformLayout();
            this._todayStatCard.ResumeLayout(false);
            this._todayStatCard.PerformLayout();
            this._failedStatCard.ResumeLayout(false);
            this._failedStatCard.PerformLayout();
            this._usersStatCard.ResumeLayout(false);
            this._usersStatCard.PerformLayout();
            this._filterPanel.ResumeLayout(false);
            this._filterPanel.PerformLayout();
            this._gridPanel.ResumeLayout(false);
            this._gridPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._auditGrid)).EndInit();
            this._pagingPanel.ResumeLayout(false);
            this._busyOverlay.ResumeLayout(false);
            this._busyOverlay.PerformLayout();
            this.ResumeLayout(false);

        }

        private DataGridViewTextBoxColumn Time;
        private DataGridViewTextBoxColumn User;
        private DataGridViewTextBoxColumn Action;
        private DataGridViewTextBoxColumn Table;
        private DataGridViewTextBoxColumn Description;
        private DataGridViewButtonColumn ViewDetail;
    }
}
