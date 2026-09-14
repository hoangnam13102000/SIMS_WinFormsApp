using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.Repositories.Interfaces;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.Services.Mail;
using SIMS_WinFormsApp.Services.Security;

namespace SIMS_WinFormsApp.Services.Implementations
{
    public sealed class UserManagementService : IUserManagementService
    {
        private static readonly Random Random = new Random();
        private const int MaxUsernameAttempts = 20;

        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly MailSender _mailSender;

        public UserManagementService(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IPasswordHasher passwordHasher,
            MailSender mailSender)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            _mailSender = mailSender ?? throw new ArgumentNullException(nameof(mailSender));
        }

        public UserManagementPageDto GetPage(
            int pageIndex,
            int pageSize,
            string searchTerm,
            string roleFilter,
            string statusFilter)
        {
            return _userRepository.GetManagementPage(
                pageIndex, pageSize, searchTerm, roleFilter, statusFilter);
        }

        public UpdateAccountResult UpdateAccount(int userId, string fullName, string email, string phone)
        {
            string normalizedEmail = (email ?? string.Empty).Trim();

            if (_userRepository.IsEmailInUseByOthers(normalizedEmail, userId))
                return UpdateAccountResult.EmailAlreadyInUse;

            bool updated = _userRepository.UpdateContactInfo(
                userId,
                (fullName ?? string.Empty).Trim(),
                normalizedEmail,
                (phone ?? string.Empty).Trim());

            return updated ? UpdateAccountResult.Success : UpdateAccountResult.UserNotFound;
        }

        public SetAccountLockResult SetAccountLocked(int userId, bool isLocked)
        {
            if (_userRepository.FindById(userId) == null)
                return SetAccountLockResult.UserNotFound;

            _userRepository.SetLocked(userId, isLocked);
            return SetAccountLockResult.Success;
        }

        public IReadOnlyList<RoleOptionDto> GetAssignableRoles() => _roleRepository.GetAssignableRoles();

        // Tạo tài khoản nhân viên mới: kiểm tra trùng email + vai trò hợp lệ, tự sinh
        // username/mật khẩu tạm, băm mật khẩu rồi lưu DB, cuối cùng gửi thông tin đăng nhập
        // qua email. Nếu gửi email thất bại vẫn coi là tạo THÀNH CÔNG (tài khoản đã lưu) -
        // chỉ báo EmailSent=false kèm mật khẩu tạm để View hiển thị cho quản trị viên tự
        // cung cấp thủ công, tương đương EmployeeDAO.createEmployee() bên bản Java.
        public EmployeeCreationOutcome CreateEmployee(CreateEmployeeRequestDto request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            string email = (request.Email ?? string.Empty).Trim();
            if (_userRepository.IsEmailInUseByOthers(email, -1))
            {
                return new EmployeeCreationOutcome { Result = CreateEmployeeResult.EmailAlreadyInUse };
            }

            if (!IsRoleAssignable(request.RoleId))
            {
                return new EmployeeCreationOutcome { Result = CreateEmployeeResult.RoleNotFound };
            }

            string username = GenerateUniqueUsername(request.FullName);
            string rawPassword = GenerateTemporaryPassword();
            string passwordHash = _passwordHasher.Hash(rawPassword);

            var newEmployee = new NewEmployeeDto
            {
                Username = username,
                PasswordHash = passwordHash,
                FullName = (request.FullName ?? string.Empty).Trim(),
                Email = email,
                Phone = string.IsNullOrWhiteSpace(request.Phone) ? null : request.Phone.Trim(),
                RoleId = request.RoleId,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender.HasValue ? request.Gender.Value.ToString().ToUpperInvariant() : null,
                HireDate = request.HireDate,
                Salary = request.Salary
            };

            _userRepository.CreateEmployee(newEmployee);

            var outcome = new EmployeeCreationOutcome
            {
                Result = CreateEmployeeResult.Success,
                Username = username,
                TemporaryPassword = rawPassword
            };

            try
            {
                _mailSender.Send(
                    email,
                    "Tài khoản nhân viên mới - SIMS",
                    BuildCredentialEmailBody(request.FullName, username, rawPassword));
                outcome.EmailSent = true;
            }
            catch (MailFailedException ex)
            {
                outcome.EmailSent = false;
                outcome.EmailError = ex.Message;
            }

            return outcome;
        }

        private bool IsRoleAssignable(int roleId)
        {
            foreach (var role in _roleRepository.GetAssignableRoles())
            {
                if (role.RoleId == roleId) return true;
            }
            return false;
        }

        private static string BuildCredentialEmailBody(string fullName, string username, string rawPassword)
        {
            return "Xin chào " + fullName + ",\r\n\r\n"
                 + "Tài khoản nhân viên của bạn trên hệ thống SIMS đã được tạo:\r\n"
                 + "- Tên đăng nhập: " + username + "\r\n"
                 + "- Mật khẩu tạm thời: " + rawPassword + "\r\n\r\n"
                 + "Vui lòng đăng nhập và đổi mật khẩu ngay trong lần sử dụng đầu tiên.";
        }

        // Sinh tên đăng nhập từ họ tên (bỏ dấu, viết liền không dấu cách, thêm số ngẫu nhiên
        // nếu trùng) - cùng cách tiếp cận với EmployeeFormDialog bên bản Java, chỉ khác là
        // kiểm tra trùng trực tiếp qua Repository thay vì để DB tự sinh.
        private string GenerateUniqueUsername(string fullName)
        {
            string baseUsername = Slugify(fullName);
            if (string.IsNullOrEmpty(baseUsername)) baseUsername = "nhanvien";

            for (int attempt = 0; attempt < MaxUsernameAttempts; attempt++)
            {
                string candidate = attempt == 0 ? baseUsername : baseUsername + Random.Next(100, 999);
                if (!_userRepository.IsUsernameInUse(candidate))
                    return candidate;
            }

            // Trường hợp cực hiếm vẫn còn trùng sau nhiều lần thử: thêm hậu tố theo thời gian
            // để đảm bảo luôn tạo được username duy nhất.
            return baseUsername + DateTime.UtcNow.Ticks.ToString(CultureInfo.InvariantCulture).Substring(10);
        }

        private static string GenerateTemporaryPassword()
        {
            const string lower = "abcdefghjkmnpqrstuvwxyz";
            const string upper = "ABCDEFGHJKMNPQRSTUVWXYZ";
            const string digits = "23456789";
            const string special = "!@#$%";

            var sb = new StringBuilder();
            sb.Append(upper[Random.Next(upper.Length)]);
            sb.Append(lower[Random.Next(lower.Length)]);
            sb.Append(digits[Random.Next(digits.Length)]);
            sb.Append(special[Random.Next(special.Length)]);
            for (int i = 0; i < 5; i++) sb.Append(lower[Random.Next(lower.Length)]);
            for (int i = 0; i < 2; i++) sb.Append(digits[Random.Next(digits.Length)]);
            return sb.ToString();
        }

        private static string Slugify(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName)) return string.Empty;
            string noDiacritics = RemoveDiacritics(fullName.Trim());
            var sb = new StringBuilder();
            foreach (char c in noDiacritics)
            {
                if (char.IsLetterOrDigit(c)) sb.Append(char.ToLowerInvariant(c));
            }
            return sb.ToString();
        }

        private static string RemoveDiacritics(string text)
        {
            string normalized = text.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
            foreach (char c in normalized)
            {
                var category = CharUnicodeInfo.GetUnicodeCategory(c);
                if (category != UnicodeCategory.NonSpacingMark) sb.Append(c);
            }
            return sb.ToString().Replace('đ', 'd').Replace('Đ', 'D').Normalize(NormalizationForm.FormC);
        }
    }
}