using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Views.Sales
{
    partial class PromotionDetailsDialog
    {
        private IContainer components = null;
        private TableLayoutPanel _layout;
        private Label _title;
        private Label _subtitle;
        private Panel _heroPanel;
        private Label _codeCaption;
        private Label _codeValue;
        private Label _nameValue;
        private Label _discountCaption;
        private Label _discountValue;
        private Label _statusValue;
        private TableLayoutPanel _cardsLayout;
        private Panel _offerCard;
        private Panel _usageCard;
        private Label _offerCardTitle;
        private Label _typeLabel;
        private Label _typeValue;
        private Label _maximumLabel;
        private Label _maximumValue;
        private Label _minimumLabel;
        private Label _minimumValue;
        private Label _usageCardTitle;
        private Label _startLabel;
        private Label _startValue;
        private Label _endLabel;
        private Label _endValue;
        private Label _limitLabel;
        private Label _limitValue;
        private Label _bannerLabel;
        private Label _bannerValue;
        private Button _closeButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();
            _layout = new TableLayoutPanel();
            _title = new Label();
            _subtitle = new Label();
            _heroPanel = new Panel();
            _codeCaption = new Label();
            _codeValue = new Label();
            _nameValue = new Label();
            _discountCaption = new Label();
            _discountValue = new Label();
            _statusValue = new Label();
            _cardsLayout = new TableLayoutPanel();
            _offerCard = new Panel();
            _usageCard = new Panel();
            _offerCardTitle = new Label();
            _typeLabel = new Label();
            _typeValue = new Label();
            _maximumLabel = new Label();
            _maximumValue = new Label();
            _minimumLabel = new Label();
            _minimumValue = new Label();
            _usageCardTitle = new Label();
            _startLabel = new Label();
            _startValue = new Label();
            _endLabel = new Label();
            _endValue = new Label();
            _limitLabel = new Label();
            _limitValue = new Label();
            _bannerLabel = new Label();
            _bannerValue = new Label();
            _closeButton = new Button();

            _layout.SuspendLayout();
            _heroPanel.SuspendLayout();
            _cardsLayout.SuspendLayout();
            _offerCard.SuspendLayout();
            _usageCard.SuspendLayout();
            SuspendLayout();

            _layout.ColumnCount = 1;
            _layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _layout.Dock = DockStyle.Fill;
            _layout.Padding = new Padding(28, 18, 28, 18);
            _layout.RowCount = 4;
            _layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
            _layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 148F));
            _layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            _layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            _layout.Controls.Add(_title, 0, 0);
            _layout.Controls.Add(_heroPanel, 0, 1);
            _layout.Controls.Add(_cardsLayout, 0, 2);
            _layout.Controls.Add(_closeButton, 0, 3);

            _title.Dock = DockStyle.Fill;
            _title.AutoSize = false;
            _title.Font = new Font("Segoe UI", 19F, FontStyle.Bold);
            _title.ForeColor = Color.FromArgb(15, 23, 42);
            _title.Margin = Padding.Empty;
            _title.Text = "Chi tiết mã giảm giá";
            _title.TextAlign = ContentAlignment.MiddleLeft;

            _heroPanel.BackColor = Color.FromArgb(238, 242, 255);
            _heroPanel.Dock = DockStyle.Fill;
            _heroPanel.Margin = new Padding(0, 4, 0, 16);
            _heroPanel.Padding = new Padding(20, 12, 20, 12);
            _heroPanel.Controls.Add(_codeCaption);
            _heroPanel.Controls.Add(_codeValue);
            _heroPanel.Controls.Add(_nameValue);
            _heroPanel.Controls.Add(_discountCaption);
            _heroPanel.Controls.Add(_discountValue);
            _heroPanel.Controls.Add(_statusValue);

            _codeCaption.AutoSize = true;
            _codeCaption.Font = new Font("Segoe UI", 9F);
            _codeCaption.ForeColor = Color.FromArgb(100, 116, 139);
            _codeCaption.Location = new Point(20, 12);
            _codeCaption.Text = "MÃ ƯU ĐÃI";

            _codeValue.AutoSize = true;
            _codeValue.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            _codeValue.ForeColor = Color.FromArgb(37, 99, 235);
            _codeValue.Location = new Point(18, 30);
            _codeValue.Text = "SUMMER10";

            _nameValue.AutoSize = true;
            _nameValue.Font = new Font("Segoe UI", 10F);
            _nameValue.ForeColor = Color.FromArgb(71, 85, 105);
            _nameValue.Location = new Point(20, 70);
            _nameValue.Text = "Ưu đãi mùa hè";

            _discountCaption.AutoSize = true;
            _discountCaption.Font = new Font("Segoe UI", 9F);
            _discountCaption.ForeColor = Color.FromArgb(100, 116, 139);
            _discountCaption.Location = new Point(500, 20);
            _discountCaption.Text = "MỨC ƯU ĐÃI";

            _discountValue.AutoSize = true;
            _discountValue.Font = new Font("Segoe UI", 25F, FontStyle.Bold);
            _discountValue.ForeColor = Color.FromArgb(22, 163, 74);
            _discountValue.Location = new Point(496, 42);
            _discountValue.Text = "10%";

            _statusValue.AutoSize = true;
            _statusValue.BackColor = Color.FromArgb(220, 252, 231);
            _statusValue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            _statusValue.ForeColor = Color.FromArgb(21, 128, 61);
            _statusValue.Location = new Point(20, 101);
            _statusValue.Padding = new Padding(9, 4, 9, 4);
            _statusValue.Text = "Đang áp dụng";

            _cardsLayout.ColumnCount = 2;
            _cardsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            _cardsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            _cardsLayout.Dock = DockStyle.Fill;
            _cardsLayout.Margin = Padding.Empty;
            _cardsLayout.RowCount = 1;
            _cardsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            _cardsLayout.Controls.Add(_offerCard, 0, 0);
            _cardsLayout.Controls.Add(_usageCard, 1, 0);

            _offerCard.BackColor = Color.White;
            _offerCard.Dock = DockStyle.Fill;
            _offerCard.Margin = new Padding(0, 0, 8, 0);
            _offerCard.Padding = new Padding(16);
            _offerCard.Controls.Add(_offerCardTitle);
            _offerCard.Controls.Add(_typeLabel);
            _offerCard.Controls.Add(_typeValue);
            _offerCard.Controls.Add(_maximumLabel);
            _offerCard.Controls.Add(_maximumValue);
            _offerCard.Controls.Add(_minimumLabel);
            _offerCard.Controls.Add(_minimumValue);

            _offerCardTitle.AutoSize = true;
            _offerCardTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            _offerCardTitle.ForeColor = Color.FromArgb(15, 23, 42);
            _offerCardTitle.Location = new Point(16, 14);
            _offerCardTitle.Text = "Điều kiện ưu đãi";

            _typeLabel.AutoSize = true;
            _typeLabel.Font = new Font("Segoe UI", 8.5F);
            _typeLabel.ForeColor = Color.FromArgb(100, 116, 139);
            _typeLabel.Location = new Point(16, 56);
            _typeLabel.Text = "Loại giảm";
            _typeValue.AutoSize = true;
            _typeValue.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _typeValue.ForeColor = Color.FromArgb(30, 41, 59);
            _typeValue.Location = new Point(16, 74);
            _typeValue.Text = "Phần trăm";

            _maximumLabel.AutoSize = true;
            _maximumLabel.Font = new Font("Segoe UI", 8.5F);
            _maximumLabel.ForeColor = Color.FromArgb(100, 116, 139);
            _maximumLabel.Location = new Point(16, 108);
            _maximumLabel.Text = "Giảm tối đa";
            _maximumValue.AutoSize = true;
            _maximumValue.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _maximumValue.ForeColor = Color.FromArgb(30, 41, 59);
            _maximumValue.Location = new Point(16, 126);
            _maximumValue.Text = "100.000 đ";

            _minimumLabel.AutoSize = true;
            _minimumLabel.Font = new Font("Segoe UI", 8.5F);
            _minimumLabel.ForeColor = Color.FromArgb(100, 116, 139);
            _minimumLabel.Location = new Point(16, 160);
            _minimumLabel.Text = "Đơn tối thiểu";
            _minimumValue.AutoSize = true;
            _minimumValue.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _minimumValue.ForeColor = Color.FromArgb(30, 41, 59);
            _minimumValue.Location = new Point(16, 178);
            _minimumValue.Text = "200.000 đ";

            _usageCard.BackColor = Color.White;
            _usageCard.Dock = DockStyle.Fill;
            _usageCard.Margin = new Padding(8, 0, 0, 0);
            _usageCard.Padding = new Padding(16);
            _usageCard.Controls.Add(_usageCardTitle);
            _usageCard.Controls.Add(_startLabel);
            _usageCard.Controls.Add(_startValue);
            _usageCard.Controls.Add(_endLabel);
            _usageCard.Controls.Add(_endValue);
            _usageCard.Controls.Add(_limitLabel);
            _usageCard.Controls.Add(_limitValue);
            _usageCard.Controls.Add(_bannerLabel);
            _usageCard.Controls.Add(_bannerValue);

            _usageCardTitle.AutoSize = true;
            _usageCardTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            _usageCardTitle.ForeColor = Color.FromArgb(15, 23, 42);
            _usageCardTitle.Location = new Point(16, 14);
            _usageCardTitle.Text = "Thời hạn & sử dụng";

            _startLabel.AutoSize = true;
            _startLabel.Font = new Font("Segoe UI", 8.5F);
            _startLabel.ForeColor = Color.FromArgb(100, 116, 139);
            _startLabel.Location = new Point(16, 56);
            _startLabel.Text = "Ngày bắt đầu";
            _startValue.AutoSize = true;
            _startValue.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _startValue.ForeColor = Color.FromArgb(30, 41, 59);
            _startValue.Location = new Point(16, 74);
            _startValue.Text = "01/06/2026";

            _endLabel.AutoSize = true;
            _endLabel.Font = new Font("Segoe UI", 8.5F);
            _endLabel.ForeColor = Color.FromArgb(100, 116, 139);
            _endLabel.Location = new Point(16, 108);
            _endLabel.Text = "Ngày kết thúc";
            _endValue.AutoSize = true;
            _endValue.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _endValue.ForeColor = Color.FromArgb(30, 41, 59);
            _endValue.Location = new Point(16, 126);
            _endValue.Text = "30/06/2026";

            _limitLabel.AutoSize = true;
            _limitLabel.Font = new Font("Segoe UI", 8.5F);
            _limitLabel.ForeColor = Color.FromArgb(100, 116, 139);
            _limitLabel.Location = new Point(16, 160);
            _limitLabel.Text = "Lượt sử dụng";
            _limitValue.AutoSize = true;
            _limitValue.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _limitValue.ForeColor = Color.FromArgb(30, 41, 59);
            _limitValue.Location = new Point(16, 178);
            _limitValue.Text = "12 / 100";

            _bannerLabel.AutoSize = true;
            _bannerLabel.Font = new Font("Segoe UI", 8.5F);
            _bannerLabel.ForeColor = Color.FromArgb(100, 116, 139);
            _bannerLabel.Location = new Point(16, 212);
            _bannerLabel.Text = "Hiển thị trên banner";
            _bannerValue.AutoSize = true;
            _bannerValue.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _bannerValue.ForeColor = Color.FromArgb(30, 41, 59);
            _bannerValue.Location = new Point(16, 230);
            _bannerValue.Text = "Có";

            _closeButton.Anchor = AnchorStyles.Right;
            _closeButton.BackColor = Color.FromArgb(241, 245, 249);
            _closeButton.DialogResult = DialogResult.OK;
            _closeButton.FlatStyle = FlatStyle.Flat;
            _closeButton.FlatAppearance.BorderSize = 0;
            _closeButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            _closeButton.ForeColor = Color.FromArgb(30, 41, 59);
            _closeButton.Size = new Size(116, 38);
            _closeButton.Text = "Đóng";
            _closeButton.UseVisualStyleBackColor = false;

            AcceptButton = _closeButton;
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 247, 250);
            ClientSize = new Size(820, 650);
            Controls.Add(_layout);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PromotionDetailsDialog";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Chi tiết mã giảm giá";

            _layout.ResumeLayout(false);
            _heroPanel.ResumeLayout(false);
            _heroPanel.PerformLayout();
            _cardsLayout.ResumeLayout(false);
            _offerCard.ResumeLayout(false);
            _offerCard.PerformLayout();
            _usageCard.ResumeLayout(false);
            _usageCard.PerformLayout();
            ResumeLayout(false);
        }
    }
}
