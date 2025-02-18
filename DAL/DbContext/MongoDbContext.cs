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
        private IMongoDatabase database;
        public MongoDbContext() 
        {
            var connectionString = "mongodb+srv://Dima:55555@cluster0.7hu6y.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";
            var mongoClient = new MongoClient(connectionString);
            database = mongoClient.GetDatabase("SocialNetwork");
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
