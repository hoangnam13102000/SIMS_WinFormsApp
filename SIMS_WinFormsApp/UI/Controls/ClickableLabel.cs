using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{
    public class ClickableLabel : Label
    {
        private Font _normalFont;
        private Font _underlineFont;

        public ClickableLabel()
        {
            ForeColor = AppColors.Accent;
            Font = AppFonts.SmallBold;
            Cursor = Cursors.Hand;
            AutoSize = true;
            BackColor = System.Drawing.Color.Transparent;
            MouseEnter += (s, e) => { Font = _underlineFont; };
            MouseLeave += (s, e) => { Font = _normalFont; };
        }

        protected override void OnFontChanged(System.EventArgs e)
        {
            base.OnFontChanged(e);
            if (_normalFont == null || Font.Style != FontStyle.Underline)
            {
                _normalFont?.Dispose();
                _underlineFont?.Dispose();
                _normalFont = Font;
                _underlineFont = new Font(Font, Font.Style | FontStyle.Underline);
            }
        }
    }
}