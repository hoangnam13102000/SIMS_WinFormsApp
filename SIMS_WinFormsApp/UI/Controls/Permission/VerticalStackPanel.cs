using System;
using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.UI.Controls.Permission
{
    public class VerticalStackPanel : FlowLayoutPanel
    {
        public VerticalStackPanel()
        {
            FlowDirection = FlowDirection.TopDown;
            WrapContents = false;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.Transparent;
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            SyncChildWidths();
        }

        protected override void OnClientSizeChanged(EventArgs e)
        {
            base.OnClientSizeChanged(e);
            SyncChildWidths();
        }

        private void SyncChildWidths()
        {
            int targetWidth = ClientSize.Width;
            if (targetWidth <= 0) return;

            foreach (Control child in Controls)
            {
                int desiredWidth = targetWidth - child.Margin.Left - child.Margin.Right;
                if (desiredWidth > 0 && child.Width != desiredWidth)
                {
                    child.Width = desiredWidth;
                }
            }
        }
    }
}