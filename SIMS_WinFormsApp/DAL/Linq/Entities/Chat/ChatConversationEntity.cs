using System;
using System.Data.Linq.Mapping;

namespace SIMS_WinFormsApp.DAL.Linq.Entities.Chat
{
    [Table(Name = "ChatConversations")]
    public class ChatConversationEntity
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int ConversationID { get; set; }

        [Column]
        public string ConversationType { get; set; }

        [Column]
        public int? CustomerUserID { get; set; }

        [Column]
        public int? StaffUserIdA { get; set; }

        [Column]
        public int? StaffUserIdB { get; set; }

        [Column]
        public DateTime CreatedAt { get; set; }

        [Column]
        public DateTime LastMessageAt { get; set; }

        [Column]
        public bool IsClosed { get; set; }
    }
}