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
            Font = new Font("Segoe UI", 12.5f, FontStyle.Bold, GraphicsUnit.Point);
            Cursor = Cursors.Hand;
            AutoSize = false;
            Size = new Size(180, 32);
            TextAlign = ContentAlignment.MiddleRight;
            BackColor = System.Drawing.Color.Transparent;
            Margin = new Padding(0);
            _normalFont = Font;
            _underlineFont = new Font(Font, Font.Style | FontStyle.Underline);
            MouseEnter += (s, e) => { Font = _underlineFont; };
            MouseLeave += (s, e) => { Font = _normalFont; };
        }

        protected override void OnFontChanged(System.EventArgs e)
        {
            base.OnFontChanged(e);
            if (_normalFont == null || _normalFont.FontFamily != Font.FontFamily || _normalFont.Size != Font.Size || _normalFont.Style != (Font.Style & ~FontStyle.Underline))
            {
                _normalFont?.Dispose();
                _underlineFont?.Dispose();
                _normalFont = new Font(Font.FontFamily, Font.Size, Font.Style & ~FontStyle.Underline, GraphicsUnit.Point);
                _underlineFont = new Font(Font.FontFamily, Font.Size, Font.Style | FontStyle.Underline, GraphicsUnit.Point);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var flags = TextFormatFlags.SingleLine | TextFormatFlags.NoPadding | TextFormatFlags.TextBoxControl | TextFormatFlags.VerticalCenter | TextFormatFlags.WordEllipsis;
            TextRenderer.DrawText(e.Graphics, Text, Font, ClientRectangle, ForeColor, flags |
                (RightToLeft == RightToLeft.Yes ? TextFormatFlags.RightToLeft : 0) |
                (TextAlign == ContentAlignment.MiddleRight ? TextFormatFlags.Right : TextFormatFlags.Left));
        }
    }
}