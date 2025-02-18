using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.MongoDbEntities
{
    [Serializable]
    public class JwtSecretKey
    {
        [BsonId, BsonElement("id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("secret_key"), BsonRepresentation(BsonType.String)]
        public string SecretKey { get; set; }
    }
}
