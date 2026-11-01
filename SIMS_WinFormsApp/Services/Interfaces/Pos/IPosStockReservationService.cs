using System.Collections.Generic;
using SIMS_WinFormsApp.Models.DTOs.Pos;

namespace SIMS_WinFormsApp.Services.Interfaces.Pos
{
    public interface IPosStockReservationService
    {
        IReadOnlyList<HeldCartDto> GetPendingReservations(int createdBy);

        int ReserveStock(
            int? reservationId,
            int productId,
            string productName,
            decimal unitPrice,
            int quantity,
            int createdBy,
            string customerLabel);

        void ReleaseReservedStock(int reservationId, int productId, int quantity);
    }
}
