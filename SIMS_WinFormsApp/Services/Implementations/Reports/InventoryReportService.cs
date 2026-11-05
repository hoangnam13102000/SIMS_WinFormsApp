using System;
using System.Collections.Generic;
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
            using (SimsDataContext db = _connectionFactory.CreateDataContext())
            {
                var products = db.Products.Where(product => product.Status == "ACTIVE");
                if (categoryId.HasValue)
                    products = products.Where(product => product.CategoryID == categoryId.Value);

                return new InventoryStockSummary
                {
                    ProductCount = products.Count(),
                    TotalQuantity = products.Select(product => (long?)product.Stock).Sum() ?? 0L,
                    SellValue = products.Select(product =>
                        (decimal?)product.Stock * product.SellPrice).Sum() ?? 0m,
                    ImportValue = products.Select(product =>
                        (decimal?)product.Stock * product.ImportPrice).Sum() ?? 0m,
                    LowStockCount = products.Count(product =>
                        product.Stock > 0 && product.Stock <= product.MinStock),
                    OutOfStockCount = products.Count(product => product.Stock == 0)
                };
            }
        }

        public List<InventoryProductStockPoint> GetTopProductStock(int? categoryId, int top = 20)
        {
            int limit = Math.Max(0, top);
            if (limit == 0)
                return new List<InventoryProductStockPoint>();

            using (SimsDataContext db = _connectionFactory.CreateDataContext())
            {
                var products = db.Products
                    .Where(product => product.Status == "ACTIVE");
                if (categoryId.HasValue)
                    products = products.Where(product => product.CategoryID == categoryId.Value);

                var productRows = products
                    .OrderByDescending(product => product.Stock)
                    .ThenBy(product => product.ProductName)
                    .Take(limit)
                    .Select(product => new
                    {
                        product.ProductID,
                        product.ProductCode,
                        product.ProductName,
                        product.Stock,
                        product.MinStock
                    })
                    .ToList();
                int[] productIds = productRows.Select(product => product.ProductID).ToArray();
                var batches = db.InventoryBatches
                    .Where(batch => productIds.Contains(batch.ProductID)
                        && batch.Status != "DEPLETED" && batch.RemainingQty > 0)
                    .Select(batch => new { batch.ProductID, batch.ExpiryDate })
                    .ToList();
                DateTime today = DateTime.Today;
                return productRows.Select(product =>
                {
                    var productBatches = batches.Where(batch => batch.ProductID == product.ProductID).ToList();
                    DateTime? nearestExpiry = productBatches
                        .Where(batch => batch.ExpiryDate.HasValue)
                        .Select(batch => batch.ExpiryDate)
                        .OrderBy(expiryDate => expiryDate)
                        .FirstOrDefault();
                    return new InventoryProductStockPoint
                    {
                        ProductCode = product.ProductCode,
                        ProductName = product.ProductName,
                        Stock = product.Stock,
                        MinStock = product.MinStock,
                        NearestExpiry = nearestExpiry,
                        HasExpired = productBatches.Any(batch =>
                            batch.ExpiryDate.HasValue && batch.ExpiryDate.Value < today)
                    };
                }).ToList();
            }
        }

        public List<InventoryStockBreakdownRow> GetStockByCategory(int? categoryId)
        {
            List<InventoryStockBreakdownRow> results;
            using (SimsDataContext db = _connectionFactory.CreateDataContext())
            {
                var products = from product in db.Products
                               join category in db.Categories
                                   on product.CategoryID equals category.CategoryID
                               where product.Status == "ACTIVE"
                               select new { product, category.CategoryName };
                if (categoryId.HasValue)
                    products = products.Where(item => item.product.CategoryID == categoryId.Value);

                results = products
                    .GroupBy(item => item.CategoryName)
                    .Select(productsInCategory => new InventoryStockBreakdownRow
                    {
                        Label = productsInCategory.Key,
                        ProductCount = productsInCategory.Count(),
                        Quantity = productsInCategory.Sum(item => (long?)item.product.Stock) ?? 0L,
                        SellValue = productsInCategory.Sum(item =>
                            (decimal?)item.product.Stock * item.product.SellPrice) ?? 0m
                    })
                    .OrderByDescending(row => row.SellValue)
                    .ToList();
            }
            ApplyBreakdownRatios(results);
            return results;
        }

        public List<InventoryStockBreakdownRow> GetStockByPriceRange(int? categoryId)
        {
            var results = new List<InventoryStockBreakdownRow>
            {
                new InventoryStockBreakdownRow { Label = "Dưới 50.000 đ" },
                new InventoryStockBreakdownRow { Label = "50.000 - dưới 100.000 đ" },
                new InventoryStockBreakdownRow { Label = "100.000 - dưới 200.000 đ" },
                new InventoryStockBreakdownRow { Label = "200.000 - dưới 500.000 đ" },
                new InventoryStockBreakdownRow { Label = "Từ 500.000 đ trở lên" }
            };

            using (SimsDataContext db = _connectionFactory.CreateDataContext())
            {
                var products = db.Products
                    .Where(product => product.Status == "ACTIVE");
                if (categoryId.HasValue)
                    products = products.Where(product => product.CategoryID == categoryId.Value);

                var productRows = products
                    .Select(product => new { product.SellPrice, product.Stock })
                    .ToList();
                foreach (var product in productRows)
                {
                    decimal sellPrice = product.SellPrice;
                    int index = sellPrice < 50000m ? 0
                        : sellPrice < 100000m ? 1
                        : sellPrice < 200000m ? 2
                        : sellPrice < 500000m ? 3
                        : 4;
                    InventoryStockBreakdownRow row = results[index];
                    row.ProductCount++;
                    row.Quantity += product.Stock;
                    row.SellValue += sellPrice * product.Stock;
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
            using (SimsDataContext db = _connectionFactory.CreateDataContext())
            {
                var products = (from product in db.Products
                                join category in db.Categories
                                    on product.CategoryID equals category.CategoryID
                                where product.Status == "ACTIVE"
                                select new
                                {
                                    product.ProductID,
                                    category.CategoryName,
                                    product.Stock
                                }).ToList();
                foreach (var product in products)
                {
                    productCategories[product.ProductID] = product.CategoryName;
                    currentStocks[product.ProductID] = product.Stock;
                }

                DateTime endExclusive = end.AddMonths(1);
                var transactions = db.InventoryTransactions
                    .Where(transaction => transaction.CreatedAt < endExclusive)
                    .OrderBy(transaction => transaction.ProductID)
                    .ThenBy(transaction => transaction.CreatedAt)
                    .ThenBy(transaction => transaction.TransactionID)
                    .Select(transaction => new
                    {
                        transaction.ProductID,
                        transaction.CreatedAt,
                        transaction.StockAfter
                    })
                    .ToList();
                foreach (var transaction in transactions)
                {
                    int productId = transaction.ProductID;
                    if (!currentStocks.ContainsKey(productId))
                        continue;

                    if (!historyByProduct.TryGetValue(productId, out List<KeyValuePair<DateTime, int>> history))
                    {
                        history = new List<KeyValuePair<DateTime, int>>();
                        historyByProduct.Add(productId, history);
                    }
                    history.Add(new KeyValuePair<DateTime, int>(
                        transaction.CreatedAt, transaction.StockAfter));
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
            var historyEvents = new List<BatchHistoryEvent>();
            var batchInfo = new Dictionary<int, BatchHistoryInfo>();
            using (SimsDataContext db = _connectionFactory.CreateDataContext())
            {
                var batches = (from batch in db.InventoryBatches
                               join product in db.Products on batch.ProductID equals product.ProductID
                               where product.Status == "ACTIVE"
                               select new
                               {
                                   batch.BatchID,
                                   batch.BatchCode,
                                   batch.LotNumber,
                                   batch.ExpiryDate,
                                   product.ProductCode,
                                   product.ProductName
                               }).ToList();
                batchInfo = batches.ToDictionary(batch => batch.BatchID, batch =>
                    new BatchHistoryInfo
                    {
                        BatchCode = batch.BatchCode,
                        LotNumber = batch.LotNumber,
                        ExpiryDate = batch.ExpiryDate,
                        ProductCode = batch.ProductCode,
                        ProductName = batch.ProductName
                    });

                historyEvents.AddRange((from detail in db.PurchaseReceiptDetails
                                        join receipt in db.PurchaseReceipts
                                            on detail.ReceiptID equals receipt.ReceiptID
                                        from batch in db.InventoryBatches
                                        where batch.ReceiptDetailID == detail.ReceiptDetailID
                                            && receipt.Status == "COMPLETED"
                                        select new BatchHistoryEvent
                                        {
                                            BatchID = batch.BatchID,
                                            ChangedAt = receipt.CreatedAt,
                                            DocumentType = "Phiếu nhập",
                                            DocumentCode = receipt.ReceiptCode,
                                            Quantity = detail.Quantity,
                                            Direction = "IN"
                                        }).ToList());

                historyEvents.AddRange((from batchLine in db.InvoiceDetailBatches
                                        join detail in db.InvoiceDetails
                                            on batchLine.InvoiceDetailID equals detail.InvoiceDetailID
                                        join invoice in db.Invoices
                                            on detail.InvoiceID equals invoice.InvoiceID
                                        join batch in db.InventoryBatches
                                            on batchLine.BatchID equals batch.BatchID
                                        select new BatchHistoryEvent
                                        {
                                            BatchID = batch.BatchID,
                                            ChangedAt = invoice.CreatedAt,
                                            DocumentType = "Hóa đơn bán",
                                            DocumentCode = invoice.InvoiceCode,
                                            Quantity = batchLine.Quantity,
                                            Direction = "OUT"
                                        }).ToList());

                var cancelledInvoiceEvents = (from batchLine in db.InvoiceDetailBatches
                                              join detail in db.InvoiceDetails
                                                  on batchLine.InvoiceDetailID equals detail.InvoiceDetailID
                                              join invoice in db.Invoices
                                                  on detail.InvoiceID equals invoice.InvoiceID
                                              join batch in db.InventoryBatches
                                                  on batchLine.BatchID equals batch.BatchID
                                              where invoice.Status == "CANCELLED"
                                              select new
                                              {
                                                  BatchID = batch.BatchID,
                                                  CancelledAt = invoice.CancelledAt,
                                                  InvoiceCode = invoice.InvoiceCode,
                                                  Quantity = batchLine.Quantity
                                              }).ToList();
                historyEvents.AddRange(cancelledInvoiceEvents
                    .Where(item => item.CancelledAt.HasValue)
                    .Select(item => new BatchHistoryEvent
                    {
                        BatchID = item.BatchID,
                        ChangedAt = item.CancelledAt.Value,
                        DocumentType = "Hủy hóa đơn",
                        DocumentCode = item.InvoiceCode,
                        Quantity = item.Quantity,
                        Direction = "IN"
                    }));

                var returnEvents = (from batchLine in db.ReturnExchangeDetailBatches
                                    join detail in db.ReturnExchangeDetails
                                        on batchLine.ReturnDetailID equals detail.ReturnDetailID
                                    join exchange in db.ReturnExchanges
                                        on detail.ReturnID equals exchange.ReturnID
                                    join batch in db.InventoryBatches
                                        on batchLine.BatchID equals batch.BatchID
                                    where exchange.Status == "APPROVED"
                                    select new
                                    {
                                        BatchID = batch.BatchID,
                                        ChangedAt = exchange.ApprovedAt ?? exchange.CreatedAt,
                                        DocumentType = exchange.Type == "RETURN" ? "Trả hàng" : "Đổi hàng",
                                        ReturnID = exchange.ReturnID,
                                        Quantity = batchLine.Quantity,
                                        Direction = detail.Direction,
                                        Note = exchange.Reason
                                    }).ToList();
                historyEvents.AddRange(returnEvents.Select(item => new BatchHistoryEvent
                {
                    BatchID = item.BatchID,
                    ChangedAt = item.ChangedAt,
                    DocumentType = item.DocumentType,
                    DocumentCode = "RT_" + item.ReturnID.ToString("D6"),
                    Quantity = item.Quantity,
                    Direction = item.Direction,
                    Note = item.Note
                }));

                historyEvents.AddRange((from detail in db.StockDisposalDetails
                                        join disposal in db.StockDisposals
                                            on detail.DisposalID equals disposal.DisposalID
                                        join batch in db.InventoryBatches
                                            on detail.BatchID equals batch.BatchID
                                        where disposal.Status == "COMPLETED"
                                        select new BatchHistoryEvent
                                        {
                                            BatchID = batch.BatchID,
                                            ChangedAt = disposal.CreatedAt,
                                            DocumentType = "Phiếu hủy",
                                            DocumentCode = disposal.DisposalCode,
                                            Quantity = detail.Quantity,
                                            Direction = "OUT",
                                            Note = disposal.Reason
                                        }).ToList());

                historyEvents.AddRange((from detail in db.SupplierReturnDetails
                                        join supplierReturn in db.SupplierReturns
                                            on detail.SupplierReturnID equals supplierReturn.SupplierReturnID
                                        join batch in db.InventoryBatches
                                            on detail.BatchID equals batch.BatchID
                                        where supplierReturn.Status == "COMPLETED"
                                        select new BatchHistoryEvent
                                        {
                                            BatchID = batch.BatchID,
                                            ChangedAt = supplierReturn.CreatedAt,
                                            DocumentType = "Trả nhà cung cấp",
                                            DocumentCode = supplierReturn.SupplierReturnCode,
                                            Quantity = detail.Quantity,
                                            Direction = "OUT",
                                            Note = supplierReturn.Reason
                                        }).ToList());
            }

            var historyRows = new List<InventoryBatchHistoryRow>();
            var runningStockByBatch = new Dictionary<int, int>();
            foreach (BatchHistoryEvent historyEvent in historyEvents
                .Where(item => batchInfo.ContainsKey(item.BatchID))
                .OrderBy(item => batchInfo[item.BatchID].BatchCode)
                .ThenBy(item => item.ChangedAt)
                .ThenBy(item => item.DocumentCode))
            {
                BatchHistoryInfo batch = batchInfo[historyEvent.BatchID];
                runningStockByBatch.TryGetValue(historyEvent.BatchID, out int stockBefore);
                int stockAfter = stockBefore +
                    (historyEvent.Direction == "IN" ? historyEvent.Quantity : -historyEvent.Quantity);
                runningStockByBatch[historyEvent.BatchID] = stockAfter;
                historyRows.Add(new InventoryBatchHistoryRow
                {
                    BatchID = historyEvent.BatchID,
                    BatchCode = batch.BatchCode,
                    LotNumber = batch.LotNumber,
                    ExpiryDate = batch.ExpiryDate,
                    ChangedAt = historyEvent.ChangedAt,
                    DocumentType = historyEvent.DocumentType,
                    DocumentCode = historyEvent.DocumentCode,
                    ProductCode = batch.ProductCode,
                    ProductName = batch.ProductName,
                    Quantity = historyEvent.Quantity,
                    Direction = historyEvent.Direction,
                    StockBefore = stockBefore,
                    StockAfter = stockAfter,
                    Note = historyEvent.Note
                });
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

        private sealed class BatchHistoryEvent
        {
            public int BatchID { get; set; }
            public DateTime ChangedAt { get; set; }
            public string DocumentType { get; set; }
            public string DocumentCode { get; set; }
            public int Quantity { get; set; }
            public string Direction { get; set; }
            public string Note { get; set; }
        }

        private sealed class BatchHistoryInfo
        {
            public string BatchCode { get; set; }
            public string LotNumber { get; set; }
            public DateTime? ExpiryDate { get; set; }
            public string ProductCode { get; set; }
            public string ProductName { get; set; }
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
                    && batch.ExpiryDate < today);
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
                Dictionary<DateTime, long> importQuantities = imports.ToDictionary(
                    import => import.Date.Date,
                    import => (long)import.Quantity);
                for (DateTime date = rangeStart; date <= toDate.Date; date = date.AddDays(1))
                {
                    importQuantities.TryGetValue(date, out long quantity);
                    report.ImportTrend.Add(new InventoryImportTrendPoint
                    {
                        Date = date,
                        Quantity = quantity
                    });
                }

                return report;
            }
        }
    }
}
