namespace SIMS_WinFormsApp.Services.Security
{
    public interface IPasswordHasher
    {
        string Hash(string plainPassword);
        bool Verify(string plainPassword, string storedHash);
    }
}
