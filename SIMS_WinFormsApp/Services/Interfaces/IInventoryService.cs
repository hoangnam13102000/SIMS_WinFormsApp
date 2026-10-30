using System;
using System.Collections.Generic;
using SIMS_WinFormsApp.Models.DTOs.Catalog;
using SIMS_WinFormsApp.Models.DTOs.Inventory;

namespace SIMS_WinFormsApp.Services.Interfaces
{
    public interface IInventoryService
    {
        IReadOnlyList<LookupItem> GetActiveSuppliers();
        IReadOnlyList<LookupItem> GetActiveProducts();
        IReadOnlyList<LookupItem> GetCategories();
        IReadOnlyList<StockRowDto> GetStock(string keyword, int? categoryId);
        IReadOnlyList<PurchaseReceiptListItemDto> SearchPurchaseReceipts(string keyword, DateTime? fromDate, DateTime? toDate);
        IReadOnlyList<PurchaseReceiptDetailLineDto> GetPurchaseReceiptDetails(int receiptId);
        IReadOnlyList<string> GetPurchaseReceiptSuggestions();
        IReadOnlyList<StockAlertListItemDto> SearchStockAlerts(
            string keyword, int? categoryId, DateTime? from, DateTime? to);
        int MarkAlertPlanned(int alertId);
        int ResolveAlert(int alertId, int userId);
        int MarkAllAlertsSeen();
        CatalogSaveResult CreatePurchaseReceipt(PurchaseReceiptDto receipt);
        CatalogSaveResult SaveStockCounts(IReadOnlyList<StockCountDto> counts, int createdBy);
    }
}