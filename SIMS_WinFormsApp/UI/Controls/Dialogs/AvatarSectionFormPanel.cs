using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{
    /// <summary>
    /// Bố cục popup dạng: cột trái là ảnh đại diện, bên phải là các "section" xếp chồng theo
    /// chiều dọc (mỗi section = tiêu đề nhóm + lưới field), giữa các section hiển thị có đường
    /// kẻ mảnh. Section có thể ẩn/hiện (đường kẻ tự điều chỉnh theo).
    ///
    /// Chỉ lo việc ghép bố cục (SRP). Việc chia cột trong từng section do
    /// <see cref="FieldGridPanel"/> đảm nhiệm; form cụ thể chỉ khai báo field và section
    /// (OCP: thêm/bớt section không phải sửa lớp này).
    /// </summary>
    [ToolboxItem(false)]
    [DesignerCategory("Code")]
    public class AvatarSectionFormPanel : Panel
    {
        private const int DefaultAvatarColumnWidth = 168;
        private const int ColumnGap = 32;
        private const int MinSectionsWidth = 200;
        private const int TopSpacing = 16;
        private const int HeaderExtraHeight = 12;
        private const int SeparatorOffset = 2;
        private const int SpacingAfterSeparator = 18;

        private sealed class Section
        {
            public Section(FieldGroupHeader header, FieldGridPanel grid)
            {
                Header = header;
                Grid = grid;
            }

            public FieldGroupHeader Header { get; }
            public FieldGridPanel Grid { get; }
            public bool IsVisible { get; set; } = true;
        }

        private readonly List<Section> _sections = new List<Section>();
        private readonly List<int> _separatorYs = new List<int>();
        private bool _isReflowing;

        public AvatarUploadPanel Avatar { get; }

        public int AvatarColumnWidth { get; set; } = DefaultAvatarColumnWidth;

        public AvatarSectionFormPanel()
        {
            BackColor = Color.Transparent;
            Dock = DockStyle.Top;
            Padding = new Padding(0, TopSpacing, 0, 0);

            Avatar = new AvatarUploadPanel();
            Controls.Add(Avatar);

            Resize += (s, e) => Reflow();
        }

        /// <summary>Thêm 1 section (tiêu đề nhóm + lưới field) xuống dưới cùng.</summary>
        public void AddSection(FieldGroupHeader header, FieldGridPanel grid)
        {
            if (header == null) throw new ArgumentNullException(nameof(header));
            if (grid == null) throw new ArgumentNullException(nameof(grid));

            // Vị trí do Reflow() định vị thủ công nên bỏ Dock mặc định (Top) của 2 control này.
            header.Dock = DockStyle.None;
            grid.Dock = DockStyle.None;

            // Chiều cao tiêu đề bám theo cỡ chữ thật (đổi theo DPI) để không dính sát field bên dưới.
            int textHeight = TextRenderer.MeasureText(
                "Ag", AppFonts.SmallBold, new Size(int.MaxValue, int.MaxValue), TextFormatFlags.NoPadding).Height;
            header.Height = Math.Max(header.Height, textHeight + HeaderExtraHeight);

            Controls.Add(header);
            Controls.Add(grid);
            _sections.Add(new Section(header, grid));
        }

        /// <summary>Ẩn/hiện cả 1 section (tiêu đề + lưới field) theo lưới đã truyền vào AddSection.</summary>
        public void SetSectionVisible(FieldGridPanel grid, bool visible)
        {
            if (grid == null) throw new ArgumentNullException(nameof(grid));

            Section section = _sections.Find(s => ReferenceEquals(s.Grid, grid));
            if (section == null) throw new ArgumentException("Section chưa được thêm vào panel.", nameof(grid));

            section.IsVisible = visible;
            section.Header.Visible = visible;
            section.Grid.Visible = visible;
            Reflow();
        }

        /// <summary>Tính lại vị trí avatar + các section và chiều cao của cả panel. Form cha nên
        /// gọi sau khi đã có handle (BaseFormDialogForm.OnContentReady).</summary>
        public void Reflow()
        {
            if (_isReflowing) return;

            int sectionsLeft = AvatarColumnWidth + ColumnGap;
            int sectionsWidth = Width - sectionsLeft;
            if (sectionsWidth < MinSectionsWidth) return;

            _isReflowing = true;
            try
            {
                Avatar.Width = AvatarColumnWidth;
                Avatar.Location = new Point(0, Padding.Top);

                _separatorYs.Clear();
                int y = Padding.Top;
                bool isFirstVisible = true;

                foreach (Section section in _sections)
                {
                    if (!section.IsVisible) continue;

                    if (!isFirstVisible)
                    {
                        y += SeparatorOffset;
                        _separatorYs.Add(y);
                        y += SpacingAfterSeparator;
                    }
                    isFirstVisible = false;

                    section.Header.SetBounds(sectionsLeft, y, sectionsWidth, section.Header.Height);
                    y = section.Header.Bottom;

                    section.Grid.SetBounds(sectionsLeft, y, sectionsWidth, section.Grid.Height);
                    section.Grid.Reflow();
                    y = section.Grid.Bottom;
                }

                int newHeight = Math.Max(Avatar.Bottom, y);
                if (Height != newHeight) Height = newHeight;
                Invalidate();
            }
            finally
            {
                _isReflowing = false;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            int left = AvatarColumnWidth + ColumnGap;
            using (var pen = new Pen(AppColors.Border, 1f))
            {
                foreach (int y in _separatorYs)
                    e.Graphics.DrawLine(pen, left, y, Width, y);
            }
        }
    }
}