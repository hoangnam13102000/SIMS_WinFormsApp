namespace SIMS_WinFormsApp.Models.DTOs.Pos
{
    public sealed class CartLineDto
    {
        public int ProductId { get; }
        public string ProductName { get; }
        public decimal UnitPrice { get; }
        public int Quantity { get; }
        public decimal LineTotal => UnitPrice * Quantity;

        public CartLineDto(int productId, string productName, decimal unitPrice, int quantity)
        {
            ProductId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }
    }
}