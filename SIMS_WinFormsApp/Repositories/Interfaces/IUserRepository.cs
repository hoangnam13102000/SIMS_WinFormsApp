using System.Collections.Generic;
using SIMS_WinFormsApp.Models;
using SIMS_WinFormsApp.Models.DTOs;

namespace SIMS_WinFormsApp.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User FindByUsername(string username);
        User FindById(int userId);
        User FindForPasswordReset(string username, string email);

        void UpdatePasswordHash(int userId, string newHash);
        void RegisterFailedLogin(int userId, int lockThreshold = 5);
        void ResetFailedLogin(int userId);
        void UpdatePassword(int userId, string newPasswordHash);
        bool IsEmailInUseByOthers(string email, int excludeUserId);

        /// <summary>Cập nhật thông tin liên hệ; nếu truyền <paramref name="employeeProfile"/> thì
        /// cập nhật luôn hồ sơ nhân viên trong CÙNG 1 lần SubmitChanges (cùng thành công hoặc
        /// cùng thất bại).</summary>
        bool UpdateContactInfo(
            int userId, string fullName, string email, string phone,
            string avatarUrl = null, EmployeeProfileDto employeeProfile = null);

        /// <summary>Hồ sơ nhân viên (bảng Employees); null nếu tài khoản không có hồ sơ này.</summary>
        EmployeeProfileDto GetEmployeeProfile(int userId);

        void SetLocked(int userId, bool isLocked);
        IReadOnlyList<User> GetPage(int pageIndex, int pageSize, string searchTerm = null);
        IReadOnlyDictionary<string, int> CountUsersByRole();
        IReadOnlyList<User> GetActiveStaffExcept(int excludeUserId);
        UserManagementPageDto GetManagementPage(
            int pageIndex,
            int pageSize,
            string searchTerm,
            string roleFilter,
            string statusFilter);

        /// <summary>Tên đăng nhập đã tồn tại chưa - dùng khi tự sinh username cho nhân viên mới
        /// để đảm bảo duy nhất trước khi ghi DB.</summary>
        bool IsUsernameInUse(string username);

        /// <summary>Tạo tài khoản nhân viên mới (username/mật khẩu đã được Service chuẩn bị
        /// sẵn). Trả về UserID vừa tạo.</summary>
        int CreateEmployee(NewEmployeeDto employee);
    }
}