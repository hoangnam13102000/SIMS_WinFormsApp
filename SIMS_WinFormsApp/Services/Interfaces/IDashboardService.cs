using SIMS_WinFormsApp.Models.DTOs.Dashboard;

namespace SIMS_WinFormsApp.Services.Interfaces
{
    public interface IDashboardService
    {
        DashboardSummaryDto GetSummary();
    }
}
