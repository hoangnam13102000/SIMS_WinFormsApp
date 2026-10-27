using System;
using System.Data;
using System.Data.SqlClient;
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

            using (SqlConnection connection = _connectionFactory.CreateConnection())
            {
                connection.Open();

                using (var command = new SqlCommand(@"
                    SELECT
                        COALESCE((
                            SELECT SUM(i.TotalAmount)
                            FROM Invoices i
                            WHERE i.Status = 'ACTIVE'
                                AND i.CreatedAt >= @Today
                                AND i.CreatedAt < @Tomorrow
                        ), 0) AS RevenueToday,
                        (
                            SELECT COUNT_BIG(*)
                            FROM Invoices i
                            WHERE i.Status = 'ACTIVE'
                                AND i.CreatedAt >= @Today
                                AND i.CreatedAt < @Tomorrow
                        ) AS OrdersToday,
                        COALESCE((
                            SELECT SUM(CONVERT(bigint, p.Stock))
                            FROM Products p
                        ), 0) AS StockQuantity,
                        (
                            SELECT COUNT_BIG(*)
                            FROM Customers
                        ) AS CustomerCount;", connection))
                {
                    command.Parameters.Add("@Today", SqlDbType.DateTime).Value = today;
                    command.Parameters.Add("@Tomorrow", SqlDbType.DateTime).Value = today.AddDays(1);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            data.RevenueToday = Convert.ToDecimal(reader["RevenueToday"]);
                            data.OrdersToday = Convert.ToInt64(reader["OrdersToday"]);
                            data.StockQuantity = Convert.ToInt64(reader["StockQuantity"]);
                            data.CustomerCount = Convert.ToInt64(reader["CustomerCount"]);
                        }
                    }
                }

                var revenueByDate = new System.Collections.Generic.Dictionary<DateTime, decimal>();
                using (var command = new SqlCommand(@"
                    SELECT CONVERT(date, i.CreatedAt) AS InvoiceDate, SUM(i.TotalAmount) AS Revenue
                    FROM Invoices i
                    WHERE i.Status = 'ACTIVE'
                        AND i.CreatedAt >= @FromDate
                        AND i.CreatedAt < @ToDateExclusive
                    GROUP BY CONVERT(date, i.CreatedAt)
                    ORDER BY InvoiceDate;", connection))
                {
                    command.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = firstTrendDate;
                    command.Parameters.Add("@ToDateExclusive", SqlDbType.DateTime).Value = today.AddDays(1);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            revenueByDate[reader.GetDateTime(0).Date] = reader.GetDecimal(1);
                    }
                }

                for (int day = 0; day < 7; day++)
                {
                    DateTime date = firstTrendDate.AddDays(day);
                    data.RevenueTrend.Add(new DashboardRevenuePointDto
                    {
                        Date = date,
                        Revenue = revenueByDate.TryGetValue(date, out decimal revenue) ? revenue : 0m
                    });
                }

                using (var command = new SqlCommand(@"
                    SELECT TOP (@Take)
                        l.Action,
                        l.TableName,
                        l.Detail,
                        COALESCE(u.Username, N'(đã xóa)') AS Username,
                        l.CreatedAt
                    FROM AuditLogs l
                    LEFT JOIN Users u ON u.UserID = l.UserID
                    ORDER BY l.CreatedAt DESC, l.LogID DESC;", connection))
                {
                    command.Parameters.Add("@Take", SqlDbType.Int).Value = 10;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            data.RecentActivities.Add(new DashboardActivityDto
                            {
                                Action = reader.GetString(0),
                                TableName = reader.IsDBNull(1) ? null : reader.GetString(1),
                                Detail = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Username = reader.GetString(3),
                                CreatedAt = reader.GetDateTime(4)
                            });
                        }
                    }
                }
            }

            return data;
        }
    }
}
