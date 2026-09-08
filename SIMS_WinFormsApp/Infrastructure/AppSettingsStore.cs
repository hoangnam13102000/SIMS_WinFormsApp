using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_WinFormsApp.Infrastructure
{
    public static class AppSettingsStore
    {
        private static readonly string FilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "SIMS", "settings.ini");

        public static string Get(string key, string defaultValue = null)
        {
            var all = ReadAll();
            return all.TryGetValue(key, out var value) ? value : defaultValue;
        }

        public static void Set(string key, string value)
        {
            var all = ReadAll();
            all[key] = value;
            WriteAll(all);
        }

        private static Dictionary<string, string> ReadAll()
        {
            var result = new Dictionary<string, string>();
            if (!File.Exists(FilePath)) return result;

            try
            {
                foreach (var line in File.ReadAllLines(FilePath))
                {
                    if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#")) continue;
                    int idx = line.IndexOf('=');
                    if (idx <= 0) continue;
                    result[line.Substring(0, idx).Trim()] = line.Substring(idx + 1).Trim();
                }
            }
            catch (IOException)
            {
                // Giữ nguyên hành vi mềm dẻo: nếu đọc lỗi thì coi như chưa có setting nào lưu.
            }

            return result;
        }

        private static void WriteAll(Dictionary<string, string> values)
        {
            try
            {
                var dir = Path.GetDirectoryName(FilePath);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                var lines = new List<string>();
                foreach (var kv in values)
                    lines.Add(kv.Key + "=" + kv.Value);

                File.WriteAllLines(FilePath, lines);
            }
            catch (IOException)
            {
                // Không crash app nếu không ghi được setting (vd ổ đĩa full, quyền truy cập...).
            }
        }
    }
}
