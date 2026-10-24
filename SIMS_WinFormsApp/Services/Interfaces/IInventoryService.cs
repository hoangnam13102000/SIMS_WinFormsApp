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
        CatalogSaveResult CreatePurchaseReceipt(PurchaseReceiptDto receipt);
        CatalogSaveResult SaveStockCounts(IReadOnlyList<StockCountDto> counts, int createdBy);
    }
}