using System.Collections.Generic;

namespace SIMS_WinFormsApp.Models.DTOs.Permission
{
    public sealed class PermissionResourceViewModel
    {
        public string Name { get; }
        public string Description { get; }
        public IReadOnlyList<PermissionToggleViewModel> Tiers { get; }

        public PermissionResourceViewModel(string name, string description, IReadOnlyList<PermissionToggleViewModel> tiers)
        {
            Name = name;
            Description = description;
            Tiers = tiers;
        }
    }
}