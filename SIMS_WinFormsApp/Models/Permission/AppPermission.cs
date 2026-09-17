using System;

namespace SIMS_WinFormsApp.Models.Permission
{
    /// <summary>
    /// Toàn bộ "mã quyền" (permission code) của hệ thống SIMS.
    /// Đây là KHOÁ/logic thuần (không có nhãn/mô tả hiển thị) - mirror 1:1 từ
    /// enum AppPermission bên dự án Java (com.model.permission.AppPermission)
    /// để 2 bản Java/C# luôn thống nhất danh sách chức năng được phân quyền.
    /// <para>
    /// Nhãn hiển thị (nhóm module, tên ngắn, mô tả) tách riêng trong
    /// <see cref="PermissionCatalog"/> - enum này chỉ đóng vai trò KHOÁ, dùng để
    /// tra cứu/so sánh và lưu xuống CSDL (PermissionCode = tên enum, xem
    /// <see cref="Repositories.Implementations.RolePermissionRepository"/>).
    /// </para>
    /// </summary>
    public enum AppPermission
    {
        DASHBOARD_VIEW,

        USER_MANAGE,
        USER_VIEW,
        USER_EDIT,
        CUSTOMER_MANAGE,
        CUSTOMER_VIEW,
        CUSTOMER_EDIT,

        CATEGORY_MANAGE,
        CATEGORY_VIEW,
        CATEGORY_EDIT,
        PRODUCT_MANAGE,
        PRODUCT_VIEW,
        PRODUCT_EDIT,
        SUPPLIER_MANAGE,
        SUPPLIER_VIEW,
        SUPPLIER_EDIT,

        STOCK_VIEW,
        STOCK_IMPORT,
        STOCK_RECONCILE,
        STOCK_DISPOSE,
        STOCK_DISPOSE_VIEW,
        STOCK_ALERT_REPORT,
        STOCK_ALERT_VIEW,
        SUPPLIER_RETURN_CREATE,
        SUPPLIER_RETURN_VIEW,

        INVOICE_CREATE,
        INVOICE_VIEW_OWN,
        INVOICE_VIEW_ALL,
        INVOICE_CANCEL_REQUEST,
        INVOICE_CANCEL,
        SHIFT_OPERATE,
        SHIFT_VIEW_ALL,
        SHIFT_APPROVE,
        RETURN_EXCHANGE_CREATE,
        RETURN_EXCHANGE_APPROVE,
        ORDER_VIEW,
        ORDER_MANAGE,
        ORDER_VIEW_ASSIGNED,
        ORDER_PROCESS_ASSIGNED,
        ORDER_ASSIGN,
        POS_CART_HOLD,
        POS_CART_RESTORE,
        PROMOTION_MANAGE,

        EXCEPTION_REPORT_VIEW,
        EXCEPTION_REPORT_CREATE,
        EXCEPTION_REPORT_HANDLE,
        REVENUE_REPORT_VIEW,
        PROFIT_REPORT_VIEW,
        STOCK_REPORT_VIEW,

        AUDIT_LOG_VIEW,
        BACKUP_MANAGE,
        SETTINGS_MANAGE,
        RBAC_MANAGE
    }
}