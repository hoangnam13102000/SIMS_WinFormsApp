using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.UI.Controls
{

    [ToolboxItem(false)]
    [DesignerCategory("Code")]
    public class FieldGridPanel : Panel
    {
        private const int DefaultColumnGap = 24;
        private const int MinColumnWidth = 40;

        private sealed class GridCell
        {
            public GridCell(Panel host, Control field, int row, int column, int columnSpan)
            {
                Host = host;
                Field = field;
                Row = row;
                Column = column;
                ColumnSpan = columnSpan;
            }

            public Panel Host { get; }
            public Control Field { get; }
            public int Row { get; }
            public int Column { get; }
            public int ColumnSpan { get; }

            // Tự theo dõi cờ hiển thị thay vì đọc Control.Visible (getter đó trả false khi Form
            // chưa được Show, sẽ làm layout sai ở lần Reflow đầu).
            public bool IsVisible { get; set; } = true;
        }

        private readonly List<GridCell> _cells = new List<GridCell>();
        private readonly int _columnCount;
        private readonly int _columnGap;
        private int _nextRow;
        private int _nextColumn;
        private bool _isReflowing;

        public FieldGridPanel(int columnCount = 3, int columnGap = DefaultColumnGap)
        {
            if (columnCount < 1) throw new ArgumentOutOfRangeException(nameof(columnCount));
            if (columnGap < 0) throw new ArgumentOutOfRangeException(nameof(columnGap));

            _columnCount = columnCount;
            _columnGap = columnGap;

            BackColor = Color.Transparent;
            Dock = DockStyle.Top;

            Resize += (s, e) => Reflow();
        }

        /// <summary>Thêm 1 field vào ô kế tiếp (đọc từ trái sang phải, từ trên xuống dưới).
        /// Thứ tự gọi AddField cũng là thứ tự Tab của các field.</summary>
        public void AddField(Control field, int columnSpan = 1)
        {
            if (field == null) throw new ArgumentNullException(nameof(field));

            int span = Math.Min(Math.Max(1, columnSpan), _columnCount);
            if (_nextColumn + span > _columnCount)
            {
                _nextRow++;
                _nextColumn = 0;
            }

            var host = new Panel { BackColor = Color.Transparent };
            field.Dock = DockStyle.Top;
            host.Controls.Add(field);
            Controls.Add(host);

            _cells.Add(new GridCell(host, field, _nextRow, _nextColumn, span));

            _nextColumn += span;
            if (_nextColumn >= _columnCount)
            {
                _nextRow++;
                _nextColumn = 0;
            }
        }

        /// <summary>Ẩn/hiện 1 field đã thêm. Ô của field ẩn vẫn giữ chỗ về cột (các field khác
        /// không bị xô lệch); hàng không còn field nào hiển thị sẽ co về chiều cao 0.</summary>
        public void SetFieldVisible(Control field, bool visible)
        {
            if (field == null) throw new ArgumentNullException(nameof(field));

            GridCell cell = _cells.Find(c => ReferenceEquals(c.Field, field));
            if (cell == null) throw new ArgumentException("Field chưa được thêm vào lưới.", nameof(field));

            cell.IsVisible = visible;
            cell.Host.Visible = visible;
            Reflow();
        }

        /// <summary>Tính lại vị trí/độ rộng các ô và chiều cao của cả lưới. Bỏ qua khi Width
        /// chưa phải kích thước thật (cùng nguyên tắc với ThreeColumnFieldsPanel.Reflow).</summary>
        public void Reflow()
        {
            if (_isReflowing || _cells.Count == 0) return;

            int totalGaps = _columnGap * (_columnCount - 1);
            if (Width < _columnCount * MinColumnWidth + totalGaps) return;

            _isReflowing = true;
            try
            {
                int columnWidth = (Width - totalGaps) / _columnCount;
                int y = 0;
                int i = 0;

                while (i < _cells.Count)
                {
                    int row = _cells[i].Row;
                    int rowStart = i;
                    int rowHeight = 0;

                    for (; i < _cells.Count && _cells[i].Row == row; i++)
                    {
                        GridCell cell = _cells[i];
                        if (!cell.IsVisible) continue;

                        int x = cell.Column * (columnWidth + _columnGap);
                        int width = cell.ColumnSpan * columnWidth + (cell.ColumnSpan - 1) * _columnGap;

                        cell.Host.SetBounds(x, y, width, Math.Max(1, cell.Host.Height));
                        cell.Host.PerformLayout();
                        rowHeight = Math.Max(rowHeight, cell.Field.Height);
                    }

                    for (int j = rowStart; j < i; j++)
                    {
                        if (_cells[j].IsVisible) _cells[j].Host.Height = rowHeight;
                    }

                    y += rowHeight;
                }

                if (Height != y) Height = y;
            }
            finally
            {
                _isReflowing = false;
            }
        }
    }
}