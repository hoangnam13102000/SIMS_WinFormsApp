using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using SIMS_WinFormsApp.Infrastructure.Configuration;
using SIMS_WinFormsApp.Models.DTOs.Inventory;

namespace SIMS_WinFormsApp.Services.Implementations.Inventory
{
    public sealed class ExpiryBatchManagementService
    {
        private readonly IDbConnectionFactory _connections;

        public ExpiryBatchManagementService(IDbConnectionFactory connections)
        {
            _connections = connections ?? throw new ArgumentNullException(nameof(connections));
        }

        public IReadOnlyList<ExpiryBatchDto> GetExpiryBatches(string keyword, ExpiryBatchFilter filter)
        {
            if (!Enum.IsDefined(typeof(ExpiryBatchFilter), filter))
                throw new ArgumentOutOfRangeException(nameof(filter));

            const string sql = @"
                SELECT b.BatchID, b.BatchCode, p.ProductCode, p.ProductName, b.LotNumber,
                       s.SupplierName, b.ExpiryDate, b.RemainingQty, b.ImportPrice
                FROM InventoryBatch b
                INNER JOIN Products p ON p.ProductID = b.ProductID
                INNER JOIN Suppliers s ON s.SupplierID = b.SupplierID
                WHERE b.ExpiryDate IS NOT NULL
                    AND b.RemainingQty > 0
                    AND (@Filter = 0
                        OR (@Filter = 1 AND b.ExpiryDate >= CONVERT(date, GETDATE())
                            AND b.ExpiryDate < DATEADD(day, 31, CONVERT(date, GETDATE())))
                        OR (@Filter = 2 AND b.ExpiryDate < CONVERT(date, GETDATE())))
                    AND (@Keyword = N'' OR p.ProductCode LIKE @Pattern
                        OR p.ProductName LIKE @Pattern OR b.BatchCode LIKE @Pattern
                        OR b.LotNumber LIKE @Pattern OR s.SupplierName LIKE @Pattern)
                ORDER BY b.ExpiryDate, p.ProductName, b.BatchID";

            string normalizedKeyword = (keyword ?? string.Empty).Trim();
            using (SqlConnection connection = _connections.CreateConnection())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@Filter", SqlDbType.Int).Value = (int)filter;
                command.Parameters.Add("@Keyword", SqlDbType.NVarChar, 200).Value = normalizedKeyword;
                command.Parameters.Add("@Pattern", SqlDbType.NVarChar, 402).Value =
                    "%" + normalizedKeyword + "%";
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var rows = new List<ExpiryBatchDto>();
                    while (reader.Read())
                    {
                        rows.Add(new ExpiryBatchDto
                        {
                            BatchId = reader.GetInt32(0),
                            BatchCode = reader.GetString(1),
                            ProductCode = reader.GetString(2),
                            ProductName = reader.GetString(3),
                            LotNumber = reader.IsDBNull(4) ? "-" : reader.GetString(4),
                            SupplierName = reader.GetString(5),
                            ExpiryDate = reader.GetDateTime(6),
                            RemainingQuantity = reader.GetInt32(7),
                            UnitCost = reader.GetDecimal(8)
                        });
                    }

                    return rows;
                }
            }
        }

        public string DisposeExpiredBatch(int batchId, int userId)
        {
            if (batchId <= 0)
                throw new ArgumentOutOfRangeException(nameof(batchId));
            if (userId <= 0)
                throw new ArgumentOutOfRangeException(nameof(userId));

            using (SqlConnection connection = _connections.CreateConnection())
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction(IsolationLevel.Serializable))
                {
                    try
                    {
                        int productId;
                        int quantity;
                        decimal unitCost;
                        string productName;
                        DateTime expiryDate;
                        const string batchSql = @"
                            SELECT b.ProductID, b.RemainingQty, b.ImportPrice, p.ProductName, b.ExpiryDate
                            FROM InventoryBatch b WITH (UPDLOCK, ROWLOCK)
                            INNER JOIN Products p WITH (UPDLOCK, ROWLOCK) ON p.ProductID = b.ProductID
                            WHERE b.BatchID = @BatchID";
                        using (var command = new SqlCommand(batchSql, connection, transaction))
                        {
                            command.Parameters.Add("@BatchID", SqlDbType.Int).Value = batchId;
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (!reader.Read())
                                    throw new InvalidOperationException("Không tìm thấy lô hàng cần xử lý.");
                                productId = reader.GetInt32(0);
                                quantity = reader.GetInt32(1);
                                unitCost = reader.GetDecimal(2);
                                productName = reader.GetString(3);
                                expiryDate = reader.IsDBNull(4) ? DateTime.MaxValue : reader.GetDateTime(4);
                            }
                        }

                        if (expiryDate.Date >= DateTime.Today)
                            throw new InvalidOperationException("Chỉ được tiêu hủy lô hàng đã hết hạn.");
                        if (quantity <= 0)
                            throw new InvalidOperationException("Lô hàng này không còn số lượng để xử lý.");

                        decimal lossAmount = quantity * unitCost;
                        int disposalId;
                        const string createDisposalSql = @"
                            INSERT INTO StockDisposals (Reason, Status, TotalLossAmount, Note, CreatedBy)
                            VALUES ('EXPIRED', 'COMPLETED', @LossAmount, @Note, @CreatedBy);
                            SELECT CAST(SCOPE_IDENTITY() AS INT);";
                        using (var command = new SqlCommand(createDisposalSql, connection, transaction))
                        {
                            command.Parameters.Add("@LossAmount", SqlDbType.Decimal).Value = lossAmount;
                            command.Parameters["@LossAmount"].Precision = 18;
                            command.Parameters["@LossAmount"].Scale = 0;
                            command.Parameters.Add("@Note", SqlDbType.NVarChar, 500).Value =
                                "Tiêu hủy toàn bộ số lượng còn lại của lô " + batchId
                                + " đã hết hạn ngày " + expiryDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                            command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = userId;
                            disposalId = Convert.ToInt32(command.ExecuteScalar(), CultureInfo.InvariantCulture);
                        }

                        const string detailSql = @"
                            INSERT INTO StockDisposalDetails (DisposalID, ProductID, BatchID, Quantity, UnitCost)
                            VALUES (@DisposalID, @ProductID, @BatchID, @Quantity, @UnitCost)";
                        using (var command = new SqlCommand(detailSql, connection, transaction))
                        {
                            command.Parameters.Add("@DisposalID", SqlDbType.Int).Value = disposalId;
                            command.Parameters.Add("@ProductID", SqlDbType.Int).Value = productId;
                            command.Parameters.Add("@BatchID", SqlDbType.Int).Value = batchId;
                            command.Parameters.Add("@Quantity", SqlDbType.Int).Value = quantity;
                            command.Parameters.Add("@UnitCost", SqlDbType.Decimal).Value = unitCost;
                            command.Parameters["@UnitCost"].Precision = 18;
                            command.Parameters["@UnitCost"].Scale = 0;
                            command.ExecuteNonQuery();
                        }

                        int stockBefore;
                        int stockAfter;
                        const string updateProductSql = @"
                            UPDATE Products
                            SET Stock = Stock - @Quantity
                            OUTPUT DELETED.Stock, INSERTED.Stock
                            WHERE ProductID = @ProductID AND Stock >= @Quantity";
                        using (var command = new SqlCommand(updateProductSql, connection, transaction))
                        {
                            command.Parameters.Add("@Quantity", SqlDbType.Int).Value = quantity;
                            command.Parameters.Add("@ProductID", SqlDbType.Int).Value = productId;
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (!reader.Read())
                                    throw new InvalidOperationException(
                                        "Tồn kho sản phẩm không khớp với số lượng lô; không thể tiêu hủy.");
                                stockBefore = reader.GetInt32(0);
                                stockAfter = reader.GetInt32(1);
                            }
                        }

                        const string updateBatchSql = @"
                            UPDATE InventoryBatch
                            SET RemainingQty = 0, Status = 'DEPLETED'
                            WHERE BatchID = @BatchID AND RemainingQty = @Quantity";
                        using (var command = new SqlCommand(updateBatchSql, connection, transaction))
                        {
                            command.Parameters.Add("@BatchID", SqlDbType.Int).Value = batchId;
                            command.Parameters.Add("@Quantity", SqlDbType.Int).Value = quantity;
                            if (command.ExecuteNonQuery() != 1)
                                throw new InvalidOperationException("Số lượng lô đã thay đổi; hãy tải lại danh sách.");
                        }

                        const string transactionSql = @"
                            INSERT INTO InventoryTransactions
                                (ProductID, TransactionType, Direction, Quantity, StockBefore, StockAfter,
                                 RefTable, RefID, CreatedBy, Note)
                            VALUES (@ProductID, 'DISPOSAL', 'OUT', @Quantity, @StockBefore, @StockAfter,
                                    'StockDisposals', @DisposalID, @CreatedBy, @Note)";
                        using (var command = new SqlCommand(transactionSql, connection, transaction))
                        {
                            command.Parameters.Add("@ProductID", SqlDbType.Int).Value = productId;
                            command.Parameters.Add("@Quantity", SqlDbType.Int).Value = quantity;
                            command.Parameters.Add("@StockBefore", SqlDbType.Int).Value = stockBefore;
                            command.Parameters.Add("@StockAfter", SqlDbType.Int).Value = stockAfter;
                            command.Parameters.Add("@DisposalID", SqlDbType.Int).Value = disposalId;
                            command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = userId;
                            command.Parameters.Add("@Note", SqlDbType.NVarChar, 255).Value =
                                "Tiêu hủy lô hết hạn của " + productName;
                            command.ExecuteNonQuery();
                        }

                        const string auditSql = @"
                            INSERT INTO AuditLogs (UserID, Action, TableName, RecordID, OldValue, NewValue, Detail)
                            VALUES (@UserID, 'DATA_CREATE', 'StockDisposals', @RecordID, @OldValue, @NewValue, @Detail)";
                        using (var command = new SqlCommand(auditSql, connection, transaction))
                        {
                            command.Parameters.Add("@UserID", SqlDbType.Int).Value = userId;
                            command.Parameters.Add("@RecordID", SqlDbType.Int).Value = disposalId;
                            command.Parameters.Add("@OldValue", SqlDbType.NVarChar, -1).Value =
                                JsonConvert.SerializeObject(new { BatchId = batchId, RemainingQuantity = quantity });
                            command.Parameters.Add("@NewValue", SqlDbType.NVarChar, -1).Value =
                                JsonConvert.SerializeObject(new { BatchId = batchId, RemainingQuantity = 0 });
                            command.Parameters.Add("@Detail", SqlDbType.NVarChar, -1).Value =
                                "Đã tiêu hủy lô " + batchId + " của sản phẩm " + productName
                                + ", số lượng " + quantity.ToString(CultureInfo.InvariantCulture)
                                + ", thiệt hại " + lossAmount.ToString(CultureInfo.InvariantCulture) + ".";
                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        return "TH_" + disposalId.ToString("D6", CultureInfo.InvariantCulture);
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }

    public enum ExpiryBatchFilter
    {
        All,
        NearExpiry,
        Expired
    }
}
