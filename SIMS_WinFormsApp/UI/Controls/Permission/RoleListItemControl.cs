using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SIMS_WinFormsApp.Models.DTOs.Permission;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls.Permission
{
    public sealed class RoleListItemControl : Panel
    {
        public int RoleId { get; }

        public event EventHandler<int> Clicked;

        private readonly bool _isSelected;
        private bool _isHover;

        public RoleListItemControl(RolePermissionRoleRowDto role)
        {
            if (role == null) throw new ArgumentNullException(nameof(role));

            RoleId = role.RoleId;
            _isSelected = role.IsSelected;

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);

            Cursor = Cursors.Hand;
            Dock = DockStyle.Top;
            Height = 72;
            Margin = new Padding(0, 0, 0, 6);
            Padding = new Padding(10, 8, 10, 8);
            BackColor = Color.Transparent;

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
            rowLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 92f));
            rowLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

            var countLabel = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight,
                Font = AppFonts.Small,
                ForeColor = AppColors.TextMuted,
                BackColor = Color.Transparent,
                Text = role.PermissionCount + " quyền"
            };
            var textCol = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent };
            var nameLabel = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Top,
                Height = 28,
                Location = new Point(0, 1),
                BackColor = Color.Transparent,
                Font = _isSelected ? AppFonts.BodyBold : AppFonts.Body,
                ForeColor = _isSelected ? AppColors.Accent : AppColors.TextPrimary,
                Text = string.IsNullOrEmpty(role.RoleName) ? role.RoleCode : role.RoleName,
                TextAlign = ContentAlignment.MiddleLeft
            };
            var codeLabel = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Font = AppFonts.Small,
                ForeColor = AppColors.TextMuted,
                Text = role.RoleCode ?? string.Empty,
                TextAlign = ContentAlignment.TopLeft,
                Padding = new Padding(0, 2, 0, 0)
            };
            textCol.Controls.Add(codeLabel);
            textCol.Controls.Add(nameLabel);
            rowLayout.Controls.Add(textCol, 0, 0);
            rowLayout.Controls.Add(countLabel, 1, 0);
            Controls.Add(rowLayout);

            MouseEnter += (s, e) => { _isHover = true; Invalidate(); };
            MouseLeave += (s, e) => { _isHover = false; Invalidate(); };
            EventHandler onClick = (s, e) => Clicked?.Invoke(this, RoleId);
            Click += onClick;
            countLabel.Click += onClick;
            textCol.Click += onClick;
            nameLabel.Click += onClick;
            codeLabel.Click += onClick;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            if (Parent != null)
            {
                g.Clear(PermissionUiHelpers.GetEffectiveBackColor(this));
            }

            if (_isSelected)
            {
                using (var path = AppRadius.GetRoundedPath(new Rectangle(0, 0, Width, Height), AppRadius.Small))
                using (var brush = new SolidBrush(AppColors.AccentSoft))
                {
                    g.FillPath(brush, path);
                }
            }
            else if (_isHover)
            {
                using (var path = AppRadius.GetRoundedPath(new Rectangle(0, 0, Width, Height), AppRadius.Small))
                using (var brush = new SolidBrush(AppColors.BgLighter))
                {
                    g.FillPath(brush, path);
                }
            }

            base.OnPaint(e);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // Nền được tự vẽ trong OnPaint (bo góc) - bỏ qua nền mặc định để tránh vẽ đè hình chữ nhật vuông góc.
        }
    }
}