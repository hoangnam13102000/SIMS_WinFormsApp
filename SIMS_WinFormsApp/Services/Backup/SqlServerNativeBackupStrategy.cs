using System;
using System.Data.SqlClient;
using SIMS_WinFormsApp.Infrastructure.Configuration;

namespace SIMS_WinFormsApp.Services.Backup
{
    public sealed class SqlServerNativeBackupStrategy : IBackupStrategy, IRestoreStrategy
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public SqlServerNativeBackupStrategy(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public string Name => "sqlservernative";
        public string FileExtension => "bak";

        public void BackupTo(string destinationFilePath)
        {
            string databaseName = GetDatabaseName();

            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                try
                {
                    ExecuteBackup(connection, databaseName, destinationFilePath, withCompression: true);
                }
                catch (SqlException ex) when (IsCompressionUnsupported(ex))
                {
                    // Một số bản SQL Server Express không có tùy chọn nén -> thử lại không nén.
                    ExecuteBackup(connection, databaseName, destinationFilePath, withCompression: false);
                }
            }
        }

        public void RestoreFrom(string backupFilePath)
        {
            string databaseName = GetDatabaseName();

            using (var connection = new SqlConnection(BuildMasterConnectionString()))
            {
                connection.Open();
                ExecuteNonQuery(connection,
                    "ALTER DATABASE [" + EscapeIdentifier(databaseName) + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;", 60);
                try
                {
                    using (var command = new SqlCommand(
                        "RESTORE DATABASE [" + EscapeIdentifier(databaseName) + "] FROM DISK = @path WITH REPLACE, STATS = 10",
                        connection))
                    {
                        command.CommandTimeout = 600;
                        command.Parameters.AddWithValue("@path", backupFilePath);
                        command.ExecuteNonQuery();
                    }
                }
                finally
                {
                    ExecuteNonQuery(connection,
                        "ALTER DATABASE [" + EscapeIdentifier(databaseName) + "] SET MULTI_USER;", 60);
                }
            }
        }

        private static void ExecuteBackup(SqlConnection connection, string databaseName, string destinationFilePath, bool withCompression)
        {
            string options = withCompression ? "WITH INIT, COMPRESSION, STATS = 10" : "WITH INIT, STATS = 10";
            using (var command = new SqlCommand(
                "BACKUP DATABASE [" + EscapeIdentifier(databaseName) + "] TO DISK = @path " + options, connection))
            {
                command.CommandTimeout = 600;
                command.Parameters.AddWithValue("@path", destinationFilePath);
                command.ExecuteNonQuery();
            }
        }

        private string GetDatabaseName()
        {
            using (var probe = _connectionFactory.CreateConnection())
                return new SqlConnectionStringBuilder(probe.ConnectionString).InitialCatalog;
        }

        private string BuildMasterConnectionString()
        {
            using (var probe = _connectionFactory.CreateConnection())
            {
                var builder = new SqlConnectionStringBuilder(probe.ConnectionString) { InitialCatalog = "master" };
                return builder.ConnectionString;
            }
        }

        private static void ExecuteNonQuery(SqlConnection connection, string sql, int timeoutSeconds)
        {
            using (var command = new SqlCommand(sql, connection))
            {
                command.CommandTimeout = timeoutSeconds;
                command.ExecuteNonQuery();
            }
        }

        private static bool IsCompressionUnsupported(SqlException ex) =>
            ex.Message.IndexOf("COMPRESSION", StringComparison.OrdinalIgnoreCase) >= 0;

        private static string EscapeIdentifier(string identifier) => identifier.Replace("]", "]]");
    }
}