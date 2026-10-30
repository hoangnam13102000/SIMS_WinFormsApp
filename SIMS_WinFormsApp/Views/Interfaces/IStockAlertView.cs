using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SIMS_WinFormsApp.Models.DTOs.Catalog;
using SIMS_WinFormsApp.Models.DTOs.Inventory;

namespace SIMS_WinFormsApp.Views.Interfaces
{
    public interface IStockAlertView
    {
        string Keyword { get; }
        int? CategoryId { get; }
        DateTime? FromDate { get; }
        DateTime? ToDate { get; }
        int PageSize { get; }
        IWin32Window Owner { get; }

        event EventHandler SearchRequested;
        event EventHandler FiltersChanged;
        event EventHandler ClearFiltersRequested;
        event EventHandler PageSizeChanged;
        event EventHandler PreviousPageRequested;
        event EventHandler NextPageRequested;
        event EventHandler CsvExportRequested;
        event EventHandler ExcelExportRequested;
        event Action<int> PlanAlertRequested;
        event Action<int> ResolveAlertRequested;

        void BindCategories(IReadOnlyList<LookupItem> categories);
        void DisplayPage(IReadOnlyList<StockAlertListItemDto> rows, int totalCount, int pageNumber, int pageCount);
        void SetClearFilterVisible(bool visible);
        void ClearFilters();
        void ClearToDate();
        void ShowInvalidDateRange();
        void ShowSuccess(string title, string message);
        void ShowError(string title, string message);
    }
}