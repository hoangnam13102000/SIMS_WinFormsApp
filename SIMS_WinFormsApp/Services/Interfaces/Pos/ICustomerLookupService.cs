using System.Collections.Generic;
using SIMS_WinFormsApp.Models.DTOs.Pos;

namespace SIMS_WinFormsApp.Services.Interfaces.Pos
{
    public interface ICustomerLookupService
    {
        /// <summary>Tìm khách theo số điện thoại, mã khách hàng hoặc tên khi bấm nút tìm.</summary>
        CustomerLookupDto FindCustomer(string keyword);

        /// <summary>Tìm gần đúng theo tên/số điện thoại - dùng cho popup "chọn khách hàng".</summary>
        IReadOnlyList<CustomerLookupDto> Search(string keyword);
    }
}