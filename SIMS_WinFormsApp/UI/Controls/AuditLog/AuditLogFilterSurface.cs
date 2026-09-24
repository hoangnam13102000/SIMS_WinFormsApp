using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Controls.Filter;
using SIMS_WinFormsApp.UI.Controls.Search;
using SIMS_WinFormsApp.UI.I18n;
using SIMS_WinFormsApp.UI.Theme;
using StatCard = SIMS_WinFormsApp.UI.Controls.StatCard;

namespace SIMS_WinFormsApp.UI.Controls.AuditLog
{
    /// <summary>
    /// Khối thống kê + tab + thanh lọc của trang nhật ký.
    /// Tự xuống dòng khi hẹp để ô ngày, combobox và nhãn không bị cắt.
    /// </summary>
    public sealed class AuditLogFilterSurface : UserControl
    {
        private readonly StatCard _statTotal;
        private readonly StatCard _statToday;
        private readonly StatCard _statFailed;
        private readonly StatCard _statUsers;
        private readonly Label _countLabel;
        private readonly Panel _card;

        public SegmentedTabControl Tabs { get; }
        public SearchBarControl SearchBar { get; }
        public FilterComboBox ActionFilter { get; }
        public FilterComboBox TableFilter { get; }
        public DateRangeFilterControl DateRange { get; }

        public AuditLogFilterSurface()
        {
            BackColor = Color.Transparent;
            Margin = Padding.Empty;

            _statTotal = CreateStat(IconChar.ClockRotateLeft, AppColors.Accent, AppColors.AccentBgSoft);
            _statToday = CreateStat(IconChar.Bolt, AppColors.Success, AppColors.SuccessBg);
            _statFailed = CreateStat(IconChar.TriangleExclamation, AppColors.Error, AppColors.ErrorBg);
            _statUsers = CreateStat(IconChar.Users, AppColors.Warning, AppColors.WarningBg);

            Tabs = new SegmentedTabControl();
            _countLabel = new Label
            {
                AutoSize = false,
                Font = AppFonts.SmallBold,
                ForeColor = AppColors.TextMuted,
                TextAlign = ContentAlignment.MiddleRight,
                BackColor = Color.Transparent,
                AutoEllipsis = true
            };

            SearchBar = new SearchBarControl { Height = 46 };
            ActionFilter = CreateCombo();
            TableFilter = CreateCombo();
            DateRange = new DateRangeFilterControl();

            _card = new Panel { BackColor = AppColors.White };
            _card.Paint += PaintCard;
            _card.Controls.Add(SearchBar);
            _card.Controls.Add(ActionFilter);
            _card.Controls.Add(TableFilter);
            _card.Controls.Add(DateRange);

            Controls.Add(_statTotal);
            Controls.Add(_statToday);
            Controls.Add(_statFailed);
            Controls.Add(_statUsers);
            Controls.Add(Tabs);
            Controls.Add(_countLabel);
            Controls.Add(_card);
            ThemeManager.Instance.ThemeChanged += OnThemeChanged;
            ApplyLocalization();
        }

        public void ApplyLocalization()
        {
            _statTotal.TitleText = Lang.Get("audit.stats.total");
            _statToday.TitleText = Lang.Get("audit.stats.today");
            _statFailed.TitleText = Lang.Get("audit.stats.failedLogin");
            _statUsers.TitleText = Lang.Get("audit.stats.activeUsers");
            Tabs.SetItems(Lang.Get("audit.tab.audit"), Lang.Get("audit.tab.incident"));
            SearchBar.PlaceholderText = Lang.Get("audit.search.placeholder");
            DateRange.ApplyLocalization();
        }

        public void SetStats(int total, int today, int failedLogin, int activeUsers)
        {
            _statTotal.ValueText = total.ToString("N0");
            _statToday.ValueText = today.ToString("N0");
            _statFailed.ValueText = failedLogin.ToString("N0");
            _statUsers.ValueText = activeUsers.ToString("N0");
        }

        public void SetCount(int totalCount)
        {
            _countLabel.Text = Lang.Get("audit.count", totalCount.ToString("N0"));
        }

        public int Arrange(int width)
        {
            width = Math.Max(320, width);
            int y = 0;
            int statsH = ArrangeStats(width);
            y += statsH + 14;

            int tabsH = ArrangeTabs(width, y);
            y += tabsH + 12;

            int cardH = ArrangeCard(width);
            _card.SetBounds(0, y, width, cardH);
            y += cardH;

            Size = new Size(width, y);
            return y;
        }

        private int ArrangeStats(int width)
        {
            const int gap = 12;
            const int cardH = 128;
            int cols = width >= 190 * 4 + gap * 3 ? 4 : 2;
            int rows = cols == 4 ? 1 : 2;
            int cardW = (width - gap * (cols - 1)) / cols;
            StatCard[] cards = { _statTotal, _statToday, _statFailed, _statUsers };
            for (int i = 0; i < cards.Length; i++)
            {
                int col = i % cols;
                int row = i / cols;
                cards[i].MinimumSize = new Size(140, 124);
                cards[i].SetBounds(col * (cardW + gap), row * (cardH + gap), cardW, cardH);
            }
            return rows * cardH + (rows - 1) * gap;
        }

        private int ArrangeTabs(int width, int y)
        {
            int tabsW = Math.Min(width, Tabs.PreferredWidth);
            Tabs.SetBounds(0, y, tabsW, 40);
            int countW = TextRenderer.MeasureText(_countLabel.Text ?? string.Empty, _countLabel.Font, Size.Empty,
                TextFormatFlags.NoPadding | TextFormatFlags.SingleLine).Width + 8;
            bool sameRow = width >= tabsW + 16 + countW;
            _countLabel.TextAlign = sameRow ? ContentAlignment.MiddleRight : ContentAlignment.MiddleLeft;
            if (sameRow)
            {
                _countLabel.SetBounds(tabsW + 12, y, Math.Max(40, width - tabsW - 12), 40);
                return 40;
            }

            _countLabel.SetBounds(0, y + 44, width, 22);
            return 66;
        }

        private int ArrangeCard(int width)
        {
            const int pad = 16;
            int inner = Math.Max(200, width - pad * 2);
            int y = pad;
            int rowH = 46;
            int gap = 10;

            bool oneRow = inner >= 240 + 12 + 170 + 12 + 170;
            bool twoCombos = inner >= 170 + 12 + 170;
            if (oneRow)
            {
                int comboW = Math.Max(170, Math.Min(220, (inner - 240) / 3));
                int searchW = inner - comboW * 2 - 24;
                SearchBar.SetBounds(pad, y, searchW, rowH);
                ActionFilter.SetBounds(pad + searchW + 12, y + 2, comboW, 42);
                TableFilter.SetBounds(pad + searchW + 12 + comboW + 12, y + 2, comboW, 42);
                y += rowH;
            }
            else if (twoCombos)
            {
                SearchBar.SetBounds(pad, y, inner, rowH);
                y += rowH + gap;
                int comboW = (inner - 12) / 2;
                ActionFilter.SetBounds(pad, y + 2, comboW, 42);
                TableFilter.SetBounds(pad + comboW + 12, y + 2, comboW, 42);
                y += rowH;
            }
            else
            {
                SearchBar.SetBounds(pad, y, inner, rowH);
                y += rowH + gap;
                ActionFilter.SetBounds(pad, y + 2, inner, 42);
                y += rowH + gap;
                TableFilter.SetBounds(pad, y + 2, inner, 42);
                y += rowH;
            }

            y += 14;
            int dateH = DateRange.Arrange(inner);
            DateRange.SetBounds(pad, y, inner, dateH);
            y += dateH + pad;
            return y;
        }

        private static StatCard CreateStat(IconChar icon, Color color, Color background)
        {
            return new StatCard
            {
                ValueText = "0",
                TitleText = string.Empty,
                TrendText = string.Empty,
                Icon = icon,
                IconColor = color,
                IconBackground = background,
                TopBorderColor = color
            };
        }

        private static FilterComboBox CreateCombo()
        {
            return new FilterComboBox
            {
                IntegralHeight = false,
                Margin = Padding.Empty
            };
        }

        private void PaintCard(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = new Rectangle(0, 0, _card.Width - 1, _card.Height - 1);
            if (rect.Width <= 0 || rect.Height <= 0) return;
            using (var path = AppRadius.GetRoundedPath(rect, AppRadius.Large))
            using (var brush = new SolidBrush(AppColors.White))
            using (var pen = new Pen(AppColors.Border))
            {
                g.FillPath(brush, path);
                g.DrawPath(pen, path);
            }
        }

        private void OnThemeChanged(object sender, EventArgs e)
        {
            _card.BackColor = AppColors.White;
            _countLabel.ForeColor = AppColors.TextMuted;
            _statTotal.IconColor = AppColors.Accent;
            _statTotal.IconBackground = AppColors.AccentBgSoft;
            _statTotal.TopBorderColor = AppColors.Accent;
            _card.Invalidate();
            Invalidate(true);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                ThemeManager.Instance.ThemeChanged -= OnThemeChanged;
            base.Dispose(disposing);
        }
    }
}
