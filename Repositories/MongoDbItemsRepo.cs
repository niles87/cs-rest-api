using GameServices.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GameServices.Repositories {

  public class MongoDbItemsRepo : IInMemItemsRepo {

    private const string databaseName = "gameservices";
    private const string collectionName = "items";

    private readonly IMongoCollection<Item> itemCollection;
    private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;

    public MongoDbItemsRepo(IMongoClient mongoClient) {
      IMongoDatabase database = mongoClient.GetDatabase(databaseName);
      itemCollection = database.GetCollection<Item>(collectionName);
    }

    public async Task CreateItemAsync(Item item) {
      await itemCollection.InsertOneAsync(item);
    }

    public async Task DeleteItemAsync(Guid id) {
      var filter = filterBuilder.Eq("_id", id);
      await itemCollection.DeleteOneAsync(filter);
    }

    public async Task<Item> GetItemAsync(Guid id) {
      var filter = filterBuilder.Eq("_id", id);
      return await itemCollection.Find(filter).SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Item>> GetItemsAsync() {
      return await itemCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task UpdateItemAsync(Item item) {
      var filter = filterBuilder.Eq("_id", item.Id);

      await itemCollection.ReplaceOneAsync(filter, item);
    }

  }
}