using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SIMS_WinFormsApp.DAL.Linq;
using SIMS_WinFormsApp.DAL.Linq.Entities.Business;
using SIMS_WinFormsApp.DAL.Linq.Entities.Identity;
using SIMS_WinFormsApp.Infrastructure.Configuration;
using SIMS_WinFormsApp.Models.DTOs.Pos;
using SIMS_WinFormsApp.MVP.Presenters.Pos;
using SIMS_WinFormsApp.Services.Interfaces.Pos;
using SIMS_WinFormsApp.Services.Session;
using Newtonsoft.Json;

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
            int reservationId,
            IReadOnlyList<CartLineDto> lines)
        {
            if (reservationId <= 0)
                throw new ArgumentOutOfRangeException(nameof(reservationId));
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
                    ValidateStockAndPrices(db, orderedLines, reservationId, createdBy);

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

                    int linkedReservation = db.ExecuteCommand(
                        "UPDATE PosCartReservations SET InvoiceID = {0} " +
                        "WHERE ReservationID = {1} AND CreatedBy = {2} AND InvoiceID IS NULL",
                        invoice.InvoiceID,
                        reservationId,
                        createdBy);
                    if (linkedReservation != 1)
                        throw new InvalidOperationException(
                            "Giỏ hàng đang giữ không còn hiệu lực. Vui lòng tải lại trang POS.");

                    ConsumePromotion(db, promotionId);
                    foreach (CartLineDto line in orderedLines)
                    {
                        db.ExecuteCommand(
                            "INSERT INTO InvoiceDetails (InvoiceID, ProductID, Quantity, UnitPrice) " +
                            "VALUES ({0}, {1}, {2}, {3})",
                            invoice.InvoiceID,
                            line.ProductId,
                            line.Quantity,
                            line.UnitPrice);
                        db.AuditLogs.InsertOnSubmit(new AuditLogEntity
                        {
                            UserID = UserSession.Instance.CurrentUser?.UserId ?? createdBy,
                            Action = "DATA_CREATE",
                            TableName = "InvoiceDetails",
                            RecordID = null,
                            NewValue = JsonConvert.SerializeObject(new
                            {
                                invoice.InvoiceID,
                                line.ProductId,
                                line.Quantity,
                                line.UnitPrice
                            }),
                            Detail = "Đã thêm dòng hàng vào hóa đơn " + invoiceCode + ".",
                            CreatedAt = DateTime.Now
                        });
                    }
                    int remainingReservationLines = db.ExecuteQuery<int>(
                        "SELECT COUNT(*) FROM PosCartReservationLines WHERE ReservationID = {0}",
                        reservationId).Single();
                    if (remainingReservationLines != 0)
                        throw new InvalidOperationException(
                            "Số lượng giữ hàng không khớp với chi tiết hóa đơn.");

                    db.ExecuteCommand(
                        "DELETE FROM PosCartReservations WHERE ReservationID = {0} AND InvoiceID = {1}",
                        reservationId,
                        invoice.InvoiceID);

                    DeductLoyaltyPoints(db, customerId, totals);
                    db.SubmitChanges();
                    transaction.Commit();
                }
            }

            return invoiceCode;
        }

        private static void ValidateStockAndPrices(
            SimsDataContext db,
            IReadOnlyList<CartLineDto> lines,
            int reservationId,
            int createdBy)
        {
            bool reservationExists = db.ExecuteQuery<int>(
                "SELECT COUNT(*) FROM PosCartReservations WITH (UPDLOCK, HOLDLOCK) " +
                "WHERE ReservationID = {0} AND CreatedBy = {1} AND InvoiceID IS NULL",
                reservationId,
                createdBy).Single() == 1;
            if (!reservationExists)
                throw new InvalidOperationException("Giỏ hàng đang giữ không còn hiệu lực.");

            int storedLineCount = db.ExecuteQuery<int>(
                "SELECT COUNT(*) FROM PosCartReservationLines WITH (UPDLOCK, HOLDLOCK) WHERE ReservationID = {0}",
                reservationId).Single();
            if (storedLineCount != lines.Count)
                throw new InvalidOperationException(
                    "Số lượng giữ hàng không khớp với giỏ. Vui lòng tải lại giỏ hàng.");

            foreach (CartLineDto line in lines)
            {
                ProductEntity product = db.Products.SingleOrDefault(item =>
                    item.ProductID == line.ProductId && item.Status == "ACTIVE");
                if (product == null)
                    throw new InvalidOperationException("Một sản phẩm trong giỏ không còn hoạt động.");

                if (product.SellPrice != line.UnitPrice)
                    throw new InvalidOperationException(
                        "Giá sản phẩm đã thay đổi. Vui lòng tải lại giỏ hàng trước khi thanh toán.");

                int reservedQuantity = db.ExecuteQuery<int>(
                    "SELECT ISNULL((SELECT Quantity FROM PosCartReservationLines " +
                    "WHERE ReservationID = {0} AND ProductID = {1} AND UnitPrice = {2}), 0)",
                    reservationId,
                    line.ProductId,
                    line.UnitPrice).Single();
                int allocatedQuantity = db.ExecuteQuery<int>(
                    "SELECT ISNULL(SUM(Quantity), 0) FROM PosCartReservationBatches " +
                    "WHERE ReservationID = {0} AND ProductID = {1}",
                    reservationId,
                    line.ProductId).Single();
                if (reservedQuantity != line.Quantity || allocatedQuantity != line.Quantity)
                    throw new InvalidOperationException(
                        "Số lượng giữ hàng không khớp với sản phẩm \"" + line.ProductName + "\".");
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
