using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Forms.Chat;
using SIMS_WinFormsApp.Forms.Dashboard;
using SIMS_WinFormsApp.Forms.SystemMgmt;
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
        private readonly List<PlaceholderPanel> _placeholders = new List<PlaceholderPanel>();

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

        private void OnViewReady(object sender, EventArgs e)
        {
            _layout = BuildLayout();
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

            StartDailyBackupScheduler();
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
            _layout.Header.SetSubtitle(Lang.Get("main.header.subtitle"));

            var sectionHeaders = SectionLabelKeysInOrder.Select(Lang.Get).ToArray();
            var itemLabels = PageLabelKeys.ToDictionary(p => p.PageKey, p => Lang.Get(p.LabelKey));

            _layout.Sidebar.ApplyLabels(
                Lang.Get("sidebar.menu.label"),
                Lang.Get("sidebar.toggle.tooltip"),
                sectionHeaders,
                itemLabels);

            foreach (var placeholder in _placeholders)
                placeholder.ApplyLabels();
        }

        private MainLayoutControl BuildLayout()
        {
            var layout = new MainLayoutControl(Lang.Get("main.header.subtitle"));
            layout.AddSection(Lang.Get("sidebar.section.overview"));
            layout.AddPage("dashboard", Lang.Get("sidebar.page.dashboard"),
                new ucDashboard(_getDisplayName()), IconChar.House);
            layout.AddSection(Lang.Get("sidebar.section.users"));
            layout.AddPage("profile", Lang.Get("header.dropdown.profile"), new SIMS_WinFormsApp.Forms.Profile.ucMyProfile(), IconChar.UserCircle, showInSidebar: false);
            layout.AddPage("accounts", Lang.Get("sidebar.page.accounts"), ManagementTablePage.Accounts(_userManagementService), IconChar.UsersCog);
            layout.AddPage("employees", Lang.Get("sidebar.page.employees"), ManagementTablePage.Employees(_userManagementService), IconChar.User);
            layout.AddPage("customers", Lang.Get("sidebar.page.customers"), ManagementTablePage.Customers(_userManagementService), IconChar.AddressBook);
            layout.AddSection(Lang.Get("sidebar.section.sales"));
            layout.AddPage("pos", Lang.Get("sidebar.page.pos"), PosPage.Create(_getDisplayName()), IconChar.CartShopping);
            layout.AddPage("orders", Lang.Get("sidebar.page.orders"), CreatePlaceholder("placeholder.orders.title", "placeholder.orders.description"), IconChar.ListUl);
            layout.AddPage("invoices", Lang.Get("sidebar.page.invoices"), CreatePlaceholder("placeholder.invoices.title", "placeholder.invoices.description"), IconChar.Receipt);
            layout.AddPage("returns", Lang.Get("sidebar.page.returns"), CreatePlaceholder("placeholder.returns.title", "placeholder.returns.description"), IconChar.RotateLeft);
            layout.AddSection(Lang.Get("sidebar.section.warehouse"));
            layout.AddPage("products", Lang.Get("sidebar.page.products"), CatalogPages.Products(), IconChar.Box);
            layout.AddPage("categories", Lang.Get("sidebar.page.categories"), CatalogPages.Categories(), IconChar.Tags);
            layout.AddPage("suppliers", Lang.Get("sidebar.page.suppliers"), CatalogPages.Suppliers(), IconChar.Building);
            layout.AddPage("inventory", Lang.Get("sidebar.page.inventory"), CreatePlaceholder("placeholder.inventory.title", "placeholder.inventory.description"), IconChar.Warehouse);
            layout.AddPage("purchase", Lang.Get("sidebar.page.purchase"), CreatePlaceholder("placeholder.purchase.title", "placeholder.purchase.description"), IconChar.Truck);
            layout.AddPage("stock-alert", Lang.Get("sidebar.page.stockAlert"), CreatePlaceholder("placeholder.stockAlert.title", "placeholder.stockAlert.description"), IconChar.TriangleExclamation);
            layout.AddSection(Lang.Get("sidebar.section.reports"));
            layout.AddPage("report-revenue", Lang.Get("sidebar.page.reportRevenue"), CreatePlaceholder("placeholder.reportRevenue.title", "placeholder.reportRevenue.description"), IconChar.ChartColumn);
            layout.AddPage("report-inventory", Lang.Get("sidebar.page.reportInventory"), CreatePlaceholder("placeholder.reportInventory.title", "placeholder.reportInventory.description"), IconChar.ChartLine);
            layout.AddSection(Lang.Get("sidebar.section.support"));
            layout.AddPage("chat", Lang.Get("sidebar.page.chat"), new ucChat(), IconChar.Comments);
            layout.AddPage("shifts", Lang.Get("sidebar.page.shifts"), CreatePlaceholder("placeholder.shifts.title", "placeholder.shifts.description"), IconChar.Stopwatch);
            layout.AddSection(Lang.Get("sidebar.section.system"));
            if (CanAccessSettingsPage())
            {
                layout.AddPage("settings", Lang.Get("sidebar.page.settings"), new ucSystemSettings(), IconChar.Gear);
            }
            layout.AddPage("backup", Lang.Get("sidebar.page.backup"), SIMS_WinFormsApp.UI.Controls.Backup.BackupPage.Create(), IconChar.ShieldHalved);
            layout.AddPage("audit-log", "Nhật ký hệ thống", new ucAuditLog(), IconChar.ClockRotateLeft);
            layout.AddPage("role-permissions", "Phân quyền vai trò", new ucRolePermission(), IconChar.UserShield);
            return layout;
        }

        private bool CanAccessSettingsPage()
        {
            var user = UserSession.Instance.CurrentUser;
            if (user == null) return false;
            if (string.Equals(user.RoleCode, RoleCodes.Admin, StringComparison.OrdinalIgnoreCase))
                return true;

            try
            {
                var repository = AppComposition.CreateRolePermissionRepository();
                return repository.GetPermissionsForRole(user.RoleId)
                    .Contains(AppPermission.SETTINGS_MANAGE);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private Control CreatePlaceholder(string titleKey, string descriptionKey)
        {
            var panel = new PlaceholderPanel(titleKey, descriptionKey);
            _placeholders.Add(panel);
            return panel;
        }

        private sealed class PlaceholderPanel : Panel
        {
            private readonly string _titleKey;
            private readonly string _descriptionKey;
            private readonly Label _titleLabel;
            private readonly Label _descLabel;
            private readonly Label _hintLabel;

            public PlaceholderPanel(string titleKey, string descriptionKey)
            {
                _titleKey = titleKey;
                _descriptionKey = descriptionKey;

                Dock = DockStyle.Fill;
                BackColor = Color.FromArgb(241, 245, 249);
                Padding = new Padding(32);

                _titleLabel = new Label
                {
                    AutoSize = true,
                    Font = new Font("Segoe UI Semibold", 18f, FontStyle.Bold),
                    ForeColor = Color.FromArgb(15, 23, 42),
                    Location = new Point(32, 32)
                };
                _descLabel = new Label
                {
                    AutoSize = true,
                    Font = new Font("Segoe UI", 10f),
                    ForeColor = Color.FromArgb(100, 116, 139),
                    Location = new Point(32, 72)
                };
                _hintLabel = new Label
                {
                    AutoSize = true,
                    Font = new Font("Segoe UI", 9f, FontStyle.Italic),
                    ForeColor = Color.FromArgb(148, 163, 184),
                    Location = new Point(32, 110)
                };

                Controls.Add(_titleLabel);
                Controls.Add(_descLabel);
                Controls.Add(_hintLabel);

                ApplyLabels();
            }

            public void ApplyLabels()
            {
                _titleLabel.Text = Lang.Get(_titleKey);
                _descLabel.Text = Lang.Get(_descriptionKey);
                _hintLabel.Text = Lang.Get("placeholder.hint");
            }
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

        public void Dispose()
        {
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