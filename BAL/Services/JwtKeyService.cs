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
            dbContext = new();
        }

        public string GetJwtSecretKey()
        {
            var key = dbContext.GetJwtSecretKey();
            return key;
        }
    }
}
