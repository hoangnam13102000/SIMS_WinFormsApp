using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.UI.Controls.Barcode
{
    partial class frmBarcodeScannerDialog
    {
        private void InitializeComponent()
        {
            _instructionLabel = new Label();
            _videoHolder = new Panel();
            _videoBox = new PictureBox();
            _errorLabel = new Label();
            _statusLabel = new Label();
            SuspendLayout();
            ContentHost.SuspendLayout();
            _videoHolder.SuspendLayout();

            _instructionLabel.AutoSize = false;
            _instructionLabel.Dock = DockStyle.Top;
            _instructionLabel.Height = 30;
            _instructionLabel.Name = "instructionLabel";
            _instructionLabel.Text = "Đưa mã vạch vào giữa khung hình";

            _videoHolder.Dock = DockStyle.Top;
            _videoHolder.Height = 330;
            _videoHolder.Name = "videoHolder";
            _videoBox.BackColor = Color.Black;
            _videoBox.Dock = DockStyle.Fill;
            _videoBox.Name = "videoPreview";
            _videoBox.SizeMode = PictureBoxSizeMode.Zoom;
            _errorLabel.Dock = DockStyle.Fill;
            _errorLabel.Name = "cameraError";
            _errorLabel.TextAlign = ContentAlignment.MiddleCenter;
            _errorLabel.Visible = false;
            _videoHolder.Controls.Add(_errorLabel);
            _videoHolder.Controls.Add(_videoBox);

            _statusLabel.AutoSize = false;
            _statusLabel.Dock = DockStyle.Top;
            _statusLabel.Height = 22;
            _statusLabel.Name = "cameraStatus";

            ContentHost.Controls.Add(_statusLabel);
            ContentHost.Controls.Add(_videoHolder);
            ContentHost.Controls.Add(_instructionLabel);
            ContentHost.ResumeLayout(false);
            _videoHolder.ResumeLayout(false);
            ClientSize = new Size(560, 560);
            MinimumSize = new Size(560, 560);
            Name = "frmBarcodeScannerDialog";
            Text = "Quét mã vạch";
            ResumeLayout(false);
        }
    }
}
