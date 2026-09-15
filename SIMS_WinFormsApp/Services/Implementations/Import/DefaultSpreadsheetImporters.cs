using System.Collections.Generic;
using SIMS_WinFormsApp.Services.Interfaces;

namespace SIMS_WinFormsApp.Services.Implementations.Import
{
    public static class DefaultSpreadsheetImporters
    {
        public static IReadOnlyList<ITableDataImporter> All { get; } = new ITableDataImporter[]
        {
            new CsvTableImporter(),
            new ExcelTableImporter()
        };

        public static string CombinedSaveOrOpenFilter => "Excel / CSV (*.xlsx;*.csv)|*.xlsx;*.csv";
    }
}