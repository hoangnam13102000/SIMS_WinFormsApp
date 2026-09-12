using System;
using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.UI.Controls
{

    public class PopupFormBase : Form
    {
        public event EventHandler PopupClosed;

        protected PopupFormBase()
        {
            
            AutoScaleMode = AutoScaleMode.None;
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            TopMost = true;
            DoubleBuffered = true;
            KeyPreview = true;

            Deactivate += (_, __) => SafeClose();
            KeyDown += (_, e) =>
            {
                if (e.KeyCode == Keys.Escape) SafeClose();
            };
        }

        private void SafeClose()
        {
            if (!IsDisposed && Visible) Close();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            PopupClosed?.Invoke(this, EventArgs.Empty);
        }

        public void ShowBelow(Control anchor, int offsetY = 4)
        {
            if (anchor == null || anchor.IsDisposed) return;

            Rectangle working = Screen.FromControl(anchor).WorkingArea;
            Point belowScreen = anchor.PointToScreen(new Point(0, anchor.Height));

            int x = belowScreen.X + anchor.Width - Width;
            if (x + Width > working.Right) x = working.Right - Width;
            if (x < working.Left) x = working.Left;

            int y = belowScreen.Y + offsetY;
            if (y + Height > working.Bottom)
            {
                // Không đủ chỗ phía dưới (cửa sổ gần mép dưới màn hình) -> lật lên trên anchor.
                int above = anchor.PointToScreen(Point.Empty).Y - Height - offsetY;
                y = above >= working.Top ? above : Math.Max(working.Top, working.Bottom - Height);
            }

            Location = new Point(x, y);
            Show(anchor.FindForm());
            Activate();
        }
        public void ShowAbove(Control anchor, int offsetY = 4)
        {
            if (anchor == null || anchor.IsDisposed) return;

            Rectangle working = Screen.FromControl(anchor).WorkingArea;
            Point anchorScreenTopLeft = anchor.PointToScreen(Point.Empty);

            int x = anchorScreenTopLeft.X + anchor.Width - Width;
            if (x + Width > working.Right) x = working.Right - Width;
            if (x < working.Left) x = working.Left;

            int y = anchorScreenTopLeft.Y - Height - offsetY;
            if (y < working.Top)
            {
                // Không đủ chỗ phía trên (anchor gần mép trên màn hình) -> lật xuống dưới anchor.
                int below = anchorScreenTopLeft.Y + anchor.Height + offsetY;
                y = below + Height <= working.Bottom ? below : Math.Max(working.Top, working.Bottom - Height);
            }

            Location = new Point(x, y);
            Show(anchor.FindForm());
            Activate();
        }
    }
}