using System.ComponentModel.DataAnnotations;

namespace GameServices.Dtos;

public record UpdateItemDto {
  [Required]
  public string Name { get; init; }

  [Required]
  [Range(1, 1000)]
  public decimal Price { get; init; }

  public string Category { get; init; }
}
