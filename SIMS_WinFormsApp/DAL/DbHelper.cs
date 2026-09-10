using System;
using System.Configuration;
using System.Data.SqlClient;

namespace SIMS_WinFormsApp.DAL
{
    public static class DbHelper
    {
        public const string ConnectionStringName = "SIMS_DB";

        public static string ConnectionString
        {
            get
            {
                EnsureConnectionStringsLoaded();

                var setting = ConfigurationManager.ConnectionStrings[ConnectionStringName];
                if (setting == null || string.IsNullOrWhiteSpace(setting.ConnectionString))
                {
                    throw new InvalidOperationException(
                        $"Chưa cấu hình ConnectionString \"{ConnectionStringName}\" trong App.config. " +
                        "Vào System → Cấu hình kết nối SQL để thiết lập.");
                }
                return setting.ConnectionString;
            }
        }

        public static SqlConnection CreateConnection() => new SqlConnection(ConnectionString);

        public static bool TestConnection(string connectionString, out string errorMessage)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                }
                errorMessage = null;
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public static string BuildConnectionString(
            string server,
            string database,
            bool useWindowsAuth,
            string userId = null,
            string password = null)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = server?.Trim() ?? "",
                InitialCatalog = database?.Trim() ?? "",
                TrustServerCertificate = true,
                ConnectTimeout = 15
            };

            if (useWindowsAuth)
            {
                builder.IntegratedSecurity = true;
            }
            else
            {
                builder.IntegratedSecurity = false;
                builder.UserID = userId?.Trim() ?? "";
                builder.Password = password ?? "";
            }

            return builder.ConnectionString;
        }

    
        public static void SaveConnectionString(string connectionString, bool encrypt = true)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("ConnectionString không được để trống.", nameof(connectionString));

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            var section = config.GetSection("connectionStrings") as ConnectionStringsSection;
            if (section != null && section.SectionInformation.IsProtected)
            {
                section.SectionInformation.UnprotectSection();
            }

            var cs = config.ConnectionStrings.ConnectionStrings[ConnectionStringName];
            if (cs != null)
            {
                cs.ConnectionString = connectionString;
            }
            else
            {
                config.ConnectionStrings.ConnectionStrings.Add(
                    new ConnectionStringSettings(ConnectionStringName, connectionString, "System.Data.SqlClient"));
            }

            if (encrypt)
            {
                EncryptConnectionStringsSection(config);
            }

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("connectionStrings");
        }

        public static void EncryptConnectionStringsSection(Configuration config = null)
        {
            if (config == null)
                config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            var section = config.GetSection("connectionStrings") as ConnectionStringsSection;
            if (section == null) return;

            if (!section.SectionInformation.IsProtected)
            {
                section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
                section.SectionInformation.ForceSave = true;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("connectionStrings");
            }
        }

        public static void DecryptConnectionStringsSection()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var section = config.GetSection("connectionStrings") as ConnectionStringsSection;
            if (section == null) return;

            if (section.SectionInformation.IsProtected)
            {
                section.SectionInformation.UnprotectSection();
                section.SectionInformation.ForceSave = true;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("connectionStrings");
            }
        }

        private static void EnsureConnectionStringsLoaded()
        {
            try
            {
                var _ = ConfigurationManager.ConnectionStrings;
            }
            catch
            {
                // Bỏ qua — sẽ throw ở chỗ lấy ConnectionString
            }
        }
    }
}