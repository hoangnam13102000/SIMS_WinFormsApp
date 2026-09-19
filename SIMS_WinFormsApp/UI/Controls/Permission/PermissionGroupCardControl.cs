using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SIMS_WinFormsApp.Models.DTOs.Permission;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.UI.Controls.Permission
{
    public sealed class PermissionGroupCardControl : VerticalStackPanel
    {
        public event EventHandler<PermissionToggledEventArgs> Toggled;

        public PermissionGroupCardControl(PermissionGroupViewModel group, bool isAdminRole)
        {
            if (group == null) throw new ArgumentNullException(nameof(group));

            Margin = new Padding(0, 0, 0, 12);
            Padding = new Padding(16, 14, 16, 8);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = AppColors.White;

            var title = new Label
            {
                AutoSize = true,
                BackColor = Color.Transparent,
                Font = AppFonts.Subtitle,
                ForeColor = AppColors.TextTitle,
                Text = group.Title ?? string.Empty,
                Margin = new Padding(0, 0, 0, 2)
            };
            Controls.Add(title);

            if (!string.IsNullOrWhiteSpace(group.Hint))
            {
                var hint = new Label
                {
                    AutoSize = true,
                    BackColor = Color.Transparent,
                    Font = AppFonts.Small,
                    ForeColor = AppColors.TextMuted,
                    Text = group.Hint,
                    Margin = new Padding(0, 0, 0, 8)
                };
                Controls.Add(hint);
            }

            foreach (var entry in group.Entries)
            {
                Control control = BuildEntryControl(entry, isAdminRole);
                control.Margin = new Padding(0, 0, 0, 4);
                Controls.Add(control);
            }
        }

        private Control BuildEntryControl(PermissionGroupEntryViewModel entry, bool isAdminRole)
        {
            switch (entry)
            {
                case PermissionToggleEntryViewModel toggleEntry:
                    var row = new PermissionToggleRowControl(toggleEntry.Toggle, isAdminRole);
                    row.Toggled += (s, isChecked) => Toggled?.Invoke(this,
                        new PermissionToggledEventArgs(row.Permission, isChecked));
                    return row;

                case PermissionResourceEntryViewModel resourceEntry:
                    var dropdown = new PermissionResourceDropdownControl(resourceEntry.Resource, isAdminRole);
                    dropdown.Toggled += (s, args) => Toggled?.Invoke(this, args);
                    return dropdown;

                default:
                    throw new NotSupportedException("Loại entry quyền không được hỗ trợ: " + entry.GetType());
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (var path = AppRadius.GetRoundedPath(new Rectangle(0, 0, Width - 1, Height - 1), AppRadius.Medium))
            using (var pen = new Pen(Color.White, 1.2f))
            {
                g.DrawPath(pen, path);
            }
        }
    }
}