using System.Collections.Generic;

namespace SIMS_WinFormsApp.Services.Interfaces
{

    public interface ITableDataImporter
    {
        
        bool CanHandle(string filePath);


        IReadOnlyList<string[]> ReadRows(string filePath);
    }
}