using System;
using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.Repositories.Interfaces;
using SIMS_WinFormsApp.Services.Interfaces;

namespace SIMS_WinFormsApp.Services.Implementations
{
    public sealed class UserManagementService : IUserManagementService
    {
        private readonly IUserRepository _userRepository;

        public UserManagementService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public UserManagementPageDto GetPage(
            int pageIndex,
            int pageSize,
            string searchTerm,
            string roleFilter,
            string statusFilter)
        {
            return _userRepository.GetManagementPage(
                pageIndex, pageSize, searchTerm, roleFilter, statusFilter);
        }
    }
}
