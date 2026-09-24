using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.UI.I18n;
using SIMS_WinFormsApp.UI.Theme;
using PopupFormBase = SIMS_WinFormsApp.UI.Controls.PopupFormBase;
using RoundedDialogFrame = SIMS_WinFormsApp.UI.Controls.RoundedDialogFrame;

namespace SIMS_WinFormsApp.UI.Controls.Filter
{
    /// <summary>
    /// Bộ lọc khoảng ngày: mốc nhanh + hai ô ngày luôn đủ chỗ cho "31/12/2026".
    /// Khi bề ngang không đủ, ô ngày xuống dòng thay vì bị cắt chữ.
    /// </summary>
    public sealed class DateRangeFilterControl : UserControl, IDateRangeFilterView
    {
        public const string DateFormat = "dd/MM/yyyy";

        private readonly Label _caption;
        private readonly PresetChip _todayChip;
        private readonly PresetChip _weekChip;
        private readonly PresetChip _monthChip;
        private readonly PresetChip _thisMonthChip;
        private readonly ClearFilterButton _clearButton;
        private readonly DateBoundField _fromField;
        private readonly DateBoundField _toField;
        private readonly Label _arrow;
        private readonly DateCalendarPopup _popup;

        private DateRangeBound _openBound = DateRangeBound.None;
        private Control _lastClosedAnchor;
        private DateTime _lastClosedAt = DateTime.MinValue;

        public event EventHandler<DateRangePreset> PresetRequested;
        public event EventHandler<DateTime> FromPicked;
        public event EventHandler<DateTime> ToPicked;
        public event EventHandler FromCleared;
        public event EventHandler ToCleared;
        public event EventHandler ClearRequested;

        public DateRangeFilterControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            BackColor = Color.Transparent;
            Margin = Padding.Empty;

            _caption = CreateCaption();
            _todayChip = new PresetChip(DateRangePreset.Today);
            _weekChip = new PresetChip(DateRangePreset.Last7Days);
            _monthChip = new PresetChip(DateRangePreset.Last30Days);
            _thisMonthChip = new PresetChip(DateRangePreset.ThisMonth);
            _clearButton = new ClearFilterButton();
            _fromField = new DateBoundField();
            _toField = new DateBoundField();
            _arrow = new Label
            {
                Text = "→",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = AppFonts.HeadingMd,
                ForeColor = AppColors.TextMuted,
                BackColor = Color.Transparent
            };
            _popup = new DateCalendarPopup();

            Controls.Add(_caption);
            Controls.Add(_todayChip);
            Controls.Add(_weekChip);
            Controls.Add(_monthChip);
            Controls.Add(_thisMonthChip);
            Controls.Add(_clearButton);
            Controls.Add(_fromField);
            Controls.Add(_toField);
            Controls.Add(_arrow);

            _todayChip.Click += (_, __) => PresetRequested?.Invoke(this, DateRangePreset.Today);
            _weekChip.Click += (_, __) => PresetRequested?.Invoke(this, DateRangePreset.Last7Days);
            _monthChip.Click += (_, __) => PresetRequested?.Invoke(this, DateRangePreset.Last30Days);
            _thisMonthChip.Click += (_, __) => PresetRequested?.Invoke(this, DateRangePreset.ThisMonth);
            _clearButton.Click += (_, __) => ClearRequested?.Invoke(this, EventArgs.Empty);
            _fromField.Click += (_, __) => OpenPopup(DateRangeBound.From, _fromField);
            _toField.Click += (_, __) => OpenPopup(DateRangeBound.To, _toField);
            _popup.DateChosen += (_, date) => OnPopupDate(date);
            _popup.Cleared += (_, __) => OnPopupCleared();
            _popup.PopupClosed += (_, __) =>
            {
                _lastClosedAnchor = _openBound == DateRangeBound.From ? _fromField : _toField;
                _lastClosedAt = DateTime.UtcNow;
                _openBound = DateRangeBound.None;
                _fromField.IsOpen = false;
                _toField.IsOpen = false;
            };

            ApplyLocalization();
            ThemeManager.Instance.ThemeChanged += OnThemeChanged;
        }

        public void ShowRange(DateRange range, DateRangePreset activePreset)
        {
            range = range ?? DateRange.Empty;
            _fromField.Value = range.From;
            _toField.Value = range.To;
            _todayChip.IsActive = activePreset == DateRangePreset.Today;
            _weekChip.IsActive = activePreset == DateRangePreset.Last7Days;
            _monthChip.IsActive = activePreset == DateRangePreset.Last30Days;
            _thisMonthChip.IsActive = activePreset == DateRangePreset.ThisMonth;
            _clearButton.Enabled = !range.IsEmpty;
            _clearButton.Invalidate();
        }

        public void ApplyLocalization()
        {
            _caption.Text = Lang.Get("audit.date.section");
            _todayChip.Text = Lang.Get("audit.date.today");
            _weekChip.Text = Lang.Get("audit.date.last7");
            _monthChip.Text = Lang.Get("audit.date.last30");
            _thisMonthChip.Text = Lang.Get("audit.date.thisMonth");
            _clearButton.Text = Lang.Get("audit.date.clear");
            _fromField.Caption = Lang.Get("audit.date.from");
            _fromField.Placeholder = Lang.Get("audit.date.placeholder");
            _toField.Caption = Lang.Get("audit.date.to");
            _toField.Placeholder = Lang.Get("audit.date.placeholder");
            _popup.ApplyLocalization();
        }

        /// <summary>Xếp chip + ô ngày trong bề ngang cho trước và trả về chiều cao cần thiết.</summary>
        public int Arrange(int width)
        {
            width = Math.Max(width, _fromField.MinimumContentWidth);
            int captionH = 22;
            int chipH = 34;
            int fieldH = DateBoundField.FieldHeight;
            int gap = 8;

            _caption.SetBounds(0, 0, width, captionH);

            int chipsWidth = _todayChip.Width + gap + _weekChip.Width + gap +
                             _monthChip.Width + gap + _thisMonthChip.Width;
            int fieldMin = Math.Max(_fromField.MinimumContentWidth, _toField.MinimumContentWidth);
            int arrowW = 28;
            int clearW = _clearButton.Width;
            int fieldsBlock = fieldMin * 2 + arrowW;
            bool oneRow = width >= chipsWidth + 16 + fieldsBlock + 8 + clearW;

            int y = captionH + 6;
            if (oneRow)
            {
                int chipY = y + (fieldH - chipH) / 2;
                PlaceChips(0, chipY, chipH, gap);
                _clearButton.SetBounds(width - clearW, y + (fieldH - _clearButton.Height) / 2, clearW, _clearButton.Height);
                int fieldsLeft = chipsWidth + 12;
                int fieldsRight = width - clearW - 12;
                int fieldW = Math.Max(fieldMin, (fieldsRight - fieldsLeft - arrowW) / 2);
                int maxField = Math.Max(fieldMin, (fieldsRight - fieldsLeft - arrowW) / 2);
                fieldW = Math.Min(fieldW, maxField);
                PlaceFields(fieldsLeft, y, fieldW, fieldH, arrowW, sideBySide: true);
                Height = y + fieldH;
            }
            else
            {
                bool clearFits = chipsWidth + 8 + clearW <= width;
                PlaceChips(0, y, chipH, gap);
                if (clearFits)
                {
                    _clearButton.SetBounds(width - clearW, y + (chipH - _clearButton.Height) / 2, clearW, _clearButton.Height);
                    y += chipH + 10;
                }
                else
                {
                    y += chipH + 6;
                    _clearButton.SetBounds(0, y, clearW, _clearButton.Height);
                    y += _clearButton.Height + 8;
                }

                bool sideBySide = width >= fieldMin * 2 + arrowW + 8;
                if (sideBySide)
                {
                    int fieldW = Math.Max(fieldMin, (width - arrowW) / 2);
                    if (fieldW * 2 + arrowW > width) fieldW = Math.Max(1, (width - arrowW) / 2);
                    PlaceFields(0, y, fieldW, fieldH, arrowW, sideBySide: true);
                    y += fieldH;
                }
                else
                {
                    PlaceFields(0, y, width, fieldH, arrowW, sideBySide: false);
                    y += fieldH * 2 + 8;
                }
                Height = y;
            }

            Width = width;
            return Height;
        }

        private void PlaceChips(int x, int y, int height, int gap)
        {
            PresetChip[] chips = { _todayChip, _weekChip, _monthChip, _thisMonthChip };
            foreach (var chip in chips)
            {
                chip.SetBounds(x, y, chip.Width, height);
                x += chip.Width + gap;
            }
        }

        private void PlaceFields(int x, int y, int fieldW, int fieldH, int arrowW, bool sideBySide)
        {
            _fromField.SetBounds(x, y, fieldW, fieldH);
            if (sideBySide)
            {
                _arrow.Visible = true;
                _arrow.SetBounds(x + fieldW, y, arrowW, fieldH);
                _toField.SetBounds(x + fieldW + arrowW, y, fieldW, fieldH);
            }
            else
            {
                _arrow.Visible = false;
                _toField.SetBounds(x, y + fieldH + 8, fieldW, fieldH);
            }
        }

        private void OpenPopup(DateRangeBound bound, DateBoundField field)
        {
            if (_lastClosedAnchor == field && (DateTime.UtcNow - _lastClosedAt).TotalMilliseconds < 280)
                return;

            _openBound = bound;
            _fromField.IsOpen = bound == DateRangeBound.From;
            _toField.IsOpen = bound == DateRangeBound.To;
            _popup.ShowAt(field, field.Value);
        }

        private void OnPopupDate(DateTime date)
        {
            if (_openBound == DateRangeBound.From) FromPicked?.Invoke(this, date);
            else if (_openBound == DateRangeBound.To) ToPicked?.Invoke(this, date);
        }

        private void OnPopupCleared()
        {
            if (_openBound == DateRangeBound.From) FromCleared?.Invoke(this, EventArgs.Empty);
            else if (_openBound == DateRangeBound.To) ToCleared?.Invoke(this, EventArgs.Empty);
        }

        private void OnThemeChanged(object sender, EventArgs e)
        {
            _caption.ForeColor = AppColors.TextMuted;
            _arrow.ForeColor = AppColors.TextMuted;
            Invalidate(true);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ThemeManager.Instance.ThemeChanged -= OnThemeChanged;
                _popup.Dispose();
            }
            base.Dispose(disposing);
        }

        private static Label CreateCaption()
        {
            return new Label
            {
                AutoSize = false,
                Font = AppFonts.SmallBold,
                ForeColor = AppColors.TextMuted,
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Transparent
            };
        }

        private enum DateRangeBound
        {
            None,
            From,
            To
        }

        private sealed class PresetChip : Control
        {
            private bool _active;
            private bool _hover;

            public DateRangePreset Preset { get; }

            public bool IsActive
            {
                get => _active;
                set
                {
                    if (_active == value) return;
                    _active = value;
                    Invalidate();
                }
            }

            public PresetChip(DateRangePreset preset)
            {
                Preset = preset;
                SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                         ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
                Cursor = Cursors.Hand;
                Height = 34;
                Font = AppFonts.SmallBold;
            }

            public override string Text
            {
                get => base.Text;
                set
                {
                    base.Text = value ?? string.Empty;
                    Width = Math.Max(72, TextRenderer.MeasureText(base.Text, Font,
                        Size.Empty, TextFormatFlags.NoPadding | TextFormatFlags.SingleLine).Width + 28);
                    Invalidate();
                }
            }

            protected override void OnMouseEnter(EventArgs e) { base.OnMouseEnter(e); _hover = true; Invalidate(); }
            protected override void OnMouseLeave(EventArgs e) { base.OnMouseLeave(e); _hover = false; Invalidate(); }

            protected override void OnPaint(PaintEventArgs e)
            {
                if (Width < 8 || Height < 8) return;
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = new Rectangle(0, 0, Width - 1, Height - 1);
                Color bg = _active ? AppColors.Accent : (_hover ? AppColors.AccentBgSoft : AppColors.BgLighter);
                Color fg = _active ? Color.White : AppColors.TextPrimary;
                using (var path = AppRadius.GetRoundedPath(rect, Height / 2))
                using (var brush = new SolidBrush(bg))
                using (var pen = new Pen(_active ? AppColors.Accent : AppColors.Border))
                {
                    g.FillPath(brush, path);
                    g.DrawPath(pen, path);
                }

                TextRenderer.DrawText(g, Text, Font, rect, fg,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter |
                    TextFormatFlags.SingleLine | TextFormatFlags.NoPadding);
            }
        }

        private sealed class ClearFilterButton : Control
        {
            private bool _hover;

            public ClearFilterButton()
            {
                SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                         ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
                Cursor = Cursors.Hand;
                Height = 32;
                Font = AppFonts.SmallBold;
            }

            public override string Text
            {
                get => base.Text;
                set
                {
                    base.Text = value ?? string.Empty;
                    int textW = TextRenderer.MeasureText(base.Text, Font, Size.Empty,
                        TextFormatFlags.NoPadding | TextFormatFlags.SingleLine).Width;
                    Width = 12 + 14 + 6 + textW + 12;
                    Invalidate();
                }
            }

            protected override void OnMouseEnter(EventArgs e) { base.OnMouseEnter(e); _hover = true; Invalidate(); }
            protected override void OnMouseLeave(EventArgs e) { base.OnMouseLeave(e); _hover = false; Invalidate(); }

            protected override void OnPaint(PaintEventArgs e)
            {
                if (Width < 8 || Height < 8) return;
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                Color fg = Enabled ? AppColors.Error : AppColors.TextDisabled;
                Color bg = Enabled && _hover ? AppColors.ErrorBg : Color.Transparent;
                var rect = new Rectangle(0, 0, Width - 1, Height - 1);
                using (var path = AppRadius.GetRoundedPath(rect, Height / 2))
                using (var brush = new SolidBrush(bg))
                    g.FillPath(brush, path);

                var iconRect = new Rectangle(10, (Height - 14) / 2, 14, 14);
                DrawIcon(g, IconChar.Xmark, fg, iconRect);
                var textRect = new Rectangle(28, 0, Width - 34, Height);
                TextRenderer.DrawText(g, Text, Font, textRect, fg,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.SingleLine | TextFormatFlags.NoPadding);
            }
        }

        private sealed class DateBoundField : Control
        {
            public const int FieldHeight = 58;

            private string _caption = string.Empty;
            private string _placeholder = string.Empty;
            private DateTime? _value;
            private bool _hover;
            private bool _open;

            public DateBoundField()
            {
                SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                         ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
                Cursor = Cursors.Hand;
                Height = FieldHeight;
                Font = AppFonts.BodyBold;
            }

            public string Caption
            {
                get => _caption;
                set { _caption = value ?? string.Empty; Invalidate(); }
            }

            public string Placeholder
            {
                get => _placeholder;
                set { _placeholder = value ?? string.Empty; Invalidate(); }
            }

            public DateTime? Value
            {
                get => _value;
                set { _value = value?.Date; Invalidate(); }
            }

            public bool IsOpen
            {
                get => _open;
                set { _open = value; Invalidate(); }
            }

            public int MinimumContentWidth
            {
                get
                {
                    int captionW = TextRenderer.MeasureText(Caption, AppFonts.Small, Size.Empty,
                        TextFormatFlags.NoPadding | TextFormatFlags.SingleLine).Width;
                    int valueW = TextRenderer.MeasureText("31/12/2026", AppFonts.BodyBold, Size.Empty,
                        TextFormatFlags.NoPadding | TextFormatFlags.SingleLine).Width;
                    int placeholderW = TextRenderer.MeasureText(Placeholder, AppFonts.Body, Size.Empty,
                        TextFormatFlags.NoPadding | TextFormatFlags.SingleLine).Width;
                    int textW = Math.Max(captionW, Math.Max(valueW, placeholderW));
                    return 14 + 20 + 10 + textW + 12 + 16 + 12;
                }
            }

            protected override void OnMouseEnter(EventArgs e) { base.OnMouseEnter(e); _hover = true; Invalidate(); }
            protected override void OnMouseLeave(EventArgs e) { base.OnMouseLeave(e); _hover = false; Invalidate(); }

            protected override void OnPaint(PaintEventArgs e)
            {
                if (Width < 8 || Height < 8) return;
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                var rect = new Rectangle(0, 0, Math.Max(1, Width - 1), Math.Max(1, Height - 1));
                Color border = _open || _hover ? AppColors.Accent : AppColors.FieldBorder;
                using (var path = AppRadius.GetRoundedPath(rect, AppRadius.Medium))
                using (var brush = new SolidBrush(AppColors.White))
                using (var pen = new Pen(border, _open ? 1.6f : 1f))
                {
                    g.FillPath(brush, path);
                    g.DrawPath(pen, path);
                }

                var iconRect = new Rectangle(12, (Height - 18) / 2, 18, 18);
                DrawIcon(g, IconChar.CalendarDays, _value.HasValue ? AppColors.Accent : AppColors.IconMuted, iconRect);

                int textLeft = 40;
                int textRight = Width - 28;
                int textWidth = Math.Max(20, textRight - textLeft);
                var captionRect = new Rectangle(textLeft, 8, textWidth, 18);
                var valueRect = new Rectangle(textLeft, 26, textWidth, 24);

                TextRenderer.DrawText(g, Caption, AppFonts.Small, captionRect, AppColors.TextMuted,
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine | TextFormatFlags.NoPadding |
                    TextFormatFlags.EndEllipsis);

                if (_value.HasValue)
                {
                    TextRenderer.DrawText(g, _value.Value.ToString(DateFormat), AppFonts.BodyBold, valueRect, AppColors.TextTitle,
                        TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine | TextFormatFlags.NoPadding);
                }
                else
                {
                    TextRenderer.DrawText(g, Placeholder, AppFonts.Body, valueRect, AppColors.TextMutedAlt,
                        TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine | TextFormatFlags.NoPadding);
                }

                var chevron = new Rectangle(Width - 24, (Height - 12) / 2, 12, 12);
                DrawIcon(g, IconChar.ChevronDown, AppColors.IconMuted, chevron);
            }
        }

        private sealed class DateCalendarPopup : PopupFormBase
        {
            private readonly MonthCalendar _calendar;
            private readonly Label _todayLink;
            private readonly Label _clearLink;

            public event EventHandler<DateTime> DateChosen;
            public event EventHandler Cleared;

            public DateCalendarPopup()
            {
                BackColor = AppColors.White;
                Padding = new Padding(12, 8, 12, 8);

                _calendar = new MonthCalendar
                {
                    MaxSelectionCount = 1,
                    ShowToday = false,
                    ShowTodayCircle = true,
                    Font = AppFonts.Small
                };
                _calendar.DateSelected += (_, e) =>
                {
                    DateChosen?.Invoke(this, e.Start.Date);
                    Close();
                };

                _todayLink = CreateLink(true);
                _clearLink = CreateLink(false);
                _todayLink.Click += (_, __) =>
                {
                    DateChosen?.Invoke(this, DateTime.Today);
                    Close();
                };
                _clearLink.Click += (_, __) =>
                {
                    Cleared?.Invoke(this, EventArgs.Empty);
                    Close();
                };

                Controls.Add(_calendar);
                Controls.Add(_todayLink);
                Controls.Add(_clearLink);
                ApplyLocalization();
            }

            public void ApplyLocalization()
            {
                _todayLink.Text = Lang.Get("audit.date.todayAction");
                _clearLink.Text = Lang.Get("audit.date.clear");
                _calendar.TitleBackColor = AppColors.Accent;
                _calendar.TitleForeColor = Color.White;
                _calendar.TrailingForeColor = AppColors.TextMuted;
                _calendar.BackColor = AppColors.White;
                _calendar.ForeColor = AppColors.TextPrimary;
            }

            public void ShowAt(Control anchor, DateTime? selected)
            {
                if (anchor == null || anchor.IsDisposed) return;
                if (!IsHandleCreated) CreateHandle();

                var day = selected ?? DateTime.Today;
                _calendar.SetDate(day);
                FitToCalendar();

                var working = Screen.FromControl(anchor).WorkingArea;
                var below = anchor.PointToScreen(new Point(0, anchor.Height + 6));
                int x = below.X;
                int y = below.Y;
                if (x + Width > working.Right) x = Math.Max(working.Left, working.Right - Width - 8);
                if (y + Height > working.Bottom)
                {
                    int above = anchor.PointToScreen(Point.Empty).Y - Height - 6;
                    y = above >= working.Top ? above : working.Top;
                }

                Location = new Point(x, y);
                Show(anchor.FindForm());
                _calendar.Focus();
            }

            private void FitToCalendar()
            {
                int calW = Math.Max(227, _calendar.Width);
                int calH = Math.Max(162, _calendar.Height);
                _calendar.SetBounds(12, 10, calW, calH);
                int linkH = Math.Max(22, Math.Max(_todayLink.PreferredSize.Height, _clearLink.PreferredSize.Height));
                int footerTop = _calendar.Bottom + 10;
                _todayLink.Location = new Point(14, footerTop);
                _clearLink.Location = new Point(Math.Max(120, _calendar.Right - Math.Max(48, _clearLink.PreferredSize.Width)), footerTop);
                ClientSize = new Size(_calendar.Right + 14, footerTop + linkH + 12);
                AppRadius.ApplyRoundedCorners(this, AppRadius.Medium);
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                var frame = new RoundedDialogFrame(AppRadius.Medium, 1f);
                frame.PaintBorder(e.Graphics, ClientSize, AppColors.Border);
            }

            private static Label CreateActionLabel(bool accent)
            {
                var normal = accent ? AppColors.Accent : AppColors.TextMuted;
                var hover = accent ? AppColors.AccentHover : AppColors.TextPrimary;
                var label = new Label
                {
                    AutoSize = true,
                    Font = AppFonts.SmallBold,
                    ForeColor = normal,
                    BackColor = Color.Transparent,
                    Cursor = Cursors.Hand
                };
                label.MouseEnter += (_, __) => label.ForeColor = hover;
                label.MouseLeave += (_, __) => label.ForeColor = normal;
                return label;
            }
        }

        private static void DrawIcon(Graphics g, IconChar icon, Color color, Rectangle bounds)
        {
            var bitmap = IconBitmapCache.Get(icon, color, bounds.Size);
            if (bitmap == null) return;
            g.DrawImageUnscaled(bitmap, bounds.Location);
        }
    }

    /// <summary>Cache bitmap FontAwesome dùng chung cho ô ngày / nút xóa — tránh tạo control mỗi lần vẽ.</summary>
    internal static class IconBitmapCache
    {
        private static readonly System.Collections.Generic.Dictionary<string, Bitmap> Cache =
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
                IconSize = Math.Max(10, Math.Min(size.Width, size.Height) - 2),
                Size = size,
                SizeMode = PictureBoxSizeMode.CenterImage,
                BackColor = Color.Transparent
            };
            var bitmap = new Bitmap(size.Width, size.Height);
            box.DrawToBitmap(bitmap, new Rectangle(Point.Empty, size));
            box.Dispose();
            bitmap.MakeTransparent(bitmap.GetPixel(0, 0));
            Cache[key] = bitmap;
            return bitmap;
        }
    }
}
