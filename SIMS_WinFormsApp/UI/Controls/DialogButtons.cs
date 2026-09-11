using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.UI.Controls
{
    /// <summary>
    /// Định nghĩa các bộ nút hiển thị trên dialog.
    /// Không phải Form - chỉ là enum.
    /// </summary>
    public enum DialogButtons
    {
        OK,
        OKCancel,
        YesNo,
        YesNoCancel
    }

    /// <summary>
    /// Thông tin cấu hình cho mỗi button trong dialog.
    /// </summary>
    internal class DialogButtonInfo
    {
        public string Text { get; set; }
        public DialogResult Result { get; set; }
        public bool IsPrimary { get; set; }
    }

    /// <summary>
    /// Helper để tạo danh sách button dựa trên DialogButtons enum.
    /// </summary>
    internal static class DialogButtonsFactory
    {
        public static List<DialogButtonInfo> CreateButtons(DialogButtons buttons, DialogType type)
        {
            var list = new List<DialogButtonInfo>();

            switch (buttons)
            {
                case DialogButtons.OK:
                    list.Add(new DialogButtonInfo { Text = "OK", Result = DialogResult.OK, IsPrimary = true });
                    break;

                case DialogButtons.OKCancel:
                    list.Add(new DialogButtonInfo { Text = "Hủy", Result = DialogResult.Cancel, IsPrimary = false });
                    list.Add(new DialogButtonInfo { Text = "Đồng ý", Result = DialogResult.OK, IsPrimary = true });
                    break;

                case DialogButtons.YesNo:
                    list.Add(new DialogButtonInfo { Text = "Không", Result = DialogResult.No, IsPrimary = false });
                    list.Add(new DialogButtonInfo { Text = "Có", Result = DialogResult.Yes, IsPrimary = true });
                    break;

                case DialogButtons.YesNoCancel:
                    list.Add(new DialogButtonInfo { Text = "Hủy", Result = DialogResult.Cancel, IsPrimary = false });
                    list.Add(new DialogButtonInfo { Text = "Không", Result = DialogResult.No, IsPrimary = false });
                    list.Add(new DialogButtonInfo { Text = "Có", Result = DialogResult.Yes, IsPrimary = true });
                    break;
            }

            return list;
        }
    }
}