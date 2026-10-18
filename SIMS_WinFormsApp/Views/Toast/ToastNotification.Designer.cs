using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Views.Toast
{
    partial class ToastNotification
    {
        private void InitializeComponent()
        {
            this._closeBox = new FontAwesome.Sharp.IconPictureBox();
            this._iconBox = new FontAwesome.Sharp.IconPictureBox();
            this._titleLabel = new System.Windows.Forms.Label();
            this._messageLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._closeBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._iconBox)).BeginInit();
            this.SuspendLayout();
            // 
            // _closeBox
            // 
            this._closeBox.BackColor = System.Drawing.Color.Transparent;
            this._closeBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this._closeBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._closeBox.IconChar = FontAwesome.Sharp.IconChar.Remove;
            this._closeBox.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._closeBox.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._closeBox.IconSize = 14;
            this._closeBox.Location = new System.Drawing.Point(350, 19);
            this._closeBox.Name = "_closeBox";
            this._closeBox.Size = new System.Drawing.Size(14, 14);
            this._closeBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._closeBox.TabIndex = 0;
            this._closeBox.TabStop = false;
            // 
            // _iconBox
            // 
            this._iconBox.BackColor = System.Drawing.Color.Transparent;
            this._iconBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this._iconBox.IconChar = FontAwesome.Sharp.IconChar.CircleInfo;
            this._iconBox.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this._iconBox.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._iconBox.IconSize = 28;
            this._iconBox.Location = new System.Drawing.Point(16, 24);
            this._iconBox.Name = "_iconBox";
            this._iconBox.Size = new System.Drawing.Size(28, 28);
            this._iconBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._iconBox.TabIndex = 3;
            this._iconBox.TabStop = false;
            // 
            // _titleLabel
            // 
            this._titleLabel.AutoSize = true;
            this._titleLabel.BackColor = System.Drawing.Color.Transparent;
            this._titleLabel.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this._titleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._titleLabel.Location = new System.Drawing.Point(56, 16);
            this._titleLabel.MaximumSize = new System.Drawing.Size(260, 0);
            this._titleLabel.Name = "_titleLabel";
            this._titleLabel.Size = new System.Drawing.Size(134, 36);
            this._titleLabel.TabIndex = 2;
            this._titleLabel.Text = "Thông tin";
            this._titleLabel.UseMnemonic = false;
            // 
            // _messageLabel
            // 
            this._messageLabel.AutoSize = true;
            this._messageLabel.BackColor = System.Drawing.Color.Transparent;
            this._messageLabel.Font = new System.Drawing.Font("Segoe UI", 12F);
            this._messageLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._messageLabel.Location = new System.Drawing.Point(56, 60);
            this._messageLabel.MaximumSize = new System.Drawing.Size(260, 0);
            this._messageLabel.Name = "_messageLabel";
            this._messageLabel.Size = new System.Drawing.Size(243, 64);
            this._messageLabel.TabIndex = 1;
            this._messageLabel.Text = "Đây là nội dung xem trước của thông báo.";
            this._messageLabel.UseMnemonic = false;
            // 
            // ToastNotification
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(242)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(380, 133);
            this.Controls.Add(this._closeBox);
            this.Controls.Add(this._messageLabel);
            this.Controls.Add(this._titleLabel);
            this.Controls.Add(this._iconBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ToastNotification";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Thông báo";
            ((System.ComponentModel.ISupportInitialize)(this._closeBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._iconBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
