using System;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.MVP.Presenters
{
    public sealed class DashboardPresenter
    {
        private readonly IDashboardView _view;
        private readonly string _displayName;

        public DashboardPresenter(IDashboardView view, string displayName)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _displayName = string.IsNullOrWhiteSpace(displayName) ? "bạn" : displayName;
        }

        public void Load()
        {
            _view.ShowDashboardUser(_displayName);
        }
    }
}
