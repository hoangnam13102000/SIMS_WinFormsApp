using System.Collections.Generic;
using System.IO;
using System.Text;
using SIMS_WinFormsApp.Services.Interfaces;

namespace SIMS_WinFormsApp.Services.Implementations.Export
{
    /// <summary>
    /// Xuất CSV dùng dấu ";" làm delimiter (Excel locale vi-VN mặc định dùng ";" thay vì ",")
    /// + BOM UTF-8 để Excel hiển thị đúng dấu tiếng Việt - cùng quy ước với TableExportUtil bên
    /// bản Java.
    /// </summary>
    public sealed class CsvTableExporter : ITableDataExporter
    {
        private const char Delimiter = ';';

        public string FileExtension => "csv";
        public string SaveDialogFilter => "CSV (*.csv)|*.csv";

        public void Export(string filePath, string sheetName, string[] headers, IReadOnlyList<object[]> rows)
        {
            using (var writer = new StreamWriter(filePath, false, new UTF8Encoding(true)))
            {
                writer.Write(ToCsvRow(headers));
                foreach (var row in rows)
                    writer.Write(ToCsvRow(row));
            }
        }

        private static string ToCsvRow(IEnumerable<object> values)
        {
            var sb = new StringBuilder();
            bool first = true;
            foreach (var value in values)
            {
                if (!first) sb.Append(Delimiter);
                first = false;
                sb.Append(Escape(value));
            }
            sb.Append("\r\n");
            return sb.ToString();
        }

        private static string Escape(object value)
        {
            string s = value?.ToString() ?? string.Empty;
            bool mustQuote = s.IndexOf(Delimiter) >= 0 || s.Contains("\"") || s.Contains("\n") || s.Contains("\r");
            if (mustQuote) s = "\"" + s.Replace("\"", "\"\"") + "\"";
            return s;
        }
    }
}