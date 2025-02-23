using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DAL.Entities.MongoDbEntities
{
    [Serializable]
    public class IVAesKey
    {
        [BsonId, BsonElement("id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("iv_key")]
        public required byte[] IVKey { get; set; }

        [BsonElement("gmail"), BsonRepresentation(BsonType.String)]
        public required string Gmail { get; set; }
    }
}
