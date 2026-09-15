using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.UI.Controls.Toast
{
    public sealed class ToastService : IToastNotifier
    {
        private const int MarginTop = 20;
        private const int MarginRight = 20;
        private const int Spacing = 10;

        public const int DefaultDurationMs = 5000;

        private readonly ConditionalWeakTable<Form, List<ToastNotification>> _active =
            new ConditionalWeakTable<Form, List<ToastNotification>>();

        public void Show(Control anchor, DialogType type, string title, string message, int durationMs)
        {
            Form owner = ResolveOwner(anchor);
            if (owner == null || owner.IsDisposed) return;

            List<ToastNotification> siblings = _active.GetValue(owner, _ => new List<ToastNotification>());

            string effectiveTitle = string.IsNullOrEmpty(title) ? DialogTypeMetadata.GetDefaultTitle(type) : title;
            var toast = new ToastNotification(owner, type, effectiveTitle, message);

            toast.Dismissed += (s, e) =>
            {
                siblings.Remove(toast);
                Reflow(owner, siblings);
            };

            Point location = NextLocation(owner, siblings);
            siblings.Add(toast);
            toast.Present(location, durationMs > 0 ? durationMs : DefaultDurationMs);
        }

        public void Success(Control anchor, string message) => Show(anchor, DialogType.Success, null, message, DefaultDurationMs);
        public void Success(Control anchor, string title, string message) => Show(anchor, DialogType.Success, title, message, DefaultDurationMs);

        public void Error(Control anchor, string message) => Show(anchor, DialogType.Error, null, message, DefaultDurationMs);
        public void Error(Control anchor, string title, string message) => Show(anchor, DialogType.Error, title, message, DefaultDurationMs);

        public void Warning(Control anchor, string message) => Show(anchor, DialogType.Warning, null, message, DefaultDurationMs);
        public void Warning(Control anchor, string title, string message) => Show(anchor, DialogType.Warning, title, message, DefaultDurationMs);

        public void Info(Control anchor, string message) => Show(anchor, DialogType.Info, null, message, DefaultDurationMs);
        public void Info(Control anchor, string title, string message) => Show(anchor, DialogType.Info, title, message, DefaultDurationMs);

        private static Form ResolveOwner(Control anchor)
        {
            if (anchor == null) return null;
            return anchor as Form ?? anchor.FindForm();
        }

        private static Point NextLocation(Form owner, List<ToastNotification> siblings)
        {
            int y = owner.Top + MarginTop;
            foreach (ToastNotification sibling in siblings)
            {
                y += sibling.Height + Spacing;
            }
            int x = owner.Left + owner.Width - ToastNotification.CardWidth - MarginRight;
            return new Point(x, y);
        }

        private static void Reflow(Form owner, List<ToastNotification> siblings)
        {
            if (owner.IsDisposed) return;

            int y = owner.Top + MarginTop;
            foreach (ToastNotification toast in siblings)
            {
                toast.Reposition(new Point(toast.Left, y));
                y += toast.Height + Spacing;
            }
        }
    }
}