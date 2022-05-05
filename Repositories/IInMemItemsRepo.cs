using GameServices.Entities;

namespace GameServices.Repositories;
  public interface IInMemItemsRepo {
    Task<Item> GetItemAsync(Guid id);
    Task<IEnumerable<Item>> GetItemsAsync();

    Task CreateItemAsync(Item item);

    Task UpdateItemAsync(Item item);

    Task DeleteItemAsync(Guid id);
  }
