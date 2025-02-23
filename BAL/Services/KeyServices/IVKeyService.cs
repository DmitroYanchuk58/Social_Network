using BAL.Helpers.Interfaces;
using DAL.DbContext;

namespace BAL.Services
{
    public static class IVKeyService
    {
        private static readonly string connectionString = GetConnectionString();
        private static readonly MongoDbContext dbContext = new(connectionString);

        private static string GetConnectionString()
        {
            var username = "Dima";
            byte[] iv = { 14, 21, 178, 218, 19, 157, 203, 202, 112, 11, 182, 76, 112, 206, 79, 194 };
            var encryptedPassword = "1iG5LucztaQtZSgUt1GmXw==";
            var password = AesEncryptor.Decrypt(encryptedPassword, iv);
            var connection = $"mongodb+srv://{username}:{password}@cluster0.7hu6y.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";
            return connection;
        }

        public static byte[] GetIVKKey(string email)
        {
            ArgumentNullException.ThrowIfNull(nameof(email));

            var iv = dbContext.GetIVKey(email);
            return iv;
        }

        public static void CreateIVKey(string email, byte[] iv)
        {
            ArgumentNullException.ThrowIfNull(nameof(email));
            ArgumentNullException.ThrowIfNull(nameof(iv));

            dbContext.CreateIVKey(email, iv);
        }
    }
}
