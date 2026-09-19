using System.Data.Linq.Mapping;

namespace SIMS_WinFormsApp.DAL.Linq.Entities.Identity
{
    [Table(Name = "Permissions")]
    public class PermissionEntity
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int PermissionID { get; set; }

        [Column]
        public string PermissionCode { get; set; }

        [Column]
        public string Description { get; set; }
    }
}