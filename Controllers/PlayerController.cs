using GameServices.Dtos;
using GameServices.Entities;
using GameServices.Repositories;
using GameServices.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace GameServices.Controllers {
  [Route("api/user")]
  [ApiController]
  public class PlayerController : ControllerBase {

    private readonly IInMemUserRepo repo;

    public PlayerController(IInMemUserRepo repo) {
      this.repo = repo;
    }

    // POST /api/user
    [HttpPost]
    public ActionResult<UserDto> CreateUser(CreateUserDto user) {
      User newUser = new() {
        Id = Guid.NewGuid(),
        Username = user.Username,
        Email = user.Email,
        Password = PasswordHash.HashPassword(user.Password),
        JoinedDate = DateTimeOffset.Now,
      };

      repo.CreateUser(newUser);

      return CreatedAtAction(nameof(Login), new { email = newUser.Email, password = user.Password }, newUser.AsDto());
    }

    [Route("/api/user/login")]
    [HttpPost]
    public ActionResult<UserDto> Login(LoginUserDto user) {
      User? existingUser = repo.GetUser(user.Email, user.Password);

      if ( existingUser == null ) {
        return BadRequest();
      }

      return existingUser.AsDto();
    }
  }
}
