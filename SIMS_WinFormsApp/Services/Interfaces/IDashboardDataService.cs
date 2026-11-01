using System;
using SIMS_WinFormsApp.Models.DTOs.Dashboard;

namespace SIMS_WinFormsApp.Services.Interfaces
{
    public interface IDashboardDataService
    {
        DashboardDataDto GetDashboardData();
        DashboardDataDto GetSalesStaffDashboardData(int userId);
        DashboardDataDto GetSalesStaffDashboardData(int userId, DateTime invoicePeriodStart, DateTime invoicePeriodEnd);
    }
}
