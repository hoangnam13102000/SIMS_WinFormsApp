using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        private const string DateFilter = @"
            i.Status = 'ACTIVE'
            AND i.CreatedAt >= @FromDate
            AND i.CreatedAt < @ToDateExclusive";

        private readonly IDbConnectionFactory _connectionFactory;

        public RevenueReportService(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public DateTime? GetLatestInvoiceDate()
        {
            using (SqlConnection connection = _connectionFactory.CreateConnection())
            using (var command = new SqlCommand(
                "SELECT MAX(CreatedAt) FROM Invoices WHERE Status = 'ACTIVE';",
                connection))
            {
                connection.Open();
                object result = command.ExecuteScalar();
                return result == null || result == DBNull.Value
                    ? (DateTime?)null
                    : Convert.ToDateTime(result).Date;
            }
        }

        public RevenueReportData GetReport(DateTime fromDate, DateTime toDate, RevenueReportPeriod period)
        {
            if (fromDate.Date > toDate.Date)
                throw new ArgumentException("Ngày bắt đầu không được sau ngày kết thúc.");

            if (!Enum.IsDefined(typeof(RevenueReportPeriod), period))
                throw new ArgumentOutOfRangeException(nameof(period));

            var report = new RevenueReportData();
            DateTime from = fromDate.Date;
            DateTime toExclusive = toDate.Date.AddDays(1);

            using (SqlConnection connection = _connectionFactory.CreateConnection())
            {
                connection.Open();

                using (var command = new SqlCommand(@"
                    SELECT ISNULL(SUM(i.TotalAmount), 0), COUNT(*)
                    FROM Invoices i
                    WHERE " + DateFilter + ";", connection))
                {
                    AddDateParameters(command, from, toExclusive);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            report.Revenue = reader.GetDecimal(0);
                            report.OrderCount = reader.GetInt32(1);
                        }
                    }
                }

                const string trendSql = @"
                    SELECT BucketStart, SUM(i.TotalAmount) AS Revenue, COUNT(*) AS OrderCount
                    FROM (
                        SELECT i.InvoiceID, i.TotalAmount,
                            CASE @Period
                                WHEN 0 THEN CONVERT(date, i.CreatedAt)
                                WHEN 1 THEN DATEFROMPARTS(YEAR(i.CreatedAt), MONTH(i.CreatedAt), 1)
                                ELSE DATEFROMPARTS(YEAR(i.CreatedAt), ((DATEPART(QUARTER, i.CreatedAt) - 1) * 3) + 1, 1)
                            END AS BucketStart
                        FROM Invoices i
                        WHERE " + DateFilter + @"
                    ) i
                    GROUP BY BucketStart
                    ORDER BY BucketStart;";

                using (var command = new SqlCommand(trendSql, connection))
                {
                    AddDateParameters(command, from, toExclusive);
                    command.Parameters.Add("@Period", SqlDbType.Int).Value = (int)period;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            report.Trend.Add(new RevenueTrendPoint
                            {
                                PeriodStart = reader.GetDateTime(0),
                                Revenue = reader.GetDecimal(1),
                                OrderCount = reader.GetInt32(2)
                            });
                        }
                    }
                }

                const string categorySql = @"
                    SELECT c.CategoryName, SUM(d.LineTotal) AS Revenue
                    FROM Invoices i
                    INNER JOIN InvoiceDetails d ON d.InvoiceID = i.InvoiceID
                    INNER JOIN Products p ON p.ProductID = d.ProductID
                    INNER JOIN Categories c ON c.CategoryID = p.CategoryID
                    WHERE " + DateFilter + @"
                    GROUP BY c.CategoryName
                    HAVING SUM(d.LineTotal) > 0
                    ORDER BY Revenue DESC;";

                using (var command = new SqlCommand(categorySql, connection))
                {
                    AddDateParameters(command, from, toExclusive);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            report.Categories.Add(new RevenueCategoryTotal
                            {
                                Name = reader.GetString(0),
                                Revenue = reader.GetDecimal(1)
                            });
                        }
                    }
                }

                const string productsSql = @"
                    SELECT TOP 5 p.ProductName, SUM(d.Quantity) AS Quantity, SUM(d.LineTotal) AS Revenue
                    FROM Invoices i
                    INNER JOIN InvoiceDetails d ON d.InvoiceID = i.InvoiceID
                    INNER JOIN Products p ON p.ProductID = d.ProductID
                    WHERE " + DateFilter + @"
                    GROUP BY p.ProductName
                    ORDER BY Revenue DESC;";

                using (var command = new SqlCommand(productsSql, connection))
                {
                    AddDateParameters(command, from, toExclusive);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            report.TopProducts.Add(new RevenueProductTotal
                            {
                                Name = reader.GetString(0),
                                Quantity = reader.GetInt32(1),
                                Revenue = reader.GetDecimal(2)
                            });
                        }
                    }
                }
            }

            return report;
        }

        private static void AddDateParameters(SqlCommand command, DateTime from, DateTime toExclusive)
        {
            command.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = from;
            command.Parameters.Add("@ToDateExclusive", SqlDbType.DateTime).Value = toExclusive;
        }
    }
}
