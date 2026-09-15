using System.Text.RegularExpressions;

namespace SIMS_WinFormsApp.Services.Validation
{
    public static class InputValidators
    {
        private static readonly Regex EmailPattern =
            new Regex(@"^[\w.+-]+@[\w-]+\.[a-zA-Z]{2,}$", RegexOptions.Compiled);
        private static readonly Regex PhonePattern =
            new Regex(@"^0\d{9,10}$", RegexOptions.Compiled);
        private static readonly Regex OtpPattern =
            new Regex(@"^\d{6}$", RegexOptions.Compiled);

        public static bool IsValidEmail(string value) =>
            value != null && EmailPattern.IsMatch(value);

        public static bool IsValidPhone(string value) =>
            value != null && PhonePattern.IsMatch(value);

        public static bool IsValidOtp(string value) =>
            value != null && OtpPattern.IsMatch(value);
    }
}
