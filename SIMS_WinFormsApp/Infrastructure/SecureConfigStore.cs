using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SIMS_WinFormsApp.Infrastructure
{
    public static class SecureConfigStore
    {
        private static readonly string FilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "SIMS", "secure-config.dat");

        public static string Get(string key, string defaultValue = null)
        {
            var all = ReadAll();
            return all.TryGetValue(key, out var value) ? value : defaultValue;
        }

        public static bool Has(string key)
        {
            var all = ReadAll();
            return all.ContainsKey(key) && !string.IsNullOrWhiteSpace(all[key]);
        }

        public static void Set(string key, string value)
        {
            var all = ReadAll();
            all[key] = value ?? string.Empty;
            WriteAll(all);
        }

        private static Dictionary<string, string> ReadAll()
        {
            var result = new Dictionary<string, string>();
            if (!File.Exists(FilePath)) return result;

            try
            {
                byte[] encrypted = File.ReadAllBytes(FilePath);
                byte[] plainBytes = ProtectedData.Unprotect(encrypted, null, DataProtectionScope.CurrentUser);
                string plainText = Encoding.UTF8.GetString(plainBytes);

                foreach (var line in plainText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    int idx = line.IndexOf('=');
                    if (idx <= 0) continue;
                    result[line.Substring(0, idx).Trim()] = line.Substring(idx + 1).Trim();
                }
            }
            catch
            {
                // File hỏng / đổi user Windows / máy khác -> coi như chưa cấu hình,
                // không throw để tránh crash app khi khởi động.
            }

            return result;
        }

        private static void WriteAll(Dictionary<string, string> values)
        {
            var dir = Path.GetDirectoryName(FilePath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var sb = new StringBuilder();
            foreach (var kv in values)
                sb.Append(kv.Key).Append('=').Append(kv.Value).Append('\n');

            byte[] plainBytes = Encoding.UTF8.GetBytes(sb.ToString());
            byte[] encrypted = ProtectedData.Protect(plainBytes, null, DataProtectionScope.CurrentUser);
            File.WriteAllBytes(FilePath, encrypted);
        }
    }
}