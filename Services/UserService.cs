using GameServices.Models;
using GameServices.Utilities;
using MongoDB.Driver;

namespace GameServices.Services;
public class UserService : IUserService {
  private const string databaseName = "gameservices";
  private const string collectionName = "users";
  private readonly IMongoCollection<User> _userCollection;

  public UserService(IMongoClient mongoClient) {
    IMongoDatabase database = mongoClient.GetDatabase(databaseName);
    _userCollection = database.GetCollection<User>(collectionName);
  }


  public async Task<List<User>> GetUsersAsync() {
    return await _userCollection.Find(_ => true).ToListAsync();
  }

  public async Task<User?> GetUserAsync(string id) {
    return await _userCollection.Find(user => user.Id == id).FirstOrDefaultAsync();
  }

  public async Task<User?> LoginUserAsync(string email, string password) {
    User? user = await _userCollection.Find(user => user.UserEmail == email).FirstOrDefaultAsync();

    if ( user == null ) {
      return null;
    }

    return PasswordHash.MatchHash(password, user.Password) ?
      user : null;
  }

  public async Task CreateUserAsync(User user) {
    await _userCollection.InsertOneAsync(user);
  }

  public async Task<User?> UpdateUserAsync(string id, User updatedUser) {
    FilterDefinitionBuilder<User> filter = Builders<User>.Filter;
    UpdateDefinitionBuilder<User> update = Builders<User>.Update;

    User user = await GetUserAsync(id);

    if ( user == null ) {
      return null;
    }

    decimal value = updatedUser.Wallet + user.Wallet;

    return await _userCollection.FindOneAndUpdateAsync(filter.Eq(user=>user.Id, id), 
      update.Set("UserName", updatedUser.UserName).Set("UserEmail", updatedUser.UserEmail).Set("Wallet", value), 
      new FindOneAndUpdateOptions<User, User> { ReturnDocument = ReturnDocument.After });
  }

  public async Task<User?> AddToUserInventoryAsync(string id, Item item) {
    FilterDefinitionBuilder<User> filter = Builders<User>.Filter;
    UpdateDefinitionBuilder<User> update = Builders<User>.Update;
    return await _userCollection.FindOneAndUpdateAsync(filter.Eq(user => user.Id, id),
      update.Push("Inventory.Items", item),
      new FindOneAndUpdateOptions<User, User?> { ReturnDocument = ReturnDocument.After });
  }

  public async Task<User?> RemoveFromUserInventoryAsync(string id, string itemId) {
    FilterDefinitionBuilder<User> filter = Builders<User>.Filter;
    UpdateDefinitionBuilder<User> update = Builders<User>.Update;
    FilterDefinitionBuilder<Item> itemFilter = Builders<Item>.Filter;
    return await _userCollection.FindOneAndUpdateAsync(filter.Eq(user => user.Id, id),
      update.PullFilter("Inventory.Items", itemFilter.Eq(exItem => exItem.Id, itemId)),
      new FindOneAndUpdateOptions<User, User?> { ReturnDocument = ReturnDocument.After });
  }

  public async Task DeleteUserAsync(string id) {
    await _userCollection.DeleteOneAsync(user => user.Id == id);
  }
}

