using System;
using System.Collections.Generic;

namespace SIMS_WinFormsApp.Models.DTOs.Pos
{
    public sealed class HeldCartDto
    {
        public int HeldCartId { get; set; }
        public DateTime HeldAt { get; set; }
        public string CustomerLabel { get; set; }
        public IReadOnlyList<CartLineDto> Lines { get; set; }

        public string DisplayText =>
            HeldAt.ToString("HH:mm dd/MM") + " - " +
            (string.IsNullOrEmpty(CustomerLabel) ? "Khách lẻ" : CustomerLabel) +
            " (" + (Lines?.Count ?? 0) + " sản phẩm)";

        public override string ToString() => DisplayText;
    }
}