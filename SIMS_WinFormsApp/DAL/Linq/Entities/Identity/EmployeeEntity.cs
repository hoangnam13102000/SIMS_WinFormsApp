using System;
using System.Data.Linq.Mapping;

namespace SIMS_WinFormsApp.DAL.Linq.Entities.Indetity
{
   
    [Table(Name = "Employees")]
    public class EmployeeEntity
    {
        [Column(IsPrimaryKey = true)]
        public int UserID { get; set; }

        [Column]
        public string EmployeeID { get; set; }

        [Column]
        public DateTime? DateOfBirth { get; set; }

        [Column]
        public string Gender { get; set; }

        [Column]
        public decimal? Salary { get; set; }

        [Column]
        public DateTime HireDate { get; set; }

        [Column]
        public DateTime CreatedAt { get; set; }
    }
}