using BAL.Services;
using DAL.Entities.MongoDbEntities;
using System.Security.Cryptography;
using System.Text;

namespace BAL.Helpers.Interfaces
{
    public static class AesEncryptor
    {
        public static string Decrypt(string ciphertext, byte[] iv)
        {
            if (string.IsNullOrEmpty(ciphertext))
            {
                throw new ArgumentException("Ciphertext cannot be null or empty.", nameof(ciphertext));
            }

            byte[] Key = Encoding.UTF8.GetBytes("Dima_Yanchuk_Key");

            using (var aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = iv;

                var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                byte[] encryptedBytes = Convert.FromBase64String(ciphertext);
                byte[] plainBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

                return Encoding.UTF8.GetString(plainBytes);
            }
        }

        public static (string, byte[]) Encrypt(string plaintext)
        {
            if (string.IsNullOrEmpty(plaintext))
            {
                throw new ArgumentException("Plaintext cannot be null or empty.",nameof(plaintext));
            }

            byte[] Key = Encoding.UTF8.GetBytes("Dima_Yanchuk_Key");

            using (var aes = Aes.Create())
            {
                aes.Key = Key;
                aes.GenerateIV();

                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                byte[] plainBytes = Encoding.UTF8.GetBytes(plaintext);
                byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

                var encryptedText = Convert.ToBase64String(encryptedBytes);

                return (encryptedText, aes.IV);
            }
        }
    }
}
