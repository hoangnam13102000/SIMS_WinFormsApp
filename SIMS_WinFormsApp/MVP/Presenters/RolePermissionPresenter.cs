using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.Models.DTOs.Permission;
using SIMS_WinFormsApp.Models.Enums;
using SIMS_WinFormsApp.Models.Permission;
using SIMS_WinFormsApp.Repositories.Interfaces;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.MVP.Presenters
{
    public sealed class RolePermissionPresenter
    {
        private static readonly string[] RoleDisplayPriority =
        {
            RoleCodes.Admin, RoleCodes.InventoryManager, RoleCodes.SalesManager, RoleCodes.SalesStaff
        };

        private readonly IRolePermissionView _view;
        private readonly IRolePermissionRepository _permissionRepository;
        private readonly IRoleRepository _roleRepository;

        private List<RoleOptionDto> _managedRoles = new List<RoleOptionDto>();
        private readonly Dictionary<int, HashSet<AppPermission>> _roleCache = new Dictionary<int, HashSet<AppPermission>>();
        private readonly HashSet<AppPermission> _workingSet = new HashSet<AppPermission>();

        private RoleOptionDto _selectedRole;
        private string _searchText = string.Empty;
        private bool _isBusy;

        public RolePermissionPresenter(
            IRolePermissionView view,
            IRolePermissionRepository permissionRepository,
            IRoleRepository roleRepository)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _permissionRepository = permissionRepository ?? throw new ArgumentNullException(nameof(permissionRepository));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));

            _view.ViewReady += OnViewReady;
            _view.RoleSelected += OnRoleSelected;
            _view.RoleSearchTextChanged += OnRoleSearchTextChanged;
            _view.PermissionToggled += OnPermissionToggled;
            _view.SaveRequested += OnSaveRequested;
            _view.ResetRequested += OnResetRequested;
        }

        // ==================== Nạp dữ liệu ====================

        private async void OnViewReady(object sender, EventArgs e) => await LoadAllAsync();

        private async Task LoadAllAsync()
        {
            if (_isBusy) return;
            _isBusy = true;
            _view.SetBusy(true, "Đang tải phân quyền...");
            try
            {
                var loaded = await Task.Run(() =>
                {
                    _permissionRepository.EnsureCatalogSeeded();

                    var roles = OrderRoles(_roleRepository.GetAssignableRoles());
                    var cache = new Dictionary<int, HashSet<AppPermission>>();
                    foreach (var role in roles)
                    {
                        cache[role.RoleId] = IsAdmin(role)
                            ? new HashSet<AppPermission>(Enum.GetValues(typeof(AppPermission)).Cast<AppPermission>())
                            : _permissionRepository.GetPermissionsForRole(role.RoleId);
                    }
                    return new LoadResult(roles, cache);
                });

                _managedRoles = loaded.Roles;
                _roleCache.Clear();
                foreach (var kv in loaded.Cache) _roleCache[kv.Key] = kv.Value;

                RoleOptionDto keepSelected = _selectedRole != null
                    ? _managedRoles.FirstOrDefault(r => r.RoleId == _selectedRole.RoleId)
                    : null;

                SelectRole(keepSelected ?? _managedRoles.FirstOrDefault(r => !IsAdmin(r)) ?? _managedRoles.FirstOrDefault());
            }
            catch (Exception)
            {
                _view.ShowError("Tải dữ liệu thất bại", "Không tải được dữ liệu phân quyền, vui lòng thử lại.");
            }
            finally
            {
                _view.SetBusy(false, null);
                _isBusy = false;
            }
        }

        private static List<RoleOptionDto> OrderRoles(IReadOnlyList<RoleOptionDto> roles)
        {
            return (roles ?? new List<RoleOptionDto>())
                .OrderBy(r => RolePriorityIndex(r.RoleCode))
                .ThenBy(r => r.RoleName, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        private static int RolePriorityIndex(string roleCode)
        {
            int idx = Array.FindIndex(RoleDisplayPriority,
                code => string.Equals(code, roleCode, StringComparison.OrdinalIgnoreCase));
            return idx < 0 ? int.MaxValue : idx;
        }

        private static bool IsAdmin(RoleOptionDto role)
        {
            return role != null && string.Equals(role.RoleCode, RoleCodes.Admin, StringComparison.OrdinalIgnoreCase);
        }

        // ==================== Danh sách vai trò (trái) ====================

        private void OnRoleSearchTextChanged(object sender, string text)
        {
            _searchText = text ?? string.Empty;
            RenderRoleList();
        }

        private IReadOnlyList<RoleOptionDto> FilteredRoles()
        {
            if (string.IsNullOrWhiteSpace(_searchText)) return _managedRoles;
            string key = _searchText.Trim();
            return _managedRoles
                .Where(r => (r.RoleName ?? string.Empty).IndexOf(key, StringComparison.OrdinalIgnoreCase) >= 0
                         || (r.RoleCode ?? string.Empty).IndexOf(key, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();
        }

        private void RenderRoleList()
        {
            var rows = FilteredRoles().Select(r => new RolePermissionRoleRowDto
            {
                RoleId = r.RoleId,
                RoleCode = r.RoleCode,
                RoleName = r.RoleName,
                IsAdmin = IsAdmin(r),
                PermissionCount = _roleCache.TryGetValue(r.RoleId, out var set) ? set.Count : 0,
                IsSelected = _selectedRole != null && _selectedRole.RoleId == r.RoleId
            }).ToList();

            _view.ShowRoles(rows);
        }

        private void OnRoleSelected(object sender, int roleId)
        {
            var role = _managedRoles.FirstOrDefault(r => r.RoleId == roleId);
            if (role == null) return;
            SelectRole(role);
        }

        private void SelectRole(RoleOptionDto role)
        {
            _selectedRole = role;
            _workingSet.Clear();
            if (role != null && _roleCache.TryGetValue(role.RoleId, out var current))
            {
                _workingSet.UnionWith(current);
            }
            RenderRoleList();
            RenderPermissionGroups();
        }

        // ==================== Danh sách quyền (phải) ====================

        private void RenderPermissionGroups()
        {
            if (_selectedRole == null)
            {
                _view.ShowPermissionGroups(string.Empty, false, new List<PermissionGroupViewModel>());
                return;
            }

            bool isAdminRole = IsAdmin(_selectedRole);
            var groups = PermissionDisplayLayout.Groups()
                .Select(BuildGroupViewModel)
                .ToList();

            string title = "Quyền của vai trò: " + (_selectedRole.RoleName ?? _selectedRole.RoleCode);
            _view.ShowPermissionGroups(title, isAdminRole, groups);
        }

        private PermissionGroupViewModel BuildGroupViewModel(PermissionGroupLayout group)
        {
            var entries = group.Entries.Select(BuildEntryViewModel).ToList();
            return new PermissionGroupViewModel(group.Title, group.Hint, entries);
        }

        private PermissionGroupEntryViewModel BuildEntryViewModel(PermissionGroupLayoutEntry entry)
        {
            if (entry is FlatPermissionLayoutEntry flat)
            {
                return new PermissionToggleEntryViewModel(BuildToggleViewModel(flat.Permission));
            }

            var resourceEntry = (ResourcePermissionLayoutEntry)entry;
            var tiers = resourceEntry.Resource.Tiers
                .Select(t => new PermissionToggleViewModel(t.Permission, t.TierLabel, t.TierDescription, _workingSet.Contains(t.Permission)))
                .ToList();
            var resourceVm = new PermissionResourceViewModel(resourceEntry.Resource.Name, resourceEntry.Resource.Description, tiers);
            return new PermissionResourceEntryViewModel(resourceVm);
        }

        private PermissionToggleViewModel BuildToggleViewModel(AppPermission permission)
        {
            var catalogEntry = PermissionCatalog.Get(permission);
            return new PermissionToggleViewModel(permission, catalogEntry.Label, catalogEntry.Description, _workingSet.Contains(permission));
        }

        private void OnPermissionToggled(object sender, PermissionToggledEventArgs e)
        {
            if (_selectedRole == null || IsAdmin(_selectedRole)) return;

            if (e.IsChecked) _workingSet.Add(e.Permission);
            else _workingSet.Remove(e.Permission);
        }

        // ==================== Hành động ====================

        private async void OnSaveRequested(object sender, EventArgs e) => await SaveAsync();

        private async Task SaveAsync()
        {
            if (_selectedRole == null || IsAdmin(_selectedRole))
            {
                _view.ShowInfo("Lưu phân quyền", "Quản trị viên luôn có toàn quyền hệ thống, không cần lưu.");
                return;
            }

            int roleId = _selectedRole.RoleId;
            string roleName = _selectedRole.RoleName;
            var toSave = new HashSet<AppPermission>(_workingSet);

            _view.SetBusy(true, "Đang lưu phân quyền...");
            try
            {
                bool ok = await Task.Run(() => _permissionRepository.SavePermissionsForRole(roleId, toSave));
                if (ok)
                {
                    _roleCache[roleId] = toSave;
                    RenderRoleList();
                    _view.ShowSuccess("Lưu thành công",
                        $"Đã lưu phân quyền cho vai trò \"{roleName}\". Áp dụng ngay cho các lần đăng nhập tiếp theo.");
                }
                else
                {
                    _view.ShowError("Lưu thất bại",
                        "Lưu phân quyền thất bại, vui lòng thử lại.\nNếu vấn đề tiếp diễn, kiểm tra kết nối CSDL.");
                }
            }
            catch (Exception)
            {
                _view.ShowError("Lưu thất bại", "Có lỗi xảy ra khi lưu phân quyền, vui lòng thử lại.");
            }
            finally
            {
                _view.SetBusy(false, null);
            }
        }

        private void OnResetRequested(object sender, EventArgs e)
        {
            if (_selectedRole == null || IsAdmin(_selectedRole))
            {
                _view.ShowInfo("Khôi phục mặc định", "Quản trị viên luôn có toàn quyền hệ thống, không cần khôi phục.");
                return;
            }

            _workingSet.Clear();
            _workingSet.UnionWith(RolePermissionDefaults.GetDefault(_selectedRole.RoleCode));
            RenderPermissionGroups();

            _view.ShowInfo("Đã khôi phục",
                $"Đã khôi phục về danh sách quyền mặc định của vai trò \"{_selectedRole.RoleName}\". Bấm \"Lưu thay đổi\" để áp dụng.");
        }

        public void Dispose()
        {
            _view.ViewReady -= OnViewReady;
            _view.RoleSelected -= OnRoleSelected;
            _view.RoleSearchTextChanged -= OnRoleSearchTextChanged;
            _view.PermissionToggled -= OnPermissionToggled;
            _view.SaveRequested -= OnSaveRequested;
            _view.ResetRequested -= OnResetRequested;
        }

        /// <summary>Kết quả tải nền (chạy trong Task.Run) - gộp 2 giá trị trả về thay vì dùng tuple để tránh phụ thuộc ValueTuple.</summary>
        private sealed class LoadResult
        {
            public List<RoleOptionDto> Roles { get; }
            public Dictionary<int, HashSet<AppPermission>> Cache { get; }

            public LoadResult(List<RoleOptionDto> roles, Dictionary<int, HashSet<AppPermission>> cache)
            {
                Roles = roles;
                Cache = cache;
            }
        }
    }
}