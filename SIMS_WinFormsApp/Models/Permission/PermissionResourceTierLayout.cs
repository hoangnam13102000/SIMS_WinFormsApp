using System.Collections.Generic;

namespace SIMS_WinFormsApp.Models.Permission
{
    /// <summary>1 nấc quyền bên trong 1 "resource dropdown" (vd: Chỉ xem / Chỉ sửa / Quản lý đầy đủ).</summary>
    public sealed class PermissionResourceTierLayout
    {
        public AppPermission Permission { get; }
        public string TierLabel { get; }
        public string TierDescription { get; }

        public PermissionResourceTierLayout(AppPermission permission, string tierLabel, string tierDescription)
        {
            Permission = permission;
            TierLabel = tierLabel;
            TierDescription = tierDescription;
        }
    }

    /// <summary>1 "resource" gộp nhiều quyền liên quan (vd: Tài khoản &amp; nhân viên) hiển thị dạng dropdown.</summary>
    public sealed class PermissionResourceLayout
    {
        public string Name { get; }
        public string Description { get; }
        public IReadOnlyList<PermissionResourceTierLayout> Tiers { get; }

        public PermissionResourceLayout(string name, string description, IReadOnlyList<PermissionResourceTierLayout> tiers)
        {
            Name = name;
            Description = description;
            Tiers = tiers;
        }
    }

    /// <summary>1 dòng trong 1 nhóm quyền trên UI: hoặc là 1 toggle phẳng, hoặc là 1 resource dropdown.</summary>
    public abstract class PermissionGroupLayoutEntry
    {
    }

    public sealed class FlatPermissionLayoutEntry : PermissionGroupLayoutEntry
    {
        public AppPermission Permission { get; }
        public FlatPermissionLayoutEntry(AppPermission permission) { Permission = permission; }
    }

    public sealed class ResourcePermissionLayoutEntry : PermissionGroupLayoutEntry
    {
        public PermissionResourceLayout Resource { get; }
        public ResourcePermissionLayoutEntry(PermissionResourceLayout resource) { Resource = resource; }
    }

    /// <summary>1 nhóm/module quyền hiển thị dạng 1 "card" trên trang Phân quyền vai trò.</summary>
    public sealed class PermissionGroupLayout
    {
        public string Title { get; }
        public string Hint { get; }
        public IReadOnlyList<PermissionGroupLayoutEntry> Entries { get; }

        public PermissionGroupLayout(string title, string hint, IReadOnlyList<PermissionGroupLayoutEntry> entries)
        {
            Title = title;
            Hint = hint;
            Entries = entries;
        }
    }

    /// <summary>
    /// Bố cục hiển thị TĨNH (không đổi theo dữ liệu) của trang "Phân quyền vai
    /// trò" - thứ tự nhóm: Tổng quan -> Người dùng -> Hàng hoá -> Kho hàng ->
    /// Bán hàng -> Báo cáo -> Hệ thống, mirror đúng cấu trúc RolePermissionPanel.java.
    /// Đây là dữ liệu THUẦN (SRP) - <see cref="MVP.Presenters.RolePermissionPresenter"/>
    /// dùng nó để dựng view-model gửi xuống View, View không tự biết cấu trúc này.
    /// </summary>
    public static class PermissionDisplayLayout
    {
        private static readonly List<PermissionGroupLayout> GroupsInternal = Build();

        public static IReadOnlyList<PermissionGroupLayout> Groups() => GroupsInternal;

        private static List<PermissionGroupLayout> Build()
        {
            var groups = new List<PermissionGroupLayout>();

            groups.Add(new PermissionGroupLayout("Tổng quan", null, new List<PermissionGroupLayoutEntry>
            {
                new FlatPermissionLayoutEntry(AppPermission.DASHBOARD_VIEW)
            }));

            groups.Add(new PermissionGroupLayout("Người dùng",
                "Bấm mũi tên để mở và bật/tắt từng quyền Xem · Sửa · Quản lý đầy đủ",
                new List<PermissionGroupLayoutEntry>
                {
                    new ResourcePermissionLayoutEntry(new PermissionResourceLayout(
                        "Tài khoản & nhân viên", "Xem / sửa / thêm NV · khoá tài khoản đăng nhập",
                        ViewEditManageTiers(AppPermission.USER_VIEW, AppPermission.USER_EDIT, AppPermission.USER_MANAGE))),
                    new ResourcePermissionLayoutEntry(new PermissionResourceLayout(
                        "Khách hàng", "Xem / sửa / xoá mềm khách hàng",
                        ViewEditManageTiers(AppPermission.CUSTOMER_VIEW, AppPermission.CUSTOMER_EDIT, AppPermission.CUSTOMER_MANAGE)))
                }));

            groups.Add(new PermissionGroupLayout("Hàng hoá",
                "Bấm mũi tên để mở và bật/tắt từng quyền Xem · Sửa · Quản lý đầy đủ",
                new List<PermissionGroupLayoutEntry>
                {
                    new ResourcePermissionLayoutEntry(new PermissionResourceLayout(
                        "Danh mục", "Xem / sửa / thêm-xoá danh mục sản phẩm",
                        ViewEditManageTiers(AppPermission.CATEGORY_VIEW, AppPermission.CATEGORY_EDIT, AppPermission.CATEGORY_MANAGE))),
                    new ResourcePermissionLayoutEntry(new PermissionResourceLayout(
                        "Sản phẩm", "Xem / sửa / thêm-xoá thông tin sản phẩm, giá bán",
                        ViewEditManageTiers(AppPermission.PRODUCT_VIEW, AppPermission.PRODUCT_EDIT, AppPermission.PRODUCT_MANAGE))),
                    new ResourcePermissionLayoutEntry(new PermissionResourceLayout(
                        "Nhà cung cấp", "Xem / sửa / thêm-xoá nhà cung cấp",
                        ViewEditManageTiers(AppPermission.SUPPLIER_VIEW, AppPermission.SUPPLIER_EDIT, AppPermission.SUPPLIER_MANAGE)))
                }));

            groups.Add(new PermissionGroupLayout("Kho hàng", null, new List<PermissionGroupLayoutEntry>
            {
                new FlatPermissionLayoutEntry(AppPermission.STOCK_VIEW),
                new FlatPermissionLayoutEntry(AppPermission.STOCK_IMPORT),
                new FlatPermissionLayoutEntry(AppPermission.STOCK_RECONCILE),
                new FlatPermissionLayoutEntry(AppPermission.STOCK_DISPOSE),
                new FlatPermissionLayoutEntry(AppPermission.STOCK_DISPOSE_VIEW),
                new FlatPermissionLayoutEntry(AppPermission.STOCK_ALERT_REPORT),
                new FlatPermissionLayoutEntry(AppPermission.STOCK_ALERT_VIEW),
                new FlatPermissionLayoutEntry(AppPermission.SUPPLIER_RETURN_CREATE),
                new FlatPermissionLayoutEntry(AppPermission.SUPPLIER_RETURN_VIEW)
            }));

            groups.Add(new PermissionGroupLayout("Bán hàng", null, new List<PermissionGroupLayoutEntry>
            {
                new FlatPermissionLayoutEntry(AppPermission.INVOICE_CREATE),
                new FlatPermissionLayoutEntry(AppPermission.INVOICE_VIEW_OWN),
                new FlatPermissionLayoutEntry(AppPermission.INVOICE_VIEW_ALL),
                new FlatPermissionLayoutEntry(AppPermission.INVOICE_CANCEL_REQUEST),
                new FlatPermissionLayoutEntry(AppPermission.INVOICE_CANCEL),
                new FlatPermissionLayoutEntry(AppPermission.SHIFT_OPERATE),
                new FlatPermissionLayoutEntry(AppPermission.SHIFT_VIEW_ALL),
                new FlatPermissionLayoutEntry(AppPermission.SHIFT_APPROVE),
                new FlatPermissionLayoutEntry(AppPermission.RETURN_EXCHANGE_CREATE),
                new FlatPermissionLayoutEntry(AppPermission.RETURN_EXCHANGE_APPROVE),
                new FlatPermissionLayoutEntry(AppPermission.ORDER_VIEW),
                new FlatPermissionLayoutEntry(AppPermission.ORDER_MANAGE),
                new FlatPermissionLayoutEntry(AppPermission.ORDER_VIEW_ASSIGNED),
                new FlatPermissionLayoutEntry(AppPermission.ORDER_PROCESS_ASSIGNED),
                new FlatPermissionLayoutEntry(AppPermission.ORDER_ASSIGN),
                new FlatPermissionLayoutEntry(AppPermission.POS_CART_HOLD),
                new FlatPermissionLayoutEntry(AppPermission.POS_CART_RESTORE),
                new FlatPermissionLayoutEntry(AppPermission.PROMOTION_MANAGE)
            }));

            groups.Add(new PermissionGroupLayout("Báo cáo",
                "Báo cáo ngoại lệ: mở dropdown để bật Xem · Gửi · Xử lý",
                new List<PermissionGroupLayoutEntry>
                {
                    new ResourcePermissionLayoutEntry(new PermissionResourceLayout(
                        "Báo cáo ngoại lệ", "NV gửi báo cáo bất thường · QL xem và xử lý",
                        new List<PermissionResourceTierLayout>
                        {
                            new PermissionResourceTierLayout(AppPermission.EXCEPTION_REPORT_VIEW,
                                "Chỉ xem", "Xem danh sách báo cáo, không gửi / xử lý"),
                            new PermissionResourceTierLayout(AppPermission.EXCEPTION_REPORT_CREATE,
                                "Gửi báo cáo", "Tạo báo cáo ngoại lệ mới (NV bán hàng)"),
                            new PermissionResourceTierLayout(AppPermission.EXCEPTION_REPORT_HANDLE,
                                "Xử lý báo cáo", "Đánh dấu đã xử lý báo cáo chờ xử lý")
                        })),
                    new FlatPermissionLayoutEntry(AppPermission.REVENUE_REPORT_VIEW),
                    new FlatPermissionLayoutEntry(AppPermission.PROFIT_REPORT_VIEW),
                    new FlatPermissionLayoutEntry(AppPermission.STOCK_REPORT_VIEW)
                }));

            groups.Add(new PermissionGroupLayout("Hệ thống", null, new List<PermissionGroupLayoutEntry>
            {
                new FlatPermissionLayoutEntry(AppPermission.AUDIT_LOG_VIEW),
                new FlatPermissionLayoutEntry(AppPermission.BACKUP_MANAGE),
                new FlatPermissionLayoutEntry(AppPermission.SETTINGS_MANAGE),
                new FlatPermissionLayoutEntry(AppPermission.RBAC_MANAGE)
            }));

            return groups;
        }

        private static List<PermissionResourceTierLayout> ViewEditManageTiers(
            AppPermission viewPermission, AppPermission editPermission, AppPermission managePermission)
        {
            return new List<PermissionResourceTierLayout>
            {
                new PermissionResourceTierLayout(viewPermission, "Chỉ xem", "Chỉ xem/tìm kiếm, không thêm/sửa/xoá"),
                new PermissionResourceTierLayout(editPermission, "Chỉ sửa", "Sửa bản ghi đã có, không thêm mới / xoá"),
                new PermissionResourceTierLayout(managePermission, "Quản lý đầy đủ", "Thêm mới + sửa + xoá / đổi trạng thái")
            };
        }
    }
}