using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DAL.Entities.MongoDbEntities
{
    [Serializable]
    public class JwtSecretKey
    {
        [BsonId, BsonElement("id"), BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }

        [BsonElement("secret_key"), BsonRepresentation(BsonType.String)]
        public required string SecretKey { get; set; }
    }
}
