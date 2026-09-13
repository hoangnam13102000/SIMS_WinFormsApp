using System;
using SIMS_WinFormsApp.Models.Enums;

namespace SIMS_WinFormsApp.Models.DTOs
{
    public sealed class UserDetailDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string AvatarUrl { get; set; }

        public string RoleCode { get; set; }
        public string RoleName { get; set; }

        public UserStatus Status { get; set; }
        public bool IsLocked { get; set; }
        public int FailedLoginCount { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}