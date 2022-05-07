using Microsoft.AspNetCore.Mvc;

namespace GameServices.Controllers;
[Route("/")]
public class HomeController : Controller {

  // GET / 
  [HttpGet]
  public IActionResult Index() {
    ViewBag.Title = "Home";
    ViewBag.Welcome = "Game Services .NET 6";
    ViewBag.Year = DateTime.Now.Year;
    return View();
  }
}

