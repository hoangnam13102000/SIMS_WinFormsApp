using SIMS_WinFormsApp.Services;

namespace SIMS_WinFormsApp.Services.Interfaces
{
    public interface IAuthService
    {
        LoginResult TryLogin(string username, string password);
        ChangePasswordStatus ChangePassword(int userId, string currentPassword, string newPassword);
    }
}
