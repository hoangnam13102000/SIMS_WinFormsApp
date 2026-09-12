using SIMS_WinFormsApp.Models.DTOs;

namespace SIMS_WinFormsApp.Services.Interfaces
{
    public interface IUserManagementService
    {
        UserManagementPageDto GetPage(
            int pageIndex,
            int pageSize,
            string searchTerm,
            string roleFilter,
            string statusFilter);
    }
}
