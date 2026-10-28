using System;
using System.Collections.Generic;
using SIMS_WinFormsApp.Models.DTOs.Catalog;
using SIMS_WinFormsApp.Models.DTOs.Inventory;

namespace SIMS_WinFormsApp.Views.Interfaces
{
    public interface IStockReconciliationView
    {
        event EventHandler LoadRequested;
        event EventHandler SaveRequested;

        string Keyword { get; }
        int? SelectedCategoryId { get; }

        void BindCategories(IReadOnlyList<LookupItem> items);
        void BindStock(IReadOnlyList<StockRowDto> rows);
        IReadOnlyList<StockCountDto> ReadCounts();
        bool Confirm(string message);
        void ShowMessage(string message, bool isError);
    }
}