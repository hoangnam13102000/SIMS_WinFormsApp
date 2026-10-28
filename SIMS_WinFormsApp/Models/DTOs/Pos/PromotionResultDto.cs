namespace SIMS_WinFormsApp.Models.DTOs.Pos
{
    public sealed class PromotionResultDto
    {
        public bool IsSuccess { get; set; }
        public int? PromotionId { get; set; }
        public string PromotionCode { get; set; }
        public decimal DiscountAmount { get; set; }
        public string Message { get; set; }

        public static PromotionResultDto Failed(string message) =>
            new PromotionResultDto { IsSuccess = false, DiscountAmount = 0m, Message = message };

        public static PromotionResultDto Applied(decimal discountAmount, string message) =>
            new PromotionResultDto { IsSuccess = true, DiscountAmount = discountAmount, Message = message };

        public static PromotionResultDto Applied(decimal discountAmount, string message, int promotionId, string promotionCode) =>
            new PromotionResultDto
            {
                IsSuccess = true,
                PromotionId = promotionId,
                PromotionCode = promotionCode,
                DiscountAmount = discountAmount,
                Message = message
            };
    }
}