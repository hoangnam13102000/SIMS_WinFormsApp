using System.Globalization;

namespace SIMS_WinFormsApp.Services.Implementations.Import
{
    public static class ImportColumnMapper
    {
        public static int[] BuildMapping(string[] expectedColumns, string[] fileHeader)
        {
            if (expectedColumns == null || expectedColumns.Length == 0 || fileHeader == null) return null;

            var normalizedFileHeader = new string[fileHeader.Length];
            for (int i = 0; i < fileHeader.Length; i++)
                normalizedFileHeader[i] = Normalize(fileHeader[i]);

            var mapping = new int[expectedColumns.Length];
            int matchedCount = 0;
            for (int i = 0; i < expectedColumns.Length; i++)
            {
                string target = Normalize(expectedColumns[i]);
                int foundAt = -1;
                for (int h = 0; h < normalizedFileHeader.Length; h++)
                {
                    if (normalizedFileHeader[h] == target) { foundAt = h; break; }
                }
                mapping[i] = foundAt;
                if (foundAt >= 0) matchedCount++;
            }

            return matchedCount == 0 ? null : mapping;
        }

        public static string[] ApplyMapping(int[] mapping, string[] rawCells)
        {
            var mapped = new string[mapping.Length];
            for (int i = 0; i < mapping.Length; i++)
            {
                int src = mapping[i];
                mapped[i] = (src >= 0 && src < rawCells.Length && rawCells[src] != null) ? rawCells[src] : string.Empty;
            }
            return mapped;
        }

        private static string Normalize(string raw)
        {
            if (string.IsNullOrEmpty(raw)) return string.Empty;
            string s = raw.Trim();
            int paren = s.IndexOf('(');
            if (paren >= 0) s = s.Substring(0, paren).Trim();
            return s.ToLower(CultureInfo.GetCultureInfo("vi-VN"));
        }
    }
}