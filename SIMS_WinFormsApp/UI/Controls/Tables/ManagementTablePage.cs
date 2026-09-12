using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.UI.Controls.Filter;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{
    public static class ManagementTablePage
    {
        public static Control Accounts(IUserManagementService service)
        {
            return Create("Quản lý tài khoản", "Quản lý tài khoản người dùng và phân quyền trong hệ thống",
                IconChar.UsersCog, null, service);
        }

        public static Control Employees(IUserManagementService service)
        {
            return Create("Quản lý nhân viên", "Danh sách nhân viên và thông tin làm việc",
                IconChar.User, "Nhân viên", service);
        }

        public static Control Customers(IUserManagementService service)
        {
            return Create("Quản lý khách hàng", "Danh sách khách hàng và lịch sử giao dịch",
                IconChar.AddressBook, "Khách hàng", service);
        }

        /// <summary>Danh sách lựa chọn cho ComboBox lọc trạng thái tài khoản. Value khớp đúng
        /// với giá trị cột Status lưu trong DB (xem UserRepository/frmUserManagement) — đổi ở
        /// đây là đủ, không phải sửa gì trong BaseTable.</summary>
        private static IList<FilterOption> AccountStatusOptions() => new[]
        {
            new FilterOption("Tất cả trạng thái", null),
            new FilterOption("Đang hoạt động", "ACTIVE"),
            new FilterOption("Vô hiệu hóa", "INACTIVE")
        };

        private static Control Create(
            string title,
            string subtitle,
            IconChar icon,
            string roleFilter,
            IUserManagementService service)
        {
            if (service == null) throw new ArgumentNullException(nameof(service));

            return new BaseTable(title, subtitle, icon,
                new[] { "Tên đăng nhập", "Họ và tên", "Email", "Vai trò", "Trạng thái", "Khóa", "Thao tác" },
                (pageIndex, pageSize, search, statusFilter) =>
                    LoadUsers(service, pageIndex, pageSize, search, roleFilter, statusFilter),
                AccountStatusOptions());
        }

        private static TablePageResult LoadUsers(IUserManagementService service, int pageIndex, int pageSize, string search,
            string roleFilter, string statusFilter)
        {
            var page = service.GetPage(pageIndex, pageSize, search, roleFilter, statusFilter);
            return new TablePageResult
            {
                TotalCount = page.TotalCount,
                Rows = page.Rows.Select(x => new object[]
                {
                    x.Username,
                    x.FullName,
                    x.Email,
                    x.RoleName,
                    string.Equals(x.Status, "ACTIVE", StringComparison.OrdinalIgnoreCase)
                        ? "Đang hoạt động" : "Vô hiệu hóa",
                    x.IsLocked ? "Đang khóa" : "Bình thường",
                    "Xem  Sửa  Khóa"
                }).ToList()
            };
        }
    }
}