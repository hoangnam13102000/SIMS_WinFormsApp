using SIMS_WinFormsApp.DAL.Linq;
using SIMS_WinFormsApp.DAL.Linq.Entities;
using SIMS_WinFormsApp.DAL.Linq.Entities.Indetity;
using SIMS_WinFormsApp.Models;
using SIMS_WinFormsApp.Services.Security;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SIMS_WinFormsApp.DAL
{
    public class UserRepository
    {
        // Where + Join + Select (single row) -----------------------------------
        public User FindByUsername(string username)
        {
            using (var db = new SimsDataContext())
            {
                var row = (
                    from u in db.Users
                    join r in db.Roles on u.RoleID equals r.RoleID
                    where u.Username == username && !u.IsDeleted
                    select new { u, r }
                ).FirstOrDefault();

                return row == null ? null : Map(row.u, row.r);
            }
        }

        public User FindById(int userId)
        {
            using (var db = new SimsDataContext())
            {
                var row = (
                    from u in db.Users
                    join r in db.Roles on u.RoleID equals r.RoleID
                    where u.UserID == userId
                    select new { u, r }
                ).FirstOrDefault();

                return row == null ? null : Map(row.u, row.r);
            }
        }

        // Tra cứu tài khoản cho luồng quên mật khẩu: khớp CẢ username lẫn email.
        // Không lấy theo username không thôi để tránh lộ thông tin qua timing/kết quả.
        public User FindForPasswordReset(string username, string email)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email))
                return null;

            using (var db = new SimsDataContext())
            {
                var row = (
                    from u in db.Users
                    join r in db.Roles on u.RoleID equals r.RoleID
                    where u.Username == username.Trim() && u.Email == email.Trim() && !u.IsDeleted
                    select new { u, r }
                ).FirstOrDefault();

                return row == null ? null : Map(row.u, row.r);
            }
        }

        public enum PasswordResetUpdateResult
        {
            Success,
            UpdateFailed,
            SameAsOldPassword,
            AccountUnavailable
        }

        // Đặt lại mật khẩu từ luồng khôi phục (sau khi OTP đã verify): nhận mật khẩu
        // gốc, tự hash. Kiểm tra không được trùng mật khẩu hiện tại + tài khoản phải
        // đang ACTIVE và không bị khóa.
        public PasswordResetUpdateResult ResetPasswordFromRecovery(int userId, string newRawPassword)
        {
            if (string.IsNullOrWhiteSpace(newRawPassword))
                return PasswordResetUpdateResult.UpdateFailed;

            using (var db = new SimsDataContext())
            {
                var user = db.Users.FirstOrDefault(u => u.UserID == userId);
                if (user == null) return PasswordResetUpdateResult.AccountUnavailable;

                if (!string.Equals(user.Status, "ACTIVE", StringComparison.OrdinalIgnoreCase) || user.IsLocked)
                    return PasswordResetUpdateResult.AccountUnavailable;

                if (PasswordHasher.Verify(newRawPassword, user.PasswordHash))
                    return PasswordResetUpdateResult.SameAsOldPassword;

                user.PasswordHash = PasswordHasher.Hash(newRawPassword);
                db.SubmitChanges();
                return PasswordResetUpdateResult.Success;
            }
        }

        // Update via tracked entity + SubmitChanges instead of hand-written UPDATE ---
        public void RegisterFailedLogin(int userId, int lockThreshold = 5)
        {
            using (var db = new SimsDataContext())
            {
                var user = db.Users.FirstOrDefault(u => u.UserID == userId);
                if (user == null) return;

                user.FailedLoginCount += 1;
                if (user.FailedLoginCount >= lockThreshold)
                    user.IsLocked = true;

                db.SubmitChanges();
            }
        }

        public void ResetFailedLogin(int userId)
        {
            using (var db = new SimsDataContext())
            {
                var user = db.Users.FirstOrDefault(u => u.UserID == userId);
                if (user == null) return;

                user.FailedLoginCount = 0;
                db.SubmitChanges();
            }
        }

        // Đổi mật khẩu (dùng sau khi đã xác thực mật khẩu hiện tại ở Service) ---
        public void UpdatePassword(int userId, string newPasswordHash)
        {
            using (var db = new SimsDataContext())
            {
                var user = db.Users.FirstOrDefault(u => u.UserID == userId);
                if (user == null) return;

                user.PasswordHash = newPasswordHash;
                db.SubmitChanges();
            }
        }

        public void SetLocked(int userId, bool isLocked)
        {
            using (var db = new SimsDataContext())
            {
                var user = db.Users.FirstOrDefault(u => u.UserID == userId);
                if (user == null) return;

                user.IsLocked = isLocked;
                db.SubmitChanges();
            }
        }

        // OrderBy + Skip + Take (paging) for frmUserManagement's grid ---------------
        public IReadOnlyList<User> GetPage(int pageIndex, int pageSize, string searchTerm = null)
        {
            using (var db = new SimsDataContext())
            {
                var query =
                    from u in db.Users
                    join r in db.Roles on u.RoleID equals r.RoleID
                    where !u.IsDeleted
                    select new { u, r };

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    query = query.Where(x =>
                        x.u.Username.Contains(searchTerm) ||
                        x.u.FullName.Contains(searchTerm));
                }

                var page = query
                    .OrderBy(x => x.u.FullName)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize)
                    .ToList();

                return page.Select(x => Map(x.u, x.r)).ToList();
            }
        }

        // GroupBy + Count for a "users per role" widget on the dashboard -------------
        public IReadOnlyDictionary<string, int> CountUsersByRole()
        {
            using (var db = new SimsDataContext())
            {
                return (
                    from u in db.Users
                    join r in db.Roles on u.RoleID equals r.RoleID
                    where !u.IsDeleted
                    group u by r.RoleName into g
                    select new { RoleName = g.Key, Total = g.Count() }
                ).ToDictionary(x => x.RoleName, x => x.Total);
            }
        }

        // Danh bạ nhân viên (trừ CUSTOMER) để hiển thị trong Chat nội bộ, kể cả khi họ
        // đang offline — trước đây UI chỉ hiển thị người đang kết nối WebSocket nên nếu
        // chỉ có 1 máy client đang chạy thì danh sách luôn trống dù DB có nhiều nhân viên.
        public IReadOnlyList<User> GetActiveStaffExcept(int excludeUserId)
        {
            using (var db = new SimsDataContext())
            {
                var rows = (
                    from u in db.Users
                    join r in db.Roles on u.RoleID equals r.RoleID
                    where !u.IsDeleted
                          && u.UserID != excludeUserId
                          && r.RoleCode != "CUSTOMER"
                          && u.Status == "ACTIVE"
                    select new { u, r }
                ).ToList();

                return rows.Select(x => Map(x.u, x.r)).ToList();
            }
        }

        private static User Map(UserEntity u, RoleEntity r)
        {
            return new User
            {
                UserId = u.UserID,
                Username = u.Username,
                PasswordHash = u.PasswordHash,
                FullName = u.FullName,
                Email = u.Email,
                Phone = u.Phone,
                AvatarUrl = u.AvatarUrl,
                RoleId = u.RoleID,
                RoleCode = r.RoleCode,
                RoleName = r.RoleName,
                IsLocked = u.IsLocked,
                FailedLoginCount = u.FailedLoginCount,
                Status = u.Status,
                IsDeleted = u.IsDeleted,
                CreatedAt = u.CreatedAt
            };
        }
    }
}