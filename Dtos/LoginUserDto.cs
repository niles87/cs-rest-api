using System.ComponentModel.DataAnnotations;

namespace GameServices.Dtos;

public record LoginUserDto {
  [Required]
  public string Email { get; init; }
  [Required]
  public string Password { get; init; }
}
