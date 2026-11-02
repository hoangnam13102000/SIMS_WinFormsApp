using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SIMS_WinFormsApp.Models.DTOs.Catalog;
using SIMS_WinFormsApp.Models.DTOs.Inventory;
using SIMS_WinFormsApp.Services.Implementations.Export;
using SIMS_WinFormsApp.Services.Implementations.Import;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.Services.Session;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.MVP.Presenters.Inventory
{
    public sealed class StockReconciliationListPresenter
    {
        private static readonly CultureInfo VietnameseCulture = CultureInfo.GetCultureInfo("vi-VN");
        private readonly IStockReconciliationListView _view;
        private readonly IStockReconciliationService _service;
        private readonly IUserSession _session;
        private List<StockReconciliationRowDto> _rows = new List<StockReconciliationRowDto>();
        private int _pageIndex;
        private bool _initialized;
        private DateTime _trackedDate = DateTime.Today;

        public StockReconciliationListPresenter(
            IStockReconciliationListView view,
            IStockReconciliationService service,
            IUserSession session)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _session = session ?? throw new ArgumentNullException(nameof(session));

            _view.ViewShown += OnViewShown;
            _view.DayCheckRequested += OnDayCheckRequested;
            _view.SearchRequested += OnSearchRequested;
            _view.DateFilterChanged += OnDateFilterChanged;
            _view.ClearDatesRequested += OnClearDatesRequested;
            _view.PageSizeChanged += OnPageSizeChanged;
            _view.PreviousPageRequested += OnPreviousPageRequested;
            _view.NextPageRequested += OnNextPageRequested;
            _view.SyncRequested += OnSyncRequested;
            _view.CsvExportRequested += (sender, args) => Export(new CsvTableExporter());
            _view.ExcelExportRequested += (sender, args) => Export(new ExcelTableExporter());
            _view.ImportRequested += OnImportRequested;
            _view.ProductCodeCopyRequested += code => _view.CopyProductCode(code);
            _view.DetailRequested += OnDetailRequested;
            _view.ActualStockEntered += OnActualStockEntered;
            _view.ReasonEntered += OnReasonEntered;
        }

        private int PageCount => Math.Max(1, (int)Math.Ceiling(_rows.Count / (double)PageSize));

        private int PageSize => _view.PageSize > 0 ? _view.PageSize : 10;

        private int CurrentUserId => _session.CurrentUser == null ? 0 : _session.CurrentUser.UserId;

        // ---------- Mở trang, chuyển ngày, đồng bộ ----------

        private void OnViewShown(object sender, EventArgs e)
        {
            if (!_initialized)
            {
                _initialized = true;
                _trackedDate = DateTime.Today;
                _view.SetDates(DateTime.Today);
            }

            int created = PrepareToday();
            if (created > 0)
                _view.ShowSuccess("Phiên đối chiếu hôm nay", "Đã tạo " + created + " sản phẩm để đối chiếu kho.");
            ReloadRows();
        }

        // Qua 00:00 thì phiên cũ tự khóa (IsEditable theo ngày), chỉ cần tạo phiên mới.
        private void OnDayCheckRequested(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;
            if (!_initialized || today == _trackedDate)
                return;

            DateTime oldDate = _trackedDate;
            _trackedDate = today;
            _view.CancelEdit();

            int created = PrepareToday();
            _view.ShowInfo("Chuyển ngày",
                "Đã qua 00:00 — Phiên đối chiếu ngày " + oldDate.ToString("dd/MM/yyyy", VietnameseCulture)
                + " đã bị KHÓA (không sửa được nữa). "
                + (created > 0
                    ? "Đã tự động tạo phiên mới cho hôm nay (" + created + " sản phẩm)."
                    : "Đã sẵn sàng phiên mới cho hôm nay."));

            _view.SetDates(today);
            _pageIndex = 0;
            ReloadRows();
        }

        private void OnSyncRequested(object sender, EventArgs e)
        {
            if (CurrentUserId <= 0)
            {
                _view.ShowError("Phiên đăng nhập không hợp lệ", "Vui lòng đăng nhập lại.");
                return;
            }

            int created = PrepareToday();
            if (created > 0)
                _view.ShowSuccess("Đồng bộ thành công",
                    "Đã thêm " + created + " sản phẩm vào phiên đối chiếu hôm nay.");
            else
                _view.ShowSuccess("Đã đồng bộ", "Phiên hôm nay đã có đủ sản phẩm đang bán.");

            _view.SetDates(DateTime.Today);
            _pageIndex = 0;
            ReloadRows();
        }

        private int PrepareToday()
        {
            if (CurrentUserId <= 0)
                return 0;

            try
            {
                return _service.PrepareToday(CurrentUserId);
            }
            catch (Exception exception)
            {
                _view.ShowError("Lỗi phiên đối chiếu", exception.Message);
                return 0;
            }
        }

        // ---------- Tìm kiếm, lọc ngày, phân trang ----------

        private void OnSearchRequested(object sender, EventArgs e)
        {
            _pageIndex = 0;
            ReloadRows();
        }

        private void OnDateFilterChanged(object sender, EventArgs e)
        {
            DateTime? fromDate = _view.FromDate;
            DateTime? toDate = _view.ToDate;
            if (fromDate.HasValue && toDate.HasValue && toDate.Value.Date < fromDate.Value.Date)
            {
                _view.ShowWarning("Khoảng ngày không hợp lệ",
                    "\"Từ ngày\" (" + fromDate.Value.ToString("dd/MM/yyyy", VietnameseCulture)
                    + ") không được sau \"Đến ngày\" (" + toDate.Value.ToString("dd/MM/yyyy", VietnameseCulture) + ").");
                _view.ClearToDate();
            }

            _pageIndex = 0;
            ReloadRows();
        }

        private void OnClearDatesRequested(object sender, EventArgs e)
        {
            _view.SetDates(null);
            _pageIndex = 0;
            ReloadRows();
        }

        private void OnPageSizeChanged(object sender, EventArgs e)
        {
            _pageIndex = 0;
            DisplayCurrentPage();
        }

        private void OnPreviousPageRequested(object sender, EventArgs e)
        {
            if (_pageIndex <= 0)
                return;

            _pageIndex--;
            DisplayCurrentPage();
        }

        private void OnNextPageRequested(object sender, EventArgs e)
        {
            if (_pageIndex + 1 >= PageCount)
                return;

            _pageIndex++;
            DisplayCurrentPage();
        }

        private void ReloadRows()
        {
            try
            {
                IReadOnlyList<StockReconciliationRowDto> results = _service.Search(
                    _view.Keyword, _view.FromDate, _view.ToDate);
                _rows = results == null
                    ? new List<StockReconciliationRowDto>()
                    : results.ToList();
            }
            catch (Exception exception)
            {
                _rows = new List<StockReconciliationRowDto>();
                _view.ShowError("Lỗi tải đối chiếu kho", exception.Message);
            }

            DisplayCurrentPage();
        }

        private void DisplayCurrentPage()
        {
            int pageCount = PageCount;
            if (_pageIndex >= pageCount)
                _pageIndex = pageCount - 1;
            if (_pageIndex < 0)
                _pageIndex = 0;

            IReadOnlyList<StockReconciliationRowDto> page = _rows
                .Skip(_pageIndex * PageSize)
                .Take(PageSize)
                .ToList();
            _view.DisplayPage(page, _rows.Count, _pageIndex + 1, pageCount);
        }

        // ---------- Sửa tồn thực tế / lý do ----------

        private void OnActualStockEntered(int reconciliationId, string text)
        {
            StockReconciliationRowDto row = FindRow(reconciliationId);
            if (row == null)
                return;

            int newActual;
            string digits = (text ?? string.Empty).Trim().Replace(".", string.Empty).Replace(",", string.Empty);
            if (!int.TryParse(digits, NumberStyles.None, CultureInfo.InvariantCulture, out newActual))
            {
                _view.RefreshRow(row);
                _view.ShowError("Số không hợp lệ", "Tồn thực tế phải là số nguyên ≥ 0.");
                return;
            }
            if (newActual > row.SystemStock)
            {
                _view.RefreshRow(row);
                _view.ShowWarning("Tồn thực tế vượt giới hạn", "Tồn thực tế không được > tồn kho lúc lập phiên.");
                return;
            }
            if (newActual == row.ActualStock)
            {
                _view.RefreshRow(row);
                return;
            }

            SaveCount(row, newActual, row.Note);
        }

        private void OnReasonEntered(int reconciliationId, string text)
        {
            StockReconciliationRowDto row = FindRow(reconciliationId);
            if (row == null)
                return;

            string newNote = (text ?? string.Empty).Trim();
            if (newNote == (row.Note ?? string.Empty))
                return;

            SaveCount(row, row.ActualStock, newNote);
        }

        private void SaveCount(StockReconciliationRowDto row, int actual, string note)
        {
            if (!row.IsEditable)
            {
                _view.RefreshRow(row);
                _view.ShowWarning("Phiên đã khóa", "Đã qua 00:00 — chỉ được sửa phiên hôm nay.");
                return;
            }

            CatalogSaveResult result = _service.UpdateCount(row.ReconciliationId, actual, note, CurrentUserId);
            if (result == null || !result.Success)
            {
                _view.RefreshRow(row);
                _view.ShowError("Cập nhật thất bại", result == null ? "Không thể cập nhật." : result.Message);
                return;
            }

            int delta = actual - row.ActualStock;
            row.ActualStock = actual;
            row.Note = string.IsNullOrWhiteSpace(note) ? null : note.Trim();
            if (delta != 0)
            {
                row.CurrentStock += delta;
                row.UpdatedAt = DateTime.Now;
            }
            _view.RefreshRow(row);

            if (delta != 0)
            {
                string message = "Tồn thực tế \"" + row.ProductName + "\" = " + row.ActualStock
                    + " (chênh lệch: " + row.DiscrepancyText + "). Tồn kho đã được cập nhật.";
                if (row.Discrepancy != 0 && row.Note == null)
                    message += " Nhập lý do ở cột Lý do.";
                _view.ShowSuccess("Đã cập nhật", message);
            }
            else
            {
                _view.ShowSuccess("Đã lưu lý do", "Đã lưu lý do cho \"" + row.ProductName + "\".");
            }
        }

        // ---------- Chi tiết, xuất file ----------

        private void OnDetailRequested(int reconciliationId)
        {
            StockReconciliationRowDto row = FindRow(reconciliationId);
            if (row != null)
                _view.OpenDetail(row);
        }

        private void OnImportRequested(object sender, EventArgs e)
        {
            if (CurrentUserId <= 0)
            {
                _view.ShowError("Phiên đăng nhập không hợp lệ", "Vui lòng đăng nhập lại.");
                return;
            }

            PrepareToday();
            var importer = new StockReconciliationImportRowHandler(_service, CurrentUserId);
            bool imported = _view.OpenImportDialog(
                "Đối chiếu kho",
                StockReconciliationImportRowHandler.ExpectedColumns,
                "Mỗi dòng cập nhật 1 sản phẩm trong phiên hôm nay. Tồn thực tế là số nguyên, không lớn hơn tồn kho của phiên. Lý do để trống thì giữ lý do cũ.",
                importer.Handle);
            if (imported)
                ReloadRows();
        }

        private StockReconciliationRowDto FindRow(int reconciliationId)
        {
            return _rows.FirstOrDefault(item => item.ReconciliationId == reconciliationId);
        }

        private void Export(ITableDataExporter exporter)
        {
            string[] headers =
            {
                "Ngày phiên", "Mã SP", "Sản phẩm", "Danh mục", "Tồn kho", "Tồn thực tế",
                "Chênh lệch", "Lý do", "Thời gian", "Trạng thái phiên"
            };
            List<object[]> rows = _rows.Select(row => new object[]
            {
                row.CreatedAt.ToString("dd/MM/yyyy", VietnameseCulture),
                row.ProductCode,
                row.ProductName,
                row.CategoryName,
                row.SystemStock,
                row.ActualStock,
                row.DiscrepancyText,
                row.Note ?? string.Empty,
                FormatTime(row.UpdatedAt ?? row.CreatedAt),
                row.SessionStatusText
            }).ToList();

            TableExportRunner.Run(_view.Owner, exporter, "đối chiếu kho",
                () => headers,
                () => rows);
        }

        public static string FormatTime(DateTime? value)
        {
            return value.HasValue ? value.Value.ToString("dd/MM/yyyy HH:mm", VietnameseCulture) : "-";
        }
    }
}