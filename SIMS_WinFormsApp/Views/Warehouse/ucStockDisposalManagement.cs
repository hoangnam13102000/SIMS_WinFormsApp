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
using SIMS_WinFormsApp.Services.Implementations.Export;
using SIMS_WinFormsApp.Services.Implementations.Import;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.Services.Session;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Interfaces;
using SIMS_WinFormsApp.Views.SystemManagement;
using SIMS_WinFormsApp.Views.Toast;

namespace SIMS_WinFormsApp.Views.Warehouse
{
    public partial class ucStockDisposalManagement : UserControl, IStockDisposalListView
    {
        private const uint EmSetCueBanner = 0x1501;
        private static readonly CultureInfo VietnameseCulture = CultureInfo.GetCultureInfo("vi-VN");
        private static readonly Font StatusFont = new Font("Segoe UI", 9.75F, FontStyle.Bold);
        private readonly IStockDisposalService _service;
        private readonly IUserSession _session;
        private readonly StockDisposalListPresenter _presenter;
        private bool _initialized;
        private bool _suppressDateEvents;

        public ucStockDisposalManagement()
            : this(null, null, false)
        {
        }

        public ucStockDisposalManagement(IStockDisposalService service, IUserSession session, bool canCreate)
        {
            InitializeComponent();
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            _createButton.Visible = canCreate;
            _service = service ?? AppComposition.CreateStockDisposalService();
            _session = session ?? UserSession.Instance;
            _presenter = new StockDisposalListPresenter(this, _service, _session);

            _searchBox.HandleCreated += new EventHandler(this.SearchBox_HandleCreated);
            if (_searchBox.IsHandleCreated)
                ApplySearchPlaceholder();
            _searchBox.TextChanged += new EventHandler(this.SearchBox_TextChanged);
            _fromDatePicker.ValueChanged += new EventHandler(this.DatePicker_ValueChanged);
            _toDatePicker.ValueChanged += new EventHandler(this.DatePicker_ValueChanged);
            _clearDateLink.LinkClicked += new LinkLabelLinkClickedEventHandler(this.ClearDateLink_LinkClicked);
            _pageSizeCombo.SelectedIndexChanged += new EventHandler(this.PageSizeCombo_SelectedIndexChanged);
            _prevButton.Click += new EventHandler(this.PreviousButton_Click);
            _nextButton.Click += new EventHandler(this.NextButton_Click);
            _createButton.Click += new EventHandler(this.CreateButton_Click);
            _optionsButton.Click += new EventHandler(this.OptionsButton_Click);
            _exportCsvItem.Click += new EventHandler(this.ExportCsvItem_Click);
            _exportExcelItem.Click += new EventHandler(this.ExportExcelItem_Click);
            _importItem.Image = IconBitmapCache.Get(IconChar.FileImport, Color.FromArgb(37, 99, 235), new Size(18, 18));
            _importItem.Click += (sender, args) => ImportRequested?.Invoke(this, EventArgs.Empty);
            _grid.CellClick += new DataGridViewCellEventHandler(this.Grid_CellClick);
            _grid.CellDoubleClick += new DataGridViewCellEventHandler(this.Grid_CellDoubleClick);
            _grid.CellMouseMove += new DataGridViewCellMouseEventHandler(this.Grid_CellMouseMove);
            _grid.MouseLeave += new EventHandler(this.Grid_MouseLeave);
            _grid.CellToolTipTextNeeded += new DataGridViewCellToolTipTextNeededEventHandler(this.Grid_CellToolTipTextNeeded);
            _grid.CellFormatting += new DataGridViewCellFormattingEventHandler(this.Grid_CellFormatting);
            VisibleChanged += new EventHandler(this.OnVisibleChanged);
        }

        public event EventHandler SearchRequested;
        public event EventHandler DateFilterChanged;
        public event EventHandler ClearDatesRequested;
        public event EventHandler PageSizeChanged;
        public event EventHandler PreviousPageRequested;
        public event EventHandler NextPageRequested;
        public event EventHandler CreateRequested;
        public event EventHandler CsvExportRequested;
        public event EventHandler ExcelExportRequested;
        public event EventHandler ImportRequested;
        public event Action<string> DisposalCodeCopyRequested;
        public event Action<int> DetailRequested;

        public string Keyword => _searchBox.Text.Trim();
        public DateTime? FromDate => _fromDatePicker.Checked
            ? _fromDatePicker.Value.Date
            : (DateTime?)null;
        public DateTime? ToDate => _toDatePicker.Checked
            ? _toDatePicker.Value.Date
            : (DateTime?)null;
        public int PageSize => _pageSizeCombo.SelectedItem == null
            ? 10
            : Convert.ToInt32(_pageSizeCombo.SelectedItem, CultureInfo.InvariantCulture);
        public IWin32Window Owner => FindForm();

        public void DisplayPage(
            IReadOnlyList<StockDisposalListItemDto> rows,
            int totalCount,
            int pageNumber,
            int pageCount)
        {
            _grid.Rows.Clear();
            foreach (StockDisposalListItemDto disposal in rows ?? Array.Empty<StockDisposalListItemDto>())
            {
                int rowIndex = _grid.Rows.Add(
                    disposal.DisposalId,
                    disposal.DisposalCode,
                    IconBitmapCache.Get(IconChar.Copy, Color.FromArgb(37, 99, 235), new Size(16, 16)),
                    disposal.ReasonLabel,
                    disposal.ItemCount,
                    disposal.TotalLossAmount.ToString("N0", VietnameseCulture),
                    disposal.CreatedByName,
                    disposal.CreatedAt.ToString("dd/MM/yyyy", VietnameseCulture),
                    disposal.StatusText,
                    IconBitmapCache.Get(IconChar.Eye, Color.FromArgb(37, 99, 235), new Size(18, 18)));
                DataGridViewRow row = _grid.Rows[rowIndex];
                row.Tag = disposal;
                row.Cells[colCopy.Index].ToolTipText = "Click để copy mã phiếu tiêu hủy: " + disposal.DisposalCode;
                row.Cells[colDetail.Index].ToolTipText = "Xem chi tiết phiếu";
            }

            _grid.ClearSelection();
            _grid.CurrentCell = null;
            _totalLabel.Text = "Tổng cộng: " + totalCount.ToString("N0", VietnameseCulture) + " phiếu tiêu hủy";
            _pageLabel.Text = pageNumber.ToString(CultureInfo.InvariantCulture);
            _pageInfoLabel.Text = "Trang " + pageNumber.ToString(CultureInfo.InvariantCulture) +
                " / " + pageCount.ToString(CultureInfo.InvariantCulture);
            _prevButton.Enabled = pageNumber > 1;
            _nextButton.Enabled = pageNumber < pageCount;

            if (_grid.Rows.Count == 0)
            {
                _emptyLabel.Text = HasActiveFilters()
                    ? "Không tìm thấy phiếu tiêu hủy phù hợp"
                    : "Chưa có phiếu tiêu hủy nào";
                _emptyLabel.Visible = true;
                _emptyLabel.BringToFront();
            }
            else
            {
                _emptyLabel.Visible = false;
            }
        }

        public void ClearToDate()
        {
            _suppressDateEvents = true;
            try
            {
                _toDatePicker.Checked = false;
            }
            finally
            {
                _suppressDateEvents = false;
            }
            UpdateClearDateVisibility();
        }

        public void ClearDates()
        {
            _suppressDateEvents = true;
            try
            {
                _fromDatePicker.Checked = false;
                _toDatePicker.Checked = false;
            }
            finally
            {
                _suppressDateEvents = false;
            }
            UpdateClearDateVisibility();
        }

        public void CopyDisposalCode(string disposalCode)
        {
            if (string.IsNullOrEmpty(disposalCode))
                return;

            try
            {
                Clipboard.SetText(disposalCode);
                AppToast.Success(this, "Copy thành công", "Đã copy mã phiếu tiêu hủy: " + disposalCode);
            }
            catch (Exception exception)
            {
                AppToast.Error(this, "Không thể copy mã phiếu tiêu hủy", exception.Message);
            }
        }

        public void ShowInvalidDateRange()
        {
            AppToast.Warning(this, "Khoảng ngày không hợp lệ",
                "\"Từ ngày\" không được sau \"Đến ngày\".");
        }

        public void ShowError(string title, string message)
        {
            AppToast.Error(this, title, message);
        }

        public void ShowSuccess(string message)
        {
            AppToast.Success(this, message);
        }

        public bool OpenCreateForm()
        {
            using (var form = new frmStockDisposalEditor(_service, _session))
            {
                if (form.ShowDialog(FindForm()) != DialogResult.OK)
                    return false;

                ShowSuccess(form.SavedMessage);
                return true;
            }
        }

        public void OpenDetail(
            StockDisposalListItemDto disposal,
            IReadOnlyList<StockDisposalDetailLineDto> lines)
        {
            using (var form = new frmStockDisposalDetail(disposal, lines))
                form.ShowDialog(FindForm());
        }

        public bool OpenImportDialog(string pageTitle, string[] columns, string instructions, ImportRowHandler rowHandler)
        {
            return frmImportData.Show(FindForm(), pageTitle, columns, instructions,
                DefaultSpreadsheetImporters.All, rowHandler) == DialogResult.OK;
        }

        private void OnVisibleChanged(object sender, EventArgs e)
        {
            if (!Visible || _initialized || _presenter == null)
                return;

            _initialized = true;
            _presenter.Initialize();
        }

        private void SearchBox_HandleCreated(object sender, EventArgs e)
        {
            ApplySearchPlaceholder();
        }

        private void ApplySearchPlaceholder()
        {
            SendMessage(_searchBox.Handle, EmSetCueBanner, new IntPtr(1),
                "Tìm mã phiếu, mã lô, lý do, người lập...");
        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            _searchTimer.Stop();
            _searchTimer.Start();
        }

        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            _searchTimer.Stop();
            SearchRequested?.Invoke(this, EventArgs.Empty);
        }

        private void DatePicker_ValueChanged(object sender, EventArgs e)
        {
            if (!_suppressDateEvents)
                ((DateTimePicker)sender).Checked = true;
            UpdateClearDateVisibility();
            if (!_suppressDateEvents)
                DateFilterChanged?.Invoke(this, EventArgs.Empty);
        }

        private void UpdateClearDateVisibility()
        {
            _clearDateLink.Visible = _fromDatePicker.Checked || _toDatePicker.Checked;
        }

        private bool HasActiveFilters()
        {
            return !string.IsNullOrWhiteSpace(Keyword) || FromDate.HasValue || ToDate.HasValue;
        }

        private void ClearDateLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ClearDatesRequested?.Invoke(this, EventArgs.Empty);
        }

        private void PageSizeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageSizeChanged?.Invoke(this, EventArgs.Empty);
        }

        private void PreviousButton_Click(object sender, EventArgs e)
        {
            PreviousPageRequested?.Invoke(this, EventArgs.Empty);
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            NextPageRequested?.Invoke(this, EventArgs.Empty);
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            CreateRequested?.Invoke(this, EventArgs.Empty);
        }

        private void OptionsButton_Click(object sender, EventArgs e)
        {
            _optionsMenu.Show(_optionsButton, 0, _optionsButton.Height);
        }

        private void ExportCsvItem_Click(object sender, EventArgs e)
        {
            CsvExportRequested?.Invoke(this, EventArgs.Empty);
        }

        private void ExportExcelItem_Click(object sender, EventArgs e)
        {
            ExcelExportRequested?.Invoke(this, EventArgs.Empty);
        }

        private void Grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _grid.Rows.Count)
                return;

            StockDisposalListItemDto disposal = _grid.Rows[e.RowIndex].Tag as StockDisposalListItemDto;
            if (disposal == null)
                return;

            if (e.ColumnIndex == colCode.Index || e.ColumnIndex == colCopy.Index)
                DisposalCodeCopyRequested?.Invoke(disposal.DisposalCode);
            else if (e.ColumnIndex == colDetail.Index)
                DetailRequested?.Invoke(disposal.DisposalId);
        }

        private void Grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _grid.Rows.Count)
                return;

            StockDisposalListItemDto disposal = _grid.Rows[e.RowIndex].Tag as StockDisposalListItemDto;
            if (disposal != null)
                DetailRequested?.Invoke(disposal.DisposalId);
        }

        private void Grid_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            bool clickable = e.RowIndex >= 0 &&
                (e.ColumnIndex == colCode.Index || e.ColumnIndex == colCopy.Index || e.ColumnIndex == colDetail.Index);
            _grid.Cursor = clickable ? Cursors.Hand : Cursors.Default;
        }

        private void Grid_MouseLeave(object sender, EventArgs e)
        {
            _grid.Cursor = Cursors.Default;
        }

        private void Grid_CellToolTipTextNeeded(
            object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _grid.Rows.Count)
                return;

            StockDisposalListItemDto disposal = _grid.Rows[e.RowIndex].Tag as StockDisposalListItemDto;
            if (disposal == null)
                return;

            if (e.ColumnIndex == colCode.Index || e.ColumnIndex == colCopy.Index)
                e.ToolTipText = "Click để copy mã phiếu tiêu hủy: " + disposal.DisposalCode;
            else if (e.ColumnIndex == colDetail.Index)
                e.ToolTipText = "Xem chi tiết phiếu";
        }

        private void Grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != colStatus.Index)
                return;

            string status = Convert.ToString(e.Value);
            bool cancelled = string.Equals(status, "Đã hủy", StringComparison.OrdinalIgnoreCase);
            Color textColor = cancelled ? Color.FromArgb(220, 38, 38) : Color.FromArgb(22, 163, 74);
            Color backColor = cancelled ? Color.FromArgb(254, 226, 226) : Color.FromArgb(220, 252, 231);
            e.CellStyle.ForeColor = textColor;
            e.CellStyle.BackColor = backColor;
            e.CellStyle.SelectionForeColor = textColor;
            e.CellStyle.SelectionBackColor = backColor;
            e.CellStyle.Font = StatusFont;
        }

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint message, IntPtr wParam, string lParam);

    }
}
