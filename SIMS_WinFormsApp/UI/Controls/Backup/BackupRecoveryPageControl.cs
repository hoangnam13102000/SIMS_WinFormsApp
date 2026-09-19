using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Models.DTOs.Backup;
using SIMS_WinFormsApp.Services.Backup;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Controls.Pagination;
using SIMS_WinFormsApp.UI.Controls.Search;
using SIMS_WinFormsApp.UI.Controls.Toast;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.UI.Controls.Backup
{
    internal sealed class BackupRecoveryPageControl : UserControl, IBackupRecoveryView
    {
        private PrimaryButton _restoreFileButton;
        private PrimaryButton _backupNowButton;

        private SearchBarControl _searchBar;
        private SearchPresenter _searchPresenter;
        private DateTimePicker _fromDate;
        private DateTimePicker _toDate;

        private DataGridView _grid;
        private List<BackupRowDto> _currentPageRows = new List<BackupRowDto>();

        private PaginationControl _paginationControl;
        private PaginationPresenter _paginationPresenter;

        #region IBackupRecoveryView - events
        public event EventHandler ViewReady;
        public event EventHandler<BackupQuery> QueryChanged;
        public event EventHandler BackupNowRequested;
        public event EventHandler RestoreFromFileRequested;
        public event EventHandler<BackupRowDto> RestoreRowRequested;
        public event EventHandler<BackupRowDto> DeleteRowRequested;
        #endregion

        public BackupRecoveryPageControl()
        {
            Dock = DockStyle.Fill;
            BackColor = AppColors.PageBg;
            Padding = new Padding(16);

            var root = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2, BackColor = Color.Transparent };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 112));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            Controls.Add(root);

            root.Controls.Add(BuildBanner(), 0, 0);
            root.Controls.Add(BuildListCard(), 0, 1);

            _searchPresenter = new SearchPresenter(_searchBar, null, 300);
            _searchPresenter.SearchCommitted += (s, text) => RaiseQueryChanged();

            _paginationPresenter = new PaginationPresenter(_paginationControl, 10);
            _paginationPresenter.PageChanged += (s, e) => RaiseQueryChanged();

            Load += (s, e) => ViewReady?.Invoke(this, EventArgs.Empty);
        }

        #region Banner tiêu đề + nút hành động
        private Panel BuildBanner()
        {
            var header = new HeaderSection
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 0, 12)
            };
            header.Set(
                "Sao lưu & Khôi phục",
                "Bảo vệ dữ liệu khi hệ thống gặp sự cố hoặc bị tấn công",
                IconChar.ShieldHalved,
                AppColors.Accent,
                AppColors.InfoBg);

            var actions = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                BackColor = Color.Transparent,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };

            _restoreFileButton = CreateActionButton(
                "Khôi phục từ file...",
                IconChar.Upload,
                isPrimary: false,
                accent: AppColors.Warning,
                size: new Size(192, 42),
                onClick: (s, e) => RestoreFromFileRequested?.Invoke(this, EventArgs.Empty));

            _backupNowButton = CreateActionButton(
                "Sao lưu ngay",
                IconChar.Download,
                isPrimary: true,
                accent: AppColors.Accent,
                size: new Size(150, 42),
                onClick: (s, e) => BackupNowRequested?.Invoke(this, EventArgs.Empty));

            actions.Controls.Add(_restoreFileButton);
            actions.Controls.Add(_backupNowButton);
            header.SetActions(actions);

            return new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = AppColors.White,
                Margin = new Padding(0),
                Padding = new Padding(0),
                Controls = { header }
            };
        }
        #endregion

        private static PrimaryButton CreateActionButton(string text, IconChar icon, bool isPrimary, Color accent, Size size, EventHandler onClick)
        {
            var button = new PrimaryButton
            {
                Text = string.Empty,
                IsPrimary = isPrimary,
                CustomAccentColor = accent,
                CornerRadius = AppRadius.Medium,
                Size = size,
                Margin = new Padding(0, 0, isPrimary ? 0 : 12, 0),
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                Padding = new Padding(0)
            };

            var iconBox = new IconPictureBox
            {
                IconChar = icon,
                IconColor = isPrimary ? Color.White : AppColors.TextPrimary,
                IconSize = 15,
                Size = new Size(18, 18),
                Location = new Point(16, (size.Height - 18) / 2),
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand
            };

            var label = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                Font = AppFonts.Button,
                ForeColor = isPrimary ? Color.White : AppColors.TextPrimary,
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Transparent,
                Padding = new Padding(42, 0, 12, 0),
                Text = text
            };

            button.Controls.Add(iconBox);
            button.Controls.Add(label);
            button.Click += onClick;
            iconBox.Click += (s, e) => onClick?.Invoke(button, EventArgs.Empty);
            label.Click += (s, e) => onClick?.Invoke(button, EventArgs.Empty);
            return button;
        }

        #region Thẻ "Các bản sao lưu hiện có"
        private Panel BuildListCard()
        {
            var card = CreateCardPanel();
            card.Margin = new Padding(0, 16, 0, 0);

            var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 4, BackColor = Color.Transparent };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 46));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            card.Controls.Add(layout);

            var titleLabel = new Label
            {
                Text = "Các bản sao lưu hiện có",
                Font = AppFonts.Subtitle,
                ForeColor = AppColors.TextTitle,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.BottomLeft,
                BackColor = Color.Transparent
            };
            layout.Controls.Add(titleLabel, 0, 0);

            layout.Controls.Add(BuildFilterRow(), 0, 1);

            _grid = CreateGrid();
            _grid.CellContentClick += OnGridCellContentClick;
            var gridHolder = new Panel { Dock = DockStyle.Fill, Margin = new Padding(0, 10, 0, 10) };
            gridHolder.Controls.Add(_grid);
            layout.Controls.Add(gridHolder, 0, 2);

            _paginationControl = new PaginationControl { Dock = DockStyle.Fill };
            layout.Controls.Add(_paginationControl, 0, 3);

            return card;
        }

        private Panel BuildFilterRow()
        {
            var panel = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent };

            _searchBar = new SearchBarControl { PlaceholderText = "Tìm theo tên file backup..." };

            var fromLabel = new Label
            {
                Text = "Từ ngày",
                Font = AppFonts.Body,
                ForeColor = AppColors.TextSecondary,
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Transparent
            };
            _fromDate = CreateDateFilterPicker();
            _fromDate.ValueChanged += (s, e) => RaiseQueryChanged();

            var toLabel = new Label
            {
                Text = "đến",
                Font = AppFonts.Body,
                ForeColor = AppColors.TextSecondary,
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Transparent
            };
            _toDate = CreateDateFilterPicker();
            _toDate.ValueChanged += (s, e) => RaiseQueryChanged();

            panel.Controls.Add(_searchBar);
            panel.Controls.Add(fromLabel);
            panel.Controls.Add(_fromDate);
            panel.Controls.Add(toLabel);
            panel.Controls.Add(_toDate);

            void Reflow()
            {
                const int pickerWidth = 130, gap = 8, h = 40;
                int y = (panel.Height - h) / 2;

                _toDate.Size = new Size(pickerWidth, h);
                _toDate.Location = new Point(panel.Width - pickerWidth, y);

                toLabel.Location = new Point(_toDate.Left - gap - toLabel.Width, y + (h - toLabel.Height) / 2);

                _fromDate.Size = new Size(pickerWidth, h);
                _fromDate.Location = new Point(toLabel.Left - gap - pickerWidth, y);

                fromLabel.Location = new Point(_fromDate.Left - gap - fromLabel.Width, y + (h - fromLabel.Height) / 2);

                _searchBar.Location = new Point(0, y);
                _searchBar.Size = new Size(Math.Max(80, fromLabel.Left - gap), h);
            }
            panel.Resize += (s, e) => Reflow();
            Reflow();
            return panel;
        }

        private static DateTimePicker CreateDateFilterPicker()
        {
            return new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                ShowCheckBox = true,
                Checked = false,
                Font = AppFonts.Body
            };
        }
        #endregion

        #region Bảng danh sách backup
        private DataGridView CreateGrid()
        {
            var grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                AutoGenerateColumns = false,
                RowHeadersVisible = false,
                BackgroundColor = AppColors.White,
                GridColor = AppColors.TableGrid,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                ColumnHeadersHeight = 50,
                RowTemplate = { Height = 52 },
                EnableHeadersVisualStyles = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ReadOnly = true
            };

            grid.Columns.Add(TextColumn("FileName", "Tên file", false, 140));
            grid.Columns.Add(TextColumn("Strategy", "Chiến lược", true, 110));
            grid.Columns.Add(TextColumn("CreatedAt", "Thời gian", true, 140));
            grid.Columns.Add(TextColumn("Size", "Dung lượng", true, 100));
            grid.Columns.Add(CreateLinkColumn("Local", "Local", 100));
            var cloudColumn = new DataGridViewLinkColumn
            {
                Name = "Cloud",
                HeaderText = "Cloud",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                MinimumWidth = 110,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                LinkColor = AppColors.Accent,
                ActiveLinkColor = AppColors.Accent,
                VisitedLinkColor = AppColors.Accent,
                TrackVisitedState = false,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            };
            cloudColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grid.Columns.Add(cloudColumn);

            var restoreColumn = new DataGridViewButtonColumn
            {
                Name = "Restore",
                HeaderText = string.Empty,
                Text = "Khôi phục",
                UseColumnTextForButtonValue = true,
                FlatStyle = FlatStyle.Flat,
                Width = 110,
                MinimumWidth = 110
            };
            restoreColumn.DefaultCellStyle.BackColor = AppColors.BgLighter;
            restoreColumn.DefaultCellStyle.ForeColor = AppColors.Accent;
            restoreColumn.DefaultCellStyle.SelectionBackColor = AppColors.BgLighter;
            restoreColumn.DefaultCellStyle.SelectionForeColor = AppColors.Accent;
            grid.Columns.Add(restoreColumn);

            var deleteColumn = new DataGridViewButtonColumn
            {
                Name = "Delete",
                HeaderText = string.Empty,
                Text = "Xóa",
                UseColumnTextForButtonValue = true,
                FlatStyle = FlatStyle.Flat,
                Width = 70,
                MinimumWidth = 70
            };
            deleteColumn.DefaultCellStyle.BackColor = AppColors.BgLighter;
            deleteColumn.DefaultCellStyle.ForeColor = AppColors.Error;
            deleteColumn.DefaultCellStyle.SelectionBackColor = AppColors.BgLighter;
            deleteColumn.DefaultCellStyle.SelectionForeColor = AppColors.Error;
            grid.Columns.Add(deleteColumn);

            grid.ColumnHeadersDefaultCellStyle.BackColor = AppColors.TableHeaderBg;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = AppFonts.BodyBold;
            grid.ColumnHeadersDefaultCellStyle.Padding = new Padding(12, 0, 12, 0);
            grid.DefaultCellStyle.Font = AppFonts.Body;
            grid.DefaultCellStyle.ForeColor = AppColors.TableRowText;
            grid.DefaultCellStyle.Padding = new Padding(12, 0, 12, 0);
            grid.DefaultCellStyle.SelectionBackColor = AppColors.AccentSelectionBg;
            grid.DefaultCellStyle.SelectionForeColor = AppColors.TextPrimary;
            grid.DefaultCellStyle.BackColor = AppColors.White;
            grid.AlternatingRowsDefaultCellStyle.BackColor = AppColors.TableRowOdd;

            return grid;
        }

        private static DataGridViewTextBoxColumn TextColumn(string name, string header, bool centered, int minWidth)
        {
            return new DataGridViewTextBoxColumn
            {
                Name = name,
                HeaderText = header,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                MinimumWidth = minWidth,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                DefaultCellStyle = { Alignment = centered ? DataGridViewContentAlignment.MiddleCenter : DataGridViewContentAlignment.MiddleLeft },
                HeaderCell = { Style = { Alignment = centered ? DataGridViewContentAlignment.MiddleCenter : DataGridViewContentAlignment.MiddleLeft } }
            };
        }

        private void OnGridCellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _currentPageRows.Count) return;
            var row = _currentPageRows[e.RowIndex];
            string columnName = _grid.Columns[e.ColumnIndex].Name;

            if (columnName == "Restore") RestoreRowRequested?.Invoke(this, row);
            else if (columnName == "Delete") DeleteRowRequested?.Invoke(this, row);
            else if (columnName == "Local" && File.Exists(row.FilePath))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "explorer.exe",
                    Arguments = "/select,\"" + row.FilePath + "\"",
                    UseShellExecute = true
                });
            }
            else if (columnName == "Cloud" && !string.IsNullOrWhiteSpace(row.CloudUrl))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = row.CloudUrl,
                    UseShellExecute = true
                });
            }
        }

        private static DataGridViewLinkColumn CreateLinkColumn(string name, string header, int minWidth)
        {
            var column = new DataGridViewLinkColumn
            {
                Name = name,
                HeaderText = header,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                MinimumWidth = minWidth,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                LinkColor = AppColors.Accent,
                ActiveLinkColor = AppColors.Accent,
                VisitedLinkColor = AppColors.Accent,
                TrackVisitedState = false,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            };
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            return column;
        }
        #endregion

        #region Tiện ích dựng UI dùng chung
        private static Panel CreateCardPanel()
        {
            var card = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent, Padding = new Padding(20), Margin = new Padding(0) };
            card.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = new Rectangle(0, 0, card.Width - 1, card.Height - 1);
                using (var path = AppRadius.GetRoundedPath(rect, AppRadius.Large))
                using (var brush = new SolidBrush(AppColors.White))
                {
                    e.Graphics.FillPath(brush, path);
                }
            };
            return card;
        }

        private void RaiseQueryChanged()
        {
            QueryChanged?.Invoke(this, new BackupQuery
            {
                Keyword = _searchBar.Text,
                From = _fromDate.Checked ? _fromDate.Value.Date : (DateTime?)null,
                To = _toDate.Checked ? _toDate.Value.Date : (DateTime?)null,
                PageIndex = _paginationPresenter.PageIndex,
                PageSize = _paginationPresenter.PageSize
            });
        }
        #endregion

        #region IBackupRecoveryView - methods
        public void BindRows(IReadOnlyList<BackupRowDto> pageRows, int totalCount)
        {
            _currentPageRows = pageRows?.ToList() ?? new List<BackupRowDto>();
            _grid.Rows.Clear();

            foreach (var row in _currentPageRows)
            {
                _grid.Rows.Add(
                    row.FileName,
                    row.StrategyName,
                    row.CreatedAt.ToString("HH:mm:ss dd/MM/yyyy"),
                    BackupFormat.Size(row.SizeBytes),
                    File.Exists(row.FilePath) ? "Mở Local" : "Không có",
                    row.IsUploadedToCloud ? "Mở Cloud" : "Chưa upload");
            }

            _grid.ClearSelection();

            if (_paginationPresenter != null)
            {
                _paginationPresenter.ApplyResult(totalCount);
            }
        }

        public void SetBusy(bool isBusy, string message)
        {
            if (_backupNowButton != null) _backupNowButton.Enabled = !isBusy;
            if (_restoreFileButton != null) _restoreFileButton.Enabled = !isBusy;
            Cursor = isBusy ? Cursors.WaitCursor : Cursors.Default;
        }

        public void ShowError(string message)
        {
            AppToast.Error(this, message);
        }

        public void ShowSuccess(string message)
        {
            AppToast.Success(this, message);
        }

        public string PromptChooseBackupFile(string initialDirectory)
        {
            using (var dialog = new OpenFileDialog
            {
                Filter = "Backup files|*.bak;*.zip;*.sql|Tất cả files|*.*",
                InitialDirectory = string.IsNullOrWhiteSpace(initialDirectory) ? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) : initialDirectory,
                Title = "Chọn file backup để khôi phục"
            })
            {
                return dialog.ShowDialog(this) == DialogResult.OK ? dialog.FileName : null;
            }
        }

        public bool Confirm(string title, string message)
        {
            return MessageBox.Show(this, message, title,
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes;
        }

        #endregion
    }
}