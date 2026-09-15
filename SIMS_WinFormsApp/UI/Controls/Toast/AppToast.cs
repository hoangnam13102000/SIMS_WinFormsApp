using System.Windows.Forms;

namespace SIMS_WinFormsApp.UI.Controls.Toast
{
    public static class AppToast
    {
        private static readonly IToastNotifier Notifier = new ToastService();

        public static void Success(Control anchor, string message) => Notifier.Success(anchor, message);
        public static void Success(Control anchor, string title, string message) => Notifier.Success(anchor, title, message);

        public static void Error(Control anchor, string message) => Notifier.Error(anchor, message);
        public static void Error(Control anchor, string title, string message) => Notifier.Error(anchor, title, message);

        public static void Warning(Control anchor, string message) => Notifier.Warning(anchor, message);
        public static void Warning(Control anchor, string title, string message) => Notifier.Warning(anchor, title, message);

        public static void Info(Control anchor, string message) => Notifier.Info(anchor, message);
        public static void Info(Control anchor, string title, string message) => Notifier.Info(anchor, title, message);

        /// <summary>Bản đầy đủ: tự chọn loại + thời gian hiển thị (ms, mặc định ~5 giây) trước khi tự biến mất.</summary>
        public static void Show(Control anchor, DialogType type, string title, string message, int durationMs = ToastService.DefaultDurationMs)
            => Notifier.Show(anchor, type, title, message, durationMs);
    }
}