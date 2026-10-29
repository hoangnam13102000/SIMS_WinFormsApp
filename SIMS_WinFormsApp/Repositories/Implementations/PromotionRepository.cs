using System;
using System.Collections.Generic;
using System.Linq;
using SIMS_WinFormsApp.DAL.Linq;
using SIMS_WinFormsApp.DAL.Linq.Entities.Business;
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
            using (var db = _connections.CreateDataContext())
            {
                DateTime today = DateTime.Today;
                return db.Promotions.Where(p => p.IsActive && !p.IsDeleted
                        && p.StartDate <= today && p.EndDate >= today
                        && (!p.UsageLimit.HasValue || p.UsedCount < p.UsageLimit.Value))
                    .OrderBy(p => p.Code).ToList().Select(Map).ToList();
            }
        }

        public IReadOnlyList<PromotionDto> GetAll(string keyword, bool includeDeleted)
        {
            using (var db = _connections.CreateDataContext())
                return Filter(db, keyword, includeDeleted).OrderBy(p => p.IsDeleted)
                    .ThenByDescending(p => p.StartDate).ThenBy(p => p.Code)
                    .ToList().Select(Map).ToList();
        }

        public PromotionPage GetPage(string keyword, bool includeDeleted, int pageIndex, int pageSize)
        {
            if (pageIndex < 0) throw new ArgumentOutOfRangeException(nameof(pageIndex));
            if (pageSize <= 0) throw new ArgumentOutOfRangeException(nameof(pageSize));

            using (var db = _connections.CreateDataContext())
            {
                var filtered = Filter(db, keyword, includeDeleted);
                return new PromotionPage
                {
                    TotalCount = filtered.Count(),
                    Rows = filtered.OrderBy(p => p.IsDeleted).ThenByDescending(p => p.StartDate)
                        .ThenBy(p => p.Code).Skip(checked(pageIndex * pageSize)).Take(pageSize)
                        .ToList().Select(Map).ToList()
                };
            }
        }

        public PromotionDto GetById(int promotionId)
        {
            using (var db = _connections.CreateDataContext())
            {
                PromotionEntity entity = db.Promotions.SingleOrDefault(p => p.PromotionID == promotionId);
                return entity == null ? null : Map(entity);
            }
        }

        public PromotionDto GetByCode(string code)
        {
            string normalized = (code ?? string.Empty).Trim().ToUpperInvariant();
            using (var db = _connections.CreateDataContext())
            {
                PromotionEntity entity = db.Promotions.SingleOrDefault(p => p.Code == normalized);
                return entity == null ? null : Map(entity);
            }
        }

        public int Insert(PromotionEditDto dto)
        {
            using (var db = _connections.CreateDataContext())
            {
                var promotion = new PromotionEntity();
                Apply(promotion, dto);
                promotion.IsDeleted = false;
                promotion.UsedCount = 0;
                db.Promotions.InsertOnSubmit(promotion);
                db.SubmitChanges();
                return promotion.PromotionID;
            }
        }

        public void Update(PromotionEditDto dto)
        {
            using (var db = _connections.CreateDataContext())
            {
                PromotionEntity promotion = db.Promotions.SingleOrDefault(p =>
                    p.PromotionID == dto.PromotionId && !p.IsDeleted);
                if (promotion == null) return;
                Apply(promotion, dto);
                db.SubmitChanges();
            }
        }

        public void SetActive(int promotionId, bool active)
        {
            using (var db = _connections.CreateDataContext())
            {
                PromotionEntity promotion = db.Promotions.SingleOrDefault(p =>
                    p.PromotionID == promotionId && !p.IsDeleted);
                if (promotion == null) return;
                promotion.IsActive = active;
                db.SubmitChanges();
            }
        }

        public void SoftDelete(int promotionId)
        {
            using (var db = _connections.CreateDataContext())
            {
                PromotionEntity promotion = db.Promotions.SingleOrDefault(p => p.PromotionID == promotionId);
                if (promotion == null) return;
                promotion.IsDeleted = true;
                promotion.IsActive = false;
                promotion.DeletedAt = DateTime.Now;
                db.SubmitChanges();
            }
        }

        private static IQueryable<PromotionEntity> Filter(
            SimsDataContext db, string keyword, bool includeDeleted)
        {
            string normalized = (keyword ?? string.Empty).Trim();
            return db.Promotions.Where(p => (includeDeleted || !p.IsDeleted)
                && (normalized == string.Empty || p.Code.Contains(normalized) || p.Name.Contains(normalized)));
        }

        private static void Apply(PromotionEntity entity, PromotionEditDto dto)
        {
            entity.Code = (dto.Code ?? string.Empty).Trim().ToUpperInvariant();
            entity.Name = (dto.Name ?? string.Empty).Trim();
            entity.DiscountType = dto.DiscountType;
            entity.DiscountValue = dto.DiscountValue;
            entity.MaxDiscountAmount = dto.MaxDiscountAmount;
            entity.MinOrderAmount = dto.MinOrderAmount;
            entity.StartDate = dto.StartDate.Date;
            entity.EndDate = dto.EndDate.Date;
            entity.UsageLimit = dto.UsageLimit;
            entity.IsActive = dto.IsActive;
            entity.ShowOnBanner = dto.ShowOnBanner;
            entity.CreatedBy = dto.CreatedBy;
        }

        private static PromotionDto Map(PromotionEntity entity)
        {
            return new PromotionDto
            {
                PromotionId = entity.PromotionID,
                Code = entity.Code,
                Name = entity.Name,
                DiscountType = entity.DiscountType,
                DiscountValue = entity.DiscountValue,
                MaxDiscountAmount = entity.MaxDiscountAmount,
                MinOrderAmount = entity.MinOrderAmount,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                UsageLimit = entity.UsageLimit,
                UsedCount = entity.UsedCount,
                IsActive = entity.IsActive,
                IsDeleted = entity.IsDeleted,
                ShowOnBanner = entity.ShowOnBanner
            };
        }
    }
}
