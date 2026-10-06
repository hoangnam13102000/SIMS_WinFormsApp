using System;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Models.DTOs.Dashboard;
using SIMS_WinFormsApp.MVP.Presenters;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Controls.Dashboard;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Interfaces;
using SIMS_WinFormsApp.Services.Interfaces;

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

    public partial class ucDashboard : UserControl, IDashboardView
    {
        private readonly DashboardPresenter _presenter;
        private HeaderSection _header;
        private TableLayoutPanel _statsRow;
        private BufferedPanel _contentPanel;
        private StatCard _revenueCard;
        private StatCard _ordersCard;
        private StatCard _stockCard;
        private StatCard _customersCard;
        private RevenueTrendChart _revenueChart;
        private Label _chartSubtitle;

        public ucDashboard(string displayName, IDashboardService dashboardService)
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
            _presenter = new DashboardPresenter(this, displayName, dashboardService);
            _presenter.Load();
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
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 100f)); // header 80 + gap 12
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
                Height = 132,
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

            _revenueCard = AddStatCard(0,
                "—", "Doanh thu hôm nay", "Đang tải số liệu...",
                IconChar.Coins,
                Color.FromArgb(37, 99, 235), Color.FromArgb(219, 234, 254), AppColors.Success);

            _ordersCard = AddStatCard(1,
                "—", "Hóa đơn hôm nay", "Đã thanh toán trong ngày",
                IconChar.CartShopping,
                Color.FromArgb(16, 185, 129), Color.FromArgb(209, 250, 229), AppColors.Success);

            _stockCard = AddStatCard(2,
                "—", "Sản phẩm tồn kho", "Đang tải số liệu...",
                IconChar.BoxOpen,
                Color.FromArgb(245, 158, 11), Color.FromArgb(254, 243, 199), AppColors.Warning);

            _customersCard = AddStatCard(3,
                "—", "Khách hàng", "Tổng khách hàng hiện có",
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

            // ===== Revenue trend =====
            var revenueCard = CreateRevenueSectionCard();
            revenueCard.Dock = DockStyle.Top;
            revenueCard.Height = 410;
            _contentPanel.Controls.Add(revenueCard);

            // Z-order: Fill dưới cùng, Top ở trên
            _contentPanel.Controls.SetChildIndex(revenueCard, 0);
            _contentPanel.Controls.SetChildIndex(spacer, 1);
            _contentPanel.Controls.SetChildIndex(_statsRow, 2);
            _contentPanel.Resize += (_, __) =>
            {
                revenueCard.Height = Math.Max(320, _contentPanel.ClientSize.Height - _statsRow.Height - spacer.Height - 8);
            };

            Controls.Add(root);
            ResumeLayout(true);
        }

        private StatCard AddStatCard(
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
                TopBorderColor = iconColor,
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, column < 3 ? 12 : 0, 0)
            };
            _statsRow.Controls.Add(card, column, 0);
            return card;
        }

        private Panel CreateRevenueSectionCard()
        {
            var panel = new BufferedPanel
            {
                BackColor = AppColors.White,
                Padding = new Padding(20),
                Margin = new Padding(0)
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
                Text = "Xu hướng doanh thu",
                Font = new Font("Segoe UI Semibold", 13f, FontStyle.Bold),
                ForeColor = AppColors.TextTitle,
                Location = new Point(20, 18),
                BackColor = Color.Transparent
            };
            panel.Controls.Add(lblTitle);

            _chartSubtitle = new Label
            {
                AutoSize = true,
                Text = "Doanh thu hóa đơn theo ngày · 7 ngày gần nhất",
                Font = new Font("Segoe UI", 9.5f),
                ForeColor = AppColors.TextSecondary,
                Location = new Point(20, 52),
                BackColor = Color.Transparent
            };
            panel.Controls.Add(_chartSubtitle);

            var rangeBadge = new Label
            {
                AutoSize = true,
                Text = "7 NGÀY",
                Font = new Font("Segoe UI Semibold", 8.5f, FontStyle.Bold),
                ForeColor = AppColors.Accent,
                BackColor = AppColors.AccentBgSoft,
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(10, 5, 10, 5),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(panel.Width - 92, 21)
            };
            panel.Controls.Add(rangeBadge);

            _revenueChart = new RevenueTrendChart
            {
                Location = new Point(20, 82),
                Size = new Size(Math.Max(280, panel.Width - 40), Math.Max(220, panel.Height - 102)),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };
            panel.Controls.Add(_revenueChart);

            return panel;
        }

        public void ShowDashboardUser(string displayName)
        {
            _header.SetDashboard(displayName);
        }

        public void ShowDashboardData(DashboardSummaryDto summary)
        {
            if (summary == null) throw new ArgumentNullException(nameof(summary));

            _revenueCard.ValueText = summary.RevenueToday.ToString("N0") + " ₫";
            UpdateRevenueComparison(summary.RevenueToday, summary.RevenueYesterday);
            _ordersCard.ValueText = summary.OrdersToday.ToString("N0");
            _ordersCard.TrendText = "Hóa đơn hợp lệ phát sinh hôm nay";
            _stockCard.ValueText = summary.StockUnits.ToString("N0");
            _stockCard.TrendText = summary.LowStockProducts.ToString("N0") + " mặt hàng sắp hết";
            _stockCard.TrendColor = summary.LowStockProducts > 0 ? AppColors.Warning : AppColors.Success;
            _customersCard.ValueText = summary.Customers.ToString("N0");
            _customersCard.TrendText = "Tổng khách hàng đang hoạt động";
            _revenueChart.SetData(summary.RevenueTrend);
        }

        public void ShowDashboardError(string message)
        {
            _revenueCard.ValueText = "—";
            _ordersCard.ValueText = "—";
            _stockCard.ValueText = "—";
            _customersCard.ValueText = "—";
            _revenueCard.TrendText = "Không tải được số liệu";
            _ordersCard.TrendText = "Không tải được số liệu";
            _stockCard.TrendText = "Không tải được số liệu";
            _customersCard.TrendText = "Không tải được số liệu";
            _chartSubtitle.Text = "Lỗi tải dữ liệu từ SQL Server: " + message;
            _chartSubtitle.ForeColor = AppColors.Error;
            _revenueChart.SetError(message);
        }

        private void UpdateRevenueComparison(decimal today, decimal yesterday)
        {
            if (yesterday <= 0)
            {
                _revenueCard.TrendText = today <= 0
                    ? "Không đổi so với hôm qua"
                    : "Có doanh thu so với hôm qua";
                _revenueCard.TrendColor = today <= 0 ? AppColors.TextMuted : AppColors.Success;
                return;
            }

            decimal percentage = (today - yesterday) / yesterday * 100m;
            string direction = percentage > 0 ? "↑ " : percentage < 0 ? "↓ " : "";
            _revenueCard.TrendText = direction + Math.Abs(percentage).ToString("0.#")
                + "% so với hôm qua";
            _revenueCard.TrendColor = percentage > 0 ? AppColors.Success
                : percentage < 0 ? AppColors.Error : AppColors.TextMuted;
        }
    }
}