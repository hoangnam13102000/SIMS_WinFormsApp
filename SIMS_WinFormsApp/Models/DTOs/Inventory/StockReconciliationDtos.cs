using System;

namespace SIMS_WinFormsApp.Models.DTOs.Inventory
{
    public sealed class StockReconciliationRowDto
    {
        public int ReconciliationId { get; set; }
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public int SystemStock { get; set; }
        public int ActualStock { get; set; }
        public int CurrentStock { get; set; }
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedByName { get; set; }

        public int Discrepancy => ActualStock - SystemStock;

        public bool IsEditable => CreatedAt.Date == DateTime.Today;

        public string DiscrepancyText => Discrepancy == 0
            ? "Khớp"
            : (Discrepancy > 0 ? "+" : string.Empty) + Discrepancy;

        public string SessionStatusText => IsEditable ? "Đang mở" : "Đã khóa";
    }

    public enum StockCountUpdateStatus
    {
        Ok,
        NotFound,
        Locked,
        OutOfRange,
        NotEnoughStock
    }
}