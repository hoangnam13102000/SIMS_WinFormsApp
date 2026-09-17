namespace SIMS_WinFormsApp.Models.DTOs.Permission
{
    public abstract class PermissionGroupEntryViewModel
    {
    }

    public sealed class PermissionToggleEntryViewModel : PermissionGroupEntryViewModel
    {
        public PermissionToggleViewModel Toggle { get; }
        public PermissionToggleEntryViewModel(PermissionToggleViewModel toggle) { Toggle = toggle; }
    }

    public sealed class PermissionResourceEntryViewModel : PermissionGroupEntryViewModel
    {
        public PermissionResourceViewModel Resource { get; }
        public PermissionResourceEntryViewModel(PermissionResourceViewModel resource) { Resource = resource; }
    }
}