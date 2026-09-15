using System;
using FontAwesome.Sharp;

namespace SIMS_WinFormsApp.Models.DTOs
{
    public sealed class OverflowMenuAction
    {
        public string Text { get; }
        public IconChar Icon { get; }
        public Action OnClick { get; }
        public bool IsDanger { get; }

        public OverflowMenuAction(string text, IconChar icon, Action onClick, bool isDanger = false)
        {
            Text = text ?? throw new ArgumentNullException(nameof(text));
            Icon = icon;
            OnClick = onClick ?? throw new ArgumentNullException(nameof(onClick));
            IsDanger = isDanger;
        }
    }
}