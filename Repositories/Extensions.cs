using GameServices.Dtos;
using GameServices.Models;

namespace GameServices;
public static class Extensions {
  public static ItemDto AsDto(this Item item) {
    return new ItemDto {
      Id = item.Id,
      Name = item.Name,
      Price = item.Price,
      Category = item.Category,
      CreateDate = item.CreateDate,
    };
  }

  public static UserDto AsDto(this User user) {
    InventoryDto inventoryDto = user.Inventory.AsDto();
    return new UserDto {
      Id = user.Id,
      Username = user.UserName,
      Email = user.UserEmail,
      Wallet = user.Wallet,
      Inventory = inventoryDto,
    };
  }

  public static InventoryDto AsDto(this Inventory inventory) {
    List<ItemDto> items = inventory.Items.ConvertAll(item => item.AsDto());
    return new InventoryDto {
      Items = items,
    };
  }
}
