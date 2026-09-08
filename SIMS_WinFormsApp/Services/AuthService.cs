using SIMS_WinFormsApp.DAL;
using SIMS_WinFormsApp.Models;
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
        public LoginStatus Status { get; set; }
        public User User { get; set; }

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

    public class AuthService
    {
        private const int MinNewPasswordLength = 8;

        private readonly UserRepository _userRepository;

        public AuthService() : this(new UserRepository()) { }

        public AuthService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public LoginResult TryLogin(string username, string password)
        {
            var user = _userRepository.FindByUsername(username);
            if (user == null)
                return LoginResult.Fail(LoginStatus.InvalidCredentials);

            if (user.IsLocked)
                return LoginResult.Fail(LoginStatus.AccountLocked);

            if (user.Status == "DISABLED")
                return LoginResult.Fail(LoginStatus.AccountDisabled);

            if (!PasswordHasher.Verify(password, user.PasswordHash))
            {
                _userRepository.RegisterFailedLogin(user.UserId);
                return LoginResult.Fail(LoginStatus.InvalidCredentials);
            }

            _userRepository.ResetFailedLogin(user.UserId);
            UserSession.Instance.SignIn(user);
            return LoginResult.Ok(user);
        }

        public ChangePasswordStatus ChangePassword(int userId, string currentPassword, string newPassword)
        {
            var user = _userRepository.FindById(userId);
            if (user == null || !PasswordHasher.Verify(currentPassword, user.PasswordHash))
                return ChangePasswordStatus.CurrentPasswordWrong;

            if (string.IsNullOrEmpty(newPassword) || newPassword.Length < MinNewPasswordLength)
                return ChangePasswordStatus.NewPasswordTooShort;

            if (PasswordHasher.Verify(newPassword, user.PasswordHash))
                return ChangePasswordStatus.NewPasswordSameAsOld;

            string newHash = PasswordHasher.Hash(newPassword);
            _userRepository.UpdatePassword(userId, newHash);

            return ChangePasswordStatus.Success;
        }
    }
}