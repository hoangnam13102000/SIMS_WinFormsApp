using System.Collections.Generic;
using System.Drawing;
using ZXing;

namespace SIMS_WinFormsApp.Services.Barcode
{
    public sealed class BarcodeScanEngine
    {
        private readonly BarcodeReader _reader;

        public BarcodeScanEngine()
        {
            _reader = new BarcodeReader
            {
                AutoRotate = true,
                Options =
                {
                    TryHarder = true,
                    PossibleFormats = new List<BarcodeFormat>
                    {
                        BarcodeFormat.CODE_128,
                        BarcodeFormat.CODE_39,
                        BarcodeFormat.EAN_13,
                        BarcodeFormat.EAN_8,
                        BarcodeFormat.UPC_A,
                        BarcodeFormat.UPC_E,
                        BarcodeFormat.QR_CODE
                    }
                }
            };
        }

        public string TryDecode(Bitmap frame)
        {
            if (frame == null) return null;
            var result = _reader.Decode(frame);
            return string.IsNullOrEmpty(result?.Text) ? null : result.Text;
        }
    }
}