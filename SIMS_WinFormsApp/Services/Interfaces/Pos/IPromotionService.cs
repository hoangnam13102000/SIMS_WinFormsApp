using SIMS_WinFormsApp.Models.DTOs.Pos;

namespace SIMS_WinFormsApp.Services.Interfaces.Pos
{
    public interface IPromotionService
    {
        PromotionResultDto Apply(string code, decimal subtotal);
    }
}