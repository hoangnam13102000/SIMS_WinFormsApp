using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.UI.Layouts
{
    public sealed class VerticalStackLayout
    {
        private enum EntryKind { Block, CenteredLink, LinkPair }

        private sealed class Entry
        {
            public EntryKind Kind;
            public Control First;
            public Control Second;
            public int MinHeight;
            public int GapBetween;
            public int GapAfter;
        }

        private const int TextHeightPadding = 2;
        private const int LinkWidthPadding = 4;

        private readonly Control _container;
        private readonly int _contentWidth;
        private readonly List<Entry> _entries = new List<Entry>();

        public VerticalStackLayout(Control container, int contentWidth)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));

            _container = container;
            _contentWidth = Math.Max(1, contentWidth);
        }

        /// <summary>Control chiếm trọn chiều ngang nội dung. Label sẽ tự cao thêm nếu chữ cần.</summary>
        public VerticalStackLayout Add(Control control, int gapAfter)
        {
            _entries.Add(CreateEntry(EntryKind.Block, control, null, 0, gapAfter));
            return this;
        }

        /// <summary>Link rộng vừa đúng chữ và canh giữa (không bị cắt thành "abc...").</summary>
        public VerticalStackLayout AddCenteredLink(Label link, int gapAfter)
        {
            _entries.Add(CreateEntry(EntryKind.CenteredLink, link, null, 0, gapAfter));
            return this;
        }

        /// <summary>
        /// Hai link cùng hàng, đối xứng quanh tâm: link trái canh phải sát tâm (TextAlign = MiddleRight),
        /// link phải canh trái sát tâm (TextAlign = MiddleLeft).
        /// </summary>
        public VerticalStackLayout AddLinkPair(Label leftLink, Label rightLink, int gapBetween, int gapAfter)
        {
            _entries.Add(CreateEntry(EntryKind.LinkPair, leftLink, rightLink, gapBetween, gapAfter));
            return this;
        }

        /// <summary>Xếp lại toàn bộ control. Trả về chiều cao nội dung (đáy của control cuối cùng).</summary>
        public int Apply()
        {
            int y = 0;
            int bottom = 0;

            _container.SuspendLayout();
            try
            {
                foreach (Entry entry in _entries)
                {
                    int height = PlaceEntry(entry, y);
                    bottom = y + height;
                    y = bottom + entry.GapAfter;
                }
            }
            finally
            {
                _container.ResumeLayout(false);
            }

            return bottom;
        }

        private static Entry CreateEntry(EntryKind kind, Control first, Control second, int gapBetween, int gapAfter)
        {
            if (first == null) throw new ArgumentNullException(nameof(first));

            return new Entry
            {
                Kind = kind,
                First = first,
                Second = second,
                // Chiều cao thiết kế ban đầu được giữ làm mức tối thiểu.
                MinHeight = Math.Max(first.Height, second != null ? second.Height : 0),
                GapBetween = Math.Max(0, gapBetween),
                GapAfter = Math.Max(0, gapAfter)
            };
        }

        private int PlaceEntry(Entry entry, int y)
        {
            switch (entry.Kind)
            {
                case EntryKind.CenteredLink:
                    return PlaceCenteredLink(entry, y);
                case EntryKind.LinkPair:
                    return PlaceLinkPair(entry, y);
                default:
                    return PlaceBlock(entry, y);
            }
        }

        private int PlaceBlock(Entry entry, int y)
        {
            Control control = entry.First;
            int height = entry.MinHeight;

            Label label = control as Label;
            if (label != null)
            {
                // GetPreferredSize dùng đúng cờ vẽ chữ của Label (Font, DPI, xuống dòng theo bề rộng).
                Size preferred = label.GetPreferredSize(new Size(_contentWidth, 0));
                height = Math.Max(height, preferred.Height + TextHeightPadding);
            }

            control.SetBounds(0, y, _contentWidth, height);
            return height;
        }

        private int PlaceCenteredLink(Entry entry, int y)
        {
            Label link = (Label)entry.First;

            Size preferred = link.GetPreferredSize(Size.Empty);
            int width = Math.Min(_contentWidth, preferred.Width + LinkWidthPadding);
            int height = Math.Max(entry.MinHeight, preferred.Height + TextHeightPadding);

            link.SetBounds((_contentWidth - width) / 2, y, width, height);
            return height;
        }

        private int PlaceLinkPair(Entry entry, int y)
        {
            Label left = (Label)entry.First;
            Label right = (Label)entry.Second;

            Size leftSize = left.GetPreferredSize(Size.Empty);
            Size rightSize = right.GetPreferredSize(Size.Empty);

            int center = _contentWidth / 2;
            int halfGap = entry.GapBetween / 2;

            int leftWidth = Math.Min(center - halfGap, leftSize.Width + LinkWidthPadding);
            int rightWidth = Math.Min(_contentWidth - center - halfGap, rightSize.Width + LinkWidthPadding);
            int height = Math.Max(entry.MinHeight, Math.Max(leftSize.Height, rightSize.Height) + TextHeightPadding);

            left.SetBounds(center - halfGap - leftWidth, y, leftWidth, height);
            right.SetBounds(center + halfGap, y, rightWidth, height);
            return height;
        }
    }
}