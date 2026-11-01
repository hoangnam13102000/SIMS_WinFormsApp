using System;
using System.Linq;
using SIMS_WinFormsApp.DAL.Linq;
using SIMS_WinFormsApp.Infrastructure.Configuration;
using SIMS_WinFormsApp.Models.DTOs.Dashboard;
using SIMS_WinFormsApp.Services.Interfaces;

namespace SIMS_WinFormsApp.Services.Implementations.Reports
{
    public sealed class DashboardDataService : IDashboardDataService
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public DashboardDataService(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public DashboardDataDto GetDashboardData()
        {
            return GetDashboardData(null);
        }

        public DashboardDataDto GetDashboardData(DateTime revenuePeriodStart, DateTime revenuePeriodEnd)
        {
            ValidatePeriod(revenuePeriodStart, revenuePeriodEnd, nameof(revenuePeriodEnd));
            return GetDashboardData(null, null, null, revenuePeriodStart, revenuePeriodEnd);
        }

        public DashboardDataDto GetSalesStaffDashboardData(int userId)
        {
            if (userId <= 0) throw new ArgumentOutOfRangeException(nameof(userId));
            return GetDashboardData(userId, null, null);
        }

        public DashboardDataDto GetSalesStaffDashboardData(
            int userId,
            DateTime invoicePeriodStart,
            DateTime invoicePeriodEnd)
        {
            if (userId <= 0) throw new ArgumentOutOfRangeException(nameof(userId));
            if (invoicePeriodEnd <= invoicePeriodStart)
                throw new ArgumentOutOfRangeException(nameof(invoicePeriodEnd), "Ngày kết thúc phải sau ngày bắt đầu.");

            return GetDashboardData(userId, invoicePeriodStart, invoicePeriodEnd);
        }

        public DashboardDataDto GetSalesStaffDashboardData(
            int userId,
            DateTime? invoicePeriodStart,
            DateTime? invoicePeriodEnd,
            DateTime revenuePeriodStart,
            DateTime revenuePeriodEnd)
        {
            if (userId <= 0) throw new ArgumentOutOfRangeException(nameof(userId));
            if (invoicePeriodStart.HasValue != invoicePeriodEnd.HasValue)
                throw new ArgumentException("Ngày bắt đầu và kết thúc lọc hóa đơn phải được cung cấp cùng nhau.");
            if (invoicePeriodStart.HasValue)
                ValidatePeriod(invoicePeriodStart.Value, invoicePeriodEnd.Value, nameof(invoicePeriodEnd));
            ValidatePeriod(revenuePeriodStart, revenuePeriodEnd, nameof(revenuePeriodEnd));

            return GetDashboardData(
                userId,
                invoicePeriodStart,
                invoicePeriodEnd,
                revenuePeriodStart,
                revenuePeriodEnd);
        }

        private DashboardDataDto GetDashboardData(
            int? salesStaffUserId,
            DateTime? invoicePeriodStart = null,
            DateTime? invoicePeriodEnd = null,
            DateTime? revenuePeriodStart = null,
            DateTime? revenuePeriodEnd = null)
        {
            var data = new DashboardDataDto
            {
                IsSalesStaffDashboard = salesStaffUserId.HasValue
            };
            DateTime today = DateTime.Today;
            DateTime firstTrendDate = today.AddDays(-6);

            using (SimsDataContext db = _connectionFactory.CreateDataContext())
            {
                DateTime tomorrow = today.AddDays(1);
                DateTime trendStart = revenuePeriodStart ?? firstTrendDate;
                DateTime trendEnd = revenuePeriodEnd ?? tomorrow;
                var invoices = db.Invoices.Where(i => i.Status == "ACTIVE");
                var userInvoices = invoices;
                if (salesStaffUserId.HasValue)
                    userInvoices = invoices.Where(i => i.CreatedBy == salesStaffUserId.Value);

                data.RevenueToday = invoices.Where(i => i.CreatedAt >= today && i.CreatedAt < tomorrow)
                    .Select(i => (decimal?)i.TotalAmount).Sum() ?? 0m;
                data.RevenueSelectedPeriod = invoices
                    .Where(i => i.CreatedAt >= trendStart && i.CreatedAt < trendEnd)
                    .Select(i => (decimal?)i.TotalAmount).Sum() ?? 0m;
                data.OrdersToday = userInvoices.Count(i => i.CreatedAt >= today && i.CreatedAt < tomorrow);

                if (salesStaffUserId.HasValue)
                {
                    data.RevenueLast7Days = invoices
                        .Where(i => i.CreatedAt >= firstTrendDate && i.CreatedAt < tomorrow)
                        .Select(i => (decimal?)i.TotalAmount).Sum() ?? 0m;
                    data.OrdersLast7Days = userInvoices
                        .Count(i => i.CreatedAt >= firstTrendDate && i.CreatedAt < tomorrow);
                }
                else
                {
                    data.StockQuantity = db.Products.Select(p => (long?)p.Stock).Sum() ?? 0L;
                    data.CustomerCount = db.Customers.Count();
                }

                if (trendEnd - trendStart > TimeSpan.FromDays(31))
                {
                    var revenueByMonth = invoices
                        .Where(i => i.CreatedAt >= trendStart && i.CreatedAt < trendEnd)
                        .GroupBy(i => new { i.CreatedAt.Year, i.CreatedAt.Month })
                        .Select(group => new
                        {
                            group.Key.Year,
                            group.Key.Month,
                            Revenue = group.Sum(i => i.TotalAmount)
                        })
                        .ToList()
                        .ToDictionary(
                            row => new DateTime(row.Year, row.Month, 1),
                            row => row.Revenue);

                    DateTime firstMonth = new DateTime(trendStart.Year, trendStart.Month, 1);
                    DateTime endMonth = new DateTime(trendEnd.Year, trendEnd.Month, 1);
                    for (DateTime month = firstMonth; month < endMonth; month = month.AddMonths(1))
                    {
                        data.RevenueTrend.Add(new DashboardRevenuePointDto
                        {
                            Date = month,
                            Revenue = revenueByMonth.TryGetValue(month, out decimal revenue) ? revenue : 0m
                        });
                    }
                }
                else
                {
                    var revenueByDate = invoices.Where(i => i.CreatedAt >= trendStart && i.CreatedAt < trendEnd)
                        .GroupBy(i => i.CreatedAt.Date)
                        .Select(group => new { Date = group.Key, Revenue = group.Sum(i => i.TotalAmount) })
                        .ToList()
                        .ToDictionary(row => row.Date.Date, row => row.Revenue);

                    for (DateTime date = trendStart.Date; date < trendEnd.Date; date = date.AddDays(1))
                    {
                        data.RevenueTrend.Add(new DashboardRevenuePointDto
                        {
                            Date = date,
                            Revenue = revenueByDate.TryGetValue(date, out decimal revenue) ? revenue : 0m
                        });
                    }
                }

                if (salesStaffUserId.HasValue)
                {
                    var recentInvoicesQuery = db.Invoices
                        .Where(i => i.CreatedBy == salesStaffUserId.Value);
                    if (invoicePeriodStart.HasValue)
                        recentInvoicesQuery = recentInvoicesQuery.Where(i => i.CreatedAt >= invoicePeriodStart.Value);
                    if (invoicePeriodEnd.HasValue)
                        recentInvoicesQuery = recentInvoicesQuery.Where(i => i.CreatedAt < invoicePeriodEnd.Value);

                    var recentInvoices = recentInvoicesQuery
                        .OrderByDescending(i => i.CreatedAt)
                        .ThenByDescending(i => i.InvoiceID)
                        .Select(i => new
                        {
                            i.InvoiceCode,
                            i.TotalAmount,
                            i.Status,
                            i.CreatedAt
                        })
                        .ToList();

                    foreach (var invoice in recentInvoices)
                        data.RecentActivities.Add(new DashboardActivityDto
                        {
                            Action = "INVOICE",
                            TableName = "Invoices",
                            Detail = invoice.InvoiceCode,
                            TotalAmount = invoice.TotalAmount,
                            RecordStatus = invoice.Status,
                            CreatedAt = invoice.CreatedAt
                        });
                }
                else
                {
                    var activities = (from log in db.AuditLogs
                                      join user in db.Users on log.UserID equals (int?)user.UserID into userLogs
                                      from user in userLogs.DefaultIfEmpty()
                                      orderby log.CreatedAt descending, log.LogID descending
                                      select new { log.Action, log.TableName, log.Detail, Username = user == null ? null : user.Username, log.CreatedAt })
                                     .Take(10).ToList();
                    foreach (var activity in activities)
                        data.RecentActivities.Add(new DashboardActivityDto
                        {
                            Action = activity.Action,
                            TableName = activity.TableName,
                            Detail = activity.Detail,
                            Username = activity.Username ?? "(đã xóa)",
                            CreatedAt = activity.CreatedAt
                        });
                }
            }

            return data;
        }

        private static void ValidatePeriod(DateTime periodStart, DateTime periodEnd, string endParameterName)
        {
            if (periodEnd <= periodStart)
                throw new ArgumentOutOfRangeException(endParameterName, "Ngày kết thúc phải sau ngày bắt đầu.");
        }
    }
}
