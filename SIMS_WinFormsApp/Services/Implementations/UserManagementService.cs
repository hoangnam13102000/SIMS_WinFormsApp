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

        public UpdateAccountResult UpdateAccount(int userId, string fullName, string email, string phone)
        {
            string normalizedEmail = (email ?? string.Empty).Trim();

            if (_userRepository.IsEmailInUseByOthers(normalizedEmail, userId))
                return UpdateAccountResult.EmailAlreadyInUse;

            bool updated = _userRepository.UpdateContactInfo(
                userId,
                (fullName ?? string.Empty).Trim(),
                normalizedEmail,
                (phone ?? string.Empty).Trim());

            return updated ? UpdateAccountResult.Success : UpdateAccountResult.UserNotFound;
        }

        public SetAccountLockResult SetAccountLocked(int userId, bool isLocked)
        {
            if (_userRepository.FindById(userId) == null)
                return SetAccountLockResult.UserNotFound;

            _userRepository.SetLocked(userId, isLocked);
            return SetAccountLockResult.Success;
        }
    }
}