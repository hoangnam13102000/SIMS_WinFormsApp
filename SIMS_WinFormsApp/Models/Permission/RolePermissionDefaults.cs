using System;
using System.Collections.Generic;
using System.Linq;
using SIMS_WinFormsApp.Models.Enums;

namespace SIMS_WinFormsApp.Models.Permission
{
    /// <summary>
    /// Bộ quyền MẶC ĐỊNH (hardcode) cho từng RoleCode dựng sẵn của hệ thống -
    /// mirror <c>RolePermissions.DEFAULT_MAP</c> bên dự án Java. Dùng làm:
    /// <list type="bullet">
    /// <item>Dữ liệu "seed" lần đầu xuống bảng RolePermissions khi CSDL còn trống.</item>
    /// <item>Giá trị khôi phục khi Admin bấm nút "Khôi phục mặc định" trên trang
    /// Phân quyền vai trò (<see cref="MVP.Presenters.RolePermissionPresenter"/>).</item>
    /// </list>
    /// Vai trò tuỳ chỉnh do Admin tự tạo thêm (không có trong danh sách này) sẽ
    /// không có bộ mặc định - trả về tập rỗng.
    /// </summary>
    public static class RolePermissionDefaults
    {
        private static readonly Dictionary<string, HashSet<AppPermission>> DefaultMap
            = new Dictionary<string, HashSet<AppPermission>>(StringComparer.OrdinalIgnoreCase);

        static RolePermissionDefaults()
        {
            DefaultMap[RoleCodes.Admin] = new HashSet<AppPermission>(
                Enum.GetValues(typeof(AppPermission)).Cast<AppPermission>());

            DefaultMap[RoleCodes.SalesManager] = new HashSet<AppPermission>(new[]
            {
                AppPermission.DASHBOARD_VIEW,
                AppPermission.PRODUCT_VIEW,
                AppPermission.REVENUE_REPORT_VIEW,
                AppPermission.PROFIT_REPORT_VIEW,
                AppPermission.INVOICE_CREATE,
                AppPermission.INVOICE_VIEW_ALL,
                AppPermission.INVOICE_CANCEL,
                AppPermission.ORDER_VIEW,
                AppPermission.ORDER_MANAGE,
                AppPermission.ORDER_ASSIGN,
                AppPermission.RETURN_EXCHANGE_APPROVE,
                AppPermission.SHIFT_VIEW_ALL,
                AppPermission.SHIFT_APPROVE,
                AppPermission.EXCEPTION_REPORT_HANDLE,
                AppPermission.STOCK_DISPOSE_VIEW,
                AppPermission.PROMOTION_MANAGE
            });

            DefaultMap[RoleCodes.InventoryManager] = new HashSet<AppPermission>(new[]
            {
                AppPermission.DASHBOARD_VIEW,
                AppPermission.PRODUCT_VIEW,
                AppPermission.STOCK_VIEW,
                AppPermission.STOCK_IMPORT,
                AppPermission.STOCK_RECONCILE,
                AppPermission.STOCK_DISPOSE,
                AppPermission.STOCK_DISPOSE_VIEW,
                AppPermission.STOCK_ALERT_VIEW,
                AppPermission.SUPPLIER_RETURN_CREATE,
                AppPermission.SUPPLIER_RETURN_VIEW,
                AppPermission.STOCK_REPORT_VIEW
            });

            DefaultMap[RoleCodes.SalesStaff] = new HashSet<AppPermission>(new[]
            {
                AppPermission.DASHBOARD_VIEW,
                AppPermission.CUSTOMER_MANAGE,
                AppPermission.PRODUCT_VIEW,
                AppPermission.STOCK_ALERT_REPORT,
                AppPermission.INVOICE_CREATE,
                AppPermission.INVOICE_VIEW_OWN,
                AppPermission.SHIFT_OPERATE,
                AppPermission.INVOICE_CANCEL_REQUEST,
                AppPermission.RETURN_EXCHANGE_CREATE,
                AppPermission.EXCEPTION_REPORT_CREATE,
                AppPermission.ORDER_VIEW_ASSIGNED,
                AppPermission.ORDER_PROCESS_ASSIGNED,
                AppPermission.POS_CART_HOLD,
                AppPermission.POS_CART_RESTORE
            });

            DefaultMap[RoleCodes.Customer] = new HashSet<AppPermission>();
        }

        /// <summary>Bộ quyền mặc định của 1 RoleCode - tập rỗng nếu là vai trò tuỳ chỉnh (không rõ mặc định).</summary>
        public static HashSet<AppPermission> GetDefault(string roleCode)
        {
            if (string.IsNullOrWhiteSpace(roleCode)) return new HashSet<AppPermission>();
            return DefaultMap.TryGetValue(roleCode.Trim(), out var set)
                ? new HashSet<AppPermission>(set)
                : new HashSet<AppPermission>();
        }

        /// <summary>Toàn bộ bản đồ mặc định - dùng để seed CSDL lần đầu (xem RolePermissionRepository.EnsureCatalogSeeded).</summary>
        public static IReadOnlyDictionary<string, HashSet<AppPermission>> All() => DefaultMap;
    }
}