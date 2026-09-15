using System;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using SIMS_WinFormsApp.Services.Interfaces;

namespace SIMS_WinFormsApp.Services.Implementations.Import
{
    public sealed class ExcelTableImporter : ITableDataImporter
    {
        public bool CanHandle(string filePath) =>
            !string.IsNullOrEmpty(filePath) && filePath.ToLowerInvariant().EndsWith(".xlsx");

        public IReadOnlyList<string[]> ReadRows(string filePath)
        {
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[1];
                if (worksheet == null || worksheet.Dimension == null)
                    throw new InvalidDataException("File Excel không có dữ liệu.");

                int rowCount = worksheet.Dimension.End.Row;
                int columnCount = worksheet.Dimension.End.Column;
                var rows = new List<string[]>(rowCount);

                for (int row = 1; row <= rowCount; row++)
                {
                    var values = new string[columnCount];
                    for (int column = 1; column <= columnCount; column++)
                        values[column - 1] = worksheet.Cells[row, column].Text ?? string.Empty;
                    rows.Add(values);
                }

                return rows;
            }
        }
    }
}