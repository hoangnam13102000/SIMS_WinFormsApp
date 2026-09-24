using System;
using SIMS_WinFormsApp.Models.DTOs;

namespace SIMS_WinFormsApp.UI.Controls.Filter
{
    /// <summary>
    /// View thuần UI của bộ lọc khoảng ngày. Không tự quyết định From/To —
    /// mọi mốc nhanh, chuẩn hóa và xóa lọc đều đi qua <see cref="DateRangeFilterPresenter"/>.
    /// </summary>
    public interface IDateRangeFilterView
    {
        event EventHandler<DateRangePreset> PresetRequested;
        event EventHandler<DateTime> FromPicked;
        event EventHandler<DateTime> ToPicked;
        event EventHandler FromCleared;
        event EventHandler ToCleared;
        event EventHandler ClearRequested;

        void ShowRange(DateRange range, DateRangePreset activePreset);
    }
}
