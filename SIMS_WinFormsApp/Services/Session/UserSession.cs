using System;
using SIMS_WinFormsApp.Models;

namespace SIMS_WinFormsApp.Services.Session
{
  
    public sealed class UserSession
    {
        private static readonly Lazy<UserSession> LazyInstance = new Lazy<UserSession>(() => new UserSession());
        public static UserSession Instance => LazyInstance.Value;

        private UserSession() { }

        public User CurrentUser { get; private set; }
        public bool IsLoggedIn => CurrentUser != null;

        public void SignIn(User user) => CurrentUser = user;

        public void SignOut() => CurrentUser = null;

        public bool IsInRole(params string[] roleCodes)
        {
            if (CurrentUser == null) return false;
            foreach (var code in roleCodes)
            {
                if (string.Equals(CurrentUser.RoleCode, code, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}