using System;
using System.Collections.Generic;
using SIMS_WinFormsApp.Models.DTOs.Backup;

namespace SIMS_WinFormsApp.Views.Interfaces
{
    public interface IBackupRecoveryView
    {
        event EventHandler ViewReady;
        event EventHandler<BackupQuery> QueryChanged;

        event EventHandler BackupNowRequested;
        event EventHandler RestoreFromFileRequested;
        event EventHandler<BackupRowDto> RestoreRowRequested;
        event EventHandler<BackupRowDto> DeleteRowRequested;

        void BindRows(IReadOnlyList<BackupRowDto> pageRows, int totalCount);

        void SetBusy(bool isBusy, string message);
        void ShowError(string message);
        void ShowSuccess(string message);

        /// <summary>Mở hộp thoại chọn file backup từ đĩa để khôi phục - trả về đường dẫn hoặc null nếu hủy.</summary>
        string PromptChooseBackupFile(string initialDirectory);

        /// <summary>Hỏi xác nhận trước 1 thao tác không thể hoàn tác (khôi phục sẽ ghi đè dữ
        /// liệu hiện tại, xóa file backup...).</summary>
        bool Confirm(string title, string message);
    }
}