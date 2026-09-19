namespace SIMS_WinFormsApp.Services.Backup
{
    public interface IBackupStrategy
    {
        string Name { get; }

        /// <summary>Phần mở rộng file mà strategy này tạo ra, ví dụ "bak".</summary>
        string FileExtension { get; }

        void BackupTo(string destinationFilePath);
    }
}