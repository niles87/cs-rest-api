using GameServices.Entities;

namespace GameServices.Repositories {
  public interface IInMemUserRepo {
    Task CreateUserAsync(User user);
    Task DeleteUserAsync(string email, string password);
    Task<User?> GetUserAsync(string email, string password);
  }
}