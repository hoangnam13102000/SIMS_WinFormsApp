using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using SIMS_WinFormsApp.Infrastructure.Configuration;
using SIMS_WinFormsApp.Models.DTOs.Catalog;
using SIMS_WinFormsApp.Models.DTOs.Inventory;
using SIMS_WinFormsApp.Repositories.Interfaces;

namespace SIMS_WinFormsApp.Repositories.Implementations
{
    public sealed class InventoryRepository : IInventoryRepository
    {
        private readonly IDbConnectionFactory _connections;

        public InventoryRepository(IDbConnectionFactory connections)
        {
            _connections = connections ?? throw new ArgumentNullException(nameof(connections));
        }

        public IReadOnlyList<LookupItem> GetActiveSuppliers()
        {
            const string sql = @"
                SELECT SupplierID, SupplierName
                FROM Suppliers
                WHERE IsDeleted = 0
                ORDER BY SupplierName";
            return ReadLookup(sql);
        }

        public IReadOnlyList<LookupItem> GetActiveProducts()
        {
            const string sql = @"
                SELECT ProductID, ProductCode + N' - ' + ProductName
                FROM Products
                WHERE Status = 'ACTIVE'
                ORDER BY ProductName";
            return ReadLookup(sql);
        }

        public IReadOnlyList<LookupItem> GetCategories()
        {
            const string sql = @"
                SELECT CategoryID, CategoryName
                FROM Categories
                ORDER BY CategoryName";
            return ReadLookup(sql);
        }

        public int CreatePurchaseReceipt(PurchaseReceiptDto receipt, out string receiptCode)
        {
            if (receipt == null) throw new ArgumentNullException(nameof(receipt));
            if (receipt.Lines == null) throw new ArgumentException("Danh sách dòng nhập không được null.", nameof(receipt));

            receiptCode = null;
            using (var connection = Open())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string codePrefix = "PN-" + DateTime.Today.ToString("yyyyMMdd", CultureInfo.InvariantCulture) + "-";
                    const string sequenceSql = @"
                        SELECT ISNULL(MAX(CAST(RIGHT(ReceiptCode, 3) AS INT)), 0)
                        FROM PurchaseReceipts WITH (UPDLOCK, HOLDLOCK)
                        WHERE ReceiptCode LIKE @Pattern";

                    int sequence;
                    using (var command = new SqlCommand(sequenceSql, connection, transaction))
                    {
                        command.Parameters.Add("@Pattern", SqlDbType.VarChar, 30).Value =
                            codePrefix + "[0-9][0-9][0-9]";
                        sequence = Convert.ToInt32(command.ExecuteScalar(), CultureInfo.InvariantCulture) + 1;
                    }

                    if (sequence > 999)
                        throw new InvalidOperationException("Mã phiếu nhập trong ngày đã vượt quá 999.");

                    receiptCode = codePrefix + sequence.ToString("D3", CultureInfo.InvariantCulture);

                    const string insertReceiptSql = @"
                        INSERT INTO PurchaseReceipts (ReceiptCode, SupplierID, CreatedBy, TotalAmount)
                        VALUES (@ReceiptCode, @SupplierID, @CreatedBy, 0);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    int receiptId;
                    using (var command = new SqlCommand(insertReceiptSql, connection, transaction))
                    {
                        command.Parameters.Add("@ReceiptCode", SqlDbType.VarChar, 30).Value = receiptCode;
                        command.Parameters.Add("@SupplierID", SqlDbType.Int).Value = receipt.SupplierId;
                        command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = receipt.CreatedBy;
                        receiptId = Convert.ToInt32(command.ExecuteScalar(), CultureInfo.InvariantCulture);
                    }

                    const string insertDetailSql = @"
                        INSERT INTO PurchaseReceiptDetails
                            (ReceiptID, ProductID, Quantity, ImportPrice, LotNumber, ManufactureDate, ExpiryDate)
                        VALUES
                            (@ReceiptID, @ProductID, @Quantity, @ImportPrice, @LotNumber, @ManufactureDate, @ExpiryDate)";

                    foreach (PurchaseLineDto line in receipt.Lines)
                    {
                        if (line == null)
                            throw new ArgumentException("Dòng nhập không được null.", nameof(receipt));

                        using (var command = new SqlCommand(insertDetailSql, connection, transaction))
                        {
                            command.Parameters.Add("@ReceiptID", SqlDbType.Int).Value = receiptId;
                            command.Parameters.Add("@ProductID", SqlDbType.Int).Value = line.ProductId;
                            command.Parameters.Add("@Quantity", SqlDbType.Int).Value = line.Quantity;
                            SqlParameter importPrice = command.Parameters.Add("@ImportPrice", SqlDbType.Decimal);
                            importPrice.Precision = 18;
                            importPrice.Scale = 0;
                            importPrice.Value = line.ImportPrice;
                            command.Parameters.Add("@LotNumber", SqlDbType.NVarChar, 50).Value =
                                (object)line.LotNumber ?? DBNull.Value;
                            command.Parameters.Add("@ManufactureDate", SqlDbType.Date).Value =
                                (object)line.ManufactureDate ?? DBNull.Value;
                            command.Parameters.Add("@ExpiryDate", SqlDbType.Date).Value =
                                (object)line.ExpiryDate ?? DBNull.Value;
                            command.ExecuteNonQuery();
                        }
                    }

                    const string updateTotalSql = @"
                        UPDATE PurchaseReceipts
                        SET TotalAmount = (
                            SELECT ISNULL(SUM(Quantity * ImportPrice), 0)
                            FROM PurchaseReceiptDetails
                            WHERE ReceiptID = @ReceiptID)
                        WHERE ReceiptID = @ReceiptID";

                    using (var command = new SqlCommand(updateTotalSql, connection, transaction))
                    {
                        command.Parameters.Add("@ReceiptID", SqlDbType.Int).Value = receiptId;
                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    return receiptId;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public IReadOnlyList<PurchaseReceiptListItemDto> SearchPurchaseReceipts(
            string keyword, DateTime? fromDate, DateTime? toDate)
        {
            const string sql = @"
                SELECT r.ReceiptID, r.ReceiptCode, s.SupplierName, u.FullName AS CreatedByName,
                       r.CreatedAt, r.TotalAmount, r.Status,
                       (SELECT COUNT(*)
                        FROM PurchaseReceiptDetails d
                        WHERE d.ReceiptID = r.ReceiptID) AS ItemCount
                FROM PurchaseReceipts r
                INNER JOIN Suppliers s ON s.SupplierID = r.SupplierID
                INNER JOIN Users u ON u.UserID = r.CreatedBy
                WHERE (@HasKeyword = 0
                       OR r.ReceiptCode LIKE @Pattern
                       OR s.SupplierName LIKE @Pattern
                       OR u.FullName LIKE @Pattern
                       OR EXISTS (
                           SELECT 1
                           FROM PurchaseReceiptDetails d
                           INNER JOIN Products p ON p.ProductID = d.ProductID
                           LEFT JOIN InventoryBatch b ON b.ReceiptDetailID = d.ReceiptDetailID
                           WHERE d.ReceiptID = r.ReceiptID
                             AND (p.ProductCode LIKE @Pattern
                                  OR p.ProductName LIKE @Pattern
                                  OR d.LotNumber LIKE @Pattern
                                  OR b.BatchCode LIKE @Pattern)))
                  AND (@FromDate IS NULL OR r.CreatedAt >= @FromDate)
                  AND (@ToExclusive IS NULL OR r.CreatedAt < @ToExclusive)
                ORDER BY r.CreatedAt DESC, r.ReceiptID DESC";

            string normalizedKeyword = (keyword ?? string.Empty).Trim();
            string pattern = "%" + normalizedKeyword + "%";
            DateTime? from = fromDate.HasValue ? fromDate.Value.Date : (DateTime?)null;
            DateTime? toExclusive = toDate.HasValue
                ? toDate.Value.Date.AddDays(1)
                : (DateTime?)null;
            var rows = new List<PurchaseReceiptListItemDto>();

            using (var connection = Open())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@HasKeyword", SqlDbType.Bit).Value = normalizedKeyword.Length > 0;
                command.Parameters.Add("@Pattern", SqlDbType.NVarChar, 4000).Value = pattern;
                command.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = (object)from ?? DBNull.Value;
                command.Parameters.Add("@ToExclusive", SqlDbType.DateTime).Value =
                    (object)toExclusive ?? DBNull.Value;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rows.Add(new PurchaseReceiptListItemDto
                        {
                            ReceiptId = reader.GetInt32(0),
                            ReceiptCode = ReadString(reader, 1),
                            SupplierName = ReadString(reader, 2),
                            CreatedByName = ReadString(reader, 3),
                            CreatedAt = reader.GetDateTime(4),
                            TotalAmount = Convert.ToDecimal(reader.GetValue(5), CultureInfo.InvariantCulture),
                            Status = ReadString(reader, 6),
                            ItemCount = ReadInt(reader, 7)
                        });
                    }
                }
            }
            return rows;
        }

        public IReadOnlyList<PurchaseReceiptDetailLineDto> GetPurchaseReceiptDetails(int receiptId)
        {
            const string sql = @"
                SELECT p.ProductCode, p.ProductName, b.BatchCode, d.LotNumber,
                       d.ManufactureDate, d.ExpiryDate, d.Quantity, d.ImportPrice
                FROM PurchaseReceiptDetails d
                INNER JOIN Products p ON p.ProductID = d.ProductID
                LEFT JOIN InventoryBatch b ON b.ReceiptDetailID = d.ReceiptDetailID
                WHERE d.ReceiptID = @id
                ORDER BY d.ReceiptDetailID";
            var rows = new List<PurchaseReceiptDetailLineDto>();

            using (var connection = Open())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@id", SqlDbType.Int).Value = receiptId;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rows.Add(new PurchaseReceiptDetailLineDto
                        {
                            ProductCode = ReadString(reader, 0),
                            ProductName = ReadString(reader, 1),
                            BatchCode = ReadString(reader, 2),
                            LotNumber = ReadString(reader, 3),
                            ManufactureDate = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4),
                            ExpiryDate = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5),
                            Quantity = ReadInt(reader, 6),
                            ImportPrice = Convert.ToDecimal(reader.GetValue(7), CultureInfo.InvariantCulture)
                        });
                    }
                }
            }
            return rows;
        }

        public IReadOnlyList<string> GetPurchaseReceiptSuggestions()
        {
            const string sql = @"
                SELECT DISTINCT suggestions.Suggestion
                FROM (
                    SELECT CONVERT(NVARCHAR(200), r.ReceiptCode) AS Suggestion
                    FROM PurchaseReceipts r
                    UNION ALL
                    SELECT CONVERT(NVARCHAR(200), s.SupplierName)
                    FROM PurchaseReceipts r
                    INNER JOIN Suppliers s ON s.SupplierID = r.SupplierID
                    UNION ALL
                    SELECT CONVERT(NVARCHAR(200), u.FullName)
                    FROM PurchaseReceipts r
                    INNER JOIN Users u ON u.UserID = r.CreatedBy
                    UNION ALL
                    SELECT CONVERT(NVARCHAR(200), p.ProductCode)
                    FROM PurchaseReceiptDetails d
                    INNER JOIN Products p ON p.ProductID = d.ProductID
                    UNION ALL
                    SELECT CONVERT(NVARCHAR(200), p.ProductName)
                    FROM PurchaseReceiptDetails d
                    INNER JOIN Products p ON p.ProductID = d.ProductID
                    UNION ALL
                    SELECT CONVERT(NVARCHAR(200), d.LotNumber)
                    FROM PurchaseReceiptDetails d
                    UNION ALL
                    SELECT CONVERT(NVARCHAR(200), b.BatchCode)
                    FROM PurchaseReceiptDetails d
                    INNER JOIN InventoryBatch b ON b.ReceiptDetailID = d.ReceiptDetailID
                ) suggestions
                WHERE suggestions.Suggestion IS NOT NULL
                  AND LTRIM(RTRIM(suggestions.Suggestion)) <> N''
                ORDER BY suggestions.Suggestion";
            var suggestions = new List<string>();

            using (var connection = Open())
            using (var command = new SqlCommand(sql, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                    suggestions.Add(reader.GetString(0));
            }
            return suggestions;
        }

        public IReadOnlyList<StockRowDto> GetStock(string keyword, int? categoryId)
        {
            const string sql = @"
                SELECT p.ProductID, p.ProductCode, p.ProductName, c.CategoryName, p.Stock, p.MinStock
                FROM Products p
                INNER JOIN Categories c ON c.CategoryID = p.CategoryID
                WHERE p.Status = 'ACTIVE'
                  AND (@Keyword = N'' OR p.ProductName LIKE @Pattern OR p.ProductCode LIKE @Pattern)
                  AND (@CategoryID IS NULL OR p.CategoryID = @CategoryID)
                ORDER BY CASE WHEN p.Stock <= p.MinStock THEN 0 ELSE 1 END, p.ProductName";

            string normalizedKeyword = (keyword ?? string.Empty).Trim();
            var rows = new List<StockRowDto>();
            using (var connection = Open())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@Keyword", SqlDbType.NVarChar, 150).Value = normalizedKeyword;
                command.Parameters.Add("@Pattern", SqlDbType.NVarChar, 152).Value = "%" + normalizedKeyword + "%";
                command.Parameters.Add("@CategoryID", SqlDbType.Int).Value = (object)categoryId ?? DBNull.Value;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rows.Add(new StockRowDto
                        {
                            ProductId = reader.GetInt32(0),
                            Code = ReadString(reader, 1),
                            Name = ReadString(reader, 2),
                            CategoryName = ReadString(reader, 3),
                            Stock = ReadInt(reader, 4),
                            MinStock = ReadInt(reader, 5)
                        });
                    }
                }
            }
            return rows;
        }

        public IReadOnlyList<StockAlertListItemDto> SearchStockAlerts(
            string keyword, int? categoryId, DateTime? from, DateTime? to)
        {
            const string sql = @"
                SELECT sa.AlertID, sa.ProductID, p.ProductCode, p.ProductName, p.CategoryID,
                       sa.AlertType, sa.StockAtReport, ISNULL(ru.FullName, N'Hệ thống'),
                       sa.CreatedAt, sa.Status, rv.FullName, sa.ResolvedAt, sa.Note
                FROM StockAlerts sa
                INNER JOIN Products p ON sa.ProductID = p.ProductID
                LEFT JOIN Users ru ON sa.ReportedBy = ru.UserID
                LEFT JOIN Users rv ON sa.ResolvedBy = rv.UserID
                WHERE (@HasKeyword = 0
                       OR p.ProductName LIKE @Pattern
                       OR p.ProductCode LIKE @Pattern)
                  AND (@CategoryID IS NULL OR p.CategoryID = @CategoryID)
                  AND (@FromDate IS NULL OR sa.CreatedAt >= @FromDate)
                  AND (@ToExclusive IS NULL OR sa.CreatedAt < @ToExclusive)
                ORDER BY sa.CreatedAt DESC, sa.AlertID DESC";

            string normalizedKeyword = (keyword ?? string.Empty).Trim();
            DateTime? fromDate = from.HasValue ? from.Value.Date : (DateTime?)null;
            DateTime? toExclusive = to.HasValue ? to.Value.Date.AddDays(1) : (DateTime?)null;
            var alerts = new List<StockAlertListItemDto>();

            using (var connection = Open())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@HasKeyword", SqlDbType.Bit).Value = normalizedKeyword.Length > 0;
                command.Parameters.Add("@Pattern", SqlDbType.NVarChar, 4000).Value =
                    "%" + normalizedKeyword + "%";
                command.Parameters.Add("@CategoryID", SqlDbType.Int).Value =
                    (object)categoryId ?? DBNull.Value;
                command.Parameters.Add("@FromDate", SqlDbType.DateTime).Value =
                    (object)fromDate ?? DBNull.Value;
                command.Parameters.Add("@ToExclusive", SqlDbType.DateTime).Value =
                    (object)toExclusive ?? DBNull.Value;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        alerts.Add(new StockAlertListItemDto
                        {
                            AlertId = reader.GetInt32(0),
                            ProductId = reader.GetInt32(1),
                            ProductCode = ReadString(reader, 2),
                            ProductName = ReadString(reader, 3),
                            CategoryId = reader.GetInt32(4),
                            AlertType = ReadString(reader, 5),
                            StockAtReport = ReadInt(reader, 6),
                            ReportedByName = ReadString(reader, 7),
                            CreatedAt = reader.GetDateTime(8),
                            Status = ReadString(reader, 9),
                            ResolvedByName = ReadString(reader, 10),
                            ResolvedAt = reader.IsDBNull(11) ? (DateTime?)null : reader.GetDateTime(11),
                            Note = ReadString(reader, 12)
                        });
                    }
                }
            }
            return alerts;
        }

        public int MarkAlertPlanned(int alertId)
        {
            const string sql = @"
                UPDATE StockAlerts
                SET Status = 'PLANNED'
                WHERE AlertID = @id AND Status = 'NEW'";
            using (var connection = Open())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@id", SqlDbType.Int).Value = alertId;
                return command.ExecuteNonQuery();
            }
        }

        public int ResolveAlert(int alertId, int userId)
        {
            const string sql = @"
                UPDATE StockAlerts
                SET Status = 'RESOLVED', ResolvedBy = @userId, ResolvedAt = GETDATE()
                WHERE AlertID = @id AND Status <> 'RESOLVED'";
            using (var connection = Open())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@id", SqlDbType.Int).Value = alertId;
                command.Parameters.Add("@userId", SqlDbType.Int).Value = userId;
                return command.ExecuteNonQuery();
            }
        }

        public int MarkAllAlertsSeen()
        {
            const string sql = @"
                UPDATE StockAlerts
                SET SeenByInventoryManager = 1
                WHERE SeenByInventoryManager = 0";
            using (var connection = Open())
            using (var command = new SqlCommand(sql, connection))
                return command.ExecuteNonQuery();
        }

        public void SaveStockCounts(IEnumerable<StockCountDto> counts, int createdBy)
        {
            if (counts == null) throw new ArgumentNullException(nameof(counts));

            const string sql = @"
                INSERT INTO StockReconciliation (ProductID, SystemStock, ActualStock, Note, CreatedBy)
                VALUES (@ProductID, @SystemStock, @ActualStock, @Note, @CreatedBy)";

            using (var connection = Open())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    foreach (StockCountDto count in counts)
                    {
                        if (count == null)
                            throw new ArgumentException("Dòng kiểm kê không được null.", nameof(counts));

                        using (var command = new SqlCommand(sql, connection, transaction))
                        {
                            command.Parameters.Add("@ProductID", SqlDbType.Int).Value = count.ProductId;
                            command.Parameters.Add("@SystemStock", SqlDbType.Int).Value = count.SystemStock;
                            command.Parameters.Add("@ActualStock", SqlDbType.Int).Value = count.ActualStock;
                            command.Parameters.Add("@Note", SqlDbType.NVarChar, 255).Value =
                                (object)count.Note ?? DBNull.Value;
                            command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = createdBy;
                            command.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        private SqlConnection Open()
        {
            var connection = _connections.CreateConnection();
            connection.Open();
            return connection;
        }

        private IReadOnlyList<LookupItem> ReadLookup(string sql)
        {
            var items = new List<LookupItem>();
            using (var connection = Open())
            using (var command = new SqlCommand(sql, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                    items.Add(new LookupItem
                    {
                        Id = reader.GetInt32(0),
                        Name = ReadString(reader, 1)
                    });
            }
            return items;
        }

        private static string ReadString(IDataRecord reader, int index)
        {
            return reader.IsDBNull(index) ? null : reader.GetString(index);
        }

        private static int ReadInt(IDataRecord reader, int index)
        {
            return reader.IsDBNull(index) ? 0 : Convert.ToInt32(reader.GetValue(index));
        }
    }
}