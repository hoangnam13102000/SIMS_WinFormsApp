using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Views.Sales
{
    partial class PromotionEditorDialog
    {
        private IContainer components = null;
        private TableLayoutPanel _layout;
        private Panel _titlePanel;
        private Label _title;
        private Label _subtitle;
        private Label _codeLabel;
        private TextBox _codeInput;
        private Label _nameLabel;
        private TextBox _nameInput;
        private Label _typeLabel;
        private ComboBox _typeInput;
        private Label _valueLabel;
        private TextBox _valueInput;
        private Label _maximumLabel;
        private TextBox _maximumInput;
        private Label _minimumLabel;
        private TextBox _minimumInput;
        private Label _startLabel;
        private DateTimePicker _startInput;
        private Label _endLabel;
        private DateTimePicker _endInput;
        private Label _limitLabel;
        private TextBox _limitInput;
        private CheckBox _activeInput;
        private CheckBox _bannerInput;
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
            this._layout = new System.Windows.Forms.TableLayoutPanel();
            this._titlePanel = new System.Windows.Forms.Panel();
            this._title = new System.Windows.Forms.Label();
            this._subtitle = new System.Windows.Forms.Label();
            this._codeLabel = new System.Windows.Forms.Label();
            this._nameLabel = new System.Windows.Forms.Label();
            this._codeInput = new System.Windows.Forms.TextBox();
            this._nameInput = new System.Windows.Forms.TextBox();
            this._typeLabel = new System.Windows.Forms.Label();
            this._valueLabel = new System.Windows.Forms.Label();
            this._typeInput = new System.Windows.Forms.ComboBox();
            this._valueInput = new System.Windows.Forms.TextBox();
            this._maximumLabel = new System.Windows.Forms.Label();
            this._minimumLabel = new System.Windows.Forms.Label();
            this._maximumInput = new System.Windows.Forms.TextBox();
            this._minimumInput = new System.Windows.Forms.TextBox();
            this._startLabel = new System.Windows.Forms.Label();
            this._endLabel = new System.Windows.Forms.Label();
            this._startInput = new System.Windows.Forms.DateTimePicker();
            this._endInput = new System.Windows.Forms.DateTimePicker();
            this._limitLabel = new System.Windows.Forms.Label();
            this._limitInput = new System.Windows.Forms.TextBox();
            this._activeInput = new System.Windows.Forms.CheckBox();
            this._bannerInput = new System.Windows.Forms.CheckBox();
            this._buttons = new System.Windows.Forms.FlowLayoutPanel();
            this._saveButton = new System.Windows.Forms.Button();
            this._cancelButton = new System.Windows.Forms.Button();
            this._layout.SuspendLayout();
            this._titlePanel.SuspendLayout();
            this._buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // _layout
            // 
            this._layout.BackColor = System.Drawing.Color.White;
            this._layout.ColumnCount = 4;
            this._layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this._layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this._layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this._layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this._layout.Controls.Add(this._titlePanel, 0, 0);
            this._layout.Controls.Add(this._codeLabel, 0, 1);
            this._layout.Controls.Add(this._nameLabel, 2, 1);
            this._layout.Controls.Add(this._codeInput, 0, 2);
            this._layout.Controls.Add(this._nameInput, 2, 2);
            this._layout.Controls.Add(this._typeLabel, 0, 3);
            this._layout.Controls.Add(this._valueLabel, 2, 3);
            this._layout.Controls.Add(this._typeInput, 0, 4);
            this._layout.Controls.Add(this._valueInput, 2, 4);
            this._layout.Controls.Add(this._maximumLabel, 0, 5);
            this._layout.Controls.Add(this._minimumLabel, 2, 5);
            this._layout.Controls.Add(this._maximumInput, 0, 6);
            this._layout.Controls.Add(this._minimumInput, 2, 6);
            this._layout.Controls.Add(this._startLabel, 0, 7);
            this._layout.Controls.Add(this._endLabel, 2, 7);
            this._layout.Controls.Add(this._startInput, 0, 8);
            this._layout.Controls.Add(this._endInput, 2, 8);
            this._layout.Controls.Add(this._limitLabel, 0, 9);
            this._layout.Controls.Add(this._limitInput, 0, 10);
            this._layout.Controls.Add(this._activeInput, 2, 9);
            this._layout.Controls.Add(this._bannerInput, 2, 10);
            this._layout.Controls.Add(this._buttons, 0, 11);
            this._layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._layout.Location = new System.Drawing.Point(0, 0);
            this._layout.Name = "_layout";
            this._layout.Padding = new System.Windows.Forms.Padding(20);
            this._layout.RowCount = 12;
            this._layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this._layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this._layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this._layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this._layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this._layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this._layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this._layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this._layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this._layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this._layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this._layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this._layout.Size = new System.Drawing.Size(1050, 700);
            this._layout.TabIndex = 0;
            // 
            // _titlePanel
            // 
            this._layout.SetColumnSpan(this._titlePanel, 4);
            this._titlePanel.Controls.Add(this._title);
            this._titlePanel.Controls.Add(this._subtitle);
            this._titlePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._titlePanel.Location = new System.Drawing.Point(20, 20);
            this._titlePanel.Margin = new System.Windows.Forms.Padding(0);
            this._titlePanel.Name = "_titlePanel";
            this._titlePanel.Size = new System.Drawing.Size(1010, 70);
            this._titlePanel.TabIndex = 0;
            // 
            // _title
            // 
            this._title.AutoSize = true;
            this._title.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this._title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._title.Location = new System.Drawing.Point(0, 0);
            this._title.Name = "_title";
            this._title.Size = new System.Drawing.Size(337, 45);
            this._title.TabIndex = 0;
            this._title.Text = "Tạo mã giảm giá mới";
            // 
            // _subtitle
            // 
            this._subtitle.AutoSize = true;
            this._subtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._subtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._subtitle.Location = new System.Drawing.Point(2, 38);
            this._subtitle.Name = "_subtitle";
            this._subtitle.Size = new System.Drawing.Size(336, 25);
            this._subtitle.TabIndex = 1;
            this._subtitle.Text = "Nhập thông tin chương trình khuyến mãi";
            // 
            // _codeLabel
            // 
            this._codeLabel.AutoSize = true;
            this._codeLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._codeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._codeLabel.Location = new System.Drawing.Point(24, 92);
            this._codeLabel.Margin = new System.Windows.Forms.Padding(4, 2, 8, 2);
            this._codeLabel.Name = "_codeLabel";
            this._codeLabel.Size = new System.Drawing.Size(105, 18);
            this._codeLabel.TabIndex = 1;
            this._codeLabel.Text = "Mã giảm giá";
            // 
            // _nameLabel
            // 
            this._nameLabel.AutoSize = true;
            this._nameLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._nameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._nameLabel.Location = new System.Drawing.Point(528, 92);
            this._nameLabel.Margin = new System.Windows.Forms.Padding(4, 2, 8, 2);
            this._nameLabel.Name = "_nameLabel";
            this._nameLabel.Size = new System.Drawing.Size(140, 18);
            this._nameLabel.TabIndex = 2;
            this._nameLabel.Text = "Tên chương trình";
            // 
            // _codeInput
            // 
            this._layout.SetColumnSpan(this._codeInput, 2);
            this._codeInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this._codeInput.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._codeInput.Location = new System.Drawing.Point(24, 112);
            this._codeInput.Margin = new System.Windows.Forms.Padding(4, 0, 8, 2);
            this._codeInput.Name = "_codeInput";
            this._codeInput.Size = new System.Drawing.Size(492, 31);
            this._codeInput.TabIndex = 3;
            // 
            // _nameInput
            // 
            this._layout.SetColumnSpan(this._nameInput, 2);
            this._nameInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this._nameInput.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._nameInput.Location = new System.Drawing.Point(528, 112);
            this._nameInput.Margin = new System.Windows.Forms.Padding(4, 0, 8, 2);
            this._nameInput.Name = "_nameInput";
            this._nameInput.Size = new System.Drawing.Size(494, 31);
            this._nameInput.TabIndex = 4;
            // 
            // _typeLabel
            // 
            this._typeLabel.AutoSize = true;
            this._typeLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._typeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._typeLabel.Location = new System.Drawing.Point(24, 208);
            this._typeLabel.Margin = new System.Windows.Forms.Padding(4, 2, 8, 2);
            this._typeLabel.Name = "_typeLabel";
            this._typeLabel.Size = new System.Drawing.Size(84, 18);
            this._typeLabel.TabIndex = 5;
            this._typeLabel.Text = "Loại giảm";
            // 
            // _valueLabel
            // 
            this._valueLabel.AutoSize = true;
            this._valueLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._valueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._valueLabel.Location = new System.Drawing.Point(528, 208);
            this._valueLabel.Margin = new System.Windows.Forms.Padding(4, 2, 8, 2);
            this._valueLabel.Name = "_valueLabel";
            this._valueLabel.Size = new System.Drawing.Size(99, 18);
            this._valueLabel.TabIndex = 6;
            this._valueLabel.Text = "Giá trị giảm";
            // 
            // _typeInput
            // 
            this._layout.SetColumnSpan(this._typeInput, 2);
            this._typeInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this._typeInput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._typeInput.Items.AddRange(new object[] {
            "PERCENT",
            "AMOUNT"});
            this._typeInput.Location = new System.Drawing.Point(24, 228);
            this._typeInput.Margin = new System.Windows.Forms.Padding(4, 0, 8, 2);
            this._typeInput.Name = "_typeInput";
            this._typeInput.Size = new System.Drawing.Size(492, 33);
            this._typeInput.TabIndex = 7;
            // 
            // _valueInput
            // 
            this._layout.SetColumnSpan(this._valueInput, 2);
            this._valueInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this._valueInput.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._valueInput.Location = new System.Drawing.Point(528, 228);
            this._valueInput.Margin = new System.Windows.Forms.Padding(4, 0, 8, 2);
            this._valueInput.Name = "_valueInput";
            this._valueInput.Size = new System.Drawing.Size(494, 31);
            this._valueInput.TabIndex = 8;
            // 
            // _maximumLabel
            // 
            this._maximumLabel.AutoSize = true;
            this._maximumLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._maximumLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._maximumLabel.Location = new System.Drawing.Point(24, 324);
            this._maximumLabel.Margin = new System.Windows.Forms.Padding(4, 2, 8, 2);
            this._maximumLabel.Name = "_maximumLabel";
            this._maximumLabel.Size = new System.Drawing.Size(124, 18);
            this._maximumLabel.TabIndex = 9;
            this._maximumLabel.Text = "Giảm tối đa (đ)";
            // 
            // _minimumLabel
            // 
            this._minimumLabel.AutoSize = true;
            this._minimumLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._minimumLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._minimumLabel.Location = new System.Drawing.Point(528, 324);
            this._minimumLabel.Margin = new System.Windows.Forms.Padding(4, 2, 8, 2);
            this._minimumLabel.Name = "_minimumLabel";
            this._minimumLabel.Size = new System.Drawing.Size(136, 18);
            this._minimumLabel.TabIndex = 10;
            this._minimumLabel.Text = "Đơn tối thiểu (đ)";
            // 
            // _maximumInput
            // 
            this._layout.SetColumnSpan(this._maximumInput, 2);
            this._maximumInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this._maximumInput.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._maximumInput.Location = new System.Drawing.Point(24, 344);
            this._maximumInput.Margin = new System.Windows.Forms.Padding(4, 0, 8, 2);
            this._maximumInput.Name = "_maximumInput";
            this._maximumInput.Size = new System.Drawing.Size(492, 31);
            this._maximumInput.TabIndex = 11;
            // 
            // _minimumInput
            // 
            this._layout.SetColumnSpan(this._minimumInput, 2);
            this._minimumInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this._minimumInput.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._minimumInput.Location = new System.Drawing.Point(528, 344);
            this._minimumInput.Margin = new System.Windows.Forms.Padding(4, 0, 8, 2);
            this._minimumInput.Name = "_minimumInput";
            this._minimumInput.Size = new System.Drawing.Size(494, 31);
            this._minimumInput.TabIndex = 12;
            // 
            // _startLabel
            // 
            this._startLabel.AutoSize = true;
            this._startLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._startLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._startLabel.Location = new System.Drawing.Point(24, 440);
            this._startLabel.Margin = new System.Windows.Forms.Padding(4, 2, 8, 2);
            this._startLabel.Name = "_startLabel";
            this._startLabel.Size = new System.Drawing.Size(114, 18);
            this._startLabel.TabIndex = 13;
            this._startLabel.Text = "Ngày bắt đầu";
            // 
            // _endLabel
            // 
            this._endLabel.AutoSize = true;
            this._endLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._endLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._endLabel.Location = new System.Drawing.Point(528, 440);
            this._endLabel.Margin = new System.Windows.Forms.Padding(4, 2, 8, 2);
            this._endLabel.Name = "_endLabel";
            this._endLabel.Size = new System.Drawing.Size(117, 18);
            this._endLabel.TabIndex = 14;
            this._endLabel.Text = "Ngày kết thúc";
            // 
            // _startInput
            // 
            this._layout.SetColumnSpan(this._startInput, 2);
            this._startInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this._startInput.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._startInput.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this._startInput.Location = new System.Drawing.Point(24, 460);
            this._startInput.Margin = new System.Windows.Forms.Padding(4, 0, 8, 2);
            this._startInput.Name = "_startInput";
            this._startInput.Size = new System.Drawing.Size(492, 31);
            this._startInput.TabIndex = 15;
            this._startInput.Value = new System.DateTime(2026, 10, 28, 0, 0, 0, 0);
            // 
            // _endInput
            // 
            this._layout.SetColumnSpan(this._endInput, 2);
            this._endInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this._endInput.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._endInput.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this._endInput.Location = new System.Drawing.Point(528, 460);
            this._endInput.Margin = new System.Windows.Forms.Padding(4, 0, 8, 2);
            this._endInput.Name = "_endInput";
            this._endInput.Size = new System.Drawing.Size(494, 31);
            this._endInput.TabIndex = 16;
            this._endInput.Value = new System.DateTime(2026, 11, 28, 0, 0, 0, 0);
            // 
            // _limitLabel
            // 
            this._limitLabel.AutoSize = true;
            this._limitLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._limitLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._limitLabel.Location = new System.Drawing.Point(24, 556);
            this._limitLabel.Margin = new System.Windows.Forms.Padding(4, 2, 8, 2);
            this._limitLabel.Name = "_limitLabel";
            this._limitLabel.Size = new System.Drawing.Size(109, 23);
            this._limitLabel.TabIndex = 17;
            this._limitLabel.Text = "Giới hạn lượt";
            // 
            // _limitInput
            // 
            this._layout.SetColumnSpan(this._limitInput, 2);
            this._limitInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this._limitInput.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._limitInput.Location = new System.Drawing.Point(24, 590);
            this._limitInput.Margin = new System.Windows.Forms.Padding(4, 0, 8, 2);
            this._limitInput.Name = "_limitInput";
            this._limitInput.Size = new System.Drawing.Size(492, 31);
            this._limitInput.TabIndex = 18;
            // 
            // _activeInput
            // 
            this._activeInput.AutoSize = true;
            this._activeInput.Checked = true;
            this._activeInput.CheckState = System.Windows.Forms.CheckState.Checked;
            this._activeInput.Location = new System.Drawing.Point(532, 562);
            this._activeInput.Margin = new System.Windows.Forms.Padding(8, 8, 0, 0);
            this._activeInput.Name = "_activeInput";
            this._activeInput.Size = new System.Drawing.Size(153, 28);
            this._activeInput.TabIndex = 19;
            this._activeInput.Text = "Đang áp dụng";
            // 
            // _bannerInput
            // 
            this._bannerInput.AutoSize = true;
            this._bannerInput.Location = new System.Drawing.Point(532, 598);
            this._bannerInput.Margin = new System.Windows.Forms.Padding(8, 8, 0, 0);
            this._bannerInput.Name = "_bannerInput";
            this._bannerInput.Size = new System.Drawing.Size(170, 28);
            this._bannerInput.TabIndex = 20;
            this._bannerInput.Text = "Hiện trên banner";
            // 
            // _buttons
            // 
            this._layout.SetColumnSpan(this._buttons, 4);
            this._buttons.Controls.Add(this._saveButton);
            this._buttons.Controls.Add(this._cancelButton);
            this._buttons.Dock = System.Windows.Forms.DockStyle.Fill;
            this._buttons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._buttons.Location = new System.Drawing.Point(20, 626);
            this._buttons.Margin = new System.Windows.Forms.Padding(0);
            this._buttons.Name = "_buttons";
            this._buttons.Size = new System.Drawing.Size(1010, 54);
            this._buttons.TabIndex = 21;
            this._buttons.WrapContents = false;
            // 
            // _saveButton
            // 
            this._saveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._saveButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._saveButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this._saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._saveButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._saveButton.ForeColor = System.Drawing.Color.White;
            this._saveButton.Location = new System.Drawing.Point(861, 3);
            this._saveButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(145, 36);
            this._saveButton.TabIndex = 0;
            this._saveButton.Text = "Tạo mã";
            this._saveButton.UseVisualStyleBackColor = false;
            // 
            // _cancelButton
            // 
            this._cancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancelButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this._cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._cancelButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._cancelButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this._cancelButton.Location = new System.Drawing.Point(753, 3);
            this._cancelButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(100, 36);
            this._cancelButton.TabIndex = 1;
            this._cancelButton.Text = "Hủy";
            this._cancelButton.UseVisualStyleBackColor = false;
            // 
            // PromotionEditorDialog
            // 
            this.AcceptButton = this._saveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.CancelButton = this._cancelButton;
            this.ClientSize = new System.Drawing.Size(1050, 700);
            this.Controls.Add(this._layout);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PromotionEditorDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tạo mã giảm giá";
            this._layout.ResumeLayout(false);
            this._layout.PerformLayout();
            this._titlePanel.ResumeLayout(false);
            this._titlePanel.PerformLayout();
            this._buttons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

    }
}
