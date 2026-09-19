using System;

namespace SIMS_WinFormsApp.Models.DTOs.Backup
{
    public sealed class BackupQuery
    {
        public string Keyword { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

        /// <summary>0-based.</summary>
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}