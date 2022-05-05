using GameServices.Models;
using GameServices.Utilities;
using MongoDB.Driver;

namespace GameServices.Services;
public class UserService {
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

  public async Task UpdateUserAsync(string id, User updatedUser) {
    await _userCollection.ReplaceOneAsync(user => user.Id == id, updatedUser);
  }

  public async Task DeleteUserAsync(string id) {
    await _userCollection.DeleteOneAsync(user => user.Id == id);
  }
}

