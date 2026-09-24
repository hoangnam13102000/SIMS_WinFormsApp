using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.MVP.ViewModels;
using SIMS_WinFormsApp.UI.Controls.Filter;
using SIMS_WinFormsApp.UI.I18n;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls.AuditLog
{
    /// <summary>
    /// Vẽ các ô đặc biệt của lưới nhật ký: thời gian 2 dòng (đủ ngày + giờ),
    /// người dùng, pill hành động/đối tượng, và nút mắt ở cột thao tác.
    /// </summary>
    public sealed class AuditLogGridPainter : IDisposable
    {
        public const string TimeColumn = "colTime";
        public const string UserColumn = "colUser";
        public const string ActionColumn = "colAction";
        public const string TableColumn = "colTable";
        public const string DetailColumn = "colDetail";
        public const string ViewColumn = "colView";

        private const int EyeSize = 34;

        private readonly DataGridView _grid;
        private IReadOnlyList<AuditLogGridRow> _rows = Array.Empty<AuditLogGridRow>();
        private int _hoverRow = -1;
        private bool _hoverEye;

        public event EventHandler<int> ViewRequested;

        public AuditLogGridPainter(DataGridView grid)
        {
            _grid = grid ?? throw new ArgumentNullException(nameof(grid));
            _grid.CellPainting += OnCellPainting;
            _grid.CellMouseMove += OnCellMouseMove;
            _grid.CellMouseLeave += OnCellMouseLeave;
            _grid.CellMouseClick += OnCellMouseClick;
            _grid.CellToolTipTextNeeded += OnToolTip;
            ThemeManager.Instance.ThemeChanged += OnThemeChanged;
        }

        public void SetRows(IReadOnlyList<AuditLogGridRow> rows)
        {
            _rows = rows ?? Array.Empty<AuditLogGridRow>();
            _hoverRow = -1;
            _hoverEye = false;
        }

        public void ApplyLocalization()
        {
            SetHeader(TimeColumn, Lang.Get("audit.column.time"));
            SetHeader(UserColumn, Lang.Get("audit.column.user"));
            SetHeader(ActionColumn, Lang.Get("audit.column.action"));
            SetHeader(TableColumn, Lang.Get("audit.column.table"));
            SetHeader(DetailColumn, Lang.Get("audit.column.detail"));
            SetHeader(ViewColumn, string.Empty);
            if (_grid.Columns.Contains(ViewColumn))
                _grid.Columns[ViewColumn].ToolTipText = Lang.Get("audit.view.tooltip");
            _grid.Invalidate();
        }

        private void SetHeader(string name, string text)
        {
            if (_grid.Columns.Contains(name))
                _grid.Columns[name].HeaderText = text;
        }

        private void OnCellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.CellBounds.Width <= 0 || e.CellBounds.Height <= 0) return;

            try
            {
                if (e.RowIndex < 0)
                {
                    if (IsColumn(e.ColumnIndex, ViewColumn))
                        PaintViewHeader(e);
                    return;
                }

                if (e.RowIndex >= _rows.Count) return;
                var row = _rows[e.RowIndex];
                if (IsColumn(e.ColumnIndex, TimeColumn)) PaintTime(e, row);
                else if (IsColumn(e.ColumnIndex, UserColumn)) PaintUser(e, row);
                else if (IsColumn(e.ColumnIndex, ActionColumn)) PaintPill(e, row.ActionLabel, row.ActionBackground, row.ActionForeground);
                else if (IsColumn(e.ColumnIndex, TableColumn)) PaintPill(e, row.TableLabel, row.TableBackground, row.TableForeground);
                else if (IsColumn(e.ColumnIndex, ViewColumn)) PaintEye(e, _hoverRow == e.RowIndex && _hoverEye);
            }
            catch (ArgumentException)
            {
                e.Handled = true;
            }
        }

        private void PaintViewHeader(DataGridViewCellPaintingEventArgs e)
        {
            e.PaintBackground(e.ClipBounds, false);
            using (var brush = new SolidBrush(AppColors.TableHeaderBg))
                e.Graphics.FillRectangle(brush, e.CellBounds);
            var icon = new Rectangle(
                e.CellBounds.X + (e.CellBounds.Width - 16) / 2,
                e.CellBounds.Y + (e.CellBounds.Height - 16) / 2,
                16, 16);
            var bitmap = IconBitmapCache.Get(IconChar.Eye, Color.White, icon.Size);
            if (bitmap != null) e.Graphics.DrawImageUnscaled(bitmap, icon.Location);
            e.Handled = true;
        }

        private static void PaintTime(DataGridViewCellPaintingEventArgs e, AuditLogGridRow row)
        {
            e.PaintBackground(e.CellBounds, true);
            var dateRect = new Rectangle(e.CellBounds.X + 12, e.CellBounds.Y + 8, e.CellBounds.Width - 20, 20);
            var timeRect = new Rectangle(e.CellBounds.X + 12, e.CellBounds.Y + 28, e.CellBounds.Width - 20, 18);
            TextRenderer.DrawText(e.Graphics, row.CreatedAt.ToString("dd/MM/yyyy"), AppFonts.BodyBold, dateRect, AppColors.TextTitle,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine | TextFormatFlags.NoPadding);
            TextRenderer.DrawText(e.Graphics, row.CreatedAt.ToString("HH:mm:ss"), AppFonts.Small, timeRect, AppColors.TextMuted,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine | TextFormatFlags.NoPadding);
            DrawBottomBorder(e);
            e.Handled = true;
        }

        private static void PaintUser(DataGridViewCellPaintingEventArgs e, AuditLogGridRow row)
        {
            e.PaintBackground(e.CellBounds, true);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            string name = string.IsNullOrWhiteSpace(row.Username) ? "-" : row.Username;
            string initial = InitialOf(name);
            var circle = new Rectangle(e.CellBounds.X + 12, e.CellBounds.Y + (e.CellBounds.Height - 28) / 2, 28, 28);
            using (var brush = new SolidBrush(AppColors.AccentBgSoft))
                e.Graphics.FillEllipse(brush, circle);
            TextRenderer.DrawText(e.Graphics, initial, AppFonts.SmallBold, circle, AppColors.Accent,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);

            var textRect = new Rectangle(circle.Right + 8, e.CellBounds.Y, Math.Max(10, e.CellBounds.Right - circle.Right - 16), e.CellBounds.Height);
            TextRenderer.DrawText(e.Graphics, name, AppFonts.Body, textRect, AppColors.TextPrimary,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding);
            DrawBottomBorder(e);
            e.Handled = true;
        }

        private static void PaintPill(DataGridViewCellPaintingEventArgs e, string text, Color bg, Color fg)
        {
            e.PaintBackground(e.CellBounds, true);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            text = text ?? string.Empty;
            if (text.Length > 0)
            {
                var measured = TextRenderer.MeasureText(e.Graphics, text, AppFonts.SmallBold,
                    Size.Empty, TextFormatFlags.NoPadding | TextFormatFlags.SingleLine);
                const int padX = 12;
                const int pillH = 28;
                int pillW = Math.Min(e.CellBounds.Width - 16, measured.Width + padX * 2);
                pillW = Math.Max(pillW, 36);
                var pill = new Rectangle(e.CellBounds.X + 10, e.CellBounds.Y + (e.CellBounds.Height - pillH) / 2, pillW, pillH);
                using (var path = AppRadius.GetRoundedPath(pill, pillH / 2))
                using (var brush = new SolidBrush(bg))
                    e.Graphics.FillPath(brush, path);
                TextRenderer.DrawText(e.Graphics, text, AppFonts.SmallBold, pill, fg,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding);
            }
            DrawBottomBorder(e);
            e.Handled = true;
        }

        private static void PaintEye(DataGridViewCellPaintingEventArgs e, bool hovered)
        {
            e.PaintBackground(e.CellBounds, true);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var button = EyeRect(e.CellBounds);
            Color iconColor = hovered ? AppColors.Accent : AppColors.TableViewAction;
            if (hovered)
            {
                using (var brush = new SolidBrush(AppColors.AccentBgSoft))
                    e.Graphics.FillEllipse(brush, button);
            }
            else
            {
                using (var brush = new SolidBrush(AppColors.BgLighter))
                    e.Graphics.FillEllipse(brush, button);
            }

            var icon = new Rectangle(button.X + 8, button.Y + 8, 18, 18);
            var bitmap = IconBitmapCache.Get(IconChar.Eye, iconColor, icon.Size);
            if (bitmap != null) e.Graphics.DrawImageUnscaled(bitmap, icon.Location);
            DrawBottomBorder(e);
            e.Handled = true;
        }

        private void OnCellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || !IsColumn(e.ColumnIndex, ViewColumn))
            {
                ClearHover();
                return;
            }

            if (_hoverRow == e.RowIndex && _hoverEye) return;
            int previous = _hoverRow;
            _hoverRow = e.RowIndex;
            _hoverEye = true;
            InvalidateView(previous);
            InvalidateView(e.RowIndex);
            _grid.Cursor = Cursors.Hand;
        }

        private void OnCellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (IsColumn(e.ColumnIndex, ViewColumn)) ClearHover();
        }

        private void OnCellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _rows.Count || !IsColumn(e.ColumnIndex, ViewColumn)) return;
            ViewRequested?.Invoke(this, e.RowIndex);
        }

        private void OnToolTip(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (IsColumn(e.ColumnIndex, ViewColumn))
            {
                e.ToolTipText = Lang.Get("audit.view.tooltip");
                return;
            }

            if (e.RowIndex >= _rows.Count) return;
            var row = _rows[e.RowIndex];
            if (IsColumn(e.ColumnIndex, TimeColumn))
                e.ToolTipText = row.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss");
            else if (IsColumn(e.ColumnIndex, UserColumn))
                e.ToolTipText = row.Username;
            else if (IsColumn(e.ColumnIndex, ActionColumn))
                e.ToolTipText = row.ActionLabel;
            else if (IsColumn(e.ColumnIndex, TableColumn))
                e.ToolTipText = row.TableLabel;
            else if (IsColumn(e.ColumnIndex, DetailColumn))
                e.ToolTipText = row.DetailText;
        }

        private void ClearHover()
        {
            if (_hoverRow < 0 && !_hoverEye) return;
            int previous = _hoverRow;
            _hoverRow = -1;
            _hoverEye = false;
            InvalidateView(previous);
            _grid.Cursor = Cursors.Default;
        }

        private void InvalidateView(int row)
        {
            if (row < 0 || row >= _grid.Rows.Count || !_grid.Columns.Contains(ViewColumn)) return;
            _grid.InvalidateCell(_grid.Columns[ViewColumn].Index, row);
        }

        private bool IsColumn(int index, string name)
        {
            return index >= 0 && index < _grid.Columns.Count && _grid.Columns[index].Name == name;
        }

        private static Rectangle EyeRect(Rectangle cell)
        {
            return new Rectangle(
                cell.X + (cell.Width - EyeSize) / 2,
                cell.Y + (cell.Height - EyeSize) / 2,
                EyeSize, EyeSize);
        }

        private static string InitialOf(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name[0] == '(' || name[0] == '-') return "?";
            return name.Trim().Substring(0, 1).ToUpperInvariant();
        }

        private static void DrawBottomBorder(DataGridViewCellPaintingEventArgs e)
        {
            using (var pen = new Pen(AppColors.TableGrid))
                e.Graphics.DrawLine(pen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right, e.CellBounds.Bottom - 1);
        }

        private void OnThemeChanged(object sender, EventArgs e) => _grid.Invalidate();

        public void Dispose()
        {
            ThemeManager.Instance.ThemeChanged -= OnThemeChanged;
            _grid.CellPainting -= OnCellPainting;
            _grid.CellMouseMove -= OnCellMouseMove;
            _grid.CellMouseLeave -= OnCellMouseLeave;
            _grid.CellMouseClick -= OnCellMouseClick;
            _grid.CellToolTipTextNeeded -= OnToolTip;
        }
    }
}
