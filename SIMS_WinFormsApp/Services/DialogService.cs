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

}