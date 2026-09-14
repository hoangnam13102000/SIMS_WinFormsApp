using System;
using SIMS_WinFormsApp.Models.Enums;

namespace SIMS_WinFormsApp.Models.DTOs
{
    public sealed class CreateEmployeeRequestDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Gender? Gender { get; set; }
        public int RoleId { get; set; }
        public DateTime HireDate { get; set; }
        public decimal? Salary { get; set; }
    }
}