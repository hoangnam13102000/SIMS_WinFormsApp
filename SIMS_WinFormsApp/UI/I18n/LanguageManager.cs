using SIMS_WinFormsApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_WinFormsApp.UI.I18n
{
    public sealed class LanguageManager
    {
        private const string PrefKey = "sims.language";

        private static readonly Lazy<LanguageManager> LazyInstance = new Lazy<LanguageManager>(() => new LanguageManager());
        public static LanguageManager Instance => LazyInstance.Value;

        public event EventHandler LanguageChanged;

        public CultureInfo CurrentCulture { get; private set; }

        private LanguageManager()
        {
            var saved = AppSettingsStore.Get(PrefKey, "vi");
            CurrentCulture = saved == "en" ? new CultureInfo("en") : new CultureInfo("vi");
            Lang.SetLocale(CurrentCulture);
        }

        public bool IsEnglish => CurrentCulture.TwoLetterISOLanguageName.Equals("en", StringComparison.OrdinalIgnoreCase);
        public bool IsVietnamese => !IsEnglish;

        public void Toggle() => SetLocale(IsEnglish ? new CultureInfo("vi") : new CultureInfo("en"));

        public void SetLocale(CultureInfo culture)
        {
            if (culture.TwoLetterISOLanguageName.Equals(CurrentCulture.TwoLetterISOLanguageName, StringComparison.OrdinalIgnoreCase))
                return;

            CurrentCulture = culture;
            AppSettingsStore.Set(PrefKey, IsEnglish ? "en" : "vi");
            Lang.SetLocale(culture);

            LanguageChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
