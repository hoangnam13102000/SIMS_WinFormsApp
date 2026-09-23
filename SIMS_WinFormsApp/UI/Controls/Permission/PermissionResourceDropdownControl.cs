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
    /// <summary>
    /// 1 dòng tài nguyên có thể mở/đóng (vd "Tài khoản &amp; nhân viên") bên trong 1 nhóm
    /// quyền. Bấm vào phần tiêu đề sẽ xổ ra danh sách các nấc quyền (Xem/Sửa/Quản lý đầy đủ...).
    ///
    /// THIẾT KẾ LẠI: viền bo góc (thay vì hình chữ nhật vuông góc trước đây) để đồng bộ với
    /// ngôn ngữ thiết kế bo tròn dùng xuyên suốt trang (card vai trò, card nhóm quyền...).
    /// </summary>
    public sealed class PermissionResourceDropdownControl : VerticalStackPanel
    {
        private const int HeaderHeight = 88;
        private const int SummaryHostWidth = 180;

        /// <summary>Người dùng đổi trạng thái 1 nấc quyền bên trong resource này.</summary>
        public event EventHandler<PermissionToggledEventArgs> Toggled;

        private readonly PermissionResourceViewModel _model;
        private readonly VerticalStackPanel _body;
        private readonly PillBadgeLabel _summaryBadge;
        private readonly IconPictureBox _chevron;
        private bool _open;
        private Action _syncSummaryHost = () => { };
        private readonly ToolTip _headerToolTip = new ToolTip { InitialDelay = 400, ReshowDelay = 100, AutoPopDelay = 4000 };

        public PermissionResourceDropdownControl(PermissionResourceViewModel model, bool isAdminRole)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));

            Margin = new Padding(0, 0, 0, 10);
            Padding = new Padding(1);
            BackColor = Color.Transparent;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            _summaryBadge = new PillBadgeLabel
            {
                Font = AppFonts.SmallBold,
                Height = PermissionUiHelpers.MeasureLineHeight(AppFonts.SmallBold) + 10
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
                Padding = new Padding(14, 8, 14, 8)
            };

            var summaryHost = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent };
            summaryHost.Controls.Add(_chevron);
            summaryHost.Controls.Add(_summaryBadge);
            void SyncSummaryHost()
            {
                _chevron.Location = new Point(
                    summaryHost.Width - _chevron.Width - 8,
                    (summaryHost.Height - _chevron.Height) / 2);
                _summaryBadge.Location = new Point(
                    Math.Max(0, _chevron.Left - 8 - _summaryBadge.Width),
                    (summaryHost.Height - _summaryBadge.Height) / 2);
            }
            summaryHost.Resize += (s, e) => SyncSummaryHost();
            _syncSummaryHost = SyncSummaryHost;

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
                Height = 28,
                Location = new Point(0, 2),
                BackColor = Color.Transparent,
                Font = AppFonts.BodyBold,
                ForeColor = AppColors.TextPrimary,
                Text = _model.Name ?? string.Empty,
                TextAlign = ContentAlignment.MiddleLeft,
                UseCompatibleTextRendering = false,
                // Tên resource lấy từ PermissionDisplayLayout có thể chứa "&" (vd "Tài khoản &
                // nhân viên") - Label mặc định coi "&" là mnemonic và âm thầm xoá khỏi hiển thị.
                UseMnemonic = false,
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
                UseCompatibleTextRendering = false,
                UseMnemonic = false,
                AutoEllipsis = true
            };
            textCol.Controls.Add(descLabel);
            textCol.Controls.Add(nameLabel);
            headerLayout.Controls.Add(textCol, 0, 0);
            headerLayout.Controls.Add(summaryHost, 1, 0);
            header.Controls.Add(headerLayout);

            if (!string.IsNullOrWhiteSpace(_model.Name)) _headerToolTip.SetToolTip(nameLabel, _model.Name);
            if (!string.IsNullOrWhiteSpace(_model.Description)) _headerToolTip.SetToolTip(descLabel, _model.Description);

            header.Click += (s, e) => ToggleOpen();
            headerLayout.Click += (s, e) => ToggleOpen();
            summaryHost.Click += (s, e) => ToggleOpen();
            textCol.Click += (s, e) => ToggleOpen();
            nameLabel.Click += (s, e) => ToggleOpen();
            descLabel.Click += (s, e) => ToggleOpen();

            SyncSummaryHost();
            return header;
        }

        private void ToggleOpen()
        {
            SuspendLayout();
            _open = !_open;
            _body.Visible = _open;
            _chevron.IconChar = _open ? IconChar.ChevronUp : IconChar.ChevronDown;
            _chevron.IconColor = _open ? AppColors.Accent : AppColors.TextMuted;
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
            Invalidate();
        }

        private void RefreshSummary()
        {
            int total = _model.Tiers.Count;
            int on = _body.Controls.OfType<PermissionToggleRowControl>().Count(row => row.IsChecked);

            string text = on == 0 ? "Chưa gán" : on + "/" + total + " quyền";
            bool isFull = total > 0 && on == total;

            _summaryBadge.Text = text;
            _summaryBadge.ForeColor = on == 0 ? AppColors.TextMuted : (isFull ? AppColors.Success : AppColors.Accent);
            _summaryBadge.PillBackColor = on == 0 ? AppColors.BgLighter : (isFull ? AppColors.SuccessBg : AppColors.AccentBgSoft);
            _summaryBadge.Width = PillBadgeLabel.MeasureWidth(text, _summaryBadge.Font) + 12;

            _syncSummaryHost();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using (var path = AppRadius.GetRoundedPath(rect, AppRadius.Medium))
            using (var pen = new Pen(_open ? AppColors.Accent : AppColors.Border, 1f))
            {
                g.DrawPath(pen, path);
            }
        }
    }
}