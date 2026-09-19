using System;
using System.Data.Linq.Mapping;

namespace SIMS_WinFormsApp.DAL.Linq.Entities.Identity
{
    /// <summary>Ánh xạ bảng AuditLogs (đã có sẵn trong sql/SIMS.sql, chưa có entity C# trước đây):
    /// LogID, UserID, Action, TableName, RecordID, OldValue, NewValue, Detail, IPAddress, CreatedAt.</summary>
    [Table(Name = "AuditLogs")]
    public class AuditLogEntity
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public long LogID { get; set; }

        [Column]
        public int? UserID { get; set; }

        [Column]
        public string Action { get; set; }

        [Column]
        public string TableName { get; set; }

        [Column]
        public int? RecordID { get; set; }

        [Column]
        public string OldValue { get; set; }

        [Column]
        public string NewValue { get; set; }

        [Column]
        public string Detail { get; set; }

        [Column]
        public string IPAddress { get; set; }

        [Column]
        public DateTime CreatedAt { get; set; }
    }
}