using System.Collections.Generic;
using SIMS_WinFormsApp.Models.DTOs;

namespace SIMS_WinFormsApp.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        IReadOnlyList<RoleOptionDto> GetAssignableRoles();
    }
}