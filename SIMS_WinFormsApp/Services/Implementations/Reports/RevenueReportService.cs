using System;
using System.Collections.Generic;
using System.Linq;
using SIMS_WinFormsApp.DAL.Linq;
using SIMS_WinFormsApp.DAL.Linq.Entities.Business;
using SIMS_WinFormsApp.Infrastructure.Configuration;

namespace SIMS_WinFormsApp.Services.Implementations.Reports
{
    public enum RevenueReportPeriod
    {
        Week = 0,
        Month = 1,
        Quarter = 2
    }

    public sealed class RevenueReportData
    {
        public decimal Revenue { get; set; }
        public int OrderCount { get; set; }
        public List<RevenueTrendPoint> Trend { get; } = new List<RevenueTrendPoint>();
        public List<RevenueCategoryTotal> Categories { get; } = new List<RevenueCategoryTotal>();
        public List<RevenueProductTotal> TopProducts { get; } = new List<RevenueProductTotal>();
    }

    public sealed class RevenueTrendPoint
    {
        public DateTime PeriodStart { get; set; }
        public decimal Revenue { get; set; }
        public int OrderCount { get; set; }
    }

    public sealed class RevenueCategoryTotal
    {
        public string Name { get; set; }
        public decimal Revenue { get; set; }
    }

    public sealed class RevenueProductTotal
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Revenue { get; set; }
    }

    public sealed class StockDisposalReportData
    {
        public int DisposalCount { get; set; }
        public long ProductQuantity { get; set; }
        public decimal TotalLoss { get; set; }
        public List<StockDisposalReasonTotal> Reasons { get; } = new List<StockDisposalReasonTotal>();
        public List<StockDisposalProductTotal> Products { get; } = new List<StockDisposalProductTotal>();
        public List<StockDisposalTrendPoint> Trend { get; } = new List<StockDisposalTrendPoint>();
    }

    public sealed class StockDisposalTrendPoint
    {
        public DateTime PeriodStart { get; set; }
        public decimal LossAmount { get; set; }
    }

    public sealed class StockDisposalReasonTotal
    {
        public string Reason { get; set; }
        public int DisposalCount { get; set; }
        public long ProductQuantity { get; set; }
        public decimal LossAmount { get; set; }
    }

    public sealed class StockDisposalProductTotal
    {
        public string Name { get; set; }
        public long Quantity { get; set; }
        public decimal LossAmount { get; set; }
    }

    public sealed class FinancialReportSignatories
    {
        public string BusinessName { get; set; }
        public string DirectorName { get; set; }
        public string InventoryManagerName { get; set; }
    }

    public sealed class RevenueReportService
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public RevenueReportService(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public DateTime? GetLatestInvoiceDate()
        {
            using (var db = _connectionFactory.CreateDataContext())
            {
                DateTime? latest = db.Invoices.Where(i => i.Status == "ACTIVE")
                    .Select(i => (DateTime?)i.CreatedAt).Max();
                return latest.HasValue ? latest.Value.Date : (DateTime?)null;
            }
        }

        public FinancialReportSignatories GetFinancialReportSignatories()
        {
            using (SimsDataContext db = _connectionFactory.CreateDataContext())
            {
                string businessName = db.StoreConfigs
                    .Where(config => config.ConfigKey == "STORE_NAME")
                    .Select(config => config.ConfigValue)
                    .FirstOrDefault();
                string directorName = (from user in db.Users
                                      join role in db.Roles on user.RoleID equals role.RoleID
                                      where role.RoleCode == "ADMIN"
                                          && !user.IsDeleted
                                          && !user.IsLocked
                                          && user.Status == "ACTIVE"
                                      orderby user.UserID
                                      select user.FullName).FirstOrDefault();
                string inventoryManagerName = (from user in db.Users
                                               join role in db.Roles on user.RoleID equals role.RoleID
                                               where role.RoleCode == "INVENTORY_MANAGER"
                                                   && !user.IsDeleted
                                                   && !user.IsLocked
                                                   && user.Status == "ACTIVE"
                                               orderby user.UserID
                                               select user.FullName).FirstOrDefault();

                return new FinancialReportSignatories
                {
                    BusinessName = string.IsNullOrWhiteSpace(businessName)
                        ? "Chưa cấu hình tên cửa hàng / đơn vị"
                        : businessName.Trim(),
                    DirectorName = string.IsNullOrWhiteSpace(directorName)
                        ? "Chưa phân công"
                        : directorName.Trim(),
                    InventoryManagerName = string.IsNullOrWhiteSpace(inventoryManagerName)
                        ? "Chưa phân công"
                        : inventoryManagerName.Trim()
                };
            }
        }

        public RevenueReportData GetReport(DateTime fromDate, DateTime toDate, RevenueReportPeriod period)
        {
            if (fromDate.Date > toDate.Date)
                throw new ArgumentException("Ngày bắt đầu không được sau ngày kết thúc.");
            if (!Enum.IsDefined(typeof(RevenueReportPeriod), period))
                throw new ArgumentOutOfRangeException(nameof(period));

            DateTime from = fromDate.Date;
            DateTime toExclusive = toDate.Date.AddDays(1);
            using (SimsDataContext db = _connectionFactory.CreateDataContext())
            {
                var invoices = db.Invoices.Where(i => i.Status == "ACTIVE"
                    && i.CreatedAt >= from && i.CreatedAt < toExclusive);
                var report = new RevenueReportData
                {
                    Revenue = invoices.Select(i => (decimal?)i.TotalAmount).Sum() ?? 0m,
                    OrderCount = invoices.Count()
                };

                if (period == RevenueReportPeriod.Week)
                {
                    var buckets = invoices.GroupBy(i => i.CreatedAt.Date)
                        .Select(group => new
                        {
                            Year = group.Key.Year,
                            Month = group.Key.Month,
                            Day = group.Key.Day,
                            Revenue = group.Sum(i => i.TotalAmount),
                            Count = group.Count()
                        }).OrderBy(bucket => bucket.Year).ThenBy(bucket => bucket.Month).ThenBy(bucket => bucket.Day)
                        .ToList();
                    foreach (var bucket in buckets)
                        report.Trend.Add(new RevenueTrendPoint
                        {
                            PeriodStart = new DateTime(bucket.Year, bucket.Month, bucket.Day),
                            Revenue = bucket.Revenue,
                            OrderCount = bucket.Count
                        });
                }
                else if (period == RevenueReportPeriod.Month)
                {
                    var buckets = invoices.GroupBy(i => new { i.CreatedAt.Year, i.CreatedAt.Month })
                        .Select(group => new
                        {
                            group.Key.Year,
                            group.Key.Month,
                            Revenue = group.Sum(i => i.TotalAmount),
                            Count = group.Count()
                        }).OrderBy(bucket => bucket.Year).ThenBy(bucket => bucket.Month).ToList();
                    foreach (var bucket in buckets)
                        report.Trend.Add(new RevenueTrendPoint
                        {
                            PeriodStart = new DateTime(bucket.Year, bucket.Month, 1),
                            Revenue = bucket.Revenue,
                            OrderCount = bucket.Count
                        });
                }
                else
                {
                    var buckets = invoices.GroupBy(i => new
                        {
                            i.CreatedAt.Year,
                            Quarter = (i.CreatedAt.Month - 1) / 3
                        })
                        .Select(group => new
                        {
                            group.Key.Year,
                            group.Key.Quarter,
                            Revenue = group.Sum(i => i.TotalAmount),
                            Count = group.Count()
                        }).OrderBy(bucket => bucket.Year).ThenBy(bucket => bucket.Quarter).ToList();
                    foreach (var bucket in buckets)
                        report.Trend.Add(new RevenueTrendPoint
                        {
                            PeriodStart = new DateTime(bucket.Year, bucket.Quarter * 3 + 1, 1),
                            Revenue = bucket.Revenue,
                            OrderCount = bucket.Count
                        });
                }

                var categoryRows = (from invoice in invoices
                                    join detail in db.InvoiceDetails on invoice.InvoiceID equals detail.InvoiceID
                                    join product in db.Products on detail.ProductID equals product.ProductID
                                    join category in db.Categories on product.CategoryID equals category.CategoryID
                                    group detail by category.CategoryName into categoryDetails
                                    let revenue = categoryDetails.Sum(detail => detail.LineTotal)
                                    where revenue > 0
                                    orderby revenue descending
                                    select new { Name = categoryDetails.Key, Revenue = revenue }).ToList();
                foreach (var row in categoryRows)
                    report.Categories.Add(new RevenueCategoryTotal { Name = row.Name, Revenue = row.Revenue });

                var productRows = (from invoice in invoices
                                   join detail in db.InvoiceDetails on invoice.InvoiceID equals detail.InvoiceID
                                   join product in db.Products on detail.ProductID equals product.ProductID
                                   group new { detail.Quantity, detail.LineTotal } by product.ProductName into productDetails
                                   orderby productDetails.Sum(item => item.LineTotal) descending
                                   select new
                                   {
                                       Name = productDetails.Key,
                                       Quantity = productDetails.Sum(item => item.Quantity),
                                       Revenue = productDetails.Sum(item => item.LineTotal)
                                   }).Take(5).ToList();
                foreach (var row in productRows)
                    report.TopProducts.Add(new RevenueProductTotal
                    {
                        Name = row.Name,
                        Quantity = row.Quantity,
                        Revenue = row.Revenue
                    });
                return report;
            }
        }

        public StockDisposalReportData GetStockDisposalReport(
            DateTime fromDate,
            DateTime toDate,
            RevenueReportPeriod period = RevenueReportPeriod.Week)
        {
            if (fromDate.Date > toDate.Date)
                throw new ArgumentException("Ngày bắt đầu không được sau ngày kết thúc.");
            if (!Enum.IsDefined(typeof(RevenueReportPeriod), period))
                throw new ArgumentOutOfRangeException(nameof(period));

            DateTime from = fromDate.Date;
            DateTime toExclusive = toDate.Date.AddDays(1);
            using (SimsDataContext db = _connectionFactory.CreateDataContext())
            {
                var disposals = db.StockDisposals.Where(item => item.Status == "COMPLETED"
                    && item.CreatedAt >= from && item.CreatedAt < toExclusive);
                var report = new StockDisposalReportData
                {
                    DisposalCount = disposals.Count(),
                    TotalLoss = disposals.Select(item => (decimal?)item.TotalLossAmount).Sum() ?? 0m
                };
                AddDisposalTrend(report, disposals, from, toExclusive, period);

                report.ProductQuantity = (from disposal in disposals
                                          join detail in db.StockDisposalDetails
                                              on disposal.DisposalID equals detail.DisposalID
                                          select (long?)detail.Quantity).Sum() ?? 0L;

                var reasonTotals = disposals.GroupBy(item => item.Reason)
                    .Select(group => new
                    {
                        Reason = group.Key,
                        DisposalCount = group.Count(),
                        LossAmount = group.Sum(item => item.TotalLossAmount)
                    })
                    .ToList();
                var reasonQuantities = (from disposal in disposals
                                        join detail in db.StockDisposalDetails
                                            on disposal.DisposalID equals detail.DisposalID
                                        group detail by disposal.Reason into reasonGroup
                                        select new
                                        {
                                            Reason = reasonGroup.Key,
                                            ProductQuantity = reasonGroup.Sum(item => item.Quantity)
                                        })
                    .ToList()
                    .ToDictionary(item => item.Reason, item => item.ProductQuantity);
                foreach (var row in reasonTotals.OrderByDescending(item => item.LossAmount))
                    report.Reasons.Add(new StockDisposalReasonTotal
                    {
                        Reason = row.Reason,
                        DisposalCount = row.DisposalCount,
                        ProductQuantity = reasonQuantities.TryGetValue(row.Reason, out int quantity) ? quantity : 0,
                        LossAmount = row.LossAmount
                    });

                var productRows = (from disposal in disposals
                                   join detail in db.StockDisposalDetails
                                       on disposal.DisposalID equals detail.DisposalID
                                   join product in db.Products on detail.ProductID equals product.ProductID
                                   group detail by product.ProductName into productGroup
                                   orderby productGroup.Sum(item => item.LineLossAmount) descending
                                   select new
                                   {
                                       Name = productGroup.Key,
                                       Quantity = productGroup.Sum(item => item.Quantity),
                                       LossAmount = productGroup.Sum(item => item.LineLossAmount)
                                   }).Take(10).ToList();
                foreach (var row in productRows)
                    report.Products.Add(new StockDisposalProductTotal
                    {
                        Name = row.Name,
                        Quantity = row.Quantity,
                        LossAmount = row.LossAmount
                    });

                return report;
            }
        }

        private static void AddDisposalTrend(
            StockDisposalReportData report,
            IQueryable<StockDisposalEntity> disposals,
            DateTime from,
            DateTime toExclusive,
            RevenueReportPeriod period)
        {
            var totals = new Dictionary<DateTime, decimal>();
            if (period == RevenueReportPeriod.Week)
            {
                foreach (var row in disposals
                    .GroupBy(item => item.CreatedAt.Date)
                    .Select(group => new { PeriodStart = group.Key, LossAmount = group.Sum(item => item.TotalLossAmount) })
                    .ToList())
                    totals[row.PeriodStart.Date] = row.LossAmount;
            }
            else if (period == RevenueReportPeriod.Month)
            {
                foreach (var row in disposals
                    .GroupBy(item => new { item.CreatedAt.Year, item.CreatedAt.Month })
                    .Select(group => new
                    {
                        group.Key.Year,
                        group.Key.Month,
                        LossAmount = group.Sum(item => item.TotalLossAmount)
                    })
                    .ToList())
                    totals[new DateTime(row.Year, row.Month, 1)] = row.LossAmount;
            }
            else
            {
                foreach (var row in disposals
                    .GroupBy(item => new
                    {
                        item.CreatedAt.Year,
                        Quarter = (item.CreatedAt.Month - 1) / 3
                    })
                    .Select(group => new
                    {
                        group.Key.Year,
                        group.Key.Quarter,
                        LossAmount = group.Sum(item => item.TotalLossAmount)
                    })
                    .ToList())
                    totals[new DateTime(row.Year, row.Quarter * 3 + 1, 1)] = row.LossAmount;
            }

            for (DateTime bucketStart = GetPeriodStart(from, period);
                bucketStart < toExclusive;
                bucketStart = GetNextPeriodStart(bucketStart, period))
            {
                totals.TryGetValue(bucketStart, out decimal lossAmount);
                report.Trend.Add(new StockDisposalTrendPoint
                {
                    PeriodStart = bucketStart,
                    LossAmount = lossAmount
                });
            }
        }

        private static DateTime GetPeriodStart(DateTime date, RevenueReportPeriod period)
        {
            if (period == RevenueReportPeriod.Week)
                return date.Date;
            if (period == RevenueReportPeriod.Month)
                return new DateTime(date.Year, date.Month, 1);
            return new DateTime(date.Year, ((date.Month - 1) / 3) * 3 + 1, 1);
        }

        private static DateTime GetNextPeriodStart(DateTime date, RevenueReportPeriod period)
        {
            if (period == RevenueReportPeriod.Week)
                return date.AddDays(1);
            if (period == RevenueReportPeriod.Month)
                return date.AddMonths(1);
            return date.AddMonths(3);

        }
    }
}
