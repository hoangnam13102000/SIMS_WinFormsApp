using System.Data.Linq.Mapping;

namespace SIMS_WinFormsApp.DAL.Linq.Entities.Indetity
{
    [Table(Name = "RolePermissions")]
    public class RolePermissionEntity
    {
        [Column(IsPrimaryKey = true)]
        public int RoleID { get; set; }

        [Column(IsPrimaryKey = true)]
        public int PermissionID { get; set; }
    }
}