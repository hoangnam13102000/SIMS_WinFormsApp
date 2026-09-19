using System;
using System.IO;

namespace SIMS_WinFormsApp.Services.Backup
{
    /// <summary>Kết quả của 1 lần sao lưu thành công - tương đương BackupResult.java.</summary>
    public sealed class BackupResult
    {
        public string FilePath { get; }
        public string StrategyName { get; }
        public DateTime StartedAt { get; }
        public DateTime FinishedAt { get; }
        public long SizeBytes { get; }

        public TimeSpan Duration => FinishedAt - StartedAt;
        public string FileName => Path.GetFileName(FilePath);

        public BackupResult(string filePath, string strategyName, DateTime startedAt, DateTime finishedAt)
        {
            FilePath = filePath;
            StrategyName = strategyName;
            StartedAt = startedAt;
            FinishedAt = finishedAt;
            SizeBytes = File.Exists(filePath) ? new FileInfo(filePath).Length : 0L;
        }
    }
}