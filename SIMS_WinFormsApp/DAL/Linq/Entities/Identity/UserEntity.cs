using System;
using System.Data.Linq.Mapping;

namespace SIMS_WinFormsApp.DAL.Linq.Entities.Indetity
{
    
    [Table(Name = "Users")]
    public class UserEntity
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int UserID { get; set; }

        [Column]
        public string Username { get; set; }

        [Column]
        public string PasswordHash { get; set; }

        [Column]
        public string FullName { get; set; }

        [Column]
        public string Email { get; set; }

        [Column]
        public string Phone { get; set; }

        [Column]
        public string AvatarUrl { get; set; }

        [Column]
        public int RoleID { get; set; }

        [Column]
        public bool IsLocked { get; set; }

        [Column]
        public int FailedLoginCount { get; set; }

        [Column]
        public string Status { get; set; }

        [Column]
        public bool IsDeleted { get; set; }

        [Column]
        public DateTime? DeletedAt { get; set; }

        [Column]
        public DateTime CreatedAt { get; set; }
    }
}