using System;
using System.Collections.Generic;
using System.Linq;
using SIMS_WinFormsApp.DAL.Linq;
using SIMS_WinFormsApp.DAL.Linq.Entities.Chat;
using SIMS_WinFormsApp.Models.Chat;
using SIMS_WinFormsApp.Repositories.Interfaces;

namespace SIMS_WinFormsApp.Repositories.Implementations
{

    public class ChatRepository : IChatRepository
    {
        // Ghi 1 tin nhắn STAFF_DM: tự tạo hội thoại (nếu chưa có) rồi insert message ------
        public long SaveStaffMessage(int userIdA, int userIdB, int senderUserId, string senderName,
            string bodyText, DateTime createdAtUtc)
        {
            int lo = Math.Min(userIdA, userIdB);
            int hi = Math.Max(userIdA, userIdB);

            using (var db = new SimsDataContext())
            {
                var conv = db.ChatConversations.FirstOrDefault(c =>
                    c.ConversationType == "STAFF_DM" &&
                    c.StaffUserIdA == lo && c.StaffUserIdB == hi);

                if (conv == null)
                {
                    conv = new ChatConversationEntity
                    {
                        ConversationType = "STAFF_DM",
                        StaffUserIdA = lo,
                        StaffUserIdB = hi,
                        CreatedAt = createdAtUtc,
                        LastMessageAt = createdAtUtc,
                        IsClosed = false
                    };
                    db.ChatConversations.InsertOnSubmit(conv);
                    db.SubmitChanges(); // cần ConversationID sinh ra trước khi gắn cho message
                }

                var message = new ChatMessageEntity
                {
                    ConversationID = conv.ConversationID,
                    SenderUserID = senderUserId,
                    SenderName = senderName ?? string.Empty,
                    FromStaff = true,
                    BodyText = bodyText,
                    CreatedAt = createdAtUtc,
                    IsReadByPeer = false
                };
                db.ChatMessages.InsertOnSubmit(message);

                conv.LastMessageAt = createdAtUtc;

                db.SubmitChanges();
                return message.MessageID;
            }
        }

        // Lấy lịch sử hội thoại DM giữa 2 nhân viên (mới nhất -> cũ nhất -> đảo lại) -------
        public IReadOnlyList<ChatMessageDto> GetStaffConversationHistory(int userIdA, int userIdB, int maxMessages = 200)
        {
            int lo = Math.Min(userIdA, userIdB);
            int hi = Math.Max(userIdA, userIdB);

            using (var db = new SimsDataContext())
            {
                var conv = db.ChatConversations.FirstOrDefault(c =>
                    c.ConversationType == "STAFF_DM" &&
                    c.StaffUserIdA == lo && c.StaffUserIdB == hi);
                if (conv == null) return Array.Empty<ChatMessageDto>();

                var rows = (
                    from m in db.ChatMessages
                    where m.ConversationID == conv.ConversationID
                    orderby m.CreatedAt descending, m.MessageID descending
                    select m
                ).Take(maxMessages).ToList();

                rows.Reverse();

                return rows.Select(m => new ChatMessageDto
                {
                    Type = "STAFF_CHAT",
                    MessageId = m.MessageID,
                    UserId = m.SenderUserID,
                    UserName = m.SenderName,
                    Text = m.BodyText,
                    Timestamp = new DateTimeOffset(DateTime.SpecifyKind(m.CreatedAt, DateTimeKind.Utc))
                        .ToUnixTimeMilliseconds(),
                    ToUserId = m.SenderUserID == lo ? hi : lo,
                    Staff = true
                }).ToList();
            }
        }
    }
}