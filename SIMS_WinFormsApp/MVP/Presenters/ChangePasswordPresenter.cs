using System;
using System.Threading.Tasks;
using SIMS_WinFormsApp.Views.Interfaces;
using SIMS_WinFormsApp.Services;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.UI.I18n;

namespace SIMS_WinFormsApp.MVP.Presenters
{
    public sealed class ChangePasswordPresenter
    {
        private readonly IChangePasswordView _view;
        private readonly IAuthService _authService;

        public ChangePasswordPresenter(IChangePasswordView view, IAuthService authService)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        public async Task ChangeAsync()
        {
            _view.ClearError();
            if (string.IsNullOrEmpty(_view.CurrentPassword) ||
                string.IsNullOrEmpty(_view.NewPassword) ||
                string.IsNullOrEmpty(_view.Confirmation))
            {
                _view.ShowError(Lang.Get("changepassword.error.emptyFields"));
                return;
            }

            if (_view.NewPassword != _view.Confirmation)
            {
                _view.ShowError(Lang.Get("changepassword.error.mismatch"));
                return;
            }

            _view.SetLoading(true);
            try
            {
                var result = await Task.Run(() => _authService.ChangePassword(
                    _view.CurrentUserId, _view.CurrentPassword, _view.NewPassword));

                switch (result)
                {
                    case ChangePasswordStatus.Success:
                        _view.ShowSuccess(Lang.Get("changepassword.success"));
                        _view.CloseOnSuccess();
                        break;
                    case ChangePasswordStatus.CurrentPasswordWrong:
                        _view.ShowError(Lang.Get("changepassword.error.currentWrong"));
                        break;
                    case ChangePasswordStatus.NewPasswordTooShort:
                        _view.ShowError(Lang.Get("changepassword.error.tooShort"));
                        break;
                    case ChangePasswordStatus.NewPasswordSameAsOld:
                        _view.ShowError(Lang.Get("changepassword.error.sameAsOld"));
                        break;
                }
            }
            catch (Exception)
            {
                _view.ShowError(Lang.Get("login.error.unexpected"));
            }
            finally
            {
                _view.SetLoading(false);
            }
        }
    }
}
