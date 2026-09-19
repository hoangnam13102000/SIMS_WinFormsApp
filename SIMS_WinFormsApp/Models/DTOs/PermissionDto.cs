namespace SIMS_WinFormsApp.Models.DTOs
{
    public sealed class PermissionDto
    {
        public int PermissionId { get; set; }
        public string PermissionCode { get; set; }
        public string Description { get; set; }

        public override string ToString() => PermissionCode;
    }
}