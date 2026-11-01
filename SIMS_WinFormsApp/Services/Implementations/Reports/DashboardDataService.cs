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

        public DashboardDataDto GetDashboardData()
        {
            return GetDashboardData(string.Empty, 0);
        }

        public DashboardDataDto GetDashboardData(string roleCode, int currentUserId)
        {
            return GetDashboardData(roleCode, currentUserId, null, null, null, null);
        }

        public DashboardDataDto GetDashboardData(DateTime revenuePeriodStart, DateTime revenuePeriodEnd)
        {
            return GetDashboardData(string.Empty, 0, null, null, revenuePeriodStart, revenuePeriodEnd);
        }

        public DashboardDataDto GetDashboardData(
            string roleCode,
            int currentUserId,
            DateTime? invoicePeriodStart,
            DateTime? invoicePeriodEnd,
            DateTime revenuePeriodStart,
            DateTime revenuePeriodEnd)
        {
            if (invoicePeriodStart.HasValue != invoicePeriodEnd.HasValue)
                throw new ArgumentException("Ngày bắt đầu và kết thúc lọc hóa đơn phải được cung cấp cùng nhau.");
            if (invoicePeriodStart.HasValue)
                ValidatePeriod(invoicePeriodStart.Value, invoicePeriodEnd.Value, nameof(invoicePeriodEnd));
            ValidatePeriod(revenuePeriodStart, revenuePeriodEnd, nameof(revenuePeriodEnd));
            return GetDashboardData(
                roleCode, currentUserId, invoicePeriodStart, invoicePeriodEnd,
                (DateTime?)revenuePeriodStart, revenuePeriodEnd);
        }

        public DashboardDataDto GetSalesStaffDashboardData(int userId)
        {
            return GetSalesStaffDashboardData(
                userId, null, null, DateTime.Today.AddDays(-6), DateTime.Today.AddDays(1));
        }

        public DashboardDataDto GetSalesStaffDashboardData(
            int userId,
            DateTime invoicePeriodStart,
            DateTime invoicePeriodEnd)
        {
            return GetSalesStaffDashboardData(
                userId, invoicePeriodStart, invoicePeriodEnd,
                DateTime.Today.AddDays(-6), DateTime.Today.AddDays(1));
        }

        public DashboardDataDto GetSalesStaffDashboardData(
            int userId,
            DateTime? invoicePeriodStart,
            DateTime? invoicePeriodEnd,
            DateTime revenuePeriodStart,
            DateTime revenuePeriodEnd)
        {
            if (userId <= 0)
                throw new ArgumentOutOfRangeException(nameof(userId));
            return GetDashboardData(
                RoleCodes.SalesStaff, userId, invoicePeriodStart, invoicePeriodEnd,
                revenuePeriodStart, revenuePeriodEnd);
        }

        private DashboardDataDto GetDashboardData(
            string roleCode,
            int currentUserId,
            DateTime? invoicePeriodStart,
            DateTime? invoicePeriodEnd,
            DateTime? revenuePeriodStart,
            DateTime? revenuePeriodEnd)
        {
            bool isSalesStaff = string.Equals(
                roleCode, RoleCodes.SalesStaff, StringComparison.OrdinalIgnoreCase);
            var data = new DashboardDataDto { IsSalesStaffDashboard = isSalesStaff };
            DateTime today = DateTime.Today;
            DateTime firstTrendDate = today.AddDays(-6);
            DateTime tomorrow = today.AddDays(1);

            using (SimsDataContext db = _connectionFactory.CreateDataContext())
            {
                if (string.Equals(roleCode, RoleCodes.InventoryManager, StringComparison.OrdinalIgnoreCase))
                    LoadInventoryDashboard(db, data, today, firstTrendDate, tomorrow);
                else if (string.Equals(roleCode, RoleCodes.SalesManager, StringComparison.OrdinalIgnoreCase))
                    LoadSalesManagementDashboard(db, data, today, firstTrendDate, tomorrow);
                else if (isSalesStaff && currentUserId > 0)
                {
                    LoadSalesDashboard(db, data, currentUserId, today, firstTrendDate, tomorrow);
                    LoadRecentInvoices(db, data, currentUserId, invoicePeriodStart, invoicePeriodEnd);
                }
                else
                    LoadOverviewDashboard(db, data, today, firstTrendDate, tomorrow);

                if (revenuePeriodStart.HasValue && revenuePeriodEnd.HasValue
                    && data.Kind != DashboardKind.Inventory)
                {
                    LoadRevenuePeriodTrend(
                        db, data, currentUserId, revenuePeriodStart.Value, revenuePeriodEnd.Value);
                }
            }

            return data;
        }

        private static void LoadRevenuePeriodTrend(
            SimsDataContext db,
            DashboardDataDto data,
            int currentUserId,
            DateTime periodStart,
            DateTime periodEnd)
        {
            ValidatePeriod(periodStart, periodEnd, nameof(periodEnd));
            var invoices = db.Invoices.Where(invoice => invoice.Status == "ACTIVE"
                && invoice.CreatedAt >= periodStart && invoice.CreatedAt < periodEnd);
            if (data.Kind == DashboardKind.Sales)
                invoices = invoices.Where(invoice => invoice.CreatedBy == currentUserId);
            data.ChartPoints.Clear();

            if (periodEnd - periodStart > TimeSpan.FromDays(31))
            {
                var totals = invoices
                    .GroupBy(invoice => new { invoice.CreatedAt.Year, invoice.CreatedAt.Month })
                    .Select(group => new
                    {
                        group.Key.Year,
                        group.Key.Month,
                        Total = group.Sum(invoice => invoice.TotalAmount)
                    })
                    .ToList()
                    .ToDictionary(item => new DateTime(item.Year, item.Month, 1), item => item.Total);
                for (DateTime month = new DateTime(periodStart.Year, periodStart.Month, 1);
                    month < periodEnd;
                    month = month.AddMonths(1))
                {
                    data.ChartPoints.Add(new DashboardChartPointDto
                    {
                        Date = month,
                        Value = totals.TryGetValue(month, out decimal total) ? total : 0m
                    });
                }
            }
            else
            {
                var totals = invoices
                    .GroupBy(invoice => invoice.CreatedAt.Date)
                    .Select(group => new { Date = group.Key, Total = group.Sum(invoice => invoice.TotalAmount) })
                    .ToList()
                    .ToDictionary(item => item.Date.Date, item => item.Total);
                for (DateTime date = periodStart.Date; date < periodEnd.Date; date = date.AddDays(1))
                {
                    data.ChartPoints.Add(new DashboardChartPointDto
                    {
                        Date = date,
                        Value = totals.TryGetValue(date, out decimal total) ? total : 0m
                    });
                }
            }

            data.ChartTitle = (data.Kind == DashboardKind.Sales ? "Doanh thu của bạn " : "Doanh thu ")
                + periodStart.ToString("dd/MM/yyyy")
                + " - " + periodEnd.AddDays(-1).ToString("dd/MM/yyyy");
        }

        private static void LoadRecentInvoices(
            SimsDataContext db,
            DashboardDataDto data,
            int userId,
            DateTime? periodStart,
            DateTime? periodEnd)
        {
            var invoices = db.Invoices.Where(invoice => invoice.CreatedBy == userId);
            if (periodStart.HasValue)
                invoices = invoices.Where(invoice => invoice.CreatedAt >= periodStart.Value);
            if (periodEnd.HasValue)
                invoices = invoices.Where(invoice => invoice.CreatedAt < periodEnd.Value);

            var recentInvoices = invoices
                .OrderByDescending(invoice => invoice.CreatedAt)
                .ThenByDescending(invoice => invoice.InvoiceID)
                .Select(invoice => new
                {
                    invoice.InvoiceCode,
                    invoice.TotalAmount,
                    invoice.Status,
                    invoice.CreatedAt
                })
                .Take(100)
                .ToList();
            foreach (var invoice in recentInvoices)
            {
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
            long expiredUnits = (from batch in db.InventoryBatches
                                 join product in db.Products on batch.ProductID equals product.ProductID
                                 where product.Status == "ACTIVE" && batch.Status != "DEPLETED"
                                     && batch.RemainingQty > 0 && batch.ExpiryDate.HasValue
                                     && batch.ExpiryDate.Value < today
                                 select (long?)batch.RemainingQty).Sum() ?? 0L;
            int batchesNearExpiry = (from batch in db.InventoryBatches
                                     join product in db.Products on batch.ProductID equals product.ProductID
                                     where product.Status == "ACTIVE" && batch.Status == "ACTIVE"
                                         && batch.RemainingQty > 0 && batch.ExpiryDate.HasValue
                                         && batch.ExpiryDate.Value >= today
                                         && batch.ExpiryDate.Value < today.AddDays(31)
                                     select batch.BatchID).Count();
            long sellableUnits = (from batch in db.InventoryBatches
                                  join product in db.Products on batch.ProductID equals product.ProductID
                                  where product.Status == "ACTIVE" && batch.Status == "ACTIVE"
                                      && batch.RemainingQty > 0
                                      && (!batch.ExpiryDate.HasValue || batch.ExpiryDate.Value >= today)
                                  select (long?)batch.RemainingQty).Sum() ?? 0L;
            AddMetric(data, "Sản phẩm cần bổ sung", lowStockProducts);
            AddMetric(data, "Hết hạn chờ xử lý (SP)", expiredUnits);
            AddMetric(data, "Lô sắp hết hạn (30 ngày)", batchesNearExpiry);
            AddMetric(data, "Tồn có thể bán (SP)", sellableUnits);

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

        private static void ValidatePeriod(DateTime periodStart, DateTime periodEnd, string endParameterName)
        {
            if (periodEnd <= periodStart)
                throw new ArgumentOutOfRangeException(endParameterName, "Ngày kết thúc phải sau ngày bắt đầu.");
        }
    }
}
