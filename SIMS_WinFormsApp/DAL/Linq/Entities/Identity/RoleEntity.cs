using System.Data.Linq.Mapping;

namespace SIMS_WinFormsApp.DAL.Linq.Entities.Identity
{
    [Table(Name = "Roles")]
    public class RoleEntity
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int RoleID { get; set; }

        [Column]
        public string RoleCode { get; set; }

        [Column]
        public string RoleName { get; set; }

        [Column]
        public string Description { get; set; }
    }
}