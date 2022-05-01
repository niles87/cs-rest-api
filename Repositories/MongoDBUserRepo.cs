using GameServices.Entities;
using GameServices.Utilities;
using MongoDB.Driver;

namespace GameServices.Repositories {

  public class MongoDBUserRepo : IInMemUserRepo {

    private const string databaseName = "gameservices";
    private const string collectionName = "game_users";

    private readonly IMongoCollection<User> userCollection;
    private readonly FilterDefinitionBuilder<User> filterBuilder = Builders<User>.Filter;

    public MongoDBUserRepo(IMongoClient mongoClient) {
      IMongoDatabase database = mongoClient.GetDatabase(databaseName);
      userCollection = database.GetCollection<User>(collectionName);
    }

    public async Task CreateUserAsync(User user) {
      await userCollection.InsertOneAsync(user);
    }

    public async Task DeleteUserAsync(string email, string password) {
      User? user = await GetUserByEmailAsync(email);

      if ( user == null ) {
        await Task.CompletedTask;
        return;
      }

      if ( PasswordHash.MatchHash(password, user.Password) ) {
        _ = userCollection.DeleteOneAsync(filterBuilder.Eq("_id", user.Id));
      }
    }

    public async Task<User?> GetUserAsync(string email, string password) {
      User? user = await GetUserByEmailAsync(email);

      if ( user == null ) {
        return null;
      }
      return PasswordHash.MatchHash(password, user.Password) ? 
        await userCollection.Find(filterBuilder.Eq("_id", user.Id)).SingleOrDefaultAsync() :
        null;
    }

    private async Task<User?> GetUserByEmailAsync(string email) {
      // filter user by email
      var filter = filterBuilder.Eq(user => user.Email, email);
      var user = await userCollection.Find(filter).SingleOrDefaultAsync();
      Console.WriteLine(user.ToString());
      return user;
    }
  }
}