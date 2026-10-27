using System;
using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.Models.Enums;

namespace SIMS_WinFormsApp.Views.Interfaces
{
    /// <summary>
    /// View thuần UI của bộ lọc khoảng ngày. Không tự quyết định From/To —
    /// mọi mốc nhanh, chuẩn hóa và xóa lọc đều được xử lý bởi Presenter.
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
