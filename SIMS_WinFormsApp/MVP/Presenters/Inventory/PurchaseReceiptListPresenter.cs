using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SIMS_WinFormsApp.Models.DTOs.Inventory;
using SIMS_WinFormsApp.Services.Implementations.Export;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.MVP.Presenters.Inventory
{
    public sealed class PurchaseReceiptListPresenter
    {
        private static readonly CultureInfo VietnameseCulture = CultureInfo.GetCultureInfo("vi-VN");
        private readonly IPurchaseReceiptListView _view;
        private readonly IInventoryService _service;
        private List<PurchaseReceiptListItemDto> _receipts = new List<PurchaseReceiptListItemDto>();
        private int _pageIndex;
        private bool _initialized;

        public PurchaseReceiptListPresenter(IPurchaseReceiptListView view, IInventoryService service)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _service = service ?? throw new ArgumentNullException(nameof(service));

            _view.SearchRequested += OnSearchRequested;
            _view.DateFilterChanged += OnDateFilterChanged;
            _view.ClearDatesRequested += OnClearDatesRequested;
            _view.PageSizeChanged += OnPageSizeChanged;
            _view.PreviousPageRequested += OnPreviousPageRequested;
            _view.NextPageRequested += OnNextPageRequested;
            _view.CreateRequested += OnCreateRequested;
            _view.CsvExportRequested += OnCsvExportRequested;
            _view.ExcelExportRequested += OnExcelExportRequested;
            _view.ReceiptCodeCopyRequested += OnReceiptCodeCopyRequested;
            _view.DetailRequested += OnDetailRequested;
        }

        private int PageCount => Math.Max(1, (int)Math.Ceiling(_receipts.Count / (double)PageSize));

        private int PageSize => _view.PageSize > 0 ? _view.PageSize : 10;

        public void Initialize()
        {
            if (_initialized)
                return;

            _initialized = true;
            ReloadSuggestions();
            ReloadReceipts();
        }

        public void RefreshAfterCreate()
        {
            ReloadSuggestions();
            _pageIndex = 0;
            ReloadReceipts();
        }

        private void OnSearchRequested(object sender, EventArgs e)
        {
            _pageIndex = 0;
            ReloadReceipts();
        }

        private void OnDateFilterChanged(object sender, EventArgs e)
        {
            DateTime? fromDate = _view.FromDate;
            DateTime? toDate = _view.ToDate;
            if (fromDate.HasValue && toDate.HasValue && toDate.Value.Date < fromDate.Value.Date)
            {
                _view.ShowInvalidDateRange();
                _view.ClearToDate();
            }

            _pageIndex = 0;
            ReloadReceipts();
        }

        private void OnClearDatesRequested(object sender, EventArgs e)
        {
            _view.ClearDates();
            _pageIndex = 0;
            ReloadReceipts();
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

        private void OnCreateRequested(object sender, EventArgs e)
        {
            _view.OpenCreateForm();
        }

        private void OnCsvExportRequested(object sender, EventArgs e)
        {
            Export(new CsvTableExporter());
        }

        private void OnExcelExportRequested(object sender, EventArgs e)
        {
            Export(new ExcelTableExporter());
        }

        private void OnReceiptCodeCopyRequested(string receiptCode)
        {
            _view.CopyReceiptCode(receiptCode);
        }

        private void OnDetailRequested(int receiptId)
        {
            PurchaseReceiptListItemDto receipt = _receipts.FirstOrDefault(item => item.ReceiptId == receiptId);
            if (receipt != null)
                _view.OpenDetail(receipt);
        }

        private void ReloadSuggestions()
        {
            try
            {
                _view.BindSuggestions(_service.GetPurchaseReceiptSuggestions());
            }
            catch (Exception exception)
            {
                _view.ShowError("Lỗi tải gợi ý tìm kiếm", exception.Message);
            }
        }

        private void ReloadReceipts()
        {
            try
            {
                IReadOnlyList<PurchaseReceiptListItemDto> results = _service.SearchPurchaseReceipts(
                    _view.Keyword, _view.FromDate, _view.ToDate);
                _receipts = results == null
                    ? new List<PurchaseReceiptListItemDto>()
                    : results.ToList();

                if (_pageIndex >= PageCount)
                    _pageIndex = PageCount - 1;

                DisplayCurrentPage();
            }
            catch (Exception exception)
            {
                _receipts.Clear();
                _pageIndex = 0;
                DisplayCurrentPage();
                _view.ShowError("Lỗi tải phiếu nhập kho", exception.Message);
            }
        }

        private void DisplayCurrentPage()
        {
            int pageCount = PageCount;
            if (_pageIndex >= pageCount)
                _pageIndex = pageCount - 1;

            IReadOnlyList<PurchaseReceiptListItemDto> page = _receipts
                .Skip(_pageIndex * PageSize)
                .Take(PageSize)
                .ToList();
            _view.DisplayPage(page, _receipts.Count, _pageIndex + 1, pageCount);
        }

        private void Export(ITableDataExporter exporter)
        {
            string[] headers =
            {
                "Mã phiếu", "Nhà cung cấp", "Người tạo", "Ngày tạo", "Số SP", "Tổng tiền", "Trạng thái"
            };
            List<object[]> rows = _receipts.Select(receipt => new object[]
            {
                receipt.ReceiptCode,
                receipt.SupplierName,
                receipt.CreatedByName,
                receipt.CreatedAt.ToString("dd/MM/yyyy", VietnameseCulture),
                receipt.ItemCount,
                receipt.TotalAmount.ToString("N0", VietnameseCulture),
                GetStatusLabel(receipt.Status)
            }).ToList();

            TableExportRunner.Run(_view.Owner, exporter, "phiếu nhập kho",
                () => headers,
                () => rows);
        }

        private static string GetStatusLabel(string status)
        {
            if (string.Equals(status, "COMPLETED", StringComparison.OrdinalIgnoreCase))
                return "Hoàn tất";
            if (string.Equals(status, "CANCELLED", StringComparison.OrdinalIgnoreCase))
                return "Đã hủy";
            return status ?? string.Empty;
        }
    }
}