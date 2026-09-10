using System;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Forms.Dashboard;
using SIMS_WinFormsApp.MVP.Views;
using SIMS_WinFormsApp.Services.Session;
using SIMS_WinFormsApp.UI.I18n;
using SIMS_WinFormsApp.UI.Layouts;

namespace SIMS_WinFormsApp.MVP.Presenters
{
    public class MainPresenter
    {
        private readonly IMainView _view;
        private MainLayoutControl _layout;
        private readonly Func<string> _getDisplayName;
        private readonly Func<string> _getEmail;
        private readonly Func<string> _getRole;

        public MainPresenter(
            IMainView view,
            Func<string> getDisplayName = null,
            Func<string> getEmail = null,
            Func<string> getRole = null)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _getDisplayName = getDisplayName ?? (() => "Admin");
            _getEmail = getEmail ?? (() => "admin@sims.local");
            // Đọc Role từ session hiện có (User.RoleName) — chỉ ĐỌC dữ liệu đã có sẵn để
            // hiển thị lên Header dropdown, KHÔNG thêm logic nghiệp vụ/service mới nào.
            _getRole = getRole ?? (() => UserSession.Instance.CurrentUser?.RoleName ?? string.Empty);
            _view.ViewReady += OnViewReady;
            _view.LogoutRequested += OnLogoutRequested;
            _view.ProfileRequested += OnProfileRequested;
            _view.PageChanged += OnPageChanged;
        }

        private void OnViewReady(object sender, EventArgs e)
        {
            _layout = BuildLayout();
            _view.AttachLayout(_layout);
            _view.SetWindowTitle("SIMS — Quản trị hệ thống");
            var name = _getDisplayName() ?? "Admin";
            var email = _getEmail() ?? string.Empty;
            var role = _getRole() ?? string.Empty;
            _view.SetUserInfo(name, email);
            _layout.SetUser(name, email, null, role);
            _layout.SetBadge("orders", 3);
            _layout.SetUnreadCount(2);
            _layout.ShowPage("dashboard");
        }

        private MainLayoutControl BuildLayout()
        {
            var layout = new MainLayoutControl("Cửa hàng điện thoại trực tuyến");
            layout.AddSection("Tổng quan");
            layout.AddPage("dashboard", "Tổng quan", new ucDashboard(), IconChar.House);
            layout.AddSection("Bán hàng");
            layout.AddPage("pos", "POS bán hàng", CreatePlaceholder("POS", "Màn hình bán hàng tại quầy"), IconChar.CartShopping);
            layout.AddPage("orders", "Đơn hàng", CreatePlaceholder("Đơn hàng", "Danh sách đơn hàng"), IconChar.ListUl);
            layout.AddPage("invoices", "Hóa đơn", CreatePlaceholder("Hóa đơn", "Quản lý hóa đơn"), IconChar.Receipt);
            layout.AddPage("returns", "Đổi trả", CreatePlaceholder("Đổi trả", "Return / Exchange"), IconChar.RotateLeft);
            layout.AddSection("Kho hàng");
            layout.AddPage("products", "Sản phẩm", CreatePlaceholder("Sản phẩm", "Danh mục sản phẩm"), IconChar.Box);
            layout.AddPage("inventory", "Tồn kho", CreatePlaceholder("Tồn kho", "Lô hàng & số lượng"), IconChar.Warehouse);
            layout.AddPage("purchase", "Nhập hàng", CreatePlaceholder("Nhập hàng", "Phiếu nhập"), IconChar.Truck);
            layout.AddPage("stock-alert", "Cảnh báo tồn", CreatePlaceholder("Cảnh báo tồn", "Sắp hết / hết hàng"), IconChar.TriangleExclamation);
            layout.AddSection("Báo cáo");
            layout.AddPage("report-revenue", "Doanh thu", CreatePlaceholder("Doanh thu", "Báo cáo doanh thu"), IconChar.ChartColumn);
            layout.AddPage("report-inventory", "Báo cáo kho", CreatePlaceholder("Báo cáo kho", "Xuất nhập tồn"), IconChar.ChartLine);
            layout.AddSection("Hệ thống");
            layout.AddPage("employees", "Nhân viên", CreatePlaceholder("Nhân viên", "Quản lý nhân viên"), IconChar.User);
            layout.AddPage("customers", "Khách hàng", CreatePlaceholder("Khách hàng", "Danh sách khách"), IconChar.Users);
            layout.AddPage("shifts", "Ca làm việc", CreatePlaceholder("Ca làm việc", "Mở/đóng ca & đối soát"), IconChar.Stopwatch);
            layout.AddPage("settings", "Cài đặt", CreatePlaceholder("Cài đặt", "Cấu hình hệ thống"), IconChar.Gear);
            layout.LogoutRequested += (_, __) => OnLogoutRequested(this, EventArgs.Empty);
            layout.ProfileRequested += (_, __) => OnProfileRequested(this, EventArgs.Empty);
            layout.PageChanged += (_, key) => _view.NavigateTo(key);
            return layout;
        }

        private static Control CreatePlaceholder(string title, string description)
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(241, 245, 249),
                Padding = new Padding(32)
            };
            var titleLabel = new Label
            {
                AutoSize = true,
                Text = title,
                Font = new Font("Segoe UI Semibold", 18f, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                Location = new Point(32, 32)
            };
            var descLabel = new Label
            {
                AutoSize = true,
                Text = description,
                Font = new Font("Segoe UI", 10f),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(32, 72)
            };
            var hint = new Label
            {
                AutoSize = true,
                Text = "Thay UserControl placeholder này bằng form/module thật (Products, POS, …).",
                Font = new Font("Segoe UI", 9f, FontStyle.Italic),
                ForeColor = Color.FromArgb(148, 163, 184),
                Location = new Point(32, 110)
            };
            panel.Controls.Add(titleLabel);
            panel.Controls.Add(descLabel);
            panel.Controls.Add(hint);
            return panel;
        }

        private void OnLogoutRequested(object sender, EventArgs e)
        {
            bool confirmed = _view.ConfirmLogout(
                Lang.Get("main.logout.confirm.message"),
                Lang.Get("main.logout.confirm.title"),
                Lang.Get("main.logout.confirm.confirmButton"),
                Lang.Get("main.logout.confirm.cancelButton"));

            if (confirmed)
            {
                UserSession.Instance.SignOut();
                _view.CloseView();
            }
        }

        private void OnProfileRequested(object sender, EventArgs e)
        {
            _view.ShowMessage("Mở form hồ sơ cá nhân (chưa gắn).", "Hồ sơ", MessageBoxIcon.Information);
        }

        private void OnPageChanged(object sender, string pageKey)
        {
            System.Diagnostics.Debug.WriteLine($"[MainPresenter] Navigated -> {pageKey}");
        }

        public void Dispose()
        {
            _view.ViewReady -= OnViewReady;
            _view.LogoutRequested -= OnLogoutRequested;
            _view.ProfileRequested -= OnProfileRequested;
            _view.PageChanged -= OnPageChanged;
        }
    }
}