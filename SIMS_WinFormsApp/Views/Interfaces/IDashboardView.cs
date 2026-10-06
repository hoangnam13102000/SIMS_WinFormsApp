using SIMS_WinFormsApp.Models.DTOs.Dashboard;

namespace SIMS_WinFormsApp.Views.Interfaces
{
    public interface IDashboardView
    {
        void ShowDashboardUser(string displayName);
        void ShowDashboardData(DashboardSummaryDto summary);
        void ShowDashboardError(string message);
    }
}
