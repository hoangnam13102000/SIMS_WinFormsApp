using System;
using System.Collections.Generic;
using SIMS_WinFormsApp.Models.DTOs;

namespace SIMS_WinFormsApp.Views.Interfaces
{
    public interface IAuditLogView
    {
        AuditLogQuery CurrentQuery { get; }

        void DisplayStats(AuditLogStatsDto stats);
        void DisplayRows(IReadOnlyList<AuditLogRowDto> rows, int totalCount);
        void DisplayActionOptions(IReadOnlyList<string> actions);
        void DisplayTableOptions(IReadOnlyList<string> tables);
        void ShowDetail(AuditLogDetailDto detail);
        void ShowError(string message);

        event EventHandler QueryChanged;
        event EventHandler<long> DetailRequested;
    }
}