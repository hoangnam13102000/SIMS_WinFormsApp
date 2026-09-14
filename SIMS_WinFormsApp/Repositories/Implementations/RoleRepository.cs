using System.Collections.Generic;
using System.Linq;
using SIMS_WinFormsApp.DAL.Linq;
using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.Models.Enums;
using SIMS_WinFormsApp.Repositories.Interfaces;

namespace SIMS_WinFormsApp.Repositories.Implementations
{
    public class RoleRepository : IRoleRepository
    {
        public IReadOnlyList<RoleOptionDto> GetAssignableRoles()
        {
            using (var db = new SimsDataContext())
            {
                return db.Roles
                    .Where(r => r.RoleCode != RoleCodes.Customer)
                    .OrderBy(r => r.RoleName)
                    .ToList()
                    .Select(r => new RoleOptionDto
                    {
                        RoleId = r.RoleID,
                        RoleCode = r.RoleCode,
                        RoleName = r.RoleName
                    })
                    .ToList();
            }
        }
    }
}