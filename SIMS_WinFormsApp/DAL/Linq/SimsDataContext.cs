using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Globalization;
using System.Linq;
using System.Reflection;
using SIMS_WinFormsApp.DAL.Linq.Entities.Identity;
using SIMS_WinFormsApp.DAL.Linq.Entities.Chat;
using SIMS_WinFormsApp.DAL.Linq.Entities.Business;
using SIMS_WinFormsApp.Infrastructure.Configuration;
using SIMS_WinFormsApp.Services.Session;
using Newtonsoft.Json;

namespace SIMS_WinFormsApp.DAL.Linq
{
    public class SimsDataContext : DataContext
    {
        private bool _writingAuditLogs;

        public SimsDataContext() : this(new ConnectionStringProvider().GetConnectionString())
        {
        }

        public SimsDataContext(string connectionString) : base(connectionString)
        {
        }

        public Table<UserEntity> Users => GetTable<UserEntity>();
        public Table<EmployeeEntity> Employees => GetTable<EmployeeEntity>();
        public Table<RoleEntity> Roles => GetTable<RoleEntity>();
        public Table<PermissionEntity> Permissions => GetTable<PermissionEntity>();
        public Table<RolePermissionEntity> RolePermissions => GetTable<RolePermissionEntity>();
        public Table<StoreConfigEntity> StoreConfigs => GetTable<StoreConfigEntity>();

        public Table<AuditLogEntity> AuditLogs => GetTable<AuditLogEntity>();
        public Table<ChatConversationEntity> ChatConversations => GetTable<ChatConversationEntity>();
        public Table<ChatMessageEntity> ChatMessages => GetTable<ChatMessageEntity>();
        public Table<CategoryEntity> Categories => GetTable<CategoryEntity>();
        public Table<ProductEntity> Products => GetTable<ProductEntity>();
        public Table<CustomerEntity> Customers => GetTable<CustomerEntity>();
        public Table<InventoryBatchEntity> InventoryBatches => GetTable<InventoryBatchEntity>();
        public Table<StockDisposalEntity> StockDisposals => GetTable<StockDisposalEntity>();
        public Table<StockDisposalDetailEntity> StockDisposalDetails => GetTable<StockDisposalDetailEntity>();
        public Table<SupplierEntity> Suppliers => GetTable<SupplierEntity>();
        public Table<SupplierProductEntity> SupplierProducts => GetTable<SupplierProductEntity>();
        public Table<InvoiceEntity> Invoices => GetTable<InvoiceEntity>();
        public Table<InvoiceDetailEntity> InvoiceDetails => GetTable<InvoiceDetailEntity>();
        public Table<ReturnExchangeEntity> ReturnExchanges => GetTable<ReturnExchangeEntity>();
        public Table<ReturnExchangeDetailEntity> ReturnExchangeDetails => GetTable<ReturnExchangeDetailEntity>();
        public Table<InventoryTransactionEntity> InventoryTransactions => GetTable<InventoryTransactionEntity>();
        public Table<PromotionEntity> Promotions => GetTable<PromotionEntity>();

        public override void SubmitChanges(ConflictMode failureMode)
        {
            if (_writingAuditLogs)
            {
                base.SubmitChanges(failureMode);
                return;
            }

            List<TrackedChange> changes = GetTrackedChanges();
            if (changes.Count == 0)
            {
                base.SubmitChanges(failureMode);
                return;
            }

            bool closeConnection = Connection.State != ConnectionState.Open;
            DbTransaction ownedTransaction = null;
            try
            {
                if (Transaction == null)
                {
                    if (closeConnection)
                        Connection.Open();
                    ownedTransaction = Connection.BeginTransaction();
                    Transaction = ownedTransaction;
                }

                base.SubmitChanges(failureMode);

                foreach (TrackedChange change in changes)
                    AuditLogs.InsertOnSubmit(CreateAuditLog(change));

                _writingAuditLogs = true;
                base.SubmitChanges(failureMode);

                if (ownedTransaction != null)
                    ownedTransaction.Commit();
            }
            finally
            {
                _writingAuditLogs = false;
                if (ownedTransaction != null)
                {
                    Transaction = null;
                    ownedTransaction.Dispose();
                }
                if (closeConnection && Connection.State != ConnectionState.Closed)
                    Connection.Close();
            }
        }

        private List<TrackedChange> GetTrackedChanges()
        {
            if (UserSession.Instance.CurrentUser == null)
                return new List<TrackedChange>();

            ChangeSet changeSet = GetChangeSet();
            var changes = new List<TrackedChange>();
            AddTrackedChanges(changes, changeSet.Inserts, "DATA_CREATE", false);
            AddTrackedChanges(changes, changeSet.Updates, "DATA_UPDATE", false);
            AddTrackedChanges(changes, changeSet.Deletes, "DATA_DELETE", true);
            return changes;
        }

        private void AddTrackedChanges(
            ICollection<TrackedChange> changes,
            IEnumerable<object> entities,
            string action,
            bool isPhysicalDelete)
        {
            foreach (object entity in entities)
            {
                Type entityType = entity.GetType();
                TableAttribute table = Attribute.GetCustomAttribute(entityType, typeof(TableAttribute))
                    as TableAttribute;
                if (table == null
                    || string.Equals(table.Name, "AuditLogs", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(table.Name, "ChatMessages", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(table.Name, "ChatConversations", StringComparison.OrdinalIgnoreCase))
                    continue;

                object original = action == "DATA_UPDATE"
                    ? GetTable(entityType).GetOriginalEntityState(entity)
                    : entity;
                string effectiveAction = action == "DATA_UPDATE" && IsSoftDelete(entity, original)
                    ? "DATA_DELETE"
                    : action;
                changes.Add(new TrackedChange(
                    entity, original, table.Name, effectiveAction, isPhysicalDelete));
            }
        }

        private AuditLogEntity CreateAuditLog(TrackedChange change)
        {
            MetaType metaType = Mapping.GetMetaType(change.Entity.GetType());
            string key = string.Join(", ", metaType.IdentityMembers.Select(member =>
                member.Name + "=" + FormatValue(GetMemberValue(change.Entity, member.Member))));
            string firstKeyValue = metaType.IdentityMembers
                .Select(member => FormatValue(GetMemberValue(change.Entity, member.Member)))
                .FirstOrDefault();
            int recordId;
            int? parsedRecordId = int.TryParse(firstKeyValue,
                NumberStyles.Integer, CultureInfo.InvariantCulture, out recordId) ? (int?)recordId : null;

            object oldEntity = change.Action == "DATA_CREATE" ? null : change.Original;
            object newEntity = change.IsPhysicalDelete ? null : change.Entity;
            string oldValue = oldEntity == null ? null : SerializeValues(oldEntity, newEntity);
            string newValue = newEntity == null ? null : SerializeValues(newEntity, oldEntity);
            string actionDescription = change.Action == "DATA_CREATE" ? "Đã thêm"
                : change.Action == "DATA_DELETE" ? "Đã xóa"
                : "Đã cập nhật";

            return new AuditLogEntity
            {
                UserID = UserSession.Instance.CurrentUser?.UserId,
                Action = change.Action,
                TableName = change.TableName,
                RecordID = parsedRecordId,
                OldValue = oldValue,
                NewValue = newValue,
                Detail = actionDescription + " dữ liệu trong " + change.TableName
                    + (string.IsNullOrWhiteSpace(key) ? string.Empty : " (" + key + ")"),
                CreatedAt = DateTime.Now
            };
        }

        private string SerializeValues(object entity, object comparisonEntity)
        {
            MetaType metaType = Mapping.GetMetaType(entity.GetType());
            var values = new Dictionary<string, object>();
            foreach (MetaDataMember member in metaType.DataMembers)
            {
                if (member.IsAssociation || IsSensitiveField(member.Name))
                    continue;

                object value = GetMemberValue(entity, member.Member);
                if (comparisonEntity != null
                    && Equals(value, GetMemberValue(comparisonEntity, member.Member)))
                    continue;

                values[member.Name] = value;
            }

            return values.Count == 0 ? null : JsonConvert.SerializeObject(values);
        }

        private static bool IsSensitiveField(string name)
        {
            return name.IndexOf("password", StringComparison.OrdinalIgnoreCase) >= 0
                || name.IndexOf("hash", StringComparison.OrdinalIgnoreCase) >= 0
                || name.IndexOf("secret", StringComparison.OrdinalIgnoreCase) >= 0
                || name.IndexOf("token", StringComparison.OrdinalIgnoreCase) >= 0
                || name.IndexOf("salt", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static bool IsSoftDelete(object entity, object original)
        {
            if (original == null)
                return false;

            foreach (string fieldName in new[] { "IsDeleted", "DeletedAt" })
            {
                PropertyInfo property = entity.GetType().GetProperty(fieldName);
                if (property == null)
                    continue;

                object currentValue = property.GetValue(entity, null);
                object originalValue = property.GetValue(original, null);
                if (fieldName == "IsDeleted"
                    && currentValue is bool && (bool)currentValue && !Equals(currentValue, originalValue))
                    return true;
                if (fieldName == "DeletedAt" && currentValue != null && originalValue == null)
                    return true;
            }

            return false;
        }

        private static object GetMemberValue(object entity, MemberInfo member)
        {
            PropertyInfo property = member as PropertyInfo;
            if (property != null)
                return property.GetValue(entity, null);

            FieldInfo field = member as FieldInfo;
            return field == null ? null : field.GetValue(entity);
        }

        private static string FormatValue(object value)
        {
            return value == null ? "null" : Convert.ToString(value, CultureInfo.InvariantCulture);
        }

        private sealed class TrackedChange
        {
            public TrackedChange(
                object entity, object original, string tableName, string action, bool isPhysicalDelete)
            {
                Entity = entity;
                Original = original;
                TableName = tableName;
                Action = action;
                IsPhysicalDelete = isPhysicalDelete;
            }

            public object Entity { get; }
            public object Original { get; }
            public string TableName { get; }
            public string Action { get; }
            public bool IsPhysicalDelete { get; }
        }
    }
}