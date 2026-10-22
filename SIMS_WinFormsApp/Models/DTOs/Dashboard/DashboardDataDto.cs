using System;
using System.Collections.Generic;

namespace SIMS_WinFormsApp.Models.DTOs.Dashboard
{
    public sealed class DashboardDataDto
    {
        public decimal RevenueToday { get; set; }
        public long OrdersToday { get; set; }
        public long StockQuantity { get; set; }
        public long CustomerCount { get; set; }
        public List<DashboardRevenuePointDto> RevenueTrend { get; } = new List<DashboardRevenuePointDto>();
        public List<DashboardActivityDto> RecentActivities { get; } = new List<DashboardActivityDto>();
    }

    public sealed class DashboardRevenuePointDto
    {
        public DateTime Date { get; set; }
        public decimal Revenue { get; set; }
    }

    public sealed class DashboardActivityDto
    {
        public string Action { get; set; }
        public string TableName { get; set; }
        public string Detail { get; set; }
        public string Username { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
