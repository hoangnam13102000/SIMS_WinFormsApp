using System;
using System.Security.Cryptography;

namespace SIMS_WinFormsApp.Services.Security
{
    public sealed class BCryptPasswordHasher : IPasswordHasher
    {
        private const string Prefix = "PBKDF2";
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int DefaultIterations = 100000;

        public string Hash(string plainPassword)
        {
            if (plainPassword == null) throw new ArgumentNullException(nameof(plainPassword));

            var salt = new byte[SaltSize];
            using (var rng = new RNGCryptoServiceProvider())
                rng.GetBytes(salt);

            var key = DeriveKey(plainPassword, salt, DefaultIterations);
            return string.Join("$", Prefix, DefaultIterations,
                Convert.ToBase64String(salt), Convert.ToBase64String(key));
        }

        public bool Verify(string plainPassword, string storedHash)
        {
            if (string.IsNullOrEmpty(plainPassword) || string.IsNullOrEmpty(storedHash))
                return false;

            if (storedHash.StartsWith("$2a$") || storedHash.StartsWith("$2b$") ||
                storedHash.StartsWith("$2y$"))
            {
                try { return BCrypt.Net.BCrypt.Verify(plainPassword, storedHash); }
                catch { return false; }
            }

            var parts = storedHash.Split('$');
            if (parts.Length != 4 || parts[0] != Prefix ||
                !int.TryParse(parts[1], out var iterations))
                return false;

            try
            {
                var salt = Convert.FromBase64String(parts[2]);
                var expected = Convert.FromBase64String(parts[3]);
                return FixedTimeEquals(
                    DeriveKey(plainPassword, salt, iterations), expected);
            }
            catch (FormatException) { return false; }
        }

        private static byte[] DeriveKey(string password, byte[] salt, int iterations)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(
                password, salt, iterations, HashAlgorithmName.SHA256))
                return pbkdf2.GetBytes(KeySize);
        }

        private static bool FixedTimeEquals(byte[] first, byte[] second)
        {
            if (first.Length != second.Length) return false;
            var difference = 0;
            for (var i = 0; i < first.Length; i++) difference |= first[i] ^ second[i];
            return difference == 0;
        }
    }
}
