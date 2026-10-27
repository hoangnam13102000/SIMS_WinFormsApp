using System.Collections.Generic;

namespace SIMS_WinFormsApp.Models.DTOs
{
    public sealed class TablePageResult
    {
        public int TotalCount { get; set; }
        public IList<object[]> Rows { get; set; } = new List<object[]>();
    }
}
