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

  [HttpPost("login")]
  public async Task<ActionResult<UserDto>> LoginAsync(LoginUserDto user) {
    User? existingUser = await repo.LoginUserAsync(user.Email, user.Password);

    if ( existingUser == null ) {
      return BadRequest();
    }

    return existingUser.AsDto();
  }

  [HttpPut("update/{id}")]
  public async Task<ActionResult<UserDto>> UpdateUserAsync(string id, UpdateUserDto user) {
    User userUpdate = new() {
      Id = id,
      UserName = user.Username,
      UserEmail = user.Email,
      Wallet = user.Wallet,
    };
    User? updatedUser = await repo.UpdateUserAsync(id, userUpdate);

    if ( updatedUser == null ) {
      return BadRequest();
    }

    return updatedUser.AsDto();
  }

  [HttpPut("inventory/{id}/add")]
  public async Task<ActionResult<UserDto>> AddToUserInventoryAsync(string id, ItemDto item) {
    Item newItem = new() {
      Id = item.Id,
      Name = item.Name,
      Price = item.Price,
      Category = item.Category,
      CreateDate = item.CreateDate,
    };
    User? updatedUser = await repo.AddToUserInventoryAsync(id, newItem);

    if ( updatedUser == null ) {
      return BadRequest();
    }

    return updatedUser.AsDto();
  }

  [HttpPut("inventory/{id}/remove/{itemId}")]
  public async Task<ActionResult<UserDto>> RemoveItemFromUserInventoryAsync(string id, string itemId) {
    User? updatedUser = await repo.RemoveFromUserInventoryAsync(id, itemId);

    if ( updatedUser == null ) {
      return BadRequest();
    }

    return updatedUser.AsDto();
  }

  [HttpPost("remove")]
  public async Task<ActionResult> DeleteUserAsync(LoginUserDto loginUser) {
    User? existingUser = await repo.LoginUserAsync(loginUser.Email, loginUser.Password);

    if ( existingUser == null ) {
      return BadRequest();
    }
    await repo.DeleteUserAsync(existingUser.Id);

    return Ok();
  }
}

