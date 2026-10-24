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