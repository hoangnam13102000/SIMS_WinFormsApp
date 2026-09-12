using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.UI.Controls
{
    /// <summary>
    /// Static helper cung cấp API ngắn gọn để hiển thị BaseDialog.
    /// Không phải Form - chỉ là static class helper.
    /// 
    /// Ví dụ sử dụng:
    ///   DialogHelper.ShowInfo(this, "Lưu thành công");
    ///   if (DialogHelper.Confirm(this, "Xác nhận", "Bạn có chắc muốn xóa?")) { ... }
    /// </summary>
    public static class DialogHelper
    {
        #region Information
        public static DialogResult ShowInfo(IWin32Window owner, string message)
        {
            return ShowInfo(owner, DialogTypeMetadata.GetDefaultTitle(DialogType.Info), message);
        }

        public static DialogResult ShowInfo(IWin32Window owner, string title, string message)
        {
            using (var dialog = new BaseDialog(owner)
            {
                Title = title,
                Message = message,
                IconType = DialogType.Info,
                Buttons = DialogButtons.OK,
                DefaultButton = DialogResult.OK
            })
            {
                return dialog.ShowDialog(owner);
            }
        }
        #endregion

        #region Success
        public static DialogResult ShowSuccess(IWin32Window owner, string message)
        {
            return ShowSuccess(owner, DialogTypeMetadata.GetDefaultTitle(DialogType.Success), message);
        }

        public static DialogResult ShowSuccess(IWin32Window owner, string title, string message)
        {
            using (var dialog = new BaseDialog(owner)
            {
                Title = title,
                Message = message,
                IconType = DialogType.Success,
                Buttons = DialogButtons.OK,
                DefaultButton = DialogResult.OK
            })
            {
                return dialog.ShowDialog(owner);
            }
        }
        #endregion

        #region Warning
        public static DialogResult ShowWarning(IWin32Window owner, string message)
        {
            return ShowWarning(owner, DialogTypeMetadata.GetDefaultTitle(DialogType.Warning), message);
        }

        public static DialogResult ShowWarning(IWin32Window owner, string title, string message)
        {
            using (var dialog = new BaseDialog(owner)
            {
                Title = title,
                Message = message,
                IconType = DialogType.Warning,
                Buttons = DialogButtons.OKCancel,
                DefaultButton = DialogResult.Cancel
            })
            {
                return dialog.ShowDialog(owner);
            }
        }
        #endregion

        #region Error
        public static DialogResult ShowError(IWin32Window owner, string message)
        {
            return ShowError(owner, DialogTypeMetadata.GetDefaultTitle(DialogType.Error), message);
        }

        public static DialogResult ShowError(IWin32Window owner, string title, string message)
        {
            using (var dialog = new BaseDialog(owner)
            {
                Title = title,
                Message = message,
                IconType = DialogType.Error,
                Buttons = DialogButtons.OK,
                DefaultButton = DialogResult.OK
            })
            {
                return dialog.ShowDialog(owner);
            }
        }
        #endregion

        #region Question / Confirmation
        /// <summary>
        /// Hiển thị dialog xác nhận Yes/No. Trả về true nếu người dùng chọn Yes.
        /// </summary>
        public static bool Confirm(IWin32Window owner, string title, string message)
        {
            using (var dialog = new BaseDialog(owner)
            {
                Title = title,
                Message = message,
                IconType = DialogType.Question,
                Buttons = DialogButtons.YesNo,
                DefaultButton = DialogResult.No
            })
            {
                return dialog.ShowDialog(owner) == DialogResult.Yes;
            }
        }

        /// <summary>
        /// Hiển thị dialog xác nhận Yes/No với DefaultButton tùy chỉnh.
        /// </summary>
        public static bool Confirm(IWin32Window owner, string title, string message, DialogResult defaultButton)
        {
            using (var dialog = new BaseDialog(owner)
            {
                Title = title,
                Message = message,
                IconType = DialogType.Question,
                Buttons = DialogButtons.YesNo,
                DefaultButton = defaultButton
            })
            {
                return dialog.ShowDialog(owner) == DialogResult.Yes;
            }
        }

        /// <summary>
        /// Hiển thị dialog xác nhận xóa với wording chuẩn.
        /// </summary>
        public static bool ConfirmDelete(IWin32Window owner, string itemType, string itemName)
        {
            string message = $"Bạn có chắc muốn xóa {itemType} '{itemName}'?\nHành động này không thể hoàn tác.";
            using (var dialog = new BaseDialog(owner)
            {
                Title = "Xác nhận xóa",
                Message = message,
                IconType = DialogType.Warning,
                Buttons = DialogButtons.YesNo,
                DefaultButton = DialogResult.No
            })
            {
                return dialog.ShowDialog(owner) == DialogResult.Yes;
            }
        }

        /// <summary>
        /// Hiển thị dialog với đầy đủ tùy chỉnh. Trả về DialogResult.
        /// </summary>
        public static DialogResult ShowCustom(IWin32Window owner, string title, string message,
            DialogType iconType, DialogButtons buttons, DialogResult defaultButton)
        {
            using (var dialog = new BaseDialog(owner)
            {
                Title = title,
                Message = message,
                IconType = iconType,
                Buttons = buttons,
                DefaultButton = defaultButton
            })
            {
                return dialog.ShowDialog(owner);
            }
        }
        #endregion
    }
}