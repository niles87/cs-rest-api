using GameServices.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GameServices.Repositories {

  public class MongoDbItemsRepo : IInMemItemsRepo {

    private const string databaseName = "catalog";
    private const string collectionName = "items";

    private readonly IMongoCollection<Item> itemCollection;
    private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;

    public MongoDbItemsRepo(IMongoClient mongoClient) {
      IMongoDatabase database = mongoClient.GetDatabase(databaseName);
      itemCollection = database.GetCollection<Item>(collectionName);
    }

    public void CreateItem(Item item) {
      itemCollection.InsertOne(item);
    }

    public void DeleteItem(Guid id) {
      var filter = filterBuilder.Eq("_id", id);
      itemCollection.DeleteOne(filter);
    }

    public Item GetItem(Guid id) {
      var filter = filterBuilder.Eq("_id", id);
      return itemCollection.Find(filter).SingleOrDefault();
    }

    public IEnumerable<Item> GetItems() {
      return itemCollection.Find(new BsonDocument()).ToList();
    }

    public void UpdateItem(Item item) {
      var filter = filterBuilder.Eq("_id", item.Id);

      itemCollection.ReplaceOne(filter, item);
    }

  }
}