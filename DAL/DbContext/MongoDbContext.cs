using DAL.Entities.MongoDbEntities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DbContext
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase database;

        public MongoDbContext(string username, string password)
        {
            var connectionString = $"mongodb+srv://{username}:{password}@cluster0.7hu6y.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";
            var mongoClient = new MongoClient(connectionString);
            database = mongoClient.GetDatabase("SocialNetwork");
        }

        public byte[] GetIVKey(string email)
        {
            ArgumentException.ThrowIfNullOrEmpty(email);
            var keys = database.GetCollection<IVAesKey>("IVAesKey");
            var firstKey = keys.Find(k => k.Gmail == email).FirstOrDefault();
            var iv = firstKey.IVKey;
            ArgumentException.ThrowIfNullOrEmpty(iv.ToString());
            return iv;
        }

        public void CreateIVKey(string gmail, byte[] iv)
        {
            var keys = database.GetCollection<IVAesKey>("IVAesKey");

            var filter = Builders<IVAesKey>.Filter.Eq(x => x.Gmail, gmail);
            var existingKey = keys.Find(filter).FirstOrDefault();

            if (existingKey != null)
            {
                var update = Builders<IVAesKey>.Update.Set(x => x.IVKey, iv);
                keys.UpdateOne(filter, update);
            }
            else
            {
                var key = new IVAesKey()
                {
                    Gmail = gmail,
                    IVKey = iv
                };
                keys.InsertOne(key);
            }
        }

        public string GetJwtSecretKey()
        {
            var keys = database.GetCollection<JwtSecretKey>("JwtSecretKey");
            var firstKey = keys.Find(_ => true).First();
            var secretKey = firstKey.SecretKey;
            return secretKey;
        }
    }
}
