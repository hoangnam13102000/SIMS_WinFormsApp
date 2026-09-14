using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Forms.SystemMgmt;
using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.Models.Mapping;
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
            // Chỉ riêng màn "Quản lý nhân viên" mới có nút "+ Thêm nhân viên" ở góc phải header
            // (Accounts/Customers không có, vì tính năng thêm mới hiện chỉ áp dụng cho nhân viên).
            return Create("Quản lý nhân viên", "Danh sách nhân viên và thông tin làm việc",
                IconChar.User, "Nhân viên", service, "+ Thêm nhân viên");
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
            IUserManagementService service,
            string addButtonText = null)
        {
            if (service == null) throw new ArgumentNullException(nameof(service));

            // Giữ lại danh sách dòng gốc (UserManagementRowDto) của trang đang hiển thị, vì cột
            // "Thao tác" trong BaseTable chỉ báo về RowIndex - cần map ngược RowIndex -> dữ liệu
            // gốc thì mới biết đang "Xem" tài khoản nào.
            IReadOnlyList<UserManagementRowDto> currentRows = Array.Empty<UserManagementRowDto>();

            var table = new BaseTable(title, subtitle, icon,
                new[] { "Tên đăng nhập", "Họ và tên", "Email", "Vai trò", "Trạng thái", "Khóa", "Thao tác" },
                (pageIndex, pageSize, search, statusFilter) =>
                    LoadUsers(service, pageIndex, pageSize, search, roleFilter, statusFilter, out currentRows),
                AccountStatusOptions(),
                addButtonText: addButtonText);

            table.ActionButtonClicked += (sender, e) => HandleActionButtonClicked(table, e, currentRows, service);

            if (!string.IsNullOrEmpty(addButtonText))
            {
                table.AddButtonClicked += (sender, e) => HandleAddButtonClicked(table, service);
            }

            return table;
        }

        /// <summary>Bấm icon mắt/bút/khóa ở cột "Thao tác". "Xem" mở popup chi tiết
        /// (frmUserAccountDetail), "Sửa" mở popup cập nhật (frmEditUserAccount), "Khóa" bật/tắt
        /// trạng thái khóa tài khoản trong DB và reload lại bảng sau khi thành công.</summary>
        private static void HandleActionButtonClicked(
            BaseTable table,
            TableActionEventArgs e,
            IReadOnlyList<UserManagementRowDto> rows,
            IUserManagementService service)
        {
            if (rows == null || e.RowIndex < 0 || e.RowIndex >= rows.Count) return;

            var detailDto = UserDetailMapper.FromRow(rows[e.RowIndex]);

            switch (e.Action)
            {
                case TableActionType.View:
                    frmUserAccountDetail.Show(table.FindForm(), detailDto);
                    break;

                case TableActionType.Edit:
                    var result = frmEditUserAccount.Show(table.FindForm(), detailDto, service);
                    if (result == DialogResult.OK) table.Reload();
                    break;

                case TableActionType.Lock:
                    bool lockAccount = !rows[e.RowIndex].IsLocked;
                    string actionText = lockAccount ? "khóa" : "mở khóa";
                    bool confirmed = DialogHelper.Confirm(
                        table.FindForm(),
                        "Xác nhận thao tác",
                        string.Format("Bạn có chắc muốn {0} tài khoản '{1}' không?", actionText, detailDto.Username));
                    if (!confirmed) break;

                    var lockResult = service.SetAccountLocked(detailDto.UserId, lockAccount);
                    if (lockResult == SetAccountLockResult.Success)
                    {
                        table.Reload();
                    }
                    else
                    {
                        DialogHelper.ShowWarning(table.FindForm(), "Không tìm thấy tài khoản.");
                    }
                    break;
            }
        }

        /// <summary>Bấm nút "+ Thêm nhân viên" ở header: mở popup frmAddEmployee, reload lại
        /// bảng nếu tạo thành công (DialogResult.OK) để dòng mới hiện ra ngay.</summary>
        private static void HandleAddButtonClicked(BaseTable table, IUserManagementService service)
        {
            var result = frmAddEmployee.Show(table.FindForm(), service);
            if (result == DialogResult.OK) table.Reload();
        }

        private static TablePageResult LoadUsers(IUserManagementService service, int pageIndex, int pageSize, string search,
            string roleFilter, string statusFilter, out IReadOnlyList<UserManagementRowDto> rows)
        {
            var page = service.GetPage(pageIndex, pageSize, search, roleFilter, statusFilter);
            rows = page.Rows;

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