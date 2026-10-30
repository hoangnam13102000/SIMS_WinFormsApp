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
            var data = new DashboardDataDto();
            DateTime today = DateTime.Today;
            DateTime firstTrendDate = today.AddDays(-6);

            using (SimsDataContext db = _connectionFactory.CreateDataContext())
            {
                DateTime tomorrow = today.AddDays(1);
                data.RevenueToday = db.Invoices.Where(i => i.Status == "ACTIVE"
                        && i.CreatedAt >= today && i.CreatedAt < tomorrow)
                    .Select(i => (decimal?)i.TotalAmount).Sum() ?? 0m;
                data.OrdersToday = db.Invoices.Count(i => i.Status == "ACTIVE"
                    && i.CreatedAt >= today && i.CreatedAt < tomorrow);
                data.StockQuantity = db.Products.Select(p => (long?)p.Stock).Sum() ?? 0L;
                data.CustomerCount = db.Customers.Count();

                var revenueByDate = db.Invoices.Where(i => i.Status == "ACTIVE"
                        && i.CreatedAt >= firstTrendDate && i.CreatedAt < tomorrow)
                    .GroupBy(i => i.CreatedAt.Date)
                    .Select(group => new { Date = group.Key, Revenue = group.Sum(i => i.TotalAmount) })
                    .ToList()
                    .ToDictionary(row => row.Date.Date, row => row.Revenue);

                for (int day = 0; day < 7; day++)
                {
                    DateTime date = firstTrendDate.AddDays(day);
                    data.RevenueTrend.Add(new DashboardRevenuePointDto
                    {
                        Date = date,
                        Revenue = revenueByDate.TryGetValue(date, out decimal revenue) ? revenue : 0m
                    });
                }

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

            return data;
        }
    }
}
