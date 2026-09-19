using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using SIMS_WinFormsApp.Models.DTOs.Backup;
using SIMS_WinFormsApp.Services.Backup;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.MVP.Presenters
{
    public sealed class BackupRecoveryPresenter : IDisposable
    {
        private readonly IBackupRecoveryView _view;
        private readonly BackupManager _backupManager;
        private readonly CloudinaryBackupUploadListener _cloudUploadListener;

        private readonly SynchronizationContext _uiContext = SynchronizationContext.Current;

        private BackupQuery _lastQuery = new BackupQuery { PageIndex = 0, PageSize = 10 };

        public BackupRecoveryPresenter(
            IBackupRecoveryView view,
            BackupManager backupManager,
            CloudinaryBackupUploadListener cloudUploadListener)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _backupManager = backupManager ?? throw new ArgumentNullException(nameof(backupManager));
            _cloudUploadListener = cloudUploadListener; // null nếu chưa cấu hình Cloudinary - vẫn hoạt động bình thường, chỉ không có backup cloud

            _view.ViewReady += OnViewReady;
            _view.QueryChanged += OnQueryChanged;
            _view.BackupNowRequested += OnBackupNowRequested;
            _view.RestoreFromFileRequested += OnRestoreFromFileRequested;
            _view.RestoreRowRequested += OnRestoreRowRequested;
            _view.DeleteRowRequested += OnDeleteRowRequested;

            if (_cloudUploadListener != null)
                _cloudUploadListener.UploadFinished += OnCloudUploadFinished;
        }

        private void OnViewReady(object sender, EventArgs e) => Reload();

        private void OnQueryChanged(object sender, BackupQuery query)
        {
            _lastQuery = query ?? new BackupQuery { PageIndex = 0, PageSize = 10 };
            Reload();
        }

        private void Reload()
        {
            var filtered = ApplyFilters(LoadAllRows(), _lastQuery);
            int total = filtered.Count;

            var page = filtered
                .Skip(_lastQuery.PageIndex * _lastQuery.PageSize)
                .Take(_lastQuery.PageSize)
                .ToList();

            _view.BindRows(page, total);
        }

        private List<BackupRowDto> LoadAllRows() =>
            _backupManager.Storage.ListBackups().Select(ToRow).ToList();

        private static BackupRowDto ToRow(string filePath)
        {
            var info = new FileInfo(filePath);
            string cloudUrl = CloudinaryBackupUploadListener.ReadUploadedUrl(filePath);

            return new BackupRowDto
            {
                FilePath = filePath,
                FileName = info.Name,
                StrategyName = ExtractStrategyName(info.Name),
                CreatedAt = info.LastWriteTime,
                SizeBytes = info.Exists ? info.Length : 0L,
                IsUploadedToCloud = !string.IsNullOrEmpty(cloudUrl),
                CloudUrl = cloudUrl
            };
        }

        /// <summary>Tách tên strategy từ tên file theo đúng quy ước BackupStorage đặt tên:
        /// backup_&lt;ngày[-giờ]&gt;_&lt;strategy&gt;.&lt;ext&gt;</summary>
        private static string ExtractStrategyName(string fileName)
        {
            string nameOnly = Path.GetFileNameWithoutExtension(fileName);
            int lastUnderscore = nameOnly.LastIndexOf('_');
            return lastUnderscore >= 0 && lastUnderscore < nameOnly.Length - 1
                ? nameOnly.Substring(lastUnderscore + 1)
                : "?";
        }

        private static List<BackupRowDto> ApplyFilters(List<BackupRowDto> rows, BackupQuery query)
        {
            IEnumerable<BackupRowDto> result = rows;

            if (!string.IsNullOrWhiteSpace(query.Keyword))
            {
                string k = query.Keyword.Trim();
                result = result.Where(r => r.FileName.IndexOf(k, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            if (query.From.HasValue)
                result = result.Where(r => r.CreatedAt.Date >= query.From.Value.Date);
            if (query.To.HasValue)
                result = result.Where(r => r.CreatedAt.Date <= query.To.Value.Date);

            return result.OrderByDescending(r => r.CreatedAt).ToList();
        }

        private void OnBackupNowRequested(object sender, EventArgs e)
        {
            _view.SetBusy(true, "Đang sao lưu...");
            try
            {
                var result = _backupManager.BackupNow();
                _view.SetBusy(false, null);
                _view.ShowSuccess("Đã tạo bản sao lưu \"" + result.FileName + "\" (" + BackupFormat.Size(result.SizeBytes) + ").");
                Reload();
            }
            catch (Exception ex)
            {
                _view.SetBusy(false, null);
                _view.ShowError("Sao lưu thất bại: " + ex.Message);
            }
        }

        private void OnRestoreFromFileRequested(object sender, EventArgs e)
        {
            string path = _view.PromptChooseBackupFile(_backupManager.Storage.DirectoryPath);
            if (!string.IsNullOrEmpty(path)) RestoreFile(path);
        }

        private void OnRestoreRowRequested(object sender, BackupRowDto row)
        {
            if (row != null) RestoreFile(row.FilePath);
        }

        private void RestoreFile(string filePath)
        {
            bool confirmed = _view.Confirm("Xác nhận khôi phục",
                "Khôi phục sẽ GHI ĐÈ toàn bộ dữ liệu hiện tại bằng nội dung trong file \"" +
                Path.GetFileName(filePath) + "\". Hành động này không thể hoàn tác. Bạn có chắc chắn muốn tiếp tục?");
            if (!confirmed) return;

            _view.SetBusy(true, "Đang khôi phục...");
            try
            {
                _backupManager.Restore(filePath);
                _view.SetBusy(false, null);
                _view.ShowSuccess("Đã khôi phục dữ liệu từ \"" + Path.GetFileName(filePath) + "\".");
            }
            catch (Exception ex)
            {
                _view.SetBusy(false, null);
                _view.ShowError("Khôi phục thất bại: " + ex.Message);
            }
        }

        private void OnDeleteRowRequested(object sender, BackupRowDto row)
        {
            if (row == null) return;
            bool confirmed = _view.Confirm("Xác nhận xóa", "Xóa vĩnh viễn bản sao lưu \"" + row.FileName + "\"?");
            if (!confirmed) return;

            try
            {
                _backupManager.Storage.Delete(row.FilePath);
                string sidecar = CloudinaryBackupUploadListener.SidecarFileFor(row.FilePath);
                if (File.Exists(sidecar)) File.Delete(sidecar);

                _view.ShowSuccess("Đã xóa \"" + row.FileName + "\".");
                Reload();
            }
            catch (Exception ex)
            {
                _view.ShowError("Không xóa được: " + ex.Message);
            }
        }

        // Chạy trên luồng nền (continuation sau khi await HttpClient trong CloudinaryUploader) -
        // BẮT BUỘC đưa về luồng UI trước khi gọi Reload() (đụng vào DataGridView).
        private void OnCloudUploadFinished(object sender, CloudUploadFinishedEventArgs e)
        {
            Action updateView = () =>
            {
                if (!e.Success)
                    _view.ShowError("Backup local đã tạo nhưng Cloudinary upload thất bại: " +
                        (e.ErrorMessage ?? "Không rõ nguyên nhân."));
                Reload();
            };

            if (_uiContext != null) _uiContext.Post(_ => updateView(), null);
            else updateView();
        }

        public void Dispose()
        {
            _view.ViewReady -= OnViewReady;
            _view.QueryChanged -= OnQueryChanged;
            _view.BackupNowRequested -= OnBackupNowRequested;
            _view.RestoreFromFileRequested -= OnRestoreFromFileRequested;
            _view.RestoreRowRequested -= OnRestoreRowRequested;
            _view.DeleteRowRequested -= OnDeleteRowRequested;

            if (_cloudUploadListener != null)
                _cloudUploadListener.UploadFinished -= OnCloudUploadFinished;
        }
    }
}