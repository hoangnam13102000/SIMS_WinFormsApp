using System;
using System.Drawing;
using AForge.Video;
using AForge.Video.DirectShow;

namespace SIMS_WinFormsApp.Services.Barcode
{
    public sealed class AForgeWebcamCaptureSession : IWebcamCaptureSession
    {
        private VideoCaptureDevice _device;
        private bool _disposed;

        public event EventHandler<Bitmap> FrameCaptured;
        public event EventHandler<string> CaptureFailed;

        public bool IsCameraAvailable
        {
            get
            {
                try
                {
                    return new FilterInfoCollection(FilterCategory.VideoInputDevice).Count > 0;
                }
                catch
                {
                    // Máy không có DirectShow / driver camera lỗi - coi như không có camera,
                    // để nơi gọi hiển thị thông báo thân thiện thay vì crash.
                    return false;
                }
            }
        }

        public void Start()
        {
            try
            {
                var devices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                if (devices.Count == 0)
                {
                    CaptureFailed?.Invoke(this, "Không tìm thấy webcam nào trên máy này. Vui lòng kiểm tra kết nối camera.");
                    return;
                }

                _device = new VideoCaptureDevice(devices[0].MonikerString);
                _device.NewFrame += OnNewFrame;
                _device.Start();
            }
            catch (Exception ex)
            {
                CaptureFailed?.Invoke(this, "Không thể mở webcam: " + ex.Message);
            }
        }

        private void OnNewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            // BẮT BUỘC Clone(): AForge tái sử dụng cùng 1 buffer ảnh cho mỗi khung hình, dùng
            // trực tiếp eventArgs.Frame ngoài phạm vi sự kiện này (kể cả chỉ để đọc) sẽ dính
            // ảnh rác/ảnh bị ghi đè giữa chừng.
            Bitmap clone;
            try
            {
                clone = (Bitmap)eventArgs.Frame.Clone();
            }
            catch
            {
                return; // 1 khung hình lỗi đơn lẻ - bỏ qua, không dừng cả phiên quay.
            }
            FrameCaptured?.Invoke(this, clone);
        }

        public void Stop()
        {
            if (_device == null) return;

            _device.NewFrame -= OnNewFrame;
            if (_device.IsRunning)
            {
                _device.SignalToStop();
                _device.WaitForStop();
            }
            _device = null;
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            Stop();
        }
    }
}