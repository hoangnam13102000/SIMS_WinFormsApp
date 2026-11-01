using System;
using System.Threading.Tasks;
using SIMS_WinFormsApp.Models.DTOs.Dashboard;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.MVP.Presenters
{
    public sealed class DashboardPresenter
    {
        private readonly IDashboardView _view;
        private readonly IDashboardDataService _dataService;
        private readonly string _displayName;
        private readonly int? _salesStaffUserId;

        public DashboardPresenter(
            IDashboardView view,
            string displayName,
            IDashboardDataService dataService,
            int? salesStaffUserId = null)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
            _displayName = string.IsNullOrWhiteSpace(displayName) ? "bạn" : displayName;
            _salesStaffUserId = salesStaffUserId;
        }

        public async Task LoadAsync()
        {
            _view.ShowDashboardUser(_displayName);
            DashboardDataDto data = await Task.Run(() => _salesStaffUserId.HasValue
                ? _dataService.GetSalesStaffDashboardData(_salesStaffUserId.Value)
                : _dataService.GetDashboardData());
            _view.ShowDashboardData(data);
        }
    }
}
