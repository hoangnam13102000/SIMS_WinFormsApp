using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Views.Dashboard
{
    partial class ucDashboard
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel _root;
        private Panel _headerPanel;
        private Label _titleLabel;
        private Label _welcomeLabel;
        private TableLayoutPanel _contentLayout;
        private TableLayoutPanel _statsLayout;
        private Panel _revenueCard;
        private Panel _ordersCard;
        private Panel _stockCard;
        private Panel _customersCard;
        private Label _revenueValue;
        private Label _ordersValue;
        private Label _stockValue;
        private Label _customersValue;
        private Label _revenueCaption;
        private Label _ordersCaption;
        private Label _stockCaption;
        private Label _customersCaption;
        private Panel _activityCard;
        private Label _activityTitle;
        private Label _activitySubtitle;
        private DataGridView _activityGrid;
        private DataGridViewTextBoxColumn _activityColumn;
        private DataGridViewTextBoxColumn _userColumn;
        private DataGridViewTextBoxColumn _timeColumn;
        private DataGridViewTextBoxColumn _statusColumn;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._root = new TableLayoutPanel();
            this._headerPanel = new Panel();
            this._titleLabel = new Label();
            this._welcomeLabel = new Label();
            this._contentLayout = new TableLayoutPanel();
            this._statsLayout = new TableLayoutPanel();
            this._revenueCard = new Panel();
            this._ordersCard = new Panel();
            this._stockCard = new Panel();
            this._customersCard = new Panel();
            this._revenueValue = new Label();
            this._ordersValue = new Label();
            this._stockValue = new Label();
            this._customersValue = new Label();
            this._revenueCaption = new Label();
            this._ordersCaption = new Label();
            this._stockCaption = new Label();
            this._customersCaption = new Label();
            this._activityCard = new Panel();
            this._activityTitle = new Label();
            this._activitySubtitle = new Label();
            this._activityGrid = new DataGridView();
            this._activityColumn = new DataGridViewTextBoxColumn();
            this._userColumn = new DataGridViewTextBoxColumn();
            this._timeColumn = new DataGridViewTextBoxColumn();
            this._statusColumn = new DataGridViewTextBoxColumn();
            this._root.SuspendLayout();
            this._headerPanel.SuspendLayout();
            this._contentLayout.SuspendLayout();
            this._statsLayout.SuspendLayout();
            this._revenueCard.SuspendLayout();
            this._ordersCard.SuspendLayout();
            this._stockCard.SuspendLayout();
            this._customersCard.SuspendLayout();
            this._activityCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._activityGrid)).BeginInit();
            this.SuspendLayout();
            //
            // root
            //
            this._root.ColumnCount = 1;
            this._root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this._root.Dock = DockStyle.Fill;
            this._root.Padding = new Padding(20, 16, 20, 20);
            this._root.RowCount = 2;
            this._root.RowStyles.Add(new RowStyle(SizeType.Absolute, 92F));
            this._root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this._root.Controls.Add(this._headerPanel, 0, 0);
            this._root.Controls.Add(this._contentLayout, 0, 1);
            //
            // header
            //
            this._headerPanel.Controls.Add(this._welcomeLabel);
            this._headerPanel.Controls.Add(this._titleLabel);
            this._headerPanel.Dock = DockStyle.Fill;
            this._titleLabel.AutoSize = true;
            this._titleLabel.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            this._titleLabel.ForeColor = Color.FromArgb(15, 23, 42);
            this._titleLabel.Location = new Point(0, 0);
            this._titleLabel.Text = "Tổng quan";
            this._welcomeLabel.AutoSize = true;
            this._welcomeLabel.Font = new Font("Segoe UI", 9F);
            this._welcomeLabel.ForeColor = Color.FromArgb(100, 116, 139);
            this._welcomeLabel.Location = new Point(2, 47);
            this._welcomeLabel.Text = "Chào mừng bạn quay trở lại";
            //
            // content
            //
            this._contentLayout.ColumnCount = 1;
            this._contentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this._contentLayout.Dock = DockStyle.Fill;
            this._contentLayout.RowCount = 2;
            this._contentLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 128F));
            this._contentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this._contentLayout.Controls.Add(this._statsLayout, 0, 0);
            this._contentLayout.Controls.Add(this._activityCard, 0, 1);
            //
            // statistics
            //
            this._statsLayout.ColumnCount = 4;
            this._statsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this._statsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this._statsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this._statsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this._statsLayout.Dock = DockStyle.Fill;
            this._statsLayout.Controls.Add(this._revenueCard, 0, 0);
            this._statsLayout.Controls.Add(this._ordersCard, 1, 0);
            this._statsLayout.Controls.Add(this._stockCard, 2, 0);
            this._statsLayout.Controls.Add(this._customersCard, 3, 0);
            this._revenueCard.BackColor = Color.White;
            this._revenueCard.Controls.Add(this._revenueCaption);
            this._revenueCard.Controls.Add(this._revenueValue);
            this._revenueCard.Dock = DockStyle.Fill;
            this._revenueCard.Margin = new Padding(0, 0, 10, 0);
            this._revenueCard.Padding = new Padding(16);
            this._ordersCard.BackColor = Color.White;
            this._ordersCard.Controls.Add(this._ordersCaption);
            this._ordersCard.Controls.Add(this._ordersValue);
            this._ordersCard.Dock = DockStyle.Fill;
            this._ordersCard.Margin = new Padding(4, 0, 6, 0);
            this._ordersCard.Padding = new Padding(16);
            this._stockCard.BackColor = Color.White;
            this._stockCard.Controls.Add(this._stockCaption);
            this._stockCard.Controls.Add(this._stockValue);
            this._stockCard.Dock = DockStyle.Fill;
            this._stockCard.Margin = new Padding(6, 0, 4, 0);
            this._stockCard.Padding = new Padding(16);
            this._customersCard.BackColor = Color.White;
            this._customersCard.Controls.Add(this._customersCaption);
            this._customersCard.Controls.Add(this._customersValue);
            this._customersCard.Dock = DockStyle.Fill;
            this._customersCard.Margin = new Padding(10, 0, 0, 0);
            this._customersCard.Padding = new Padding(16);
            this._revenueValue.AutoSize = true;
            this._revenueValue.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this._revenueValue.ForeColor = Color.FromArgb(37, 99, 235);
            this._revenueValue.Location = new Point(16, 20);
            this._revenueValue.Text = "12.450.000 ₫";
            this._revenueCaption.AutoSize = true;
            this._revenueCaption.ForeColor = Color.FromArgb(100, 116, 139);
            this._revenueCaption.Location = new Point(18, 66);
            this._revenueCaption.Text = "Doanh thu hôm nay";
            this._ordersValue.AutoSize = true;
            this._ordersValue.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this._ordersValue.ForeColor = Color.FromArgb(16, 185, 129);
            this._ordersValue.Location = new Point(16, 20);
            this._ordersValue.Text = "48";
            this._ordersCaption.AutoSize = true;
            this._ordersCaption.ForeColor = Color.FromArgb(100, 116, 139);
            this._ordersCaption.Location = new Point(18, 66);
            this._ordersCaption.Text = "Đơn hàng mới";
            this._stockValue.AutoSize = true;
            this._stockValue.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this._stockValue.ForeColor = Color.FromArgb(245, 158, 11);
            this._stockValue.Location = new Point(16, 20);
            this._stockValue.Text = "1.284";
            this._stockCaption.AutoSize = true;
            this._stockCaption.ForeColor = Color.FromArgb(100, 116, 139);
            this._stockCaption.Location = new Point(18, 66);
            this._stockCaption.Text = "Sản phẩm tồn";
            this._customersValue.AutoSize = true;
            this._customersValue.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this._customersValue.ForeColor = Color.FromArgb(139, 92, 246);
            this._customersValue.Location = new Point(16, 20);
            this._customersValue.Text = "326";
            this._customersCaption.AutoSize = true;
            this._customersCaption.ForeColor = Color.FromArgb(100, 116, 139);
            this._customersCaption.Location = new Point(18, 66);
            this._customersCaption.Text = "Khách hàng";
            //
            // recent activity
            //
            this._activityCard.BackColor = Color.White;
            this._activityCard.Controls.Add(this._activityGrid);
            this._activityCard.Controls.Add(this._activitySubtitle);
            this._activityCard.Controls.Add(this._activityTitle);
            this._activityCard.Dock = DockStyle.Fill;
            this._activityCard.Margin = new Padding(0, 14, 0, 0);
            this._activityCard.Padding = new Padding(18);
            this._activityTitle.AutoSize = true;
            this._activityTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            this._activityTitle.ForeColor = Color.FromArgb(15, 23, 42);
            this._activityTitle.Location = new Point(18, 14);
            this._activityTitle.Text = "Hoạt động gần đây";
            this._activitySubtitle.AutoSize = true;
            this._activitySubtitle.Font = new Font("Segoe UI", 9F);
            this._activitySubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            this._activitySubtitle.Location = new Point(20, 45);
            this._activitySubtitle.Text = "Các giao dịch và sự kiện mới nhất trong hệ thống";
            this._activityGrid.AllowUserToAddRows = false;
            this._activityGrid.AllowUserToDeleteRows = false;
            this._activityGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this._activityGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this._activityGrid.BackgroundColor = Color.White;
            this._activityGrid.BorderStyle = BorderStyle.None;
            this._activityGrid.ColumnHeadersHeight = 36;
            this._activityGrid.Columns.AddRange(new DataGridViewColumn[] {
                this._activityColumn, this._userColumn, this._timeColumn, this._statusColumn});
            this._activityGrid.Location = new Point(18, 80);
            this._activityGrid.ReadOnly = true;
            this._activityGrid.RowHeadersVisible = false;
            this._activityGrid.Size = new Size(900, 300);
            this._activityColumn.HeaderText = "Hoạt động";
            this._activityColumn.Name = "Activity";
            this._userColumn.HeaderText = "Người thực hiện";
            this._userColumn.Name = "User";
            this._timeColumn.HeaderText = "Thời gian";
            this._timeColumn.Name = "Time";
            this._statusColumn.HeaderText = "Trạng thái";
            this._statusColumn.Name = "Status";
            //
            // UserControl
            //
            this.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = Color.FromArgb(244, 247, 250);
            this.Controls.Add(this._root);
            this.Dock = DockStyle.Fill;
            this.Font = new Font("Segoe UI", 9F);
            this.Margin = Padding.Empty;
            this.Name = "ucDashboard";
            this.Size = new Size(1000, 700);
            this._root.ResumeLayout(false);
            this._headerPanel.ResumeLayout(false);
            this._headerPanel.PerformLayout();
            this._contentLayout.ResumeLayout(false);
            this._statsLayout.ResumeLayout(false);
            this._revenueCard.ResumeLayout(false);
            this._revenueCard.PerformLayout();
            this._ordersCard.ResumeLayout(false);
            this._ordersCard.PerformLayout();
            this._stockCard.ResumeLayout(false);
            this._stockCard.PerformLayout();
            this._customersCard.ResumeLayout(false);
            this._customersCard.PerformLayout();
            this._activityCard.ResumeLayout(false);
            this._activityCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._activityGrid)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
