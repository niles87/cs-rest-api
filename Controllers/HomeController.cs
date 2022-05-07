using Microsoft.AspNetCore.Mvc;

namespace GameServices.Controllers;
[Route("/")]
public class HomeController : Controller {

  // GET / 
  [HttpGet]
  public IActionResult Index() {
    ViewBag.Title = "Home";
    ViewBag.Welcome = "Hello World!";
    ViewBag.Year = DateTime.Now.Year;
    return View();
  }
}

