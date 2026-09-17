namespace SIMS_WinFormsApp.Models.Permission
{
    public sealed class PermissionCatalogEntry
    {
        public string Group { get; }
        public string Label { get; }
        public string Description { get; }

        public PermissionCatalogEntry(string group, string label, string description)
        {
            Group = group;
            Label = label;
            Description = description ?? string.Empty;
        }
    }
}