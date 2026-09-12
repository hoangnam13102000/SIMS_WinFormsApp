using System.Collections.Generic;

namespace SIMS_WinFormsApp.Models.DTOs
{
    public sealed class UserManagementRowDto
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public string Status { get; set; }
        public bool IsLocked { get; set; }
    }

    public sealed class UserManagementPageDto
    {
        public int TotalCount { get; set; }
        public IReadOnlyList<UserManagementRowDto> Rows { get; set; }
    }
}
