using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Domain;

public class PlaylistState
{
    [BsonId]
    [BsonElement("_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    [BsonElement("franchiseId")]
    [BsonRepresentation(BsonType.String)]
    public string FranchiseId { get; set; }
    
    [BsonElement("enable")]
    [BsonRepresentation(BsonType.Boolean)]
    public bool Enable { get; set; }
}
