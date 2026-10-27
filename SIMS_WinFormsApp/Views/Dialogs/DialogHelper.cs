using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Views.Dialogs
{
    public static class DialogHelper
    {
        public static DialogResult Show(
            IWin32Window owner,
            string message,
            string title,
            MessageBoxButtons buttons,
            MessageBoxIcon icon,
            MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1)
        {
            string[] buttonTexts;
            DialogResult[] results;
            GetButtons(buttons, out buttonTexts, out results);

            MessageDialogStyle style = GetStyle(icon);
            using (var dialog = new frmMessageDialog())
            {
                dialog.Configure(title, message, buttonTexts, results,
                    GetDefaultButtonIndex(defaultButton), GetCloseResult(results),
                    style.IconText, style.AccentColor);

                return owner == null ? dialog.ShowDialog() : dialog.ShowDialog(owner);
            }
        }

        public static DialogResult ShowInfo(IWin32Window owner, string message)
        {
            return ShowInfo(owner, "Thông tin", message);
        }

        public static DialogResult ShowInfo(IWin32Window owner, string title, string message)
        {
            return Show(owner, message, title ?? "Thông tin",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult ShowSuccess(IWin32Window owner, string message)
        {
            return ShowSuccess(owner, "Thành công", message);
        }

        public static DialogResult ShowSuccess(IWin32Window owner, string title, string message)
        {
            return ShowCore(owner, title ?? "Thành công", message,
                MessageBoxButtons.OK, DialogResult.OK, 0, "✓", AppColors.Success);
        }

        public static DialogResult ShowWarning(IWin32Window owner, string message)
        {
            return ShowWarning(owner, "Cảnh báo", message);
        }

        public static DialogResult ShowWarning(IWin32Window owner, string title, string message)
        {
            return Show(owner, message, title ?? "Cảnh báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static DialogResult ShowError(IWin32Window owner, string message)
        {
            return ShowError(owner, "Lỗi", message);
        }

        public static DialogResult ShowError(IWin32Window owner, string title, string message)
        {
            return Show(owner, message, title ?? "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static bool Confirm(IWin32Window owner, string title, string message)
        {
            return Show(owner, message, title ?? "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes;
        }

        public static bool ConfirmCustom(
            IWin32Window owner,
            string title,
            string message,
            string confirmText,
            string cancelText,
            Color? accentColor = null)
        {
            using (var dialog = new frmMessageDialog())
            {
                dialog.Configure(title ?? "Xác nhận", message,
                    new[] { confirmText ?? "Đồng ý", cancelText ?? "Hủy" },
                    new[] { DialogResult.Yes, DialogResult.No },
                    1, DialogResult.No, "?", accentColor ?? AppColors.Accent);

                return (owner == null ? dialog.ShowDialog() : dialog.ShowDialog(owner)) == DialogResult.Yes;
            }
        }

        public static bool ConfirmDelete(IWin32Window owner, string itemType, string itemName)
        {
            string message = $"Bạn có chắc muốn xóa {itemType} '{itemName}'?\nHành động này không thể hoàn tác.";
            return Show(owner, message, "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes;
        }

        private static DialogResult ShowCore(
            IWin32Window owner,
            string title,
            string message,
            MessageBoxButtons buttons,
            DialogResult closeResult,
            int defaultButtonIndex,
            string iconText,
            Color accentColor)
        {
            string[] buttonTexts;
            DialogResult[] results;
            GetButtons(buttons, out buttonTexts, out results);

            using (var dialog = new frmMessageDialog())
            {
                dialog.Configure(title, message, buttonTexts, results,
                    defaultButtonIndex, closeResult, iconText, accentColor);

                return owner == null ? dialog.ShowDialog() : dialog.ShowDialog(owner);
            }
        }

        private static void GetButtons(
            MessageBoxButtons buttons,
            out string[] texts,
            out DialogResult[] results)
        {
            switch (buttons)
            {
                case MessageBoxButtons.OK:
                    texts = new[] { "Đóng" };
                    results = new[] { DialogResult.OK };
                    break;
                case MessageBoxButtons.OKCancel:
                    texts = new[] { "Đồng ý", "Hủy" };
                    results = new[] { DialogResult.OK, DialogResult.Cancel };
                    break;
                case MessageBoxButtons.YesNo:
                    texts = new[] { "Có", "Không" };
                    results = new[] { DialogResult.Yes, DialogResult.No };
                    break;
                case MessageBoxButtons.YesNoCancel:
                    texts = new[] { "Có", "Không", "Hủy" };
                    results = new[] { DialogResult.Yes, DialogResult.No, DialogResult.Cancel };
                    break;
                case MessageBoxButtons.RetryCancel:
                    texts = new[] { "Thử lại", "Hủy" };
                    results = new[] { DialogResult.Retry, DialogResult.Cancel };
                    break;
                case MessageBoxButtons.AbortRetryIgnore:
                    texts = new[] { "Hủy bỏ", "Thử lại", "Bỏ qua" };
                    results = new[] { DialogResult.Abort, DialogResult.Retry, DialogResult.Ignore };
                    break;
                default:
                    throw new System.ArgumentOutOfRangeException(nameof(buttons));
            }
        }

        private static int GetDefaultButtonIndex(MessageBoxDefaultButton defaultButton)
        {
            switch (defaultButton)
            {
                case MessageBoxDefaultButton.Button2: return 1;
                case MessageBoxDefaultButton.Button3: return 2;
                default: return 0;
            }
        }

        private static DialogResult GetCloseResult(DialogResult[] results)
        {
            foreach (DialogResult result in results)
            {
                if (result == DialogResult.Cancel) return DialogResult.Cancel;
            }

            foreach (DialogResult result in results)
            {
                if (result == DialogResult.No) return DialogResult.No;
            }

            foreach (DialogResult result in results)
            {
                if (result == DialogResult.Abort) return DialogResult.Abort;
            }

            return results[0];
        }

        private static MessageDialogStyle GetStyle(MessageBoxIcon icon)
        {
            switch (icon)
            {
                case MessageBoxIcon.Warning:
                    return new MessageDialogStyle("!", AppColors.Warning);
                case MessageBoxIcon.Error:
                    return new MessageDialogStyle("×", AppColors.Error);
                case MessageBoxIcon.Question:
                    return new MessageDialogStyle("?", AppColors.Accent);
                case MessageBoxIcon.Information:
                    return new MessageDialogStyle("i", AppColors.Info);
                default:
                    return new MessageDialogStyle("•", AppColors.Accent);
            }
        }

        private sealed class MessageDialogStyle
        {
            public MessageDialogStyle(string iconText, Color accentColor)
            {
                IconText = iconText;
                AccentColor = accentColor;
            }

            public string IconText { get; private set; }
            public Color AccentColor { get; private set; }
        }
    }
}
