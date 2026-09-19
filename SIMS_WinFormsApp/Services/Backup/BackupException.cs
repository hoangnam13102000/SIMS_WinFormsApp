using System;

namespace SIMS_WinFormsApp.Services.Backup
{
    public sealed class BackupException : Exception
    {
        public BackupException(string message) : base(message) { }
        public BackupException(string message, Exception innerException) : base(message, innerException) { }
    }
}