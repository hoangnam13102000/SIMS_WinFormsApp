using System;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Services.Session;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Forms.Dashboard
{
    internal class BufferedPanel : Panel
    {
        public BufferedPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw, true);
            UpdateStyles();
        }
    }

    public partial class ucDashboard : UserControl
    {
        private HeaderSection _header;
        private TableLayoutPanel _statsRow;
        private BufferedPanel _contentPanel;

        public ucDashboard()
        {
            AutoScaleMode = AutoScaleMode.None;
            Font = new Font("Segoe UI", 9f);
            DoubleBuffered = true;
            Dock = DockStyle.Fill;
            BackColor = AppColors.PageBg;
            Padding = new Padding(20, 16, 20, 20);
            Margin = new Padding(0);

            InitializeComponent();
            BuildUI();
            LoadData();
        }

        private void BuildUI()
        {
            SuspendLayout();

            // ===== Root: Header (cố định) + Content (fill) =====
            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                BackColor = AppColors.PageBg,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 120f)); // header 108 + gap 12
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

            // ===== HeaderSection =====
            _header = new HeaderSection
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 0, 12)
            };
            root.Controls.Add(_header, 0, 0);

            // ===== Content host =====
            _contentPanel = new BufferedPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = AppColors.PageBg,
                Padding = new Padding(0)
            };
            root.Controls.Add(_contentPanel, 0, 1);

            // ===== Stats: 4 cột chia đều =====
            _statsRow = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 144,
                ColumnCount = 4,
                RowCount = 1,
                BackColor = AppColors.PageBg,  
                Margin = new Padding(0),
                Padding = new Padding(0)
            };
            _statsRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            _statsRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            _statsRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            _statsRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            _statsRow.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            _contentPanel.Controls.Add(_statsRow);

            AddStatCard(0,
                "12.450.000 ₫", "Doanh thu hôm nay", "+12.5% so với hôm qua",
                IconChar.Coins,
                Color.FromArgb(37, 99, 235), Color.FromArgb(219, 234, 254), AppColors.Success);

            AddStatCard(1,
                "48", "Đơn hàng mới", "+8 đơn so với hôm qua",
                IconChar.CartShopping,
                Color.FromArgb(16, 185, 129), Color.FromArgb(209, 250, 229), AppColors.Success);

            AddStatCard(2,
                "1.284", "Sản phẩm tồn", "12 sắp hết hàng",
                IconChar.BoxOpen,
                Color.FromArgb(245, 158, 11), Color.FromArgb(254, 243, 199), AppColors.Warning);

            AddStatCard(3,
                "326", "Khách hàng", "+15 khách mới",
                IconChar.Users,
                Color.FromArgb(139, 92, 246), Color.FromArgb(237, 233, 254), AppColors.Success);

            // ===== Spacer =====
            var spacer = new Panel
            {
                Dock = DockStyle.Top,
                Height = 12,
                BackColor = Color.Transparent
            };
            _contentPanel.Controls.Add(spacer);

            // ===== Activity section =====
            var activityCard = CreateSectionCard(
                "Hoạt động gần đây",
                "Các giao dịch và sự kiện mới nhất trong hệ thống");
            // Keep the activity card in the normal vertical flow. A Fill child
            // inside an AutoScroll host can occupy the same bounds as the
            // statistic cards and paint over their content.
            activityCard.Dock = DockStyle.Top;
            activityCard.Height = 420;
            _contentPanel.Controls.Add(activityCard);

            // Z-order: Fill dưới cùng, Top ở trên
            _contentPanel.Controls.SetChildIndex(activityCard, 0);
            _contentPanel.Controls.SetChildIndex(spacer, 1);
            _contentPanel.Controls.SetChildIndex(_statsRow, 2);
            _contentPanel.Resize += (_, __) =>
            {
                activityCard.Height = Math.Max(280, _contentPanel.ClientSize.Height - _statsRow.Height - spacer.Height - 8);
            };

            Controls.Add(root);
            ResumeLayout(true);
        }

        private void AddStatCard(
            int column,
            string value, string title, string trend,
            IconChar icon, Color iconColor, Color iconBg, Color trendColor)
        {
            var card = new StatCard
            {
                ValueText = value,
                TitleText = title,
                TrendText = trend,
                Icon = icon,
                IconColor = iconColor,
                IconBackground = iconBg,
                TrendColor = trendColor,
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, column < 3 ? 12 : 0, 0)
            };
            _statsRow.Controls.Add(card, column, 0);
        }

        private Panel CreateSectionCard(string title, string subtitle)
        {
            var panel = new BufferedPanel
            {
                BackColor = AppColors.White,
                Padding = new Padding(20)
            };

            panel.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                var rect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);
                using (var path = AppRadius.GetRoundedPath(rect, AppRadius.Large))
                using (var brush = new SolidBrush(AppColors.White))
                using (var pen = new Pen(AppColors.Border, 1f))
                {
                    e.Graphics.FillPath(brush, path);
                    e.Graphics.DrawPath(pen, path);
                }
            };

            var lblTitle = new Label
            {
                AutoSize = true,
                Text = title,
                Font = new Font("Segoe UI Semibold", 13f, FontStyle.Bold),
                ForeColor = AppColors.TextTitle,
                Location = new Point(20, 18),
                BackColor = Color.Transparent
            };
            panel.Controls.Add(lblTitle);

            var lblSub = new Label
            {
                AutoSize = true,
                Text = subtitle,
                Font = new Font("Segoe UI", 9.5f),
                ForeColor = AppColors.TextSecondary,
                Location = new Point(20, 52),
                BackColor = Color.Transparent
            };
            panel.Controls.Add(lblSub);

            var lblEmpty = new Label
            {
                AutoSize = false,
                Text = "Chưa có dữ liệu hoạt động.\nKết nối module Đơn hàng / POS để hiển thị tại đây.",
                Font = new Font("Segoe UI", 9.5f),
                ForeColor = AppColors.TextMuted,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(20, 80),
                Size = new Size(panel.Width - 40, 120),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.Transparent
            };
            panel.Controls.Add(lblEmpty);

            return panel;
        }

        private void LoadData()
        {
            var session = UserSession.Instance;
            string name = session.CurrentUser?.FullName
                       ?? session.CurrentUser?.Username
                       ?? "bạn";
            _header.SetDashboard(name);
        }
    }
}