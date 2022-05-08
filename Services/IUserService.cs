using GameServices.Models;

namespace GameServices.Services;

public interface IUserService {
  Task<List<User>> GetUsersAsync();
  Task<User?> GetUserAsync(string id);
  Task<User?> LoginUserAsync(string email, string password);
  Task CreateUserAsync(User user);
  Task<User?> UpdateUserAsync(string id, User updatedUser);
  Task<User?> AddToUserInventoryAsync(string id, Item item);
  Task<User?> RemoveFromUserInventoryAsync(string id, string itemId);
  Task DeleteUserAsync(string id);
}