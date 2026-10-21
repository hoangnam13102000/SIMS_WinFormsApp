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
            this._root = new System.Windows.Forms.TableLayoutPanel();
            this._titleLabel = new System.Windows.Forms.Label();
            this._nameLabel = new System.Windows.Forms.Label();
            this._nameTextBox = new System.Windows.Forms.TextBox();
            this._statusLabel = new System.Windows.Forms.Label();
            this._statusComboBox = new System.Windows.Forms.ComboBox();
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
            this._root.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this._root.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._root.Controls.Add(this._titleLabel, 0, 0);
            this._root.Controls.Add(this._nameLabel, 0, 1);
            this._root.Controls.Add(this._nameTextBox, 1, 1);
            this._root.Controls.Add(this._statusLabel, 0, 2);
            this._root.Controls.Add(this._statusComboBox, 1, 2);
            this._root.Controls.Add(this._buttons, 0, 3);
            this._root.Dock = System.Windows.Forms.DockStyle.Fill;
            this._root.Location = new System.Drawing.Point(0, 0);
            this._root.Name = "_root";
            this._root.Padding = new System.Windows.Forms.Padding(24);
            this._root.RowCount = 4;
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._root.Size = new System.Drawing.Size(580, 280);
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
            this._titleLabel.Size = new System.Drawing.Size(526, 54);
            this._titleLabel.TabIndex = 0;
            this._titleLabel.Text = "Danh mục";
            this._titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _nameLabel
            // 
            this._nameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._nameLabel.Location = new System.Drawing.Point(27, 78);
            this._nameLabel.Name = "_nameLabel";
            this._nameLabel.Size = new System.Drawing.Size(124, 48);
            this._nameLabel.TabIndex = 1;
            this._nameLabel.Text = "Tên danh mục";
            this._nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _nameTextBox
            // 
            this._nameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._nameTextBox.Location = new System.Drawing.Point(154, 86);
            this._nameTextBox.Margin = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this._nameTextBox.MaxLength = 150;
            this._nameTextBox.Name = "_nameTextBox";
            this._nameTextBox.Size = new System.Drawing.Size(402, 26);
            this._nameTextBox.TabIndex = 2;
            // 
            // _statusLabel
            // 
            this._statusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._statusLabel.Location = new System.Drawing.Point(27, 126);
            this._statusLabel.Name = "_statusLabel";
            this._statusLabel.Size = new System.Drawing.Size(124, 48);
            this._statusLabel.TabIndex = 3;
            this._statusLabel.Text = "Trạng thái";
            this._statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _statusComboBox
            // 
            this._statusComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._statusComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._statusComboBox.Items.AddRange(new object[] {
            "Đang hoạt động",
            "Vô hiệu hóa"});
            this._statusComboBox.Location = new System.Drawing.Point(154, 134);
            this._statusComboBox.Margin = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this._statusComboBox.Name = "_statusComboBox";
            this._statusComboBox.Size = new System.Drawing.Size(402, 28);
            this._statusComboBox.TabIndex = 4;
            // 
            // _buttons
            // 
            this._root.SetColumnSpan(this._buttons, 2);
            this._buttons.Controls.Add(this._cancelButton);
            this._buttons.Controls.Add(this._saveButton);
            this._buttons.Dock = System.Windows.Forms.DockStyle.Fill;
            this._buttons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._buttons.Location = new System.Drawing.Point(27, 177);
            this._buttons.Name = "_buttons";
            this._buttons.Padding = new System.Windows.Forms.Padding(0, 16, 0, 0);
            this._buttons.Size = new System.Drawing.Size(526, 76);
            this._buttons.TabIndex = 5;
            // 
            // _cancelButton
            // 
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancelButton.Location = new System.Drawing.Point(431, 19);
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
            this._saveButton.Location = new System.Drawing.Point(313, 19);
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(112, 34);
            this._saveButton.TabIndex = 1;
            this._saveButton.Text = "Lưu";
            this._saveButton.UseVisualStyleBackColor = false;
            // 
            // frmCategoryEditor
            // 
            this.AcceptButton = this._saveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cancelButton;
            this.ClientSize = new System.Drawing.Size(580, 280);
            this.Controls.Add(this._root);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCategoryEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Danh mục";
            this._root.ResumeLayout(false);
            this._root.PerformLayout();
            this._buttons.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
