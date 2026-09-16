using System.Globalization;

namespace SIMS_WinFormsApp.MVP.Presenters.Pos
{
    public static class PosFormat
    {
        public static string Vnd(decimal amount)
        {
            return amount.ToString("N0", CultureInfo.GetCultureInfo("vi-VN")) + " đ";
        }
    }
}
