using GameServices.Entities;

namespace GameServices.Repositories {
  public interface IInMemUserRepo {
    void CreateUser(User user);
    void DeleteUser(string email, string password);
    User? GetUser(string email, string password);
  }
}