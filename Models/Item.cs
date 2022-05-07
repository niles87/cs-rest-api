using MongoDB.Bson.Serialization.Attributes;

namespace GameServices.Models;
public class Item {

  [BsonId]
  [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
  public string? Id { get; set; }
  public string Name { get; set; } = null!;
  public decimal Price { get; set; }
  public string Category { get; set; } = null!;
  public DateTimeOffset CreateDate { get; set; }
}

