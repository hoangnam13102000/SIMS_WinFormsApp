using System.Windows.Forms;
using System.Drawing;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Views.Sales
{
    public partial class PromotionEditorDialog : Form
    {
        public PromotionEditorDialog()
        {
            InitializeComponent();
            _codeInput.Text = "SUMMER10";
            _nameInput.Text = "Ưu đãi mùa hè";
            _valueInput.Text = "10";
            _maximumInput.Text = "100000";
            _minimumInput.Text = "200000";
            _limitInput.Text = "100";
            _activeInput.Checked = true;
            ApplyThemeColors();
        }

        public PromotionEditorDialog(bool isNew)
        {
            InitializeComponent();
            Text = isNew ? "Tạo mã giảm giá" : "Cập nhật mã giảm giá";
            _title.Text = isNew ? "Tạo mã giảm giá mới" : "Cập nhật mã giảm giá";
            _saveButton.Text = isNew ? "Tạo mã" : "Lưu thay đổi";
            _minimumInput.Text = "0";
            _activeInput.Checked = true;
            ApplyThemeColors();
        }

        public TextBox CodeInput { get { return _codeInput; } }
        public TextBox NameInput { get { return _nameInput; } }
        public ComboBox DiscountTypeInput { get { return _typeInput; } }
        public TextBox DiscountValueInput { get { return _valueInput; } }
        public TextBox MaximumDiscountInput { get { return _maximumInput; } }
        public TextBox MinimumOrderInput { get { return _minimumInput; } }
        public DateTimePicker StartDateInput { get { return _startInput; } }
        public DateTimePicker EndDateInput { get { return _endInput; } }
        public TextBox UsageLimitInput { get { return _limitInput; } }
        public CheckBox ActiveInput { get { return _activeInput; } }
        public CheckBox BannerInput { get { return _bannerInput; } }
        public Button SaveButton { get { return _saveButton; } }

        private void ApplyThemeColors()
        {
            BackColor = AppColors.PageBg;
            _layout.BackColor = AppColors.White;
            _title.ForeColor = AppColors.TextTitle;
            _subtitle.ForeColor = AppColors.TextMuted;

            Label[] labels =
            {
                _codeLabel, _nameLabel, _typeLabel, _valueLabel, _maximumLabel,
                _minimumLabel, _startLabel, _endLabel, _limitLabel
            };
            foreach (Label label in labels)
                label.ForeColor = AppColors.TextMuted;

            Control[] fields =
            {
                _codeInput, _nameInput, _typeInput, _valueInput, _maximumInput,
                _minimumInput, _startInput, _endInput, _limitInput
            };
            foreach (Control field in fields)
            {
                field.BackColor = AppColors.White;
                field.ForeColor = AppColors.TextPrimary;
            }

            _saveButton.BackColor = AppColors.Accent;
            _saveButton.FlatAppearance.BorderColor = AppColors.Accent;
            _saveButton.FlatAppearance.MouseOverBackColor = AppColors.AccentHover;
            _cancelButton.BackColor = AppColors.CancelBg;
            _cancelButton.FlatAppearance.BorderColor = AppColors.Border;
            _cancelButton.FlatAppearance.MouseOverBackColor = AppColors.CancelHover;
            _cancelButton.ForeColor = AppColors.TextPrimary;
        }
    }
}
