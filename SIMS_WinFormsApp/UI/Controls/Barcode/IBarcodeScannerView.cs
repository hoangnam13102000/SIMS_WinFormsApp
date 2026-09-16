using System.Drawing;

namespace SIMS_WinFormsApp.UI.Controls.Barcode
{
    public interface IBarcodeScannerView
    {
        void RenderFrame(Bitmap frame);

        void ShowStatus(string statusText);

        void ShowCameraError(string message);
    }
}
