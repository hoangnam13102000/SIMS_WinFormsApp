using System;
using System.Collections.Generic;

namespace SIMS_WinFormsApp.UI.Controls.Filter
{
    public sealed class FilterPresenter
    {
        private readonly IFilterView _view;
        private string _lastCommittedValue;

        public event EventHandler<FilterOption> FilterChanged;

        public FilterPresenter(IFilterView view, IList<FilterOption> options)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));

            _view.SetOptions(options ?? new List<FilterOption>());
            _lastCommittedValue = _view.SelectedOption?.Value;

            _view.OptionChanged += (_, __) =>
            {
                var selected = _view.SelectedOption;
                if (string.Equals(selected?.Value, _lastCommittedValue, StringComparison.Ordinal)) return;
                _lastCommittedValue = selected?.Value;
                FilterChanged?.Invoke(this, selected);
            };
        }

        public string CurrentValue => _view.SelectedOption?.Value;
    }
}