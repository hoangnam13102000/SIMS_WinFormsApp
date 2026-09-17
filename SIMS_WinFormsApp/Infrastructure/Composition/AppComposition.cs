using SIMS_WinFormsApp.DAL;
using SIMS_WinFormsApp.Infrastructure.Configuration;
using SIMS_WinFormsApp.Repositories.Implementations;
using SIMS_WinFormsApp.Repositories.Interfaces;
using SIMS_WinFormsApp.Services;
using SIMS_WinFormsApp.Services.Implementations;
using SIMS_WinFormsApp.Services.Implementations.Pos;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.Services.Interfaces.Pos;
using SIMS_WinFormsApp.Services.Mail;
using SIMS_WinFormsApp.Services.Security;
using SIMS_WinFormsApp.Services.Session;
using SIMS_WinFormsApp.Forms.Auth;
using SIMS_WinFormsApp.UI.Controls.Barcode;
using SIMS_WinFormsApp.UI.Controls.Toast;

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

        public static IChatRepository CreateChatRepository()
        {
            return new ChatRepository();
        }

        // ---- Bổ sung cho trang "Phân quyền vai trò" (RolePermissionPresenter) ----
        // Chỉ THÊM factory mới, không đổi bất kỳ factory nào ở trên.
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

        // ---- Bổ sung các factory còn thiếu cho tính năng POS (UI/Controls/Pos/PosPage.cs) ----
        // Các interface/implementation này đã có sẵn trong project (Services/Interfaces/Pos,
        // Services/Implementations/Pos, UI/Controls/Barcode) nhưng chưa được nối vào
        // AppComposition - đây là lỗi build có sẵn, không liên quan tới trang "Phân quyền vai
        // trò". Chỉ THÊM các method dưới đây, không đổi bất kỳ method nào ở trên.
        public static IDbConnectionFactory CreateConnectionFactory()
        {
            return new DbConnectionFactory(new ConnectionStringProvider());
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
    }
}