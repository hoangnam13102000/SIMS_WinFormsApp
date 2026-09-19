using System.Configuration;

namespace SIMS_WinFormsApp.Services.Backup
{
    public static class CloudinaryConfig
    {
        public static string CloudName => ConfigurationManager.AppSettings["CloudinaryCloudName"];

        public static string BackupUploadPreset => ConfigurationManager.AppSettings["CloudinaryBackupUploadPreset"];

        public static bool IsConfigured =>
            !string.IsNullOrWhiteSpace(CloudName) && !string.IsNullOrWhiteSpace(BackupUploadPreset);
    }
}