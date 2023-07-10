using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain;

public class Track
{
    [BsonId]
    [BsonElement("_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("deezerId")]
    [BsonRepresentation(BsonType.String)]
    public string DeezerId { get; set; }

    [BsonElement("franchiseId")]
    [BsonRepresentation(BsonType.String)]
    public string FranchiseId { get; set; }

    [BsonElement("title")]
    [BsonRepresentation(BsonType.String)]
    public string Title { get; set; }

    [BsonElement("artist")]
    [BsonRepresentation(BsonType.String)]
    public string Artist { get; set; }

    [BsonElement("album")]
    [BsonRepresentation(BsonType.String)]
    public string Album { get; set; }

    [BsonElement("cover")]
    [BsonRepresentation(BsonType.String)]
    public string Cover { get; set; }

    [BsonElement("duration")]
    [BsonRepresentation(BsonType.Int32)]
    public int Duration { get; set; }

    [BsonElement("upvotes")]
    public List<string> Upvotes { get; set; }

    [BsonElement("downvotes")]
    public List<string> Downvotes { get; set; }

    [BsonElement("isReading")]
    [BsonRepresentation(BsonType.Boolean)]
    public bool IsReading { get; set; }

    [BsonElement("currentDuration")]
    [BsonRepresentation(BsonType.Double)]
    public float CurrentDuration { get; set; }

    [BsonElement("createdAt")]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime? DatePromote { get; set; }

    [BsonElement("position")]
    [BsonRepresentation(BsonType.Int32)]
    public int Position { get; set; }
}