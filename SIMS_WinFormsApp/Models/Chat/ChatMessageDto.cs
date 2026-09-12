using System;

namespace SIMS_WinFormsApp.Models.Chat
{
    public sealed class ChatMessageDto
    {
        public string Type { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }
        public long Timestamp { get; set; }
        public long MessageId { get; set; }
        public bool FromAdmin { get; set; }

        public int ToUserId { get; set; }
        public string RoleCode { get; set; }
        public bool Staff { get; set; }

        public string[] OnlineUsers { get; set; }

        public ChatMessageDto() { }

        public ChatMessageDto(string type, int userId, string userName, string text)
        {
            Type = type;
            UserId = userId;
            UserName = userName;
            Text = text;
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }

        // ---------- Factory ----------

        public static ChatMessageDto Join(int userId, string userName)
            => new ChatMessageDto("JOIN", userId, userName, null);

        public static ChatMessageDto Leave(int userId, string userName)
            => new ChatMessageDto("LEAVE", userId, userName, null);

        public static ChatMessageDto StaffJoin(int userId, string userName, string roleCode)
        {
            var m = new ChatMessageDto("STAFF_JOIN", userId, userName, null)
            {
                RoleCode = roleCode,
                Staff = true
            };
            return m;
        }

        public static ChatMessageDto StaffChat(int fromUserId, string fromName, int toUserId, string text)
        {
            return new ChatMessageDto("STAFF_CHAT", fromUserId, fromName, text)
            {
                ToUserId = toUserId,
                Staff = true
            };
        }

        public static ChatMessageDto Presence(string[] onlineUsers)
        {
            return new ChatMessageDto
            {
                Type = "PRESENCE",
                OnlineUsers = onlineUsers ?? Array.Empty<string>(),
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
            };
        }

        public bool IsStaffChat => string.Equals(Type, "STAFF_CHAT", StringComparison.OrdinalIgnoreCase);
        public bool IsJoin => string.Equals(Type, "JOIN", StringComparison.OrdinalIgnoreCase)
                           || string.Equals(Type, "STAFF_JOIN", StringComparison.OrdinalIgnoreCase);
        public bool IsLeave => string.Equals(Type, "LEAVE", StringComparison.OrdinalIgnoreCase);
        public bool IsPresence => string.Equals(Type, "PRESENCE", StringComparison.OrdinalIgnoreCase);
    }
}