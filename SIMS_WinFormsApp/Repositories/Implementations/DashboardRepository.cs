using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            using (var connection = _connections.CreateConnection())
            {
                connection.Open();

                var summary = new DashboardSummaryDto();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
SELECT
    ISNULL((SELECT SUM(TotalAmount) FROM Invoices
            WHERE Status = 'ACTIVE' AND CreatedAt >= @Today AND CreatedAt < DATEADD(day, 1, @Today)), 0),
    ISNULL((SELECT SUM(TotalAmount) FROM Invoices
            WHERE Status = 'ACTIVE' AND CreatedAt >= DATEADD(day, -1, @Today) AND CreatedAt < @Today), 0),
    (SELECT COUNT(*) FROM Invoices
     WHERE Status = 'ACTIVE' AND CreatedAt >= @Today AND CreatedAt < DATEADD(day, 1, @Today)),
    ISNULL((SELECT SUM(CAST(Stock AS bigint)) FROM Products WHERE Status = 'ACTIVE'), 0),
    (SELECT COUNT(*) FROM Products WHERE Status = 'ACTIVE' AND Stock <= MinStock),
    (SELECT COUNT(*) FROM Customers c
     INNER JOIN Users u ON u.UserID = c.CustomerID
     WHERE u.IsDeleted = 0);";
                    command.Parameters.Add("@Today", SqlDbType.Date).Value = today.Date;

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                            throw new InvalidOperationException("Không nhận được dữ liệu tổng quan từ cơ sở dữ liệu.");

                        summary.RevenueToday = Convert.ToDecimal(reader.GetValue(0));
                        summary.RevenueYesterday = Convert.ToDecimal(reader.GetValue(1));
                        summary.OrdersToday = Convert.ToInt32(reader.GetValue(2));
                        summary.StockUnits = Convert.ToInt64(reader.GetValue(3));
                        summary.LowStockProducts = Convert.ToInt32(reader.GetValue(4));
                        summary.Customers = Convert.ToInt32(reader.GetValue(5));
                    }
                }

                summary.RevenueTrend = GetRevenueTrend(connection, today.Date.AddDays(-6), today.Date.AddDays(1));
                return summary;
            }
        }

        public IReadOnlyList<DashboardRevenuePointDto> GetRevenueTrend(DateTime firstDay, DateTime dayAfterLast)
        {
            using (var connection = _connections.CreateConnection())
            {
                connection.Open();
                return GetRevenueTrend(connection, firstDay.Date, dayAfterLast.Date);
            }
        }

        private static IReadOnlyList<DashboardRevenuePointDto> GetRevenueTrend(
            SqlConnection connection, DateTime firstDay, DateTime dayAfterLast)
        {
            var points = new List<DashboardRevenuePointDto>();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
SELECT CAST(CreatedAt AS date) AS RevenueDate, SUM(TotalAmount) AS Revenue
FROM Invoices
WHERE Status = 'ACTIVE' AND CreatedAt >= @FirstDay AND CreatedAt < @DayAfterLast
GROUP BY CAST(CreatedAt AS date)
ORDER BY RevenueDate;";
                command.Parameters.Add("@FirstDay", SqlDbType.Date).Value = firstDay;
                command.Parameters.Add("@DayAfterLast", SqlDbType.Date).Value = dayAfterLast;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        points.Add(new DashboardRevenuePointDto
                        {
                            Date = reader.GetDateTime(0),
                            Revenue = Convert.ToDecimal(reader.GetValue(1))
                        });
                    }
                }
            }

            return points;
        }
    }
}
