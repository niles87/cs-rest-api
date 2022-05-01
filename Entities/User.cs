namespace GameServices.Entities {

  public record User {
    public Guid Id { get; init; }
    public string Username { get; init; }
    public string Password { get; init; }
    public string Email { get; init; }
    public DateTimeOffset JoinedDate { get; init; }
  }
}