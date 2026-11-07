using System.Collections.Generic;
using SIMS_WinFormsApp.Models.DTOs.Pos;

namespace SIMS_WinFormsApp.Services.Interfaces.Pos
{
    public interface IInvoiceService
    {
        string CreateInvoice(
            int createdBy,
            int? customerId,
            string paymentMethod,
            CartTotalsDto totals,
            IReadOnlyList<CartLineDto> lines);
    }
}
