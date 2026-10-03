using SIMS_WinFormsApp.Models.Permission;
using SIMS_WinFormsApp.Services.Session;

namespace SIMS_WinFormsApp.Services.AI.Interfaces
{
    public interface IAiToolPermissionPolicy
    {
        bool IsAllowed(IUserSession session, AppPermission permission);
    }
}
