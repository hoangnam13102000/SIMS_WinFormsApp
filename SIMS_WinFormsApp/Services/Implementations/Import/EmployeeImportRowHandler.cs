using System;
using System.Globalization;
using System.Text.RegularExpressions;
using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.Models.Enums;
using SIMS_WinFormsApp.Services.Interfaces;

namespace SIMS_WinFormsApp.Services.Implementations.Import
{
    public sealed class EmployeeImportRowHandler
    {
        public static readonly string[] ExpectedColumns =
        {
            "Họ và tên", "Email", "Số điện thoại", "Ngày sinh",
            "Giới tính", "Vai trò", "Ngày vào làm", "Lương"
        };

        private readonly IUserManagementService _service;

        public EmployeeImportRowHandler(IUserManagementService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public ImportRowResult Handle(string[] cells, int rowNumber)
        {
            string fullName = Cell(cells, 0);
            string email = Cell(cells, 1);
            string phone = Cell(cells, 2);
            string dobText = Cell(cells, 3);
            string genderText = Cell(cells, 4);
            string roleText = Cell(cells, 5);
            string hireDateText = Cell(cells, 6);
            string salaryText = Cell(cells, 7);

            if (string.IsNullOrWhiteSpace(fullName))
                return ImportRowResult.Failure("Dòng " + rowNumber + ": thiếu họ và tên.");
            if (string.IsNullOrWhiteSpace(email))
                return ImportRowResult.Failure("Dòng " + rowNumber + ": thiếu email.");

            int? roleId = ResolveRoleId(roleText);
            if (roleId == null)
                return ImportRowResult.Failure("Dòng " + rowNumber + ": vai trò \"" + roleText + "\" không hợp lệ.");

            decimal? salary = ParseSalary(salaryText);
            if (!string.IsNullOrWhiteSpace(salaryText) && salary == null)
                return ImportRowResult.Failure("Dòng " + rowNumber + ": lương không hợp lệ.");

            var request = new CreateEmployeeRequestDto
            {
                FullName = fullName.Trim(),
                Email = email.Trim(),
                Phone = string.IsNullOrWhiteSpace(phone) ? null : phone.Trim(),
                DateOfBirth = ParseDate(dobText),
                Gender = ParseGender(genderText),
                RoleId = roleId.Value,
                HireDate = ParseDate(hireDateText) ?? DateTime.Today,
                Salary = salary
            };

            try
            {
                var outcome = _service.CreateEmployee(request);
                switch (outcome.Result)
                {
                    case CreateEmployeeResult.Success:
                        return ImportRowResult.Success();
                    case CreateEmployeeResult.EmailAlreadyInUse:
                        return ImportRowResult.Failure("Dòng " + rowNumber + ": email \"" + email + "\" đã được sử dụng.");
                    case CreateEmployeeResult.RoleNotFound:
                        return ImportRowResult.Failure("Dòng " + rowNumber + ": vai trò không hợp lệ.");
                    default:
                        return ImportRowResult.Failure("Dòng " + rowNumber + ": lỗi không xác định.");
                }
            }
            catch (Exception ex)
            {
                return ImportRowResult.Failure("Dòng " + rowNumber + ": " + ex.Message);
            }
        }

        private int? ResolveRoleId(string roleText)
        {
            if (string.IsNullOrWhiteSpace(roleText)) return null;
            string normalized = roleText.Trim();
            foreach (var role in _service.GetAssignableRoles())
            {
                if (string.Equals(role.RoleName, normalized, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(role.RoleCode, normalized, StringComparison.OrdinalIgnoreCase))
                    return role.RoleId;
            }
            return null;
        }

        private static string Cell(string[] cells, int index) =>
            index >= 0 && index < cells.Length && cells[index] != null ? cells[index].Trim() : string.Empty;

        private static DateTime? ParseDate(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return null;
            string[] formats = { "dd/MM/yyyy", "d/M/yyyy", "yyyy-MM-dd", "MM/dd/yyyy" };
            if (DateTime.TryParseExact(text.Trim(), formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var exact))
                return exact;
            if (DateTime.TryParse(text.Trim(), CultureInfo.InvariantCulture, DateTimeStyles.None, out var loose))
                return loose;
            return null;
        }

        private static Gender? ParseGender(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return null;
            string normalized = text.Trim().ToLowerInvariant();
            if (normalized == "nam" || normalized == "male") return Gender.Male;
            if (normalized == "nữ" || normalized == "nu" || normalized == "female") return Gender.Female;
            return Gender.Other;
        }

        private static decimal? ParseSalary(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return null;
            string digitsOnly = Regex.Replace(text, "[^0-9]", "");
            return digitsOnly.Length == 0 ? (decimal?)null : decimal.Parse(digitsOnly, CultureInfo.InvariantCulture);
        }
    }
}