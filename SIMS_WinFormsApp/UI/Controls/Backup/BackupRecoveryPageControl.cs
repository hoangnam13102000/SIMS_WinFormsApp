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
using SIMS_WinFormsApp.UI.Controls.Filter;
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
        private DateRangeFilterControl _dateFilter;
        private DateRangeFilterPresenter _datePresenter;
        private TableLayoutPanel _listLayout;
        private Panel _filterHost;
        private bool _reflowingFilter;

        private DataGridView _grid;
        private List<BackupRowDto> _currentPageRows = new List<BackupRowDto>();
        private int _hoverRow = -1;
        private int _hoverAction = -1;

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

            _datePresenter = new DateRangeFilterPresenter(_dateFilter);
            _datePresenter.RangeChanged += (s, e) =>
            {
                if (_paginationPresenter.PageIndex != 0)
                    _paginationPresenter.ResetToFirstPage();
                else
                    RaiseQueryChanged();
            };

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

            var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3, BackColor = Color.Transparent };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 148));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            _listLayout = layout;
            card.Controls.Add(layout);

            layout.Controls.Add(BuildFilterRow(), 0, 0);

            _grid = CreateGrid();
            _grid.CellContentClick += OnGridCellContentClick;
            var gridHolder = new Panel { Dock = DockStyle.Fill, Margin = new Padding(0, 10, 0, 10) };
            gridHolder.Controls.Add(_grid);
            layout.Controls.Add(gridHolder, 0, 1);

            _paginationControl = new PaginationControl { Dock = DockStyle.Fill };
            layout.Controls.Add(_paginationControl, 0, 2);

            return card;
        }

        private Panel BuildFilterRow()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Padding = new Padding(4, 2, 4, 0)
            };
            _filterHost = panel;

            _searchBar = new SearchBarControl { PlaceholderText = "Tìm theo tên file backup..." };
            _dateFilter = new DateRangeFilterControl { ShowCaption = false };

            panel.Controls.Add(_searchBar);
            panel.Controls.Add(_dateFilter);
            panel.Resize += (s, e) => ReflowFilter();
            return panel;
        }

        private void ReflowFilter()
        {
            if (_reflowingFilter || _filterHost == null || _dateFilter == null || _searchBar == null) return;
            _reflowingFilter = true;
            try
            {
                int widthBefore = _filterHost.ClientSize.Width;
                int innerW = Math.Max(200, _filterHost.ClientSize.Width - _filterHost.Padding.Horizontal);
                int x = _filterHost.Padding.Left;
                int y = _filterHost.Padding.Top;

                _searchBar.SetBounds(x, y, innerW, 40);
                y += 48;
                int dateH = _dateFilter.Arrange(innerW);
                _dateFilter.SetBounds(x, y, innerW, dateH);

                int needed = y + dateH + 6;
                if (_listLayout != null && Math.Abs(_listLayout.RowStyles[0].Height - needed) > 1)
                    _listLayout.RowStyles[0].Height = needed;

                if (_filterHost.ClientSize.Width != widthBefore)
                {
                    _reflowingFilter = false;
                    ReflowFilter();
                }
            }
            finally
            {
                _reflowingFilter = false;
            }
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

            var actionColumn = new DataGridViewTextBoxColumn
            {
                Name = "Actions",
                HeaderText = string.Empty,
                Width = 88,
                MinimumWidth = 88,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                ReadOnly = true
            };
            actionColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grid.Columns.Add(actionColumn);
            grid.CellPainting += PaintActionCell;
            grid.CellMouseMove += OnActionMouseMove;
            grid.CellMouseLeave += OnActionMouseLeave;
            grid.CellMouseClick += OnActionMouseClick;
            grid.CellToolTipTextNeeded += OnCellToolTip;

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

            if (columnName == "Local" && File.Exists(row.FilePath))
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

        private void PaintActionCell(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || _grid.Columns[e.ColumnIndex].Name != "Actions") return;

            e.PaintBackground(e.CellBounds, true);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            bool rowHot = _hoverRow == e.RowIndex;
            DrawActionIcon(e.Graphics, ActionRect(e.CellBounds, 0), IconChar.RotateLeft,
                AppColors.Accent, AppColors.AccentBgSoft, rowHot && _hoverAction == 0);
            DrawActionIcon(e.Graphics, ActionRect(e.CellBounds, 1), IconChar.TrashCan,
                AppColors.Error, AppColors.ErrorBg, rowHot && _hoverAction == 1);

            using (var pen = new Pen(AppColors.TableGrid))
                e.Graphics.DrawLine(pen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right, e.CellBounds.Bottom - 1);
            e.Handled = true;
        }

        private static void DrawActionIcon(Graphics g, Rectangle button, IconChar icon, Color color, Color hotBackground, bool hovered)
        {
            using (var brush = new SolidBrush(hovered ? hotBackground : AppColors.BgLighter))
                g.FillEllipse(brush, button);

            var iconRect = new Rectangle(button.X + 7, button.Y + 7, 18, 18);
            var bitmap = IconBitmapCache.Get(icon, color, iconRect.Size);
            if (bitmap != null) g.DrawImageUnscaled(bitmap, iconRect.Location);
        }

        private static Rectangle ActionRect(Rectangle cell, int index)
        {
            const int size = 32;
            const int gap = 8;
            int total = size * 2 + gap;
            int x = cell.X + Math.Max(4, (cell.Width - total) / 2);
            int y = cell.Y + (cell.Height - size) / 2;
            return new Rectangle(x + index * (size + gap), y, size, size);
        }

        private void OnActionMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || _grid.Columns[e.ColumnIndex].Name != "Actions")
            {
                ClearActionHover();
                return;
            }

            var cell = _grid.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            int action = -1;
            if (ActionRect(new Rectangle(Point.Empty, cell.Size), 0).Contains(e.Location)) action = 0;
            else if (ActionRect(new Rectangle(Point.Empty, cell.Size), 1).Contains(e.Location)) action = 1;

            if (_hoverRow == e.RowIndex && _hoverAction == action) return;
            int previous = _hoverRow;
            _hoverRow = e.RowIndex;
            _hoverAction = action;
            InvalidateAction(previous);
            InvalidateAction(e.RowIndex);
            _grid.Cursor = action >= 0 ? Cursors.Hand : Cursors.Default;
        }

        private void OnActionMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && _grid.Columns[e.ColumnIndex].Name == "Actions")
                ClearActionHover();
        }

        private void OnActionMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _currentPageRows.Count) return;
            if (e.ColumnIndex < 0 || _grid.Columns[e.ColumnIndex].Name != "Actions") return;

            var cell = _grid.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            var local = new Rectangle(Point.Empty, cell.Size);
            var row = _currentPageRows[e.RowIndex];
            if (ActionRect(local, 0).Contains(e.Location))
                RestoreRowRequested?.Invoke(this, row);
            else if (ActionRect(local, 1).Contains(e.Location))
                DeleteRowRequested?.Invoke(this, row);
        }

        private void OnCellToolTip(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.RowIndex >= _currentPageRows.Count) return;
            string name = _grid.Columns[e.ColumnIndex].Name;
            var row = _currentPageRows[e.RowIndex];
            if (name == "Actions")
            {
                e.ToolTipText = _hoverAction == 1 ? "Xóa" : "Khôi phục";
                return;
            }
            if (name == "FileName") e.ToolTipText = row.FileName;
            else if (name == "CreatedAt") e.ToolTipText = row.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private void ClearActionHover()
        {
            if (_hoverRow < 0 && _hoverAction < 0) return;
            int previous = _hoverRow;
            _hoverRow = -1;
            _hoverAction = -1;
            InvalidateAction(previous);
            if (_grid != null) _grid.Cursor = Cursors.Default;
        }

        private void InvalidateAction(int rowIndex)
        {
            if (_grid == null || rowIndex < 0 || rowIndex >= _grid.Rows.Count) return;
            int column = _grid.Columns["Actions"] == null ? -1 : _grid.Columns["Actions"].Index;
            if (column >= 0) _grid.InvalidateCell(column, rowIndex);
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
                Keyword = _searchBar == null ? null : _searchBar.Text,
                From = _datePresenter == null ? null : _datePresenter.Current.From,
                To = _datePresenter == null ? null : _datePresenter.Current.To,
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
                    row.CreatedAt.ToString("dd/MM/yyyy HH:mm"),
                    BackupFormat.Size(row.SizeBytes),
                    File.Exists(row.FilePath) ? "Mở Local" : "Không có",
                    row.IsUploadedToCloud ? "Mở Cloud" : "Chưa upload",
                    string.Empty);
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