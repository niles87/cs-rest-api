using MongoDB.Bson.Serialization.Attributes;

namespace GameServices.Models;
  public class Inventory {
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]  
    public string? Id { get; set; }
    public List<Item> Items { get; set; } = new List<Item>();
  }

