using System;
using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Services
{
    public interface IDialogService
    {
        bool Confirm(IWin32Window owner, string title, string message);

        bool ConfirmCustom(IWin32Window owner, string title, string message,
            string confirmText, string cancelText, Color? accentColor = null);

        bool ConfirmDelete(IWin32Window owner, string itemType, string itemName);
    }

    public class DialogService : IDialogService
    {
        public bool Confirm(IWin32Window owner, string title, string message)
        {
            return MessageBox.Show(owner, message, title, MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes;
        }

        public bool ConfirmCustom(IWin32Window owner, string title, string message,
            string confirmText, string cancelText, Color? accentColor = null)
        {
            return MessageBox.Show(owner, message, title, MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes;
        }

        public bool ConfirmDelete(IWin32Window owner, string itemType, string itemName)
        {
            string message = $"Bạn có chắc muốn xóa {itemType} '{itemName}'?";
            return MessageBox.Show(owner, message, "Xác nhận xóa", MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes;
        }
    }
}