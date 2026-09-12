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
    }
}
