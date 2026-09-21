using System.Drawing;

namespace SIMS_WinFormsApp.MVP.ViewModels
{
    public sealed class UserAccountDetailViewModel
    {
        public string AvatarInitial { get; }
        public Color AvatarColor { get; }
        public string FullName { get; }
        public string HandleText { get; }

        public string AccountCode { get; }
        public string RoleName { get; }
        public string RoleCode { get; }
        public string Email { get; }
        public string Phone { get; }
        public string CreatedAtText { get; }

        public AccountStatusDisplay AccountStatus { get; }
        public AccountStatusDisplay LockStatus { get; }
        public string FailedLoginCountText { get; }

        // MỚI: chỉ có ý nghĩa với tài khoản nhân viên (null/"—" với khách hàng - View quyết định
        // có hiện 2 ô này hay không dựa trên RoleCode, không dựa trên 2 field này).
        public string GenderText { get; }
        public string SalaryText { get; }

        public UserAccountDetailViewModel(
            string avatarInitial,
            Color avatarColor,
            string fullName,
            string handleText,
            string accountCode,
            string roleName,
            string roleCode,
            string email,
            string phone,
            string createdAtText,
            AccountStatusDisplay accountStatus,
            AccountStatusDisplay lockStatus,
            string failedLoginCountText,
            string genderText,
            string salaryText)
        {
            AvatarInitial = avatarInitial;
            AvatarColor = avatarColor;
            FullName = fullName;
            HandleText = handleText;
            AccountCode = accountCode;
            RoleName = roleName;
            RoleCode = roleCode;
            Email = email;
            Phone = phone;
            CreatedAtText = createdAtText;
            AccountStatus = accountStatus;
            LockStatus = lockStatus;
            FailedLoginCountText = failedLoginCountText;
            GenderText = genderText;
            SalaryText = salaryText;
        }
    }
}