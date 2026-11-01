using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SIMS_WinFormsApp.Models.DTOs.Inventory;
using SIMS_WinFormsApp.Services.Interfaces;

namespace SIMS_WinFormsApp.Views.Interfaces
{
    public interface IStockReconciliationListView
    {
        event EventHandler ViewShown;
        event EventHandler DayCheckRequested;
        event EventHandler SearchRequested;
        event EventHandler DateFilterChanged;
        event EventHandler ClearDatesRequested;
        event EventHandler PageSizeChanged;
        event EventHandler PreviousPageRequested;
        event EventHandler NextPageRequested;
        event EventHandler SyncRequested;
        event EventHandler CsvExportRequested;
        event EventHandler ExcelExportRequested;
        event EventHandler ImportRequested;
        event Action<string> ProductCodeCopyRequested;
        event Action<int> DetailRequested;
        event Action<int, string> ActualStockEntered;
        event Action<int, string> ReasonEntered;

        string Keyword { get; }
        DateTime? FromDate { get; }
        DateTime? ToDate { get; }
        int PageSize { get; }
        IWin32Window Owner { get; }

        void DisplayPage(
            IReadOnlyList<StockReconciliationRowDto> rows,
            int totalCount,
            int pageNumber,
            int pageCount);
        void RefreshRow(StockReconciliationRowDto row);
        void SetDates(DateTime? date);
        void ClearToDate();
        void CancelEdit();
        void CopyProductCode(string productCode);
        void OpenDetail(StockReconciliationRowDto row);
        bool OpenImportDialog(string pageTitle, string[] columns, string instructions, ImportRowHandler rowHandler);
        void ShowSuccess(string title, string message);
        void ShowInfo(string title, string message);
        void ShowWarning(string title, string message);
        void ShowError(string title, string message);
    }
}