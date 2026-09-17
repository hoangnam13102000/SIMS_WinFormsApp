using System.Collections.Generic;

namespace SIMS_WinFormsApp.Models.DTOs.Permission
{
    public sealed class PermissionGroupViewModel
    {
        public string Title { get; }
        public string Hint { get; }
        public IReadOnlyList<PermissionGroupEntryViewModel> Entries { get; }

        public PermissionGroupViewModel(string title, string hint, IReadOnlyList<PermissionGroupEntryViewModel> entries)
        {
            Title = title;
            Hint = hint;
            Entries = entries;
        }
    }
}