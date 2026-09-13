using System;
using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.Models.Enums;
using SIMS_WinFormsApp.Models;

namespace SIMS_WinFormsApp.Models.Mapping
{

    public static class UserDetailMapper
    {

        public static UserDetailDto FromDomain(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            return new UserDetailDto
            {
                UserId = user.UserId,
                Username = user.Username,
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.Phone,
                AvatarUrl = user.AvatarUrl,
                RoleCode = user.RoleCode,
                RoleName = user.RoleName,
                Status = user.Status,
                IsLocked = user.IsLocked,
                FailedLoginCount = user.FailedLoginCount,
                CreatedAt = user.CreatedAt
            };
        }

        public static UserDetailDto FromRow(UserManagementRowDto row)
        {
            if (row == null) throw new ArgumentNullException(nameof(row));

            return new UserDetailDto
            {
                Username = row.Username,
                FullName = row.FullName,
                Email = row.Email,
                RoleName = row.RoleName,
                Status = string.Equals(row.Status, "ACTIVE", StringComparison.OrdinalIgnoreCase)
                    ? UserStatus.Active
                    : UserStatus.Disabled,
                IsLocked = row.IsLocked,
                CreatedAt = null
            };
        }
    }
}