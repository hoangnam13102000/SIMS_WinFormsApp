using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_WinFormsApp.UI.I18n
{
    public static class Lang
    {
        private static Dictionary<string, string> _bundle;
        private static CultureInfo _currentCulture;

        static Lang()
        {
            SetLocale(new CultureInfo("vi"));
        }

        public static void SetLocale(CultureInfo culture)
        {
            _currentCulture = culture;
            string lang = culture.TwoLetterISOLanguageName.Equals("en", StringComparison.OrdinalIgnoreCase) ? "en" : "vi";
            _bundle = LoadBundle(lang);
        }

        public static CultureInfo CurrentCulture => _currentCulture;

        public static string Get(string key)
        {
            if (_bundle != null && _bundle.TryGetValue(key, out var value))
                return value;
            return "!!" + key + "!!";
        }

        /// <summary>Lấy chuỗi đã dịch và thay thế tham số kiểu {0}, {1}... (string.Format).</summary>
        public static string Get(string key, params object[] args)
        {
            var pattern = Get(key);
            if (pattern.StartsWith("!!")) return pattern;
            return string.Format(pattern, args);
        }

        private static Dictionary<string, string> LoadBundle(string lang)
        {
            var result = new Dictionary<string, string>();
            var assembly = Assembly.GetExecutingAssembly();

            string resourceName = assembly.GetManifestResourceNames()
                .FirstOrDefault(n => n.EndsWith($"messages_{lang}.properties", StringComparison.OrdinalIgnoreCase));

            if (resourceName == null) return result;

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null) return result;
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var trimmed = line.Trim();
                        if (trimmed.Length == 0 || trimmed.StartsWith("#")) continue;

                        int idx = line.IndexOf('=');
                        if (idx <= 0) continue;

                        var key = line.Substring(0, idx).Trim();
                        var value = line.Substring(idx + 1).Trim()
                            .Replace("\\n", Environment.NewLine)
                            .Replace("\\r", "\r")
                            .Replace("\\t", "\t");
                        result[key] = value;
                    }
                }
            }

            return result;
        }
    }
}
