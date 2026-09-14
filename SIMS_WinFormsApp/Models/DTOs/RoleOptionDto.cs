namespace SIMS_WinFormsApp.Models.DTOs
{
    public sealed class RoleOptionDto
    {
        public int RoleId { get; set; }
        public string RoleCode { get; set; }
        public string RoleName { get; set; }

        
        public override string ToString() => RoleName;
    }
}