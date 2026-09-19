using System;

namespace SIMS_WinFormsApp.Services.Backup
{
    public sealed class CloudinaryUploadException : Exception
    {
        public CloudinaryUploadException(string message) : base(message) { }
        public CloudinaryUploadException(string message, Exception innerException) : base(message, innerException) { }
    }
}