using System.Data.SqlClient;
using SIMS_WinFormsApp.DAL.Linq;

namespace SIMS_WinFormsApp.Infrastructure.Configuration
{
    public interface IDbConnectionFactory
    {
        SqlConnection CreateConnection();
        SimsDataContext CreateDataContext();
    }
}
