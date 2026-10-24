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
        IReadOnlyList<StockRowDto> GetStock(string keyword, int? categoryId);
        void SaveStockCounts(IEnumerable<StockCountDto> counts, int createdBy);
    }
}