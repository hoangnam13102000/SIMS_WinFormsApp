using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.UI.Theme
{
    public static class AppFonts
    {
        // Logical resource names must match the <LogicalName> entries added in the .csproj.
        private const string ResourceRegular = "SIMS_WinFormsApp.Fonts.Inter-Regular.ttf";
        private const string ResourceMedium = "SIMS_WinFormsApp.Fonts.Inter-Medium.ttf";
        private const string ResourceSemiBold = "SIMS_WinFormsApp.Fonts.Inter-SemiBold.ttf";
        private const string ResourceBold = "SIMS_WinFormsApp.Fonts.Inter-Bold.ttf";

        private static readonly PrivateFontCollection _fonts = new PrivateFontCollection();
        private static readonly List<IntPtr> _fontMemoryHandles = new List<IntPtr>();

        private const string FamilyRegularBold = "Inter";
        private const string FamilyMedium = "Inter Medium";
        private const string FamilySemiBold = "Inter SemiBold";

        // Safe fallback if embedded fonts fail to load for any reason.
        private const string FallbackFamily = "Segoe UI";

        static AppFonts()
        {
            TryLoadEmbeddedFont(ResourceRegular);
            TryLoadEmbeddedFont(ResourceMedium);
            TryLoadEmbeddedFont(ResourceSemiBold);
            TryLoadEmbeddedFont(ResourceBold);
        }

        private static void TryLoadEmbeddedFont(string resourceName)
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                using (var stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream == null) return;

                    byte[] fontData = new byte[stream.Length];
                    stream.Read(fontData, 0, fontData.Length);

                    IntPtr ptr = Marshal.AllocCoTaskMem(fontData.Length);
                    Marshal.Copy(fontData, 0, ptr, fontData.Length);
                    _fonts.AddMemoryFont(ptr, fontData.Length);
                    _fontMemoryHandles.Add(ptr);
                }
            }
            catch
            {
                // Swallow: CreateFont() below falls back to Segoe UI if the family
                // never got registered, so a bad/missing resource is never fatal.
            }
        }

        private static bool FamilyLoaded(string familyName)
        {
            return _fonts.Families.Any(f => string.Equals(f.Name, familyName, StringComparison.OrdinalIgnoreCase));
        }

        private static Font CreateFont(string preferredFamily, float size, FontStyle style)
        {
            if (FamilyLoaded(preferredFamily))
            {
                try { return new Font(preferredFamily, size, style, GraphicsUnit.Point, 0, false); }
                catch { /* fall through to fallback */ }
            }
            return new Font(FallbackFamily, size, style);
        }

       
        public static readonly Font Title = CreateFont(FamilyRegularBold, 24f, FontStyle.Bold);

        public static readonly Font Subtitle = CreateFont(FamilySemiBold, 13f, FontStyle.Regular);

        public static readonly Font Brand = CreateFont(FamilyRegularBold, 26f, FontStyle.Bold);
        public static readonly Font Body = CreateFont(FamilyRegularBold, 11f, FontStyle.Regular);
        public static readonly Font BodyBold = CreateFont(FamilyRegularBold, 11f, FontStyle.Bold);
        public static readonly Font Input = CreateFont(FamilyRegularBold, 11f, FontStyle.Regular);
        public static readonly Font Feature = CreateFont(FamilyMedium, 10.5f, FontStyle.Regular);
        public static readonly Font Small = CreateFont(FamilyRegularBold, 9f, FontStyle.Regular);
        public static readonly Font SmallBold = CreateFont(FamilyRegularBold, 9f, FontStyle.Bold);
        public static readonly Font Button = CreateFont(FamilySemiBold, 10.5f, FontStyle.Regular);
    }
}