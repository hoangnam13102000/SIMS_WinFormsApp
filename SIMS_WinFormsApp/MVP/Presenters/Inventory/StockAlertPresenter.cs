using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SIMS_WinFormsApp.Models.DTOs.Inventory;
using SIMS_WinFormsApp.Services;
using SIMS_WinFormsApp.Services.Implementations.Export;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.Services.Session;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.MVP.Presenters.Inventory
{
    public sealed class StockAlertPresenter
    {
        private static readonly CultureInfo VietnameseCulture = CultureInfo.GetCultureInfo("vi-VN");
        private readonly IStockAlertView _view;
        private readonly IInventoryService _service;
        private readonly IUserSession _session;
        private readonly IDialogService _dialogs;
        private readonly Action _alertsSeen;
        private List<StockAlertListItemDto> _alerts = new List<StockAlertListItemDto>();
        private int _pageIndex;
        private bool _initialized;

        public StockAlertPresenter(
            IStockAlertView view,
            IInventoryService service,
            IUserSession session,
            IDialogService dialogs,
            Action alertsSeen)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _session = session ?? throw new ArgumentNullException(nameof(session));
            _dialogs = dialogs ?? throw new ArgumentNullException(nameof(dialogs));
            _alertsSeen = alertsSeen;

            _view.SearchRequested += OnSearchRequested;
            _view.FiltersChanged += OnFiltersChanged;
            _view.ClearFiltersRequested += OnClearFiltersRequested;
            _view.PageSizeChanged += OnPageSizeChanged;
            _view.PreviousPageRequested += OnPreviousPageRequested;
            _view.NextPageRequested += OnNextPageRequested;
            _view.CsvExportRequested += OnCsvExportRequested;
            _view.ExcelExportRequested += OnExcelExportRequested;
            _view.PlanAlertRequested += OnPlanAlertRequested;
            _view.ResolveAlertRequested += OnResolveAlertRequested;
        }

        private int PageCount => Math.Max(1, (int)Math.Ceiling(_alerts.Count / (double)PageSize));

        private int PageSize => _view.PageSize > 0 ? _view.PageSize : 10;

        public void Initialize()
        {
            if (_initialized)
                return;

            _initialized = true;
            try
            {
                _view.BindCategories(_service.GetCategories());
                _view.ClearFilters();
                _pageIndex = 0;
                if (!ReloadAlerts())
                    return;

                _service.MarkAllAlertsSeen();
                if (_alertsSeen != null)
                    _alertsSeen();
            }
            catch (Exception exception)
            {
                _view.ShowError("Lỗi tải cảnh báo tồn kho", exception.Message);
            }
        }

        private void OnSearchRequested(object sender, EventArgs e)
        {
            _pageIndex = 0;
            ReloadAlerts();
        }

        private void OnFiltersChanged(object sender, EventArgs e)
        {
            DateTime? from = _view.FromDate;
            DateTime? to = _view.ToDate;
            if (from.HasValue && to.HasValue && to.Value.Date < from.Value.Date)
            {
                _view.ShowInvalidDateRange();
                _view.ClearToDate();
            }

            _pageIndex = 0;
            ReloadAlerts();
        }

        private void OnClearFiltersRequested(object sender, EventArgs e)
        {
            _view.ClearFilters();
            _pageIndex = 0;
            ReloadAlerts();
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

        private void OnPlanAlertRequested(int alertId)
        {
            StockAlertListItemDto alert = FindAlert(alertId);
            if (alert == null || !string.Equals(alert.Status, "NEW", StringComparison.OrdinalIgnoreCase))
                return;

            string message = "Đánh dấu \"" + alert.ProductName +
                "\" đã được lên kế hoạch nhập hàng bổ sung?";
            if (!_dialogs.Confirm(_view.Owner, "Lên kế hoạch nhập bổ sung", message))
                return;

            try
            {
                int updated = _service.MarkAlertPlanned(alert.AlertId);
                if (updated == 0)
                {
                    _view.ShowError("Không thể cập nhật",
                        "Cảnh báo đã được người khác cập nhật. Đang tải lại.");
                    ReloadAlerts();
                    return;
                }

                _view.ShowSuccess("Thành công", "Đã đánh dấu \"" + alert.ProductName +
                    "\" đang được lên kế hoạch nhập bổ sung.");
                ReloadAlerts();
            }
            catch (Exception exception)
            {
                _view.ShowError("Không thể cập nhật", exception.Message);
                ReloadAlerts();
            }
        }

        private void OnResolveAlertRequested(int alertId)
        {
            StockAlertListItemDto alert = FindAlert(alertId);
            if (alert == null || string.Equals(alert.Status, "RESOLVED", StringComparison.OrdinalIgnoreCase))
                return;

            string message = "Xác nhận \"" + alert.ProductName +
                "\" đã được nhập hàng bổ sung / không còn cần xử lý?";
            if (!_dialogs.Confirm(_view.Owner, "Đánh dấu đã xử lý", message))
                return;

            try
            {
                if (_session.CurrentUser == null)
                {
                    _view.ShowError("Không thể cập nhật", "Không tìm thấy người dùng đang đăng nhập.");
                    return;
                }

                int updated = _service.ResolveAlert(alert.AlertId, _session.CurrentUser.UserId);
                if (updated == 0)
                {
                    _view.ShowError("Không thể cập nhật",
                        "Cảnh báo đã được người khác cập nhật. Đang tải lại.");
                    ReloadAlerts();
                    return;
                }

                _view.ShowSuccess("Thành công", "Đã xử lý xong \"" + alert.ProductName + "\".");
                ReloadAlerts();
            }
            catch (Exception exception)
            {
                _view.ShowError("Không thể cập nhật", exception.Message);
                ReloadAlerts();
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

        private bool ReloadAlerts()
        {
            try
            {
                IReadOnlyList<StockAlertListItemDto> results = _service.SearchStockAlerts(
                    _view.Keyword, _view.CategoryId, _view.FromDate, _view.ToDate);
                _alerts = results == null
                    ? new List<StockAlertListItemDto>()
                    : results.ToList();
                _view.SetClearFilterVisible(HasActiveFilter());

                if (_pageIndex >= PageCount)
                    _pageIndex = PageCount - 1;

                DisplayCurrentPage();
                return true;
            }
            catch (Exception exception)
            {
                _alerts.Clear();
                _pageIndex = 0;
                _view.SetClearFilterVisible(HasActiveFilter());
                DisplayCurrentPage();
                _view.ShowError("Lỗi tải cảnh báo tồn kho", exception.Message);
                return false;
            }
        }

        private bool HasActiveFilter()
        {
            return !string.IsNullOrWhiteSpace(_view.Keyword) || _view.CategoryId.HasValue ||
                _view.FromDate.HasValue || _view.ToDate.HasValue;
        }

        private void DisplayCurrentPage()
        {
            int pageCount = PageCount;
            if (_pageIndex >= pageCount)
                _pageIndex = pageCount - 1;

            IReadOnlyList<StockAlertListItemDto> page = _alerts
                .Skip(_pageIndex * PageSize)
                .Take(PageSize)
                .ToList();
            _view.DisplayPage(page, _alerts.Count, _pageIndex + 1, pageCount);
        }

        private StockAlertListItemDto FindAlert(int alertId)
        {
            return _alerts.FirstOrDefault(alert => alert.AlertId == alertId);
        }

        private void Export(ITableDataExporter exporter)
        {
            string[] headers =
            {
                "Mã SP", "Tên sản phẩm", "Loại cảnh báo", "Người báo cáo", "Thời gian", "Trạng thái"
            };
            List<object[]> rows = _alerts.Select(alert => new object[]
            {
                alert.ProductCode,
                alert.ProductName,
                GetAlertTypeLabel(alert.AlertType),
                alert.ReportedByName,
                alert.CreatedAt.ToString("dd/MM/yy HH:mm", VietnameseCulture),
                GetStatusLabel(alert.Status)
            }).ToList();

            TableExportRunner.Run(_view.Owner, exporter, "cảnh báo tồn kho",
                () => headers,
                () => rows);
        }

        private static string GetAlertTypeLabel(string alertType)
        {
            if (string.Equals(alertType, "OUT_OF_STOCK", StringComparison.OrdinalIgnoreCase))
                return "Hết hàng";
            if (string.Equals(alertType, "LOW_STOCK", StringComparison.OrdinalIgnoreCase))
                return "Sắp hết hàng";
            return alertType ?? string.Empty;
        }

        private static string GetStatusLabel(string status)
        {
            if (string.Equals(status, "NEW", StringComparison.OrdinalIgnoreCase))
                return "Mới";
            if (string.Equals(status, "PLANNED", StringComparison.OrdinalIgnoreCase))
                return "Đã lên kế hoạch";
            if (string.Equals(status, "RESOLVED", StringComparison.OrdinalIgnoreCase))
                return "Đã xử lý";
            return status ?? string.Empty;
        }
    }
}