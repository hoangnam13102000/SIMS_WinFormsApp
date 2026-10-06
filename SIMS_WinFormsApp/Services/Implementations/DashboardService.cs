using System;
using System.Collections.Generic;
using System.Linq;
using SIMS_WinFormsApp.Models.DTOs.Dashboard;
using SIMS_WinFormsApp.Repositories.Interfaces;
using SIMS_WinFormsApp.Services.Interfaces;

namespace SIMS_WinFormsApp.Services.Implementations
{
    public sealed class DashboardService : IDashboardService
    {
        private const int TrendDayCount = 7;
        private readonly IDashboardRepository _repository;
        private readonly Func<DateTime> _todayProvider;

        public DashboardService(IDashboardRepository repository, Func<DateTime> todayProvider = null)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _todayProvider = todayProvider ?? (() => DateTime.Today);
        }

        public DashboardSummaryDto GetSummary()
        {
            DateTime today = _todayProvider().Date;
            var summary = _repository.GetSummary(today);
            var dailyRevenue = (summary.RevenueTrend ?? new List<DashboardRevenuePointDto>())
                .ToDictionary(point => point.Date.Date, point => point.Revenue);

            summary.RevenueTrend = Enumerable.Range(0, TrendDayCount)
                .Select(offset =>
                {
                    DateTime date = today.AddDays(offset - (TrendDayCount - 1));
                    decimal revenue;
                    dailyRevenue.TryGetValue(date, out revenue);
                    return new DashboardRevenuePointDto { Date = date, Revenue = revenue };
                })
                .ToList();

            return summary;
        }
    }
}
