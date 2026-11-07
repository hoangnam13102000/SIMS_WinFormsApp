using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Views.Warehouse
{
    partial class ucExpiredBatchDisposal
    {
        private TableLayoutPanel _root;
        private Panel _header;
        private Label _titleLabel;
        private Label _subtitleLabel;
        private Label _statusLabel;
        private TableLayoutPanel _details;
        private Label _productCaption;
        private Label _productValue;
        private Label _batchCaption;
        private Label _batchValue;
        private Label _expiryCaption;
        private Label _expiryValue;
        private Label _quantityCaption;
        private Label _quantityValue;
        private Label _lossCaption;
        private Label _lossValue;
        private FlowLayoutPanel _buttons;
        private Button _confirmButton;
        private Button _cancelButton;

        private void InitializeComponent()
        {
            _root = new TableLayoutPanel();
            _header = new Panel();
            _titleLabel = new Label();
            _subtitleLabel = new Label();
            _statusLabel = new Label();
            _details = new TableLayoutPanel();
            _productCaption = new Label();
            _productValue = new Label();
            _batchCaption = new Label();
            _batchValue = new Label();
            _expiryCaption = new Label();
            _expiryValue = new Label();
            _quantityCaption = new Label();
            _quantityValue = new Label();
            _lossCaption = new Label();
            _lossValue = new Label();
            _buttons = new FlowLayoutPanel();
            _confirmButton = new Button();
            _cancelButton = new Button();
            _root.SuspendLayout();
            _header.SuspendLayout();
            _details.SuspendLayout();
            _buttons.SuspendLayout();
            SuspendLayout();

            _root.BackColor = Color.Transparent;
            _root.ColumnCount = 1;
            _root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _root.Controls.Add(_header, 0, 0);
            _root.Controls.Add(_statusLabel, 0, 1);
            _root.Controls.Add(_details, 0, 2);
            _root.Controls.Add(_buttons, 0, 3);
            _root.Dock = DockStyle.Fill;
            _root.Name = "_root";
            _root.RowCount = 4;
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 290F));
            _root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            _header.Controls.Add(_titleLabel);
            _header.Controls.Add(_subtitleLabel);
            _header.Dock = DockStyle.Fill;
            _header.Name = "_header";

            _titleLabel.AutoSize = true;
            _titleLabel.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
            _titleLabel.ForeColor = Color.FromArgb(15, 23, 42);
            _titleLabel.Location = new Point(0, 0);
            _titleLabel.Name = "_titleLabel";
            _titleLabel.Text = "Lập phiếu tiêu hủy hàng hết hạn";

            _subtitleLabel.AutoSize = true;
            _subtitleLabel.Font = new Font("Segoe UI", 9.5F);
            _subtitleLabel.ForeColor = Color.FromArgb(100, 116, 139);
            _subtitleLabel.Location = new Point(2, 50);
            _subtitleLabel.Name = "_subtitleLabel";
            _subtitleLabel.Text = "Kiểm tra đúng lô và số lượng trước khi xác nhận trừ tồn kho.";

            _statusLabel.Dock = DockStyle.Fill;
            _statusLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _statusLabel.ForeColor = Color.FromArgb(153, 27, 27);
            _statusLabel.Name = "_statusLabel";
            _statusLabel.Text = "Đang tải thông tin lô...";
            _statusLabel.TextAlign = ContentAlignment.MiddleLeft;

            _details.BackColor = Color.White;
            _details.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            _details.ColumnCount = 2;
            _details.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));
            _details.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 68F));
            _details.Controls.Add(_productCaption, 0, 0);
            _details.Controls.Add(_productValue, 1, 0);
            _details.Controls.Add(_batchCaption, 0, 1);
            _details.Controls.Add(_batchValue, 1, 1);
            _details.Controls.Add(_expiryCaption, 0, 2);
            _details.Controls.Add(_expiryValue, 1, 2);
            _details.Controls.Add(_quantityCaption, 0, 3);
            _details.Controls.Add(_quantityValue, 1, 3);
            _details.Controls.Add(_lossCaption, 0, 4);
            _details.Controls.Add(_lossValue, 1, 4);
            _details.Dock = DockStyle.Top;
            _details.Height = 260;
            _details.Name = "_details";
            _details.Padding = new Padding(18);
            _details.RowCount = 5;
            _details.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            _details.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            _details.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            _details.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            _details.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));

            _productCaption.Dock = DockStyle.Fill;
            _productCaption.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            _productCaption.ForeColor = Color.FromArgb(71, 85, 105);
            _productCaption.Name = "_productCaption";
            _productCaption.Padding = new Padding(4, 0, 0, 0);
            _productCaption.Text = "Sản phẩm";
            _productCaption.TextAlign = ContentAlignment.MiddleLeft;

            _productValue.AutoEllipsis = true;
            _productValue.Dock = DockStyle.Fill;
            _productValue.Font = new Font("Segoe UI", 10F);
            _productValue.ForeColor = Color.FromArgb(15, 23, 42);
            _productValue.Name = "_productValue";
            _productValue.Padding = new Padding(8, 0, 0, 0);
            _productValue.Text = "—";
            _productValue.TextAlign = ContentAlignment.MiddleLeft;

            _batchCaption.Dock = DockStyle.Fill;
            _batchCaption.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            _batchCaption.ForeColor = Color.FromArgb(71, 85, 105);
            _batchCaption.Name = "_batchCaption";
            _batchCaption.Padding = new Padding(4, 0, 0, 0);
            _batchCaption.Text = "Mã lô";
            _batchCaption.TextAlign = ContentAlignment.MiddleLeft;

            _batchValue.AutoEllipsis = true;
            _batchValue.Dock = DockStyle.Fill;
            _batchValue.Font = new Font("Segoe UI", 10F);
            _batchValue.ForeColor = Color.FromArgb(15, 23, 42);
            _batchValue.Name = "_batchValue";
            _batchValue.Padding = new Padding(8, 0, 0, 0);
            _batchValue.Text = "—";
            _batchValue.TextAlign = ContentAlignment.MiddleLeft;

            _expiryCaption.Dock = DockStyle.Fill;
            _expiryCaption.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            _expiryCaption.ForeColor = Color.FromArgb(71, 85, 105);
            _expiryCaption.Name = "_expiryCaption";
            _expiryCaption.Padding = new Padding(4, 0, 0, 0);
            _expiryCaption.Text = "Hạn sử dụng";
            _expiryCaption.TextAlign = ContentAlignment.MiddleLeft;

            _expiryValue.AutoEllipsis = true;
            _expiryValue.Dock = DockStyle.Fill;
            _expiryValue.Font = new Font("Segoe UI", 10F);
            _expiryValue.ForeColor = Color.FromArgb(15, 23, 42);
            _expiryValue.Name = "_expiryValue";
            _expiryValue.Padding = new Padding(8, 0, 0, 0);
            _expiryValue.Text = "—";
            _expiryValue.TextAlign = ContentAlignment.MiddleLeft;

            _quantityCaption.Dock = DockStyle.Fill;
            _quantityCaption.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            _quantityCaption.ForeColor = Color.FromArgb(71, 85, 105);
            _quantityCaption.Name = "_quantityCaption";
            _quantityCaption.Padding = new Padding(4, 0, 0, 0);
            _quantityCaption.Text = "Số lượng tiêu hủy";
            _quantityCaption.TextAlign = ContentAlignment.MiddleLeft;

            _quantityValue.AutoEllipsis = true;
            _quantityValue.Dock = DockStyle.Fill;
            _quantityValue.Font = new Font("Segoe UI", 10F);
            _quantityValue.ForeColor = Color.FromArgb(15, 23, 42);
            _quantityValue.Name = "_quantityValue";
            _quantityValue.Padding = new Padding(8, 0, 0, 0);
            _quantityValue.Text = "—";
            _quantityValue.TextAlign = ContentAlignment.MiddleLeft;

            _lossCaption.Dock = DockStyle.Fill;
            _lossCaption.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            _lossCaption.ForeColor = Color.FromArgb(71, 85, 105);
            _lossCaption.Name = "_lossCaption";
            _lossCaption.Padding = new Padding(4, 0, 0, 0);
            _lossCaption.Text = "Giá trị thiệt hại";
            _lossCaption.TextAlign = ContentAlignment.MiddleLeft;

            _lossValue.AutoEllipsis = true;
            _lossValue.Dock = DockStyle.Fill;
            _lossValue.Font = new Font("Segoe UI", 10F);
            _lossValue.ForeColor = Color.FromArgb(15, 23, 42);
            _lossValue.Name = "_lossValue";
            _lossValue.Padding = new Padding(8, 0, 0, 0);
            _lossValue.Text = "—";
            _lossValue.TextAlign = ContentAlignment.MiddleLeft;

            _buttons.Dock = DockStyle.Top;
            _buttons.FlowDirection = FlowDirection.RightToLeft;
            _buttons.Name = "_buttons";
            _buttons.Padding = new Padding(0, 16, 0, 0);
            _buttons.Size = new Size(200, 100);
            _buttons.WrapContents = false;
            _buttons.Controls.Add(_confirmButton);
            _buttons.Controls.Add(_cancelButton);

            _confirmButton.BackColor = Color.FromArgb(220, 38, 38);
            _confirmButton.Enabled = false;
            _confirmButton.FlatAppearance.BorderSize = 0;
            _confirmButton.FlatStyle = FlatStyle.Flat;
            _confirmButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            _confirmButton.ForeColor = Color.White;
            _confirmButton.Margin = new Padding(8, 0, 0, 0);
            _confirmButton.Name = "_confirmButton";
            _confirmButton.Size = new Size(180, 40);
            _confirmButton.Text = "Xác nhận lập phiếu";
            this._confirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);

            _cancelButton.FlatStyle = FlatStyle.Flat;
            _cancelButton.Font = new Font("Segoe UI", 9F);
            _cancelButton.Margin = new Padding(8, 0, 0, 0);
            _cancelButton.Name = "_cancelButton";
            _cancelButton.Size = new Size(180, 40);
            _cancelButton.Text = "Quay lại danh sách lô";
            this._cancelButton.Click += new System.EventHandler(this.CancelButton_Click);

            BackColor = Color.FromArgb(244, 247, 250);
            Controls.Add(_root);
            Name = "ucExpiredBatchDisposal";
            Padding = new Padding(28);
            AutoScaleMode = AutoScaleMode.Font;
            Size = new Size(1200, 760);
            _buttons.ResumeLayout(false);
            _details.ResumeLayout(false);
            _header.ResumeLayout(false);
            _header.PerformLayout();
            _root.ResumeLayout(false);
            ResumeLayout(false);
        }

    }
}
