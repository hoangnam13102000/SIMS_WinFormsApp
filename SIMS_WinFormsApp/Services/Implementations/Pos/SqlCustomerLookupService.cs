using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

            const string sql = @"
                SELECT TOP 1 c.CustomerID, u.FullName, u.Phone, c.MemberPoint
                FROM Customers c
                INNER JOIN Users u ON u.UserID = c.CustomerID
                WHERE u.Status = 'ACTIVE'
                  AND u.IsDeleted = 0
                  AND (u.Phone = @Keyword OR c.CustomerCode = @Keyword)
                ORDER BY c.CustomerID";

            using (SqlConnection connection = _connectionFactory.CreateConnection())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@Keyword", SqlDbType.NVarChar, 100).Value = keyword.Trim();
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    return reader.Read() ? MapCustomer(reader) : null;
                }
            }
        }

        public IReadOnlyList<CustomerLookupDto> Search(string keyword)
        {
            const string sql = @"
                SELECT TOP 50 c.CustomerID, u.FullName, u.Phone, c.MemberPoint
                FROM Customers c
                INNER JOIN Users u ON u.UserID = c.CustomerID
                WHERE u.Status = 'ACTIVE'
                  AND u.IsDeleted = 0
                  AND (@Keyword = N''
                       OR u.FullName LIKE @Pattern
                       OR u.Phone LIKE @Pattern
                       OR c.CustomerCode LIKE @Pattern)
                ORDER BY u.FullName";

            string normalizedKeyword = (keyword ?? string.Empty).Trim();
            var customers = new List<CustomerLookupDto>();
            using (SqlConnection connection = _connectionFactory.CreateConnection())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@Keyword", SqlDbType.NVarChar, 100).Value = normalizedKeyword;
                command.Parameters.Add("@Pattern", SqlDbType.NVarChar, 102).Value = "%" + normalizedKeyword + "%";
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        customers.Add(MapCustomer(reader));
                }
            }

            return customers;
        }

        private static CustomerLookupDto MapCustomer(SqlDataReader reader)
        {
            return new CustomerLookupDto
            {
                CustomerId = reader.GetInt32(0),
                FullName = reader.GetString(1),
                Phone = reader.IsDBNull(2) ? null : reader.GetString(2),
                LoyaltyPoints = reader.GetInt32(3)
            };
        }
    }
}
