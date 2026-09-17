using SIMS_WinFormsApp.Models.Permission;

namespace SIMS_WinFormsApp.Models.DTOs.Permission
{
    public sealed class PermissionToggleViewModel
    {
        public AppPermission Permission { get; }
        public string Label { get; }
        public string Description { get; }
        public bool IsChecked { get; }

        public PermissionToggleViewModel(AppPermission permission, string label, string description, bool isChecked)
        {
            Permission = permission;
            Label = label;
            Description = description;
            IsChecked = isChecked;
        }
    }
}