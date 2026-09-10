using System;
using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.UI.Controls
{
    /// <summary>
    /// Base dùng chung cho các popup không viền neo vào Header (Account dropdown,
    /// Notification panel...). Chuẩn hoá 2 việc hay bị lỗi ở loại control này:
    ///
    /// 1) VỊ TRÍ: tự tránh bị cắt khỏi màn hình khi cửa sổ gần mép (dịch trái nếu
    ///    tràn phải, tự lật lên trên anchor nếu không đủ chỗ phía dưới).
    ///
    /// 2) ĐÓNG POPUP: đóng khi mất kích hoạt (click ra ngoài / chuyển sang cửa sổ khác),
    ///    hoặc khi nhấn Esc. Cố ý KHÔNG dùng cờ WS_EX_NOACTIVATE — một popup không bao
    ///    giờ activate được thì sự kiện Deactivate không đáng tin cậy, khiến dropdown
    ///    "vừa mở đã tự đóng" hoặc ngược lại "click ra ngoài không đóng được".
    /// </summary>
    public class PopupFormBase : Form
    {
        public event EventHandler PopupClosed;

        protected PopupFormBase()
        {
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

        /// <summary>Hiển thị popup ngay dưới control neo (căn phải theo mép phải của anchor),
        /// tự động dịch chuyển để không bao giờ bị cắt khỏi vùng làm việc của màn hình.</summary>
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
    }
}