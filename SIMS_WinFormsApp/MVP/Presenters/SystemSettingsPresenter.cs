using System;
using System.Globalization;
using System.Windows.Forms;
using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.Repositories.Interfaces;
using SIMS_WinFormsApp.UI.Controls.Loading;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.MVP.Presenters
{
    public sealed class SystemSettingsPresenter
    {
        private readonly ISystemSettingsView _view;
        private readonly IStoreConfigRepository _repository;
        private readonly ILoadingIndicator _loadingIndicator;

        public SystemSettingsPresenter(
            ISystemSettingsView view,
            IStoreConfigRepository repository,
            ILoadingIndicator loadingIndicator = null)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _loadingIndicator = loadingIndicator;

            _view.SaveRequested += (s, e) => Save();
        }

        public void Load()
        {
            _loadingIndicator?.ShowLoading("Đang tải cấu hình hệ thống...");
            Application.DoEvents();
            try
            {
                var settings = _repository.GetSettings();
                _view.StoreName = settings.StoreName;
                _view.DefaultUnit = settings.DefaultUnit;
                _view.VatRateText = settings.VatRate.ToString(CultureInfo.InvariantCulture);
                _view.DefaultMarginText = settings.DefaultMargin.ToString(CultureInfo.InvariantCulture);
                _view.ReturnPolicyDaysText = settings.ReturnPolicyDays.ToString(CultureInfo.InvariantCulture);
                _view.ApprovalThresholdText = settings.ApprovalThreshold.ToString(CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                _view.ShowError("Không thể tải cấu hình hệ thống: " + ex.Message);
            }
            finally
            {
                _loadingIndicator?.HideLoading();
            }
        }

        private void Save()
        {
            string storeName = _view.StoreName?.Trim();
            if (string.IsNullOrWhiteSpace(storeName))
            {
                _view.ShowError("Vui lòng nhập tên cửa hàng.");
                return;
            }

            if (!TryParseNonNegativeDecimal(_view.VatRateText, out var vatRate) || vatRate > 100)
            {
                _view.ShowError("Thuế GTGT phải là số từ 0 đến 100.");
                return;
            }
            if (!TryParseNonNegativeDecimal(_view.DefaultMarginText, out var margin))
            {
                _view.ShowError("Chênh lệch giá bán phải là số không âm.");
                return;
            }
            if (!int.TryParse(_view.ReturnPolicyDaysText, out var returnDays) || returnDays < 0)
            {
                _view.ShowError("Số ngày đổi/trả phải là số nguyên không âm.");
                return;
            }
            if (!TryParseNonNegativeDecimal(_view.ApprovalThresholdText, out var threshold))
            {
                _view.ShowError("Ngưỡng cần duyệt phải là số không âm.");
                return;
            }

            var settings = new StoreSettingsDto
            {
                StoreName = storeName,
                DefaultUnit = _view.DefaultUnit?.Trim(),
                VatRate = vatRate,
                DefaultMargin = margin,
                ReturnPolicyDays = returnDays,
                ApprovalThreshold = threshold
            };

            _view.SetSaving(true);
            try
            {
                _repository.SaveSettings(settings);
                _view.ShowSuccess("Đã lưu cài đặt hệ thống thành công.");
            }
            catch (Exception ex)
            {
                _view.ShowError("Không thể lưu cài đặt hệ thống: " + ex.Message);
            }
            finally
            {
                _view.SetSaving(false);
            }
        }

        private static bool TryParseNonNegativeDecimal(string text, out decimal value) =>
            decimal.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out value) && value >= 0;
    }
}