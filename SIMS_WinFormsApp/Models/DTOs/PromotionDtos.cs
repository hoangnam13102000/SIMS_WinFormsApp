using System;
using System.Collections.Generic;

namespace SIMS_WinFormsApp.Models.DTOs.Promotions
{
    public sealed class PromotionDto
    {
        public int PromotionId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string DiscountType { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal? MaxDiscountAmount { get; set; }
        public decimal MinOrderAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? UsageLimit { get; set; }
        public int UsedCount { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool ShowOnBanner { get; set; }
    }

    public sealed class PromotionEditDto
    {
        public int PromotionId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string DiscountType { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal? MaxDiscountAmount { get; set; }
        public decimal MinOrderAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? UsageLimit { get; set; }
        public bool IsActive { get; set; }
        public bool ShowOnBanner { get; set; }
        public int? CreatedBy { get; set; }
    }

    public sealed class PromotionSaveResult
    {
        public bool IsSuccess { get; private set; }
        public int PromotionId { get; private set; }
        public string Message { get; private set; }

        public static PromotionSaveResult Ok(int id, string message) =>
            new PromotionSaveResult { IsSuccess = true, PromotionId = id, Message = message };

        public static PromotionSaveResult Fail(string message) =>
            new PromotionSaveResult { IsSuccess = false, Message = message };
    }

    public sealed class PromotionPage
    {
        public IReadOnlyList<PromotionDto> Rows { get; set; }
        public int TotalCount { get; set; }
    }
}
