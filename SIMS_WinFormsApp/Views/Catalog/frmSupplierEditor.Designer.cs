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
            this._root = new System.Windows.Forms.TableLayoutPanel();
            this._titleLabel = new System.Windows.Forms.Label();
            this._nameLabel = new System.Windows.Forms.Label();
            this._nameTextBox = new System.Windows.Forms.TextBox();
            this._phoneLabel = new System.Windows.Forms.Label();
            this._phoneTextBox = new System.Windows.Forms.TextBox();
            this._emailLabel = new System.Windows.Forms.Label();
            this._emailTextBox = new System.Windows.Forms.TextBox();
            this._addressLabel = new System.Windows.Forms.Label();
            this._addressTextBox = new System.Windows.Forms.TextBox();
            this._itemsLabel = new System.Windows.Forms.Label();
            this._itemsTextBox = new System.Windows.Forms.TextBox();
            this._buttons = new System.Windows.Forms.FlowLayoutPanel();
            this._cancelButton = new System.Windows.Forms.Button();
            this._saveButton = new System.Windows.Forms.Button();
            this._root.SuspendLayout();
            this._buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // _root
            // 
            this._root.ColumnCount = 2;
            this._root.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this._root.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._root.Controls.Add(this._titleLabel, 0, 0);
            this._root.Controls.Add(this._nameLabel, 0, 1);
            this._root.Controls.Add(this._nameTextBox, 1, 1);
            this._root.Controls.Add(this._phoneLabel, 0, 2);
            this._root.Controls.Add(this._phoneTextBox, 1, 2);
            this._root.Controls.Add(this._emailLabel, 0, 3);
            this._root.Controls.Add(this._emailTextBox, 1, 3);
            this._root.Controls.Add(this._addressLabel, 0, 4);
            this._root.Controls.Add(this._addressTextBox, 1, 4);
            this._root.Controls.Add(this._itemsLabel, 0, 5);
            this._root.Controls.Add(this._itemsTextBox, 1, 5);
            this._root.Controls.Add(this._buttons, 0, 6);
            this._root.Dock = System.Windows.Forms.DockStyle.Fill;
            this._root.Location = new System.Drawing.Point(0, 0);
            this._root.Name = "_root";
            this._root.Padding = new System.Windows.Forms.Padding(24);
            this._root.RowCount = 7;
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._root.Size = new System.Drawing.Size(640, 500);
            this._root.TabIndex = 0;
            // 
            // _titleLabel
            // 
            this._root.SetColumnSpan(this._titleLabel, 2);
            this._titleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._titleLabel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this._titleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._titleLabel.Location = new System.Drawing.Point(27, 24);
            this._titleLabel.Name = "_titleLabel";
            this._titleLabel.Size = new System.Drawing.Size(586, 54);
            this._titleLabel.TabIndex = 0;
            this._titleLabel.Text = "Nhà cung cấp";
            this._titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _nameLabel
            // 
            this._nameLabel.AutoSize = true;
            this._nameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._nameLabel.Location = new System.Drawing.Point(27, 78);
            this._nameLabel.Name = "_nameLabel";
            this._nameLabel.Size = new System.Drawing.Size(144, 46);
            this._nameLabel.TabIndex = 1;
            this._nameLabel.Text = "Tên nhà cung cấp";
            this._nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _nameTextBox
            // 
            this._nameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._nameTextBox.Location = new System.Drawing.Point(174, 84);
            this._nameTextBox.Margin = new System.Windows.Forms.Padding(0, 6, 0, 6);
            this._nameTextBox.MaxLength = 150;
            this._nameTextBox.Name = "_nameTextBox";
            this._nameTextBox.Size = new System.Drawing.Size(442, 26);
            this._nameTextBox.TabIndex = 2;
            // 
            // _phoneLabel
            // 
            this._phoneLabel.AutoSize = true;
            this._phoneLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._phoneLabel.Location = new System.Drawing.Point(27, 124);
            this._phoneLabel.Name = "_phoneLabel";
            this._phoneLabel.Size = new System.Drawing.Size(144, 46);
            this._phoneLabel.TabIndex = 3;
            this._phoneLabel.Text = "Số điện thoại";
            this._phoneLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _phoneTextBox
            // 
            this._phoneTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._phoneTextBox.Location = new System.Drawing.Point(174, 130);
            this._phoneTextBox.Margin = new System.Windows.Forms.Padding(0, 6, 0, 6);
            this._phoneTextBox.MaxLength = 30;
            this._phoneTextBox.Name = "_phoneTextBox";
            this._phoneTextBox.Size = new System.Drawing.Size(442, 26);
            this._phoneTextBox.TabIndex = 4;
            // 
            // _emailLabel
            // 
            this._emailLabel.AutoSize = true;
            this._emailLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._emailLabel.Location = new System.Drawing.Point(27, 170);
            this._emailLabel.Name = "_emailLabel";
            this._emailLabel.Size = new System.Drawing.Size(144, 46);
            this._emailLabel.TabIndex = 5;
            this._emailLabel.Text = "Email";
            this._emailLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _emailTextBox
            // 
            this._emailTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._emailTextBox.Location = new System.Drawing.Point(174, 176);
            this._emailTextBox.Margin = new System.Windows.Forms.Padding(0, 6, 0, 6);
            this._emailTextBox.MaxLength = 150;
            this._emailTextBox.Name = "_emailTextBox";
            this._emailTextBox.Size = new System.Drawing.Size(442, 26);
            this._emailTextBox.TabIndex = 6;
            // 
            // _addressLabel
            // 
            this._addressLabel.AutoSize = true;
            this._addressLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._addressLabel.Location = new System.Drawing.Point(27, 216);
            this._addressLabel.Name = "_addressLabel";
            this._addressLabel.Size = new System.Drawing.Size(144, 88);
            this._addressLabel.TabIndex = 7;
            this._addressLabel.Text = "Địa chỉ";
            this._addressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _addressTextBox
            // 
            this._addressTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._addressTextBox.Location = new System.Drawing.Point(174, 222);
            this._addressTextBox.Margin = new System.Windows.Forms.Padding(0, 6, 0, 6);
            this._addressTextBox.Multiline = true;
            this._addressTextBox.Name = "_addressTextBox";
            this._addressTextBox.Size = new System.Drawing.Size(442, 76);
            this._addressTextBox.TabIndex = 8;
            // 
            // _itemsLabel
            // 
            this._itemsLabel.AutoSize = true;
            this._itemsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._itemsLabel.Location = new System.Drawing.Point(27, 304);
            this._itemsLabel.Name = "_itemsLabel";
            this._itemsLabel.Size = new System.Drawing.Size(144, 88);
            this._itemsLabel.TabIndex = 9;
            this._itemsLabel.Text = "Mặt hàng cung cấp";
            this._itemsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _itemsTextBox
            // 
            this._itemsTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._itemsTextBox.Location = new System.Drawing.Point(174, 310);
            this._itemsTextBox.Margin = new System.Windows.Forms.Padding(0, 6, 0, 6);
            this._itemsTextBox.Multiline = true;
            this._itemsTextBox.Name = "_itemsTextBox";
            this._itemsTextBox.Size = new System.Drawing.Size(442, 76);
            this._itemsTextBox.TabIndex = 10;
            // 
            // _buttons
            // 
            this._root.SetColumnSpan(this._buttons, 2);
            this._buttons.Controls.Add(this._cancelButton);
            this._buttons.Controls.Add(this._saveButton);
            this._buttons.Dock = System.Windows.Forms.DockStyle.Fill;
            this._buttons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._buttons.Location = new System.Drawing.Point(27, 395);
            this._buttons.Name = "_buttons";
            this._buttons.Padding = new System.Windows.Forms.Padding(0, 14, 0, 0);
            this._buttons.Size = new System.Drawing.Size(586, 78);
            this._buttons.TabIndex = 11;
            // 
            // _cancelButton
            // 
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancelButton.Location = new System.Drawing.Point(491, 17);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(92, 34);
            this._cancelButton.TabIndex = 0;
            this._cancelButton.Text = "Hủy";
            // 
            // _saveButton
            // 
            this._saveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._saveButton.FlatAppearance.BorderSize = 0;
            this._saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._saveButton.ForeColor = System.Drawing.Color.White;
            this._saveButton.Location = new System.Drawing.Point(373, 17);
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(112, 34);
            this._saveButton.TabIndex = 1;
            this._saveButton.Text = "Lưu";
            this._saveButton.UseVisualStyleBackColor = false;
            // 
            // frmSupplierEditor
            // 
            this.AcceptButton = this._saveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cancelButton;
            this.ClientSize = new System.Drawing.Size(640, 500);
            this.Controls.Add(this._root);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSupplierEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nhà cung cấp";
            this._root.ResumeLayout(false);
            this._root.PerformLayout();
            this._buttons.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
