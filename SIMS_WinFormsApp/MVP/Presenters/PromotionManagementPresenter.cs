using System;
using System.Globalization;
using SIMS_WinFormsApp.Models.DTOs.Promotions;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.MVP.Presenters
{
    public sealed class PromotionManagementPresenter : IDisposable
    {
        private readonly IPromotionManagementView _view;
        private readonly IPromotionManagementService _service;

        public PromotionManagementPresenter(
            IPromotionManagementView view,
            IPromotionManagementService service)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _service = service ?? throw new ArgumentNullException(nameof(service));

            _view.ReloadRequested += OnReloadRequested;
            _view.NewPromotionRequested += OnNewPromotionRequested;
            _view.ViewPromotionRequested += OnViewPromotionRequested;
            _view.EditPromotionRequested += OnEditPromotionRequested;
            _view.ToggleActiveRequested += OnToggleActiveRequested;
            _view.DeletePromotionRequested += OnDeletePromotionRequested;
            _view.CancelEditorRequested += OnCancelEditorRequested;
            _view.SaveRequested += OnSaveRequested;
        }

        private void OnReloadRequested(object sender, EventArgs e)
        {
            try
            {
                PromotionPage page = _service.GetPage(
                    _view.Keyword,
                    _view.IncludeDeleted,
                    _view.PageIndex,
                    _view.PageSize);
                if (page == null)
                {
                    _view.ShowError("Không thể tải danh sách mã giảm giá.");
                    return;
                }

                if (_view.ShowItems(page))
                    OnReloadRequested(this, EventArgs.Empty);
            }
            catch (Exception exception)
            {
                _view.ShowError(exception.Message);
            }
        }

        private void OnNewPromotionRequested(object sender, EventArgs e)
        {
            _view.ShowEditor(null);
        }

        private void OnCancelEditorRequested(object sender, EventArgs e)
        {
            _view.CloseEditor();
        }

        private void OnViewPromotionRequested(object sender, EventArgs e)
        {
            PromotionDto promotion = GetSelectedPromotion("Chọn mã giảm giá cần xem.");
            if (promotion != null)
                _view.ShowDetails(promotion);
        }

        private void OnEditPromotionRequested(object sender, EventArgs e)
        {
            PromotionDto promotion = GetSelectedPromotion("Chọn mã giảm giá cần sửa.");
            if (promotion != null)
                _view.ShowEditor(promotion);
        }

        private PromotionDto GetSelectedPromotion(string selectionError)
        {
            if (_view.SelectedPromotionId <= 0)
            {
                _view.ShowError(selectionError);
                return null;
            }

            try
            {
                PromotionDto promotion = _service.GetById(_view.SelectedPromotionId);
                if (promotion == null)
                    _view.ShowError("Không tìm thấy mã giảm giá đã chọn.");
                return promotion;
            }
            catch (Exception exception)
            {
                _view.ShowError(exception.Message);
                return null;
            }
        }

        private void OnSaveRequested(object sender, PromotionSaveRequestedEventArgs e)
        {
            if (e == null || e.Input == null)
                throw new ArgumentNullException(nameof(e));

            PromotionEditDto dto;
            string validationError;
            if (!TryCreateEditDto(e.Input, out dto, out validationError))
            {
                _view.ShowError(validationError);
                return;
            }

            try
            {
                PromotionSaveResult result = _service.Save(dto);
                if (result == null || !result.IsSuccess)
                {
                    _view.ShowError(result == null
                        ? "Không thể lưu mã giảm giá."
                        : result.Message);
                    return;
                }

                _view.ShowInfo(result.Message);
                OnReloadRequested(this, EventArgs.Empty);
                _view.CloseEditor();
            }
            catch (Exception exception)
            {
                _view.ShowError(exception.Message);
            }
        }

        private static bool TryCreateEditDto(
            PromotionEditorInput input,
            out PromotionEditDto dto,
            out string error)
        {
            dto = null;
            error = null;

            decimal value;
            decimal maximum;
            decimal minimum;
            int limit;
            if (!decimal.TryParse(input.DiscountValue, NumberStyles.Number, CultureInfo.InvariantCulture, out value))
            {
                error = "Giá trị giảm không hợp lệ.";
                return false;
            }

            if (!TryOptionalDecimal(input.MaximumDiscount, out maximum) ||
                !decimal.TryParse(input.MinimumOrder, NumberStyles.Number, CultureInfo.InvariantCulture, out minimum) ||
                !TryOptionalInt(input.UsageLimit, out limit))
            {
                error = "Thông tin tiền hoặc giới hạn lượt không hợp lệ.";
                return false;
            }

            dto = new PromotionEditDto
            {
                PromotionId = input.PromotionId,
                Code = input.Code,
                Name = input.Name,
                DiscountType = input.DiscountType,
                DiscountValue = value,
                MaxDiscountAmount = string.IsNullOrWhiteSpace(input.MaximumDiscount)
                    ? (decimal?)null
                    : maximum,
                MinOrderAmount = minimum,
                StartDate = input.StartDate.Date,
                EndDate = input.EndDate.Date,
                UsageLimit = string.IsNullOrWhiteSpace(input.UsageLimit)
                    ? (int?)null
                    : limit,
                IsActive = input.IsActive,
                ShowOnBanner = input.ShowOnBanner
            };
            return true;
        }

        private static bool TryOptionalDecimal(string text, out decimal value)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                value = 0;
                return true;
            }

            return decimal.TryParse(text, NumberStyles.Number, CultureInfo.InvariantCulture, out value);
        }

        private static bool TryOptionalInt(string text, out int value)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                value = 0;
                return true;
            }

            return int.TryParse(text, NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out value);
        }

        private void OnToggleActiveRequested(object sender, EventArgs e)
        {
            if (_view.SelectedPromotionId <= 0)
            {
                _view.ShowError("Chọn mã cần bật hoặc tắt.");
                return;
            }

            try
            {
                PromotionDto promotion = _service.GetById(_view.SelectedPromotionId);
                if (promotion == null)
                    return;

                PromotionSaveResult result = _service.SetActive(
                    promotion.PromotionId,
                    !promotion.IsActive);
                if (result == null || !result.IsSuccess)
                {
                    _view.ShowError(result == null ? "Không thể cập nhật mã giảm giá." : result.Message);
                    return;
                }

                _view.ShowInfo(result.Message);
                OnReloadRequested(this, EventArgs.Empty);
            }
            catch (Exception exception)
            {
                _view.ShowError(exception.Message);
            }
        }

        private void OnDeletePromotionRequested(object sender, EventArgs e)
        {
            if (_view.SelectedPromotionId <= 0)
            {
                _view.ShowError("Chọn mã cần xóa.");
                return;
            }
            if (!_view.ConfirmDelete())
                return;

            try
            {
                PromotionSaveResult result = _service.Delete(_view.SelectedPromotionId);
                if (result == null || !result.IsSuccess)
                {
                    _view.ShowError(result == null ? "Không thể xóa mã giảm giá." : result.Message);
                    return;
                }

                _view.ShowInfo(result.Message);
                _view.ClearSelection();
                OnReloadRequested(this, EventArgs.Empty);
            }
            catch (Exception exception)
            {
                _view.ShowError(exception.Message);
            }
        }

        public void Dispose()
        {
            _view.ReloadRequested -= OnReloadRequested;
            _view.NewPromotionRequested -= OnNewPromotionRequested;
            _view.ViewPromotionRequested -= OnViewPromotionRequested;
            _view.EditPromotionRequested -= OnEditPromotionRequested;
            _view.ToggleActiveRequested -= OnToggleActiveRequested;
            _view.DeletePromotionRequested -= OnDeletePromotionRequested;
            _view.CancelEditorRequested -= OnCancelEditorRequested;
            _view.SaveRequested -= OnSaveRequested;
        }
    }
}
