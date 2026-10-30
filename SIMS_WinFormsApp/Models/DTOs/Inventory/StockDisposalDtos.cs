using System;
using System.Collections.Generic;
using System.Globalization;

namespace SIMS_WinFormsApp.Models.DTOs.Inventory
{
    public static class DisposalReasons
    {
        public const string Expired = "EXPIRED";
        public const string Damaged = "DAMAGED";
        public const string Quality = "QUALITY";
        public const string Other = "OTHER";

        /// <summary>Đúng thứ tự các mục trong ô "Lý do" của form lập phiếu.</summary>
        public static readonly string[] Codes = { Expired, Damaged, Quality, Other };

        public static string ToLabel(string code)
        {
            switch ((code ?? string.Empty).Trim().ToUpperInvariant())
            {
                case Expired: return "Hết hạn";
                case Damaged: return "Hỏng / hư hỏng";
                case Quality: return "Chất lượng";
                case Other: return "Khác";
                default: return code ?? string.Empty;
            }
        }
    }

    public sealed class StockDisposalListItemDto
    {
        public int DisposalId { get; set; }
        public string DisposalCode { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public decimal TotalLossAmount { get; set; }
        public string Note { get; set; }
        public string CreatedByName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ItemCount { get; set; }
        public string ReasonLabel => DisposalReasons.ToLabel(Reason);
        public bool IsCancelled => string.Equals(Status, "CANCELLED", StringComparison.OrdinalIgnoreCase);
        public string StatusText => IsCancelled ? "Đã hủy" : "Hoàn tất";
    }

    public sealed class StockDisposalDetailLineDto
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ReceiptCode { get; set; }
        public string BatchCode { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int Quantity { get; set; }
        public decimal UnitCost { get; set; }
        public decimal LossAmount => Quantity * UnitCost;
    }

    /// <summary>1 lô còn hàng để chọn trong ô "Lô hàng *" (giống InventoryBatch bên Java).</summary>
    public sealed class DisposableBatchDto
    {
        public int BatchId { get; set; }
        public string BatchCode { get; set; }
        public string LotNumber { get; set; }
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string SupplierName { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int RemainingQty { get; set; }
        public decimal ImportPrice { get; set; }

        /// <summary>Giống Java: "LOT_000019  ·  Bánh quy bơ 200g  ·  còn 10  ·  HSD 2026-10-27".</summary>
        public override string ToString()
        {
            string expiry = ExpiryDate.HasValue
                ? ExpiryDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                : "—";
            return BatchCode + "  ·  " + ProductName + "  ·  còn " + RemainingQty + "  ·  HSD " + expiry;
        }
    }

    public sealed class StockDisposalLineDto
    {
        public int BatchId { get; set; }
        public int Quantity { get; set; }
    }

    public sealed class StockDisposalDto
    {
        public string Reason { get; set; }
        public string Note { get; set; }
        public int CreatedBy { get; set; }
        public List<StockDisposalLineDto> Lines { get; set; } = new List<StockDisposalLineDto>();
    }
}
