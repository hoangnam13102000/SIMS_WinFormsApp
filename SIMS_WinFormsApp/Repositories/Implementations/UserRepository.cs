using SIMS_WinFormsApp.DAL.Linq;
using SIMS_WinFormsApp.DAL.Linq.Entities;
using SIMS_WinFormsApp.DAL.Linq.Entities.Identity;
using SIMS_WinFormsApp.Models;
using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.Models.Enums;
using SIMS_WinFormsApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SIMS_WinFormsApp.Repositories.Implementations
{
    public class UserRepository : IUserRepository
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

        public void UpdatePasswordHash(int userId, string newHash)
        {
            using (var db = new SimsDataContext())
            {
                var user = db.Users.FirstOrDefault(u => u.UserID == userId);
                if (user == null) return;
                user.PasswordHash = newHash;
                db.SubmitChanges();
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
                if (user.FailedLoginCount >= lockThreshold) user.IsLocked = true;

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

        // Kiểm tra email đã được dùng bởi tài khoản KHÁC hay chưa (loại trừ chính user đang
        // sửa) - dùng trước khi UpdateContactInfo để báo lỗi rõ ràng thay vì để DB ném lỗi
        // ràng buộc unique (nếu có) hoặc âm thầm gán trùng email. Khi tạo mới (chưa có
        // UserID), gọi với excludeUserId = -1 để kiểm tra trùng trên toàn bộ bảng.
        public bool IsEmailInUseByOthers(string email, int excludeUserId)
        {
            string normalized = (email ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(normalized)) return false;

            using (var db = new SimsDataContext())
            {
                return db.Users.Any(u =>
                    u.Email == normalized && u.UserID != excludeUserId && !u.IsDeleted);
            }
        }

        // Cập nhật thông tin liên hệ (họ tên/email/SĐT) cho popup "Cập nhật tài khoản".
        // Trả về false nếu không tìm thấy user (ví dụ đã bị xóa) để Service báo lỗi phù hợp.
        public bool UpdateContactInfo(int userId, string fullName, string email, string phone, string avatarUrl = null)
        {
            using (var db = new SimsDataContext())
            {
                var user = db.Users.FirstOrDefault(u => u.UserID == userId);
                if (user == null) return false;

                user.FullName = fullName;
                user.Email = email;
                user.Phone = phone;
                if (!string.IsNullOrWhiteSpace(avatarUrl)) user.AvatarUrl = avatarUrl;
                db.SubmitChanges();
                return true;
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

        public UserManagementPageDto GetManagementPage(
                int pageIndex,
                int pageSize,
                string searchTerm,
                string roleFilter,
                string statusFilter)
        {
            using (var db = new SimsDataContext())
            {
                var query =
                    from u in db.Users
                    join r in db.Roles on u.RoleID equals r.RoleID
                    where !u.IsDeleted
                    select new { u, r };

                if (string.Equals(roleFilter, UserManagementFilters.NonCustomer, StringComparison.OrdinalIgnoreCase))
                    query = query.Where(x => x.r.RoleCode != RoleCodes.Customer);
                else if (string.Equals(roleFilter, RoleCodes.Customer, StringComparison.OrdinalIgnoreCase))
                    query = query.Where(x => x.r.RoleCode == RoleCodes.Customer);
                else if (!string.IsNullOrWhiteSpace(roleFilter))
                    query = query.Where(x => x.r.RoleName.Contains(roleFilter));
                if (!string.IsNullOrWhiteSpace(statusFilter))
                    query = query.Where(x => x.u.Status == statusFilter);
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    string term = searchTerm.Trim();
                    query = query.Where(x =>
                        x.u.Username.Contains(term) ||
                        x.u.FullName.Contains(term) ||
                        x.u.Email.Contains(term));
                }

                int total = query.Count();
                var rows = query
                    .OrderBy(x => x.u.FullName)
                    .Skip(Math.Max(0, pageIndex) * pageSize)
                    .Take(pageSize)
                    .ToList()
                    .Select(x => new UserManagementRowDto
                    {
                        UserId = x.u.UserID,
                        Username = x.u.Username,
                        FullName = x.u.FullName,
                        Email = x.u.Email,
                        Phone = x.u.Phone,
                        AvatarUrl = x.u.AvatarUrl,
                        RoleCode = x.r.RoleCode,
                        RoleName = x.r.RoleName,
                        Status = x.u.Status,
                        IsLocked = x.u.IsLocked
                    })
                    .ToList();

                return new UserManagementPageDto
                {
                    TotalCount = total,
                    Rows = rows
                };
            }
        }

        // Kiểm tra trùng tên đăng nhập - dùng khi UserManagementService tự sinh username cho
        // nhân viên mới (thử tối đa vài lần, mỗi lần kiểm tra trùng qua hàm này).
        public bool IsUsernameInUse(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) return false;

            using (var db = new SimsDataContext())
            {
                return db.Users.Any(u => u.Username == username);
            }
        }

        // Ghi tài khoản nhân viên mới VÀO 2 BẢNG: Users (tài khoản đăng nhập) rồi Employees
        // (hồ sơ nhân viên mở rộng 1-1, xem EmployeeEntity) - giống cách Customers mở rộng
        // Users cho khách hàng. Username/PasswordHash đã được Service chuẩn bị sẵn (sinh +
        // băm) - Repository chỉ lo phần persist, không tự quyết định nghiệp vụ. Cả 2 lần ghi
        // nằm trong CÙNG 1 transaction ADO.NET: nếu insert Employees lỗi thì insert Users
        // cũng được rollback, tránh sinh ra tài khoản "mồ côi" không có hồ sơ nhân viên.
        public int CreateEmployee(NewEmployeeDto employee)
        {
            if (employee == null) throw new ArgumentNullException(nameof(employee));

            using (var db = new SimsDataContext())
            {
                db.Connection.Open();
                using (var transaction = db.Connection.BeginTransaction())
                {
                    db.Transaction = transaction;
                    try
                    {
                        var userEntity = new UserEntity
                        {
                            Username = employee.Username,
                            PasswordHash = employee.PasswordHash,
                            FullName = employee.FullName,
                            Email = employee.Email,
                            Phone = employee.Phone,
                            AvatarUrl = employee.AvatarUrl,
                            RoleID = employee.RoleId,
                            IsLocked = false,
                            FailedLoginCount = 0,
                            Status = "ACTIVE",
                            IsDeleted = false,
                            CreatedAt = DateTime.Now
                        };
                        db.Users.InsertOnSubmit(userEntity);
                        db.SubmitChanges();
                        // Sau SubmitChanges, userEntity.UserID đã được DB sinh (IDENTITY) - dùng
                        // ngay để tạo mã nhân viên "EMP_0001" theo đúng quy ước ghi trong SIMS.sql.
                        var employeeEntity = new EmployeeEntity
                        {
                            UserID = userEntity.UserID,
                            EmployeeID = "EMP_" + userEntity.UserID.ToString("D4"),
                            DateOfBirth = employee.DateOfBirth,
                            Gender = employee.Gender,
                            Salary = employee.Salary,
                            HireDate = employee.HireDate ?? DateTime.Today,
                            CreatedAt = DateTime.Now
                        };
                        db.Employees.InsertOnSubmit(employeeEntity);
                        db.SubmitChanges();

                        transaction.Commit();
                        return userEntity.UserID;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        private static User Map(UserEntity u, RoleEntity r)
        {
            return User.FromPersistence(
                u.UserID, u.Username, u.PasswordHash, u.FullName, u.Email,
                u.Phone, u.AvatarUrl, u.RoleID, r.RoleCode, r.RoleName,
                u.IsLocked, u.FailedLoginCount, u.Status, u.IsDeleted, u.CreatedAt);
        }
    }
}