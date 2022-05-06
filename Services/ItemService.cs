using GameServices.Models;
using MongoDB.Driver;

namespace GameServices.Services;
public class ItemService {
  private const string databaseName = "gameservices";
  private const string collectionName = "items";
  private readonly IMongoCollection<Item> _itemCollection;

  public ItemService(IMongoClient mongoClient) {
    IMongoDatabase database = mongoClient.GetDatabase(databaseName);
    _itemCollection = database.GetCollection<Item>(collectionName);
  }

  public async Task<IEnumerable<Item>> GetItemsAsync() {
    return await _itemCollection.Find(_ => true).ToListAsync();
  }

  public async Task<IEnumerable<Item>> GetItemsByCategoryAsync(string categoryName) {
    return await _itemCollection.Find(item => item.Category == categoryName).ToListAsync();
  }

  // get a single item item
  public async Task<Item?> GetItemAsync(string id) {
    return await _itemCollection.Find(item => item.Id == id).FirstOrDefaultAsync();
  }

  // create an item
  public async Task CreateItemAsync(Item item) {
    await _itemCollection.InsertOneAsync(item);
  }

  public async Task<Item?> UpdateItemAsync(string id, Item item) {
    FilterDefinitionBuilder<Item> filter = Builders<Item>.Filter;
    UpdateDefinitionBuilder<Item> update = Builders<Item>.Update;
    return await _itemCollection.FindOneAndUpdateAsync(filter.Eq(item => item.Id, id), 
      update.Set("Name", item.Name).Set("Price", item.Price).Set("Category", item.Category),
      new FindOneAndUpdateOptions<Item> { ReturnDocument = ReturnDocument.After});
  }

  public async Task DeleteItemAsync(string id) {
    await _itemCollection.DeleteOneAsync(item => item.Id == id);
  }
}

