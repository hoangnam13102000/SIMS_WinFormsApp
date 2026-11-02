using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using SIMS_WinFormsApp.Infrastructure.Configuration;
using SIMS_WinFormsApp.Models.DTOs.Inventory;
using SIMS_WinFormsApp.Repositories.Interfaces;

namespace SIMS_WinFormsApp.Repositories.Implementations
{
    public sealed class StockDisposalRepository : IStockDisposalRepository
    {
        private readonly IDbConnectionFactory _connections;

        public StockDisposalRepository(IDbConnectionFactory connections)
        {
            _connections = connections ?? throw new ArgumentNullException(nameof(connections));
        }

        public IReadOnlyList<StockDisposalListItemDto> SearchDisposals(
            string keyword, string reasonCodeList, DateTime? fromDate, DateTime? toDate)
        {
            const string sql = @"
                SELECT d.DisposalID, d.DisposalCode, d.Reason, d.Status, d.TotalLossAmount, d.Note,
                       u.FullName AS CreatedByName, d.CreatedAt,
                       (SELECT COUNT(*) FROM StockDisposalDetails x WHERE x.DisposalID = d.DisposalID) AS ItemCount
                FROM StockDisposals d
                INNER JOIN Users u ON u.UserID = d.CreatedBy
                WHERE (@HasKeyword = 0
                       OR d.DisposalCode LIKE @Pattern
                       OR u.FullName LIKE @Pattern
                       OR d.Note LIKE @Pattern
                       OR CHARINDEX(',' + d.Reason + ',', @ReasonList) > 0
                       OR EXISTS (
                           SELECT 1
                           FROM StockDisposalDetails sd
                           INNER JOIN Products p ON p.ProductID = sd.ProductID
                           INNER JOIN InventoryBatch b ON b.BatchID = sd.BatchID
                           WHERE sd.DisposalID = d.DisposalID
                             AND (b.BatchCode LIKE @Pattern
                                  OR b.LotNumber LIKE @Pattern
                                  OR p.ProductCode LIKE @Pattern
                                  OR p.ProductName LIKE @Pattern)))
                  AND (@FromDate IS NULL OR d.CreatedAt >= @FromDate)
                  AND (@ToExclusive IS NULL OR d.CreatedAt < @ToExclusive)
                ORDER BY d.CreatedAt DESC, d.DisposalID DESC";

            string normalizedKeyword = (keyword ?? string.Empty).Trim();
            DateTime? from = fromDate.HasValue ? fromDate.Value.Date : (DateTime?)null;
            DateTime? toExclusive = toDate.HasValue ? toDate.Value.Date.AddDays(1) : (DateTime?)null;
            var rows = new List<StockDisposalListItemDto>();

            using (var connection = Open())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@HasKeyword", SqlDbType.Bit).Value = normalizedKeyword.Length > 0;
                command.Parameters.Add("@Pattern", SqlDbType.NVarChar, 4000).Value = "%" + normalizedKeyword + "%";
                command.Parameters.Add("@ReasonList", SqlDbType.VarChar, 200).Value =
                    "," + (reasonCodeList ?? string.Empty) + ",";
                command.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = (object)from ?? DBNull.Value;
                command.Parameters.Add("@ToExclusive", SqlDbType.DateTime).Value = (object)toExclusive ?? DBNull.Value;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rows.Add(new StockDisposalListItemDto
                        {
                            DisposalId = reader.GetInt32(0),
                            DisposalCode = ReadString(reader, 1),
                            Reason = ReadString(reader, 2),
                            Status = ReadString(reader, 3),
                            TotalLossAmount = ReadDecimal(reader, 4),
                            Note = ReadString(reader, 5),
                            CreatedByName = ReadString(reader, 6),
                            CreatedAt = reader.GetDateTime(7),
                            ItemCount = ReadInt(reader, 8)
                        });
                    }
                }
            }
            return rows;
        }

        public IReadOnlyList<StockDisposalDetailLineDto> GetDisposalDetails(int disposalId)
        {
            const string sql = @"
                SELECT p.ProductCode, p.ProductName, r.ReceiptCode, b.BatchCode, b.ExpiryDate,
                       sd.Quantity, sd.UnitCost
                FROM StockDisposalDetails sd
                INNER JOIN Products p ON p.ProductID = sd.ProductID
                INNER JOIN InventoryBatch b ON b.BatchID = sd.BatchID
                LEFT JOIN PurchaseReceiptDetails rd ON rd.ReceiptDetailID = b.ReceiptDetailID
                LEFT JOIN PurchaseReceipts r ON r.ReceiptID = rd.ReceiptID
                WHERE sd.DisposalID = @DisposalID
                ORDER BY sd.DisposalDetailID";
            var rows = new List<StockDisposalDetailLineDto>();

            using (var connection = Open())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@DisposalID", SqlDbType.Int).Value = disposalId;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rows.Add(new StockDisposalDetailLineDto
                        {
                            ProductCode = ReadString(reader, 0),
                            ProductName = ReadString(reader, 1),
                            ReceiptCode = ReadString(reader, 2),
                            BatchCode = ReadString(reader, 3),
                            ExpiryDate = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4),
                            Quantity = ReadInt(reader, 5),
                            UnitCost = ReadDecimal(reader, 6)
                        });
                    }
                }
            }
            return rows;
        }

        public IReadOnlyList<DisposableBatchDto> GetDisposableBatches()
        {
            // Giống StockDisposalDAO.listDisposableBatches: lô còn hàng, HSD gần trước, không có HSD để cuối.
            const string sql = @"
                SELECT b.BatchID, b.BatchCode, b.LotNumber, b.ProductID, p.ProductCode, p.ProductName,
                       s.SupplierName, b.ExpiryDate, b.RemainingQty, b.ImportPrice
                FROM InventoryBatch b
                INNER JOIN Products p ON p.ProductID = b.ProductID
                INNER JOIN Suppliers s ON s.SupplierID = b.SupplierID
                WHERE b.RemainingQty > 0
                ORDER BY CASE WHEN b.ExpiryDate IS NULL THEN 1 ELSE 0 END, b.ExpiryDate ASC, b.BatchID ASC";
            var rows = new List<DisposableBatchDto>();

            using (var connection = Open())
            using (var command = new SqlCommand(sql, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    rows.Add(new DisposableBatchDto
                    {
                        BatchId = reader.GetInt32(0),
                        BatchCode = ReadString(reader, 1),
                        LotNumber = ReadString(reader, 2),
                        ProductId = reader.GetInt32(3),
                        ProductCode = ReadString(reader, 4),
                        ProductName = ReadString(reader, 5),
                        SupplierName = ReadString(reader, 6),
                        ExpiryDate = reader.IsDBNull(7) ? (DateTime?)null : reader.GetDateTime(7),
                        RemainingQty = ReadInt(reader, 8),
                        ImportPrice = ReadDecimal(reader, 9)
                    });
                }
            }
            return rows;
        }

        /// <summary>Giống StockDisposalDAO.createDisposal (Java): 1 transaction thêm phiếu + dòng, trừ lô, trừ tồn, ghi DISPOSAL.</summary>
        public int CreateDisposal(StockDisposalDto disposal, out string disposalCode)
        {
            if (disposal == null) throw new ArgumentNullException(nameof(disposal));
            if (disposal.Lines == null || disposal.Lines.Count == 0)
                throw new ArgumentException("Phiếu tiêu hủy phải có ít nhất 1 dòng.", nameof(disposal));

            disposalCode = null;
            using (var connection = Open())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    const string batchSql = @"
                        SELECT b.ProductID, b.RemainingQty, b.ImportPrice, p.ProductName
                        FROM InventoryBatch b WITH (UPDLOCK, ROWLOCK)
                        INNER JOIN Products p ON p.ProductID = b.ProductID
                        WHERE b.BatchID = @BatchID";

                    var prepared = new List<PreparedLine>();
                    decimal totalLoss = 0m;
                    foreach (StockDisposalLineDto line in disposal.Lines)
                    {
                        using (var command = new SqlCommand(batchSql, connection, transaction))
                        {
                            command.Parameters.Add("@BatchID", SqlDbType.Int).Value = line.BatchId;
                            using (var reader = command.ExecuteReader())
                            {
                                if (!reader.Read())
                                    throw new InvalidOperationException("Không tìm thấy lô hàng đã chọn.");

                                int remaining = ReadInt(reader, 1);
                                if (line.Quantity > remaining)
                                    throw new InvalidOperationException("Lô của \"" + ReadString(reader, 3) + "\" chỉ còn "
                                        + remaining + ", không đủ để hủy " + line.Quantity + ".");

                                var item = new PreparedLine
                                {
                                    BatchId = line.BatchId,
                                    ProductId = reader.GetInt32(0),
                                    Quantity = line.Quantity,
                                    UnitCost = ReadDecimal(reader, 2)
                                };
                                prepared.Add(item);
                                totalLoss += item.UnitCost * item.Quantity;
                            }
                        }
                    }

                    const string insertHeaderSql = @"
                        INSERT INTO StockDisposals (Reason, Status, TotalLossAmount, Note, CreatedBy)
                        VALUES (@Reason, 'COMPLETED', @Total, @Note, @CreatedBy);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    int disposalId;
                    using (var command = new SqlCommand(insertHeaderSql, connection, transaction))
                    {
                        command.Parameters.Add("@Reason", SqlDbType.VarChar, 20).Value = disposal.Reason;
                        AddMoney(command, "@Total", totalLoss);
                        command.Parameters.Add("@Note", SqlDbType.NVarChar, 500).Value =
                            string.IsNullOrWhiteSpace(disposal.Note) ? (object)DBNull.Value : disposal.Note.Trim();
                        command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = disposal.CreatedBy;
                        disposalId = Convert.ToInt32(command.ExecuteScalar(), CultureInfo.InvariantCulture);
                    }

                    using (var command = new SqlCommand(
                        "SELECT DisposalCode FROM StockDisposals WHERE DisposalID = @DisposalID", connection, transaction))
                    {
                        command.Parameters.Add("@DisposalID", SqlDbType.Int).Value = disposalId;
                        disposalCode = Convert.ToString(command.ExecuteScalar(), CultureInfo.InvariantCulture);
                    }

                    const string insertDetailSql = @"
                        INSERT INTO StockDisposalDetails (DisposalID, ProductID, BatchID, Quantity, UnitCost)
                        VALUES (@DisposalID, @ProductID, @BatchID, @Quantity, @UnitCost)";
                    const string updateBatchSql = @"
                        UPDATE InventoryBatch
                        SET RemainingQty = RemainingQty - @Quantity,
                            Status = CASE WHEN RemainingQty - @Quantity = 0 THEN 'DEPLETED' ELSE Status END
                        WHERE BatchID = @BatchID AND RemainingQty >= @Quantity";
                    const string updateStockSql = @"
                        DECLARE @Before INT;
                        SELECT @Before = Stock FROM Products WITH (UPDLOCK, ROWLOCK) WHERE ProductID = @ProductID;
                        IF @Before IS NULL OR @Before < @Quantity
                        BEGIN
                            SELECT CAST(-1 AS INT);
                            RETURN;
                        END
                        UPDATE Products SET Stock = Stock - @Quantity WHERE ProductID = @ProductID;
                        INSERT INTO InventoryTransactions (ProductID, TransactionType, Direction, Quantity,
                                                           StockBefore, StockAfter, RefTable, RefID, CreatedBy, Note)
                        VALUES (@ProductID, 'DISPOSAL', 'OUT', @Quantity, @Before, @Before - @Quantity,
                                'StockDisposals', @DisposalID, @CreatedBy, @Note);
                        SELECT @Before;";
                    string transactionNote = "Tiêu hủy " + DisposalReasons.ToLabel(disposal.Reason) + " " + disposalCode;

                    foreach (PreparedLine line in prepared)
                    {
                        using (var command = new SqlCommand(insertDetailSql, connection, transaction))
                        {
                            command.Parameters.Add("@DisposalID", SqlDbType.Int).Value = disposalId;
                            command.Parameters.Add("@ProductID", SqlDbType.Int).Value = line.ProductId;
                            command.Parameters.Add("@BatchID", SqlDbType.Int).Value = line.BatchId;
                            command.Parameters.Add("@Quantity", SqlDbType.Int).Value = line.Quantity;
                            AddMoney(command, "@UnitCost", line.UnitCost);
                            command.ExecuteNonQuery();
                        }

                        using (var command = new SqlCommand(updateBatchSql, connection, transaction))
                        {
                            command.Parameters.Add("@BatchID", SqlDbType.Int).Value = line.BatchId;
                            command.Parameters.Add("@Quantity", SqlDbType.Int).Value = line.Quantity;
                            if (command.ExecuteNonQuery() == 0)
                                throw new InvalidOperationException("Không trừ được tồn của lô. Vui lòng tải lại và thử lại.");
                        }

                        using (var command = new SqlCommand(updateStockSql, connection, transaction))
                        {
                            command.Parameters.Add("@ProductID", SqlDbType.Int).Value = line.ProductId;
                            command.Parameters.Add("@Quantity", SqlDbType.Int).Value = line.Quantity;
                            command.Parameters.Add("@DisposalID", SqlDbType.Int).Value = disposalId;
                            command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = disposal.CreatedBy;
                            command.Parameters.Add("@Note", SqlDbType.NVarChar, 255).Value = transactionNote;
                            int before = Convert.ToInt32(command.ExecuteScalar(), CultureInfo.InvariantCulture);
                            if (before < 0)
                                throw new InvalidOperationException("Tồn kho của sản phẩm không đủ để tiêu hủy.");
                        }
                    }

                    transaction.Commit();
                    return disposalId;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        private sealed class PreparedLine
        {
            public int BatchId { get; set; }
            public int ProductId { get; set; }
            public int Quantity { get; set; }
            public decimal UnitCost { get; set; }
        }

        private SqlConnection Open()
        {
            var connection = _connections.CreateConnection();
            connection.Open();
            return connection;
        }

        private static void AddMoney(SqlCommand command, string name, decimal value)
        {
            SqlParameter parameter = command.Parameters.Add(name, SqlDbType.Decimal);
            parameter.Precision = 18;
            parameter.Scale = 0;
            parameter.Value = value;
        }

        private static string ReadString(IDataRecord reader, int index)
        {
            return reader.IsDBNull(index) ? null : Convert.ToString(reader.GetValue(index), CultureInfo.InvariantCulture);
        }

        private static int ReadInt(IDataRecord reader, int index)
        {
            return reader.IsDBNull(index) ? 0 : Convert.ToInt32(reader.GetValue(index), CultureInfo.InvariantCulture);
        }

        private static decimal ReadDecimal(IDataRecord reader, int index)
        {
            return reader.IsDBNull(index) ? 0m : Convert.ToDecimal(reader.GetValue(index), CultureInfo.InvariantCulture);
        }
    }
}
