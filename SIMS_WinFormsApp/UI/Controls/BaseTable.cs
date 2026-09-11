using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Controls.Filter;
using SIMS_WinFormsApp.UI.Controls.Pagination;
using SIMS_WinFormsApp.UI.Controls.Search;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{
    public sealed class TablePageResult
    {
        public int TotalCount { get; set; }
        public IList<object[]> Rows { get; set; } = new List<object[]>();
    }

    /// <summary>Loại thao tác tương ứng với từng nút icon trong cột "Thao tác".</summary>
    public enum TableActionType
    {
        View = 0,
        Edit = 1,
        Lock = 2
    }

    /// <summary>Thông tin nút thao tác được bấm: dòng nào, loại thao tác nào.</summary>
    public sealed class TableActionEventArgs : EventArgs
    {
        public int RowIndex { get; }
        public TableActionType Action { get; }

        public TableActionEventArgs(int rowIndex, TableActionType action)
        {
            RowIndex = rowIndex;
            Action = action;
        }
    }

    public class BaseTable : UserControl
    {
        // Tham số thứ 4 (string) là giá trị lọc trạng thái đang chọn (null = "Tất cả trạng
        // thái" — không áp thêm điều kiện). Nơi khởi tạo BaseTable (vd: ManagementTablePage)
        // chịu trách nhiệm build câu truy vấn DB dựa trên giá trị này.
        private readonly Func<int, int, string, string, TablePageResult> _loadPage;
        private SearchBarControl _searchBar;
        private SearchPresenter _searchPresenter;
        private FilterComboBox _filterBox;
        private FilterPresenter _filterPresenter;
        private DataGridView _grid;

        // Phân trang: tách theo MVP — PaginationControl là View (thuần UI),
        // PaginationPresenter nắm state + phép toán phân trang (pageIndex/pageSize/pageCount).
        // BaseTable chỉ đóng vai trò cung cấp dữ liệu (_loadPage) khi Presenter báo cần tải lại.
        private readonly PaginationControl _pagination = new PaginationControl();
        private readonly PaginationPresenter _paginationPresenter;
        private readonly IList<FilterOption> _statusOptions;

        // ===================== Cột đặc biệt (badge / nút thao tác) =====================
        private readonly int _statusColumnIndex = -1;
        private readonly int _lockColumnIndex = -1;
        private readonly int _actionColumnIndex = -1;

        // Trạng thái hover của các nút icon trong cột "Thao tác"
        private int _hoverRowIndex = -1;
        private int _hoverButtonIndex = -1;

        private const int ActionButtonSize = 28;
        private const int ActionButtonGap = 14;

        public DataGridView Grid => _grid;

        /// <summary>Phát sinh khi người dùng bấm 1 trong các nút icon (Xem/Sửa/Khóa) ở cột "Thao tác".</summary>
        public event EventHandler<TableActionEventArgs> ActionButtonClicked;

        public BaseTable(string title, string subtitle, FontAwesome.Sharp.IconChar icon,
            string[] columns, Func<int, int, string, string, TablePageResult> loadPage,
            IList<FilterOption> statusOptions = null, int pageSize = 10)
        {
            _loadPage = loadPage ?? throw new ArgumentNullException(nameof(loadPage));
            _statusOptions = statusOptions ?? DefaultStatusOptions();
            _paginationPresenter = new PaginationPresenter(_pagination, pageSize);
            _paginationPresenter.PageChanged += (_, __) => LoadCurrentPage();
            AutoScaleMode = AutoScaleMode.None;
            Dock = DockStyle.Fill;
            BackColor = AppColors.PageBg;
            Padding = new Padding(10);

            _statusColumnIndex = Array.IndexOf(columns, "Trạng thái");
            _lockColumnIndex = Array.IndexOf(columns, "Khóa");
            _actionColumnIndex = Array.IndexOf(columns, "Thao tác");

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
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 86));
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
            AttachActionCellHandlers();
            root.Controls.Add(CreateGridCard(), 0, 2);
            root.Controls.Add(CreatePaginationBar(), 0, 3);
            Controls.Add(root);
            LoadCurrentPage();
        }

        /// <summary>Bộ lọc trạng thái mặc định khi nơi gọi BaseTable không tự cung cấp
        /// (chỉ có "Tất cả trạng thái", tương đương không lọc).</summary>
        private static IList<FilterOption> DefaultStatusOptions() =>
            new List<FilterOption> { new FilterOption("Tất cả trạng thái", null) };

        private Control CreateFilterBar()
        {
            // Panel.Padding chỉ có tác dụng với control Dock/Anchor qua layout engine, KHÔNG tự
            // áp dụng cho control đặt bằng Location tuyệt đối — nên trước đây search/filter bị
            // dính sát mép panel dù đã khai Padding. Ở đây lấy thẳng panelPadding để tính
            // Location, đảm bảo margin thực sự hiển thị đúng như khai báo và không lệch nếu
            // sau này đổi Padding.
            var panelPadding = new Padding(20, 16, 20, 14);
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = AppColors.White,
                Padding = panelPadding,
                Margin = Padding.Empty
            };

            const int controlHeight = 46;
            const int gapBetween = 16;
            int top = panelPadding.Top;

            // Ô tìm kiếm: tách theo MVP giống thanh phân trang — SearchBarControl là View
            // (khung bo góc, icon kính lúp, placeholder, danh sách gợi ý), SearchPresenter
            // nắm debounce + việc lấy gợi ý autocomplete. BaseTable chỉ cần cung cấp nguồn
            // gợi ý (dựa trên chính _loadPage đang có, không thêm truy vấn/logic mới) và
            // lắng nghe SearchCommitted để tải lại trang — không còn phải tự set placeholder
            // qua Win32 (SetCueBanner) như bản cũ.
            _searchBar = new SearchBarControl
            {
                PlaceholderText = "Tìm theo tên, email...",
                Location = new Point(panelPadding.Left, top),
                Size = new Size(420, controlHeight)
            };
            _searchPresenter = new SearchPresenter(_searchBar, BuildSuggestions);
            _searchPresenter.SearchCommitted += (_, __) => _paginationPresenter.ResetToFirstPage();

            // Bộ lọc trạng thái: tách theo MVP giống ô tìm kiếm — FilterComboBox là View thuần
            // UI, FilterPresenter nắm state lựa chọn hiện tại và chỉ báo FilterChanged khi giá
            // trị thực sự đổi. BaseTable lắng nghe để reset về trang 1 rồi tải lại dữ liệu;
            // ManagementTablePage/nơi gọi chịu trách nhiệm build điều kiện WHERE trên DB dựa
            // trên FilterOption.Value đang chọn.
            _filterBox = new FilterComboBox
            {
                Location = new Point(_searchBar.Right + gapBetween, top),
                Size = new Size(240, controlHeight)
            };
            _filterPresenter = new FilterPresenter(_filterBox, _statusOptions);
            _filterPresenter.FilterChanged += (_, __) => _paginationPresenter.ResetToFirstPage();

            panel.Controls.Add(_searchBar);
            panel.Controls.Add(_filterBox);
            return panel;
        }

        /// <summary>Nguồn gợi ý autocomplete: tái sử dụng thẳng _loadPage hiện có (chỉ lấy vài
        /// dòng đầu khớp từ khoá), lấy giá trị cột đầu tiên làm gợi ý — không thêm truy vấn
        /// hay logic dữ liệu mới nào ngoài _loadPage đã có sẵn.</summary>
        private IList<string> BuildSuggestions(string keyword)
        {
            var result = _loadPage(0, 8, keyword, _filterPresenter?.CurrentValue);
            var suggestions = new List<string>();
            foreach (var row in result.Rows)
            {
                if (row.Length == 0) continue;
                var value = Convert.ToString(row[0]);
                if (!string.IsNullOrWhiteSpace(value) && !suggestions.Contains(value))
                    suggestions.Add(value);
            }
            return suggestions;
        }

        private Control CreatePaginationBar()
        {
            var panel = new Panel { Dock = DockStyle.Fill, BackColor = AppColors.White, Padding = new Padding(18, 2, 18, 2) };
            panel.Controls.Add(_pagination);
            return panel;
        }

        private void LoadCurrentPage()
        {
            var result = _loadPage(_paginationPresenter.PageIndex, _paginationPresenter.PageSize,
                _searchBar == null ? string.Empty : _searchBar.Text,
                _filterPresenter?.CurrentValue);
            _grid.Rows.Clear();
            foreach (var row in result.Rows)
                _grid.Rows.Add(row);
            _paginationPresenter.ApplyResult(result.TotalCount);
            ClearHover();
        }

        /// <summary>Bọc DataGridView trong 1 lớp viền mỏng đồng nhất, để thanh cuộn dọc
        /// nằm gọn bên trong khung thay vì tách rời như khi để DataGridView tự vẽ border.</summary>
        private Control CreateGridCard()
        {
            var card = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = AppColors.Border,
                Padding = new Padding(1),
                Margin = Padding.Empty
            };
            card.Controls.Add(_grid);
            return card;
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
                RowHeadersVisible = false,
                BackgroundColor = AppColors.White,
                GridColor = AppColors.TableGrid,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                ColumnHeadersHeight = 58,
                RowTemplate = { Height = 60 },
                EnableHeadersVisualStyles = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ReadOnly = true
            };
            foreach (var column in columns)
            {
                bool isCentered = column == "Trạng thái" || column == "Khóa" || column == "Thao tác";
                grid.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = column,
                    Name = column,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    MinimumWidth = isCentered ? 90 : 100,
                    SortMode = DataGridViewColumnSortMode.Automatic,
                    DefaultCellStyle = { Alignment = isCentered
                        ? DataGridViewContentAlignment.MiddleCenter
                        : DataGridViewContentAlignment.MiddleLeft },
                    HeaderCell = { Style = { Alignment = isCentered
                        ? DataGridViewContentAlignment.MiddleCenter
                        : DataGridViewContentAlignment.MiddleLeft } }
                });
            }

            grid.ColumnHeadersDefaultCellStyle.BackColor = AppColors.TableHeaderBg;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = AppFonts.BodyBold;
            grid.ColumnHeadersDefaultCellStyle.Padding = new Padding(14, 0, 14, 0);
            grid.DefaultCellStyle.Font = AppFonts.Body;
            grid.DefaultCellStyle.ForeColor = AppColors.TableRowText;
            grid.DefaultCellStyle.Padding = new Padding(14, 0, 14, 0);
            grid.DefaultCellStyle.SelectionBackColor = AppColors.AccentSelectionBg;
            grid.DefaultCellStyle.SelectionForeColor = AppColors.TextPrimary;
            grid.DefaultCellStyle.BackColor = AppColors.White;
            grid.AlternatingRowsDefaultCellStyle.BackColor = AppColors.White;
            return grid;
        }

        // ===================== Vẽ lại badge trạng thái/khóa + các nút icon thao tác =====================

        private void AttachActionCellHandlers()
        {
            _grid.CellPainting += Grid_CellPainting;
            _grid.CellMouseMove += Grid_CellMouseMove;
            _grid.CellMouseLeave += Grid_CellMouseLeave;
            _grid.CellMouseClick += Grid_CellMouseClick;
        }

        private void Grid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return; // để header vẽ mặc định

            if (e.ColumnIndex == _statusColumnIndex || e.ColumnIndex == _lockColumnIndex)
            {
                PaintPillCell(e);
            }
            else if (e.ColumnIndex == _actionColumnIndex)
            {
                PaintActionCell(e);
            }
        }

        private void PaintPillCell(DataGridViewCellPaintingEventArgs e)
        {
            e.PaintBackground(e.CellBounds, true);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            var text = Convert.ToString(e.Value ?? string.Empty);
            Color bg, fg;
            ResolvePillColors(e.ColumnIndex, text, out bg, out fg);

            if (!string.IsNullOrEmpty(text))
            {
                var textSize = e.Graphics.MeasureString(text, AppFonts.SmallBold);
                const int paddingX = 14;
                const int pillHeight = 30;
                int pillWidth = (int)Math.Ceiling(textSize.Width) + paddingX * 2;
                var pillRect = new Rectangle(
                    e.CellBounds.X + (e.CellBounds.Width - pillWidth) / 2,
                    e.CellBounds.Y + (e.CellBounds.Height - pillHeight) / 2,
                    pillWidth, pillHeight);

                using (var path = RoundedRect(pillRect, pillHeight / 2))
                using (var brush = new SolidBrush(bg))
                    e.Graphics.FillPath(brush, path);

                TextRenderer.DrawText(e.Graphics, text, AppFonts.SmallBold, pillRect, fg,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
            }

            DrawCellBottomBorder(e);
            e.Handled = true;
        }

        private void ResolvePillColors(int columnIndex, string text, out Color bg, out Color fg)
        {
            if (columnIndex == _statusColumnIndex)
            {
                bool active = text.IndexOf("hoạt động", StringComparison.OrdinalIgnoreCase) >= 0;
                bg = active ? AppColors.SuccessBg : AppColors.ErrorBg;
                fg = active ? AppColors.Success : AppColors.Error;
                return;
            }
            if (columnIndex == _lockColumnIndex)
            {
                bool locked = text.IndexOf("khóa", StringComparison.OrdinalIgnoreCase) >= 0
                    && text.IndexOf("Bình thường", StringComparison.OrdinalIgnoreCase) < 0;
                bg = locked ? AppColors.WarningBg : AppColors.BgLighter;
                fg = locked ? AppColors.Warning : AppColors.TextMuted;
                return;
            }
            bg = AppColors.White;
            fg = AppColors.TextPrimary;
        }

        private void PaintActionCell(DataGridViewCellPaintingEventArgs e)
        {
            e.PaintBackground(e.CellBounds, true);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            var rects = GetActionButtonRects(e.CellBounds.Size);
            for (int i = 0; i < rects.Length; i++)
            {
                var abs = new Rectangle(e.CellBounds.X + rects[i].X, e.CellBounds.Y + rects[i].Y, rects[i].Width, rects[i].Height);
                bool hovered = _hoverRowIndex == e.RowIndex && _hoverButtonIndex == i;
                DrawActionButton(e.Graphics, abs, (TableActionType)i, hovered);
            }

            DrawCellBottomBorder(e);
            e.Handled = true;
        }

        private void DrawCellBottomBorder(DataGridViewCellPaintingEventArgs e)
        {
            using (var pen = new Pen(AppColors.TableGrid))
                e.Graphics.DrawLine(pen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right, e.CellBounds.Bottom - 1);
        }

        /// <summary>Toạ độ 3 nút icon, TƯƠNG ĐỐI so với góc trên-trái của ô (0,0).</summary>
        private static Rectangle[] GetActionButtonRects(Size cellSize)
        {
            const int size = ActionButtonSize;
            const int gap = ActionButtonGap;
            int totalWidth = size * 3 + gap * 2;
            int startX = (cellSize.Width - totalWidth) / 2;
            int y = (cellSize.Height - size) / 2;
            return new[]
            {
                new Rectangle(startX, y, size, size),
                new Rectangle(startX + size + gap, y, size, size),
                new Rectangle(startX + 2 * (size + gap), y, size, size)
            };
        }

        private static Color ActionColor(TableActionType type)
        {
            switch (type)
            {
                case TableActionType.View: return AppColors.TableViewAction;
                case TableActionType.Edit: return AppColors.TableEditAction;
                case TableActionType.Lock: return AppColors.Warning;
                default: return AppColors.TableViewAction;
            }
        }

        private static void DrawActionButton(Graphics g, Rectangle rect, TableActionType type, bool hovered)
        {
            var color = ActionColor(type);

            if (hovered)
            {
                using (var hoverBrush = new SolidBrush(Color.FromArgb(28, color.R, color.G, color.B)))
                    g.FillEllipse(hoverBrush, rect);
            }

            switch (type)
            {
                case TableActionType.View:
                    DrawEyeIcon(g, rect, color);
                    break;
                case TableActionType.Edit:
                    DrawEditIcon(g, rect, color);
                    break;
                case TableActionType.Lock:
                    DrawLockIcon(g, rect, color);
                    break;
            }
        }

        private static void DrawEyeIcon(Graphics g, Rectangle bounds, Color color)
        {
            var r = Deflate(bounds, 4);
            int h = Math.Max(8, (int)(r.Width * 0.62));
            var eyeRect = new Rectangle(r.X, r.Y + (r.Height - h) / 2, r.Width, h);

            using (var brush = new SolidBrush(color))
                g.FillEllipse(brush, eyeRect);

            int iris = Math.Max(6, h * 3 / 5);
            var irisRect = new Rectangle(eyeRect.X + (eyeRect.Width - iris) / 2, eyeRect.Y + (eyeRect.Height - iris) / 2, iris, iris);
            using (var whiteBrush = new SolidBrush(Color.White))
                g.FillEllipse(whiteBrush, irisRect);

            int pupil = Math.Max(3, iris / 2);
            var pupilRect = new Rectangle(irisRect.X + (irisRect.Width - pupil) / 2, irisRect.Y + (irisRect.Height - pupil) / 2, pupil, pupil);
            using (var pupilBrush = new SolidBrush(color))
                g.FillEllipse(pupilBrush, pupilRect);
        }

        private static void DrawEditIcon(Graphics g, Rectangle bounds, Color color)
        {
            var r = Deflate(bounds, 3);
            var squareRect = new Rectangle(r.X, r.Y + r.Height / 3, r.Width * 2 / 3, r.Height * 2 / 3);

            using (var pen = new Pen(color, 2f))
            using (var path = RoundedRect(squareRect, 3))
                g.DrawPath(pen, path);

            var tailStart = new Point(squareRect.X + 4, squareRect.Bottom - 4);
            var tipEnd = new Point(r.Right, r.Y);
            using (var pen = new Pen(color, 2.3f) { StartCap = LineCap.Round, EndCap = LineCap.Round })
                g.DrawLine(pen, tailStart, tipEnd);

            var nib = new[]
            {
                tipEnd,
                new Point(tipEnd.X - 7, tipEnd.Y),
                new Point(tipEnd.X, tipEnd.Y + 7)
            };
            using (var brush = new SolidBrush(color))
                g.FillPolygon(brush, nib);
        }

        private static void DrawLockIcon(Graphics g, Rectangle bounds, Color color)
        {
            var r = Deflate(bounds, 5);
            int shackleHeight = (int)(r.Height * 0.55);
            var shackleRect = new Rectangle(r.X + r.Width / 6, r.Y, r.Width * 2 / 3, shackleHeight * 2);

            using (var pen = new Pen(color, 2.3f))
                g.DrawArc(pen, shackleRect, 180, 180);

            var bodyRect = new Rectangle(r.X, r.Y + shackleHeight - 2, r.Width, r.Height - shackleHeight + 2);
            using (var path = RoundedRect(bodyRect, 3))
            using (var brush = new SolidBrush(color))
                g.FillPath(brush, path);

            int holeSize = Math.Max(3, bodyRect.Width / 5);
            var holeRect = new Rectangle(
                bodyRect.X + (bodyRect.Width - holeSize) / 2,
                bodyRect.Y + (bodyRect.Height - holeSize) / 2 - 1,
                holeSize, holeSize);
            using (var holeBrush = new SolidBrush(Color.White))
                g.FillEllipse(holeBrush, holeRect);
        }

        private static Rectangle Deflate(Rectangle rect, int amount)
        {
            return new Rectangle(rect.X + amount, rect.Y + amount,
                Math.Max(1, rect.Width - amount * 2), Math.Max(1, rect.Height - amount * 2));
        }

        private static GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int d = radius * 2;
            var path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        // ===================== Hover + click cho các nút icon =====================

        private void Grid_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != _actionColumnIndex)
            {
                ClearHover();
                return;
            }

            var cellSize = _grid.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Size;
            var rects = GetActionButtonRects(cellSize);
            int hoveredButton = -1;
            for (int i = 0; i < rects.Length; i++)
            {
                if (rects[i].Contains(e.Location)) { hoveredButton = i; break; }
            }

            if (hoveredButton == _hoverButtonIndex && e.RowIndex == _hoverRowIndex) return;

            int previousRow = _hoverRowIndex;
            _hoverRowIndex = e.RowIndex;
            _hoverButtonIndex = hoveredButton;
            InvalidateActionCell(previousRow);
            InvalidateActionCell(e.RowIndex);
            _grid.Cursor = hoveredButton >= 0 ? Cursors.Hand : Cursors.Default;
        }

        private void Grid_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == _actionColumnIndex) ClearHover();
        }

        private void Grid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != _actionColumnIndex) return;

            var cellSize = _grid.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Size;
            var rects = GetActionButtonRects(cellSize);
            for (int i = 0; i < rects.Length; i++)
            {
                if (rects[i].Contains(e.Location))
                {
                    ActionButtonClicked?.Invoke(this, new TableActionEventArgs(e.RowIndex, (TableActionType)i));
                    break;
                }
            }
        }

        private void ClearHover()
        {
            if (_hoverRowIndex == -1 && _hoverButtonIndex == -1) return;
            int previousRow = _hoverRowIndex;
            _hoverRowIndex = -1;
            _hoverButtonIndex = -1;
            InvalidateActionCell(previousRow);
            if (_grid != null) _grid.Cursor = Cursors.Default;
        }

        private void InvalidateActionCell(int rowIndex)
        {
            if (_actionColumnIndex < 0 || rowIndex < 0 || rowIndex >= _grid.Rows.Count) return;
            _grid.InvalidateCell(_actionColumnIndex, rowIndex);
        }

        // ===================== Giải phóng tài nguyên =====================

        protected override void Dispose(bool disposing)
        {
            if (disposing) _searchPresenter?.Dispose();
            base.Dispose(disposing);
        }
    }
}