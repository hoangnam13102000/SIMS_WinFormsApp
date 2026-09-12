using System;
using System.Data.Linq.Mapping;

namespace SIMS_WinFormsApp.DAL.Linq.Entities.Chat
{
    [Table(Name = "ChatMessages")]
    public class ChatMessageEntity
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int MessageID { get; set; }

        [Column]
        public int ConversationID { get; set; }

        [Column]
        public int SenderUserID { get; set; }

        [Column]
        public string SenderName { get; set; }

        [Column]
        public bool FromStaff { get; set; }

        [Column]
        public string BodyText { get; set; }

        [Column]
        public string ImagePath { get; set; }

        [Column]
        public string ImageMime { get; set; }

        [Column]
        public string FilePath { get; set; }

        [Column]
        public string FileName { get; set; }

        [Column]
        public DateTime CreatedAt { get; set; }

        [Column]
        public bool IsReadByPeer { get; set; }
    }
}