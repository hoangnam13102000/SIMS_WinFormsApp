using System.Drawing;
using FontAwesome.Sharp;

namespace SIMS_WinFormsApp.MVP.ViewModels
{
    public readonly struct AccountStatusDisplay
    {
        public string Text { get; }
        public IconChar Icon { get; }
        public Color Foreground { get; }
        public Color Background { get; }

        public AccountStatusDisplay(string text, IconChar icon, Color foreground, Color background)
        {
            Text = text;
            Icon = icon;
            Foreground = foreground;
            Background = background;
        }
    }
}