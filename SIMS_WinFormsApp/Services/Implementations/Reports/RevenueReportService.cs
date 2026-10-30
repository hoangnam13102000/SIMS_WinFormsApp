using System;
using System.Collections.Generic;
using System.Linq;
using SIMS_WinFormsApp.DAL.Linq;
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
    }
}
