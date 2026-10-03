using System;
using System.Collections.Generic;
using System.IO;
using SIMS_WinFormsApp.Infrastructure;

namespace SIMS_WinFormsApp.Infrastructure.Security
{
    public static class AppConfig
    {
        private const string EncryptedFileName = "secure-config.enc";
        private static readonly Lazy<Dictionary<string, string>> EncryptedValues =
            new Lazy<Dictionary<string, string>>(LoadEncryptedValues);

        public static string Get(string key, string defaultValue = null)
        {
            if (string.IsNullOrWhiteSpace(key)) return defaultValue;

            if (EncryptedValues.Value.TryGetValue(key, out string value)
                && !string.IsNullOrWhiteSpace(value))
                return value.Trim();

            value = Environment.GetEnvironmentVariable(key);
            if (!string.IsNullOrWhiteSpace(value)) return value.Trim();

            value = SecureConfigStore.Get(key);
            return string.IsNullOrWhiteSpace(value) ? defaultValue : value.Trim();
        }

        private static Dictionary<string, string> LoadEncryptedValues()
        {
            var values = new Dictionary<string, string>(StringComparer.Ordinal);
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, EncryptedFileName);
            if (!File.Exists(filePath)) return values;

            try
            {
                byte[] encryptedBytes = File.ReadAllBytes(filePath);
                string plainText = SecureConfigFile.Decrypt(encryptedBytes);
                ParseValues(plainText, values);
                return values;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "Không thể giải mã secure-config.enc cho tài khoản Windows hiện tại. "
                    + "Hãy chạy lại SecureConfigGenerator bằng đúng tài khoản.", ex);
            }
        }

        private static void ParseValues(string plainText, IDictionary<string, string> values)
        {
            foreach (string line in plainText.Split(
                new[] { '\r', '\n' },
                StringSplitOptions.RemoveEmptyEntries))
            {
                string trimmed = line.Trim();
                if (trimmed.Length == 0 || trimmed.StartsWith("#", StringComparison.Ordinal)) continue;

                int separator = trimmed.IndexOf('=');
                if (separator <= 0) continue;

                values[trimmed.Substring(0, separator).Trim()] =
                    trimmed.Substring(separator + 1).Trim();
            }
        }
    }
}
