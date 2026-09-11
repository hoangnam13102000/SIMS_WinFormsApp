using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Theme;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.UI.Controls
{
    /// <summary>
    /// Định nghĩa các loại dialog với icon, accent color và title mặc định tương ứng.
    /// Không phải Form - chỉ là enum + metadata helper.
    /// </summary>
    public enum DialogType
    {
        Info,
        Warning,
        Error,
        Success,
        Question
    }

    /// <summary>
    /// Metadata cho mỗi loại DialogType - tránh hard-code màu ở nhiều nơi,
    /// toàn bộ màu lấy từ AppColors hiện có của project.
    /// </summary>
    internal static class DialogTypeMetadata
    {
        public static IconChar GetIcon(DialogType type)
        {
            switch (type)
            {
                case DialogType.Info: return IconChar.CircleInfo;
                case DialogType.Warning: return IconChar.TriangleExclamation;
                case DialogType.Error: return IconChar.CircleXmark;
                case DialogType.Success: return IconChar.CircleCheck;
                case DialogType.Question: return IconChar.CircleQuestion;
                default: return IconChar.CircleInfo;
            }
        }

        public static Color GetAccentColor(DialogType type)
        {
            switch (type)
            {
                case DialogType.Info: return AppColors.Info;
                case DialogType.Warning: return AppColors.Warning;
                case DialogType.Error: return AppColors.Error;
                case DialogType.Success: return AppColors.Success;
                case DialogType.Question: return AppColors.Accent;
                default: return AppColors.Info;
            }
        }

        public static Color GetSoftBgColor(DialogType type)
        {
            switch (type)
            {
                case DialogType.Info: return AppColors.InfoBg;
                case DialogType.Warning: return AppColors.WarningBg;
                case DialogType.Error: return AppColors.ErrorBg;
                case DialogType.Success: return AppColors.SuccessBg;
                case DialogType.Question: return AppColors.AccentBgSoft;
                default: return AppColors.InfoBg;
            }
        }

        public static string GetDefaultTitle(DialogType type)
        {
            switch (type)
            {
                case DialogType.Info: return "Thông tin";
                case DialogType.Warning: return "Cảnh báo";
                case DialogType.Error: return "Lỗi";
                case DialogType.Success: return "Thành công";
                case DialogType.Question: return "Xác nhận";
                default: return "Thông tin";
            }
        }

        public static DialogButtons GetDefaultButtons(DialogType type)
        {
            switch (type)
            {
                case DialogType.Info: return DialogButtons.OK;
                case DialogType.Warning: return DialogButtons.OKCancel;
                case DialogType.Error: return DialogButtons.OK;
                case DialogType.Success: return DialogButtons.OK;
                case DialogType.Question: return DialogButtons.YesNo;
                default: return DialogButtons.OK;
            }
        }

        public static DialogResult GetDefaultButton(DialogType type)
        {
            switch (type)
            {
                case DialogType.Info: return DialogResult.OK;
                case DialogType.Warning: return DialogResult.Cancel;
                case DialogType.Error: return DialogResult.OK;
                case DialogType.Success: return DialogResult.OK;
                case DialogType.Question: return DialogResult.No;
                default: return DialogResult.OK;
            }
        }
    }
}