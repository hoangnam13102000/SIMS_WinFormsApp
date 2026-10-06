using System;
using System.Data.SqlClient;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.MVP.Presenters
{
    public sealed class DashboardPresenter
    {
        private readonly IDashboardView _view;
        private readonly string _displayName;
        private readonly IDashboardService _dashboardService;

        public DashboardPresenter(IDashboardView view, string displayName, IDashboardService dashboardService)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _displayName = string.IsNullOrWhiteSpace(displayName) ? "bạn" : displayName;
            _dashboardService = dashboardService ?? throw new ArgumentNullException(nameof(dashboardService));
        }

        public void Load()
        {
            _view.ShowDashboardUser(_displayName);
            try
            {
                _view.ShowDashboardData(_dashboardService.GetSummary());
            }
            catch (SqlException exception)
            {
                _view.ShowDashboardError(exception.Message);
            }
            catch (InvalidOperationException exception)
            {
                _view.ShowDashboardError(exception.Message);
            }
        }
    }
}
