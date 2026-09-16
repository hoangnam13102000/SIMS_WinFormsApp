using System;
using System.Collections.Generic;
using System.Linq;
using SIMS_WinFormsApp.Models.DTOs.Pos;
using SIMS_WinFormsApp.Services.Interfaces.Pos;

namespace SIMS_WinFormsApp.Services.Implementations.Pos
{
    /// <summary>
    /// Cài đặt DEMO cho <see cref="ICustomerLookupService"/> - vài khách hàng mẫu trong bộ
    /// nhớ. Thay bằng cài đặt đọc bảng Customer thật khi có (cùng cách swap như
    /// DemoProductCatalogService, xem ghi chú ở đó).
    /// </summary>
    public sealed class DemoCustomerLookupService : ICustomerLookupService
    {
        private readonly List<CustomerLookupDto> _customers = new List<CustomerLookupDto>
        {
            new CustomerLookupDto { CustomerId = 1, FullName = "Nguyễn Văn An", Phone = "0901234567", LoyaltyPoints = 120 },
            new CustomerLookupDto { CustomerId = 2, FullName = "Trần Thị Bích", Phone = "0912345678", LoyaltyPoints = 45 },
            new CustomerLookupDto { CustomerId = 3, FullName = "Lê Hoàng Nam", Phone = "0987654321", LoyaltyPoints = 300 },
        };

        public CustomerLookupDto FindByPhoneOrCode(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return null;
            string k = keyword.Trim();
            return _customers.FirstOrDefault(c =>
                string.Equals(c.Phone, k, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(c.CustomerId.ToString(), k, StringComparison.OrdinalIgnoreCase));
        }

        public IReadOnlyList<CustomerLookupDto> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return _customers;
            string k = keyword.Trim();
            return _customers.Where(c =>
                c.FullName.IndexOf(k, StringComparison.OrdinalIgnoreCase) >= 0 ||
                (c.Phone ?? string.Empty).IndexOf(k, StringComparison.OrdinalIgnoreCase) >= 0
            ).ToList();
        }
    }
}