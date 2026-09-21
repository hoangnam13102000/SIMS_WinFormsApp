using System.Collections.Generic;
using SIMS_WinFormsApp.Models.DTOs;

namespace SIMS_WinFormsApp.Services.Interfaces
{

    public enum UpdateAccountResult
    {
        Success,
        UserNotFound,
        EmailAlreadyInUse
    }

    public enum SetAccountLockResult
    {
        Success,
        UserNotFound
    }

    public enum CreateEmployeeResult
    {
        Success,
        EmailAlreadyInUse,
        RoleNotFound
    }

    /// <summary>Kết quả tạo tài khoản nhân viên. Kèm Username/TemporaryPassword để popup có thể
    /// hiển thị cho quản trị viên khi gửi email thất bại (giống
    /// EmployeeDAO.EmployeeCreationResult ở bản Java) — không được để mất thông tin đăng nhập
    /// chỉ vì lỗi gửi mail.</summary>
    public sealed class EmployeeCreationOutcome
    {
        public CreateEmployeeResult Result { get; set; }
        public string Username { get; set; }
        public string TemporaryPassword { get; set; }
        public bool EmailSent { get; set; }
        public string EmailError { get; set; }
    }

    public interface IUserManagementService
    {
        UserManagementPageDto GetPage(
            int pageIndex,
            int pageSize,
            string searchTerm,
            string roleFilter,
            string statusFilter);

        /// <summary>Cập nhật thông tin tài khoản. <paramref name="employeeProfile"/> khác null thì
        /// cập nhật luôn hồ sơ nhân viên (Ngày sinh/Giới tính/Ngày vào làm/Lương) cùng lúc.</summary>
        UpdateAccountResult UpdateAccount(
            int userId, string fullName, string email, string phone,
            string avatarFilePath = null, EmployeeProfileDto employeeProfile = null);

        /// <summary>Hồ sơ nhân viên của tài khoản; null nếu tài khoản không phải nhân viên.</summary>
        EmployeeProfileDto GetEmployeeProfile(int userId);

        SetAccountLockResult SetAccountLocked(int userId, bool isLocked);
        IReadOnlyList<RoleOptionDto> GetAssignableRoles();

        EmployeeCreationOutcome CreateEmployee(CreateEmployeeRequestDto request);
    }
}