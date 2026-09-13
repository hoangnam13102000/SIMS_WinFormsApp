using System;
using System.Drawing;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.Models.Enums;
using SIMS_WinFormsApp.MVP.ViewModels;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Services.Implementations
{
    public sealed class UserAccountDetailViewModelBuilder : IUserAccountDetailViewModelBuilder
    {
        private const string DateFormat = "dd/MM/yyyy";

        public UserAccountDetailViewModel Build(UserDetailDto user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            string fullName = string.IsNullOrWhiteSpace(user.FullName) ? user.Username ?? "N/A" : user.FullName;

            return new UserAccountDetailViewModel(
                avatarInitial: BuildInitial(fullName),
                avatarColor: AppColors.Accent,
                fullName: fullName,
                handleText: string.IsNullOrWhiteSpace(user.Username) ? string.Empty : "@" + user.Username,
                accountCode: string.IsNullOrWhiteSpace(user.Username) ? "—" : user.Username,
                roleName: string.IsNullOrWhiteSpace(user.RoleName) ? "Chưa phân quyền" : user.RoleName,
                roleCode: string.IsNullOrWhiteSpace(user.RoleCode) ? "—" : user.RoleCode,
                email: string.IsNullOrWhiteSpace(user.Email) ? "—" : user.Email,
                phone: string.IsNullOrWhiteSpace(user.Phone) ? "Chưa cập nhật" : user.Phone,
                createdAtText: user.CreatedAt.HasValue ? user.CreatedAt.Value.ToString(DateFormat) : "—",
                accountStatus: DescribeAccountStatus(user.Status),
                lockStatus: DescribeLockStatus(user.IsLocked),
                failedLoginCountText: user.FailedLoginCount.ToString());
        }

        private static string BuildInitial(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName)) return "?";

            var parts = fullName.Trim().Split(' ');
            string lastWord = parts[parts.Length - 1];
            return lastWord.Substring(0, 1).ToUpperInvariant();
        }

        private static AccountStatusDisplay DescribeAccountStatus(UserStatus status)
        {
            switch (status)
            {
                case UserStatus.Active:
                    return new AccountStatusDisplay("Đang hoạt động", IconChar.CircleCheck, AppColors.Success, AppColors.SuccessBg);
                case UserStatus.Disabled:
                    return new AccountStatusDisplay("Vô hiệu hóa", IconChar.CircleXmark, AppColors.Error, AppColors.ErrorBg);
                default:
                    return new AccountStatusDisplay("Không xác định", IconChar.CircleQuestion, AppColors.TextMuted, AppColors.BgLighter);
            }
        }

        private static AccountStatusDisplay DescribeLockStatus(bool isLocked)
        {
            return isLocked
                ? new AccountStatusDisplay("Đang khóa", IconChar.Lock, AppColors.Error, AppColors.ErrorBg)
                : new AccountStatusDisplay("Bình thường", IconChar.LockOpen, AppColors.Success, AppColors.SuccessBg);
        }
    }
}