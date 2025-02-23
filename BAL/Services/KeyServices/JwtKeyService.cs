using BAL.Helpers.Interfaces;
using DAL.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class JwtKeyService
    {
        private readonly MongoDbContext dbContext;

        public JwtKeyService()
        {
            dbContext = new("Dima", GetPassword());
        }

        private static string GetPassword()
        {
            byte[] iv = { 14, 21, 178, 218, 19, 157, 203, 202, 112, 11, 182, 76, 112, 206, 79, 194 };
            var encryptedPassword = "1iG5LucztaQtZSgUt1GmXw==";
            var password = AesEncryptor.Decrypt(encryptedPassword, iv);
            return password;
        }

        public string GetJwtSecretKey()
        {
            var key = dbContext.GetJwtSecretKey();
            return key;
        }
    }
}
