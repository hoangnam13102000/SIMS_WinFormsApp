using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.I18n;

namespace SIMS_WinFormsApp.UI.Controls
{

    public enum DialogButtons
    {
        OK,
        OKCancel,
        YesNo,
        YesNoCancel
    }

    internal class DialogButtonInfo
    {
        public string Text { get; set; }
        public DialogResult Result { get; set; }
        public bool IsPrimary { get; set; }
    }

    internal static class DialogButtonsFactory
    {
        public static List<DialogButtonInfo> CreateButtons(DialogButtons buttons, DialogType type)
        {
            var list = new List<DialogButtonInfo>();

            switch (buttons)
            {
                case DialogButtons.OK:
                    list.Add(new DialogButtonInfo { Text = Lang.Get("dialog.button.ok"), Result = DialogResult.OK, IsPrimary = true });
                    break;

                case DialogButtons.OKCancel:
                    list.Add(new DialogButtonInfo { Text = Lang.Get("common.cancel"), Result = DialogResult.Cancel, IsPrimary = false });
                    list.Add(new DialogButtonInfo { Text = Lang.Get("dialog.button.ok"), Result = DialogResult.OK, IsPrimary = true });
                    break;

                case DialogButtons.YesNo:
                    list.Add(new DialogButtonInfo { Text = Lang.Get("common.no"), Result = DialogResult.No, IsPrimary = false });
                    list.Add(new DialogButtonInfo { Text = Lang.Get("common.yes"), Result = DialogResult.Yes, IsPrimary = true });
                    break;

                case DialogButtons.YesNoCancel:
                    list.Add(new DialogButtonInfo { Text = Lang.Get("common.cancel"), Result = DialogResult.Cancel, IsPrimary = false });
                    list.Add(new DialogButtonInfo { Text = Lang.Get("common.no"), Result = DialogResult.No, IsPrimary = false });
                    list.Add(new DialogButtonInfo { Text = Lang.Get("common.yes"), Result = DialogResult.Yes, IsPrimary = true });
                    break;
            }

            return list;
        }
    }
}