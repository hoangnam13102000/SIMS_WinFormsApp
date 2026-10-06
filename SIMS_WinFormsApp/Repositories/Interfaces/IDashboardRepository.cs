using System;
using System.Collections.Generic;
using SIMS_WinFormsApp.Models.DTOs.Dashboard;

namespace SIMS_WinFormsApp.Repositories.Interfaces
{
    public interface IDashboardRepository
    {
        DashboardSummaryDto GetSummary(DateTime today);
        IReadOnlyList<DashboardRevenuePointDto> GetRevenueTrend(DateTime firstDay, DateTime dayAfterLast);
    }
}
