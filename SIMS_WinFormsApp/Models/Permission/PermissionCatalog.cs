using System.Collections.Generic;

namespace SIMS_WinFormsApp.Models.Permission
{
    public static class PermissionCatalog
    {
        private static readonly List<KeyValuePair<AppPermission, PermissionCatalogEntry>> OrderedEntries
            = new List<KeyValuePair<AppPermission, PermissionCatalogEntry>>();

        private static readonly Dictionary<AppPermission, PermissionCatalogEntry> Lookup
            = new Dictionary<AppPermission, PermissionCatalogEntry>();

        static PermissionCatalog()
        {
            Put(AppPermission.DASHBOARD_VIEW, "Tổng quan",
                "Xem trang tổng quan", "Xem số liệu tổng quan (doanh thu, đơn hàng, tồn kho...) trên Dashboard.");

            Put(AppPermission.USER_MANAGE, "Người dùng",
                "Quản lý tài khoản & NV (đầy đủ)", "Thêm nhân viên + sửa + khoá/mở khoá tài khoản.");
            Put(AppPermission.USER_EDIT, "Người dùng",
                "Chỉ sửa tài khoản & NV", "Sửa thông tin tài khoản/nhân viên, không thêm mới hay khoá.");
            Put(AppPermission.USER_VIEW, "Người dùng",
                "Chỉ xem tài khoản & NV", "Chỉ xem/tìm kiếm tài khoản và nhân viên.");
            Put(AppPermission.CUSTOMER_MANAGE, "Người dùng",
                "Quản lý khách hàng (đầy đủ)", "Sửa + xoá mềm / thùng rác khách hàng.");
            Put(AppPermission.CUSTOMER_EDIT, "Người dùng",
                "Chỉ sửa khách hàng", "Sửa thông tin khách hàng, không xoá.");
            Put(AppPermission.CUSTOMER_VIEW, "Người dùng",
                "Chỉ xem khách hàng", "Chỉ xem/tìm kiếm khách hàng, không sửa/xoá.");

            Put(AppPermission.CATEGORY_MANAGE, "Hàng hoá",
                "Quản lý danh mục (đầy đủ)", "Thêm mới + sửa + xoá/vô hiệu hoá danh mục sản phẩm.");
            Put(AppPermission.CATEGORY_EDIT, "Hàng hoá",
                "Chỉ sửa danh mục", "Sửa danh mục đã có và đổi trạng thái, không được thêm mới.");
            Put(AppPermission.CATEGORY_VIEW, "Hàng hoá",
                "Chỉ xem danh mục", "Chỉ xem/tìm kiếm danh mục, không được thêm/sửa/xoá.");
            Put(AppPermission.PRODUCT_MANAGE, "Hàng hoá",
                "Quản lý sản phẩm (đầy đủ)", "Thêm mới + sửa + đổi trạng thái bán sản phẩm.");
            Put(AppPermission.PRODUCT_EDIT, "Hàng hoá",
                "Chỉ sửa sản phẩm", "Sửa thông tin sản phẩm đã có và đổi trạng thái bán, không được thêm mới.");
            Put(AppPermission.PRODUCT_VIEW, "Hàng hoá",
                "Chỉ xem sản phẩm", "Chỉ xem/tìm kiếm sản phẩm, không được thêm/sửa/xoá.");
            Put(AppPermission.SUPPLIER_MANAGE, "Hàng hoá",
                "Quản lý nhà cung cấp (đầy đủ)", "Thêm mới + sửa + xoá mềm / thùng rác nhà cung cấp.");
            Put(AppPermission.SUPPLIER_EDIT, "Hàng hoá",
                "Chỉ sửa nhà cung cấp", "Sửa thông tin nhà cung cấp đã có, không được thêm mới hay xoá.");
            Put(AppPermission.SUPPLIER_VIEW, "Hàng hoá",
                "Chỉ xem nhà cung cấp", "Chỉ xem/tìm kiếm nhà cung cấp, không được thêm/sửa/xoá.");

            Put(AppPermission.STOCK_VIEW, "Kho hàng",
                "Xem tồn kho", "Xem tình trạng tồn kho, danh sách lô hàng.");
            Put(AppPermission.STOCK_IMPORT, "Kho hàng",
                "Nhập kho", "Lập phiếu nhập hàng, tạo lô hàng mới.");
            Put(AppPermission.STOCK_RECONCILE, "Kho hàng",
                "Đối chiếu kho cuối ngày", "Đối chiếu / kiểm kê tồn kho, so sánh tồn hệ thống với tồn đếm thực tế.");
            Put(AppPermission.STOCK_DISPOSE, "Kho hàng",
                "Tiêu huỷ hàng", "Lập phiếu tiêu huỷ hàng hỏng/hết hạn (trừ lô + ghi tổn thất).");
            Put(AppPermission.STOCK_DISPOSE_VIEW, "Kho hàng",
                "Xem lịch sử tiêu huỷ", "Xem lịch sử tiêu huỷ và báo cáo tổn thất tài chính.");
            Put(AppPermission.STOCK_ALERT_REPORT, "Kho hàng",
                "Báo cáo hàng sắp hết", "Báo cáo sản phẩm hết/sắp hết hàng cho Quản lý kho.");
            Put(AppPermission.STOCK_ALERT_VIEW, "Kho hàng",
                "Xử lý cảnh báo tồn", "Xem và xử lý các báo cáo hết/sắp hết hàng, lên kế hoạch nhập bổ sung.");
            Put(AppPermission.SUPPLIER_RETURN_CREATE, "Kho hàng",
                "Trả hàng nhà cung cấp", "Lập phiếu trả hàng lô về nhà cung cấp (trừ lô + ghi công nợ).");
            Put(AppPermission.SUPPLIER_RETURN_VIEW, "Kho hàng",
                "Xem trả hàng NCC", "Xem lịch sử trả hàng nhà cung cấp và báo cáo công nợ.");

            Put(AppPermission.INVOICE_CREATE, "Bán hàng",
                "Tạo hoá đơn", "Lập hoá đơn bán hàng tại quầy (POS).");
            Put(AppPermission.INVOICE_VIEW_OWN, "Bán hàng",
                "Xem hóa đơn của mình", "Chỉ xem các hóa đơn do chính nhân viên đang đăng nhập tạo.");
            Put(AppPermission.INVOICE_VIEW_ALL, "Bán hàng",
                "Xem tất cả hóa đơn", "Xem hóa đơn của tất cả nhân viên.");
            Put(AppPermission.INVOICE_CANCEL_REQUEST, "Bán hàng",
                "Yêu cầu huỷ hoá đơn", "Gửi yêu cầu huỷ hoá đơn để Quản lý bán hàng/Admin duyệt hoặc từ chối.");
            Put(AppPermission.INVOICE_CANCEL, "Bán hàng",
                "Huỷ hoá đơn", "Duyệt và thực hiện huỷ hoá đơn theo thẩm quyền quản lý.");
            Put(AppPermission.SHIFT_OPERATE, "Bán hàng",
                "Vận hành ca bán hàng", "Mở ca, ghi thu/chi và đóng/đối soát ca của chính nhân viên.");
            Put(AppPermission.SHIFT_VIEW_ALL, "Bán hàng",
                "Xem tất cả ca bán hàng", "Xem lịch sử ca và chênh lệch quỹ của tất cả nhân viên.");
            Put(AppPermission.SHIFT_APPROVE, "Bán hàng",
                "Duyệt đối soát ca", "Duyệt hoặc từ chối kết quả đối soát quỹ khi nhân viên đóng ca.");
            Put(AppPermission.RETURN_EXCHANGE_CREATE, "Bán hàng",
                "Tạo yêu cầu đổi/trả", "Tạo yêu cầu đổi/trả hàng cho 1 hoá đơn (bắt buộc ghi rõ lý do).");
            Put(AppPermission.RETURN_EXCHANGE_APPROVE, "Bán hàng",
                "Duyệt đổi/trả hàng", "Duyệt/từ chối yêu cầu đổi/trả hàng giá trị lớn.");
            Put(AppPermission.ORDER_VIEW, "Bán hàng",
                "Xem đơn hàng online", "Xem đơn hàng online từ khách.");
            Put(AppPermission.ORDER_MANAGE, "Bán hàng",
                "Xử lý tất cả đơn online", "Xác nhận / huỷ tất cả đơn hàng online từ khách.");
            Put(AppPermission.ORDER_VIEW_ASSIGNED, "Bán hàng",
                "Xem đơn được giao", "Chỉ xem các đơn hàng online được gán cho chính nhân viên đang đăng nhập.");
            Put(AppPermission.ORDER_PROCESS_ASSIGNED, "Bán hàng",
                "Xử lý đơn được giao", "Chỉ xử lý trạng thái các đơn hàng online được gán cho chính nhân viên đang đăng nhập.");
            Put(AppPermission.ORDER_ASSIGN, "Bán hàng",
                "Gán đơn cho nhân viên", "Gán hoặc đổi nhân viên bán hàng phụ trách đơn online.");
            Put(AppPermission.POS_CART_HOLD, "Bán hàng",
                "Tạm giữ giỏ POS", "Tạm giữ giỏ hàng hiện tại để phục vụ khách khác trong cùng ca.");
            Put(AppPermission.POS_CART_RESTORE, "Bán hàng",
                "Khôi phục giỏ POS", "Tìm, khôi phục hoặc hủy các giỏ tạm giữ của chính nhân viên trong ca hiện tại.");
            Put(AppPermission.PROMOTION_MANAGE, "Bán hàng",
                "Quản lý khuyến mãi", "Tạo, sửa, bật/tắt, xoá khuyến mãi / mã giảm giá.");

            Put(AppPermission.EXCEPTION_REPORT_VIEW, "Báo cáo",
                "Chỉ xem báo cáo ngoại lệ", "Chỉ xem danh sách báo cáo ngoại lệ, không gửi mới / xử lý.");
            Put(AppPermission.EXCEPTION_REPORT_CREATE, "Báo cáo",
                "Gửi báo cáo ngoại lệ", "Gửi báo cáo sản phẩm chưa có trong hệ thống / tình huống bất thường.");
            Put(AppPermission.EXCEPTION_REPORT_HANDLE, "Báo cáo",
                "Xử lý báo cáo ngoại lệ", "Đánh dấu đã xử lý các báo cáo ngoại lệ từ nhân viên bán hàng.");
            Put(AppPermission.REVENUE_REPORT_VIEW, "Báo cáo",
                "Báo cáo doanh thu", "Thống kê doanh thu theo thời gian / sản phẩm / phương thức thanh toán.");
            Put(AppPermission.PROFIT_REPORT_VIEW, "Báo cáo",
                "Báo cáo lợi nhuận", "So sánh giá nhập/giá bán, lợi nhuận gộp theo sản phẩm/danh mục/kỳ.");
            Put(AppPermission.STOCK_REPORT_VIEW, "Báo cáo",
                "Báo cáo hàng tồn kho", "Thống kê số lượng/giá trị tồn kho theo danh mục, khoảng giá bán, xu hướng theo tháng.");

            Put(AppPermission.AUDIT_LOG_VIEW, "Hệ thống",
                "Nhật ký audit", "Xem lịch sử thao tác (thêm/sửa/xoá/đăng nhập...) của người dùng.");
            Put(AppPermission.BACKUP_MANAGE, "Hệ thống",
                "Sao lưu & khôi phục", "Tự sao lưu thủ công hoặc khôi phục CSDL từ file backup.");
            Put(AppPermission.SETTINGS_MANAGE, "Hệ thống",
                "Cài đặt hệ thống", "Xem/sửa cấu hình chung (thuế VAT, ngưỡng duyệt đổi trả...).");
            Put(AppPermission.RBAC_MANAGE, "Hệ thống",
                "Phân quyền vai trò", "Xem/chỉnh sửa quyền truy cập chức năng theo từng vai trò trong hệ thống.");
        }

        private static void Put(AppPermission permission, string group, string label, string description)
        {
            var entry = new PermissionCatalogEntry(group, label, description);
            Lookup[permission] = entry;
            OrderedEntries.Add(new KeyValuePair<AppPermission, PermissionCatalogEntry>(permission, entry));
        }

        /// <summary>Metadata của 1 quyền. Không bao giờ null - quyền chưa khai báo trả về entry mặc định.</summary>
        public static PermissionCatalogEntry Get(AppPermission permission)
        {
            return Lookup.TryGetValue(permission, out var entry)
                ? entry
                : new PermissionCatalogEntry("Khác", permission.ToString(), string.Empty);
        }

        /// <summary>Toàn bộ catalog, GIỮ THỨ TỰ khai báo (dùng để nhóm quyền trên UI).</summary>
        public static IReadOnlyList<KeyValuePair<AppPermission, PermissionCatalogEntry>> All()
        {
            return OrderedEntries;
        }
    }
}