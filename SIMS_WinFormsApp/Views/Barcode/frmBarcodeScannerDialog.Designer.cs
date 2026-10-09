using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.UI.Controls.Barcode
{
    partial class frmBarcodeScannerDialog
    {
        private void InitializeComponent()
        {
            this._instructionLabel = new System.Windows.Forms.Label();
            this._videoHolder = new System.Windows.Forms.Panel();
            this._errorLabel = new System.Windows.Forms.Label();
            this._videoBox = new System.Windows.Forms.PictureBox();
            this._statusLabel = new System.Windows.Forms.Label();
            this._videoHolder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._videoBox)).BeginInit();
            this.SuspendLayout();
            // 
            // _instructionLabel
            // 
            this._instructionLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._instructionLabel.Location = new System.Drawing.Point(28, 20);
            this._instructionLabel.Name = "_instructionLabel";
            this._instructionLabel.Size = new System.Drawing.Size(500, 38);
            this._instructionLabel.TabIndex = 3;
            this._instructionLabel.Text = "Đưa mã vạch vào giữa khung hình";
            // 
            // _videoHolder
            // 
            this._videoHolder.Controls.Add(this._errorLabel);
            this._videoHolder.Controls.Add(this._videoBox);
            this._videoHolder.Dock = System.Windows.Forms.DockStyle.Top;
            this._videoHolder.Location = new System.Drawing.Point(28, 58);
            this._videoHolder.Name = "_videoHolder";
            this._videoHolder.Size = new System.Drawing.Size(500, 330);
            this._videoHolder.TabIndex = 2;
            // 
            // _errorLabel
            // 
            this._errorLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._errorLabel.Location = new System.Drawing.Point(0, 0);
            this._errorLabel.Name = "_errorLabel";
            this._errorLabel.Size = new System.Drawing.Size(500, 330);
            this._errorLabel.TabIndex = 0;
            this._errorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._errorLabel.Visible = false;
            // 
            // _videoBox
            // 
            this._videoBox.BackColor = System.Drawing.Color.Black;
            this._videoBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._videoBox.Location = new System.Drawing.Point(0, 0);
            this._videoBox.Name = "_videoBox";
            this._videoBox.Size = new System.Drawing.Size(500, 330);
            this._videoBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this._videoBox.TabIndex = 1;
            this._videoBox.TabStop = false;
            // 
            // _statusLabel
            // 
            this._statusLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._statusLabel.Location = new System.Drawing.Point(28, 388);
            this._statusLabel.Name = "_statusLabel";
            this._statusLabel.Size = new System.Drawing.Size(500, 22);
            this._statusLabel.TabIndex = 1;
            // 
            // frmBarcodeScannerDialog
            // 
            this.ClientSize = new System.Drawing.Size(560, 560);
            this.MinimumSize = new System.Drawing.Size(560, 560);
            this.Name = "frmBarcodeScannerDialog";
            this.Text = "Quét mã vạch";
            this._videoHolder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._videoBox)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
