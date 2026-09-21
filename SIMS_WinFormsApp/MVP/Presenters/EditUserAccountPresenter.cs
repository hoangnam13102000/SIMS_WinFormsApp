using System;
using System.Threading.Tasks;
using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.Services.Validation;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.MVP.Presenters
{
    /// <summary>
    /// Presenter cho popup "Cập nhật tài khoản" (dùng chung cho nhân viên lẫn khách hàng).
    /// Đảm nhiệm nạp hồ sơ nhân viên (nếu có) lên View, validate input + gọi
    /// <see cref="IUserManagementService.UpdateAccount"/> - <c>frmEditUserAccount</c> chỉ lo
    /// dựng UI và đọc/ghi giá trị field, không tự validate hay biết gì về Service (giống cách
    /// UserAccountDetailPresenter/ChangePasswordPresenter đã tách trong project).
    /// </summary>
    public sealed class EditUserAccountPresenter
    {
        private readonly IEditUserAccountView _view;
        private readonly IUserManagementService _userManagementService;
        private readonly int _userId;
        private bool _hasEmployeeProfile;

        public EditUserAccountPresenter(IEditUserAccountView view, IUserManagementService userManagementService, int userId)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _userManagementService = userManagementService ?? throw new ArgumentNullException(nameof(userManagementService));
            _userId = userId;

            _view.SaveRequested += OnSaveRequested;
            LoadEmployeeProfile();
        }

        // Tài khoản có hồ sơ nhân viên -> đổ lên form; không có (khách hàng...) -> ẩn các trường
        // đó đi và khi lưu sẽ không đụng tới bảng Employees.
        private void LoadEmployeeProfile()
        {
            EmployeeProfileDto profile = _userManagementService.GetEmployeeProfile(_userId);
            _hasEmployeeProfile = profile != null;
            _view.SetEmployeeProfileVisible(_hasEmployeeProfile);
            if (!_hasEmployeeProfile) return;

            _view.DateOfBirth = profile.DateOfBirth;
            _view.SelectedGender = profile.Gender;
            _view.HireDate = profile.HireDate;
            _view.SalaryText = profile.Salary.HasValue ? profile.Salary.Value.ToString("0.##") : string.Empty;
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

            if (!InputValidators.IsValidEmail(email))
            {
                _view.ShowError("Email không đúng định dạng.");
                return;
            }

            if (!string.IsNullOrEmpty(phone) && !InputValidators.IsValidPhone(phone))
            {
                _view.ShowError("Số điện thoại không đúng định dạng (VD: 09xxxxxxxx).");
                return;
            }

            EmployeeProfileDto employeeProfile = null;
            if (_hasEmployeeProfile && !TryReadEmployeeProfile(out employeeProfile))
                return;

            _view.SetSaving(true);
            try
            {
                var result = await Task.Run(() =>
                    _userManagementService.UpdateAccount(_userId, fullName, email, phone, _view.AvatarFilePath, employeeProfile));

                switch (result)
                {
                    case UpdateAccountResult.Success:
                        _view.CloseOnSuccess();
                        break;
                    case UpdateAccountResult.EmailAlreadyInUse:
                        _view.ShowError("Email này đã được sử dụng bởi tài khoản khác.");
                        break;
                    case UpdateAccountResult.UserNotFound:
                        _view.ShowError("Không tìm thấy tài khoản (có thể đã bị xóa).");
                        break;
                }
            }
            catch (Exception ex)
            {
                _view.ShowError("Không thể lưu ảnh đại diện hoặc thông tin tài khoản: " + ex.Message);
            }
            finally
            {
                _view.SetSaving(false);
            }
        }

        // Cùng quy tắc validate Lương với AddEmployeePresenter (số không âm, để trống = chưa xác định).
        private bool TryReadEmployeeProfile(out EmployeeProfileDto profile)
        {
            profile = null;

            decimal? salary = null;
            string salaryText = (_view.SalaryText ?? string.Empty).Trim();
            if (!string.IsNullOrEmpty(salaryText))
            {
                if (!decimal.TryParse(salaryText, out var parsedSalary) || parsedSalary < 0)
                {
                    _view.ShowError("Lương phải là một số không âm.");
                    return false;
                }
                salary = parsedSalary;
            }

            profile = new EmployeeProfileDto
            {
                DateOfBirth = _view.DateOfBirth,
                Gender = _view.SelectedGender,
                HireDate = _view.HireDate,
                Salary = salary
            };
            return true;
        }

        public void Dispose()
        {
            _view.SaveRequested -= OnSaveRequested;
        }
    }
}