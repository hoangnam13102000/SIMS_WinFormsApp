using System;
using System.Security.Cryptography;
using System.Text;

namespace SIMS_WinFormsApp.Infrastructure.Security
{
    public static class SecureConfigFile
    {
        public static byte[] Encrypt(string plainText)
        {
            if (plainText == null) throw new ArgumentNullException(nameof(plainText));

            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            return ProtectedData.Protect(plainBytes, null, DataProtectionScope.CurrentUser);
        }

        public static string Decrypt(byte[] encryptedBytes)
        {
            if (encryptedBytes == null || encryptedBytes.Length == 0)
                throw new ArgumentException("File cấu hình mã hóa không có dữ liệu.", nameof(encryptedBytes));

            byte[] plainBytes = ProtectedData.Unprotect(
                encryptedBytes,
                null,
                DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(plainBytes);
        }
    }
}
