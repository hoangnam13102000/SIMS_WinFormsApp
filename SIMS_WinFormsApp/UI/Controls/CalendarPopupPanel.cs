using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{
    public sealed class CalendarPopupPanel : UserControl
    {
        private const int PanelWidth = 280;
        private const int PanelHeight = 330;
        private const int HeaderHeight = 40;
        private const int WeekdayHeight = 28;
        private const int GridTop = HeaderHeight + WeekdayHeight;
        private const int CellWidth = 36;
        private const int CellHeight = 32;
        private static readonly Font MonthFont = new Font("Segoe UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point);
        private static readonly Font NavigationFont = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
        private static readonly string[] WeekdayLabels = { "T2", "T3", "T4", "T5", "T6", "T7", "CN" };
        private readonly IconButton _todayButton;
        private readonly IconButton _clearButton;
        private readonly Rectangle _previousMonthBounds = new Rectangle(10, 6, 28, 28);
        private readonly Rectangle _nextMonthBounds = new Rectangle(242, 6, 28, 28);
        private DateTime _displayedMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        private DateTime? _selectedDate;
        private DateTime? _hoveredDate;
        private int _hoveredNavigation;

        public CalendarPopupPanel()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            DoubleBuffered = true;
            AutoScaleMode = AutoScaleMode.None;
            BackColor = AppColors.White;
            Size = new Size(PanelWidth, PanelHeight);
            MinimumSize = Size;
            MaximumSize = Size;
            Cursor = Cursors.Hand;

            _todayButton = CreateFooterButton(
                "Hôm nay", IconChar.CalendarDay,
                Color.FromArgb(232, 240, 254), Color.FromArgb(26, 75, 145),
                Color.FromArgb(220, 232, 252));
            _todayButton.Location = new Point(12, 290);
            _todayButton.Click += (sender, args) => TodaySelected?.Invoke(DateTime.Today);

            _clearButton = CreateFooterButton(
                "Xóa ngày", IconChar.Xmark,
                Color.FromArgb(253, 236, 236), Color.FromArgb(198, 40, 40),
                Color.FromArgb(250, 220, 220));
            _clearButton.Location = new Point(144, 290);
            _clearButton.Click += (sender, args) => ClearRequested?.Invoke(this, EventArgs.Empty);

            Controls.Add(_todayButton);
            Controls.Add(_clearButton);
            MouseMove += CalendarPopupPanel_MouseMove;
            MouseLeave += (sender, args) =>
            {
                _hoveredDate = null;
                _hoveredNavigation = 0;
                Invalidate();
            };
            MouseDown += CalendarPopupPanel_MouseDown;
        }

        public event Action<DateTime> DateSelected;
        public event Action<DateTime> TodaySelected;
        public event EventHandler ClearRequested;

        public DateTime? SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value.HasValue ? value.Value.Date : (DateTime?)null;
                if (_selectedDate.HasValue)
                    _displayedMonth = new DateTime(_selectedDate.Value.Year, _selectedDate.Value.Month, 1);
                Invalidate();
            }
        }

        public DateTime DisplayedMonth
        {
            get => _displayedMonth;
            set
            {
                _displayedMonth = new DateTime(value.Year, value.Month, 1);
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.Clear(AppColors.White);

            DrawNavigationButton(graphics, _previousMonthBounds, "‹", _hoveredNavigation == -1);
            DrawNavigationButton(graphics, _nextMonthBounds, "›", _hoveredNavigation == 1);

            string monthText = "Tháng " + _displayedMonth.Month.ToString(CultureInfo.InvariantCulture) +
                ", " + _displayedMonth.Year.ToString(CultureInfo.InvariantCulture);
            TextRenderer.DrawText(graphics, monthText, MonthFont,
                new Rectangle(42, 0, 196, HeaderHeight), AppColors.TextTitle,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter |
                TextFormatFlags.NoPadding | TextFormatFlags.SingleLine);

            int gridLeft = (PanelWidth - CellWidth * 7) / 2;
            for (int column = 0; column < 7; column++)
            {
                Color weekdayColor = column == 6
                    ? Color.FromArgb(198, 89, 89)
                    : AppColors.TextMuted;
                var weekdayBounds = new Rectangle(
                    gridLeft + column * CellWidth,
                    HeaderHeight,
                    CellWidth,
                    WeekdayHeight);
                TextRenderer.DrawText(graphics, WeekdayLabels[column], AppFonts.Small,
                    weekdayBounds, weekdayColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter |
                    TextFormatFlags.NoPadding | TextFormatFlags.SingleLine);
            }

            DateTime firstOfMonth = new DateTime(_displayedMonth.Year, _displayedMonth.Month, 1);
            int mondayFirstOffset = ((int)firstOfMonth.DayOfWeek + 6) % 7;
            DateTime firstVisibleDate = firstOfMonth.AddDays(-mondayFirstOffset);
            for (int index = 0; index < 42; index++)
            {
                DateTime date = firstVisibleDate.AddDays(index);
                int column = index % 7;
                int row = index / 7;
                var cellBounds = new Rectangle(
                    gridLeft + column * CellWidth,
                    GridTop + row * CellHeight,
                    CellWidth,
                    CellHeight);
                DrawDateCell(graphics, cellBounds, date, column == 6);
            }

            using (var pen = new Pen(AppColors.Border))
                graphics.DrawLine(pen, 12, 281, PanelWidth - 12, 281);
        }

        private static IconButton CreateFooterButton(
            string text,
            IconChar icon,
            Color background,
            Color foreground,
            Color hoverBackground)
        {
            var button = new IconButton
            {
                BackColor = background,
                FlatStyle = FlatStyle.Flat,
                Font = AppFonts.SmallBold,
                ForeColor = foreground,
                IconChar = icon,
                IconColor = foreground,
                IconSize = 14,
                ImageAlign = ContentAlignment.MiddleLeft,
                Size = new Size(124, 32),
                Text = text,
                TextImageRelation = TextImageRelation.ImageBeforeText,
                UseVisualStyleBackColor = false
            };
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = hoverBackground;
            button.FlatAppearance.MouseDownBackColor = hoverBackground;
            return button;
        }

        private void DrawNavigationButton(Graphics graphics, Rectangle bounds, string text, bool hovered)
        {
            if (hovered)
            {
                using (var brush = new SolidBrush(AppColors.BgLighter))
                    graphics.FillEllipse(brush, bounds);
            }

            TextRenderer.DrawText(graphics, text, NavigationFont,
                bounds, AppColors.TextSecondary,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter |
                TextFormatFlags.NoPadding | TextFormatFlags.SingleLine);
        }

        private void DrawDateCell(Graphics graphics, Rectangle bounds, DateTime date, bool sunday)
        {
            bool selected = _selectedDate.HasValue && _selectedDate.Value.Date == date.Date;
            bool today = DateTime.Today == date.Date;
            bool hovered = _hoveredDate.HasValue && _hoveredDate.Value.Date == date.Date;
            Rectangle circle = new Rectangle(bounds.Left + 4, bounds.Top + 2, 28, 28);

            if (selected)
            {
                using (var brush = new SolidBrush(AppColors.Accent))
                    graphics.FillEllipse(brush, circle);
            }
            else if (hovered)
            {
                using (var brush = new SolidBrush(AppColors.BgLighter))
                    graphics.FillEllipse(brush, circle);
            }

            if (today && !selected)
            {
                using (var pen = new Pen(AppColors.Accent, 1.5F))
                    graphics.DrawEllipse(pen, circle);
            }

            Color textColor;
            if (selected)
                textColor = Color.White;
            else if (date.Month != _displayedMonth.Month || date.Year != _displayedMonth.Year)
                textColor = AppColors.TextMutedAlt;
            else if (sunday)
                textColor = Color.FromArgb(198, 89, 89);
            else
                textColor = AppColors.TextPrimary;

            TextRenderer.DrawText(graphics, date.Day.ToString(CultureInfo.InvariantCulture),
                selected ? AppFonts.SmallBold : AppFonts.Small,
                bounds, textColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter |
                TextFormatFlags.NoPadding | TextFormatFlags.SingleLine);
        }

        private void CalendarPopupPanel_MouseMove(object sender, MouseEventArgs e)
        {
            int navigation = _previousMonthBounds.Contains(e.Location)
                ? -1
                : (_nextMonthBounds.Contains(e.Location) ? 1 : 0);
            DateTime? hoveredDate = HitTestDate(e.Location);
            if (_hoveredNavigation == navigation && _hoveredDate == hoveredDate)
                return;

            _hoveredNavigation = navigation;
            _hoveredDate = hoveredDate;
            Cursor = navigation != 0 || hoveredDate.HasValue ? Cursors.Hand : Cursors.Default;
            Invalidate();
        }

        private void CalendarPopupPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            if (_previousMonthBounds.Contains(e.Location))
            {
                _displayedMonth = _displayedMonth.AddMonths(-1);
                Invalidate();
                return;
            }

            if (_nextMonthBounds.Contains(e.Location))
            {
                _displayedMonth = _displayedMonth.AddMonths(1);
                Invalidate();
                return;
            }

            DateTime? date = HitTestDate(e.Location);
            if (date.HasValue)
            {
                _selectedDate = date.Value.Date;
                _displayedMonth = new DateTime(date.Value.Year, date.Value.Month, 1);
                Invalidate();
                Action<DateTime> handler = DateSelected;
                if (handler != null)
                    handler(date.Value.Date);
            }
        }

        private DateTime? HitTestDate(Point location)
        {
            int gridLeft = (PanelWidth - CellWidth * 7) / 2;
            int column = (location.X - gridLeft) / CellWidth;
            int row = (location.Y - GridTop) / CellHeight;
            if (column < 0 || column >= 7 || row < 0 || row >= 6)
                return null;

            DateTime firstOfMonth = new DateTime(_displayedMonth.Year, _displayedMonth.Month, 1);
            int mondayFirstOffset = ((int)firstOfMonth.DayOfWeek + 6) % 7;
            return firstOfMonth.AddDays(row * 7 + column - mondayFirstOffset);
        }
    }
}
