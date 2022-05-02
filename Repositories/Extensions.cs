using GameServices.Dtos;
using GameServices.Entities;

namespace GameServices;
  public static class Extensions {
    public static ItemDto AsDto(this Item item) {
      return new ItemDto {
        Id = item.Id,
        Name = item.Name,
        Price = item.Price,
        CreatedDate = item.CreatedDate,
      };
    }

    public static UserDto AsDto(this User user) {
      return new UserDto {
        Id = user.Id,
        Username = user.Username,
        Email = user.Email,
        JoinedDate = user.JoinedDate,
      };
    }
  }
