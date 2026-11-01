using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using SIMS_WinFormsApp.DAL.Linq;
using SIMS_WinFormsApp.Infrastructure.Configuration;
using SIMS_WinFormsApp.Models.DTOs.Pos;
using SIMS_WinFormsApp.Services.Interfaces.Pos;

namespace SIMS_WinFormsApp.Services.Implementations.Pos
{
    public sealed class SqlPosStockReservationService : IPosStockReservationService
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public SqlPosStockReservationService(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public IReadOnlyList<HeldCartDto> GetPendingReservations(int createdBy)
        {
            if (createdBy <= 0)
                throw new ArgumentOutOfRangeException(nameof(createdBy));

            var reservations = new Dictionary<int, HeldCartDto>();
            using (SimsDataContext db = _connectionFactory.CreateDataContext())
            {
                db.Connection.Open();
                const string sql = @"
                    SELECT r.ReservationID, r.CreatedAt, r.CustomerLabel,
                           l.ProductID, l.ProductName, l.UnitPrice, l.Quantity
                    FROM PosCartReservations r
                    INNER JOIN PosCartReservationLines l ON l.ReservationID = r.ReservationID
                    WHERE r.CreatedBy = @CreatedBy AND r.InvoiceID IS NULL
                    ORDER BY r.CreatedAt DESC, l.ProductID";
                using (var command = new SqlCommand(sql, (SqlConnection)db.Connection))
                {
                    command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = createdBy;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int reservationId = reader.GetInt32(0);
                            if (!reservations.TryGetValue(reservationId, out HeldCartDto reservation))
                            {
                                reservation = new HeldCartDto
                                {
                                    HeldCartId = reservationId,
                                    ReservationId = reservationId,
                                    HeldAt = reader.GetDateTime(1),
                                    CustomerLabel = reader.IsDBNull(2) ? null : reader.GetString(2),
                                    Lines = new List<CartLineDto>()
                                };
                                reservations.Add(reservationId, reservation);
                            }

                            ((List<CartLineDto>)reservation.Lines).Add(new CartLineDto(
                                reader.GetInt32(3),
                                reader.GetString(4),
                                reader.GetDecimal(5),
                                reader.GetInt32(6)));
                        }
                    }
                }
            }

            return reservations.Values.OrderByDescending(reservation => reservation.HeldAt).ToList();
        }

        public int ReserveStock(
            int? reservationId,
            int productId,
            string productName,
            decimal unitPrice,
            int quantity,
            int createdBy,
            string customerLabel)
        {
            if (productId <= 0 || quantity <= 0 || createdBy <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Thông tin giữ hàng không hợp lệ.");

            using (SimsDataContext db = _connectionFactory.CreateDataContext())
            {
                db.Connection.Open();
                using (var transaction = (SqlTransaction)db.Connection.BeginTransaction(IsolationLevel.Serializable))
                {
                    try
                    {
                        int currentStock;
                        decimal currentPrice;
                        using (var command = new SqlCommand(@"
                            SELECT Stock, SellPrice
                            FROM Products WITH (UPDLOCK, HOLDLOCK)
                            WHERE ProductID = @ProductID AND Status = 'ACTIVE'",
                            (SqlConnection)db.Connection, transaction))
                        {
                            command.Parameters.Add("@ProductID", SqlDbType.Int).Value = productId;
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (!reader.Read())
                                    throw new InvalidOperationException("Sản phẩm không còn hoạt động.");
                                currentStock = reader.GetInt32(0);
                                currentPrice = reader.GetDecimal(1);
                            }
                        }

                        if (currentPrice != unitPrice)
                            throw new InvalidOperationException(
                                "Giá sản phẩm đã thay đổi. Vui lòng tải lại sản phẩm trước khi thêm vào giỏ.");

                        int currentReservationId;
                        if (reservationId.HasValue)
                        {
                            currentReservationId = reservationId.Value;
                            using (var command = new SqlCommand(@"
                                SELECT COUNT(*)
                                FROM PosCartReservations WITH (UPDLOCK, HOLDLOCK)
                                WHERE ReservationID = @ReservationID AND CreatedBy = @CreatedBy AND InvoiceID IS NULL",
                                (SqlConnection)db.Connection, transaction))
                            {
                                command.Parameters.Add("@ReservationID", SqlDbType.Int).Value = currentReservationId;
                                command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = createdBy;
                                if (Convert.ToInt32(command.ExecuteScalar()) != 1)
                                    throw new InvalidOperationException(
                                        "Giỏ hàng đã giữ không còn tồn tại. Vui lòng tải lại trang POS.");
                            }
                        }
                        else
                        {
                            using (var command = new SqlCommand(@"
                                INSERT INTO PosCartReservations (CreatedBy, CustomerLabel)
                                VALUES (@CreatedBy, @CustomerLabel);
                                SELECT CAST(SCOPE_IDENTITY() AS INT);",
                                (SqlConnection)db.Connection, transaction))
                            {
                                command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = createdBy;
                                command.Parameters.Add("@CustomerLabel", SqlDbType.NVarChar, 150).Value =
                                    (object)customerLabel ?? DBNull.Value;
                                currentReservationId = Convert.ToInt32(command.ExecuteScalar());
                            }
                        }

                        var batches = new List<Tuple<int, int>>();
                        using (var command = new SqlCommand(@"
                            SELECT BatchID, RemainingQty
                            FROM InventoryBatch WITH (UPDLOCK, HOLDLOCK, ROWLOCK)
                            WHERE ProductID = @ProductID AND Status = 'ACTIVE' AND RemainingQty > 0
                              AND (ExpiryDate IS NULL OR ExpiryDate >= CAST(GETDATE() AS DATE))
                            ORDER BY ISNULL(ExpiryDate, '9999-12-31'), BatchID",
                            (SqlConnection)db.Connection, transaction))
                        {
                            command.Parameters.Add("@ProductID", SqlDbType.Int).Value = productId;
                            using (SqlDataReader reader = command.ExecuteReader())
                                while (reader.Read())
                                    batches.Add(Tuple.Create(reader.GetInt32(0), reader.GetInt32(1)));
                        }

                        if (currentStock < quantity || batches.Sum(batch => batch.Item2) < quantity)
                            throw new InvalidOperationException(
                                "Tồn kho khả dụng không đủ cho sản phẩm \"" + productName + "\".");

                        UpsertReservationLine(
                            (SqlConnection)db.Connection,
                            transaction,
                            currentReservationId,
                            productId,
                            productName,
                            unitPrice,
                            quantity,
                            customerLabel);

                        int remainingToReserve = quantity;
                        foreach (Tuple<int, int> batch in batches)
                        {
                            if (remainingToReserve == 0) break;
                            int take = Math.Min(batch.Item2, remainingToReserve);
                            using (var command = new SqlCommand(@"
                                UPDATE InventoryBatch
                                SET RemainingQty = RemainingQty - @Quantity,
                                    Status = CASE WHEN RemainingQty - @Quantity = 0 THEN 'DEPLETED' ELSE Status END
                                WHERE BatchID = @BatchID AND RemainingQty >= @Quantity",
                                (SqlConnection)db.Connection, transaction))
                            {
                                command.Parameters.Add("@Quantity", SqlDbType.Int).Value = take;
                                command.Parameters.Add("@BatchID", SqlDbType.Int).Value = batch.Item1;
                                if (command.ExecuteNonQuery() != 1)
                                    throw new InvalidOperationException(
                                        "Tồn kho đã thay đổi trong lúc thêm sản phẩm. Vui lòng thử lại.");
                            }

                            using (var command = new SqlCommand(@"
                                UPDATE PosCartReservationBatches
                                SET Quantity = Quantity + @Quantity
                                WHERE ReservationID = @ReservationID AND ProductID = @ProductID AND BatchID = @BatchID;
                                IF @@ROWCOUNT = 0
                                    INSERT INTO PosCartReservationBatches (ReservationID, ProductID, BatchID, Quantity)
                                    VALUES (@ReservationID, @ProductID, @BatchID, @Quantity);",
                                (SqlConnection)db.Connection, transaction))
                            {
                                command.Parameters.Add("@Quantity", SqlDbType.Int).Value = take;
                                command.Parameters.Add("@ReservationID", SqlDbType.Int).Value = currentReservationId;
                                command.Parameters.Add("@ProductID", SqlDbType.Int).Value = productId;
                                command.Parameters.Add("@BatchID", SqlDbType.Int).Value = batch.Item1;
                                command.ExecuteNonQuery();
                            }

                            remainingToReserve -= take;
                        }

                        if (remainingToReserve != 0)
                            throw new InvalidOperationException(
                                "Tồn kho khả dụng không đủ cho sản phẩm \"" + productName + "\".");

                        using (var command = new SqlCommand(@"
                            UPDATE Products
                            SET Stock = Stock - @Quantity
                            WHERE ProductID = @ProductID AND Stock >= @Quantity",
                            (SqlConnection)db.Connection, transaction))
                        {
                            command.Parameters.Add("@Quantity", SqlDbType.Int).Value = quantity;
                            command.Parameters.Add("@ProductID", SqlDbType.Int).Value = productId;
                            if (command.ExecuteNonQuery() != 1)
                                throw new InvalidOperationException(
                                    "Tồn kho khả dụng không đủ cho sản phẩm \"" + productName + "\".");
                        }

                        transaction.Commit();
                        return currentReservationId;
                    }
                    catch
                    {
                        if (transaction.Connection != null)
                            transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void ReleaseReservedStock(int reservationId, int productId, int quantity)
        {
            if (reservationId <= 0 || productId <= 0 || quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Thông tin hoàn tồn không hợp lệ.");

            using (SimsDataContext db = _connectionFactory.CreateDataContext())
            {
                db.Connection.Open();
                using (var transaction = (SqlTransaction)db.Connection.BeginTransaction(IsolationLevel.Serializable))
                {
                    try
                    {
                        int lineQuantity;
                        using (var command = new SqlCommand(@"
                            SELECT l.Quantity
                            FROM PosCartReservationLines l WITH (UPDLOCK, HOLDLOCK)
                            INNER JOIN PosCartReservations r WITH (UPDLOCK, HOLDLOCK)
                                ON r.ReservationID = l.ReservationID
                            WHERE l.ReservationID = @ReservationID AND l.ProductID = @ProductID
                              AND r.InvoiceID IS NULL",
                            (SqlConnection)db.Connection, transaction))
                        {
                            command.Parameters.Add("@ReservationID", SqlDbType.Int).Value = reservationId;
                            command.Parameters.Add("@ProductID", SqlDbType.Int).Value = productId;
                            object value = command.ExecuteScalar();
                            if (value == null || value == DBNull.Value)
                                throw new InvalidOperationException("Không tìm thấy số hàng đang được giữ.");
                            lineQuantity = Convert.ToInt32(value);
                        }

                        if (quantity > lineQuantity)
                            throw new InvalidOperationException("Số lượng hoàn vượt quá số hàng đang được giữ.");

                        var allocations = new List<Tuple<int, int>>();
                        using (var command = new SqlCommand(@"
                            SELECT rb.BatchID, rb.Quantity
                            FROM PosCartReservationBatches rb WITH (UPDLOCK, HOLDLOCK)
                            INNER JOIN InventoryBatch b ON b.BatchID = rb.BatchID
                            WHERE rb.ReservationID = @ReservationID AND rb.ProductID = @ProductID
                            ORDER BY ISNULL(b.ExpiryDate, '9999-12-31') DESC, rb.BatchID DESC",
                            (SqlConnection)db.Connection, transaction))
                        {
                            command.Parameters.Add("@ReservationID", SqlDbType.Int).Value = reservationId;
                            command.Parameters.Add("@ProductID", SqlDbType.Int).Value = productId;
                            using (SqlDataReader reader = command.ExecuteReader())
                                while (reader.Read())
                                    allocations.Add(Tuple.Create(reader.GetInt32(0), reader.GetInt32(1)));
                        }

                        if (allocations.Sum(allocation => allocation.Item2) != lineQuantity)
                            throw new InvalidOperationException("Dữ liệu lô hàng đang giữ không khớp.");

                        int remainingToRelease = quantity;
                        foreach (Tuple<int, int> allocation in allocations)
                        {
                            if (remainingToRelease == 0) break;
                            int release = Math.Min(allocation.Item2, remainingToRelease);
                            using (var command = new SqlCommand(@"
                                UPDATE InventoryBatch
                                SET RemainingQty = RemainingQty + @Quantity,
                                    Status = CASE WHEN Status = 'DEPLETED' THEN 'ACTIVE' ELSE Status END
                                WHERE BatchID = @BatchID;
                                IF EXISTS (
                                    SELECT 1 FROM PosCartReservationBatches
                                    WHERE ReservationID = @ReservationID AND ProductID = @ProductID
                                      AND BatchID = @BatchID AND Quantity = @Quantity
                                )
                                    DELETE FROM PosCartReservationBatches
                                    WHERE ReservationID = @ReservationID AND ProductID = @ProductID
                                      AND BatchID = @BatchID;
                                ELSE
                                    UPDATE PosCartReservationBatches
                                    SET Quantity = Quantity - @Quantity
                                    WHERE ReservationID = @ReservationID AND ProductID = @ProductID
                                      AND BatchID = @BatchID AND Quantity > @Quantity;
                                IF @@ROWCOUNT <> 1
                                    THROW 51001, 'Reservation batch allocation changed unexpectedly.', 1;",
                                (SqlConnection)db.Connection, transaction))
                            {
                                command.Parameters.Add("@Quantity", SqlDbType.Int).Value = release;
                                command.Parameters.Add("@BatchID", SqlDbType.Int).Value = allocation.Item1;
                                command.Parameters.Add("@ReservationID", SqlDbType.Int).Value = reservationId;
                                command.Parameters.Add("@ProductID", SqlDbType.Int).Value = productId;
                                command.ExecuteNonQuery();
                            }
                            remainingToRelease -= release;
                        }

                        if (remainingToRelease != 0)
                            throw new InvalidOperationException("Dữ liệu lô hàng đang giữ không khớp.");

                        using (var command = new SqlCommand(@"
                            UPDATE Products SET Stock = Stock + @Quantity WHERE ProductID = @ProductID;
                            IF EXISTS (
                                SELECT 1 FROM PosCartReservationLines
                                WHERE ReservationID = @ReservationID AND ProductID = @ProductID
                                  AND Quantity = @Quantity
                            )
                                DELETE FROM PosCartReservationLines
                                WHERE ReservationID = @ReservationID AND ProductID = @ProductID;
                            ELSE
                                UPDATE PosCartReservationLines
                                SET Quantity = Quantity - @Quantity
                                WHERE ReservationID = @ReservationID AND ProductID = @ProductID
                                  AND Quantity > @Quantity;
                            DELETE FROM PosCartReservations
                            WHERE ReservationID = @ReservationID
                              AND NOT EXISTS (SELECT 1 FROM PosCartReservationLines
                                              WHERE ReservationID = @ReservationID);",
                            (SqlConnection)db.Connection, transaction))
                        {
                            command.Parameters.Add("@Quantity", SqlDbType.Int).Value = quantity;
                            command.Parameters.Add("@ProductID", SqlDbType.Int).Value = productId;
                            command.Parameters.Add("@ReservationID", SqlDbType.Int).Value = reservationId;
                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        if (transaction.Connection != null)
                            transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        private static void UpsertReservationLine(
            SqlConnection connection,
            SqlTransaction transaction,
            int reservationId,
            int productId,
            string productName,
            decimal unitPrice,
            int quantity,
            string customerLabel)
        {
            using (var command = new SqlCommand(@"
                UPDATE PosCartReservationLines
                SET Quantity = Quantity + @Quantity
                WHERE ReservationID = @ReservationID AND ProductID = @ProductID AND UnitPrice = @UnitPrice;
                IF @@ROWCOUNT = 0
                BEGIN
                    IF EXISTS (SELECT 1 FROM PosCartReservationLines
                               WHERE ReservationID = @ReservationID AND ProductID = @ProductID)
                        THROW 51002, 'Product price in reservation does not match.', 1;
                    INSERT INTO PosCartReservationLines
                        (ReservationID, ProductID, ProductName, UnitPrice, Quantity)
                    VALUES (@ReservationID, @ProductID, @ProductName, @UnitPrice, @Quantity);
                END
                UPDATE PosCartReservations SET CustomerLabel = @CustomerLabel
                WHERE ReservationID = @ReservationID;",
                connection, transaction))
            {
                command.Parameters.Add("@ReservationID", SqlDbType.Int).Value = reservationId;
                command.Parameters.Add("@ProductID", SqlDbType.Int).Value = productId;
                command.Parameters.Add("@ProductName", SqlDbType.NVarChar, 150).Value = productName;
                SqlParameter price = command.Parameters.Add("@UnitPrice", SqlDbType.Decimal);
                price.Precision = 18;
                price.Scale = 0;
                price.Value = unitPrice;
                command.Parameters.Add("@Quantity", SqlDbType.Int).Value = quantity;
                command.Parameters.Add("@CustomerLabel", SqlDbType.NVarChar, 150).Value =
                    (object)customerLabel ?? DBNull.Value;
                command.ExecuteNonQuery();
            }
        }
    }
}
