using Microsoft.AspNetCore.Mvc;
using GameServices.Repositories;
using GameServices.Dtos;
using GameServices.Entities;

namespace GameServices.Controllers {
  [ApiController]
  [Route("api/items")]
  public class ItemsController : Controller {

    private readonly IInMemItemsRepo repo;

    public ItemsController(IInMemItemsRepo repo) {
      this.repo = repo;
    }

    // GET /api/items
    [HttpGet]
    public async Task<IEnumerable<ItemDto>> GetItemsAsync() {
      var items = (await repo.GetItemsAsync()).Select(item => item.AsDto());

      return items;
    }

    // GET /api/items/:id
    [HttpGet("{id}")]
    public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id) {
      var item = await repo.GetItemAsync(id);

      if ( item is null ) {
        return NotFound();
      }

      return item.AsDto();
    }

    // Post /api/items
    [HttpPost]
    public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto item) {
      Item newItem = new() {
        Id = Guid.NewGuid(),
        Name = item.Name,
        Price = item.Price,
        CreatedDate = DateTimeOffset.Now,
      };

      await repo.CreateItemAsync(newItem);

      return CreatedAtAction(nameof(GetItemAsync), new { id = newItem.Id }, newItem.AsDto());
    }

    // PUT /api/items/:id
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDto item) {
      var existingItem = await repo.GetItemAsync(id);

      if ( existingItem is null ) {
        return NotFound();
      }

      Item updatedItem = existingItem with {
        Name = item.Name,
        Price = item.Price
      };

      await repo.UpdateItemAsync(updatedItem);

      return NoContent();
    }

    // DELETE /api/items/:id
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteItemAsync(Guid id) {
      var item = await repo.GetItemAsync(id);

      if ( item is null ) {
        return NotFound();
      }

      await repo.DeleteItemAsync(id);

      return NoContent();
    }
  }
}
