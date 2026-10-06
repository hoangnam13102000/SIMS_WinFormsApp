using System;
using System.Collections.Generic;

namespace SIMS_WinFormsApp.Models.DTOs.Dashboard
{
    public sealed class DashboardRevenuePointDto
    {
        public DateTime Date { get; set; }
        public decimal Revenue { get; set; }
    }

    public sealed class DashboardSummaryDto
    {
        public decimal RevenueToday { get; set; }
        public decimal RevenueYesterday { get; set; }
        public int OrdersToday { get; set; }
        public long StockUnits { get; set; }
        public int LowStockProducts { get; set; }
        public int Customers { get; set; }
        public IReadOnlyList<DashboardRevenuePointDto> RevenueTrend { get; set; }
    }
}
