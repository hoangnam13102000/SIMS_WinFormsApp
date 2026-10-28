using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SIMS_WinFormsApp.Infrastructure.Configuration;
using SIMS_WinFormsApp.Models.DTOs.Promotions;
using SIMS_WinFormsApp.Repositories.Interfaces;

namespace SIMS_WinFormsApp.Repositories.Implementations
{
    public sealed class PromotionRepository : IPromotionRepository
    {
        private readonly IDbConnectionFactory _connections;

        public PromotionRepository(IDbConnectionFactory connections)
        {
            _connections = connections ?? throw new ArgumentNullException(nameof(connections));
        }

        public IReadOnlyList<PromotionDto> GetAvailableForPos()
        {
            const string sql = @"
                SELECT PromotionID, Code, Name, DiscountType, DiscountValue, MaxDiscountAmount,
                       MinOrderAmount, StartDate, EndDate, UsageLimit, UsedCount, IsActive,
                       IsDeleted, ShowOnBanner
                FROM Promotions
                WHERE IsActive = 1
                  AND IsDeleted = 0
                  AND StartDate <= CAST(GETDATE() AS DATE)
                  AND EndDate >= CAST(GETDATE() AS DATE)
                  AND (UsageLimit IS NULL OR UsedCount < UsageLimit)
                ORDER BY Code;";
            var rows = new List<PromotionDto>();
            using (var connection = Open())
            using (var command = new SqlCommand(sql, connection))
            using (var reader = command.ExecuteReader())
                while (reader.Read()) rows.Add(Read(reader));
            return rows;
        }

        public IReadOnlyList<PromotionDto> GetAll(string keyword, bool includeDeleted)
        {
            const string sql = @"
                SELECT PromotionID, Code, Name, DiscountType, DiscountValue, MaxDiscountAmount,
                       MinOrderAmount, StartDate, EndDate, UsageLimit, UsedCount, IsActive,
                       IsDeleted, ShowOnBanner
                FROM Promotions
                WHERE (@IncludeDeleted = 1 OR IsDeleted = 0)
                  AND (@Keyword = N'' OR Code LIKE @Pattern OR Name LIKE @Pattern)
                ORDER BY IsDeleted, StartDate DESC, Code;";
            var rows = new List<PromotionDto>();
            using (var connection = Open())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@IncludeDeleted", SqlDbType.Bit).Value = includeDeleted;
                command.Parameters.Add("@Keyword", SqlDbType.NVarChar, 150).Value = keyword ?? string.Empty;
                command.Parameters.Add("@Pattern", SqlDbType.NVarChar, 160).Value = "%" + (keyword ?? string.Empty) + "%";
                using (var reader = command.ExecuteReader())
                    while (reader.Read()) rows.Add(Read(reader));
            }
            return rows;
        }

        public PromotionPage GetPage(string keyword, bool includeDeleted, int pageIndex, int pageSize)
        {
            if (pageIndex < 0) throw new ArgumentOutOfRangeException(nameof(pageIndex));
            if (pageSize <= 0) throw new ArgumentOutOfRangeException(nameof(pageSize));

            const string filter = @"
                FROM Promotions
                WHERE (@IncludeDeleted = 1 OR IsDeleted = 0)
                  AND (@Keyword = N'' OR Code LIKE @Pattern OR Name LIKE @Pattern)";
            const string rowsSql = @"
                SELECT PromotionID, Code, Name, DiscountType, DiscountValue, MaxDiscountAmount,
                       MinOrderAmount, StartDate, EndDate, UsageLimit, UsedCount, IsActive,
                       IsDeleted, ShowOnBanner " + filter + @"
                ORDER BY IsDeleted, StartDate DESC, Code
                OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;";
            const string countSql = "SELECT COUNT(*) " + filter + ";";

            var result = new PromotionPage { Rows = new List<PromotionDto>() };
            using (var connection = Open())
            using (var command = new SqlCommand(countSql, connection))
            {
                AddSearchParameters(command, keyword, includeDeleted);
                result.TotalCount = Convert.ToInt32(command.ExecuteScalar());
            }

            using (var connection = Open())
            using (var command = new SqlCommand(rowsSql, connection))
            {
                AddSearchParameters(command, keyword, includeDeleted);
                command.Parameters.Add("@Offset", SqlDbType.Int).Value = checked(pageIndex * pageSize);
                command.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;
                var rows = new List<PromotionDto>();
                using (var reader = command.ExecuteReader())
                    while (reader.Read()) rows.Add(Read(reader));
                result.Rows = rows;
            }

            return result;
        }

        public PromotionDto GetById(int promotionId)
        {
            return QueryOne("WHERE PromotionID = @PromotionID", command =>
                command.Parameters.Add("@PromotionID", SqlDbType.Int).Value = promotionId);
        }

        public PromotionDto GetByCode(string code)
        {
            return QueryOne("WHERE Code = @Code", command =>
                command.Parameters.Add("@Code", SqlDbType.VarChar, 30).Value = (code ?? string.Empty).Trim().ToUpperInvariant());
        }

        public int Insert(PromotionEditDto dto)
        {
            const string sql = @"
                INSERT INTO Promotions
                    (Code, Name, DiscountType, DiscountValue, MaxDiscountAmount, MinOrderAmount,
                     StartDate, EndDate, UsageLimit, IsActive, ShowOnBanner, CreatedBy)
                VALUES
                    (@Code, @Name, @DiscountType, @DiscountValue, @MaxDiscountAmount, @MinOrderAmount,
                     @StartDate, @EndDate, @UsageLimit, @IsActive, @ShowOnBanner, @CreatedBy);
                SELECT CAST(SCOPE_IDENTITY() AS INT);";
            using (var connection = Open())
            using (var command = new SqlCommand(sql, connection))
            {
                Bind(command, dto);
                return (int)command.ExecuteScalar();
            }
        }

        public void Update(PromotionEditDto dto)
        {
            const string sql = @"
                UPDATE Promotions SET Code = @Code, Name = @Name, DiscountType = @DiscountType,
                    DiscountValue = @DiscountValue, MaxDiscountAmount = @MaxDiscountAmount,
                    MinOrderAmount = @MinOrderAmount, StartDate = @StartDate, EndDate = @EndDate,
                    UsageLimit = @UsageLimit, IsActive = @IsActive, ShowOnBanner = @ShowOnBanner
                WHERE PromotionID = @PromotionID AND IsDeleted = 0;";
            using (var connection = Open())
            using (var command = new SqlCommand(sql, connection))
            {
                Bind(command, dto);
                command.Parameters.Add("@PromotionID", SqlDbType.Int).Value = dto.PromotionId;
                command.ExecuteNonQuery();
            }
        }

        public void SetActive(int promotionId, bool active)
        {
            const string sql = "UPDATE Promotions SET IsActive = @Active WHERE PromotionID = @PromotionID AND IsDeleted = 0;";
            ExecuteTwoParameters(sql, promotionId, active);
        }

        public void SoftDelete(int promotionId)
        {
            const string sql = "UPDATE Promotions SET IsDeleted = 1, IsActive = 0, DeletedAt = GETDATE() WHERE PromotionID = @PromotionID;";
            using (var connection = Open())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@PromotionID", SqlDbType.Int).Value = promotionId;
                command.ExecuteNonQuery();
            }
        }

        private PromotionDto QueryOne(string where, Action<SqlCommand> bind)
        {
            const string select = @"SELECT PromotionID, Code, Name, DiscountType, DiscountValue, MaxDiscountAmount,
                       MinOrderAmount, StartDate, EndDate, UsageLimit, UsedCount, IsActive, IsDeleted, ShowOnBanner
                FROM Promotions ";
            using (var connection = Open())
            using (var command = new SqlCommand(select + where, connection))
            {
                bind(command);
                using (var reader = command.ExecuteReader())
                    return reader.Read() ? Read(reader) : null;
            }
        }

        private static void AddSearchParameters(SqlCommand command, string keyword, bool includeDeleted)
        {
            command.Parameters.Add("@IncludeDeleted", SqlDbType.Bit).Value = includeDeleted;
            command.Parameters.Add("@Keyword", SqlDbType.NVarChar, 150).Value = keyword ?? string.Empty;
            command.Parameters.Add("@Pattern", SqlDbType.NVarChar, 160).Value = "%" + (keyword ?? string.Empty) + "%";
        }

        private void ExecuteTwoParameters(string sql, int promotionId, bool active)
        {
            using (var connection = Open())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@Active", SqlDbType.Bit).Value = active;
                command.Parameters.Add("@PromotionID", SqlDbType.Int).Value = promotionId;
                command.ExecuteNonQuery();
            }
        }

        private static void Bind(SqlCommand command, PromotionEditDto dto)
        {
            command.Parameters.Add("@Code", SqlDbType.VarChar, 30).Value = dto.Code.Trim().ToUpperInvariant();
            command.Parameters.Add("@Name", SqlDbType.NVarChar, 150).Value = dto.Name.Trim();
            command.Parameters.Add("@DiscountType", SqlDbType.VarChar, 10).Value = dto.DiscountType;
            AddMoney(command, "@DiscountValue", dto.DiscountValue);
            AddNullableMoney(command, "@MaxDiscountAmount", dto.MaxDiscountAmount);
            AddMoney(command, "@MinOrderAmount", dto.MinOrderAmount);
            command.Parameters.Add("@StartDate", SqlDbType.Date).Value = dto.StartDate.Date;
            command.Parameters.Add("@EndDate", SqlDbType.Date).Value = dto.EndDate.Date;
            command.Parameters.Add("@UsageLimit", SqlDbType.Int).Value = dto.UsageLimit.HasValue ? (object)dto.UsageLimit.Value : DBNull.Value;
            command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = dto.IsActive;
            command.Parameters.Add("@ShowOnBanner", SqlDbType.Bit).Value = dto.ShowOnBanner;
            command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = dto.CreatedBy.HasValue ? (object)dto.CreatedBy.Value : DBNull.Value;
        }

        private static PromotionDto Read(SqlDataReader reader)
        {
            return new PromotionDto
            {
                PromotionId = reader.GetInt32(0), Code = reader.GetString(1), Name = reader.GetString(2),
                DiscountType = reader.GetString(3), DiscountValue = reader.GetDecimal(4),
                MaxDiscountAmount = reader.IsDBNull(5) ? (decimal?)null : reader.GetDecimal(5),
                MinOrderAmount = reader.GetDecimal(6), StartDate = reader.GetDateTime(7), EndDate = reader.GetDateTime(8),
                UsageLimit = reader.IsDBNull(9) ? (int?)null : reader.GetInt32(9), UsedCount = reader.GetInt32(10),
                IsActive = reader.GetBoolean(11), IsDeleted = reader.GetBoolean(12), ShowOnBanner = reader.GetBoolean(13)
            };
        }

        private SqlConnection Open()
        {
            SqlConnection connection = _connections.CreateConnection();
            connection.Open();
            return connection;
        }

        private static void AddMoney(SqlCommand command, string name, decimal value)
        {
            SqlParameter parameter = command.Parameters.Add(name, SqlDbType.Decimal);
            parameter.Precision = 18; parameter.Scale = 0; parameter.Value = value;
        }

        private static void AddNullableMoney(SqlCommand command, string name, decimal? value)
        {
            SqlParameter parameter = command.Parameters.Add(name, SqlDbType.Decimal);
            parameter.Precision = 18; parameter.Scale = 0; parameter.Value = value.HasValue ? (object)value.Value : DBNull.Value;
        }
    }
}
