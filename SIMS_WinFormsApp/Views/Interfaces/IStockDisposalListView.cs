using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SIMS_WinFormsApp.Models.DTOs.Inventory;
using SIMS_WinFormsApp.Services.Interfaces;

namespace SIMS_WinFormsApp.Views.Interfaces
{
    public interface IStockDisposalListView
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
        event EventHandler ImportRequested;
        event Action<string> DisposalCodeCopyRequested;
        event Action<int> DetailRequested;

        string Keyword { get; }
        DateTime? FromDate { get; }
        DateTime? ToDate { get; }
        int PageSize { get; }
        IWin32Window Owner { get; }

        void DisplayPage(IReadOnlyList<StockDisposalListItemDto> rows, int totalCount, int pageNumber, int pageCount);
        void ClearToDate();
        void ClearDates();
        void CopyDisposalCode(string disposalCode);
        void ShowInvalidDateRange();
        void ShowError(string title, string message);
        void ShowSuccess(string message);
        bool OpenCreateForm();
        void OpenDetail(StockDisposalListItemDto disposal, IReadOnlyList<StockDisposalDetailLineDto> lines);
        bool OpenImportDialog(string pageTitle, string[] columns, string instructions, ImportRowHandler rowHandler);
    }
}
