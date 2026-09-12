namespace SIMS_WinFormsApp.Views.Interfaces
{
    public interface IForgotPasswordView
    {
        string Username { get; }
        string Email { get; }
        string OtpCode { get; }
        string NewPassword { get; }
        string ConfirmPassword { get; }
        string ChallengeId { get; }
    }
}
