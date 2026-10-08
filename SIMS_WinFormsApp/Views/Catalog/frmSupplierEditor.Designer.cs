using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Views.Catalog
{
    partial class frmSupplierEditor
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel _root;
        private Label _titleLabel;
        private Label _nameLabel;
        private TextBox _nameTextBox;
        private Label _phoneLabel;
        private TextBox _phoneTextBox;
        private Label _emailLabel;
        private TextBox _emailTextBox;
        private Label _addressLabel;
        private TextBox _addressTextBox;
        private Label _itemsLabel;
        private TextBox _itemsTextBox;
        private FlowLayoutPanel _buttons;
        private Button _saveButton;
        private Button _cancelButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            _root = new TableLayoutPanel();
            _titleLabel = new Label();
            _nameLabel = new Label();
            _nameTextBox = new TextBox();
            _phoneLabel = new Label();
            _phoneTextBox = new TextBox();
            _emailLabel = new Label();
            _emailTextBox = new TextBox();
            _addressLabel = new Label();
            _addressTextBox = new TextBox();
            _itemsLabel = new Label();
            _itemsTextBox = new TextBox();
            _buttons = new FlowLayoutPanel();
            _saveButton = new Button();
            _cancelButton = new Button();
            _root.SuspendLayout();
            _buttons.SuspendLayout();
            SuspendLayout();

            _root.ColumnCount = 2;
            _root.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            _root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _root.Dock = DockStyle.Fill;
            _root.Padding = new Padding(24);
            _root.RowCount = 7;
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 88F));
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 88F));
            _root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            _root.Controls.Add(_titleLabel, 0, 0);
            _root.SetColumnSpan(_titleLabel, 2);
            _nameLabel.AutoSize = true;
            _nameLabel.Dock = DockStyle.Fill;
            _nameLabel.Text = "Tên nhà cung cấp";
            _nameLabel.TextAlign = ContentAlignment.MiddleLeft;
            _nameTextBox.Dock = DockStyle.Fill;
            _nameTextBox.Margin = new Padding(0, 6, 0, 6);
            _root.Controls.Add(_nameLabel, 0, 1);
            _root.Controls.Add(_nameTextBox, 1, 1);
            _phoneLabel.AutoSize = true;
            _phoneLabel.Dock = DockStyle.Fill;
            _phoneLabel.Text = "Số điện thoại";
            _phoneLabel.TextAlign = ContentAlignment.MiddleLeft;
            _phoneTextBox.Dock = DockStyle.Fill;
            _phoneTextBox.Margin = new Padding(0, 6, 0, 6);
            _root.Controls.Add(_phoneLabel, 0, 2);
            _root.Controls.Add(_phoneTextBox, 1, 2);
            _emailLabel.AutoSize = true;
            _emailLabel.Dock = DockStyle.Fill;
            _emailLabel.Text = "Email";
            _emailLabel.TextAlign = ContentAlignment.MiddleLeft;
            _emailTextBox.Dock = DockStyle.Fill;
            _emailTextBox.Margin = new Padding(0, 6, 0, 6);
            _root.Controls.Add(_emailLabel, 0, 3);
            _root.Controls.Add(_emailTextBox, 1, 3);
            _addressLabel.AutoSize = true;
            _addressLabel.Dock = DockStyle.Fill;
            _addressLabel.Text = "Địa chỉ";
            _addressLabel.TextAlign = ContentAlignment.MiddleLeft;
            _addressTextBox.Dock = DockStyle.Fill;
            _addressTextBox.Margin = new Padding(0, 6, 0, 6);
            _root.Controls.Add(_addressLabel, 0, 4);
            _root.Controls.Add(_addressTextBox, 1, 4);
            _itemsLabel.AutoSize = true;
            _itemsLabel.Dock = DockStyle.Fill;
            _itemsLabel.Text = "Mặt hàng cung cấp";
            _itemsLabel.TextAlign = ContentAlignment.MiddleLeft;
            _itemsTextBox.Dock = DockStyle.Fill;
            _itemsTextBox.Margin = new Padding(0, 6, 0, 6);
            _root.Controls.Add(_itemsLabel, 0, 5);
            _root.Controls.Add(_itemsTextBox, 1, 5);
            _root.Controls.Add(_buttons, 0, 6);
            _root.SetColumnSpan(_buttons, 2);

            _titleLabel.Dock = DockStyle.Fill;
            _titleLabel.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            _titleLabel.ForeColor = Color.FromArgb(15, 23, 42);
            _titleLabel.Text = "Nhà cung cấp";
            _titleLabel.TextAlign = ContentAlignment.MiddleLeft;
            _nameTextBox.MaxLength = 150;
            _nameTextBox.Name = "_nameTextBox";
            _phoneTextBox.MaxLength = 30;
            _phoneTextBox.Name = "_phoneTextBox";
            _emailTextBox.MaxLength = 150;
            _emailTextBox.Name = "_emailTextBox";
            _addressTextBox.Multiline = true;
            _addressTextBox.Name = "_addressTextBox";
            _itemsTextBox.Multiline = true;
            _itemsTextBox.Name = "_itemsTextBox";
            _buttons.Controls.Add(_cancelButton);
            _buttons.Controls.Add(_saveButton);
            _buttons.Dock = DockStyle.Fill;
            _buttons.FlowDirection = FlowDirection.RightToLeft;
            _buttons.Padding = new Padding(0, 14, 0, 0);
            _cancelButton.DialogResult = DialogResult.Cancel;
            _cancelButton.Size = new Size(92, 34);
            _cancelButton.Text = "Hủy";
            _saveButton.BackColor = Color.FromArgb(37, 99, 235);
            _saveButton.FlatAppearance.BorderSize = 0;
            _saveButton.FlatStyle = FlatStyle.Flat;
            _saveButton.ForeColor = Color.White;
            _saveButton.Size = new Size(112, 34);
            _saveButton.Text = "Lưu";
            AcceptButton = _saveButton;
            CancelButton = _cancelButton;

            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(640, 500);
            Controls.Add(_root);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmSupplierEditor";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Nhà cung cấp";
            _root.ResumeLayout(false);
            _root.PerformLayout();
            _buttons.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
