using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using SIMS_WinFormsApp.Infrastructure.Composition;
using SIMS_WinFormsApp.MVP.Presenters;
using SIMS_WinFormsApp.Models.DTOs.Permission;
using SIMS_WinFormsApp.Models.Permission;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.Views.UserManager
{
    public sealed partial class ucRolePermission : UserControl, IRolePermissionView
    {
        private readonly RolePermissionPresenter _presenter;
        private bool _bindingRoles;
        private bool _bindingPermissions;

        public event EventHandler ViewReady;
        public event EventHandler<int> RoleSelected;
        public event EventHandler<string> RoleSearchTextChanged;
        public event EventHandler<PermissionToggledEventArgs> PermissionToggled;
        public event EventHandler SaveRequested;
        public event EventHandler ResetRequested;

        public ucRolePermission()
        {
            InitializeComponent();
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;

            _presenter = new RolePermissionPresenter(
                this,
                AppComposition.CreateRolePermissionRepository(),
                AppComposition.CreateRoleRepository());

            _roleSearchTextBox.TextChanged += (sender, args) =>
                RoleSearchTextChanged?.Invoke(this, _roleSearchTextBox.Text);
            _rolesGrid.SelectionChanged += OnRoleSelectionChanged;
            _permissionsGrid.CurrentCellDirtyStateChanged += (sender, args) =>
            {
                if (_permissionsGrid.IsCurrentCellDirty)
                    _permissionsGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };
            _permissionsGrid.CellValueChanged += OnPermissionCellValueChanged;
            _saveButton.Click += (sender, args) => SaveRequested?.Invoke(this, EventArgs.Empty);
            _resetButton.Click += (sender, args) => ResetRequested?.Invoke(this, EventArgs.Empty);
            Load += (sender, args) => ViewReady?.Invoke(this, EventArgs.Empty);
            Disposed += (sender, args) => _presenter.Dispose();
        }

        public void ShowRoles(IReadOnlyList<RolePermissionRoleRowDto> roles)
        {
            _bindingRoles = true;
            try
            {
                _rolesGrid.Rows.Clear();
                roles = roles ?? Array.Empty<RolePermissionRoleRowDto>();
                _roleCountLabel.Text = roles.Count + " vai trò";

                foreach (RolePermissionRoleRowDto role in roles)
                {
                    int index = _rolesGrid.Rows.Add(
                        role.RoleName,
                        role.PermissionCount + " quyền",
                        role.IsAdmin ? "Quản trị viên" : role.RoleCode);
                    DataGridViewRow row = _rolesGrid.Rows[index];
                    row.Tag = role;
                    row.Selected = role.IsSelected;
                }

                if (_rolesGrid.SelectedRows.Count == 0 && _rolesGrid.Rows.Count > 0)
                    _rolesGrid.Rows[0].Selected = true;
            }
            finally
            {
                _bindingRoles = false;
            }

            _rolesEmptyLabel.Visible = _rolesGrid.Rows.Count == 0;
        }

        public void ShowPermissionGroups(
            string roleTitle,
            bool isAdminRole,
            IReadOnlyList<PermissionGroupViewModel> groups)
        {
            _permissionsTitleLabel.Text = string.IsNullOrWhiteSpace(roleTitle)
                ? "Quyền của vai trò"
                : roleTitle;

            _bindingPermissions = true;
            try
            {
                _permissionsGrid.Rows.Clear();
                groups = groups ?? Array.Empty<PermissionGroupViewModel>();
                _permissionsGrid.Enabled = groups.Count > 0;
                _adminInfoLabel.Visible = isAdminRole && groups.Count > 0;
                _permissionsGrid.Top = _adminInfoLabel.Visible ? 110 : 58;

                foreach (PermissionGroupViewModel group in groups)
                {
                    foreach (PermissionGroupEntryViewModel entry in group.Entries)
                    {
                        if (entry is PermissionToggleEntryViewModel toggleEntry)
                        {
                            AddPermissionRow(
                                group.Title,
                                toggleEntry.Toggle.Label,
                                JoinDescription(group.Hint, toggleEntry.Toggle.Description),
                                toggleEntry.Toggle,
                                isAdminRole);
                        }
                        else if (entry is PermissionResourceEntryViewModel resourceEntry)
                        {
                            PermissionResourceViewModel resource = resourceEntry.Resource;
                            foreach (PermissionToggleViewModel tier in resource.Tiers)
                            {
                                AddPermissionRow(
                                    group.Title,
                                    resource.Name + " — " + tier.Label,
                                    JoinDescription(resource.Description, tier.Description),
                                    tier,
                                    isAdminRole);
                            }
                        }
                    }
                }
            }
            finally
            {
                _bindingPermissions = false;
            }

            _permissionEmptyLabel.Visible = _permissionsGrid.Rows.Count == 0;
        }

        private void AddPermissionRow(
            string groupName,
            string label,
            string description,
            PermissionToggleViewModel permission,
            bool isAdminRole)
        {
            int index = _permissionsGrid.Rows.Add(
                groupName,
                label,
                description,
                permission.IsChecked);
            DataGridViewRow row = _permissionsGrid.Rows[index];
            row.Tag = permission.Permission;
            row.Cells[_enabledColumn.Index].ReadOnly = isAdminRole;
        }

        private static string JoinDescription(string first, string second)
        {
            if (string.IsNullOrWhiteSpace(first)) return second ?? string.Empty;
            if (string.IsNullOrWhiteSpace(second)) return first;
            return first + " " + second;
        }

        private void OnRoleSelectionChanged(object sender, EventArgs e)
        {
            if (_bindingRoles || _rolesGrid.CurrentRow == null) return;
            var role = _rolesGrid.CurrentRow.Tag as RolePermissionRoleRowDto;
            if (role != null) RoleSelected?.Invoke(this, role.RoleId);
        }

        private void OnPermissionCellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_bindingPermissions || e.RowIndex < 0 ||
                e.ColumnIndex != _enabledColumn.Index)
                return;

            DataGridViewRow row = _permissionsGrid.Rows[e.RowIndex];
            if (!(row.Tag is AppPermission)) return;
            bool isChecked = Convert.ToBoolean(row.Cells[_enabledColumn.Index].Value);
            PermissionToggled?.Invoke(
                this,
                new PermissionToggledEventArgs((AppPermission)row.Tag, isChecked));
        }

        public void SetBusy(bool isBusy, string message)
        {
            Cursor = isBusy ? Cursors.WaitCursor : Cursors.Default;
            _saveButton.Enabled = !isBusy;
            _resetButton.Enabled = !isBusy;
            _roleSearchTextBox.Enabled = !isBusy;
            _rolesGrid.Enabled = !isBusy;
            _permissionsGrid.Enabled = !isBusy && _permissionsGrid.Rows.Count > 0;
            _busyMessageLabel.Text = string.IsNullOrWhiteSpace(message) ? "Đang xử lý..." : message;
            _busyOverlay.Visible = isBusy;
            if (isBusy) _busyOverlay.BringToFront();
        }

        public void ShowError(string title, string message) =>
            DialogHelper.ShowError(FindForm(), title, message);

        public void ShowSuccess(string title, string message) =>
            DialogHelper.ShowSuccess(FindForm(), title, message);

        public void ShowInfo(string title, string message) =>
            DialogHelper.ShowInfo(FindForm(), title, message);
    }
}
