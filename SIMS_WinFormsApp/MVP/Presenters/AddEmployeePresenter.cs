using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.MVP.Presenters
{

    public sealed class AddEmployeePresenter
    {
        private static readonly Regex EmailPattern =
            new Regex(@"^[\w.+-]+@[\w-]+\.[a-zA-Z]{2,}$", RegexOptions.Compiled);
        private static readonly Regex PhonePattern =
            new Regex(@"^0\d{9,10}$", RegexOptions.Compiled);

        private readonly IAddEmployeeView _view;
        private readonly IUserManagementService _userManagementService;

        public AddEmployeePresenter(IAddEmployeeView view, IUserManagementService userManagementService)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _userManagementService = userManagementService
                ?? throw new ArgumentNullException(nameof(userManagementService));

            _view.SaveRequested += OnSaveRequested;
            _view.BindRoles(_userManagementService.GetAssignableRoles());
        }

        private async void OnSaveRequested(object sender, EventArgs e) => await SaveAsync();

        public async Task SaveAsync()
        {
            string fullName = (_view.FullName ?? string.Empty).Trim();
            string email = (_view.Email ?? string.Empty).Trim();
            string phone = (_view.Phone ?? string.Empty).Trim();

            if (string.IsNullOrEmpty(fullName))
            {
                _view.ShowError("Vui lòng nhập họ và tên.");
                return;
            }
            if (string.IsNullOrEmpty(email))
            {
                _view.ShowError("Vui lòng nhập email.");
                return;
            }
            if (!EmailPattern.IsMatch(email))
            {
                _view.ShowError("Email không đúng định dạng.");
                return;
            }
            if (!string.IsNullOrEmpty(phone) && !PhonePattern.IsMatch(phone))
            {
                _view.ShowError("Số điện thoại không đúng định dạng (VD: 09xxxxxxxx).");
                return;
            }
            if (!_view.SelectedRoleId.HasValue)
            {
                _view.ShowError("Vui lòng chọn vai trò.");
                return;
            }

            decimal? salary = null;
            string salaryText = (_view.SalaryText ?? string.Empty).Trim();
            if (!string.IsNullOrEmpty(salaryText))
            {
                if (!decimal.TryParse(salaryText, out var parsedSalary) || parsedSalary < 0)
                {
                    _view.ShowError("Lương phải là một số không âm.");
                    return;
                }
                salary = parsedSalary;
            }

            var request = new CreateEmployeeRequestDto
            {
                FullName = fullName,
                Email = email,
                Phone = string.IsNullOrEmpty(phone) ? null : phone,
                DateOfBirth = _view.DateOfBirth,
                Gender = _view.SelectedGender,
                RoleId = _view.SelectedRoleId.Value,
                HireDate = _view.HireDate,
                Salary = salary
            };

            _view.SetSaving(true);
            try
            {
                var outcome = await Task.Run(() => _userManagementService.CreateEmployee(request));

                switch (outcome.Result)
                {
                    case CreateEmployeeResult.Success:
                        _view.ShowCreationResult(outcome.Username, outcome.EmailSent, outcome.EmailError, outcome.TemporaryPassword);
                        _view.CloseOnSuccess();
                        break;
                    case CreateEmployeeResult.EmailAlreadyInUse:
                        _view.ShowError("Email này đã được sử dụng bởi tài khoản khác.");
                        break;
                    case CreateEmployeeResult.RoleNotFound:
                        _view.ShowError("Vai trò đã chọn không hợp lệ, vui lòng chọn lại.");
                        break;
                }
            }
            catch (Exception)
            {
                _view.ShowError("Có lỗi xảy ra, vui lòng thử lại.");
            }
            finally
            {
                _view.SetSaving(false);
            }
        }

        public void Dispose()
        {
            _view.SaveRequested -= OnSaveRequested;
        }
    }
}