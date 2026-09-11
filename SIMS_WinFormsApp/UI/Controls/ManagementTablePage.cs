using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.DAL.Linq;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{
    public static class ManagementTablePage
    {
        public static Control Accounts()
        {
            return Create("Quản lý tài khoản", "Quản lý tài khoản người dùng và phân quyền trong hệ thống",
                IconChar.UsersCog, null);
        }

        public static Control Employees()
        {
            return Create("Quản lý nhân viên", "Danh sách nhân viên và thông tin làm việc",
                IconChar.User, "Nhân viên");
        }

        public static Control Customers()
        {
            return Create("Quản lý khách hàng", "Danh sách khách hàng và lịch sử giao dịch",
                IconChar.AddressBook, "Khách hàng");
        }

        private static Control Create(string title, string subtitle, IconChar icon, string roleFilter)
        {
            return new BaseTable(title, subtitle, icon,
                new[] { "Tên đăng nhập", "Họ và tên", "Email", "Vai trò", "Trạng thái", "Khóa", "Thao tác" },
                (pageIndex, pageSize, search) => LoadUsers(pageIndex, pageSize, search, roleFilter));
        }

        private static TablePageResult LoadUsers(int pageIndex, int pageSize, string search, string roleFilter)
        {
            using (var db = new SimsDataContext())
            {
                var query = from u in db.Users
                            join r in db.Roles on u.RoleID equals r.RoleID
                            where !u.IsDeleted
                            select new { u, r };
                if (!string.IsNullOrWhiteSpace(roleFilter))
                    query = query.Where(x => x.r.RoleName.Contains(roleFilter));
                if (!string.IsNullOrWhiteSpace(search))
                {
                    var term = search.Trim();
                    query = query.Where(x => x.u.Username.Contains(term)
                        || x.u.FullName.Contains(term)
                        || x.u.Email.Contains(term));
                }

                int total = query.Count();
                var rows = query.OrderBy(x => x.u.FullName)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize)
                    .ToList()
                    .Select(x => new object[]
                    {
                        x.u.Username,
                        x.u.FullName,
                        x.u.Email,
                        x.r.RoleName,
                        string.Equals(x.u.Status, "ACTIVE", StringComparison.OrdinalIgnoreCase)
                            ? "Đang hoạt động" : "Vô hiệu hóa",
                        x.u.IsLocked ? "Đang khóa" : "Bình thường",
                        "Xem  Sửa  Khóa"
                    }).ToList();
                return new TablePageResult { TotalCount = total, Rows = rows };
            }
        }
    }
}
