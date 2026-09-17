namespace SIMS_WinFormsApp.Models.DTOs.Permission
{
    public sealed class RolePermissionRoleRowDto
    {
        public int RoleId { get; set; }
        public string RoleCode { get; set; }
        public string RoleName { get; set; }
        public bool IsAdmin { get; set; }
        public int PermissionCount { get; set; }
        public bool IsSelected { get; set; }
    }
}