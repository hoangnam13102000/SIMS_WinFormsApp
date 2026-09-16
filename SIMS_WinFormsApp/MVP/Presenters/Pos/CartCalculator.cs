using System;
using SIMS_WinFormsApp.Models.DTOs.Pos;

namespace SIMS_WinFormsApp.MVP.Presenters.Pos
{
    /// <summary>
    /// Tính "Tạm tính / Giảm giá / VAT (8%) / Trừ điểm / Tổng cộng" từ giỏ hàng - tách riêng
    /// khỏi PosCart (chỉ lo danh sách dòng hàng) và khỏi PosPresenter (chỉ lo điều phối UI),
    /// đúng Single Responsibility: đây là hàm thuần túy, không phụ thuộc DB/UI nên dễ unit-test
    /// và dễ chỉnh khi có quy tắc thuế/điểm thưởng thật từ StoreConfig sau này.
    /// </summary>
    public sealed class CartCalculator
    {
        public const decimal VatRate = 0.08m;

        /// <summary>Giá trị quy đổi 1 điểm thành tiền - DEMO cố định (bản Java đọc từ
        /// StoreConfigDAO.getPointRedeemRate(), project C# chưa có bảng cấu hình cửa hàng).</summary>
        public const decimal PointRedeemRateVnd = 1000m;

        public CartTotalsDto Calculate(decimal subtotal, decimal promotionDiscount, int pointsToRedeem)
        {
            decimal discount = Clamp(promotionDiscount, 0m, subtotal);
            decimal taxable = Math.Max(0m, subtotal - discount);
            decimal vat = Math.Round(taxable * VatRate, 0, MidpointRounding.AwayFromZero);

            decimal beforePoints = taxable + vat;
            decimal pointsDiscount = Clamp(Math.Max(0, pointsToRedeem) * PointRedeemRateVnd, 0m, beforePoints);

            decimal grandTotal = Math.Max(0m, beforePoints - pointsDiscount);

            return new CartTotalsDto
            {
                Subtotal = subtotal,
                DiscountAmount = discount,
                VatAmount = vat,
                PointsDiscountAmount = pointsDiscount,
                GrandTotal = grandTotal
            };
        }

        private static decimal Clamp(decimal value, decimal min, decimal max) =>
            value < min ? min : (value > max ? max : value);
    }
}