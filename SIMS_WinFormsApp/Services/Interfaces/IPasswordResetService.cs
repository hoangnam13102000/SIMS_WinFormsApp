using SIMS_WinFormsApp.Services.Security;

namespace SIMS_WinFormsApp.Services.Interfaces
{
    public interface IPasswordResetService
    {
        PasswordResetService.RequestResult RequestOtp(string username, string email);
        PasswordResetService.VerifyResult VerifyOtp(string challengeId, string code);
        PasswordResetService.ResendResult ResendOtp(string challengeId);
        PasswordResetService.ResetResult ResetPassword(string challengeId, string password);
        void CancelChallenge(string challengeId);
        PasswordResetService.PasswordValidationStatus ValidatePassword(string password);
    }
}
