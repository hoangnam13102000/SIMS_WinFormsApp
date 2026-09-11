using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.UI.Controls.Search
{

    public sealed class SearchPresenter : IDisposable
    {
        private readonly ISearchView _view;
        private readonly Func<string, IList<string>> _suggestionProvider;
        private readonly Timer _debounceTimer;
        private string _lastCommittedText = string.Empty;

        public event EventHandler<string> SearchCommitted;

        public SearchPresenter(ISearchView view, Func<string, IList<string>> suggestionProvider = null, int debounceMilliseconds = 350)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _suggestionProvider = suggestionProvider;

            _debounceTimer = new Timer { Interval = Math.Max(50, debounceMilliseconds) };
            _debounceTimer.Tick += (_, __) =>
            {
                _debounceTimer.Stop();
                Commit(_view.Text, refreshSuggestions: true);
            };

            _view.TextEdited += (_, __) =>
            {
                _debounceTimer.Stop();
                _debounceTimer.Start();
            };
            _view.SuggestionPicked += (_, picked) =>
            {
                _debounceTimer.Stop();
                _view.Text = picked;
                _view.HideSuggestions();
                Commit(picked, refreshSuggestions: false);
            };
        }

        private void Commit(string text, bool refreshSuggestions)
        {
            text = text ?? string.Empty;
            if (refreshSuggestions) RefreshSuggestions(text);
            if (text == _lastCommittedText) return;
            _lastCommittedText = text;
            SearchCommitted?.Invoke(this, text);
        }

        private void RefreshSuggestions(string text)
        {
            if (_suggestionProvider == null || string.IsNullOrWhiteSpace(text))
            {
                _view.HideSuggestions();
                return;
            }

            var items = _suggestionProvider(text) ?? new List<string>();
            if (items.Count == 0) _view.HideSuggestions();
            else _view.ShowSuggestions(items);
        }

        public void Dispose() => _debounceTimer.Dispose();
    }
}