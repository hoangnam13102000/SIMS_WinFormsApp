using System;
using System.Diagnostics;
using System.Threading;

namespace SIMS_WinFormsApp.Services.Backup
{
    public sealed class DailyBackupScheduler : IDisposable
    {
        private readonly BackupManager _backupManager;
        private readonly TimeSpan _checkInterval;
        private readonly object _sync = new object();
        private Timer _timer;
        private int _isRunning;
        private bool _disposed;

        public DailyBackupScheduler(BackupManager backupManager, TimeSpan checkInterval)
        {
            _backupManager = backupManager ?? throw new ArgumentNullException(nameof(backupManager));
            if (checkInterval <= TimeSpan.Zero)
                throw new ArgumentOutOfRangeException(nameof(checkInterval));

            _checkInterval = checkInterval;
        }

        public void Start()
        {
            lock (_sync)
            {
                if (_disposed || _timer != null) return;
                _timer = new Timer(Run, null, TimeSpan.Zero, _checkInterval);
            }
        }

        private void Run(object state)
        {
            if (Interlocked.Exchange(ref _isRunning, 1) == 1) return;

            try
            {
                BackupResult result = _backupManager.BackupIfNotDoneToday();
                if (result != null)
                    Debug.WriteLine("[BackupScheduler] Created " + result.FilePath);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[BackupScheduler] Backup failed: " + ex);
            }
            finally
            {
                Volatile.Write(ref _isRunning, 0);
            }
        }

        public void Dispose()
        {
            lock (_sync)
            {
                if (_disposed) return;
                _disposed = true;
                _timer?.Dispose();
                _timer = null;
            }
        }
    }
}
