using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SIMS_WinFormsApp.Models.DTOs.Inventory;

namespace SIMS_WinFormsApp.Views.Interfaces
{
    public interface IPurchaseReceiptListView
    {
        event EventHandler SearchRequested;
        event EventHandler DateFilterChanged;
        event EventHandler ClearDatesRequested;
        event EventHandler PageSizeChanged;
        event EventHandler PreviousPageRequested;
        event EventHandler NextPageRequested;
        event EventHandler CreateRequested;
        event EventHandler CsvExportRequested;
        event EventHandler ExcelExportRequested;
        event Action<string> ReceiptCodeCopyRequested;
        event Action<int> DetailRequested;

        string Keyword { get; }
        DateTime? FromDate { get; }
        DateTime? ToDate { get; }
        int PageSize { get; }
        IWin32Window Owner { get; }

        void DisplayPage(
            IReadOnlyList<PurchaseReceiptListItemDto> rows,
            int totalCount,
            int pageNumber,
            int pageCount);
        void BindSuggestions(IReadOnlyList<string> suggestions);
        void ClearToDate();
        void ClearDates();
        void CopyReceiptCode(string receiptCode);
        void ShowInvalidDateRange();
        void ShowError(string title, string message);
        void OpenCreateForm();
        void OpenDetail(PurchaseReceiptListItemDto receipt);
    }
}