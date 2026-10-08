using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Views.Catalog
{
    partial class frmCategoryEditor
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel _root;
        private Label _titleLabel;
        private Label _nameLabel;
        private TextBox _nameTextBox;
        private Label _statusLabel;
        private ComboBox _statusComboBox;
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
            _statusLabel = new Label();
            _statusComboBox = new ComboBox();
            _buttons = new FlowLayoutPanel();
            _saveButton = new Button();
            _cancelButton = new Button();
            _root.SuspendLayout();
            _buttons.SuspendLayout();
            SuspendLayout();

            _root.ColumnCount = 2;
            _root.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            _root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _root.Dock = DockStyle.Fill;
            _root.Padding = new Padding(24);
            _root.RowCount = 4;
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            _root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            _root.Controls.Add(_titleLabel, 0, 0);
            _root.SetColumnSpan(_titleLabel, 2);
            _root.Controls.Add(_nameLabel, 0, 1);
            _root.Controls.Add(_nameTextBox, 1, 1);
            _root.Controls.Add(_statusLabel, 0, 2);
            _root.Controls.Add(_statusComboBox, 1, 2);
            _root.Controls.Add(_buttons, 0, 3);
            _root.SetColumnSpan(_buttons, 2);
            _root.SetRowSpan(_buttons, 1);

            _titleLabel.Dock = DockStyle.Fill;
            _titleLabel.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            _titleLabel.ForeColor = Color.FromArgb(15, 23, 42);
            _titleLabel.Text = "Danh mục";
            _titleLabel.TextAlign = ContentAlignment.MiddleLeft;
            _nameLabel.Dock = DockStyle.Fill;
            _nameLabel.Text = "Tên danh mục";
            _nameLabel.TextAlign = ContentAlignment.MiddleLeft;
            _nameTextBox.Dock = DockStyle.Fill;
            _nameTextBox.Margin = new Padding(0, 8, 0, 8);
            _nameTextBox.MaxLength = 150;
            _nameTextBox.Name = "_nameTextBox";
            _statusLabel.Dock = DockStyle.Fill;
            _statusLabel.Text = "Trạng thái";
            _statusLabel.TextAlign = ContentAlignment.MiddleLeft;
            _statusComboBox.Dock = DockStyle.Fill;
            _statusComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            _statusComboBox.Items.AddRange(new object[] { "Đang hoạt động", "Vô hiệu hóa" });
            _statusComboBox.Margin = new Padding(0, 8, 0, 8);
            _statusComboBox.Name = "_statusComboBox";
            _statusComboBox.SelectedIndex = 0;
            _buttons.Controls.Add(_cancelButton);
            _buttons.Controls.Add(_saveButton);
            _buttons.Dock = DockStyle.Fill;
            _buttons.FlowDirection = FlowDirection.RightToLeft;
            _buttons.Padding = new Padding(0, 16, 0, 0);
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
            ClientSize = new Size(580, 280);
            Controls.Add(_root);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmCategoryEditor";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Danh mục";
            _root.ResumeLayout(false);
            _root.PerformLayout();
            _buttons.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
