using System;

namespace SIMS_WinFormsApp.Models.Permission
{
    /// <see cref="PermissionCatalog"/> - enum này chỉ đóng vai trò KHOÁ, dùng để
    /// tra cứu/so sánh và lưu xuống CSDL (PermissionCode = tên enum, xem
    /// <see cref="Repositories.Implementations.RolePermissionRepository"/>).
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

        INVOICE_CREATE,
        INVOICE_VIEW_OWN,
        INVOICE_VIEW_ALL,
        INVOICE_CANCEL_REQUEST,
        INVOICE_CANCEL,
        RETURN_EXCHANGE_CREATE,
        RETURN_EXCHANGE_APPROVE,
        POS_CART_HOLD,
        POS_CART_RESTORE,

        REVENUE_REPORT_VIEW,
        PROFIT_REPORT_VIEW,
        STOCK_REPORT_VIEW,

        AUDIT_LOG_VIEW,
        SETTINGS_MANAGE,
        RBAC_MANAGE
    }
}