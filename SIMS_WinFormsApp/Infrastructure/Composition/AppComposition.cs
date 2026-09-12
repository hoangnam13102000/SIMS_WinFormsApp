using SIMS_WinFormsApp.DAL;
using SIMS_WinFormsApp.Infrastructure.Configuration;
using SIMS_WinFormsApp.Repositories.Implementations;
using SIMS_WinFormsApp.Repositories.Interfaces;
using SIMS_WinFormsApp.Services;
using SIMS_WinFormsApp.Services.Implementations;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.Services.Security;
using SIMS_WinFormsApp.Services.Session;
using SIMS_WinFormsApp.Forms.Auth;

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

        public static IChatRepository CreateChatRepository()
        {
            return new ChatRepository();
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
            return new UserManagementService(CreateUserRepository());
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
    }
}
