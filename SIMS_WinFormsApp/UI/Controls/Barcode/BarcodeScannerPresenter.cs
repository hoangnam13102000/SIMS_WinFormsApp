using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.Services.Barcode;

namespace SIMS_WinFormsApp.UI.Controls.Barcode
{
    public sealed class BarcodeScannerPresenter : IDisposable
    {
        private const int DecodeIntervalMs = 250;

        private readonly IBarcodeScannerView _view;
        private readonly ISynchronizeInvoke _uiThread;
        private readonly IWebcamCaptureSession _capture;
        private readonly BarcodeScanEngine _engine;
        private readonly Timer _decodeTimer;

        private Bitmap _latestFrame;
        private bool _scanning;

        /// <summary>Bắn ra ĐÚNG 1 LẦN khi đọc được 1 mã (luôn trên luồng UI).</summary>
        public event EventHandler<string> Scanned;

        public BarcodeScannerPresenter(
            IBarcodeScannerView view,
            ISynchronizeInvoke uiThread,
            IWebcamCaptureSession captureSession,
            BarcodeScanEngine engine)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _uiThread = uiThread ?? throw new ArgumentNullException(nameof(uiThread));
            _capture = captureSession ?? throw new ArgumentNullException(nameof(captureSession));
            _engine = engine ?? throw new ArgumentNullException(nameof(engine));

            _decodeTimer = new Timer { Interval = DecodeIntervalMs };
            _decodeTimer.Tick += (s, e) => TryDecodeLatestFrame();
        }

        public void Start()
        {
            if (!_capture.IsCameraAvailable)
            {
                _view.ShowCameraError("Không tìm thấy webcam nào trên máy này. Vui lòng kiểm tra kết nối camera.");
                return;
            }

            _capture.FrameCaptured += OnFrameCaptured;
            _capture.CaptureFailed += OnCaptureFailed;
            _capture.Start();

            _scanning = true;
            _view.ShowStatus("Đang quét...");
            _decodeTimer.Start();
        }

        public void Stop()
        {
            _scanning = false;
            _decodeTimer.Stop();
            _capture.FrameCaptured -= OnFrameCaptured;
            _capture.CaptureFailed -= OnCaptureFailed;
            _capture.Stop();
        }

        // Chạy trên luồng nền của camera (AForge) - CHỈ đưa lệnh về luồng UI, không đụng vào
        // _view/_latestFrame trực tiếp ở đây để tránh truy cập control ngoài luồng UI.
        private void OnFrameCaptured(object sender, Bitmap frame)
        {
            if (!_scanning) { frame.Dispose(); return; }
            try
            {
                _uiThread.BeginInvoke(new Action(() => HandleFrameOnUiThread(frame)), null);
            }
            catch (ObjectDisposedException)
            {
                // Dialog đã đóng đúng lúc khung hình cuối bay tới - bỏ qua khung hình này.
                frame.Dispose();
            }
        }

        private void HandleFrameOnUiThread(Bitmap frame)
        {
            if (!_scanning) { frame.Dispose(); return; }

            var previous = _latestFrame;
            _latestFrame = frame;
            _view.RenderFrame(frame);
            previous?.Dispose();
        }

        private void OnCaptureFailed(object sender, string message)
        {
            if (!_uiThread.InvokeRequired) { HandleCaptureFailed(message); return; }
            _uiThread.BeginInvoke(new Action(() => HandleCaptureFailed(message)), null);
        }

        private void HandleCaptureFailed(string message)
        {
            _scanning = false;
            _decodeTimer.Stop();
            _view.ShowCameraError(message);
        }

        // Chạy trên luồng UI (Timer.Tick) - an toàn đọc _latestFrame trực tiếp.
        private void TryDecodeLatestFrame()
        {
            if (!_scanning || _latestFrame == null) return;

            string code;
            try
            {
                code = _engine.TryDecode(_latestFrame);
            }
            catch
            {
                // Lỗi giải mã 1 khung hình đơn lẻ (ảnh hỏng/đang ghi dở) - bỏ qua, thử khung kế tiếp.
                return;
            }

            if (string.IsNullOrEmpty(code)) return;

            _scanning = false;
            _decodeTimer.Stop();
            Scanned?.Invoke(this, code);
        }

        public void Dispose()
        {
            Stop();
            _decodeTimer.Dispose();
            _capture.Dispose();
            _latestFrame?.Dispose();
            _latestFrame = null;
        }
    }
}