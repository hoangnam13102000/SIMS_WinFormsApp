using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SIMS_WinFormsApp.Models.DTOs.Permission;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls.Permission
{
    /// <summary>
    /// 1 dòng vai trò trong cột "VAI TRÒ" của trang Phân quyền vai trò.
    ///
    /// THIẾT KẾ LẠI (redesign theo mockup): badge số quyền hiển thị lại ĐẦY ĐỦ dạng
    /// "N quyền" (không chỉ số trần) để khớp với bản thiết kế mẫu - đồng thời vẫn giữ
    /// nguyên kỹ thuật "đo trước rồi dùng chung 1 độ rộng cột cho cả danh sách" (xem
    /// MeasureBadgeWidth) để mọi badge luôn hiển thị trọn vẹn và thẳng hàng, không còn bị
    /// AutoEllipsis cắt chữ như phiên bản v1 trước đây.
    ///
    /// Dải màu nhấn (accent bar) bên trái khi đang được chọn giúp tín hiệu lựa chọn rõ ràng
    /// hơn là chỉ dựa vào nền màu nhạt (AccentSoft) - vốn khó nhận ra khi lướt nhanh qua
    /// danh sách.
    /// </summary>
    public sealed class RoleListItemControl : Panel
    {
        private const int HorizontalPadding = 12;
        private const int VerticalPadding = 10;
        private const int TextRowGap = 2;
        private const int SelectionBarWidth = 3;

        /// <summary>Độ rộng cột badge tối thiểu - đủ cho "0 quyền" trong 1 viên pill cân đối.</summary>
        private const int MinBadgeColumnWidth = 76;

        /// <summary>
        /// Trần độ rộng cột badge - tránh badge chiếm quá nhiều chỗ nếu số quyền cực lớn (3+
        /// chữ số). NÂNG từ 112 lên 136: giá trị cũ từng đủ khi đo bằng NoPadding, nhưng sau
        /// khi PermissionUiHelpers.MeasureTextWidth bỏ NoPadding để đo sát với chữ vẽ thật
        /// (xem giải thích trong PermissionUiHelpers), số đo "53 quyền" có thể nhỉnh hơn 112
        /// một chút và bị trần này CẮT MẤT phần dư ra dù đã đo đúng - khiến badge vẫn hiện
        /// "53 qu...". Nới trần rộng hơn hẳn mức cần thiết để không còn xảy ra tình huống đó.
        /// </summary>
        private const int MaxBadgeColumnWidth = 136;

        public int RoleId { get; }

        public event EventHandler<int> Clicked;

        private readonly bool _isSelected;
        private bool _isHover;
        private readonly ToolTip _nameToolTip;

        /// <summary>
        /// Đo độ rộng (px) cần thiết để hiển thị TRỌN VẸN nhãn "N quyền" của 1 vai trò, đã
        /// kẹp trong khoảng [MinBadgeColumnWidth, MaxBadgeColumnWidth]. Gọi tĩnh (không cần
        /// khởi tạo control) để nơi gọi (ucRolePermission.ShowRoles) tính trước độ rộng cột
        /// dùng chung cho toàn bộ danh sách trước khi dựng từng dòng, để mọi badge thẳng hàng
        /// nhau dù số quyền dài ngắn khác nhau.
        /// </summary>
        public static int MeasureBadgeWidth(int permissionCount)
        {
            string text = FormatBadgeText(permissionCount);
            // horizontalPadding nới từ 22 lên 30: cộng thêm phần đệm 2 bên viên pill rộng rãi
            // hơn hẳn mức chữ cần, để chữ không bao giờ chạm sát viền pill dù đo/vẽ lệch nhau
            // vài px giữa các máy/mức DPI khác nhau.
            int width = PillBadgeLabel.MeasureWidth(text, AppFonts.SmallBold, horizontalPadding: 30, minWidth: MinBadgeColumnWidth);
            return Math.Min(MaxBadgeColumnWidth, width);
        }

        private static string FormatBadgeText(int permissionCount) => permissionCount + " quyền";

        public RoleListItemControl(RolePermissionRoleRowDto role, int badgeColumnWidth)
        {
            if (role == null) throw new ArgumentNullException(nameof(role));

            RoleId = role.RoleId;
            _isSelected = role.IsSelected;

            int effectiveBadgeWidth = Math.Max(MinBadgeColumnWidth, badgeColumnWidth);

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);

            Cursor = Cursors.Hand;
            Dock = DockStyle.Top;
            Margin = new Padding(0, 0, 0, 8);
            Padding = new Padding(HorizontalPadding, VerticalPadding, HorizontalPadding, VerticalPadding);
            BackColor = _isSelected ? AppColors.AccentSoft : AppColors.White;

            var nameFont = _isSelected ? AppFonts.BodyBold : AppFonts.Body;
            var codeFont = AppFonts.Small;

            // Chiều cao từng dòng lấy từ chiều cao THẬT của font (đủ chỗ cho mọi dấu tiếng
            // Việt) + 1 khoảng đệm nhỏ để không bị bó sát - thay cho số px đoán sẵn.
            int nameRowHeight = PermissionUiHelpers.MeasureLineHeight(nameFont) + 6;
            int codeRowHeight = PermissionUiHelpers.MeasureLineHeight(codeFont) + 4;
            int contentHeight = nameRowHeight + TextRowGap + codeRowHeight;
            Height = VerticalPadding * 2 + contentHeight;

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
            // Cột badge rộng theo ĐÚNG nội dung thực tế của toàn danh sách (xem MeasureBadgeWidth).
            rowLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, effectiveBadgeWidth));
            rowLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

            // Badge cố định 1 chiều cao "vừa mắt" rồi tự căn giữa theo chiều dọc trong cột
            // badge - tránh badge bị kéo thành hình oval dẹt nếu để Dock = Fill lấp đầy
            // nguyên cột cao bằng cả tên lẫn mã vai trò.
            int badgeHeight = PermissionUiHelpers.MeasureLineHeight(codeFont) + 10;
            int badgeTopMargin = Math.Max(0, (contentHeight - badgeHeight) / 2);
            string badgeText = FormatBadgeText(role.PermissionCount);
            var countLabel = new PillBadgeLabel
            {
                Dock = DockStyle.Top,
                Height = badgeHeight,
                Width = Math.Max(10, effectiveBadgeWidth - 8),
                Margin = new Padding(4, badgeTopMargin, 4, 0),
                Font = AppFonts.SmallBold,
                ForeColor = _isSelected ? AppColors.White : AppColors.Accent,
                PillBackColor = _isSelected ? AppColors.Accent : AppColors.AccentBgSoft,
                Text = badgeText
            };

            // textCol có Padding riêng để luôn chừa 1 khoảng cách đều với cột số quyền bên phải,
            // thay vì để 2 cột dính sát nhau.
            var textCol = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Padding = new Padding(0, 0, 10, 0)
            };
            var nameLabel = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Top,
                Height = nameRowHeight,
                BackColor = Color.Transparent,
                Font = nameFont,
                ForeColor = _isSelected ? AppColors.Accent : AppColors.TextPrimary,
                Text = string.IsNullOrEmpty(role.RoleName) ? role.RoleCode : role.RoleName,
                TextAlign = ContentAlignment.MiddleLeft,
                UseMnemonic = false,
                // AutoEllipsis là lưới an toàn cuối cùng cho tên vai trò: dù tên dài cỡ nào và
                // cột hẹp đến đâu, chữ sẽ luôn kết thúc gọn bằng "..." thay vì bị cắt cứng giữa
                // chừng. Có tooltip đầy đủ bù lại (xem _nameToolTip bên dưới).
                AutoEllipsis = true
            };
            var codeLabel = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Top,
                Height = codeRowHeight,
                BackColor = Color.Transparent,
                Font = codeFont,
                ForeColor = AppColors.TextMuted,
                Text = role.RoleCode ?? string.Empty,
                TextAlign = ContentAlignment.TopLeft,
                Margin = new Padding(0, TextRowGap, 0, 0),
                UseMnemonic = false,
                AutoEllipsis = true
            };
            textCol.Controls.Add(codeLabel);
            textCol.Controls.Add(nameLabel);

            _nameToolTip = new ToolTip { InitialDelay = 400, ReshowDelay = 100, AutoPopDelay = 4000 };
            string fullName = string.IsNullOrEmpty(role.RoleName) ? role.RoleCode : role.RoleName;
            _nameToolTip.SetToolTip(nameLabel, fullName);
            _nameToolTip.SetToolTip(codeLabel, role.RoleCode ?? string.Empty);
            _nameToolTip.SetToolTip(countLabel, "Vai trò này đang được cấp " + role.PermissionCount + " quyền.");

            rowLayout.Controls.Add(textCol, 0, 0);
            rowLayout.Controls.Add(countLabel, 1, 0);
            Controls.Add(rowLayout);

            MouseEnter += (s, e) =>
            {
                _isHover = true;
                BackColor = AppColors.BgLighter;
                Invalidate();
            };
            MouseLeave += (s, e) =>
            {
                _isHover = false;
                BackColor = _isSelected ? AppColors.AccentSoft : AppColors.White;
                Invalidate();
            };
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

            g.Clear(AppColors.White);

            var fullRect = new Rectangle(0, 0, Width, Height);

            if (_isSelected)
            {
                using (var path = AppRadius.GetRoundedPath(fullRect, AppRadius.Small))
                using (var brush = new SolidBrush(AppColors.AccentSoft))
                {
                    g.FillPath(brush, path);
                }

                // Dải màu nhấn bên trái - tín hiệu "đang chọn" rõ ràng hơn khi lướt nhanh qua
                // danh sách, thay vì chỉ dựa vào nền màu nhạt vốn dễ bị bỏ sót.
                var previousClip = g.Clip;
                try
                {
                    using (var clipPath = AppRadius.GetRoundedPath(fullRect, AppRadius.Small))
                    {
                        g.SetClip(clipPath, CombineMode.Intersect);
                        using (var accentBrush = new SolidBrush(AppColors.Accent))
                        {
                            g.FillRectangle(accentBrush, 0, 0, SelectionBarWidth, Height);
                        }
                    }
                }
                finally
                {
                    g.Clip = previousClip;
                }
            }
            else if (_isHover)
            {
                using (var path = AppRadius.GetRoundedPath(fullRect, AppRadius.Small))
                using (var brush = new SolidBrush(AppColors.BgLighter))
                {
                    g.FillPath(brush, path);
                }
            }

            base.OnPaint(e);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            pevent.Graphics.Clear(AppColors.White);
        }
    }
}