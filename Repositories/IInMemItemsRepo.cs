using GameServices.Entities;

namespace GameServices.Repositories {
  public interface IInMemItemsRepo {
    Item GetItem(Guid id);
    IEnumerable<Item> GetItems();
  }
}