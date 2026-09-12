using System;

namespace SIMS_WinFormsApp.Views.Interfaces
{
    public interface IChangePasswordView
    {
        string CurrentPassword { get; }
        string NewPassword { get; }
        string Confirmation { get; }
        int CurrentUserId { get; }

        void ClearError();
        void ShowError(string message);
        void SetLoading(bool isBusy);
        void ShowSuccess(string message);
        void CloseOnSuccess();
    }
}
