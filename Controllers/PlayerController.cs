using GameServices.Dtos;
using GameServices.Models;
using GameServices.Services;
using GameServices.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace GameServices.Controllers;
  [Route("api/user")]
  [ApiController]
  public class PlayerController : ControllerBase {

    private readonly UserService repo;

    public PlayerController(UserService repo) {
      this.repo = repo;
    }

    // POST /api/user
    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUserAsync(CreateUserDto user) {
      User newUser = new() {
        UserName = user.Username,
        UserEmail = user.Email,
        Password = PasswordHash.HashPassword(user.Password)
      };

      await repo.CreateUserAsync(newUser);

      return CreatedAtAction(nameof(LoginAsync), new { email = newUser.UserEmail, password = user.Password }, newUser.AsDto());
    }

    [Route("/api/user/login")]
    [HttpPost]
    public async Task<ActionResult<UserDto>> LoginAsync(LoginUserDto user) {
      User? existingUser = await repo.LoginUserAsync(user.Email, user.Password);

      if ( existingUser == null ) {
        return BadRequest();
      }

      return existingUser.AsDto();
    }
  }

