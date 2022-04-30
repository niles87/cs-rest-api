using Microsoft.AspNetCore.Mvc;
using GameServices.Repositories;
using GameServices.Dtos;

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

    // GET /items/:id
    [HttpGet("{id}")]
    public ActionResult<ItemDto> GetItem(Guid id) { 
      var item = repo.GetItem(id);

      if ( item is null ) {
        return NotFound();
      }

      return item.AsDto();
    }
  }
}
