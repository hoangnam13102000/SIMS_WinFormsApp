using System;
using System.Collections.Generic;
using System.Linq;
using SIMS_WinFormsApp.DAL.Linq;
using SIMS_WinFormsApp.DAL.Linq.Entities.Identity;
using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.Models.Mapping;
using SIMS_WinFormsApp.Repositories.Interfaces;

namespace SIMS_WinFormsApp.Repositories.Implementations
{
    public class AuditLogRepository : IAuditLogRepository, IAuditLogWriter
    {
        public AuditLogPageResult GetPage(AuditLogQuery query)
        {
            query = query ?? new AuditLogQuery();

            using (var db = new SimsDataContext())
            {
                IQueryable<AuditLogEntity> baseQuery = db.AuditLogs;

                if (query.FromDate.HasValue)
                    baseQuery = baseQuery.Where(l => l.CreatedAt >= query.FromDate.Value.Date);
                if (query.ToDate.HasValue)
                    baseQuery = baseQuery.Where(l => l.CreatedAt < query.ToDate.Value.Date.AddDays(1));
                if (!string.IsNullOrWhiteSpace(query.ActionFilter))
                    baseQuery = baseQuery.Where(l => l.Action == query.ActionFilter);
                if (!string.IsNullOrWhiteSpace(query.TableFilter))
                    baseQuery = baseQuery.Where(l => l.TableName == query.TableFilter);

                var joined =
                    from l in baseQuery
                    join u in db.Users on l.UserID equals u.UserID into userJoin
                    from u in userJoin.DefaultIfEmpty()
                    select new
                    {
                        l.LogID,
                        l.CreatedAt,
                        l.Action,
                        l.TableName,
                        l.Detail,
                        Username = u != null ? u.Username : null
                    };

                var materialized = joined.ToList();

                if (!string.IsNullOrWhiteSpace(query.Search))
                {
                    string s = query.Search.Trim();
                    materialized = materialized.Where(x =>
                        (x.Username != null && x.Username.IndexOf(s, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (x.Detail != null && x.Detail.IndexOf(s, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        AuditLogDisplayMapper.GetActionLabel(x.Action).IndexOf(s, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        AuditLogDisplayMapper.GetTableLabel(x.TableName).IndexOf(s, StringComparison.OrdinalIgnoreCase) >= 0
                    ).ToList();
                }

                var classified = materialized
                    .Select(x => new AuditLogRowDto
                    {
                        LogId = x.LogID,
                        CreatedAt = x.CreatedAt,
                        Username = x.Username ?? "(đã xóa)",
                        Action = x.Action,
                        TableName = x.TableName,
                        Detail = x.Detail,
                        IsIncident = AuditLogDisplayMapper.IsIncident(x.Action)
                    })
                    .Where(r => r.IsIncident == query.IncidentOnly)
                    .OrderByDescending(r => r.CreatedAt)
                    .ToList();

                int total = classified.Count;
                int pageSize = Math.Max(1, query.PageSize);
                var page = classified.Skip(query.PageIndex * pageSize).Take(pageSize).ToList();

                return new AuditLogPageResult { TotalCount = total, Rows = page };
            }
        }

        public AuditLogStatsDto GetStats()
        {
            using (var db = new SimsDataContext())
            {
                var all = db.AuditLogs.Select(l => new { l.Action, l.CreatedAt, l.UserID }).ToList();
                var today = DateTime.Today;

                return new AuditLogStatsDto
                {
                    TotalCount = all.Count,
                    TodayCount = all.Count(x => x.CreatedAt.Date == today),
                    FailedLoginCount = all.Count(x => AuditLogDisplayMapper.IsIncident(x.Action) &&
                        string.Equals(x.Action, "LOGIN_FAILED", StringComparison.OrdinalIgnoreCase)),
                    ActiveUserCount = all.Where(x => x.UserID.HasValue)
                        .Select(x => x.UserID.Value)
                        .Distinct()
                        .Count()
                };
            }
        }

        public AuditLogDetailDto GetById(long logId)
        {
            using (var db = new SimsDataContext())
            {
                var found =
                    (from l in db.AuditLogs
                     join u in db.Users on l.UserID equals u.UserID into userJoin
                     from u in userJoin.DefaultIfEmpty()
                     where l.LogID == logId
                     select new { Log = l, Username = u != null ? u.Username : null })
                    .FirstOrDefault();

                if (found == null) return null;

                return new AuditLogDetailDto
                {
                    LogId = found.Log.LogID,
                    CreatedAt = found.Log.CreatedAt,
                    Username = found.Username ?? "(đã xóa)",
                    Action = found.Log.Action,
                    TableName = found.Log.TableName,
                    RecordId = found.Log.RecordID,
                    OldValue = found.Log.OldValue,
                    NewValue = found.Log.NewValue,
                    Detail = found.Log.Detail,
                    IPAddress = found.Log.IPAddress
                };
            }
        }

        public IReadOnlyList<string> GetDistinctActions()
        {
            using (var db = new SimsDataContext())
            {
                return db.AuditLogs
                    .Select(l => l.Action)
                    .Distinct()
                    .ToList()
                    .Where(a => !string.IsNullOrEmpty(a))
                    .OrderBy(a => a)
                    .ToList();
            }
        }

        public IReadOnlyList<string> GetDistinctTables()
        {
            using (var db = new SimsDataContext())
            {
                return db.AuditLogs
                    .Select(l => l.TableName)
                    .Distinct()
                    .ToList()
                    .Where(t => !string.IsNullOrEmpty(t))
                    .OrderBy(t => t)
                    .ToList();
            }
        }

        public void Record(
            int? userId,
            string action,
            string tableName = null,
            int? recordId = null,
            string oldValue = null,
            string newValue = null,
            string detail = null)
        {
            using (var db = new SimsDataContext())
            {
                db.AuditLogs.InsertOnSubmit(new AuditLogEntity
                {
                    UserID = userId,
                    Action = action,
                    TableName = tableName,
                    RecordID = recordId,
                    OldValue = oldValue,
                    NewValue = newValue,
                    Detail = detail,
                    CreatedAt = DateTime.Now
                });
                db.SubmitChanges();
            }
        }
    }
}