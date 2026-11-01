using System;
using System.Collections.Generic;

namespace SIMS_WinFormsApp.Models.DTOs.Dashboard
{
    public sealed class DashboardDataDto
    {
        public bool IsSalesStaffDashboard { get; set; }
        public DashboardKind Kind { get; set; }
        public string ChartTitle { get; set; }
        public string SeriesTitle { get; set; }
        public List<DashboardMetricDto> Metrics { get; } = new List<DashboardMetricDto>();
        public List<DashboardChartPointDto> ChartPoints { get; } = new List<DashboardChartPointDto>();
        public List<DashboardActivityDto> RecentActivities { get; } = new List<DashboardActivityDto>();
    }

    public enum DashboardKind
    {
        Overview,
        Inventory,
        Sales,
        SalesManagement
    }

    public sealed class DashboardMetricDto
    {
        public string Caption { get; set; }
        public decimal Value { get; set; }
        public bool IsCurrency { get; set; }
    }

    public sealed class DashboardChartPointDto
    {
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
    }

    public sealed class DashboardActivityDto
    {
        public string Action { get; set; }
        public string TableName { get; set; }
        public string Detail { get; set; }
        public string Username { get; set; }
        public decimal? TotalAmount { get; set; }
        public string RecordStatus { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
