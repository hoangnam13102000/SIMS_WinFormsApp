using System;
using System.Collections.Generic;
using SIMS_WinFormsApp.Models.DTOs.Catalog;
using SIMS_WinFormsApp.Models.DTOs.Inventory;

namespace SIMS_WinFormsApp.Repositories.Interfaces
{
    public interface IInventoryRepository
    {
        IReadOnlyList<LookupItem> GetActiveSuppliers();
        IReadOnlyList<LookupItem> GetActiveProducts();
        IReadOnlyList<LookupItem> GetCategories();
        int CreatePurchaseReceipt(PurchaseReceiptDto receipt, out string receiptCode);
        IReadOnlyList<PurchaseReceiptListItemDto> SearchPurchaseReceipts(string keyword, DateTime? fromDate, DateTime? toDate);
        IReadOnlyList<PurchaseReceiptDetailLineDto> GetPurchaseReceiptDetails(int receiptId);
        IReadOnlyList<string> GetPurchaseReceiptSuggestions();
        IReadOnlyList<StockRowDto> GetStock(string keyword, int? categoryId);
        IReadOnlyList<StockAlertListItemDto> SearchStockAlerts(
            string keyword, int? categoryId, DateTime? from, DateTime? to);
        int MarkAlertPlanned(int alertId);
        int ResolveAlert(int alertId, int userId);
        int MarkAllAlertsSeen();
        void SaveStockCounts(IEnumerable<StockCountDto> counts, int createdBy);
    }
}