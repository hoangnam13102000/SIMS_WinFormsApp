using System;
using System.Collections.Generic;
using SIMS_WinFormsApp.Models.Chat;

namespace SIMS_WinFormsApp.Repositories.Interfaces
{
    public interface IChatRepository
    {
        long SaveStaffMessage(int userIdA, int userIdB, int senderUserId, string senderName,
            string bodyText, DateTime createdAtUtc);

        IReadOnlyList<ChatMessageDto> GetStaffConversationHistory(int userIdA, int userIdB, int maxMessages = 200);
    }
}
