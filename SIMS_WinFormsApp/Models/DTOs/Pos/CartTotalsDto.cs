namespace SIMS_WinFormsApp.Models.DTOs.Pos
{
    public sealed class CartTotalsDto
    {
        public decimal Subtotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal VatAmount { get; set; }
        public decimal PointsDiscountAmount { get; set; }
        public decimal GrandTotal { get; set; }
    }
}