using System;
using System.Collections.Generic;
using SIMS_WinFormsApp.Models.DTOs.Catalog;
using SIMS_WinFormsApp.Models.DTOs.Inventory;
using SIMS_WinFormsApp.Repositories.Interfaces;
using SIMS_WinFormsApp.Services.Interfaces;

namespace SIMS_WinFormsApp.Services.Implementations.Inventory
{
    public sealed class StockReconciliationService : IStockReconciliationService
    {
        private readonly IStockReconciliationRepository _repository;

        public StockReconciliationService(IStockReconciliationRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        // Tạo phiên hôm nay (nếu chưa có) rồi đồng bộ theo tồn kho. Trả về số sản phẩm vừa thêm.
        public int PrepareToday(int userId)
        {
            if (userId <= 0)
                throw new InvalidOperationException("Phiên đăng nhập không hợp lệ. Vui lòng đăng nhập lại.");

            int created = _repository.CreateTodaySession(userId);
            _repository.SyncTodayWithStock();
            return created;
        }

        public IReadOnlyList<StockReconciliationRowDto> Search(string keyword, DateTime? from, DateTime? to)
        {
            return _repository.Search(keyword, from, to);
        }

        public CatalogSaveResult UpdateCount(int reconciliationId, int actualStock, string note, int userId)
        {
            if (userId <= 0)
                return CatalogSaveResult.Fail("Phiên đăng nhập không hợp lệ. Vui lòng đăng nhập lại.");
            if (actualStock < 0)
                return CatalogSaveResult.Fail("Tồn thực tế phải là số nguyên ≥ 0.");

            string normalizedNote = (note ?? string.Empty).Trim();
            if (normalizedNote.Length > 255)
                return CatalogSaveResult.Fail("Lý do tối đa 255 ký tự.");

            try
            {
                switch (_repository.UpdateCount(reconciliationId, actualStock, normalizedNote, userId))
                {
                    case StockCountUpdateStatus.Ok:
                        return CatalogSaveResult.Ok(reconciliationId, "Đã cập nhật.");
                    case StockCountUpdateStatus.Locked:
                        return CatalogSaveResult.Fail("Đã qua 00:00 — phiên cũ đã khóa. Chỉ được sửa phiên hôm nay.");
                    case StockCountUpdateStatus.OutOfRange:
                        return CatalogSaveResult.Fail("Tồn thực tế không được > tồn kho của phiên.");
                    case StockCountUpdateStatus.NotEnoughStock:
                        return CatalogSaveResult.Fail(
                            "Tồn hiện tại không đủ để trừ (đã bán sau khi lập phiên). Bấm \"Đồng bộ SP hôm nay\" rồi đếm lại.");
                    default:
                        return CatalogSaveResult.Fail("Không tìm thấy dòng đối chiếu.");
                }
            }
            catch (Exception exception)
            {
                return CatalogSaveResult.Fail("Không thể cập nhật: " + exception.Message);
            }
        }
    }
}