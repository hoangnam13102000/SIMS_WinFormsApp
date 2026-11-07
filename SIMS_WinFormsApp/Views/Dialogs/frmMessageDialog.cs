using System;
using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Views.Dialogs
{
    public partial class frmMessageDialog : Form
    {
        public frmMessageDialog()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.None;
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            ShowInTaskbar = false;
            MinimizeBox = false;
            MaximizeBox = false;
            KeyPreview = true;
        }

        internal void Configure(
            string title,
            string message,
            string[] buttonTexts,
            DialogResult[] results,
            int defaultButtonIndex,
            DialogResult closeResult,
            string iconText,
            Color accentColor)
        {
            if (buttonTexts == null) throw new ArgumentNullException(nameof(buttonTexts));
            if (results == null) throw new ArgumentNullException(nameof(results));
            if (buttonTexts.Length == 0 || buttonTexts.Length > 3 || buttonTexts.Length != results.Length)
                throw new ArgumentException("A dialog must have one to three matching buttons.");

            Text = title ?? string.Empty;
            lblTitle.Text = title ?? string.Empty;
            lblMessage.Text = message ?? string.Empty;
            Size messageSize = TextRenderer.MeasureText(
                lblMessage.Text,
                lblMessage.Font,
                new Size(lblMessage.Width, int.MaxValue),
                TextFormatFlags.WordBreak | TextFormatFlags.TextBoxControl);
            lblMessage.Height = Math.Max(58, messageSize.Height + 8);
            ClientSize = new Size(
                ClientSize.Width,
                88 + lblMessage.Height + 18 + pnlFooter.Height);
            lblIcon.Text = iconText ?? string.Empty;
            pnlIcon.BackColor = Color.FromArgb(
                (accentColor.R + 4 * 255) / 5,
                (accentColor.G + 4 * 255) / 5,
                (accentColor.B + 4 * 255) / 5);
            lblIcon.ForeColor = accentColor;

            Button[] buttons =
            {
                btnPrimary,
                btnSecondary,
                buttonTexts.Length == 3 ? _tertiaryButton : null
            };
            _tertiaryButton.Visible = buttonTexts.Length == 3;
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i] == null) continue;
                buttons[i].Visible = i < buttonTexts.Length;
                if (i >= buttonTexts.Length) continue;

                buttons[i].Text = buttonTexts[i] ?? string.Empty;
                buttons[i].DialogResult = results[i];
                buttons[i].BackColor = i == 0 ? accentColor : Color.White;
                buttons[i].ForeColor = i == 0 ? Color.White : Color.FromArgb(51, 65, 85);
                buttons[i].FlatAppearance.BorderColor = i == 0
                    ? accentColor
                    : Color.FromArgb(203, 213, 225);
            }

            int safeDefaultIndex = Math.Max(0, Math.Min(defaultButtonIndex, buttonTexts.Length - 1));
            AcceptButton = buttons[safeDefaultIndex];
            _closeResult = closeResult;
            PositionButtons(buttons, buttonTexts.Length);
        }

        private DialogResult _closeResult = DialogResult.Cancel;

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                DialogResult = _closeResult;
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void PositionButtons(Button[] buttons, int visibleCount)
        {
            const int rightMargin = 24;
            const int spacing = 10;
            int x = pnlFooter.ClientSize.Width - rightMargin;

            for (int i = 0; i < visibleCount; i++)
            {
                Button button = buttons[i];
                if (button == null) continue;
                x -= button.Width;
                button.Location = new Point(x, button.Location.Y);
                x -= spacing;
            }
        }

        private void btnPrimary_Click(object sender, EventArgs e)
        {
            // Trả kết quả đã gán trong Configure; WinForms tự đóng dialog.
            DialogResult = btnPrimary.DialogResult;
        }

        private void btnSecondary_Click(object sender, EventArgs e)
        {
            // Trả kết quả đã gán trong Configure; WinForms tự đóng dialog.
            DialogResult = btnSecondary.DialogResult;
        }

        private void _tertiaryButton_Click(object sender, EventArgs e)
        {
            // Trả kết quả đã gán trong Configure; WinForms tự đóng dialog.
            DialogResult = _tertiaryButton.DialogResult;
        }
    }
}
