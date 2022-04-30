using GameServices.Entities;

namespace GameServices.Repositories {

  public class InMemItemsRepo : IInMemItemsRepo {

    private readonly List<Item> items = new() {
      new Item() { Id = Guid.NewGuid(), Name = "Potion", Price = 9, CreatedDate = DateTimeOffset.UtcNow },
      new Item() { Id = Guid.NewGuid(), Name = "Sword", Price = 20, CreatedDate = DateTimeOffset.UtcNow },
      new Item() { Id = Guid.NewGuid(), Name = "Broze Shield", Price = 18, CreatedDate = DateTimeOffset.UtcNow },
    };


    public IEnumerable<Item> GetItems() {
      return items;
    }

    public Item GetItem(Guid id) {
      return items.SingleOrDefault(item => item.Id == id);
    }

    public void CreateItem(Item item) {
      items.Add(item);
    }

    public void UpdateItem(Item item) {
      var index = items.FindIndex(exsitingItem => exsitingItem.Id == item.Id);
      items[index] = item;
    }

    public void DeleteItem(Guid id) {
      var index = items.FindIndex((item) => item.Id == id);
      items.RemoveAt(index);
    }
  }
}