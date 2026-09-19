using System;
using System.Collections.Generic;
using System.Drawing;
using SIMS_WinFormsApp.UI.I18n;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Models.Mapping
{
    public static class AuditLogDisplayMapper
    {
        private static readonly HashSet<string> IncidentActions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "LOGIN_FAILED", "TWOFA_FAILED", "USER_LOCK", "SYSTEM_ERROR"
        };

        private static readonly Dictionary<string, string> ActionLabels = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["LOGIN"] = "audit.action.login",
            ["LOGIN_FAILED"] = "audit.action.loginFailed",
            ["LOGOUT"] = "audit.action.logout",
            ["TWOFA_SUCCESS"] = "audit.action.twoFactorSuccess",
            ["TWOFA_FAILED"] = "audit.action.twoFactorFailed",
            ["USER_LOCK"] = "audit.action.userLock",
            ["USER_UNLOCK"] = "audit.action.userUnlock",
            ["USER_UPDATE"] = "audit.action.userUpdate",
            ["USER_CREATE"] = "audit.action.userCreate",
            ["PASSWORD_CHANGE"] = "audit.action.passwordChange",
            ["ROLE_PERMISSION_GRANT"] = "audit.action.rolePermissionGrant",
            ["ROLE_PERMISSION_REVOKE"] = "audit.action.rolePermissionRevoke",
            ["SYSTEM_ERROR"] = "audit.action.systemError",
        };

        private static readonly Dictionary<string, string> TableLabels = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["Users"] = "audit.table.users",
            ["Roles"] = "audit.table.roles",
            ["RolePermissions"] = "audit.table.rolePermissions",
            ["Invoices"] = "audit.table.invoices",
            ["Products"] = "audit.table.products",
            ["Customers"] = "audit.table.customers",
            ["Employees"] = "audit.table.employees",
        };

        public static bool IsIncident(string action)
        {
            if (string.IsNullOrEmpty(action)) return false;
            return IncidentActions.Contains(action)
                || action.IndexOf("FAIL", StringComparison.OrdinalIgnoreCase) >= 0
                || action.IndexOf("ERROR", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public static string GetActionLabel(string action)
        {
            if (string.IsNullOrEmpty(action)) return string.Empty;
            return ActionLabels.TryGetValue(action, out var resourceKey) ? Lang.Get(resourceKey) : action;
        }

        public static string GetTableLabel(string tableName)
        {
            if (string.IsNullOrEmpty(tableName)) return "-";
            return TableLabels.TryGetValue(tableName, out var resourceKey) ? Lang.Get(resourceKey) : tableName;
        }

        public static (Color Bg, Color Fg) GetActionColors(string action)
        {
            if (IsIncident(action)) return (AppColors.ErrorBg, AppColors.Error);
            if (!string.IsNullOrEmpty(action) &&
                (action.IndexOf("SUCCESS", StringComparison.OrdinalIgnoreCase) >= 0 ||
                 string.Equals(action, "LOGIN", StringComparison.OrdinalIgnoreCase)))
                return (AppColors.SuccessBg, AppColors.Success);
            return (AppColors.InfoBg, AppColors.Info);
        }

        public static (Color Bg, Color Fg) GetTableColors(string tableName) =>
            (AppColors.AccentBgSoft, AppColors.Accent);
    }
}