using System.Windows.Forms;

namespace SIMS_WinFormsApp.UI.Controls.Barcode
{
    public interface IBarcodeScannerLauncher
    {
        /// <summary>Mở popup quét, trả về mã quét được hoặc null nếu người dùng hủy.</summary>
        string Scan(IWin32Window owner);

        /// <summary>Mở popup quét với tiêu đề và hướng dẫn riêng.</summary>
        string Scan(IWin32Window owner, string dialogTitle, string instructionText);
    }
}