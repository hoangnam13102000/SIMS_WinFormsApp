using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Infrastructure.Composition;
using SIMS_WinFormsApp.Models.DTOs.Inventory;
using SIMS_WinFormsApp.MVP.Presenters.Inventory;
using SIMS_WinFormsApp.Services.Implementations.Import;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.Services.Session;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Interfaces;
using SIMS_WinFormsApp.Views.SystemManagement;
using SIMS_WinFormsApp.Views.Toast;

namespace SIMS_WinFormsApp.Views.Warehouse
{
    public partial class ucStockReconciliation : UserControl, IStockReconciliationListView
    {
        private const uint EmSetCueBanner = 0x1501;
        private static readonly Color Blue = Color.FromArgb(37, 99, 235);
        private static readonly Color Muted = Color.FromArgb(100, 116, 139);
        private static readonly Color TextColor = Color.FromArgb(15, 23, 42);

        private readonly StockReconciliationListPresenter _presenter;
        private readonly bool _canReconcile;
        private bool _suppressDateEvents;
        private bool _eventsReady;
        private bool _fromDateActive;
        private bool _toDateActive;

        public ucStockReconciliation() : this(null, null, false)
        {
        }

        public ucStockReconciliation(IStockReconciliationService service, IUserSession session)
            : this(service, session, true)
        {
        }

        public ucStockReconciliation(
            IStockReconciliationService service,
            IUserSession session,
            bool canReconcile)
        {
            InitializeComponent();
            _canReconcile = canReconcile;
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            _presenter = new StockReconciliationListPresenter(
                this,
                service ?? AppComposition.CreateStockReconciliationService(),
                session ?? UserSession.Instance,
                _canReconcile);

            _pageSizeCombo.SelectedIndex = 0;
            _syncButton.Visible = _canReconcile;
            _importItem.Visible = _canReconcile;
            _menuSeparator.Visible = _canReconcile;
            colEdit.Visible = _canReconcile;
            colActual.ReadOnly = !_canReconcile;
            colReason.ReadOnly = !_canReconcile;
            _exportCsvItem.Image = IconBitmapCache.Get(IconChar.FileCsv, Blue, new Size(18, 18));
            _exportExcelItem.Image = IconBitmapCache.Get(IconChar.FileExcel, Blue, new Size(18, 18));
            _importItem.Image = IconBitmapCache.Get(IconChar.FileImport, Blue, new Size(18, 18));

            if (_searchBox.IsHandleCreated)
                ApplySearchPlaceholder();
            _rolloverTimer.Start();
            _eventsReady = true;
        }

        public event EventHandler ViewShown;
        public event EventHandler DayCheckRequested;
        public event EventHandler SearchRequested;
        public event EventHandler DateFilterChanged;
        public event EventHandler ClearDatesRequested;
        public event EventHandler PageSizeChanged;
        public event EventHandler PreviousPageRequested;
        public event EventHandler NextPageRequested;
        public event EventHandler SyncRequested;
        public event EventHandler CsvExportRequested;
        public event EventHandler ExcelExportRequested;
        public event EventHandler ImportRequested;
        public event Action<string> ProductCodeCopyRequested;
        public event Action<int> DetailRequested;
        public event Action<int, string> ActualStockEntered;
        public event Action<int, string> ReasonEntered;

        public string Keyword => _searchBox.Text;
        public DateTime? FromDate => _fromDateActive ? _fromDatePicker.Value.Date : (DateTime?)null;
        public DateTime? ToDate => _toDateActive ? _toDatePicker.Value.Date : (DateTime?)null;
        public int PageSize => _pageSizeCombo.SelectedItem == null
            ? 10
            : Convert.ToInt32(_pageSizeCombo.SelectedItem, CultureInfo.InvariantCulture);
        public IWin32Window Owner => FindForm();

        // ---------- Hiển thị (Presenter gọi) ----------

        public void DisplayPage(
            IReadOnlyList<StockReconciliationRowDto> rows,
            int totalCount,
            int pageNumber,
            int pageCount)
        {
            CancelEdit();
            Bitmap copyIcon = IconBitmapCache.Get(IconChar.Copy, Blue, new Size(16, 16));
            Bitmap eyeIcon = IconBitmapCache.Get(IconChar.Eye, Color.FromArgb(51, 65, 85), new Size(20, 20));

            _grid.Rows.Clear();
            foreach (StockReconciliationRowDto row in rows)
            {
                int index = _grid.Rows.Add(
                    row.ReconciliationId,
                    row.ProductCode,
                    copyIcon,
                    row.ProductName,
                    row.SystemStock,
                    row.ActualStock,
                    null,
                    row.DiscrepancyText,
                    row.Note ?? string.Empty,
                    StockReconciliationListPresenter.FormatTime(row.UpdatedAt ?? row.CreatedAt),
                    eyeIcon);
                _grid.Rows[index].Tag = row;
                FillRow(_grid.Rows[index], row);
            }

            _totalLabel.Text = "Tổng cộng: " + totalCount + " dòng đối chiếu";
            _pageLabel.Text = pageNumber.ToString(CultureInfo.InvariantCulture);
            _pageInfoLabel.Text = "Trang " + pageNumber + " / " + pageCount;
            _prevButton.Enabled = pageNumber > 1;
            _nextButton.Enabled = pageNumber < pageCount;

            bool filtering = _searchBox.TextLength > 0 || _fromDateActive || _toDateActive;
            _emptyLabel.Text = filtering
                ? "Không có dòng đối chiếu phù hợp"
                : "Chưa có dữ liệu đối chiếu";
            _emptyLabel.Visible = totalCount == 0;
            if (_emptyLabel.Visible)
                _emptyLabel.BringToFront();
        }

        public void RefreshRow(StockReconciliationRowDto row)
        {
            foreach (DataGridViewRow gridRow in _grid.Rows)
            {
                if (gridRow.Tag == row)
                {
                    FillRow(gridRow, row);
                    _grid.InvalidateRow(gridRow.Index);
                    return;
                }
            }
        }

        private void FillRow(DataGridViewRow gridRow, StockReconciliationRowDto row)
        {
            gridRow.Cells[colSystem.Index].Value = row.SystemStock;
            gridRow.Cells[colActual.Index].Value = row.ActualStock;
            gridRow.Cells[colEdit.Index].Value = _canReconcile && row.IsEditable
                ? IconBitmapCache.Get(IconChar.Pen, Blue, new Size(15, 15))
                : null;
            gridRow.Cells[colDiff.Index].Value = row.DiscrepancyText;
            gridRow.Cells[colReason.Index].Value = row.Note ?? string.Empty;
            gridRow.Cells[colTime.Index].Value = StockReconciliationListPresenter.FormatTime(row.UpdatedAt ?? row.CreatedAt);
        }

        public void CancelEdit()
        {
            if (_grid.IsCurrentCellInEditMode)
                _grid.CancelEdit();
        }

        public void CopyProductCode(string productCode)
        {
            if (string.IsNullOrWhiteSpace(productCode))
                return;

            try
            {
                Clipboard.SetText(productCode);
                AppToast.Success(this, "Copy thành công", "Đã copy mã sản phẩm: " + productCode);
            }
            catch (Exception exception)
            {
                AppToast.Error(this, "Không copy được", exception.Message);
            }
        }

        public void OpenDetail(StockReconciliationRowDto row)
        {
            using (var form = new frmStockReconciliationDetail(row))
            {
                form.ShowDialog(FindForm());
            }
        }

        public bool OpenImportDialog(string pageTitle, string[] columns, string instructions, ImportRowHandler rowHandler)
        {
            return frmImportData.Show(FindForm(), pageTitle, columns, instructions,
                DefaultSpreadsheetImporters.All, rowHandler) == DialogResult.OK;
        }

        public void ShowSuccess(string title, string message) => AppToast.Success(this, title, message);
        public void ShowInfo(string title, string message) => AppToast.Info(this, title, message);
        public void ShowWarning(string title, string message) => AppToast.Warning(this, title, message);
        public void ShowError(string title, string message) => AppToast.Error(this, title, message);

        // ---------- Lọc ngày (không có ô tick, icon lịch bên phải) ----------

        public void SetDates(DateTime? date)
        {
            _suppressDateEvents = true;
            try
            {
                if (date.HasValue)
                {
                    _fromDatePicker.Value = date.Value;
                    _toDatePicker.Value = date.Value;
                }
                SetDateActive(_fromDatePicker, date.HasValue);
                SetDateActive(_toDatePicker, date.HasValue);
            }
            finally
            {
                _suppressDateEvents = false;
            }
        }

        public void ClearToDate()
        {
            SetDateActive(_toDatePicker, false);
        }

        private void SetDateActive(DateTimePicker picker, bool active)
        {
            if (picker == _fromDatePicker)
                _fromDateActive = active;
            else
                _toDateActive = active;
            picker.CustomFormat = active ? "dd/MM/yyyy" : " ";
            _clearDateLink.Visible = _fromDateActive || _toDateActive;
        }

        private void DatePicker_ValueChanged(object sender, EventArgs e)
        {
            if (!_eventsReady)
                return;

            if (_suppressDateEvents)
                return;
            SetDateActive((DateTimePicker)sender, true);
            DateFilterChanged?.Invoke(this, EventArgs.Empty);
        }

        private void DatePicker_CloseUp(object sender, EventArgs e)
        {
            var picker = (DateTimePicker)sender;
            bool wasActive = picker == _fromDatePicker ? _fromDateActive : _toDateActive;
            if (wasActive)
                return;
            SetDateActive(picker, true);
            DateFilterChanged?.Invoke(this, EventArgs.Empty);
        }

        private void _fromDateIcon_Click(object sender, EventArgs e)
        {
            OpenCalendar(_fromDatePicker);
        }

        private void _toDateIcon_Click(object sender, EventArgs e)
        {
            OpenCalendar(_toDatePicker);
        }

        private void _clearDateLink_Click(object sender, EventArgs e)
        {
            ClearDatesRequested?.Invoke(this, EventArgs.Empty);
        }

        private void _optionsButton_Click(object sender, EventArgs e)
        {
            _optionsMenu.Show(_optionsButton, 0, _optionsButton.Height);
        }

        private void _exportCsvItem_Click(object sender, EventArgs e)
        {
            CsvExportRequested?.Invoke(this, EventArgs.Empty);
        }

        private void _exportExcelItem_Click(object sender, EventArgs e)
        {
            ExcelExportRequested?.Invoke(this, EventArgs.Empty);
        }

        private void _importItem_Click(object sender, EventArgs e)
        {
            ImportRequested?.Invoke(this, EventArgs.Empty);
        }

        private void _syncButton_Click(object sender, EventArgs e)
        {
            SyncRequested?.Invoke(this, EventArgs.Empty);
        }

        private void _prevButton_Click(object sender, EventArgs e)
        {
            PreviousPageRequested?.Invoke(this, EventArgs.Empty);
        }

        private void _nextButton_Click(object sender, EventArgs e)
        {
            NextPageRequested?.Invoke(this, EventArgs.Empty);
        }

        private void _pageSizeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_eventsReady)
                return;

            PageSizeChanged?.Invoke(this, EventArgs.Empty);
        }

        private void _rolloverTimer_Tick(object sender, EventArgs e)
        {
            if (!_eventsReady)
                return;

            DayCheckRequested?.Invoke(this, EventArgs.Empty);
        }

        private void OnViewVisibleChanged(object sender, EventArgs e)
        {
            if (!_eventsReady)
                return;

            if (Visible)
                ViewShown?.Invoke(this, EventArgs.Empty);
        }

        private static void OpenCalendar(DateTimePicker picker)
        {
            picker.Focus();
            SendKeys.Send("%{DOWN}");
        }

        // ---------- Lưới: copy, sửa tồn thực tế / lý do, xem chi tiết ----------

        private StockReconciliationRowDto RowAt(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= _grid.Rows.Count)
                return null;
            return _grid.Rows[rowIndex].Tag as StockReconciliationRowDto;
        }

        private void Grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            StockReconciliationRowDto row = RowAt(e.RowIndex);
            if (row == null)
                return;

            if (e.ColumnIndex == colCopy.Index || e.ColumnIndex == colProductCode.Index)
                ProductCodeCopyRequested?.Invoke(row.ProductCode);
            else if (_canReconcile && e.ColumnIndex == colEdit.Index)
                BeginEditCell(e.RowIndex, colActual.Index);
            else if (e.ColumnIndex == colDetail.Index)
                DetailRequested?.Invoke(row.ReconciliationId);
        }

        private void Grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            StockReconciliationRowDto row = RowAt(e.RowIndex);
            if (row == null)
                return;

            if (_canReconcile && (e.ColumnIndex == colActual.Index || e.ColumnIndex == colReason.Index))
                BeginEditCell(e.RowIndex, e.ColumnIndex);
            else if (e.ColumnIndex != colCopy.Index && e.ColumnIndex != colProductCode.Index
                     && e.ColumnIndex != colEdit.Index && e.ColumnIndex != colDetail.Index)
                DetailRequested?.Invoke(row.ReconciliationId);
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (!_canReconcile || e.KeyCode != Keys.F2 || _grid.CurrentCell == null)
                return;
            int column = _grid.CurrentCell.ColumnIndex == colReason.Index ? colReason.Index : colActual.Index;
            BeginEditCell(_grid.CurrentCell.RowIndex, column);
            e.Handled = true;
        }

        private void BeginEditCell(int rowIndex, int columnIndex)
        {
            StockReconciliationRowDto row = RowAt(rowIndex);
            if (row == null)
                return;
            if (!_canReconcile || !row.IsEditable)
            {
                ShowWarning("Phiên đã khóa", "Đã qua 00:00 — chỉ được sửa phiên hôm nay.");
                return;
            }
            _grid.CurrentCell = _grid.Rows[rowIndex].Cells[columnIndex];
            _grid.BeginEdit(true);
        }

        private void Grid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            StockReconciliationRowDto row = RowAt(e.RowIndex);
            if (!_canReconcile || row == null || !row.IsEditable
                || (e.ColumnIndex != colActual.Index && e.ColumnIndex != colReason.Index))
                e.Cancel = true;
        }

        private void Grid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            StockReconciliationRowDto row = RowAt(e.RowIndex);
            if (row == null)
                return;

            string text = Convert.ToString(_grid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value,
                CultureInfo.InvariantCulture) ?? string.Empty;
            if (!_canReconcile)
                return;
            if (e.ColumnIndex == colActual.Index)
                ActualStockEntered?.Invoke(row.ReconciliationId, text);
            else if (e.ColumnIndex == colReason.Index)
                ReasonEntered?.Invoke(row.ReconciliationId, text);
        }

        private void Grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            StockReconciliationRowDto row = RowAt(e.RowIndex);
            if (row == null)
                return;

            if (e.ColumnIndex == colDiff.Index)
            {
                if (row.Discrepancy == 0)
                {
                    e.CellStyle.BackColor = Color.FromArgb(220, 252, 231);
                    e.CellStyle.ForeColor = Color.FromArgb(21, 128, 61);
                }
                else if (row.Discrepancy > 0)
                {
                    e.CellStyle.BackColor = Color.FromArgb(255, 237, 213);
                    e.CellStyle.ForeColor = Color.FromArgb(194, 65, 12);
                }
                else
                {
                    e.CellStyle.BackColor = Color.FromArgb(254, 226, 226);
                    e.CellStyle.ForeColor = Color.FromArgb(185, 28, 28);
                }
                e.CellStyle.SelectionBackColor = e.CellStyle.BackColor;
                e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor;
            }
            else if (e.ColumnIndex == colActual.Index)
            {
                e.CellStyle.ForeColor = row.IsEditable ? TextColor : Muted;
            }
            else if (e.ColumnIndex == colReason.Index && string.IsNullOrEmpty(row.Note)
                     && row.Discrepancy != 0)
            {
                e.CellStyle.BackColor = Color.FromArgb(254, 242, 242);
            }
        }

        private void Grid_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            StockReconciliationRowDto row = RowAt(e.RowIndex);
            if (row == null)
                return;

            if (e.ColumnIndex == colCopy.Index || e.ColumnIndex == colProductCode.Index)
                e.ToolTipText = "Click để copy mã sản phẩm: " + row.ProductCode;
            else if (e.ColumnIndex == colActual.Index || e.ColumnIndex == colEdit.Index)
                e.ToolTipText = !_canReconcile
                    ? "Bạn chỉ có quyền xem kho"
                    : row.IsEditable
                    ? "Double-click, F2 hoặc bấm bút để sửa tồn thực tế (phiên hôm nay)"
                    : "Phiên đã khóa lúc 00:00 — chỉ xem, không sửa được";
            else if (e.ColumnIndex == colReason.Index)
                e.ToolTipText = !_canReconcile
                    ? "Bạn chỉ có quyền xem kho"
                    : row.IsEditable
                    ? "Double-click hoặc F2 để ghi lý do chênh lệch"
                    : (row.Note ?? string.Empty);
            else if (e.ColumnIndex == colDetail.Index)
                e.ToolTipText = "Xem chi tiết";
        }

        private void Grid_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            StockReconciliationRowDto row = RowAt(e.RowIndex);
            bool hand = row != null
                && (e.ColumnIndex == colCopy.Index || e.ColumnIndex == colProductCode.Index
                    || e.ColumnIndex == colDetail.Index
                    || (e.ColumnIndex == colEdit.Index && row.IsEditable));
            _grid.Cursor = hand ? Cursors.Hand : Cursors.Default;
        }

        private void _grid_MouseLeave(object sender, EventArgs e)
        {
            _grid.Cursor = Cursors.Default;
        }

        private void _searchBox_HandleCreated(object sender, EventArgs e)
        {
            ApplySearchPlaceholder();
        }

        private void _searchBox_TextChanged(object sender, EventArgs e)
        {
            if (!_eventsReady)
                return;

            _searchTimer.Stop();
            _searchTimer.Start();
        }

        private void _searchTimer_Tick(object sender, EventArgs e)
        {
            if (!_eventsReady)
                return;

            _searchTimer.Stop();
            SearchRequested?.Invoke(this, EventArgs.Empty);
        }

        private void ApplySearchPlaceholder()
        {
            SendMessage(_searchBox.Handle, EmSetCueBanner, new IntPtr(1),
                "Tìm theo tên sản phẩm, mã SP, danh mục, lý do...");
        }

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint message, IntPtr wParam, string lParam);
    }
}
