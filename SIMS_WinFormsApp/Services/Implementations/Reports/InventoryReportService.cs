using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using SIMS_WinFormsApp.DAL.Linq;
using SIMS_WinFormsApp.Infrastructure.Configuration;

namespace SIMS_WinFormsApp.Services.Implementations.Reports
{
    public sealed class InventoryReportData
    {
        public DateTime SnapshotDate { get; set; }
        public string BusinessName { get; set; }
        public string DirectorName { get; set; }
        public string InventoryManagerName { get; set; }
        public int ProductCount { get; set; }
        public long TotalQuantity { get; set; }
        public decimal TotalStockValue { get; set; }
        public int LowStockProductCount { get; set; }
        public long ExpiredQuantity { get; set; }
        public List<InventoryReportProduct> Products { get; } = new List<InventoryReportProduct>();
        public List<InventoryImportTrendPoint> ImportTrend { get; } = new List<InventoryImportTrendPoint>();
    }

    public sealed class InventoryReportCategory
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
    }

    public sealed class InventoryReportProduct
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public int Stock { get; set; }
        public int MinStock { get; set; }
        public decimal ImportPrice { get; set; }
        public decimal StockValue { get; set; }
    }

    public sealed class InventoryImportTrendPoint
    {
        public DateTime Date { get; set; }
        public long Quantity { get; set; }
    }

    public sealed class InventoryStockSummary
    {
        public int ProductCount { get; set; }
        public long TotalQuantity { get; set; }
        public decimal SellValue { get; set; }
        public decimal ImportValue { get; set; }
        public int LowStockCount { get; set; }
        public int OutOfStockCount { get; set; }
    }

    public sealed class InventoryProductStockPoint
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int Stock { get; set; }
        public int MinStock { get; set; }
        public DateTime? NearestExpiry { get; set; }
        public bool HasExpired { get; set; }
    }

    public sealed class InventoryStockBreakdownRow
    {
        public string Label { get; set; }
        public int ProductCount { get; set; }
        public long Quantity { get; set; }
        public decimal SellValue { get; set; }
        public double Ratio { get; set; }
    }

    public sealed class InventoryCategoryTrendSeries
    {
        public string CategoryName { get; set; }
        public List<long> Quantities { get; } = new List<long>();
    }

    public sealed class InventoryMonthlyTrend
    {
        public List<DateTime> Months { get; } = new List<DateTime>();
        public List<InventoryCategoryTrendSeries> Series { get; } = new List<InventoryCategoryTrendSeries>();
    }

    public sealed class InventoryBatchHistoryRow
    {
        public int BatchID { get; set; }
        public string BatchCode { get; set; }
        public string LotNumber { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime ChangedAt { get; set; }
        public string DocumentType { get; set; }
        public string DocumentCode { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string Direction { get; set; }
        public int StockBefore { get; set; }
        public int StockAfter { get; set; }
        public string Note { get; set; }
    }

    public sealed class InventoryReportService
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public InventoryReportService(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public InventoryStockSummary GetStockSummary(int? categoryId)
        {
            const string sql = @"
SELECT COUNT(*) AS ProductCount,
       ISNULL(SUM(CAST(p.Stock AS BIGINT)), 0) AS TotalQuantity,
       ISNULL(SUM(CAST(p.Stock AS DECIMAL(18,0)) * p.SellPrice), 0) AS SellValue,
       ISNULL(SUM(CAST(p.Stock AS DECIMAL(18,0)) * p.ImportPrice), 0) AS ImportValue,
       ISNULL(SUM(CASE WHEN p.Stock > 0 AND p.Stock <= p.MinStock THEN 1 ELSE 0 END), 0) AS LowStockCount,
       ISNULL(SUM(CASE WHEN p.Stock = 0 THEN 1 ELSE 0 END), 0) AS OutOfStockCount
FROM Products p
WHERE p.Status = 'ACTIVE'
  AND (@CategoryID IS NULL OR p.CategoryID = @CategoryID);";

            using (SqlConnection cn = _connectionFactory.CreateConnection())
            {
                if (cn.State != ConnectionState.Open)
                    cn.Open();
                using (var command = new SqlCommand(sql, cn))
                {
                    command.Parameters.Add("@CategoryID", SqlDbType.Int).Value =
                        categoryId.HasValue ? (object)categoryId.Value : DBNull.Value;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                            return new InventoryStockSummary();

                        return new InventoryStockSummary
                        {
                            ProductCount = reader.GetInt32(0),
                            TotalQuantity = reader.GetInt64(1),
                            SellValue = reader.GetDecimal(2),
                            ImportValue = reader.GetDecimal(3),
                            LowStockCount = reader.GetInt32(4),
                            OutOfStockCount = reader.GetInt32(5)
                        };
                    }
                }
            }
        }

        public List<InventoryProductStockPoint> GetTopProductStock(int? categoryId, int top = 20)
        {
            const string sql = @"
SELECT TOP (@Top) p.ProductCode, p.ProductName, p.Stock, p.MinStock,
       (SELECT MIN(b.ExpiryDate)
        FROM InventoryBatch b
        WHERE b.ProductID = p.ProductID AND b.Status <> 'DEPLETED'
          AND b.RemainingQty > 0 AND b.ExpiryDate IS NOT NULL) AS NearestExpiry,
       CASE WHEN EXISTS
       (
           SELECT 1
           FROM InventoryBatch b
           WHERE b.ProductID = p.ProductID AND b.Status <> 'DEPLETED'
             AND b.RemainingQty > 0 AND b.ExpiryDate < CAST(GETDATE() AS DATE)
       ) THEN 1 ELSE 0 END AS HasExpired
FROM Products p
WHERE p.Status = 'ACTIVE'
  AND (@CategoryID IS NULL OR p.CategoryID = @CategoryID)
ORDER BY p.Stock DESC, p.ProductName;";

            var results = new List<InventoryProductStockPoint>();
            using (SqlConnection cn = _connectionFactory.CreateConnection())
            {
                if (cn.State != ConnectionState.Open)
                    cn.Open();
                using (var command = new SqlCommand(sql, cn))
                {
                    command.Parameters.Add("@Top", SqlDbType.Int).Value = Math.Max(0, top);
                    command.Parameters.Add("@CategoryID", SqlDbType.Int).Value =
                        categoryId.HasValue ? (object)categoryId.Value : DBNull.Value;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(new InventoryProductStockPoint
                            {
                                ProductCode = reader.GetString(0),
                                ProductName = reader.GetString(1),
                                Stock = reader.GetInt32(2),
                                MinStock = reader.GetInt32(3),
                                NearestExpiry = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4),
                                HasExpired = reader.GetInt32(5) != 0
                            });
                        }
                    }
                }
            }

            return results;
        }

        public List<InventoryStockBreakdownRow> GetStockByCategory(int? categoryId)
        {
            const string sql = @"
SELECT c.CategoryName AS Label,
       COUNT(p.ProductID) AS ProductCount,
       ISNULL(SUM(CAST(p.Stock AS BIGINT)), 0) AS Quantity,
       ISNULL(SUM(CAST(p.Stock AS DECIMAL(18,0)) * p.SellPrice), 0) AS SellValue
FROM Products p
JOIN Categories c ON c.CategoryID = p.CategoryID
WHERE p.Status = 'ACTIVE'
  AND (@CategoryID IS NULL OR p.CategoryID = @CategoryID)
GROUP BY c.CategoryName
ORDER BY SellValue DESC;";

            var results = new List<InventoryStockBreakdownRow>();
            using (SqlConnection cn = _connectionFactory.CreateConnection())
            {
                if (cn.State != ConnectionState.Open)
                    cn.Open();
                using (var command = new SqlCommand(sql, cn))
                {
                    command.Parameters.Add("@CategoryID", SqlDbType.Int).Value =
                        categoryId.HasValue ? (object)categoryId.Value : DBNull.Value;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(new InventoryStockBreakdownRow
                            {
                                Label = reader.GetString(0),
                                ProductCount = reader.GetInt32(1),
                                Quantity = reader.GetInt64(2),
                                SellValue = reader.GetDecimal(3)
                            });
                        }
                    }
                }
            }

            ApplyBreakdownRatios(results);
            return results;
        }

        public List<InventoryStockBreakdownRow> GetStockByPriceRange(int? categoryId)
        {
            const string sql = @"
SELECT p.SellPrice, p.Stock
FROM Products p
WHERE p.Status = 'ACTIVE'
  AND (@CategoryID IS NULL OR p.CategoryID = @CategoryID);";

            var results = new List<InventoryStockBreakdownRow>
            {
                new InventoryStockBreakdownRow { Label = "Dưới 50.000 đ" },
                new InventoryStockBreakdownRow { Label = "50.000 - dưới 100.000 đ" },
                new InventoryStockBreakdownRow { Label = "100.000 - dưới 200.000 đ" },
                new InventoryStockBreakdownRow { Label = "200.000 - dưới 500.000 đ" },
                new InventoryStockBreakdownRow { Label = "Từ 500.000 đ trở lên" }
            };

            using (SqlConnection cn = _connectionFactory.CreateConnection())
            {
                if (cn.State != ConnectionState.Open)
                    cn.Open();
                using (var command = new SqlCommand(sql, cn))
                {
                    command.Parameters.Add("@CategoryID", SqlDbType.Int).Value =
                        categoryId.HasValue ? (object)categoryId.Value : DBNull.Value;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            decimal sellPrice = reader.GetDecimal(0);
                            int stock = reader.GetInt32(1);
                            int index = sellPrice < 50000m ? 0
                                : sellPrice < 100000m ? 1
                                : sellPrice < 200000m ? 2
                                : sellPrice < 500000m ? 3
                                : 4;
                            InventoryStockBreakdownRow row = results[index];
                            row.ProductCount++;
                            row.Quantity += stock;
                            row.SellValue += sellPrice * stock;
                        }
                    }
                }
            }

            ApplyBreakdownRatios(results);
            return results;
        }

        public InventoryMonthlyTrend GetMonthlyCategoryTrend(DateTime fromMonth, DateTime toMonth)
        {
            DateTime start = new DateTime(fromMonth.Year, fromMonth.Month, 1);
            DateTime end = new DateTime(toMonth.Year, toMonth.Month, 1);
            if (start > end)
                throw new ArgumentException("Tháng bắt đầu không được sau tháng kết thúc.");

            var trend = new InventoryMonthlyTrend();
            for (DateTime month = start; month <= end; month = month.AddMonths(1))
                trend.Months.Add(month);

            var currentStocks = new Dictionary<int, int>();
            var productCategories = new Dictionary<int, string>();
            var historyByProduct = new Dictionary<int, List<KeyValuePair<DateTime, int>>>();
            const string productsSql = @"
SELECT p.ProductID, c.CategoryName, p.Stock
FROM Products p
JOIN Categories c ON c.CategoryID = p.CategoryID
WHERE p.Status = 'ACTIVE';";
            const string transactionsSql = @"
SELECT t.ProductID, t.CreatedAt, t.StockAfter
FROM InventoryTransactions t
WHERE t.CreatedAt < @EndExclusive
ORDER BY t.ProductID, t.CreatedAt, t.TransactionID;";

            using (SqlConnection cn = _connectionFactory.CreateConnection())
            {
                if (cn.State != ConnectionState.Open)
                    cn.Open();

                using (var command = new SqlCommand(productsSql, cn))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int productId = reader.GetInt32(0);
                        productCategories[productId] = reader.GetString(1);
                        currentStocks[productId] = reader.GetInt32(2);
                    }
                }

                using (var command = new SqlCommand(transactionsSql, cn))
                {
                    command.Parameters.Add("@EndExclusive", SqlDbType.DateTime).Value = end.AddMonths(1);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int productId = reader.GetInt32(0);
                            if (!currentStocks.ContainsKey(productId))
                                continue;

                            if (!historyByProduct.TryGetValue(productId, out List<KeyValuePair<DateTime, int>> history))
                            {
                                history = new List<KeyValuePair<DateTime, int>>();
                                historyByProduct.Add(productId, history);
                            }
                            history.Add(new KeyValuePair<DateTime, int>(reader.GetDateTime(1), reader.GetInt32(2)));
                        }
                    }
                }
            }

            var seriesByCategory = new Dictionary<string, InventoryCategoryTrendSeries>(StringComparer.Ordinal);
            DateTime currentMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            foreach (KeyValuePair<int, int> product in currentStocks)
            {
                string categoryName = productCategories[product.Key];
                if (!seriesByCategory.TryGetValue(categoryName, out InventoryCategoryTrendSeries series))
                {
                    series = new InventoryCategoryTrendSeries { CategoryName = categoryName };
                    for (int i = 0; i < trend.Months.Count; i++)
                        series.Quantities.Add(0L);
                    seriesByCategory.Add(categoryName, series);
                }

                historyByProduct.TryGetValue(product.Key, out List<KeyValuePair<DateTime, int>> history);
                int historyIndex = 0;
                int? historicalStock = null;
                for (int i = 0; i < trend.Months.Count; i++)
                {
                    DateTime month = trend.Months[i];
                    long quantity;
                    if (month >= currentMonth || history == null || history.Count == 0)
                    {
                        quantity = product.Value;
                    }
                    else
                    {
                        DateTime monthExclusive = month.AddMonths(1);
                        while (historyIndex < history.Count && history[historyIndex].Key < monthExclusive)
                        {
                            historicalStock = history[historyIndex].Value;
                            historyIndex++;
                        }
                        quantity = historicalStock ?? 0;
                    }
                    series.Quantities[i] += quantity;
                }
            }

            trend.Series.AddRange(seriesByCategory.Values
                .OrderByDescending(series => series.Quantities[series.Quantities.Count - 1]));
            return trend;
        }

        public List<InventoryBatchHistoryRow> GetBatchHistory(string keyword, DateTime? fromDate, DateTime? toDate)
        {
            const string sql = @"
SELECT b.BatchID, b.BatchCode, b.LotNumber, b.ExpiryDate, r.CreatedAt AS ChangedAt,
       N'Phiếu nhập' AS DocumentType, r.ReceiptCode AS DocumentCode,
       p.ProductCode, p.ProductName, d.Quantity, 'IN' AS Direction,
       CAST(NULL AS NVARCHAR(255)) AS Note
FROM PurchaseReceiptDetails d
JOIN PurchaseReceipts r ON r.ReceiptID = d.ReceiptID
JOIN InventoryBatch b ON b.ReceiptDetailID = d.ReceiptDetailID
JOIN Products p ON p.ProductID = b.ProductID
WHERE r.Status = 'COMPLETED' AND p.Status = 'ACTIVE'
UNION ALL
SELECT b.BatchID, b.BatchCode, b.LotNumber, b.ExpiryDate, i.CreatedAt,
       N'Hóa đơn bán', i.InvoiceCode, p.ProductCode, p.ProductName,
       x.Quantity, 'OUT', CAST(NULL AS NVARCHAR(255))
FROM InvoiceDetailBatches x
JOIN InvoiceDetails d ON d.InvoiceDetailID = x.InvoiceDetailID
JOIN Invoices i ON i.InvoiceID = d.InvoiceID
JOIN InventoryBatch b ON b.BatchID = x.BatchID
JOIN Products p ON p.ProductID = b.ProductID
WHERE p.Status = 'ACTIVE'
UNION ALL
SELECT b.BatchID, b.BatchCode, b.LotNumber, b.ExpiryDate, i.CancelledAt,
       N'Hủy hóa đơn', i.InvoiceCode, p.ProductCode, p.ProductName,
       x.Quantity, 'IN', CAST(NULL AS NVARCHAR(255))
FROM InvoiceDetailBatches x
JOIN InvoiceDetails d ON d.InvoiceDetailID = x.InvoiceDetailID
JOIN Invoices i ON i.InvoiceID = d.InvoiceID
JOIN InventoryBatch b ON b.BatchID = x.BatchID
JOIN Products p ON p.ProductID = b.ProductID
WHERE i.Status = 'CANCELLED' AND i.CancelledAt IS NOT NULL AND p.Status = 'ACTIVE'
UNION ALL
SELECT b.BatchID, b.BatchCode, b.LotNumber, b.ExpiryDate,
       ISNULL(re.ApprovedAt, re.CreatedAt),
       CASE WHEN re.Type = 'RETURN' THEN N'Trả hàng' ELSE N'Đổi hàng' END,
       'RT_' + RIGHT('000000' + CAST(re.ReturnID AS VARCHAR(10)), 6),
       p.ProductCode, p.ProductName, x.Quantity, d.Direction, re.Reason
FROM ReturnExchangeDetailBatches x
JOIN ReturnExchangeDetails d ON d.ReturnDetailID = x.ReturnDetailID
JOIN ReturnExchanges re ON re.ReturnID = d.ReturnID
JOIN InventoryBatch b ON b.BatchID = x.BatchID
JOIN Products p ON p.ProductID = b.ProductID
WHERE re.Status = 'APPROVED' AND p.Status = 'ACTIVE'
UNION ALL
SELECT b.BatchID, b.BatchCode, b.LotNumber, b.ExpiryDate, s.CreatedAt,
       N'Phiếu hủy', s.DisposalCode, p.ProductCode, p.ProductName,
       d.Quantity, 'OUT', s.Reason
FROM StockDisposalDetails d
JOIN StockDisposals s ON s.DisposalID = d.DisposalID
JOIN InventoryBatch b ON b.BatchID = d.BatchID
JOIN Products p ON p.ProductID = b.ProductID
WHERE s.Status = 'COMPLETED' AND p.Status = 'ACTIVE'
UNION ALL
SELECT b.BatchID, b.BatchCode, b.LotNumber, b.ExpiryDate, sr.CreatedAt,
       N'Trả nhà cung cấp', sr.SupplierReturnCode, p.ProductCode, p.ProductName,
       d.Quantity, 'OUT', sr.Reason
FROM SupplierReturnDetails d
JOIN SupplierReturns sr ON sr.SupplierReturnID = d.SupplierReturnID
JOIN InventoryBatch b ON b.BatchID = d.BatchID
JOIN Products p ON p.ProductID = b.ProductID
WHERE sr.Status = 'COMPLETED' AND p.Status = 'ACTIVE'
ORDER BY BatchCode, ChangedAt, DocumentCode;";

            var historyRows = new List<InventoryBatchHistoryRow>();
            var runningStockByBatch = new Dictionary<int, int>();
            using (SqlConnection cn = _connectionFactory.CreateConnection())
            {
                if (cn.State != ConnectionState.Open)
                    cn.Open();
                using (var command = new SqlCommand(sql, cn))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int batchId = reader.GetInt32(0);
                        int quantity = reader.GetInt32(9);
                        string direction = reader.GetString(10);
                        runningStockByBatch.TryGetValue(batchId, out int stockBefore);
                        int stockAfter = stockBefore + (direction == "IN" ? quantity : -quantity);
                        runningStockByBatch[batchId] = stockAfter;
                        historyRows.Add(new InventoryBatchHistoryRow
                        {
                            BatchID = batchId,
                            BatchCode = reader.GetString(1),
                            LotNumber = reader.IsDBNull(2) ? null : reader.GetString(2),
                            ExpiryDate = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3),
                            ChangedAt = reader.GetDateTime(4),
                            DocumentType = reader.GetString(5),
                            DocumentCode = reader.GetString(6),
                            ProductCode = reader.GetString(7),
                            ProductName = reader.GetString(8),
                            Quantity = quantity,
                            Direction = direction,
                            StockBefore = stockBefore,
                            StockAfter = stockAfter,
                            Note = reader.IsDBNull(11) ? null : reader.GetString(11)
                        });
                    }
                }
            }

            DateTime? from = fromDate.HasValue ? fromDate.Value.Date : (DateTime?)null;
            DateTime? toExclusive = toDate.HasValue ? toDate.Value.Date.AddDays(1) : (DateTime?)null;
            string search = string.IsNullOrWhiteSpace(keyword) ? null : keyword.Trim();
            return historyRows
                .Where(row => (!from.HasValue || row.ChangedAt >= from.Value)
                    && (!toExclusive.HasValue || row.ChangedAt < toExclusive.Value)
                    && (search == null || MatchesBatchHistoryKeyword(row, search)))
                .OrderByDescending(row => row.ChangedAt)
                .ToList();
        }

        private static void ApplyBreakdownRatios(List<InventoryStockBreakdownRow> rows)
        {
            decimal totalSellValue = rows.Sum(row => row.SellValue);
            foreach (InventoryStockBreakdownRow row in rows)
                row.Ratio = totalSellValue == 0m ? 0d : (double)(row.SellValue * 100m / totalSellValue);
        }

        private static bool MatchesBatchHistoryKeyword(InventoryBatchHistoryRow row, string keyword)
        {
            return ContainsIgnoreCase(row.BatchCode, keyword)
                || ContainsIgnoreCase(row.LotNumber, keyword)
                || ContainsIgnoreCase(row.DocumentCode, keyword)
                || ContainsIgnoreCase(row.ProductCode, keyword)
                || ContainsIgnoreCase(row.ProductName, keyword);
        }

        private static bool ContainsIgnoreCase(string value, string keyword)
        {
            return value != null && value.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public List<InventoryReportCategory> GetCategories()
        {
            using (SimsDataContext db = _connectionFactory.CreateDataContext())
            {
                List<InventoryReportCategory> categories = db.Categories
                    .OrderBy(category => category.CategoryName)
                    .Select(category => new InventoryReportCategory
                    {
                        CategoryId = category.CategoryID,
                        Name = category.CategoryName
                    })
                    .ToList();
                return categories;
            }
        }

        public InventoryReportData GetReport(DateTime fromDate, DateTime toDate, int? categoryId)
        {
            if (fromDate.Date > toDate.Date)
                throw new ArgumentException("Ngày bắt đầu không được sau ngày kết thúc.");

            DateTime rangeStart = fromDate.Date;
            DateTime toExclusive = toDate.Date.AddDays(1);
            DateTime today = DateTime.Today;
            using (SimsDataContext db = _connectionFactory.CreateDataContext())
            {
                var activeProducts = db.Products.Where(product => product.Status == "ACTIVE");
                if (categoryId.HasValue)
                    activeProducts = activeProducts.Where(product => product.CategoryID == categoryId.Value);

                var products = (from product in activeProducts
                               join category in db.Categories on product.CategoryID equals category.CategoryID
                               orderby category.CategoryName, product.ProductName
                               select new InventoryReportProduct
                               {
                                   ProductCode = product.ProductCode,
                                   ProductName = product.ProductName,
                                   CategoryName = category.CategoryName,
                                   Stock = product.Stock,
                                   MinStock = product.MinStock,
                                   ImportPrice = product.ImportPrice,
                                   StockValue = product.ImportPrice * product.Stock
                               }).ToList();

                var report = new InventoryReportData
                {
                    SnapshotDate = DateTime.Now,
                    BusinessName = db.StoreConfigs
                        .Where(config => config.ConfigKey == "STORE_NAME")
                        .Select(config => config.ConfigValue)
                        .FirstOrDefault(),
                    DirectorName = (from user in db.Users
                                    join role in db.Roles on user.RoleID equals role.RoleID
                                    where role.RoleCode == "ADMIN"
                                        && !user.IsDeleted
                                        && !user.IsLocked
                                        && user.Status == "ACTIVE"
                                    orderby user.UserID
                                    select user.FullName).FirstOrDefault(),
                    InventoryManagerName = (from user in db.Users
                                            join role in db.Roles on user.RoleID equals role.RoleID
                                            where role.RoleCode == "INVENTORY_MANAGER"
                                                && !user.IsDeleted
                                                && !user.IsLocked
                                                && user.Status == "ACTIVE"
                                            orderby user.UserID
                                            select user.FullName).FirstOrDefault(),
                    ProductCount = products.Count,
                    TotalQuantity = products.Sum(product => (long)product.Stock),
                    TotalStockValue = products.Sum(product => product.StockValue),
                    LowStockProductCount = products.Count(product => product.Stock <= product.MinStock)
                };
                report.Products.AddRange(products);

                var nonEmptyBatches = db.InventoryBatches.Where(batch => batch.Status != "DEPLETED"
                    && batch.RemainingQty > 0
                    && batch.ExpiryDate.HasValue
                    && batch.ExpiryDate.Value < today);
                var expiredBatches = from batch in nonEmptyBatches
                                          join product in db.Products on batch.ProductID equals product.ProductID
                                          where product.Status == "ACTIVE"
                                          select new { batch, product };
                if (categoryId.HasValue)
                    expiredBatches = expiredBatches.Where(item => item.product.CategoryID == categoryId.Value);
                report.ExpiredQuantity = expiredBatches
                    .Select(item => (long?)item.batch.RemainingQty)
                    .Sum() ?? 0L;

                var importMovements = from movement in db.InventoryTransactions
                               join product in db.Products on movement.ProductID equals product.ProductID
                               where movement.TransactionType == "IMPORT"
                                   && movement.CreatedAt >= rangeStart
                                   && movement.CreatedAt < toExclusive
                                   && product.Status == "ACTIVE"
                               select new { movement, product };
                if (categoryId.HasValue)
                    importMovements = importMovements.Where(item => item.product.CategoryID == categoryId.Value);

                var imports = (from item in importMovements
                               group item.movement by item.movement.CreatedAt.Date into day
                               orderby day.Key
                               select new
                               {
                                   Date = day.Key,
                                   Quantity = day.Sum(item => item.Quantity)
                               }).ToList();
                foreach (var import in imports)
                {
                    report.ImportTrend.Add(new InventoryImportTrendPoint
                    {
                        Date = import.Date,
                        Quantity = import.Quantity
                    });
                }

                return report;
            }
        }
    }
}
