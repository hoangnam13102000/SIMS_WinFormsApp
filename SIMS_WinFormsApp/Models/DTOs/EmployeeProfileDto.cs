using System;
using SIMS_WinFormsApp.Models.Enums;

namespace SIMS_WinFormsApp.Models.DTOs
{
    public sealed class EmployeeProfileDto
    {
        public DateTime? DateOfBirth { get; set; }
        public Gender? Gender { get; set; }
        public DateTime HireDate { get; set; }
        public decimal? Salary { get; set; }
    }
}