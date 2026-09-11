using System;
using System.Collections.Generic;

namespace SIMS_WinFormsApp.UI.Controls.Filter
{

    public interface IFilterView
    {
        event EventHandler OptionChanged;

        void SetOptions(IList<FilterOption> options);
        FilterOption SelectedOption { get; }
    }
}