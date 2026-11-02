using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Views.Chat;
using SIMS_WinFormsApp.Views.Dashboard;
using SIMS_WinFormsApp.Views.Reports;
using SIMS_WinFormsApp.Views.Sales.InvoiceManagement;
using SIMS_WinFormsApp.Views.Sales;
using SIMS_WinFormsApp.Views.UserManager;
using SIMS_WinFormsApp.Views.SystemManagement;
using SIMS_WinFormsApp.Views.Warehouse;
using SIMS_WinFormsApp.Views.Interfaces;
using SIMS_WinFormsApp.Infrastructure.Composition;
using SIMS_WinFormsApp.Services.Session;
using SIMS_WinFormsApp.Services.Backup;
using SIMS_WinFormsApp.Services.Implementations.Sales;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.Services.Chat;
using SIMS_WinFormsApp.UI.I18n;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Controls.Catalog;
using SIMS_WinFormsApp.UI.Controls.Pos;
using SIMS_WinFormsApp.Models.Enums;
using SIMS_WinFormsApp.Models.Permission;
using SIMS_WinFormsApp.Models.Chat;
using SIMS_WinFormsApp.Models.DTOs.Sales;
using SIMS_WinFormsApp.Views.Shell;

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
        private readonly SIMS_WinFormsApp.Repositories.Interfaces.IAuditLogWriter _auditLogWriter;
        private HashSet<AppPermission> _currentPermissions;
        private DailyBackupScheduler _dailyBackupScheduler;
        private ReturnManagementDataService _notificationService;
        private Timer _notificationTimer;
        private readonly HashSet<string> _seenNotificationIds = new HashSet<string>(StringComparer.Ordinal);
        private readonly HashSet<string> _readNotificationIds = new HashSet<string>(StringComparer.Ordinal);
        private readonly HashSet<string> _dismissedNotificationIds = new HashSet<string>(StringComparer.Ordinal);
        private readonly List<NotificationDto> _chatNotifications = new List<NotificationDto>();
        private IChatClientService _chatClient;
        private bool _notificationBaselineLoaded;
        private bool _notificationFailureShown;
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
            ("returns", "sidebar.page.returns"),
            ("products", "sidebar.page.products"),
            ("categories", "sidebar.page.categories"),
            ("suppliers", "sidebar.page.suppliers"),
            ("inventory", "sidebar.page.inventory"),
            ("purchase", "sidebar.page.purchase"),
            ("stock-alert", "sidebar.page.stockAlert"),
            ("expiry-batches", "sidebar.page.expiryBatches"),
            ("disposal", "sidebar.page.disposal"),
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
            _auditLogWriter = AppComposition.CreateAuditLogWriter();
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
                var permissions = await Task.Run(GetCurrentUserPermissions);
                _currentPermissions = permissions;
                _layout = BuildLayout(permissions);
                _layout.AiChatRequested += OnAiChatRequested;
                _view.AttachLayout(_layout);
                _view.SetWindowTitle(Lang.Get("main.window.title"));
                var name = _getDisplayName() ?? "Admin";
                var email = _getEmail() ?? string.Empty;
                var role = _getRole() ?? string.Empty;
                _view.SetUserInfo(name, email);
                _layout.SetUser(name, email, null, role, UserSession.Instance.CurrentUser?.AvatarUrl);
                StartNotificationPolling();
                StartChatNotifications();
                RefreshUnviewedInvoiceBadge(permissions);
                _layout.ShowPage(CanViewDashboard(permissions) ? "dashboard" : "profile");

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

        private void StartNotificationPolling()
        {
            var user = UserSession.Instance.CurrentUser;
            if (user == null)
                return;

            _notificationService = new ReturnManagementDataService(
                AppComposition.CreateConnectionFactory());
            _layout.NotificationReadRequested += OnNotificationReadRequested;
            _layout.NotificationDeleteRequested += OnNotificationDeleteRequested;
            _layout.NotificationsClearRequested += OnNotificationsClearRequested;
            _notificationTimer = new Timer { Interval = 10000 };
            _notificationTimer.Tick += (sender, args) => RefreshNotifications();
            RefreshNotifications();
            _notificationTimer.Start();
        }

        private void StartChatNotifications()
        {
            var user = UserSession.Instance.CurrentUser;
            if (user == null)
                return;

            ChatServerHost.Instance.Start();
            _chatClient = ChatClientService.Instance;
            _chatClient.MessageReceived += OnChatMessageReceived;
            if (!_chatClient.IsConnected)
                _chatClient.ConnectStaff(user.UserId,
                    user.FullName ?? user.Username, user.RoleCode ?? string.Empty);
        }

        private void RefreshNotifications()
        {
            var user = UserSession.Instance.CurrentUser;
            if (_layout == null || user == null || _notificationService == null)
                return;

            try
            {
                var notifications = _notificationService.GetCompletedReturns(user.UserId);
                if (_currentPermissions != null &&
                    _currentPermissions.Contains(AppPermission.RETURN_EXCHANGE_APPROVE))
                    notifications.AddRange(_notificationService.GetPendingReturnNotifications(user.UserId));
                notifications.RemoveAll(notification =>
                    _dismissedNotificationIds.Contains(notification.NotificationId));

                foreach (var notification in notifications)
                {
                    if (_seenNotificationIds.Add(notification.NotificationId))
                    {
                        if (_notificationBaselineLoaded)
                            _layout.NotifyNewNotification(notification.Title, notification.Message);
                        else if (notification.Kind != "RETURN_PENDING")
                            _readNotificationIds.Add(notification.NotificationId);
                    }
                    notification.IsRead = _readNotificationIds.Contains(notification.NotificationId);
                }

                _notificationBaselineLoaded = true;
                notifications.AddRange(_chatNotifications.Where(notification =>
                    !_dismissedNotificationIds.Contains(notification.NotificationId)));
                foreach (NotificationDto notification in notifications)
                    notification.IsRead = _readNotificationIds.Contains(notification.NotificationId);
                notifications = notifications
                    .OrderByDescending(notification => notification.CreatedAt)
                    .Take(20)
                    .ToList();
                _layout.SetNotifications(notifications,
                    notifications.Count(notification => !notification.IsRead));
                _notificationFailureShown = false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(
                    "[MainPresenter] Failed to load notifications: " + ex);
                if (!_notificationFailureShown)
                {
                    _notificationFailureShown = true;
                    _layout.ShowNotificationToast(
                        "Không thể tải thông báo", ex.Message);
                }
            }
        }

        private void OnChatMessageReceived(ChatMessageDto message)
        {
            var user = UserSession.Instance.CurrentUser;
            if (message == null || user == null || !message.IsStaffChat ||
                message.ToUserId != user.UserId || message.UserId == user.UserId ||
                !string.Equals(message.RoleCode, RoleCodes.SalesStaff, StringComparison.OrdinalIgnoreCase))
                return;

            string notificationId = "chat:" + (message.MessageId > 0
                ? message.MessageId.ToString(System.Globalization.CultureInfo.InvariantCulture)
                : message.UserId + ":" + message.Timestamp.ToString(
                    System.Globalization.CultureInfo.InvariantCulture));
            DateTime createdAt = DateTimeOffset.FromUnixTimeMilliseconds(message.Timestamp).LocalDateTime;
            var notification = new NotificationDto
            {
                NotificationId = notificationId,
                Kind = "CHAT_MESSAGE",
                Title = "Tin nhắn mới từ " + (message.UserName ?? "nhân viên bán hàng"),
                Message = message.Text ?? string.Empty,
                CreatedAt = createdAt,
                TargetPageKey = "chat",
                TargetUserId = message.UserId
            };

            if (_layout == null || _layout.IsDisposed || !_layout.IsHandleCreated)
                return;

            Action showNotification = () =>
            {
                if (_layout == null || _layout.IsDisposed)
                    return;
                if (!_seenNotificationIds.Add(notificationId))
                    return;
                _chatNotifications.Insert(0, notification);
                if (_chatNotifications.Count > 20)
                    _chatNotifications.RemoveRange(20, _chatNotifications.Count - 20);
                _layout.NotifyNewNotification(notification.Title, notification.Message);
                RefreshNotifications();
            };
            if (_layout.InvokeRequired)
                _layout.BeginInvoke(showNotification);
            else
                showNotification();
        }

        private void OnNotificationReadRequested(NotificationDto notification)
        {
            if (notification == null)
                return;

            _readNotificationIds.Add(notification.NotificationId);
            if (!string.IsNullOrWhiteSpace(notification.TargetPageKey) && _layout != null)
            {
                _layout.ShowPage(notification.TargetPageKey);
                if (notification.TargetPageKey == "chat" &&
                    notification.TargetUserId.HasValue &&
                    _layout.TryGetPage<ucChat>("chat", out ucChat chat))
                    chat.OpenConversation(notification.TargetUserId.Value);
            }
            RefreshNotifications();
        }

        private void OnNotificationDeleteRequested(string notificationId)
        {
            if (string.IsNullOrWhiteSpace(notificationId))
                return;

            _dismissedNotificationIds.Add(notificationId);
            _readNotificationIds.Add(notificationId);
            _chatNotifications.RemoveAll(notification =>
                string.Equals(notification.NotificationId, notificationId, StringComparison.Ordinal));
            RefreshNotifications();
        }

        private void OnNotificationsClearRequested(IReadOnlyList<string> notificationIds)
        {
            if (notificationIds != null)
            {
                foreach (string notificationId in notificationIds)
                {
                    if (string.IsNullOrWhiteSpace(notificationId))
                        continue;

                    _dismissedNotificationIds.Add(notificationId);
                    _readNotificationIds.Add(notificationId);
                }
            }

            _chatNotifications.RemoveAll(notification =>
                _dismissedNotificationIds.Contains(notification.NotificationId));
            RefreshNotifications();
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

        private MainLayoutControl BuildLayout(HashSet<AppPermission> permissions)
        {
            var layout = new MainLayoutControl(Lang.Get("main.header.subtitle"));
            ucDashboard dashboard = null;
            layout.AddLazyPage("profile", Lang.Get("header.dropdown.profile"),
                () => new SIMS_WinFormsApp.Views.Profile.ucMyProfile(), IconChar.UserCircle, showInSidebar: false);
            if (CanViewDashboard(permissions))
            {
                layout.AddSection(Lang.Get("sidebar.section.overview"));
                dashboard = new ucDashboard(
                    _getDisplayName(),
                    UserSession.Instance.CurrentUser?.RoleCode,
                    UserSession.Instance.CurrentUser?.UserId ?? 0,
                    AppComposition.CreateDashboardDataService());
                layout.AddPage("dashboard", Lang.Get("sidebar.page.dashboard"),
                    dashboard, IconChar.House);
            }

            if (HasAnyPermission(permissions,
                    AppPermission.USER_MANAGE, AppPermission.USER_EDIT, AppPermission.USER_VIEW,
                    AppPermission.CUSTOMER_MANAGE, AppPermission.CUSTOMER_EDIT, AppPermission.CUSTOMER_VIEW))
            {
                layout.AddSection(Lang.Get("sidebar.section.users"));
                if (HasAnyPermission(permissions, AppPermission.USER_MANAGE, AppPermission.USER_EDIT, AppPermission.USER_VIEW))
                {
                    layout.AddLazyPage("accounts", Lang.Get("sidebar.page.accounts"),
                        () => new frmUserManagement(_userManagementService), IconChar.UsersCog);
                    layout.AddLazyPage("employees", Lang.Get("sidebar.page.employees"),
                        () => new frmEmployeeManagement(_userManagementService), IconChar.User);
                }
                if (HasAnyPermission(permissions,
                        AppPermission.CUSTOMER_MANAGE, AppPermission.CUSTOMER_EDIT, AppPermission.CUSTOMER_VIEW))
                {
                    layout.AddLazyPage("customers", Lang.Get("sidebar.page.customers"),
                        () => new frmCustomerManagement(_userManagementService), IconChar.AddressBook);
                }
            }

            if (HasAnyPermission(permissions,
                    AppPermission.INVOICE_CREATE, AppPermission.INVOICE_VIEW_OWN,
                    AppPermission.INVOICE_VIEW_ALL, AppPermission.INVOICE_CANCEL,
                    AppPermission.INVOICE_CANCEL_REQUEST, AppPermission.RETURN_EXCHANGE_CREATE,
                     AppPermission.RETURN_EXCHANGE_APPROVE, AppPermission.PROMOTION_MANAGE))
            {
                layout.AddSection(Lang.Get("sidebar.section.sales"));
                if (HasAnyPermission(permissions, AppPermission.INVOICE_CREATE))
                {
                    layout.AddLazyPage("pos", Lang.Get("sidebar.page.pos"),
                        () => PosPage.Create(
                            _getDisplayName(),
                            () => RefreshUnviewedInvoiceBadge(permissions),
                            invoiceCode => MarkInvoiceViewed(null, invoiceCode, permissions)),
                        IconChar.CartShopping);
                }
                if (HasAnyPermission(permissions, AppPermission.PROMOTION_MANAGE))
                    layout.AddLazyPage("promotions", "Mã giảm giá",
                        () => new PromotionManagementPage(AppComposition.CreatePromotionManagementService()), IconChar.Tags);
                if (HasAnyPermission(permissions,
                        AppPermission.INVOICE_VIEW_OWN, AppPermission.INVOICE_VIEW_ALL,
                        AppPermission.INVOICE_CANCEL, AppPermission.INVOICE_CANCEL_REQUEST,
                        AppPermission.RETURN_EXCHANGE_CREATE))
                {
                    layout.AddLazyPage("orders", Lang.Get("sidebar.page.orders"),
                        () => PrepareEmbeddedForm(new InvoiceManagementPage(
                            AppComposition.CreateConnectionFactory(),
                            UserSession.Instance.CurrentUser?.UserId ?? 0,
                            CanViewAllInvoices(permissions),
                            permissions.Contains(AppPermission.RETURN_EXCHANGE_CREATE),
                            permissions.Contains(AppPermission.RETURN_EXCHANGE_APPROVE),
                            invoiceId => MarkInvoiceViewed(invoiceId, null, permissions))), IconChar.Receipt);
                }
                if (HasAnyPermission(permissions,
                        AppPermission.RETURN_EXCHANGE_CREATE, AppPermission.RETURN_EXCHANGE_APPROVE))
                {
                    layout.AddLazyPage("returns", Lang.Get("sidebar.page.returns"),
                        () => new ucReturnManagement(
                            AppComposition.CreateConnectionFactory(),
                            UserSession.Instance.CurrentUser?.UserId ?? 0,
                            permissions.Contains(AppPermission.RETURN_EXCHANGE_APPROVE)), IconChar.RotateLeft);
                }
            }

            if (HasAnyPermission(permissions,
                    AppPermission.PRODUCT_VIEW, AppPermission.PRODUCT_EDIT, AppPermission.PRODUCT_MANAGE,
                    AppPermission.CATEGORY_VIEW, AppPermission.CATEGORY_EDIT, AppPermission.CATEGORY_MANAGE,
                    AppPermission.SUPPLIER_VIEW, AppPermission.SUPPLIER_EDIT, AppPermission.SUPPLIER_MANAGE,
                    AppPermission.STOCK_VIEW, AppPermission.STOCK_IMPORT, AppPermission.STOCK_RECONCILE,
                    AppPermission.STOCK_DISPOSE, AppPermission.STOCK_DISPOSE_VIEW,
                    AppPermission.STOCK_ALERT_REPORT, AppPermission.STOCK_ALERT_VIEW,
                    AppPermission.SUPPLIER_RETURN_CREATE, AppPermission.SUPPLIER_RETURN_VIEW))
            {
                layout.AddSection(Lang.Get("sidebar.section.warehouse"));
                if (HasAnyPermission(permissions,
                        AppPermission.PRODUCT_VIEW, AppPermission.PRODUCT_EDIT, AppPermission.PRODUCT_MANAGE))
                    layout.AddLazyPage("products", Lang.Get("sidebar.page.products"),
                        () => CatalogPages.Products(
                            permissions.Contains(AppPermission.PRODUCT_EDIT) ||
                                permissions.Contains(AppPermission.PRODUCT_MANAGE),
                            permissions.Contains(AppPermission.PRODUCT_MANAGE),
                            HasAnyPermission(permissions, AppPermission.STOCK_VIEW,
                                AppPermission.STOCK_DISPOSE, AppPermission.STOCK_DISPOSE_VIEW)
                                ? (Action<int>)(productId =>
                                {
                                    layout.ShowPage("expiry-batches");
                                    if (layout.TryGetPage<ucExpiryBatchManagement>(
                                        "expiry-batches", out ucExpiryBatchManagement expiryPage))
                                        expiryPage.FilterByProductId(productId);
                                })
                                : null), IconChar.Box);
                if (HasAnyPermission(permissions,
                        AppPermission.CATEGORY_VIEW, AppPermission.CATEGORY_EDIT, AppPermission.CATEGORY_MANAGE))
                    layout.AddLazyPage("categories", Lang.Get("sidebar.page.categories"),
                        CatalogPages.Categories, IconChar.Tags);
                if (HasAnyPermission(permissions,
                        AppPermission.SUPPLIER_VIEW, AppPermission.SUPPLIER_EDIT, AppPermission.SUPPLIER_MANAGE,
                        AppPermission.SUPPLIER_RETURN_CREATE, AppPermission.SUPPLIER_RETURN_VIEW))
                    layout.AddLazyPage("suppliers", Lang.Get("sidebar.page.suppliers"),
                        CatalogPages.Suppliers, IconChar.Building);
                if (HasAnyPermission(permissions,
                        AppPermission.STOCK_VIEW, AppPermission.STOCK_IMPORT, AppPermission.STOCK_RECONCILE,
                        AppPermission.STOCK_DISPOSE, AppPermission.STOCK_DISPOSE_VIEW))
                    layout.AddLazyPage("inventory", Lang.Get("sidebar.page.inventory"),
                        () => new ucStockReconciliation(
                            AppComposition.CreateStockReconciliationService(),
                            UserSession.Instance),
                        IconChar.ScaleBalanced);
                if (HasAnyPermission(permissions, AppPermission.STOCK_IMPORT))
                    layout.AddLazyPage("purchase", Lang.Get("sidebar.page.purchase"),
                        () => new ucPurchaseReceiptManagement(), IconChar.Truck);
                if (HasAnyPermission(permissions, AppPermission.STOCK_ALERT_REPORT, AppPermission.STOCK_ALERT_VIEW))
                    layout.AddLazyPage("stock-alert", Lang.Get("sidebar.page.stockAlert"),
                        () => new ucStockAlert(
                            AppComposition.CreateInventoryService(),
                            UserSession.Instance,
                            () => _view.SetUnreadNotifications(0)),
                        IconChar.TriangleExclamation);
                if (HasAnyPermission(permissions,
                        AppPermission.STOCK_VIEW, AppPermission.STOCK_DISPOSE, AppPermission.STOCK_DISPOSE_VIEW))
                {
                    if (dashboard != null)
                        dashboard.ExpiryBatchesRequested += (sender, args) => layout.ShowPage("expiry-batches");
                    layout.AddLazyPage("expiry-batches", Lang.Get("sidebar.page.expiryBatches"),
                        () =>
                        {
                            var expiryPage = new ucExpiryBatchManagement(
                                HasAnyPermission(permissions, AppPermission.STOCK_DISPOSE));
                            if (permissions.Contains(AppPermission.STOCK_DISPOSE))
                            {
                                expiryPage.DisposeBatchRequested += batchId =>
                                {
                                    layout.ShowPage("expired-disposal");
                                    if (layout.TryGetPage<ucExpiredBatchDisposal>(
                                        "expired-disposal", out ucExpiredBatchDisposal disposalPage))
                                        _ = disposalPage.LoadBatchAsync(batchId);
                                };
                            }
                            return expiryPage;
                        },
                        IconChar.HourglassHalf);
                }
                if (permissions.Contains(AppPermission.STOCK_DISPOSE))
                {
                    layout.AddLazyPage("expired-disposal", "Lập phiếu tiêu hủy",
                        () =>
                        {
                            var disposalPage = new ucExpiredBatchDisposal(
                                AppComposition.CreateExpiryBatchManagementService());
                            disposalPage.ReturnRequested += (sender, args) => layout.ShowPage("expiry-batches");
                            disposalPage.DisposalCompleted += (sender, args) =>
                                layout.ShowPage("expiry-batches");
                            return disposalPage;
                        },
                        IconChar.TrashCan,
                        showInSidebar: false);
                }
                if (HasAnyPermission(permissions, AppPermission.STOCK_DISPOSE, AppPermission.STOCK_DISPOSE_VIEW))
                    layout.AddLazyPage("disposal", Lang.Get("sidebar.page.disposal"),
                        () => new ucStockDisposalManagement(
                            AppComposition.CreateStockDisposalService(),
                            UserSession.Instance,
                            permissions.Contains(AppPermission.STOCK_DISPOSE)),
                        IconChar.TrashCan);
            }

            if (HasAnyPermission(permissions,
                    AppPermission.REVENUE_REPORT_VIEW, AppPermission.PROFIT_REPORT_VIEW,
                    AppPermission.STOCK_REPORT_VIEW, AppPermission.STOCK_DISPOSE_VIEW))
            {
                layout.AddSection(Lang.Get("sidebar.section.reports"));
                if (HasAnyPermission(permissions, AppPermission.REVENUE_REPORT_VIEW, AppPermission.PROFIT_REPORT_VIEW))
                    layout.AddLazyPage("report-revenue", Lang.Get("sidebar.page.reportRevenue"),
                        () => PrepareEmbeddedForm(new frmChartDashboard()), IconChar.ChartColumn);
                if (HasAnyPermission(permissions, AppPermission.STOCK_REPORT_VIEW, AppPermission.STOCK_DISPOSE_VIEW))
                    layout.AddLazyPage("report-inventory", Lang.Get("sidebar.page.reportInventory"),
                        () => new ucInventoryReport(), IconChar.ChartLine);
            }

            layout.AddSection(Lang.Get("sidebar.section.support"));
            layout.AddLazyPage("chat", Lang.Get("sidebar.page.chat"),
                () => new ucChat(), IconChar.Comments);
            if (!IsAdmin() && HasAnyPermission(permissions,
                    AppPermission.SHIFT_OPERATE, AppPermission.SHIFT_VIEW_ALL, AppPermission.SHIFT_APPROVE))
            {
                layout.AddLazyPage("shifts", Lang.Get("sidebar.page.shifts"),
                    () => new ucShiftManagement(), IconChar.Stopwatch);
            }

            if (HasAnyPermission(permissions,
                    AppPermission.SETTINGS_MANAGE, AppPermission.BACKUP_MANAGE,
                    AppPermission.AUDIT_LOG_VIEW, AppPermission.RBAC_MANAGE))
            {
                layout.AddSection(Lang.Get("sidebar.section.system"));
                if (HasAnyPermission(permissions, AppPermission.SETTINGS_MANAGE))
                    layout.AddLazyPage("settings", Lang.Get("sidebar.page.settings"),
                        () => new ucSystemSettings(), IconChar.Gear);
                if (HasAnyPermission(permissions, AppPermission.BACKUP_MANAGE))
                    layout.AddLazyPage("backup", Lang.Get("sidebar.page.backup"),
                        SIMS_WinFormsApp.Views.Backup.BackupPage.Create, IconChar.ShieldHalved);
                if (HasAnyPermission(permissions, AppPermission.AUDIT_LOG_VIEW))
                    layout.AddLazyPage("audit-log", "Nhật ký hệ thống",
                        () => new ucAuditLog(), IconChar.ClockRotateLeft);
                if (HasAnyPermission(permissions, AppPermission.RBAC_MANAGE))
                    layout.AddLazyPage("role-permissions", "Phân quyền vai trò",
                        () => new ucRolePermission(), IconChar.UserShield);
            }

            return layout;
        }

        private static bool CanViewDashboard(HashSet<AppPermission> permissions)
        {
            var user = UserSession.Instance.CurrentUser;
            return HasAnyPermission(permissions, AppPermission.DASHBOARD_VIEW)
                || (user != null && string.Equals(
                    user.RoleCode,
                    RoleCodes.SalesStaff,
                    StringComparison.OrdinalIgnoreCase));
        }

        private static HashSet<AppPermission> GetCurrentUserPermissions()
        {
            var user = UserSession.Instance.CurrentUser;
            if (user == null) return new HashSet<AppPermission>();
            if (IsAdmin())
                return new HashSet<AppPermission>(Enum.GetValues(typeof(AppPermission)).Cast<AppPermission>());

            var repository = AppComposition.CreateRolePermissionRepository();
            return repository.GetPermissionsForRole(user.RoleId);
        }

        private static bool HasAnyPermission(HashSet<AppPermission> permissions, params AppPermission[] required)
        {
            return permissions != null && required.Any(permissions.Contains);
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
            if (string.Equals(pageKey, "orders", StringComparison.OrdinalIgnoreCase))
                RefreshUnviewedInvoiceBadge(_currentPermissions);
        }

        private void RefreshUnviewedInvoiceBadge(HashSet<AppPermission> permissions)
        {
            if (_layout == null)
                return;

            try
            {
                int currentUserId = UserSession.Instance.CurrentUser?.UserId ?? 0;
                _layout.SetBadge("orders", _auditLogWriter.CountUnviewedInvoices(
                    currentUserId, CanViewAllInvoices(permissions), DateTime.Today));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("[MainPresenter] Failed to load unseen invoice count: " + ex);
                _view.ShowMessage(
                    "Không thể cập nhật số hóa đơn chưa xem.\r\n" + ex.Message,
                    "Lỗi tải thông tin đơn hàng",
                    MessageBoxIcon.Warning);
            }
        }

        private void MarkInvoiceViewed(int? invoiceId, string invoiceCode, HashSet<AppPermission> permissions)
        {
            try
            {
                _auditLogWriter.RecordInvoiceViewed(
                    UserSession.Instance.CurrentUser?.UserId ?? 0, invoiceId, invoiceCode);

                RefreshUnviewedInvoiceBadge(permissions);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("[MainPresenter] Failed to record invoice view: " + ex);
                _view.ShowMessage(
                    "Không thể cập nhật trạng thái đã xem của hóa đơn.\r\n" + ex.Message,
                    "Lỗi cập nhật đơn hàng",
                    MessageBoxIcon.Warning);
            }
        }

        private static bool CanViewAllInvoices(HashSet<AppPermission> permissions)
        {
            return permissions.Contains(AppPermission.INVOICE_VIEW_ALL) ||
                   permissions.Contains(AppPermission.INVOICE_CANCEL);
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
            ShowPopupAbove(popup, _layout.ChatbotButton, 12);
        }

        private static void ShowPopupAbove(Form popup, Control anchor, int offsetY)
        {
            if (popup == null || anchor == null || anchor.IsDisposed) return;

            Rectangle working = Screen.FromControl(anchor).WorkingArea;
            Point anchorTopLeft = anchor.PointToScreen(Point.Empty);
            int x = anchorTopLeft.X + anchor.Width - popup.Width;
            x = Math.Max(working.Left, Math.Min(x, working.Right - popup.Width));

            int y = anchorTopLeft.Y - popup.Height - offsetY;
            if (y < working.Top)
            {
                int below = anchorTopLeft.Y + anchor.Height + offsetY;
                y = below + popup.Height <= working.Bottom
                    ? below
                    : Math.Max(working.Top, working.Bottom - popup.Height);
            }

            popup.Location = new Point(x, y);
            popup.Show(anchor.FindForm());
            popup.Activate();
        }

        public void Dispose()
        {
            if (_layout != null)
            {
                _layout.AiChatRequested -= OnAiChatRequested;
                _layout.NotificationReadRequested -= OnNotificationReadRequested;
                _layout.NotificationDeleteRequested -= OnNotificationDeleteRequested;
                _layout.NotificationsClearRequested -= OnNotificationsClearRequested;
                if (_aiChatPopup != null && !_aiChatPopup.IsDisposed)
                    _aiChatPopup.Close();
                _aiChatPopup = null;
            }
            if (_chatClient != null)
            {
                _chatClient.MessageReceived -= OnChatMessageReceived;
                _chatClient.Disconnect();
                _chatClient = null;
            }
            if (_notificationTimer != null)
            {
                _notificationTimer.Stop();
                _notificationTimer.Dispose();
                _notificationTimer = null;
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