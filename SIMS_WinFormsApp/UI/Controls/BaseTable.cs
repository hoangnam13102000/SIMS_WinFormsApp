using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{
    public sealed class TablePageResult
    {
        public int TotalCount { get; set; }
        public IList<object[]> Rows { get; set; } = new List<object[]>();
    }

    public class BaseTable : UserControl
    {
        private readonly Func<int, int, string, TablePageResult> _loadPage;
        private Label _totalLabel;
        private Label _pageLabel;
        private TextBox _searchBox;
        private ComboBox _filterBox;
        private DataGridView _grid;
        private Button _previousButton;
        private Button _nextButton;
        private readonly int _pageSize;
        private int _pageIndex;
        private int _totalCount;

        public DataGridView Grid => _grid;

        public BaseTable(string title, string subtitle, FontAwesome.Sharp.IconChar icon,
            string[] columns, Func<int, int, string, TablePageResult> loadPage, int pageSize = 10)
        {
            _loadPage = loadPage ?? throw new ArgumentNullException(nameof(loadPage));
            _pageSize = Math.Max(1, pageSize);
            AutoScaleMode = AutoScaleMode.None;
            Dock = DockStyle.Fill;
            BackColor = AppColors.PageBg;
            Padding = new Padding(10);

            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                BackColor = AppColors.PageBg,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 120));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 72));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 64));

            var header = new HeaderSection
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 0, 12),
                Title = title,
                Subtitle = subtitle,
                Icon = icon
            };
            root.Controls.Add(header, 0, 0);
            root.Controls.Add(CreateFilterBar(), 0, 1);
            _grid = CreateGrid(columns);
            root.Controls.Add(_grid, 0, 2);
            root.Controls.Add(CreatePaginationBar(), 0, 3);
            Controls.Add(root);
            LoadCurrentPage();
        }

        private Control CreateFilterBar()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = AppColors.White,
                Padding = new Padding(18, 12, 18, 10),
                Margin = Padding.Empty
            };
            _searchBox = new TextBox
            {
                Font = AppFonts.Input,
                ForeColor = AppColors.TextPrimary,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(0, 0),
                Size = new Size(390, 42)
            };
            _searchBox.TextChanged += (_, __) =>
            {
                _pageIndex = 0;
                LoadCurrentPage();
            };
            _filterBox = new ComboBox
            {
                Font = AppFonts.Input,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(410, 0),
                Size = new Size(220, 42)
            };
            _filterBox.Items.Add("Tất cả trạng thái");
            _filterBox.SelectedIndex = 0;
            panel.Controls.Add(_searchBox);
            panel.Controls.Add(_filterBox);
            return panel;
        }

        private Control CreatePaginationBar()
        {
            var panel = new Panel { Dock = DockStyle.Fill, BackColor = AppColors.White, Padding = new Padding(18, 10, 18, 10) };
            _previousButton = CreatePageButton("<");
            _previousButton.Location = new Point(0, 0);
            _previousButton.Click += (_, __) =>
            {
                if (_pageIndex <= 0) return;
                _pageIndex--;
                LoadCurrentPage();
            };
            _pageLabel = new Label
            {
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = AppFonts.Body,
                ForeColor = AppColors.TextPrimary,
                Location = new Point(44, 0),
                Size = new Size(140, 42)
            };
            _nextButton = CreatePageButton(">");
            _nextButton.Location = new Point(190, 0);
            _nextButton.Click += (_, __) =>
            {
                if (_pageIndex + 1 >= PageCount) return;
                _pageIndex++;
                LoadCurrentPage();
            };
            _totalLabel = new Label
            {
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleRight,
                Font = AppFonts.Body,
                ForeColor = AppColors.TextMuted,
                Dock = DockStyle.Right,
                Width = 240
            };
            panel.Controls.Add(_previousButton);
            panel.Controls.Add(_pageLabel);
            panel.Controls.Add(_nextButton);
            panel.Controls.Add(_totalLabel);
            return panel;
        }

        private static Button CreatePageButton(string text)
        {
            return new Button
            {
                Text = text,
                FlatStyle = FlatStyle.Flat,
                Font = AppFonts.BodyBold,
                BackColor = AppColors.White,
                ForeColor = AppColors.TextPrimary,
                Size = new Size(40, 42),
                FlatAppearance = { BorderColor = AppColors.Border }
            };
        }

        private int PageCount => Math.Max(1, (int)Math.Ceiling(_totalCount / (double)_pageSize));

        private void LoadCurrentPage()
        {
            var result = _loadPage(_pageIndex, _pageSize, _searchBox == null ? string.Empty : _searchBox.Text);
            _totalCount = Math.Max(0, result.TotalCount);
            _grid.Rows.Clear();
            foreach (var row in result.Rows)
                _grid.Rows.Add(row);
            _pageIndex = Math.Min(_pageIndex, PageCount - 1);
            _pageLabel.Text = "Trang " + (_pageIndex + 1) + " / " + PageCount;
            _totalLabel.Text = "Tổng cộng: " + _totalCount + " bản ghi";
            _previousButton.Enabled = _pageIndex > 0;
            _nextButton.Enabled = _pageIndex + 1 < PageCount;
        }

        private static DataGridView CreateGrid(string[] columns)
        {
            var grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                AutoGenerateColumns = false,
                BackgroundColor = AppColors.White,
                BorderStyle = BorderStyle.FixedSingle,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                ColumnHeadersHeight = 58,
                RowTemplate = { Height = 58 },
                EnableHeadersVisualStyles = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ReadOnly = true
            };
            foreach (var column in columns)
                grid.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = column,
                    Name = column,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    MinimumWidth = 100,
                    SortMode = DataGridViewColumnSortMode.Automatic
                });
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 43, 64);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = AppFonts.BodyBold;
            grid.DefaultCellStyle.Font = AppFonts.Body;
            grid.DefaultCellStyle.ForeColor = Color.FromArgb(71, 85, 105);
            grid.DefaultCellStyle.SelectionBackColor = AppColors.AccentSelectionBg;
            grid.DefaultCellStyle.SelectionForeColor = AppColors.TextPrimary;
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            return grid;
        }
    }
}
