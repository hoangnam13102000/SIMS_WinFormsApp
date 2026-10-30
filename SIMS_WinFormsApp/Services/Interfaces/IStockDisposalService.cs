using System;
using System.Collections.Generic;
using SIMS_WinFormsApp.Models.DTOs.Catalog;
using SIMS_WinFormsApp.Models.DTOs.Inventory;

namespace SIMS_WinFormsApp.Services.Interfaces
{
    public interface IStockDisposalService
    {
        IReadOnlyList<StockDisposalListItemDto> SearchDisposals(string keyword, DateTime? fromDate, DateTime? toDate);
        IReadOnlyList<StockDisposalDetailLineDto> GetDisposalDetails(int disposalId);
        IReadOnlyList<DisposableBatchDto> GetDisposableBatches();
        CatalogSaveResult CreateDisposal(StockDisposalDto disposal);
    }
}
