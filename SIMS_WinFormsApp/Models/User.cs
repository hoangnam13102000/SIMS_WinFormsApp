using System;
using SIMS_WinFormsApp.Models.Enums;
using SIMS_WinFormsApp.Services.Security;

namespace SIMS_WinFormsApp.Models
{
    public class User
    {
        public User(string username, string fullName, string email)
        {
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException("Username is required.", nameof(username));
            if (string.IsNullOrWhiteSpace(fullName)) throw new ArgumentException("Full name is required.", nameof(fullName));
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email is required.", nameof(email));
            Username = username;
            FullName = fullName;
            Email = email;
            Status = UserStatus.Active;
        }

        private User() { }

        public int UserId { get; internal set; }
        public string Username { get; private set; }
        public string PasswordHash { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string AvatarUrl { get; private set; }

        public int RoleId { get; internal set; }
        public string RoleCode { get; internal set; }
        public string RoleName { get; internal set; }

        public bool IsLocked { get; private set; }
        public int FailedLoginCount { get; private set; }
        public UserStatus Status { get; private set; }
        public bool IsDeleted { get; internal set; }
        public DateTime CreatedAt { get; internal set; }

        public bool IsActive => Status == UserStatus.Active && !IsDeleted && !IsLocked;
        public bool IsLockedOut => IsLocked;

        public void Lock() => IsLocked = true;
        public void Unlock() => IsLocked = false;
        public void RegisterFailedLogin(int threshold)
        {
            FailedLoginCount++;
            if (FailedLoginCount >= threshold) Lock();
        }
        public void ResetFailedLogin() => FailedLoginCount = 0;
        public void Disable() => Status = UserStatus.Disabled;
        public void Enable() => Status = UserStatus.Active;
        public bool ChangePassword(string oldHash, string newHash, IPasswordHasher hasher)
        {
            if (!hasher.Verify(oldHash, PasswordHash)) return false;
            PasswordHash = newHash;
            return true;
        }

        internal static User FromPersistence(
            int userId, string username, string passwordHash, string fullName,
            string email, string phone, string avatarUrl, int roleId,
            string roleCode, string roleName, bool isLocked, int failedLoginCount,
            string status, bool isDeleted, DateTime createdAt)
        {
            var user = new User(username, fullName, email)
            {
                UserId = userId,
                PasswordHash = passwordHash,
                Phone = phone,
                AvatarUrl = avatarUrl,
                RoleId = roleId,
                RoleCode = roleCode,
                RoleName = roleName,
                IsLocked = isLocked,
                FailedLoginCount = failedLoginCount,
                IsDeleted = isDeleted,
                CreatedAt = createdAt
            };
            if (string.Equals(status, "DISABLED", StringComparison.OrdinalIgnoreCase))
                user.Disable();
            return user;
        }
    }
}