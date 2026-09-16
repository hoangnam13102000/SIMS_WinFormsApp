using System;

namespace SIMS_WinFormsApp.Views.Interfaces
{
    public interface IEditUserAccountView
    {
        string FullName { get; set; }
        string Email { get; set; }
        string Phone { get; set; }
        string AvatarFilePath { get; }

        event EventHandler SaveRequested;

        event EventHandler CancelRequested;

        void ShowError(string message);
        void ShowSuccess(string message);

        void SetSaving(bool isSaving);

        void CloseOnSuccess();
    }
}