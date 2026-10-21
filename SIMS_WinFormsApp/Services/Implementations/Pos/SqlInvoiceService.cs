using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
            IReadOnlyList<CartLineDto> lines)
        {
            ValidateRequest(createdBy, paymentMethod, totals, lines);
            List<CartLineDto> orderedLines = lines.OrderBy(line => line.ProductId).ToList();
            string invoiceCode = "HD-" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + "-" +
                                 Guid.NewGuid().ToString("N").Substring(0, 8).ToUpperInvariant();

            using (SqlConnection connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction(IsolationLevel.Serializable))
                {
                    ValidateStockAndPrices(connection, transaction, orderedLines);
                    int invoiceId = InsertInvoice(
                        connection, transaction, invoiceCode, createdBy, customerId, paymentMethod, totals);
                    InsertInvoiceDetails(connection, transaction, invoiceId, orderedLines);
                    DeductLoyaltyPoints(connection, transaction, customerId, totals);
                    transaction.Commit();
                }
            }

            return invoiceCode;
        }

        private static void ValidateStockAndPrices(
            SqlConnection connection,
            SqlTransaction transaction,
            IReadOnlyList<CartLineDto> lines)
        {
            const string sql = @"
                SELECT p.SellPrice, ISNULL(SUM(b.RemainingQty), 0) AS Available
                FROM Products p WITH (UPDLOCK, HOLDLOCK)
                LEFT JOIN InventoryBatch b WITH (UPDLOCK, HOLDLOCK)
                    ON b.ProductID = p.ProductID
                   AND b.Status = 'ACTIVE'
                   AND (b.ExpiryDate IS NULL OR b.ExpiryDate >= CAST(GETDATE() AS DATE))
                WHERE p.ProductID = @ProductID
                  AND p.Status = 'ACTIVE'
                GROUP BY p.SellPrice;";

            using (var command = new SqlCommand(sql, connection, transaction))
            {
                SqlParameter productIdParameter = command.Parameters.Add("@ProductID", SqlDbType.Int);
                foreach (CartLineDto line in lines)
                {
                    productIdParameter.Value = line.ProductId;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                            throw new InvalidOperationException("Một sản phẩm trong giỏ không còn hoạt động.");

                        decimal currentPrice = reader.GetDecimal(0);
                        int available = reader.GetInt32(1);
                        if (currentPrice != line.UnitPrice)
                            throw new InvalidOperationException(
                                "Giá sản phẩm đã thay đổi. Vui lòng tải lại giỏ hàng trước khi thanh toán.");
                        if (available < line.Quantity)
                            throw new InvalidOperationException(
                                "Tồn kho không đủ cho sản phẩm \"" + line.ProductName + "\".");
                    }
                }
            }
        }

        private static int InsertInvoice(
            SqlConnection connection,
            SqlTransaction transaction,
            string invoiceCode,
            int createdBy,
            int? customerId,
            string paymentMethod,
            CartTotalsDto totals)
        {
            const string sql = @"
                INSERT INTO Invoices
                    (InvoiceCode, CreatedBy, CustomerID, SubTotal, DiscountAmount, VATRate,
                     TotalAmount, PaymentMethod, PointsUsed, PointsDiscountAmount)
                OUTPUT INSERTED.InvoiceID
                VALUES
                    (@InvoiceCode, @CreatedBy, @CustomerID, @SubTotal, @DiscountAmount, @VATRate,
                     @TotalAmount, @PaymentMethod, @PointsUsed, @PointsDiscountAmount);";

            decimal taxableAmount = Math.Max(0m, totals.Subtotal - totals.DiscountAmount);
            decimal vatRate = taxableAmount == 0
                ? 0
                : Math.Round(totals.VatAmount * 100m / taxableAmount, 2, MidpointRounding.AwayFromZero);
            int pointsUsed = GetPointsUsed(totals);

            using (var command = new SqlCommand(sql, connection, transaction))
            {
                command.Parameters.Add("@InvoiceCode", SqlDbType.VarChar, 30).Value = invoiceCode;
                command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = createdBy;
                command.Parameters.Add("@CustomerID", SqlDbType.Int).Value =
                    customerId.HasValue ? (object)customerId.Value : DBNull.Value;
                AddMoneyParameter(command, "@SubTotal", totals.Subtotal);
                AddMoneyParameter(command, "@DiscountAmount", totals.DiscountAmount);

                SqlParameter rateParameter = command.Parameters.Add("@VATRate", SqlDbType.Decimal);
                rateParameter.Precision = 5;
                rateParameter.Scale = 2;
                rateParameter.Value = vatRate;

                AddMoneyParameter(command, "@TotalAmount", totals.GrandTotal);
                command.Parameters.Add("@PaymentMethod", SqlDbType.VarChar, 20).Value = paymentMethod;
                command.Parameters.Add("@PointsUsed", SqlDbType.Int).Value = pointsUsed;
                AddMoneyParameter(command, "@PointsDiscountAmount", totals.PointsDiscountAmount);
                return (int)command.ExecuteScalar();
            }
        }

        private static void InsertInvoiceDetails(
            SqlConnection connection,
            SqlTransaction transaction,
            int invoiceId,
            IReadOnlyList<CartLineDto> lines)
        {
            const string sql = @"
                INSERT INTO InvoiceDetails (InvoiceID, ProductID, Quantity, UnitPrice)
                VALUES (@InvoiceID, @ProductID, @Quantity, @UnitPrice);";

            using (var command = new SqlCommand(sql, connection, transaction))
            {
                command.Parameters.Add("@InvoiceID", SqlDbType.Int).Value = invoiceId;
                SqlParameter productId = command.Parameters.Add("@ProductID", SqlDbType.Int);
                SqlParameter quantity = command.Parameters.Add("@Quantity", SqlDbType.Int);
                SqlParameter unitPrice = AddMoneyParameter(command, "@UnitPrice", 0m);

                foreach (CartLineDto line in lines)
                {
                    productId.Value = line.ProductId;
                    quantity.Value = line.Quantity;
                    unitPrice.Value = line.UnitPrice;
                    command.ExecuteNonQuery();
                }
            }
        }

        private static void DeductLoyaltyPoints(
            SqlConnection connection,
            SqlTransaction transaction,
            int? customerId,
            CartTotalsDto totals)
        {
            int pointsUsed = GetPointsUsed(totals);
            if (pointsUsed == 0)
                return;
            if (!customerId.HasValue)
                throw new InvalidOperationException("Cần chọn khách hàng để sử dụng điểm thưởng.");

            using (var command = new SqlCommand(@"
                UPDATE Customers
                SET MemberPoint = MemberPoint - @PointsUsed
                WHERE CustomerID = @CustomerID
                  AND MemberPoint >= @PointsUsed;", connection, transaction))
            {
                command.Parameters.Add("@CustomerID", SqlDbType.Int).Value = customerId.Value;
                command.Parameters.Add("@PointsUsed", SqlDbType.Int).Value = pointsUsed;
                if (command.ExecuteNonQuery() != 1)
                    throw new InvalidOperationException("Điểm thưởng hiện có không đủ để thanh toán.");
            }
        }

        private static SqlParameter AddMoneyParameter(SqlCommand command, string name, decimal value)
        {
            SqlParameter parameter = command.Parameters.Add(name, SqlDbType.Decimal);
            parameter.Precision = 18;
            parameter.Scale = 0;
            parameter.Value = value;
            return parameter;
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
