using System;

namespace SIMS_WinFormsApp.Views.Interfaces
{
    public interface ILoginView
    {
        string Username { get; }
        string Password { get; }
        bool RememberMe { get; }

        void ClearError();
        void ShowError(string message);
        void SetLoading(bool isBusy);
        void SaveRememberedUsername(string username, bool rememberMe);
        void CloseOnSuccess();
    }
}
