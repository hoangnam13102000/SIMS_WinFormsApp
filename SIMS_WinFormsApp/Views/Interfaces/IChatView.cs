using System;
using System.Collections.Generic;
using SIMS_WinFormsApp.Models.Chat;
using SIMS_WinFormsApp.Services.Chat;

namespace SIMS_WinFormsApp.Views.Interfaces
{

    public interface IChatView
    {
        event Action ViewLoaded;
        event Action ViewUnloaded;
        event Action<int, string> SendRequested;       
        event Action<int> ConversationSelected;         

        void SetConnectionStatus(bool connected, string statusText);
        void SetOnlineUsers(IReadOnlyList<OnlineUserInfo> users, int currentUserId);
        void SetSelectedPeer(int userId, string displayName);
        void AppendMessage(ChatMessageDto message, bool isMine);
        void ClearMessages();
        void ShowInfo(string message);
        void ShowError(string message);
        void SetInputEnabled(bool enabled);
        void ClearInput();
    }
}