using System;
using SIMS_WinFormsApp.Models.Enums;

namespace SIMS_WinFormsApp.Views.Interfaces
{
    public interface IEditUserAccountView
    {
        string FullName { get; set; }
        string Email { get; set; }
        string Phone { get; set; }
        string AvatarFilePath { get; }

        // Hồ sơ nhân viên (chỉ có ý nghĩa với tài khoản nhân viên - xem SetEmployeeProfileVisible).
        DateTime? DateOfBirth { get; set; }
        Gender? SelectedGender { get; set; }
        DateTime HireDate { get; set; }
        string SalaryText { get; set; }

        /// <summary>Hiện/ẩn các trường hồ sơ nhân viên (Ngày sinh, Giới tính, Ngày vào làm,
        /// Lương). Tài khoản khách hàng không có hồ sơ này nên Presenter sẽ ẩn đi.</summary>
        void SetEmployeeProfileVisible(bool visible);

        event EventHandler SaveRequested;

        event EventHandler CancelRequested;

        void ShowError(string message);
        void ShowSuccess(string message);

        void SetSaving(bool isSaving);

        void CloseOnSuccess();
    }
}