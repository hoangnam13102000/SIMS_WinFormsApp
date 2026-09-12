using System;
using System.Collections.Generic;
using SIMS_WinFormsApp.Models.Chat;

namespace SIMS_WinFormsApp.Services.Chat
{
    public interface IChatClientService : IDisposable
    {
        bool IsConnected { get; }
        int CurrentUserId { get; }
        string CurrentUserName { get; }

        event Action<ChatMessageDto> MessageReceived;
        event Action<bool> ConnectionChanged;
        event Action<IReadOnlyList<OnlineUserInfo>> PresenceUpdated;

        void ConnectStaff(int userId, string userName, string roleCode);

        void Disconnect();

        bool SendStaffMessage(int toUserId, string text);

        void FlushPending();
    }

    public sealed class OnlineUserInfo
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string RoleCode { get; set; }

        // true = đang kết nối WebSocket thật sự (real-time presence).
        // false = lấy từ danh bạ nhân viên trong DB nhưng hiện không online.
        public bool IsOnline { get; set; }

        public override string ToString() => $"{UserName} ({RoleCode})";
    }
}