using System;
using System.Collections.Generic;
using SIMS_WinFormsApp.Models.DTOs.Catalog;
using SIMS_WinFormsApp.Models.DTOs.Inventory;

namespace SIMS_WinFormsApp.Services.Interfaces
{
    public interface IStockReconciliationService
    {
        int PrepareToday(int userId);
        IReadOnlyList<StockReconciliationRowDto> Search(string keyword, DateTime? from, DateTime? to);
        CatalogSaveResult UpdateCount(int reconciliationId, int actualStock, string note, int userId);
    }
}