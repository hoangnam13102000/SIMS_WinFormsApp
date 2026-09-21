using System;
using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.MVP.Presenters
{

    public sealed class UserAccountDetailPresenter : IDisposable
    {
        private readonly IUserAccountDetailView _view;
        private readonly IUserAccountDetailViewModelBuilder _viewModelBuilder;
        private readonly IUserManagementService _userManagementService;

        public UserAccountDetailPresenter(
            IUserAccountDetailView view,
            IUserAccountDetailViewModelBuilder viewModelBuilder,
            IUserManagementService userManagementService = null)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _viewModelBuilder = viewModelBuilder ?? throw new ArgumentNullException(nameof(viewModelBuilder));
            _userManagementService = userManagementService;

            _view.CloseRequested += OnCloseRequested;
        }

        public void Load(UserDetailDto user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var employeeProfile = _userManagementService?.GetEmployeeProfile(user.UserId);

            var viewModel = _viewModelBuilder.Build(user, employeeProfile);
            _view.Render(viewModel);
        }

        private void OnCloseRequested(object sender, EventArgs e) => _view.CloseView();

        public void Dispose()
        {
            _view.CloseRequested -= OnCloseRequested;
        }
    }
}