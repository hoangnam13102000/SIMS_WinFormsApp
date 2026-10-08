using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Views.SystemManagement
{
    partial class ucSystemSettings
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel _root;
        private Panel _header;
        private Label _title;
        private Label _subtitle;
        private Button _saveButton;
        private Panel _storeCard;
        private Label _storeCardTitle;
        private Label _storeNameLabel;
        private TextBox _storeName;
        private Label _defaultUnitLabel;
        private TextBox _defaultUnit;
        private TableLayoutPanel _lowerCards;
        private Panel _pricingCard;
        private Label _pricingCardTitle;
        private Label _vatRateLabel;
        private TextBox _vatRate;
        private Label _defaultMarginLabel;
        private TextBox _defaultMargin;
        private Panel _policyCard;
        private Label _policyCardTitle;
        private Label _returnDaysLabel;
        private TextBox _returnDays;
        private Label _approvalThresholdLabel;
        private TextBox _approvalThreshold;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                VisibleChanged -= OnVisibleChanged;
                if (components != null) components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            _root = new TableLayoutPanel();
            _header = new Panel();
            _title = new Label();
            _subtitle = new Label();
            _saveButton = new Button();
            _storeCard = new Panel();
            _storeCardTitle = new Label();
            _storeNameLabel = new Label();
            _storeName = new TextBox();
            _defaultUnitLabel = new Label();
            _defaultUnit = new TextBox();
            _lowerCards = new TableLayoutPanel();
            _pricingCard = new Panel();
            _pricingCardTitle = new Label();
            _vatRateLabel = new Label();
            _vatRate = new TextBox();
            _defaultMarginLabel = new Label();
            _defaultMargin = new TextBox();
            _policyCard = new Panel();
            _policyCardTitle = new Label();
            _returnDaysLabel = new Label();
            _returnDays = new TextBox();
            _approvalThresholdLabel = new Label();
            _approvalThreshold = new TextBox();
            _root.SuspendLayout();
            _header.SuspendLayout();
            _storeCard.SuspendLayout();
            _lowerCards.SuspendLayout();
            _pricingCard.SuspendLayout();
            _policyCard.SuspendLayout();
            SuspendLayout();

            _root.ColumnCount = 1;
            _root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _root.Dock = DockStyle.Fill;
            _root.Padding = new Padding(24);
            _root.RowCount = 3;
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 88F));
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 160F));
            _root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            _root.Controls.Add(_header, 0, 0);
            _root.Controls.Add(_storeCard, 0, 1);
            _root.Controls.Add(_lowerCards, 0, 2);

            _header.Controls.Add(_subtitle);
            _header.Controls.Add(_title);
            _header.Controls.Add(_saveButton);
            _header.Dock = DockStyle.Fill;
            _title.AutoSize = true;
            _title.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            _title.ForeColor = Color.FromArgb(15, 23, 42);
            _title.Location = new Point(0, 0);
            _title.Text = "Cài đặt hệ thống";
            _subtitle.AutoSize = true;
            _subtitle.ForeColor = Color.FromArgb(100, 116, 139);
            _subtitle.Location = new Point(2, 46);
            _subtitle.Text = "Cấu hình chung áp dụng cho toàn bộ cửa hàng";
            _saveButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _saveButton.BackColor = Color.FromArgb(37, 99, 235);
            _saveButton.FlatStyle = FlatStyle.Flat;
            _saveButton.FlatAppearance.BorderSize = 0;
            _saveButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            _saveButton.ForeColor = Color.White;
            _saveButton.Location = new Point(814, 16);
            _saveButton.Name = "_saveButton";
            _saveButton.Size = new Size(138, 40);
            _saveButton.Text = "Lưu thay đổi";
            _saveButton.UseVisualStyleBackColor = false;

            _storeCard.BackColor = Color.White;
            _storeCard.BorderStyle = BorderStyle.FixedSingle;
            _storeCard.Controls.Add(_storeCardTitle);
            _storeCard.Controls.Add(_storeNameLabel);
            _storeCard.Controls.Add(_storeName);
            _storeCard.Controls.Add(_defaultUnitLabel);
            _storeCard.Controls.Add(_defaultUnit);
            _storeCard.Dock = DockStyle.Fill;
            _storeCard.Margin = new Padding(0, 0, 0, 16);
            _storeCardTitle.AutoSize = true;
            _storeCardTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            _storeCardTitle.ForeColor = Color.FromArgb(15, 23, 42);
            _storeCardTitle.Location = new Point(16, 12);
            _storeCardTitle.Text = "Thông tin cửa hàng";
            _storeNameLabel.AutoSize = true;
            _storeNameLabel.Location = new Point(18, 48);
            _storeNameLabel.Text = "Tên cửa hàng";
            _storeName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _storeName.Location = new Point(18, 70);
            _storeName.MaxLength = 150;
            _storeName.Name = "_storeName";
            _storeName.Size = new Size(420, 23);
            _defaultUnitLabel.AutoSize = true;
            _defaultUnitLabel.Location = new Point(468, 48);
            _defaultUnitLabel.Text = "Đơn vị tính mặc định";
            _defaultUnit.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _defaultUnit.Location = new Point(468, 70);
            _defaultUnit.MaxLength = 30;
            _defaultUnit.Name = "_defaultUnit";
            _defaultUnit.Size = new Size(420, 23);

            _lowerCards.ColumnCount = 2;
            _lowerCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            _lowerCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            _lowerCards.Dock = DockStyle.Fill;
            _lowerCards.Margin = new Padding(0);
            _lowerCards.RowCount = 1;
            _lowerCards.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            _lowerCards.Controls.Add(_pricingCard, 0, 0);
            _lowerCards.Controls.Add(_policyCard, 1, 0);

            _pricingCard.BackColor = Color.White;
            _pricingCard.BorderStyle = BorderStyle.FixedSingle;
            _pricingCard.Controls.Add(_pricingCardTitle);
            _pricingCard.Controls.Add(_vatRateLabel);
            _pricingCard.Controls.Add(_vatRate);
            _pricingCard.Controls.Add(_defaultMarginLabel);
            _pricingCard.Controls.Add(_defaultMargin);
            _pricingCard.Dock = DockStyle.Fill;
            _pricingCard.Margin = new Padding(0, 0, 8, 0);
            _pricingCardTitle.AutoSize = true;
            _pricingCardTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            _pricingCardTitle.ForeColor = Color.FromArgb(15, 23, 42);
            _pricingCardTitle.Location = new Point(16, 12);
            _pricingCardTitle.Text = "Thuế và chính sách giá";
            _vatRateLabel.AutoSize = true;
            _vatRateLabel.Location = new Point(18, 52);
            _vatRateLabel.Text = "Thuế GTGT - VAT (%)";
            _vatRate.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _vatRate.Location = new Point(18, 74);
            _vatRate.MaxLength = 6;
            _vatRate.Name = "_vatRate";
            _vatRate.Size = new Size(385, 23);
            _defaultMarginLabel.AutoSize = true;
            _defaultMarginLabel.Location = new Point(18, 112);
            _defaultMarginLabel.Text = "Chênh lệch giá bán (VNĐ)";
            _defaultMargin.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _defaultMargin.Location = new Point(18, 134);
            _defaultMargin.MaxLength = 12;
            _defaultMargin.Name = "_defaultMargin";
            _defaultMargin.Size = new Size(385, 23);

            _policyCard.BackColor = Color.White;
            _policyCard.BorderStyle = BorderStyle.FixedSingle;
            _policyCard.Controls.Add(_policyCardTitle);
            _policyCard.Controls.Add(_returnDaysLabel);
            _policyCard.Controls.Add(_returnDays);
            _policyCard.Controls.Add(_approvalThresholdLabel);
            _policyCard.Controls.Add(_approvalThreshold);
            _policyCard.Dock = DockStyle.Fill;
            _policyCard.Margin = new Padding(8, 0, 0, 0);
            _policyCardTitle.AutoSize = true;
            _policyCardTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            _policyCardTitle.ForeColor = Color.FromArgb(15, 23, 42);
            _policyCardTitle.Location = new Point(16, 12);
            _policyCardTitle.Text = "Chính sách đổi trả";
            _returnDaysLabel.AutoSize = true;
            _returnDaysLabel.Location = new Point(18, 52);
            _returnDaysLabel.Text = "Số ngày đổi/trả";
            _returnDays.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _returnDays.Location = new Point(18, 74);
            _returnDays.MaxLength = 5;
            _returnDays.Name = "_returnDays";
            _returnDays.Size = new Size(385, 23);
            _approvalThresholdLabel.AutoSize = true;
            _approvalThresholdLabel.Location = new Point(18, 112);
            _approvalThresholdLabel.Text = "Ngưỡng cần duyệt (VNĐ)";
            _approvalThreshold.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _approvalThreshold.Location = new Point(18, 134);
            _approvalThreshold.MaxLength = 12;
            _approvalThreshold.Name = "_approvalThreshold";
            _approvalThreshold.Size = new Size(385, 23);

            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 247, 250);
            Controls.Add(_root);
            Name = "ucSystemSettings";
            Size = new Size(1000, 700);
            _root.ResumeLayout(false);
            _header.ResumeLayout(false);
            _header.PerformLayout();
            _storeCard.ResumeLayout(false);
            _storeCard.PerformLayout();
            _lowerCards.ResumeLayout(false);
            _pricingCard.ResumeLayout(false);
            _pricingCard.PerformLayout();
            _policyCard.ResumeLayout(false);
            _policyCard.PerformLayout();
            ResumeLayout(false);
        }
    }
}
