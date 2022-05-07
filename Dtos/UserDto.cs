namespace GameServices.Dtos;

public record UserDto {
  public string Id { get; init; }
  public string Username { get; init; }
  public string Email { get; init; }
  public decimal Wallet { get; init; }
  public InventoryDto Inventory { get; init; }
}
