using System;
using System.Collections.Generic;
using System.Linq;
using SIMS_WinFormsApp.DAL.Linq.Entities.Business;
using SIMS_WinFormsApp.Infrastructure.Configuration;
using SIMS_WinFormsApp.Models.DTOs.Pos;
using SIMS_WinFormsApp.Services.Interfaces.Pos;

namespace SIMS_WinFormsApp.Services.Implementations.Pos
{
    public sealed class SqlCustomerLookupService : ICustomerLookupService
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public SqlCustomerLookupService(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public CustomerLookupDto FindByPhoneOrCode(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return null;

            string normalizedKeyword = keyword.Trim();
            using (var db = _connectionFactory.CreateDataContext())
            {
                var row = (from customer in db.Customers
                           join user in db.Users on customer.CustomerID equals user.UserID
                           where user.Status == "ACTIVE" && !user.IsDeleted
                               && (user.Phone == normalizedKeyword || customer.CustomerCode == normalizedKeyword)
                           orderby customer.CustomerID
                           select new { customer.CustomerID, user.FullName, user.Phone, customer.MemberPoint })
                           .FirstOrDefault();
                return row == null ? null : MapCustomer(row.CustomerID, row.FullName, row.Phone, row.MemberPoint);
            }
        }

        public IReadOnlyList<CustomerLookupDto> Search(string keyword)
        {
            string normalizedKeyword = (keyword ?? string.Empty).Trim();
            using (var db = _connectionFactory.CreateDataContext())
            {
                var rows = (from customer in db.Customers
                            join user in db.Users on customer.CustomerID equals user.UserID
                            where user.Status == "ACTIVE" && !user.IsDeleted
                                && (normalizedKeyword == string.Empty
                                    || user.FullName.Contains(normalizedKeyword)
                                    || user.Phone.Contains(normalizedKeyword)
                                    || customer.CustomerCode.Contains(normalizedKeyword))
                            orderby user.FullName
                            select new { customer.CustomerID, user.FullName, user.Phone, customer.MemberPoint })
                            .Take(50).ToList();
                return rows.Select(row => MapCustomer(row.CustomerID, row.FullName, row.Phone, row.MemberPoint)).ToList();
            }
        }

        private static CustomerLookupDto MapCustomer(int id, string name, string phone, int points)
        {
            return new CustomerLookupDto
            {
                CustomerId = id,
                FullName = name,
                Phone = phone,
                LoyaltyPoints = points
            };
        }
    }
}
