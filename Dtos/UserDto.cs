namespace GameServices.Dtos;

  public record UserDto {
    public Guid Id { get; init; }
    public string Username { get; init; }
    public string Email { get; init; }
    public DateTimeOffset JoinedDate { get; init; }
  }
