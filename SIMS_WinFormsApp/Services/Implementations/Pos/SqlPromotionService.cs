using System;
using System.Collections.Generic;
using SIMS_WinFormsApp.Models.DTOs.Pos;
using SIMS_WinFormsApp.Models.DTOs.Promotions;
using SIMS_WinFormsApp.Repositories.Interfaces;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.Services.Interfaces.Pos;

namespace SIMS_WinFormsApp.Services.Implementations.Pos
{
    public sealed class SqlPromotionService : IPromotionService, IPromotionManagementService
    {
        private readonly IPromotionRepository _repository;

        public SqlPromotionService(IPromotionRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public PromotionResultDto Apply(string code, decimal subtotal)
        {
            if (string.IsNullOrWhiteSpace(code)) return PromotionResultDto.Failed("Vui lòng nhập mã khuyến mãi.");
            if (subtotal <= 0) return PromotionResultDto.Failed("Giỏ hàng đang trống, chưa có gì để áp mã.");
            PromotionDto promotion = _repository.GetByCode(code);
            string error;
            if (!IsUsable(promotion, subtotal, out error)) return PromotionResultDto.Failed(error);
            decimal discount = promotion.DiscountType == "PERCENT"
                ? Math.Round(subtotal * promotion.DiscountValue / 100m, 0, MidpointRounding.AwayFromZero)
                : promotion.DiscountValue;
            if (promotion.MaxDiscountAmount.HasValue) discount = Math.Min(discount, promotion.MaxDiscountAmount.Value);
            discount = Math.Min(Math.Max(0m, discount), subtotal);
            return PromotionResultDto.Applied(discount, "Đã áp dụng mã " + promotion.Code + ".", promotion.PromotionId, promotion.Code);
        }

        public IReadOnlyList<PromotionDto> GetAvailablePromotions() => _repository.GetAvailableForPos();
        public IReadOnlyList<PromotionDto> GetAll(string keyword, bool includeDeleted) => _repository.GetAll(keyword, includeDeleted);
        public PromotionPage GetPage(string keyword, bool includeDeleted, int pageIndex, int pageSize) =>
            _repository.GetPage(keyword, includeDeleted, pageIndex, pageSize);
        public PromotionDto GetById(int promotionId) => _repository.GetById(promotionId);

        public PromotionSaveResult Save(PromotionEditDto dto)
        {
            string error = Validate(dto);
            if (error != null) return PromotionSaveResult.Fail(error);
            PromotionDto sameCode = _repository.GetByCode(dto.Code);
            if (sameCode != null && sameCode.PromotionId != dto.PromotionId)
                return PromotionSaveResult.Fail("Mã giảm giá đã tồn tại.");
            try
            {
                if (dto.PromotionId <= 0) return PromotionSaveResult.Ok(_repository.Insert(dto), "Đã thêm mã giảm giá.");
                _repository.Update(dto);
                return PromotionSaveResult.Ok(dto.PromotionId, "Đã cập nhật mã giảm giá.");
            }
            catch (Exception ex) { return PromotionSaveResult.Fail(ex.Message); }
        }

        public PromotionSaveResult SetActive(int promotionId, bool active)
        {
            try { _repository.SetActive(promotionId, active); return PromotionSaveResult.Ok(promotionId, active ? "Đã bật mã giảm giá." : "Đã tắt mã giảm giá."); }
            catch (Exception ex) { return PromotionSaveResult.Fail(ex.Message); }
        }

        public PromotionSaveResult Delete(int promotionId)
        {
            try { _repository.SoftDelete(promotionId); return PromotionSaveResult.Ok(promotionId, "Đã xóa mã giảm giá."); }
            catch (Exception ex) { return PromotionSaveResult.Fail(ex.Message); }
        }

        private static string Validate(PromotionEditDto dto)
        {
            if (dto == null) return "Dữ liệu mã giảm giá không hợp lệ.";
            if (string.IsNullOrWhiteSpace(dto.Code) || dto.Code.Trim().Length > 30) return "Mã giảm giá bắt buộc và tối đa 30 ký tự.";
            if (string.IsNullOrWhiteSpace(dto.Name) || dto.Name.Trim().Length > 150) return "Tên chương trình bắt buộc và tối đa 150 ký tự.";
            if (dto.DiscountType != "PERCENT" && dto.DiscountType != "AMOUNT") return "Loại giảm giá không hợp lệ.";
            if (dto.DiscountValue <= 0 || (dto.DiscountType == "PERCENT" && dto.DiscountValue > 100)) return "Giá trị giảm giá không hợp lệ.";
            if (dto.MinOrderAmount < 0 || (dto.MaxDiscountAmount.HasValue && dto.MaxDiscountAmount.Value < 0)) return "Mức tiền không hợp lệ.";
            if (dto.EndDate.Date < dto.StartDate.Date) return "Ngày kết thúc phải sau hoặc bằng ngày bắt đầu.";
            if (dto.UsageLimit.HasValue && dto.UsageLimit.Value <= 0) return "Giới hạn lượt dùng phải lớn hơn 0.";
            return null;
        }

        private static bool IsUsable(PromotionDto promotion, decimal subtotal, out string error)
        {
            error = null;
            if (promotion == null) { error = "Mã khuyến mãi không tồn tại."; return false; }
            DateTime today = DateTime.Today;
            if (promotion.IsDeleted || !promotion.IsActive) { error = "Mã khuyến mãi đã bị tắt."; return false; }
            if (today < promotion.StartDate.Date || today > promotion.EndDate.Date) { error = "Mã khuyến mãi đã hết hạn hoặc chưa bắt đầu."; return false; }
            if (subtotal < promotion.MinOrderAmount) { error = "Đơn hàng chưa đạt giá trị tối thiểu của mã."; return false; }
            if (promotion.UsageLimit.HasValue && promotion.UsedCount >= promotion.UsageLimit.Value) { error = "Mã khuyến mãi đã hết lượt sử dụng."; return false; }
            return true;
        }
    }
}
