using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SIMS_WinFormsApp.Infrastructure.Configuration;
using SIMS_WinFormsApp.Models.DTOs.Inventory;
using SIMS_WinFormsApp.Repositories.Interfaces;

namespace SIMS_WinFormsApp.Repositories.Implementations
{
    public sealed class StockReconciliationRepository : IStockReconciliationRepository
    {
        private readonly IDbConnectionFactory _connections;

        public StockReconciliationRepository(IDbConnectionFactory connections)
        {
            _connections = connections ?? throw new ArgumentNullException(nameof(connections));
        }

        // Tạo phiên hôm nay: mỗi sản phẩm đang bán, còn tồn, chưa có dòng hôm nay thì thêm 1 dòng.
        // Tồn thực tế = tồn kho nên trigger trg_StockReconciliation_Apply không đổi tồn.
        public int CreateTodaySession(int createdBy)
        {
            const string sql = @"
                SET NOCOUNT ON;
                DECLARE @Today DATETIME = CAST(CAST(GETDATE() AS DATE) AS DATETIME);
                DECLARE @New TABLE (ProductID INT, Stock INT);

                INSERT INTO @New (ProductID, Stock)
                SELECT p.ProductID, p.Stock
                FROM Products p WITH (UPDLOCK)
                INNER JOIN Categories c ON c.CategoryID = p.CategoryID
                WHERE p.Status = 'ACTIVE' AND c.Status = 'ACTIVE' AND p.Stock > 0
                  AND NOT EXISTS (
                      SELECT 1 FROM StockReconciliation r WITH (UPDLOCK, HOLDLOCK)
                      WHERE r.ProductID = p.ProductID
                        AND r.CreatedAt >= @Today AND r.CreatedAt < DATEADD(DAY, 1, @Today));

                INSERT INTO StockReconciliation (ProductID, SystemStock, ActualStock, Note, CreatedBy)
                SELECT ProductID, Stock, Stock, NULL, @CreatedBy FROM @New;

                SELECT COUNT(*) FROM @New;";

            using (var connection = Open())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    int created;
                    using (var command = new SqlCommand(sql, connection, transaction))
                    {
                        command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = createdBy;
                        created = Convert.ToInt32(command.ExecuteScalar());
                    }
                    transaction.Commit();
                    return created;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        // Dòng hôm nay còn "Khớp" mà tồn sản phẩm đã đổi (bán, nhập, tiêu hủy...) thì
        // lấy lại tồn mới. Dòng đang lệch thì giữ nguyên số bạn đã đếm.
        public int SyncTodayWithStock()
        {
            const string sql = @"
                DECLARE @Today DATETIME = CAST(CAST(GETDATE() AS DATE) AS DATETIME);
                UPDATE r
                SET r.SystemStock = p.Stock,
                    r.ActualStock = p.Stock
                FROM StockReconciliation r
                INNER JOIN Products p ON p.ProductID = r.ProductID
                WHERE r.CreatedAt >= @Today AND r.CreatedAt < DATEADD(DAY, 1, @Today)
                  AND r.ActualStock = r.SystemStock
                  AND r.SystemStock <> p.Stock;";

            using (var connection = Open())
            using (var command = new SqlCommand(sql, connection))
            {
                return command.ExecuteNonQuery();
            }
        }

        public IReadOnlyList<StockReconciliationRowDto> Search(string keyword, DateTime? from, DateTime? to)
        {
            const string sql = @"
                SELECT r.ReconciliationID, r.ProductID, p.ProductCode, p.ProductName, c.CategoryName,
                       r.SystemStock, r.ActualStock, p.Stock, r.Note, r.CreatedAt,
                       (SELECT MAX(t.CreatedAt) FROM InventoryTransactions t
                        WHERE t.RefTable = 'StockReconciliation' AND t.RefID = r.ReconciliationID),
                       u.FullName
                FROM StockReconciliation r
                INNER JOIN Products p ON p.ProductID = r.ProductID
                INNER JOIN Categories c ON c.CategoryID = p.CategoryID
                INNER JOIN Users u ON u.UserID = r.CreatedBy
                WHERE r.SystemStock <> 0
                  AND (@Keyword = N'' OR p.ProductName LIKE @Pattern OR p.ProductCode LIKE @Pattern
                       OR c.CategoryName LIKE @Pattern OR r.Note LIKE @Pattern)
                  AND (@From IS NULL OR r.CreatedAt >= @From)
                  AND (@ToNext IS NULL OR r.CreatedAt < @ToNext)
                ORDER BY CAST(r.CreatedAt AS DATE) DESC,
                         CASE WHEN r.ActualStock <> r.SystemStock THEN 0 ELSE 1 END,
                         p.ProductName, r.ReconciliationID DESC";

            string normalizedKeyword = (keyword ?? string.Empty).Trim();
            var rows = new List<StockReconciliationRowDto>();
            using (var connection = Open())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@Keyword", SqlDbType.NVarChar, 150).Value = normalizedKeyword;
                command.Parameters.Add("@Pattern", SqlDbType.NVarChar, 152).Value = "%" + normalizedKeyword + "%";
                command.Parameters.Add("@From", SqlDbType.DateTime).Value =
                    from.HasValue ? (object)from.Value.Date : DBNull.Value;
                command.Parameters.Add("@ToNext", SqlDbType.DateTime).Value =
                    to.HasValue ? (object)to.Value.Date.AddDays(1) : DBNull.Value;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rows.Add(new StockReconciliationRowDto
                        {
                            ReconciliationId = reader.GetInt32(0),
                            ProductId = reader.GetInt32(1),
                            ProductCode = ReadString(reader, 2),
                            ProductName = ReadString(reader, 3),
                            CategoryName = ReadString(reader, 4),
                            SystemStock = reader.GetInt32(5),
                            ActualStock = reader.GetInt32(6),
                            CurrentStock = reader.GetInt32(7),
                            Note = ReadString(reader, 8),
                            CreatedAt = reader.GetDateTime(9),
                            UpdatedAt = reader.IsDBNull(10) ? (DateTime?)null : reader.GetDateTime(10),
                            CreatedByName = ReadString(reader, 11)
                        });
                    }
                }
            }
            return rows;
        }

        // Sửa tồn thực tế / lý do của dòng hôm nay. Phần chênh so với lần đếm trước được
        // cộng/trừ vào Products.Stock ngay và ghi InventoryTransactions (RECONCILE_ADJUST).
        public StockCountUpdateStatus UpdateCount(int reconciliationId, int actualStock, string note, int userId)
        {
            const string selectRowSql = @"
                SELECT ProductID, SystemStock, ActualStock, CreatedAt
                FROM StockReconciliation WITH (UPDLOCK)
                WHERE ReconciliationID = @ID";
            const string selectStockSql = @"
                SELECT Stock FROM Products WITH (UPDLOCK) WHERE ProductID = @ProductID";
            const string updateRowSql = @"
                UPDATE StockReconciliation SET ActualStock = @Actual, Note = @Note
                WHERE ReconciliationID = @ID";
            const string applySql = @"
                UPDATE Products SET Stock = @After WHERE ProductID = @ProductID;
                INSERT INTO InventoryTransactions (ProductID, TransactionType, Direction, Quantity,
                    StockBefore, StockAfter, RefTable, RefID, CreatedBy, Note)
                VALUES (@ProductID, 'RECONCILE_ADJUST', @Direction, @Quantity,
                    @Before, @After, 'StockReconciliation', @ID, @UserID, @Note);";

            object noteValue = string.IsNullOrWhiteSpace(note) ? (object)DBNull.Value : note.Trim();

            using (var connection = Open())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    int productId, systemStock, oldActual;
                    DateTime createdAt;
                    using (var command = new SqlCommand(selectRowSql, connection, transaction))
                    {
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = reconciliationId;
                        using (var reader = command.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                reader.Close();
                                transaction.Rollback();
                                return StockCountUpdateStatus.NotFound;
                            }
                            productId = reader.GetInt32(0);
                            systemStock = reader.GetInt32(1);
                            oldActual = reader.GetInt32(2);
                            createdAt = reader.GetDateTime(3);
                        }
                    }

                    if (createdAt.Date != DateTime.Today)
                    {
                        transaction.Rollback();
                        return StockCountUpdateStatus.Locked;
                    }
                    if (actualStock < 0 || actualStock > systemStock)
                    {
                        transaction.Rollback();
                        return StockCountUpdateStatus.OutOfRange;
                    }

                    int delta = actualStock - oldActual;
                    int before = 0;
                    if (delta != 0)
                    {
                        using (var command = new SqlCommand(selectStockSql, connection, transaction))
                        {
                            command.Parameters.Add("@ProductID", SqlDbType.Int).Value = productId;
                            before = Convert.ToInt32(command.ExecuteScalar());
                        }
                        if (before + delta < 0)
                        {
                            transaction.Rollback();
                            return StockCountUpdateStatus.NotEnoughStock;
                        }
                    }

                    using (var command = new SqlCommand(updateRowSql, connection, transaction))
                    {
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = reconciliationId;
                        command.Parameters.Add("@Actual", SqlDbType.Int).Value = actualStock;
                        command.Parameters.Add("@Note", SqlDbType.NVarChar, 255).Value = noteValue;
                        command.ExecuteNonQuery();
                    }

                    if (delta != 0)
                    {
                        using (var command = new SqlCommand(applySql, connection, transaction))
                        {
                            command.Parameters.Add("@ID", SqlDbType.Int).Value = reconciliationId;
                            command.Parameters.Add("@ProductID", SqlDbType.Int).Value = productId;
                            command.Parameters.Add("@Direction", SqlDbType.VarChar, 3).Value = delta > 0 ? "IN" : "OUT";
                            command.Parameters.Add("@Quantity", SqlDbType.Int).Value = Math.Abs(delta);
                            command.Parameters.Add("@Before", SqlDbType.Int).Value = before;
                            command.Parameters.Add("@After", SqlDbType.Int).Value = before + delta;
                            command.Parameters.Add("@UserID", SqlDbType.Int).Value = userId;
                            command.Parameters.Add("@Note", SqlDbType.NVarChar, 255).Value = noteValue;
                            command.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                    return StockCountUpdateStatus.Ok;
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

        private static string ReadString(IDataRecord reader, int index)
        {
            return reader.IsDBNull(index) ? null : reader.GetString(index);
        }
    }
}