using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Infrastructure;
using SIMS_WinFormsApp.UI.I18n;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{
    public class SettingsPopupControl : PopupFormBase
    {
        private const string PrefKeyNotificationSound = "sims.notification.soundEnabled";
        private const string PrefKeyHideNewOrderNotification = "sims.notification.hideNewOrder";

        // Tăng kích thước popup để dễ nhìn hơn. Dùng màu nền elevated (BgLighter)
        // thay vì AppColors.White (gần trùng PageBg ở dark mode → khó phân biệt).
        private const int PopupWidth = 380;
        private const int RowHeight = 48;
        private const int SwatchSize = 36;
        private const int ContentPadding = 20;

        private readonly Panel _host;

        private readonly List<SettingsOptionRowControl> _themeRows = new List<SettingsOptionRowControl>();
        private readonly List<AccentSwatchControl> _accentSwatches = new List<AccentSwatchControl>();
        private readonly List<SettingsOptionRowControl> _languageRows = new List<SettingsOptionRowControl>();
        private readonly List<Label> _sectionLabels = new List<Label>();
        private SettingsToggleRowControl _soundToggleRow;
        private SettingsToggleRowControl _hideOrderToggleRow;

        public SettingsPopupControl()
        {
            Width = PopupWidth;
            // BgLighter: dark ≈ (38,42,53) nổi rõ trên PageBg (18,20,25);
            // light ≈ (241,245,249) nhẹ nhàng trên nền trắng.
            BackColor = AppColors.BgLighter;

            _host = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = AppColors.BgLighter,
                // Padding chỉ có ý nghĩa với Docked controls; vì ta dùng absolute layout
                // nên sẽ tự offset Location trong BuildContent.
                Padding = new Padding(0),
                AutoScroll = false
            };
            Controls.Add(_host);

            BuildContent();
        }

        // ===================== XÂY NỘI DUNG =====================

        private void BuildContent()
        {
            int y = ContentPadding;          // margin trên
            int contentW = PopupWidth - ContentPadding * 2;
            int x = ContentPadding;          // margin trái

            y = AddSectionLabel("settings.section.appearance", y, x, contentW);
            y = AddThemeRows(y, x, contentW);

            y = AddSeparator(y, x, contentW);
            y = AddSectionLabel("settings.section.accent", y, x, contentW);
            y = AddAccentSwatches(y, x);

            y = AddSeparator(y, x, contentW);
            y = AddSectionLabel("settings.section.notification", y, x, contentW);
            y = AddNotificationToggles(y, x, contentW);

            y = AddSeparator(y, x, contentW);
            y = AddSectionLabel("settings.section.language", y, x, contentW);
            y = AddLanguageRows(y, x, contentW);

            Height = y + ContentPadding;   // margin dưới
            ApplyRoundedRegion();
        }

        private int AddSectionLabel(string i18nKey, int y, int x, int contentW)
        {
            var label = new Label
            {
                AutoSize = false,
                Text = Lang.Get(i18nKey),
                Tag = i18nKey,
                Font = AppFonts.SmallBold,
                ForeColor = AppColors.TextMuted,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleLeft,
                Location = new Point(x, y),
                Size = new Size(contentW, 22)
            };
            _host.Controls.Add(label);
            _sectionLabels.Add(label);
            return y + label.Height + 8;
        }

        private int AddSeparator(int y, int x, int contentW)
        {
            int sepY = y + 8;
            var sep = new Panel
            {
                Location = new Point(x, sepY),
                Size = new Size(contentW, 1),
                BackColor = AppColors.Border
            };
            _host.Controls.Add(sep);
            return sepY + 1 + 12;
        }

        private int AddThemeRows(int y, int x, int contentW)
        {
            var lightRow = new SettingsOptionRowControl("light", Lang.Get("settings.theme.light"), IconChar.Sun)
            {
                Location = new Point(x, y),
                Size = new Size(contentW, RowHeight)
            };
            var darkRow = new SettingsOptionRowControl("dark", Lang.Get("settings.theme.dark"), IconChar.Moon)
            {
                Location = new Point(x, y + RowHeight + 4),
                Size = new Size(contentW, RowHeight)
            };

            lightRow.Selected = !ThemeManager.Instance.IsDark;
            darkRow.Selected = ThemeManager.Instance.IsDark;

            lightRow.Clicked += (_, __) => SelectTheme(ThemeMode.Light, lightRow, darkRow);
            darkRow.Clicked += (_, __) => SelectTheme(ThemeMode.Dark, lightRow, darkRow);

            _themeRows.Add(lightRow);
            _themeRows.Add(darkRow);
            _host.Controls.Add(lightRow);
            _host.Controls.Add(darkRow);

            return y + RowHeight + 4 + RowHeight;
        }

        private void SelectTheme(ThemeMode mode, SettingsOptionRowControl lightRow, SettingsOptionRowControl darkRow)
        {
            ThemeManager.Instance.SetMode(mode);
            lightRow.Selected = mode == ThemeMode.Light;
            darkRow.Selected = mode == ThemeMode.Dark;
            RefreshColorsAfterThemeChange();
        }

        private int AddAccentSwatches(int y, int startX)
        {
            const int gap = 14;

            var accents = new[]
            {
                AccentColor.Blue, AccentColor.Purple, AccentColor.Green,
                AccentColor.Orange, AccentColor.Rose, AccentColor.Teal
            };

            int x = startX;
            foreach (var accent in accents)
            {
                var swatch = new AccentSwatchControl(accent)
                {
                    Location = new Point(x, y),
                    Size = new Size(SwatchSize, SwatchSize),
                    Selected = accent.Name == AppColors.CurrentAccent.Name
                };
                swatch.Clicked += (_, __) => SelectAccent(accent);
                _accentSwatches.Add(swatch);
                _host.Controls.Add(swatch);
                x += SwatchSize + gap;
            }

            return y + SwatchSize;
        }

        private void SelectAccent(AccentColor accent)
        {
            ThemeManager.Instance.SetAccent(accent);
            foreach (var swatch in _accentSwatches)
                swatch.Selected = swatch.ColorName == accent.Name;

            RefreshColorsAfterThemeChange();
        }

        private int AddNotificationToggles(int y, int x, int contentW)
        {
            bool soundOn = AppSettingsStore.Get(PrefKeyNotificationSound, "true") != "false";
            bool hideNewOrder = AppSettingsStore.Get(PrefKeyHideNewOrderNotification, "false") == "true";

            var soundRow = new SettingsToggleRowControl(Lang.Get("settings.notification.sound"), IconChar.VolumeHigh, soundOn)
            {
                Location = new Point(x, y),
                Size = new Size(contentW, RowHeight)
            };
            soundRow.Toggled += (_, isOn) => AppSettingsStore.Set(PrefKeyNotificationSound, isOn ? "true" : "false");

            var hideOrderRow = new SettingsToggleRowControl(Lang.Get("settings.notification.hideNewOrder"), IconChar.BellSlash, hideNewOrder)
            {
                Location = new Point(x, y + RowHeight + 4),
                Size = new Size(contentW, RowHeight)
            };
            hideOrderRow.Toggled += (_, isOn) => AppSettingsStore.Set(PrefKeyHideNewOrderNotification, isOn ? "true" : "false");

            _soundToggleRow = soundRow;
            _hideOrderToggleRow = hideOrderRow;

            _host.Controls.Add(soundRow);
            _host.Controls.Add(hideOrderRow);

            return y + RowHeight + 4 + RowHeight;
        }

        private int AddLanguageRows(int y, int x, int contentW)
        {
            var viRow = new SettingsOptionRowControl("vi", Lang.Get("settings.language.vi"), IconChar.Globe)
            {
                Location = new Point(x, y),
                Size = new Size(contentW, RowHeight)
            };
            var enRow = new SettingsOptionRowControl("en", Lang.Get("settings.language.en"), IconChar.Globe)
            {
                Location = new Point(x, y + RowHeight + 4),
                Size = new Size(contentW, RowHeight)
            };

            viRow.Selected = LanguageManager.Instance.IsVietnamese;
            enRow.Selected = LanguageManager.Instance.IsEnglish;

            viRow.Clicked += (_, __) => SelectLanguage(new System.Globalization.CultureInfo("vi"), viRow, enRow);
            enRow.Clicked += (_, __) => SelectLanguage(new System.Globalization.CultureInfo("en"), viRow, enRow);

            _languageRows.Add(viRow);
            _languageRows.Add(enRow);
            _host.Controls.Add(viRow);
            _host.Controls.Add(enRow);

            return y + RowHeight + 4 + RowHeight;
        }

        private void SelectLanguage(System.Globalization.CultureInfo culture, SettingsOptionRowControl viRow, SettingsOptionRowControl enRow)
        {
            LanguageManager.Instance.SetLocale(culture);
            viRow.Selected = LanguageManager.Instance.IsVietnamese;
            enRow.Selected = LanguageManager.Instance.IsEnglish;

            RefreshAllTexts();
        }
        private void RefreshAllTexts()
        {
            foreach (var label in _sectionLabels)
            {
                if (label.Tag is string key)
                    label.Text = Lang.Get(key);
            }

            foreach (var row in _themeRows)
                row.SetText(Lang.Get(row.Key == "light" ? "settings.theme.light" : "settings.theme.dark"));

            foreach (var row in _languageRows)
                row.SetText(Lang.Get(row.Key == "vi" ? "settings.language.vi" : "settings.language.en"));

            _soundToggleRow?.SetText(Lang.Get("settings.notification.sound"));
            _hideOrderToggleRow?.SetText(Lang.Get("settings.notification.hideNewOrder"));
        }

        private void RefreshColorsAfterThemeChange()
        {
            BackColor = AppColors.BgLighter;
            _host.BackColor = AppColors.BgLighter;

            // Cập nhật màu chữ section labels theo theme mới
            foreach (var label in _sectionLabels)
            {
                label.ForeColor = AppColors.TextMuted;
            }

            // Force re-apply selected state để cập nhật Accent / text colors
            foreach (var row in _themeRows)
                row.Selected = row.Selected;
            foreach (var row in _languageRows)
                row.Selected = row.Selected;
            foreach (var swatch in _accentSwatches)
                swatch.Selected = swatch.Selected;

            Invalidate(true);
        }

        private void ApplyRoundedRegion()
        {
            if (Width <= 0 || Height <= 0) return;
            try
            {
                Region?.Dispose();
                Region = new Region(AppRadius.GetRoundedPath(new Rectangle(0, 0, Width, Height), AppRadius.Large));
            }
            catch { /* an toàn nếu control chưa handle */ }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            // Viền rõ hơn một chút để popup tách biệt khỏi nền content
            using (var pen = new Pen(AppColors.Border, 1.5f))
            using (var path = AppRadius.GetRoundedPath(new Rectangle(0, 0, Width - 1, Height - 1), AppRadius.Large))
                g.DrawPath(pen, path);
        }
    }
    internal sealed class SettingsOptionRowControl : UserControl
    {
        private readonly IconPictureBox _iconBox;
        private readonly Label _textLabel;
        private readonly IconPictureBox _checkIcon;
        private bool _hover;
        private bool _selected;

        public string Key { get; }
        public event EventHandler Clicked;

        public bool Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                _checkIcon.Visible = value;
                _textLabel.ForeColor = value ? AppColors.Accent : AppColors.TextPrimary;
                _textLabel.Font = value ? AppFonts.BodyBold : AppFonts.Body;
                _iconBox.IconColor = value ? AppColors.Accent : AppColors.TextSecondary;
                Invalidate();
            }
        }

        public SettingsOptionRowControl(string key, string text, IconChar icon)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));

            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);

            Height = 48;
            Cursor = Cursors.Hand;
            BackColor = Color.Transparent;

            _iconBox = new IconPictureBox
            {
                IconChar = icon,
                IconFont = IconFont.Solid,
                IconColor = AppColors.TextSecondary,
                IconSize = 20,
                Size = new Size(26, 26),
                Location = new Point(12, 11),
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand
            };

            _textLabel = new Label
            {
                AutoSize = false,
                Text = text,
                Font = AppFonts.Body,
                ForeColor = AppColors.TextPrimary,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleLeft,
                Location = new Point(48, 0),
                Size = new Size(200, 48),
                Cursor = Cursors.Hand
            };

            _checkIcon = new IconPictureBox
            {
                IconChar = IconChar.Check,
                IconFont = IconFont.Solid,
                IconColor = AppColors.Accent,
                IconSize = 16,
                Size = new Size(20, 20),
                BackColor = Color.Transparent,
                Visible = false,
                Cursor = Cursors.Hand
            };

            Controls.Add(_textLabel);
            Controls.Add(_iconBox);
            Controls.Add(_checkIcon);
            _checkIcon.BringToFront();

            Resize += (_, __) =>
            {
                _textLabel.Size = new Size(Math.Max(40, Width - 48 - 36), Height);
                _checkIcon.Location = new Point(Width - _checkIcon.Width - 10, (Height - _checkIcon.Height) / 2);
            };

            Click += (_, __) => Clicked?.Invoke(this, EventArgs.Empty);
            _iconBox.Click += (_, __) => Clicked?.Invoke(this, EventArgs.Empty);
            _textLabel.Click += (_, __) => Clicked?.Invoke(this, EventArgs.Empty);

            MouseEnter += OnHover;
            _iconBox.MouseEnter += OnHover;
            _textLabel.MouseEnter += OnHover;
            MouseLeave += OnLeave;
            _iconBox.MouseLeave += OnLeave;
            _textLabel.MouseLeave += OnLeave;
        }

        private void OnHover(object sender, EventArgs e)
        {
            _hover = true;
            Invalidate();
        }

        private void OnLeave(object sender, EventArgs e)
        {
            if (!ClientRectangle.Contains(PointToClient(Cursor.Position)))
            {
                _hover = false;
                Invalidate();
            }
        }
        public void SetText(string text) => _textLabel.Text = text;

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            if (_selected)
            {
                using (var brush = new SolidBrush(AppColors.AccentBgSoft))
                using (var path = AppRadius.GetRoundedPath(new Rectangle(0, 0, Width - 1, Height - 1), AppRadius.Medium))
                    g.FillPath(brush, path);
            }
            else if (_hover)
            {
                // Hover nổi hơn nền popup (BgLighter): dùng màu trung gian giữa Border và BgLighter
                // để vẫn thấy rõ ở cả dark lẫn light mode.
                Color hover = ThemeManager.Instance.IsDark
                    ? Color.FromArgb(50, 55, 68)
                    : Color.FromArgb(226, 232, 240);
                using (var brush = new SolidBrush(hover))
                using (var path = AppRadius.GetRoundedPath(new Rectangle(0, 0, Width - 1, Height - 1), AppRadius.Medium))
                    g.FillPath(brush, path);
            }

            base.OnPaint(e);
        }
    }
    internal sealed class SettingsToggleRowControl : UserControl
    {
        private readonly IconPictureBox _iconBox;
        private readonly Label _textLabel;
        private readonly ToggleSwitchControl _toggle;

        public event EventHandler<bool> Toggled;

        public bool Checked
        {
            get => _toggle.Checked;
            set => _toggle.SetCheckedSilently(value);
        }

        public SettingsToggleRowControl(string text, IconChar icon, bool initialChecked)
        {
            Height = 48;
            BackColor = Color.Transparent;

            _iconBox = new IconPictureBox
            {
                IconChar = icon,
                IconFont = IconFont.Solid,
                IconColor = AppColors.TextSecondary,
                IconSize = 20,
                Size = new Size(26, 26),
                Location = new Point(12, 11),
                BackColor = Color.Transparent
            };

            _textLabel = new Label
            {
                AutoSize = false,
                Text = text,
                Font = AppFonts.Body,
                ForeColor = AppColors.TextPrimary,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleLeft,
                Location = new Point(48, 0),
                Size = new Size(210, 48)
            };

            _toggle = new ToggleSwitchControl
            {
                Size = new Size(48, 26)
            };
            _toggle.SetCheckedSilently(initialChecked);
            _toggle.CheckedChanged += (_, __) => Toggled?.Invoke(this, _toggle.Checked);

            Controls.Add(_iconBox);
            Controls.Add(_textLabel);
            Controls.Add(_toggle);

            Resize += (_, __) =>
            {
                _textLabel.Size = new Size(Math.Max(40, Width - 48 - 64), Height);
                _toggle.Location = new Point(Width - _toggle.Width - 10, (Height - _toggle.Height) / 2);
            };
        }
        public void SetText(string text) => _textLabel.Text = text;
    }

    internal sealed class AccentSwatchControl : UserControl
    {
        private readonly AccentColor _accent;
        private readonly IconPictureBox _checkIcon;
        private bool _selected;

        public AccentColorName ColorName => _accent.Name;
        public event EventHandler Clicked;

        public bool Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                _checkIcon.Visible = value;
                Invalidate();
            }
        }

        public AccentSwatchControl(AccentColor accent)
        {
            _accent = accent ?? throw new ArgumentNullException(nameof(accent));

            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);

            Size = new Size(36, 36);
            Cursor = Cursors.Hand;
            BackColor = Color.Transparent;

            _checkIcon = new IconPictureBox
            {
                IconChar = IconChar.Check,
                IconFont = IconFont.Solid,
                IconColor = Color.White,
                IconSize = 14,
                Size = new Size(18, 18),
                BackColor = Color.Transparent,
                Visible = false,
                Cursor = Cursors.Hand
            };
            Controls.Add(_checkIcon);

            Resize += (_, __) => CenterCheckIcon();
            CenterCheckIcon();

            Click += (_, __) => Clicked?.Invoke(this, EventArgs.Empty);
            _checkIcon.Click += (_, __) => Clicked?.Invoke(this, EventArgs.Empty);
        }

        private void CenterCheckIcon()
        {
            _checkIcon.Location = new Point((Width - _checkIcon.Width) / 2, (Height - _checkIcon.Height) / 2);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            var rect = new Rectangle(2, 2, Width - 5, Height - 5);
            using (var brush = new SolidBrush(_accent.Swatch))
                g.FillEllipse(brush, rect);

            if (_selected)
            {
                using (var pen = new Pen(Color.White, 2f))
                    g.DrawEllipse(pen, rect.X + 2, rect.Y + 2, rect.Width - 4, rect.Height - 4);
                using (var ringPen = new Pen(AppColors.Accent, 2f))
                    g.DrawEllipse(ringPen, 0, 0, Width - 1, Height - 1);
            }

            base.OnPaint(e);
        }
    }
}