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
    public IEnumerable<ItemDto> GetItems() {
      var items = repo.GetItems().Select(item => item.AsDto());

      return items;
    }

    // GET /api/items/:id
    [HttpGet("{id}")]
    public ActionResult<ItemDto> GetItem(Guid id) {
      var item = repo.GetItem(id);

      if ( item is null ) {
        return NotFound();
      }

      return item.AsDto();
    }

    // Post /api/items
    [HttpPost]
    public ActionResult<ItemDto> CreateItem(CreateItemDto item) {
      Item newItem = new() {
        Id = Guid.NewGuid(),
        Name = item.Name,
        Price = item.Price,
        CreatedDate = DateTimeOffset.Now,
      };

      repo.CreateItem(newItem);

      return CreatedAtAction(nameof(GetItem), new { id = newItem.Id }, newItem.AsDto());
    }

    // PUT /api/items/:id
    [HttpPut("{id}")]
    public ActionResult UpdateItem(Guid id, UpdateItemDto item) {
      var existingItem = repo.GetItem(id);

      if ( existingItem is null ) {
        return NotFound();
      }

      Item updatedItem = existingItem with {
        Name = item.Name,
        Price = item.Price
      };

      repo.UpdateItem(updatedItem);

      return NoContent();
    }

    // DELETE /api/items/:id
    [HttpDelete("{id}")]
    public ActionResult DeleteItem(Guid id) {
      var item = repo.GetItem(id);

      if ( item is null ) {
        return NotFound();
      }

      repo.DeleteItem(id);

      return NoContent();
    }
  }
}
