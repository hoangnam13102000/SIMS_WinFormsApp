using SIMS_WinFormsApp.DAL.Linq;

namespace SIMS_WinFormsApp.Infrastructure.Configuration
{
    public interface IDbConnectionFactory
    {
        string GetConnectionString();
        SimsDataContext CreateDataContext();
    }
}
