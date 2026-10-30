using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Views.Shell
{
    public partial class SettingsQuickPopup : Form
    {
        public event EventHandler LanguageToggleRequested;
        public event Action<bool> NotificationsEnabledChanged;

        public SettingsQuickPopup()
        {
            InitializeComponent();
            _headerIconPanel.Paint += PaintRoundedPanel;
            _rowsPanel.Paint += PaintRoundedPanel;
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                Deactivate += (sender, args) => Hide();
                Resize += (sender, args) => SetRoundedRegion(this, 12);
                _rowsPanel.SizeChanged += (sender, args) => SetRoundedRegion(_rowsPanel, 10);
                SetRoundedRegion(this, 12);
                SetRoundedRegion(_headerIconPanel, 7);
                SetRoundedRegion(_rowsPanel, 10);
            }
            _languageButton.Click += (sender, args) => LanguageToggleRequested?.Invoke(this, EventArgs.Empty);
            _notificationsToggle.CheckedChanged += enabled =>
                NotificationsEnabledChanged?.Invoke(enabled);
            ApplyAppearance();
        }

        public void SetLabels(
            string title,
            string subtitle,
            string section,
            string language,
            string languageValue,
            bool isVietnamese,
            string notifications,
            string notificationsHint,
            string footer)
        {
            _titleLabel.Text = title;
            _subtitleLabel.Text = subtitle;
            _sectionLabel.Text = section;
            _languageLabel.Text = language;
            _languageValue.Text = languageValue;
            _languageButton.Text = isVietnamese ? "VI" : "EN";
            _notificationsLabel.Text = notifications;
            _notificationsHint.Text = notificationsHint;
            _languageButton.AccessibleName = language;
            _languageButton.AccessibleDescription = languageValue;
            _notificationsToggle.AccessibleName = notifications;
            _notificationsToggle.AccessibleDescription = notificationsHint;
            _footerLabel.Text = footer;
        }

        public bool NotificationsEnabled
        {
            get => _notificationsToggle.Checked;
            set => _notificationsToggle.Checked = value;
        }

        public void ApplyAppearance()
        {
            BackColor = AppColors.Border;
            _contentPanel.BackColor = AppColors.White;
            _rowsPanel.BackColor = AppColors.BgLight;
            _separator.BackColor = AppColors.Border;
            _headerIconPanel.BackColor = AppColors.AccentBgSoft;
            _headerIcon.IconColor = AppColors.Accent;
            _languageIcon.IconColor = AppColors.Accent;
            _notificationsIcon.IconColor = AppColors.Accent;

            _titleLabel.ForeColor = AppColors.TextTitle;
            _subtitleLabel.ForeColor = AppColors.TextMuted;
            _sectionLabel.ForeColor = AppColors.TextMuted;
            _languageLabel.ForeColor = AppColors.TextPrimary;
            _languageValue.ForeColor = AppColors.TextMuted;
            _notificationsLabel.ForeColor = AppColors.TextPrimary;
            _notificationsHint.ForeColor = AppColors.TextMuted;
            _footerLabel.ForeColor = AppColors.TextMuted;
            _languageButton.BackColor = AppColors.White;
            _languageButton.ForeColor = AppColors.Accent;
            _languageButton.FlatAppearance.BorderColor = AppColors.Border;
            _notificationsToggle.ApplyAppearance();
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var bounds = new Rectangle(0, 0, Width - 1, Height - 1);
            using (var path = CreateRoundedPath(bounds, 12))
            using (var pen = new Pen(AppColors.Border))
                e.Graphics.DrawPath(pen, path);
        }

        private static void PaintRoundedPanel(object sender, PaintEventArgs e)
        {
            var panel = (Panel)sender;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var bounds = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);
            using (var path = CreateRoundedPath(bounds, 10))
            using (var brush = new SolidBrush(panel.BackColor))
                e.Graphics.FillPath(brush, path);
        }

        private static GraphicsPath CreateRoundedPath(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            var path = new GraphicsPath();
            path.AddArc(bounds.Left, bounds.Top, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Top, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.Left, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }

        private static void SetRoundedRegion(Control control, int radius)
        {
            if (control.Width <= 0 || control.Height <= 0)
                return;

            Region previousRegion = control.Region;
            using (var path = CreateRoundedPath(
                new Rectangle(0, 0, control.Width, control.Height), radius))
                control.Region = new Region(path);
            previousRegion?.Dispose();
        }

        private void _contentPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void _notificationsRow_Paint(object sender, PaintEventArgs e)
        {

        }
    }

    internal sealed class SettingsToggleSwitch : Control
    {
        public event Action<bool> CheckedChanged;

        public SettingsToggleSwitch()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.Selectable, true);
            TabStop = true;
            AccessibleRole = AccessibleRole.CheckButton;
        }

        public bool Checked
        {
            get => _checked;
            set
            {
                if (value == _checked)
                    return;

                _checked = value;
                Invalidate();
                CheckedChanged?.Invoke(value);
            }
        }

        private bool _checked = true;

        public void ApplyAppearance()
        {
            Invalidate();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Checked = !Checked;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode != Keys.Space)
                return;

            Checked = !Checked;
            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Color trackColor = Checked ? AppColors.Accent : AppColors.Border;
            using (var brush = new SolidBrush(trackColor))
                e.Graphics.FillEllipse(brush, 0, 0, Width - 1, Height - 1);

            int diameter = Height - 6;
            int left = Checked ? Width - diameter - 3 : 3;
            using (var brush = new SolidBrush(Color.White))
                e.Graphics.FillEllipse(brush, left, 3, diameter, diameter);
        }
    }
}
