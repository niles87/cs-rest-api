namespace GameServices.Dtos;

public record InventoryDto {
  public List<ItemDto> Items { get; init; }
}