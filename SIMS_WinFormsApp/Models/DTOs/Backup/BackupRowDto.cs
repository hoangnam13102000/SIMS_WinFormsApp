using System;

namespace SIMS_WinFormsApp.Models.DTOs.Backup
{
    public sealed class BackupRowDto
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string StrategyName { get; set; }
        public DateTime CreatedAt { get; set; }
        public long SizeBytes { get; set; }
        public bool IsUploadedToCloud { get; set; }
        public string CloudUrl { get; set; }
    }
}