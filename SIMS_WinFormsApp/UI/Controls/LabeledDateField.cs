using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{

    [ToolboxItem(false)]
    [DesignerCategory("Code")]
    public class LabeledDateField : UserControl
    {
        private const int FieldHeight = 36;
        private const int SpacingAfterLabel = 6;
        private const int SpacingAfterBlock = 18;

        private readonly Label _lblTitle;
        private readonly Label _lblRequiredMark;
        private readonly DateTimePicker _picker;

        public LabeledDateField()
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
            _picker = new DateTimePicker
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy",
                Font = AppFonts.Body,
                Height = FieldHeight,
                MaxDate = DateTime.Today,
                MinDate = new DateTime(1900, 1, 1)
            };

            Controls.Add(_picker);
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

        /// <summary>Cho phép để trống (hiện checkbox trước ô ngày). Khi false (mặc định), ô
        /// luôn có giá trị và <see cref="Value"/> không bao giờ trả về null.</summary>
        public bool AllowEmpty
        {
            get => _picker.ShowCheckBox;
            set { _picker.ShowCheckBox = value; if (!value) _picker.Checked = true; }
        }

        public DateTime? Value
        {
            get => (!_picker.ShowCheckBox || _picker.Checked) ? _picker.Value.Date : (DateTime?)null;
            set
            {
                if (value.HasValue)
                {
                    _picker.Value = value.Value;
                    if (_picker.ShowCheckBox) _picker.Checked = true;
                }
                else if (_picker.ShowCheckBox)
                {
                    _picker.Checked = false;
                }
            }
        }

        public DateTime MaxDate
        {
            get => _picker.MaxDate;
            set => _picker.MaxDate = value;
        }

        public DateTime MinDate
        {
            get => _picker.MinDate;
            set => _picker.MinDate = value;
        }

        private void LayoutChildren()
        {
            if (_picker == null) return;

            _lblTitle.Location = new Point(0, 0);
            _lblRequiredMark.Location = new Point(_lblTitle.Right + 1, 0);

            int titleBottom = Math.Max(_lblTitle.Bottom, _lblRequiredMark.Visible ? _lblRequiredMark.Bottom : 0);
            _picker.Location = new Point(0, titleBottom + SpacingAfterLabel);
            _picker.Width = Math.Max(10, ClientSize.Width);

            int preferredHeight = _picker.Bottom + SpacingAfterBlock;
            if (Height != preferredHeight) Height = preferredHeight;
        }
    }
}