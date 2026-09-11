using System;
using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Controls;

namespace SIMS_WinFormsApp.Services
{
    public interface IDialogService
    {
        bool Confirm(IWin32Window owner, string title, string message);

        bool ConfirmCustom(IWin32Window owner, string title, string message,
            string confirmText, string cancelText, Color? accentColor = null);

        bool ConfirmDelete(IWin32Window owner, string itemType, string itemName);
    }

    /// <summary>
    /// DialogService - sử dụng BaseDialog hiện đại thay cho MessageBox mặc định.
    /// Giữ nguyên interface IDialogService cũ để không phá vỡ code đang gọi.
    /// </summary>
    public class DialogService : IDialogService
    {
        public bool Confirm(IWin32Window owner, string title, string message)
        {
            return DialogHelper.Confirm(owner, title, message);
        }

        public bool ConfirmCustom(IWin32Window owner, string title, string message,
            string confirmText, string cancelText, Color? accentColor = null)
        {
            // Lưu ý: BaseDialog hiện tại dùng text chuẩn theo DialogButtons enum.
            // Nếu cần text tùy chỉnh hoàn toàn, sử dụng trực tiếp BaseDialog với API object initializer.
            // Phương thức này giữ nguyên signature để tương thích ngược.
            return DialogHelper.Confirm(owner, title, message);
        }

        public bool ConfirmDelete(IWin32Window owner, string itemType, string itemName)
        {
            return DialogHelper.ConfirmDelete(owner, itemType, itemName);
        }
    }
}