using System;
using SIMS_WinFormsApp.Models.DTOs.Pos;
using SIMS_WinFormsApp.Services.Interfaces.Pos;

namespace SIMS_WinFormsApp.Services.Implementations.Pos
{
    public sealed class DemoPromotionService : IPromotionService
    {
        public PromotionResultDto Apply(string code, decimal subtotal)
        {
            if (string.IsNullOrWhiteSpace(code))
                return PromotionResultDto.Failed("Vui lòng nhập mã khuyến mãi.");

            if (subtotal <= 0)
                return PromotionResultDto.Failed("Giỏ hàng đang trống, chưa có gì để áp mã.");

            string normalized = code.Trim().ToUpperInvariant();

            switch (normalized)
            {
                case "GIAM10":
                    return PromotionResultDto.Applied(Math.Round(subtotal * 0.10m, 0), "Đã áp dụng mã giảm 10%.");
                case "GIAM20K":
                    return PromotionResultDto.Applied(Math.Min(20000m, subtotal), "Đã áp dụng mã giảm 20.000đ.");
                default:
                    return PromotionResultDto.Failed("Mã khuyến mãi không hợp lệ hoặc đã hết hạn.");
            }
        }
    }
}