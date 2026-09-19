namespace SIMS_WinFormsApp.Services.Backup
{
    public interface IRestoreStrategy
    {
        string Name { get; }
        void RestoreFrom(string backupFilePath);
    }
}