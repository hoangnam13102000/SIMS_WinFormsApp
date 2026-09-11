using System;
using System.Collections.Generic;

namespace SIMS_WinFormsApp.UI.Controls.Search
{

    public interface ISearchView
    {
        event EventHandler<string> TextEdited;

        event EventHandler<string> SuggestionPicked;

        string Text { get; set; }
        string PlaceholderText { set; }

        void ShowSuggestions(IList<string> suggestions);

        void HideSuggestions();
    }
}