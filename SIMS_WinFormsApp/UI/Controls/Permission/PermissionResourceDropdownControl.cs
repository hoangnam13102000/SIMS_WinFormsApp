using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Models.DTOs.Permission;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.UI.Controls.Permission
{
    public sealed class PermissionResourceDropdownControl : VerticalStackPanel
    {
        private const int HeaderHeight = 80;
        private const int SummaryHostWidth = 190;

        /// <summary>Người dùng đổi trạng thái 1 nấc quyền bên trong resource này.</summary>
        public event EventHandler<PermissionToggledEventArgs> Toggled;

        private readonly PermissionResourceViewModel _model;
        private readonly VerticalStackPanel _body;
        private readonly Label _summaryLabel;
        private readonly IconPictureBox _chevron;
        private bool _open;

        public PermissionResourceDropdownControl(PermissionResourceViewModel model, bool isAdminRole)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));

            Margin = new Padding(0, 0, 0, 8);
            Padding = new Padding(1);
            BackColor = Color.Transparent;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            _summaryLabel = new Label
            {
                AutoSize = true,
                BackColor = Color.Transparent,
                Font = AppFonts.Small,
                ForeColor = AppColors.TextMuted,
                TextAlign = ContentAlignment.MiddleRight
            };
            _chevron = new IconPictureBox
            {
                IconChar = IconChar.ChevronDown,
                IconColor = AppColors.TextMuted,
                IconSize = 12,
                Size = new Size(14, 14),
                BackColor = Color.Transparent
            };

            var header = BuildHeader();
            header.Margin = new Padding(0);
            Controls.Add(header);

            _body = new VerticalStackPanel
            {
                BackColor = AppColors.BgLighter,
                Padding = new Padding(10, 2, 10, 4),
                Margin = new Padding(0),
                Visible = false
            };
            foreach (var tier in model.Tiers)
            {
                var row = new PermissionToggleRowControl(tier, isAdminRole, nested: true) { Margin = new Padding(0) };
                row.Toggled += (s, isChecked) =>
                {
                    Toggled?.Invoke(this, new PermissionToggledEventArgs(row.Permission, isChecked));
                    RefreshSummary();
                };
                _body.Controls.Add(row);
            }
            Controls.Add(_body);

            RefreshSummary();
        }

        private Panel BuildHeader()
        {
            var header = new Panel
            {
                Height = HeaderHeight,
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                Padding = new Padding(12, 8, 12, 8)
            };

            var summaryHost = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent };
            _summaryLabel.Location = new Point(0, (HeaderHeight - 16 - _summaryLabel.Height) / 2);
            _chevron.Location = new Point(SummaryHostWidth - _chevron.Width - 8, (HeaderHeight - 16 - _chevron.Height) / 2);
            summaryHost.Controls.Add(_chevron);
            summaryHost.Controls.Add(_summaryLabel);
            // Bố cục hai cột ngăn vùng tên/mô tả chồng lên vùng trạng thái.
            var headerLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Color.Transparent,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };
            headerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            headerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, SummaryHostWidth));
            headerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

            var textCol = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent };
            var nameLabel = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Top,
                Height = 24,
                Location = new Point(0, 2),
                BackColor = Color.Transparent,
                Font = AppFonts.BodyBold,
                ForeColor = AppColors.TextPrimary,
                Text = _model.Name ?? string.Empty,
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
                Text = _model.Description ?? string.Empty,
                TextAlign = ContentAlignment.TopLeft,
                Padding = new Padding(0, 0, 0, 3),
                UseCompatibleTextRendering = true,
                AutoEllipsis = true
            };
            textCol.Controls.Add(descLabel);
            textCol.Controls.Add(nameLabel);
            headerLayout.Controls.Add(textCol, 0, 0);
            headerLayout.Controls.Add(summaryHost, 1, 0);
            header.Controls.Add(headerLayout);

            header.Click += (s, e) => ToggleOpen();
            headerLayout.Click += (s, e) => ToggleOpen();
            summaryHost.Click += (s, e) => ToggleOpen();
            textCol.Click += (s, e) => ToggleOpen();
            nameLabel.Click += (s, e) => ToggleOpen();
            descLabel.Click += (s, e) => ToggleOpen();

            return header;
        }

        private void ToggleOpen()
        {
            SuspendLayout();
            _open = !_open;
            _body.Visible = _open;
            _chevron.IconChar = _open ? IconChar.ChevronUp : IconChar.ChevronDown;
            ResumeLayout(false);

            var stack = Parent as VerticalStackPanel;
            if (stack != null)
            {
                stack.SuspendLayout();
                stack.PerformLayout();
                stack.ResumeLayout(true);
            }

            var scrollHost = stack?.Parent;
            scrollHost?.PerformLayout();
        }

        private void RefreshSummary()
        {
            var currentOn = _body.Controls.OfType<PermissionToggleRowControl>()
                .Zip(_model.Tiers, (row, tier) => new { row, tier })
                .Where(x => x.row.IsChecked)
                .Select(x => x.tier.Label)
                .ToList();

            _summaryLabel.Text = currentOn.Count == 0 ? "Chưa gán" : string.Join(" · ", currentOn);
            _summaryLabel.Location = new Point(
                SummaryHostWidth - _chevron.Width - 8 - 8 - _summaryLabel.Width,
                (HeaderHeight - 16 - _summaryLabel.Height) / 2);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (var pen = new Pen(AppColors.Border, 1f))
            {
                g.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
            }
        }
    }
}