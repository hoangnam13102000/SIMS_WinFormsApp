using SIMS_WinFormsApp.Models;

namespace SIMS_WinFormsApp.Services.Session
{
    public interface IUserSession
    {
        User CurrentUser { get; }
        bool IsLoggedIn { get; }
        void SignIn(User user);
        void SignOut();
        bool IsInRole(params string[] roleCodes);
    }
}
