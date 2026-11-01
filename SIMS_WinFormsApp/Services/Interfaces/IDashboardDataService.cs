using System;
using SIMS_WinFormsApp.Models.DTOs.Dashboard;

namespace SIMS_WinFormsApp.Services.Interfaces
{
    public interface IDashboardDataService
    {
        DashboardDataDto GetDashboardData();
        DashboardDataDto GetDashboardData(DateTime revenuePeriodStart, DateTime revenuePeriodEnd);
        DashboardDataDto GetSalesStaffDashboardData(int userId);
        DashboardDataDto GetSalesStaffDashboardData(int userId, DateTime invoicePeriodStart, DateTime invoicePeriodEnd);
        DashboardDataDto GetSalesStaffDashboardData(
            int userId,
            DateTime? invoicePeriodStart,
            DateTime? invoicePeriodEnd,
            DateTime revenuePeriodStart,
            DateTime revenuePeriodEnd);
    }
}
