using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_WinFormsApp.UI.Theme
{
    public enum AccentColorName
    {
        Blue,
        Purple,
        Green,
        Orange,
        Rose,
        Teal
    }

    public sealed class AccentColor
    {
        public AccentColorName Name { get; }
        public Color Light { get; }
        public Color Dark { get; }

        private AccentColor(AccentColorName name, Color light, Color dark)
        {
            Name = name;
            Light = light;
            Dark = dark;
        }

        public static readonly AccentColor Blue = new AccentColor(AccentColorName.Blue, Color.FromArgb(30, 100, 200), Color.FromArgb(96, 165, 250));
        public static readonly AccentColor Purple = new AccentColor(AccentColorName.Purple, Color.FromArgb(124, 58, 237), Color.FromArgb(167, 139, 250));
        public static readonly AccentColor Green = new AccentColor(AccentColorName.Green, Color.FromArgb(5, 150, 105), Color.FromArgb(52, 211, 153));
        public static readonly AccentColor Orange = new AccentColor(AccentColorName.Orange, Color.FromArgb(194, 65, 12), Color.FromArgb(251, 146, 60));
        public static readonly AccentColor Rose = new AccentColor(AccentColorName.Rose, Color.FromArgb(225, 29, 72), Color.FromArgb(251, 113, 133));
        public static readonly AccentColor Teal = new AccentColor(AccentColorName.Teal, Color.FromArgb(13, 148, 136), Color.FromArgb(45, 212, 191));

        private static readonly AccentColor[] All = { Blue, Purple, Green, Orange, Rose, Teal };

        public Color Swatch => Light;

        public string I18nKey => "settings.accent." + Name.ToString().ToLowerInvariant();

        public static AccentColor FromName(AccentColorName name)
        {
            foreach (var a in All)
            {
                if (a.Name == name) return a;
            }
            return Blue;
        }

        public static AccentColor Parse(string name, AccentColor fallback = null)
        {
            foreach (var a in All)
            {
                if (string.Equals(a.Name.ToString(), name, System.StringComparison.OrdinalIgnoreCase))
                    return a;
            }
            return fallback ?? Blue;
        }

        public override string ToString() => Name.ToString();
    }
}
