using System;
using System.Drawing;

namespace SIMS_WinFormsApp.Services.Barcode
{
    public interface IWebcamCaptureSession : IDisposable
    {
        /// <summary>true nếu máy có ít nhất 1 webcam khả dụng.</summary>
        bool IsCameraAvailable { get; }

        /// <summary>Bắn ra mỗi khi có 1 khung hình mới (đã Clone - an toàn dùng ngoài luồng camera).</summary>
        event EventHandler<Bitmap> FrameCaptured;

        /// <summary>Bắn ra nếu mở camera thất bại (ví dụ camera đang bị ứng dụng khác chiếm).</summary>
        event EventHandler<string> CaptureFailed;

        void Start();

        void Stop();
    }
}