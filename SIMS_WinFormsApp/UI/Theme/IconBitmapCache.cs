using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Views.Toast;

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
            return GetActionStrip(true, finalAction, finalActionColor);
        }

        public static Bitmap GetActionStrip(bool canEdit, IconChar finalAction, Color finalActionColor)
        {
            string key = canEdit + ":" + finalAction + ":" + finalActionColor.ToArgb();
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
                if (canEdit)
                {
                    graphics.DrawImageUnscaled(Get(IconChar.PenToSquare, AppColors.TableEditAction, new Size(iconSize, iconSize)), iconSize + gap, 2);
                    graphics.DrawImageUnscaled(Get(finalAction, finalActionColor, new Size(iconSize, iconSize)), (iconSize + gap) * 2, 2);
                }

            }

            ActionStripCache[key] = bitmap;
            return bitmap;
        }
    }

    internal static class DataGridViewCodeCopy
    {
        private const int IconSize = 14;
        private const int RightPadding = 26;

        public static void Attach(DataGridView grid, params string[] columnNames)
        {
            Attach(grid, true, columnNames);
        }

        public static void Attach(DataGridView grid, bool copyOnClick, params string[] columnNames)
        {
            if (grid == null)
                throw new ArgumentNullException(nameof(grid));
            if (columnNames == null || columnNames.Length == 0)
                throw new ArgumentException("At least one code column is required.", nameof(columnNames));

            var columns = columnNames
                .Select(name => grid.Columns[name] ??
                    throw new ArgumentException("Column not found: " + name, nameof(columnNames)))
                .ToArray();

            foreach (DataGridViewColumn column in columns)
            {
                Padding padding = column.DefaultCellStyle.Padding;
                column.DefaultCellStyle.Padding = new Padding(
                    padding.Left,
                    padding.Top,
                    padding.Right + RightPadding,
                    padding.Bottom);
            }

            grid.CellPainting += (sender, args) =>
            {
                if (args.RowIndex < 0 || !columns.Any(column => column.Index == args.ColumnIndex))
                    return;

                args.Paint(args.CellBounds, DataGridViewPaintParts.All);
                Bitmap icon = IconBitmapCache.Get(
                    IconChar.Copy, AppColors.Accent, new Size(IconSize, IconSize));
                var iconBounds = new Rectangle(
                    args.CellBounds.Right - RightPadding + (RightPadding - IconSize) / 2,
                    args.CellBounds.Top + (args.CellBounds.Height - IconSize) / 2,
                    IconSize,
                    IconSize);
                args.Graphics.DrawImageUnscaled(icon, iconBounds.Location);
                args.Handled = true;
            };

            if (copyOnClick)
            {
                grid.CellClick += (sender, args) =>
                {
                    if (args.RowIndex < 0 || !columns.Any(column => column.Index == args.ColumnIndex))
                        return;

                    string value = Convert.ToString(
                        grid.Rows[args.RowIndex].Cells[args.ColumnIndex].Value,
                        CultureInfo.CurrentCulture);
                    if (string.IsNullOrWhiteSpace(value))
                        return;

                    try
                    {
                        Clipboard.SetText(value);
                        AppToast.Success(grid, "Copy thành công", "Đã copy mã: " + value);
                    }
                    catch (Exception exception)
                    {
                        AppToast.Error(grid, "Không thể copy mã", exception.Message);
                    }
                };
            }

            grid.CellToolTipTextNeeded += (sender, args) =>
            {
                if (args.RowIndex >= 0 &&
                    columns.Any(column => column.Index == args.ColumnIndex) &&
                    string.IsNullOrEmpty(args.ToolTipText))
                    args.ToolTipText = "Click để copy mã";
            };
            grid.CellMouseMove += (sender, args) =>
            {
                if (args.RowIndex >= 0 && columns.Any(column => column.Index == args.ColumnIndex))
                    grid.Cursor = Cursors.Hand;
                else if (grid.Cursor == Cursors.Hand)
                    grid.Cursor = Cursors.Default;
            };
            grid.MouseLeave += (sender, args) => grid.Cursor = Cursors.Default;
        }
    }
}
