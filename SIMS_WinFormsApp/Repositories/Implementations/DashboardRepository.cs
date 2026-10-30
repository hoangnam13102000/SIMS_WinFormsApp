using System;
using System.Collections.Generic;
using System.Linq;
using SIMS_WinFormsApp.DAL.Linq;
using SIMS_WinFormsApp.Infrastructure.Configuration;
using SIMS_WinFormsApp.Models.DTOs.Dashboard;
using SIMS_WinFormsApp.Repositories.Interfaces;

namespace SIMS_WinFormsApp.Repositories.Implementations
{
    public sealed class DashboardRepository : IDashboardRepository
    {
        private readonly IDbConnectionFactory _connections;

        public DashboardRepository(IDbConnectionFactory connections)
        {
            _connections = connections ?? throw new ArgumentNullException(nameof(connections));
        }

        public DashboardSummaryDto GetSummary(DateTime today)
        {
            using (var db = _connections.CreateDataContext())
            {
                DateTime start = today.Date;
                DateTime tomorrow = start.AddDays(1);
                DateTime yesterday = start.AddDays(-1);
                var summary = new DashboardSummaryDto
                {
                    RevenueToday = db.Invoices.Where(i => i.Status == "ACTIVE"
                        && i.CreatedAt >= start && i.CreatedAt < tomorrow)
                        .Select(i => (decimal?)i.TotalAmount).Sum() ?? 0m,
                    RevenueYesterday = db.Invoices.Where(i => i.Status == "ACTIVE"
                        && i.CreatedAt >= yesterday && i.CreatedAt < start)
                        .Select(i => (decimal?)i.TotalAmount).Sum() ?? 0m,
                    OrdersToday = db.Invoices.Count(i => i.Status == "ACTIVE"
                        && i.CreatedAt >= start && i.CreatedAt < tomorrow),
                    StockUnits = db.Products.Where(p => p.Status == "ACTIVE")
                        .Select(p => (long?)p.Stock).Sum() ?? 0L,
                    LowStockProducts = db.Products.Count(p => p.Status == "ACTIVE" && p.Stock <= p.MinStock),
                    Customers = (from customer in db.Customers
                                 join user in db.Users on customer.CustomerID equals user.UserID
                                 where !user.IsDeleted
                                 select customer.CustomerID).Count()
                };
                summary.RevenueTrend = GetRevenueTrend(db, start.AddDays(-6), tomorrow);
                return summary;
            }
        }

        public IReadOnlyList<DashboardRevenuePointDto> GetRevenueTrend(DateTime firstDay, DateTime dayAfterLast)
        {
            using (var db = _connections.CreateDataContext())
                return GetRevenueTrend(db, firstDay.Date, dayAfterLast.Date);
        }

        private static IReadOnlyList<DashboardRevenuePointDto> GetRevenueTrend(
            SimsDataContext db, DateTime firstDay, DateTime dayAfterLast)
        {
            return db.Invoices.Where(i => i.Status == "ACTIVE"
                    && i.CreatedAt >= firstDay && i.CreatedAt < dayAfterLast)
                .GroupBy(i => i.CreatedAt.Date)
                .Select(group => new DashboardRevenuePointDto
                {
                    Date = group.Key,
                    Revenue = group.Sum(i => i.TotalAmount)
                })
                .OrderBy(point => point.Date)
                .ToList();
        }
    }
}
