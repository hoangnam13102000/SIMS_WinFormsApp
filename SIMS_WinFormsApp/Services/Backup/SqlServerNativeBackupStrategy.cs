using System;
using System.Data.Common;
using SIMS_WinFormsApp.DAL.Linq;
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

            using (SimsDataContext context = _connectionFactory.CreateDataContext())
            {
                context.CommandTimeout = 600;
                try
                {
                    context.ExecuteCommand(
                        "BACKUP DATABASE [" + EscapeIdentifier(databaseName) + "] TO DISK = {0} WITH INIT, COMPRESSION, STATS = 10",
                        destinationFilePath);
                }
                catch (Exception ex) when (ex.Message.IndexOf("COMPRESSION", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    // Một số bản SQL Server Express không có tùy chọn nén -> thử lại không nén.
                    context.ExecuteCommand(
                        "BACKUP DATABASE [" + EscapeIdentifier(databaseName) + "] TO DISK = {0} WITH INIT, STATS = 10",
                        destinationFilePath);
                }
            }
        }

        public void RestoreFrom(string backupFilePath)
        {
            string databaseName = GetDatabaseName();

            using (var master = new SimsDataContext(BuildMasterConnectionString()))
            {
                master.CommandTimeout = 600;
                master.ExecuteCommand(
                    "ALTER DATABASE [" + EscapeIdentifier(databaseName) + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
                try
                {
                    master.ExecuteCommand(
                        "RESTORE DATABASE [" + EscapeIdentifier(databaseName) + "] FROM DISK = {0} WITH REPLACE, STATS = 10",
                        backupFilePath);
                }
                finally
                {
                    master.ExecuteCommand(
                        "ALTER DATABASE [" + EscapeIdentifier(databaseName) + "] SET MULTI_USER");
                }
            }
        }

        private string GetDatabaseName()
        {
            var builder = new DbConnectionStringBuilder
            {
                ConnectionString = _connectionFactory.GetConnectionString()
            };
            string databaseKey = GetDatabaseKey(builder);
            return Convert.ToString(builder[databaseKey]);
        }

        private string BuildMasterConnectionString()
        {
            var builder = new DbConnectionStringBuilder
            {
                ConnectionString = _connectionFactory.GetConnectionString()
            };
            builder[GetDatabaseKey(builder)] = "master";
            return builder.ConnectionString;
        }

        private static string GetDatabaseKey(DbConnectionStringBuilder builder)
        {
            if (builder.ContainsKey("Initial Catalog"))
                return "Initial Catalog";
            if (builder.ContainsKey("Database"))
                return "Database";
            throw new InvalidOperationException("Connection string không chứa tên cơ sở dữ liệu.");
        }

        private static string EscapeIdentifier(string identifier) => identifier.Replace("]", "]]");
    }
}