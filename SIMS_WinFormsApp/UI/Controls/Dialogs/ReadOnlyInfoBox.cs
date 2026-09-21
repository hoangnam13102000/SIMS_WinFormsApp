using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{
    /// <summary>
    /// Khối xám bo góc hiển thị các thông tin CHỈ ĐỌC (Mã tài khoản, Tên đăng nhập, Vai trò...)
    /// xếp ngang theo N cột, tự xuống hàng và tự co chiều cao. Thay cho các hàm dựng chip nằm
    /// riêng trong form (SRP: form chỉ khai báo dữ liệu, không tự vẽ/đo).
    /// </summary>
    [ToolboxItem(false)]
    [DesignerCategory("Code")]
    public class ReadOnlyInfoBox : Panel
    {
        private const int BoxPadding = 16;
        private const int ItemGap = 24;
        private const int RowGap = 14;
        private const int TrailingSpace = 18; // cùng nhịp SpacingAfterBlock của các Labeled*Field
        private const int MinItemWidth = 40;

        private readonly List<InfoItem> _items = new List<InfoItem>();
        private readonly int _columnCount;
        private bool _isLayingOut;

        public ReadOnlyInfoBox(int columnCount = 3)
        {
            if (columnCount < 1) throw new ArgumentOutOfRangeException(nameof(columnCount));
            _columnCount = columnCount;

            BackColor = Color.Transparent;
            Dock = DockStyle.Top;
            DoubleBuffered = true;

            Resize += (s, e) => LayoutItems();
        }

        public void AddItem(IconChar icon, string caption, string value)
        {
            var item = new InfoItem(icon, caption, value);
            _items.Add(item);
            Controls.Add(item);
            LayoutItems();
        }

        private void LayoutItems()
        {
            if (_isLayingOut || _items.Count == 0) return;

            int gaps = ItemGap * (_columnCount - 1);
            int contentWidth = Width - BoxPadding * 2;
            if (contentWidth < _columnCount * MinItemWidth + gaps) return;

            _isLayingOut = true;
            try
            {
                int itemWidth = (contentWidth - gaps) / _columnCount;
                int y = BoxPadding;
                int i = 0;

                while (i < _items.Count)
                {
                    int rowHeight = 0;
                    for (int column = 0; column < _columnCount && i < _items.Count; column++, i++)
                    {
                        InfoItem item = _items[i];
                        item.SetBounds(BoxPadding + column * (itemWidth + ItemGap), y, itemWidth, item.PreferredHeight);
                        rowHeight = Math.Max(rowHeight, item.PreferredHeight);
                    }
                    y += rowHeight + RowGap;
                }

                int newHeight = y - RowGap + BoxPadding + TrailingSpace;
                if (Height != newHeight) Height = newHeight;
                Invalidate();
            }
            finally
            {
                _isLayingOut = false;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = new Rectangle(0, 0, Width - 1, Math.Max(1, Height - TrailingSpace - 1));
            using (var path = AppRadius.GetRoundedPath(rect, AppRadius.Medium))
            using (var brush = new SolidBrush(AppColors.BgLighter))
                e.Graphics.FillPath(brush, path);
        }

        /// <summary>1 ô thông tin: icon + nhãn nhỏ phía trên + giá trị đậm phía dưới.</summary>
        private sealed class InfoItem : Panel
        {
            private const int IconSize = 14;
            private const int IconColumnWidth = 22;

            private readonly IconPictureBox _icon;
            private readonly Label _caption;
            private readonly Label _value;
            private readonly int _captionHeight;
            private readonly int _valueHeight;

            public InfoItem(IconChar icon, string caption, string value)
            {
                BackColor = Color.Transparent;

                _icon = new IconPictureBox
                {
                    IconChar = icon,
                    IconColor = AppColors.TextMuted,
                    IconSize = IconSize,
                    Size = new Size(IconSize, IconSize),
                    BackColor = Color.Transparent
                };
                _caption = new Label
                {
                    AutoSize = false,
                    Text = caption ?? string.Empty,
                    Font = AppFonts.Small,
                    ForeColor = AppColors.TextMuted,
                    BackColor = Color.Transparent,
                    AutoEllipsis = true,
                    UseMnemonic = false
                };
                _value = new Label
                {
                    AutoSize = false,
                    Text = value ?? string.Empty,
                    Font = AppFonts.BodyBold,
                    ForeColor = AppColors.TextTitle,
                    BackColor = Color.Transparent,
                    AutoEllipsis = true,
                    UseMnemonic = false
                };

                // Đo chiều cao 1 dòng theo Font/DPI thật (không đoán số cố định) để chữ không bị cắt chân.
                _captionHeight = Math.Max(16, MeasureLineHeight("Ag", _caption.Font));
                _valueHeight = Math.Max(20, MeasureLineHeight("Ag", _value.Font));
                Height = PreferredHeight;

                Controls.Add(_icon);
                Controls.Add(_caption);
                Controls.Add(_value);

                Resize += (s, e) => LayoutParts();
                LayoutParts();
            }

            public int PreferredHeight => _captionHeight + _valueHeight;

            private static int MeasureLineHeight(string text, Font font) =>
                TextRenderer.MeasureText(text, font, new Size(int.MaxValue, int.MaxValue), TextFormatFlags.NoPadding).Height;

            private void LayoutParts()
            {
                int textWidth = Math.Max(10, Width - IconColumnWidth);
                _icon.Location = new Point(0, 4);
                _caption.SetBounds(IconColumnWidth, 0, textWidth, _captionHeight);
                _value.SetBounds(IconColumnWidth, _captionHeight, textWidth, _valueHeight);
            }
        }
    }
}