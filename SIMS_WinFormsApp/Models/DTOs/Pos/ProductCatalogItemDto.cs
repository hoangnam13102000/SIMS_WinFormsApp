using System.Drawing;

namespace SIMS_WinFormsApp.Models.DTOs.Pos
{
    public sealed class ProductCatalogItemDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int StockQuantity { get; set; }
        public int BatchStockQuantity { get; set; }
        public int SellableQuantity { get; set; }
        public int ExpiredQuantity { get; set; }
        public int AvailableToAddQuantity { get; set; }
        public int StockBatchDifference => StockQuantity - BatchStockQuantity;
        public string ImagePath { get; set; }

        public Color TileColor { get; set; }

        public bool IsOutOfStock => SellableQuantity <= 0;
        public bool IsCartQuantityLimitReached =>
            SellableQuantity > 0 && AvailableToAddQuantity <= 0;
    }
}