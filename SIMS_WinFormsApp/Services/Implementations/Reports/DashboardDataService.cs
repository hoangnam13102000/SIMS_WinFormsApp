using System;
using System.Linq;
using SIMS_WinFormsApp.DAL.Linq;
using SIMS_WinFormsApp.Infrastructure.Configuration;
using SIMS_WinFormsApp.Models.DTOs.Dashboard;
using SIMS_WinFormsApp.Models.Enums;
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

        public DashboardDataDto GetDashboardData(string roleCode, int currentUserId)
        {
            var data = new DashboardDataDto();
            DateTime today = DateTime.Today;
            DateTime firstTrendDate = today.AddDays(-6);

            using (SimsDataContext db = _connectionFactory.CreateDataContext())
            {
                DateTime tomorrow = today.AddDays(1);
                if (string.Equals(roleCode, RoleCodes.InventoryManager, StringComparison.OrdinalIgnoreCase))
                    LoadInventoryDashboard(db, data, today, firstTrendDate, tomorrow);
                else if (string.Equals(roleCode, RoleCodes.SalesManager, StringComparison.OrdinalIgnoreCase))
                    LoadSalesManagementDashboard(db, data, today, firstTrendDate, tomorrow);
                else if (string.Equals(roleCode, RoleCodes.SalesStaff, StringComparison.OrdinalIgnoreCase))
                    LoadSalesDashboard(db, data, currentUserId, today, firstTrendDate, tomorrow);
                else
                    LoadOverviewDashboard(db, data, today, firstTrendDate, tomorrow);
            }

            return data;
        }

        private static void LoadOverviewDashboard(
            SimsDataContext db, DashboardDataDto data, DateTime today, DateTime firstTrendDate, DateTime tomorrow)
        {
            data.Kind = DashboardKind.Overview;
            data.ChartTitle = "Doanh thu 7 ngày gần nhất";
            data.SeriesTitle = "Doanh thu";

            decimal revenueToday = db.Invoices.Where(i => i.Status == "ACTIVE"
                    && i.CreatedAt >= today && i.CreatedAt < tomorrow)
                .Select(i => (decimal?)i.TotalAmount).Sum() ?? 0m;
            int ordersToday = db.Invoices.Count(i => i.Status == "ACTIVE"
                && i.CreatedAt >= today && i.CreatedAt < tomorrow);
            long stockQuantity = db.Products.Select(p => (long?)p.Stock).Sum() ?? 0L;
            int customerCount = db.Customers.Count();
            AddMetric(data, "Doanh thu hôm nay", revenueToday, true);
            AddMetric(data, "Hóa đơn hôm nay", ordersToday);
            AddMetric(data, "Tổng tồn kho", stockQuantity);
            AddMetric(data, "Khách hàng", customerCount);

            var trend = db.Invoices.Where(i => i.Status == "ACTIVE"
                    && i.CreatedAt >= firstTrendDate && i.CreatedAt < tomorrow)
                .GroupBy(i => i.CreatedAt.Date)
                .Select(group => new { Date = group.Key, Value = group.Sum(i => i.TotalAmount) })
                .ToList()
                .ToDictionary(row => row.Date.Date, row => row.Value);
            AddTrend(data, firstTrendDate, trend);
            AddRecentActivities(db, data);
        }

        private static void LoadSalesManagementDashboard(
            SimsDataContext db, DashboardDataDto data, DateTime today, DateTime firstTrendDate, DateTime tomorrow)
        {
            data.Kind = DashboardKind.SalesManagement;
            data.ChartTitle = "Doanh thu toàn cửa hàng 7 ngày gần nhất";
            data.SeriesTitle = "Doanh thu";

            var invoices = db.Invoices.Where(i => i.Status == "ACTIVE");
            decimal revenueToday = invoices.Where(i => i.CreatedAt >= today && i.CreatedAt < tomorrow)
                .Select(i => (decimal?)i.TotalAmount).Sum() ?? 0m;
            int ordersToday = invoices.Count(i => i.CreatedAt >= today && i.CreatedAt < tomorrow);
            int pendingReturns = db.ReturnExchanges.Count(r => r.Status == "PENDING");
            decimal approvedReturnsToday = db.ReturnExchanges
                .Where(r => r.Status == "APPROVED" && r.ApprovedAt >= today && r.ApprovedAt < tomorrow)
                .Select(r => (decimal?)r.TotalValue).Sum() ?? 0m;
            AddMetric(data, "Doanh thu hôm nay", revenueToday, true);
            AddMetric(data, "Hóa đơn hôm nay", ordersToday);
            AddMetric(data, "Yêu cầu đổi/trả chờ duyệt", pendingReturns);
            AddMetric(data, "Giá trị trả hàng đã duyệt hôm nay", approvedReturnsToday, true);

            var trend = invoices.Where(i => i.CreatedAt >= firstTrendDate && i.CreatedAt < tomorrow)
                .GroupBy(i => i.CreatedAt.Date)
                .Select(group => new { Date = group.Key, Value = group.Sum(i => i.TotalAmount) })
                .ToList()
                .ToDictionary(row => row.Date.Date, row => row.Value);
            AddTrend(data, firstTrendDate, trend);
        }

        private static void LoadInventoryDashboard(
            SimsDataContext db, DashboardDataDto data, DateTime today, DateTime firstTrendDate, DateTime tomorrow)
        {
            data.Kind = DashboardKind.Inventory;
            data.ChartTitle = "Số lượng nhập kho 7 ngày gần nhất";
            data.SeriesTitle = "Sản phẩm nhập";

            int lowStockProducts = db.Products.Count(p => p.Status == "ACTIVE" && p.Stock <= p.MinStock);
            long expiredUnits = db.InventoryBatches.Where(b => b.Status != "DEPLETED"
                    && b.RemainingQty > 0 && b.ExpiryDate.HasValue && b.ExpiryDate.Value < today)
                .Select(b => (long?)b.RemainingQty).Sum() ?? 0L;
            int batchesNearExpiry = db.InventoryBatches.Count(b => b.Status == "ACTIVE"
                && b.RemainingQty > 0 && b.ExpiryDate.HasValue
                && b.ExpiryDate.Value >= today && b.ExpiryDate.Value < today.AddDays(31));
            long totalStock = db.Products.Select(p => (long?)p.Stock).Sum() ?? 0L;
            AddMetric(data, "Sản phẩm cần bổ sung", lowStockProducts);
            AddMetric(data, "Số lượng đã hết hạn", expiredUnits);
            AddMetric(data, "Lô sắp hết hạn (30 ngày)", batchesNearExpiry);
            AddMetric(data, "Tổng tồn kho", totalStock);

            var imports = db.InventoryTransactions.Where(t => t.TransactionType == "IMPORT"
                    && t.CreatedAt >= firstTrendDate && t.CreatedAt < tomorrow)
                .GroupBy(t => t.CreatedAt.Date)
                .Select(group => new { Date = group.Key, Value = group.Sum(t => t.Quantity) })
                .ToList()
                .ToDictionary(row => row.Date.Date, row => (decimal)row.Value);
            AddTrend(data, firstTrendDate, imports);
        }

        private static void LoadSalesDashboard(
            SimsDataContext db, DashboardDataDto data, int currentUserId,
            DateTime today, DateTime firstTrendDate, DateTime tomorrow)
        {
            data.Kind = DashboardKind.Sales;
            data.ChartTitle = "Doanh thu của bạn 7 ngày gần nhất";
            data.SeriesTitle = "Doanh thu";

            var invoices = db.Invoices.Where(i => i.CreatedBy == currentUserId && i.Status == "ACTIVE");
            decimal revenueToday = invoices.Where(i => i.CreatedAt >= today && i.CreatedAt < tomorrow)
                .Select(i => (decimal?)i.TotalAmount).Sum() ?? 0m;
            int ordersToday = invoices.Count(i => i.CreatedAt >= today && i.CreatedAt < tomorrow);
            long returnedUnitsToday = (from request in db.ReturnExchanges
                                       join line in db.ReturnExchangeDetails on request.ReturnID equals line.ReturnID
                                       where request.CreatedBy == currentUserId
                                           && request.Status == "APPROVED"
                                           && request.ApprovedAt >= today && request.ApprovedAt < tomorrow
                                           && line.Direction == "IN"
                                       select (long?)line.Quantity).Sum() ?? 0L;
            decimal averageOrderValue = ordersToday == 0 ? 0m : revenueToday / ordersToday;
            AddMetric(data, "Doanh thu hôm nay", revenueToday, true);
            AddMetric(data, "Hóa đơn hôm nay", ordersToday);
            AddMetric(data, "Số lượng hàng hoàn trả", returnedUnitsToday);
            AddMetric(data, "Giá trị hóa đơn TB", averageOrderValue, true);

            var trend = invoices.Where(i => i.CreatedAt >= firstTrendDate && i.CreatedAt < tomorrow)
                .GroupBy(i => i.CreatedAt.Date)
                .Select(group => new { Date = group.Key, Value = group.Sum(i => i.TotalAmount) })
                .ToList()
                .ToDictionary(row => row.Date.Date, row => row.Value);
            AddTrend(data, firstTrendDate, trend);
        }

        private static void AddMetric(DashboardDataDto data, string caption, decimal value, bool isCurrency = false)
        {
            data.Metrics.Add(new DashboardMetricDto
            {
                Caption = caption,
                Value = value,
                IsCurrency = isCurrency
            });
        }

        private static void AddTrend(
            DashboardDataDto data, DateTime firstDate, System.Collections.Generic.IDictionary<DateTime, decimal> values)
        {
            for (int day = 0; day < 7; day++)
            {
                DateTime date = firstDate.AddDays(day);
                data.ChartPoints.Add(new DashboardChartPointDto
                {
                    Date = date,
                    Value = values.TryGetValue(date, out decimal value) ? value : 0m
                });
            }
        }

        private static void AddRecentActivities(SimsDataContext db, DashboardDataDto data)
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
}
