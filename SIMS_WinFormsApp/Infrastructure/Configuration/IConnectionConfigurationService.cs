namespace SIMS_WinFormsApp.Infrastructure.Configuration
{
    public interface IConnectionConfigurationService
    {
        string GetCurrentConnectionString();
        string BuildConnectionString(
            string server,
            string database,
            bool integratedSecurity,
            string userId,
            string password);
        bool TestConnection(string connectionString, out string error);
        void SaveConnectionString(string connectionString);
    }
}
