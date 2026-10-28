using System.Collections.Generic;
using SIMS_WinFormsApp.Models.DTOs.Pos;
using SIMS_WinFormsApp.Models.DTOs.Promotions;

namespace SIMS_WinFormsApp.Services.Interfaces.Pos
{
    public interface IPromotionService
    {
        IReadOnlyList<PromotionDto> GetAvailablePromotions();
        PromotionResultDto Apply(string code, decimal subtotal);
    }
}