using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_WinFormsApp.UI.Controls.Filter
{
    public sealed class FilterOption
    {
        public string DisplayText { get; }
        public string Value { get; }

        public FilterOption(string displayText, string value)
        {
            DisplayText = displayText ?? string.Empty;
            Value = value;
        }

        /// <summary>ComboBox gọi ToString() để hiển thị từng item, nên chỉ cần trả về nhãn.</summary>
        public override string ToString() => DisplayText;
    }
}
