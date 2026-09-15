using System.Collections.Generic;

namespace SIMS_WinFormsApp.Models.DTOs
{
    public sealed class ImportSummaryDto
    {
        public int SuccessCount { get; set; }
        public IReadOnlyList<string> Errors { get; set; } = new List<string>();
    }
}