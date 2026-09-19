using System;
using System.Threading.Tasks;
using SIMS_WinFormsApp.Views.Interfaces;
using SIMS_WinFormsApp.Services;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.Repositories.Interfaces;
using SIMS_WinFormsApp.Infrastructure.Composition;
using SIMS_WinFormsApp.UI.I18n;

namespace SIMS_WinFormsApp.MVP.Presenters
{
    public sealed class LoginPresenter
    {
        private readonly ILoginView _view;
        private readonly IAuthService _authService;
        private readonly IAuditLogWriter _auditLogWriter;

        public LoginPresenter(ILoginView view, IAuthService authService, IAuditLogWriter auditLogWriter = null)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            // MỚI: optional - null vẫn chạy được (không ghi log), giữ nguyên khả năng tương thích
            // ngược với nơi gọi cũ chưa truyền tham số này.
            _auditLogWriter = auditLogWriter ?? AppComposition.CreateAuditLogWriter();
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
                        _auditLogWriter?.Record(result.User?.UserId, "LOGIN", "Users", result.User?.UserId,
                            detail: (result.User?.FullName ?? username) + " đã đăng nhập thành công.");
                        _view.SaveRememberedUsername(username, _view.RememberMe);
                        _view.CloseOnSuccess();
                        return;

                    case LoginStatus.AccountLocked:
                        _auditLogWriter?.Record(null, "LOGIN_FAILED", "Users",
                            detail: "Đăng nhập thất bại (tài khoản '" + username + "' đang bị khóa).");
                        _view.ShowError(Lang.Get("login.error.locked"));
                        return;

                    case LoginStatus.AccountDisabled:
                        _auditLogWriter?.Record(null, "LOGIN_FAILED", "Users",
                            detail: "Đăng nhập thất bại (tài khoản '" + username + "' đã bị vô hiệu hóa).");
                        _view.ShowError(Lang.Get("login.error.disabled"));
                        return;

                    default:
                        _auditLogWriter?.Record(null, "LOGIN_FAILED", "Users",
                            detail: "Đăng nhập thất bại (sai tên đăng nhập hoặc mật khẩu: '" + username + "').");
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