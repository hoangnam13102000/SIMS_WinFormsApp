using System;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Models.DTOs.Permission;
using SIMS_WinFormsApp.Models.Permission;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls.Permission
{
    public sealed class PermissionToggleRowControl : Panel
    {
        private const int ControlHostWidth = 92;

        public AppPermission Permission { get; }

        /// <summary>Trạng thái bật/tắt hiện tại của dòng này (Admin/khoá luôn trả về true).</summary>
        public bool IsChecked => _isLocked || (_toggle != null && _toggle.Checked);

        /// <summary>Người dùng đổi trạng thái công tắc (tham số: đang bật hay không). Không phát khi bị khoá (Admin).</summary>
        public event EventHandler<bool> Toggled;

        private readonly bool _nested;
        private readonly bool _isLocked;
        private ToggleSwitchControl _toggle;

        public PermissionToggleRowControl(PermissionToggleViewModel model, bool isLocked, bool nested = false)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            Permission = model.Permission;
            _nested = nested;
            _isLocked = isLocked;

            int rowHeight = nested ? 72 : 84;

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);

            Dock = DockStyle.Top;
            Height = rowHeight;
            BackColor = Color.Transparent;
            Padding = nested ? new Padding(4, 8, 4, 8) : new Padding(0, 10, 0, 10);

            var rowLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Color.Transparent,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };
            rowLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            rowLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, ControlHostWidth));
            rowLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

            var controlHost = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent };
            Control control = isLocked ? BuildLockedBadge() : BuildToggle(model.IsChecked);
            control.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            control.Location = new Point(
                Math.Max(0, controlHost.Width - control.Width - 4),
                Math.Max(0, (rowHeight - Padding.Top - Padding.Bottom - control.Height) / 2));
            controlHost.Controls.Add(control);
            controlHost.Resize += (s, e) => control.Location = new Point(
                Math.Max(0, controlHost.ClientSize.Width - control.Width - 4),
                Math.Max(0, (controlHost.ClientSize.Height - control.Height) / 2));

            // Location thủ công (không Dock=Top) cho cặp tên/mô tả - xem giải thích quy ước
            // trong RoleListItemControl.
            var textCol = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent };
            var nameLabel = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Top,
                Height = 24,
                BackColor = Color.Transparent,
                Font = AppFonts.BodyBold,
                ForeColor = AppColors.TextPrimary,
                Text = model.Label ?? string.Empty,
                TextAlign = ContentAlignment.MiddleLeft,
                UseCompatibleTextRendering = true,
                AutoEllipsis = true
            };
            var descLabel = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Font = AppFonts.Small,
                ForeColor = AppColors.TextMuted,
                Text = model.Description ?? string.Empty,
                TextAlign = ContentAlignment.TopLeft,
                Padding = new Padding(0, 0, 0, 3),
                UseCompatibleTextRendering = true,
                AutoEllipsis = true
            };
            textCol.Controls.Add(descLabel);
            textCol.Controls.Add(nameLabel);
            rowLayout.Controls.Add(textCol, 0, 0);
            rowLayout.Controls.Add(controlHost, 1, 0);
            Controls.Add(rowLayout);
        }

        private Control BuildToggle(bool isChecked)
        {
            _toggle = new ToggleSwitchControl { Checked = isChecked };
            _toggle.CheckedChanged += (s, e) => Toggled?.Invoke(this, _toggle.Checked);
            return _toggle;
        }

        private static Control BuildLockedBadge()
        {
            var panel = new FlowLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = Color.Transparent
            };
            var icon = new IconPictureBox
            {
                IconChar = IconChar.CircleCheck,
                IconColor = AppColors.Success,
                IconSize = 13,
                Size = new Size(14, 14),
                BackColor = Color.Transparent,
                Margin = new Padding(0, 2, 4, 0)
            };
            var label = new Label
            {
                AutoSize = true,
                Text = "Luôn bật",
                Font = AppFonts.SmallBold,
                ForeColor = AppColors.Success,
                BackColor = Color.Transparent,
                Margin = new Padding(0)
            };
            panel.Controls.Add(icon);
            panel.Controls.Add(label);
            panel.Size = panel.PreferredSize;
            return panel;
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            pevent.Graphics.Clear(PermissionUiHelpers.GetEffectiveBackColor(this));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Đường kẻ mảnh phân cách giữa các dòng quyền liền kề (tương đương
            // JSeparator giữa các buildPermissionRow/buildNestedToggleRow bên Java).
            using (var pen = new Pen(AppColors.Border, 1f))
            {
                e.Graphics.DrawLine(pen, 0, Height - 1, Width, Height - 1);
            }
        }
    }
}