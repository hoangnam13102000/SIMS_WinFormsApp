using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Services.Barcode;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls.Barcode
{
    /// <summary>
    /// Popup "Quét mã vạch sản phẩm" - dựng trên cùng khung <see cref="BaseFormDialogForm"/>
    /// dùng chung với các popup khác trong project (header bo góc + icon + nút X, thân cuộn,
    /// footer 1 nút "Hủy") để đồng bộ giao diện toàn ứng dụng, thay vì tự vẽ dialog riêng như
    /// BarcodeScannerDialog.java bên bản gốc. Chỉ đóng vai trò View trong MVP
    /// (implement <see cref="IBarcodeScannerView"/>) - toàn bộ việc mở camera + giải mã do
    /// <see cref="BarcodeScannerPresenter"/> đảm nhiệm.
    /// <para>
    /// Overload tiêu đề/hướng dẫn tùy biến được giữ lại (như bản Java) để có thể tái sử dụng
    /// dialog này cho mục đích khác ngoài quét sản phẩm sau này (ví dụ quét thẻ/mã khách hàng)
    /// mà không phải sửa lại UI đang hardcode "sản phẩm".
    /// </para>
    /// </summary>
    public sealed partial class frmBarcodeScannerDialog : BaseFormDialogForm, IBarcodeScannerView
    {
        private static readonly Size DialogSize = new Size(560, 560);

        private BarcodeScannerPresenter _presenter;
        private Label _instructionLabel;
        private Panel _videoHolder;
        private PictureBox _videoBox;
        private Label _errorLabel;
        private Label _statusLabel;

        private string _scannedCode;

        public frmBarcodeScannerDialog()
        {
            InitializeComponent();
            HeaderTitle = "Quét mã vạch sản phẩm";
            SetHeaderIcon(IconChar.Barcode, AppColors.Accent);
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
        }

        public frmBarcodeScannerDialog(
            IWin32Window owner,
            string dialogTitle,
            string instructionText,
            IWebcamCaptureSession captureSession,
            BarcodeScanEngine engine) : base(owner)
        {
            InitializeComponent();
            Size = DialogSize;
            MinimumSize = DialogSize;

            HeaderTitle = dialogTitle;
            SetHeaderIcon(IconChar.Barcode, AppColors.Accent);
            _instructionLabel.Text = instructionText;
            AddFooterButton("Hủy", false, CancelButton_Click);
            CloseRequested += CloseRequestedHandler;

            _presenter = new BarcodeScannerPresenter(this, this, captureSession, engine);
            _presenter.Scanned += (s, code) =>
            {
                _scannedCode = code;
                DialogResult = DialogResult.OK;
                Close();
            };

            Shown += Presenter_Start;
        }

        private void CancelButton_Click(object sender, EventArgs e) => RaiseCloseRequested();

        private void CloseRequestedHandler(object sender, EventArgs e) => Close();

        private void Presenter_Start(object sender, EventArgs e) => _presenter.Start();

        /// <summary>
        /// Cách gọi nhanh, gọn cho nơi khác trong ứng dụng:
        /// <c>string code = frmBarcodeScannerDialog.Scan(this);</c> - trả về mã đã quét được,
        /// hoặc null nếu người dùng bấm "Hủy" / đóng dialog trước khi quét được.
        /// </summary>
        public static string Scan(
            IWin32Window owner,
            string dialogTitle = "Quét mã vạch sản phẩm",
            string instructionText = "Đưa mã vạch sản phẩm vào giữa khung hình")
        {
            var captureSession = new AForgeWebcamCaptureSession();
            var engine = new BarcodeScanEngine();
            using (var dialog = new frmBarcodeScannerDialog(owner, dialogTitle, instructionText, captureSession, engine))
            {
                dialog.ShowDialog(owner);
                return dialog._scannedCode;
            }
        }

        #region IBarcodeScannerView
        public void RenderFrame(Bitmap frame)
        {
            if (IsDisposed) { frame.Dispose(); return; }

            var previousImage = _videoBox.Image;
            _videoBox.Image = frame;
            previousImage?.Dispose();
        }

        public void ShowStatus(string statusText)
        {
            if (IsDisposed) return;
            _statusLabel.Text = statusText ?? string.Empty;
        }

        public void ShowCameraError(string message)
        {
            if (IsDisposed) return;

            _videoBox.Visible = false;
            _errorLabel.Text = message;
            _errorLabel.Visible = true;
            _statusLabel.Text = string.Empty;
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _presenter?.Dispose();
                _videoBox.Image?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}