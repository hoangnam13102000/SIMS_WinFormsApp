using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.I18n;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{
    /// <summary>Một thông báo hiển thị trong <see cref="NotificationDropdownControl"/>.
    /// Đây thuần tuý là DTO cho lớp UI — KHÔNG kết nối DB/Service nào; dữ liệu (nếu có)
    /// phải do lớp gọi (Presenter) truyền vào qua <see cref="NotificationDropdownControl.SetItems"/>.</summary>
    public class NotificationItem
    {
        public string Title { get; set; }
        public string TimeText { get; set; }
        public bool IsUnread { get; set; }
    }

    /// <summary>
    /// Panel thông báo thả xuống dưới icon chuông trên Header. Vì project hiện tại
    /// CHƯA có nguồn dữ liệu/notification service thật (không tự thêm backend mới theo
    /// yêu cầu), control này mặc định hiển thị empty-state; nếu sau này có dữ liệu thật,
    /// Presenter chỉ cần gọi SetItems(...) trước khi ShowBelow().
    /// </summary>
    public class NotificationDropdownControl : PopupFormBase
    {
        private const int Width_ = 320;
        private const int MaxListHeight = 320;

        private readonly Panel _host;
        private readonly Label _titleLabel;
        private readonly Label _unreadBadge;
        private readonly Panel _listHost;
        private readonly Panel _emptyState;

        public event EventHandler<NotificationItem> ItemClicked;

        public NotificationDropdownControl()
        {
            BackColor = LayoutColors.DropdownBg;
            Width = Width_;
            Height = 200;

            _host = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = LayoutColors.DropdownBg,
                Padding = new Padding(0)
            };
            Controls.Add(_host);

            // ===== Header: "Thông báo" + số chưa đọc =====
            var headerPanel = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(Width_, 44),
                BackColor = Color.Transparent
            };
            headerPanel.Paint += (s, e) =>
            {
                using (var pen = new Pen(LayoutColors.DropdownBorder))
                    e.Graphics.DrawLine(pen, 14, headerPanel.Height - 1, headerPanel.Width - 14, headerPanel.Height - 1);
            };

            _titleLabel = new Label
            {
                AutoSize = true,
                Text = Lang.Get("header.notifications.title"),
                Font = new Font("Segoe UI Semibold", 10.5f, FontStyle.Bold),
                ForeColor = LayoutColors.TextWhite,
                BackColor = Color.Transparent,
                Location = new Point(16, 12)
            };

            _unreadBadge = new Label
            {
                AutoSize = false,
                Visible = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 7.5f, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = LayoutColors.Accent,
                Size = new Size(22, 18)
            };
            using (var path = new GraphicsPath())
            {
                path.AddArc(0, 0, 17, 17, 0, 360);
                _unreadBadge.Region = new Region(path);
            }

            headerPanel.Controls.Add(_titleLabel);
            headerPanel.Controls.Add(_unreadBadge);

            // ===== Empty state =====
            _emptyState = new Panel
            {
                Location = new Point(0, 44),
                Size = new Size(Width_, 140),
                BackColor = Color.Transparent
            };
            var emptyIcon = new IconPictureBox
            {
                IconChar = IconChar.Bell,
                IconColor = LayoutColors.SidebarTextMuted,
                IconSize = 30,
                Size = new Size(36, 36),
                BackColor = Color.Transparent
            };
            var emptyLabel = new Label
            {
                AutoSize = false,
                Text = Lang.Get("header.notifications.empty"),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 9f),
                ForeColor = LayoutColors.SidebarTextMuted,
                BackColor = Color.Transparent,
                Size = new Size(Width_ - 32, 24)
            };
            emptyIcon.Location = new Point((Width_ - emptyIcon.Width) / 2, 24);
            emptyLabel.Location = new Point((Width_ - emptyLabel.Width) / 2, 24 + emptyIcon.Height + 10);
            _emptyState.Controls.Add(emptyIcon);
            _emptyState.Controls.Add(emptyLabel);

            // ===== Danh sách thông báo (khi có dữ liệu thật) =====
            _listHost = new Panel
            {
                Location = new Point(0, 44),
                Size = new Size(Width_, MaxListHeight),
                AutoScroll = true,
                BackColor = Color.Transparent,
                Visible = false
            };

            _host.Controls.Add(headerPanel);
            _host.Controls.Add(_emptyState);
            _host.Controls.Add(_listHost);

            SetItems(null);
        }

        /// <summary>Nạp danh sách thông báo thật (nếu có). Truyền null/rỗng để hiển thị empty-state.</summary>
        public void SetItems(IEnumerable<NotificationItem> items)
        {
            _listHost.SuspendLayout();
            _listHost.Controls.Clear();

            var list = new List<NotificationItem>();
            if (items != null) list.AddRange(items);

            int unread = 0;
            foreach (var i in list) if (i.IsUnread) unread++;
            if (unread > 0)
            {
                _unreadBadge.Text = unread > 9 ? "9+" : unread.ToString();
                _unreadBadge.Visible = true;
            }
            else
            {
                _unreadBadge.Visible = false;
            }
            _unreadBadge.Location = new Point(Width_ - 16 - _unreadBadge.Width, 13);

            if (list.Count == 0)
            {
                _emptyState.Visible = true;
                _listHost.Visible = false;
                Height = 44 + _emptyState.Height;
            }
            else
            {
                _emptyState.Visible = false;
                _listHost.Visible = true;

                int y = 0;
                foreach (var item in list)
                {
                    var row = BuildRow(item);
                    row.Location = new Point(0, y);
                    row.Width = Width_;
                    _listHost.Controls.Add(row);
                    y += row.Height;
                }

                int listHeight = Math.Min(MaxListHeight, y);
                _listHost.Height = listHeight;
                Height = 44 + listHeight;
            }

            _listHost.ResumeLayout(true);
            ApplyRoundedRegion();
            Invalidate();
        }

        private Control BuildRow(NotificationItem item)
        {
            var row = new Panel { Height = 60, BackColor = Color.Transparent, Cursor = Cursors.Hand };
            bool hover = false;

            var dot = new Panel
            {
                Size = new Size(8, 8),
                Location = new Point(16, 26),
                BackColor = item.IsUnread ? LayoutColors.Accent : Color.Transparent
            };

            var title = new Label
            {
                AutoSize = false,
                AutoEllipsis = true,
                Text = item.Title ?? string.Empty,
                Font = new Font("Segoe UI", 9f, item.IsUnread ? FontStyle.Bold : FontStyle.Regular),
                ForeColor = LayoutColors.TextWhite,
                BackColor = Color.Transparent,
                Location = new Point(32, 10),
                Size = new Size(Width_ - 32 - 16, 20)
            };

            var time = new Label
            {
                AutoSize = false,
                Text = item.TimeText ?? string.Empty,
                Font = new Font("Segoe UI", 8f),
                ForeColor = LayoutColors.TextMuted,
                BackColor = Color.Transparent,
                Location = new Point(32, 32),
                Size = new Size(Width_ - 32 - 16, 18)
            };

            row.Controls.Add(dot);
            row.Controls.Add(title);
            row.Controls.Add(time);

            row.Paint += (s, e) =>
            {
                if (!hover) return;
                using (var b = new SolidBrush(LayoutColors.RowHover))
                    e.Graphics.FillRectangle(b, 0, 0, row.Width, row.Height);
            };

            EventHandler enter = (_, __) => { hover = true; row.Invalidate(); };
            EventHandler leave = (_, __) =>
            {
                if (!row.ClientRectangle.Contains(row.PointToClient(Cursor.Position)))
                {
                    hover = false;
                    row.Invalidate();
                }
            };
            EventHandler click = (_, __) => ItemClicked?.Invoke(this, item);

            row.MouseEnter += enter; title.MouseEnter += enter; time.MouseEnter += enter; dot.MouseEnter += enter;
            row.MouseLeave += leave; title.MouseLeave += leave; time.MouseLeave += leave; dot.MouseLeave += leave;
            row.Click += click; title.Click += click; time.Click += click; dot.Click += click;

            return row;
        }

        private void ApplyRoundedRegion()
        {
            try
            {
                Region?.Dispose();
                Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width + 1, Height + 1, 12, 12));
            }
            catch { /* ignore */ }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (var pen = new Pen(LayoutColors.DropdownBorder, 1f))
            using (var path = RoundedRect(new Rectangle(0, 0, Width - 1, Height - 1), 11))
                g.DrawPath(pen, path);
        }

        private static GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int d = radius * 2;
            var path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect, int nTopRect, int nRightRect, int nBottomRect,
            int nWidthEllipse, int nHeightEllipse);
    }

}