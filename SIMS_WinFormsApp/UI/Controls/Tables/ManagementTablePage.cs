using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Forms.SystemMgmt;
using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.Models.Mapping;
using SIMS_WinFormsApp.Services.Implementations.Export;
using SIMS_WinFormsApp.Services.Implementations.Import;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.UI.Controls.Filter;
using SIMS_WinFormsApp.UI.Controls.Loading;
using SIMS_WinFormsApp.UI.Controls.Toast;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{
    public static class ManagementTablePage
    {
        public static Control Accounts(IUserManagementService service)
        {
            return Create("Quản lý tài khoản", "Quản lý tài khoản người dùng và phân quyền trong hệ thống",
                IconChar.UsersCog, null, service,
                buildOverflowActions: getTable => BuildAccountsOverflowActions(getTable));
        }

        private static IList<OverflowMenuAction> BuildAccountsOverflowActions(Func<BaseTable> getTable)
        {
            return new List<OverflowMenuAction>
            {
                new OverflowMenuAction("Phân quyền vai trò", IconChar.UserShield, () =>
                    frmRoleManagement.Show(getTable().FindForm()))
            };
        }
        public static Control Employees(IUserManagementService service)
        {
            // Chỉ riêng màn "Quản lý nhân viên" mới có nút "+ Thêm nhân viên" ở góc phải header
            // (Accounts/Customers không có, vì tính năng thêm mới hiện chỉ áp dụng cho nhân viên).
            // MỚI: kèm theo menu "Tùy chọn" (Xuất CSV/Xuất Excel/Nhập dữ liệu) cạnh nút "+ Thêm".
            return Create("Quản lý nhân viên", "Danh sách nhân viên và thông tin làm việc",
                IconChar.User, "Nhân viên", service, "+ Thêm nhân viên",
                getTable => BuildEmployeeOverflowActions(service, "Nhân viên", getTable));
        }

        public static Control Customers(IUserManagementService service)
        {
            return Create("Quản lý khách hàng", "Danh sách khách hàng và lịch sử giao dịch",
                IconChar.AddressBook, "Khách hàng", service);
        }

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
            string addButtonText = null,
            // MỚI: factory nhận 1 accessor tới BaseTable (để Reload() sau khi Nhập dữ liệu xong)
            // và trả về danh sách hành động cho menu "Tùy chọn". null (mặc định) => không có menu,
            // hành vi y hệt trước đây.
            Func<Func<BaseTable>, IList<OverflowMenuAction>> buildOverflowActions = null)
        {
            if (service == null) throw new ArgumentNullException(nameof(service));

            IReadOnlyList<UserManagementRowDto> currentRows = Array.Empty<UserManagementRowDto>();

            BaseTable table = null; // MỚI: khai báo trước - buildOverflowActions cần tham chiếu để Reload() sau khi nhập xong
            var overflowActions = buildOverflowActions?.Invoke(() => table); // MỚI

            // MỚI: LoadingOverlayHost - hiện lớp phủ "Đang tải dữ liệu..." mỗi khi BaseTable gọi
            // lại delegate tải trang bên dưới (đổi trang/tìm kiếm/lọc/Reload()). Khai báo trước vì
            // delegate tham chiếu tới nó, nhưng chỉ được gán SAU khi `table` dựng xong bên dưới -
            // nên lần tải đầu tiên (bên trong constructor của BaseTable) sẽ không có lớp phủ, các
            // lần tải sau (đổi trang/tìm kiếm/lọc/Reload) đều có. Đây là giới hạn chấp nhận được
            // để không phải sửa BaseTable.cs.
            LoadingOverlayHost overlayHost = null;

            table = new BaseTable(title, subtitle, icon,
                new[] { "Tên đăng nhập", "Họ và tên", "Email", "Vai trò", "Trạng thái", "Khóa", "Thao tác" },
                (pageIndex, pageSize, search, statusFilter) =>
                    LoadUsersWithLoadingIndicator(overlayHost, service, pageIndex, pageSize, search, roleFilter, statusFilter, out currentRows),
                AccountStatusOptions(),
                addButtonText: addButtonText,
                overflowActions: overflowActions); // MỚI

            table.ActionButtonClicked += (sender, e) => HandleActionButtonClicked(table, e, currentRows, service);

            if (!string.IsNullOrEmpty(addButtonText))
            {
                table.AddButtonClicked += (sender, e) => HandleAddButtonClicked(table, service);
            }

            // MỚI: bọc bảng trong LoadingOverlayHost - hoàn toàn không đụng vào BaseTable hay
            // LoadUsers, chỉ bao ngoài Control trả về (Open/Closed Principle).
            overlayHost = new LoadingOverlayHost(table);
            return overlayHost;
        }

        /// <summary>MỚI: bọc quanh <see cref="LoadUsers"/> để hiện/ẩn lớp phủ đang tải qua
        /// <see cref="ILoadingIndicator"/> - KHÔNG thay đổi bất kỳ dòng nào trong LoadUsers.
        /// BaseTable gọi delegate tải trang đồng bộ trên UI thread (kiến trúc hiện tại của dự
        /// án), nên cần Application.DoEvents() để ép vẽ lại 1 lần, cho lớp phủ kịp hiển thị
        /// trước khi câu lệnh truy vấn (đồng bộ) phía dưới chạy.</summary>
        private static TablePageResult LoadUsersWithLoadingIndicator(
            ILoadingIndicator loadingIndicator,
            IUserManagementService service, int pageIndex, int pageSize, string search,
            string roleFilter, string statusFilter, out IReadOnlyList<UserManagementRowDto> rows)
        {
            loadingIndicator?.ShowLoading("Đang tải dữ liệu...");
            Application.DoEvents();
            try
            {
                return LoadUsers(service, pageIndex, pageSize, search, roleFilter, statusFilter, out rows);
            }
            finally
            {
                loadingIndicator?.HideLoading();
            }
        }

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
                    if (result == DialogResult.OK)
                    {
                        table.Reload();
                        AppToast.Success(table.FindForm(), "Cập nhật tài khoản thành công.");
                    }
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
                        // Người dùng đã xác nhận (Confirm ở trên) - đây chỉ là báo kết quả thất
                        // bại sau đó, không cần xác nhận thêm nữa -> toast tự biến mất.
                        AppToast.Warning(table.FindForm(), "Không tìm thấy tài khoản.");
                    }
                    break;
            }
        }

        private static void HandleAddButtonClicked(BaseTable table, IUserManagementService service)
        {
            var result = frmAddEmployee.Show(table.FindForm(), service);
            if (result == DialogResult.OK)
            {
                table.Reload();
                AppToast.Success(table.FindForm(), "Đã thêm nhân viên thành công.");
            }
        }

        /// <summary>
        /// MỚI: các hành động trong menu "Tùy chọn" cạnh nút "+ Thêm nhân viên" - Xuất CSV/Excel
        /// toàn bộ danh sách nhân viên hiện có + Nhập nhân viên hàng loạt từ file Excel/CSV. Chỉ
        /// áp dụng cho trang "Quản lý nhân viên" (trang duy nhất hiện có nút "+ Thêm..."); các
        /// trang khác (Accounts/Customers) không truyền buildOverflowActions nên không bị ảnh hưởng.
        /// Cùng cấu trúc dữ liệu với Product panel sau này: chỉ cần viết
        /// ProductImportRowHandler + BuildProductOverflowActions tương tự, không phải sửa
        /// BaseTable/ImportDataPresenter/frmImportData.
        /// </summary>
        private static IList<OverflowMenuAction> BuildEmployeeOverflowActions(
            IUserManagementService service, string roleFilter, Func<BaseTable> getTable)
        {
            string[] exportHeaders = { "Tên đăng nhập", "Họ và tên", "Email", "Vai trò", "Trạng thái", "Khóa" };

            Func<IReadOnlyList<object[]>> fetchAllRows = () =>
            {
                var page = service.GetPage(0, 100000, null, roleFilter, null);
                var rows = new List<object[]>();
                foreach (var x in page.Rows)
                {
                    rows.Add(new object[]
                    {
                        x.Username, x.FullName, x.Email, x.RoleName,
                        string.Equals(x.Status, "ACTIVE", StringComparison.OrdinalIgnoreCase) ? "Đang hoạt động" : "Vô hiệu hóa",
                        x.IsLocked ? "Đang khóa" : "Bình thường"
                    });
                }
                return rows;
            };

            var rowHandler = new EmployeeImportRowHandler(service);

            return new List<OverflowMenuAction>
            {
                new OverflowMenuAction("Xuất CSV", IconChar.FileCsv, () =>
                    TableExportRunner.Run(getTable().FindForm(), new CsvTableExporter(), "Nhân viên",
                        () => exportHeaders, fetchAllRows)),

                new OverflowMenuAction("Xuất Excel", IconChar.FileExcel, () =>
                    TableExportRunner.Run(getTable().FindForm(), new ExcelTableExporter(), "Nhân viên",
                        () => exportHeaders, fetchAllRows)),

                new OverflowMenuAction("Nhập dữ liệu", IconChar.Upload, () =>
                {
                    var table = getTable();
                    var result = frmImportData.Show(
                        table.FindForm(),
                        "Nhân viên",
                        EmployeeImportRowHandler.ExpectedColumns,
                        "Ngày sinh/Ngày vào làm định dạng dd/MM/yyyy. Giới tính: Nam/Nữ/Khác. Để trống Lương nếu chưa xác định.",
                        DefaultSpreadsheetImporters.All,
                        rowHandler.Handle);
                    if (result == DialogResult.OK) table.Reload();
                })
            };
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