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
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this._root = new System.Windows.Forms.TableLayoutPanel();
            this._headerPanel = new System.Windows.Forms.Panel();
            this._welcomeLabel = new System.Windows.Forms.Label();
            this._titleLabel = new System.Windows.Forms.Label();
            this._contentLayout = new System.Windows.Forms.TableLayoutPanel();
            this._statsLayout = new System.Windows.Forms.TableLayoutPanel();
            this._revenueCard = new System.Windows.Forms.Panel();
            this._revenueCaption = new System.Windows.Forms.Label();
            this._revenueValue = new System.Windows.Forms.Label();
            this._ordersCard = new System.Windows.Forms.Panel();
            this._ordersCaption = new System.Windows.Forms.Label();
            this._ordersValue = new System.Windows.Forms.Label();
            this._stockCard = new System.Windows.Forms.Panel();
            this._stockCaption = new System.Windows.Forms.Label();
            this._stockValue = new System.Windows.Forms.Label();
            this._customersCard = new System.Windows.Forms.Panel();
            this._customersCaption = new System.Windows.Forms.Label();
            this._customersValue = new System.Windows.Forms.Label();
            this._activityCard = new System.Windows.Forms.Panel();
            this._activityGrid = new System.Windows.Forms.DataGridView();
            this.Activity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.User = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._activitySubtitle = new System.Windows.Forms.Label();
            this._activityTitle = new System.Windows.Forms.Label();
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
            // _root
            // 
            this._root.ColumnCount = 1;
            this._root.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._root.Controls.Add(this._headerPanel, 0, 0);
            this._root.Controls.Add(this._contentLayout, 0, 1);
            this._root.Dock = System.Windows.Forms.DockStyle.Fill;
            this._root.Location = new System.Drawing.Point(0, 0);
            this._root.Name = "_root";
            this._root.Padding = new System.Windows.Forms.Padding(20, 16, 20, 20);
            this._root.RowCount = 2;
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 92F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._root.Size = new System.Drawing.Size(1251, 857);
            this._root.TabIndex = 0;
            // 
            // _headerPanel
            // 
            this._headerPanel.Controls.Add(this._welcomeLabel);
            this._headerPanel.Controls.Add(this._titleLabel);
            this._headerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._headerPanel.Location = new System.Drawing.Point(23, 19);
            this._headerPanel.Name = "_headerPanel";
            this._headerPanel.Size = new System.Drawing.Size(1205, 86);
            this._headerPanel.TabIndex = 0;
            // 
            // _welcomeLabel
            // 
            this._welcomeLabel.AutoSize = true;
            this._welcomeLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._welcomeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._welcomeLabel.Location = new System.Drawing.Point(2, 60);
            this._welcomeLabel.Name = "_welcomeLabel";
            this._welcomeLabel.Size = new System.Drawing.Size(235, 25);
            this._welcomeLabel.TabIndex = 0;
            this._welcomeLabel.Text = "Chào mừng bạn quay trở lại";
            // 
            // _titleLabel
            // 
            this._titleLabel.AutoSize = true;
            this._titleLabel.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this._titleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._titleLabel.Location = new System.Drawing.Point(-3, 0);
            this._titleLabel.Name = "_titleLabel";
            this._titleLabel.Size = new System.Drawing.Size(249, 60);
            this._titleLabel.TabIndex = 1;
            this._titleLabel.Text = "Tổng quan";
            this._titleLabel.Click += new System.EventHandler(this._titleLabel_Click);
            // 
            // _contentLayout
            // 
            this._contentLayout.ColumnCount = 1;
            this._contentLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._contentLayout.Controls.Add(this._statsLayout, 0, 0);
            this._contentLayout.Controls.Add(this._activityCard, 0, 1);
            this._contentLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._contentLayout.Location = new System.Drawing.Point(23, 111);
            this._contentLayout.Name = "_contentLayout";
            this._contentLayout.RowCount = 2;
            this._contentLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 128F));
            this._contentLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._contentLayout.Size = new System.Drawing.Size(1205, 723);
            this._contentLayout.TabIndex = 1;
            // 
            // _statsLayout
            // 
            this._statsLayout.ColumnCount = 4;
            this._statsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this._statsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this._statsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this._statsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this._statsLayout.Controls.Add(this._revenueCard, 0, 0);
            this._statsLayout.Controls.Add(this._ordersCard, 1, 0);
            this._statsLayout.Controls.Add(this._stockCard, 2, 0);
            this._statsLayout.Controls.Add(this._customersCard, 3, 0);
            this._statsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._statsLayout.Location = new System.Drawing.Point(3, 3);
            this._statsLayout.Name = "_statsLayout";
            this._statsLayout.Size = new System.Drawing.Size(1199, 122);
            this._statsLayout.TabIndex = 0;
            // 
            // _revenueCard
            // 
            this._revenueCard.BackColor = System.Drawing.Color.White;
            this._revenueCard.Controls.Add(this._revenueCaption);
            this._revenueCard.Controls.Add(this._revenueValue);
            this._revenueCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._revenueCard.Location = new System.Drawing.Point(0, 0);
            this._revenueCard.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this._revenueCard.Name = "_revenueCard";
            this._revenueCard.Padding = new System.Windows.Forms.Padding(16);
            this._revenueCard.Size = new System.Drawing.Size(289, 122);
            this._revenueCard.TabIndex = 0;
            // 
            // _revenueCaption
            // 
            this._revenueCaption.AutoSize = true;
            this._revenueCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._revenueCaption.Location = new System.Drawing.Point(18, 66);
            this._revenueCaption.Name = "_revenueCaption";
            this._revenueCaption.Size = new System.Drawing.Size(171, 25);
            this._revenueCaption.TabIndex = 0;
            this._revenueCaption.Text = "Doanh thu hôm nay";
            // 
            // _revenueValue
            // 
            this._revenueValue.AutoSize = true;
            this._revenueValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this._revenueValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._revenueValue.Location = new System.Drawing.Point(16, 20);
            this._revenueValue.Name = "_revenueValue";
            this._revenueValue.Size = new System.Drawing.Size(241, 48);
            this._revenueValue.TabIndex = 1;
            this._revenueValue.Text = "12.450.000 ₫";
            // 
            // _ordersCard
            // 
            this._ordersCard.BackColor = System.Drawing.Color.White;
            this._ordersCard.Controls.Add(this._ordersCaption);
            this._ordersCard.Controls.Add(this._ordersValue);
            this._ordersCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ordersCard.Location = new System.Drawing.Point(303, 0);
            this._ordersCard.Margin = new System.Windows.Forms.Padding(4, 0, 6, 0);
            this._ordersCard.Name = "_ordersCard";
            this._ordersCard.Padding = new System.Windows.Forms.Padding(16);
            this._ordersCard.Size = new System.Drawing.Size(289, 122);
            this._ordersCard.TabIndex = 1;
            // 
            // _ordersCaption
            // 
            this._ordersCaption.AutoSize = true;
            this._ordersCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._ordersCaption.Location = new System.Drawing.Point(18, 66);
            this._ordersCaption.Name = "_ordersCaption";
            this._ordersCaption.Size = new System.Drawing.Size(127, 25);
            this._ordersCaption.TabIndex = 0;
            this._ordersCaption.Text = "Đơn hàng mới";
            // 
            // _ordersValue
            // 
            this._ordersValue.AutoSize = true;
            this._ordersValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this._ordersValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this._ordersValue.Location = new System.Drawing.Point(16, 20);
            this._ordersValue.Name = "_ordersValue";
            this._ordersValue.Size = new System.Drawing.Size(62, 48);
            this._ordersValue.TabIndex = 1;
            this._ordersValue.Text = "48";
            // 
            // _stockCard
            // 
            this._stockCard.BackColor = System.Drawing.Color.White;
            this._stockCard.Controls.Add(this._stockCaption);
            this._stockCard.Controls.Add(this._stockValue);
            this._stockCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._stockCard.Location = new System.Drawing.Point(604, 0);
            this._stockCard.Margin = new System.Windows.Forms.Padding(6, 0, 4, 0);
            this._stockCard.Name = "_stockCard";
            this._stockCard.Padding = new System.Windows.Forms.Padding(16);
            this._stockCard.Size = new System.Drawing.Size(289, 122);
            this._stockCard.TabIndex = 2;
            // 
            // _stockCaption
            // 
            this._stockCaption.AutoSize = true;
            this._stockCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._stockCaption.Location = new System.Drawing.Point(18, 66);
            this._stockCaption.Name = "_stockCaption";
            this._stockCaption.Size = new System.Drawing.Size(124, 25);
            this._stockCaption.TabIndex = 0;
            this._stockCaption.Text = "Sản phẩm tồn";
            // 
            // _stockValue
            // 
            this._stockValue.AutoSize = true;
            this._stockValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this._stockValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this._stockValue.Location = new System.Drawing.Point(16, 20);
            this._stockValue.Name = "_stockValue";
            this._stockValue.Size = new System.Drawing.Size(114, 48);
            this._stockValue.TabIndex = 1;
            this._stockValue.Text = "1.284";
            // 
            // _customersCard
            // 
            this._customersCard.BackColor = System.Drawing.Color.White;
            this._customersCard.Controls.Add(this._customersCaption);
            this._customersCard.Controls.Add(this._customersValue);
            this._customersCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._customersCard.Location = new System.Drawing.Point(907, 0);
            this._customersCard.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this._customersCard.Name = "_customersCard";
            this._customersCard.Padding = new System.Windows.Forms.Padding(16);
            this._customersCard.Size = new System.Drawing.Size(292, 122);
            this._customersCard.TabIndex = 3;
            // 
            // _customersCaption
            // 
            this._customersCaption.AutoSize = true;
            this._customersCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._customersCaption.Location = new System.Drawing.Point(18, 66);
            this._customersCaption.Name = "_customersCaption";
            this._customersCaption.Size = new System.Drawing.Size(104, 25);
            this._customersCaption.TabIndex = 0;
            this._customersCaption.Text = "Khách hàng";
            // 
            // _customersValue
            // 
            this._customersValue.AutoSize = true;
            this._customersValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this._customersValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(92)))), ((int)(((byte)(246)))));
            this._customersValue.Location = new System.Drawing.Point(16, 20);
            this._customersValue.Name = "_customersValue";
            this._customersValue.Size = new System.Drawing.Size(83, 48);
            this._customersValue.TabIndex = 1;
            this._customersValue.Text = "326";
            // 
            // _activityCard
            // 
            this._activityCard.BackColor = System.Drawing.Color.White;
            this._activityCard.Controls.Add(this._activityGrid);
            this._activityCard.Controls.Add(this._activitySubtitle);
            this._activityCard.Controls.Add(this._activityTitle);
            this._activityCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._activityCard.Location = new System.Drawing.Point(0, 142);
            this._activityCard.Margin = new System.Windows.Forms.Padding(0, 14, 0, 0);
            this._activityCard.Name = "_activityCard";
            this._activityCard.Padding = new System.Windows.Forms.Padding(18);
            this._activityCard.Size = new System.Drawing.Size(1205, 581);
            this._activityCard.TabIndex = 1;
            // 
            // _activityGrid
            // 
            this._activityGrid.AllowUserToAddRows = false;
            this._activityGrid.AllowUserToDeleteRows = false;
            this._activityGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._activityGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._activityGrid.BackgroundColor = System.Drawing.Color.White;
            this._activityGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._activityGrid.ColumnHeadersHeight = 36;
            this._activityGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Activity,
            this.User,
            this.Time,
            this.Status});
            this._activityGrid.Location = new System.Drawing.Point(18, 80);
            this._activityGrid.Name = "_activityGrid";
            this._activityGrid.ReadOnly = true;
            this._activityGrid.RowHeadersVisible = false;
            this._activityGrid.RowHeadersWidth = 62;
            this._activityGrid.Size = new System.Drawing.Size(1905, 781);
            this._activityGrid.TabIndex = 0;
            // 
            // Activity
            // 
            this.Activity.HeaderText = "Hoạt động";
            this.Activity.MinimumWidth = 8;
            this.Activity.Name = "Activity";
            this.Activity.ReadOnly = true;
            // 
            // User
            // 
            this.User.HeaderText = "Người thực hiện";
            this.User.MinimumWidth = 8;
            this.User.Name = "User";
            this.User.ReadOnly = true;
            // 
            // Time
            // 
            this.Time.HeaderText = "Thời gian";
            this.Time.MinimumWidth = 8;
            this.Time.Name = "Time";
            this.Time.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.HeaderText = "Trạng thái";
            this.Status.MinimumWidth = 8;
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // _activitySubtitle
            // 
            this._activitySubtitle.AutoSize = true;
            this._activitySubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._activitySubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._activitySubtitle.Location = new System.Drawing.Point(20, 45);
            this._activitySubtitle.Name = "_activitySubtitle";
            this._activitySubtitle.Size = new System.Drawing.Size(404, 25);
            this._activitySubtitle.TabIndex = 1;
            this._activitySubtitle.Text = "Các giao dịch và sự kiện mới nhất trong hệ thống";
            // 
            // _activityTitle
            // 
            this._activityTitle.AutoSize = true;
            this._activityTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this._activityTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._activityTitle.Location = new System.Drawing.Point(18, 14);
            this._activityTitle.Name = "_activityTitle";
            this._activityTitle.Size = new System.Drawing.Size(250, 36);
            this._activityTitle.TabIndex = 2;
            this._activityTitle.Text = "Hoạt động gần đây";
            this._activityTitle.Click += new System.EventHandler(this._activityTitle_Click);
            // 
            // ucDashboard
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.Controls.Add(this._root);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ucDashboard";
            this.Size = new System.Drawing.Size(1251, 857);
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

        private DataGridViewTextBoxColumn Activity;
        private DataGridViewTextBoxColumn User;
        private DataGridViewTextBoxColumn Time;
        private DataGridViewTextBoxColumn Status;
    }
}
