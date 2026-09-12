using System.Data.SqlClient;
using SIMS_WinFormsApp.DAL.Linq;

namespace SIMS_WinFormsApp.Infrastructure.Configuration
{
    public sealed class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly IConnectionStringProvider _connectionStringProvider;

        public DbConnectionFactory(IConnectionStringProvider connectionStringProvider)
        {
            _connectionStringProvider = connectionStringProvider;
        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionStringProvider.GetConnectionString());
        }

        public SimsDataContext CreateDataContext()
        {
            return new SimsDataContext(_connectionStringProvider.GetConnectionString());
        }
    }
}
