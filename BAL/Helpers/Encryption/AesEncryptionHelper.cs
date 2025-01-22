using System.Security.Cryptography;
using System.Text;

namespace BAL.Helpers.Interfaces
{
    public class AesEncryptionHelper : IEncryption
    {
        public string Decrypt(string ciphertext)
        {
            if (string.IsNullOrEmpty(ciphertext))
            {
                throw new ArgumentException("Ciphertext cannot be null or empty.", nameof(ciphertext));
            }

            byte[] Key = Encoding.UTF8.GetBytes("Dima_Yanchuk_Key");
            byte[] IV = Encoding.UTF8.GetBytes("Social_NetworkIV");

            using (var aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                byte[] encryptedBytes = Convert.FromBase64String(ciphertext);
                byte[] plainBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

                return Encoding.UTF8.GetString(plainBytes);
            }
        }

        public string Encrypt(string plaintext)
        {
            if (string.IsNullOrEmpty(plaintext))
            {
                throw new ArgumentException(nameof(plaintext));
            }

            byte[] Key = Encoding.UTF8.GetBytes("Dima_Yanchuk_Key");
            byte[] IV = Encoding.UTF8.GetBytes("Social_NetworkIV");

            using (var aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                byte[] plainBytes = Encoding.UTF8.GetBytes(plaintext);
                byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

                return Convert.ToBase64String(encryptedBytes);
            }
        }
    }
}
