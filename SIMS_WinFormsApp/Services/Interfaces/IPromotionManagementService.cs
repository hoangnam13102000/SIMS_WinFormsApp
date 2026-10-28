using System.Collections.Generic;
using SIMS_WinFormsApp.Models.DTOs.Promotions;

namespace SIMS_WinFormsApp.Services.Interfaces
{
    public interface IPromotionManagementService
    {
        IReadOnlyList<PromotionDto> GetAll(string keyword, bool includeDeleted);
        PromotionPage GetPage(string keyword, bool includeDeleted, int pageIndex, int pageSize);
        PromotionDto GetById(int promotionId);
        PromotionSaveResult Save(PromotionEditDto dto);
        PromotionSaveResult SetActive(int promotionId, bool active);
        PromotionSaveResult Delete(int promotionId);
    }
}
