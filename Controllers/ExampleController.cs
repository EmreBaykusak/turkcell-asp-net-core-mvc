using Microsoft.AspNetCore.Mvc;

namespace MyAspNetCoreApp.Web.Controllers;

public class ExampleController : Controller
{
    // GET
    public IActionResult Index()
    {
        var productList = new List<Player>()
        {
            new Player() { Id = 1, Name = "Malik" },
            new Player() { Id = 2, Name = "Beasley" }
        };
        
        ViewBag.name = "Asp.Net Core";

        ViewData["age"] = 30;
        ViewData["names"] = new List<string>() { "josh, edward, anthony" };

        TempData["surname"] = "james";
        
        
        return View(productList);
    }

    public IActionResult Index2()
    {

        string surname = TempData["surname"] as string;
        return View();
    }
    public IActionResult Index3()
    {
        return RedirectToAction("index");
    }

    public IActionResult ParameterView(int id)
    {
        return RedirectToAction("JsonResult", "Example", new { id });
    }
    public IActionResult JsonResult(int id)
    {
        return Json(new { id, name = "pencil" });
    }

    public IActionResult Content()
    {
        return Content("ExampleController");
    }

    public IActionResult Empty()
    {
        return new EmptyResult();
    }

    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    
}

