using System;
using System.Collections.Generic;
using SIMS_WinFormsApp.Models.DTOs.Inventory;

namespace SIMS_WinFormsApp.Repositories.Interfaces
{
    public interface IStockReconciliationRepository
    {
        int CreateTodaySession(int createdBy);
        int SyncTodayWithStock();
        IReadOnlyList<StockReconciliationRowDto> Search(string keyword, DateTime? from, DateTime? to);
        StockCountUpdateStatus UpdateCount(int reconciliationId, int actualStock, string note, int userId);
    }
}