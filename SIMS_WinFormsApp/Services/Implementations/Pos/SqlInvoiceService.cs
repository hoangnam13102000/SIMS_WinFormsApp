using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SIMS_WinFormsApp.DAL.Linq;
using SIMS_WinFormsApp.DAL.Linq.Entities.Business;
using SIMS_WinFormsApp.Infrastructure.Configuration;
using SIMS_WinFormsApp.Models.DTOs.Pos;
using SIMS_WinFormsApp.MVP.Presenters.Pos;
using SIMS_WinFormsApp.Services.Interfaces.Pos;

namespace SIMS_WinFormsApp.Services.Implementations.Pos
{
    public sealed class SqlInvoiceService : IInvoiceService
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public SqlInvoiceService(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public string CreateInvoice(
            int createdBy,
            int? customerId,
            string paymentMethod,
            CartTotalsDto totals,
            int? promotionId,
            string promotionCode,
            IReadOnlyList<CartLineDto> lines)
        {
            ValidateRequest(createdBy, paymentMethod, totals, lines);
            List<CartLineDto> orderedLines = lines.OrderBy(line => line.ProductId).ToList();
            string invoiceCode = "HD-" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + "-" +
                                 Guid.NewGuid().ToString("N").Substring(0, 8).ToUpperInvariant();

            using (SimsDataContext db = _connectionFactory.CreateDataContext())
            {
                db.Connection.Open();
                using (var transaction = db.Connection.BeginTransaction(IsolationLevel.Serializable))
                {
                    db.Transaction = transaction;
                    ValidateStockAndPrices(db, orderedLines);

                    decimal taxableAmount = Math.Max(0m, totals.Subtotal - totals.DiscountAmount);
                    decimal vatRate = taxableAmount == 0
                        ? 0
                        : Math.Round(totals.VatAmount * 100m / taxableAmount, 2, MidpointRounding.AwayFromZero);
                    var invoice = new InvoiceEntity
                    {
                        InvoiceCode = invoiceCode,
                        CreatedBy = createdBy,
                        CustomerID = customerId,
                        SubTotal = totals.Subtotal,
                        DiscountAmount = totals.DiscountAmount,
                        PromotionID = promotionId,
                        PromotionCode = string.IsNullOrWhiteSpace(promotionCode) ? null : promotionCode,
                        VATRate = vatRate,
                        TotalAmount = totals.GrandTotal,
                        PaymentMethod = paymentMethod,
                        PointsUsed = GetPointsUsed(totals),
                        PointsDiscountAmount = totals.PointsDiscountAmount,
                        Status = "ACTIVE"
                    };
                    db.Invoices.InsertOnSubmit(invoice);
                    db.SubmitChanges();

                    ConsumePromotion(db, promotionId);
                    foreach (CartLineDto line in orderedLines)
                    {
                        db.InvoiceDetails.InsertOnSubmit(new InvoiceDetailEntity
                        {
                            InvoiceID = invoice.InvoiceID,
                            ProductID = line.ProductId,
                            Quantity = line.Quantity,
                            UnitPrice = line.UnitPrice
                        });
                    }
                    DeductLoyaltyPoints(db, customerId, totals);
                    db.SubmitChanges();
                    transaction.Commit();
                }
            }

            return invoiceCode;
        }

        private static void ValidateStockAndPrices(SimsDataContext db, IReadOnlyList<CartLineDto> lines)
        {
            DateTime today = DateTime.Today;
            foreach (CartLineDto line in lines)
            {
                ProductEntity product = db.Products.SingleOrDefault(item =>
                    item.ProductID == line.ProductId && item.Status == "ACTIVE");
                if (product == null)
                    throw new InvalidOperationException("Một sản phẩm trong giỏ không còn hoạt động.");

                int available = db.InventoryBatches
                    .Where(batch => batch.ProductID == line.ProductId && batch.Status == "ACTIVE"
                        && (!batch.ExpiryDate.HasValue || batch.ExpiryDate.Value >= today))
                    .Select(batch => (int?)batch.RemainingQty).Sum() ?? 0;
                if (product.SellPrice != line.UnitPrice)
                    throw new InvalidOperationException(
                        "Giá sản phẩm đã thay đổi. Vui lòng tải lại giỏ hàng trước khi thanh toán.");
                if (available < line.Quantity)
                    throw new InvalidOperationException(
                        "Tồn kho không đủ cho sản phẩm \"" + line.ProductName + "\".");
            }
        }

        private static void ConsumePromotion(SimsDataContext db, int? promotionId)
        {
            if (!promotionId.HasValue) return;
            PromotionEntity promotion = db.Promotions.SingleOrDefault(item =>
                item.PromotionID == promotionId.Value && !item.IsDeleted && item.IsActive);
            DateTime today = DateTime.Today;
            if (promotion == null || promotion.StartDate.Date > today || promotion.EndDate.Date < today
                || (promotion.UsageLimit.HasValue && promotion.UsedCount >= promotion.UsageLimit.Value))
                throw new InvalidOperationException("Mã giảm giá vừa hết lượt sử dụng hoặc không còn hiệu lực.");
            promotion.UsedCount++;
        }

        private static void DeductLoyaltyPoints(SimsDataContext db, int? customerId, CartTotalsDto totals)
        {
            int pointsUsed = GetPointsUsed(totals);
            if (pointsUsed == 0)
                return;
            if (!customerId.HasValue)
                throw new InvalidOperationException("Cần chọn khách hàng để sử dụng điểm thưởng.");

            CustomerEntity customer = db.Customers.SingleOrDefault(item => item.CustomerID == customerId.Value);
            if (customer == null || customer.MemberPoint < pointsUsed)
                throw new InvalidOperationException("Điểm thưởng hiện có không đủ để thanh toán.");
            customer.MemberPoint -= pointsUsed;
        }

        private static void ValidateRequest(
            int createdBy,
            string paymentMethod,
            CartTotalsDto totals,
            IReadOnlyList<CartLineDto> lines)
        {
            if (createdBy <= 0)
                throw new InvalidOperationException("Không xác định được nhân viên đang đăng nhập.");
            if (paymentMethod != "CASH" &&
                paymentMethod != "BANK_TRANSFER" &&
                paymentMethod != "CARD" &&
                paymentMethod != "PAYPAL")
                throw new InvalidOperationException("Vui lòng chọn phương thức thanh toán hợp lệ.");
            if (totals == null)
                throw new ArgumentNullException(nameof(totals));
            if (lines == null || lines.Count == 0)
                throw new InvalidOperationException("Giỏ hàng đang trống.");
            if (totals.Subtotal < 0 || totals.DiscountAmount < 0 ||
                totals.VatAmount < 0 || totals.PointsDiscountAmount < 0 || totals.GrandTotal < 0)
                throw new ArgumentOutOfRangeException(nameof(totals), "Giá trị thanh toán không hợp lệ.");

            decimal linesSubtotal = 0;
            foreach (CartLineDto line in lines)
            {
                if (line == null || line.ProductId <= 0 || line.Quantity <= 0 || line.UnitPrice < 0)
                    throw new ArgumentException("Chi tiết sản phẩm trong giỏ không hợp lệ.", nameof(lines));

                linesSubtotal += line.LineTotal;
            }

            if (linesSubtotal != totals.Subtotal)
                throw new InvalidOperationException("Tổng tiền giỏ hàng đã thay đổi. Vui lòng thử lại.");

            if (GetPointsUsed(totals) * CartCalculator.PointRedeemRateVnd != totals.PointsDiscountAmount)
                throw new ArgumentException("Số tiền quy đổi điểm thưởng không hợp lệ.", nameof(totals));
        }

        private static int GetPointsUsed(CartTotalsDto totals)
        {
            return decimal.ToInt32(
                Math.Round(totals.PointsDiscountAmount / CartCalculator.PointRedeemRateVnd, 0));
        }
    }
}
