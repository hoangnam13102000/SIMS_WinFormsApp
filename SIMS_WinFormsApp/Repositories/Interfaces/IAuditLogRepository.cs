using System.Collections.Generic;
using SIMS_WinFormsApp.Models.DTOs;

namespace SIMS_WinFormsApp.Repositories.Interfaces
{
    public interface IAuditLogRepository
    {
        AuditLogPageResult GetPage(AuditLogQuery query);
        AuditLogStatsDto GetStats();
        AuditLogDetailDto GetById(long logId);

        IReadOnlyList<string> GetDistinctActions();
        IReadOnlyList<string> GetDistinctTables();
    }
}