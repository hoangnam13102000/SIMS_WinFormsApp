using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
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
    /// <summary>
    /// View (MVP) cho trang "Phân quyền vai trò". Đây là bản THIẾT KẾ LẠI phần UI/UX theo
    /// mockup: khôi phục ô tìm kiếm vai trò, khôi phục badge số quyền dạng đầy đủ "N quyền",
    /// và tinh chỉnh lại khoảng cách/độ tương phản cho gần với bản thiết kế mẫu.
    ///
    /// QUAN TRỌNG: đây CHỈ là thay đổi ở tầng View (cách dựng/hiển thị control). Hợp đồng
    /// <see cref="IRolePermissionView"/> (tên sự kiện, tên hàm, tham số) và toàn bộ nghiệp vụ
    /// trong <see cref="RolePermissionPresenter"/> giữ NGUYÊN VẸN không đổi - đúng tinh thần
    /// MVP: Presenter không biết và không phụ thuộc vào việc View trình bày dữ liệu ra sao.
    /// </summary>
    public sealed class ucRolePermission : UserControl, IRolePermissionView
    {
        public event EventHandler ViewReady;
        public event EventHandler<int> RoleSelected;
        public event EventHandler<string> RoleSearchTextChanged;
        public event EventHandler<PermissionToggledEventArgs> PermissionToggled;
        public event EventHandler SaveRequested;
        public event EventHandler ResetRequested;

        // Thang khoảng cách DÙNG CHUNG cho toàn trang: PagePad cho lề ngoài/cạnh card,
        // SectionGap cho khoảng cách giữa các khối bên trong 1 card. Quy về 2 mức duy nhất để
        // bố cục không còn chỗ khít, chỗ rộng như trước khi mỗi nơi tự chọn 1 số khác nhau.
        private const int PagePad = 20;
        private const int SectionGap = 12;

        // Độ rộng cột "VAI TRÒ" bên trái. Rộng hơn bản cũ (380 -> 408) để chừa chỗ cho ô tìm
        // kiếm và cho badge "N quyền" (dạng đầy đủ, thay vì chỉ số trần) không ép sát cột tên.
        private const int RoleColumnWidth = 408;

        private readonly RolePermissionPresenter _presenter;

        private Label _roleCountLabel;
        private VerticalStackPanel _roleListStack;
        private Panel _roleScrollHost;
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
            // Margin ngoài đồng đều 4 phía để tổng thể trang không bị lệch/khít 1 bên so với
            // 3 bên còn lại.
            Padding = new Padding(PagePad);
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
            // AutoSize + GrowAndShrink để buttonsFlow luôn báo cáo ĐÚNG độ rộng nó thực sự cần
            // (qua GetPreferredSize) cho HeaderSection.SetActions tính khung chứa.
            var buttonsFlow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                BackColor = Color.Transparent,
                Padding = new Padding(0),
                Margin = new Padding(0)
            };
            // SỬA LỖI (nút bị cắt chữ "Khôi phục ...", "Lưu tha..."): trước đây Width của 2 nút
            // là số px ĐOÁN SẴN (205/150) dựa theo 1 mức DPI/font cụ thể. Ở máy chạy DPI cao
            // hơn hoặc font hệ thống thay thế rộng hơn, chữ thực tế cần nhiều chỗ hơn số đã
            // đoán -> PrimaryButton tự cắt bớt bằng EndEllipsis. Nay đo ĐÚNG độ rộng chữ cần
            // thiết bằng chính Font sẽ dùng để vẽ (AppFonts.Button) rồi cộng thêm khoảng đệm
            // an toàn, thay vì đoán cứng - đảm bảo chữ trên nút luôn hiển thị trọn vẹn dù chạy
            // ở máy/độ phân giải nào.
            const int ButtonHorizontalPadding = 44;
            string resetText = "Khôi phục mặc định";
            string saveText = "Lưu thay đổi";
            _btnReset = new PrimaryButton
            {
                Text = resetText,
                IsPrimary = false,
                Width = PermissionUiHelpers.MeasureTextWidth(resetText, AppFonts.Button) + ButtonHorizontalPadding,
                Height = 44,
                Margin = new Padding(0, 0, SectionGap, 0)
            };
            _btnSave = new PrimaryButton
            {
                Text = saveText,
                IsPrimary = true,
                Width = PermissionUiHelpers.MeasureTextWidth(saveText, AppFonts.Button) + ButtonHorizontalPadding,
                Height = 44,
                Margin = new Padding(0)
            };
            _btnReset.Click += (s, e) => ResetRequested?.Invoke(this, EventArgs.Empty);
            _btnSave.Click += (s, e) => SaveRequested?.Invoke(this, EventArgs.Empty);
            buttonsFlow.Controls.Add(_btnReset);
            buttonsFlow.Controls.Add(_btnSave);
            var headerSection = new HeaderSection { Dock = DockStyle.Fill, Margin = new Padding(0, 0, SectionGap, 0) };
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
            body.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, RoleColumnWidth));
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
            // thủ công (để dễ tính lại kích thước khi card đổi cỡ), nên dùng hằng số pad cục
            // bộ (= SectionGap, cùng thang đo với phần còn lại của trang) để tự cộng khoảng
            // đệm vào Location/Width thay vì dựa vào card.Padding.
            const int pad = SectionGap;
            // Khoảng cách với cột quyền bên phải dùng PagePad (20) thay vì SectionGap (12) -
            // đây là ranh giới giữa 2 khối lớn của trang nên cần rộng hơn khoảng cách giữa các
            // phần tử nhỏ bên trong 1 khối.
            var card = new RoundedCardPanel { Dock = DockStyle.Fill, Margin = new Padding(0, 0, PagePad, 0) };

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
                AutoSize = false,
                Location = new Point(pad, pad),
                Height = headerRowLabel.Height + 4,
                BackColor = Color.Transparent,
                Font = AppFonts.Small,
                ForeColor = AppColors.TextMuted,
                TextAlign = ContentAlignment.MiddleRight,
                Text = string.Empty
            };
            card.Controls.Add(headerRowLabel);
            card.Controls.Add(_roleCountLabel);

            var scrollHost = new Panel
            {
                AutoScroll = true,
                BackColor = Color.Transparent,
                Location = new Point(pad, headerRowLabel.Bottom + pad)
            };
            _roleScrollHost = scrollHost;
            _roleListStack = new VerticalStackPanel { Dock = DockStyle.Top, BackColor = Color.Transparent };
            scrollHost.Controls.Add(_roleListStack);
            card.Controls.Add(scrollHost);

            void SyncLayout()
            {
                int innerWidth = Math.Max(0, card.ClientSize.Width - pad * 2);
                int countLeft = headerRowLabel.Right + 4;
                _roleCountLabel.Location = new Point(countLeft, pad);
                _roleCountLabel.Width = Math.Max(0, pad + innerWidth - countLeft);
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

            roles = roles ?? new List<RolePermissionRoleRowDto>();

            if (roles.Count == 0)
            {
                _roleListStack.Controls.Add(BuildRoleListEmptyState());
                _roleListStack.ResumeLayout(true);
                _roleCountLabel.Text = "0 vai trò";
                return;
            }

            // Đo trước độ rộng badge "N quyền" LỚN NHẤT trong danh sách đang hiển thị rồi dùng
            // CHUNG 1 độ rộng cho mọi dòng, để các badge thẳng hàng nhau và không bị cắt chữ.
            int badgeColumnWidth = roles.Max(r => RoleListItemControl.MeasureBadgeWidth(r.PermissionCount));

            foreach (var role in roles)
            {
                var item = new RoleListItemControl(role, badgeColumnWidth);
                item.Clicked += (s, roleId) => RoleSelected?.Invoke(this, roleId);
                _roleListStack.Controls.Add(item);
            }
            _roleListStack.ResumeLayout(true);

            _roleCountLabel.Text = roles.Count + " vai trò";
        }

        private static Control BuildRoleListEmptyState()
        {
            var panel = new Panel { Height = 96, BackColor = Color.Transparent };
            var icon = new IconPictureBox
            {
                IconChar = IconChar.TriangleExclamation,
                IconColor = AppColors.TextMutedAlt,
                IconSize = 18,
                Size = new Size(20, 20),
                BackColor = Color.Transparent,
                Location = new Point(0, 14)
            };
            var label = new Label
            {
                AutoSize = true,
                Location = new Point(0, icon.Bottom + 8),
                Font = AppFonts.Body,
                ForeColor = AppColors.TextMuted,
                BackColor = Color.Transparent,
                Text = "Không tìm thấy vai trò phù hợp.",
                UseMnemonic = false
            };
            panel.Controls.Add(icon);
            panel.Controls.Add(label);
            return panel;
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

            if (isAdminRole)
            {
                _permissionsStack.Controls.Add(new InfoBannerPanel(
                    "Quản trị viên luôn có toàn quyền hệ thống nên các công tắc bên dưới bị khoá ở " +
                    "trạng thái bật (\"Luôn bật\") và không thể chỉnh sửa riêng lẻ."));
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
            _busyMessageLabel.Text = string.IsNullOrWhiteSpace(message)
                ? "Đang xử lý..."
                : message;
            _busyOverlay.Visible = isBusy;
            if (isBusy) _busyOverlay.BringToFront();
        }

        public void ShowError(string title, string message) => DialogHelper.ShowError(FindForm(), title, message);

        public void ShowSuccess(string title, string message) => DialogHelper.ShowSuccess(FindForm(), title, message);

        public void ShowInfo(string title, string message) => DialogHelper.ShowInfo(FindForm(), title, message);

        // ==================== Banner thông báo (vd: giải thích Admin bị khoá quyền) ====================

        private sealed class InfoBannerPanel : Panel
        {
            public InfoBannerPanel(string message)
            {
                Height = 52;
                BackColor = AppColors.InfoBg;
                Margin = new Padding(0, 0, 0, 12);
                Padding = new Padding(44, 0, 16, 0);
                SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                         ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw |
                         ControlStyles.SupportsTransparentBackColor, true);

                var icon = new IconPictureBox
                {
                    IconChar = IconChar.CircleInfo,
                    IconColor = AppColors.Info,
                    IconSize = 16,
                    Size = new Size(18, 18),
                    BackColor = Color.Transparent,
                    Location = new Point(16, (Height - 18) / 2)
                };
                var label = new Label
                {
                    AutoSize = false,
                    Dock = DockStyle.Fill,
                    BackColor = Color.Transparent,
                    Font = AppFonts.Small,
                    ForeColor = AppColors.TextPrimary,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Text = message
                };
                Controls.Add(label);
                Controls.Add(icon);
                icon.BringToFront();
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = new Rectangle(0, 0, Width - 1, Height - 1);
                using (var path = AppRadius.GetRoundedPath(rect, AppRadius.Medium))
                using (var brush = new SolidBrush(AppColors.InfoBg))
                {
                    g.FillPath(brush, path);
                }
                using (var pen = new Pen(AppColors.Info, 1f))
                {
                    g.DrawPath(pen, AppRadius.GetRoundedPath(rect, AppRadius.Medium));
                }
                base.OnPaint(e);
            }

            protected override void OnPaintBackground(PaintEventArgs pevent)
            {
                pevent.Graphics.Clear(PermissionUiHelpers.GetEffectiveBackColor(this));
            }
        }

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
                using (var pen = new Pen(AppColors.Border, 1f))
                {
                    g.FillPath(brush, path);

                    g.DrawPath(pen, path);
                }
                base.OnPaint(e);
            }
        }
    }
}