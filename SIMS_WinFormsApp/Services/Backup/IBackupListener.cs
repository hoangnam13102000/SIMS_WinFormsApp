using System;

namespace SIMS_WinFormsApp.Services.Backup
{
    /// <summary>Observer nhận thông báo các sự kiện vòng đời sao lưu/khôi phục (Observer
    /// pattern) - tương đương BackupListener.java. Cho phép gắn thêm hành vi (ví dụ tự động tải
    /// lên Cloudinary) mà không phải sửa BackupManager (Open/Closed).</summary>
    public interface IBackupListener
    {
        void OnBackupStarted(string strategyName);
        void OnBackupSucceeded(BackupResult result);
        void OnBackupFailed(string strategyName, Exception error);
        void OnRestoreStarted(string strategyName, string filePath);
        void OnRestoreSucceeded(string strategyName, string filePath);
        void OnRestoreFailed(string strategyName, string filePath, Exception error);
    }

    /// <summary>Lớp cơ sở no-op cho IBackupListener (tương đương các phương thức "default {}"
    /// rỗng bên interface Java) - lớp con chỉ cần override đúng sự kiện mình quan tâm.</summary>
    public abstract class BackupListenerBase : IBackupListener
    {
        public virtual void OnBackupStarted(string strategyName) { }
        public virtual void OnBackupSucceeded(BackupResult result) { }
        public virtual void OnBackupFailed(string strategyName, Exception error) { }
        public virtual void OnRestoreStarted(string strategyName, string filePath) { }
        public virtual void OnRestoreSucceeded(string strategyName, string filePath) { }
        public virtual void OnRestoreFailed(string strategyName, string filePath, Exception error) { }
    }
}