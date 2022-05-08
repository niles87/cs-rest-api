using Microsoft.AspNetCore.Mvc;
using GameServices.Services;
using GameServices.Dtos;
using GameServices.Models;

namespace GameServices.Controllers;
[ApiController]
[Route("api/items")]
public class ItemsController : Controller {

  private readonly IItemService repo;

  public ItemsController(IItemService repo) {
    this.repo = repo;
  }

  [HttpGet]
  public async Task<IEnumerable<ItemDto>> GetItemsAsync() {
    return ( await repo.GetItemsAsync() ).Select(item => item.AsDto());
  }

  // GET /api/items/:id
  [HttpGet("{id}")]
  public async Task<ActionResult<ItemDto>> GetItemAsync(string id) {
    var item = await repo.GetItemAsync(id);

    if ( item is null ) {
      return NotFound();
    }

    return item.AsDto();
  }


  [HttpGet("search/{category}")]
  public async Task<IEnumerable<ItemDto>> GetItemsByCategoryAsync(string category) {
    return ( await repo.GetItemsByCategoryAsync(category) ).Select(item => item.AsDto());
  }

  // Post /api/items
  [HttpPost]
  public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto item) {
    Item newItem = new() {
      Name = item.Name,
      Price = item.Price,
      Category = item.Category,
      CreateDate = DateTimeOffset.Now,
    };

    await repo.CreateItemAsync(newItem);

    return CreatedAtAction(nameof(GetItemAsync), new { id = newItem.Id }, newItem.AsDto());
  }

  // PUT /api/items/:id
  [HttpPut("{id}")]
  public async Task<ActionResult<ItemDto>> UpdateItemAsync(string id, UpdateItemDto item) {
    var existingItem = await repo.GetItemAsync(id);

    if ( existingItem is null ) {
      return NotFound();
    }

    Item newItem = new() { Name = item.Name, Price = item.Price, Category = item.Category };

    Item? updatedItem = await repo.UpdateItemAsync(id, newItem);

    return updatedItem == null ? NotFound() : updatedItem.AsDto();
  }

  // DELETE /api/items/:id
  [HttpDelete("{id}")]
  public async Task<ActionResult> DeleteItemAsync(string id) {
    var item = await repo.GetItemAsync(id);

    if ( item is null ) {
      return NotFound();
    }

    await repo.DeleteItemAsync(id);

    return NoContent();
  }


}

