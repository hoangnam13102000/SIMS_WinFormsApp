using System;
using System.Collections.Generic;

namespace SIMS_WinFormsApp.Views.Interfaces
{
    public interface IImportDataView
    {
        void SetChooseFileEnabled(bool enabled);
        void SetSelectedFileLabel(string text);
        void SetStartEnabled(bool enabled);
        void SetBusy(bool isBusy, string message);
        void ShowResult(int successCount, IReadOnlyList<string> errors);
        void ShowValidationError(string title, string message);

        
        event EventHandler<string> FileChosen;
        event EventHandler StartImportRequested;
    }
}