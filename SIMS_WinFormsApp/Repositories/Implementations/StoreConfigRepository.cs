using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SIMS_WinFormsApp.DAL.Linq;
using SIMS_WinFormsApp.DAL.Linq.Entities.Identity;
using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.Repositories.Interfaces;

namespace SIMS_WinFormsApp.Repositories.Implementations
{
    /// <summary>
    /// Đọc/ghi bảng StoreConfig (key-value, đã có sẵn trong sql/SIMS.sql cùng 6 dòng dữ liệu mẫu -
    /// xem sql/Insert_SIMS.sql). 5 khóa đầu dùng ĐÚNG tên đã có sẵn trong dữ liệu mẫu
    /// (STORE_NAME, DEFAULT_UNIT, VAT_RATE, DEFAULT_MARGIN, RETURN_POLICY_DAYS) - không đổi tên,
    /// không xóa dòng nào. Riêng "Ngưỡng cần duyệt" (ô cuối trong hình mẫu) chưa có khóa tương ứng
    /// trong dữ liệu mẫu, nên dùng 1 khóa MỚI (RETURN_APPROVAL_THRESHOLD) - vì StoreConfig là
    /// bảng key-value nên thêm khóa mới không cần đổi schema hay đụng tới dòng nào đã có; nếu
    /// khóa này chưa tồn tại trong DB, GetSettings() trả về 0 và lần Lưu đầu tiên sẽ tự thêm dòng.
    /// </summary>
    public class StoreConfigRepository : IStoreConfigRepository
    {
        private const string KeyStoreName = "STORE_NAME";
        private const string KeyDefaultUnit = "DEFAULT_UNIT";
        private const string KeyVatRate = "VAT_RATE";
        private const string KeyDefaultMargin = "DEFAULT_MARGIN";
        private const string KeyReturnPolicyDays = "RETURN_POLICY_DAYS";
        private const string KeyApprovalThreshold = "RETURN_APPROVAL_THRESHOLD"; // MỚI

        public StoreSettingsDto GetSettings()
        {
            using (var db = new SimsDataContext())
            {
                var all = db.StoreConfigs
                    .ToDictionary(c => c.ConfigKey, c => c.ConfigValue, StringComparer.OrdinalIgnoreCase);

                return new StoreSettingsDto
                {
                    StoreName = GetString(all, KeyStoreName),
                    DefaultUnit = GetString(all, KeyDefaultUnit),
                    VatRate = GetDecimal(all, KeyVatRate),
                    DefaultMargin = GetDecimal(all, KeyDefaultMargin),
                    ReturnPolicyDays = GetInt(all, KeyReturnPolicyDays),
                    ApprovalThreshold = GetDecimal(all, KeyApprovalThreshold)
                };
            }
        }

        public void SaveSettings(StoreSettingsDto settings)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));

            using (var db = new SimsDataContext())
            {
                Upsert(db, KeyStoreName, settings.StoreName);
                Upsert(db, KeyDefaultUnit, settings.DefaultUnit);
                Upsert(db, KeyVatRate, settings.VatRate.ToString(CultureInfo.InvariantCulture));
                Upsert(db, KeyDefaultMargin, settings.DefaultMargin.ToString(CultureInfo.InvariantCulture));
                Upsert(db, KeyReturnPolicyDays, settings.ReturnPolicyDays.ToString(CultureInfo.InvariantCulture));
                Upsert(db, KeyApprovalThreshold, settings.ApprovalThreshold.ToString(CultureInfo.InvariantCulture));
                db.SubmitChanges();
            }
        }

        private static void Upsert(SimsDataContext db, string key, string value)
        {
            var existing = db.StoreConfigs.FirstOrDefault(c => c.ConfigKey == key);
            if (existing != null)
            {
                existing.ConfigValue = value ?? string.Empty;
            }
            else
            {
                db.StoreConfigs.InsertOnSubmit(new StoreConfigEntity
                {
                    ConfigKey = key,
                    ConfigValue = value ?? string.Empty
                });
            }
        }

        private static string GetString(Dictionary<string, string> all, string key) =>
            all.TryGetValue(key, out var v) ? v : string.Empty;

        private static decimal GetDecimal(Dictionary<string, string> all, string key) =>
            all.TryGetValue(key, out var v) &&
            decimal.TryParse(v, NumberStyles.Any, CultureInfo.InvariantCulture, out var d) ? d : 0m;

        private static int GetInt(Dictionary<string, string> all, string key) =>
            all.TryGetValue(key, out var v) && int.TryParse(v, out var i) ? i : 0;
    }
}