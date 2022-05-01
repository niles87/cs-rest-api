using GameServices.Entities;
using GameServices.Utilities;

namespace GameServices.Repositories {
  public class InMemUserRepo : IInMemUserRepo {

    private readonly List<User> users = new() {
      new User() { Id = Guid.NewGuid(), Username = "Daffy", Email = "daffy@email.com", Password = PasswordHash.HashPassword("HelloWorld"), JoinedDate = DateTimeOffset.UtcNow },
      new User() { Id = Guid.NewGuid(), Username = "Barry", Email = "barry@email.com", Password = PasswordHash.HashPassword("Password"), JoinedDate = DateTimeOffset.UtcNow },
    };

    public async Task CreateUserAsync(User user) {
      users.Add(user);
      await Task.CompletedTask;
    }

    public async Task<User?> GetUserAsync(string email, string password) {
      User? currentUser = await GetUserByEmailAsync(email);

      if ( PasswordHash.MatchHash(password, currentUser.Password) )
        return await Task.FromResult(currentUser);

      return await Task.FromResult(currentUser);
    }

    public async Task DeleteUserAsync(string email, string password) {
      User? currentUser = await GetUserByEmailAsync(email);

      if ( currentUser is null ) {
        Console.WriteLine("No user found");
        await Task.CompletedTask;
        return;
      }

      if ( PasswordHash.MatchHash(password, currentUser.Password) )
        users.Remove(currentUser);
      await Task.CompletedTask;
    }

    private async Task<User?> GetUserByEmailAsync(string email) {
      User? user = users.FirstOrDefault(user => user.Email == email);
      return await Task.FromResult(user);
    }
  }
}