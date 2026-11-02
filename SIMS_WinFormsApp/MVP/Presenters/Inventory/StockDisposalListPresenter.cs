using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SIMS_WinFormsApp.Models.DTOs.Inventory;
using SIMS_WinFormsApp.Services.Implementations.Export;
using SIMS_WinFormsApp.Services.Implementations.Import;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.Services.Session;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.MVP.Presenters.Inventory
{
    public sealed class StockDisposalListPresenter
    {
        private static readonly CultureInfo VietnameseCulture = CultureInfo.GetCultureInfo("vi-VN");
        private readonly IStockDisposalListView _view;
        private readonly IStockDisposalService _service;
        private readonly IUserSession _session;
        private List<StockDisposalListItemDto> _disposals = new List<StockDisposalListItemDto>();
        private int _pageIndex;
        private bool _initialized;

        public StockDisposalListPresenter(
            IStockDisposalListView view, IStockDisposalService service, IUserSession session)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _session = session ?? throw new ArgumentNullException(nameof(session));

            _view.SearchRequested += OnSearchRequested;
            _view.DateFilterChanged += OnDateFilterChanged;
            _view.ClearDatesRequested += OnClearDatesRequested;
            _view.PageSizeChanged += OnPageSizeChanged;
            _view.PreviousPageRequested += OnPreviousPageRequested;
            _view.NextPageRequested += OnNextPageRequested;
            _view.CreateRequested += OnCreateRequested;
            _view.CsvExportRequested += OnCsvExportRequested;
            _view.ExcelExportRequested += OnExcelExportRequested;
            _view.ImportRequested += OnImportRequested;
            _view.DisposalCodeCopyRequested += OnDisposalCodeCopyRequested;
            _view.DetailRequested += OnDetailRequested;
        }

        private int PageCount => Math.Max(1, (int)Math.Ceiling(_disposals.Count / (double)PageSize));

        private int PageSize => _view.PageSize > 0 ? _view.PageSize : 10;

        public void Initialize()
        {
            if (_initialized)
                return;

            _initialized = true;
            ReloadDisposals();
        }

        private void OnSearchRequested(object sender, EventArgs e)
        {
            _pageIndex = 0;
            ReloadDisposals();
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
            ReloadDisposals();
        }

        private void OnClearDatesRequested(object sender, EventArgs e)
        {
            _view.ClearDates();
            _pageIndex = 0;
            ReloadDisposals();
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
            if (_view.OpenCreateForm())
            {
                _pageIndex = 0;
                ReloadDisposals();
            }
        }

        private void OnCsvExportRequested(object sender, EventArgs e)
        {
            Export(new CsvTableExporter());
        }

        private void OnExcelExportRequested(object sender, EventArgs e)
        {
            Export(new ExcelTableExporter());
        }

        private void OnImportRequested(object sender, EventArgs e)
        {
            int userId = _session.CurrentUser == null ? 0 : _session.CurrentUser.UserId;
            if (userId <= 0)
            {
                _view.ShowError("Phiên đăng nhập không hợp lệ", "Vui lòng đăng nhập lại.");
                return;
            }

            var importer = new StockDisposalImportRowHandler(_service, userId);
            bool imported = _view.OpenImportDialog(
                "Tiêu hủy hàng",
                StockDisposalImportRowHandler.ExpectedColumns,
                "Mỗi dòng tạo 1 phiếu tiêu hủy cho 1 lô. Mã lô dạng LOT_000xxx, số lượng không lớn hơn số còn của lô. Lý do: Hết hạn, Hỏng / hư hỏng, Chất lượng hoặc Khác (để trống là Khác).",
                importer.Handle);
            if (imported)
            {
                _pageIndex = 0;
                ReloadDisposals();
            }
        }

        private void OnDisposalCodeCopyRequested(string disposalCode)
        {
            _view.CopyDisposalCode(disposalCode);
        }

        private void OnDetailRequested(int disposalId)
        {
            StockDisposalListItemDto disposal =
                _disposals.FirstOrDefault(item => item.DisposalId == disposalId);
            if (disposal == null)
                return;

            try
            {
                IReadOnlyList<StockDisposalDetailLineDto> lines = _service.GetDisposalDetails(disposalId);
                _view.OpenDetail(disposal, lines);
            }
            catch (Exception exception)
            {
                _view.ShowError("Lỗi tải chi tiết phiếu tiêu hủy", exception.Message);
            }
        }

        private void ReloadDisposals()
        {
            try
            {
                IReadOnlyList<StockDisposalListItemDto> results = _service.SearchDisposals(
                    _view.Keyword, _view.FromDate, _view.ToDate);
                _disposals = results == null
                    ? new List<StockDisposalListItemDto>()
                    : results.ToList();

                if (_pageIndex >= PageCount)
                    _pageIndex = PageCount - 1;

                DisplayCurrentPage();
            }
            catch (Exception exception)
            {
                _disposals.Clear();
                _pageIndex = 0;
                DisplayCurrentPage();
                _view.ShowError("Lỗi tải phiếu tiêu hủy", exception.Message);
            }
        }

        private void DisplayCurrentPage()
        {
            int pageCount = PageCount;
            if (_pageIndex >= pageCount)
                _pageIndex = pageCount - 1;

            IReadOnlyList<StockDisposalListItemDto> page = _disposals
                .Skip(_pageIndex * PageSize)
                .Take(PageSize)
                .ToList();
            _view.DisplayPage(page, _disposals.Count, _pageIndex + 1, pageCount);
        }

        private void Export(ITableDataExporter exporter)
        {
            string[] headers =
            {
                "Mã phiếu", "Lý do", "Số dòng", "Tổn thất", "Người lập", "Ngày lập", "Trạng thái"
            };
            List<object[]> rows = _disposals.Select(disposal => new object[]
            {
                disposal.DisposalCode,
                disposal.ReasonLabel,
                disposal.ItemCount,
                disposal.TotalLossAmount.ToString("N0", VietnameseCulture),
                disposal.CreatedByName,
                disposal.CreatedAt.ToString("dd/MM/yyyy", VietnameseCulture),
                disposal.StatusText
            }).ToList();

            TableExportRunner.Run(_view.Owner, exporter, "phiếu tiêu hủy",
                () => headers,
                () => rows);
        }
    }
}
