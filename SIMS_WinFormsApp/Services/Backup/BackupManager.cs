using System;
using System.Collections.Generic;
using System.IO;

namespace SIMS_WinFormsApp.Services.Backup
{
    public sealed class BackupManager
    {
        private enum BackupMode { Manual, Scheduled, Emergency }

        private readonly BackupStorage _storage;
        private readonly List<IBackupStrategy> _strategies;
        private readonly List<IBackupListener> _listeners = new List<IBackupListener>();

        public BackupManager(BackupStorage storage, IReadOnlyList<IBackupStrategy> strategiesInOrder)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            if (strategiesInOrder == null || strategiesInOrder.Count == 0)
                throw new ArgumentException("Cần ít nhất 1 IBackupStrategy.", nameof(strategiesInOrder));
            _strategies = new List<IBackupStrategy>(strategiesInOrder);
        }

        public BackupStorage Storage => _storage;

        public void AddListener(IBackupListener listener)
        {
            if (listener != null) _listeners.Add(listener);
        }

        // ================================================================
        // 1) NGƯỜI DÙNG BẤM "SAO LƯU NGAY" -> tên có timestamp -> mỗi lần 1 file riêng
        // ================================================================
        public BackupResult BackupNow() => DoBackup(BackupMode.Manual);

        // ================================================================
        // 2) TỰ ĐỘNG (nếu có lịch chạy nền gọi hàm này mỗi ngày) -> 1 ngày 1 file, bỏ qua nếu đã có
        // ================================================================
        public BackupResult BackupIfNotDoneToday() =>
            _storage.HasDailyBackupToday() ? null : DoBackup(BackupMode.Scheduled);

        // ================================================================
        // 3) KHẨN CẤP (hệ thống gặp sự cố, cần lưu trạng thái cuối ngay) -> tên có timestamp
        // ================================================================
        public BackupResult BackupEmergency() => DoBackup(BackupMode.Emergency);

        private BackupResult DoBackup(BackupMode mode)
        {
            Exception lastError = null;

            foreach (var strategy in _strategies)
            {
                string destination = mode == BackupMode.Scheduled
                    ? _storage.NewDailyBackupFile(strategy.Name, strategy.FileExtension)
                    : _storage.NewTimestampedBackupFile(strategy.Name, strategy.FileExtension);

                if (mode == BackupMode.Scheduled && File.Exists(destination))
                {
                    try { File.Delete(destination); } catch { /* ghi đè lại ngay phía dưới */ }
                }

                string suffix = mode == BackupMode.Manual ? " (thủ công)"
                    : mode == BackupMode.Emergency ? " (khẩn cấp)" : string.Empty;
                NotifyStarted(strategy.Name + suffix);

                DateTime startedAt = DateTime.Now;
                try
                {
                    strategy.BackupTo(destination);

                    if (!File.Exists(destination) || new FileInfo(destination).Length == 0)
                    {
                        if (File.Exists(destination)) File.Delete(destination);
                        throw new BackupException("Strategy " + strategy.Name + " ghi ra file rỗng (0 byte).");
                    }

                    var result = new BackupResult(destination, strategy.Name, startedAt, DateTime.Now);
                    NotifySucceeded(result);
                    return result;
                }
                catch (Exception ex)
                {
                    lastError = ex;
                    NotifyFailed(strategy.Name, ex);
                    if (File.Exists(destination)) { try { File.Delete(destination); } catch { /* bỏ qua */ } }
                }
            }

            throw new BackupException(
                "Tất cả " + _strategies.Count + " backup strategy đều thất bại. Lỗi cuối cùng: " +
                (lastError?.Message ?? "không rõ"), lastError);
        }

        /// <summary>Khôi phục từ 1 file backup cụ thể - tự nhận diện strategy phù hợp theo quy
        /// ước đặt tên file (chứa "_&lt;strategy&gt;." trước phần mở rộng).</summary>
        public void Restore(string backupFilePath)
        {
            string fileName = Path.GetFileName(backupFilePath);

            foreach (var strategy in _strategies)
            {
                if (!(strategy is IRestoreStrategy restoreStrategy)) continue;
                if (fileName.IndexOf("_" + strategy.Name + ".", StringComparison.OrdinalIgnoreCase) < 0) continue;

                NotifyRestoreStarted(strategy.Name, backupFilePath);
                try
                {
                    restoreStrategy.RestoreFrom(backupFilePath);
                    NotifyRestoreSucceeded(strategy.Name, backupFilePath);
                }
                catch (Exception ex)
                {
                    NotifyRestoreFailed(strategy.Name, backupFilePath, ex);
                    throw ex is BackupException ? ex : new BackupException("Khôi phục thất bại: " + ex.Message, ex);
                }
                return;
            }

            throw new BackupException("Không xác định được strategy phù hợp để khôi phục file: " + fileName);
        }

        public void RestoreLatest()
        {
            string latest = _storage.GetLatestBackup();
            if (latest == null)
                throw new BackupException("Không có bản backup nào trong " + _storage.DirectoryPath);
            Restore(latest);
        }

        private void NotifyStarted(string s) { foreach (var l in _listeners) SafeInvoke(() => l.OnBackupStarted(s)); }
        private void NotifySucceeded(BackupResult r) { foreach (var l in _listeners) SafeInvoke(() => l.OnBackupSucceeded(r)); }
        private void NotifyFailed(string s, Exception e) { foreach (var l in _listeners) SafeInvoke(() => l.OnBackupFailed(s, e)); }
        private void NotifyRestoreStarted(string s, string f) { foreach (var l in _listeners) SafeInvoke(() => l.OnRestoreStarted(s, f)); }
        private void NotifyRestoreSucceeded(string s, string f) { foreach (var l in _listeners) SafeInvoke(() => l.OnRestoreSucceeded(s, f)); }
        private void NotifyRestoreFailed(string s, string f, Exception e) { foreach (var l in _listeners) SafeInvoke(() => l.OnRestoreFailed(s, f, e)); }

        private static void SafeInvoke(Action action)
        {
            try { action(); }
            catch { /* 1 listener lỗi không được làm hỏng các listener khác */ }
        }
    }
}