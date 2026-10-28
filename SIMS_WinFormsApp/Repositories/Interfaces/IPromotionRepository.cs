using System.Collections.Generic;
using SIMS_WinFormsApp.Models.DTOs.Promotions;

namespace SIMS_WinFormsApp.Repositories.Interfaces
{
    public interface IPromotionRepository
    {
        IReadOnlyList<PromotionDto> GetAvailableForPos();
        IReadOnlyList<PromotionDto> GetAll(string keyword, bool includeDeleted);
        PromotionPage GetPage(string keyword, bool includeDeleted, int pageIndex, int pageSize);
        PromotionDto GetById(int promotionId);
        PromotionDto GetByCode(string code);
        int Insert(PromotionEditDto dto);
        void Update(PromotionEditDto dto);
        void SetActive(int promotionId, bool active);
        void SoftDelete(int promotionId);
    }
}
