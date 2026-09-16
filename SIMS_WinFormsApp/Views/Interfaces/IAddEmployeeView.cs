using System;
using System.Collections.Generic;
using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.Models.Enums;

namespace SIMS_WinFormsApp.Views.Interfaces
{

    public interface IAddEmployeeView
    {
        string FullName { get; set; }
        string Email { get; set; }
        string Phone { get; set; }
        string AvatarFilePath { get; }
        DateTime? DateOfBirth { get; set; }
        Gender? SelectedGender { get; set; }
        int? SelectedRoleId { get; set; }
        DateTime HireDate { get; set; }

        string SalaryText { get; set; }

        void BindRoles(IReadOnlyList<RoleOptionDto> roles);

        event EventHandler SaveRequested;
        event EventHandler CancelRequested;

        void ShowError(string message);
        void SetSaving(bool isSaving);
        void CloseOnSuccess();

        /// <summary>Hiển thị kết quả sau khi tạo tài khoản thành công. Nếu gửi email thất bại
        /// thì PHẢI hiển thị mật khẩu tạm để quản trị viên tự cung cấp cho nhân viên - không
        /// được để mất mật khẩu chỉ vì lỗi gửi mail (giống showCreationResult() bên bản Java).</summary>
        void ShowCreationResult(string username, bool emailSent, string emailError, string rawPassword);
    }
}