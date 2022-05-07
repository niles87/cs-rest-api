namespace GameServices.Dtos;

public record UpdateUserDto {
  public string Username { get; init; }
  public string Email { get; init; }
  public decimal Wallet { get; init; }

}