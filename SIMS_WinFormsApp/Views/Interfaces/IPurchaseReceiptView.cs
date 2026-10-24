using System;
using System.Collections.Generic;
using SIMS_WinFormsApp.Models.DTOs.Catalog;
using SIMS_WinFormsApp.Models.DTOs.Inventory;

namespace SIMS_WinFormsApp.Views.Interfaces
{
    public interface IPurchaseReceiptView
    {
        event EventHandler SaveRequested;
        event EventHandler LinesChanged;

        int SelectedSupplierId { get; }
        IReadOnlyList<PurchaseLineDto> ReadLines();
        void BindSuppliers(IReadOnlyList<LookupItem> items);
        void BindProducts(IReadOnlyList<LookupItem> items);
        void ShowTotal(decimal total);
        void ShowReceiptCode(string code);
        void ShowMessage(string message, bool isError);
        void ResetLines();
    }
}