using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Forms.Chat
{
    partial class AiChatPopupForm
    {
        private Panel _designerFrame;

        private void InitializeComponent()
        {
            _designerFrame = new Panel();
            SuspendLayout();
            _designerFrame.BackColor = SystemColors.Window;
            _designerFrame.Dock = DockStyle.Fill;
            _designerFrame.Name = "chatFrame";
            _designerFrame.Controls.Add(new Label
            {
                Dock = DockStyle.Top,
                Height = 76,
                Text = "Trợ lý AI",
                TextAlign = ContentAlignment.MiddleLeft
            });
            Controls.Add(_designerFrame);
            BackColor = SystemColors.Window;
            ClientSize = new Size(410, 590);
            MinimumSize = new Size(360, 420);
            Name = "AiChatPopupForm";
            Text = "Trợ lý AI";
            ResumeLayout(false);
        }
    }
}
