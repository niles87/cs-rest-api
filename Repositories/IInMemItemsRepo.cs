using GameServices.Entities;

namespace GameServices.Repositories {
  public interface IInMemItemsRepo {
    Item GetItem(Guid id);
    IEnumerable<Item> GetItems();

    void CreateItem(Item item);

    void UpdateItem(Item item);

    void DeleteItem(Guid id);
  }
}