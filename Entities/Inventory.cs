namespace GameServices.Entities;

  public record Inventory {
    public Guid UserId { get; init; }
    public List<Item> Items { get; init; } = new List<Item> { };
    public decimal Money { get; init; } = 50;
  }
