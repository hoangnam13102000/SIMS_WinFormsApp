using System;
using System.Threading.Tasks;
using SIMS_WinFormsApp.Views.Interfaces;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.Services.Security;

namespace SIMS_WinFormsApp.MVP.Presenters
{
    public sealed class ForgotPasswordPresenter
    {
        private readonly IPasswordResetService _service;
        private readonly IForgotPasswordView _view;

        public ForgotPasswordPresenter(
            IForgotPasswordView view,
            IPasswordResetService service)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public Task<PasswordResetService.RequestResult> RequestOtpAsync()
        {
            return Task.Run(() => _service.RequestOtp(_view.Username, _view.Email));
        }

        public Task<PasswordResetService.VerifyResult> VerifyOtpAsync()
        {
            return Task.Run(() => _service.VerifyOtp(_view.ChallengeId, _view.OtpCode));
        }

        public Task<PasswordResetService.ResendResult> ResendOtpAsync()
        {
            return Task.Run(() => _service.ResendOtp(_view.ChallengeId));
        }

        public Task<PasswordResetService.ResetResult> ResetPasswordAsync()
        {
            return Task.Run(() => _service.ResetPassword(_view.ChallengeId, _view.NewPassword));
        }

        public void CancelChallenge()
        {
            _service.CancelChallenge(_view.ChallengeId);
        }

        public PasswordResetService.PasswordValidationStatus ValidatePassword()
        {
            return _service.ValidatePassword(_view.NewPassword);
        }
    }
}
