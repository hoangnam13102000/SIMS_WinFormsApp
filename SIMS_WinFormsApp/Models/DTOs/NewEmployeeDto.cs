using System;

namespace SIMS_WinFormsApp.Models.DTOs
{
    public sealed class NewEmployeeDto
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int RoleId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        /// <summary>"MALE" | "FEMALE" | "OTHER" | null.</summary>
        public string Gender { get; set; }
        public DateTime? HireDate { get; set; }
        public decimal? Salary { get; set; }
    }
}