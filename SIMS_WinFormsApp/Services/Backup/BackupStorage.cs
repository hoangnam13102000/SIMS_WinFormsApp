using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SIMS_WinFormsApp.Services.Backup
{
    /// <summary>Quản lý thư mục chứa file backup: đặt tên file, liệt kê, dọn dẹp - tương đương
    /// BackupStorage.java. Không biết gì về cách 1 file backup được TẠO RA (việc đó là của
    /// IBackupStrategy) - tuân thủ Single Responsibility.</summary>
    public sealed class BackupStorage
    {
        private const string CloudinarySidecarSuffix = ".cloudinary.url";

        public string DirectoryPath { get; }

        public BackupStorage(string directoryPath)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
                throw new ArgumentException("Thiếu đường dẫn thư mục backup.", nameof(directoryPath));

            DirectoryPath = directoryPath;
            if (!Directory.Exists(DirectoryPath)) Directory.CreateDirectory(DirectoryPath);
        }

        /// <summary>Tên file cho SCHEDULER/TỰ ĐỘNG: chỉ có ngày -> cùng ngày = cùng tên = ghi đè,
        /// đảm bảo 1 ngày 1 file. backup_20260918_sqlservernative.bak</summary>
        public string NewDailyBackupFile(string strategyName, string extension)
        {
            string datePart = DateTime.Now.ToString("yyyyMMdd");
            return Path.Combine(DirectoryPath, "backup_" + datePart + "_" + strategyName + "." + extension);
        }

        /// <summary>Tên file cho THỦ CÔNG/KHẨN CẤP: có giờ:phút:giây -> mỗi lần gọi = 1 file
        /// riêng, không ghi đè lẫn nhau. backup_20260918-164608_sqlservernative.bak</summary>
        public string NewTimestampedBackupFile(string strategyName, string extension)
        {
            string ts = DateTime.Now.ToString("yyyyMMdd-HHmmss");
            return Path.Combine(DirectoryPath, "backup_" + ts + "_" + strategyName + "." + extension);
        }

        private static bool IsRealBackupFile(string fileName) =>
            fileName.StartsWith("backup_", StringComparison.OrdinalIgnoreCase) &&
            !fileName.EndsWith(CloudinarySidecarSuffix, StringComparison.OrdinalIgnoreCase);

        /// <summary>Hôm nay đã có bản backup TỰ ĐỘNG (định dạng theo ngày) nào thành công chưa.</summary>
        public bool HasDailyBackupToday()
        {
            string prefix = "backup_" + DateTime.Now.ToString("yyyyMMdd") + "_";
            return Directory.EnumerateFiles(DirectoryPath)
                .Select(Path.GetFileName)
                .Where(name => name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase) && IsRealBackupFile(name))
                .Any(name => new FileInfo(Path.Combine(DirectoryPath, name)).Length > 0);
        }

        public string GetLatestBackup() => ListBackups().FirstOrDefault();

        /// <summary>Liệt kê TẤT CẢ file backup (cả tự động lẫn thủ công), mới nhất lên đầu.</summary>
        public List<string> ListBackups()
        {
            if (!Directory.Exists(DirectoryPath)) return new List<string>();
            return Directory.EnumerateFiles(DirectoryPath)
                .Where(path => IsRealBackupFile(Path.GetFileName(path)))
                .OrderByDescending(File.GetLastWriteTime)
                .ToList();
        }

        /// <summary>Xóa file cũ, giữ lại <paramref name="keepCount"/> bản mới nhất.</summary>
        public int CleanupOldBackups(int keepCount)
        {
            var all = ListBackups();
            int deleted = 0;
            for (int i = keepCount; i < all.Count; i++)
            {
                try { File.Delete(all[i]); deleted++; }
                catch { /* 1 file xóa lỗi không được chặn việc dọn các file còn lại */ }
            }
            return deleted;
        }

        public long TotalSizeBytes() => ListBackups().Sum(f => new FileInfo(f).Length);

        public void Delete(string backupFilePath)
        {
            string parent = Path.GetDirectoryName(backupFilePath) ?? string.Empty;
            if (!string.Equals(Path.GetFullPath(parent), Path.GetFullPath(DirectoryPath), StringComparison.OrdinalIgnoreCase))
                throw new IOException("File không thuộc thư mục backup do storage này quản lý: " + backupFilePath);

            if (File.Exists(backupFilePath)) File.Delete(backupFilePath);
        }
    }
}