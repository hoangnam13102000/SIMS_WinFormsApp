using System;
using System.Collections.Generic;
using SIMS_WinFormsApp.Models.DTOs.Inventory;

namespace SIMS_WinFormsApp.Views.Interfaces
{
    public interface IStockDisposalEditorView
    {
        event EventHandler LinesChanged;
        event EventHandler SaveRequested;

        string SelectedReason { get; }
        string Note { get; }

        IReadOnlyList<StockDisposalLineDto> ReadLines();
        void BindBatches(IReadOnlyList<DisposableBatchDto> batches);
        void ShowTotal(decimal total);
        bool ConfirmSave(int lineCount, decimal total);
        void ShowMessage(string message, bool isError);
        void CloseAfterSave(string message);
    }
}
