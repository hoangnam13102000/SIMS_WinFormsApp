using System.Windows.Forms;

namespace SIMS_WinFormsApp.UI.Controls.Barcode
{
    public sealed class BarcodeScannerLauncher : IBarcodeScannerLauncher
    {
        public string Scan(IWin32Window owner) => frmBarcodeScannerDialog.Scan(owner);
    }
}