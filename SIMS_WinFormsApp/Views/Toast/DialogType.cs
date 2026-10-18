using FontAwesome.Sharp;
using System.Drawing;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Views.Toast
{
    public enum DialogType
    {
        Info,
        Warning,
        Error,
        Success,
        Question,
        Danger
    }

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
                case DialogType.Danger: return IconChar.SignOutAlt;
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
                case DialogType.Danger: return AppColors.Error;
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
                case DialogType.Danger: return AppColors.ErrorBg;
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
                case DialogType.Danger: return "Xác nhận";
                default: return "Thông tin";
            }
        }

    }
}