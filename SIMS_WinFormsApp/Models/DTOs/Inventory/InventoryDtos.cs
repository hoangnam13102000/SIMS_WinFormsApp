using System;
using System.Collections.Generic;

namespace SIMS_WinFormsApp.Models.DTOs.Inventory
{
    public sealed class PurchaseLineDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal ImportPrice { get; set; }
        public string LotNumber { get; set; }
        public DateTime? ManufactureDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public decimal Amount => Quantity * ImportPrice;
    }

    public sealed class PurchaseReceiptDto
    {
        public int SupplierId { get; set; }
        public int CreatedBy { get; set; }
        public List<PurchaseLineDto> Lines { get; set; } = new List<PurchaseLineDto>();
    }

    public sealed class StockRowDto
    {
        public int ProductId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public int Stock { get; set; }
        public int MinStock { get; set; }
        public string StatusText => Stock <= 0
            ? "Hết hàng"
            : (Stock <= MinStock ? "Sắp hết" : "Còn hàng");
    }

    public sealed class StockCountDto
    {
        public int ProductId { get; set; }
        public int SystemStock { get; set; }
        public int ActualStock { get; set; }
        public string Note { get; set; }
    }
}