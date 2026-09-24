using System;
using System.Collections.Generic;
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

            Margin = new Padding(0, 0, 0, 14);
            Padding = new Padding(18, 16, 18, 10);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = AppColors.White;

            Controls.Add(BuildTitleRow(group));

            if (!string.IsNullOrWhiteSpace(group.Hint))
            {
                var hint = new Label
                {
                    AutoSize = true,
                    BackColor = Color.Transparent,
                    Font = AppFonts.Small,
                    ForeColor = AppColors.TextMuted,
                    Text = group.Hint,
                    UseMnemonic = false,
                    Margin = new Padding(0, 0, 0, 10)
                };
                Controls.Add(hint);
            }

            foreach (var entry in group.Entries)
            {
                Control control = BuildEntryControl(entry, isAdminRole);
                control.Margin = new Padding(0, 0, 0, 6);
                Controls.Add(control);
            }
        }

        private Control BuildTitleRow(PermissionGroupViewModel group)
        {
            var title = new Label
            {
                AutoSize = true,
                BackColor = Color.Transparent,
                Font = AppFonts.Subtitle,
                ForeColor = AppColors.TextTitle,
                Location = new Point(0, 0),
                Text = group.Title ?? string.Empty,
                UseMnemonic = false
            };

            int total = CountTotalPermissions(group.Entries);
            int on = CountCheckedPermissions(group.Entries);
            bool hasCountable = total > 0;
            bool isFull = hasCountable && on == total;

            PillBadgeLabel countBadge = null;
            if (hasCountable)
            {
                string badgeText = on + "/" + total + " quyền";
                countBadge = new PillBadgeLabel
                {
                    Font = AppFonts.SmallBold,
                    Text = badgeText,
                    ForeColor = on == 0 ? AppColors.TextMuted : (isFull ? AppColors.Success : AppColors.Accent),
                    PillBackColor = on == 0 ? AppColors.BgLighter : (isFull ? AppColors.SuccessBg : AppColors.AccentBgSoft),
                    Height = PermissionUiHelpers.MeasureLineHeight(AppFonts.SmallBold) + 10
                };
                countBadge.Width = PillBadgeLabel.MeasureWidth(badgeText, countBadge.Font);
            }

            int rowHeight = Math.Max(title.PreferredHeight, countBadge?.Height ?? 0);
            var row = new Panel
            {
                AutoSize = false,
                Height = rowHeight,
                BackColor = Color.Transparent,
                Margin = new Padding(0, 0, 0, 4)
            };
            row.Controls.Add(title);
            if (countBadge != null) row.Controls.Add(countBadge);

            void SyncTitleRow()
            {
                title.Location = new Point(0, Math.Max(0, (row.Height - title.Height) / 2));
                if (countBadge == null) return;
                int minLeft = title.Right + 8;
                countBadge.Location = new Point(
                    Math.Max(minLeft, row.Width - countBadge.Width),
                    Math.Max(0, (row.Height - countBadge.Height) / 2));
            }
            row.Resize += (s, e) => SyncTitleRow();
            SyncTitleRow();

            return row;
        }

        private static int CountTotalPermissions(IReadOnlyList<PermissionGroupEntryViewModel> entries)
        {
            int total = 0;
            foreach (var entry in entries)
            {
                if (entry is PermissionToggleEntryViewModel) total += 1;
                else if (entry is PermissionResourceEntryViewModel resourceEntry) total += resourceEntry.Resource.Tiers.Count;
            }
            return total;
        }

        private static int CountCheckedPermissions(IReadOnlyList<PermissionGroupEntryViewModel> entries)
        {
            int on = 0;
            foreach (var entry in entries)
            {
                if (entry is PermissionToggleEntryViewModel toggleEntry)
                {
                    if (toggleEntry.Toggle.IsChecked) on++;
                }
                else if (entry is PermissionResourceEntryViewModel resourceEntry)
                {
                    foreach (var tier in resourceEntry.Resource.Tiers)
                        if (tier.IsChecked) on++;
                }
            }
            return on;
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
            using (var pen = new Pen(AppColors.Border, 1f))
            {
                g.DrawPath(pen, path);
            }
        }
    }
}