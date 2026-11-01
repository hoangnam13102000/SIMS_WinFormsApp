using System;

namespace SIMS_WinFormsApp.Models.DTOs.Inventory
{
    public sealed class ExpiryBatchDto
    {
        public int BatchId { get; set; }
        public string BatchCode { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string LotNumber { get; set; }
        public string SupplierName { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int RemainingQuantity { get; set; }
        public decimal UnitCost { get; set; }
        public decimal LossAmount => RemainingQuantity * UnitCost;
        public bool IsExpired => ExpiryDate.Date < DateTime.Today;
    }
}
