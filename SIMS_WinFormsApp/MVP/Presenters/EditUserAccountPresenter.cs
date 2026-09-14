using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.MVP.Presenters
{
    /// <summary>
    /// Presenter cho popup "Cập nhật tài khoản" (dùng chung cho nhân viên lẫn khách hàng).
    /// Đảm nhiệm validate input + gọi <see cref="IUserManagementService.UpdateAccount"/> -
    /// <c>frmEditUserAccount</c> chỉ lo dựng UI và đọc/ghi giá trị field, không tự validate hay
    /// biết gì về Service (giống cách UserAccountDetailPresenter/ChangePasswordPresenter đã
    /// tách trong project).
    /// </summary>
    public sealed class EditUserAccountPresenter
    {
        // Cùng 1 mẫu regex email đã dùng ở PasswordResetService - giữ nhất quán quy tắc hợp lệ
        // email trong toàn bộ ứng dụng.
        private static readonly Regex EmailPattern =
            new Regex(@"^[\w.+-]+@[\w-]+\.[a-zA-Z]{2,}$", RegexOptions.Compiled);

        private readonly IEditUserAccountView _view;
        private readonly IUserManagementService _userManagementService;
        private readonly int _userId;

        public EditUserAccountPresenter(IEditUserAccountView view, IUserManagementService userManagementService, int userId)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _userManagementService = userManagementService ?? throw new ArgumentNullException(nameof(userManagementService));
            _userId = userId;

            _view.SaveRequested += OnSaveRequested;
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

            if (!string.IsNullOrEmpty(phone) && !Regex.IsMatch(phone, @"^0\d{9,10}$"))
            {
                _view.ShowError("Số điện thoại không đúng định dạng (VD: 09xxxxxxxx).");
                return;
            }

            _view.SetSaving(true);
            try
            {
                var result = await Task.Run(() =>
                    _userManagementService.UpdateAccount(_userId, fullName, email, phone));

                switch (result)
                {
                    case UpdateAccountResult.Success:
                        _view.ShowSuccess("Cập nhật tài khoản thành công.");
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