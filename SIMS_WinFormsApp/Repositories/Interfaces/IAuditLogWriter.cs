namespace SIMS_WinFormsApp.Repositories.Interfaces
{
    public interface IAuditLogWriter
    {
        void Record(
            int? userId,
            string action,
            string tableName = null,
            int? recordId = null,
            string oldValue = null,
            string newValue = null,
            string detail = null);
    }
}