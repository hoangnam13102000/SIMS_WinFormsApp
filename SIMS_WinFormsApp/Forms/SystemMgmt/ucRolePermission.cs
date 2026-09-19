using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Infrastructure.Composition;
using SIMS_WinFormsApp.MVP.Presenters;
using SIMS_WinFormsApp.Models.DTOs.Permission;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Controls.Permission;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.Forms.SystemMgmt
{
    public sealed class ucRolePermission : UserControl, IRolePermissionView
    {
        public event EventHandler ViewReady;
        public event EventHandler<int> RoleSelected;
        public event EventHandler<string> RoleSearchTextChanged;
        public event EventHandler<PermissionToggledEventArgs> PermissionToggled;
        public event EventHandler SaveRequested;
        public event EventHandler ResetRequested;

        private readonly RolePermissionPresenter _presenter;

        private IconRoundedTextBox _searchBox;
        private Label _roleCountLabel;
        private VerticalStackPanel _roleListStack;
        private Label _permissionsTitleLabel;
        private VerticalStackPanel _permissionsStack;
        private PrimaryButton _btnSave;
        private PrimaryButton _btnReset;
        private Panel _emptyState;
        private Panel _busyOverlay;
        private Label _busyMessageLabel;

        public ucRolePermission()
        {
            Dock = DockStyle.Fill;
            BackColor = AppColors.PageBg;
            Padding = new Padding(20, 16, 20, 20);
            DoubleBuffered = true;

            BuildUi();

            _presenter = new RolePermissionPresenter(
                this,
                AppComposition.CreateRolePermissionRepository(),
                AppComposition.CreateRoleRepository());

            Load += (s, e) => ViewReady?.Invoke(this, EventArgs.Empty);
            Disposed += (s, e) => _presenter.Dispose();
        }

        // ==================== Dựng UI ====================

        private void BuildUi()
        {
            SuspendLayout();

            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                BackColor = AppColors.PageBg,
                Margin = new Padding(0)
            };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 132f));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

            root.Controls.Add(BuildHeaderRow(), 0, 0);
            root.Controls.Add(BuildBodyRow(), 0, 1);

            Controls.Add(root);
            Controls.Add(BuildBusyOverlay());
            ResumeLayout(false);
        }

        private Control BuildBusyOverlay()
        {
            _busyMessageLabel = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                Font = AppFonts.Subtitle,
                ForeColor = AppColors.TextTitle,
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "Đang tải phân quyền..."
            };

            var progress = new ProgressBar
            {
                Style = ProgressBarStyle.Marquee,
                MarqueeAnimationSpeed = 30,
                Dock = DockStyle.Top,
                Height = 5,
                Margin = new Padding(80, 0, 80, 0)
            };

            var content = new TableLayoutPanel
            {
                Anchor = AnchorStyles.None,
                BackColor = AppColors.White,
                ColumnCount = 1,
                RowCount = 3,
                Width = 360,
                Height = 78,
                Padding = new Padding(0, 0, 0, 12)
            };
            content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            content.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            content.RowStyles.Add(new RowStyle(SizeType.Absolute, 5f));
            content.RowStyles.Add(new RowStyle(SizeType.Absolute, 12f));
            content.Controls.Add(_busyMessageLabel, 0, 0);
            content.Controls.Add(progress, 0, 1);

            _busyOverlay = new Panel
            {
                BackColor = AppColors.White,
                Dock = DockStyle.Fill,
                Visible = false
            };
            _busyOverlay.Controls.Add(content);
            _busyOverlay.Resize += (s, e) =>
            {
                content.Left = Math.Max(0, (_busyOverlay.ClientSize.Width - content.Width) / 2);
                content.Top = Math.Max(0, (_busyOverlay.ClientSize.Height - content.Height) / 2);
            };
            _busyOverlay.BringToFront();
            return _busyOverlay;
        }

        private Control BuildHeaderRow()
        {
            var buttonsFlow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoSize = false,
                BackColor = Color.Transparent,
                Padding = new Padding(0, 0, 0, 0),
                Anchor = AnchorStyles.None
            };
            _btnReset = new PrimaryButton
            {
                Text = "Khôi phục mặc định",
                IsPrimary = false,
                Width = 205,
                Height = 44,
                Margin = new Padding(0, 0, 8, 0)
            };
            _btnSave = new PrimaryButton
            {
                Text = "Lưu thay đổi",
                IsPrimary = true,
                Width = 150,
                Height = 44,
                Margin = new Padding(0)
            };
            _btnReset.Click += (s, e) => ResetRequested?.Invoke(this, EventArgs.Empty);
            _btnSave.Click += (s, e) => SaveRequested?.Invoke(this, EventArgs.Empty);
            buttonsFlow.Controls.Add(_btnReset);
            buttonsFlow.Controls.Add(_btnSave);
            var headerSection = new HeaderSection { Dock = DockStyle.Fill, Margin = new Padding(0, 0, 12, 0) };
            headerSection.Set(
                "Phân quyền vai trò",
                "Chọn vai trò bên trái, bật/tắt quyền bên phải, rồi Lưu.",
                IconChar.UserShield,
                AppColors.Accent,
                AppColors.AccentBgSoft);
            headerSection.SetActions(buttonsFlow);

            return headerSection;
        }

        private TableLayoutPanel BuildBodyRow()
        {
            var body = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = AppColors.PageBg,
                Margin = new Padding(0)
            };
            body.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 360f));
            body.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            body.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

            body.Controls.Add(BuildRoleListCard(), 0, 0);
            body.Controls.Add(BuildPermissionListPanel(), 1, 0);

            return body;
        }

        private Control BuildRoleListCard()
        {
            // RoundedCardPanel là Panel thường - Padding của Panel CHỈ tự động áp dụng cho
            // control con có Dock (Top/Right/Fill...), KHÔNG áp dụng cho control con đặt vị
            // trí thủ công bằng Location. Toàn bộ control bên trong card này đều đặt Location
            // thủ công (để dễ tính lại kích thước khi card đổi cỡ), nên dùng hằng số Pad cục
            // bộ để tự cộng khoảng đệm vào Location/Width thay vì dựa vào card.Padding.
            const int pad = 16;
            var card = new RoundedCardPanel { Dock = DockStyle.Fill, Margin = new Padding(0, 0, 16, 0) };

            var headerRowLabel = new Label
            {
                AutoSize = true,
                Location = new Point(pad, pad),
                BackColor = Color.Transparent,
                Font = AppFonts.SmallBold,
                ForeColor = AppColors.TextMuted,
                Text = "VAI TRÒ"
            };
            _roleCountLabel = new Label
            {
                AutoSize = true,
                Location = new Point(pad, pad),
                BackColor = Color.Transparent,
                Font = AppFonts.Small,
                ForeColor = AppColors.TextMuted,
                Text = string.Empty
            };
            card.Controls.Add(headerRowLabel);
            card.Controls.Add(_roleCountLabel);

            _searchBox = new IconRoundedTextBox
            {
                Icon = IconChar.MagnifyingGlass,
                PlaceholderText = "Tìm vai trò theo tên hoặc mã...",
                Height = 42,
                Location = new Point(pad, headerRowLabel.Bottom + 10)
            };
            _searchBox.TextChanged2 += (s, e) => RoleSearchTextChanged?.Invoke(this, _searchBox.Text);
            card.Controls.Add(_searchBox);

            var scrollHost = new Panel
            {
                AutoScroll = true,
                BackColor = Color.Transparent,
                Location = new Point(pad, _searchBox.Bottom + 12)
            };
            _roleListStack = new VerticalStackPanel { Dock = DockStyle.Top, BackColor = Color.Transparent };
            scrollHost.Controls.Add(_roleListStack);
            card.Controls.Add(scrollHost);

            void SyncLayout()
            {
                int innerWidth = Math.Max(0, card.ClientSize.Width - pad * 2);
                _roleCountLabel.Location = new Point(
                    Math.Max(headerRowLabel.Right + 4, pad + innerWidth - _roleCountLabel.Width), pad);
                _searchBox.Width = innerWidth;
                scrollHost.Width = innerWidth;
                scrollHost.Height = Math.Max(0, card.ClientSize.Height - pad - scrollHost.Top);
                _roleListStack.Width = Math.Max(0,
                    scrollHost.ClientSize.Width - 12);
            }
            card.Resize += (s, e) => SyncLayout();
            SyncLayout();

            return card;
        }

        private Control BuildPermissionListPanel()
        {
            var panel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                BackColor = AppColors.PageBg,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 44f));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

            _permissionsTitleLabel = new Label
            {
                AutoSize = true,
                BackColor = Color.Transparent,
                Font = AppFonts.Subtitle,
                ForeColor = AppColors.TextTitle,
                Anchor = AnchorStyles.Left | AnchorStyles.Top,
                Margin = new Padding(0, 0, 0, 8),
                Text = "Quyền của vai trò"
            };
            panel.Controls.Add(_permissionsTitleLabel, 0, 0);

            var scrollHost = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.Transparent,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };
            _permissionsStack = new VerticalStackPanel { Dock = DockStyle.Top, BackColor = Color.Transparent };
            scrollHost.Controls.Add(_permissionsStack);
            panel.Controls.Add(scrollHost, 0, 1);

            void SyncPermissionStackWidth()
            {
                int scrollbarSpace = SystemInformation.VerticalScrollBarWidth + 12;
                _permissionsStack.Width = Math.Max(0,
                    scrollHost.ClientSize.Width - scrollbarSpace);
            }
            scrollHost.Resize += (s, e) => SyncPermissionStackWidth();
            scrollHost.ClientSizeChanged += (s, e) => SyncPermissionStackWidth();
            scrollHost.Layout += (s, e) => SyncPermissionStackWidth();
            SyncPermissionStackWidth();

            _emptyState = new Panel { Dock = DockStyle.Top, Height = 80, BackColor = Color.Transparent, Visible = false };
            var emptyLabel = new Label
            {
                AutoSize = true,
                Location = new Point(0, 16),
                Font = AppFonts.Body,
                ForeColor = AppColors.TextMuted,
                BackColor = Color.Transparent,
                Text = "Chọn 1 vai trò bên trái để xem/chỉnh sửa quyền."
            };
            _emptyState.Controls.Add(emptyLabel);
            _permissionsStack.Controls.Add(_emptyState);

            return panel;
        }

        // ==================== IRolePermissionView ====================

        public void ShowRoles(IReadOnlyList<RolePermissionRoleRowDto> roles)
        {
            _roleListStack.SuspendLayout();
            _roleListStack.Controls.Clear();
            foreach (var role in roles)
            {
                var item = new RoleListItemControl(role);
                item.Clicked += (s, roleId) => RoleSelected?.Invoke(this, roleId);
                _roleListStack.Controls.Add(item);
            }
            _roleListStack.ResumeLayout(true);

            int totalManaged = roles.Count;
            _roleCountLabel.Text = totalManaged + " vai trò";
        }

        public void ShowPermissionGroups(string roleTitle, bool isAdminRole, IReadOnlyList<PermissionGroupViewModel> groups)
        {
            _permissionsTitleLabel.Text = string.IsNullOrWhiteSpace(roleTitle) ? "Quyền của vai trò" : roleTitle;

            _permissionsStack.SuspendLayout();
            _permissionsStack.Controls.Clear();

            if (groups == null || groups.Count == 0)
            {
                _emptyState.Visible = true;
                _permissionsStack.Controls.Add(_emptyState);
                _permissionsStack.ResumeLayout(true);
                return;
            }

            foreach (var group in groups)
            {
                var card = new PermissionGroupCardControl(group, isAdminRole);
                card.Toggled += (s, args) => PermissionToggled?.Invoke(this, args);
                _permissionsStack.Controls.Add(card);
            }
            _permissionsStack.ResumeLayout(true);
        }

        public void SetBusy(bool isBusy, string message)
        {
            Cursor = isBusy ? Cursors.WaitCursor : Cursors.Default;
            _btnSave.Enabled = !isBusy;
            _btnReset.Enabled = !isBusy;
            _searchBox.Enabled = !isBusy;
            _busyMessageLabel.Text = string.IsNullOrWhiteSpace(message)
                ? "Đang xử lý..."
                : message;
            _busyOverlay.Visible = isBusy;
            if (isBusy) _busyOverlay.BringToFront();
        }

        public void ShowError(string title, string message) => DialogHelper.ShowError(FindForm(), title, message);

        public void ShowSuccess(string title, string message) => DialogHelper.ShowSuccess(FindForm(), title, message);

        public void ShowInfo(string title, string message) => DialogHelper.ShowInfo(FindForm(), title, message);

        // ==================== Card bo góc dùng riêng cho panel danh sách vai trò ====================

        private sealed class RoundedCardPanel : Panel
        {
            public RoundedCardPanel()
            {
                BorderStyle = BorderStyle.None;
                SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                         ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
                BackColor = AppColors.White;
            }

            protected override void OnPaintBackground(PaintEventArgs pevent)
            {
                pevent.Graphics.Clear(AppColors.White);
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = new Rectangle(0, 0, Width - 1, Height - 1);
                using (var path = AppRadius.GetRoundedPath(rect, AppRadius.Large))
                using (var brush = new SolidBrush(AppColors.White))
                using (var pen = new Pen(Color.White, 1.2f))
                {
                    g.FillPath(brush, path);
                    g.DrawPath(pen, path);
                }
                base.OnPaint(e);
            }
        }
    }
}