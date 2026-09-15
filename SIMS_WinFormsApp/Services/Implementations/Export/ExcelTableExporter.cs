using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using SIMS_WinFormsApp.Services.Interfaces;

namespace SIMS_WinFormsApp.Services.Implementations.Export
{
    public sealed class ExcelTableExporter : ITableDataExporter
    {
        public string FileExtension => "xlsx";
        public string SaveDialogFilter => "Excel (*.xlsx)|*.xlsx";

        public void Export(string filePath, string sheetName, string[] headers, IReadOnlyList<object[]> rows)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add(SafeSheetName(sheetName));

                for (int column = 0; column < headers.Length; column++)
                    worksheet.Cells[1, column + 1].Value = headers[column];

                for (int row = 0; row < rows.Count; row++)
                {
                    for (int column = 0; column < rows[row].Length; column++)
                        worksheet.Cells[row + 2, column + 1].Value = rows[row][column];
                }

                package.SaveAs(new FileInfo(filePath));
            }
        }

        private static string SafeSheetName(string name)
        {
            string cleaned = name ?? "Sheet1";
            foreach (char invalidCharacter in new[] { '\\', '/', '*', '?', ':', '[', ']' })
                cleaned = cleaned.Replace(invalidCharacter.ToString(), string.Empty);
            cleaned = cleaned.Trim();
            if (cleaned.Length == 0) cleaned = "Sheet1";
            return cleaned.Length > 31 ? cleaned.Substring(0, 31) : cleaned;
        }
    }
}