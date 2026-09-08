using System;

namespace SIMS_WinFormsApp.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string AvatarUrl { get; set; }

        public int RoleId { get; set; }
        public string RoleCode { get; set; }
        public string RoleName { get; set; }

        public bool IsLocked { get; set; }
        public int FailedLoginCount { get; set; }
        public string Status { get; set; } // ACTIVE | DISABLED
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }

        public bool IsActive => Status == "ACTIVE" && !IsDeleted;
    }
}