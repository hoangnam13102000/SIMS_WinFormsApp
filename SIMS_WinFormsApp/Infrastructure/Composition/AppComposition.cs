using SIMS_WinFormsApp.DAL;
using SIMS_WinFormsApp.Views.Auth;
using SIMS_WinFormsApp.Views.Dialogs;
using SIMS_WinFormsApp.Infrastructure.Configuration;
using SIMS_WinFormsApp.Repositories.Implementations;
using SIMS_WinFormsApp.Repositories.Interfaces;
using SIMS_WinFormsApp.Services;
using SIMS_WinFormsApp.Services.Implementations;
using SIMS_WinFormsApp.Services.Implementations.Catalog;
using SIMS_WinFormsApp.Services.Implementations.Inventory;
using SIMS_WinFormsApp.Services.Implementations.Pos;
using SIMS_WinFormsApp.Services.Implementations.Reports;
using SIMS_WinFormsApp.Services.Implementations.Sales;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.Services.Interfaces.Pos;
using SIMS_WinFormsApp.Services.Mail;
using SIMS_WinFormsApp.Services.Security;
using SIMS_WinFormsApp.Services.Session;
using SIMS_WinFormsApp.UI.Controls.Barcode;
using SIMS_WinFormsApp.Views.Toast;
using System;

namespace SIMS_WinFormsApp.Infrastructure.Composition
{
    public static class AppComposition
    {
        public static frmLogin CreateLoginForm()
        {
            return new frmLogin(CreateAuthService());
        }

        public static IUserRepository CreateUserRepository()
        {
            return new UserRepository();
        }

        public static IRoleRepository CreateRoleRepository()
        {
            return new RoleRepository();
        }

        public static IPermissionRepository CreatePermissionRepository()
        {
            return new PermissionRepository();
        }

        public static IRolePermissionRepository CreateRolePermissionRepository()
        {
            return new RolePermissionRepository();
        }

        public static IAuthService CreateAuthService()
        {
            return new AuthService(
                CreateUserRepository(),
                UserSession.Instance,
                new BCryptPasswordHasher());
        }

        public static IUserManagementService CreateUserManagementService()
        {
            return new UserManagementService(
                CreateUserRepository(),
                CreateRoleRepository(),
                new BCryptPasswordHasher(),
                new MailSender());
        }

        public static IConnectionConfigurationService CreateConnectionConfigurationService()
        {
            return new ConnectionConfigurationService();
        }

        public static IPasswordResetService CreatePasswordResetService()
        {
            return PasswordResetService.Instance;
        }

        public static IDialogService CreateDialogService()
        {
            return new DialogService();
        }

        public static IToastNotifier CreateToastNotifier()
        {
            return new ToastService();
        }

        public static IDbConnectionFactory CreateConnectionFactory()
        {
            return new DbConnectionFactory(new ConnectionStringProvider());
        }

        public static IProductCatalogService CreateProductCatalogService()
        {
            return new SqlProductCatalogService(CreateConnectionFactory(), new LocalProductImageStore());
        }

        public static RevenueReportService CreateRevenueReportService()
        {
            return new RevenueReportService(CreateConnectionFactory());
        }

        public static InventoryReportService CreateInventoryReportService()
        {
            return new InventoryReportService(CreateConnectionFactory());
        }

        public static ExpiryBatchManagementService CreateExpiryBatchManagementService()
        {
            return new ExpiryBatchManagementService(CreateConnectionFactory());
        }

        public static IDashboardDataService CreateDashboardDataService()
        {
            return new DashboardDataService(CreateConnectionFactory());
        }

        public static ICatalogAdminService CreateCatalogAdminService()
        {
            return new CatalogAdminService(
                new CatalogRepository(CreateConnectionFactory()),
                new LocalProductImageStore());
        }

        public static IInventoryService CreateInventoryService()

        {
            return new InventoryService(new InventoryRepository(CreateConnectionFactory()));
        }

        public static IStockReconciliationService CreateStockReconciliationService()
        {
            return new StockReconciliationService(new StockReconciliationRepository(CreateConnectionFactory()));
        }
        public static IStockDisposalService CreateStockDisposalService()
        {
            return new StockDisposalService(new StockDisposalRepository(CreateConnectionFactory()));
        }

        public static ICustomerLookupService CreateCustomerLookupService()
        {
            return new SqlCustomerLookupService(CreateConnectionFactory());
        }

        public static IInvoiceService CreateInvoiceService()
        {
            return new SqlInvoiceService(CreateConnectionFactory());
        }

        internal static IInvoiceManagementDataService CreateInvoiceManagementDataService()
        {
            return new InvoiceManagementDataService(CreateConnectionFactory());
        }

        internal static IReturnManagementDataService CreateReturnManagementDataService()
        {
            return new ReturnManagementDataService(CreateConnectionFactory());
        }

        public static IBarcodeScannerLauncher CreateBarcodeScannerLauncher()
        {
            return new BarcodeScannerLauncher();
        }

        public static IAuditLogRepository CreateAuditLogRepository()
        {
            return new AuditLogRepository();
        }

        public static IStoreConfigRepository CreateStoreConfigRepository()
        {
            return new StoreConfigRepository();
        }

        public static IAuditLogWriter CreateAuditLogWriter()
        {
            return new AuditLogRepository();
        }
    }
}