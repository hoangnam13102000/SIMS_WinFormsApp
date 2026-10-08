using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Views.Chat
{
    public sealed partial class AiChatPopupForm : PopupFormBase
    {
        public AiChatPopupForm()
        {
            InitializeComponent();
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
        }

        public AiChatPopupForm(ucAiChat chatView)
        {
            InitializeComponent();
            Controls.Clear();
            if (chatView == null)
                throw new System.ArgumentNullException(nameof(chatView));

            Size = new Size(410, 590);
            MinimumSize = new Size(360, 420);
            BackColor = AppColors.BgLighter;
            Padding = new Padding(1);
            FormClosed += (sender, args) => chatView.CloseRequested -= OnCloseRequested;
            chatView.CloseRequested += OnCloseRequested;

            var frame = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(0),
                BackColor = AppColors.BgLighter
            };
            chatView.Dock = DockStyle.Fill;
            frame.Controls.Add(chatView);
            Controls.Add(frame);

            Resize += (sender, args) => ApplyRoundedRegion();
            ApplyRoundedRegion();
        }

        private void OnCloseRequested()
        {
            if (!IsDisposed) Close();
        }

        private void ApplyRoundedRegion()
        {
            if (Width <= 0 || Height <= 0) return;

            using (var path = AppRadius.GetRoundedPath(
                new Rectangle(0, 0, Width, Height),
                AppRadius.Large))
            {
                Region oldRegion = Region;
                Region = new Region(path);
                oldRegion?.Dispose();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (var pen = new Pen(AppColors.Border, 1.5f))
            using (var path = AppRadius.GetRoundedPath(
                new Rectangle(0, 0, Width - 1, Height - 1),
                AppRadius.Large))
            {
                e.Graphics.DrawPath(pen, path);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Region?.Dispose();
            base.Dispose(disposing);
        }
    }
}
