using GameServices.Entities;
using GameServices.Utilities;

namespace GameServices.Repositories {
  public class InMemUserRepo : IInMemUserRepo {

    private readonly List<User> users = new() {
      new User() { Id = Guid.NewGuid(), Username = "Daffy", Email = "daffy@email.com", Password = PasswordHash.HashPassword("HelloWorld"), JoinedDate = DateTimeOffset.UtcNow },
      new User() { Id = Guid.NewGuid(), Username = "Barry", Email = "barry@email.com", Password = PasswordHash.HashPassword("Password"), JoinedDate = DateTimeOffset.UtcNow },
    };

    public void CreateUser(User user) {
      users.Add(user);
    }

    public User? GetUser(string email, string password) {
      User? currentUser = GetUserByEmail(email);

      if ( currentUser is null )
        return null;

      if ( PasswordHash.MatchHash(password, currentUser.Password) )
        return currentUser;

      return null;
    }

    public void DeleteUser(string email, string password) {
      User? currentUser = GetUserByEmail(email);

      if ( currentUser is null ) {
        Console.WriteLine("No user found");
        return;
      }

      if ( PasswordHash.MatchHash(password, currentUser.Password) )
        users.Remove(currentUser);
    }

    private User? GetUserByEmail(string email) {
      User? user = users.FirstOrDefault(user => user.Email == email);
      if ( user == null )
        return null;
      return user;
    }
  }
}