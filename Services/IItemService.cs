using GameServices.Models;

namespace GameServices.Services;
public interface IItemService {
  Task<IEnumerable<Item>> GetItemsAsync();
  Task<Item?> GetItemAsync(string id);
  Task<IEnumerable<Item>> GetItemsByCategoryAsync(string categoryName);
  Task CreateItemAsync(Item item);
  Task<Item> UpdateItemAsync(string id, Item item);
  Task DeleteItemAsync(string id);
}