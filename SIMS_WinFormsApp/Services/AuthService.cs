using SIMS_WinFormsApp.Models;
using SIMS_WinFormsApp.Models.Enums;
using SIMS_WinFormsApp.Repositories.Interfaces;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.Services.Security;
using SIMS_WinFormsApp.Services.Session;

namespace SIMS_WinFormsApp.Services
{
    public enum LoginStatus
    {
        Success,
        InvalidCredentials,
        AccountLocked,
        AccountDisabled
    }

    public class LoginResult
    {
        public LoginStatus Status { get; private set; }
        public User User { get; private set; }

        public static LoginResult Ok(User user) => new LoginResult { Status = LoginStatus.Success, User = user };
        public static LoginResult Fail(LoginStatus status) => new LoginResult { Status = status };
    }

    public enum ChangePasswordStatus
    {
        Success,
        CurrentPasswordWrong,
        NewPasswordTooShort,
        NewPasswordSameAsOld
    }

    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserSession _session;
        private readonly IPasswordHasher _passwordHasher;

        public AuthService(
            IUserRepository userRepository,
            IUserSession session,
            IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository ?? throw new System.ArgumentNullException(nameof(userRepository));
            _session = session ?? throw new System.ArgumentNullException(nameof(session));
            _passwordHasher = passwordHasher ?? throw new System.ArgumentNullException(nameof(passwordHasher));
        }

        public LoginResult TryLogin(string username, string password)
        {
            var user = _userRepository.FindByUsername(username);
            if (user == null)
                return LoginResult.Fail(LoginStatus.InvalidCredentials);

            if (user.IsLocked)
                return LoginResult.Fail(LoginStatus.AccountLocked);

            if (user.Status == UserStatus.Disabled)
                return LoginResult.Fail(LoginStatus.AccountDisabled);

            if (!_passwordHasher.Verify(password, user.PasswordHash))
            {
                _userRepository.RegisterFailedLogin(user.UserId, AppConstants.MaxFailedLoginAttempts);
                return LoginResult.Fail(LoginStatus.InvalidCredentials);
            }

            _userRepository.ResetFailedLogin(user.UserId);
            _session.SignIn(user);
            return LoginResult.Ok(user);
        }

        public ChangePasswordStatus ChangePassword(int userId, string currentPassword, string newPassword)
        {
            var user = _userRepository.FindById(userId);
            if (user == null || !_passwordHasher.Verify(currentPassword, user.PasswordHash))
                return ChangePasswordStatus.CurrentPasswordWrong;

            if (string.IsNullOrEmpty(newPassword) || newPassword.Length < AppConstants.MinPasswordLength)
                return ChangePasswordStatus.NewPasswordTooShort;

            if (_passwordHasher.Verify(newPassword, user.PasswordHash))
                return ChangePasswordStatus.NewPasswordSameAsOld;

            string newHash = _passwordHasher.Hash(newPassword);
            _userRepository.UpdatePassword(userId, newHash);

            return ChangePasswordStatus.Success;
        }
    }
}