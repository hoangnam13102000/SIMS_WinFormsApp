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

    public sealed class InventoryReportService
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public InventoryReportService(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
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
