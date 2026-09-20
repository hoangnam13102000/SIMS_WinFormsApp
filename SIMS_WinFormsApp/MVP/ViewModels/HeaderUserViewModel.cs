using System;

namespace SIMS_WinFormsApp.MVP.ViewModels
{
    public sealed class HeaderUserViewModel
    {
        public string DisplayName { get; }
        public string Email { get; }
        public string Role { get; }
        public string AvatarInitial { get; }
        public string AvatarPath { get; }

        public bool HasRole => !string.IsNullOrEmpty(Role);

        public HeaderUserViewModel(
            string displayName,
            string email,
            string role = null,
            string avatarInitial = null,
            string avatarPath = null)
        {
            DisplayName = string.IsNullOrWhiteSpace(displayName) ? "User" : displayName.Trim();
            Email = email ?? string.Empty;
            Role = role ?? string.Empty;
            AvatarInitial = string.IsNullOrWhiteSpace(avatarInitial)
                ? DisplayName.Substring(0, 1).ToUpperInvariant()
                : avatarInitial.Trim();
            AvatarPath = avatarPath;
        }
    }
}