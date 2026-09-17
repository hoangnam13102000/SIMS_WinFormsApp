using System;
using System.Collections.Generic;
using System.Linq;
using SIMS_WinFormsApp.DAL.Linq;
using SIMS_WinFormsApp.DAL.Linq.Entities.Indetity;
using SIMS_WinFormsApp.Models.Permission;
using SIMS_WinFormsApp.Repositories.Interfaces;

namespace SIMS_WinFormsApp.Repositories.Implementations
{
    public class RolePermissionRepository : IRolePermissionRepository
    {
        public HashSet<AppPermission> GetPermissionsForRole(int roleId)
        {
            var result = new HashSet<AppPermission>();
            try
            {
                using (var db = new SimsDataContext())
                {
                    var codes =
                        from rp in db.RolePermissions
                        join p in db.Permissions on rp.PermissionID equals p.PermissionID
                        where rp.RoleID == roleId
                        select p.PermissionCode;

                    foreach (var code in codes)
                    {
                        // Bỏ qua mã quyền cũ đã xoá khỏi enum AppPermission (dữ liệu lịch sử trong DB).
                        if (Enum.TryParse(code, out AppPermission permission))
                        {
                            result.Add(permission);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("[RolePermissionRepository] GetPermissionsForRole: " + ex);
            }
            return result;
        }

        public bool SavePermissionsForRole(int roleId, HashSet<AppPermission> permissions)
        {
            var toSave = permissions ?? new HashSet<AppPermission>();
            var codes = toSave.Select(p => p.ToString()).ToList();

            try
            {
                using (var db = new SimsDataContext())
                {
                    db.Connection.Open();
                    using (var transaction = db.Connection.BeginTransaction())
                    {
                        db.Transaction = transaction;
                        try
                        {
                            EnsurePermissionCodesExist(db, codes);

                            var oldRows = db.RolePermissions.Where(rp => rp.RoleID == roleId);
                            db.RolePermissions.DeleteAllOnSubmit(oldRows);
                            db.SubmitChanges();

                            if (codes.Count > 0)
                            {
                                var idByCode = db.Permissions
                                    .Where(p => codes.Contains(p.PermissionCode))
                                    .ToDictionary(p => p.PermissionCode, p => p.PermissionID);

                                foreach (var code in codes)
                                {
                                    if (!idByCode.TryGetValue(code, out var permissionId)) continue;
                                    db.RolePermissions.InsertOnSubmit(new RolePermissionEntity
                                    {
                                        RoleID = roleId,
                                        PermissionID = permissionId
                                    });
                                }
                                db.SubmitChanges();
                            }

                            transaction.Commit();
                            return true;
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("[RolePermissionRepository] SavePermissionsForRole: " + ex);
                return false;
            }
        }

        public void EnsureCatalogSeeded()
        {
            try
            {
                using (var db = new SimsDataContext())
                {
                    var allCodes = Enum.GetValues(typeof(AppPermission))
                        .Cast<AppPermission>()
                        .Select(p => p.ToString());
                    EnsurePermissionCodesExist(db, allCodes);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("[RolePermissionRepository] EnsureCatalogSeeded: " + ex);
            }
        }

        /// <summary>Thêm vào bảng Permissions mọi mã trong <paramref name="codes"/> còn thiếu - bỏ qua mã đã có.</summary>
        private static void EnsurePermissionCodesExist(SimsDataContext db, IEnumerable<string> codes)
        {
            var existing = new HashSet<string>(db.Permissions.Select(p => p.PermissionCode));
            bool inserted = false;
            foreach (var code in codes)
            {
                if (existing.Contains(code)) continue;
                db.Permissions.InsertOnSubmit(new PermissionEntity
                {
                    PermissionCode = code,
                    Description = code
                });
                existing.Add(code);
                inserted = true;
            }
            if (inserted) db.SubmitChanges();
        }
    }
}