using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_WinFormsApp.Services.Backup
{
    /// <summary>
    /// IBackupListener tự động tải file backup vừa tạo LOCAL thành công lên Cloudinary, để có
    /// thêm 1 bản sao NGOÀI máy (yêu cầu "ngoài sao lưu tại local hệ thống thì phải sao lưu bên
    /// thứ 3 Cloudinary") - tương đương CloudinaryBackupUploader.java. Chạy bất đồng bộ, không
    /// chặn luồng backup local.
    /// <para>
    /// Nếu upload lỗi (mất mạng, thiếu cấu hình...), bản backup LOCAL vẫn coi là thành công -
    /// lỗi upload chỉ báo qua sự kiện <see cref="UploadFinished"/>, không ném ngược lại
    /// BackupManager, vì mất mạng không nên làm hỏng 1 bản backup local đã chạy xong.
    /// </para>
    /// <para>
    /// Link secure_url được lưu vào file sidecar "&lt;tên file backup&gt;.cloudinary.url" cạnh
    /// file gốc, để BackupRecoveryPresenter đọc lại và hiển thị cột "Cloud" mà không cần gọi lại
    /// Cloudinary mỗi lần load trang.
    /// </para>
    /// </summary>
    public sealed class CloudinaryBackupUploadListener : BackupListenerBase
    {
        private const string SidecarSuffix = ".cloudinary.url";

        private readonly ICloudinaryUploader _uploader;

        /// <summary>Bắn ra khi 1 lần upload Cloudinary (thành công hoặc thất bại) đã xong, để UI tự làm mới bảng.</summary>
        public event EventHandler<CloudUploadFinishedEventArgs> UploadFinished;

        public CloudinaryBackupUploadListener(ICloudinaryUploader uploader)
        {
            _uploader = uploader ?? throw new ArgumentNullException(nameof(uploader));
        }

        public override void OnBackupSucceeded(BackupResult result)
        {
            if (result?.FilePath == null || !File.Exists(result.FilePath)) return;
            _ = UploadInBackgroundAsync(result.FilePath);
        }

        private async Task UploadInBackgroundAsync(string filePath)
        {
            bool success = false;
            string errorMessage = null;
            try
            {
                string secureUrl = await _uploader.UploadBackupFileAsync(filePath).ConfigureAwait(false);
                WriteSidecarUrl(filePath, secureUrl);
                success = true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            finally
            {
                UploadFinished?.Invoke(this, new CloudUploadFinishedEventArgs(filePath, success, errorMessage));
            }
        }

        private static void WriteSidecarUrl(string backupFilePath, string secureUrl)
        {
            File.WriteAllText(SidecarFileFor(backupFilePath), secureUrl, Encoding.UTF8);
        }

        public static string SidecarFileFor(string backupFilePath) => backupFilePath + SidecarSuffix;

        /// <summary>Đọc link Cloudinary đã lưu cho 1 file backup - null nếu chưa từng upload thành công.</summary>
        public static string ReadUploadedUrl(string backupFilePath)
        {
            string sidecar = SidecarFileFor(backupFilePath);
            if (!File.Exists(sidecar)) return null;
            try
            {
                string url = File.ReadAllText(sidecar, Encoding.UTF8).Trim();
                return url.Length == 0 ? null : url;
            }
            catch { return null; }
        }
    }

    /// <summary>Dữ liệu đi kèm sự kiện <see cref="CloudinaryBackupUploadListener.UploadFinished"/>.</summary>
    public sealed class CloudUploadFinishedEventArgs : EventArgs
    {
        public string FilePath { get; }
        public bool Success { get; }
        public string ErrorMessage { get; }

        public CloudUploadFinishedEventArgs(string filePath, bool success, string errorMessage)
        {
            FilePath = filePath;
            Success = success;
            ErrorMessage = errorMessage;
        }
    }
}