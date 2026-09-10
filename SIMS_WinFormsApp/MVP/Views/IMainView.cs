using System;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.MVP.Views
{
    public interface IMainView
    {
        void AttachLayout(Control layout);

        void SetWindowTitle(string title);
        void SetUserInfo(string displayName, string email, string avatarInitial = null);
        void SetUnreadNotifications(int count);
        void SetSidebarBadge(string pageKey, int count);

        void NavigateTo(string pageKey);
        void ShowMessage(string message, string caption, MessageBoxIcon icon);
        bool Confirm(string message, string caption);

        bool ConfirmLogout(string message, string caption, string confirmText, string cancelText);

        void CloseView();

        event EventHandler ViewReady;
        event EventHandler LogoutRequested;
        event EventHandler ProfileRequested;
        event EventHandler<string> PageChanged;
    }
}