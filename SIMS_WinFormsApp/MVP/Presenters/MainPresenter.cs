using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Views.Chat;
using SIMS_WinFormsApp.Views.Dashboard;
using SIMS_WinFormsApp.Views.Reports;
using SIMS_WinFormsApp.Views.Sales;
using SIMS_WinFormsApp.Views.UserManager;
using SIMS_WinFormsApp.Views.SystemManagement;
using SIMS_WinFormsApp.Views.Warehouse;
using SIMS_WinFormsApp.Views.Interfaces;
using SIMS_WinFormsApp.Infrastructure.Composition;
using SIMS_WinFormsApp.Services.Session;
using SIMS_WinFormsApp.Services.Backup;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.UI.I18n;
using SIMS_WinFormsApp.UI.Layouts;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Controls.Catalog;
using SIMS_WinFormsApp.UI.Controls.Pos;
using SIMS_WinFormsApp.Models.Enums;
using SIMS_WinFormsApp.Models.Permission;

namespace SIMS_WinFormsApp.MVP.Presenters
{
    public class MainPresenter
    {
        private readonly IMainView _view;
        private MainLayoutControl _layout;
        private readonly Func<string> _getDisplayName;
        private readonly Func<string> _getEmail;
        private readonly Func<string> _getRole;
        private readonly IUserManagementService _userManagementService;
        private DailyBackupScheduler _dailyBackupScheduler;
        private bool _isLoggingOut;

        private static readonly string[] SectionLabelKeysInOrder =
        {
            "sidebar.section.overview",
            "sidebar.section.sales",
            "sidebar.section.warehouse",
            "sidebar.section.reports",
            "sidebar.section.users",
            "sidebar.section.support",
            "sidebar.section.system",
        };

        private static readonly (string PageKey, string LabelKey)[] PageLabelKeys =
        {
            ("dashboard", "sidebar.page.dashboard"),
            ("profile", "header.dropdown.profile"),
            ("pos", "sidebar.page.pos"),
            ("orders", "sidebar.page.orders"),
            ("invoices", "sidebar.page.invoices"),
            ("returns", "sidebar.page.returns"),
            ("products", "sidebar.page.products"),
            ("categories", "sidebar.page.categories"),
            ("suppliers", "sidebar.page.suppliers"),
            ("inventory", "sidebar.page.inventory"),
            ("purchase", "sidebar.page.purchase"),
            ("stock-alert", "sidebar.page.stockAlert"),
            ("report-revenue", "sidebar.page.reportRevenue"),
            ("report-inventory", "sidebar.page.reportInventory"),
            ("accounts", "sidebar.page.accounts"),
            ("employees", "sidebar.page.employees"),
            ("customers", "sidebar.page.customers"),
            ("chat", "sidebar.page.chat"),
            ("shifts", "sidebar.page.shifts"),
            ("settings", "sidebar.page.settings"),
        };

        public MainPresenter(
            IMainView view,
            Func<string> getDisplayName = null,
            Func<string> getEmail = null,
            Func<string> getRole = null,
            IUserManagementService userManagementService = null)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _getDisplayName = getDisplayName ?? (() => "Admin");
            _getEmail = getEmail ?? (() => "admin@sims.local");
            _getRole = getRole ?? (() => UserSession.Instance.CurrentUser?.RoleName ?? string.Empty);
            _userManagementService = userManagementService ??
                AppComposition.CreateUserManagementService();
            _view.ViewReady += OnViewReady;
            _view.LogoutRequested += OnLogoutRequested;
            _view.ProfileRequested += OnProfileRequested;
            _view.PageChanged += OnPageChanged;
            LanguageManager.Instance.LanguageChanged += OnLanguageChanged;
        }

        private async void OnViewReady(object sender, EventArgs e)
        {
            try
            {
                _layout = BuildLayout();
                _layout.AiChatRequested += OnAiChatRequested;
                _view.AttachLayout(_layout);
                _view.SetWindowTitle(Lang.Get("main.window.title"));
                var name = _getDisplayName() ?? "Admin";
                var email = _getEmail() ?? string.Empty;
                var role = _getRole() ?? string.Empty;
                _view.SetUserInfo(name, email);
                _layout.SetUser(name, email, null, role);
                _layout.SetBadge("orders", 3);
                _layout.SetUnreadCount(2);
                _layout.ShowPage("dashboard");

                if (!IsAdmin())
                {
                    bool canAccessSettings = await Task.Run(CanAccessSettingsPage);
                    if (canAccessSettings && _layout != null && !_layout.IsDisposed)
                    {
                        _layout.AddLazyPage("settings", Lang.Get("sidebar.page.settings"),
                            () => new ucSystemSettings(), IconChar.Gear);
                    }
                }

                StartDailyBackupScheduler();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("[MainPresenter] Main screen initialization failed: " + ex);
                _view.ShowMessage(
                    "Không thể khởi tạo màn hình chính.\r\n" + ex.Message,
                    "Lỗi khởi tạo",
                    MessageBoxIcon.Error);
            }
        }

        private void StartDailyBackupScheduler()
        {
            if (_dailyBackupScheduler != null) return;

            var backupManager = AppComposition.CreateBackupManager();
            var cloudUploadListener = AppComposition.CreateCloudinaryBackupUploadListener();
            if (cloudUploadListener != null)
                backupManager.AddListener(cloudUploadListener);

            _dailyBackupScheduler = new DailyBackupScheduler(
                backupManager,
                TimeSpan.FromHours(1));
            _dailyBackupScheduler.Start();
        }

        private void OnLanguageChanged(object sender, EventArgs e)
        {
            if (_layout == null) return;

            _view.SetWindowTitle(Lang.Get("main.window.title"));
            _layout.SetSubtitle(Lang.Get("main.header.subtitle"));

            var sectionHeaders = SectionLabelKeysInOrder.Select(Lang.Get).ToArray();
            var itemLabels = PageLabelKeys.ToDictionary(p => p.PageKey, p => Lang.Get(p.LabelKey));

            _layout.ApplyLabels(
                Lang.Get("sidebar.menu.label"),
                Lang.Get("sidebar.toggle.tooltip"),
                sectionHeaders,
                itemLabels);

        }

        private MainLayoutControl BuildLayout()
        {
            var layout = new MainLayoutControl(Lang.Get("main.header.subtitle"));
            layout.AddSection(Lang.Get("sidebar.section.overview"));
            layout.AddPage("dashboard", Lang.Get("sidebar.page.dashboard"),
                new ucDashboard(_getDisplayName()), IconChar.House);
            layout.AddSection(Lang.Get("sidebar.section.users"));
            layout.AddLazyPage("profile", Lang.Get("header.dropdown.profile"),
                () => new SIMS_WinFormsApp.Views.Profile.ucMyProfile(), IconChar.UserCircle, showInSidebar: false);
            layout.AddLazyPage("accounts", Lang.Get("sidebar.page.accounts"),
                () => new frmUserManagement(_userManagementService), IconChar.UsersCog);
            layout.AddLazyPage("employees", Lang.Get("sidebar.page.employees"),
                () => new frmEmployeeManagement(_userManagementService), IconChar.User);
            layout.AddLazyPage("customers", Lang.Get("sidebar.page.customers"),
                () => new frmCustomerManagement(_userManagementService), IconChar.AddressBook);
            layout.AddSection(Lang.Get("sidebar.section.sales"));
            layout.AddLazyPage("pos", Lang.Get("sidebar.page.pos"), () => PosPage.Create(_getDisplayName()), IconChar.CartShopping);
            layout.AddLazyPage("orders", Lang.Get("sidebar.page.orders"), () => new ucOrderManagement(), IconChar.ListUl);
            layout.AddLazyPage("invoices", Lang.Get("sidebar.page.invoices"),
                () => PrepareEmbeddedForm(new frmInvoiceReport()), IconChar.Receipt);
            layout.AddLazyPage("returns", Lang.Get("sidebar.page.returns"), () => new ucReturnManagement(), IconChar.RotateLeft);
            layout.AddSection(Lang.Get("sidebar.section.warehouse"));
            layout.AddLazyPage("products", Lang.Get("sidebar.page.products"), CatalogPages.Products, IconChar.Box);
            layout.AddLazyPage("categories", Lang.Get("sidebar.page.categories"), CatalogPages.Categories, IconChar.Tags);
            layout.AddLazyPage("suppliers", Lang.Get("sidebar.page.suppliers"), CatalogPages.Suppliers, IconChar.Building);
            layout.AddLazyPage("inventory", Lang.Get("sidebar.page.inventory"),
                () => PrepareEmbeddedForm(new frmStockReconciliation()), IconChar.Warehouse);
            layout.AddLazyPage("purchase", Lang.Get("sidebar.page.purchase"),
                () => PrepareEmbeddedForm(new frmPurchaseReceipt()), IconChar.Truck);
            layout.AddLazyPage("stock-alert", Lang.Get("sidebar.page.stockAlert"), () => new ucStockAlert(), IconChar.TriangleExclamation);
            layout.AddSection(Lang.Get("sidebar.section.reports"));
            layout.AddLazyPage("report-revenue", Lang.Get("sidebar.page.reportRevenue"),
                () => PrepareEmbeddedForm(new frmChartDashboard()), IconChar.ChartColumn);
            layout.AddLazyPage("report-inventory", Lang.Get("sidebar.page.reportInventory"),
                () => new ucInventoryReport(), IconChar.ChartLine);
            layout.AddSection(Lang.Get("sidebar.section.support"));
            layout.AddLazyPage("chat", Lang.Get("sidebar.page.chat"), () => new ucChat(), IconChar.Comments);
            layout.AddLazyPage("shifts", Lang.Get("sidebar.page.shifts"), () => new ucShiftManagement(), IconChar.Stopwatch);
            layout.AddSection(Lang.Get("sidebar.section.system"));
            if (IsAdmin())
            {
                layout.AddLazyPage("settings", Lang.Get("sidebar.page.settings"), () => new ucSystemSettings(), IconChar.Gear);
            }
            layout.AddLazyPage("backup", Lang.Get("sidebar.page.backup"), SIMS_WinFormsApp.UI.Controls.Backup.BackupPage.Create, IconChar.ShieldHalved);
            layout.AddLazyPage("audit-log", "Nhật ký hệ thống", () => new ucAuditLog(), IconChar.ClockRotateLeft);
            layout.AddLazyPage("role-permissions", "Phân quyền vai trò", () => new ucRolePermission(), IconChar.UserShield);
            return layout;
        }

        private bool CanAccessSettingsPage()
        {
            var user = UserSession.Instance.CurrentUser;
            if (user == null) return false;
            if (IsAdmin())
                return true;

            try
            {
                var repository = AppComposition.CreateRolePermissionRepository();
                return repository.GetPermissionsForRole(user.RoleId)
                    .Contains(AppPermission.SETTINGS_MANAGE);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("[MainPresenter] Settings permission check failed: " + ex);
                return false;
            }
        }

        private static bool IsAdmin()
        {
            return string.Equals(
                UserSession.Instance.CurrentUser?.RoleCode,
                RoleCodes.Admin,
                StringComparison.OrdinalIgnoreCase);
        }

        private static Form PrepareEmbeddedForm(Form form)
        {
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            return form;
        }

        private void OnLogoutRequested(object sender, EventArgs e)
        {

            if (_isLoggingOut) return;
            _isLoggingOut = true;

            bool confirmed = _view.ConfirmLogout(
                Lang.Get("main.logout.confirm.message"),
                Lang.Get("main.logout.confirm.title"),
                Lang.Get("main.logout.confirm.confirmButton"),
                Lang.Get("main.logout.confirm.cancelButton"));

            if (!confirmed)
            {
                _isLoggingOut = false;
                return;
            }

            UserSession.Instance.SignOut();
            _view.CloseView();

        }

        private void OnProfileRequested(object sender, EventArgs e)
        {
            if (_layout == null) return;

            _layout.ShowPage("profile");
            _view.NavigateTo("profile");
        }

        private void OnPageChanged(object sender, string pageKey)
        {
            System.Diagnostics.Debug.WriteLine($"[MainPresenter] Navigated -> {pageKey}");
        }

        private AiChatPopupForm _aiChatPopup;

        private void OnAiChatRequested(object sender, EventArgs e)
        {
            if (_layout == null || _layout.ChatbotButton == null) return;

            if (_aiChatPopup != null && !_aiChatPopup.IsDisposed)
            {
                _aiChatPopup.Close();
                return;
            }

            var chatView = new ucAiChat(AppComposition.CreateAiChatService());
            var popup = new AiChatPopupForm(chatView);
            popup.FormClosed += (_, __) => _aiChatPopup = null;
            _aiChatPopup = popup;
            popup.ShowAbove(_layout.ChatbotButton, 12);
        }

        public void Dispose()
        {
            if (_layout != null)
            {
                _layout.AiChatRequested -= OnAiChatRequested;
                if (_aiChatPopup != null && !_aiChatPopup.IsDisposed)
                    _aiChatPopup.Close();
                _aiChatPopup = null;
            }
            _dailyBackupScheduler?.Dispose();
            _dailyBackupScheduler = null;
            _view.ViewReady -= OnViewReady;
            _view.LogoutRequested -= OnLogoutRequested;
            _view.ProfileRequested -= OnProfileRequested;
            _view.PageChanged -= OnPageChanged;
            LanguageManager.Instance.LanguageChanged -= OnLanguageChanged;
        }
    }
}