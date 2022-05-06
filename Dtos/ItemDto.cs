namespace GameServices.Dtos;

public record ItemDto {
  public string Id { get; init; }
  public string Name { get; init; }
  public decimal Price { get; init; }
  public string Category { get; init; }
  public DateTimeOffset CreateDate { get; init; }

}
