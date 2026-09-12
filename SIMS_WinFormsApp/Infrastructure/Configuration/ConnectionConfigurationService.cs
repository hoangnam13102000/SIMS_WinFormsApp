using System;
using System.Configuration;
using System.Data.SqlClient;

namespace SIMS_WinFormsApp.Infrastructure.Configuration
{
    public sealed class ConnectionConfigurationService : IConnectionConfigurationService
    {
        public string GetCurrentConnectionString()
        {
            var setting = ConfigurationManager.ConnectionStrings[ConnectionStringProvider.ConnectionStringName];
            return setting == null ? null : setting.ConnectionString;
        }

        public string BuildConnectionString(
            string server,
            string database,
            bool integratedSecurity,
            string userId,
            string password)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = server == null ? string.Empty : server.Trim(),
                InitialCatalog = database == null ? string.Empty : database.Trim(),
                TrustServerCertificate = true,
                ConnectTimeout = 15,
                IntegratedSecurity = integratedSecurity
            };
            if (!integratedSecurity)
            {
                builder.UserID = userId == null ? string.Empty : userId.Trim();
                builder.Password = password ?? string.Empty;
            }
            return builder.ConnectionString;
        }

        public bool TestConnection(string connectionString, out string error)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                    connection.Open();
                error = null;
                return true;
            }
            catch (Exception exception)
            {
                error = exception.Message;
                return false;
            }
        }

        public void SaveConnectionString(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("ConnectionString không được để trống.", nameof(connectionString));

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var section = config.ConnectionStrings;
            var setting = section.ConnectionStrings[ConnectionStringProvider.ConnectionStringName];
            if (setting == null)
                section.ConnectionStrings.Add(new ConnectionStringSettings(
                    ConnectionStringProvider.ConnectionStringName,
                    connectionString, "System.Data.SqlClient"));
            else
                setting.ConnectionString = connectionString;

            if (!section.SectionInformation.IsProtected)
                section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
            section.SectionInformation.ForceSave = true;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("connectionStrings");
        }
    }
}
