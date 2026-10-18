using System.Drawing;
using FontAwesome.Sharp;

namespace SIMS_WinFormsApp.UI.Theme
{
    internal static class IconBitmapCache
    {
        private static readonly System.Collections.Generic.Dictionary<string, Bitmap> Cache =
            new System.Collections.Generic.Dictionary<string, Bitmap>();
        private static readonly System.Collections.Generic.Dictionary<string, Bitmap> ActionStripCache =
            new System.Collections.Generic.Dictionary<string, Bitmap>();

        public static Bitmap Get(IconChar icon, Color color, Size size)
        {
            if (size.Width <= 0 || size.Height <= 0) return null;
            string key = icon + ":" + color.ToArgb() + ":" + size.Width + "x" + size.Height;
            Bitmap cached;
            if (Cache.TryGetValue(key, out cached)) return cached;

            var box = new IconPictureBox
            {
                IconChar = icon,
                IconColor = color,
                IconSize = System.Math.Max(10, System.Math.Min(size.Width, size.Height) - 2),
                Size = size,
                SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage,
                BackColor = Color.Transparent
            };
            var bitmap = new Bitmap(size.Width, size.Height);
            box.DrawToBitmap(bitmap, new Rectangle(Point.Empty, size));
            box.Dispose();
            bitmap.MakeTransparent(bitmap.GetPixel(0, 0));
            Cache[key] = bitmap;
            return bitmap;
        }

        public static Bitmap GetActionStrip(bool isLocked)
        {
            return GetActionStrip(
                isLocked ? IconChar.LockOpen : IconChar.Lock,
                AppColors.Warning);
        }

        public static Bitmap GetActionStrip(IconChar finalAction, Color finalActionColor)
        {
            string key = finalAction + ":" + finalActionColor.ToArgb();
            Bitmap cached;
            if (ActionStripCache.TryGetValue(key, out cached)) return cached;

            const int iconSize = 24;
            const int gap = 8;
            const int height = 28;
            var bitmap = new Bitmap(iconSize * 3 + gap * 2, height);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.Transparent);
                graphics.DrawImageUnscaled(Get(IconChar.Eye, AppColors.TableViewAction, new Size(iconSize, iconSize)), 0, 2);
                graphics.DrawImageUnscaled(Get(IconChar.PenToSquare, AppColors.TableEditAction, new Size(iconSize, iconSize)), iconSize + gap, 2);
                graphics.DrawImageUnscaled(Get(finalAction, finalActionColor, new Size(iconSize, iconSize)), (iconSize + gap) * 2, 2);
            }

            ActionStripCache[key] = bitmap;
            return bitmap;
        }
    }
}
