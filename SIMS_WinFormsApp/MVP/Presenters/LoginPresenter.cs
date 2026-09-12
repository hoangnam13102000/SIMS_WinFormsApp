using System;
using System.Threading.Tasks;
using SIMS_WinFormsApp.Views.Interfaces;
using SIMS_WinFormsApp.Services;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.UI.I18n;

namespace SIMS_WinFormsApp.MVP.Presenters
{
    public sealed class LoginPresenter
    {
        private readonly ILoginView _view;
        private readonly IAuthService _authService;

        public LoginPresenter(ILoginView view, IAuthService authService)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        public async Task LoginAsync()
        {
            _view.ClearError();

            string username = _view.Username?.Trim() ?? string.Empty;
            string password = _view.Password ?? string.Empty;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                _view.ShowError(Lang.Get("login.error.emptyFields"));
                return;
            }

            _view.SetLoading(true);
            try
            {
                var result = await Task.Run(() => _authService.TryLogin(username, password));

                switch (result.Status)
                {
                    case LoginStatus.Success:
                        _view.SaveRememberedUsername(username, _view.RememberMe);
                        _view.CloseOnSuccess();
                        return;

                    case LoginStatus.AccountLocked:
                        _view.ShowError(Lang.Get("login.error.locked"));
                        return;

                    case LoginStatus.AccountDisabled:
                        _view.ShowError(Lang.Get("login.error.disabled"));
                        return;

                    default:
                        _view.ShowError(Lang.Get("login.error.invalid"));
                        return;
                }
            }
            catch (Exception)
            {
                _view.ShowError(Lang.Get("login.error.configMissing"));
            }
            finally
            {
                _view.SetLoading(false);
            }
        }
    }
}
