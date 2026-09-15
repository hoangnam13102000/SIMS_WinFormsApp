using System.Collections.Generic;
using System.IO;
using System.Text;
using SIMS_WinFormsApp.Services.Interfaces;

namespace SIMS_WinFormsApp.Services.Implementations.Import
{
    public sealed class CsvTableImporter : ITableDataImporter
    {
        public bool CanHandle(string filePath) =>
            !string.IsNullOrEmpty(filePath) && filePath.ToLowerInvariant().EndsWith(".csv");

        public IReadOnlyList<string[]> ReadRows(string filePath)
        {
            var lines = ReadAllLinesHandlingBom(filePath);
            if (lines.Count == 0) return new List<string[]>();

            char delimiter = DetectDelimiter(lines[0]);
            var rows = new List<string[]>(lines.Count);
            foreach (var line in lines)
                rows.Add(ParseLine(line, delimiter));
            return rows;
        }

        private static char DetectDelimiter(string headerLine)
        {
            int semicolons = CountChar(headerLine, ';');
            int commas = CountChar(headerLine, ',');
            return commas > semicolons ? ',' : ';';
        }

        private static int CountChar(string s, char c)
        {
            int count = 0;
            foreach (char ch in s) if (ch == c) count++;
            return count;
        }

        private static List<string> ReadAllLinesHandlingBom(string filePath)
        {
            var result = new List<string>();
            using (var reader = new StreamReader(filePath, Encoding.UTF8, true))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                    if (line.Length > 0) result.Add(line);
            }
            return result;
        }

        /// <summary>Parser CSV cơ bản, hỗ trợ dấu nháy kép bao quanh + escape "" -> ".</summary>
        private static string[] ParseLine(string line, char delimiter)
        {
            var cells = new List<string>();
            var current = new StringBuilder();
            bool inQuotes = false;

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (inQuotes)
                {
                    if (c == '"')
                    {
                        if (i + 1 < line.Length && line[i + 1] == '"') { current.Append('"'); i++; }
                        else inQuotes = false;
                    }
                    else current.Append(c);
                }
                else
                {
                    if (c == '"') inQuotes = true;
                    else if (c == delimiter) { cells.Add(current.ToString()); current.Clear(); }
                    else current.Append(c);
                }
            }
            cells.Add(current.ToString());
            return cells.ToArray();
        }
    }
}