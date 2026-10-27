using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.Services;

namespace SIMS_WinFormsApp.Views.Dialogs
{
    public sealed class DialogService : IDialogService
    {
        public bool Confirm(IWin32Window owner, string title, string message)
        {
            return DialogHelper.Confirm(owner, title, message);
        }

        public bool ConfirmCustom(
            IWin32Window owner,
            string title,
            string message,
            string confirmText,
            string cancelText,
            Color? accentColor = null)
        {
            return DialogHelper.ConfirmCustom(owner, title, message,
                confirmText, cancelText, accentColor);
        }

        public bool ConfirmDelete(IWin32Window owner, string itemType, string itemName)
        {
            return DialogHelper.ConfirmDelete(owner, itemType, itemName);
        }
    }
}
