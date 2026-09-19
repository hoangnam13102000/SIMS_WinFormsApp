using System.Globalization;

namespace SIMS_WinFormsApp.Services.Backup
{
    /// <summary>Định dạng dung lượng file kiểu Việt Nam (dấu phẩy thập phân) - dùng chung cho
    /// cả Presenter (thông báo) lẫn View (cột "Dung lượng") thay vì mỗi nơi tự ToString() riêng.</summary>
    public static class BackupFormat
    {
        private static readonly CultureInfo Vn = new CultureInfo("vi-VN");

        public static string Size(long bytes)
        {
            double kb = bytes / 1024.0;
            if (kb < 1024) return kb.ToString("N1", Vn) + " KB";
            return (kb / 1024.0).ToString("N1", Vn) + " MB";
        }
    }
}