using System;
using SIMS_WinFormsApp.Models.DTOs;

namespace SIMS_WinFormsApp.UI.Controls.Filter
{
    /// <summary>
    /// Presenter của bộ lọc khoảng ngày: mốc nhanh, chuẩn hóa From &lt;= To,
    /// và chỉ phát <see cref="RangeChanged"/> khi giá trị thực sự đổi.
    /// </summary>
    public sealed class DateRangeFilterPresenter
    {
        private readonly IDateRangeFilterView _view;
        private DateRange _current = DateRange.Empty;
        private DateRangePreset _preset = DateRangePreset.None;

        public event EventHandler RangeChanged;

        public DateRange Current => _current;
        public DateRangePreset ActivePreset => _preset;

        public DateRangeFilterPresenter(IDateRangeFilterView view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));

            _view.PresetRequested += (_, preset) => Commit(Resolve(preset), preset);
            _view.FromPicked += (_, date) => Commit(WithFrom(date), DateRangePreset.None);
            _view.ToPicked += (_, date) => Commit(WithTo(date), DateRangePreset.None);
            _view.FromCleared += (_, __) => Commit(new DateRange(null, _current.To), DateRangePreset.None);
            _view.ToCleared += (_, __) => Commit(new DateRange(_current.From, null), DateRangePreset.None);
            _view.ClearRequested += (_, __) => Commit(DateRange.Empty, DateRangePreset.None);

            _view.ShowRange(_current, _preset);
        }

        private void Commit(DateRange range, DateRangePreset presetHint)
        {
            range = range ?? DateRange.Empty;
            var preset = presetHint == DateRangePreset.None ? Match(range) : presetHint;
            if (range.Equals(_current) && preset == _preset) return;

            _current = range;
            _preset = preset;
            _view.ShowRange(range, preset);
            RangeChanged?.Invoke(this, EventArgs.Empty);
        }

        private DateRange WithFrom(DateTime from)
        {
            from = from.Date;
            var to = _current.To;
            if (to.HasValue && to.Value < from) to = from;
            return new DateRange(from, to);
        }

        private DateRange WithTo(DateTime to)
        {
            to = to.Date;
            var from = _current.From;
            if (from.HasValue && from.Value > to) from = to;
            return new DateRange(from, to);
        }

        public static DateRange Resolve(DateRangePreset preset)
        {
            var today = DateTime.Today;
            switch (preset)
            {
                case DateRangePreset.Today:
                    return new DateRange(today, today);
                case DateRangePreset.Last7Days:
                    return new DateRange(today.AddDays(-6), today);
                case DateRangePreset.Last30Days:
                    return new DateRange(today.AddDays(-29), today);
                case DateRangePreset.ThisMonth:
                    return new DateRange(new DateTime(today.Year, today.Month, 1), today);
                default:
                    return DateRange.Empty;
            }
        }

        public static DateRangePreset Match(DateRange range)
        {
            if (range == null || range.IsEmpty) return DateRangePreset.None;

            DateRangePreset[] presets =
            {
                DateRangePreset.Today,
                DateRangePreset.Last7Days,
                DateRangePreset.Last30Days,
                DateRangePreset.ThisMonth
            };

            foreach (var preset in presets)
            {
                if (range.Equals(Resolve(preset))) return preset;
            }

            return DateRangePreset.None;
        }
    }
}
