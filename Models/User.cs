using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GameServices.Models;
public class User {

  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
  public string? Id { get; set; }
  public string UserName { get; set; } = null!;
  public string UserEmail { get; set; } = null!;
  public string Password { get; set; } = null!;
  public decimal Wallet { get; set; } = 50;
  public Inventory Inventory { get; set; } = new Inventory();
}

