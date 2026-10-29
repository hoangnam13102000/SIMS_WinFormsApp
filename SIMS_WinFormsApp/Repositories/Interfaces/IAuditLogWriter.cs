namespace SIMS_WinFormsApp.Repositories.Interfaces
{
    public interface IAuditLogWriter
    {
        int CountUnviewedInvoices(int userId, bool canViewAll, System.DateTime today);
        void RecordInvoiceViewed(int userId, int? invoiceId, string invoiceCode);

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