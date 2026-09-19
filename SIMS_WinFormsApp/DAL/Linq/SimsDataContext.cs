using System.Data.Linq;
using SIMS_WinFormsApp.DAL.Linq.Entities.Identity;
using SIMS_WinFormsApp.DAL.Linq.Entities.Chat;
using SIMS_WinFormsApp.Infrastructure.Configuration;

namespace SIMS_WinFormsApp.DAL.Linq
{
    public class SimsDataContext : DataContext
    {
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
    }
}