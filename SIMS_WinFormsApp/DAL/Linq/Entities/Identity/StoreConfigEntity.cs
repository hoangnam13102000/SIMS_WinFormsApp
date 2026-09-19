using System.Data.Linq.Mapping;

namespace SIMS_WinFormsApp.DAL.Linq.Entities.Identity
{
    [Table(Name = "StoreConfig")]
    public class StoreConfigEntity
    {
        [Column(IsPrimaryKey = true)]
        public string ConfigKey { get; set; }

        [Column]
        public string ConfigValue { get; set; }
    }
}