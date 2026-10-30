using System;
using System.Windows.Forms;
using SIMS_WinFormsApp.Models.DTOs.Promotions;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Views.Sales
{
    public partial class PromotionDetailsDialog : Form
    {
        public PromotionDetailsDialog()
        {
            InitializeComponent();
            ApplyThemeColors();
        }

        public PromotionDetailsDialog(PromotionDto promotion)
            : this()
        {
            if (promotion == null)
                throw new ArgumentNullException(nameof(promotion));

            _codeValue.Text = promotion.Code;
            _nameValue.Text = promotion.Name;
            _typeValue.Text = promotion.DiscountType == "PERCENT" ? "Phần trăm" : "Số tiền";
            _discountValue.Text = promotion.DiscountType == "PERCENT"
                ? promotion.DiscountValue.ToString("0") + "%"
                : promotion.DiscountValue.ToString("N0") + " đ";
            _maximumValue.Text = promotion.MaxDiscountAmount.HasValue
                ? promotion.MaxDiscountAmount.Value.ToString("N0") + " đ"
                : "Không giới hạn";
            _minimumValue.Text = promotion.MinOrderAmount.ToString("N0") + " đ";
            _startValue.Text = promotion.StartDate.ToString("dd/MM/yyyy");
            _endValue.Text = promotion.EndDate.ToString("dd/MM/yyyy");
            _limitValue.Text = promotion.UsageLimit.HasValue
                ? promotion.UsedCount + " / " + promotion.UsageLimit.Value
                : promotion.UsedCount + " / Không giới hạn";
            _statusValue.Text = promotion.IsDeleted ? "Đã xóa" : promotion.IsActive ? "Đang áp dụng" : "Đã tắt";
            _statusValue.BackColor = promotion.IsDeleted
                ? AppColors.ErrorBg
                : promotion.IsActive ? AppColors.SuccessBg : AppColors.CancelBg;
            _statusValue.ForeColor = promotion.IsDeleted
                ? AppColors.Error
                : promotion.IsActive ? AppColors.Success : AppColors.TextMuted;
            _bannerValue.Text = promotion.ShowOnBanner ? "Có" : "Không";
        }

        private void ApplyThemeColors()
        {
            BackColor = AppColors.PageBg;
            _layout.BackColor = AppColors.White;
            _title.ForeColor = AppColors.TextTitle;
            _heroPanel.BackColor = AppColors.AccentBgSoft;
            _codeCaption.ForeColor = AppColors.TextMuted;
            _codeValue.ForeColor = AppColors.Accent;
            _nameValue.ForeColor = AppColors.TextSecondary;
            _discountCaption.ForeColor = AppColors.TextMuted;
            _discountValue.ForeColor = AppColors.Success;
            _offerCard.BackColor = AppColors.White;
            _usageCard.BackColor = AppColors.White;
            _offerCardTitle.ForeColor = AppColors.TextTitle;
            _usageCardTitle.ForeColor = AppColors.TextTitle;

            Label[] labels = { _typeLabel, _maximumLabel, _minimumLabel, _startLabel, _endLabel, _limitLabel, _bannerLabel };
            foreach (Label label in labels)
                label.ForeColor = AppColors.TextMuted;

            Label[] values = { _typeValue, _maximumValue, _minimumValue, _startValue, _endValue, _limitValue, _bannerValue };
            foreach (Label value in values)
                value.ForeColor = AppColors.TextPrimary;

            _closeButton.BackColor = AppColors.CancelBg;
            _closeButton.ForeColor = AppColors.TextPrimary;
        }

        private void _layout_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
