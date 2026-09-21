using System.Collections.Generic;
using System.Linq;
using SIMS_WinFormsApp.Models.Enums;

namespace SIMS_WinFormsApp.UI.Controls
{
    /// <summary>Item hiển thị trong ComboBox "Giới tính" (dùng chung cho popup thêm/sửa nhân viên).</summary>
    public sealed class GenderOption
    {
        public static readonly IReadOnlyList<GenderOption> All = new[]
        {
            new GenderOption(Gender.Male, "Nam"),
            new GenderOption(Gender.Female, "Nữ"),
            new GenderOption(Gender.Other, "Khác")
        };

        private readonly string _label;

        private GenderOption(Gender value, string label)
        {
            Value = value;
            _label = label;
        }

        public Gender Value { get; }

        /// <summary>Tìm item theo giá trị; null nếu chưa có giới tính.</summary>
        public static GenderOption From(Gender? gender) =>
            gender.HasValue ? All.FirstOrDefault(g => g.Value == gender.Value) : null;

        public override string ToString() => _label;
    }
}