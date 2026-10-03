namespace SIMS_WinFormsApp.Services.AI.Interfaces
{
    public interface IAiRateLimiter
    {
        bool TryAcquire(int userId, out string message);
    }
}
