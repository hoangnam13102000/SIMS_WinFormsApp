using System.Collections.Generic;

namespace SIMS_WinFormsApp.Services.Interfaces
{
    public interface ITableDataExporter
    {
        string FileExtension { get; }

        string SaveDialogFilter { get; }

        void Export(string filePath, string sheetName, string[] headers, IReadOnlyList<object[]> rows);
    }
}