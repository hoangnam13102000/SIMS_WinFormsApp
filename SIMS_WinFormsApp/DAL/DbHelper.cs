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
                var setting = ConfigurationManager.ConnectionStrings[ConnectionStringName];
                if (setting == null || string.IsNullOrWhiteSpace(setting.ConnectionString))
                {
                    throw new InvalidOperationException(
                        $"Chưa cấu hình ConnectionString \"{ConnectionStringName}\" trong App.config. " +
                        "Vào Forms/SystemMgmt/frmConnectionConfig để cấu hình kết nối SQL Server.");
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
    }
}