using System;
using System.Collections.Generic;
using SIMS_WinFormsApp.Models.DTOs.Inventory;

namespace SIMS_WinFormsApp.Repositories.Interfaces
{
    public interface IStockDisposalRepository
    {
        IReadOnlyList<StockDisposalListItemDto> SearchDisposals(
            string keyword, string reasonCodeList, DateTime? fromDate, DateTime? toDate);
        IReadOnlyList<StockDisposalDetailLineDto> GetDisposalDetails(int disposalId);
        IReadOnlyList<DisposableBatchDto> GetDisposableBatches();
        int CreateDisposal(StockDisposalDto disposal, out string disposalCode);
    }
}
