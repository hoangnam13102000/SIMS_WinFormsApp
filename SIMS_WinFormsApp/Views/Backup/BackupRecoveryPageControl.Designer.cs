using System;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Models.Enums;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.I18n;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Pagination;

namespace SIMS_WinFormsApp.Views.Backup
{
    internal sealed partial class BackupRecoveryPageControl
    {
        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.Dock = DockStyle.Fill;
            this.BackColor = AppColors.PageBg;
            this.Padding = new Padding(16);

            _rootLayout = new TableLayoutPanel();
            _headerPanel = new Panel();
            _cardPanel = new Panel();
            _listLayout = new TableLayoutPanel();
            _filterHost = new Panel();
            _searchFrame = new Panel();
            _dateRangePanel = new Panel();
            _searchIcon = new Label();
            _searchTextBox = new TextBox();
            _todayButton = new Button();
            _clearRangeButton = new Button();
            _fromDateCheckBox = new CheckBox();
            _fromDatePicker = new DateTimePicker();
            _toDateCheckBox = new CheckBox();
            _toDatePicker = new DateTimePicker();
            _restoreFileButton = new PrimaryButton();
            _backupNowButton = new PrimaryButton();
            _headerIconCircle = new Panel();
            _headerIcon = new IconPictureBox();
            _headerTitle = new Label();
            _headerSubtitle = new Label();
            _headerActionsPanel = new FlowLayoutPanel();
            _grid = new DataGridView();
            _paginationControl = new PaginationControl();

            _rootLayout.Dock = DockStyle.Fill;
            _rootLayout.BackColor = Color.Transparent;
            _rootLayout.ColumnCount = 1;
            _rootLayout.RowCount = 2;
            _rootLayout.Margin = Padding.Empty;
            _rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 112F));
            _rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            _headerPanel.Dock = DockStyle.Fill;
            _headerPanel.Margin = new Padding(0, 0, 0, 12);
            _headerPanel.BackColor = AppColors.White;
            _headerPanel.Padding = new Padding(20, 14, 20, 14);

            _headerIconCircle.Size = new Size(48, 48);
            _headerIconCircle.Location = new Point(0, 0);
            _headerIconCircle.BackColor = Color.Transparent;
            _headerIconCircle.Paint += new PaintEventHandler(this._headerIconCircle_Paint);

            _headerIcon.IconChar = IconChar.ShieldHalved;
            _headerIcon.IconColor = AppColors.Accent;
            _headerIcon.IconSize = 24;
            _headerIcon.Size = new Size(28, 28);
            _headerIcon.SizeMode = PictureBoxSizeMode.CenterImage;
            _headerIcon.BackColor = Color.Transparent;
            _headerIcon.Location = new Point(10, 10);

            _headerTitle.AutoSize = true;
            _headerTitle.Text = "Sao lưu & Khôi phục";
            _headerTitle.Font = AppFonts.HeadingMd;
            _headerTitle.ForeColor = AppColors.TextTitle;
            _headerTitle.BackColor = Color.Transparent;
            _headerTitle.Location = new Point(84, 13);

            _headerSubtitle.AutoSize = true;
            _headerSubtitle.Text = "Bảo vệ dữ liệu khi hệ thống gặp sự cố hoặc bị tấn công";
            _headerSubtitle.Font = AppFonts.Small;
            _headerSubtitle.ForeColor = AppColors.TextMuted;
            _headerSubtitle.BackColor = Color.Transparent;
            _headerSubtitle.Location = new Point(84, 47);

            _headerActionsPanel.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            _headerActionsPanel.FlowDirection = FlowDirection.LeftToRight;
            _headerActionsPanel.WrapContents = false;
            _headerActionsPanel.Size = new Size(360, 80);
            _headerActionsPanel.BackColor = Color.Transparent;
            _headerActionsPanel.Margin = new Padding(0);
            _headerActionsPanel.Padding = new Padding(0, 19, 0, 0);
            _headerActionsPanel.Location = new Point(0, 0);
            _headerActionsPanel.Resize += new EventHandler(this._headerActionsPanel_Resize);

            _restoreFileButton.Text = string.Empty;
            _restoreFileButton.IsPrimary = false;
            _restoreFileButton.CustomAccentColor = AppColors.Warning;
            _restoreFileButton.CornerRadius = AppRadius.Medium;
            _restoreFileButton.Size = new Size(192, 42);
            _restoreFileButton.Margin = new Padding(0, 0, 12, 0);
            _restoreFileButton.BackColor = Color.Transparent;
            _restoreFileButton.ForeColor = Color.White;
            _restoreFileButton.Padding = new Padding(0);
            _restoreFileButton.Click += new EventHandler(this._restoreFileButton_Click);

            var restoreIcon = new IconPictureBox
            {
                IconChar = IconChar.Upload,
                IconColor = AppColors.TextPrimary,
                IconSize = 15,
                Size = new Size(18, 18),
                Location = new Point(16, 12),
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand
            };
            restoreIcon.Click += new EventHandler(this._restoreFileButton_Click);

            var restoreLabel = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                Font = AppFonts.Button,
                ForeColor = AppColors.TextPrimary,
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Transparent,
                Padding = new Padding(42, 0, 12, 0),
                Text = "Khôi phục từ file..."
            };
            restoreLabel.Click += new EventHandler(this._restoreFileButton_Click);
            _restoreFileButton.Controls.Add(restoreIcon);
            _restoreFileButton.Controls.Add(restoreLabel);

            _backupNowButton.Text = string.Empty;
            _backupNowButton.IsPrimary = true;
            _backupNowButton.CustomAccentColor = AppColors.Accent;
            _backupNowButton.CornerRadius = AppRadius.Medium;
            _backupNowButton.Size = new Size(150, 42);
            _backupNowButton.Margin = new Padding(0);
            _backupNowButton.BackColor = Color.Transparent;
            _backupNowButton.ForeColor = Color.White;
            _backupNowButton.Padding = new Padding(0);
            _backupNowButton.Click += new EventHandler(this._backupNowButton_Click);

            var backupIcon = new IconPictureBox
            {
                IconChar = IconChar.Download,
                IconColor = Color.White,
                IconSize = 15,
                Size = new Size(18, 18),
                Location = new Point(16, 12),
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand
            };
            backupIcon.Click += new EventHandler(this._backupNowButton_Click);

            var backupLabel = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                Font = AppFonts.Button,
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Transparent,
                Padding = new Padding(42, 0, 12, 0),
                Text = "Sao lưu ngay"
            };
            backupLabel.Click += new EventHandler(this._backupNowButton_Click);
            _backupNowButton.Controls.Add(backupIcon);
            _backupNowButton.Controls.Add(backupLabel);

            _headerIconCircle.Controls.Add(_headerIcon);
            _headerPanel.Controls.Add(_headerIconCircle);
            _headerPanel.Controls.Add(_headerTitle);
            _headerPanel.Controls.Add(_headerSubtitle);
            _headerActionsPanel.Controls.Add(_restoreFileButton);
            _headerActionsPanel.Controls.Add(_backupNowButton);
            _headerPanel.Controls.Add(_headerActionsPanel);

            _cardPanel.Margin = new Padding(0, 16, 0, 0);
            _cardPanel.BackColor = AppColors.White;
            _cardPanel.Padding = new Padding(12, 10, 12, 8);

            _listLayout.Dock = DockStyle.Fill;
            _listLayout.ColumnCount = 1;
            _listLayout.RowCount = 3;
            _listLayout.BackColor = Color.Transparent;
            _listLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _listLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 148F));
            _listLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            _listLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));

            _filterHost.Dock = DockStyle.Fill;
            _filterHost.BackColor = Color.Transparent;
            _filterHost.Padding = new Padding(4, 2, 4, 0);
            _filterHost.Resize += new EventHandler(this._filterHost_Resize);

            _searchFrame.BackColor = AppColors.White;
            _searchFrame.BorderStyle = BorderStyle.FixedSingle;
            _searchFrame.Size = new Size(220, 38);
            _searchFrame.Location = new Point(0, 8);
            _searchFrame.Resize += new EventHandler(this._searchFrame_Resize);

            _searchIcon.Text = "⌕";
            _searchIcon.Font = new Font("Segoe UI Symbol", 16F);
            _searchIcon.ForeColor = AppColors.IconMuted;
            _searchIcon.TextAlign = ContentAlignment.MiddleCenter;
            _searchIcon.BackColor = Color.Transparent;
            _searchIcon.Size = new Size(32, 38);
            _searchIcon.Location = new Point(0, 0);

            _searchTextBox.BorderStyle = BorderStyle.None;
            _searchTextBox.Font = AppFonts.Input;
            _searchTextBox.ForeColor = AppColors.TextPrimary;
            _searchTextBox.BackColor = AppColors.White;
            _searchTextBox.Location = new Point(38, 8);
            _searchTextBox.Height = 24;
            _searchTextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            _searchTextBox.TextChanged += new EventHandler(this._searchTextBox_TextChanged);
            NativeTextBoxBehavior.SetCueBanner(_searchTextBox, "Tìm theo tên file backup...");

            _dateRangePanel.BackColor = Color.Transparent;
            _dateRangePanel.Size = new Size(540, 42);
            _dateRangePanel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _dateRangePanel.Location = new Point(0, 0);

            _todayButton.Text = Lang.Get("audit.date.today");
            _todayButton.Tag = DateRangePreset.Today;
            _todayButton.AutoSize = false;
            _todayButton.Height = 30;
            _todayButton.Font = AppFonts.Small;
            _todayButton.FlatStyle = FlatStyle.Flat;
            _todayButton.BackColor = AppColors.White;
            _todayButton.ForeColor = AppColors.TextPrimary;
            _todayButton.Margin = Padding.Empty;
            _todayButton.Padding = new Padding(8, 0, 8, 0);
            _todayButton.FlatAppearance.BorderColor = AppColors.FieldBorder;
            _todayButton.Click += new EventHandler(this._todayButton_Click);

            _clearRangeButton.Text = Lang.Get("audit.date.clear");
            _clearRangeButton.AutoSize = false;
            _clearRangeButton.Height = 30;
            _clearRangeButton.Font = AppFonts.Small;
            _clearRangeButton.FlatStyle = FlatStyle.Flat;
            _clearRangeButton.BackColor = AppColors.White;
            _clearRangeButton.ForeColor = AppColors.TextMuted;
            _clearRangeButton.FlatAppearance.BorderColor = AppColors.FieldBorder;
            _clearRangeButton.Click += new EventHandler(this._clearRangeButton_Click);

            _fromDateCheckBox.Text = Lang.Get("audit.date.from");
            _fromDateCheckBox.AutoSize = true;
            _fromDateCheckBox.Font = AppFonts.Small;
            _fromDateCheckBox.ForeColor = AppColors.TextMuted;
            _fromDateCheckBox.BackColor = Color.Transparent;
            _fromDateCheckBox.Height = 30;
            _fromDateCheckBox.CheckedChanged += new EventHandler(this._fromDateCheckBox_CheckedChanged);

            _fromDatePicker.Format = DateTimePickerFormat.Custom;
            _fromDatePicker.CustomFormat = "dd/MM/yyyy";
            _fromDatePicker.Font = AppFonts.Small;
            _fromDatePicker.Width = 150;
            _fromDatePicker.Height = 30;
            _fromDatePicker.ValueChanged += new EventHandler(this._fromDatePicker_ValueChanged);

            _toDateCheckBox.Text = Lang.Get("audit.date.to");
            _toDateCheckBox.AutoSize = true;
            _toDateCheckBox.Font = AppFonts.Small;
            _toDateCheckBox.ForeColor = AppColors.TextMuted;
            _toDateCheckBox.BackColor = Color.Transparent;
            _toDateCheckBox.Height = 30;
            _toDateCheckBox.CheckedChanged += new EventHandler(this._toDateCheckBox_CheckedChanged);

            _toDatePicker.Format = DateTimePickerFormat.Custom;
            _toDatePicker.CustomFormat = "dd/MM/yyyy";
            _toDatePicker.Font = AppFonts.Small;
            _toDatePicker.Width = 150;
            _toDatePicker.Height = 30;
            _toDatePicker.ValueChanged += new EventHandler(this._toDatePicker_ValueChanged);

            _searchFrame.Controls.Add(_searchIcon);
            _searchFrame.Controls.Add(_searchTextBox);
            _dateRangePanel.Controls.AddRange(new Control[]
            {
                _todayButton,
                _clearRangeButton,
                _fromDateCheckBox,
                _fromDatePicker,
                _toDateCheckBox,
                _toDatePicker
            });

            _filterHost.Controls.Add(_searchFrame);
            _filterHost.Controls.Add(_dateRangePanel);

            _grid = new DataGridView();
            _grid.Dock = DockStyle.Fill;
            _grid.BackgroundColor = AppColors.White;
            _grid.BorderStyle = BorderStyle.None;
            _grid.AllowUserToAddRows = false;
            _grid.AllowUserToDeleteRows = false;
            _grid.AllowUserToResizeRows = false;
            _grid.RowHeadersVisible = false;
            _grid.AutoGenerateColumns = false;
            _grid.ReadOnly = true;
            _grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _grid.MultiSelect = false;
            _grid.CellContentClick += new DataGridViewCellEventHandler(this.OnGridCellContentClick);
            _grid.CellPainting += new DataGridViewCellPaintingEventHandler(this.PaintActionCell);
            _grid.CellMouseMove += new DataGridViewCellMouseEventHandler(this.OnActionMouseMove);
            _grid.CellMouseLeave += new DataGridViewCellEventHandler(this.OnActionMouseLeave);
            _grid.CellMouseClick += new DataGridViewCellMouseEventHandler(this.OnActionMouseClick);
            _grid.CellToolTipTextNeeded += new DataGridViewCellToolTipTextNeededEventHandler(this.OnCellToolTip);
            _grid.ColumnHeadersDefaultCellStyle.BackColor = AppColors.TableHeaderBg;
            _grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            _grid.ColumnHeadersDefaultCellStyle.Font = AppFonts.BodyBold;
            _grid.ColumnHeadersDefaultCellStyle.Padding = new Padding(12, 0, 12, 0);
            _grid.DefaultCellStyle.Font = AppFonts.Body;
            _grid.DefaultCellStyle.ForeColor = AppColors.TableRowText;
            _grid.DefaultCellStyle.Padding = new Padding(12, 0, 12, 0);
            _grid.DefaultCellStyle.SelectionBackColor = AppColors.AccentSelectionBg;
            _grid.DefaultCellStyle.SelectionForeColor = AppColors.TextPrimary;
            _grid.DefaultCellStyle.BackColor = AppColors.White;
            _grid.AlternatingRowsDefaultCellStyle.BackColor = AppColors.TableRowOdd;

            _grid.Columns.Add(TextColumn("FileName", "Tên file", false, 140));
            _grid.Columns.Add(TextColumn("Strategy", "Chiến lược", true, 110));
            _grid.Columns.Add(TextColumn("CreatedAt", "Thời gian", true, 140));
            _grid.Columns.Add(TextColumn("Size", "Dung lượng", true, 100));
            _grid.Columns.Add(CreateLinkColumn("Local", "Local", 100));

            var cloudColumn = new DataGridViewLinkColumn();
            cloudColumn.Name = "Cloud";
            cloudColumn.HeaderText = "Cloud";
            cloudColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            cloudColumn.MinimumWidth = 110;
            cloudColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            cloudColumn.LinkColor = AppColors.Accent;
            cloudColumn.ActiveLinkColor = AppColors.Accent;
            cloudColumn.VisitedLinkColor = AppColors.Accent;
            cloudColumn.TrackVisitedState = false;
            cloudColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            cloudColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            _grid.Columns.Add(cloudColumn);

            var actionColumn = new DataGridViewTextBoxColumn();
            actionColumn.Name = "Actions";
            actionColumn.HeaderText = string.Empty;
            actionColumn.Width = 88;
            actionColumn.MinimumWidth = 88;
            actionColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            actionColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            actionColumn.ReadOnly = true;
            actionColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            _grid.Columns.Add(actionColumn);

            var gridHolder = new Panel();
            gridHolder.Dock = DockStyle.Fill;
            gridHolder.Margin = new Padding(0, 10, 0, 10);
            gridHolder.Controls.Add(_grid);

            _paginationControl.Dock = DockStyle.Fill;

            _listLayout.Controls.Add(_filterHost, 0, 0);
            _listLayout.Controls.Add(gridHolder, 0, 1);
            _listLayout.Controls.Add(_paginationControl, 0, 2);

            _cardPanel.Controls.Add(_listLayout);
            _rootLayout.Controls.Add(_headerPanel, 0, 0);
            _rootLayout.Controls.Add(_cardPanel, 0, 1);
            this.Controls.Add(_rootLayout);

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
