using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{
    [ToolboxItem(false)]
    [DesignerCategory("Code")]
    public class LabeledComboField : UserControl
    {
        private const int FieldHeight = 36;
        private const int SpacingAfterLabel = 6;
        private const int SpacingAfterBlock = 18;

        private readonly Label _lblTitle;
        private readonly Label _lblRequiredMark;
        private readonly ComboBox _combo;

        public LabeledComboField()
        {
            BackColor = Color.Transparent;
            Dock = DockStyle.Top;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;

            _lblTitle = new Label
            {
                AutoSize = true,
                Font = AppFonts.SmallBold,
                ForeColor = AppColors.TextTitle,
                BackColor = Color.Transparent,
                Location = new Point(0, 0)
            };
            _lblRequiredMark = new Label
            {
                AutoSize = true,
                Text = " *",
                Font = AppFonts.SmallBold,
                ForeColor = AppColors.Error,
                BackColor = Color.Transparent,
                Visible = false
            };
            _combo = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat,
                Font = AppFonts.Body,
                Height = FieldHeight
            };

            Controls.Add(_combo);
            Controls.Add(_lblRequiredMark);
            Controls.Add(_lblTitle);

            Resize += (s, e) => LayoutChildren();
            LayoutChildren();
        }

        public string LabelText
        {
            get => _lblTitle.Text;
            set { _lblTitle.Text = value ?? string.Empty; LayoutChildren(); }
        }

        public bool IsRequired
        {
            get => _lblRequiredMark.Visible;
            set { _lblRequiredMark.Visible = value; LayoutChildren(); }
        }

        public void SetItems(IEnumerable items)
        {
            _combo.Items.Clear();
            if (items != null)
                foreach (var item in items) _combo.Items.Add(item);
            if (_combo.Items.Count > 0) _combo.SelectedIndex = 0;
        }

        public object SelectedItem
        {
            get => _combo.SelectedItem;
            set => _combo.SelectedItem = value;
        }

        private void LayoutChildren()
        {
            if (_combo == null) return;

            _lblTitle.Location = new Point(0, 0);
            _lblRequiredMark.Location = new Point(_lblTitle.Right + 1, 0);

            int titleBottom = Math.Max(_lblTitle.Bottom, _lblRequiredMark.Visible ? _lblRequiredMark.Bottom : 0);
            _combo.Location = new Point(0, titleBottom + SpacingAfterLabel);
            _combo.Width = Math.Max(10, ClientSize.Width);

            int preferredHeight = _combo.Bottom + SpacingAfterBlock;
            if (Height != preferredHeight) Height = preferredHeight;
        }
    }
}