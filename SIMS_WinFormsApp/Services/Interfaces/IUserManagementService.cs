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

    public interface IUserManagementService
    {
        UserManagementPageDto GetPage(
            int pageIndex,
            int pageSize,
            string searchTerm,
            string roleFilter,
            string statusFilter);
        UpdateAccountResult UpdateAccount(int userId, string fullName, string email, string phone);
        SetAccountLockResult SetAccountLocked(int userId, bool isLocked);
    }
}