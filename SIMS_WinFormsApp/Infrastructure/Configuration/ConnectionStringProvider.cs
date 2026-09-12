using System;
using System.Configuration;

namespace SIMS_WinFormsApp.Infrastructure.Configuration
{
    public sealed class ConnectionStringProvider : IConnectionStringProvider
    {
        public const string ConnectionStringName = "SIMS_DB";

        public string GetConnectionString()
        {
            var setting = ConfigurationManager.ConnectionStrings[ConnectionStringName];
            if (setting == null || string.IsNullOrWhiteSpace(setting.ConnectionString))
                throw new InvalidOperationException(
                    "Chưa cấu hình ConnectionString \"SIMS_DB\" trong App.config.");
            return setting.ConnectionString;
        }
    }
}
