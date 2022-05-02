using System.ComponentModel.DataAnnotations;

namespace GameServices.Dtos;

  public record CreateUserDto {
    [Required]
    [MinLength(4)]
    public string Username { get; init; }
    [Required]
    [MinLength(5)]
    public string Password { get; init; }
    [Required]
    [RegularExpression("^\\S+@\\S+\\.\\S+$")]
    public string Email { get; init; }

  }