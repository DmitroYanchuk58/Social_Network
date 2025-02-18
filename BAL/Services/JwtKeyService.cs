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
            var username = "Dima";
            var password = AesEncryptor.Decrypt("xo7J5AytJ5pUjOoBsSUfNQ==");
            var connectionString = $"mongodb+srv://{username}:{password}@cluster0.7hu6y.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";
            dbContext = new(connectionString);
        }

        public string GetJwtSecretKey()
        {
            var key = dbContext.GetJwtSecretKey();
            return key;
        }
    }
}
