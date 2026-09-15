using System.Windows.Forms;

namespace SIMS_WinFormsApp.UI.Controls.Toast
{

    public interface IToastNotifier
    {
        void Show(Control anchor, DialogType type, string title, string message, int durationMs);

        void Success(Control anchor, string message);
        void Success(Control anchor, string title, string message);

        void Error(Control anchor, string message);
        void Error(Control anchor, string title, string message);

        void Warning(Control anchor, string message);
        void Warning(Control anchor, string title, string message);

        void Info(Control anchor, string message);
        void Info(Control anchor, string title, string message);
    }
}