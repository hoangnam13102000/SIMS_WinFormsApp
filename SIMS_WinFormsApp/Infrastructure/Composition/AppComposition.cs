using SIMS_WinFormsApp.DAL;
using SIMS_WinFormsApp.Forms.Auth;
using SIMS_WinFormsApp.Infrastructure.Configuration;
using SIMS_WinFormsApp.Repositories.Implementations;
using SIMS_WinFormsApp.Repositories.Interfaces;
using SIMS_WinFormsApp.Services;
using SIMS_WinFormsApp.Services.Backup;
using SIMS_WinFormsApp.Services.Implementations;
using SIMS_WinFormsApp.Services.Implementations.Pos;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.Services.Interfaces.Pos;
using SIMS_WinFormsApp.Services.Mail;
using SIMS_WinFormsApp.Services.Security;
using SIMS_WinFormsApp.Services.Session;
using SIMS_WinFormsApp.UI.Controls.Barcode;
using SIMS_WinFormsApp.UI.Controls.Toast;
using System;
using System.Configuration;
using System.IO;

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

        public static IChatRepository CreateChatRepository()
        {
            return new ChatRepository();
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

        public static BackupManager CreateBackupManager()
        {
            string directory = ConfigurationManager.AppSettings["BackupDirectory"];
            if (string.IsNullOrWhiteSpace(directory))
                directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Backups");

            var storage = new BackupStorage(directory);
            var strategies = new IBackupStrategy[]
            {
                new SqlServerNativeBackupStrategy(CreateConnectionFactory())
            };
            return new BackupManager(storage, strategies);
        }

        public static CloudinaryBackupUploadListener CreateCloudinaryBackupUploadListener()
        {
            return CloudinaryConfig.IsConfigured
                ? new CloudinaryBackupUploadListener(new CloudinaryUploader())
                : null;
        }

        public static IProductCatalogService CreateProductCatalogService()
        {
            return new SqlProductCatalogService(CreateConnectionFactory());
        }

        public static ICustomerLookupService CreateCustomerLookupService()
        {
            return new DemoCustomerLookupService();
        }

        public static IPromotionService CreatePromotionService()
        {
            return new DemoPromotionService();
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